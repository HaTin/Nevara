using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Areas.Admin.Models;
using Nevara.ViewModel;

namespace Nevara.Interfaces
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
