using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using BLO.Objects;
using DBO.Data.Managers;

namespace BLO.Managers
{
    public class EmailManager
    {
        public static bool SendMail(MailAddress from, IEnumerable<MailAddress> tos, string subject, string message)
        {
            return SendMail(from, tos, subject, message, null);
        }
        public static bool SendMail(MailAddress from, IEnumerable<MailAddress> tos, string subject, string message, Dictionary<string, Stream> attachedFiles)
        {
            return SendMail(from, tos, null, null, subject, message, attachedFiles);
        }
        public static bool SendMail(MailAddress from, IEnumerable<MailAddress> tos, IEnumerable<MailAddress> cc, IEnumerable<MailAddress> bcc, string subject, string message)
        {
            return SendMail(from, tos, cc, bcc, subject, message, null);
        }
        public static bool SendMail(MailAddress from, IEnumerable<MailAddress> tos, IEnumerable<MailAddress> cc, IEnumerable<MailAddress> bcc, string subject, string message, Dictionary<string, Stream> attachedFiles)
        {
            foreach (var to in tos)
            {
                try
                {
                    SmtpClient mySmtpClient = new SmtpClient(AppSettings.EmailSetting.ServerName, AppSettings.EmailSetting.Port);

                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    var basicAuthenticationInfo = new NetworkCredential(AppSettings.EmailSetting.Username, AppSettings.EmailSetting.Password);
                    mySmtpClient.Credentials = basicAuthenticationInfo;
                    mySmtpClient.EnableSsl = AppSettings.EmailSetting.EnableSsl;

                    // add from,to mailaddresses
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
                    if (cc != null)
                        foreach (var c in cc)
                            myMail.CC.Add(c);
                    if (bcc != null)
                        foreach (var c in bcc)
                            myMail.Bcc.Add(c);

                    //// add ReplyTo
                    //MailAddress replyto = new MailAddress("reply@example.com");
                    //myMail.ReplyToList.Add(replyto);

                    // set subject and encoding
                    myMail.Subject = subject;
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = message;
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;

                    if (attachedFiles != null)
                    {
                        foreach (var item in attachedFiles)
                            myMail.Attachments.Add(new Attachment(item.Value, item.Key));
                    }

                    mySmtpClient.Send(myMail);
                }
                catch (SmtpException ex)
                {
                    throw new ApplicationException
                      ("SmtpException has occured: {0};\nInner Exception: {1}".FormatWith(ex.Message, ex.InnerException.Message));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return true;
        }
    }
}
