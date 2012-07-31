using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Controls
{
	public partial class LatestChatHotTopics : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		public int Items
		{
			get
			{
				return LatestChatUc.Items;
			}
			set
			{
				LatestChatUc.Items = value;
			}
		}

		public IDiscussable Discussable
		{
			get
			{
				return LatestChatUc.Discussable;
			}
			set
			{
				LatestChatUc.Discussable = value;
			}
		}
	}
}
