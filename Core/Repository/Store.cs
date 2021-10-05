using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Phoenix.Helpers;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public class Store : Binder
    {
        private Storage _storeOld = new Storage(new Dictionary<string, dynamic>());
        private readonly Storage _store = new Storage(new Dictionary<string, dynamic>());

        private readonly List<Action> _subscribers = new List<Action>();
        private readonly List<Tuple<string[], Action, string>> _effects = new List<Tuple<string[], Action, string>>();

        internal string StoreType { get; set; } = StoreTypes.LOCAL;

        public Store(Storage store = null)
        {
            if (store != null) _store = store;
        }

        /// <summary>
        /// An accessor that returns the current store.
        /// </summary>
        public Storage GetState => _store;

        /// <summary>
        /// An accessor that returns the previous store.
        /// </summary>
        public Storage GetStatePrev => _storeOld;

        internal event Action<Storage, Storage> DidChangeStore;
        internal event Action<Storage, Storage> WillChangeStore;
        internal event Action Render;

        internal event Action<string> CombinedStores;

        /// <summary>
        /// Store subscription method. Callback functions will be called whenever the store changes.
        /// </summary>
        public void Subscribe(Action callback)
        {
            _subscribers.Add(callback);
        }

        /// <summary>
        /// Method removing subscriber.
        /// </summary>
        public void Unsubscribe(Action callback)
        {
            _subscribers.Remove(callback);
        }

        /// <summary>
        /// State tracking method.
        /// </summary>
        public string Effect(Action callback, string[] deps, bool isRun = false)
        {
            string id = Utils.GetUniqueId(string.Join("", deps));

            Tuple<string[], Action, string> effect = new Tuple<string[], Action, string>(deps, callback, id);

            _effects.Add(effect);

            if (isRun) callback();

            return id;
        }

        /// <summary>
        /// A method for canceling the state tracking.
        /// </summary>
        public void CancelEffect(string id)
        {
            int index = _effects.FindIndex((Tuple<string[], Action, string> effect) => effect.Item3 == id);

            _effects.RemoveAt(index);
        }

        private void CallEffects()
        {
            foreach (Tuple<string[], Action, string> effect in _effects)
            {
                foreach (string dep in effect.Item1)
                {
                    if (!Mathf.IsStrongEqual(GetState.Has(dep), GetStatePrev.Has(dep)))
                    {
                        effect.Item2();
                    }
                }
            }
        }

        /// <summary>
        /// Method of sending data to storage.
        /// </summary>
        public void Dispatch<T>(string type, T payload) 
        {
            QuietDispatch(type, payload);

            foreach (var callback in _subscribers) callback();

            CallEffects();
        }

        /// <summary>
        /// A method for sending data to the store as a component.
        /// When using this method, component binding is available.
        /// </summary>
        public void DispatchAsComponent(Control observeComponent)
        {
            Dispatch(observeComponent.Name, observeComponent.Text);
            UpdateTargets(observeComponent.Name, observeComponent.Text);
        }

        /// <summary>
        /// An advanced method for sending data to the store as a component. 
        /// When using this method, component binding with advanced binding customization is available.
        /// </summary>
        public void DispatchAsComponentExtended<T, R>(T observeComponent, R value, string property, bool isAddToStore = false) where T : Control
        {
            Dispatch(observeComponent.Name, value);
            UpdatePropertiesTargetWithSettings(observeComponent.Name, value, property, isAddToStore, Dispatch);
        }

        /// <summary>
        /// A method to silently send data to the storage. 
        /// Using this method, no repository listeners will be notified of the changes.
        /// </summary>
        public void QuietDispatch<T>(string type, T payload)
        {
            if (payload is Control)
            {
                throw new ArgumentException("Payload cannot be a component!", "payload");
            }

            WillChangeStore?.Invoke(GetStatePrev, GetState);

            _storeOld = new Storage(_store);

            _store[type] = payload;

            DidChangeStore?.Invoke(GetStatePrev, GetState);

            Render?.Invoke();
        }

        /// <summary>
        /// A method that combines stores.
        /// </summary>
        public Store CombineStores(params Store[] stores)
        {
            bool isChanged = false;

            foreach (Store store in stores)
            {
                foreach (KeyValuePair<string, dynamic> item in store._store)
                {
                    _store.Add(item.Key, item.Value);
                    isChanged = true;
                }
            }

            if (isChanged) CombinedStores?.Invoke(StoreType);

            return this;
        }

        /// <summary>
        /// The method combines the stores with internal matching of the parent store and the passed stores. 
        /// In this case, the states with the same keys will be replaced with more relevant ones.
        /// </summary>
        public Store MergeStores(params Store[] stores)
        {
            bool isChanged = false;

            foreach (Store store in stores)
            {
                if (_store.Scan() == store._store.Scan()) continue;

                foreach (KeyValuePair<string, dynamic> item in store._store)
                {
                    _store.AddWithReplace(item.Key, item.Value);
                    isChanged = true;
                }
            }

            if (isChanged) CombinedStores?.Invoke(StoreType);

            return this;
        }
    }
}