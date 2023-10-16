using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Common
{
    public class Md5Helper
    {
        public static string Encrypt(string value, MD5Type type = MD5Type.MD32)
        {
            var md5 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(value);
            byte[] md5_b = md5.ComputeHash(b);
            md5.Clear();

            var sb = new StringBuilder();
            foreach (var item in md5_b)
            {
                sb.Append(item.ToString("x2"));
            }

            string md5Str = sb.ToString();

            if ((int)type == (int)MD5Type.MD16)
            {
                md5Str = md5Str.Substring(8, 16);
            }
            return md5Str;
        }       
    }

    public enum MD5Type
    {
        MD16 = 0,
        MD32 = 1
    }
}
