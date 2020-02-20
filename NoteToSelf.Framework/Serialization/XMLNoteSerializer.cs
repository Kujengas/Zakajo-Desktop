using System;


namespace NoteToSelf.Framework.Serialization
{
    using System.IO;
    using System.Xml.Serialization;

    using NoteToSelf.Framework.Logging;
    using NoteToSelf.Model;

  public class XmlNoteSerializer:INoteSerializer
    {
      private INoteToSelfLogger logger;


      public XmlNoteSerializer()
      {
          logger = new NoteToSelfEventLogger();
      }

      public void Serialize(NoteSet noteSet, string fileName)
        {
            var serializer = new XmlSerializer(typeof(NoteSet));

            try
            {
                noteSet.LastSaveDate = DateTime.Now;
                using (var stream = File.Open(fileName,FileMode.Create))
                {
                    serializer.Serialize(stream, noteSet);
                }
            }
            catch (Exception ex)
            {
                logger.WriteMessage("XmlNoteSerializer.Serialize()" , ex.Message);
            }
        }
       
        public NoteSet Deserialize(string fileName)
        {
            var serializer = new XmlSerializer(typeof(NoteSet));
            var noteSet = new NoteSet();
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();
                    this.Serialize(noteSet,fileName);
                    return noteSet;

                }
                noteSet.LastSaveDate = DateTime.Now;
                using (var stream = File.OpenRead(fileName))
                {
                    noteSet = (NoteSet)serializer.Deserialize(stream);
                }
                return noteSet;
            }
            catch (Exception ex)
            {

                logger.WriteMessage("XmlNoteSerializer.Deserialize()" , ex.Message); 
                return noteSet;
            }
           
        }

    }
}
