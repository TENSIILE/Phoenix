using System;
using System.Linq;
using Phoenix.Json;
using Phoenix.Extentions;

namespace Phoenix
{
    public static class Debugger
    {
        /// <summary>
        /// Prints all passed arguments to the console.
        /// </summary>
        public static void Log(params dynamic[] parameters)
        {
            Console.WriteLine(parameters?.ToList().Unite(", "));
        }

        /// <summary>
        /// Prints all passed arguments to the console as json.
        /// </summary>
        public static void LogAsJson(params dynamic[] parameters)
        {
            Console.WriteLine(string.Join(", ", parameters?.ToList().ToJson()));
        }
    }
}
