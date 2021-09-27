using Phoenix.Helpers;

namespace Phoenix.Core
{
    public class Observer<T>
    {
        private readonly string _name;
        private T _value;

        /// <summary>
        /// An accessor that returns the value of a observer instance.
        /// </summary>
        public virtual T Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// An accessor that returns the name of a observer instance.
        /// </summary>
        public string Name => _name;

        public Observer(T instanceValue)
        {
            _value = instanceValue;
            _name = GenerateName();
        }

        /// <summary>
        /// The method generates a unique name for the state.
        /// </summary>
        protected virtual string GenerateName()
        {
            return $@"__{Utils.GetUniqueId()}_state";
        }
    }
}
