using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_1
{
    /// <summary>
    /// Класс для логирования событий в файл, по умолчанию пишет в Log.txt
    /// </summary>
    internal static class Log
    {
        /// <summary>
        /// Путь к лог файлу по умолчанию Log.txt
        /// </summary>
        static string DefaultPath { get; } = "Log.txt";
        /// <summary>
        /// Путь к файлу
        /// </summary>
        public static string Path { get; private set; }

        /// <summary>
        /// Статический конструктор класса
        /// </summary>
        static Log () => Path = DefaultPath;
        /// <summary>
        /// Метод для инициализации логирования в произвольный файл
        /// </summary>
        /// <param name="s">Путь к файлу</param>
        public static void InitLog(string s) => Path = s ?? DefaultPath;
        /// <summary>
        /// Метод для записи строки в лог файл
        /// </summary>
        /// <param name="s">сообщение для записи</param>
        public static void LogToFile(string obj)
        {
            try
            {
                File.AppendAllText(Path, $"{DateTime.Now.ToString(),15} {obj}\n");
            }
            catch (Exception)
            {
                throw new Exception($"Can't open file {Path} for logging");
            }
        }
    }
}
