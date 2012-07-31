using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Caching;
using Bobs.BannerServer;
using Quartz.Impl;

namespace Bobs.JobProcessor
{
	class AsyncJobRunnerUsingQuartz : AsyncJobRunner
	{
		public override void Execute(Job job)
		{
			Instances.MainCounterStore.Increment(new Caching.CacheKey(Caching.CacheKeyPrefix.JobQueued, Timeslots.GetCurrentTimeslot().StartTime.ToString()), () => 0);
			JobDetail jobDetail = new JobDetail(job.Name, "immediate", job.GetType());
			jobDetail.JobDataMap = job.JobDataMap;
			SimpleTrigger trigger = new SimpleTrigger("Trigger" + job.Name, "immediate");
			ISchedulerFactory schedulerFactory = new StdSchedulerFactory(Common.Properties.QuartzSchedulerProperties);
			IScheduler scheduler = schedulerFactory.GetScheduler();
			scheduler.ScheduleJob(jobDetail, trigger);
			
		}
	}
}
