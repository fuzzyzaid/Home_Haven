using Home_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Home_Haven.Controllers
{
    public class HomeController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public HomeController(Home_Haven_DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve all products from the database
            var allProducts = _context.Products.ToList();

            // Shuffle the list to get random products
            Random rng = new Random();
            allProducts = allProducts.OrderBy(p => rng.Next()).ToList();

           
            int productsToDisplay = Math.Min(9, allProducts.Count);
            var randomProducts = allProducts.Take(productsToDisplay).ToList();

            // Split the products into three groups for featured, sale, and latest
            var featuredProducts = randomProducts.Take(3).ToList();
            var saleProducts = randomProducts.Skip(3).Take(3).ToList();
            var latestProducts = randomProducts.Skip(6).Take(3).ToList();

            // Store the products in ViewBag
            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.SaleProducts = saleProducts;
            ViewBag.LatestProducts = latestProducts;

            // Return the home page view
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            Debug.WriteLine(quantity);
            // Get the product from the database
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);

            // Validate the product and quantity
            if (product == null)
            {
                // Set a custom message for product not found
                TempData["CustomMessage"] = "Product not found.";
                return RedirectToAction("Index"); ;
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
            return RedirectToAction("Index");
        }

    }
}
