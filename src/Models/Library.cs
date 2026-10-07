using System;
using System.Collections;
using System.Collections.Generic;

using Legion.Util;
using Legion.Parsers;
using Legion.Services;

namespace Legion.Models 
{
    class Library {
        private SortedList<string, Game> _games;

        public Library() {
            _games = new SortedList<string, Game>();
        }

        #region helper methods

        #endregion

        #region private methods
        private void LoadLibrary() {
            var list = JsonParser.ReadLibrary();
            foreach (Game game in list)
                _games.Add(game.Name, game);
        }

        private void AddSteamGames() {

            var steamGames = new List<Game>();
            steamGames = Fetcher.GetSteamGames();
            
            foreach (Game game in steamGames) {
                _games.Add(game.Name, game);
            }

            LoadLibrary();
        }

        #endregion

        #region public methods

        public void AddGame(Game game) {
            _games.Add(game.Name, game);

            var list = new List<Game>(_games.Values);
            JsonParser.WriteLibrary(list);

        }

        public void RemoveGame(Game game) {
            var list = new List<Game>(_games.Values);

            list.Remove(game);
            JsonParser.WriteLibrary(list);
        }

        public List<Game> GetGames() {
            var returnList = new List<Game>();
            foreach (var value in _games)
                returnList.Add(value.Value);
            
            return returnList;
        }
        
        #endregion
    }
}