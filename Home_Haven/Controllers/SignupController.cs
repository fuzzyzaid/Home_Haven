using Home_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Home_Haven.Controllers
{
    public class SignupController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public SignupController(Home_Haven_DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email is already in use. Please try a different email.");
                    return View(model);
                }

                // Create a new User instance
                var newUser = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                };

                // Add the new user to the context and save changes
                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Redirect the user to the home page or another view
                return RedirectToAction("Index", "Home");
            }

            // Return the view with the model state errors
            return View(model);
        }
    }
}
