﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using Phoenix.Core;
using Phoenix.Helpers;
using Phoenix._System;

namespace Phoenix
{
    public partial class PhoenixForm : Form
    {
        private Store _localStore = new Store();
    
        private static Provider _provider = new Provider();

        private PrivatePhoenixFormDictionary _subComponents = new PrivatePhoenixFormDictionary();

        public delegate void TryCloseForm(Action onClose);

        /// <summary>
        /// The accessor that returns store of the form.
        /// </summary>
        [Browsable(false)]
        public Store Store => _localStore;

        /// <summary>
        /// The accessor that returns static provider of the form.
        /// </summary>
        [Browsable(false)]
        public static Provider Provider => _provider;

        /// <summary>
        /// An accessor called when the form is closed, allowing you to cancel or confirm the closing of the form.
        /// </summary>
        [Browsable(false)]
        protected TryCloseForm OnTryCloseForm { get; set; } = null;

        /// <summary>
        /// Returns a list of connected sub-components to the form.
        /// </summary>
        [Browsable(false)]
        protected PrivatePhoenixFormDictionary SubComponents => _subComponents;

        internal event Action<dynamic> FormDidHide;
        internal event Action<dynamic> FormDidShow;

        internal Store GetStoreByType(string storeType = StoreTypes.LOCAL)
        {
            return storeType == StoreTypes.LOCAL ? _localStore : Provider.GlobalStore;
        }

        /// <summary>
        /// A method that initializes the forms.
        /// </summary>
        protected void Init(params Type[] formTypes)
        {
            PContainer.Append(Name, this);
            InitializeEvents();

            Provider.GlobalStore.StoreType = StoreTypes.GLOBAL;
            Provider.GlobalStore.CombinedStores += StoreCombined;

            formTypes.ToList().ForEach((Type formType) =>
            {
                FormActivator.ProtectsAndValidatesPassedTypes(formType);

                var formConstructor = FormActivator.CreateFormConstructor(formType);
                FormActivator.AddFormConstructor(formType.Name, formConstructor);
            });
        }

        /// <summary>
        /// Method for connecting a sub-component to a form.
        /// </summary>
        protected void ConnectSubComponents(params PhoenixForm[] components)
        {
            Mathf.For(components.Length, (i) => 
            {
                PhoenixForm component = components[i];

                _subComponents.Add(component.Name, component);
                component.InitializeEvents();
                component.EnableFormHiding();
            });
        }

        internal void InitializeEvents()
        {
            Shown += FormDidFirstMount;
            FormDidShow += FormDidMount;
            FormDidHide += FormWillUnmount;

            FormAddedListeners();

            _localStore.DidChangeStore += StoreDidUpdate;
            _localStore.WillChangeStore += StoreWillUpdate;
            _localStore.Render += Render;

            _localStore.CombinedStores += StoreCombined;
        }

        /// <summary>
        /// A method to disable full form closing.
        /// </summary>
        internal void EnableFormHiding()
        {
            FormClosing += new FormClosingEventHandler(PhoenixClosing);
        }

        /// <summary>
        /// A method to override disabling the closure of a form.
        /// </summary>
        internal void DisableFormHiding()
        {
            FormClosing -= new FormClosingEventHandler(PhoenixClosing);
        }

        private PhoenixException GetComponentException(string name)
        {
            return new PhoenixException(
                $@"The component with the name ['{name}'] does not exist on the form - [{Name}]!",
                new KeyNotFoundException()
            );
        }

        /// <summary>
        /// A method for searching a component on a form by its name. 
        /// As Generic, the type is passed to the type that will need to be returned on success.
        /// </summary>
        public T GetComponent<T>(string name) where T : Control
        {
            try
            {
                return Converting.ToType<T>(Controls.Find(name, true).ToArray()[0]);
            }
            catch (Exception)
            {
                throw GetComponentException(name);
            }
        }

        /// <summary>
        /// A method for searching a component on a form by its name. 
        /// </summary>
        public Control GetComponent(string name)
        {
            try
            {
                return Controls.Find(name, true).ToArray()[0];
            }
            catch (Exception)
            {
                throw GetComponentException(name);
            }
        }

        /// <summary>
        /// A method for finding components on a form by their type.
        /// Returns an array of found components in the form.
        /// </summary>
        public T[] GetComponent<T>() where T : Control
        {
            List<T> result = new List<T>();

            foreach (Control control in Controls.OfType<T>())
                result.Add((T)control);

            return result.ToArray();
        }

        /// <summary>
        /// The method hides the control from the user.
        /// </summary>
        public void Hide(dynamic args = null)
        {
            base.Hide();
            FormDidHide?.Invoke(args);
        }

        /// <summary>
        /// Form display method.
        /// </summary>
        public void Show(dynamic args = null)
        {
            base.Show();
            FormDidShow?.Invoke(args);
        }

        /// <summary>
        /// The method completely destroys the form from the system.
        /// </summary>
        public void Destroy()
        {
            PContainer.Delete(Name);
            DisableFormHiding();
            Close();
        }

        private void PhoenixClosing(object sender, FormClosingEventArgs e)
        {
            Action hideAction = () =>
            {
                e.Cancel = true;
                Hide();
            };

            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                if (!TypeMatchers.IsNull(OnTryCloseForm))
                {
                    OnTryCloseForm(hideAction);
                    e.Cancel = true;
                    return;
                }

                hideAction();
            }
        }

        /// <summary>
        /// Lifecycle method, executed when the form is first launched.
        /// </summary>
        protected virtual void FormDidFirstMount(object sender, EventArgs e) { }
        /// <summary>
        /// Lifecycle method, executed when the form is created.
        /// </summary>
        protected virtual void FormDidMount(dynamic args) { }
        /// <summary>
        /// Lifecycle method, executed when the form is destroyed.
        /// </summary>
        protected virtual void FormWillUnmount(dynamic args) { }
        /// <summary>
        /// The lifecycle method is executed once when the form is first launched, 
        /// starting the listeners initialized in this method.
        /// </summary>
        protected virtual void FormAddedListeners() { }
        /// <summary>
        /// The store lifecycle method, which is called after it has been updated.
        /// </summary>
        protected virtual void StoreDidUpdate(Storage prevStore, Storage currentStore) { }
        /// <summary>
        /// The store lifecycle method, which is called before updating it.
        /// </summary>
        protected virtual void StoreWillUpdate(Storage prevStore, Storage currentStore) { }
        /// <summary>
        /// The lifecycle method is triggered when the local or global store of the form has been combined.
        /// </summary>
        protected virtual void StoreCombined(string storeType) { }
        /// <summary>
        /// A lifecycle method that is called after a data update to display up-to-date information.
        /// </summary>
        protected virtual void Render() {}
    }
}
