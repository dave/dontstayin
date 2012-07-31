using System;
using System.Collections.Generic;
using System.Text;

namespace MediaEncoderInstaller
{
	class Program
	{
		static void Main(string[] args)
		{

		//	String[] installParams = { @"C:\Inetpub\DontStayIn\MediaEncoderService\bin\Release\MediaEncoderService.exe" };
		//	System.Configuration.Install.ManagedInstallerClass.InstallHelper(installParams);

			Console.WriteLine("Video [v] or Photo [p]?");
			string app = Console.ReadLine();

			Console.WriteLine("Production [p] or development [d]?");
			string dev = Console.ReadLine();

			Console.WriteLine("Install [i] or uninstall [u]?");
			string install = Console.ReadLine();

			string path = @"";
			if (dev.Equals("d"))
				path = @"C:\Source\" + Common.Properties.CurrentBranchName + @"\MediaEncoder\EncoderService\";
			else
				path = @"C:\Release\";

		//	string dir = "Release";
		//	if (debug.Equals("d"))
		//		dir = @"Debug";

			string filename = path + @"VideoEncoderService\VideoEncoderService.exe";
			if (app.Equals("p"))
				filename = path + @"PhotoEncoderService\PhotoEncoderService.exe";

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
