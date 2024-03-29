﻿using System;
using System.Linq;
using System.Windows.Forms;
using Phoenix.Helpers;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public static class EffectDeps
    {
        private const string MESSAGE_ERROR = "Unable to track state changes!";

        /// <summary>
        /// A method observing the changes in the transmitted states.
        /// </summary>
        public static string[] Watch(params object[] parameters)
        {
            return parameters.ToList().Map((object el) =>
            {
                if (TypeMatchers.IsNullOrEmpty(el))
                {
                    throw new PhoenixException(MESSAGE_ERROR, new ArgumentException(MESSAGE_ERROR, el.ToString()));
                }
                
                if (el is Control || (el.GetType().BaseType.Name == typeof(Observer<>).Name))
                {
                    return el.GetType().GetProperty("Name").GetValue(el).ToString();
                }
                else if (typeof(string) == el.GetType()) 
                {
                    return el.ToString();
                }
                else
                {
                    throw new PhoenixException(MESSAGE_ERROR, new ArgumentException(MESSAGE_ERROR, el.ToString()));
                }
            }).Distinct().ToArray();
        }
    }
}
