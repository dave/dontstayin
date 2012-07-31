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

namespace Spotted.Controls.PagedData.Templates.Events
{
	public partial class Header : EnhancedUserControl
	{
		protected HtmlInputRadioButton uiShowPast;
		protected HtmlInputRadioButton uiShowFuture;
		protected DropDownList uiMusicType;
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			var panel = new Panel();
			panel.HorizontalAlign = HorizontalAlign.Center;
			panel.Controls.Add(new Label() { Text = "Past"});
			this.uiShowPast = new HtmlInputRadioButton()
			{
				ID = "uiShowPast",
				Name = "EventPeriod"
			};
			panel.Controls.Add(this.uiShowPast);
			panel.Controls.Add(new Label() { Text = "Future" });
			this.uiShowFuture = new HtmlInputRadioButton()
			{
				ID = "uiShowFuture",
				Name = "EventPeriod",
				Checked = true
			};
			panel.Controls.Add(this.uiShowFuture);
			panel.Controls.Add(new Label() { Width = new Unit(20, UnitType.Pixel) });
			panel.Controls.Add(new Label() { Text = "Music type " });
			this.uiMusicType = new DropDownList();
			this.uiMusicType.ID = "uiMusicType";
			uiMusicType.Items.AddRange(Bobs.MusicType.MusicTypes.Select(mt => new ListItem(mt.Key, mt.Value.ToString())).ToArray());
			panel.Controls.Add(uiMusicType);
			this.Controls.Add(panel);
		}

	}
}
