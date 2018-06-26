using Nevara.Areas.Admin.Interfaces;
using Nevara.Dtos;
using Nevara.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nevara.Services
{
    public class ProductService : IProductService
    {
        private readonly NevaraDbContext _context;
        public ProductService(NevaraDbContext context)
        {
            _context = context;
        }
        public PageResult<Product> GetProduct(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _context.Products.Where(p => p.IsDeleted == false).Select(p => new Product()
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Thumbnail = p.Thumbnail,
                Quantity = p.Quantity,
            });
   
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
            var data = query.ToList();
            var paginationSet = new PageResult<Product>()
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
