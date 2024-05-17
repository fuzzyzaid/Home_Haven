using Home_Haven.Models;
using Microsoft.AspNetCore.Mvc;

namespace Home_Haven.Controllers
{
    public class ViewProductController : Controller
    {
        private readonly Home_Haven_DBContext _context;

        public ViewProductController(Home_Haven_DBContext context)
        {
            _context = context;
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
