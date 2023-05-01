using Microsoft.AspNetCore.Mvc;

namespace TherapyFM_Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
