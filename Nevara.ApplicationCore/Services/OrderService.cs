using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nevara.ApplicationCore.Dtos;
using Nevara.ApplicationCore.Helpers;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.Models.Entities;
using Nevara.ApplicationCore.Models.Enum;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Services
{
    public class OrderService : IOrderSerivce

    {
        private readonly NevaraDbContext _context;

        public OrderService(NevaraDbContext context)
        {
            _context = context;
        }

        public async Task<PageResult<OrderViewModel>> GetOrders(int page, int pageSize,string keyword)
        {            
            var query = _context.Orders.AsQueryable().IgnoreQueryFilters().Where(p => !p.IsDeleted);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x =>
                    Util.ConvertToUnsign(x.CustomerName).Contains(Util.ConvertToUnsign(keyword),
                        StringComparison.CurrentCultureIgnoreCase));
            }
            int totalRow = await query.CountAsync();
            query = query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            var data = await query.Select(p => new OrderViewModel
            {
                Id = p.Id,
                CustomerName = p.CustomerName,
                BillStatus = p.BillStatus,
                PaymentMethod = p.PaymentMethod,
                CreatedDate = p.CreatedDate               
            }).ToListAsync();

            var result = new PageResult<OrderViewModel>
            {
                CurrentPage = page,
                Results = data,
                PageSize = pageSize,
                RowCount = totalRow                          
            };
            return result;
        }

        public async Task<bool> Create(OrderViewModel viewModel)
        {
            var check = true;
            var order = new Order
            {
                CustomerName = viewModel.CustomerName,
                CustomerEmail = viewModel.CustomerEmail,
                BillStatus = viewModel.BillStatus,
                CreatedDate = DateTime.Now,
                CustomerAddress = viewModel.CustomerAddress,
                CustomerMessage = viewModel.CustomerMessage ?? string.Empty,
                CustomerMobile = viewModel.CustomerMobile,
                PaymentMethod = viewModel.PaymentMethod,
                UserId = viewModel.UserId
            };
            var orderDetails = viewModel.DetailViewModels.Select(p => new OrderDetail()
            {
                OrderId = p.OrderId,
                Quantity = p.Quantity,
                Price = p.Price,
                ProductId = p.ProductId
            }).ToList();
            order.OrderDetails = orderDetails;
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    foreach (var item in viewModel.DetailViewModels)
                    {
                        var product = await _context.Products.FindAsync(item.ProductId);
                        product.Quantity -= item.Quantity; 
                    }
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    check = false;
                }
                return check;
            }
        }

        public async Task UpdateStatus(int orderId, BillStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            order.BillStatus = status;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int orderId)
            {
                var order = await _context.Orders.FindAsync(orderId);
                order.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

        public async Task<List<OrderDetailViewModel>> getOrderDetails(int orderId)
        {
            var orderDetails = await _context.OrderDetails.Where(p => p.OrderId == orderId)
                .Select(p => new OrderDetailViewModel
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Price = p.Price
                }).ToListAsync();
            return orderDetails;
        }


        public async Task<OrderViewModel> getOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.Id == orderId);
            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                BillStatus = order.BillStatus,
                CustomerAddress = order.CustomerAddress,
                CustomerMessage = order.CustomerMessage,
                CustomerMobile = order.CustomerMobile,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,                
                PaymentMethod = order.PaymentMethod,
                CreatedDate = order.CreatedDate
                
            };
            var orderDetails = await _context.OrderDetails.Where(p => p.OrderId == orderId).              
                Select(p => new OrderDetail
                {                    
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Product = new Product{ Name = p.Product.Name}
                }).ToListAsync();
            var orderDetailViewModels = orderDetails.Select(p => new OrderDetailViewModel
            {
                Price = p.Price,
                Quantity = p.Quantity,
                ProductViewModel = new ProductViewModel {Name = p.Product.Name}
            }).ToList();
            orderViewModel.DetailViewModels = orderDetailViewModels;
            return orderViewModel;
        }

        public async Task<bool> CheckProductInOrder(int? id)
        {
            return await _context.OrderDetails.AnyAsync(p => p.ProductId == id);
        }

        public async Task<List<OrderViewModel>> GetOrderForCustomer(string userId)
        {        
            var data = await _context.Orders.Where(p => p.UserId == new Guid(userId)).Select(p => new OrderViewModel
            {
                Id = p.Id,                
                BillStatus = p.BillStatus,
                PaymentMethod = p.PaymentMethod,
                CreatedDate = p.CreatedDate               
            }).ToListAsync();
            return data;
        }
    }
    }