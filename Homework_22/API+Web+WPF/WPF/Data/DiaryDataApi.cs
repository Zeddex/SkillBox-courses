using Homework_22_WPF.Data.Interfaces;
using Homework_22_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_22_WPF.Data
{
    public class DiaryDataApi : IDiaryDataAsync
    {
        public Task AddNoteAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> AllNotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteNoteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetNoteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNoteAsync(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
