using System.Collections.Generic;
using System.Threading.Tasks;
using Nevara.ApplicationCore.Dtos;
using Nevara.ApplicationCore.Models.Entities;
using Nevara.ApplicationCore.Models.Enum;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Interfaces
{
    public interface IOrderSerivce
    {
        Task<PageResult<OrderViewModel>> GetOrders(int page, int pageSize,string keyword);
        Task<bool> Create(OrderViewModel orderViewModel);
        Task UpdateStatus(int orderId,BillStatus status ); 
        Task Remove(int orderId);
        Task<List<OrderDetailViewModel>> getOrderDetails(int orderId);
        Task<OrderViewModel> getOrder(int orderId);
        Task<bool> CheckProductInOrder(int? id);
        Task<List<OrderViewModel>> GetOrderForCustomer(string customerId);
    }
}
