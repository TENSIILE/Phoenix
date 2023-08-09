namespace Phoenix.Core
{
    public class ReducerAction
    {
        public string Type { get; private set; }
        public dynamic Payload { get; private set; }

        public ReducerAction(string type, dynamic payload)
        {
            Type = type;
            Payload = payload;
        }
    }
    
    public delegate T ReducerActionCallback<T>(State<T> state, ReducerAction action);

    public sealed class Reducer<T> : Observer<T>
    {
        private readonly ReducerActionCallback<T> _reducer;
        private readonly State<T> _state;

        /// <summary>
        /// An accessor that returns the value of a reducer instance.
        /// </summary>
        public override T Value => _state.Value;

        public Reducer(ReducerActionCallback<T> reducer, T initialState, Store store, bool isRunHiddenDispatch = true) : base(initialState)
        {
            _reducer = reducer;

            _state = new State<T>(initialState, store, isRunHiddenDispatch, Name);

            store.HiddenDispatch(Name, initialState);
        }

        /// <summary>
        /// The method changes state relative to the passed type.
        /// </summary>
        public void Dispatch(string type, dynamic payload = null)
        {
            _state.Value = _reducer(_state, new ReducerAction(type, payload));
        }

        /// <summary>
        /// The method generates a unique name for the state.
        /// </summary>
        protected override string GenerateName()
        {
            return $@"__{base.GenerateName()}_reducer";
        }
    }
}