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

namespace Spotted.Controls
{
	public partial class MultiUsr : System.Web.UI.UserControl, IPostBackDataHandler
	{
		public string ServerMethod, ServerType, ServerAssembly;
		public Cambro.Web.DbCombo.DbCombo Combo;
		public HtmlSelect SelectBox;
		public HtmlButton AddButton, RemoveButton;
		protected HtmlInputHidden Texts, Values;
		public int BuddiesUsrK;

		public void AddUsr(Usr u)
		{
			if (u != null)
			{
				Items.Add(new ListItem(u.NickName, u.K.ToString()));
				ItemsPlain.Add(new ListItem(u.NickName, u.K + "$" + u.Pic.ToString().ToUpper()));
			}
		}

		public bool ShowDropDown = false;

		#region Items
		public ListItemCollection Items
		{
			get
			{
				if (items == null)
					items = new ListItemCollection();
				return items;
			}
			set
			{
				items = value;
			}
		}
		private ListItemCollection items;
		public ListItemCollection ItemsPlain
		{
			get
			{
				if (itemsPlain == null)
					itemsPlain = new ListItemCollection();
				return itemsPlain;
			}
			set
			{
				itemsPlain = value;
			}
		}
		private ListItemCollection itemsPlain;
		#endregion

		#region SelectBoxRows
		public int SelectBoxRows
		{
			get
			{
				return selectBoxRows;
			}
			set
			{
				selectBoxRows = value;
			}
		}
		private int selectBoxRows = 10;
		#endregion

		#region DropDownRows
		public int DropDownRows
		{
			get
			{
				return dropDownRows;
			}
			set
			{
				dropDownRows = value;
			}
		}
		private int dropDownRows = 0;
		#endregion

		bool ForceDownLevel = false;

		#region Width
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}
		private int width = 200;
		#endregion

		#region LoadPostData
		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			string texts = Texts.Value;
			string values = Values.Value;
			string[] textsAry = texts.Split('&');
			string[] valuesAry = values.Split('&');
			for (int i = 0; i < textsAry.Length; i++)
			{
				if (valuesAry[i].Length > 0)
				{
					ItemsPlain.Add(
						new ListItem(
							HttpUtility.UrlDecode(textsAry[i]),
							HttpUtility.UrlDecode(valuesAry[i])
						)
					);
					Items.Add(
						new ListItem(
							HttpUtility.UrlDecode(textsAry[i]),
							HttpUtility.UrlDecode(valuesAry[i]).Split('$')[0]
						)
					);
				}
			}
			return false;
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			Combo.ForceDownLevel = ForceDownLevel;
			Page.RegisterRequiresPostBack(this);
			if (ServerMethod != null && ServerMethod.Length > 0)
				Combo.ServerMethod = ServerMethod;
			if (ServerType != null && ServerType.Length > 0)
				Combo.ServerType = ServerType;
			if (ServerAssembly != null && ServerAssembly.Length > 0)
				Combo.ServerAssembly = ServerAssembly;
			if (DropDownRows > 0)
				Combo.DropDownRows = DropDownRows;
			SelectBox.Size = SelectBoxRows;
			SelectBox.Style["width"] = Width.ToString() + "px";

			Hashtable hash = new Hashtable();
			hash.Add("BuddiesUsrK", BuddiesUsrK.ToString());
			Combo.ServerState = hash;
