using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legion.Services
{
    internal static class GameServices
    {                
        public static void GameStart(string installDir)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = installDir,
            };

            Process.Start(psi);
        }
    }
}
