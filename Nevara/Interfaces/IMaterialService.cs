using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Areas.Admin.Models;
using Nevara.ViewModel;

namespace Nevara.Interfaces
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
