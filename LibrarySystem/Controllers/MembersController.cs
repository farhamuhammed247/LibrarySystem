using LibrarySystem.Context;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class MembersController : Controller
    {

        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Members = _context.Members.ToList();
            return View(Members);
        }

        public IActionResult Details(int id)
        {
            var Members = _context.Members.Include(m => m.Borrowings).FirstOrDefault(m => m.Id == id);
            if (Members == null)
                return NotFound();

            return View(Members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Members.Add(member);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Member = _context.Members.Find(id);

            if (Member == null)
                return NotFound();

            return View(Member);
        }


        [HttpPost]

        public IActionResult Edit(int id, Member member)
        {
            if (id != member.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Members.Update(member);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Member = _context.Members.Find(id);

            if (Member == null)
                return NotFound();

            return View(Member);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var Member = _context.Members.Find(id);

            if (Member == null)
                return NotFound();

            bool hasBorrowings = _context.Borrowings
                .Any(b => b.MemberId == id);

            if (hasBorrowings)
            {
                ModelState.AddModelError("",
                    "Cannot Delete this Member because he has Borrowings");

                return View(Member);
            }

            _context.Members.Remove(Member);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
    }
