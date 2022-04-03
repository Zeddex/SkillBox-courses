using Homework_22_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Homework_22_Web.Data;

namespace Homework_22_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiaryStore _diaryStore;

        public HomeController(DiaryStore diaryStore)
        {
            _diaryStore = diaryStore;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notes = await _diaryStore.AllNotesAsync();
            return View(notes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var note = await _diaryStore.GetNoteByIdAsync(id);

            if (note == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(note);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(Note note)
        {
            if (ModelState.IsValid)
            {
                await _diaryStore.AddNoteAsync(note);

                return RedirectToAction(nameof(Index));
            }

            else
            {
                return RedirectToAction(nameof(Add));
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _diaryStore.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            await _diaryStore.DeleteNoteAsync(note);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _diaryStore.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                await _diaryStore.UpdateNoteAsync(note);

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

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
