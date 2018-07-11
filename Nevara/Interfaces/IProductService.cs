using System.Threading.Tasks;
using Nevara.Areas.Admin.Models;
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
        Task<bool> CheckProductAmountInCategory(int? id);
        Task<bool> CheckProductAmountInMaterial(int? id);
        Task<bool> CheckProductAmountInManufacturer(int? id);
        Task<bool> CheckProductAmountInCollection(int? id);
        Task<bool> CheckProductAmountInColor(int? id);
    }
}
