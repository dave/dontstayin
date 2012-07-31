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
using System.Text.RegularExpressions;

namespace Spotted.Admin
{
	public partial class SalesFind : AdminUserControl
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.uiGoToPromoterPageByBrandButton.Click += new EventHandler(uiGoToPromoterPageByBrandButton_Click);
			this.uiGoToPromoterPageByPromoterButton.Click += new EventHandler(uiGoToPromoterPageByPromoterButton_Click);
			this.uiLookupUserButton.Click += new EventHandler(uiLookupUserButton_Click);
			this.uiBrandPromoterIsNullPanel.Visible = false;
			this.uiUserIsNotPromoterPanel.Visible = false;
		}

		void uiLookupUserButton_Click(object sender, EventArgs e)
		{
			//string PromoterRefNumeric = rNumbers.Replace(PromoterRef.Text.Trim(), "");
			//try
			//{

			Usr u = new Usr(int.Parse(this.uiUserAutoComplete.Value));
			if (u.IsPromoter)
			{
				Response.Redirect(u.UrlApp("promoters"));
			}
			else
			{
				this.uiUserIsNotPromoterPanel.Visible = true;
			}
				//else if (u.IsSkeleton)
				//{
				//    NoPromoterAccounts.Visible = false;
				//    NotFoundLabel.Visible = true;
				//}
				//else
				//{
				//    NotFoundLabel.Visible = false;
				//    NoPromoterAccounts.InnerHtml = u.Link() + " is not in any promoter accounts";
				//    NoPromoterAccounts.Visible = true;
				//}
			//}
			//catch
			//{
			//    NoPromoterAccounts.Visible = false;
			//    NotFoundLabel.Visible = true;
			//}
		}

		void uiGoToPromoterPageByPromoterButton_Click(object sender, EventArgs e)
		{
			try
			{
				Promoter p = new Promoter(int.Parse(this.uiPromoterAutoComplete.Value));
				Response.Redirect(p.Url());
			}catch{

			}

		}

		void uiGoToPromoterPageByBrandButton_Click(object sender, EventArgs e)
		{
			try
			{
				Brand b = new Brand(int.Parse(this.uiBrandsAutoComplete.Value));
				if (b.Promoter == null)
				{
					this.uiBrandPromoterIsNullPanel.Visible = true;
				}
				else
				{
					
					Response.Redirect(b.Promoter.Url());
				}
			}
			catch
			{
			}
		}
	 
	}
}
