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
using System.Xml;
using Bobs;
using System.Text.RegularExpressions;
using SpottedScript.Controls.ChatClient.Shared;

namespace Spotted.Blank
{
	public partial class OverdueAccounts : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(Usr.Current == null)
				Response.Redirect("http://" + Vars.DomainName);
			else
			{
				this.LoggedInAs.Text = Usr.Current.NickName;
			}
		}

		#region LoggedInPanel
		protected Panel LoggedInPanel;
		protected HtmlAnchor LoggedInLink;
		protected LinkButton LogOutLink;
		public void LoggedInPanel_PreRender(object o, System.EventArgs e)
		{
			

		}
		#region LogOutClick
		public void LogOutClick(object o, System.EventArgs e)
		{
			Usr.SignOut();

			
		}
		static void AddAttribute(XmlNode node, XmlDocument xmlDoc, string key, string val)
		{
			XmlAttribute att = xmlDoc.CreateAttribute(key);
			att.Value = val;
			node.Attributes.Append(att);
		}
		static XmlAttribute ReturnAttribute(XmlNode node, XmlDocument xmlDoc, string key, string val)
		{
			XmlAttribute att = xmlDoc.CreateAttribute(key);
			att.Value = val;
			node.Attributes.Append(att);
			return att;
		}
		#endregion
		#endregion
	}
}
