using IptvPlayer.Core.Model;
using System;

namespace IptvPlayer.Core.Services
{
    public class SettingsService
    {
        static string settingsFileName = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), ".iptv-settings");
        static SettingsService()
        {
            Instance = new SettingsService();
        }

        public static SettingsService Instance { get;  }


        private Settings settings = new();

        public SettingsService()
        {
            if (System.IO.File.Exists(settingsFileName))
            {
                try
                {
                    var settingsData = System.IO.File.ReadAllText(settingsFileName);
                    settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(settingsData);
                    if (settings == null)
                        settings = new();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    settings = new();
                }

            }
        }

        private void Save()
        {
            try
            {
                var serializedSettings = System.Text.Json.JsonSerializer.Serialize(settings);
                System.IO.File.WriteAllText(settingsFileName, serializedSettings);
            }
            catch( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public string GetChannelGroup() => settings.LastGroup;

        public void SetChannelGroup(string channelGroup)
        {
            settings.LastGroup = channelGroup;
            Save();
        }

        public string GetPlayList() => settings.PlayList;


        public void SetPlayList(string playList)
        {
            settings.PlayList = playList;
            Save();
        }

    }
}
