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
using Homework_22_WPF.Infrastructure.Extensions;

namespace Homework_22_WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        IDiaryData _data = new DiaryDataTest();
        //IDiaryData _data = new DiaryDataApi();

        public MainWindowViewModel()
        {
            NotesList = (ObservableCollection<Note>)_data.AllNotes();
        }

        #region Properties

        private ObservableCollection<Note> _notesList;
        public ObservableCollection<Note> NotesList
        {
            get => _notesList;
            set => Set(ref _notesList, value);
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        private string _iban;
        public string Iban
        {
            get => _iban;
            set => Set(ref _iban, value);
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

            RefreshData();
        });

        private readonly ICommand _updateNoteCommand;
        public ICommand UpdateNoteCommand => _updateNoteCommand ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();
            _data.UpdateNote(note);

            RefreshData();
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

            RefreshData();
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

        private void RefreshData()      // workaround for api mode
        {
            NotesList = (ObservableCollection<Note>)_data.AllNotes();
        }
    }
}
