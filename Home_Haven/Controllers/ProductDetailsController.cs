using Home_Haven.Models;
using Microsoft.AspNetCore.Mvc;

namespace Home_Haven.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public ProductDetailsController(Home_Haven_DBContext context)
        {
            _context = context;
        }

       
        public IActionResult Index(int id)
        {
            // Find the product by ID in the database
            var product = _context.Products.FirstOrDefault(p => p.ProductID == id);

            // If the product does not exist, return a NotFound result
            if (product == null)
            {
                return NotFound();
            }

            // Pass the product model to the view
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            // Get the product from the database
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);

            // Validate the product and quantity
            if (product == null)
            {
                // Set a custom message for product not found
                TempData["CustomMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }

            if (quantity <= 0)
            {
                // Set a custom message for invalid quantity
                TempData["CustomMessage"] = "Invalid quantity. Please enter a quantity greater than zero.";
                return RedirectToAction("Index");
            }

            // Retrieve the cart from the session, or create a new one if it doesn't exist
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            // Check if the product already exists in the cart
            var existingCartItem = cart.FirstOrDefault(item => item.ProductID == productId);

            if (existingCartItem != null)
            {
                // Increase the quantity if the product already exists in the cart
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Add the new cart item to the cart
                cart.Add(new CartItem
                {
                    ProductID = product.ProductID,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            // Save the updated cart back to the session
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            // Set a success message in TempData
            TempData["SuccessMessage"] = "Product added to cart successfully.";

            // Redirect back to the home page
            return RedirectToAction("Index", "ProductDetails", new { id = productId });

        }
    }
}
