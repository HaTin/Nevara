using System.ComponentModel;

namespace Nevara.ApplicationCore.Models.Enum
{
    public enum BillStatus
    {
        [Description("Pending")]
        New,
        [Description("In Progress")]
        InProgress,
        [Description("Paid")]
        Paid,
        [Description("Cancelled")]
        Cancelled,
        [Description("Completed")]
        Completed
    }
}
