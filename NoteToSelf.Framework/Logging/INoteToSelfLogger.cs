

namespace NoteToSelf.Framework.Logging
{
   
   public interface INoteToSelfLogger
    {
        void WriteMessage(string sourceMethod, string message);

    }
}
