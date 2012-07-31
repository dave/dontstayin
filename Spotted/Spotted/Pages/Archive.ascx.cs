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

namespace Spotted.Pages
{
	public partial class Archive : DsiUserControl
	{
		public Model.Entities.ArchiveObjectType Type
		{
			get
			{
				if (ContainerPage.Url[0].Key.Equals("galleries"))
					return Model.Entities.ArchiveObjectType.Gallery;
				else if (ContainerPage.Url[0].Key.Equals("news"))
					return Model.Entities.ArchiveObjectType.News;
				else if (ContainerPage.Url[0].Key.Equals("reviews"))
					return Model.Entities.ArchiveObjectType.Review;
				else if (ContainerPage.Url[0].Key.Equals("competitions"))
					return Model.Entities.ArchiveObjectType.Comp;
				else if (ContainerPage.Url[0].Key.Equals("articles"))
					return Model.Entities.ArchiveObjectType.Article;
				else if (ContainerPage.Url[0].Key.Equals("guestlists"))
					return Model.Entities.ArchiveObjectType.Guestlist;
				else
					return Model.Entities.ArchiveObjectType.News;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			string name = "";
			if (Type.Equals(Model.Entities.ArchiveObjectType.Gallery))
			{
				name = "Galleries";
			}
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Article))
			{
				name = "Articles";
			}
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Comp))
			{
				name = "Competitions";
			}
			else if (Type.Equals(Model.Entities.ArchiveObjectType.News))
			{
				name = "News";
			}
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Review))
			{
				name = "Reviews";
			}
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Guestlist))
			{
				name = "Guestlists";
			}

			name += " archive";

			if (ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is IHasArchive)
				name += " for " + ((IName)ContainerPage.Url.ObjectFilterBob).FriendlyName;

			name += " - " + ContainerPage.Url.DateFilter.ToString("MMMM") + " " + ContainerPage.Url.DateFilter.Year.ToString();

			ContainerPage.SetPageTitle(name);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
