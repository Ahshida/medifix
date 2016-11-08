using System.ComponentModel;

namespace BLO.Objects.Enums.MediFast
{
    public enum Enum_COMPANYTYPE
    {
        None = 0,
        [Description("Medifast")]
        Medifast = 1,
        [Description("Insurance Company")]
        InsuranceCompany = 2,
        [Description("Insurance Agent")]
        InsuranceAgent = 3
    }
}

