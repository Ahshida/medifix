using System.ComponentModel;

namespace BLO.Objects.Enums.MediFast
{
    public enum Enum_CASESTATUS
    {
        None = 0,
        [Description("New Order")]
        NewOrder = 1,
        [Description("Assigned")]
        Assigned = 2,
        [Description("To Call Back")]
        CallBack = 3,
        [Description("Customer Verified")]
        CustomerVerified = 4,
        [Description("Pending Medical Check")]
        PendingMedicalCheck = 5,
        [Description("Pending Report")]
        PendingReport = 6,
        [Description("Pending APS")]
        PendingAPS = 7,
        [Description("Closed/Completed")]
        Closed = 98,
        [Description("Cancelled")]
        Cancelled = 99
    }
}

