using System;
using Sys;
using Sys.Net;
using Utils;

namespace SpottedScript.Utils
{

	#region interfaces, basic classes
	internal interface IProviderService
	{
		void CallWebService(Dictionary parameterSource, WebServiceSuccessCallback successCallback, WebServiceFailureCallback failureCallback, object userContext);
	}
	internal interface IPagedProviderService
	{
		void CallWebService(int firstRowIndex, int lastRowIndex, Dictionary parameterSource, WebServiceSuccessCallback successCallback, WebServiceFailureCallback failureCallback, object userContext);
	}

	internal class ProviderService : IProviderService
	{
		private readonly string servicePath;
		private readonly string serviceMethod;
		private readonly int timeOut;

		internal ProviderService(string servicePath, string serviceMethod, int timeOut)
		{
			this.servicePath = servicePath;
			this.serviceMethod = serviceMethod;
			this.timeOut = timeOut;
		}

		private WebRequest webRequest;
		private WebServiceSuccessCallback successCallback;
		private WebServiceFailureCallback failureCallback;
		public void CallWebService(Dictionary parameters, WebServiceSuccessCallback successCallback, WebServiceFailureCallback failureCallback, object userContext)
		{
			this.successCallback = successCallback;
			this.failureCallback = failureCallback;

			if (this.webRequest != null)
			{
				Trace.Write("ABORT");
				this.webRequest.Executor.Abort();
			}

			this.webRequest = WebServiceProxy.Invoke(servicePath, serviceMethod, false, parameters, successCallback2, failureCallback2, userContext, timeOut);
		}
		private void successCallback2(object result, object userContext, string methodName)
		{
			this.webRequest = null;
			this.successCallback(result, userContext, methodName);
		}
		private void failureCallback2(WebServiceError error, object userContext, string methodName)
		{
			this.webRequest = null;
			this.failureCallback(error, userContext, methodName);
		}
	}
	internal class PagedProviderService : ProviderService, IPagedProviderService
	{
		internal PagedProviderService(string servicePath, string serviceMethod, int timeOut)
			: base(servicePath, serviceMethod, timeOut)
		{
		}

		public void CallWebService(int firstRowIndex, int lastRowIndex, Dictionary parameters, WebServiceSuccessCallback successCallback, WebServiceFailureCallback failureCallback, object userContext)
		{
			Dictionary pagedParameters = new Dictionary();
			pagedParameters["firstRowIndex"] = firstRowIndex;
			pagedParameters["lastRowIndex"] = lastRowIndex;
			foreach (DictionaryEntry de in parameters)
			{
				pagedParameters[de.Key] = de.Value;
			}
			base.CallWebService(pagedParameters, successCallback, failureCallback, userContext);
		}
	}
	#endregion

	internal delegate void DataRetrieved(object[] data, int totalData, bool moreDataAvailable);
	internal delegate void NoDataRetrieved();

	#region CachedPagedProvider
	internal class CachedPagedProvider
	{
		const int cachePageSizeMultiplier = 1;
		private readonly IPagedProviderService provider;

		private string key;
		private Dictionary cachedDataStore;
		private Array cachedData
		{
			get { return (Array) (cachedDataStore[this.key] ?? (cachedDataStore[this.key] = new Array())); }
		}
		private Dictionary totalKnownDataItemsStore;
		private int totalKnownDataItems
		{
			get { return (int)(totalKnownDataItemsStore[this.key] ?? (totalKnownDataItemsStore[this.key] = 0)); }
			set { totalKnownDataItemsStore[this.key] = value; }
		}

		private Dictionary thereIsNoMoreDataInDatabaseStore;
		private bool thereIsNoMoreDataInDatabase
		{
			get { return (bool)(thereIsNoMoreDataInDatabaseStore[this.key]); } // no cast necessary - null === false
			set { thereIsNoMoreDataInDatabaseStore[this.key] = value; }
		}

		internal CachedPagedProvider(IPagedProviderService provider)
		{
			this.provider = provider;
			this.SetUpCaches();
		}
		internal void Flush()
		{
			this.SetUpCaches();
		}
		private void SetUpCaches()
		{
			this.cachedDataStore = new Dictionary();
			this.totalKnownDataItemsStore = new Dictionary();
			this.thereIsNoMoreDataInDatabaseStore = new Dictionary();
		}

		private DataRetrieved dataRetrievedCallback;
		private NoDataRetrieved noDataRetrievedCallback;
		private int pageNumber;
		private int pageSize;
		private Dictionary parameters;

		private int firstRowIndexToGet = -1;
		private int lastRowIndexToGet = -1;


		internal void Get(int pageNumber, int pageSize, Dictionary parameters, DataRetrieved dataRetrievedCallback, NoDataRetrieved noDataRetrievedCallback)
		{
			this.dataRetrievedCallback = dataRetrievedCallback;
			this.noDataRetrievedCallback = noDataRetrievedCallback;
			this.pageNumber = pageNumber;
			this.pageSize = pageSize;
			this.parameters = parameters;
			this.key = GetKey(parameters);

			object[] requestedPageData = new object[0]; // leave as zero in declaration to fill up and expand later 

			bool someRequestedDataIsNotInTheCacheAlready = false;
			
			// 1, 10: 0..9
			for (int i = (pageNumber - 1) * pageSize; i < pageNumber * pageSize - 1; i++)
			{
				if (cachedData[i] != null)
				{
					if (cachedData[i] is NoMoreData)
					{
						break;
					}
					requestedPageData[requestedPageData.Length] = cachedData[i];
				}
				else
				{
					someRequestedDataIsNotInTheCacheAlready = true;
					break;
				}
			}

			if (someRequestedDataIsNotInTheCacheAlready && !thereIsNoMoreDataInDatabase)
			{
				int cachePageSize = cachePageSizeMultiplier * pageSize;
				this.firstRowIndexToGet = (int)Math.Floor((pageNumber - 1) / cachePageSizeMultiplier) * cachePageSize;
				this.lastRowIndexToGet = firstRowIndexToGet + cachePageSize - 1;

				if (this.cachedData != null)
				{
					// 1, 10: 0..9
					for (; firstRowIndexToGet < lastRowIndexToGet; firstRowIndexToGet++)
					{
						if (this.cachedData[firstRowIndexToGet] == null)
						{
							break;
						}
					}
				}

				provider.CallWebService(this.firstRowIndexToGet, this.lastRowIndexToGet + 1, parameters, successCallback, failureCallback, null);
			}
			else
			{
				dataRetrievedCallback(requestedPageData, this.totalKnownDataItems, !this.thereIsNoMoreDataInDatabase);
			}
		}

		private void failureCallback(WebServiceError error, object userContext, string methodName)
		{
			Trace.WebServiceFailure(error, userContext, methodName);
			this.noDataRetrievedCallback();
		}

		private void successCallback(object dataObject, object userContext, string methodName)
		{
			object[] data = (object[])dataObject;

			for (int i = 0; i < data.Length; i++)
			{
				this.cachedData[firstRowIndexToGet + i] = data[i];
			}

			if (data.Length > lastRowIndexToGet - firstRowIndexToGet + 1) // inclusive
			{
				this.totalKnownDataItems = lastRowIndexToGet + 1;
				this.thereIsNoMoreDataInDatabase = false;
			}
			else
			{
				this.totalKnownDataItems = firstRowIndexToGet + data.Length;
				this.thereIsNoMoreDataInDatabase = true;
				this.cachedData[firstRowIndexToGet + data.Length] = new NoMoreData();
			}

			Get(this.pageNumber, this.pageSize, this.parameters, this.dataRetrievedCallback, this.noDataRetrievedCallback);
		}

		private static string GetKey(Dictionary parameters)
		{
			StringBuilder sb = new StringBuilder();
			foreach (DictionaryEntry de in parameters)
			{
				sb.Append(de.Key);
				sb.Append(":");
				sb.Append(de.Value.ToString());
				sb.Append(";");
			}
			return sb.ToString();
		}
	}
	#endregion

	class NoMoreData{}
}
