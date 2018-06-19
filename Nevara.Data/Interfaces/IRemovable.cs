using System;
using System.Collections.Generic;
using System.Text;

namespace Nevara.Data.Interfaces
{
    interface IRemovable
    {
        bool? IsRemoved { get; set; }
    }
}
