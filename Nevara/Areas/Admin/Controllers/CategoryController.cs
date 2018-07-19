using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nevara.ApplicationCore.Dtos;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var model = await _categoryService.GetCategories();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> Find(int? id)
        {
            var model = await _categoryService.Find(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEntity(CategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {                
                return new BadRequestResult();
            }

            if (viewModel.Id == 0)
            {
                await _categoryService.Add(viewModel);
            }
            else
            {
                await _categoryService.Update(viewModel);
            }
            return new OkObjectResult(viewModel);


        }
        [HttpPost]        
        public async Task<IActionResult> Remove(int? id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (await _productService.CheckProductAmountInCategory(id))
            {
                return new OkObjectResult(new GenericResult(){Success = false,Message = "Please remove all products belonging to this category"});
            }
            await _categoryService.Remove(id);
            return new OkObjectResult(new GenericResult(){Success = true});
        }
    }
}