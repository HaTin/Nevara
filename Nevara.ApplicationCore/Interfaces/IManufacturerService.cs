using System.Collections.Generic;
using System.Threading.Tasks;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Interfaces
{
    public interface IManufacturerService
    {
        Task<List<ManufacturerViewModel>> GetManufacturers();
        Task<ManufacturerViewModel> Find(int? id);
        Task Add(ManufacturerViewModel viewModel);
        Task Update(ManufacturerViewModel viewModel);
        Task Remove(int? id);
    }
}
