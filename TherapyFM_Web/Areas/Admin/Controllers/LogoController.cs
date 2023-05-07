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
            string directoryPath = Path.Combine(_environment.WebRootPath, "images", "icons");
            //string fileNameWithoutExtension = "favicon";
            //string searchPattern = fileNameWithoutExtension + ".*"; // use * to match any extension

            //string[] searchfilePaths = Directory.GetFiles(directoryPath, searchPattern);

            //if (searchfilePaths.Length > 0)
            //{
            //    // file found
            //    string oldImagefilePath = searchfilePaths[0]; // get the first matching file path
            //    System.IO.File.Delete(oldImagefilePath);
            //}
            if (Directory.Exists(directoryPath))
            {
                // Get all files in the folder
                string[] files = Directory.GetFiles(directoryPath);

                // Delete each file
                foreach (string f in files)
                {
                    System.IO.File.Delete(f);
                }
            }

            if (file != null && file.Length > 0)
            {
                // Get the file name and extension
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);

                // Generate a unique file name
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                // Save the file to the wwwroot/images folder
                var filePath = Path.Combine(_environment.WebRootPath, "images", "icons", uniqueFileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                


                if (System.IO.File.Exists(filePath))
                {
                    // Get the extension of the file
                    string fileExtension = Path.GetExtension(filePath);

                    // Construct the new file name with the updated name but the same extension
                    string newFilePath = Path.Combine(_environment.WebRootPath, "images", "icons", "mainLogo" + fileExtension);

                    // Rename the file
                    System.IO.File.Move(filePath, newFilePath);


                    

                }

                return RedirectToAction("Index","Dashboard");
                //datajsona elave et
            }

            return BadRequest("No file selected");
        }
    }
}
