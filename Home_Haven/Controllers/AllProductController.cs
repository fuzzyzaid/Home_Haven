using Home_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Home_Haven.Controllers
{
    public class AllProductController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public AllProductController(Home_Haven_DBContext context)
        {
            _context = context;
        }
      
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Delete(int id)
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
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
