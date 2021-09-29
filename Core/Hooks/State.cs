namespace Phoenix.Core
{
    public sealed class State<T> : Observer<T>
    {
        private readonly Store _store;

        public State(T value, Store store) : base(value)
        {
            _store = store;
        }

        /// <summary>
        /// An accessor that returns and sets the value of a state instance.
        /// </summary>
        public override T Value
        {
            get => base.Value;
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