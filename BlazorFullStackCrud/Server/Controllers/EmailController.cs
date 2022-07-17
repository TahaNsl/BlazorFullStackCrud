using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public void Sendmail(string email)
        {
            try
            {
                using (MailMessage mm = new MailMessage("tahanasrollahii@gmail.com", "natson@7869"))
                {
                    mm.From = new MailAddress("tahanasrollahii@gmail.com");
                    mm.To.Add(new MailAddress(email));
                    mm.Subject = "Verify your email";
                    mm.Body = "<h1>Your Email Is Verified</h1>";
                    mm.BodyEncoding = System.Text.Encoding.UTF8;
                    mm.Priority = MailPriority.Normal;
                    //if (model.Attachment.Length > 0)
                    //{
                    //    string fileName = Path.GetFileName(model.Attachment.FileName);
                    //    mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                    //}
                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("tahanasrollahii@gmail.com", "natson@7869");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(mm);
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

        }
    }
}
