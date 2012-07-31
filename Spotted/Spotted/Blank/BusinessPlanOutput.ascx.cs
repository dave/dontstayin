using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Bobs;

namespace Spotted.Blank
{
	public partial class BusinessPlanOutput : BlankUserControl
	{
		int Year
		{
			get
			{
				return ContainerPage.Url[0];
			}
		}
		int Month
		{
			get
			{
				return ContainerPage.Url[1];
			}
		}
		DateTime Start
		{
			get
			{
				return new DateTime(Year, Month, 1);
			}
		}
		DateTime End
		{
			get
			{
				return new DateTime(Year, Month, 1).AddMonths(1);
			}
		}
		StringBuilder sb = new StringBuilder();
		void Write(string key, string val)
		{
			sb.Append(key);
			sb.Append(",");
			sb.Append(val);
			sb.Append("\n");
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Usr.Current.IsAdmin)
				throw new Exception("Admin only!");

			Write("Year", Year.ToString());
			Write("Month", Month.ToString("00"));
			Write("GenerationDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			try
			{
				Query q = new Query();
				q.NoLock = true;
				q.Columns = new ColumnSet();
				q.ExtraSelectElements["sum"] = "sum(Count)";
				q.QueryCondition = new And(
					new Q(Log.Columns.Date, QueryOperator.GreaterThanOrEqualTo, Start),
					new Q(Log.Columns.Date, QueryOperator.LessThan, End),
					new Q(Log.Columns.Item, Log.Items.DsiPages)
				);
				LogSet ls = new LogSet(q);
				Write("PageImpressions", ls[0].ExtraSelectElements["sum"].ToString());
			}
			catch
			{
				Write("PageImpressions", "0");
			}

			
			/*



UniqueVisitors
MembersOnline
NewMembers
LiveChatPosts
Comments
PhotosAdded
EventsAdded
TunesAdded
NewPromoters
Stat1
Stat2
Stat3
Stat4
Stat5
Banner1Name
Banner1SlotWeekPrice
Banner1MaximumSlotWeeks
Banner1Impressions
Banner1SlotWeeksSold
Banner1Revenue
Banner2Name
Banner2SlotWeekPrice
Banner2MaximumSlotWeeks
Banner2Impressions
Banner2SlotWeeksSold
Banner2Revenue
Banner3Name
Banner3SlotWeekPrice
Banner3MaximumSlotWeeks
Banner3Impressions
Banner3SlotWeeksSold
Banner3Revenue
Banner4Name
Banner4SlotWeekPrice
Banner4MaximumSlotWeeks
Banner4Impressions
Banner4SlotWeeksSold
Banner4Revenue
Banner5Name
Banner5SlotWeekPrice
Banner5MaximumSlotWeeks
Banner5Impressions
Banner5SlotWeeksSold
Banner5Revenue
Banner6Name
Banner6SlotWeekPrice
Banner6MaximumSlotWeeks
Banner6Impressions
Banner6SlotWeeksSold
Banner6Revenue
Banner7Name
Banner7SlotWeekPrice
Banner7MaximumSlotWeeks
Banner7Impressions
Banner7SlotWeeksSold
Banner7Revenue
Banner8Name
Banner8SlotWeekPrice
Banner8MaximumSlotWeeks
Banner8Impressions
Banner8SlotWeeksSold
Banner8Revenue
Banner9Name
Banner9SlotWeekPrice
Banner9MaximumSlotWeeks
Banner9Impressions
Banner9SlotWeeksSold
Banner9Revenue
Banner10Name
Banner10SlotWeekPrice
Banner10MaximumSlotWeeks
Banner10Impressions
Banner10SlotWeeksSold
Banner10Revenue
Banner1AgencyImpressionsSent
Banner1AgencyDefaultReturned
Banner2AgencyImpressionsSent
Banner2AgencyDefaultReturned
Banner3AgencyImpressionsSent
Banner3AgencyDefaultReturned
Banner4AgencyImpressionsSent
Banner4AgencyDefaultReturned
Banner5AgencyImpressionsSent
Banner5AgencyDefaultReturned
Banner6AgencyImpressionsSent
Banner6AgencyDefaultReturned
Banner7AgencyImpressionsSent
Banner7AgencyDefaultReturned
Banner8AgencyImpressionsSent
Banner8AgencyDefaultReturned
Banner9AgencyImpressionsSent
Banner9AgencyDefaultReturned
Banner10AgencyImpressionsSent
Banner10AgencyDefaultReturned
DonationsCount
DonationsRevenue
OtherRevenue1Name
OtherRevenue1Value
OtherRevenue2Name
OtherRevenue2Value
OtherRevenue3Name
OtherRevenue3Value
OtherRevenue4Name
OtherRevenue4Value
OtherRevenue5Name
OtherRevenue5Value
GuestlistCreditsUsed
GuestlistCreditRevenue
TicketsCount
TicketsRevenue
DownloadsCount
DownloadsRevenue

			 * */

			Response.Write("<pre>");
			Response.Write(sb.ToString());
			Response.Write("</pre>");
			Response.End();

		}
	}
}
