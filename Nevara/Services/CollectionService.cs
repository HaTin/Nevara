using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Areas.Admin.Models;
using Nevara.Interfaces;

namespace Nevara.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly NevaraDbContext _context;
        public CollectionService(NevaraDbContext context)
        {
            _context = context;
        }
        public async Task<List<CollectionViewModel>> GetCollections()
        {
            return await _context.Collections.Select(p => new CollectionViewModel()
            {
                Id = p.Id,
                CollectionName = p.CollectionName
            }).ToListAsync();
        }
    }
}
