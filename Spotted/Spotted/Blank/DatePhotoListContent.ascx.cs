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

namespace Spotted.Blank
{
	public partial class DatePhotoListContent : System.Web.UI.UserControl
	{
		public Repeater PhotosDataList;
		Master.BlankPage ContainerPage
		{
			get
			{
				return ((Spotted.BlankUserControl)this.NamingContainer).ContainerPage;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			int usrK = ContainerPage.Url["UsrK"];
			Query q = new Query();
			q.TopRecords = 50;
			q.OrderBy = new OrderBy(new OrderBy(Photo.Columns.WeightedSexyRating, OrderBy.OrderDirection.Descending), new OrderBy(Photo.Columns.WeightedCoolRating, OrderBy.OrderDirection.Descending));
			q.TableElement = Photo.UsrMeJoin;
			q.QueryCondition = new And(new Q(Usr.Columns.K, usrK), Photo.EnabledQueryCondition);
			PhotoSet ps = new PhotoSet(q);
			PhotosDataList.DataSource = ps;
			PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/DatePhotoList.ascx");
			PhotosDataList.DataBind();
		}
	}
}
