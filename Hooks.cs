using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.Core;

namespace Phoenix
{
    public partial class PhoenixForm
    {
        /// <summary>
        /// Hook method that creates the state of the form.
        /// </summary>
        protected static State<T> UseState<T>(T value)
        {
            return new State<T>(value, _store);
        }

        /// <summary>
        /// State tracking hook method.
        /// </summary>
        protected string UseEffect(Action callback, string[] deps, bool isRunStartAway = false)
        {
            return _store.Effect(callback, deps, isRunStartAway);
        }

        /// <summary>
        /// A method for canceling the state tracking hook.
        /// </summary>
        protected void UseCancelEffect(string id)
        {
            _store.CancelEffect(id);
        }

        /// <summary>
        /// An alternative to UseState. 
        /// Accepts a reducer of type (state, action) => newState, and returns the current state paired with a dispatch method.
        /// (If you’re familiar with Redux, you already know how this works.)
        /// </summary>
        protected Reducer<T> UseReducer<T>(ReducerActionCallback<T> reducer, T initialState)
        {
            return new Reducer<T>(this, reducer, initialState);
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
