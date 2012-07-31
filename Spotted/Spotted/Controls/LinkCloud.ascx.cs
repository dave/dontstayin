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
using SpottedLibrary.Controls.LinkCloud;
using System.Collections.Generic;
using Bobs;
using SpottedLibrary.Controls.SearchBoxControl;

namespace Spotted.Controls
{
	public partial class LinkCloud : EnhancedUserControl, ILinkCloud
	{
		public bool IsValid
		{
			get { return true; }
		}

		public List<KeyValuePair<ILinkable, int>> Items
		{
			set { 
				this.Panel1.Controls.Clear();
				int maxValue = int.MinValue;
				int minValue = int.MaxValue;
				foreach (var item in value)
				{
					if (item.Value > maxValue) { maxValue = item.Value; }
					if (item.Value < minValue) { minValue = item.Value; }
				}
				foreach(var item in value){
					HyperLink link = new HyperLink();
					link.NavigateUrl = item.Key.Url();
					link.Text = item.Key.ReadableReference;
					link.CssClass = "LinkCloudLink" + GetWeight(minValue, maxValue, item.Value);
					this.Panel1.Controls.Add(link);
					this.Panel1.Controls.Add(new LiteralControl(" "));
				}
			}
		}
		static int GetWeight(int minValue, int maxValue, int value)
		{
			if (maxValue == minValue)
			{
				return 2;
			}
			return ((int)(4 * (Convert.ToDouble(value - minValue) / (maxValue - minValue))));
		}

 
	}
}
