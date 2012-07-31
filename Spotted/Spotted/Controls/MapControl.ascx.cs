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

namespace Spotted.Controls
{
	[ClientScript]
	public partial class MapControl : EnhancedUserControl
	{

		public MapControl()
		{
		}

		public const string GOOGLE_KEY_DEV = "ABQIAAAArtnzOH7TK00m4RFuKeW8GBT2yXp_ZAY8_ufC3CFXhHIE1NvwkxQahvgsKrJsoQvYcINHJ2Thm1Zceg";
		public const string GOOGLE_KEY_HOTH = "ABQIAAAArtnzOH7TK00m4RFuKeW8GBQk_2nu3N8f_nxYUkKZKjxhoVA_aBR1lCyR5WDHPapFYDRwSShI4We18A";
		public const string GOOGLE_KEY_TEST = "ABQIAAAArtnzOH7TK00m4RFuKeW8GBTQIH8oyMK1HfsI8DTq5Q104LLKRhS4DeKx-XgqZCIHuUkZ4bUVJkXuXA";
		public const string GOOGLE_KEY_LIVE = "ABQIAAAAhCFaB2WIeHNTtMzQbHOhSRSFN74nUGzixgqmawSLETOEvB7cSxQWmMV5nQgzTWd1Rvo20UGXYEKmFw";

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
		public double North
		{
			get { return double.Parse(this.uiNorth.Value); }
			set { this.uiNorth.Value = value.ToString(); }
		}
		public double South
		{
			get { return double.Parse(this.uiSouth.Value); }
			set { this.uiSouth.Value = value.ToString(); }
		}
		public double East
		{
			get { return double.Parse(this.uiEast.Value); }
			set { this.uiEast.Value = value.ToString(); }
		}
		public double West
		{
			get { return double.Parse(this.uiWest.Value); }
			set { this.uiWest.Value = value.ToString(); }
		}
		

	}
}
