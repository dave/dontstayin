using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.Text;
using Spotted.com.royalmail.epro.www1;
using Spotted.com.royalmail.epro.www;

namespace Spotted.Blank
{
	public partial class MailingUpload : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotAdmin("Only admin!");


			Bobs.Global glo = new Bobs.Global(Bobs.Global.Records.RoyalMailEproPassword);
			OldPass.Text = glo.ValueString;
		}
		#region Upload
		protected void Upload(object sender, EventArgs eventArgs)
		{
			int BagMaxWeight = 10000;
			System.Collections.Generic.Dictionary<string,int> Weight = new System.Collections.Generic.Dictionary<string,int>();
			Weight["A"] = 100;
			Weight["B"] = 500;
			Weight["C"] = 500;
			Weight["D"] = 500;
			Weight["E"] = 500;
			Weight["F"] = 20;

			#region Calculate mailing quantities
			int ItemsA, ItemsB, ItemsC, ItemsD, ItemsE, ItemsF = 0;

			#region UK 100g large letter 2nd class
			if (true)
			{
				//Query q = new Query();
				//q.QueryCondition = new And(
				//    new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
				//    new Q(Usr.Columns.AddressCountryK, 224)
				//);
				//q.ReturnCountOnly = true;
				//UsrSet us = new UsrSet(q);
				//ItemsA = us.Count;
				ItemsA = 0;
			}
			#endregion

			#region UK 500g large-letter 1st class
			if (true)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Or(new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill),new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack)),
					new Q(Usr.Columns.AddressCountryK, 224)
				);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				ItemsB = us.Count;
			}
			#endregion

			#region Western europe 500g large-letter 1st class format sort
			if (true)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Or(
						new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
						new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill)
					),
					new Q(Bobs.Country.Columns.PostageZone, Bobs.Country.PostageZones.WesternEurope)
				);
				q.TableElement = new Join(Usr.Columns.AddressCountryK, Bobs.Country.Columns.K);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				ItemsC = us.Count;
			}
			#endregion

			#region Europe 500g large-letter 1st class zone sort
			if (true)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Or(
						new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
						new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill)
					),
					new Q(Bobs.Country.Columns.PostageZone, Bobs.Country.PostageZones.RestOfEurope)
				);
				q.TableElement = new Join(Usr.Columns.AddressCountryK, Bobs.Country.Columns.K);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				ItemsD = us.Count;
			}
			#endregion

			#region Rest of world 500g large-letter 1st class zone sort
			if (true)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Or(
						new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
						new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill)
					),
					new Or(
						new Q(Bobs.Country.Columns.PostageZone, Bobs.Country.PostageZones.World1),
						new Q(Bobs.Country.Columns.PostageZone, Bobs.Country.PostageZones.World2)
					)
				);
				q.TableElement = new Join(Usr.Columns.AddressCountryK, Bobs.Country.Columns.K);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				ItemsE = us.Count;
			}
			#endregion

			#region UK Promoter letters (letter, 20g)
			if (true)
			{
				Query q = new Query();
				q.QueryCondition = new Q(Promoter.Columns.LetterStatus, Promoter.LetterStatusEnum.Printing);
				q.ReturnCountOnly = true;
				PromoterSet ps = new PromoterSet(q);
				ItemsF = ps.Count;
			}
			#endregion
			#endregion

			#region Calculate number of bags
			int BagsA = (int)Math.Ceiling(ItemsA * (double)Weight["A"] / (double)BagMaxWeight);
			int BagsB = (int)Math.Ceiling(ItemsB * (double)Weight["B"] / (double)BagMaxWeight);
			int BagsC = (int)Math.Ceiling(ItemsC * (double)Weight["C"] / (double)BagMaxWeight);
			int BagsD = (int)Math.Ceiling(ItemsD * (double)Weight["D"] / (double)BagMaxWeight);
			int BagsE = (int)Math.Ceiling(ItemsE * (double)Weight["E"] / (double)BagMaxWeight);
			int BagsF = (int)Math.Ceiling(ItemsF * (double)Weight["F"] / (double)BagMaxWeight);
			#endregion

			#region Get codes
			string CodeA = GetCode("STL", "01", Weight["A"], ItemsA, BagsA, "S", "L", "");
			string CodeB = GetCode("CRL", "01", Weight["B"], ItemsB, BagsB, "F", "F", "");
			string CodeC = GetCode("OF1", "01", Weight["C"], ItemsC, BagsC, "F", "F", "WEU");
			string CodeD = GetCode("OZ1", "01", Weight["D"], ItemsD, BagsD, "F", "F", "EUR");
			string CodeE = GetCode("OZ1", "01", Weight["E"], ItemsE, BagsE, "F", "F", "ROW");
			string CodeF = GetCode("STL", "01", Weight["F"], ItemsF, BagsF, "F", "L", "");
			#endregion

			#region Test (Removed)
		//	to test
			//Response.Write("<pre>");
			//Response.Write(CodeA);
			//Response.Write("\n");
			//Response.Write(CodeB);
			//Response.Write("\n");
			//Response.Write(CodeC);
			//Response.Write("\n");
			//Response.Write(CodeD);
			//Response.Write("\n");
			//Response.Write(CodeE);
			//Response.Write("\n");
			//Response.Write("</pre>");
			//return;
			#endregion

			#region Get transaction id's
			string TransactionA = "", TransactionB = "", TransactionC = "", TransactionD = "", TransactionE = "", TransactionF = "";
			if (ItemsA > 0)
				TransactionA = CreateTransaction(CodeA);
			if (ItemsB > 0)
				TransactionB = CreateTransaction(CodeB);
			if (ItemsC > 0)
				TransactionC = CreateTransaction(CodeC);
			if (ItemsD > 0)
				TransactionD = CreateTransaction(CodeD);
			if (ItemsE > 0)
				TransactionE = CreateTransaction(CodeE);
			if (ItemsF > 0)
				TransactionF = CreateTransaction(CodeF);
			#endregion

			#region Confirm transactions and get docket numbers
			string DocketA = "", DocketB = "", DocketC = "", DocketD = "", DocketE = "", DocketF = "";
			if (ItemsA > 0)
				DocketA = ConfirmTransaction(TransactionA);
			if (ItemsB > 0)
				DocketB = ConfirmTransaction(TransactionB);
			if (ItemsC > 0)
				DocketC = ConfirmTransaction(TransactionC);
			if (ItemsD > 0)
				DocketD = ConfirmTransaction(TransactionD);
			if (ItemsE > 0)
				DocketE = ConfirmTransaction(TransactionE);
			if (ItemsF > 0)
				DocketF = ConfirmTransaction(TransactionF);
			#endregion

			Response.Write("<body topmargin=\"0\" bottommargin=\"0\" leftmargin=\"0\" rightmargin=\"0\">");

			#region Create bag labels list
			List<BagLabelInfo> BagLabels = new List<BagLabelInfo>();
			for (int bagNumber=1; bagNumber<=BagsA; bagNumber++)
				BagLabels.Add(new BagLabelInfo("A", bagNumber, BagsA, DocketA));
			for (int bagNumber=1; bagNumber<=BagsB; bagNumber++)
				BagLabels.Add(new BagLabelInfo("B", bagNumber, BagsB, DocketB));
			for (int bagNumber=1; bagNumber<=BagsC; bagNumber++)
				BagLabels.Add(new BagLabelInfo("C", bagNumber, BagsC, DocketC));
			for (int bagNumber=1; bagNumber<=BagsD; bagNumber++)
				BagLabels.Add(new BagLabelInfo("D", bagNumber, BagsD, DocketD));
			for (int bagNumber=1; bagNumber<=BagsE; bagNumber++)
				BagLabels.Add(new BagLabelInfo("E", bagNumber, BagsE, DocketE));
			for (int bagNumber = 1; bagNumber <= BagsF; bagNumber++)
				BagLabels.Add(new BagLabelInfo("F", bagNumber, BagsF, DocketF));
			#endregion

			#region Write bag tag labels
			StringBuilder s = new StringBuilder();
			BagLabelInfo b;
			for (int count = 0; count < BagLabels.Count || (count % 8) != 0; count++)
			{
				if (count < BagLabels.Count)
					b = BagLabels[count];
				else
					b = null;

				if (count % 2 == 0 && count > 0)
				{
					s.Append("</tr>");
				}

				if (count % 8 == 0)
				{
					if (count > 0)
						s.Append("</table></div>");

					s.Append("<div style=\"page-break-after:always;\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\">");
				}

				if (count % 2 == 0)
				{
					s.Append("<tr>");
				}

				string padding = "";
				if (count % 2 != 0)
				{
					padding = " style=\"padding-left:10px;\"";
				}


				if (b == null)
					s.Append("<td width=\"50%\" height=\"25%\"" + padding + "></td>");
				else
				{
					s.Append("<td width=\"50%\" height=\"25%\"" + padding + ">");

					s.Append("<table width=\"95%\" height=\"90%\" style=\"margin:10px;\">");
					s.Append("<tr>");
					s.Append("<td valign=\"top\">");
					s.Append("Docket: " + b.DocketNumber + "<br>");
					s.Append("Account: 043767000<br>");
					s.Append("Poster: Development Hell Limited,<br>Greenhill House, Thorpe Road,<br>Peterborough, PE3 6RU<br>UNITED KINGDOM<br>");
					#region Zone
					if (b.FormatCode.Equals("C"))
						s.Append("<b>Zone: Western Europe</b><br>");
					else if (b.FormatCode.Equals("D"))
						s.Append("<b>Zone: Rest of Europe</b><br>");
					else if (b.FormatCode.Equals("E"))
						s.Append("<b>Zone: Rest of World</b><br>");
					else
						s.Append("<br>");
					#endregion
					#region Format
					if (b.FormatCode.Equals("F") || b.FormatCode.Equals("A"))
						s.Append("<b>Format: Letter</b><br>");
					else if (b.FormatCode.Equals("B"))
						s.Append("<b>Format: Large letter</b><br>");
					else
						s.Append("<b>Format: G</b><br>"); //P=Letter, G=Large letter, E=Packet
					#endregion
					s.Append("<br><br><br>");
					#region Tag helper
					if (b.FormatCode.Equals("A"))
						s.Append("(green, 2nd class tag)");
					else if (b.FormatCode.Equals("F"))
						s.Append("(red, 1st class tag)");
					else if (b.FormatCode.Equals("B"))
						s.Append("(red, 1st class tag)");
					else
						s.Append("(large white international tag)");
					#endregion
					s.Append("</td>");
					s.Append("<td align=\"right\" valign=\"top\">");
					s.Append("<b>Bag:<br>" + b.BagNumber + " of " + b.TotalBags + "</b><br><br>");
					#region Mail seperation symbol
					s.Append("<img src=\"/gfx/mail-symbol-" + b.FormatCode.ToLower() + ".gif\" width=\"35\"/><br>");
					#endregion
					#region Packaging format symbol
					if (b.FormatCode.Equals("A") || b.FormatCode.Equals("F"))
						s.Append("<img src=\"/gfx/mail-format-envelope-a5.gif\" width=\"30\"/>");
					else
						s.Append("<img src=\"/gfx/mail-format-box.gif\" width=\"30\"/>");
					#endregion

					s.Append("</td>");
					s.Append("</tr>");
					s.Append("</table>");

					s.Append("</td>");
				}
			}
			s.Append("</tr></table></div>");

			Response.Write(s.ToString());
			#endregion

			#region Reset database (only on live site)
			if (!Vars.DevEnv)
			{
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new Or(
					new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingWelcomePack),
					new Q(Usr.Columns.CardStatus, Usr.CardStatusEnum.PrintingRefill));
				UsrSet us = new UsrSet(q);
				foreach (Usr u in us)
				{
					if (u.CardStatus.Equals(Usr.CardStatusEnum.PrintingWelcomePack))
					{
						u.CardStatus = Usr.CardStatusEnum.WelcomePackInPost;
						if (u.AddressCountryK == 224)
							u.TotalCardsSent += 60;
						else
							u.TotalCardsSent += 360;
					}
					else
					{
						u.CardStatus = Usr.CardStatusEnum.CardsInPost;
						u.TotalCardsSent += 360;
					}

					u.Update();
				}

				Query q1 = new Query();
				q1.QueryCondition = new Q(Promoter.Columns.LetterStatus, Promoter.LetterStatusEnum.Printing);
				PromoterSet ps = new PromoterSet(q1);
				Usr dsi = new Usr(8);
				foreach (Promoter p in ps)
				{
					p.LetterStatus = Promoter.LetterStatusEnum.Posted;
					p.SalesHold = false;
					p.SalesNextCall = DateTime.Today.AddDays(5);
					p.Update();
					p.AddNote("Sent tickets domain intro letter", Guid.NewGuid(), dsi);
				}

			}
			#endregion

			#region Log mailing
			Log.Increment(Log.Items.MailUkSmall, ItemsA);
			Log.Increment(Log.Items.MailUkLarge, ItemsB);
			Log.Increment(Log.Items.MailWestEurope, ItemsC);
			Log.Increment(Log.Items.MailRestEurope, ItemsD);
			Log.Increment(Log.Items.MailRestWorld, ItemsE);
			Log.Increment(Log.Items.MailPromoterLetters, ItemsF);
			#endregion

			Response.Write("</body>");

			Response.End();

			

		}
		#endregion

		#region GetReport
		protected void GetReport1(object sender, EventArgs eventArgs)
		{
			throw new DsiUserFriendlyException("OI!!! I told you not to press that button!!!");
		}
		protected void GetReport(object sender, EventArgs eventArgs)
		{
			#region Get summary report
			EProExportService export = new EProExportService();
			Spotted.com.royalmail.epro.www1.Authentication authExport = new Spotted.com.royalmail.epro.www1.Authentication();
			authExport.Version = "1.0.0.1";
			if (!Vars.DevEnv)
			{
				authExport.AccessCode = 47;
				authExport.Username = "manager";
				authExport.Password = "Demonstrate1";
			}
			else
			{
				Bobs.Global glo = new Bobs.Global(Bobs.Global.Records.RoyalMailEproPassword);

				authExport.AccessCode = 10702;
				authExport.Username = "dave@dontstayin.com";
				authExport.Password = glo.ValueString;
			}
			export.AuthenticationValue = authExport;
			Export[] exportList = export.GetExportList();
			if (true)
			{
				Response.Write("<pre>");
				foreach (Export e in exportList)
				{
					Response.Write(e.Reference + " " + e.Name + " (" + e.Description + ")\n");
				}
				Response.Write("</pre>");
				//return;
			}
			
			GenerateExportInput exportRequest = new GenerateExportInput();
			//exportRequest.ExportReference = new Guid("3a33ebb0-6a78-4bec-b703-9c73531ad46a");
			exportRequest.ExportReference = new Guid("3a33ebb0-6a78-4bec-b703-9c73531ad46a");
			exportRequest.OutputType = ExportOutputTypes.html;


			MultiSelectParameter parameter1 = new MultiSelectParameter();
			parameter1.Name = "CustomerIDList";
		//	if (Vars.DevEnv)
		//		parameter1.Values = new string[] { "3325" }; //test account
		//	else
				parameter1.Values = new string[] { "22527" }; //dsi account

			MultiSelectParameter parameter2 = new MultiSelectParameter();
			parameter2.Name = "ServiceIDList";
			parameter2.Values = new string[] { "7111", "7107", "200", "3005" };

			DateTimeParameter parameter3 = new DateTimeParameter();
			parameter3.Name = "StartDate";
			parameter3.Value = DateTime.Today.AddDays(0);

			DateTimeParameter parameter4 = new DateTimeParameter();
			parameter4.Name = "EndDate";
			parameter4.Value = DateTime.Today.AddDays(1);

			BooleanParameter parameter5 = new BooleanParameter();
			parameter5.Name = "PosterDetails";
			parameter5.Value = true;

			BooleanParameter parameter6 = new BooleanParameter();
			parameter6.Name = "DocketDetails";
			parameter6.Value = true;

			SingleSelectParameter parameter7 = new SingleSelectParameter();
			parameter7.Name = "ApprovalState";
			parameter7.Value = "3"; //1=Approved, 2=Unapproved, 3=Both

			BooleanParameter parameter8 = new BooleanParameter();
			parameter8.Name = "RPSiteComments";
			parameter8.Value = false;

			BooleanParameter parameter9 = new BooleanParameter();
			parameter9.Name = "PosterComments";
			parameter9.Value = false;

			exportRequest.Parameters = new ExportParameter[] { parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9 };
			GenerateExportOutput report;
			try
			{
				report = export.GenerateExport(exportRequest);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			#endregion

			if (report.ExtraParametersRequired!=null && report.ExtraParametersRequired.Length > 0)
			{
				foreach (RequiredParameter rp in report.ExtraParametersRequired)
				{
					Response.Write("<b>" + rp.Name + " (" + rp.ParameterType + ")</b> ");
					if (rp.PossibleValues != null)
					{
						foreach (PossibleValue pv in rp.PossibleValues)
							Response.Write("<br>" + pv.Value + " (" + pv.Description + ")");
					}
				}
				return;
			}


			#region Output report to page
		//	Response.Clear();
		//	Response.Write("<div style=\"page-break-after:always;\">");
			Response.Write(report.ExportData.InnerText);
		//	Response.Write("</div>");
			#endregion

		}
		#endregion

		#region BagLabelInfo
		public class BagLabelInfo
		{
			#region BagLabelInfo(string FormatCode, int BagNumber, int TotalBags, string DocketNumber)
			public BagLabelInfo(string FormatCode, int BagNumber, int TotalBags, string DocketNumber)
			{
				this.FormatCode = FormatCode;
				this.BagNumber = BagNumber;
				this.TotalBags = TotalBags;
				this.DocketNumber = DocketNumber;
			}
			#endregion

			#region FormatCode
			public string FormatCode
			{
				get
				{
					return formatCode;
				}
				set
				{
					formatCode = value;
				}
			}
			string formatCode;
			#endregion

			#region BagNumber
			public int BagNumber
			{
				get
				{
					return bagNumber;
				}
				set
				{
					bagNumber = value;
				}
			}
			int bagNumber;
			#endregion

			#region TotalBags
			public int TotalBags
			{
				get
				{
					return totalBags;
				}
				set
				{
					totalBags = value;
				}
			}
			int totalBags;
			#endregion

			#region DocketNumber
			public string DocketNumber
			{
				get
				{
					return docketNumber;
				}
				set
				{
					docketNumber = value;
				}
			}
			string docketNumber;
			#endregion

		}
		#endregion

		#region CreateTransaction
		public string CreateTransaction(string code)
		{
			#region Create transatcion
			EProImportService mail = GetEpro();
			TransactionInfo ti = mail.MailingItem(new string[] { code });
			if (ti.Errors.Length > 0)
			{
				string errors = "";
				foreach (Spotted.com.royalmail.epro.www.Error e in ti.Errors)
				{
					errors += " | " + e.Message + " (" + e.ExtraInformation + ")";
				}
				throw new Exception("Errors " + errors);
			}
			return ti.TransactionID;
			#endregion
		}
		#endregion

		#region ConfirmTransaction (create docket)
		public string ConfirmTransaction(string transationId)
		{
			#region Confirm a transaction (create a docket code)
			EProImportService mail = GetEpro();
			TransactionInfo ti = mail.CreateDockets(transationId);
			
			if (ti.Errors.Length > 0)
			{
				string errors = "";
				foreach (Spotted.com.royalmail.epro.www.Error e in ti.Errors)
				{
					errors += " | " + e.Message + " (" + e.ExtraInformation + ")";
				}
				throw new Exception("WARNING! Errors confirming transation - (other dockets already created) - DO NOT RE-RUN THIS PAGE " + errors);
			}

			if (ti.Dockets.Length > 1)
			{
				string dockets = "";
				foreach (Spotted.com.royalmail.epro.www.Docket d in ti.Dockets)
				{
					dockets += " | " + d.DocketNumber;
				}
				throw new Exception("WARNING! Multiple dockets created! - (other dockets already created) - DO NOT RE-RUN THIS PAGE " + dockets);
			}

			if (ti.Dockets.Length == 0)
				throw new Exception("WARNING! No dockets created! - (other dockets already created) - DO NOT RE-RUN THIS PAGE");

			return ti.Dockets[0].DocketNumber;
			#endregion
		}
		#endregion

		#region GetEpro()
		public EProImportService GetEpro()
		{
			EProImportService mail = new EProImportService();
			Spotted.com.royalmail.epro.www.Authentication auth = new Spotted.com.royalmail.epro.www.Authentication();
			auth.Version = "1.0.0.1";
			if (Vars.DevEnv)
			{
				auth.AccessCode = 47;
				auth.Username = "manager";
				auth.Password = "Demonstrate1";
			}
			else
			{
				Bobs.Global glo = new Bobs.Global(Bobs.Global.Records.RoyalMailEproPassword);

				auth.AccessCode = 10702;
				auth.Username = "dave@dontstayin.com";
				auth.Password = glo.ValueString;
			}
			mail.AuthenticationValue = auth;
			return mail;
		}
		#endregion

		#region UpdatePass
		protected void UpdatePass(object sender, EventArgs eventArgs)
		{
			Bobs.Global glo = new Bobs.Global(Bobs.Global.Records.RoyalMailEproPassword);
			glo.ValueString = Pass.Text;
			glo.Update();
			Pass.Text="";
		}
		#endregion

		#region GetCode
		/// <summary>
		/// Gets a E*Pro code
		/// </summary>
		/// <param name="serviceRegister">
		/// [STL = Standard tarrif letters]
		/// [OF1 = International format sort priority]
		/// [OZ1 = International zone sort priority]
		/// [CRL = Packet post]
		/// </param>
		/// <param name="serviceLevel">
		/// [For STL: Always 01]
		/// [For CRL: 01 = First class, 02 = Second class]
		/// </param>
		/// <param name="weight">
		/// Item weight in g (for all serivces - we force this later)
		/// </param>
		/// <param name="itemCount">
		/// Number of items
		/// </param>
		/// <param name="bagCount">
		/// Number of postage bags
		/// </param>
		/// <param name="postageClass">
		/// [F = First class]
		/// [S = Second class]
		/// </param>
		/// <param name="format">
		/// [L = Letters]
		/// [F = Flats / large letters]
		/// [P = Packets]
		/// [A = A3 packets]
		/// </param>
		/// <param name="zone">
		/// [WEU = Western europe]
		/// [ROW = Rest of world]
		/// [ROE = Rest of europe]
		/// [EUR = Europe (only for OZ1, OZ3, ZC1)]
		/// [RWE = Rest of world (+500g for TC2, TC3)]
		/// [WEE = Western Europe (+500g for TC2, TC3)]
		/// </param>
		/// <returns></returns>
		public string GetCode(string serviceRegister, string serviceLevel, int weight, int itemCount, int bagCount, string postageClass, string format, string zone)
		{
			StringBuilder s = new StringBuilder();
			if (Vars.DevEnv)
				s.Append(Pad("764070001", 9)); // Test customer account number
			else
				s.Append(Pad("043767000", 9)); // DontStayIn customer account number
			s.Append(Pad(serviceRegister, 3)); // Service register - depends which post format
			s.Append(Pad(serviceLevel, 2)); // Service level
			s.Append(Pad(weight.ToString(), 10)); // Service level
			s.Append(Pad(itemCount.ToString(), 10)); // Item count
			s.Append(Pad(bagCount.ToString(), 5)); // Bag count
			s.Append(Pad(postageClass, 1)); // class
			s.Append(Pad(format, 1)); // format
			s.Append(Pad("", 1)); // sortation
			s.Append(Pad("", 1)); // submission
			s.Append(Pad("", 1)); // ocr indacator
			s.Append(Pad("", 1)); // franking indacator
			s.Append(Pad("", 1)); // franking surcharge
			s.Append(Pad(zone, 3)); // zone
			s.Append(Pad("", 3)); // country
			s.Append(Pad("", 3)); // locality
			s.Append(Pad("", 1)); // region
			s.Append(Pad("1", 1)); // force single weight (this forces all services to interpret the weight as the single item weight)
			s.Append(Pad("", 9)); // reserved
			s.Append(Pad("", 3)); // consignment ref
			s.Append(Pad("", 50)); // department
			s.Append(Pad("", 200)); // validation
			s.Append(Pad("", 250)); // fees
			s.Append(Pad("", 8)); // issue id
			s.Append(Pad("", 50)); // magazine name
			s.Append(Pad("", 8)); // booking reference number
			s.Append(Pad("", 12)); // map reference number
			s.Append(Pad("", 15)); // bag identifier
			s.Append(Pad("", 5)); // mailsort collection code

			return s.ToString();
		}
		#endregion

		#region Pad
		public string Pad(string unpadded, int length)
		{
			if (unpadded.Length > length)
				return unpadded.Substring(0, length);
			else if (unpadded.Length == length)
				return unpadded;
			else
				return unpadded.PadRight(length);
		}
		#endregion

	}
}
