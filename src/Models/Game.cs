using System;

namespace Legion.Models
{
    public class Game {
        public string Name;
        public string InstallDir;
        public int Id;
        private GameSource _source;

        public Game(string name, string installDir, int id, GameSource source) {
            Name = name;
            InstallDir = installDir;
            Id = id;
            _source = source;
        }

        public GameSource Source { get{return _source;} }

    }
}