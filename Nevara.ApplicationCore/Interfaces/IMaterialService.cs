using System.Collections.Generic;
using System.Threading.Tasks;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Interfaces
{
    public interface IMaterialService
    {
        Task<List<MaterialViewModel>> GetMaterials();
        Task<MaterialViewModel> Find(int? id);
        Task Add(MaterialViewModel viewModel);
        Task Update(MaterialViewModel viewModel);
        Task Remove(int? id);
    }
}
