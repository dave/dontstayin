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
			Combo.ServerStateSecretString = "fgdfjshgofdshdf$£%%$FBskjhgjklsd";

			if (UpLevel && !ForceDownLevel)
			{
				Combo.ClientOnSelectFunction = "DbComboMultiUsrOnSelect_" + this.ClientID;
				Combo.TextBoxStyle = "border:solid 1px #999999;width:" + (((int)Width) - 14).ToString() + "px;";
				AddButton.Attributes["onclick"] = "DbComboMultiUsrAddItem('" + this.ClientID + "', '" + this.UniqueID + "')";
				RemoveButton.Attributes["onclick"] = "DbComboMultiUsrRemoveItem('" + this.ClientID + "', '" + this.UniqueID + "')";
				SelectBox.Attributes["onchange"] = "DbComboMultiUsrSelectChange('" + this.ClientID + "', '" + this.UniqueID + "')";
			}
			else
			{
				AddButton.ServerClick += new EventHandler(DownLevelAdd);
				RemoveButton.ServerClick += new EventHandler(DownLevelRemove);
			}
			this.DataBind();
		}

		public void DownLevelAdd(object o, System.EventArgs e)
		{
			bool found = false;
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i].Value.Equals(Combo.Value))
				{
					Items[i].Text = Combo.Text;
					found = true;
				}
			}
			if (!found)
			{
				Items.Add(new ListItem(Combo.Text, Combo.Value));
				ItemsPlain.Add(new ListItem(Combo.Text, Combo.Value + "$0"));
			}
		}
		public void DownLevelRemove(object o, System.EventArgs e)
		{
			for (int i = 0; i < ItemsPlain.Count; i++)
			{
				if (ItemsPlain[i].Value.Equals(SelectBox.Items[SelectBox.SelectedIndex].Value))
				{
					Items.RemoveAt(i);
					ItemsPlain.RemoveAt(i);
				}
			}
		}

		bool UpLevel
		{
			get
			{
				return (HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1 &&
					HttpContext.Current.Request.Browser.MajorVersion >= 5 &&
					HttpContext.Current.Request.Browser.Platform.StartsWith("Win"));
			}
		}

		public void Page_PreRender(object o, System.EventArgs e)
		{
			if (UpLevel && !ForceDownLevel)
			{
				ScriptManager.RegisterStartupScript(this, typeof(Page), this.UniqueID + "_StartUp", "DbComboMultiUsrInit('" + this.ClientID + "', '" + this.UniqueID + "');", true);
				ScriptManager.RegisterClientScriptInclude(this, typeof(Page), "DbComboMultiUsr", "/misc/MultiUsr.js?a=5");
			}
		}

		#region DbComboOnLoad
		public void DbComboOnLoad(object o, EventArgs e)
		{
			Cambro.Web.DbCombo.DbCombo d = (Cambro.Web.DbCombo.DbCombo)o;
			d.RegistrationKey = Vars.CambroDbComboRegistrationKey;
			d.TextBoxColumns = 40;
			d.ServerDir = "/Support/";
		}
		#endregion

		#region IPostBackDataHandler Members

		public void RaisePostDataChangedEvent()
		{
			// TODO:  Add ucDbComboMulti.RaisePostDataChangedEvent implementation
		}

		#endregion

		protected override void Render(HtmlTextWriter writer)
		{
			// TODO:  Add ucDbComboMulti.Render implementation
			base.Render(writer);
		}

		protected override void LoadViewState(object savedState)
		{
			Texts.Value = (string)this.ViewState["Texts"];
			Values.Value = (string)this.ViewState["Values"];
			base.LoadViewState(savedState);
		}

		protected override object SaveViewState()
		{
			string texts = "";
			string values = "";
			SelectBox.Items.Clear();
			foreach (ListItem li in ItemsPlain)
			{
				SelectBox.Items.Add(new ListItem(li.Text, li.Value));
				texts += (texts.Length > 0 ? "&" : "") + HttpUtility.UrlEncodeUnicode(li.Text).Replace("+", "%20");
				values += (values.Length > 0 ? "&" : "") + HttpUtility.UrlEncodeUnicode(li.Value).Replace("+", "%20");

			}
			Texts.Value = texts;
			Values.Value = values;

			this.ViewState["Texts"] = Texts.Value;
			this.ViewState["Values"] = Values.Value;
			return base.SaveViewState();
		}
	}
}