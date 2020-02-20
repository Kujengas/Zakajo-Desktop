namespace NoteToSelf.Framework.Serialization
{
    using System;

    //TODO:-RR Create a JSON note serializer that can be used if the user prefers. This will be useful for HTTP/REST based services that may support mobile implementations
    class JsonNoteSerializer:INoteSerializer
    {
        public void Serialize(Model.NoteSet noteSet, string fileName)
        {
            throw new NotImplementedException();
        }

        public Model.NoteSet Deserialize(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
