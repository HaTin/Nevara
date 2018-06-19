using System;
using System.Collections.Generic;
using System.Text;
using Nevara.Data.Enum;

namespace Nevara.Data.Interfaces
{
    public interface ISwitchable
    {
        Status status { get; set; }
    }
}
