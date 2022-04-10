using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_22_WPF.Models;

namespace Homework_22_WPF.Data.Interfaces
{
    public interface IDiaryData
    {
        IEnumerable<Note> AllNotes();

        Note GetNoteById(int id);

        void AddNote(Note note);

        void DeleteNote(int id);

        void UpdateNote(Note note);
    }
}
