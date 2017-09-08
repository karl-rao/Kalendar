using System; 
using System.IO; 
using System.Text;
using System.Security.Cryptography;

namespace Kalendar.Utility.Common
{
    /// <summary>
    /// QuringString ���ܽ�����
    /// </summary>
    public class Encryption64
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 
        /// </summary>
        private static byte[] _key = { };
        /// <summary>
        /// 
        /// </summary>
        private static readonly byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

        /// <summary>
        /// 
        /// </summary>
        private static readonly string EncryptionKey = Config.EncryptionKey ;

        /// <summary>
        /// Decrypts the specified string to decrypt.
        /// </summary>
        /// <param name="stringToDecrypt">The string to decrypt.</param>
        /// <returns></returns>
        public static string Decrypt(string stringToDecrypt)
        {
            return Decrypt(stringToDecrypt, EncryptionKey);
        }

        /// <summary>
        /// Decrypts the specified string to decrypt.
        /// </summary>
        /// <param name="stringToDecrypt">The string to decrypt.</param>
        /// <param name="sEncryptionKey">The s encryption _key.</param>
        /// <returns></returns>
        public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Convert.FromBase64String(stringToDecrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(_key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ex.Message;
            }
        }


        /// <summary>
        /// Encrypts the specified string to decrypt.
        /// </summary>
        /// <param name="stringToDecrypt">The string to decrypt.</param>
        /// <returns></returns>
        public static string Encrypt(string stringToDecrypt)
        {
            return Encrypt(stringToDecrypt, EncryptionKey);
        }
        /// <summary>
        /// Encrypts the specified string to encrypt.
        /// </summary>
        /// <param name="stringToEncrypt">The string to encrypt.</param>
        /// <param name="sEncryptionKey">The S encryption _key.</param>
        /// <returns></returns>
        public static string Encrypt(string stringToEncrypt, string sEncryptionKey)
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(_key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ex.Message;
            }
        }
    }
} 