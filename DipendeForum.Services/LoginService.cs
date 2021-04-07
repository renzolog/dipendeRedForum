using DipendeForum.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DipendeForum.Domain;
using DipendeForum.Interfaces.Repositories;

namespace DipendeForum.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }

        private byte[] HideKeyIv(byte[] key, byte[] iv)
        {
            var byteList = new List<byte>();

            for (int index = 0; index < key.Length; index++)
            {
                key[index] ^= iv[index];
                byteList.Add(key[index]);
                byteList.Add(iv[index]);
            }

            return byteList.ToArray();
        }

        private byte[] GetHiddenKey(byte[] hidden)
        {
            var byteList = new List<byte>();

            for (int index = 0; index < hidden.Length - 1; index += 2)
            {
                hidden[index] ^= hidden[index + 1];
                byteList.Add(hidden[index]);
            }

            return byteList.ToArray();
        }

        private byte[] GetHiddenIv(byte[] hidden)
        {
            var byteList = new List<byte>();

            for (int index = 1; index < hidden.Length; index += 2)
            {
                byteList.Add(hidden[index]);
            }

            return byteList.ToArray();
        }

        private List<byte> AppendAtEnd(byte[] starting, byte[] toAppend)
        {
            var totalBytes = new List<byte>(starting);
            totalBytes.AddRange(toAppend);
            return totalBytes;
        }

        private string ByteArrayToString(byte[] toConvert)
        {
            return BitConverter.ToString(toConvert).Replace("-", "");
        }

        private RijndaelManaged GetAlgorithm()
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            algorithm.GenerateKey();
            algorithm.GenerateIV();
            return algorithm;
        }

        private byte[] GetEncryptedText(string text)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            var result = new List<byte>();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                var algo = GetAlgorithm();
                byte[] temp = null;

                using (CryptoStream cryptoStream = new CryptoStream(
                    memoryStream, 
                    algo.CreateEncryptor(), 
                    CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    temp = memoryStream.ToArray();
                }

                var toAppend = HideKeyIv(algo.Key, algo.IV);
                result = AppendAtEnd(temp, toAppend);
            }

            result.Reverse();
            return result.ToArray();
        }

        private string GetDecryptedText(byte[] encryptedText)
        {
            var outputBytes = encryptedText.Reverse().ToArray();

            string decryptedText = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream(outputBytes))
            {
                var key = GetHiddenKey(outputBytes);
                var iv = GetHiddenIv(outputBytes);
                using (CryptoStream cryptoStream = new CryptoStream(
                    memoryStream, 
                    GetAlgorithm().CreateDecryptor(key, iv), 
                    CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(cryptoStream))
                    {
                        char[] temp = new char[outputBytes.Length - (key.Length + iv.Length)];
                        srDecrypt.ReadBlock(temp, 0, temp.Length);
                        decryptedText = new string(temp);
                    }
                }
            }

            return decryptedText;
        }

        private string ManipulateRole(string role)
        {
            var final = new List<byte>();
            var rnd = new Random().Next(10, 101);
            for (int index = 0; index < rnd; index++)
            {
                final.Add(0);
            }
            final.Add(1);
            final.AddRange(Encoding.UTF8.GetBytes(role));

            var chaos = new List<byte>(Encoding.UTF8.GetBytes(DateTime.Now.ToLongDateString()));
            chaos.AddRange(Encoding.UTF8.GetBytes(DateTime.Now.Ticks.ToString()));
            
            final.AddRange(chaos);
            final.Reverse();

            return ByteArrayToString(final.ToArray());
        }

        public string EncryptPassword(string password)
        {
            var encrypted = GetEncryptedText(password);
            return ByteArrayToString(encrypted);
        }

        public string EncryptEmail(string email)
        {
            var encrypted = GetEncryptedText(email);
            return ByteArrayToString(encrypted);
        }

        public string EncryptRole(Role role)
        {
            var toEncrypt = ManipulateRole(role.ToString("D"));
            var encrypted = GetEncryptedText(toEncrypt);
            return ByteArrayToString(encrypted);
        }

        public bool ComparePassword(string id, string password)
        {
            var user = _repository.Get(id);
            if (user == null)
                return false;

            var encryptedPassword = user.Password;
            var decrypted = GetDecryptedText(encryptedPassword);

            return decrypted.Equals(password);
        }

        public string DecryptEmail(string id)
        {
            var user = _repository.Get(id);

            var encryptedEmail = user.Email;
            return GetDecryptedText(encryptedEmail);
        }

        public int CheckAuthorization(string id)
        {
            var user = _repository.Get(id);

            var encryptedRole = user.Role;
            var decrypted = GetDecryptedText(encryptedRole);

            var index = decrypted.IndexOf('1');

            return Convert.ToInt32(decrypted.Substring(index + 1, 8), 2);
        }

        public bool CompareEmail(string id, string email)
        {
            var user = _repository.Get(id);
            if (user == null)
                return false;

            var decrypted = DecryptEmail(id);

            return decrypted.Equals(email);
        }
    }
}
