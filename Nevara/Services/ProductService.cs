using Nevara.Dtos;
using Nevara.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Areas.Admin.Models;
using Nevara.Interfaces;

namespace Nevara.Services
{
    public class ProductService : IProductService
    {
        private readonly NevaraDbContext _context;
        public ProductService(NevaraDbContext context)
        {
            _context = context;
        }
        public PageResult<ProductViewModel> GetProduct(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            int totalRow = query.Count();            
            query = query.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            var data = query.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Price = p.Price,
                    Thumbnail = p.Thumbnail,
                    Quantity = p.Quantity,
                }).
                ToList();
            var paginationSet = new PageResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }
    }
}
