using System;

namespace Grimoire.Utilities
{
    public static class StringExt
    {
        static string[] sizes = { "B", "KB", "MB", "GB", "TB" };

        public static string FormatToSize(long length)
        {
            double len = length;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
                           
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

        public static string MilisecondsToTime(long miliseconds)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(miliseconds);
            return string.Format("{0:D2}s:{1:D3}ms",
                                    t.Seconds,
                                    t.Milliseconds);
        }
    }
}
