using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework_22_Web.Models;

namespace Homework_22_Web.Data
{
    public class DiaryStore
    {
        private readonly DiaryContext _db;

        public DiaryStore(DiaryContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<Note>> AllNotesAsync()
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

        public async Task DeleteNoteAsync(Note note)
        {
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
