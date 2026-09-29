using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Legion.Models;
using Legion.Parsers;

namespace Legion.Util
{
    internal static class GameSorter
    {
        public static List<Game> SortByName(List<Game> gameParam)
        {            
            return gameParam.OrderBy(g => g.Name).ToList();
        }        
    }
}
