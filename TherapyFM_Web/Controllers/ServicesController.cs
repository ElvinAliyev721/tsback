using Microsoft.AspNetCore.Mvc;

namespace TherapyFM_Web.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
