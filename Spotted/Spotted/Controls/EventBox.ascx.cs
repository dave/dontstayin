using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Text;
using Bobs;
using Js.Controls.EventBox;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class EventBox : EnhancedUserControl
	{

		public EventBox()
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			JQuery.Include(Page, "ui.core");
			JQuery.Include(Page, "effects.core");
			JQuery.Include(Page, "effects.drop");
		}

		#region ObjectType
		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return objectType;
			}
			set
			{
				objectType = value;
			}
		}
		Model.Entities.ObjectType objectType = Model.Entities.ObjectType.None;
		#endregion

		#region ObjectK
		public int ObjectK
		{
			get
			{
				return objectK;
			}
			set
			{
				objectK = value;
			}
		}
		int objectK = 0;
		#endregion

		public void Build()
		{
			InitClientID.Value = this.ClientID;
			InitEnableEffects.Value = ((Master.DsiPage)Page).Url["Animate"].Exists ? "true" : "false";

			int musicTypeK = Prefs.Current["MusicPref"].IsNull ? 1 : (int)Prefs.Current["MusicPref"];

			EventPageStub data = new EventPageStub(ObjectType, ObjectK, TabType.Future, musicTypeK, 0, 0, null);

			EventSet es = Event.GetEventSetFromEventBoxKey(EventPageDetails.GetKeyStatic(data));

			EventPageDetails firstPage = new EventPageDetails(
				this.ClientID,
				es,
				data,
				false);

			
			firstPage.Selected = true;
			firstPage.Events[0].Selected = true;

			EventIconsHolder.Controls.Clear();
			EventIconsHolder.Controls.Add(new LiteralControl(firstPage.Html.ToHtml()));

			EventInfoHolderOuter.Controls.Clear();
			EventInfoHolderOuter.Controls.Add(new LiteralControl(firstPage.Events[0].Html.ToHtmlInfo()));

			InitFirstPage.Value = firstPage.Serialize();

		}
	}
}
