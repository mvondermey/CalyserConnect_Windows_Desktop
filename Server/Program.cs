using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace calyserconnect
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
            Debug.WriteLine("Program.Start Server Thread");
            new Server();
            Debug.WriteLine("Program.Done Server Thread");
            */
            //
            Debug.WriteLine("Program.Start Client Thread");
            new Client();
            Debug.WriteLine("Program.Done Client Thread");
             //
            //
        }
    }
}
