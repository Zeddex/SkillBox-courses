using Homework_22_Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_22_Web.Data
{
    public interface IDiary
    {
        void AddNote(Note note);

        IReadOnlyList<Note> AllNotes();

        void DeleteNote(Note note);

        Note GetNoteById(int id);

        void UpdateNote(Note note);
    }
}