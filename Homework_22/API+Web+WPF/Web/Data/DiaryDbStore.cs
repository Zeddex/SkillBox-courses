using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework_22_Web.Models;

namespace Homework_22_Web.Data
{
    public class DiaryDbStore : IDiary
    {
        private readonly DiaryContext _db;

        public DiaryDbStore(DiaryContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Note>> AllNotesAsync()
        {
            return await _db.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddNoteAsync(Note note)
        {
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);
            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Note note)
        {
            _db.Notes.Update(note);
            await _db.SaveChangesAsync();
        }
    }
}
