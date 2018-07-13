using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nevara.Dtos;
using Nevara.Interfaces;
using Nevara.ViewModel;

namespace Nevara.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;
        private readonly IProductService _productService;

        public MaterialController(IMaterialService materialService, IProductService productService)
        {
            _materialService = materialService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMaterial()
        {
            var model = await _materialService.GetMaterials();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> Find(int? id)
        {
            var model = await _materialService.Find(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEntity(MaterialViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (viewModel.Id == 0)
            {
                await _materialService.Add(viewModel);
            }
            else
            {
                await _materialService.Update(viewModel);
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

            if (await _productService.CheckProductAmountInMaterial(id))
            {
                return new OkObjectResult(new GenericResult() { Success = false, Message = "Please remove all products belonging to this material" });
            }
            await _materialService.Remove(id);
            return new OkObjectResult(new GenericResult() { Success = true });
        }
    }
}