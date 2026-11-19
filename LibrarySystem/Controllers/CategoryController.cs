using LibrarySystem.Context;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class CategoryController : Controller
    {

        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var Categories = _context.Categories.ToList();
            return View(Categories);

        }
        public IActionResult Details(int id)
        {
            var Category = _context.Categories
                    .Include(c => c.Books)
                    .FirstOrDefault(c => c.Id == id);

            if (Category == null)
                return NotFound();

            return View(Category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Category = _context.Categories.Find(id);
            if (Category == null)
                return NotFound();
            return View(Category);

        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
                return NotFound();
            if (ModelState.IsValid) 
            {
                _context.Categories.Update(category);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        
        {
            var category = _context.Categories.Find(id);

            if(category == null)
                return NotFound();
            return View(category);
        
        
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
        
        var category = _context.Categories.Find(id);
            if(category==null)
                return NotFound();
            bool hasBooks = _context.Books.Any(b => b.CategoryId == id);
                if (hasBooks)
            {

                ModelState.AddModelError("", "Cannot Delete this Category because it has Books");
                    return View(category);
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
