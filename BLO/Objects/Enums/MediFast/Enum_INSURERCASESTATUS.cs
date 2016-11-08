using System.ComponentModel;

namespace BLO.Objects.Enums.MediFast
{
    public enum Enum_INSURERCASESTATUS
    {
        [Description("Pending Case")]
        PendingCase = 0,
        [Description("New Client Case")]
        NewClientCase = 1,
        [Description("To Call Back")]
        CallBack = 3,
        [Description("Customer Verified")]
        CustomerVerified = 4,
        [Description("Work In Progress")]
        WorkInProgress = 7,
        [Description("Closed/Completed")]
        Closed = 98,
        [Description("Cancelled")]
        Cancelled = 99
    }
}

