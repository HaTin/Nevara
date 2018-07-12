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

namespace Nevara.Services
{
    public class ProductService : IProductService
    {
        private readonly NevaraDbContext _context;
        public ProductService(NevaraDbContext context)
        {
            _context = context;
        }
        public async Task<PageResult<ProductViewModel>> GetProduct(int? categoryId, int? collectionId, string keyword, int page, int pageSize)
        {
            var query = _context.Products.AsQueryable().IgnoreQueryFilters().Where(p => !p.IsDeleted);
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
                Length = model.Length,
                Height = model.Height,
                Depth = model.Depth,
                Price = model.Price,
                HomeFlag = model.HomeFlag,
                CollectionId = model.CollectionId,
                ColorId = model.CollectionId,
                HotFlag = model.HotFlag,
                Description = model.Description,
                PromotionPrice = model.PromotionPrice,
                ManufacturerId = model.ManufacturerId,
                MaterialId = model.MaterialId,
                NewFlag = model.NewFlag,
                OriginalPrice = model.OriginalPrice,
                Thumbnail = model.Thumbnail
            };
            return viewModel;
        }


        public async Task Add(ProductViewModel pro)
        {
            var product = new Product()
            {
                Name = pro.Name,
                CategoryId = pro.CategoryId,
                CollectionId = pro.CollectionId,
                ColorId = pro.ColorId,
                MaterialId = pro.MaterialId,
                Depth = pro.Depth,
                Height = pro.Height,
                Length = pro.Length,
                HomeFlag = pro.HomeFlag,
                HotFlag = pro.HotFlag,
                OriginalPrice = pro.OriginalPrice,
                PromotionPrice = pro.PromotionPrice,
                Price = pro.Price,
                NewFlag = pro.NewFlag,
                ManufacturerId = pro.ManufacturerId,
                Quantity = pro.Quantity,
                IsDeleted = false,
                Description = pro.Description,
                Thumbnail = pro.Thumbnail ?? "/images/1.png",
            };

            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductViewModel pro)
        {
            var prod = await _context.Products.FindAsync(pro.Id);
            prod.Name = pro.Name;
            prod.CategoryId = pro.CategoryId;
            prod.CollectionId = pro.CollectionId;
            prod.ColorId = pro.ColorId;
            prod.MaterialId = pro.MaterialId;
            prod.Depth = pro.Depth;
            prod.Height = pro.Height;
            prod.Length = pro.Length;
            prod.HomeFlag = pro.HomeFlag;
            prod.HotFlag = pro.HotFlag;
            prod.OriginalPrice = pro.OriginalPrice;
            prod.PromotionPrice = pro.PromotionPrice;
            prod.Price = pro.Price;
            prod.NewFlag = pro.NewFlag;
            prod.ManufacturerId = pro.ManufacturerId;
            prod.Quantity = pro.Quantity;
            prod.Description = pro.Description;
            prod.Thumbnail = pro.Thumbnail;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int? id)
        {
            Product pro = await _context.Products.FindAsync(id);
            pro.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckProductAmountInCategory(int? id)
        {
            if (await _context.Products.AnyAsync(p => p.CategoryId == id))
                return true;
            return false;
        }

        public async Task<bool> CheckProductAmountInMaterial(int? id)
        {
            if (await _context.Products.AnyAsync(p => p.MaterialId == id))
                return true;
            return false;
        }

        public async Task<bool> CheckProductAmountInManufacturer(int? id)
        {
            if (await _context.Products.AnyAsync(p => p.ManufacturerId == id))
                return true;
            return false;
        }

        public async Task<bool> CheckProductAmountInCollection(int? id)
        {
            if (await _context.Products.AnyAsync(p => p.CollectionId == id))
                return true;
            return false;
        }

        public async Task<bool> CheckProductAmountInColor(int? id)
        {
            if (await _context.Products.AnyAsync(p => p.ColorId == id))
                return true;
            return false;
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

        public async Task<ProductViewModel> GetProducDetail(int? productId)
        {
            var model = await _context.Products.Include(p=>p.Collection).Include(p=>p.Category).
                Include(p => p.Material).Include(p => p.Manufacturer).Include(p=>p.Color).
                FirstOrDefaultAsync(p => p.Id == productId);
            var viewMdodel = new ProductViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Quantity = model.Quantity,
                PromotionPrice = model.PromotionPrice,
                CategoryName = model.Category.Name,
                MaterialName = model.Material.MaterialName,
                CollectionName = model.Collection.CollectionName,
                ManufacturerName = model.Manufacturer.ManufacturerName,
                ColorName = model.Color.ColorName,
                Length = model.Length,
                Depth = model.Depth,
                Height = model.Height,                
                Images = await _context.Images.Where(p => p.ProductId == productId).Select(p => new Image()
                {
                    ImagePath = p.ImagePath
                }).ToListAsync()
            };
            
            return viewMdodel;
        }

        public async Task<List<ProductViewModel>> GetProductByCategories(int? cateId)
        {
            var result = await _context.Products.Where(p => p.CategoryId == cateId).Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Thumbnail = p.Thumbnail,
                HotFlag = p.HotFlag,
                NewFlag = p.NewFlag,
                PromotionPrice = p.PromotionPrice
            }).ToListAsync();
            return result;
        }

    

       
       
    }
}
