using System.Collections.Generic;
using System.Threading.Tasks;
using Nevara.ApplicationCore.ViewModel;

namespace Nevara.ApplicationCore.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorViewModel>> GetColors();
        Task<ColorViewModel> Find(int? id);
        Task Add(ColorViewModel viewModel);
        Task Update(ColorViewModel viewModel);
        Task Remove(int? id);
    }
}
