using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Areas.Admin.Models;
using Nevara.Interfaces;
using Nevara.Models.Entities;
using Nevara.ViewModel;

namespace Nevara.Services
{
    public class ColorService : IColorService
    {
        private readonly NevaraDbContext _context;

        public ColorService(NevaraDbContext context)
        {
            _context = context;
        }

        public async Task<List<ColorViewModel>> GetColors()
        {
            return await _context.Colors.Select(p => new ColorViewModel()
            {
                Id = p.Id,
                ColorName = p.ColorName,
                Code = p.Code
            }).ToListAsync();
        }

        public async Task<ColorViewModel> Find(int? id)
        {
            var model = await _context.Colors.FindAsync(id);
            var viewModel = new ColorViewModel()
            {
                Id = model.Id,
                ColorName = model.ColorName,
                Code = model.Code
            };
            return viewModel;
        }
        public async Task Add(ColorViewModel viewModel)
        {
            var model = new Color()
            {    
                ColorName = viewModel.ColorName,
                Code = viewModel.Code
            };
            _context.Colors.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ColorViewModel viewModel)
        {
            var model = await _context.Colors.FindAsync(viewModel.Id);
            model.ColorName = viewModel.ColorName;
            model.Code = viewModel.Code;            
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int? id)
        {
            var model = await _context.Colors.FindAsync(id);
            model.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
