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

namespace Spotted.Templates.Venues
{
	public partial class Admin : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.NamingContainer.ID = "VenueContainerK" + Current.K.ToString();
			this.ID = "VenueTemplateK" + Current.K.ToString();
			// Put user code to initialize the page here
			if (Current.HasPic)
				Pic.Src = Current.PicPath;
			else
				Pic.Visible = false;

			SimilarVenuesRow.Visible = Current.SimilarVenues != null && Current.SimilarVenues.Count > 0;

			DeleteButton.Attributes["onclick"] = "return confirm('Are you sure? This will delete this venue, and any objects that are under it (e.g. all events, comments, galleries, photos etc.!)')";
		}

		protected string EventsText
		{
			get
			{
				if (Current.Events.Count == 0)
					return "(none)";
				else
				{
					string txt = Current.Events.Count.ToString() + " event" + (Current.Events.Count == 1 ? "" : "s") + ":<br>&nbsp;<br>";
					if (Current.Events.Count <= 6)
					{
						foreach (Event e in Current.Events)
						{
							txt += "<a href=\"" + e.Url() + "\" target=\"_blank\">" + e.FriendlyName + "</a><br>";
						}
					}
					else
					{
                        //Current.Events.ReFill(0, 3);
                        for (int i = 0; i < 3; i++)
						{
                            txt += "<a href=\"" + Current.Events[i].Url() + "\" target=\"_blank\">" + Current.Events[i].FriendlyName + "</a><br>";
						}
                        //						Current.Events = null;
						txt += "...(" + ((int)(Current.Events.Count - 6)).ToString() + " removed)...<br>";
                        //						Current.Events.ReFill(Current.Events.Count - 4, 3);
                        for (int j = Current.Events.Count - 4; j < Current.Events.Count; j++)
						{
                            txt += "<a href=\"" + Current.Events[j].Url() + "\" target=\"_blank\">" + Current.Events[j].FriendlyName + "</a><br>";
						}
					}
					return txt;
				}
			}
		}

		public void DeleteAll(object o, System.EventArgs e)
		{
			Venue.DeleteReturnStatus status = Current.DeleteAllUsr(Usr.Current);
			if (!status.Equals(Venue.DeleteReturnStatus.Success))
			{
				StatusLabel.Text = "<b>FAILED</b> - code: " + status.ToString() + ". Contact admin with details to delete this venue.";
				((Spotted.Master.DsiPage)Page).AnchorSkip("ucAdminAnchorVenueK" + Current.K.ToString());
			}
			else
				Response.Redirect("/pages/venues/moderate");
		}

		protected string SimilarText
		{
			get
			{
				if (Current.SimilarVenues == null || Current.SimilarVenues.Count == 0)
					return "";
				else
				{
					string html = "";
					foreach (Venue v in Current.SimilarVenues)
					{
						html += "<a href=\"" + v.Url() + "\" target=\"_blank\">" + v.FriendlyName + "</a> ("+v.Postcode+")<br>";
					}
					return html;
				}
			}
		}



		public void Enable(object o, System.EventArgs e)
		{
			if (Current.IsDescriptionText || Current.IsDescriptionCleanHtml || Current.DetailsHtml.ToLower().IndexOf("<dsi:html") > -1 || Current.DetailsHtml.ToLower().IndexOf("<p>") > -1)
			{
				Current.IsNew = false;
				Current.IsEdited = false;
				Current.Update();
				Response.Redirect("/pages/venues/moderate");
			}
			else
			{
				//EnableError.Visible = true;
				throw new DsiUserFriendlyException("Unable to enable venue - please check Description");
			}
		}

		protected Venue Current
		{
			get
			{
				if (currentVenue == null)
					currentVenue = ((Venue)((RepeaterItem)NamingContainer).DataItem);
				return currentVenue;
			}
		}
		Venue currentVenue;

		protected string enc(string instr)
		{
			return HttpUtility.HtmlEncode(instr);
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
