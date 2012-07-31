using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
namespace Spotted.Pages.Events
{
	public partial class MyPhotos : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current == null)
				Response.Redirect(ContainerPage.Url.ObjectFilterEvent.Url());
			else
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Gallery.Columns.OwnerUsrK, Usr.Current.K), new Q(Gallery.Columns.EventK, ContainerPage.Url.ObjectFilterEvent.K));
				q.OrderBy = new OrderBy(Gallery.Columns.CreateDateTime, OrderBy.OrderDirection.Ascending);
				GallerySet gs = new GallerySet(q);
				if (gs.Count > 0)
					Response.Redirect(gs[0].Url());
				else
					Response.Redirect(ContainerPage.Url.ObjectFilterEvent.Url());
			}
		}
	}
}
