using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Areas.Admin.Models;
using Nevara.Interfaces;
using Nevara.Models.Entities;

namespace Nevara.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NevaraDbContext _context;
        public CategoryService(NevaraDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await _context.Categories.Select(p => new CategoryViewModel()
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
        }
        
    }
}
