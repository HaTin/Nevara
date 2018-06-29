using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Areas.Admin.Models;
using Nevara.Interfaces;

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
    }
}
