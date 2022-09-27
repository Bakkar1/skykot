using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Helpers
{
    public class EmailHelper
    {
        private const string SendToEmail = "mbarkbakkar1@gmail.com";
        private const string Password = "pkcfdjsqxpvhozwd";
        public static void SendConfirmationEmail(string email, string confirmLink)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SendToEmail);
                mail.To.Add(email);
                mail.Subject = "Confirm Email";

                StringBuilder stb = new StringBuilder();
                stb.Append($"<p>Beste</p>");

                stb.Append($"Please confirm your Email");
                stb.Append($"<a href='{confirmLink}'>here</a>");

                mail.Body = stb.ToString();

                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(SendToEmail, Password);

                    smtp.Send(mail);
                }
            }
        }
        public static void SendInvatationEmail(string email, string confirmLink)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SendToEmail);
                mail.To.Add(email);
                mail.Subject = "Confirm Email";

                StringBuilder stb = new StringBuilder();
                stb.Append($"<p>Beste</p>");

                stb.Append($"You are invated to become a sky kot memeber, Please confirm your Email and ask the owner house for password");
                stb.Append($"<a href='{confirmLink}'>here</a>");

                mail.Body = stb.ToString();

                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(SendToEmail, Password);

                    smtp.Send(mail);
                }
            }
        }

        public static void SendRestPasswordlink(string email, string passLink)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SendToEmail);
                mail.To.Add(email);
                mail.Subject = "Reset Password";

                StringBuilder stb = new StringBuilder();
                stb.Append($"<p>Beste</p>");

                stb.Append($"Hier is the link to Change  your password");
                stb.Append($"<a href='{passLink}'>here</a>");

                stb.Append($"<div>SkyKot</div>");

                mail.Body = stb.ToString();

                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(SendToEmail, Password);

                    smtp.Send(mail);
                }
            }
        }
    }
}
