using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Nevara.ApplicationCore.Interfaces;
using Nevara.ApplicationCore.Models.Entities;
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

        public async Task<bool> Create(OrderViewModel viewModel)
        {
            bool check = true;
            var order = new Order()
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
            public Task Update(OrderViewModel orderViewModel)
            {
                throw new NotImplementedException();
            }

            public Task<OrderDetail> CreateOrderDetail(OrderDetailViewModel orderDetail)
            {
                throw new NotImplementedException();
            }

            public Task Remove(int orderId)
            {
                throw new NotImplementedException();
            }

            public async Task<bool> CheckProductInOrder(int? id)
            {
                if (await _context.OrderDetails.AnyAsync(p => p.ProductId == id))
                    return true;
                return false;
            }
        }
    }