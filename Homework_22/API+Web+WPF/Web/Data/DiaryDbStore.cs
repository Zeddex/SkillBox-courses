using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework_22_Web.Models;

namespace Homework_22_Web.Data
{
    public class DiaryDbStore : IDiaryAsync
    {
        private readonly DiaryContext _db;

        public DiaryDbStore(DiaryContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<Note>> AllNotes()
        {
            return await _db.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await _db.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddNote(Note note)
        {
            _db.Notes.Add(note);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteNote(Note note)
        {
            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateNote(Note note)
        {
            _db.Notes.Update(note);
            await _db.SaveChangesAsync();
        }
    }
}
