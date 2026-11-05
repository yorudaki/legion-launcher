namespace Legion.Util 
{
    static class LegionPath {
        public static readonly string App = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Legion" );
        public static readonly string LibraryJson = Path.Combine(App,"library.json");

        /// <summary>
        /// The relative path to 'steamapps' directory.
        /// <para> Intended use is to be appended to a root directory.</para>
        /// <code> string SteamappsRemote = "Steam/steamapps"</code>
        /// </summary>
        public static readonly string SteamappsRemote = "Steam/steamapps/";

        /// <summary>
        /// The absolute path to 'steamapps' directory located in the systems drive.
        /// <para>
        /// Value of 'Steamapps' is equal to <c>$APPDATA/Steam/steamapps/</c>
        /// </para>
        /// </summary>
        public static readonly string Steamapps = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Steam/steamapps/");
    }
}