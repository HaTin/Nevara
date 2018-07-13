using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Interfaces;
using Nevara.ViewModel;
using Nevara.Models.Entities;
using Nevara.ViewModel;

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

        public async Task<CategoryViewModel> Find(int? id)
        {

            var model = await _context.Categories.FindAsync(id);
            var viewModel = new CategoryViewModel()
            {
                Id = model.Id,
                Name = model.Name,
            };
            return viewModel;
        }

        public async Task Add(CategoryViewModel categoryViewModel)
        {
            var category = new Category()
            {                
                Name = categoryViewModel.Name
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public  async Task Update(CategoryViewModel categoryViewModel)
        {
            var category = await _context.Categories.FindAsync(categoryViewModel.Id);
            category.Name = categoryViewModel.Name;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int? id)
        {               
            var category = await _context.Categories.FindAsync(id);
            category.IsDeleted = true;
            await _context.SaveChangesAsync();            
        }    
    }
}
