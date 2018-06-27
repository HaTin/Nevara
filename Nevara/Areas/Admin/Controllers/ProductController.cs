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
        private readonly ICategoryService _categoryService;
        private readonly ICollectionService _collectionService;
        private readonly IProductService _productService;

        public ProductController(ICategoryService categoryService, ICollectionService collectionService, IProductService productService)
        {
            _categoryService = categoryService;
            _collectionService = collectionService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetProduct(int? categoryId,int? collectionId, string keyword, int page, int pageSize)
        {
            var model = _productService.GetProduct(categoryId,collectionId, keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var model = _categoryService.GetCategories();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetCollections()
        {
            var model = _collectionService.GetCollections();
            return new OkObjectResult(model);
        }
    }
}