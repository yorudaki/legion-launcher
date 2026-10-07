using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

using Legion.Models;
using Legion.Util;

namespace Legion.Parsers
{
    internal static class JsonParser
    {
        private static async void CreateLibraryJson(List<Game> gameList) {
            var jso = new JsonSerializerOptions {
                WriteIndented = true
            };

            await using FileStream stream = File.Create(LegionPath.LibraryJson);
            await JsonSerializer.SerializeAsync(stream, gameList, jso);
        }

        /// <summary>
        /// Writes a json file of all games saved.
        /// <param name="gameList">List of all games to be added</param>
        /// </summary>

        public static bool WriteLibrary(List<Game> gameList)
        {
            bool response = false;
            if (File.Exists(LegionPath.LibraryJson)) {
                CreateLibraryJson(gameList);
                response = true;
            }

            return response;
        }
        
        /// <summary>
        /// Reads the data from the json containing the games seved in Library
        /// </summary>
        /// <returns>Returns a list of all games saved in the json file</returns>
        public static List<Game> ReadLibrary()
        {
            List<Game> games = JsonSerializer.Deserialize<List<Game>>(LegionPath.LibraryJson);
            return games;
        }        
    }
}
