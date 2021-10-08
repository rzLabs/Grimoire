using System;

namespace Grimoire.Utilities
{
    /// <summary>
    /// Set of methods to provide string manipulation
    /// </summary>
    public static class StringExt
    {
        static string[] sizes = { "B", "KB", "MB", "GB", "TB" };

        /// <summary>
        /// Convert a files raw length to a formatted string like 1MB
        /// </summary>
        /// <param name="length">Length to be converted</param>
        /// <returns>Formatted size string</returns>
        public static string SizeToString(long length)
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

        /// <summary>
        /// Convert a timespan in miliseconds to a formatted string like 1s 200ms
        /// </summary>
        /// <param name="miliseconds">Miliseconds count to be converted</param>
        /// <returns>Formatted time string</returns>
        public static string MilisecondsToString(long miliseconds)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(miliseconds);

            string timeStr = null;

            if (miliseconds > 1000)
                timeStr = string.Format("{0:D2}s {1:D3}ms", t.Seconds, t.Milliseconds);
            else
                timeStr = string.Format("{0:D3}ms", t.Milliseconds);

            return timeStr;
        }

        /// <summary>
        /// Convert a byte array to a prepared string capable of representing the array via hex values.
        /// </summary>
        /// <param name="buffer">Byte array containing information</param>
        /// <returns>Formatted string representing the provided byte array</returns>
        public static string ByteArrayToString(byte[] buffer)
        {
            string outStr = null;
            string curRowStr = null;
            int curCol = 0;

            for (int i = 0; i <= buffer.Length; i++)
            {
                if (i == buffer.Length)
                {
                    outStr = outStr.Remove(outStr.Length - 2, 1);
                    break;
                }

                string byteStr = $"0x{buffer[i].ToString("x2")}";
                curRowStr += $"{byteStr},";
                curCol++;

                if (curCol == 16)
                {
                    outStr += $"{curRowStr}\n";
                    curRowStr = null;
                    curCol = 0;
                }
            }

            return outStr;
        }
    }
}
