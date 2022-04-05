using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_22_Web.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Homework_22_Web.Data
{
    public class DiaryApiStore : IDiary
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrl = @"http://localhost:41444/api/diary";

        public DiaryApiStore()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Note>> AllNotesAsync()
        {
            string json = await _httpClient.GetStringAsync(_apiUrl);
            return JsonConvert.DeserializeObject<List<Note>>(json);
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            string json = await _httpClient.GetStringAsync(_apiUrl + $"/{id}");
            return JsonConvert.DeserializeObject<Note>(json);
        }

        public async Task AddNoteAsync(Note note)
        {
            var content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8);
            _ = await _httpClient.PostAsync(_apiUrl, content);
        }

        public async Task DeleteNoteAsync(int id)
        {
            _ = await _httpClient.DeleteAsync(_apiUrl + $"/{id}");
            
        }

        public async Task UpdateNoteAsync(Note note)
        {
            var content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8);
            await _httpClient.PutAsync(_apiUrl, content);
        }
    }
}
