using System;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public class Memo
    {
        private Store _store;

        public Memo(Store store) => _store = store;

        /// <summary>
        /// A method observing the changes in the transmitted states.
        /// </summary>
        public static string[] Watch(params object[] deps)
        {
            return EffectDeps.Watch(deps);
        }

        /// <summary>
        /// Memoizing data method.
        /// </summary>
        public void Memoize(Action callback, string[] deps)
        {
            foreach (string dep in deps)
            {
                if (_store.GetPrevState.Has(dep) != _store.GetState.Has(dep))
                {
                    callback();
                }
            }
        }
    }
}
