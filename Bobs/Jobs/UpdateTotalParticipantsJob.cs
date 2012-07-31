using System;
using System.Collections.Generic;
using System.Text;
using Bobs.JobProcessor;
using Quartz;

namespace Bobs.Jobs
{
	public class UpdateTotalParticipantsJob : Job
	{
		JobDataMapItemProperty<int> ThreadK { get { return new JobDataMapItemProperty<int>("ThreadK", JobDataMap); } }
		public UpdateTotalParticipantsJob()		{		}
		public UpdateTotalParticipantsJob(int threadK) 
		{
			ThreadK.Value = threadK;
		}
		public UpdateTotalParticipantsJob(Thread thread) : this(thread.K) { }
		protected override void Execute()
		{
			Thread thread = new Thread(ThreadK.Value);
			ThreadUsrSet tuWatchSet = GetWatchingThreadUsrs(thread.K);
			thread.TotalWatching = tuWatchSet.Count;
			ThreadUsrSet tus = GetParticipatingThreadUsrs(thread);
			thread.TotalParticipants = tus.Count;

			if (thread.Private && thread.TotalParticipants == 2)
			{
				thread.FirstParticipantUsrK = GetUsrKOfFirstParticipant(thread.K, thread.UsrK) ?? 0;
			}
			else
			{
				thread.FirstParticipantUsrK = 0;
			}
			thread.Update();
		}

		private static ThreadUsrSet GetParticipatingThreadUsrs(Thread thread)
		{
			Query tuQ = new Query();
			tuQ.ReturnCountOnly = true;
			tuQ.QueryCondition = new And(
				ThreadUsr.PrivateCanSeeQ,
				new Q(ThreadUsr.Columns.ThreadK, thread.K));
			ThreadUsrSet tus = new ThreadUsrSet(tuQ);
			return tus;
		}
		private ThreadUsrSet GetWatchingThreadUsrs(int threadK)
		{
			Query tuWatchQ = new Query();
			tuWatchQ.ReturnCountOnly = true;
			tuWatchQ.QueryCondition = new And(
				ThreadUsr.WatchingQ,
				new Q(ThreadUsr.Columns.ThreadK, threadK)
			);
			ThreadUsrSet tuWatchSet = new ThreadUsrSet(tuWatchQ);
			return tuWatchSet;
		}
		private int? GetUsrKOfFirstParticipant(int threadK, int threadUsrK)
		{
			try
			{
				Query q = new Query();
				q.NoLock = true;
				q.TopRecords = 1;
				q.QueryCondition = new And(
					ThreadUsr.PrivateCanSeeQ,
					new Q(ThreadUsr.Columns.ThreadK, threadK),
					new Q(ThreadUsr.Columns.UsrK, QueryOperator.NotEqualTo, threadUsrK));
				ThreadUsrSet tusUsr = new ThreadUsrSet(q);
				return tusUsr[0].UsrK;
			}
			catch
			{
				return null;
			}
		}
	}
}
