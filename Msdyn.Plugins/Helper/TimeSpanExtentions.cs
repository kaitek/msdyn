using System;

namespace Msdyn.Plugins.Helper
{
    public static class TimeSpanExtentions
    {
        public static string TimeSpanConvert(this TimeSpan elapsed)
        {
            const string format = " {0:00}:{1:00}:{2:00}.{3:000000}";
            return string.Format(format,
                      elapsed.Hours, elapsed.Minutes,
                      elapsed.Seconds, elapsed.Milliseconds * 1000);
        }

    }
}
