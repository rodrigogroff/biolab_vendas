using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Cryptography;

namespace Master.Controller.Helper
{
    public class HelperAesDecrypt
    {
        public const string MyKey = "7061737323313233";

        [NonAction]
        public string DecryptStringAES(string encryptedValue)
        {
            try
            {
                var keybytes = Encoding.UTF8.GetBytes(MyKey);
                var iv = Encoding.UTF8.GetBytes(MyKey);
                var encrypted = Convert.FromBase64String(encryptedValue);
                var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);

                return decriptedFromJavascript;
            }
            catch
            {
                return null;
            }
        }

        public string EncryptStringAES(string text)
        {
            var key = Encoding.UTF8.GetBytes(MyKey);
            var iv = Encoding.UTF8.GetBytes(MyKey);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, iv))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var decryptedContent = msEncrypt.ToArray();
                        return Convert.ToBase64String(decryptedContent);
                    }
                }
            }
        }

        private string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext = null;

            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
    }
}
