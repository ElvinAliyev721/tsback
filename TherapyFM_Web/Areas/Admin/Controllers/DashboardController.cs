using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using TherapyFM_Web.Models;
namespace TherapyFM_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public DashboardController(IWebHostEnvironment environment,IConfiguration configuration)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "data.json");
            var json = System.IO.File.ReadAllText(filePath);
            var user = System.Text.Json.JsonSerializer
                .Deserialize<User>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = false });
            return View(user);
        }
        [HttpGet]
        public IActionResult UpdateContact()
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "data.json");
            var json = System.IO.File.ReadAllText(filePath);

            //var user = JsonConvert.DeserializeObject<User>(json);
            var user = System.Text.Json.JsonSerializer
                .Deserialize<User>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = false });
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateContact(User user)
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, "data.json");
                var json = System.IO.File.ReadAllText(filePath);

                //var user = JsonConvert.DeserializeObject<User>(json);
                var dbuser = System.Text.Json.JsonSerializer
                    .Deserialize<User>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = false });
                dbuser.email = user.email;
                dbuser.whatsapp = user.whatsapp;
                dbuser.companyname = user.companyname;
                var updateddata = System.Text.Json.JsonSerializer.Serialize(dbuser);
                System.IO.File.WriteAllText(filePath, updateddata);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Error");
            }

        }
    }
}
