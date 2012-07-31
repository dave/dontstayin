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
using System.Text;

namespace Spotted.Blank
{
	public partial class MailingLabelsIbizaRocks : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int BagMaxWeight = 10000;
			System.Collections.Generic.Dictionary<string, int> Weight = new System.Collections.Generic.Dictionary<string, int>();
			Weight["A"] = 100;

			int ItemsA = 0;

			Usr.KickUserIfNotAdmin("Only admin!");
			Query q = new Query();
			q.QueryCondition = new And(new Q(Usr.Columns.AddressCountryK, 224), new Q(Usr.Columns.IsSpotter, true));
			//q.TableElement = new Join(Usr.Columns.AddressCountryK, Country.Columns.K);
			q.OrderBy = new OrderBy("(select count(*) from Photo where EnabledDateTime > dateadd(month, -1, getdate()) and Photo.UsrK = Usr.k) desc");
			q.TopRecords = 1000;
			UsrSet us = new UsrSet(q);

			int totalLabels = us.Count;

			StringBuilder s = new StringBuilder();
			Usr u;
			s.Append("<body topmargin=\"0\" bottommargin=\"0\" leftmargin=\"0\" rightmargin=\"0\">");
			for (int count = 0; count < totalLabels || (count % 8) != 0; count++)
			{
				if (count < us.Count)
					u = us[count];
				else
				{
					u = null;
					//isBlank = count < us.Count;
				}


				if (count % 2 == 0 && count>0)
				{
					s.Append("</tr>");
				}

				if (count % 8 == 0)
				{
					if (count > 0)
						s.Append("</table></div>\n");

					s.Append("<div style=\"page-break-after:always;\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\">");
				}

				if (count % 2 == 0)
				{
					s.Append("\n<tr>");
				}

				string padding = "";
				if (count % 2 != 0)
				{
					padding = " style=\"padding-left:10px;\"";
				}


				if (false)
				{
					s.Append("<td width=\"50%\" height=\"25%\"" + padding + ">");
					s.Append("<table width=\"95%\" height=\"90%\" style=\"margin:10px;\">");
					s.Append("<tr>");
					s.Append("<td rowspan=\"2\" valign=\"bottom\">");
					s.Append("&nbsp;");
					s.Append("</td>");
					s.Append("<td align=\"right\" valign=\"top\" colspan=\"2\" style=\"padding-top:10px;\">");
					s.Append("<img src=\"/gfx/mail-1st-class.gif\" width=\"135\"/>");
					s.Append("</td>");
					s.Append("</tr>");
					s.Append("<tr>");
					s.Append("<td align=\"center\" valign=\"bottom\" width=\"75\">");
					s.Append("<img src=\"/gfx/mail-format-envelope-a5.gif\" width=\"75\"/>");
					s.Append("</td>");
					s.Append("<td align=\"center\" valign=\"bottom\" width=\"75\">");
					s.Append("<img src=\"/gfx/mail-symbol-f.gif\" width=\"75\"/>");
					s.Append("</td>");
					s.Append("</tr>");
					s.Append("</table>");
					s.Append("</td>");
				}
				else if (u == null)
				{
					s.Append("<td width=\"50%\" height=\"25%\"" + padding + "></td>");
				}
				else
				{
					s.Append("<td width=\"50%\" height=\"25%\"" + padding + ">");
					s.Append("<table width=\"95%\" height=\"90%\" style=\"margin:10px;\">");
					s.Append("<tr>");
					s.Append("<td rowspan=\"2\" valign=\"bottom\">");
					#region Address
					s.Append(u.FirstName);
					s.Append(" ");
					s.Append(u.LastName);
					s.Append("<br>");
					s.Append(u.AddressStreet);
					s.Append("<br>");
					if (u.AddressArea.Length > 0)
					{
						s.Append(u.AddressArea);
						s.Append("<br>");
					}
					s.Append(u.AddressTown.ToUpper());
					s.Append("<br>");
					s.Append(u.AddressCounty.ToUpper());
					s.Append("<br>");
					s.Append(u.AddressPostcode.ToUpper());
					s.Append("<br>");
					s.Append(u.AddressCountry.Name.ToUpper());
					s.Append("<br>");
					#endregion
					s.Append("</td>");
					s.Append("<td align=\"right\" valign=\"top\" colspan=\"2\" style=\"padding-top:10px;\">");
					#region Stamp PPI
					//1st class stamp
					s.Append("<img src=\"/gfx/mail-1st-class.gif\" width=\"135\"/>");
					#endregion
					s.Append("</td>");
					s.Append("</tr>");
					s.Append("<tr>");
					s.Append("<td align=\"center\" valign=\"bottom\" width=\"75\">");
					#region Packaging format symbol
					s.Append("<img src=\"/gfx/mail-format-envelope-a4.gif\" width=\"75\"/><br>100g");
					#endregion
					s.Append("</td>");
					s.Append("<td align=\"center\" valign=\"bottom\" width=\"75\">");
					#region Mail seperation symbol
					s.Append("<img src=\"/gfx/mail-symbol-a.gif\" width=\"75\"/>");
					s.Append("<br>&nbsp;</td>");
					s.Append("</tr>");
					s.Append("</table>");
					s.Append("</td>");
					u.Update();
					#endregion
				}
			}
			s.Append("</tr></table></div>");
			
			int BagsA = (int)Math.Ceiling(ItemsA * (double)Weight["A"] / (double)BagMaxWeight);
			
			s.Append("<div style=\"font-size:30px\">");
			s.Append("<p style=\"margin-bottom:60px;\"><img src=\"/gfx/mail-symbol-a.gif\" width=\"75\" align=\"middle\"/><img src=\"/gfx/mail-format-envelope-a5.gif\" width=\"70\" align=\"middle\" style=\"margin-right:30px;\"/> " + ItemsA.ToString() + " item" + (ItemsA == 1 ? "" : "s") + " = " + BagsA.ToString() + " bag" + (BagsA == 1 ? "" : "s") + "</p>");
			s.Append("</div>");
			s.Append("</body>");
			
			Response.Clear();
			Response.Write(s.ToString());
			Response.Flush();
			Response.End();
		}
	}
}
