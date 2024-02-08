using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Windows.Forms;

namespace CalculatorAppForNichi
{
    internal static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        [STAThread]
        static void Main()
        {
            // Load log4net configuration based on build configuration
            log4net.Util.LogLog.InternalDebugging = true;

#if DEBUG
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
#else
            XmlConfigurator.Configure();
#endif

            // Log a test message
            log.Info("Application started.");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
