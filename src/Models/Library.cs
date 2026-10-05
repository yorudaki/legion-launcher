using System;
using System.Collections.Generic;

using Legion.Models;
using Legion.Util;
using Legion.Parsers;
using Legion.Services;
class Library {
    private SortedList<string, Game> _games;

    public Library() {
        _games = JsonParser.ReadLibrary();
        _directories = new List<string>();
    }

    #region helper methods
    private bool SearchGame(out int index) {
        for (int i = 0; i < _games; i++) {
            
        }
    }
    #endregion

    #region private methods
    private void LoadLibrary() {
        _games = JsonParser.ReadLibrary();
    }

    private bool AddSteamGames() {

        var steamGames = new List<Game>();
        steamGames = Fetcher.FetchSteamGames();
        
        foreach (Game game in steamGames) {
            _games.Add(game);
        }

        LoadLibrary();
    }

    #endregion

    #region public methods

    public bool AddGame(Game game) {
        bool response = false;

        _games.Add(game.Name, game);
        if (JsonParser.WriteLibrary(_games)) // if action is successful
            response = true;
        
        return response;

    }

    public bool RemoveGame(Game game) {
        bool response = false;

        _games.Remove(game.Name);
        if (JsonParser.WriteLibrary(_games))
            response = true;

        return response;
    }

    public List<Game> GetGames() {
        return _games;
    }
    
    #endregion
}