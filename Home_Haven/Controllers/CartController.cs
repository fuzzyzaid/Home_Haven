using Home_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Home_Haven.Controllers
{
    public class CartController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public CartController(Home_Haven_DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

           
            decimal cartTotal = cart?.Sum(item => item.Quantity * item.Price) ?? 0;

           
            ViewBag.CartItems = cart;
            ViewBag.CartTotal = cartTotal;

            // Return the cart page view
            return View(cart);
        }

       
        [HttpPost]
        public IActionResult UpdateCartItem(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                // Set a custom message for invalid quantity
                TempData["CustomMessage"] = "Invalid quantity. Please enter a quantity greater than zero.";
                return RedirectToAction("Index");
            }

            // Retrieve the cart from the session
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart != null)
            {
                // Find the cart item with the specified productId
                CartItem existingCartItem = cart.FirstOrDefault(item => item.ProductID == productId);

                if (existingCartItem != null)
                {
                    // Update the quantity of the cart item
                    existingCartItem.Quantity = quantity;

                    // Save the updated cart back to the session
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            // Redirect back to the cart page
            return RedirectToAction("Index");
        }

        // Remove an item from the cart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            // Retrieve the cart from the session
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart != null)
            {
                // Find the cart item with the specified productId
                CartItem existingCartItem = cart.FirstOrDefault(item => item.ProductID == productId);

                if (existingCartItem != null)
                {
                    // Remove the cart item from the cart
                    cart.Remove(existingCartItem);

                    // Save the updated cart back to the session
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }

            // Redirect back to the cart page
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public IActionResult Checkout()
        {
            // Check if the user is logged in by retrieving the UserId from the session
            int? userId = HttpContext.Session.GetInt32("UserId");
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userId == null && userEmail != "admin@admin.com")
            {
                // If the user is not logged in, redirect to the login page with a message
                TempData["LoginMessage"] = "You need to log in before checkout.";
                TempData.Keep("LoginMessage");
                return RedirectToAction("Index", "Login");
            }

            
            return RedirectToAction("Index", "Checkout");
        }


    }
}
