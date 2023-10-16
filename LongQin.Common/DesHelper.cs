using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Common
{
    public class DesHelper
    {
        static byte[] keyvi = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x05, 0x07, 0x07 };

        public static string Encrypt(string normalTxt, string EncryptKey)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);
            var key = Encoding.UTF8.GetBytes(EncryptKey.PadLeft(8, '0').Substring(0, 8));
            using (MemoryStream ms = new MemoryStream())
            {
                var encry = new DESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, encry.CreateEncryptor(key, keyvi), CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static string Decrypt(string securityTxt, string EncryptKey)
        {
            try
            {
                var bytes = Convert.FromBase64String(securityTxt);
                var key = Encoding.UTF8.GetBytes(EncryptKey.PadLeft(8, '0').Substring(0, 8));
                using (MemoryStream ms = new MemoryStream())
                {
                    var descrypt = new DESCryptoServiceProvider();
                    CryptoStream cs = new CryptoStream(ms, descrypt.CreateDecryptor(key, keyvi), CryptoStreamMode.Write);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
