using System;
using System.DHTML;
using SpottedScript.Controls.Tabbing.Tab;
using SpottedScript.Pages.MapBrowser;
using Sys;
using Sys.Net;
using SpottedScript.Utils;
using Utils;

namespace SpottedScript.Controls.PagedData.Display
{
	public class Controller : ITabController
	{
		private EventHandler _updated;
		public EventHandler Updated { get { return _updated; } set { _updated = value; } }
		View view;
		public Controller(View view)
		{
			this.view = view;
			this.view.uiPager.CurrentPage = 1;
			this.view.uiPager.OnPageChanged = delegate { DisplayInfoForTab(); };

			this.provider = new CachedPagedProvider(new PagedProviderService(view.uiServicePath.Value, view.uiServiceMethod.Value, int.ParseInvariant(view.uiTimeout.Value)));

			for(int i=0;i<this.ParameterSources.Length;i++)
			{
				ParameterSources[i].ParametersUpdated = Misc.CombineEventHandler(ParameterSources[i].ParametersUpdated, OnParametersUpdated);
			}
		}

		private void OnParametersUpdated(object sender, EventArgs e)
		{
			DisplayInfoForTab();
		}

		public MapItem[] CurrentData;
		 
		private void DataReturned(MapItem[] result, int totalData, bool moreDataAvailable)
		{
			CurrentData = result;
			Trace.Write("MapInfoReturned");
			view.uiRepeater.DisplayData(GetDataFromMapItems(CurrentData));

			view.uiPager.LastPage = moreDataAvailable ? -1 : (int) Math.Ceil(totalData/PageSize);
			if (header != null)
			{
				header.InnerHTML = "<center>" + view.uiTabName.Value + "<br>(" + totalData +
				                   (moreDataAvailable ? "+" : "") + ")" + "</center>";
			}

			if (Updated != null) Updated(this, null);

		}
		int PageSize
		{
			get { return int.ParseInvariant(view.uiPageSize.Value); }
		}
		private static object[] GetDataFromMapItems(MapItem[] mapItem)
		{
			object[] data = new object[mapItem.Length];
			for (int i=0; i< data.Length; i++)
			{
				data[i] = mapItem[i].data;
			}
			return data;
		}


		public DOMElement uiPanel
		{
			get { return view.uiPanel; }
		}

		#region ITabController Members

		private readonly CachedPagedProvider provider;
		public void DisplayInfoForTab()
		{
			Dictionary parameterSourceParameters = new Dictionary();
			for (int i = 0; i < ParameterSources.Length; i++)
			{
				foreach (DictionaryEntry de in ParameterSources[i].Parameters)
				{
					parameterSourceParameters[de.Key] = de.Value;
				}
			}
			provider.Get(view.uiPager.CurrentPage, PageSize, parameterSourceParameters, OnResultsGot, OnFailure);
		}
		IParameterSource[] parameterSources;
		IParameterSource[] ParameterSources
		{
			get
			{
				if (parameterSources == null)
				{
					string[] parameterSourceNames = this.view.uiParameterSourceNames.Value.Split(',');
					parameterSources = new IParameterSource[parameterSourceNames.Length];
					for (int i = 0; i < parameterSourceNames.Length; i++)
					{
						parameterSources[i] = (IParameterSource) Script.Eval(parameterSourceNames[i]);
					}
				}
				return parameterSources;
			}
		}

		private void OnResultsGot(object[] results, int totalData, bool moreDataAvailable)
		{
			DataReturned((MapItem[])results, totalData, moreDataAvailable);
		}

		private void OnFailure()
		{
			DataReturned(new MapItem[0], 0, false);
		}

		#endregion


		public DOMElement header;
		public void SetHeader(DOMElement el)
		{
			header = el;
		}

	}
}
