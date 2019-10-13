using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPlayer
{
    class Settings
    {
        public static void UpdateSettings(int lastPlayed, string Path)
        {
            AutoPlayer.Properties.Settings.Default.LastPlayed = lastPlayed;
            AutoPlayer.Properties.Settings.Default.Path = Path;
            AutoPlayer.Properties.Settings.Default.Saved = true;
            AutoPlayer.Properties.Settings.Default.Save();
        }
        public static void Save()
        {
            AutoPlayer.Properties.Settings.Default.Saved = true;
            AutoPlayer.Properties.Settings.Default.Save();
        }
        public static void dontSave()
        {
            AutoPlayer.Properties.Settings.Default.Saved = false;
            AutoPlayer.Properties.Settings.Default.Save();
        }
    }
}
