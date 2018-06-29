using System.Threading.Tasks;
using Nevara.Areas.Admin.Models;
using Nevara.Dtos;
using Nevara.Models.Entities;

namespace Nevara.Interfaces
{
    public interface IProductService
    {
       Task<PageResult<ProductViewModel>> GetProduct(int? categoryId,int? collectionId, string keyword, int page, int pageSize);
       Task<ProductViewModel> Find(int? id);
    }
}
