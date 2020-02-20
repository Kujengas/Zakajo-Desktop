

namespace NoteToSelf.Framework.Serialization
{
    using NoteToSelf.Model;
    
    interface INoteSerializer
    {
          void Serialize(NoteSet noteSet, string fileName);

          NoteSet Deserialize(string fileName);
    }
}
