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
    public class ManufacturerService : IManufacturerService
    {
        private readonly NevaraDbContext _context;
        public ManufacturerService(NevaraDbContext context)
        {
            _context = context;
        }
        public async Task<List<ManufacturerViewModel>> GetManufacturers()
        {
            return await _context.Manufacturers.Select(p => new ManufacturerViewModel()
            {
                Id = p.Id,
                ManufacturerName = p.ManufacturerName
            }).ToListAsync();
        }

        public Task<ManufacturerViewModel> Find(int? id)
        {
            throw new NotImplementedException();
        }

        public Task Add(ManufacturerViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task Update(ManufacturerViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
