using System.Collections.Generic;
using System.Threading.Tasks;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Interfaces
{
    public interface ICategoryService
    {
      Task<List<CategoryViewModel>> GetCategories();
        Task<CategoryViewModel> Find(int? id);
        Task Add(CategoryViewModel categoryViewModel);
        Task Update(CategoryViewModel categoryViewModel);
        Task Remove(int? id);
    }
}
