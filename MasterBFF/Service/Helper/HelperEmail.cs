using Master.Entity;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System;

namespace Master.Service.Helper
{
    public class HelperEmail
    {
        /*
        public void SendEmail(LocalNetwork network, string emailSender, string subject, string texto, List<string> attachs = null)
        {
            MailMessage email = new MailMessage
            {
                From = new MailAddress("<" + network._emailSmtp + ">")
            };
            email.To.Add(emailSender);
            email.Priority = MailPriority.Normal;
            email.IsBodyHtml = false;
            email.Subject = subject;
            email.Body = texto;

            email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
            SmtpClient emailSmtp = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(network._emailSmtp, network._passwordSmtp),
                Host = network._hostSmtp,
                Port = Convert.ToInt32(network._smtpPort),
            };

            try
            {
                emailSmtp.Send(email);
            }
            catch (Exception erro)
            {
                throw new Exception("erro: " + erro.Message);
            }
        }
        */
    }
}
