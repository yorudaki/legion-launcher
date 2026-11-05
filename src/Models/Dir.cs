namespace Legion.Models 
{
    public class Dir {
        private bool _isSteam;
        public bool IsSteam {get {return _isSteam;}}
        public string Path;
        
        public Dir(string path, bool isSteam = false) {
            Path = path;
            _isSteam = isSteam;
        }
    }
}