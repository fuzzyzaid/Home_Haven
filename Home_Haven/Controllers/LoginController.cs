using Home_Haven.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Home_Haven.Controllers
{
    public class LoginController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public LoginController(Home_Haven_DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Find user in the database by email
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            // Store user details in session
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserPhone", user.PhoneNumber);
            HttpContext.Session.SetString("UserAddress", user.Address);
            HttpContext.Session.SetInt32("UserId", user.UserID);

            // Check if user is admin
            bool isAdmin = user.Email == "admin@admin.com"; // Adjust this condition as needed

            // Redirect to the appropriate controller and action based on user type
            if (isAdmin)
            {
                // Redirect to the AllProduct controller for admin users
                return RedirectToAction("Index", "AllProduct");
            }

            // Check if there is a login message from checkout redirection
            if (TempData.ContainsKey("LoginMessage"))
            {
                // TempData.Keep("LoginMessage");
                TempData.Remove("LoginMessage");
                // Redirect to the cart page if user came from checkout
                return RedirectToAction("Index", "Cart");
            }

            // Redirect to the home page after successful login for regular users
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            // Clear all session data
            HttpContext.Session.Clear();

            // Redirect to the home page or the login page
            return RedirectToAction("Index", "Home");
        }
    }
}
