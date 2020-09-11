namespace CGCCrawler.Libs
{
    public static class GVars
    {
        /// <summary>
        /// white space pattern for regular expression
        /// </summary>
        internal static readonly string REGEX_WHITE_SPACE_PATTERN = "\\s+";

        /// <summary>
        /// digits only regular expression pattern
        /// </summary>
        internal static readonly string REGEX_DIGITS_ONLY_PATTERN = "^[0-9]+$";

        /// <summary>
        /// allow only digits and  chars
        /// </summary>
        internal static readonly string REGEX_DIGITS_AND_SYMBOLS = "^[a-zA-Z0-9]+$";

        /// <summary>
        /// Configuration file path
        /// </summary>
        internal static readonly string CONFIG_FILEPATH = "Config.cfg";

        /// <summary>
        /// copyright constant
        /// </summary>
        internal static readonly string COPYRIGHT = "Developed by B4BTC 2020.";

        /// <summary>
        /// cryptoguru url
        /// </summary>
        internal static readonly string URL_CRYPTOGURU = "https://lbc.cryptoguru.org/dio/";

        /// <summary>
        /// blockchain url
        /// </summary>
        internal static readonly string URL_BLOCKCHAIN = "https://blockchain.info/address/";

        /// <summary>
        /// get json format data from url
        /// </summary>
        internal static readonly string URL_JSON_FORMAT = "?format=json";

        /// <summary>
        /// application name and title for console window
        /// </summary>
        internal static readonly string APPLICATION_NAME = "Btc Address Balance Check v1.";

        /// <summary>
        /// welcome message for console app
        /// </summary>
        internal static readonly string APPLICATION_START_MESSAGE = "CryptoGuru btc addresses check balance.";

        /// <summary>
        /// database connection string
        /// </summary>
        internal static readonly string CONNECTION_STRING = @"workstation id=CryptoDb.mssql.somee.com;packet size=4096;
                                                              user id=BEFX89_SQLLogin_1;pwd=cbofoyfl7b;
                                                              data source=CryptoDb.mssql.somee.com;persist security info=False;
                                                              initial catalog=CryptoDb";
    }
}
