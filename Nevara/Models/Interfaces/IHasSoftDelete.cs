using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Models.Interfaces
{
    public interface IHasSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
