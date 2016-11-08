using System.ComponentModel;
using DBO.Data.Attributes;
using ReportListing.Attributes;

namespace MediFast.dbo
{
    [AssignProperty("PanelType", typeof(HiddenAttribute))]
    [AssignProperty("ClinicCode", typeof(DescriptionAttribute), "Clinic Code")]
    [AssignProperty("ClinicName", typeof(DescriptionAttribute), "Clinic Name")]
    [AssignProperty("DoctorName", typeof(DescriptionAttribute), "Doctor Name")]
    [AssignProperty("BankAccountNo", typeof(DescriptionAttribute), "Bank Acc. No.")]
    [AssignProperty("BankAccountName", typeof(DescriptionAttribute), "Bank Acc. Name")]
    [AssignProperty("CreatedBy", typeof(HiddenAttribute))]
    [AssignProperty("CreatedDate", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedBy", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedDate", typeof(HiddenAttribute))]
    [AssignProperty("Active", typeof(DescriptionAttribute), "Status")]
    [AssignProperty("Active", typeof(ReferenceDataAttribute), "True:Active;False:Inactive")]
    public partial class T_PANELDOCTOR
    {
       
    }
}
