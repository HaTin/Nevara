using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Areas.Admin.Models;
using Nevara.Interfaces;
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

        public Task<CollectionViewModel> Find(int? id)
        {
            throw new NotImplementedException();
        }

        public Task Add(ColorViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task Update(ColorViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
