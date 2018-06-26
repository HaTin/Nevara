using Nevara.Dtos;
using Nevara.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Areas.Admin.Interfaces
{
    public interface IProductService
    {
        PageResult<Product> GetProduct(int? categoryId, string keyword, int page, int pageSize);
    }
}
