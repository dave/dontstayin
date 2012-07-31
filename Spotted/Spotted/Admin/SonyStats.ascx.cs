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
using Bobs;

namespace Spotted.Admin
{
	public partial class SonyStats : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			for (DateTime d = new DateTime(2006, 07, 31); d < new DateTime(2006, 10, 01); d = d.AddDays(7))
			{
				Table t = new Table();
				t.BorderWidth = new Unit(1);
				t.BorderColor = System.Drawing.Color.Black;
				t.Style.Add("margin-bottom", "5px");

				Output("Week " + d.ToString("ddd dd-MM") + " to " + d.AddDays(6).ToString("ddd dd-MM"), "", t);
				

				if (true)
				{

					Output("New group members", Log.GetSum(Log.Items.CaptionGroupJoin, d, d.AddDays(7)).ToString("#,##0"), t);

				}

				if (true)
				{
					Query q = new Query();
					q.Columns = new ColumnSet();
					q.ReturnCountOnly = true;
					if (Vars.CaptionIsBrand)
					{
						q.QueryCondition = new And(
							new Q(Photo.Columns.ParentDateTime, QueryOperator.GreaterThanOrEqualTo, d),
							new Q(Photo.Columns.ParentDateTime, QueryOperator.LessThan, d.AddDays(7)),
							new Q(EventBrand.Columns.BrandK, Vars.CaptionBrandK));
						q.TableElement = new Join.Series(Photo.Columns.GalleryK, Gallery.Columns.K, Gallery.Columns.EventK, Event.Columns.K, EventBrand.Columns.EventK);
					}
					else
					{
						q.QueryCondition = new And(
							new Q(Photo.Columns.ParentDateTime, QueryOperator.GreaterThanOrEqualTo, d),
							new Q(Photo.Columns.ParentDateTime, QueryOperator.LessThan, d.AddDays(7)),
							new Q(GroupEvent.Columns.GroupK, Vars.CompetitionGroupK));
						q.TableElement = new Join.Series(Photo.Columns.GalleryK, Gallery.Columns.K, Gallery.Columns.EventK, Event.Columns.K, GroupEvent.Columns.EventK);
					}
					PhotoSet ps = new PhotoSet(q);
					

					Output("Photos uploaded", ps.Count.ToString("#,##0"), t);

				}

				//Output("Competition entries", Log.GetSum(Log.Items.CaptionsAdded, d, d.AddDays(7)).ToString("#,##0"), t);
				//Output("Banner impressions", BannerStat.GetHits(3913, d, d.AddDays(7)).ToString("#,##0"), t);
				//Output("Banner clicks", BannerStat.GetClicks(3913, d, d.AddDays(7)).ToString("#,##0"), t);


				MainDiv.Controls.Add(t);

			}
		}
		public void Output(string title, string val, Table t)
		{
			TableRow tr = new TableRow();

			TableCell tc = new TableCell();
			tc.Text = title;
			tr.Cells.Add(tc);

			TableCell tc1 = new TableCell();
			tc1.Text = val;

			tr.Cells.Add(tc1);

			t.Rows.Add(tr);
		}
	}
}
