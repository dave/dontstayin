using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs.JobProcessor
{
	public abstract class DependentJobStore
	{
		public abstract void AddJob(Job job);
		public abstract void ExecuteStoredJobs();
	}
}
