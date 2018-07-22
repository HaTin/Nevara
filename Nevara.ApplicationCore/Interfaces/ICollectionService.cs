using System.Collections.Generic;
using System.Threading.Tasks;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Interfaces
{
    public interface ICollectionService
    {
       Task<List<CollectionViewModel>> GetCollections();
        Task<CollectionViewModel> Find(int? id);
        Task Add(CollectionViewModel viewModel);
        Task Update(CollectionViewModel viewModel);
        Task Remove(int? id);
        
    }
}
