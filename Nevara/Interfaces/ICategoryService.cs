using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Entities;
using Nevara.ViewModel;

namespace Nevara.Interfaces
{
    public interface ICategoryService
    {
      Task<List<CategoryViewModel>> GetCategories();
    }
}
