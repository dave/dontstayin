using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Controls.DonateText
{
	public partial class DonateTextControl : DsiUserControl
	{
		private DonationIcon donationIcon;
		public DonationIcon DonationIcon
		{
			get
			{
				if (donationIcon == null)
				{
					if (Url["K"].Exists && Url["K"].IsInt)
					{
						try
						{
							donationIcon = new DonationIcon(Url["K"].ValueInt);
						}
						catch (BobNotFound)
						{
						}
					}

					if (donationIcon == null ||
						((donationIcon.Enabled == false || donationIcon.StartDateTime > Common.Time.Now) && (Usr.Current == null || !Usr.Current.IsAdmin)))
					{
						donationIcon = DonationIcon.GetActiveDonationIcon();
					}
				}

				return donationIcon;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			switch (DonationIcon.DonatePageControl)
			{
				case DonationIcon.Control.Basic: uiBasic.Visible = true; break;
				case DonationIcon.Control.Default: uiDefault.Visible = true; break;
				case DonationIcon.Control.Monkey: uiMonkey.Visible = true; break;
				default:
					throw new NotImplementedException("Unknown control " + DonationIcon.DonatePageControl.ToString());
			}
		}
	}
}
