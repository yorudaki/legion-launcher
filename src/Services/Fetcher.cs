using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Legion.Models;
using Legion.Parsers;

namespace Legion.Services
{
    internal class Fetcher
    {
        private char drive;
        public Fetcher(char drive)
        {
            this.drive = drive;
        }
        public char Drive { get; set; }

        /// <summary>
        /// Searches the steamapps folder and fetches the acf files from the currently installed steam games
        /// </summary>
        /// <returns>Returns a string array containing the full path of all acf files</returns>
        private string[] FetchAcf()
        {
            string steamappsDir;
            string[] files;
            List<string> list = new List<string>();

            // ascii: 65 = A, 90 = Z
            if (!(drive >= 65 && drive <= 90))
                return null;

            steamappsDir = $"{drive}:\\SteamLibrary\\steamapps";
            files = Directory.GetFiles(steamappsDir, "*.acf");
            foreach (string file in files)
            {
                if (!file.Contains("480"))
                    list.Add(file);
            }

            return files = list.ToArray();
        }        

        /// <summary>
        /// Extracts the relevant info out of the acf files (uses FetchAcf method)
        /// </summary>
        /// <returns>Returns a list of Game objects of all steam games</returns>
        public List<Game> FetchGamesSteam()
        {            
            string[] acfFiles = FetchAcf();
            List<Game> games = new List<Game>(acfFiles.Length);

            string name = null;
            string appId = null;
            string installDir = null;

            string linha;

            for (int i = 0; i < games.Count; i++)
            {
                using (StreamReader sr = new StreamReader(acfFiles[i]))
                {
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.TrimStart().StartsWith("\"name\""))
                            name = AcfParser.ReadLine(linha);

                        else if (linha.TrimStart().StartsWith("\"appid\""))
                            appId = AcfParser.ReadLine(linha);

                        else if (linha.TrimStart().StartsWith("\"installdir\""))
                            installDir = AcfParser.ReadLine(linha);
                    }
                }
                games.Add(new Game(name, installDir, appId));
            }
            return games;
        }

        /// <summary>
        /// Goes through param directory and fetches the name of all executable files found
        /// </summary>        
        /// <returns>Returns a filtered array containing all the file full paths</returns>
        public static List<Game> FetchAllExe(string diretorio)
        {
            bool found;
            string[] allExePaths = Directory.GetFiles(diretorio, "*.exe", SearchOption.AllDirectories);
            string[] fileName = new string[allExePaths.Length];
            List<Game> filtered = new List<Game>();

            for (int i = 0; i < fileName.Length; i++)
            {
                fileName[i] = Path.GetFileName(allExePaths[i]);
            }

            string[] filters = new string[] // write all filters in lowered char
            {
                "launcher", "installer", "uninstall", "unins000", "shipping", "oalinst",
                "unitycrashhandler", "helper", "crash", "report", "redist", "setup", "browser", "x86"
            };
            
            // =============================
            // --    Logic starts here    --
            // =============================

            for (int i = 0; i < fileName.Length; i++)
            {
                found = false;
                for (int j = 0; j < filters.Length; j++)
                {
                    if (fileName[i].Contains(filters[j], StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    filtered.Add(new Game(fileName[i].Substring(0, fileName[i].Length - 4), allExePaths[i]));
            }

            return filtered;
        }
    }
}
