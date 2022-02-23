using System;
using System.Collections.Generic;
using System.Threading;
using Phoenix.Helpers;

namespace Phoenix.Addons
{
    public static class AsyncThread
    {
        private readonly static Dictionary<string, Thread> _stackTimeouts = new Dictionary<string, Thread>();
        private readonly static Dictionary<string, Thread> _stackIntervals = new Dictionary<string, Thread>();

        /// <summary>
        /// The method calls the callback function after a specified number of seconds.
        /// </summary>
        public static string SetTimeout(Action callback, int ms)
        {
            string id = Utils.UuidV1();

            RegisterTimeout(id ,callback, ms);

            return id;
        }

        /// <summary>
        /// The method invokes the callback function infinitely after a specified number of seconds.
        /// </summary>
        public static string SetInterval(Action callback, int ms)
        {
            string id = Utils.UuidV1();

            RegisterInterval(id, callback, ms);

            return id;
        }

        /// <summary>
        /// The method interrupts the execution of SetTimeout.
        /// </summary>
        public static bool ClearTimeout(string id)
        {
            return Clear(_stackTimeouts, id);
        }

        /// <summary>
        /// The method interrupts the execution of SetInterval.
        /// </summary>
        public static bool ClearInterval(string id)
        {
            return Clear(_stackIntervals, id);
        }

        private static bool Clear(Dictionary<string, Thread> stack, string id)
        {
            if (stack.ContainsKey(id))
            {
                stack[id].Abort();
                stack.Remove(id);

                return true;
            }

            return false;
        }

        private static void RegisterTimeout(string id, Action callback, int ms)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(ms);
                callback();
            }));

            _stackTimeouts.Add(id, thread);

            thread.Start();
        }

        private static void RegisterInterval(string id, Action callback, int ms)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    Thread.Sleep(ms);
                    callback();
                }
            }));

            _stackIntervals.Add(id, thread);

            thread.Start();
        }
    }
}
