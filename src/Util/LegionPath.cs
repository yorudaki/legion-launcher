using System;
using System.IO;
using Legion.Models;
static class LegionPath {
    public static readonly string LibraryJson = Path.Combine(App, "library.json");

    public static readonly string App = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Legion" );

    public static readonly string SteamappsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Steam/steamapps");
}