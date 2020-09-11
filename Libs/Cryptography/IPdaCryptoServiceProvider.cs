using System;

namespace CGCCrawler.Libs.Cryptography
{
    public interface IPdaCryptoServiceProvider: IDisposable
    {
        /// <summary>
        /// Encrypts files 
        /// </summary>
        /// <param name="originalFile">file path</param>
        /// <param name="encFileExt">Encrypted file extension</param>
        void EncryptFile(string originalFile, string encFileExt);

        /// <summary>
        /// Decrypts files
        /// </summary>
        /// <param name="encryptedFile">encrypted file path</param>
        void DecryptFile(string encryptedFile, string encFileExt);

        /// <summary>
        /// Encrypts object, save to file
        /// </summary>
        /// <typeparam name="EnObject"></typeparam>
        /// <param name="wheretosave"></param>
        /// <param name="encryptobject"></param>
        void EncryptObject<EnObject>(string wheretosave, EnObject encryptobject);

        /// <summary>
        /// Decrypts file
        /// </summary>
        /// <typeparam name="EnObject"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        EnObject DecryptObject<EnObject>(string filePath);
    }
}
