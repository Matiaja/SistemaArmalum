using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "{78AF07A2-FA9A-4F7C-ACF2-9CA9E6F0E27D}");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-AR");
                //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-AR");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PrincipalForm());
                mutex.ReleaseMutex();
            }

        }
    }
}
