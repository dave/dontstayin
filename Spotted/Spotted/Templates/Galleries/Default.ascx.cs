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

namespace Spotted.Templates.Galleries
{
	public partial class Default : System.Web.UI.UserControl
	{
		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Gallery.Columns.K,
					Gallery.Columns.Name,
					Gallery.Columns.UrlFragment,
					Gallery.Columns.ArticleK,
					Gallery.Columns.MainPhotoK,
					Gallery.Columns.LivePhotos,
					Gallery.Columns.CreateDateTime,
					Gallery.Columns.EventK
				);
			}
		}

		public static TableElement PerformJoins(TableElement tIn, bool skipEventJoin)
		{
			if (tIn == null)
				tIn = new TableElement(TablesEnum.Gallery);
			TableElement t = tIn;
			if (skipEventJoin)
			{
				t = new Join(
					tIn,
					Venue.CountryJoin,
					QueryJoinType.Inner,
					Event.Columns.VenueK,
					Venue.Columns.K);
			}
			else
			{
				t = new Join(
					tIn,
					Event.CountryAllJoin,
					QueryJoinType.Inner,
					Gallery.Columns.EventK,
					Event.Columns.K);
			}
			return t;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentGallery != null)
			{
				NameAnchor.InnerText = CurrentGallery.Name;
				NameAnchor.HRef = CurrentGallery.Url();
				//CurrentGallery.MakeRollover(NameAnchor);
				LivePhotosLabel.Text = CurrentGallery.LivePhotos.ToString() + " photo" + (CurrentGallery.LivePhotos == 1 ? "" : "s");
				//EventLabel.Text=CurrentGallery.Event.FriendlyName;
				try
				{
					if (NamingContainer.NamingContainer.NamingContainer.NamingContainer is Controls.Latest)
					{
						object parent = ((Controls.Latest)(NamingContainer.NamingContainer.NamingContainer.NamingContainer)).Parent;
						if (parent == null || parent is Country || parent is Brand || parent is Group)
						{
							EventLabel.Text = " from <a href=\"" + CurrentGallery.Event.Url() + "\">" + CurrentGallery.Event.Name + "</a> @ <a href=\"" + CurrentGallery.Event.Venue.Url() + "\">" + CurrentGallery.Event.Venue.Name + "</a> in <a href=\"" + CurrentGallery.Event.Venue.Place.Url() + "\">" + CurrentGallery.Event.Venue.Place.Name + "</a>, " + CurrentGallery.Event.FriendlyDate(false);
						}
						else if (parent is Place)
						{
							EventLabel.Text = " from <a href=\"" + CurrentGallery.Event.Url() + "\">" + CurrentGallery.Event.Name + "</a> @ <a href=\"" + CurrentGallery.Event.Venue.Url() + "\">" + CurrentGallery.Event.Venue.Name + "</a>, " + CurrentGallery.Event.FriendlyDate(false);
						}
						else if (parent is Venue)
						{
							EventLabel.Text = " from <a href=\"" + CurrentGallery.Event.Url() + "\">" + CurrentGallery.Event.Name + "</a>, " + CurrentGallery.Event.FriendlyDate(false);
						}
						else if (parent is Event)
						{
							EventLabel.Text = "";
						}
					}
					else
					{
						if (NamingContainer.NamingContainer.NamingContainer.NamingContainer is Spotted.Pages.FindYourPhotoContent)
						{
							if (HttpContext.Current.Request.QueryString["eventk"] != null)
								EventLabel.Text = "";
							else
								EventLabel.Text = " from <a href=\"" + CurrentGallery.Event.Url() + "\">" + CurrentGallery.Event.Name + "</a> @ <a href=\"" + CurrentGallery.Event.Venue.Url() + "\">" + CurrentGallery.Event.Venue.Name + "</a> in <a href=\"" + CurrentGallery.Event.Venue.Place.Url() + "\">" + CurrentGallery.Event.Venue.Place.Name + "</a>, " + CurrentGallery.Event.FriendlyDate(false);
						}
						else
						{
							EventLabel.Text = " from <a href=\"" + CurrentGallery.Event.Url() + "\">" + CurrentGallery.Event.Name + "</a> @ <a href=\"" + CurrentGallery.Event.Venue.Url() + "\">" + CurrentGallery.Event.Venue.Name + "</a> in <a href=\"" + CurrentGallery.Event.Venue.Place.Url() + "\">" + CurrentGallery.Event.Venue.Place.Name + "</a>, " + CurrentGallery.Event.FriendlyDate(false);
						}
					}
				}
				catch
				{
					EventLabel.Text = " from <a href=\"" + CurrentGallery.Event.Url() + "\">" + CurrentGallery.Event.Name + "</a> @ <a href=\"" + CurrentGallery.Event.Venue.Url() + "\">" + CurrentGallery.Event.Venue.Name + "</a> in <a href=\"" + CurrentGallery.Event.Venue.Place.Url() + "\">" + CurrentGallery.Event.Venue.Place.Name + "</a>, " + CurrentGallery.Event.FriendlyDate(false);
				}
			}
		}
		public void Page_Init(object o, System.EventArgs e)
		{
			//Strange - CurrentGallery is always null if we don't access it in the Init!
			if (CurrentGallery != null)
			{
				int i = CurrentGallery.K;
			}
		}


		protected Gallery CurrentGallery
		{
			get
			{
				if (currentGallery == null)
					currentGallery = (Gallery)((DataListItem)NamingContainer).DataItem;
				return currentGallery;
			}
		}
		Gallery currentGallery;

	}
}
