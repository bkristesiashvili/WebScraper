using CGCCrawler.Libs.Cryptography;
using System;
using CGCCrawler.Libs.Extensions;

namespace CGCCrawler.Libs.Config
{
    public sealed class ConfigProcessor
    {
        /// <summary>
        /// configuration object
        /// </summary>
        public ConfigObject Config { get; private set; }

        /// <summary>
        /// Gets if configurations exist.
        /// </summary>
        public bool IsConfigured { get; private set; }

        /// <summary>
        /// Creates configuration processor object
        /// </summary>
        public ConfigProcessor()
        {
            Config = null;
            IsConfigured = false;
        }

        /// <summary>
        /// Saves configuration to file
        /// </summary>
        /// <param name="config"></param>
        public void SaveConfig(ConfigObject config)
        {
            if (config == null)
                throw new NullReferenceException($"{nameof(config)}");

            try
            {
                string pass = GVars.COPYRIGHT.ToMD5String();
                string key = GVars.COPYRIGHT;

                using (IPdaCryptoServiceProvider _algo = new PdaCryptoServiceProvider(key, pass))
                {
                    _algo.EncryptObject(GVars.CONFIG_FILEPATH, config);
                }

                Config = config;

                IsConfigured = true;
            }
            catch
            {
                IsConfigured = false;
            }
        }

        /// <summary>
        /// load configuration from file
        /// </summary>
        /// <returns></returns>
        public void LoadConfig()
        {

            string pass = GVars.COPYRIGHT.ToMD5String();
            string key = GVars.COPYRIGHT;

            try
            {
                using(IPdaCryptoServiceProvider _algo = new PdaCryptoServiceProvider(key, pass))
                {
                    Config = _algo.DecryptObject<ConfigObject>(GVars.CONFIG_FILEPATH);
                }

                IsConfigured = true;
            }
            catch
            {
                IsConfigured = false;
            }
        }
    }
}
