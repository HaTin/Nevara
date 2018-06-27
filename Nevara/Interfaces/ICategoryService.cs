using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Areas.Admin.Models;
using Nevara.Models.Entities;

namespace Nevara.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryViewModel> GetCategories();
    }
}
