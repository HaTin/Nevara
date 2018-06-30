using Nevara.Dtos;
using Nevara.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Areas.Admin.Models;
using Nevara.Helpers;
using Nevara.Interfaces;
using StackExchange.Profiling.Internal;

namespace Nevara.Services
{
    public class ProductService : IProductService
    {
        private readonly NevaraDbContext _context;
        public ProductService(NevaraDbContext context)
        {
            _context = context;
        }
        public async Task<PageResult<ProductViewModel>> GetProduct(int? categoryId,int? collectionId, string keyword, int page, int pageSize)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
               
                    query = query.Where(x =>
                        Util.ConvertToUnsign(x.Name).Contains(Util.ConvertToUnsign(keyword), StringComparison.CurrentCultureIgnoreCase));
                
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            if (collectionId.HasValue)
            {
                query = query.Where(x => x.CollectionId == collectionId);
            }
            int totalRow = await query.CountAsync();            
            query = query.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var data = await query.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Price = p.Price,
                    Thumbnail = p.Thumbnail,
                    Quantity = p.Quantity,
                }).
                ToListAsync();
            var paginationSet = new PageResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public async Task<ProductViewModel> Find(int? id)
        {
            var model = await _context.Products.FindAsync(id);        
            var viewModel = new ProductViewModel()
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Name = model.Name,
                Quantity = model.Quantity,
                Width = model.Width,
                Height = model.Height,
                Depth = model.Depth,
                Price = model.Price,
                HomeFlag = model.HomeFlag,
                CollectionId = model.CollectionId,
                ColorId = model.CollectionId,
                HotFlag = model.HotFlag,
                PromotionPrice = model.PromotionPrice,
                ManufacturerId = model.ManufacturerId,
                MaterialId = model.MaterialId,
                NewFlag = model.NewFlag
            };
            return viewModel;
        }
    }
}
