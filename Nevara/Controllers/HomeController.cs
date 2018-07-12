using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nevara.Interfaces;

namespace Nevara.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var proList = await _productService.GetProductList();
            return View(proList);
        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            var proDetail = await _productService.GetProducDetail(id);
            return View(proDetail);
        }

        public async Task<IActionResult> Categories(int? id)
        {
            ViewBag.Categories = await _categoryService.GetCategories();
            if (id == null)
            {
                id = 1;
            }
            ViewBag.Products = await _productService.GetProductByCategories(id);
            return View();
        }
    }
}