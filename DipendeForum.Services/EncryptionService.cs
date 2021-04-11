using DipendeForum.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DipendeForum.Domain;
using DipendeForum.Interfaces.CustomExceptions;
using DipendeForum.Interfaces.Repositories;

namespace DipendeForum.Services
{
    public class EncryptionService : IEncryptionService
    {
        #region Key/Iv

        private byte[] HideKeyIv(byte[] key, byte[] iv)
        {
            var byteList = new List<byte>();

            for (int index = 0; index < key.Length; index++)
            {
                byteList.Add(key[index]);
                byteList.Add(iv[index]);
            }

            byteList.Reverse();
            return byteList.ToArray();
        }

        private byte[] GetHiddenKey(byte[] hidden)
        {
            var byteList = new List<byte>();
            var cycledOn = hidden.TakeLast(32).Reverse().ToList();

            for (int index = 0; index < cycledOn.Count; index += 2)
            {
                byteList.Add(cycledOn[index]);
            }

            return byteList.ToArray();
        }

        private byte[] GetHiddenIv(byte[] hidden)
        {
            var byteList = new List<byte>();
            var cycledOn = hidden.TakeLast(32).Reverse().ToList();

            for (int index = 1; index < cycledOn.Count; index += 2)
            {
                byteList.Add(cycledOn[index]);
            }

            return byteList.ToArray();
        }

        private byte[] AppendAtEnd(byte[] starting, byte[] toAppend)
        {
            var totalBytes = new List<byte>(starting);
            totalBytes.AddRange(toAppend);
            totalBytes.Reverse();
            return totalBytes.ToArray();
        }

        #endregion

        #region Conversion-byte/string

        private string ByteArrayToString(byte[] toConvert)
        {
            return BitConverter.ToString(toConvert).Replace("-", "");
        }

        private byte[] StringToByteArray(string text)
        {
            var byteList = new List<byte>();

            for (int index = 0; index < text.Length; index += 2)
            {
                byteList.Add(Convert.ToByte(text.Substring(index, 2), 16));
            }

            byteList.Reverse();
            return byteList.ToArray();
        }

        #endregion

        #region Encryption/Decryption

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

        private RijndaelManaged GetDecryptionAlgorithm(byte[] key, byte[] iv)
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            algorithm.Key = key;
            algorithm.IV = iv;
            return algorithm;
        }

        private byte[] Encrypt(string toEncrypt)
        {
            byte[] toEncryptBytes = Encoding.UTF8.GetBytes(toEncrypt);
            byte[] encryptedBytes = null;

            using (var symmetricKey = GetAlgorithm())
            {
                var encryptor = symmetricKey.CreateEncryptor();

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(toEncryptBytes, 0, toEncryptBytes.Length);
                        cs.Flush();
                        cs.FlushFinalBlock();

                        ms.Position = 0;
                        encryptedBytes = ms.ToArray();
                    }
                }

                var toAppend = HideKeyIv(symmetricKey.Key, symmetricKey.IV);
                encryptedBytes = AppendAtEnd(encryptedBytes, toAppend);
            }

            return encryptedBytes;
        }

        private string Decrypt(byte[] encryptedBytes)
        {
            var key = GetHiddenKey(encryptedBytes);
            var iv = GetHiddenIv(encryptedBytes);
            encryptedBytes = encryptedBytes.Take(encryptedBytes.Length - 32).ToArray();

            string decryptedText;

            using (var symmetricKey = GetDecryptionAlgorithm(key, iv))
            {
                var decryptor = symmetricKey.CreateDecryptor();

                using (var ms = new MemoryStream(encryptedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        var decryptedBytes = new byte[encryptedBytes.Length];
                        var size = cs.Read(decryptedBytes, 0, encryptedBytes.Length);
                        cs.Close();

                        var trimmedBytes = new byte[size];
                        Array.Copy(decryptedBytes, trimmedBytes, size);
                        decryptedText = Encoding.UTF8.GetString(trimmedBytes);
                    }
                }
            }

            return decryptedText;
        }

        private string ManipulateRole(int role)
        {
            var final = new List<byte>();
            var rnd = new Random().Next(10, 101);
            for (int index = 0; index < rnd; index++)
            {
                final.Add((byte)new Random().Next(2, 255));
            }
            final.Add(1);

            var roleInBytes = Convert.ToString(role, 2).PadLeft(8, '0');

            foreach (var @byte in roleInBytes)
            {
                final.Add(byte.Parse(@byte.ToString()));
            }

            var chaos = new List<byte>(Encoding.UTF8.GetBytes(DateTime.Now.ToLongDateString()));
            chaos.AddRange(Encoding.UTF8.GetBytes(DateTime.Now.Ticks.ToString()));
            
            final.AddRange(chaos);

            return ByteArrayToString(final.ToArray());
        }

        private string DecypherRole(string cypheredRole)
        {
            var cryptedBytes = StringToByteArray(cypheredRole);
            var binaryRole = cryptedBytes.Reverse().SkipWhile(c => c != 1).Skip(1).Take(8).ToList();
            byte sum = 0;
            for (int index = 1; index <= binaryRole.Count; index++)
            {
                sum += (byte)(binaryRole[^index] * Math.Pow(2, index - 1));
            }
            
            return sum.ToString();
        }

        #endregion

        public string EncryptPassword(string password)
        {
            var encryptedBytes = Encrypt(password);
            return ByteArrayToString(encryptedBytes);
        }

        public string EncryptRole(int role)
        {
            var toEncrypt = ManipulateRole(role);
            var encryptedBytes = Encrypt(toEncrypt);
            return ByteArrayToString(encryptedBytes);
        }

        public bool IsPasswordMatching(string password, string toCheck)
        {
            var byteArr = StringToByteArray(password);
            var decrypted = Decrypt(byteArr);

            return decrypted.Equals(toCheck);
        }

        public Role DecryptRole(string encryptedRole)
        {
            var byteArr = StringToByteArray(encryptedRole);
            var decrypted = Decrypt(byteArr);
            var role = DecypherRole(decrypted);
            return Enum.Parse<Role>(role);
        }
    }
}
