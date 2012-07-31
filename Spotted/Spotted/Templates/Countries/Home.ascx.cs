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

namespace Spotted.Templates.Countries
{
	public partial class Home : System.Web.UI.UserControl
	{
		protected HtmlGenericControl EventsSpan;

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Country.Columns.TotalEvents,
					Country.Columns.Code2Letter,
					Country.LinkColumns
					);
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			//EventsSpan.Visible = CurrentCountry.TotalEvents > 0;
		}
		protected Country CurrentCountry
		{
			get
			{
				if (currentCountry == null)
					currentCountry = ((Country)((DataListItem)NamingContainer).DataItem);
				return currentCountry;
			}
		}
		Country currentCountry;


	}
}
