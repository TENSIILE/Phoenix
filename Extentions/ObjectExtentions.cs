using Phoenix.Helpers;

namespace Phoenix.Extentions
{
    public static class ObjectExtentions
    {
        /// <summary>
        /// Converts the value of an object to an equivalent boolean value.
        /// </summary>
        public static bool ToBool(this object self)
        {
            return Converting.ObjectToBoolean(self) && Converting.IntOrDoubleToBoolean(self, true);
        }
    }
}