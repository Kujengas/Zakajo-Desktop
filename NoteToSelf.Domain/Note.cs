using System;


namespace NoteToSelf.Model
{
   public class Note
    {
        public int Id { get; set; }
        public NoteType Type { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
