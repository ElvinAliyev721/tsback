using Microsoft.AspNetCore.Mvc;

namespace TherapyFM_Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
