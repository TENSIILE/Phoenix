using System;
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
        /// State tracking hook method.
        /// </summary>
        protected string UseEffect(string formName, Action callback, string[] deps, bool isRunStartAway = false, string storeType = StoreTypes.LOCAL)
        {
            if (storeType == StoreTypes.LOCAL)
            {
                return PContainer.Get(formName).Store.Effect(callback, deps, isRunStartAway);
            }

            throw new PhoenixException("You cannot refer to the global store while referring to the store for a specific form!");
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
            return new Broadcast(TypeMatchers.IsNull(store) ? Store : store);
        }

        /// <summary>
        /// A hook that guarantees that a value with a specific key exists in the store.
        /// And if so, then the callback will be executed.
        /// </summary>
        protected void UseEnsure<T>(string key, Action<T> callback, string storeType = StoreTypes.LOCAL)
        {
            new Ensurer(this).Insure(key, callback, storeType);
        }

        /// <summary>
        /// A hook that guarantees that a value with a specific key exists in the store.
        /// And if so, then the callback will be executed.
        /// </summary>
        protected void UseEnsure<T>(Observer<T> observer, Action<T> callback, string storeType = StoreTypes.LOCAL)
        {
            new Ensurer(this).Insure(observer.Name, callback, storeType);
        }
    }
}
