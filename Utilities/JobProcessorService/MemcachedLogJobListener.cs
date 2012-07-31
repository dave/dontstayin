using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Bobs.BannerServer;
using Caching;

namespace JobProcessorService
{
	class MemcachedLogJobListener : IJobListener
	{

		public string Name
		{
			get { return "MemcachedCounterJobListener"; }
		}

		public void JobExecutionVetoed(JobExecutionContext context)
		{
			
		}

		public void JobToBeExecuted(JobExecutionContext context)
		{
		}

		public void JobWasExecuted(JobExecutionContext context, JobExecutionException jobException)
		{
			Instances.MainCounterStore.Increment(new CacheKey(CacheKeyPrefix.JobExecuted, Timeslots.GetCurrentTimeslot().StartTime.ToString()), () => 0);
		}

	}
}
