using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.Core;
using Phoenix.Helpers;

namespace Phoenix
{
    public partial class PhoenixForm
    {
        /// <summary>
        /// Hook method that creates the state of the form.
        /// </summary>
        protected State<T> UseState<T>(T value, string storeType = StoreTypes.LOCAL)
        {
            return new State<T>(value, GetStoreByType(storeType));
        }

        /// <summary>
        /// State tracking hook method.
        /// </summary>
        protected string UseEffect(Action callback, string[] deps, bool isRunStartAway = false, string storeType = StoreTypes.LOCAL)
        {
            return GetStoreByType(storeType).Effect(callback, deps, isRunStartAway);
        }

        /// <summary>
        /// A method for canceling the state tracking hook.
        /// </summary>
        protected void UseCancelEffect(string id, string storeType = StoreTypes.LOCAL)
        {
            GetStoreByType(storeType).CancelEffect(id);
        }

        /// <summary>
        /// An alternative to UseState. 
        /// Accepts a reducer of type (state, action) => newState, and returns the current state paired with a dispatch method.
        /// (If you’re familiar with Redux, you already know how this works.)
        /// </summary>
        protected Reducer<T> UseReducer<T>(ReducerActionCallback<T> reducer, T initialState, string storeType = StoreTypes.LOCAL)
        {
            return new Reducer<T>(reducer, initialState, GetStoreByType(storeType));
        }

        /// <summary>
        /// Hook method memorizes an unnecessary re render.
        /// </summary>
        protected void UseMemo(Action callback, string[] deps, string storeType = StoreTypes.LOCAL)
        {
            new Memo(GetStoreByType(storeType)).Memoize(callback, deps);
        }

        /// <summary>
        /// A hook that translates all changes to the specified repository to the global one.
        /// </summary>
        protected Broadcast UseBroadcast(Store store = null)
        {
            return new Broadcast(this, TypeMatchers.IsNull(store) ? Store : store);
        }

        /// <summary>
        /// Hook method creating a multiple window application.
        /// </summary>
        protected void UseCreateMWA(List<PhoenixForm> forms)
        {
            _UseCreateMWA(forms);
        }

        /// <summary>
        /// Hook method creating a multiple window application.
        /// </summary>
        protected void UseCreateMWA(params PhoenixForm[] forms)
        {
            _UseCreateMWA(forms.ToList());
        }

        private void _UseCreateMWA(List<PhoenixForm> forms)
        {
            Init();
            forms.ForEach((PhoenixForm form) =>
            {
                PContainer.Append(form.Name, form);
                form.InitializeEvents();
                form.EnableFormHiding();
            });
        }
    }
}
