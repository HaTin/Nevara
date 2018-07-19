using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nevara.ApplicationCore.Interfaces;

namespace Nevara.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ICategoryService _categoryService;
        public NavigationController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var cate = await _categoryService.GetCategories();
            return View(cate);
        }
    }
}