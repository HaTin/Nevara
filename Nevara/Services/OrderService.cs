using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Interfaces;
using Nevara.Models.Entities;
using Nevara.ViewModel;

namespace Nevara.Services
{
    public class OrderService : IOrderSerivce
    {
        private readonly NevaraDbContext _context;

        public OrderService(NevaraDbContext context)
        {
            _context = context;
        }

        public async Task Create(OrderViewModel viewModel)
        {
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
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
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
    }
}