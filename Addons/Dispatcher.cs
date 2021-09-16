using System.Collections.Generic;

namespace Phoenix.Addons
{
    public class Dispatcher
    {
        private Dictionary<string, List<DispatcherAction>> _listeners = new Dictionary<string, List<DispatcherAction>>();

        /// <summary>
        /// A method that allows you to subscribe to an event.
        /// </summary>
        public void On(string eventName, DispatcherAction func)
        {
            if (_listeners.ContainsKey(eventName))
            {
                _listeners[eventName].Add(func);
                return;
            }

            List<DispatcherAction> delegateActions = new List<DispatcherAction>();
            delegateActions.Add(func);

            _listeners.Add(eventName, delegateActions);
        }

        /// <summary>
        /// The method unsubscribes from the event.
        /// </summary>
        public void Off(string eventName)
        {
            if (_listeners.ContainsKey(eventName))
            {
                _listeners.Remove(eventName);
            }
        }

        /// <summary>
        /// A method that allows you to simulate a specific event.
        /// </summary>
        public void Emit(string eventName, dynamic data = null)
        {
            if (!_listeners.ContainsKey(eventName)) return;

            _listeners[eventName].ForEach((DispatcherAction handler) => handler(data));
        }

        public delegate void DispatcherAction(dynamic arg);
    }
}