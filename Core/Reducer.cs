namespace Phoenix.Core
{
    public class ReducerAction
    {
        public string Type;
        public dynamic Payload;

        public ReducerAction(string type, dynamic payload)
        {
            Type = type;
            Payload = payload;
        }
    }
    
    public delegate T ReducerActionCallback<T>(State<T> state, ReducerAction action);

    public class Reducer<T>
    {
        private readonly PhoenixForm _form;
        private readonly ReducerActionCallback<T> _reducer;
        private readonly State<T> _initialState;

        /// <summary>
        /// An accessor that returns the value of a reducer instance.
        /// </summary>
        public T Value => _initialState.Value;

        /// <summary>
        /// An accessor that returns the name of a reducer instance.
        /// </summary>
        public string Name => _initialState.Name;

        public Reducer(PhoenixForm form, ReducerActionCallback<T> reducer, T initialState)
        {
            _form = form;
            _reducer = reducer;
            _initialState = new State<T>(initialState, _form.StaticStore);
        }

        /// <summary>
        /// The method changes state relative to the passed type.
        /// </summary>
        public void Dispatch(string type, dynamic payload = null)
        {
            _initialState.Value = _reducer(_initialState, new ReducerAction(type, payload));
        }
    }
}