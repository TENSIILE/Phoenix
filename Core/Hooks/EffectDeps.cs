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
                if (TypeMatchers.IsNullOrEmpty(el))
                {
                    throw new ArgumentException($@"Unable to track state changes for - '[{el.ToString()}]'!");
                }

                if (el is Control || (el.GetType().Name == typeof(State<>).Name) || (el.GetType().Name == typeof(Reducer<>).Name))
                {
                    return el.GetType().GetProperty("Name").GetValue(el).ToString();
                }
                else
                {
                    return el.ToString();
                }
                
                throw new ArgumentException($@"Unable to track state changes for - '[{el.ToString()}]'!");
            }).Distinct().ToArray();
        }
    }
}
