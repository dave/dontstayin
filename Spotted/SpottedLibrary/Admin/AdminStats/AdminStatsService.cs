using System;
using System.Collections.Generic;
using System.Text;
using Bobs.BannerServer;
using Caching;

namespace SpottedLibrary.Admin.AdminStats
{
	public class AdminStatsService 
	{

		internal List<JobProcessorLogDataItem> GetRecentJobProcessorLogInfo()
		{
			List<JobProcessorLogDataItem> dataItems = new List<JobProcessorLogDataItem>();
			DateTime currentTimeslotStart = Timeslots.GetCurrentTimeslot().StartTime;
			DateTime timeslotStart = currentTimeslotStart;
			for (int i = 0; i < 12; i++)
			{
				dataItems.Add(new JobProcessorLogDataItem()
				{
					StartOfTimePeriod = timeslotStart,
					NumberOfItemsQueued = (int) Caching.Instances.MainCounterStore.GetCounter(new CacheKey(CacheKeyPrefix.JobQueued, timeslotStart.ToString()), () => 0u),
					NumberOfItemsServed = (int) Caching.Instances.MainCounterStore.GetCounter(new CacheKey(CacheKeyPrefix.JobExecuted, timeslotStart.ToString()), () => 0u),
					NumberOfExceptions = (int)Caching.Instances.MainCounterStore.GetCounter(new CacheKey(CacheKeyPrefix.JobException, timeslotStart.ToString()), () => 0u)
				});
				timeslotStart = timeslotStart.Subtract(Timeslot.Duration);
			}
			return dataItems;
			
		}
		internal int GetCurrentQueueSize()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			return (int) db.ExecuteScalar("SELECT COUNT(*) FROM QRTZ_JOB_DETAILS WHERE JOB_GROUP = 'immediate'");
		}

		internal List<PhotoUploaderTriesDataItem> GetPhotoUploaderTriesDataItems()
		{
			List<PhotoUploaderTriesDataItem> items = new List<PhotoUploaderTriesDataItem>();
			for (int i = 0; i <= 10; i++)
			{
				items.Add
				(
					new PhotoUploaderTriesDataItem()
					{
						NumberOfOccurences = (int) Caching.Instances.MainCounterStore.GetCounter(CacheKeyPrefix.PhotoUploaderTries.ToString() + i + DateTime.Now.Date.ToString(), () => 0),
						NumberOfTries = i
					}
				);
			}
			return items;
		}

		internal List<PhotoUploaderSuccessFailureDataItem> GetPhotoUploaderSuccessFailureDataItems()
		{
			List<PhotoUploaderSuccessFailureDataItem> items = new List<PhotoUploaderSuccessFailureDataItem>();
			foreach(string serverName in PhotoProcessingServers())
			{
				items.Add
				(
					new PhotoUploaderSuccessFailureDataItem()
					{
						Failures = (int)Caching.Instances.MainCounterStore.GetCounter(CacheKeyPrefix.PhotoUploaderFailures.ToString() + serverName.ToLower() + DateTime.Now.Date.ToString(), () => 0),
						Successes = (int)Caching.Instances.MainCounterStore.GetCounter(CacheKeyPrefix.PhotoUploaderSuccesses.ToString() + serverName.ToLower() + DateTime.Now.Date.ToString(), () => 0),
						HostName = serverName
					}
				);
			}
			return items;
		}
		IEnumerable<string> PhotoProcessingServers()
		{
			if (Common.Properties.IsDevelopmentEnvironment)
			{
				yield return Common.Properties.MachineName;
			}
			else
			{
				for (int i = 1; i <= 8; i++)
				{
					yield return "SERVER" + i;
				}
				yield return "EXTRA";
			}
		}
	}
}
