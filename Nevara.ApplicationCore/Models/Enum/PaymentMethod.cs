using System.ComponentModel;

namespace Nevara.ApplicationCore.Models.Enum
{
    public enum PaymentMethod
    {
        [Description("Cash on Delivery")]
        CashOnDelivery,
        [Description("Visa/Master Card")]
        Visa     
    }
}
