using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Homework_22_API.Models;

namespace Homework_22_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiaryController : ControllerBase
    {
        private readonly DiaryContext _db;

        public DiaryController(DiaryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<Note>> Get()
        {
            var notes = await _db.Notes.ToListAsync();

            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> Post(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Notes.Add(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }

        [HttpPut]
        public async Task<ActionResult<Note>> Put(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_db.Notes.Any(x => x.Id == note.Id))
            {
                return NotFound();
            }

            _db.Update(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> Delete(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();

            return Ok(note);
        }

    }
}
