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
        public static void SendConfirmationEmail(string email, string confirmLink)
        {
            string SendToEmail = "mbarkbakkar1@gmail.com";
            string Password = "lsjmgejqlthqugpi";
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
    }
}
