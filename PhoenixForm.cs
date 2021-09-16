using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using Phoenix.Core;
using Phoenix.Helpers;

namespace Phoenix
{
    public partial class PhoenixForm : Form
    {
        private static Store _store = new Store();
        private static Provider _provider = new Provider();

        /// <summary>
        /// Static accessor that returns static store of the form.
        /// </summary>
        [Browsable(false)]
        public static Store Store => _store;

        /// <summary>
        /// The accessor that returns static store of the form.
        /// </summary>
        [Browsable(false)]
        public Store GetStaticStore => _store;

        /// <summary>
        /// Static accessor that returns static provider of the form.
        /// </summary>
        [Browsable(false)]
        public static Provider Provider => _provider;

        /// <summary>
        /// The accessor that returns static provider of the form.
        /// </summary>
        [Browsable(false)]
        public Provider GetStaticProvider => _provider;

        internal event Action FormDidHide;
        internal event Action FormDidShow;

        /// <summary>
        /// A method that initializes the form.
        /// </summary>
        protected void Init()
        {
            PhoenixContainerForms.Append(Name, this);
            InitializeEvents();
        }

        internal void InitializeEvents()
        {
            Shown += FormDidFirstMount;
            FormDidShow += FormDidMount;
            FormDidHide += FormWillUnmount;

            FormDidAddedListeners();

            _store.DidChangeStore += StoreDidUpdate;
            _store.WillChangeStore += StoreWillUpdate;
            _store.Render += Render;
        }

        /// <summary>
        /// A method to disable full form closing.
        /// </summary>
        public void EnableFormHiding()
        {
            FormClosing += new FormClosingEventHandler(PhoenixClosing);
        }

        /// <summary>
        /// A method to override disabling the closure of a form.
        /// </summary>
        public void DisableFormHiding()
        {
            FormClosing -= new FormClosingEventHandler(PhoenixClosing);
        }

        /// <summary>
        /// A method for searching a component on a form by its name. 
        /// As Generic, the type is passed to the type that will need to be returned on success.
        /// </summary>
        public T GetComponent<T>(string name) where T : Control
        {
            try
            {
                return (T)Convert.ChangeType(Controls.Find(name, true).ToArray()[0], typeof(T));
            }
            catch (Exception)
            {
                throw new KeyNotFoundException($@"The component with the name ['{name}'] does not exist on the form - {Name}!");
            }
        }

        /// <summary>
        /// A method for finding components on a form by their type.
        /// Returns an array of found components in the form.
        /// You can also exclude specific components by their name when searching.
        /// </summary>
        public T[] GetComponent<T>(string[] nameExceptions = null) where T : Control
        {
            List<T> result = new List<T>();

            foreach (Control control in Controls.OfType<T>())
            {
                result.Add((T)control);
            }

            if (TypeMatchers.IsNull(nameExceptions))
            {
                return result.ToArray();
            }

            foreach (string exception in nameExceptions)
            {
                int index = result.FindIndex((T el) => el.Name == exception);

                result.RemoveAt(index);
            }

            return result.ToArray();
        }

        /// <summary>
        /// A method to completely close the application.
        /// </summary>
        protected void Destroy()
        {
            Application.Exit();
        }

        /// <summary>
        /// The method hides the control from the user.
        /// </summary>
        public new void Hide()
        {
            base.Hide();
            FormDidHide?.Invoke();
        }

        /// <summary>
        /// Form showing method.
        /// </summary>
        public new void Show()
        {
            base.Show();
            FormDidShow?.Invoke();
        }

        private void PhoenixClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                Hide();
            }
        }

        /// <summary>
        /// Lifecycle method, executed when the form is first launched.
        /// </summary>
        protected virtual void FormDidFirstMount(object sender, EventArgs e) { }
        /// <summary>
        /// Lifecycle method, executed when the form is created.
        /// </summary>
        protected virtual void FormDidMount() { }
        /// <summary>
        /// Lifecycle method, executed when the form is destroyed.
        /// </summary>
        protected virtual void FormWillUnmount() { }
        /// <summary>
        /// The lifecycle method is executed once when the form is first launched, 
        /// starting the listeners initialized in this method.
        /// </summary>
        protected virtual void FormDidAddedListeners() { }
        /// <summary>
        /// The store lifecycle method, which is called after it has been updated.
        /// </summary>
        protected virtual void StoreDidUpdate(Storage storePrev, Storage storeNow) { }
        /// <summary>
        /// The store lifecycle method, which is called before updating it.
        /// </summary>
        protected virtual void StoreWillUpdate(Storage storePrev, Storage storeNow) { }
        /// <summary>
        /// A lifecycle method that is called after a data update to display up-to-date information.
        /// </summary>
        protected virtual void Render() {}
    }
}
