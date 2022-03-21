using Homework_21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Homework_21.Controllers
{
    public class NoteController : Controller
    {
        private readonly DiaryContext _db;
        public NoteController(DiaryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notes = await _db.Notes.ToListAsync();
            return View(notes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var note = await _db.Notes.SingleOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(note);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Note note)
        {
            if (ModelState.IsValid)
            {
                _db.Notes.Add(note);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Add));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Note note)
        {
            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _db.Notes.SingleOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                _db.Notes.Update(note);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Edit), new { id = note.Id });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
