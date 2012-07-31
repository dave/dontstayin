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

namespace Spotted.Controls
{
	public partial class MusicTypeDropDownList2 : System.Web.UI.UserControl
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			if (this.uiList.Items.Count == 0)
			{
				// note: these aren't spaces beginning indented items, since whitespace doesn't display correctly. it is Alt+255
				this.uiList.Items.Add(new ListItem("All Music", "1"));
				this.uiList.Items.Add(new ListItem("Commercial", "42"));
				this.uiList.Items.Add(new ListItem("  Commercial Pop", "2"));
				this.uiList.Items.Add(new ListItem("  Commercial Dance", "3"));
				this.uiList.Items.Add(new ListItem("  Club Classics", "43"));
				this.uiList.Items.Add(new ListItem("House", "4"));
				this.uiList.Items.Add(new ListItem("  Funky House", "9"));
				this.uiList.Items.Add(new ListItem("  Jackin House", "56"));
				this.uiList.Items.Add(new ListItem("  Electro House", "55"));
				this.uiList.Items.Add(new ListItem("  Dirty House", "57"));
				this.uiList.Items.Add(new ListItem("  Fidgit House", "74"));
				this.uiList.Items.Add(new ListItem("  Latin House", "58"));
				this.uiList.Items.Add(new ListItem("  Soulful House", "41"));
				this.uiList.Items.Add(new ListItem("  Deep House", "7"));
				this.uiList.Items.Add(new ListItem("  US House", "5"));
				this.uiList.Items.Add(new ListItem("  US Garage", "34"));
				this.uiList.Items.Add(new ListItem("  Progressive House", "6"));
				this.uiList.Items.Add(new ListItem("  Tech House", "8"));
				this.uiList.Items.Add(new ListItem("  Tribal House", "40"));
				this.uiList.Items.Add(new ListItem("  Old Skool House", "44"));
				this.uiList.Items.Add(new ListItem("  Acid House", "54"));
				this.uiList.Items.Add(new ListItem("  Bassline House", "73"));
				this.uiList.Items.Add(new ListItem("Hard Dance", "10"));
				this.uiList.Items.Add(new ListItem("  Hard House", "11"));
				this.uiList.Items.Add(new ListItem("  Hardstyle", "59"));
				this.uiList.Items.Add(new ListItem("  Hard Trance", "60"));
				this.uiList.Items.Add(new ListItem("  Trance", "12"));
				this.uiList.Items.Add(new ListItem("  Psy-Trance", "13"));
				this.uiList.Items.Add(new ListItem("  Hardcore", "14"));
				this.uiList.Items.Add(new ListItem("  Old Skool Hardcore", "45"));
				this.uiList.Items.Add(new ListItem("Alternative Dance", "15"));
				this.uiList.Items.Add(new ListItem("  Electro", "16"));
				this.uiList.Items.Add(new ListItem("  Big Beat", "17"));
				this.uiList.Items.Add(new ListItem("  Breaks", "18"));
				this.uiList.Items.Add(new ListItem("Techno", "20"));
				this.uiList.Items.Add(new ListItem("  Minimal Techno", "61"));
				this.uiList.Items.Add(new ListItem("  Detroit Techno", "21"));
				this.uiList.Items.Add(new ListItem("  Funky Techno", "62"));
				this.uiList.Items.Add(new ListItem("  Acid Techno", "22"));
				this.uiList.Items.Add(new ListItem("  Electro Techno", "23"));
				this.uiList.Items.Add(new ListItem("Drum and Bass", "24"));
				this.uiList.Items.Add(new ListItem("  Liquid Drum and Bass", "70"));
				this.uiList.Items.Add(new ListItem("  Jazzy Drum and Bass", "25"));
				this.uiList.Items.Add(new ListItem("  Jump Up Drum and Bass", "26"));
				this.uiList.Items.Add(new ListItem("  Jungle", "27"));
				this.uiList.Items.Add(new ListItem("Urban", "28"));
				this.uiList.Items.Add(new ListItem("  Hip Hop", "29"));
				this.uiList.Items.Add(new ListItem("  R&B", "30"));
				this.uiList.Items.Add(new ListItem("  Dancehall / Bashment", "31"));
				this.uiList.Items.Add(new ListItem("  Reggae", "32"));
				this.uiList.Items.Add(new ListItem("  UK Garage", "33"));
				this.uiList.Items.Add(new ListItem("  Dubstep", "71"));
				this.uiList.Items.Add(new ListItem("  Reggaeton", "72"));
				this.uiList.Items.Add(new ListItem("Alternative Electronic", "65"));
				this.uiList.Items.Add(new ListItem("  Industrial", "66"));
				this.uiList.Items.Add(new ListItem("  Electronic Body Music", "67"));
				this.uiList.Items.Add(new ListItem("  Futurepop", "68"));
				this.uiList.Items.Add(new ListItem("  Powernoise", "69"));
				this.uiList.Items.Add(new ListItem("Retro", "46"));
				this.uiList.Items.Add(new ListItem("  Funk", "47"));
				this.uiList.Items.Add(new ListItem("  Disco", "48"));
				this.uiList.Items.Add(new ListItem("  Jazz-Funk", "49"));
				this.uiList.Items.Add(new ListItem("  Soul", "50"));
				this.uiList.Items.Add(new ListItem("  Jazz", "51"));
				this.uiList.Items.Add(new ListItem("  Rare Groove", "52"));
				this.uiList.Items.Add(new ListItem("  Chillout / Leftfield", "35"));
				this.uiList.Items.Add(new ListItem("Rock", "36"));
				this.uiList.Items.Add(new ListItem("  Indie", "37"));
				this.uiList.Items.Add(new ListItem("  Rock", "38"));
				this.uiList.Items.Add(new ListItem("  Metal", "39"));
				this.uiList.Items.Add(new ListItem("  Punk", "63"));
				this.uiList.Items.Add(new ListItem("  Acoustic", "64"));
			}

			this.uiList.AutoPostBack = this.AutoPostBack;
			this.uiList.SelectedIndexChanged += new EventHandler(uiList_SelectedIndexChanged);
		}

		public event EventHandler SelectedIndexChanged;
		void uiList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.SelectedIndexChanged != null) this.SelectedIndexChanged(sender, e);
		}

		public bool AutoPostBack { get; set; }
		public int SelectedValue
		{
			get { return int.Parse(this.uiList.SelectedValue); }
			set { this.uiList.SelectedValue = value.ToString(); }
		}
		public bool Enabled
		{
			get { return this.uiList.Enabled; }
			set { this.uiList.Enabled = value; }
		}
	}
}
