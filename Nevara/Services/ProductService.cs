using Nevara.Dtos;
using Nevara.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nevara.Helpers;
using Nevara.ViewModel;
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
            var query = _context.Products.Where(p => !p.IsDeleted).AsQueryable();            
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

        public async Task<List<ProductViewModel>> GetProductList()
        {
            var model = await _context.Products.Where(p => p.HomeFlag == true).Where(p => !p.IsDeleted).Take(10).Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Thumbnail = p.Thumbnail
                }).ToListAsync();
            return model;
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
                    NewFlag = model.NewFlag,
                    OriginalPrice = model.OriginalPrice ,                                       
                };
                return viewModel;
        }

      
        public async Task Add(ProductViewModel pro)
        {
            var product = new Product()
            {
                Id = pro.Id,
                Name = pro.Name,
                CategoryId = pro.CategoryId,
                CollectionId = pro.CollectionId,
                ColorId = pro.ColorId,
                MaterialId = pro.MaterialId,
                Depth = pro.Depth,
                Height = pro.Height,
                Width = pro.Width,
                HomeFlag = pro.HomeFlag,
                HotFlag = pro.HotFlag,
                OriginalPrice = pro.OriginalPrice,
                PromotionPrice = pro.PromotionPrice,
                Price = pro.Price,
                NewFlag = pro.NewFlag,
                ManufacturerId = pro.ManufacturerId,
                Quantity = pro.Quantity,                
                IsDeleted = false,
                Description = "No Description",
                Thumbnail = "/images/1.png",                
            };
      
                _context.Add(product);                     
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductViewModel pro)
        {
            /*var product = new Product()
            {
                Id =pro.Id,
                Name = pro.Name,
                CategoryId = pro.CategoryId,
                CollectionId = pro.CollectionId,
                ColorId = pro.ColorId,
                MaterialId = pro.MaterialId,
                Depth = pro.Depth,
                Height = pro.Height,
                Width = pro.Width,
                HomeFlag = pro.HomeFlag,
                HotFlag = pro.HotFlag,
                OriginalPrice = pro.OriginalPrice,
                PromotionPrice = pro.PromotionPrice,
                Price = pro.Price,
                NewFlag = pro.NewFlag,
                ManufacturerId = pro.ManufacturerId               
            };*/
            var prod = await _context.Products.FindAsync(pro.Id);
            prod.Name = pro.Name;
            prod.CategoryId = pro.CategoryId;
            prod.CollectionId = pro.CollectionId;
            prod.ColorId = pro.ColorId;
            prod.MaterialId = pro.MaterialId;
            prod.Depth = pro.Depth;
            prod.Height = pro.Height;
            prod.Width = pro.Width;
            prod.HomeFlag = pro.HomeFlag;
            prod.HotFlag = pro.HotFlag;
            prod.OriginalPrice = pro.OriginalPrice;
            prod.PromotionPrice = pro.PromotionPrice;
            prod.Price = pro.Price;
            prod.NewFlag = pro.NewFlag;
            prod.ManufacturerId = pro.ManufacturerId;
            prod.Quantity = pro.Quantity;            
           await _context.SaveChangesAsync();
        }

        public async Task Remove(int? id)
        {
            Product pro = await _context.Products.FindAsync(id);
            pro.IsDeleted = true;
            await _context.SaveChangesAsync();

        }

       

       

       

     


     

       
    }
}
