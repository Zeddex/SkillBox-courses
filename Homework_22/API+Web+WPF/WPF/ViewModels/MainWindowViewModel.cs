using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
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

        public MainWindowViewModel()
        {
            _data = new DiaryDataTest();
            //_data = new DiaryDataApi();

            IEnumerable<Note> allNotes = _data.AllNotes();

            NotesList = (ObservableCollection<Note>)allNotes;           // for test data
            //NotesList = new ObservableCollection<Note>(allNotes);     // for api data
        }

        #region Properties

        private ObservableCollection<Note> _NotesList;
        public ObservableCollection<Note> NotesList
        {
            get => _NotesList;
            set => Set(ref _NotesList, value);
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set => Set(ref _Id, value);
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        private string _Surname;
        public string Surname
        {
            get => _Surname;
            set => Set(ref _Surname, value);
        }

        private string _Phone;
        public string Phone
        {
            get => _Phone;
            set => Set(ref _Phone, value);
        }

        private string _Address;
        public string Address
        {
            get => _Address;
            set => Set(ref _Address, value);
        }

        private string _Iban;
        public string Iban
        {
            get => _Iban;
            set => Set(ref _Iban, value);
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
            }
        }

        #endregion

        #region Commands

        private readonly ICommand _addNoteCommand;
        public ICommand AddNoteCommand => _addNoteCommand ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();

            _data.AddNote(note);
        });

        private readonly ICommand _updateNoteCommand;
        public ICommand UpdateNoteCommand => _updateNoteCommand ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();

            _data.UpdateNote(note);
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
    }
}
