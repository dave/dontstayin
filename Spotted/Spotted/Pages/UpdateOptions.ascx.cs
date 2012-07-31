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
	public partial class UpdateOptions : DsiUserControl
	{
		protected CheckBox EmailCheck, MusicGeneric;
		protected Label MusicLabel, GenericMusicLabel, PlacesLabel;
		protected HtmlGenericControl GenericMusicP;
		protected Panel OptionsPanel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			ContainerPage.SetPageTitle("Weekly email options");
			Usr.KickUserIfNotLoggedIn("You must be logged in to use this page.");

			MusicLabel.Text = "";
			GenericMusicLabel.Text = "";
			PlacesLabel.Text = "";

			#region Build SelectedMusicTypes, GenericMusicTypes and SelectedPlaces
			SortedList SelectedMusicTypes = new SortedList();
			SortedList GenericMusicTypes = new SortedList();
			SortedList SelectedPlaces = new SortedList();
			ArrayList musicTypesK = new ArrayList();

			if (Usr.Current.MusicTypesFavouriteCount > 0)
			{
				foreach (MusicType mt in Usr.Current.MusicTypesFavourite)
				{
					if (!musicTypesK.Contains(mt.K))
					{
						musicTypesK.Add(mt.K);
						SelectedMusicTypes.Add(mt.Order, mt);
						if (mt.ParentK > 1 && !musicTypesK.Contains(mt.ParentK))
						{
							musicTypesK.Add(mt.ParentK);
							GenericMusicTypes.Add(mt.Parent.Order, mt.Parent);
						}
					}
				}
			}
			else
			{
				if (Usr.Current.FavouriteMusicTypeK != 0)
				{
					musicTypesK.Add(Usr.Current.FavouriteMusicTypeK);
					SelectedMusicTypes.Add(Usr.Current.FavouriteMusicType.Order, Usr.Current.FavouriteMusicType);
				}
			}
			if (!musicTypesK.Contains(1))
			{
				musicTypesK.Add(1);
				MusicType mtAllMusic = new MusicType(1);
				GenericMusicTypes.Add(mtAllMusic.Order, mtAllMusic);
			}


			ArrayList placesK = new ArrayList();
			if (Usr.Current.HomePlaceK > 0)
			{
				placesK.Add(Usr.Current.HomePlaceK);
				SelectedPlaces.Add(Usr.Current.Home.Name, Usr.Current.Home);
			}
			PlaceSet ps = Usr.Current.PlacesVisit(null, 0);
			foreach (Place p in ps)
			{
				if (!placesK.Contains(p.K))
				{
					placesK.Add(p.K);
					SelectedPlaces.Add(p.Name, p);
				}
			}
			#endregion

			bool first = true;
			if (SelectedMusicTypes.Count == 0)
				MusicLabel.Text = "[none selected]";
			else
			{
				foreach (MusicType mt in SelectedMusicTypes.Values)
				{
					MusicLabel.Text += (first ? "" : ", ") + mt.Name;
					first = false;
				}
			}

			first = true;
			if (GenericMusicTypes.Count == 0)
				GenericMusicP.Visible = false;
			else
			{
				foreach (MusicType mt in GenericMusicTypes.Values)
				{
					GenericMusicLabel.Text += (first ? "" : ", ") + mt.Name;
					first = false;
				}
			}

			first = true;
			if (SelectedPlaces.Count == 0)
				PlacesLabel.Text = "[none selected]";
			else
			{
				foreach (Place p in SelectedPlaces.Values)
				{
					PlacesLabel.Text += (first ? "" : ", ") + p.NamePlainRegion;
					first = false;
				}
			}


			if (!Page.IsPostBack)
			{
				EmailCheck.Checked = Usr.Current.SendSpottedEmails;

				MusicGeneric.Checked = Usr.Current.UpdateSendGenericMusic;

				OptionsPanel.Visible = Usr.Current.SendSpottedEmails;
			}
		}
		public void Save(object o, System.EventArgs e)
		{
			Page.Validate();
			if (Page.IsValid)
			{
				Usr.Current.SendSpottedEmails = EmailCheck.Checked;
				Usr.Current.UpdateSendGenericMusic = MusicGeneric.Checked;
				Usr.Current.Update();
				OptionsPanel.Visible = Usr.Current.SendSpottedEmails;
			}
		}
	}
}
