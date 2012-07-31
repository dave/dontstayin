using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace Bobs.JobProcessor
{
	public class DependentJobStoreUsingQuartz : DependentJobStore
	{
		public DependentJobStoreUsingQuartz(Job parentJob)
		{
			ParentJob = parentJob;
		}
		Job ParentJob {get;set;}
		List<string> JobNames 
		{ 
			get 
			{ 
				JobDataMapItemProperty<List<string>> prop =  new JobDataMapItemProperty<List<string>>("MechanismForRunningJobsSequentiallyUsingQuartzJobNames", ParentJob.JobDataMap);
				if (prop.Value == null)
				{
					prop.Value = new List<string>();
				}
				return prop.Value;
			} 
		}
	
		public override void AddJob(Job job)
		{
			JobNames.Add(job.Name);
			ISchedulerFactory schedulerFactory = new StdSchedulerFactory(Common.Properties.QuartzSchedulerProperties);
			IScheduler scheduler = schedulerFactory.GetScheduler();
			JobDetail jobDetail = new JobDetail(job.Name, "sequential", job.GetType(), false, true, true);
			jobDetail.JobDataMap = job.JobDataMap;
			
			scheduler.AddJob(jobDetail, true);
		}

		public override void ExecuteStoredJobs()
		{
			ISchedulerFactory schedulerFactory = new StdSchedulerFactory(Common.Properties.QuartzSchedulerProperties);
			IScheduler scheduler = schedulerFactory.GetScheduler();
			foreach (string jobName in JobNames)
			{
				
				SimpleTrigger trigger = new SimpleTrigger("Trigger" + jobName, "immediate");
				trigger.JobName = jobName;
				trigger.JobGroup = "sequential";
				
				scheduler.ScheduleJob(trigger);
				JobDetail jobDetail = scheduler.GetJobDetail(jobName, "sequential");
				jobDetail.Durable = false;
				scheduler.AddJob(jobDetail, true);
			}
		}
	}
}
