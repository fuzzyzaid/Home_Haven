using Home_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Home_Haven.Controllers
{
    public class AddProductController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public AddProductController(Home_Haven_DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] Product model, IFormFile ImageFile)
        {
            Debug.WriteLine("Image URL Before ");
            Debug.WriteLine(model.ImageURL);

            // Handle image file upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                
                var fileName = ImageFile.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                model.ImageURL = $"/images/{fileName}";
                ModelState.Remove("ImageURL");
            }

            _context.Products.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "AllProduct");
        }
    }
}
