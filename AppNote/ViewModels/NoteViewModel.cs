using AppNote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppNote.ViewModels
{
    public partial class NoteViewModel : INotifyPropertyChanged
    {
       // تعريف الحقول
        private string _noteTitle;
        private string _noteDescreption;
        private Note _selectedNote;
        // get and set to felds
        public string NoteTitle
        {
            get => _noteTitle;
                set
            {
                if (_noteTitle != value)
                {
                    _noteTitle = value;
                    OnPropertyChanged();
                }

            }
        }
        public string NoteDescreption
        {
            get => _noteDescreption;
            set
            {
                if (_noteDescreption != value)
                {
                    _noteDescreption = value;
                    OnPropertyChanged();
                }

            }
        }
        public Note SlectedNote
        {
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    NoteTitle = _selectedNote.Title; NoteDescreption = _selectedNote.Description; // set from list to UI
                    OnPropertyChanged();
                }

            }
        }


        // الخاصيه
        public ObservableCollection<Note> NoteCollection { get; set; }
        public ICommand AddNoteCommand  { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        public NoteViewModel()
        {
            NoteCollection = new ObservableCollection<Note>();
            AddNoteCommand = new Command(AddNote);
            DeleteNoteCommand = new Command(DeleteNote);

            EditNoteCommand = new Command(EditeNote);

        }
        private void EditeNote(object obj)
        {
            if (SlectedNote != null)
            {
                foreach(Note  note in NoteCollection.ToList())
                {
                    if(note == SlectedNote)
                    {
                        var newNote = new Note
                        {
                            // set new Note
                            Id = note.Id,
                            Title = NoteTitle,
                            Description=NoteDescreption
                        };

                        // remove Note
                        NoteCollection.Remove(note);
                        NoteCollection.Add(newNote);
                    }
                }
            }
        }

        private void DeleteNote(object obj)
        {
           if(SlectedNote != null)
            {
                NoteCollection.Remove(SlectedNote);
                // reset to value
                NoteTitle = string.Empty;
                NoteDescreption = string.Empty;

            }
        }

        private void AddNote(object obj)
        {
            int newId = NoteCollection.Count > 0 ? NoteCollection.Max(p => p.Id) + 1 : 1;
            //set new Note
            var note = new Note
            {
                Id = newId,
                Title = NoteTitle,
                Description = NoteDescreption,
            };
            NoteCollection.Add(note);
            // reset to value
            NoteTitle = string.Empty; 
            NoteDescreption = string.Empty;

        }





        // PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;//حلقة الاستشعار ما بين الفيو و الموديل فيو (هل حدث تغيير ام لا )تمام فهمت 

       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
