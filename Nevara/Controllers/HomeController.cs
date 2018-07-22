using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.ViewModel;

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
            var homeProducts = await _productService.GetHomeProducts();
            return View(homeProducts);
        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            var proDetail = await _productService.GetProducDetail(id);
            return View(proDetail);
        }

        public async Task<IActionResult> Categories(int? id)
        {
            var productVm = await _productService.GetProductByCategories(id);
            ViewBag.Categories = await _categoryService.GetCategories();                   
            return View(productVm);
        }
        public async Task<IActionResult> Collections(int? id)
        {
            var productVm = await _productService.getProductByCollections(id);
            ViewBag.Categories = await _categoryService.GetCategories();
            return View(productVm);
        }
        public async Task<IActionResult> Search(string keyword ,int page = 1)
        {
            ViewBag.Categories = await _categoryService.GetCategories();
            var searchViewModel = new SearchResultViewModel
            {                
                Data = await _productService.GetProduct(null, null, keyword, page, 3),
                Keyword = keyword
                
            };
            return View(searchViewModel);
        }
    }
}