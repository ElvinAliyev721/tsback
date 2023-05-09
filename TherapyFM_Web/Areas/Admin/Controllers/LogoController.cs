using Microsoft.AspNetCore.Mvc;

namespace TherapyFM_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LogoController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public LogoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadLogo(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string directoryPath = Path.Combine(_environment.WebRootPath, "images", "icons");

                if (Directory.Exists(directoryPath))
                {
                    string[] files = Directory.GetFiles(directoryPath);
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }
                }
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(_environment.WebRootPath, "images", "icons", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                if (System.IO.File.Exists(filePath))
                {
                    string fileExtension = Path.GetExtension(filePath);

                    string newFilePath = Path.Combine(_environment.WebRootPath,
                                             "images", "icons", "mainLogo" + fileExtension);
                    System.IO.File.Move(filePath, newFilePath);
                }
                return RedirectToAction("Index", "Dashboard");
            }
            return BadRequest("No file selected");
        }
    }
}
