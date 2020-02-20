

namespace NoteToSelf.Framework.Configuration
{
    using System;

    using NoteToSelf.Framework.Logging;
    using System.Configuration;

    public class NoteToSelfConfiguration:INoteToSelfConfiguration
    {

        private NoteToSelfEventLogger logger;
        public NoteToSelfConfiguration()
        {
            logger = new NoteToSelfEventLogger();
        }

        public void UpdateConfigSetting(string key, string value)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings.Remove(key);
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                
                ConfigurationManager.RefreshSection("appSettings");
            }
              catch (ConfigurationErrorsException ex)
          {
           
               logger.WriteMessage("NoteToSelfConfiguration.UpdateConfigSetting()",ex.Message);
               
            }
        }

       public string ReadConfigSetting(string key)
        {
             
            try
            {
               
               return ConfigurationManager.AppSettings[key] ?? String.Empty;
               
            }
            catch (ConfigurationErrorsException ex)
          {
           
               logger.WriteMessage("NoteToSelfConfiguration.ReadConfigSetting()",ex.Message);
               return null;
            }
       
        }
    }
}
