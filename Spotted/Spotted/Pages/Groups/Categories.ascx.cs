using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Model.Entities;
using ServiceLocator;
using Spotted.Controls;

namespace Spotted.Pages.Groups
{
	public partial class Categories : DsiUserControl
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			

			var dc = SL.Get<IDsiDataContext>();

			var threadsInReverseOrder =
				from t in dc.Threads
				orderby t.K descending
				select t;

			var groupsWithThreads =
				from g in dc.Groups
				join t in threadsInReverseOrder.DefaultIfEmpty().Take(1) on g.K equals t.GroupK
				select new { g, t }

			;


	
      
				
			

					
			this.uiList.Headings = (from t in dc.Themes
						  orderby t.Order
						  select new GroupsListedByHeading.HeadingDTO
									{
										Title = t.Name,
										Description = t.Examples,
										Url = "/theme-" + t.K,
										Groups = (from gt in groupsWithThreads
												  where gt.g.ThemeK == t.K
												  orderby gt.g.TotalMembers descending
												  select new GroupsListedByHeading.GroupDTO
												         	{
												         		Title = gt.g.Name,
																Description = gt.g.Description,
																TotalMembers = gt.g.TotalMembers,
																Url = "/group-" + gt.g.K,
																LastThread = new GroupsListedByHeading.ThreadDTO{
																	DateTime = gt.t.DateTime,
																	Subject = gt.t.Subject,
																	Url = "/thread-" + t.K
																	}
																	
												         	}
												).Take(5)
									}).ToArray();

		}
	}
}
