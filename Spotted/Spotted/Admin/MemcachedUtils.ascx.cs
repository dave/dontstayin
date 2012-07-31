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
using Caching;

namespace Spotted.Admin
{
	public partial class MemcachedUtils : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		#region Delete
		protected void Delete(object sender, EventArgs eventArgs)
		{
			try
			{
				Instances.Main.Delete(DeleteKey.Text);
				DeleteKeyResponse.Text = true.ToString();
			}
			catch
			{
				DeleteKeyResponse.Text = false.ToString();
			}
		}
		#endregion

		#region SetCounter
		protected void SetCounter(object sender, EventArgs eventArgs)
		{
			Instances.MainCounterStore.SetCounter(SetCounterKey.Text, uint.Parse(SetCounterValue.Text));
			SetCounterKeyResponse.Text = "done";
		}
		#endregion

		#region Set
		protected void Set(object sender, EventArgs eventArgs)
		{
			//Bobs.ColumnData<object> ob = (Bobs.ColumnData<object>)Static.Cache.Get(GetKey.Text);
			//foreach (object o in ob)
			//{
			//    GetPara.Controls.Add(new LiteralControl(Cambro.Web.Helpers.Strip(o.ToString())));
			//    GetPara.Controls.Add(new LiteralControl("<br>"));
			//}
			try
			{
				Instances.Main.Store(SetKey.Text, SetValue.Text);
				SetResponse.Text = true.ToString();
			}
			catch
			{
				SetResponse.Text = false.ToString();
			}

		}
		#endregion

		#region Get
		protected void Get(object sender, EventArgs eventArgs)
		{
			//Bobs.ColumnData<object> ob = (Bobs.ColumnData<object>)Static.Cache.Get(GetKey.Text);
			//foreach (object o in ob)
			//{
			//    GetPara.Controls.Add(new LiteralControl(Cambro.Web.Helpers.Strip(o.ToString())));
			//    GetPara.Controls.Add(new LiteralControl("<br>"));
			//}

			object o = Instances.Main.Get(GetKey.Text);
			if (o==null)
				GetPara.Controls.Add(new LiteralControl("[null]"));
			else if (o is Bobs.ColumnData<object>)
			{
				foreach (object o1 in (Bobs.ColumnData<object>)o)
				{
					GetPara.Controls.Add(new LiteralControl(Cambro.Web.Helpers.Strip(o1.ToString())));
					GetPara.Controls.Add(new LiteralControl("<br>"));
				}
			}
			else
				GetPara.Controls.Add(new LiteralControl(o.ToString()));

		}
		#endregion

		#region GetCounter
		protected void GetCounter(object sender, EventArgs eventArgs)
		{
			string key = GetCounterKey.Text.Trim();
			GetCounterKeyLabel.Text = "Value for \"" + key + "\" = ";
			GetCounterValue.Text = Instances.MainCounterStore.GetCounter(key, () => 0).ToString();
		}
		#endregion
	}
}
