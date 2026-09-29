using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using Legion.Models;
using Legion.Util;
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
    internal static class AcfParser
    {               
        public static string ReadLine(string linha)
        {           
            // levando em consideração que esse metodo só vai ler esses ACFs de um jogo steam,
            // é tudo padronizado, de qualquer jeito, então n precisa preocupar com validação         

            int primeiro = linha.IndexOf('"');
            int segundo = linha.IndexOf('"', primeiro + 1);
            primeiro = linha.IndexOf('"', segundo + 1);
            segundo = linha.IndexOf('"', primeiro + 1);            

            return linha.Substring(primeiro + 1, segundo - primeiro - 1);
        }                                
    }
}
