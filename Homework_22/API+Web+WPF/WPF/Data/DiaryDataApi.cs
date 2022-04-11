using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Homework_22_WPF.Data.Interfaces;
using Homework_22_WPF.Models;

namespace Homework_22_WPF.Data
{
    public class DiaryDataApi : IDiaryData
    {
        private readonly HttpClient _httpClient;
        private const string _apiUrl = @"http://localhost:41444/api/diary";

        public DiaryDataApi()
        {
            _httpClient = new HttpClient();
        }

        public IEnumerable<Note> AllNotes()
        {
            string json = _httpClient.GetStringAsync(_apiUrl).Result;
            return JsonConvert.DeserializeObject<IEnumerable<Note>>(json);
        }

        public Note GetNoteById(int id)
        {
            string json = _httpClient.GetStringAsync(_apiUrl + $"/{id}").Result;
            return JsonConvert.DeserializeObject<Note>(json);
        }

        public void AddNote(Note note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync(_apiUrl, content).Result;
        }

        public void DeleteNote(int id)
        {
            var result = _httpClient.DeleteAsync(_apiUrl + $"/{id}").Result;
        }

        public void UpdateNote(Note note)
        {
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = _httpClient.PutAsync(_apiUrl, content).Result;
        }
    }
}
