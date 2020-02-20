


namespace NoteToSelf.Framework.Configuration
{
   public interface INoteToSelfConfiguration
    {

        void UpdateConfigSetting(string key, string value);
        
        string ReadConfigSetting(string key);

    }
}
