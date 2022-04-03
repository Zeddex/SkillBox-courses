using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework_22_Web.Models;
using System.Net.Http;

namespace Homework_22_Web.Data
{
    public class DiaryApiStore : IDiary
    {
        private readonly HttpClient _httpClient;

        public DiaryApiStore()
        {
            _httpClient = new HttpClient();
        }

        public void AddNote(Note note)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Note> AllNotes()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteNote(Note note)
        {
            throw new System.NotImplementedException();
        }

        public Note GetNoteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateNote(Note note)
        {
            throw new System.NotImplementedException();
        }
    }
}
