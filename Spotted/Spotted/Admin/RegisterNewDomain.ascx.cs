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

namespace Spotted.Admin
{
	public partial class RegisterNewDomain : AdminUserControl
	{
		#region Stage control
		enum Stage
		{
			SelectPromoter,
			SetNewDomainDetails,
			TestRedirect
		}
		void SetStage(Stage stage)
		{
			uiNewDomainDetailsPanel.Visible = (stage == Stage.SetNewDomainDetails || stage == Stage.TestRedirect);
			uiTestRedirectPanel.Visible = (stage == Stage.TestRedirect);
		}
		#endregion
		#region Properties - UI
		int? promoterK;
		int PromoterK
		{
			get
			{
				if (!promoterK.HasValue)
				{
					promoterK = int.Parse(uiPromotersAutoComplete.Value);
				}
				return promoterK.Value;
			}
		}
		int DisplayDomainsPromoterK
		{
			set 
			{
				Query q = new Query();
				q.QueryCondition = new Q(Domain.Columns.PromoterK, value);
				DomainSet ds = new DomainSet(q);
				if (ds.Count == 0)
				{
					uiDomainsRegistered.Text = "<ul><li>none so far..</li></ul>";
				}
				else
				{
					uiDomainsRegistered.Text = "<ul>";
					foreach (Domain d in ds)
					{
						uiDomainsRegistered.Text += GetDomainHtml(d);
					}
					uiDomainsRegistered.Text += "</ul>";
				}

			}
		}
		string DomainName
		{
			get { return uiDomainName.Text + ".com"; }
		}
		string BrandRedirectApp
		{
			get { return uiBrandRedirectApp.Text; }
			set { uiBrandRedirectApp.Text = value; }
		}
		string VenueRedirectApp
		{
			get { return uiVenueRedirectApp.Text; }
			set { uiVenueRedirectApp.Text = value; }
		}
		int BrandK
		{
			get { return int.Parse(uiBrandsDdl.SelectedValue); }
		}
		int VenueK
		{
			get { return int.Parse(uiVenuesDdl.SelectedValue); }
		}
		int EventK
		{
			get { return int.Parse(uiEventK.Text); }
		}
		int GroupK
		{
			get { return int.Parse(uiGroupK.Text); }
		}
		string CustomUrl
		{
			get { return "/" + uiCustomUrlText.Text; }
		}
		BrandSet Brands
		{
			set
			{
				uiBrandsDdl.DataSource = value;
				uiBrandsDdl.DataBind();
			}
		}
		VenueSet Venues
		{
			set
			{
				uiVenuesDdl.DataSource = value;
				uiVenuesDdl.DataBind();
			}
		}
		string OnClickRegisterConfirm
		{
			set { uiRegisterDomainButton.Attributes["onclick"] = value; }
		}
		Domain AddedDomain
		{
			set { uiAddedDomain.Text = "<ul>" + GetDomainHtml(value)  + "</ul>"; }
		}
		string GetDomainHtml(Domain d)
		{
			return string.Format("<li>{0} (redirects to <a href=\"{1}\">{1}</a>)</li>", d.DomainName, d.RedirectUrlComplete);
		}
		bool Availability
		{
			set
			{
				uiDomainAvailability.Text = value ? "Domain is available!" : "Domain not available.";
				uiDomainAvailability.ForeColor = value ? System.Drawing.Color.Green : System.Drawing.Color.Red;
			}
		}
		string ErrorMessage
		{
			set { uiErrorLbl.Text = value; }
		}
		#endregion

		#region Actions
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetStage(Stage.SelectPromoter);
				BrandRedirectApp = "tickets";
				VenueRedirectApp = "tickets";

				OnClickRegisterConfirm = "return ConfirmDomain();";

				uiOptionsList.OptionItems.Add(new Spotted.Controls.OptionsList.Option(uiBrandDiv.ClientID, "Brand") { Checked = true });
				uiOptionsList.OptionItems.Add(new Spotted.Controls.OptionsList.Option(uiVenueDiv.ClientID, "Venue"));
				uiOptionsList.OptionItems.Add(new Spotted.Controls.OptionsList.Option(uiEventDiv.ClientID, "Event"));
				uiOptionsList.OptionItems.Add(new Spotted.Controls.OptionsList.Option(uiGroupDiv.ClientID, "Group"));
				uiOptionsList.OptionItems.Add(new Spotted.Controls.OptionsList.Option(uiCustomUrlDiv.ClientID, "Custom Url"));
				uiOptionsList.DataBind();
			}
			
			ErrorMessage = "";
		}

		protected void PromoterSelected(object o, EventArgs e)
		{
			DisplayDomainsPromoterK = PromoterK;
			Brands = new BrandSet(new Query(new Q(Brand.Columns.PromoterK, PromoterK)));
			Venues = new VenueSet(new Query(new Q(Venue.Columns.PromoterK, PromoterK)));
			SetStage(Stage.SetNewDomainDetails);
		}

		protected void TestAvailability(object o, EventArgs e)
		{
			Domain d = new Domain() { DomainName = DomainName };
			Availability = d.IsAvailable;
		}
		
		protected void RegisterDomain(object o, EventArgs e)
		{
			Domain d = new Domain();
			d.DomainName = DomainName;
			if (!d.IsAvailable)
			{
				ErrorMessage = d.DomainName + " already exists!";
				return;
			}

			d.PromoterK = PromoterK;

			if (uiOptionsList.SelectedPanelID == uiBrandDiv.ClientID)
			{
				d.RedirectApp = BrandRedirectApp;
				d.RedirectObjectK = BrandK;
				d.RedirectObjectType = Model.Entities.ObjectType.Brand;
			}
			else if (uiOptionsList.SelectedPanelID == uiVenueDiv.ClientID)
			{
				d.RedirectApp = VenueRedirectApp;
				d.RedirectObjectK = VenueK;
				d.RedirectObjectType = Model.Entities.ObjectType.Venue;
			}
			else if (uiOptionsList.SelectedPanelID == uiEventDiv.ClientID)
			{
				Event ev = new Event(EventK);
				if (!ev.IsPromoter(PromoterK))
					throw new Exception("This event isn't in this promoter account!");

				d.RedirectObjectK = EventK;
				d.RedirectObjectType = Model.Entities.ObjectType.Event;
			}
			else if (uiOptionsList.SelectedPanelID == uiGroupDiv.ClientID)
			{
				Group gr = new Group(GroupK);

				d.RedirectObjectK = GroupK;
				d.RedirectObjectType = Model.Entities.ObjectType.Group;
			}
			else
			{
				d.RedirectUrl = CustomUrl;
			}

			d.Update();

			d.Register();

			AddedDomain = d;
			SetStage(Stage.TestRedirect);
		}
		#endregion
	}
}
