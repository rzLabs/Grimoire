using System;
using System.IO;
using System.Text;

using System.Runtime.Serialization.Formatters.Binary;

namespace Grimoire.Utilities
{
    public class ByteConverterExt
    {
        /// <summary>
        /// Convert byte array to encoded string
        /// </summary>
        /// <param name="inBuffer"><c>byte[]</c> containing string data</param>
        /// <returns>Compressed (trimmed of nulls) string</returns>
        public static string ToString(byte[] inBuffer)
        {
            int index = -1;

            for (int i = 0; i < inBuffer.Length; i++)
            {
                if (inBuffer[i] == 0)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return Encoding.Default.GetString(inBuffer);

            return Encoding.Default.GetString(inBuffer, 0, index);
        }

        public static Byte[] ToBytes(string str)
        {
            Byte[] bytes = null;
            bytes = Encoding.GetEncoding("ASCII").GetBytes(str);
            return bytes;
        }

        public static byte[] ToBytes(string str, int length, Encoding encoding)
        {
            byte[] strBuffer = encoding.GetBytes(str);
            byte[] outBuffer = new byte[length];

            Buffer.BlockCopy(strBuffer, 0, outBuffer, 0, strBuffer.Length);

            return outBuffer;
        }

        public static byte[] ToByte<T>(object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T FromBytes<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}
