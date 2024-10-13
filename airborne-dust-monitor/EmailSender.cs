using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace airborne_dust_monitor
{
    internal class EmailSender
    {
        public static void SendEmail(string toAddress, string subject, string body)
        {
            // Gmail SMTP server settings
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            // Sender's email and password (use app-specific password or OAuth for security)
            string fromAddress = "nagy.norbert1226@gmail.com"; // Replace with your Gmail address
            string password = "afec mhey omye ttnu";    // Replace with your Gmail password or app password

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true; // Set to true if the body is HTML

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(fromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}
