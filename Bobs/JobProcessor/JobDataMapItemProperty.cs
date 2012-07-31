using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Quartz;

namespace Bobs.JobProcessor
{
	public class JobDataMapItemProperty<T>
	{
		JobDataMap jobDataMap;
		string key;
		T defaultValue;
		public JobDataMapItemProperty(string key, JobDataMap jobDataMap)
		{
			this.key = key;
			this.jobDataMap = jobDataMap;
		}
		public JobDataMapItemProperty(string key, JobDataMap jobDataMap, T defaultValue)
		{
			this.key = key;
			this.jobDataMap = jobDataMap;
			this.defaultValue = defaultValue;
		}
		public T Value
		{
			//private 
			get
			{
				if (!jobDataMap.Contains(key)){
					return defaultValue;
				}
				return (T)jobDataMap[key];
				
			}
			set
			{
				jobDataMap[key] = value;
			}
		}
		static public implicit operator T(JobDataMapItemProperty<T> jobDataMapItemProperty)
		{
			return jobDataMapItemProperty.Value;
		}
	}
}
