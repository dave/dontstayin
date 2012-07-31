using System;
using System.Collections.Generic;
using System.Text;
using Caching;

using Quartz.Impl;
using Bobs.BannerServer;
using Quartz;
using Quartz.Impl;
using System.ComponentModel;
using Common.General;

namespace Bobs.JobProcessor
{
	/// <summary>
	/// NOTE TO IMPLEMENTERS - you must have a default empty constructor for Quartz.NET to work!
	/// </summary>
	public abstract class Job : IJob
	{
		#region DependentJobStore

		DependentJobStore DependentJobStore
		{
			get
			{
				if (dependentJobStore == null)
				{
					if (Common.Properties.UseJobProcessorService && Common.Settings.UseJobProcessorService)
					{
						dependentJobStore = new DependentJobStoreUsingQuartz(this);
					}
					else
					{
						dependentJobStore = new DependentJobStoreUsingInstance();
					}
				}
				return dependentJobStore;
			}
		}
		DependentJobStore dependentJobStore;
		#endregion
		#region AsyncJobRunner
		AsyncJobRunner AsyncJobRunner
		{
			get
			{
				if (asyncJobRunner == null)
				{
					if (Common.Properties.UseJobProcessorService && Common.Settings.UseJobProcessorService)
					{
						asyncJobRunner = new AsyncJobRunnerUsingQuartz();
					}
					else
					{
						asyncJobRunner = new AsyncJobRunnerUsingASecondThread(); 
					}
				}
				return asyncJobRunner;
			}
		}
		AsyncJobRunner asyncJobRunner;
		#endregion
		protected abstract void Execute();
		public void ExecuteSynchronously()
		{
			Execute();
			DependentJobStore.ExecuteStoredJobs();
		}
		int retryLimit = 3;
	
	
		#region JobDataMap
		public JobDataMap JobDataMap
		{
			get { return this.jobDataMap; }
			set { this.jobDataMap = value; }
		}
		protected JobDataMap jobDataMap = new JobDataMap();
		#endregion
		#region Name
		public string Name
		{
			get
			{
				if (name == null) { name = Guid.NewGuid().ToString(); }
				return name;

			}
		}
		string name = null;
		#endregion
		public void ExecuteAsynchronously()
		{
			AsyncJobRunner.Execute(this);
		}
		internal void AddJobToRunOnceFinished(Job job)
		{
			DependentJobStore.AddJob(job);
		}
		public enum JobStatus
		{
			Queued = 1,
			Running = 2,
			Completed = 3,
			Failed = 4
		}

		#region IJob Members

		public void Execute(JobExecutionContext context)
		{
			try
			{
				this.name = context.JobDetail.Name;
				this.JobDataMap = context.MergedJobDataMap;
				ExecuteSynchronously();
			}
			catch (Exception ex)
			{
				if (context.RefireCount >= retryLimit)
				{
					try
					{
						Instances.MainCounterStore.Increment(new Caching.CacheKey(Caching.CacheKeyPrefix.JobException, Timeslots.GetCurrentTimeslot().StartTime.ToString()), () => 0);
						SpottedException.TryToSaveExceptionAndChildExceptions(ex);
					}
					catch
					{
						throw new JobExecutionException(ex, false);
					}
				}
				else
				{
					throw new JobExecutionException(ex, true); ;
				}
			}
		}

		#endregion
	}
}
