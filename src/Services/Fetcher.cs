using System;
using System.IO;
using System.Collections.Generic;

using Legion.Util;
using Legion.Models;
using Legion.Parsers;

namespace Legion.Services 
{
    public static class Fetcher {
        private static string[] FetchAcf() {
            string[] files = Directory.GetFiles(LegionPath.SteamappsPath, "*.acf");

            return files;
        }

        public static List<Game> GetSteamGames() {
            var returnList = new List<Game>();
            string[] acfArray = FetchAcf();

            foreach (string file in acfArray)
                returnList.Add(AcfParser.GetGameFromAcf(file));
            
            return returnList;
        }
    }
}