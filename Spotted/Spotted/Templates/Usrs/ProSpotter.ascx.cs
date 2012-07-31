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

namespace Spotted.Templates.Usrs
{
	public partial class ProSpotter : System.Web.UI.UserControl
	{
		protected DataList PhotosDataList;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Query q = new Query();
			q.NoLock = true;
			q.TableElement = Photo.UsrFavouritesJoin;
			q.QueryCondition = new And(
				Photo.EnabledQueryCondition,
				new Q(Photo.Columns.UsrK, CurrentUsr.K),
				new Q(UsrPhotoFavourite.Columns.UsrK, CurrentUsr.K)
				);
			q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);
			q.TopRecords = 5;
			PhotoSet ps = new PhotoSet(q);
			if (ps.Count > 0)
			{
				PhotosDataList.Visible = true;
				PhotosDataList.DataSource = ps;
				PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/IconRollover.ascx");
				PhotosDataList.DataBind();
			}
			else
				PhotosDataList.Visible = false;
		}

		protected Usr CurrentUsr
		{
			get
			{
				if (currentUsr == null)
					currentUsr = ((Usr)((DataListItem)NamingContainer).DataItem);
				return currentUsr;
			}
		}
		Usr currentUsr;
	}
}
