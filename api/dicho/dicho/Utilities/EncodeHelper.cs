using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace dicho.Utilities
{
    public class EncodeHelper
    {
        private static string SecurityKey = "adsfasdfasdfsadf34234234DJFHASKDFASDFJSDF1234231423423432!@#$%^&*()hjfsadfsadfsdf@#$34SDFdsfsf#$@dsfsdf#@$SDcvmvsdf";

        /// <summary>
        /// Hash password with MD5 algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashMD5Password(string password)
        {
            if (password == "") return "";

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)

            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);

            ////Convert encoded bytes back to a 'readable' string
            //string encryptLevel1 = BitConverter.ToString(encodedBytes);
            //originalBytes = ASCIIEncoding.Default.GetBytes(encryptLevel1);
            //encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).ToLower().Replace("-", "");
        }

        /// <summary>
        /// Encrypt symmetric plain text with RijndealManaged 256 bit
        /// </summary>
        /// <returns>Encrypted text</returns>
        public static string EncryptString(string inputText)
        {


            // "Password" string variable is nothing but the key(your secret key) value which is sent from the front end.
            // "InputText" string variable is the actual password sent from the login page.
            // We are now going to create an instance of the
            // Rihndael class.
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            // First we need to turn the input strings into a byte array.
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);

            // We are using Salt to make it harder to guess our key
            // using a dictionary attack.
            byte[] salt = Encoding.ASCII.GetBytes(SecurityKey.Length.ToString());

            // The (Secret Key) will be generated from the specified
            // password and Salt.
            //PasswordDeriveBytes -- It Derives a key from a password
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(SecurityKey, salt);

            // Create a encryptor from the existing SecretKey bytes.
            // We use 32 bytes for the secret key
            // (the default Rijndael key length is 256 bit = 32 bytes) and
            // then 16 bytes for the IV (initialization vector),
            // (the default Rijndael IV length is 128 bit = 16 bytes)
            // 32 byte = 258 bits
            ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));

            // Create a MemoryStream that is going to hold the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Create a CryptoStream through which we are going to be processing our data.
            // CryptoStreamMode.Write means that we are going to be writing data
            // to the stream and the output will be written in the MemoryStream
            // we have provided. (always use write mode for encryption)
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            // Start the encryption process.
            cryptoStream.Write(plainText, 0, plainText.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memoryStream into a byte array.
            byte[] cipherBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            // A common mistake would be to use an Encoding class for that.
            // It does not work, because not all byte values can be
            // represented by characters. We are going to be using Base64 encoding
            // That is designed exactly for what we are trying to do.
            string encryptedData = Convert.ToBase64String(cipherBytes);

            // Return encrypted string.
            return encryptedData;
        }


        /// <summary>
        /// Decrypt symmetric plain text encryption with RijndealManaged 256 bit
        /// </summary>
        /// <returns>Plain text</returns>
        public static string DecryptString(string inputText)
        {
            try
            {
                inputText = inputText.Replace(" ", "+");

                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                byte[] encryptedData = Convert.FromBase64String(inputText);
                byte[] salt = Encoding.ASCII.GetBytes(SecurityKey.Length.ToString());
                PasswordDeriveBytes secretKey = new PasswordDeriveBytes(SecurityKey, salt);

                // Create a decryptor from the existing SecretKey bytes.
                ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(encryptedData);

                // Create a CryptoStream. (always use Read mode for decryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold EncryptedData;
                // DecryptedData is never longer than EncryptedData.
                byte[] plainText = new byte[encryptedData.Length];

                // Start decrypting.
                int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string.
                string decryptedData = Encoding.Unicode.GetString(plainText, 0, decryptedCount);

                // Return decrypted string.
                return decryptedData;
            }
            catch
            {
                return "";
            }
        }






        /// <summary>
        /// Hash a share key
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string SharedKeyHash(string message)
        {
            var key = Encoding.UTF8.GetBytes(SecurityKey);
            string hashString;

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString;
        }


        #region Hash Access Token

        public static string HashAccessToken(HttpRequestMessage request, string sharedKey)
        {
            string serverHash = string.Empty;
            string dateTime = request.Headers.GetValues("DateTime").ToList()[0];
            string signMessage = string.Format("{0}\n{1}", dateTime, sharedKey);
            //string signMessage = DataSignature(request);

            serverHash = HMACHash(sharedKey, signMessage);

            return serverHash;
        }



        private static string HMACHash(string sharedKey, string message)
        {

            var key = Encoding.UTF8.GetBytes(sharedKey);
            string hashString;

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString;
        }

        private static string DataSignature(HttpRequestMessage request)
        {
            string sign = string.Empty;
            string httpVerb = request.Method.Method;

            long contentLength = request.Content.Headers.ContentLength.HasValue ? request.Content.Headers.ContentLength.Value : 0;
            string dateTime = request.Headers.GetValues("DateTime").ToList()[0];


            sign = string.Format("{0}\n{1}\n{2}", dateTime, httpVerb, contentLength);

            return sign;
        }

        #endregion


    }
}