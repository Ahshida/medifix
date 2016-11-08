using System.ComponentModel;
using DBO.Data.Attributes;

namespace MediFast.dbo
{
    [AssignProperty("Branch", typeof(DescriptionAttribute), "Bank Branch")]
    [AssignProperty("Telephone", typeof(DescriptionAttribute), "Bank Tel")]
    [AssignProperty("Fax", typeof(DescriptionAttribute), "Bank Fax")]
    [AssignProperty("ContactPerson", typeof(DescriptionAttribute), "Contact Person")]
    [AssignProperty("Title", typeof(DescriptionAttribute), "Position")]
    [Description("Bank Info")]
    public partial class T_BANKINFO
    {
    }
}
