using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace DKD.Framework.Utility.MD5
{
    public static class MD5Helper
    {
        public const String EncodingString = "dukedionly";//加密解密秘钥


        #region 字符加密

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">要被解密字符</param>
        /// <returns></returns>
        public static string Decrypt(this string text)
        {
            return Decrypt(text, EncodingString);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">要被解密字符</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public static string Decrypt(this string text, string sKey)
        {
            var provider = new DESCryptoServiceProvider();
            int num = text.Length / 2;
            byte[] buffer = new byte[num];
            try
            {
                for (int i = 0; i < num; i++)
                {
                    int num3 = Convert.ToInt32(text.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte)num3;
                }
            }
            catch
            {
                return string.Empty;
            }
            provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            try
            {
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
            }
            catch
            {
                return string.Empty;
            }
            return Encoding.Default.GetString(stream.ToArray());
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">要被加密字符</param> 
        public static string Encrypt(this string text)
        {
            return Encrypt(text, EncodingString);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">要被加密字符</param>
        /// <param name="sKey">密钥</param> 
        public static string Encrypt(this string text, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(text);
            provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }

        #endregion
    }
}
