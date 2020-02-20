namespace NoteToSelf.Application.ViewModels
{
    using System;
    using System.ComponentModel;

    public class NotesGridViewModel
    {
        public BindingList<NoteViewModel> Notes;

        public DateTime SaveDate;

        public bool IsSyncronized;

    }
}
