namespace NoteToSelf.Application.Extensions
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using NoteToSelf.Application.ViewModels;
    using NoteToSelf.Model;

    //TODO:-RR This needs to be refactored to remove repeating code
    public static class NoteSetConversionExtensions
    {
        public static BindingList<NoteViewModel> ToViewModel(this List<Note> notes)
        {


            if (notes != null)
            {
                return new BindingList<NoteViewModel>((from n in notes
                                                       select
                                                           new NoteViewModel
                                                               {
                                                                   Name = n.Name,
                                                                   CategoryId = n.Type.Id,
                                                                   CategoryName = n.Type.Name,
                                                                   CreationDate = n.CreationDate,
                                                                   Text = n.Text,
                                                                   Id = n.Id
                                                               }).ToList());

            }




            return new BindingList<NoteViewModel>();
        }

        public static List<Note> ToNoteList(this BindingList<NoteViewModel> notes)
        {
            if (notes != null)
            {
                return new List<Note>(
                    (from n in notes
                     select
                         new Note
                             {
                                 Name = n.Name,
                                 Type = new NoteType { Id = n.CategoryId, Name = n.CategoryName },
                                 Text = n.Text,
                                 Id = n.Id
                             }).ToList());
            }
            return new List<Note>();
        }

        public static Note ToNote(this NoteViewModel note)
        {
            return new Note
                             {
                                 Name = note.Name,
                                 Type = new NoteType { Id = note.CategoryId, Name = note.CategoryName },
                                 Text = note.Text,
                                 Id = note.Id
                             };
        }


        public static NoteViewModel ToNoteViewModel(this Note note)
        {
            return new NoteViewModel
                       {
                           Name = note.Name,
                           CategoryId = note.Type.Id,
                           CategoryName = note.Type.Name,
                           CreationDate = note.CreationDate,
                           Text = note.Text,
                           Id = note.Id
                       };
        }

    }
}
