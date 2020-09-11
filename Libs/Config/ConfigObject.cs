using CGCCrawler.Libs.NumString;
using System;

namespace CGCCrawler.Libs.Config
{
    [Serializable]
    public sealed class ConfigObject
    {
        /// <summary>
        /// stoped page id
        /// </summary>
        public NString SavedPageId { get; set; }

        ///// <summary>
        ///// saves bitcoin address  current page
        ///// </summary>
        //public IList<string> Addresses { get; set; }

    }
}
