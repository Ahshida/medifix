using System.ComponentModel;

namespace BLO.Objects.Enums.MediFast
{
    public enum Enum_EVENTLOGTYPE
    {
        None = 0,
        [Description("Details Changed")]
        DetailsChanged = 1,
        [Description("Status Changed")]
        StatusChanged = 2,
        [Description("Forms")]
        Forms = 3,
        [Description("Case Created")]
        CaseCreated = 4,
        [Description("Deferment")]
        Deferment = 5
    }
}

