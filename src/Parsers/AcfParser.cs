using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using Legion.Models;
using Legion.Util;
using Legion.Enums;
/* acf
 
"AppState"
{
	"appid"		"480"
	"universe"		"1"
	"LauncherPath"		"C:\\Program Files (x86)\\Steam\\steam.exe"
	"name"		"Spacewar"
	"StateFlags"		"4"
	"installdir"		"Spacewar"
	"LastUpdated"		"1749866836"
	"LastPlayed"		"1771643735"
	"SizeOnDisk"		"1906055"
	"StagingSize"		"0"
	"buildid"		"3538192"
	"LastOwner"		"76561198280032793"
	"DownloadType"		"1"
	"UpdateResult"		"0"
	"BytesToDownload"		"797632"
	"BytesDownloaded"		"797632"
	"BytesToStage"		"1906055"
	"BytesStaged"		"1906055"
	"TargetBuildID"		"3538192"
	"AutoUpdateBehavior"		"0"
	"AllowOtherDownloadsWhileRunning"		"0"
	"ScheduledAutoUpdate"		"0"
	"InstalledDepots"
	{
		"481"
		{
			"manifest"		"3183503801510301321"
			"size"		"1906055"
		}
	}
	"InstallScripts"
	{
		"481"		"installscript.vdf"
	}
	"SharedDepots"
	{
		"229006"		"228980"
	}
	"UserConfig"
	{
		"language"		"english"
	}
	"MountedConfig"
	{
		"language"		"english"
	}
}
*/
namespace Legion.Parsers
{
    public static class AcfParser
    {
		/// <summary>
		/// Given a line of content following the format: ("field" "content"), extracts the "content" bit and returns it.
		/// </summary>
        private static string ExtractContent(string line)
        {           
            /// no verification needed, acf is standardized anyways
			string ret;

            int first = line.IndexOf('"');
            int second = line.IndexOf('"', first + 1);

            first = line.IndexOf('"', second + 1);
            second = line.IndexOf('"', first + 1);            

            ret = line.Substring(first + 1, second - first - 1);

			return ret;
        }

		/// <summary>
		/// Checks if <paramref name="name"/> is contained in a set array of filter words:
		/// <para>
		/// "proton" "runtime" "steamworks"
		/// </para>
		/// </summary>
		/// <returns>Boolean value true if <paramref name="name"/> is contained in the array of filter words. False if not contained.</returns>
		private static bool IsService(string name) {
			bool response = false;
			string[] filters = {
				"proton", "runtime", "steamworks",
			};

			foreach (string word in filters){
				if (name.ToLower().Contains(word))
					response = true;
			}

			return response;
		}

		/// <summary>
		/// Given an .acf text file as string, extracts its contents, being a name, an id and an "install dir"
		/// </summary>
		/// <returns>
		/// String array of length 3 containing the relevant game information. The contents of the array are:
		/// <para>info[0] = name;</para>
		/// <para>info[1] = installDir;</para>
		/// <para>info[2] = id;</para>
		/// </returns>
		private static string[] GetGameInfo(string file) {
			string line;
			string[] info = new string[3];

			using (var sr = new StreamReader(file)) {
				while ( (line = sr.ReadLine()) != null) {

					line = line.TrimStart();

					if (line.StartsWith($"\"name\"")) {
						line = ExtractContent(line);

						if (IsService(line))
							break;

						info[0] = ExtractContent(line);
					}

					else if (line.StartsWith($"\"installdir\"")) {
						info[1] = ExtractContent(line);
					}

					else if (line.StartsWith($"\"appid\"")) {
						info[2] = ExtractContent(line);
					}
				}
			}

			return info;
		}

		/// <summary>
		/// Given an .acf file text as string, returns a Game object based on the string.
		/// </summary>
		/// <returns>Game object.
		/// <para> Returns 'null' if the file fetched is a service program.</para>
		/// </returns>
		public static Game GetGameFromAcf(string file) {
			string[] info = GetGameInfo(file);
			if (info[0] == null)
				return null;

			return new Game(info[0], info[1], int.Parse(info[2]), EGameSource.Steam);
		}
    }
}
