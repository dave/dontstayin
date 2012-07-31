using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs.JobProcessor
{
	public class DependentJobStoreUsingInstance : DependentJobStore
	{
		List<Job> jobs = new List<Job>();
		public override void AddJob(Job job)
		{
			jobs.Add(job);
		}

		public override void ExecuteStoredJobs()
		{
			foreach (Job job in jobs)
			{
				job.ExecuteSynchronously();
			}
		}
	}
}
