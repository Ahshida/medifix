using System.Collections.Generic;
using System.Net.Mail;
using BLO.Objects;
using BLO.Objects.Enums.MediFast;
using DBO.Data;
using DBO.Data.Managers;
using DBO.Data.Objects;

namespace BLO.Managers
{
    public class EventLogManager : BLOObject
    {
        public void CreateEventLog(Enum_EVENTLOGTYPE eventLogType, string description, int clientCaseID, TransactionManager trans)
        {
            CreateEventLog(eventLogType, description, clientCaseID, trans, null);
        }
        public void CreateEventLog(Enum_EVENTLOGTYPE eventLogType, string description, int clientCaseID, TransactionManager trans, SqlReference sqlRef)
        {
            var sqlRelationShips = new List<SqlRelationship>();
            if (sqlRef != null)
                sqlRelationShips.Add(new SqlRelationship("ClientCaseID", sqlRef.Guid));
            trans.Insert(new MediFast.dbo.T_EVENTLOG()
            {
                ClientCaseID = clientCaseID,
                CreatedBy = this.Config.UserInfo.ID,
                EventLogType = eventLogType,
                Description = description
            }, sqlRelationShips.ToArray());
        }
        public void CreateEventLog(Enum_EVENTLOGTYPE eventLogType, string description, int clientCaseID)
        {
            var trans = new DBODataManager().BeginTransaction();
            CreateEventLog(eventLogType, description, clientCaseID, trans);
            trans.SubmitChanges();
        }

        public void SendCaseEmail(MediFast.dbo.T_CLIENTCASE clientCase, string insurerMessage, string mediFastMessage, string remarks, bool remarkOnly = false)
        {
            var medifast = MediFast.dbo.T_COMPANY.SelectSingle(new WhereClause("CompanyType={0}", Enum_COMPANYTYPE.Medifast));
            MediFast.dbo.T_COMPANY insurer;
            if (clientCase.ID == 0)
                insurer = this.Config.Company;
            else
                insurer = clientCase.Insurer;

            string picEmail = null;
            string insurerEmail = insurer.Email, secondEmail = insurer.SecondEmail, medifastEmail = medifast.Email;

            if (clientCase.InsurerPIC.HasValue)
                picEmail = MediFast.dbo.T_USER.SelectExact(clientCase.InsurerPIC.Value).Email;

            HTML.InformStatusChanged.Remarks remarksItem = null;
            if (!string.IsNullOrEmpty(remarks))
                remarksItem = new HTML.InformStatusChanged.Remarks() { Message = remarks };

            if (remarkOnly)// Send to underwriter
            {
                insurerEmail = secondEmail;
            }

            if (!string.IsNullOrEmpty(insurerMessage) && !string.IsNullOrEmpty(insurerEmail) && !string.IsNullOrEmpty(picEmail))
                //Email to Insurer
                EmailManager.SendMail(
                    AppSettings.EmailSetting.SupportEmail,
                    new MailAddress[] { new MailAddress(picEmail) },
                    new MailAddress[] { new MailAddress(insurerEmail) },
                    AppSettings.EmailSetting.InformEmail,
                    insurerMessage,
                    HTMLReportCreator.ReportCreator.GenerateReport(new HTML.InformStatusChanged.MainItem()
                    {
                        Title = insurerMessage,
                        Bank = clientCase.BankInfo.Bank.GetDescription(),
                        CaseStatus = clientCase.CaseStatus.GetDescription(),
                        FullName = clientCase.ClientName,
                        LANo = clientCase.LANo,
                        NRIC = clientCase.NRIC,
                        PolicyNo = clientCase.PolicyNo,
                        Tel = clientCase.MobileNo,
                        Receiver = insurer.CompanyName,
                        Remarks = remarksItem
                    }));

            if (!string.IsNullOrEmpty(mediFastMessage) && !string.IsNullOrEmpty(medifastEmail))
                //Email to Medifast
                EmailManager.SendMail(
                    AppSettings.EmailSetting.SupportEmail,
                    new MailAddress[] { new MailAddress(medifastEmail) },
                    null,
                    AppSettings.EmailSetting.InformEmail,
                    mediFastMessage,
                    HTMLReportCreator.ReportCreator.GenerateReport(new HTML.InformStatusChanged.MainItem()
                    {
                        Title = mediFastMessage,
                        Bank = clientCase.BankInfo.Bank.GetDescription(),
                        CaseStatus = clientCase.CaseStatus.GetDescription(),
                        FullName = clientCase.ClientName,
                        LANo = clientCase.LANo,
                        NRIC = clientCase.NRIC,
                        PolicyNo = clientCase.PolicyNo,
                        Tel = clientCase.MobileNo,
                        Receiver = medifast.CompanyName,
                        Remarks = remarksItem
                    }));
        }
        public void SendMessageMail(MediFast.dbo.T_CLIENTCASE clientCase, string remarks)
        {
            var message = "Message for Case {0}:".FormatWith(clientCase.PolicyNo);
            SendCaseEmail(clientCase, message, message, remarks, true);
        }
        public void SendStatusChangedMail(MediFast.dbo.T_CLIENTCASE clientCase, Enum_CASESTATUS? oriCaseStatus, Enum_INSURERCASESTATUS oriInsCaseStatus, string remarks)
        {
            string insurerMessage = null, mediFastMessage = null;
            if (clientCase.CaseStatus != oriCaseStatus)
                mediFastMessage = "Case ({0}) status was changed to {1}".FormatWith(clientCase.PolicyNo, clientCase.CaseStatus.GetDescription());
            if (clientCase.InsCaseStatus != oriInsCaseStatus)
                insurerMessage = "Case ({0}) status was changed to {1}".FormatWith(clientCase.PolicyNo, clientCase.InsCaseStatus.GetDescription());

            if (!string.IsNullOrEmpty(remarks))
                remarks = "Remarks: {0}".FormatWith(remarks);

            SendCaseEmail(clientCase, insurerMessage, mediFastMessage, remarks);
        }
        public void SendCreateCaseMail(MediFast.dbo.T_CLIENTCASE clientCase)
        {
            string subject;
            if (clientCase.AssignedTo.HasValue)
            {
                var assignedTo = MediFast.dbo.T_USER.SelectExact(clientCase.AssignedTo.Value);
                subject = "Case ({0}) created and assigned to {1}".FormatWith(clientCase.PolicyNo, assignedTo.FullName);
            }
            else
                subject = "Case ({0}) created".FormatWith(clientCase.PolicyNo);

            SendCaseEmail(clientCase, subject, subject, null);
        }
        public void SendAssignedToMail(MediFast.dbo.T_CLIENTCASE clientCase)
        {
            if (clientCase.AssignedTo.HasValue)
            {
                var assignedTo = MediFast.dbo.T_USER.SelectExact(clientCase.AssignedTo.Value);
                var subject = "Case ({0}) assigned to {1}".FormatWith(clientCase.PolicyNo, assignedTo.FullName);
                SendCaseEmail(clientCase, subject, subject, null);
            }
        }
    }
}
