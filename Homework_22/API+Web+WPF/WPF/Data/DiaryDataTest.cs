using Homework_22_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_22_WPF.Data.Interfaces;

namespace Homework_22_WPF.Data
{
    public class DiaryDataTest : IDiaryData
    {
        readonly ObservableCollection<Note> data;

        public DiaryDataTest()
        {
            data = new ObservableCollection<Note>()
            {
                new Note { Id = 1, Name = "Clinton", Surname = "Nielsen", Phone = "073-950-032", Address = "548-9419 Ac St.", Iban = "MC4687627477700586662925905" },
                new Note { Id = 2, Name = "Summer", Surname = "Horton", Phone = "981-871-867", Address = "P.O. Box 755, 5715 Velit Street", Iban = "DK0458275512361305" },
                new Note { Id = 3, Name = "Brock", Surname = "Benson", Phone = "644-470-655", Address = "5539 Elit. St.", Iban = "IE50OCEY16214666156540" },
                new Note { Id = 4, Name = "Donovan", Surname = "Sanchez", Phone = "021-627-378", Address = "Ap #972-9103 Eu Rd.", Iban = "SM7625407558845802833383352" },
                new Note { Id = 5, Name = "Martena", Surname = "Stewart", Phone = "464-878-841", Address = "432-4010 Molestie Road", Iban = "AL04700264981286482617074842" },
                new Note { Id = 6, Name = "Breanna", Surname = "Benson", Phone = "436-251-379", Address = "P.O. Box 351, 8135 Lorem Av.", Iban = "PL39719618279189562460931448" },
                new Note { Id = 7, Name = "Octavia", Surname = "Cleveland", Phone = "494-387-366", Address = "448-9849 Blandit Ave", Iban = "GB98GWGL12119886433060" }
            };
        }

        public IEnumerable<Note> AllNotes()
        {
            return data;
        }

        public Note GetNoteById(int id)
        {
            var note = data.FirstOrDefault(x => x.Id == id);
            return note;
        }

        public void AddNote(Note note)
        {
            data.Add(note);
        }

        public void DeleteNote(int id)
        {
            var note = data.FirstOrDefault(x => x.Id == id);

            if (note != null)
                data.Remove(note);
        }

        public void UpdateNote(Note note)
        {
            var currentNote = data.FirstOrDefault(x => x.Id == note.Id);

            if (currentNote != null)
                data[currentNote.Id] = note;
        }
    }
}
