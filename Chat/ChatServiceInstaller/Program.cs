using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ChatServiceInstaller
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Production [p] or development [d]?");
            string dev = Console.ReadLine();

            Console.WriteLine("Install [i] or uninstall [u]?");
            string install = Console.ReadLine();

			string path = @".\";
			if (dev.Equals("d"))
			{
				if (System.Environment.MachineName.EndsWith("YODA"))
					path = @"C:\Documents and Settings\c.hg\My Documents\Visual Studio 2005\Projects\Dsi\Chat\";
				else
					path = @"C:\Source\" + Common.Properties.CurrentBranchName + @"\Chat\ChatService\bin\";
			}

            string filename = path + @"ChatService.exe";

            string[] installParams;

            if (install.Equals("u"))
                installParams = new string[] { "/u", filename };
            else
                installParams = new string[] { filename };

            System.Configuration.Install.ManagedInstallerClass.InstallHelper(installParams);

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
