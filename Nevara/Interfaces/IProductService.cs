using System.Collections.Generic;
using System.Threading.Tasks;
using Nevara.Dtos;
using Nevara.Models.Entities;
using Nevara.ViewModel;

namespace Nevara.Interfaces
{
    public interface IProductService
    {
       Task<PageResult<ProductViewModel>> GetProduct(int? categoryId,int? collectionId, string keyword, int page, int pageSize);
       Task<ProductViewModel> Find(int? id);
        Task Add(ProductViewModel productViewModel);
        Task Update(ProductViewModel productViewModel);
        Task Remove(int? id);
        Task<List<ProductViewModel>> GetProductList();
        Task<ProductViewModel> GetProducDetail(int? productID);
    }
}
