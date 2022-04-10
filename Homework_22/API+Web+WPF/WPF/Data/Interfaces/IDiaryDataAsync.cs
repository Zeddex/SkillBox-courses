using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_22_WPF.Models;

namespace Homework_22_WPF.Data.Interfaces
{
    public interface IDiaryDataAsync
    {
        Task<List<Note>> AllNotesAsync();

        Task<Note> GetNoteByIdAsync(int id);

        Task AddNoteAsync(Note note);

        Task DeleteNoteAsync(int id);

        Task UpdateNoteAsync(Note note);
    }
}
