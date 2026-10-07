using System;
using System.Diagnostics;

namespace Legion.Services
{
    internal static class SteamServices
    {
        internal static string StartGame(int appId)
        {
            // start the game normally if steam is already started, else start steam silent

            string message = null;
            
            try
            {
                if (Process.GetProcessesByName("steam").Length == 0) {
                    ProcessStartInfo steamPsi = new ProcessStartInfo {
                        FileName = "steam",
                        Arguments = "-silent",
                        UseShellExecute = true
                    };
                    Process.Start(steamPsi);
                }

                ProcessStartInfo gamePsi = new ProcessStartInfo {
                    FileName = $"steam://rungameid/{appId}",
                    UseShellExecute = true
                };

                Process.Start(gamePsi);
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return message;
        }
    }
}
