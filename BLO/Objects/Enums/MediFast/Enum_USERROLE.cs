using System.ComponentModel;

namespace BLO.Objects.Enums.MediFast
{
    public enum Enum_USERROLE
    {
        [Description("Anonymous User")]
        AnonymousUser = 0,
        [Description("System")]
        System = 1,
        [Description("Admin")]
        Administrator = 2,
        [Description("View Only")]
        ViewOnly = 98,
        [Description("User")]
        User = 99
    }
}

