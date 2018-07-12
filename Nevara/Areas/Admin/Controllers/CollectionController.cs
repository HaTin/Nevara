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
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly IProductService _productService;

        public CollectionController(ICollectionService collectionService, IProductService productService)
        {
            _collectionService = collectionService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCollection()
        {
            var model = await _collectionService.GetCollections();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> Find(int? id)
        {
            var model = await _collectionService.Find(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEntity(CollectionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (viewModel.Id == 0)
            {
                await _collectionService.Add(viewModel);
            }
            else
            {
                await _collectionService.Update(viewModel);
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

            if (await _productService.CheckProductAmountInCollection(id))
            {
                return new OkObjectResult(new GenericResult() { Success = false, Message = "Please remove all products belonging to this collection" });
            }
            await _collectionService.Remove(id);
            return new OkObjectResult(new GenericResult() { Success = true });
        }
    }
}