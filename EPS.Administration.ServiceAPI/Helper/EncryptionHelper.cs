using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EPS.Administration.ServiceAPI.Helper
{
    public static class EncryptionHelper
    {
        private static byte[] GetKey(string password)
        {
            var keyBytes = Encoding.UTF8.GetBytes(password);
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(keyBytes);
            }
        }

        internal static string Encrypt(string textToEncrypt, string key)
        {
            if (string.IsNullOrEmpty(textToEncrypt))
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Key = GetKey(key);

                    var IV = aes.IV;
                    ms.Write(IV, 0, IV.Length);

                    using (var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] byteText = Encoding.UTF8.GetBytes(textToEncrypt);
                        cryptoStream.Write(byteText);
                        cryptoStream.FlushFinalBlock();
                        byte[] cipherBytes = ms.ToArray();
                        ms.Close();
                        cryptoStream.Close();
                        return Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
                    }
                }
            }
        }

        internal static string Decrypt(string password, string key)
        {
            if (password.Length < 20)
            {
                return password;
            }

            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = GetKey(key);
            byte[] cipherBytes = Convert.FromBase64String(password);
            byte[] ivBytes = cipherBytes.Take(encryptor.IV.Length).ToArray();
            encryptor.IV = ivBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var aesDecryptor = encryptor.CreateDecryptor())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write))
                    {
                        string plainText = String.Empty;
                        try
                        {
                            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            byte[] plainBytes = memoryStream.ToArray();
                            plainText = Encoding.UTF8.GetString(plainBytes, encryptor.IV.Length, plainBytes.Length - encryptor.IV.Length);
                            return plainText;
                        }
                        catch (CryptographicException)
                        {
                            return string.Empty;
                        }
                        finally
                        {
                            memoryStream.Close();
                            cryptoStream.Close();
                        }
                    }

                }
            }
        }
    }
}
