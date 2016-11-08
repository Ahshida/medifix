using System.ComponentModel;
using DBO.Data.Attributes;

namespace MediFast.dbo
{
    [AssignProperty("ID", typeof(HiddenAttribute))]
    [AssignProperty("ClientCaseID", typeof(HiddenAttribute))]
    [AssignProperty("EventLogType", typeof(DescriptionAttribute), "Event Type")]
    [AssignProperty("CreatedBy", typeof(DescriptionAttribute), "By")]
    [AssignProperty("CreatedDate", typeof(DescriptionAttribute), "Date")]
    public partial class T_EVENTLOG
    {
    }
}
