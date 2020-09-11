using System;
using CGCCrawler.Libs;
using System.Diagnostics;
using System.Threading;

namespace CGCCrawler
{
    class Program
    {
        private static Boot CGCCSystem { get; set; }
        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

                CGCCSystem = new Boot();

                CGCCSystem.Start();
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                CGCCSystem.Exit();
                Thread.Sleep(300);
                Process.Start(Process.GetCurrentProcess().ProcessName);
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            CGCCSystem.Exit();
        }
    }
}
