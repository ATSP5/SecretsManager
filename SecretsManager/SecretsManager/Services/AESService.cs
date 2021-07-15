using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace SecretsManager
{
    public class AESService
    {
        private byte[] AESKEY;
        private byte[] AESIV;
        AesManaged APaes = new AesManaged();
        bool IsKeySet;
        public bool _isKeySet
        {
            get { return IsKeySet; }
        }
        private AESService()
        {
            AESKEY = APaes.Key;
            AESIV = APaes.IV;
            IsKeySet = false;
        }
        private static AESService _instance;
        public static AESService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AESService();
            }
            return _instance;
        }
        public void SetZeroKey()
        {
            for (int i = 0; i < 32; i++)
            {
                AESKEY[i] = 0;
            }
            for (int i = 0; i < 16; i++)
            {
                AESIV[i] = 0;
            }
            IsKeySet = false;
        }
        public void SetKey(string key)
        {
            byte[] fullkey = Encoding.Default.GetBytes(key);
            if (fullkey.Length < 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    AESKEY[i] = fullkey[i % fullkey.Length];
                }
                for (int i = 0; i < 16; i++)
                {
                    AESIV[i] = AESKEY[i];
                }
            }
            if (fullkey.Length > 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    AESKEY[i] = fullkey[i];
                }
                for (int i = 0; i < 16; i++)
                {
                    AESIV[i] = AESKEY[i];
                }
            }
            if (fullkey.Length == 32)
            {
                AESKEY = fullkey;
                for (int i = 0; i < 16; i++)
                {
                    AESIV[i] = AESKEY[i];
                }
            }
            IsKeySet = true;
        }
        public string EncryptAESManaged(string raw)
        {

            try
            {
                // Create Aes that generates a new key and initialization vector (IV).    
                // Same key must be used in encryption and decryption    

                // Encrypt string    

                byte[] encrypted = Encrypt(raw, AESKEY, AESIV);

                string encryptedchartable = "";

                encryptedchartable = Encoding.Default.GetString(encrypted);
                // Print encrypted string    
                return encryptedchartable;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return "";
        }
        public string DecryptAESManaged(string encryptedText)
        {

            try
            {
                // Create Aes that generates a new key and initialization vector (IV).    
                // Same key must be used in encryption and decryption    

                // Encrypt string    
                byte[] bytesEnc = Encoding.Default.GetBytes(encryptedText);
                string DecryptedText = Decrypt(bytesEnc, AESKEY, AESIV);
                // Print encrypted string    
                return DecryptedText;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return "";
        }
        private static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }

        private static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
