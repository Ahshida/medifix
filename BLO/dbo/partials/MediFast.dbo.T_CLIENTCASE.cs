using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLO;
using BLO.Managers;
using BLO.Objects;
using DBO.Data;
using DBO.Data.Attributes;
using DBO.Data.Managers;
using ReportListing;

namespace MediFast.dbo
{
    [AssignProperty("ClientName", typeof(DescriptionAttribute), "Full Name")]
    [AssignProperty("LANo", typeof(DescriptionAttribute), "LA No")]
    [AssignProperty("PolicyNo", typeof(DescriptionAttribute), "Policy No")]
    [AssignProperty("PolicyType", typeof(DescriptionAttribute), "Policy Type")]
    [AssignProperty("ClientNo", typeof(DescriptionAttribute), "Client No")]
    [AssignProperty("DueDate", typeof(DescriptionAttribute), "Due Date")]
    [AssignProperty("OfficePhone", typeof(DescriptionAttribute), "Tel (Office)")]
    [AssignProperty("HomePhone", typeof(DescriptionAttribute), "Tel (Home)")]
    [AssignProperty("MobileNo", typeof(DescriptionAttribute), "Tel (H/P)")]
    [AssignProperty("FaxNo", typeof(DescriptionAttribute), "Fax No")]
    [AssignProperty("Address1", typeof(DescriptionAttribute), "Address")]
    [AssignProperty("Address2", typeof(DescriptionAttribute), "")]
    [AssignProperty("CaseStatus", typeof(DescriptionAttribute), "Case Status")]
    [AssignProperty("CaseStatusDate", typeof(DescriptionAttribute), "Date")]
    [AssignProperty("InsCaseStatus", typeof(DescriptionAttribute), "Case Status")]
    [AssignProperty("InsCaseStatusDate", typeof(DescriptionAttribute), "Date")]
    [AssignProperty("InsurerID", typeof(DescriptionAttribute), "Insurer")]
    [AssignProperty("AssignedTo", typeof(DescriptionAttribute), "Assigned To")]
    [AssignProperty("InsurerPIC", typeof(DescriptionAttribute), "Insurer PIC")]
    [AssignProperty("Active", typeof(HiddenAttribute))]
    [AssignProperty("CreatedBy", typeof(HiddenAttribute))]
    [AssignProperty("CreatedDate", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedBy", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedDate", typeof(HiddenAttribute))]
    [Description("Client Information")]
    public partial class T_CLIENTCASE
    {
        private T_BANKINFO _bankInfo;
        [DatabaseIgnore]
        public T_BANKINFO BankInfo
        {
            get
            {
                if (_bankInfo == null && this.ID > 0)
                    _bankInfo = T_BANKINFO.SelectSingle(new WhereClause("ClientCaseID={0}", this.ID));
                if (_bankInfo == null)
                    _bankInfo = new T_BANKINFO();
                return _bankInfo;
            }
        }

        private T_COMPANY _insurer;
        [DatabaseIgnore]
        public T_COMPANY Insurer
        {
            get
            {
                if (_insurer == null && this.ID > 0)
                    _insurer = T_COMPANY.SelectSingle(new WhereClause("ID={0}", this.InsurerID));
                if (_insurer == null)
                    _insurer = new T_COMPANY();
                return _insurer;
            }
        }

        private T_USER _supervisorUser;
        [DatabaseIgnore]
        public T_USER SupervisorUser
        {
            get
            {
                if (_supervisorUser == null && this.Supervisor.HasValue)
                    _supervisorUser = T_USER.SelectSingle(new WhereClause("ID={0}", this.Supervisor.Value));
                if (_supervisorUser == null)
                    _supervisorUser = new T_USER();
                return _supervisorUser;
            }
        }

        private T_USER _coordinatorUser;
        [DatabaseIgnore]
        public T_USER CoordinatorUser
        {
            get
            {
                if (_coordinatorUser == null && this.AssignedTo.HasValue)
                    _coordinatorUser = T_USER.SelectSingle(new WhereClause("ID={0}", this.AssignedTo.Value));
                if (_coordinatorUser == null)
                    _coordinatorUser = new T_USER();
                return _coordinatorUser;
            }
        }


        private T_CLIENTMESSAGE _clientMessage;
        [DatabaseIgnore]
        public T_CLIENTMESSAGE ClientMessage
        {
            get
            {
                if (_clientMessage == null && this.ID > 0)
                    _clientMessage = T_CLIENTMESSAGE.SelectSingle(new WhereClause("ClientCaseID={0}", this.ID));
                if (_clientMessage == null)
                    _clientMessage = new T_CLIENTMESSAGE();
                return _clientMessage;
            }
        }

        private List<T_CLIENTMEDICALTESTREQUIREMENT> _testRequirements;
        [DatabaseIgnore]
        public List<T_CLIENTMEDICALTESTREQUIREMENT> TestRequirements
        {
            get
            {
                if (_testRequirements == null && this.ID > 0)
                    _testRequirements = T_CLIENTMEDICALTESTREQUIREMENT.Select(new WhereClause("ClientCaseID={0}", this.ID));
                if (_testRequirements == null)
                    _testRequirements = new List<T_CLIENTMEDICALTESTREQUIREMENT>();
                return _testRequirements;
            }
            set { _testRequirements = value; }
        }

        private Controller _controller;
        public void SetController(Controller controller)
        {
            _controller = controller;
        }

        private List<T_CLIENTCASEACTION> _clientCaseAction;
        [DatabaseIgnore]
        public List<T_CLIENTCASEACTION> ClientCaseAction
        {
            get
            {
                if (_clientCaseAction == null && this.ID > 0)
                    _clientCaseAction = T_CLIENTCASEACTION.Select(new WhereClause("ClientCaseID={0}", this.ID));
                if (_clientCaseAction == null)
                    _clientCaseAction = new List<T_CLIENTCASEACTION>();
                return _clientCaseAction;
            }
            set { _clientCaseAction = value; }
        }

        private List<T_CLIENTCASEFORM> _clientCaseForms;
        [DatabaseIgnore]
        public List<T_CLIENTCASEFORM> ClientCaseForms
        {
            get
            {
                if (_clientCaseForms == null && this.ID > 0)
                    _clientCaseForms = T_CLIENTCASEFORM.Select(new WhereClause("ClientCaseID={0}", this.ID));
                if (_clientCaseForms == null)
                    _clientCaseForms = new List<T_CLIENTCASEFORM>();
                return _clientCaseForms;
            }
            set { _clientCaseForms = value; }
        }

        private TestRequirementReport _editTestRequirements;
        [DatabaseIgnore]
        [Description("")]
        public TestRequirementReport EditTestRequirements
        {
            get
            {
                if (_editTestRequirements == null && _controller != null)
                    _editTestRequirements = new TestRequirementReport(_controller, this.ID) { ParentName = "TestRequirements" };
                return _editTestRequirements;
            }
            set
            {
            }
        }
        public class TestRequirementReport : ListReport<MedicalTestRequirementInfo>
        {
            private int _clientCaseID;
            public TestRequirementReport(Controller controller, int clientCaseID)
                : base(controller)
            {
                this.CanAdd = false;
                this.CanDelete = false;
                this.CanEdit = true;
                this.EditInline = true;
                this.HidePaging = true;
                this.AutoGenerateColumns = false;
                this.ShowButtons = false;
                _clientCaseID = clientCaseID;

                this.ColumnOptions.Add(new ColumnOption("ID") { ReferenceOnly = true });
                if (this.Config.IsInsurer)
                    this.ColumnOptions.Add(new ColumnOption("IsSelected"));
                this.ColumnOptions.Add(new ColumnOption("Category") { IsCategory = true });
                this.ColumnOptions.Add(new ColumnOption("Code"));
                this.ColumnOptions.Add(new ColumnOption("Name"));
                this.ColumnOptions.Add(new ColumnOption("IsMediFastReceived") { Readonly = this.Config.IsInsurer });
                this.ColumnOptions.Add(new ColumnOption("IsDespatched") { Readonly = this.Config.IsInsurer });
                this.ColumnOptions.Add(new ColumnOption("IsInsurerReceived") { Readonly = !this.Config.IsInsurer });
                this.ColumnOptions.Add(new ColumnOption("Remarks"));
            }

            private Config _config;
            protected Config Config
            {
                get
                {
                    if (_config == null)
                        _config = new Config(this.Controller);
                    return _config;
                }
            }
            protected override IEnumerable<MedicalTestRequirementInfo> GenerateItems()
            {
                List<T_MEDICALTESTREQUIREMENT> testRequirements = new List<T_MEDICALTESTREQUIREMENT>();
                var clientItems = T_CLIENTMEDICALTESTREQUIREMENT.Select(new WhereClause("ClientCaseID={0}", this._clientCaseID));
                if (this.Config.IsInsurer)
                {
                    testRequirements = T_MEDICALTESTREQUIREMENT.Select(new WhereClause("InsurerID={0} and Active=1", this.Config.UserInfo.CompanyID));
                    if (testRequirements.Count == 0 && T_MEDICALTESTREQUIREMENT.SelectCount(new WhereClause("InsurerID={0}", this.Config.UserInfo.CompanyID)) == 0)
                    {
                        new MediFastManager().CreateDefaultMedifastTestRequirements();
                        testRequirements = T_MEDICALTESTREQUIREMENT.Select(new WhereClause("InsurerID={0} and Active=1", this.Config.UserInfo.CompanyID));
                    }
                }
                else if (clientItems.Count() > 0)
                    testRequirements = T_MEDICALTESTREQUIREMENT.Select(new WhereClause("ID in ({0}) and Active=1", clientItems.Select(item => item.MedicalTestRequirement)));

                return from testRequirement in testRequirements.OrderBy(item => item.Category).ThenBy(item => item.Name)
                       select new MedicalTestRequirementInfo(testRequirement, clientItems.FirstOrDefault(i => i.MedicalTestRequirement == testRequirement.ID));
            }
        }

        private List<T_ATTACHMENT> _attachments;
        [DatabaseIgnore]
        public List<T_ATTACHMENT> Attachments
        {
            get
            {
                if (_attachments == null && this.ID > 0)
                    _attachments = T_ATTACHMENT.Select(new WhereClause("ClientCaseID={0}", this.ID));
                if (_attachments == null)
                    _attachments = new List<T_ATTACHMENT>();
                return _attachments;
            }
        }

        //[DatabaseIgnore]
        //[Description("")]
        //[EditFormat("[TestRequirementCodesForEdit]")]
        //public int[] TestRequirementCodes
        //{
        //    get
        //    {
        //        return this.TestRequirements.Select(item => item.MedicalTestRequirement).ToArray();
        //    }
        //    set
        //    {
        //        this.TestRequirements = value.Select(item => new T_CLIENTMEDICALTESTREQUIREMENT() { MedicalTestRequirement = item }).ToList();
        //    }
        //}
        //[DatabaseIgnore]
        //public string TestRequirementCodesForEdit
        //{
        //    get
        //    {
        //        object htmlAttributes = null;
        //        if (!this.Config.IsInsurer)
        //            htmlAttributes = new { disabled = true };

        //        var whereList = new List<string>();
        //        if (this.Config.IsMedifast && this.TestRequirementCodes.Count() > 0)
        //            whereList.Add(new WhereClause("ID in ({0})", this.TestRequirementCodes).Where);
        //        whereList.Add("Active=1");

        //        return CheckboxExtension.CommonCheckBoxList(
        //            null,
        //            "TestRequirementCodes",
        //            T_MEDICALTESTREQUIREMENT.Select(new WhereClause(string.Join(" and ", whereList))).Select(item =>
        //                new SelectListItem()
        //                {
        //                    Value = item.ID.ConvertTo(),
        //                    Text = "{0} - {1}".FormatWith(item.Code, item.Name),
        //                    Selected = this.TestRequirementCodes.Contains(item.ID)
        //                }),
        //            Orientation.Vertical,
        //            htmlAttributes).ToHtmlString();
        //    }
        //}

        private Config _config;
        [DatabaseIgnore]
        protected Config Config
        {
            get
            {
                if (_config == null)
                    _config = new Config(HttpContext.Current);
                return _config;
            }
        }
        [DatabaseIgnore]
        public int Aging
        {
            get
            {
                if (this.Config.IsMedifast)
                    return this.CaseStatusDate.HasValue ? (DateTime.Now.Date - this.CaseStatusDate.Value.Date).TotalDays.ConvertTo<int>() : -1;
                else
                    return (DateTime.Now.Date - this.InsCaseStatusDate.Date).TotalDays.ConvertTo<int>();
            }
        }
    }
}
