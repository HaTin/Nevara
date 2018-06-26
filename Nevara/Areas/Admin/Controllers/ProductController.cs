using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nevara.Interfaces;

namespace Nevara.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetProduct(int? categoryId, string keyword, int page, int pageSize)
        {
            var model = _productService.GetProduct(categoryId, keyword, page, pageSize);
            return new OkObjectResult(model);
        }
    }
}