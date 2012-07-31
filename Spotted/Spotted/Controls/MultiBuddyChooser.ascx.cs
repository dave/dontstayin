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
using Spotted.Master;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Js.Controls.MultiBuddyChooser;
using Spotted.WebServices.Controls.MultiBuddyChooser;
using Bobs.BannerServer;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class MultiBuddyChooser : EnhancedUserControl, IIncludesJs
	{

		public MultiBuddyChooser()
		{
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			JsWebControls.MultiSelector.IncludeJs(page);
			
			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}
		
		protected JsWebControls.MultiSelector uiBuddyMultiSelector;
		public int TabIndexBase { get; set; }

		public int ThreadK
		{
			get { return ViewState["ThreadK"] as int? ?? 0; }
			set { ViewState["ThreadK"] = value; }
		}
		public int RestrictionGroupK
		{
			get { return ViewState["RestrictionGroupK"] as int? ?? 0; }
			set { ViewState["RestrictionGroupK"] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				
				
				DsiPage page = (DsiPage)this.Page;

				var musicTypes = new Dictionary<string, int>() {{"All Music", 0}};
				var musicTypeSet = new MusicTypeSet(new Query(new Q(Bobs.MusicType.Columns.K, page.RelevantMusic.ToArray())));
				foreach (var musicType in musicTypeSet)
				{
					musicTypes[musicType.Name] = musicType.K;
				}
				foreach (var musicType in Identity.Current.FavouriteMusicTypes.ConvertAll(k => new MusicType(k)))
				{
					musicTypes[musicType.Name] = musicType.K;
				}
				this.uiMusicTypes.Items.AddRange(musicTypes.Select(mt => new ListItem(mt.Key, mt.Value.ToString())).ToArray());

				var places = new Dictionary<string, int>() {{"Anywhere", -1}};
				var placeSet = new PlaceSet(new Query(new Q(Bobs.Place.Columns.K, page.RelevantMusic.ToArray())));
				foreach (var place in placeSet)
				{
					places[place.Name] = place.K;
				}
				foreach (var place in Identity.Current.PlacesVisited.ConvertAll(k => new Place(k)))
				{
					places[place.Name] = place.K;
				}
				this.uiPlaces.Items.AddRange(places.Select(mt => new ListItem(mt.Key, mt.Value.ToString())).ToArray());
			}
			//if (Common.Settings.UseCometToServeRequests)
			//{
			//    this.uiBuddyMultiSelector.WebServiceMethod = "";
			//    this.uiBuddyMultiSelector.WebServiceUrl = "";
			//    this.uiBuddyMultiSelector.CometServiceUrl = @"/WebServices/CometAutoComplete/GetBuddiesThenUsrs.ashx";
			//}

			uiBuddyMultiSelector.TextBoxTabIndex = TabIndexBase + 1;
			uiJustBuddiesRadio.TabIndex = (short)(TabIndexBase + 2);
			uiAllMembersRadio.TabIndex = (short)(TabIndexBase + 3);
			uiShowBuddyList.TabIndex = (short)(TabIndexBase + 4);
			uiBuddyList.TabIndex = (short)(TabIndexBase + 5);
			uiShowAddAll.TabIndex = (short)(TabIndexBase + 6);
			uiAddAllButton.Attributes["tabindex"] = (TabIndexBase + 7).ToString();
			uiShowAddBy.TabIndex = (short)(TabIndexBase + 8);
			uiPlaces.TabIndex = (short)(TabIndexBase + 9);
			uiMusicTypes.TabIndex = (short)(TabIndexBase + 10);
			uiAddByMusicAndPlace.Attributes["tabindex"] = (TabIndexBase + 11).ToString();
			uiShowAllTownsAndMusic.TabIndex = (short)(TabIndexBase + 12);


		}
		public IEnumerable<int> SelectedUsrKs
		{
			get
			{
				Service service = new Service();

				return from kvp in service.ResolveUsrsFromMultiBuddyChooserValues(uiBuddyMultiSelector.Selections.Select(kvp => kvp.Value))
					   select (int) kvp.Value;
			}
		}
		#region RestrictionGroupQ
		public Q RestrictionGroupQ
		{
			get
			{
				if (!restrictionGroupQDone)
				{
					if (RestrictionGroupK > 0)
					{
						ArrayList al = new ArrayList();
						al.Add(new Q(GroupUsr.Columns.Status, QueryOperator.IsNull, null));
						if (!RestrictionGroup.Restriction.Equals(Bobs.Group.RestrictionEnum.Moderator) || Usr.Current.CanGroupMemberAdmin(RestrictionGroupUsr))
						{
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Request));
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Recommend));
						}
						if (Usr.Current.CanGroupMemberAdmin(RestrictionGroupUsr))
						{
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.RequestRejected));
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Barred));
							al.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.RecommendRejected));
						}
						restrictionGroupQ = new Or((Q[])al.ToArray(typeof(Q)));
					}
					else
					{
						restrictionGroupQ = new Q(true);
					}
					restrictionGroupQDone = true;
				}
				return restrictionGroupQ;
			}
			set
			{
				restrictionGroupQ = value;
			}
		}
		private bool restrictionGroupQDone = false;
		private Q restrictionGroupQ;
		#endregion
		#region RestrictionGroup
		public Bobs.Group RestrictionGroup
		{
			get
			{
				if (restrictionGroup == null && RestrictionGroupK > 0)
					restrictionGroup = new Bobs.Group(RestrictionGroupK);

				return restrictionGroup;
			}
			set
			{
				restrictionGroup = value;
			}
		}
		private Bobs.Group restrictionGroup;
		#endregion
		#region RestrictionGroupUsr
		public GroupUsr RestrictionGroupUsr
		{
			get
			{
				if (!restrictionGroupUsrDone && RestrictionGroupK > 0)
				{
					restrictionGroupUsr = RestrictionGroup.GetGroupUsr(Usr.Current);
					restrictionGroupUsrDone = true;
				}
				return restrictionGroupUsr;
			}
			set
			{
				restrictionGroupUsr = value;
			}
		}
		private bool restrictionGroupUsrDone = false;
		private GroupUsr restrictionGroupUsr;
		#endregion

		internal void Clear()
		{
			this.uiBuddyMultiSelector.Clear();
		}
	}
}
