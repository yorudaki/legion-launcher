using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace Legion.Services
{
    internal static class SteamServices
    {
        internal static string GameStart(string appid)
        {
            // abrir jogo normalmente se a steam estiver aberta, senão, abre steam silenciosamente antes

            string message = null;
            string steamPath = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam", "SteamExe", null) as string;
            ProcessStartInfo psi = new ProcessStartInfo();

            if ( !(Process.GetProcessesByName("steam").Length > 0) )
            {
                psi.FileName = steamPath;
                psi.UseShellExecute = true;
                psi.Arguments = "-silent";
                Process.Start(psi);
            }

            psi.FileName = $"steam://rungameid/{appid}";
            psi.UseShellExecute = true;            

            try
            {
                Process.Start(psi);
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return message;
        }
    }
}
