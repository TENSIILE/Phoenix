using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Phoenix.Helpers;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public class Binder
    {
        private readonly Dictionary<string, List<dynamic>> _targets = new Dictionary<string, List<dynamic>>();

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
                throw new PhoenixException(
                    "The key is empty or null!", 
                    new ArgumentNullException("keyStore")
                );
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

        protected void UpdateTargets<T>(string type, T payload)
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
                            throw new PhoenixException(
                                "Unable to find the output property for this component!",
                                new MissingFieldException()
                            );
                        }
                    }
                }
            }
        }

        protected delegate void DispatchCallback<T>(string type, T payload);

        protected void UpdatePropertiesTargetWithSettings<T>(string type, T payload, string property, bool isAddToStore, DispatchCallback<T> dispatch)
        {
            foreach (KeyValuePair<string, List<dynamic>> target in _targets)
            {
                if (target.Key == type)
                {
                    foreach (dynamic item in target.Value)
                    {
                        try
                        {
                            if (isAddToStore) dispatch(item.Name, payload);

                            ((object)item).GetType().GetProperty(property).SetValue(item, payload);
                        }
                        catch (NullReferenceException)
                        {
                            throw new PhoenixException(
                                $@"The property ['{property}'] was not found for the component!",
                                new ArgumentException()
                            );
                        }
                    }
                }
            }
        }
    }
}
