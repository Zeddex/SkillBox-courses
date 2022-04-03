using Homework_22_Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_22_Web.Data
{
    public interface IDiaryAsync
    {
        Task AddNote(Note note);

        Task<IReadOnlyList<Note>> AllNotes();

        Task DeleteNote(Note note);

        Task<Note> GetNoteById(int id);

        Task UpdateNote(Note note);
    }
}