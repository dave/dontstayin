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

namespace Spotted.Pages.Venues
{
	public partial class GoogleMap : DsiUserControl
	{
		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentVenue != null)
				CurrentVenue.AddRelevant(ContainerPage);

		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}


		public const string GOOGLE_KEY_DEV = "ABQIAAAArtnzOH7TK00m4RFuKeW8GBT2yXp_ZAY8_ufC3CFXhHIE1NvwkxQahvgsKrJsoQvYcINHJ2Thm1Zceg";
		public const string GOOGLE_KEY_HOTH = "ABQIAAAArtnzOH7TK00m4RFuKeW8GBQk_2nu3N8f_nxYUkKZKjxhoVA_aBR1lCyR5WDHPapFYDRwSShI4We18A";
		public const string GOOGLE_KEY_TEST = "ABQIAAAArtnzOH7TK00m4RFuKeW8GBTQIH8oyMK1HfsI8DTq5Q104LLKRhS4DeKx-XgqZCIHuUkZ4bUVJkXuXA";
		public const string GOOGLE_KEY_LIVE = "ABQIAAAArtnzOH7TK00m4RFuKeW8GBSFN74nUGzixgqmawSLETOEvB7cSxRIewbFpTTq-aLn7DJfm2crUUhaoA";

		public string GoogleKey
		{
			get
			{
				if (Vars.DevEnv)
				{
					return GOOGLE_KEY_DEV;
				}
				else
				{
					return GOOGLE_KEY_LIVE;
				}
			}
		}

		#region CurrentVenue
		public Venue CurrentVenue
		{
			get
			{
				return ContainerPage.Url.ObjectFilterVenue;
			}
		}
		#endregion
	}
}
