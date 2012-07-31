using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;

namespace EncoderService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			ServiceBase[] ServicesToRun;

			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new ServiceBase[] { new VideoEncoderService() };

			ServiceBase.Run(ServicesToRun);
		}
	}



	[RunInstaller(true)]
	public class ProjectInstaller : Installer
	{
		private ServiceInstaller serviceInstaller;
		private ServiceProcessInstaller processInstaller;
		public ProjectInstaller()
		{
			processInstaller = new ServiceProcessInstaller();
			serviceInstaller = new ServiceInstaller();
			// Service will run under system account
			processInstaller.Account = ServiceAccount.LocalSystem;
			// Service will have Start Type of Manual
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = "VideoEncoder";
			Installers.Add(serviceInstaller);
			Installers.Add(processInstaller);
		}
	}


}
