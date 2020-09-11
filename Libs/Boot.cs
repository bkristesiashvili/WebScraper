using CGCCrawler.Libs.Address;
using CGCCrawler.Libs.Config;
using CGCCrawler.Libs.NumString;
using CGCCrawler.Libs.WebHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CGCCrawler.Libs
{
    public sealed class Boot
    {
        /// <summary>
        /// Configuration object
        /// </summary>
        private ConfigProcessor Process { get; set; }

        /// <summary>
        /// already started process
        /// </summary>
        private bool IsStarted { get; set; }

        /// <summary>
        /// Page index
        /// </summary>
        private NString Index { get; set; }

        /// <summary>
        /// addresses from the current page
        /// </summary>
        private IList<string> Addresses { get; set; }

        /// <summary>
        /// database process
        /// </summary>
        private AddressDbProcessor DbProcess { get; set; }

        /// <summary>
        /// Creates boot object
        /// </summary>
        public Boot()
        {
            Process = new ConfigProcessor();
            DbProcess = new AddressDbProcessor();

            IsStarted = false;
        }

        /// <summary>
        /// starts checking process method
        /// </summary>
        public void Start()
        {
            if (IsStarted) return;

            IsStarted = true;

            Console.Title = GVars.APPLICATION_NAME;
            Process.LoadConfig();

            Greetings();

            Index = Process.Config.SavedPageId;

            string Host;
            string tag = "//a";
            bool foundout;

            while (true)
            {
                Host = string.Format($"{GVars.URL_CRYPTOGURU}{Index}");

                Addresses = WebResolver.GetAddress(Host, tag);

                foreach (string address in Addresses)
                {
                    Console.WriteLine($"Checking address: {address}");
                    Console.WriteLine($"Address located on page #{Index}");

                    foundout = WebResolver.CheckAddress(address);

                    if (foundout)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Congrats, one positive address found!");
                        Console.WriteLine($"Positive address is {address}");
                        Console.WriteLine($"Page index is {Index}");
                        Console.WriteLine("Please check current page!");

                        DbProcess.Insert(new BtcAddress
                        {
                            Id = 0,
                            PageId = Index.ToString(),
                            PositiveAddress = address
                        });
                        Console.WriteLine("Data Saved Successfully.");

                        Console.Beep(255, 6000);
                        Console.ResetColor();

                    }
                }

                Index++;
            }
        }

        /// <summary>
        /// Exit checking process
        /// </summary>
        public void Exit()
        {
            if (!IsStarted) return;

            IsStarted = false;

            ConfigObject config = new ConfigObject
            {
                SavedPageId = Index,
            };

            Task.Run(() =>
            {
                Process.SaveConfig(config);
            }).Wait(1000);
        }

        /// <summary>
        /// Greeting method
        /// </summary>
        private void Greetings()
        {
            Console.WriteLine(GVars.APPLICATION_START_MESSAGE);
            Console.WriteLine(GVars.COPYRIGHT);

            if (!Process.IsConfigured)
                Config();
        }

        /// <summary>
        /// Config method 
        /// </summary>
        private void Config()
        {
            int startPage = 1;

            string Host = string.Format($"{GVars.URL_CRYPTOGURU}{startPage}");

            ConfigObject config = new ConfigObject
            {
                SavedPageId = startPage.ToString(),
            };

            Process.SaveConfig(config);

        }
    }
}
