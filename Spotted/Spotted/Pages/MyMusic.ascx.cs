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
using System.Collections.Generic;
namespace Spotted.Pages
{
	public partial class MyMusic : DsiUserControl
	{
		protected Controls.MusicTypes MusicTypes;
		protected Label UpdatedLabel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("Sorry, you must be logged in to use this page...");
			Prefs.Current["SeenMusic"] = 1;
			MusicTypes.InitialMusicTypes = Usr.Current.MusicTypesFavourite;

		}

		public void Update(object o, System.EventArgs e)
		{
			MusicTypeSet mtsFav = Usr.Current.MusicTypesFavourite;
			Query q = new Query();
			q.OrderBy = MusicType.OrderBy;
			MusicTypeSet mts = new MusicTypeSet(q);
			List<int> alSel = MusicTypes.SelectedMusicTypes;

			foreach (MusicType mt in mts)
			{
				bool found = false;
				foreach (MusicType mtFav in mtsFav)
				{
					if (mtFav.K == mt.K)
					{
						found = true;
						if (!alSel.Contains(mt.K))
						{
							UsrMusicTypeFavourite u = new UsrMusicTypeFavourite(Usr.Current.K, mt.K);
							u.Delete();
							u.Update();
						}
						break;
					}
				}
				if (!found && alSel.Contains(mt.K))
				{
					UsrMusicTypeFavourite newMtf = new UsrMusicTypeFavourite();
					newMtf.UsrK = Usr.Current.K;
					newMtf.MusicTypeK = mt.K;
					newMtf.Update();
				}
			}
			Usr.Current.MusicTypesFavourite = null;
			MusicTypes.InitialMusicTypes = Usr.Current.MusicTypesFavourite;
			MusicTypes.SetState();
			UpdatedLabel.Visible = true;
			Usr.Current.UpdateMusicTypesFavouriteCount(true);
			if (ContainerPage.Url["NextPage"].Equals("UpdateOptions"))
				Response.Redirect("/pages/updateoptions");
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
