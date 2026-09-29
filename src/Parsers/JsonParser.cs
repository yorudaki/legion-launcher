using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Legion.Models;
using Legion.Services;
using Legion.Util;

namespace Legion.Parsers
{
    internal class JsonParser
    {
        // appdata roaming
        private string folder;
        // library: "library.json"
        private string jsonPath;

        public JsonParser(string fileName)
        {
            this.folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Legion"
            );
            this.jsonPath = $"{folder}\\{fileName}";

        }

        /// <summary>
        /// Creates a json file of all games saved. 
        /// <para>Checks if directory of "folder" field exists then creates the directory if false.
        /// If the json already exists, concatenates the existing and new data then overwrites the previous file.</para>
        /// </summary>
        /// <param name="gamesParam">List of all games to be added</param>
        public async void WriteLibrary(List<Game> gamesParam)
        {                                    
            JsonSerializerOptions jso = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var createjson = async () =>
            {
                await using FileStream stream = File.Create(jsonPath);
                await JsonSerializer.SerializeAsync(stream, gamesParam, jso);
            };

            ////////////////////////////////////////////////////////////////

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            if (File.Exists(jsonPath))
            {
                string gameJson = File.ReadAllText(jsonPath);
                List<Game> gamesL = JsonSerializer.Deserialize<List<Game>>(gameJson);                
                                
                foreach (Game game in gamesParam)
                {
                    if (!gamesL.Contains(game))
                        gamesL.Add(game);
                }

                gamesParam = GameSorter.SortByName(gamesL);
                await createjson();
            }
            else
            {
                await createjson();
            }
            
        }
        
        /// <summary>
        /// Reads the data from the json containing the games seved in Library
        /// </summary>
        /// <returns>Returns a list of all games saved in the json file</returns>
        public static List<Game> ReadLibrary()
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Legion"
            );
            string jsonLibrary = File.ReadAllText($"{folder}\\library.json");

            List<Game> games = GameSorter.SortByName(JsonSerializer.Deserialize<List<Game>>(jsonLibrary));
                       
            return games;
        }        
    }
}
