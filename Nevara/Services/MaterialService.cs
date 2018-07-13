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
    public class MaterialService : IMaterialService
    {
        private readonly NevaraDbContext _context;
        public MaterialService(NevaraDbContext context)
        {
            _context = context;
        }
        public async Task<List<MaterialViewModel>> GetMaterials()
        {
            return await _context.Materials.Select(p => new MaterialViewModel()
            {
                Id = p.Id,
                MaterialName = p.MaterialName
            }).ToListAsync();
        }

        public async Task<MaterialViewModel> Find(int? id)
        {
            var model = await _context.Materials.FindAsync(id);
            var viewModel = new MaterialViewModel()
            {
                Id = model.Id,
                MaterialName = model.MaterialName,
            };
            return viewModel;
        }

        public async Task Add(MaterialViewModel viewModel)
        {
            var model = new Material()
            {
                MaterialName = viewModel.MaterialName
            };
            _context.Materials.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Update(MaterialViewModel viewModel)
        {
            var model = await _context.Materials.FindAsync(viewModel.Id);
            model.MaterialName = viewModel.MaterialName;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int? id)
        {
            var model = await _context.Materials.FindAsync(id);
            model.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
