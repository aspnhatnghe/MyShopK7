using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UI.Helpers
{
    public class GoogleMailer
    {
        private readonly IOptions<MailConfig> _config;
        public GoogleMailer(IOptions<MailConfig> config)
        {
            _config = config;
        }

        public void Send(String to, String subject, String body)
        {
            String from = $"{_config.Value.EmailName} <{_config.Value.Email}>";
            Send(from, to, "", "", subject, body, "");
        }

        public void Send(String from, String to, String subject, String body)
        {
            Send(from, to, "", "", subject, body, "");
        }

        public void Send(String from, String to, String cc, String bcc, String subject, String body, String attachments)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            if (!String.IsNullOrEmpty(cc))
            {
                mail.CC.Add(cc);
            }

            if (!String.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(bcc);
            }

            if (!String.IsNullOrEmpty(attachments))
            {
                String[] fileNames = attachments.Split(";,".ToCharArray());
                foreach (String fileName in fileNames)
                {
                    if (fileName.Trim().Length > 0)
                    {
                        mail.Attachments.Add(new Attachment(fileName.Trim()));
                    }
                }
            }

            SmtpClient client = new SmtpClient(_config.Value.SmtpHost, _config.Value.SmtpPort);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_config.Value.Email, _config.Value.Password);
            client.Send(mail);
        }
    }
}
