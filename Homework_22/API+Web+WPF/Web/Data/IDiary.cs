using Homework_22_Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework_22_Web.Data
{
    public interface IDiary
    {
        Task<List<Note>> AllNotesAsync();

        Task<Note> GetNoteByIdAsync(int id);

        Task AddNoteAsync(Note note);

        Task DeleteNoteAsync(int id);

        Task UpdateNoteAsync(Note note);
    }
}