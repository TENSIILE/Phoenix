using Phoenix.Helpers;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public sealed class State<T> : Observer<T>
    {
        private readonly Store _store;
        private readonly bool _isRunHiddenDispatch;

        public State(T value, Store store, bool isRunHiddenDispatch = true, string name = null) : base(value)
        {
            _store = store;
            _isRunHiddenDispatch = isRunHiddenDispatch;

            if (!TypeMatchers.IsNull(name))
                Name = name;

            if (isRunHiddenDispatch)
                _store.HiddenDispatch(Name, value);
        }

        /// <summary>
        /// An accessor that returns and sets the value of a state instance.
        /// </summary>
        public override T Value
        {
            get
            {
                return Converting.ToType<T>
                (
                    _isRunHiddenDispatch      ?
                    _store.GetState.Get(Name) :
                    base.Value
                );
            }
            set
            {
                base.Value = value;
                _store.Dispatch(Name, base.Value);
            }
        }

        /// <summary>
        /// The method generates a unique name for the state.
        /// </summary>
        protected override string GenerateName()
        {
            return $@"__{base.GenerateName()}_state"; 
        }
    }
}