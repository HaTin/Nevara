using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nevara.Extensions;
using Nevara.Interfaces;
using Nevara.ViewModel;

namespace Nevara.Controllers
{
    public class CartController : Controller
    {
        IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCart()
        {
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if (session != null)
            {
                session = new List<ShoppingCartViewModel>();
            }
            return new OkObjectResult(session);
        }
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("cart");
            return new OkObjectResult("OK");
        }
        public IActionResult AddToCart(int productId,int quantity)
        {
            var product = _productService.Find(productId);
            var session = HttpContext.Session.Get<List<ShoppingCartViewModel>>("cart");
            if (session != null)
            {
            
                if (session.Any(p => p.Product.Id == productId))
                {
                    var item = session.FirstOrDefault(p => p.Product.Id == productId);
                    item.Quantity += quantity;
                }
                else
                {
                    session.Add(new ShoppingCartViewModel()
                    {
                        Product = product.Result,
                        Quantity = quantity,
                        Price = product.Result.PromotionPrice ?? product.Result.Price
                    });
                }
                
            }
            return new OkObjectResult("add");
        }
    }
}