using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.Components
{
    public class NavViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly ICollectionService _collectionService;

        public NavViewComponent(ICategoryService categoryService, ICollectionService collectionService)
        {
            _categoryService = categoryService;
            _collectionService = collectionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navVM = new NavViewModel();
            navVM.Categories = await _categoryService.GetCategories();
            navVM.Collections = await _collectionService.GetCollections();
            return View(navVM);
        }
    }
}
