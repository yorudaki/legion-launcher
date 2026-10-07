using System;
using System.Diagnostics;

namespace Legion.Services
{
    internal static class GameServices
    {                
        public static void StartGame(string installDir)
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
