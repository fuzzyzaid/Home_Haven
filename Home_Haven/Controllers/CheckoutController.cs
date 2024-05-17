using Home_Haven.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Home_Haven.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public CheckoutController(Home_Haven_DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Pre-fill the view model with data from the session
            CheckoutViewModel model = new CheckoutViewModel
            {
                Name = HttpContext.Session.GetString("UserName"),
                Email = HttpContext.Session.GetString("UserEmail"),
                Address = HttpContext.Session.GetString("UserAddress"),
                PhoneNumber = HttpContext.Session.GetString("UserPhone")
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Iterate through the model state dictionary to find errors
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];

                    // Check if there are any errors associated with the key
                    if (state.Errors.Count > 0)
                    {
                        // Iterate through the errors for each key
                        foreach (var error in state.Errors)
                        {
                            // Print error message to the console
                            Debug.WriteLine($"Error in {key}: {error.ErrorMessage}");
                        }
                    }
                }

               
                return View(model);
            }

            

            // Retrieve the cart from the session
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (cart == null || cart.Count == 0)
            {
                // If the cart is empty, redirect back to the cart page
                return RedirectToAction("Index");
            }

            // Create a new order for the user
            Order newOrder = new Order
            {
                UserID = userId.Value,
                OrderDate = DateTime.Now
            };

            // Iterate through the cart items and create order details
            foreach (var cartItem in cart)
            {
                // Retrieve the product from the database
                Product product = _context.Products.FirstOrDefault(p => p.ProductID == cartItem.ProductID);

                if (product != null)
                {
                    // Subtract the ordered quantity from the product's available quantity
                    product.Quantity -= cartItem.Quantity;

                    // Save the updated product back to the database
                    _context.Products.Update(product);
                }

                // Create a new OrderDetail for each cart item
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductID = cartItem.ProductID,
                    Quantity = cartItem.Quantity,
                    TotalPrice = cartItem.Quantity * cartItem.Price,
                    Order = newOrder // Associate the OrderDetail with the current order
                };

                // Add the OrderDetail to the order
                newOrder.OrderDetails.Add(orderDetail);
            }

            // Add the new order to the database
            _context.Orders.Add(newOrder);

            // Save changes to the database
            _context.SaveChanges();

            // Clear the cart from the session after checkout
            HttpContext.Session.Remove("Cart");

           
            return RedirectToAction("Index", "SuccessfullOrder");
        }
    }
}
