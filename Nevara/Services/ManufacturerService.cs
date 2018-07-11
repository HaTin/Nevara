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

        public  async Task<ManufacturerViewModel> Find(int? id)
        {

            var model = await _context.Manufacturers.FindAsync(id);
            var viewModel = new ManufacturerViewModel()
            {
                Id = model.Id,
                ManufacturerName = model.ManufacturerName
            };
            return viewModel;
        }

        public async Task Add(ManufacturerViewModel viewModel)
        {
            var model = new Manufacturer()
            {
                ManufacturerName = viewModel.ManufacturerName
            };
            _context.Manufacturers.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ManufacturerViewModel viewModel)
        {
            var model = await _context.Manufacturers.FindAsync(viewModel.Id);
            model.ManufacturerName = viewModel.ManufacturerName;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int? id)
        {
            var model = await _context.Manufacturers.FindAsync(id);
            model.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
