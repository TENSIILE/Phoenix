using Phoenix.Helpers;

namespace Phoenix.Core
{
    public class State<T>
    {
        private readonly string _name;

        private readonly Store _store;
        private T _valueState;

        public State(T value, Store store)
        {
            _valueState = value;
            _store = store;

            _name = GenerateId();
        }

        /// <summary>
        /// An accessor that returns and sets the value of a state instance.
        /// </summary>
        public T Value
        {
            get => _valueState;
            set
            {
                _valueState = value;
                _store.Dispatch(_name, _valueState);
            }
        }

        /// <summary>
        /// An accessor that returns the name of a state instance.
        /// </summary>
        public string Name => _name; 

        private string GenerateId()
        {
            return $@"__{Utils.GetUniqueId()}_state";
        }
    }
}