using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.Module.Render;
using UnityEngine;

namespace EvilEye.SDK
{
    class LoggerUtill
    {
        private static List<string> DebugLogs = new List<string>();
        private static int duplicateCount = 1;
        private static string lastMsg = "";

        public static void DisplayLogo()
        {
            Console.Title = "EvilEye";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=============================================================================================================");
            Console.WriteLine("                                                                                                             ");           
            Console.WriteLine("                          ███████╗██╗░░░██╗██╗██╗░░░░░  ███████╗██╗░░░██╗███████╗                            ");
            Console.WriteLine("                          ██╔════╝██║░░░██║██║██║░░░░░  ██╔════╝╚██╗░██╔╝██╔════╝                            ");
            Console.WriteLine("                          █████╗░░╚██╗░██╔╝██║██║░░░░░  █████╗░░░╚████╔╝░█████╗░░                            ");
            Console.WriteLine("                          ██╔══╝░░░╚████╔╝░██║██║░░░░░  ██╔══╝░░░░╚██╔╝░░██╔══╝░░                            ");
            Console.WriteLine("                          ███████╗░░╚██╔╝░░██║███████╗  ███████╗░░░██║░░░███████╗                            ");
            Console.WriteLine("                          ╚══════╝░░░╚═╝░░░╚═╝╚══════╝  ╚══════╝░░░╚═╝░░░╚══════╝                            ");
            Console.WriteLine("                                 Beta Release By Four_DJ, Literal, and Fish.                                 ");                               
            Console.WriteLine("=============================================================================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogDebug(string message)
        {
            if (message == lastMsg)
            {
                DebugLogs.RemoveAt(DebugLogs.Count - 1);
                duplicateCount++;
                DebugLogs.Add($"<color=white><b>[<color=red>EvilEye</color>] [<color=#ff00ffff>{DateTime.Now.ToString("hh:mm tt")}</color>] {message} <color=red><i>x{duplicateCount}</i></color></b></color>");
            }
            else
            {
                lastMsg = message;
                duplicateCount = 1;
                DebugLogs.Add($"<color=white><b>[<color=red>EvilEye</color>] [<color=#ff00ffff>{DateTime.Now.ToString("hh:mm tt")}</color>] {message}</b></color>");
                if (DebugLogs.Count == 25)
                {
                    DebugLogs.RemoveAt(0);
                }
            }
            DebugLog.debugLog.text.text = string.Join("\n", DebugLogs.Take(25));
            DebugLog.debugLog.text.enableWordWrapping = false;
            DebugLog.debugLog.text.fontSizeMin = 25;
            DebugLog.debugLog.text.fontSizeMax = 30;
            DebugLog.debugLog.text.alignment = TMPro.TextAlignmentOptions.Left;
            DebugLog.debugLog.text.verticalAlignment = TMPro.VerticalAlignmentOptions.Top;
        }

        public static void Log(string msg, ConsoleColor color = ConsoleColor.White, bool debug = false)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("EvilEye");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] "); 
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(DateTime.Now.ToString("hh:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            if (debug)
                LogDebug(msg);
        }
    }
}
