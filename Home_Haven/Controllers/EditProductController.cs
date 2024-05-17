using Home_Haven.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace Home_Haven.Controllers
{
    public class EditProductController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public EditProductController(Home_Haven_DBContext context)
        {
            _context = context;
        }

       
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Home_Haven.Models.Product product, IFormFile image)
        {   
            var existingProduct = _context.Products.Find(id);
            if (existingProduct != null)
            {
                // Update existing product fields
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;

                // If a new image is uploaded
                if (image != null && image.Length > 0)
                {
                    // Save the uploaded image
                    var fileName = Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    // Update the ImageURL property with the path to the image
                    existingProduct.ImageURL = $"/images/{fileName}";
                    ModelState.Remove("ImageURL");
                }
                else
                {
                    // If no new image is uploaded, keep the existing ImageURL
                    ModelState.Remove("image");
                    existingProduct.ImageURL = product.ImageURL;
                }
            }

            if (!ModelState.IsValid)
            {
                    // Log and return validation errors
                    foreach (var key in ModelState.Keys)
                    {
                        var state = ModelState[key];
                        if (state.Errors.Count > 0)
                        {
                            foreach (var error in state.Errors)
                            {
                                Debug.WriteLine($"Error in {key}: {error.ErrorMessage}");
                            }
                        }
                    }
                    return View(product);
            }

            // Save changes to the database
            _context.SaveChanges();

             // Redirect to the AllProduct Index action
               return RedirectToAction("Index", "AllProduct");
        }

        }
    }

