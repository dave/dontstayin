using Sys;
using Sys.Net;
using System;
using Spotted.WebServices.Controls.LatestChat;
using Utils;

namespace SpottedScript.Controls.LatestChat
{
	class LatestThreadsProvider
	{
		int threadsCount;
		bool hasGroupObjectFilter;
		int objectType;

		Array threads;
		int objectK;
		internal EventHandler OnLoaded;

		internal ThreadStub[] CurrentThreads
		{
			get
			{
				return (ThreadStub[])threads[objectK];
			}
			private set
			{
				threads[objectK] = value;
			}
		}

		internal LatestThreadsProvider(int threadsCount, bool hasGroupObjectFilter, int objectType)
		{
			this.threadsCount = threadsCount;
			this.hasGroupObjectFilter = hasGroupObjectFilter;
			this.objectType = objectType;

			this.threads = new Array();
		}

		public void LoadThreads(int objectK)
		{
			this.objectK = objectK;
			if (CurrentThreads != null)
			{
				loaded();
			}
			else
			{
				loadThreadsViaWebService();
			}
		}

		void loadThreadsViaWebService()
		{
			Service.GetThreads(this.objectType, this.objectK, this.threadsCount, this.hasGroupObjectFilter, getThreadsSuccess, Trace.WebServiceFailure, null, -1);
		}

		void getThreadsSuccess(ThreadStub[] threads, object context, string methodName)
		{
			this.CurrentThreads = threads;
			loaded();
		}

		void loaded()
		{
			if (OnLoaded != null)
				OnLoaded(this, EventArgs.Empty);
		}

		internal void ReloadThreads(int objectK)
		{
			this.objectK = objectK;
			loadThreadsViaWebService();
		}
	}
}
