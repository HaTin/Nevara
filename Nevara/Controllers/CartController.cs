using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nevara.Dtos;
using Nevara.Extensions;
using Nevara.Interfaces;
using Nevara.ViewModel;

namespace Nevara.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart") ?? new List<ShoppingCartViewModel>();
            return new OkObjectResult(session);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId,int quantity)
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
                await HttpContext.Session.Set("cart", session);
                return new OkObjectResult(productId);
            }
            return new EmptyResult();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int productId,int quantity)
        {
            var session = await HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if (session != null && quantity > 0)
            {                
                var productQuantity = await _productService.GetAvailableQuantity(productId);                
                if(quantity <= productQuantity){
                var item = session.FirstOrDefault(p => p.Product.Id == productId);
                if (item != null) item.Quantity = quantity;
                await HttpContext.Session.Set("cart", session);
                return new OkObjectResult(new GenericResult(){ Success = true});
                }
                else{
               return new OkObjectResult(new GenericResult(){ Success = false, Message ="Item's quantity exceeds available quantity",Data = productQuantity });
                }
            }
            return new EmptyResult();
        }
    }
}