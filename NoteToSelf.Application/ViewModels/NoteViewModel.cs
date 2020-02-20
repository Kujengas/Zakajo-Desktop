namespace NoteToSelf.Application.ViewModels
{
    using System;

    public class NoteViewModel
    {
        public int Id { get; set; }
        public string CategoryName  { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
