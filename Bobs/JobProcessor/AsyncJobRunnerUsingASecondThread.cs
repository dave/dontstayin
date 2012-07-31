using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs.JobProcessor
{
	class AsyncJobRunnerUsingASecondThread : AsyncJobRunner
	{
		public override void Execute(Job job)
		{
			System.Threading.Thread thread = Utilities.GetSafeThread(() => job.ExecuteSynchronously());
			thread.Start();
		}
	}
}
