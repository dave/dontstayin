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
	public partial class MailingLabels : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int BagMaxWeight = 10000;
			System.Collections.Generic.Dictionary<string, int> Weight = new System.Collections.Generic.Dictionary<string, int>();
			Weight["A"] = 100;
			Weight["B"] = 500;
			Weight["C"] = 500;
			Weight["D"] = 500;
			Weight["E"] = 500;
			Weight["F"] = 20;

			int ItemsA = 0, ItemsB = 0, ItemsC = 0, ItemsD = 0, ItemsE = 0, ItemsF = 0;

			Usr.KickUserIfNotAdmin("Only admin!");
			Query q = new Query();
			q.QueryCondition = new Or(
				new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.New),
				new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
				new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.NeedCards),
				new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill)
			);
			q.TableElement = new Join(Usr.Columns.AddressCountryK, Country.Columns.K);
			q.OrderBy = new OrderBy(
				new OrderBy("(CASE [Country].[PostageZone] WHEN 0 THEN 1 WHEN 4 THEN 2 WHEN 3 THEN 3 WHEN 1 THEN 4 WHEN 2 THEN 5 END)"),
				new OrderBy("(CASE [Usr].[CardStatus] WHEN 3 THEN 1 WHEN 5 THEN 2 WHEN 4 THEN 3 WHEN 1 THEN 4 WHEN 6 THEN 5 WHEN 2 THEN 6 WHEN 0 THEN 7 END)"),
				new OrderBy(Usr.Columns.SpottingsTotal, OrderBy.OrderDirection.Descending)
			);
			UsrSet us = new UsrSet(q);

			//Query qMediaPacks = new Query();
			//qMediaPacks.QueryCondition = new Q(Promoter.Columns.LetterStatus, Promoter.LetterStatusEnum.Printing);
			//qMediaPacks.ReturnCountOnly = true;
			//PromoterSet psMediaPacks = new PromoterSet(qMediaPacks);
			Query qPromoterLetters = new Query();
			qPromoterLetters.QueryCondition = new Q(Promoter.Columns.LetterStatus, Promoter.LetterStatusEnum.Printing);
			qPromoterLetters.ReturnCountOnly = true;
			PromoterSet psPromoterLetters = new PromoterSet(qPromoterLetters);
			ItemsF = psPromoterLetters.Count;


			int totalLabels = us.Count + ItemsF;

			StringBuilder s = new StringBuilder();
			Usr u;
			s.Append("<body topmargin=\"0\" bottommargin=\"0\" leftmargin=\"0\" rightmargin=\"0\">");
			for (int count = 0; count < totalLabels || (count % 8) != 0; count++)
			{
				bool isBlank = false;

				if (count < us.Count)
					u = us[count];
				else
				{
					u = null;
					isBlank = count < (us.Count + ItemsF);
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


				if (isBlank)
				{
					s.Append("<td width=\"50%\" height=\"25%\"" + padding + ">");
					s.Append("<table width=\"95%\" height=\"90%\" style=\"margin:10px;\">");
					s.Append("<tr>");
					s.Append("<td rowspan=\"2\" valign=\"bottom\">");
					s.Append("&nbsp;");
					s.Append("</td>");
					s.Append("<td align=\"right\" valign=\"top\" colspan=\"2\" style=\"padding-top:10px;\">");
					s.Append("<img src=\"/gfx/mail-1st-class-dh.gif\" width=\"135\"/>");
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
					//Removed 2008-12-11 - now we only send large packs.
					//if (u.AddressCountryK == 224 && (u.CardStatus.Equals(Usr.CardStatusEnum.New) || u.CardStatus.Equals(Usr.CardStatusEnum.PrintingWelcomePack)))
					//{
					//    //2nd class stamp
					//    s.Append("<img src=\"/gfx/mail-2nd-class-dh.gif\" width=\"135\"/>");
					//}
					//else
					if (u.AddressCountryK == 224)
					{
						//1st class stamp
						//s.Append("<img src=\"/gfx/mail-1st-class-dh.gif\" width=\"135\"/>");

						//2nd class stamp
						s.Append("<img src=\"/gfx/mail-2nd-class-dh.gif\" width=\"135\"/>");
					}
					else
					{
						//1st class stamp with airmail mark
						s.Append("<img src=\"/gfx/mail-1st-class-airmail-dh.gif\" width=\"135\"/>");
					}
					#endregion
					s.Append("</td>");
					s.Append("</tr>");
					s.Append("<tr>");
					s.Append("<td align=\"center\" valign=\"bottom\" width=\"75\">");
					#region Packaging format symbol
					//Removed 2008-12-11 - now we only send large packs.
					//if (u.AddressCountryK == 224 && (u.CardStatus.Equals(Usr.CardStatusEnum.New) || u.CardStatus.Equals(Usr.CardStatusEnum.PrintingWelcomePack)))
					//{
					//	//envelope
					//	s.Append("<img src=\"/gfx/mail-format-envelope-a5.gif\" width=\"75\"/><br>100g");
					//}
					//else
					//{
						//box
						s.Append("<img src=\"/gfx/mail-format-box.gif\" width=\"75\"/><br>500g");
					//}
					#endregion
					s.Append("</td>");
					s.Append("<td align=\"center\" valign=\"bottom\" width=\"75\">");
					#region Mail seperation symbol
					//Removed 2008-12-11 - now we only send large packs.
					//if (u.AddressCountryK == 224 && (u.CardStatus.Equals(Usr.CardStatusEnum.New) || u.CardStatus.Equals(Usr.CardStatusEnum.PrintingWelcomePack)))
					//{
					//	//symbol A
					//	ItemsA++;
					//	s.Append("<img src=\"/gfx/mail-symbol-a.gif\" width=\"75\"/>");
					//}
					//else 
					if (u.AddressCountryK == 224)
					{
						//symbol B
						ItemsB++;
						s.Append("<img src=\"/gfx/mail-symbol-b.gif\" width=\"75\"/>");
					}
					else if (u.AddressCountry.PostageZone.Equals(Country.PostageZones.WesternEurope))
					{
						//symbol C
						ItemsC++;
						s.Append("<img src=\"/gfx/mail-symbol-c.gif\" width=\"75\"/>");
					}
					else if (u.AddressCountry.PostageZone.Equals(Country.PostageZones.RestOfEurope))
					{
						//symbol D
						ItemsD++;
						s.Append("<img src=\"/gfx/mail-symbol-d.gif\" width=\"75\"/>");
					}
					else if (u.AddressCountry.PostageZone.Equals(Country.PostageZones.World1) || u.AddressCountry.PostageZone.Equals(Country.PostageZones.World2))
					{
						//symbol E
						ItemsE++;
						s.Append("<img src=\"/gfx/mail-symbol-e.gif\" width=\"75\"/>");
					}
					#endregion
					s.Append("<br>&nbsp;</td>");
					s.Append("</tr>");
					s.Append("</table>");
					s.Append("</td>");

					if (u.CardStatus.Equals(Usr.CardStatusEnum.New))
						u.CardStatus = Usr.CardStatusEnum.PrintingWelcomePack;
					else if (u.CardStatus.Equals(Usr.CardStatusEnum.NeedCards))
						u.CardStatus = Usr.CardStatusEnum.PrintingRefill;
					u.Update();
				}
			}
			s.Append("</tr></table></div>");
			
			int BagsA = (int)Math.Ceiling(ItemsA * (double)Weight["A"] / (double)BagMaxWeight);
			int BagsB = (int)Math.Ceiling(ItemsB * (double)Weight["B"] / (double)BagMaxWeight);
			int BagsC = (int)Math.Ceiling(ItemsC * (double)Weight["C"] / (double)BagMaxWeight);
			int BagsD = (int)Math.Ceiling(ItemsD * (double)Weight["D"] / (double)BagMaxWeight);
			int BagsE = (int)Math.Ceiling(ItemsE * (double)Weight["E"] / (double)BagMaxWeight);
			int BagsF = (int)Math.Ceiling(ItemsF * (double)Weight["F"] / (double)BagMaxWeight);

			s.Append("<div style=\"font-size:30px\">");
			s.Append("<p style=\"margin-bottom:60px;\"><img src=\"/gfx/mail-symbol-a.gif\" width=\"75\" align=\"middle\"/><img src=\"/gfx/mail-format-envelope-a5.gif\" width=\"70\" align=\"middle\" style=\"margin-right:30px;\"/> " + ItemsA.ToString() + " item" + (ItemsA == 1 ? "" : "s") + " = " + BagsA.ToString() + " bag" + (BagsA == 1 ? "" : "s") + "</p>");
			s.Append("<p style=\"margin-bottom:60px;\"><img src=\"/gfx/mail-symbol-b.gif\" width=\"75\" align=\"middle\"/><img src=\"/gfx/mail-format-box.gif\" width=\"70\" align=\"middle\" style=\"margin-right:30px;\"/> " + ItemsB.ToString() + " item" + (ItemsB == 1 ? "" : "s") + " = " + BagsB.ToString() + " bag" + (BagsB == 1 ? "" : "s") + "</p>");
			s.Append("<p style=\"margin-bottom:60px;\"><img src=\"/gfx/mail-symbol-c.gif\" width=\"75\" align=\"middle\"/><img src=\"/gfx/mail-format-box.gif\" width=\"70\" align=\"middle\" style=\"margin-right:30px;\"/> " + ItemsC.ToString() + " item" + (ItemsC == 1 ? "" : "s") + " = " + BagsC.ToString() + " bag" + (BagsC == 1 ? "" : "s") + "</p>");
			s.Append("<p style=\"margin-bottom:60px;\"><img src=\"/gfx/mail-symbol-d.gif\" width=\"75\" align=\"middle\"/><img src=\"/gfx/mail-format-box.gif\" width=\"70\" align=\"middle\" style=\"margin-right:30px;\"/> " + ItemsD.ToString() + " item" + (ItemsD == 1 ? "" : "s") + " = " + BagsD.ToString() + " bag" + (BagsD == 1 ? "" : "s") + "</p>");
			s.Append("<p style=\"margin-bottom:60px;\"><img src=\"/gfx/mail-symbol-e.gif\" width=\"75\" align=\"middle\"/><img src=\"/gfx/mail-format-box.gif\" width=\"70\" align=\"middle\" style=\"margin-right:30px;\"/> " + ItemsE.ToString() + " item" + (ItemsE == 1 ? "" : "s") + " = " + BagsE.ToString() + " bag" + (BagsE == 1 ? "" : "s") + "</p>");
			s.Append("<p style=\"margin-bottom:60px;\"><img src=\"/gfx/mail-symbol-f.gif\" width=\"75\" align=\"middle\"/><img src=\"/gfx/mail-format-envelope-a4.gif\" width=\"70\" align=\"middle\" style=\"margin-right:30px;\"/> " + ItemsF.ToString() + " item" + (ItemsF == 1 ? "" : "s") + " = " + BagsF.ToString() + " bag" + (BagsF == 1 ? "" : "s") + "</p>");
			s.Append("</div>");
			s.Append("</body>");
			
			Response.Clear();
			Response.Write(s.ToString());
			Response.Flush();
			Response.End();
		}
	}
}
