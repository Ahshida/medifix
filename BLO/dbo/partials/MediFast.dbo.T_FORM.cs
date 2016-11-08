using System.ComponentModel;
using System.Web;
using DBO.Data.Attributes;
using ReportListing;
using ReportListing.Attributes;

namespace MediFast.dbo
{
    public partial class T_FORM
    {
        [DatabaseIgnore]
        [Description("Upload File")]
        [EditFormat(EditFormat.Upload)]
        public HttpPostedFileBase FileUpload
        {
            get;
            set;
        }

        [DatabaseIgnore]
        [Description("Selected")]
        [ReferenceData("True:Yes;False:")]
        public bool IsSelected { get; set; }

        private T_CLIENTCASEFORM _clientCaseForm;
        [DatabaseIgnore]
        public T_CLIENTCASEFORM ClientCaseForm
        {
            get
            {
                if (_clientCaseForm == null)
                    _clientCaseForm = new T_CLIENTCASEFORM();
                return _clientCaseForm;
            }
            set
            {
                _clientCaseForm = value;
                this.IsSelected = _clientCaseForm != null;
            }
        }
    }
}
