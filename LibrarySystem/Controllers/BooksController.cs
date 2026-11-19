using LibrarySystem.Context;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() {
            var books = _context.Books.Include(x => x.Category).ToList();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var books = _context.Books
                .Include(y => y.Category)
                .FirstOrDefault(y => y.Id == id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        [HttpGet]
            public IActionResult Create() {
                ViewBag.CategoryId = new SelectList(
                 _context.Categories,
                 "Id",
                 "Name"
             );
                return View();

            }

            [HttpPost]
            public IActionResult Create(Book book )
            {
                if (ModelState.IsValid)
                {
                    _context.Books.Add(book);
                    _context.SaveChanges();
                    return  RedirectToAction(nameof(Index));
                }
                ViewBag.CategoryId = new SelectList(
               _context.Categories,
               "Id",
               "Name",
               book.CategoryId
           );

                return View(book);
            }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var books = _context.Books.Find(id);

            if (books == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                books.CategoryId
            );

            return View(books);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                book.CategoryId
            );

            return View(book);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _context.Books
                .Include(b => b.Category)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    } }