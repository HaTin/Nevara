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
    [Authorize]
    [Area("Admin")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;

        public ManufacturerController(IManufacturerService manufacturerService, IProductService productService)
        {
            _manufacturerService = manufacturerService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
       
  

        [HttpGet]
        public async Task<IActionResult> GetManufacturer()
        {
            var model = await  _manufacturerService.GetManufacturers();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> Find(int? id)
        {
            var model = await _manufacturerService.Find(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEntity(ManufacturerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (viewModel.Id == 0)
            {
                await  _manufacturerService.Add(viewModel);
            }
            else
            {
                await  _manufacturerService.Update(viewModel);
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

            if (await _productService.CheckProductAmountInManufacturer(id))
            {
                return new OkObjectResult(new GenericResult() { Success = false, Message = "Please remove all products belonging to this manufacturer" });
            }
            await  _manufacturerService.Remove(id);
            return new OkObjectResult(new GenericResult() { Success = true });
        }
    }
}
