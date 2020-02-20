namespace NoteToSelf.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NoteSet
    {
        public List<Note> Notes { get; set; }

        public DateTime LastSaveDate { get; set; }

        public int MaxIdentity
        {
            get
            {
                var maxId = this.Notes.Count > 0 ? (from n in this.Notes select n.Id).Max() + 1 : 1;
                return maxId;
            }
        }

        public NoteSet()
        {
            this.Notes = new List<Note>();
        }
    }

}

