using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Phoenix.Helpers;
using Phoenix.Extentions;
using Phoenix.Core.Mounting;

namespace Phoenix.Core
{
    public class Router
    {
        private readonly Dictionary<string, PhoenixForm> _pages = new Dictionary<string, PhoenixForm>();

        private readonly Panel _panel;

        private string _route;

        /// <summary>
        /// An accessor that returns the value of the current route.
        /// </summary>
        public string GetRoute => _route;
        
        public Router(Panel panel)
        {
            _panel = panel;
        }

        /// <summary>
        /// A method that renders the component according to its route name.
        /// </summary>
        public void SetRoute(string name)
        {
            PhoenixForm form = _pages.Get(name);

            if (TypeMatchers.IsNull(form))
            {
                Mounter.UnmountComponent(_panel);
                return;
            }

            if (((PhoenixForm)_panel.Tag)?.Name == form.Name) return;

            _route = name;

            Mounter.MountComponent(_panel, form);
        }

        /// <summary>
        /// A method that creates a new route in a router.
        /// </summary>
        public void CreateRoute(string name, PhoenixForm form)
        {
            _pages.Add(name, form);
        }

        /// <summary>
        /// A method that returns the current component according to the current route.
        /// </summary>
        public PhoenixForm GetFormFromCurrentRoute()
        {
            return _panel.Controls.OfType<PhoenixForm>().First();
        }
    }
}
