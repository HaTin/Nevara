using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nevara.Areas.Admin.Models;
using Nevara.Interfaces;
using Nevara.Services;
using Nevara.ViewModel;

namespace Nevara.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
  
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICollectionService _collectionService;
        private readonly IProductService _productService;
        private readonly IMaterialService _materialService;
        private readonly IColorService _colorService;
        private readonly IManufacturerService _manufacturerService;
        public ProductController(ICategoryService categoryService,
            ICollectionService collectionService, IProductService productService,
            IMaterialService materialService,IColorService colorService,
            IManufacturerService manufacturerService)
        {
            _categoryService = categoryService;
            _collectionService = collectionService;
            _productService = productService;
            _materialService = materialService;
            _colorService = colorService;
            _manufacturerService = manufacturerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(int? categoryId,int? collectionId, string keyword, int page, int pageSize)
        {
            var model = await _productService.GetProduct(categoryId,collectionId, keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var model = await _categoryService.GetCategories();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetCollections()
        {
            var model = await _collectionService.GetCollections();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetMaterials()
        {
            var model = await _materialService.GetMaterials();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var model = await _colorService.GetColors();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> Find(int ? id)
        {
            var model = await _productService.Find(id);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetManufacturers()
        {
            var model = await _manufacturerService.GetManufacturers();
           return new OkObjectResult(model);            
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
        
                if (viewModel.Id == 0)
                {
                    await _productService.Add(viewModel);
                }
                else
                {
                   await _productService.Update(viewModel);
                }
                return new OkObjectResult(viewModel);
            
         
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            await _productService.Remove(id);
            return new OkObjectResult(id);
        }
    }
}