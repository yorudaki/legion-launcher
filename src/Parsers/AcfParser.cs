using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using Legion.Models;
using Legion.Util;
using Legion.Enums;
/* exemplo de arquivo acf
 
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
        private static string ReadLine(string linha)
        {           
            /// no verification needed, acf is standardized anyways

            int primeiro = linha.IndexOf('"');
            int segundo = linha.IndexOf('"', primeiro + 1);
            primeiro = linha.IndexOf('"', segundo + 1);
            segundo = linha.IndexOf('"', primeiro + 1);            

            return linha.Substring(primeiro + 1, segundo - primeiro - 1);
        }

		/// <summary>
		/// Given an .acf text file as string, returns an array of string containing the name, appid and installdir respectively.
		/// </summary>
		/// <returns>array[0] = name; array[1] = appid; array[2] = installdir; </returns>
		private static string[] GetGameInfo(string file) {
			string line, trimmedLine;
			string[] returnArray = new string[3];

			using (var sr = new StreamReader(file)) {
				line = sr.ReadLine();
				while (line != null) 
				{
					line = line.TrimStart();

					if (line.StartsWith(""""name""""))
						returnArray[0] = ReadLine(line);

					else if (line.StartsWith(""""appid""""))
						returnArray[1] = ReadLine(line);

					else if (line.StartsWith(""""installdir""""))
						returnArray[2] = ReadLine(line);
				}
			}

			return returnArray;
		}

		/// <summary>
		/// Given an .acf file text as string, returns a Game object based on the string.
		/// </summary>
		public static Game GetGameFromAcf(string file) {
			string[] info = GetGameInfo(file);

			return new Game(info[0], info[1], info[2], GameSource.Steam);
		}
    }
}
