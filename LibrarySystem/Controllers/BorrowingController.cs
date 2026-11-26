using LibrarySystem.Context;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowingController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Borrowings = _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.Member)
                .ToList();

            return View(Borrowings);
        }

        public IActionResult Details(int id)
        {
            var Borrowing = _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefault(b => b.Id == id);

            if (Borrowing == null)
                return NotFound();

            return View(Borrowing);
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.BookId = new SelectList(
                _context.Books.ToList(),
                "Id",
                "Title"
            );

            ViewBag.MemberId = new SelectList(
                _context.Members.ToList(),
                "Id",
                "FullName"
            );

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                _context.Borrowings.Add(borrowing);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.BookId = new SelectList(
                _context.Books.ToList(),
                "Id",
                "Title",
                borrowing.BookId
            );

            ViewBag.MemberId = new SelectList(
                _context.Members.ToList(),
                "Id",
                "FullName",
                borrowing.MemberId
            );

            return View(borrowing);
        }

     
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Borrowing = _context.Borrowings.Find(id);

            if (Borrowing == null)
                return NotFound();

            ViewBag.BookId = new SelectList(
                _context.Books.ToList(),
                "Id",
                "Title",
                Borrowing.BookId
            );

            ViewBag.MemberId = new SelectList(
                _context.Members.ToList(),
                "Id",
                "FullName",
                Borrowing.MemberId
            );

            return View(Borrowing);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Borrowing borrowing)
        {
            if (id != borrowing.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Borrowings.Update(borrowing);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.BookId = new SelectList(
                _context.Books.ToList(),
                "Id",
                "Title",
                borrowing.BookId
            );

            ViewBag.MemberId = new SelectList(
                _context.Members.ToList(),
                "Id",
                "FullName",
                borrowing.MemberId
            );

            return View(borrowing);
        }

  
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Borrowing = _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefault(b => b.Id == id);

            if (Borrowing == null)
                return NotFound();

            return View(Borrowing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var Borrowing = _context.Borrowings.Find(id);

            if (Borrowing == null)
                return NotFound();

            _context.Borrowings.Remove(Borrowing);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
