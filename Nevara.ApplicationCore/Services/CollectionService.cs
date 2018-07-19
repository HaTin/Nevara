using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.Models.Entities;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Services
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

        public async Task<CollectionViewModel> Find(int? id)
        {
            var model = await _context.Collections.FindAsync(id);
            var viewModel = new CollectionViewModel(){
                Id = model.Id,
                CollectionName = model.CollectionName,
                Description =  model.Description,
                Image = model.Image
            };
            return viewModel;
        }

        public async Task Add(CollectionViewModel viewModel)
        {
            var model = new Collection
            {
                CollectionName = viewModel.CollectionName,
                Description = viewModel.Description,
                Image = viewModel.Image ?? "/images/1.png"
            };
            _context.Collections.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CollectionViewModel viewModel)
        {
            var model = await _context.Collections.FindAsync(viewModel.Id);
            model.CollectionName = viewModel.CollectionName;
            model.Description = viewModel.Description;
            model.Image = viewModel.Image;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int? id)
        {
            var model = await _context.Collections.FindAsync(id);
            model.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
