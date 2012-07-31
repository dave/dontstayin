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
using System.Reflection;
using Common.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Spotted.Admin
{
	public partial class Settings : AdminUserControl
	{
		Dictionary<string, Control> dataControls = new Dictionary<string, Control>();
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			BuildTable();
		}

		private void BuildTable()
		{
			Table table = new Table();
			table.Rows.Add(GetHeaderRow());
			foreach (MethodInfo mi in TypeExtensions.GetGetters(typeof(Common.Settings)))
			{
				table.Rows.Add(GetSettingTableRow(mi));
			}
			PlaceHolder1.Controls.Add(table);
		}
		
		private TableRow GetSettingTableRow(MethodInfo mi)
		{
			TableRow row = new TableRow();

			string name = mi.Name.Substring(4);

			TableCell cell0 = new TableCell();
			cell0.Controls.Add(new Label() { Text = Bobs.Utilities.CamelCaseToString(name) });
			row.Cells.Add(cell0);

			WebControl control = null;
			Type propertyType = mi.ReturnType;
			if (propertyType.BaseType == typeof(Enum))
			{
				Array names = Enum.GetNames(propertyType);
				Array values = Enum.GetValues(propertyType);

				DropDownList ddl = new DropDownList();
				for (int i = 0; i < names.Length; i++)
				{
					ddl.Items.Add(new ListItem(Bobs.Utilities.CamelCaseToString(names.GetValue(i).ToString()), ((int)values.GetValue(i)).ToString()));
				}
				ddl.SelectedValue = ((int)OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null)).ToString();
				control = ddl;
			}
			else if(propertyType == typeof(int))
			{
				TextBox tb = new TextBox();
				tb.Width = 400;
				tb.Text = ((int)OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null)).ToString();
				control = tb;
			}
			else if (propertyType == typeof(string))
			{
				
				TextBox tb = new TextBox();
				
				if (name.EndsWith("Html"))
				{
					tb.Width = 600;
					tb.Height = 300;
					tb.TextMode = TextBoxMode.MultiLine;
				}
				else
					tb.Width = 400;
				tb.Text = ((string)OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null)).ToString();
				control = tb;
			}
			else if (propertyType == typeof(string[]))
			{
				TextBox tb = new TextBox();
				tb.Width = 400;
				tb.TextMode = TextBoxMode.MultiLine;
				string[] values = ((string[])OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null));
				tb.Text = string.Join('\n'.ToString(), values);
				tb.Rows = Math.Max(6, values.Length);
				control = tb;
			}
			else if (propertyType == typeof(bool))
			{
				RadioButtonList rbl = new RadioButtonList();
				rbl.Items.Add(new ListItem("On", bool.TrueString));
				rbl.Items.Add(new ListItem("Off", bool.FalseString));
				rbl.RepeatDirection = RepeatDirection.Horizontal;
				rbl.SelectedValue = ((bool)OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, null)).ToString();
				control = rbl;
			}


			if (control != null)
			{
				TableCell cell1 = new TableCell();
				cell1.Controls.Add(control);
				row.Cells.Add(cell1);

				control.ID = mi.Name;
				this.dataControls.Add(control.ID, control);
			}
			
			return row;
		}

		private static TableHeaderRow GetHeaderRow()
		{
			TableHeaderRow headerRow = new TableHeaderRow();
			headerRow.Cells.Add(new TableCell() { Text = "Setting" });
			headerRow.Cells.Add(new TableCell() { Text = "Value" });
			headerRow.Font.Bold = true;
			return headerRow;
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			SaveSettings();
			SendRefreshAllToAllServers();
		}



		#region Save Settings logic
		private void SaveSettings()
		{
			foreach (MethodInfo mi in TypeExtensions.GetSetters(typeof(Common.Settings)))
			{
				string getMethodName = mi.Name.Replace("set_", "get_");
				object value = null;
				if (dataControls[getMethodName] is DropDownList)
				{
					value = Enum.Parse(typeof(Common.Settings).GetMethod(getMethodName).ReturnType, ((DropDownList)dataControls[getMethodName]).SelectedValue);
				}
				else if (dataControls[getMethodName] is TextBox)
				{
					
					TextBox tb = (TextBox)dataControls[getMethodName];
					if (tb.TextMode == TextBoxMode.SingleLine || mi.Name.EndsWith("Html"))
					{
						value = tb.Text;
					}
					else if (tb.TextMode == TextBoxMode.MultiLine)
					{
						value = tb.Text.Split('\n');
					}
					if (typeof(Common.Settings).GetMethod(getMethodName).ReturnType.FullName == "System.Int32") value = int.Parse(value.ToString());
				}
				else if (dataControls[getMethodName] is RadioButtonList)
				{
					value = (bool.TrueString == ((RadioButtonList)dataControls[getMethodName]).SelectedValue);
				}
				OptimizedMethodCall.BuildOptimizedDelegate(mi).Invoke(null, new object[] { value });
			}
		}
		#endregion

		private void SendRefreshAllToAllServers()
		{
			string[] servers = Common.Properties.FrontEndInternalWebServers;
			StringBuilder successful = new StringBuilder();
			StringBuilder failed = new StringBuilder();

			foreach (string server in servers)
			{
				try
				{
					if (SendWebRequest("http://" + server + "/popup/SettingsRefresh"))
					{
						successful.Append(server + @"\n");
					}
					else
					{
						failed.Append(server + @"\n");
					}
				}
				catch
				{
					failed.Append(server + @"\n");
				}
			}

			string refreshResults = "";
			string strSuccessful = successful.ToString();
			if (strSuccessful.Length > 0)
			{
				refreshResults += @"Successfully refreshed the following servers:\n" + strSuccessful;
			}
			string strFailed = failed.ToString();
			if (strFailed.Length > 0)
			{
				refreshResults += @"\nFailed to refresh the following servers:\n" + strFailed;
			}

			if (refreshResults.Length > 0)
			{
				Response.Write("<script>alert('" + refreshResults + "');</script>");
			}
			else
			{
				// only refreshed this server and no remote ones. successful
				Response.Write("<script>alert('Refresh successful.');</script>");
			}
		}

		private bool SendWebRequest(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Timeout = 5000;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			return (new StreamReader(response.GetResponseStream()).ReadToEnd() == Spotted.Blank.SettingsRefresh.Success);
		}

	}
}
