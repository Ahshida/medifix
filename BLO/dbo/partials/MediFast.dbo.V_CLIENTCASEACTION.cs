using System.ComponentModel;
using BLO.Objects.Enums.MediFast;
using DBO.Data.Attributes;
using DBO.Data.Managers;

namespace MediFast.dbo
{
    [AssignProperty("FullName", typeof(DescriptionAttribute), "By")]
    [AssignProperty("CreatedDate", typeof(DescriptionAttribute), "Date")]
    public partial class V_CLIENTCASEACTION
    {
        [DatabaseIgnore]
        [Description("Case Status")]
        public string CaseStatusDesc
        {
            get
            {
                if (this.CaseStatus.HasValue)
                    return this.CaseStatus.ConvertTo<Enum_CASESTATUS>().GetDescription();
                else
                    return "Message";
            }
        }
    }
}
