using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Models.Enum
{
    public enum PaymentMethod
    {
        [Description("Cash on Delivery")]
        CashOnDelivery,
        [Description("Visa/Master Card")]
        Visa     
    }
}
