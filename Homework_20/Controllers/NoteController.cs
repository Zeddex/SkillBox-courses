using Homework_20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Homework_20.MemoryStorage;

namespace Homework_20.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteStorage _noteStorage;

        public NoteController(ILogger<NoteController> logger, INoteStorage noteStorage)
        {
            _logger = logger;
            _noteStorage = noteStorage;
        }

        public IActionResult Index()
        {
            var notes = _noteStorage.GetAll();

            return View(notes);
        }

        public IActionResult Details(int id)
        {
            var note = _noteStorage.GetById(id);

            if (note == null)
            {
                return RedirectToAction("Index");
            }

            return View(note);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
