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

        public Task<MaterialViewModel> Find(int? id)
        {
            throw new NotImplementedException();
        }

        public Task Add(MaterialViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task Update(MaterialViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
