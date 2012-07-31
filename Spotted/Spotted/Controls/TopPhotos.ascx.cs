using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Controls
{
	public partial class TopPhotos : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = new Query();
			q.Columns = Templates.Photos.TopPhoto.Columns;
			q.QueryCondition = new Q(Photo.Columns.PhotoOfWeek, true);
			q.OrderBy = new OrderBy(Photo.Columns.PhotoOfWeekDateTime, OrderBy.OrderDirection.Descending);
			q.TopRecords = 8;
			PhotoSet ps = new PhotoSet(q);
			if (ps.Count != 8)
			{
				if (Holder != null)
					Holder.Visible = false;
			}
			else
			{
				PhotoOfWeekAllDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/TopPhoto.ascx");
				PhotoOfWeekAllDataList.DataSource = ps;
				PhotoOfWeekAllDataList.DataBind();
			}
		}
		public Panel Holder { get; set; }
	}
}
