using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Phoenix.Helpers;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public class Store
    {
        private Storage _storeOld = new Storage(new Dictionary<string, dynamic>());
        private Storage _store = new Storage(new Dictionary<string, dynamic>());

        private List<Action> _subscribers = new List<Action>();
        private List<Tuple<string[], Action, string>> _effects = new List<Tuple<string[], Action, string>>();

        private Dictionary<string, List<dynamic>> _targets = new Dictionary<string, List<dynamic>>();

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

        /// <summary>
        /// Store subscription method. Callback functions will be called whenever the store changes.
        /// </summary>
        public void Subscribe(Action callback)
        {
            _subscribers.Add(callback);
        }

        /// <summary>
        /// State tracking method.
        /// </summary>
        public string Effect(Action callback, string[] deps, bool isRunStartAway = false)
        {
            string id = Utils.GetUniqueId();

            Tuple<string[], Action, string> effect = new Tuple<string[], Action, string>(deps, callback, id);

            _effects.Add(effect);

            if (isRunStartAway) callback();

            return id;
        }

        /// <summary>
        /// A method for canceling the state tracking.
        /// </summary>
        public void CancelEffect(string id)
        {
            int index =_effects.FindIndex((Tuple<string[], Action, string> effect) => effect.Item3 == id);

            _effects.RemoveAt(index);
        }

        private void CallEffects()
        {
            foreach (Tuple<string[], Action, string> effect in _effects)
            {
                foreach (string dep in effect.Item1)
                {
                    if (!Mathf.IsWeakEqual(GetState.Has(dep), GetStatePrev.Has(dep)))
                    {
                        effect.Item2();
                    }
                }
            }
        }

        /// <summary>
        /// A method for binding a value from a store to a component. Where the name of a cell in the store can be a component.
        /// </summary>
        public void Bind<T>(Control observeComponent, T target) where T : Control
        {
            _Bind(observeComponent.Name, target);
        }

        /// <summary>
        /// A method for binding a value from a store to a component.
        /// </summary>
        public void Bind<T>(string keyStore, T target) where T : Control
        {
            _Bind(keyStore, target);
        }

        private void _Bind<T>(string keyStore, T target) where T : Control
        {
            if (TypeMatchers.IsNullOrEmpty(keyStore))
            {
                throw new ArgumentException("The key is empty or null!", "keyStore");
            }

            if (_targets.ContainsKey(keyStore))
            {
                _targets[keyStore].Add(target);
                return;
            }

            _targets.Add(keyStore, new List<dynamic>() { target });
        }

        /// <summary>
        /// A method for unbinding a value from a store to a component.
        /// </summary>
        public bool Unbind<T>(string keyStore, T target) where T : Control
        {
            return _Unbind(keyStore, target);
        }

        /// <summary>
        /// A method for unbinding a value from a store to a component.
        /// </summary>
        public bool Unbind<T>(Control observeComponent, T target) where T : Control
        {
            return _Unbind(observeComponent.Name, target);
        }

        private bool _Unbind<T>(string keyStore, T target) where T : Control
        {
            if (_targets.ContainsKey(keyStore) && !TypeMatchers.IsNull(target))
            {
                _targets[keyStore] = _targets[keyStore].Filter(el => el.Name != target.Name);
                return true;
            }

            return false;
        }

        private void ReactiveUpdateTargets<T>(string type, T payload)
        {
            foreach (KeyValuePair<string, List<dynamic>> target in _targets)
            {
                if (target.Key == type)
                {
                    foreach (dynamic item in target.Value)
                    {
                        if (Utils.ExistsProperty<Control>("Value"))
                        {
                            item.Value = payload.ToString();
                        }
                        else if (Utils.ExistsProperty<Control>("Text"))
                        {
                            item.Text = payload.ToString();
                        }
                        else
                        {
                            throw new MissingFieldException("Unable to find the output property for this component!");
                        }
                    }
                }
            }
        }

        private void ReactiveUpdatePropertiesTargetWithSettings<T>(string type, T payload, string property, bool isAddToStore)
        {
            foreach (KeyValuePair<string, List<dynamic>> target in _targets)
            {
                if (target.Key == type)
                {
                    foreach (dynamic item in target.Value)
                    {
                        try
                        {
                            if (isAddToStore) Dispatch(item.Name, payload);

                            ((object)item).GetType().GetProperty(property).SetValue(item, payload);
                        }
                        catch (NullReferenceException)
                        {
                            throw new ArgumentException($@"The property ['{property}'] was not found for the component!");
                        }
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
            ReactiveUpdateTargets(observeComponent.Name, observeComponent.Text);
        }

        /// <summary>
        /// An advanced method for sending data to the store as a component. 
        /// When using this method, component binding with advanced binding customization is available.
        /// </summary>
        public void DispatchAsComponentExtended<T, R>(T observeComponent, R value, string property, bool isAddToStore = false) where T : Control
        {
            Dispatch(observeComponent.Name, observeComponent.Text);
            ReactiveUpdatePropertiesTargetWithSettings(observeComponent.Name, value, property, isAddToStore);
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
        /// A method that checks the existence of a method for the passed type.
        /// </summary>
        public Store CombineStores(params Store[] stores)
        {
            foreach (Store store in stores)
            {
                foreach (KeyValuePair<string, dynamic> item in store._store)
                {
                    _store.Add(item.Key, item.Value);
                }
            }

            return this;
        }
    }
}