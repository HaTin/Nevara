using Nevara.Areas.Admin.Models;
using Nevara.Dtos;
using Nevara.Models.Entities;

namespace Nevara.Interfaces
{
    public interface IProductService
    {
        PageResult<ProductViewModel> GetProduct(int? categoryId,int? collectionId, string keyword, int page, int pageSize);
    }
}
