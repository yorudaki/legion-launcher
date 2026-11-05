using System;
using System.Collections;
using System.Collections.Generic;

using Legion.Util;
using Legion.Parsers;
using Legion.Services;
using Legion.Enums;

namespace Legion.Models
{
    class Library {
        private SortedList<int, Game> _games;
        private List<Dir> _directories;

        #region constructors
        private void Init() {
            _games = new SortedList<int, Game>();
            _directories = new List<Dir>();
            _directories.Add(new Dir(LegionPath.Steamapps, true));
        }
        public Library() {
            Init();
        }
        public Library(string directory) {
            Init();
            _directories.Add(new Dir(directory));
        }
        public Library(string directory, bool isSteamDirectory){
            Init();
            _directories.Add(new Dir(directory, isSteamDirectory));
        }
        #endregion

        #region helper methods

        private bool IsEven(int number) {
            return number % 2 == 0;
        }

        private int GenerateId() { // custom game base id: 108101103
            Random rnd = new Random();
            int num = rnd.Next(1, 101);
            int id = 108101103; // 108-l 101-e 103-g

            if (IsEven(num))
                id += num;
            else
                id -= num;
            
            return id;
        }

        #endregion

        #region private methods
        private void LoadLibrary() {
            var list = JsonParser.ReadLibrary();
            foreach (Game game in list)
                _games.Add(game.Id, game);
        }
        #endregion

        #region public methods

        /// <summary>
        /// Tries to fetch steam games within the steam directories and save the games in the library list.
        /// </summary>
        /// <returns>
        /// Returns <c>true</c> if any game is successfully saved in the library, else returns <c>false</c> if no games are saved due to failure in fetching.
        /// </returns>
        public bool AddSteamGames() {
            bool response = true;
            List<Game> steamGames = Fetcher.GetSteamGames();
            
            foreach (Game game in steamGames)
                _games.Add(game.Id, game);
            
            if (_games.Count == 0)
                response = false;
            
            return response;
        }

        public bool AddGame(string name, string installDir, EGameSource source) {
            bool response;
            Game game = new Game(name, installDir, GenerateId(), source);
            var list = new List<Game>(_games.Values);

            _games.Add(game.Id, game);

            response = JsonParser.WriteLibrary(list);

            return response;
        }

        public bool RemoveGame(Game game) {
            bool response = false;
            var list = new List<Game>(_games.Values);

            list.Remove(game);
            if (JsonParser.WriteLibrary(list))
                response = true;
            
            return response;
        }

        public List<Game> GetGames() {
            var returnList = new List<Game>();
            foreach (var value in _games)
                returnList.Add(value.Value);
            
            return returnList;
        }
        
        public void AddDirectory(string directory, bool isSteamDirectory = false) {
            _directories.Add( new Dir(directory, isSteamDirectory) );
        }

        public void RemoveDirectory(int index) {
            _directories.RemoveAt(index);
        }
        #endregion
    }
}