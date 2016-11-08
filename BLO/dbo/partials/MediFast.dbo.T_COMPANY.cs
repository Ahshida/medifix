using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DBO.Data.Attributes;

namespace MediFast.dbo
{
    [AssignProperty("CompanyType", typeof(DescriptionAttribute), "Company Type")]
    [AssignProperty("CompanyName", typeof(DescriptionAttribute), "Company Name")]
    [AssignProperty("Address1", typeof(DescriptionAttribute), "Address")]
    [AssignProperty("Address2", typeof(DescriptionAttribute), "")]
    [AssignProperty("City", typeof(DescriptionAttribute), "City")]
    [AssignProperty("State", typeof(DescriptionAttribute), "State")]
    [AssignProperty("Postcode", typeof(DescriptionAttribute), "Postcode")]
    [AssignProperty("Country", typeof(DescriptionAttribute), "Country")]
    [AssignProperty("Telephone1", typeof(DescriptionAttribute), "Telephone 1")]
    [AssignProperty("Telephone2", typeof(DescriptionAttribute), "Telephone 2")]
    [AssignProperty("Fax1", typeof(DescriptionAttribute), "Fax 1")]
    [AssignProperty("Fax2", typeof(DescriptionAttribute), "Fax 2")]
    [AssignProperty("CompanyCode", typeof(HiddenAttribute))]
    [AssignProperty("CreatedBy", typeof(HiddenAttribute))]
    [AssignProperty("CreatedDate", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedBy", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedDate", typeof(HiddenAttribute))]
    public partial class T_COMPANY
    {
    }
}
