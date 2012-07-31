using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Js.Controls.EventBox;
using Bobs;
using System.Collections;


namespace Spotted.WebServices.Controls.EventBox
{
	/// <summary>
	/// Summary description for Service
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{

		[WebMethod]
		[ScriptMethod]
		public EventPageStub GetEventPage(string key)
		{
			//int rndK = new Random().Next(190000);
			//Query q = new Query();
			//q.QueryCondition = new And(
			//    new Q(Event.Columns.Pic, QueryOperator.IsNotNull, null),
			//    new Q(Event.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty),
			//    new Q(Event.Columns.K, QueryOperator.GreaterThan, rndK),
			//    new Q(Event.Columns.K, QueryOperator.LessThan, rndK + 1000)
			//);
			//q.OrderBy = new OrderBy(Event.Columns.UsrAttendCount, OrderBy.OrderDirection.Descending);
			//q.TopRecords = 8;
			//EventSet es = new EventSet(q);

			try
			{

				EventPageStub data = EventPageDetails.GetStubFromKey(key);

				int currentMusicPref = Prefs.Current["MusicPref"].Exists || Prefs.Current["MusicPref"].IsInt ? (int)Prefs.Current["MusicPref"] : 1;
				if (data.musicTypeK != currentMusicPref)
					Prefs.Current["MusicPref"] = data.musicTypeK;
				
				EventSet es = Event.GetEventSetFromEventBoxKey(key);

				data.requestedPageIndex = data.pageIndex;
				data.pageIndex = es.Paging.ReturnedPageIndex;

				EventPageDetails page = new EventPageDetails(
					"",
					es,
					data,
					false);

				return page.Data;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
