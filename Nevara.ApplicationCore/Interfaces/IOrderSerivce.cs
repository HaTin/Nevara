using System.Threading.Tasks;
using Nevara.ApplicationCore.Models.Entities;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Interfaces
{
    public interface IOrderSerivce
    {
        Task<bool> Create(OrderViewModel orderViewModel);
        Task Update(OrderViewModel orderViewModel);
        Task<OrderDetail> CreateOrderDetail(OrderDetailViewModel orderDetail);
        Task Remove(int orderId);
        Task<bool> CheckProductInOrder(int? id);
    }
}
