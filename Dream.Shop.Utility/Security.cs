using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Shop.Utility
{
    /// <summary>
    /// 密码/验证码
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="originalString">待加密字符串</param>
        /// <returns>结果</returns>
        public static string MD5Encrypt16(string originalString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(originalString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="originalString">待加密字符串</param>
        /// <returns>结果</returns>
        public static string MD5Encrypt32(string originalString)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(originalString));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        /// <summary>
        /// RAS加密
        /// </summary>
        /// <param name="originalString">待加密字符串</param>
        /// <param name="key">密匙</param>
        /// <returns>结果</returns>
        public static string RSAEncrypt(string originalString, string key = "RSA")
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = key;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(originalString);
                byte[] encryptdata = rsa.Encrypt(plaindata, false);
                return Convert.ToBase64String(encryptdata);
            }
        }

        /// <summary>
        /// RAS解密
        /// </summary>
        /// <param name="securitylString">待加密字符串</param>
        /// <param name="key">密匙</param>
        /// <returns>结果</returns>
        public static string RSADecrypt(string securitylString, string key = "RSA")
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = key;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(securitylString);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="originalString">待加密字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>结果</returns>
        public static string DESEncrypt(string originalString, string key = "THISDESP", string iv = "PSEDSIHT")
        {
            string securtyString = null;
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIV = Encoding.UTF8.GetBytes(iv);
            byte[] inData = Encoding.UTF8.GetBytes(originalString);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write);
            cs.Write(inData, 0, inData.Length);
            cs.FlushFinalBlock();
            securtyString = Convert.ToBase64String(ms.ToArray());
            cs.Close();
            ms.Close();
            return securtyString;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="securityString">待加密字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>结果</returns>
        public static string DESDecrypt(string securityString, string key = "THISDESP", string iv = "PSEDSIHT")
        {
            byte[] inData = null;
            try
            {
                inData = Convert.FromBase64String(securityString);
            }
            catch (Exception)
            {
                return null;
            }
            string originalString = null;
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIV = Encoding.UTF8.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write);
            cs.Write(inData, 0, inData.Length);
            try
            {
                cs.FlushFinalBlock();
            }
            catch (Exception)
            {
                ms.Close();
                return null;
            }
            originalString = Encoding.UTF8.GetString(ms.ToArray());
            cs.Close();
            ms.Close();
            return originalString;
        }
    }

}
