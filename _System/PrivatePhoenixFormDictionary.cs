using System.Collections.Generic;

namespace Phoenix._System
{
    public class PrivatePhoenixFormDictionary : PrivateDictionary<PhoenixForm>
    {
        public PrivatePhoenixFormDictionary(Dictionary<string, PhoenixForm> dict = null) : base(dict) { }

        /// <summary>
        /// Method for obtaining PhoenixForm.
        /// </summary>
        public PhoenixForm Get(PhoenixForm form) => _dict[form.Name];
    }
}
