using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ResourceViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (args.Length != 0)
            {
                string f = args[0];
                if(f == "-help")
                {
                    Console.WriteLine("ResourceViewer.exe [packfile] [output directory]");
                    return;
                }
                PackFile p = new PackFile(f);
                p.WritePackToDirectory(args[1]);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
