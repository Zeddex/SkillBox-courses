using System;
using System.Collections.Generic;
using System.Linq;
using Homework_20.Models;

namespace Homework_20.MemoryStorage
{
    public class NoteMemoryStorage : INoteStorage
    {
        private readonly List<Note> _notes;
        private int _countId;
        private readonly Random _random = new Random();

        public NoteMemoryStorage()
        {
            _countId = 1;
            _notes = new List<Note>
            {
                new Note
                {
                    Id = _countId++,
                    FirstName = "Christina",
                    MiddleName = "D.",
                    LastName = "Hall",
                    PhoneNumber = _random.Next(1000000, 9999999),
                    Address = "1762 Liberty Street",
                    Information = Guid.NewGuid().ToString()
                },
                new Note
                {
                    Id = _countId++,
                    FirstName = "Nathan",
                    MiddleName = "C.",
                    LastName = "Coleman",
                    PhoneNumber = _random.Next(1000000, 9999999),
                    Address = "1912 Sycamore Circle",
                    Information = Guid.NewGuid().ToString()
                },
                new Note
                {
                    Id = _countId++,
                    FirstName = "Margaret",
                    MiddleName = "A.",
                    LastName = "Parker",
                    PhoneNumber = _random.Next(1000000, 9999999),
                    Address = "2574 Cedar Lane",
                    Information = Guid.NewGuid().ToString()
                },
                new Note
                {
                    Id = _countId++,
                    FirstName = "Timothy",
                    MiddleName = "J.",
                    LastName = "Anderson",
                    PhoneNumber = _random.Next(1000000, 9999999),
                    Address = "4478 Five Points",
                    Information = Guid.NewGuid().ToString()
                },
                new Note
                {
                    Id = _countId++,
                    FirstName = "Daniel",
                    MiddleName = "C.",
                    LastName = "Evans",
                    PhoneNumber = _random.Next(1000000, 9999999),
                    Address = "3757 Harrison Street",
                    Information = Guid.NewGuid().ToString()
                }
            };
        }

        public Note GetById(int id)
        {
            return _notes.Find(x => x.Id == id);
        }

        public List<Note> GetAll()
        {
            return _notes;
        }
    }
}
