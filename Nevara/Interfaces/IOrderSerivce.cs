using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Entities;
using Nevara.ViewModel;

namespace Nevara.Interfaces
{
    public interface IOrderSerivce
    {
        Task Create(OrderViewModel orderViewModel);
        Task Update(OrderViewModel orderViewModel);
        Task<OrderDetail> CreateOrderDetail(OrderDetailViewModel orderDetail);
        Task Remove(int orderId);   
    }
}
