using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Homework_22_WPF.Infrastructure.Commands;
using Homework_22_WPF.Models;
using Homework_22_WPF.Data;
using Homework_22_WPF.Data.Interfaces;

namespace Homework_22_WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IDiaryData _data;

        public ObservableCollection<Note> NotesList { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Iban { get; set; }

        public MainWindowViewModel()
        {
            _data = new DiaryDataTest();
            NotesList = (ObservableCollection<Note>)_data.AllNotes();
        }

        private void RefreshView()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Phone));
            OnPropertyChanged(nameof(Address));
            OnPropertyChanged(nameof(Iban));
        }

        private void ClearDetails()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            Iban = string.Empty;
        }

        private Note GetCurrentNote()
        {
            var note = new Note();

            note.Id = Id;
            note.Name = Name;
            note.Surname = Surname;
            note.Phone = Phone;
            note.Address = Address;
            note.Iban = Iban;

            return note;
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                Set(ref _selectedNote, value);

                if (SelectedNote != null)
                {
                    var currentNote = _data.GetNoteById(SelectedNote.Id);
                    
                    Id = currentNote.Id;
                    Name = currentNote.Name;
                    Surname = currentNote.Surname;
                    Phone = currentNote.Phone;
                    Address = currentNote.Address;
                    Iban = currentNote.Iban;
                }

                else
                {
                    ClearDetails();
                }

                RefreshView();
            }
        }

        #region Commands

        private readonly ICommand _addNoteCommand;
        public ICommand AddNoteCommand => _addNoteCommand ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();

            _data.AddNote(note);

            RefreshView();
        });

        private readonly ICommand _editNoteCommand;
        public ICommand EditNoteCommand => _editNoteCommand ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();

            _data.UpdateNote(note);

            RefreshView();
        });

        private readonly ICommand _clearDetailsCommand;
        public ICommand ClearDetailsCommand => _clearDetailsCommand ?? new RelayCommand(() =>
        {
            SelectedNote = null;
            ClearDetails();
        });

        private readonly ICommand _deleteNoteCommand;
        public ICommand DeleteNoteCommand => _deleteNoteCommand ?? new RelayCommand(() =>
        {
            if (SelectedNote != null)
                _data.DeleteNote(SelectedNote.Id);
        });

        #endregion
    }
}
