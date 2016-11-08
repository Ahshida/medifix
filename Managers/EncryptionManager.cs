using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DBO.Data.Managers
{
    public static class EncryptionManagerExtension
    {
        public static string Encrypt(this object item)
        {
            return new EncryptionManager().Encrypt(item);
        }
        public static string Encrypt(this object item, bool isEncoded)
        {
            return new EncryptionManager().Encrypt(item, isEncoded);
        }
        public static string Decrypt(this string item)
        {
            return new EncryptionManager().Decrypt<string>(item);
        }
        public static string Decrypt(this string item, bool isDecoded)
        {
            return new EncryptionManager().Decrypt<string>(item, isDecoded);
        }
        public static T Decrypt<T>(this string item)
        {
            return new EncryptionManager().Decrypt<T>(item);
        }
        public static T Decrypt<T>(this string item, bool isDecoded)
        {
            return new EncryptionManager().Decrypt<T>(item, isDecoded);
        }
        public static string Encrypt(this object item, string sEncryptionKey)
        {
            return new EncryptionManager().Encrypt(item, sEncryptionKey);
        }
        public static T Decrypt<T>(this string item, string sEncryptionKey)
        {
            return new EncryptionManager().Decrypt<T>(item, sEncryptionKey);
        }
    }

    public class EncryptionManager
    {
        private byte[] key = { };
        private byte[] IV = {
            0x12,
            0x34,
            0x56,
            0x78,
            0x90,
            0xab,
            0xcd,
            0xef
        };
        //private byte[] IV = {
        //    0x34,
        //    0x90,
        //    0x56,
        //    0x56,
        //    0xEF,
        //    0xCD,
        //    0x12,
        //    0xAB
        //};

        public T Decrypt<T>(string stringToDecrypt)
        {
            return Decrypt(stringToDecrypt).ConvertTo<T>();
        }
        public string Decrypt(string stringToDecrypt)
        {
            return Decrypt(stringToDecrypt, false);
        }
        public T Decrypt<T>(string stringToDecrypt, bool isDecoded)
        {
            return Decrypt(stringToDecrypt, isDecoded).ConvertTo<T>();
        }
        public string Decrypt(string stringToDecrypt, bool isDecoded)
        {
            return Decrypt(stringToDecrypt, "a@B#c$90", isDecoded);
        }
        public T Decrypt<T>(string stringToDecrypt, string sEncryptionKey)
        {
            return Decrypt(stringToDecrypt, sEncryptionKey).ConvertTo<T>();
        }
        public string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            return Decrypt(stringToDecrypt, sEncryptionKey, false);
        }
        public T Decrypt<T>(string stringToDecrypt, string sEncryptionKey, bool isDecoded)
        {
            return Decrypt(stringToDecrypt, sEncryptionKey, isDecoded).ConvertTo<T>();
        }
        public string Decrypt(string stringToDecrypt, string sEncryptionKey, bool isDecoded)
        {
            if (string.IsNullOrEmpty(stringToDecrypt))
                return stringToDecrypt;

            if (isDecoded)
                stringToDecrypt = HttpUtility.UrlDecode(stringToDecrypt);

            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Encrypt(object stringToEncrypt)
        {
            return Encrypt(stringToEncrypt, false);
        }
        public string Encrypt(object stringToEncrypt, bool isEncoded)
        {
            return Encrypt(stringToEncrypt, "a@B#c$90", isEncoded);
        }
        public string Encrypt(object stringToEncrypt, string sEncryptionKey)
        {
            return Encrypt(stringToEncrypt, sEncryptionKey, false);
        }
        public string Encrypt(object stringToEncrypt, string sEncryptionKey, bool isEncoded)
        {
            try
            {
                if (stringToEncrypt == null)
                    return null;

                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt.ConvertTo<string>());
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                if (isEncoded)
                    return HttpUtility.UrlEncode(Convert.ToBase64String(ms.ToArray()));
                else
                    return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}