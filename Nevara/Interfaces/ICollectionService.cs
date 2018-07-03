using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Areas.Admin.Models;
using Nevara.ViewModel;

namespace Nevara.Interfaces
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
