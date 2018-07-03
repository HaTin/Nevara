using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Areas.Admin.Models;
using Nevara.ViewModel;

namespace Nevara.Interfaces
{
    public interface IColorService
    {
        Task<List<ColorViewModel>> GetColors();
        Task<CollectionViewModel> Find(int? id);
        Task Add(ColorViewModel viewModel);
        Task Update(ColorViewModel viewModel);
        Task Remove(int? id);
    }
}
