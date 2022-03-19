using Homework_21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Homework_21.Models;
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

        public IActionResult Index()
        {
            var notes = _db.Notes.ToList();
            return View(notes);
        }

        public IActionResult Details(int id)
        {
            var note = _db.Notes.SingleOrDefault(x => x.Id == id);
            return View(note);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
