using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace airborne_dust_monitor
{
    internal static class EmailSender
    {
        static string smtpAddress;
        static int portNumber;
        static bool enableSSL;
        static string fromAddress;
        static string toAddress;
        static string password;

        public static void SendEmail(string subject, string body)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(fromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }

        public static string SmtpAddress { get => smtpAddress; set => smtpAddress = value; }
        public static int PortNumber { get => portNumber; set => portNumber = value; }
        public static bool EnableSSL { get => enableSSL; set => enableSSL = value; }
        public static string FromAddress { get => fromAddress; set => fromAddress = value; }
        public static string ToAddress { get => toAddress; set => toAddress = value; }
        public static string Password { get => password; set => password = value; }

    }
}
