using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Text;

namespace Spotted.MixmagVote
{
	public partial class Results : MixmagVoteUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = new Query();
			q.QueryCondition = new Q(MixmagEntry.Columns.MixmagCompK, int.Parse(Url["CompK"]));
			q.OrderBy = new OrderBy(MixmagEntry.Columns.DateTime, OrderBy.OrderDirection.Descending);
			q.ExtraSelectElements.Add("Votes", "(SELECT COUNT(*) FROM [MixmagVote] WHERE [MixmagVote].[MixmagEntryK]=[MixmagEntry].[K])");
			MixmagEntrySet mes = new MixmagEntrySet(q);

			StringBuilder s = new StringBuilder();
			foreach (MixmagEntry me in mes)
			{
				s.Append("<div style='width:30px; float:left;'>" + me.ExtraSelectElements["Votes"].ToString() + "</div>");
				s.Append("<div style='float:left;'><a href='" + me.ImageUrl + "' target='_blank'>" + me.ImageUrl + "</a></div><br />");
			}
			Output.Controls.Add(new LiteralControl(s.ToString()));
		}
	}
}
