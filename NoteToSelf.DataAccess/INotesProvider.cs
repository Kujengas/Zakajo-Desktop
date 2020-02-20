

namespace NoteToSelf.DataAccess
{
    
    using NoteToSelf.Model;

   public interface INotesProvider
    {
        NoteSet LoadNoteSet();

       void SaveNoteSet(NoteSet noteSet);

    }
}
