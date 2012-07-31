using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs.JobProcessor
{
	abstract class AsyncJobRunner
	{
		public abstract void Execute(Job job);
	}
}
