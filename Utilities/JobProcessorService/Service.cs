using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
namespace JobProcessorService
{
	
	public partial class Service : ServiceBase
	{
		ISchedulerFactory schedulerFactory;
		IScheduler scheduler;
		

		public Service()
		{
			InitializeComponent();
			schedulerFactory = new StdSchedulerFactory(Common.Properties.QuartzSchedulerProperties);
			scheduler = schedulerFactory.GetScheduler();
			scheduler.AddGlobalJobListener(new MemcachedLogJobListener());
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			EventLog.WriteEntry("DSI Job Processor service", e.ExceptionObject.ToString());
		}

		protected override void OnStart(string[] args)
		{
			scheduler.Start();
		}

		protected override void OnStop()
		{
			scheduler.Shutdown();
		}
	}
}
