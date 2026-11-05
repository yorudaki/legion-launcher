using System;
using Legion.Enums;

namespace Legion.Models
{
    public class Game {
        public string Name;
        public string InstallDir;
        public int Id;
        private EGameSource _source;

        public Game(string name, string installDir, int id, EGameSource source) {
            if (name == " " || name == null)
                throw new ArgumentNullException("name is empty!");

            Name = name;
            InstallDir = installDir;
            Id = id;
            _source = source;
        }

        public EGameSource Source { get{return _source;} }

    }
}