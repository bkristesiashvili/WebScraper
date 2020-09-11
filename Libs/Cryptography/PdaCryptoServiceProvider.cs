using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using CGCCrawler.Libs.Extensions;

namespace CGCCrawler.Libs.Cryptography
{
    public sealed class PdaCryptoServiceProvider : IPdaCryptoServiceProvider
    {
        /// <summary>
        /// cryptographic algorithm
        /// </summary>
        private AesCryptoServiceProvider Encryptor { get; }

        /// <summary>
        /// Creates PdaCryptoServiceProvider object for encryption
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pass"></param>
        public PdaCryptoServiceProvider(string key, string pass)
        {
            key = key.ToMD5String();
            byte[] salt = key.GetBytes();

            Encryptor = new AesCryptoServiceProvider();
            Rfc2898DeriveBytes derivebytes = new Rfc2898DeriveBytes(pass, salt);

            Encryptor.Key = derivebytes.GetBytes(Encryptor.KeySize / 8);
            Encryptor.IV = derivebytes.GetBytes(Encryptor.BlockSize / 8);
        }

        /// <summary>
        /// Encrypts files 
        /// </summary>
        /// <param name="originalFile">file path</param>
        /// <param name="encFileExt">Encrypted file extension</param>
        public void EncryptFile(string originalFile, string encFileExt)
        {
            string EncFilename = string.Format($"{originalFile}.{encFileExt}");

            using (FileStream fsRead = File.OpenRead(originalFile))
            using (FileStream fsWrite = File.OpenWrite(EncFilename))
            using (CryptoStream cs = new CryptoStream(fsWrite, Encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] readbits = new byte[fsRead.Length];
                fsRead.ReadAsync(readbits, 0, readbits.Length).Wait();
                fsRead.FlushAsync().Wait();

                cs.WriteAsync(readbits, 0, readbits.Length).Wait();
                cs.FlushAsync().Wait();
            }
        }

        /// <summary>
        /// Decrypts files
        /// </summary>
        /// <param name="encryptedFile">encrypted file path</param>
        public void DecryptFile(string encryptedFile, string encFileExt)
        {
            if (!encryptedFile.EndsWith(encFileExt))
                throw new Exception("File type doesn't support");

            string enFileOldPath = encryptedFile;

            encryptedFile = encryptedFile.Replace(encFileExt, string.Empty);

            using (FileStream fsread = File.OpenRead(enFileOldPath))
            using (FileStream fswrite = File.OpenWrite(encryptedFile))
            using (CryptoStream cs = new CryptoStream(fswrite, Encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                byte[] bits = new byte[fsread.Length];
                fsread.ReadAsync(bits, 0, bits.Length).Wait();
                fsread.FlushAsync().Wait();

                cs.WriteAsync(bits, 0, bits.Length).Wait();
                cs.FlushAsync().Wait();
            }
        }

        /// <summary>
        /// Encrypts object, save to file
        /// </summary>
        /// <typeparam name="EnObject"></typeparam>
        /// <param name="wheretosave"></param>
        /// <param name="encryptobject"></param>
        public void EncryptObject<EnObject>(string wheretosave, EnObject encryptobject)
        {
            if (!CheckIsSerializable(encryptobject)) throw new Exception("Object isn't serializable");

            using(FileStream fs = File.OpenWrite(wheretosave))
            using(CryptoStream cs= new CryptoStream(fs,Encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                BinaryFormatter bformat = new BinaryFormatter();
                bformat.Serialize(cs, encryptobject);
                cs.Flush();
                fs.Flush();
            }
        }

        /// <summary>
        /// Decrypts file
        /// </summary>
        /// <typeparam name="EnObject"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public EnObject DecryptObject<EnObject>(string filePath)
        {
            object result = null;

            using(FileStream fs = File.OpenRead(filePath))
            using (CryptoStream cs = new CryptoStream(fs, Encryptor.CreateDecryptor(), CryptoStreamMode.Read))
            {
                BinaryFormatter bformat = new BinaryFormatter();
                result = bformat.Deserialize(cs);
                cs.Flush();
                fs.Flush();
            }

            return (EnObject)result;
        }

        /// <summary>
        /// checks if object is serializable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CheckIsSerializable<T>(T obj)
        {
            return ((obj is ISerializable) || (Attribute.IsDefined(typeof(T), typeof(SerializableAttribute))));
        }

        /// <summary>
        /// Disposes PdaCryptoservice provider object
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
