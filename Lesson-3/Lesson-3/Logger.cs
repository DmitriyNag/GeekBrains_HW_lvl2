using System;
using System.IO;
namespace MyGame
{
    internal static class Logger
    {
        internal static DateTime dt = DateTime.Now;
        internal static StreamWriter sw;
        internal static string logPath = "../../log.txt";
        static Logger()
        {
        }

        public static void StartLog()
        {
            sw = new StreamWriter(new FileStream(logPath, FileMode.Append,FileAccess.Write));
            sw.WriteLine($"{dt} - Игра начата");
            sw.Flush();
        }

        public static void CloseLog()
        {
            sw.WriteLine($"{dt} - Игра окончена \n");
            sw.Close();
        }
        internal static void Log(string str)
        {
            sw.WriteLine($"{dt} - {str}");
            sw.Flush();
        }
    }
}
