using System;
using Spectre.Console;

using Legion.Models;

class Program {

    #region buffer
    
    static void EnterAltBuffer() {
        Console.Write("\x1b[?1049h");
        Console.Clear();
    }
    static void LeaveBuffer() {
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        Console.Write("\x1b[?1049l");
    }

    #endregion
    
    #region helper methods
    
    #endregion
    
    #region library control

    static void AddNewGame(List<Game> list) {
        string name, path;
        int choice;

        Console.Write($"\nInsert game name: ");
        name = Console.ReadLine();

        Console.Write($"\nInsert game path: ");
        path = Console.ReadLine();

        Console.Write($"\nIs game an executable or AppImage? (1-2): ");
        choice = int.Parse(Console.ReadLine());
    }
    
    #endregion

    static void Main(string[] args) 
    {
        EnterAltBuffer();
        /// --- INSERT APP LOGIC BELOW ---
        
        Library library = new Library();
        library.AddSteamGames();
        List<Game> gamesList = library.GetGames();
        
        if (gamesList.Count == 0)
            Console.WriteLine("No games found.");
        else
            foreach (Game game in gamesList) {
                Console.WriteLine(game.Name);
            }

        /// --- INSERT APP LOGIC ABOVE ---
        LeaveBuffer();
    }
}