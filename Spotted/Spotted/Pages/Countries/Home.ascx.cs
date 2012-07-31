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

namespace Spotted.Pages.Countries
{
	public partial class Home : DsiUserControl
	{
		protected PlaceHolder ContentPlaceHolder;
		protected Controls.Latest Latest;

		#region CountryK
		public int CountryK
		{
			get
			{
				if (ContainerPage.Url.HasCountryObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["K"];
			}
		}
		#endregion
		#region CurrentCountry
		public Country CurrentCountry
		{
			get
			{
				if (currentCountry == null)
				{
					if (ContainerPage.Url.HasCountryObjectFilter)
						currentCountry = ContainerPage.Url.ObjectFilterCountry;
					else if (CountryK > 0)
						currentCountry = new Country(CountryK);
				}
				return currentCountry;
			}
			set
			{
				currentCountry = value;
			}
		}
		private Country currentCountry;
		#endregion

		private void Page_Init(object sender, System.EventArgs e)
		{
			try
			{
				if (CountryK == 225)
				{
					Banner b = new Banner(9110);
					b.RegisterHit();
				}
			}
			catch { }

			if (Request.QueryString["ChangeHomeCountryK"] != null)
			{
				int newFilterK = int.Parse(Request.QueryString["ChangeHomeCountryK"]);
				Country.FilterK = newFilterK;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			Latest.Parent = CurrentCountry;
			ContainerPage.SetPageTitle(CurrentCountry.FriendlyName + " home");
			if (Usr.Current != null && Usr.Current.IsAdmin)
				ContainerPage.Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/country?ID=" + CurrentCountry.K.ToString() + "\">Edit this country</a></p>"));
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
