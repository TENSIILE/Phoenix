﻿using System.Collections.Generic;
using Phoenix.Core;
using Phoenix.Helpers;
using Phoenix.Extentions;

namespace Phoenix
{
    internal class PContainerFormsType : Dictionary<string, PhoenixForm> { }

    public static class PContainer
    {
        private static PContainerFormsType _phoenixListForms = new PContainerFormsType();

        internal static bool CheckExistsForm(string formName)
        {
            return _phoenixListForms.Has(formName).ToBool();
        }

        private static PhoenixException GetException(string formName)
        {
            return new PhoenixException(
                $@"The container does not have a form with such a key - [{formName}]!",
                new KeyNotFoundException()
            );
        }

        /// <summary>
        /// The method returns the form by its name.
        /// </summary>
        public static T Get<T>(string formName) where T : PhoenixForm
        {
            try
            {
                FormActivator.TryActivateForm<T>(formName);
                return Converting.ToType<T>(_phoenixListForms.Get(formName));
            }
            catch (KeyNotFoundException)
            {
                throw GetException(formName);
            }
        }

        /// <summary>
        /// The method returns the form by its name.
        /// </summary>
        public static PhoenixForm Get(string formName)
        {
            try
            {
                FormActivator.TryActivateForm<PhoenixForm>(formName);
                return _phoenixListForms.Get(formName);
            }
            catch (KeyNotFoundException)
            {
                throw GetException(formName);
            }
        }

        /// <summary>
        /// Adds the form to the list.
        /// </summary>
        public static void Append(string key, PhoenixForm form)
        {
            _phoenixListForms.Add(key, form);
        }

        /// <summary>
        /// Removes the form from the list.
        /// </summary>
        public static void Delete(string key)
        {
            _phoenixListForms.Remove(key);
        }
    }
}
