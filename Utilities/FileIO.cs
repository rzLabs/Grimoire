using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grimoire.Utilities
{
    public static class FileIO
    {
        public static long Length(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path).Length;
            }
            else
                throw new FileNotFoundException("File not found at path!", path);
        }

        public static long Length(string[] paths)
        {
            long len = 0;

            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    len += new FileInfo(path).Length;
                }
                else
                    throw new FileNotFoundException("File not found at path!", path);
            }

            return len;
        }
    }
}
