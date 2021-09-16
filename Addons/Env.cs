using System.Collections.Generic;
using System.IO;

namespace Phoenix.Addons
{
    public static class Env
    {
        /// <summary>
        /// The method starts parsing the .env file and returns a dictionary.
        /// </summary>
        public static Dictionary<string, string> Read(string path)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (File.Exists(path))
            {
                foreach (string line in File.ReadLines(path))
                {
                    string[] array = line.Split('=');
                    result.Add(array[0], array[1].Trim());
                }
            }

            return result;
        }

        /// <summary>
        /// The method saves the dictionary as a .env file.
        /// </summary>
        public static void Save<T, R>(Dictionary<T, R> dict, string path, string filename = "")
        {
            string result = "";

            foreach (KeyValuePair<T, R> item in dict)
            {
                result += $@"{item.Key.ToString().ToUpper()}={item.Value}";
                result += "\t\n";
            }

            using (StreamWriter writer = File.CreateText(Path.Combine(path, $@"{filename}.env")))
            {
                writer.Write(result);
            }
        }
    }
}
