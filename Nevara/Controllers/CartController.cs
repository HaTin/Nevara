using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nevara.ApplicationCore.Dtos;
using Nevara.ApplicationCore.Extensions;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.Models.Enum;
using Nevara.ApplicationCore.ViewModel;


namespace Nevara.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderSerivce _orderSerivce;

        public CartController(IProductService productService, IOrderSerivce orderSerivce)
        {
            _productService = productService;
            _orderSerivce = orderSerivce;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if (session == null) return RedirectToAction("index");           
                foreach (var item in session)
                {
                    if (item.Quantity > item.Product.Quantity)
                    {
                        TempData["Failed"] = "items's current quantity is not allowed to checkout";
                        return RedirectToAction("index");
                    }
                }
            var model = new CheckoutViewModel {Carts = session};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
                if (session != null)
                {
                    var orderDetails = new List<OrderDetailViewModel>();
                    foreach (var item in session)
                    {                        
                      orderDetails.Add(new OrderDetailViewModel()
                      {
                          Price = item.Price,
                          Quantity = item.Quantity,
                          ProductId = item.Product.Id                          
                      });
                    }

                    var orderViewModel = new OrderViewModel()
                    {
                        CustomerName = model.CustomerName,
                        CustomerAddress = model.CustomerAddress,
                        CustomerEmail = model.CustomerEmail,
                        CustomerMobile = model.CustomerMobile,                        
                        BillStatus = BillStatus.New,
                        CustomerMessage = model.CustomerMessage,
                        DetailViewModels = orderDetails,
                        PaymentMethod = model.PaymentMethod,                        
                    };
                    if (User.Identity.IsAuthenticated == true)
                    {
                        orderViewModel.UserId = Guid.Parse(User.GetClaim("UserId"));
                    }                            
                        bool check = await _orderSerivce.Create(orderViewModel);             
                    if(check){
                        ViewData["Success"] = true;
                        HttpContext.Session.Remove("cart");                 
                       }else{
                        ViewData["Success"] = false;             
                    }
                    
                }
            }        
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart") ??
                          new List<ShoppingCartViewModel>();
            return new OkObjectResult(session);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _productService.Find(productId);
            var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if (session != null)
            {
                if (session.Any(p => p.Product.Id == productId))
                {
                    var item = session.FirstOrDefault(p => p.Product.Id == productId);
                    if (item != null) item.Quantity += quantity;
                }
                else
                {
                    session.Add(new ShoppingCartViewModel()
                    {
                        Product = product,
                        Quantity = quantity,
                        Price = product.PromotionPrice ?? product.Price
                    });
                }

                await HttpContext.Session.Set("cart", session);
            }
            else
            {
                var cart = new List<ShoppingCartViewModel>
                {
                    new ShoppingCartViewModel()
                    {
                        Product = product,
                        Quantity = quantity,
                        Price = product.PromotionPrice ?? product.Price
                    }
                };
                await HttpContext.Session.Set("cart", cart);
            }

            return new OkObjectResult(productId);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if (session != null)
            {
                var item = session.FirstOrDefault(p => p.Product.Id == productId);
                session.Remove(item);
                if (session.Count == 0) session = null;
                await HttpContext.Session.Set("cart", session);
                return new OkObjectResult(productId);
            }

            return new EmptyResult();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(int productId, int quantity)
        {
            var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if (session != null && quantity > 0)
            {
                var productQuantity = await _productService.GetAvailableQuantity(productId);
                var item = session.FirstOrDefault(p => p.Product.Id == productId);
                if (item != null)
                {
                    if (quantity <= productQuantity)
                    {
                        item.Quantity = quantity;
                        await HttpContext.Session.Set("cart", session);
                        return new OkObjectResult(new GenericResult() {Success = true});
                    }
                        item.Quantity = productQuantity;
                        await HttpContext.Session.Set("cart", session);
                        return new OkObjectResult(new GenericResult()
                        {
                            Success = false,
                            Message = "Item's quantity exceeds available quantity",
                            Data = productQuantity
                        });                    
                }
            }
            return new EmptyResult();
        }
    }
}