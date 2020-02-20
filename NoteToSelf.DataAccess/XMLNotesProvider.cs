namespace NoteToSelf.DataAccess
{
    using System;

    using NoteToSelf.Framework.Configuration;
    using NoteToSelf.Framework.Logging;
    using NoteToSelf.Framework.Serialization;
    using NoteToSelf.Model;

    public class XMLNotesProvider:INotesProvider
    {
        private string xmlFileLocation;

        private INoteToSelfLogger logger;
        public XMLNotesProvider()
        {
            logger=new NoteToSelfEventLogger();
            var config = new NoteToSelfConfiguration();
            xmlFileLocation = config.ReadConfigSetting(Constants.ConfigFileLocation);
        }

        public NoteSet LoadNoteSet()
        {
            try
            {
                var serializer = new XmlNoteSerializer();
                return serializer.Deserialize(this.xmlFileLocation);

            }
            catch (Exception ex)
            {
                logger.WriteMessage("XMLNotesProvider.LoadNoteSet()" , ex.Message);
                return new NoteSet();
            }

        }

        public void SaveNoteSet(NoteSet noteSet)
        {
            try
            {
                var serializer = new XmlNoteSerializer();
                serializer.Serialize(noteSet, this.xmlFileLocation);
            }
           catch (Exception ex)
            {
               logger.WriteMessage("XMLNotesProvider.LoadNoteSet()", ex.Message);
            }

        }

       
    }
}
