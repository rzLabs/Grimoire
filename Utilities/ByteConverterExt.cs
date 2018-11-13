using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Utilities
{
    public class ByteConverterExt
    {
        public static string ToString(byte[] b)
        {
            int num = 0;
            for (int i = 0; i < (int)b.Length && b[i] > 0; i++)
            {
                num++;
            }
            byte[] numArray = new byte[num];
            for (int i = 0; i < num && b[i] > 0; i++)
            {
                numArray[i] = b[i];
            }

            return Encoding.ASCII.GetString(numArray);
        }

        public static Byte[] ToBytes(string str)
        {
            Byte[] bytes = null;
            bytes = Encoding.GetEncoding("ASCII").GetBytes(str);
            return bytes;
        }

        public static Byte[] ToBytes(string str, int size)
        {
            Byte[] bytes = new Byte[size];
            byte[] buffer = Encoding.GetEncoding("ASCII").GetBytes(str);
            try
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    bytes[i] = buffer[i];
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show(Encoding.Default.GetString(buffer));
            }
            return bytes;
        }
    }
}
