using System;
using System.Linq;
using System.Windows.Forms;
using Phoenix.Helpers;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public static class EffectDeps
    {
        /// <summary>
        /// A method observing the changes in the transmitted states.
        /// </summary>
        public static string[] Watch(params object[] parameters)
        {
            return parameters.ToList().Map((object el) =>
            {
                if ((!TypeMatchers.IsNullOrEmpty(el) && el is Control) ||
                (!TypeMatchers.IsNullOrEmpty(el) && el.GetType().Name == typeof(State<>).Name) ||
                (!TypeMatchers.IsNullOrEmpty(el) && el.GetType().Name == typeof(Reducer<>).Name))
                {
                    return el.GetType().GetProperty("Name").GetValue(el).ToString();
                }
                else if (typeof(string) == el.GetType())
                {
                    return el.ToString();
                }
                else
                {
                    throw new ArgumentException($@"Unable to track state changes for - '[{el.ToString()}]'!");
                }
            }).Distinct().ToArray();
        }
    }
}
