using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Legion.Parsers;
using Legion.Services;

namespace Legion.Models
{
    public class Library
    {
        private string folder;
        private string jsonFile;
        private string jsonPath;
        private char[] drives;
        private List<string> directories;
        private List<Game> games;

        public Library(char[] drives)
        {
            folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Legion"
            );
            jsonFile = "library.json";
            jsonPath = $"{folder}\\{jsonFile}";
            this.drives = drives;
            directories = new List<string>();
            games = JsonParser.ReadLibrary();                        
        }
        //
        public string Folder { get { return this.folder; } set { this.folder = value; } }        
        public string JsonFile { get { return jsonFile; } set { this.jsonFile = value; } }
        public string JsonPath { get { return jsonPath; } set { this.jsonPath = value; } }
        public char[] Drives { get { return drives; } set { this.drives = value; } }
        public List<string> Directories { get { return directories; } set { this.directories = value; } }
        public List<Game> Games { get { return games; } set { this.games = value; } }
        //

        /// <summary>
        /// Updates games field
        /// </summary>
        private void LoadGames()
        {            
            games = JsonParser.ReadLibrary();
        }        

        /// <summary>
        /// Adds new game to json file then updates games list property
        /// </summary>
        public void AddGame(Game newGame)
        {
            JsonParser parser = new JsonParser(jsonFile);
            parser.WriteLibrary(new List<Game> { newGame });
            LoadGames();
        }     

        /// <summary>
        /// Adds all installed steam games to games list field
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddGamesSteam()
        {
            JsonParser parse = new JsonParser(jsonFile);
            Fetcher fetch;
            List<Game> newGames, allGames = JsonParser.ReadLibrary();
            

            if (drives.Length > 0)
            {
                foreach (char drive in drives)
                {                    
                    fetch = new Fetcher(drive);
                    newGames = fetch.FetchGamesSteam();

                    foreach (Game objGame in newGames)
                    {
                        if (games.Contains(objGame))
                            allGames.Add(objGame);
                    }                    
                }
                parse.WriteLibrary(allGames);
            }                    
            else
                throw new ArgumentOutOfRangeException(nameof(drives), "Drives (SSD/HDD) are non-existent or invalid!");
            
            LoadGames();
        }

        /// <summary>
        /// Gets games from param mainDirectory then adds to games list field and json
        /// </summary>
        /// <param name="mainDirectory">Directory of the folder containing installed games without a launcher or in custom directory</param>
        public void AddGamesBulk(string mainDirectory) // TODO: actually write the returned game paths into the json
        {
            JsonParser parser = new JsonParser(jsonFile);            
            List<Game> allGames = games;            
            List<Game> exePath = Fetcher.FetchAllExe(mainDirectory);
            
            directories.Add(mainDirectory);

            foreach (Game game in exePath)
            {
                Console.WriteLine(game.Name + " " + game.AppId + " " + game.InstallDir);                
            }

            parser.WriteLibrary(exePath);
            LoadGames();
        }
        
        /// <summary>
        /// Reads the library json, checks if game exists
        /// then removes from List, rewrites json and updates "games" field.
        /// </summary>
        /// <param name="targetGame">Object from Game class to be removed from the library</param>
        /// <returns>Returns the name of the removed game.</returns>
        public string RemoveGame(Game targetGame)
        {
            List<Game> gameList = JsonParser.ReadLibrary();            
            var parser = new JsonParser(jsonFile);

            if (!gameList.Contains(targetGame))
                throw new Exception("Error: Game not found");

            gameList.Remove(targetGame);

            parser.WriteLibrary(gameList);
            LoadGames();

            return targetGame.Name;
        }

        public void RemoveDuplicate() // TODO: optimize this method w/ linq
        {            
            List<Game> list = JsonParser.ReadLibrary();
            var parser = new JsonParser(jsonFile);
            var dupe = new Dictionary<Game, int>(); // key name, value quant                        
            Game elemKey;

            // count occurrences of games, allowing to know how many occurrances to delete
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (dupe.ContainsKey(list[i]))
                    dupe[list[i]]++;
                else
                    dupe[list[i]] = 1;                                   
            }

            for (int i = 0; i < dupe.Count; i++)
            {
                while ( dupe.ElementAt(i).Value > 1)
                {
                    elemKey = dupe.ElementAt(i).Key;
                    RemoveGame(elemKey);

                    dupe[elemKey]--;
                }
            }            
            // no need to update the json file and the list field here
            // method RemoveGame already does that.
        }
    }
}