using System;
using Phoenix.Helpers;

namespace Phoenix.Core
{
    internal class Ensurer
    {
        private PhoenixForm _form;

        public Ensurer(PhoenixForm form) => _form = form;

        internal void Insure<T>(string key, Action<T> callback, string storeType)
        {
            dynamic value;

            _form.GetStoreByType(storeType).GetState.TryGetValue(key, out value);

            if (!TypeMatchers.IsNull<dynamic>(value))
            {
                callback(Converting.ToType<T>(value));
            } 
        }
    }
}
