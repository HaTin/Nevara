using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nevara.ApplicationCore.Dtos;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;
        private readonly IProductService _productService;

        public ColorController(IColorService colorService, IProductService productService)
        {
            _colorService = colorService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetColor()
        {
            var model = await _colorService.GetColors();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> Find(int? id)
        {
            var model = await _colorService.Find(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEntity(ColorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (viewModel.Id == 0)
            {
                await _colorService.Add(viewModel);
            }
            else
            {
                await _colorService.Update(viewModel);
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

            if (await _productService.CheckProductAmountInColor(id))
            {
                return new OkObjectResult(new GenericResult() { Success = false, Message = "Please remove all products belonging to this color" });
            }
            await _colorService.Remove(id);
            return new OkObjectResult(new GenericResult() { Success = true });
        }
    }
}
