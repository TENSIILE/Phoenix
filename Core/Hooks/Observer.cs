using System;
using Phoenix.Helpers;

namespace Phoenix.Core
{
    internal static class ObserverCounter
    {
        public static int Counter { get; set; } = 0;
    }

    public class Observer<T>
    {
        private string _name;
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
        public string Name
        {
            get => _name;
            protected set
            {
                _name = value;
            }
        }

        public Observer(T instanceValue)
        {
            ++ObserverCounter.Counter;

            _value = instanceValue;
            _name = GenerateName();
        }

        /// <summary>
        /// The method generates a unique name for the state.
        /// </summary>
        protected virtual string GenerateName()
        {
            Random random = new Random();

            int randomValue = random.Next(int.MinValue, int.MaxValue);

            string substring = ObserverCounter.Counter.ToString() + randomValue + _value;

            return Utils.GetUniqueId(substring);
        }
    }
}
