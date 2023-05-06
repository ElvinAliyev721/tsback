using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using System.IO;
using TherapyFM_Web.Models;
using System.Text.Json;
using Org.BouncyCastle.Crypto.Macs;

namespace TherapyFM_Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(EmailViewModel data)
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "", "data.json");
            var json = System.IO.File.ReadAllText(path);
            User? info = JsonSerializer.Deserialize<User>(json);
            if(info is null)
            {
                info = new User()
                {
                    companyname = "Company",
                    email = "therapyfm2015@gmail.com"
                };
            }
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress($"Message from {data.Name}", data.Email));
            mimeMessage.To.Add(new MailboxAddress("Admin", info.email));
            mimeMessage.Body = new TextPart("html")
            {
                Text = @$"<body style=""background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
    <div style=""width:100%;margin-top:10px;margin-bottom:10px; text-align:center;""><img  width=""150"" height=""150"" src=""https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Yandex_Mail_icon.svg/1200px-Yandex_Mail_icon.svg.png""/></div>
    <span class=""preheader"" style=""color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;"">New email from {data.Name}!</span>
    <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""body"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #f6f6f6; width: 100%;"" width=""100%"" bgcolor=""#f6f6f6"">
      <tr>
        <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">&nbsp;</td>
        <td class=""container"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; max-width: 580px; padding: 10px; width: 580px; margin: 0 auto;"" width=""580"" valign=""top"">
          <div class=""content"" style=""box-sizing: border-box; display: block; margin: 0 auto; max-width: 580px; padding: 10px;"">

            <!-- START CENTERED WHITE CONTAINER -->
            <table role=""presentation"" class=""main"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background: #ffffff; border-radius: 3px; width: 100%;"" width=""100%"">

              <!-- START MAIN CONTENT AREA -->
              <tr>
                <td class=""wrapper"" style=""font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;"" valign=""top"">
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;"" width=""100%"">
                    <tr>
                      <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">
                        <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">Hi, you have new message from {data.Email}!</p>
                        <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">{data.Message}</p>
                        <p style=""font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;"">I hope you will contact with {data.Name} [{data.Email}] as soon as possible!</p>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>

          </div>
        </td>
        <td style=""font-family: sans-serif; font-size: 14px; vertical-align: top;"" valign=""top"">&nbsp;</td>
      </tr>
    </table>
  </body>"
            };
            mimeMessage.Subject = $"{info.companyname} - New email from {data.Name}";

            using (SmtpClient smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync("smtp.gmail.com", 587, false);
                await smtpClient.AuthenticateAsync("therapyfmmailsender@gmail.com", "vvfaeaepzlmqvjks");
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
                smtpClient.Dispose();
            }
            return RedirectToAction("Index");
        }
    }
}
