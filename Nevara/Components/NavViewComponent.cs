using Microsoft.AspNetCore.Mvc;
using Nevara.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Components
{
    public class NavViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public NavViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cate = await _categoryService.GetCategories();
            return View(cate);
        }
    }
}
