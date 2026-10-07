using System;
using System.IO;

namespace Legion.Util 
{
    static class LegionPath {
        public static readonly string LibraryJson = Path.Combine(App,"library.json");

        public static readonly string App = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Legion" );

        public static readonly string SteamappsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Steam/steamapps");
    }
}