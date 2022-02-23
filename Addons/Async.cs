using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Phoenix.Helpers;

namespace Phoenix.Addons
{
    public static class Async
    {
        private readonly static Dictionary<string, Timer> _stackTimeouts = new Dictionary<string, Timer>();
        private readonly static Dictionary<string, Timer> _stackIntervals = new Dictionary<string, Timer>();

        /// <summary>
        /// The method calls the callback function after a specified number of seconds.
        /// </summary>
        public static string SetTimeout(Action callback, int ms)
        {
            string id = Utils.UuidV1();

            Timer timer = Register(callback, ms, true);

            _stackTimeouts.Add(id, timer);
            timer.Start();

            return id;
        }

        /// <summary>
        /// The method invokes the callback function infinitely after a specified number of seconds.
        /// </summary>
        public static string SetInterval(Action callback, int ms)
        {
            string id = Utils.UuidV1();

            Timer timer = Register(callback, ms);

            _stackIntervals.Add(id, timer);
            timer.Start();

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

        private static bool Clear(Dictionary<string, Timer> stack, string id)
        {
            if (stack.ContainsKey(id))
            {
                stack[id].Stop();
                stack[id].Dispose();
                stack.Remove(id);

                return true;
            }

            return false;
        }

        private static Timer Register(Action callback, int ms, bool isTimeout = false)
        {
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = ms;

            timer.Tick += (object _, EventArgs e) =>
            {
                callback();
                if (isTimeout) timer.Stop();
            };

            return timer;
        }
    }
}
