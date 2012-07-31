using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

using Common;
using Bobs.JobProcessor;
using NUnit.Framework;

namespace Bobs
{

	public class Utilities
	{
		// Do not allow users to instantiate a Utilities object
		private Utilities()
		{ }

		public static string TruncateIp(string ip)
		{
			if (ip.Length > 15)
				return ip.Substring(0, 15);
			else
				return ip;
		}

		public class UpdateChildUrlFragmentsJob : Job
		{
			JobDataMapItemProperty<Model.Entities.ObjectType> ObjectType { get { return new JobDataMapItemProperty<Model.Entities.ObjectType>("ObjectType", JobDataMap); } }
			JobDataMapItemProperty<int> ObjectK { get { return new JobDataMapItemProperty<int>("ObjectK", JobDataMap); } }
			JobDataMapItemProperty<bool> Cascade { get { return new JobDataMapItemProperty<bool>("Cascade", JobDataMap); } }

			public UpdateChildUrlFragmentsJob()
			{ }
			public UpdateChildUrlFragmentsJob(Model.Entities.ObjectType objectType, int objectK, bool cascade)
			{
				ObjectType.Value = objectType;
				ObjectK.Value = objectK;
				Cascade.Value = cascade;
			}

			protected override void Execute()
			{
				var b = Bob.Get(ObjectType, ObjectK);
				if (b is IObjectPage)
					((IObjectPage)b).UpdateChildUrlFragments(Cascade);
			}
		}

		public static void DeletePic(IPic Pic)
		{
			try
			{
				if (!Pic.Equals(Guid.Empty))
				{
					Storage.RemoveFromStore(Storage.Stores.Pix, Pic.Pic, "jpg");
				}
			}
			catch { }

			try
			{
				if (Pic.PicMiscK > 0 && Pic.PicMisc != null)
					Pic.PicMisc.DeleteAll(null);
			}
			catch { }

			Pic.Pic = Guid.Empty;
			Pic.PicMiscK = 0;
			Pic.PicPhotoK = 0;
			Pic.PicState = "";
			((IBob)Pic).Update();
		}
		public static void CopyPic(IPic From, IPic To)
		{
			if (To.HasPic)
				Utilities.DeletePic(To);

			if (From.HasPic)
			{
				To.Pic = Guid.NewGuid();
				Storage.AddToStore(
					Storage.GetFromStore(Storage.Stores.Pix, From.Pic, "jpg"),
					Storage.Stores.Pix,
					To.Pic,
					"jpg",
					(IBob)To,
					"Pic");
				To.PicState = From.PicState;
				To.PicPhotoK = From.PicPhotoK;
				if (From.PicMiscK > 0)
				{
					Misc m = From.PicMisc.Duplicate();
					To.PicMiscK = m.K;
				}
				((IBob)To).Update();
			}
		}

		public delegate void Action();

		public static System.Threading.Thread GetSafeThread(Action action)
		{
			System.Threading.Thread thread = new System.Threading.Thread
			(
				new ThreadStart
					(
						() =>
						{
							try
							{
								action();
							}
							catch (Exception ex)
							{
								SpottedException.TryToSaveExceptionAndChildExceptions(ex, null, null, null, "", "SafeThreadException", "", 0, null);
							}
						}
				)
			);
			return thread;
		}


			
		
		public static void EnableDisableControls(WebControl control, bool enable)
		{
			if (control is TextBox)
			{
				((TextBox)control).ReadOnly = !enable;
				if (!enable)
					control.CssClass = "disabledTextBox";
				else
					control.CssClass = "";
			}
			else
				control.Enabled = enable;

			// Never disable Panels, Labels, ValidationSummaries
			if (control is Panel || control is Label || control is ValidationSummary)
				control.Enabled = true;

			EnableDisableChildControls(control, enable);
		}

		public static void EnableDisableChildControls(Control control, bool enable)
		{
			foreach (Control ctl in control.Controls)
			{
				if (ctl.GetType().Equals(typeof(Cambro.Web.DbCombo.DbCombo)))
					((Cambro.Web.DbCombo.DbCombo)ctl).Enabled = enable;
				else if (ctl is WebControl)
					EnableDisableControls((WebControl)ctl, enable);
				else if(!(ctl is System.Web.UI.UserControl))
					EnableDisableChildControls(ctl, enable);
			}
		}

		#region Enums
		public enum DateRange
		{
			Current = 1,
			Old = 2,
			All = 3
		}
		#endregion

		#region String Tools
		#region Convert Percentage string to double
		public static double ConvertPercentageStringToDouble(string percentage)
		{
			double value;
			double.TryParse(percentage.Replace("%", "").Trim(), out value);
			return value / 100d;
		}
		#endregion
		#region Convert Money string to double
		public static decimal ConvertMoneyStringToDecimal(string money)
		{
			money = money.Trim().Replace("£", "").Replace("$", "");
			decimal output = 0;
			try
			{
				output = Math.Round(Convert.ToDecimal(money), 2);
			}
			catch (Exception)
			{ }

			return output;
		}
		#endregion

        public static string MoneyToHTML(decimal amount)
        {
            return "<nobr>" + HttpUtility.HtmlEncode(amount.ToString("c")).Replace("-", "&#8209;").Replace("£", "&#163;") + "</nobr>";
        }
        public static string MoneyToHTML(double amount)
        {
            return MoneyToHTML(Convert.ToDecimal(amount));
        }

		#region Camel Case to string
		public static string CamelCaseToString(string camelCaseString)
		{
			StringBuilder sb = new StringBuilder();
			char[] chars = camelCaseString.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i > 0 && char.IsUpper(chars[i]) && (char.IsLower(chars[i-1]) || ((i+1<chars.Length) && char.IsLower(chars[i+1]))))
                {
                    sb.Append(" ");
                    sb.Append(chars[i]);
                }
                else
                    sb.Append(chars[i]);
            }
			return sb.ToString();
		}

		/// <summary>
		/// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
		/// </summary>
		/// <param name="characters">Unicode Byte Array to be converted to String</param>
		/// <returns>String converted from Unicode Byte Array</returns>
		public static String UTF8ByteArrayToString(Byte[] characters)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			String constructedString = encoding.GetString(characters);
			return (constructedString);
		}

		/// <summary>
		/// Converts the String to UTF8 Byte array and is used in De serialization
		/// </summary>
		/// <param name="pXmlString"></param>
		/// <returns></returns>
		public static Byte[] StringToUTF8ByteArray(String pXmlString)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			Byte[] byteArray = encoding.GetBytes(pXmlString);
			return byteArray;
		}
		#endregion

		#region Crop String
		public static string CropString(string s, int maxlength)
		{
			if (s.Length > maxlength)
				return s.Substring(0, maxlength);
			else
				return s;
		}
		#endregion

		#region Remove Non Ascii from string
		public static string GetAsciiOnly(string s)
		{
			return GetAsciiOnly(s, GetAsciiOptions.StandardAscii);
		}

		public static string GetAsciiOnly(string s, GetAsciiOptions getAsciiOption)
		{
			byte[] b = System.Text.UnicodeEncoding.Unicode.GetBytes(s);
			StringBuilder sb = new StringBuilder();
			//string output = "";
			int i = 0;
			//char c;
			while (i + 1 < b.Length)
			{
				if (b[i + 1] == 0)
				{
					// Sage approved characters, as dictated by Gee Thatcher on 16/11/06
					if (getAsciiOption.Equals(GetAsciiOptions.SageApprovedChars))
					{
						if (b[i] >= 32 && b[i] <= 126)
							sb.Append((char)b[i]);
						//else
						//    c = (char)b[i];
					}
					else if (getAsciiOption.Equals(GetAsciiOptions.StandardAscii))
					{
						if (b[i] >= 0 && b[i] < 128)
							sb.Append((char)b[i]);
					}
				}
				i += 2;
			}

			return sb.ToString();
		}

		public enum GetAsciiOptions
		{
			SageApprovedChars = 1,
			StandardAscii = 2
		}
		#endregion

		public static string DateToString(DateTime date)
		{
			if (date > DateTime.MinValue)
				return date.ToString("dd/MM/yy");
			else
				return "";
		}

		public static string BooleanToYesNo(bool myBool)
		{
			return myBool ? "Yes" : "No";
		}

		public static string GetFirstName(string name)
		{
			if (name.LastIndexOf(" ") > 0 && name.LastIndexOf(" ") <= name.Length)
                return name.Trim().Substring(0, name.LastIndexOf(" ")).Trim();
			else
                return name.Trim();            
        }
		
		public static string GetLastName(string name)
		{
			if (name.LastIndexOf(" ") > 0 && name.LastIndexOf(" ") < name.Length)
				return name.Trim().Substring(name.LastIndexOf(" ") + 1).Trim();
			else
				return "";
		}

        public static string StripTitleFromName(string name)
        {
            if (name.Length > 3 && (name.ToUpper().IndexOf("MR ") == 0 || name.ToUpper().IndexOf("DR ") == 0 || name.ToUpper().IndexOf("MS ") == 0))
                return name.Substring(3).Trim();
            else if (name.Length > 4 && (name.ToUpper().IndexOf("MR. ") == 0 || name.ToUpper().IndexOf("DR. ") == 0 || name.ToUpper().IndexOf("MS. ") == 0 || name.ToUpper().IndexOf("MRS ") == 0))
                return name.Substring(4).Trim();
            else if (name.Length > 5 && (name.ToUpper().IndexOf("MRS. ") == 0 || name.ToUpper().IndexOf("MISS ") == 0))
                return name.Substring(5).Trim();
            else
                return name;
        }

		public static string CreateMailtoAnchor(string emailAddress)
		{
			if (emailAddress.Trim() == "") 
				return "";
			else
				return "<a href='mailto:" + emailAddress + "'>" + emailAddress + "</a>";
		}
		#endregion

        #region Enums To ListItems and DropDownLists
        public static void AddEnumValuesToDropDownList(DropDownList ddl, Type enumType)
        {
            AddEnumValuesToDropDownList(ddl, enumType, false, true);
        }
        public static void AddEnumValuesToDropDownList(DropDownList ddl, Type enumType, bool addBlankListItem, bool sortAlphabetically)
        {
            ddl.Items.AddRange(EnumToListItemArray(enumType, addBlankListItem, sortAlphabetically));            
        }
        public static ListItem[] EnumToListItemArray(Type enumType)
        {
            return EnumToListItemArray(enumType, false, true);
        }
        public static ListItem[] EnumToListItemArray(Type enumType, bool addBlankListItem, bool sortAlphabetically)
        {
            List<ListItem> listItems = new List<ListItem>();
            if (addBlankListItem)
            {
                listItems.Add(new ListItem("", ""));
            }
            Array enumValues = Enum.GetValues(enumType);

            if (sortAlphabetically)
            {
                SortedList enumSortedList = new SortedList();
                foreach (object value in enumValues)
                {
                    enumSortedList.Add(value.ToString(), (int)value);
                }
                
                for(int i=0; i < enumSortedList.Count; i++)
                {
                    listItems.Add(new ListItem(Utilities.CamelCaseToString(enumSortedList.GetKey(i).ToString()), (enumSortedList[enumSortedList.GetKey(i)]).ToString()));
                }
            }
            else
            {
                foreach (object value in enumValues)
                {
                    listItems.Add(new ListItem(Utilities.CamelCaseToString(value.ToString()), ((int)value).ToString()));
                }
            }
            return listItems.ToArray();
        }
		#endregion

		#region Link
		public static string Link(string url, string text)
		{
			return Link(url, text, "");
		}
		public static string LinkNewWindow(string url, string text)
		{
			return Link(url, text, " target=\"_blank\"");
		}
		public static string Link(string url, string text, string attributes)
		{
			return "<a href=\"" + url + "\"" + attributes + ">" + text + "</a>";
		}
		#endregion

		#region IconHtml
		public enum Icon
		{
			Tick,
			Cross,
			Add,
			Edit,
			Cancel,
			Delete,
			View,
			Printer,
			Save,
			Tickets,
			Article,
			Pause,
			Resume,
			Stop,
			Lock,
            TickLocked,
            LockTicked
		}

		public static string TickLockHtml(bool tick)
		{
			if (tick)
				return IconHtml(Icon.Tick);
			else
				return IconHtml(Icon.Lock);
		}

        public static string TickLockHtml(bool tick, bool manualLock, bool manualOverride)
        {
            if (manualOverride)
                return TickLockOverrideHtml(tick);
            else if (manualLock)
                return TickManualLockHtml(tick);
            else
                return TickLockHtml(tick);
        }

        public static string TickManualLockHtml(bool tick)
        {
            if (tick)
                return IconHtml(Icon.TickLocked);
            else
                return IconHtml(Icon.Lock);
        }

        public static string TickLockOverrideHtml(bool tick)
        {
            if (tick)
                return IconHtml(Icon.Tick);
            else
                return IconHtml(Icon.LockTicked);
        }

		public static string TickCrossHtml(bool tick)
		{
			if (tick)
				return IconHtml(Icon.Tick);
			else
				return IconHtml(Icon.Cross);
		}
        public static string TickCrossHtml(bool tick, bool manualLock, bool manualOverride)
        {
            if (tick)
            {
                if(manualLock && !manualOverride)
                    return IconHtml(Icon.TickLocked);
                else
                    return IconHtml(Icon.Tick);
            }
            else
                return IconHtml(Icon.Cross);
        }
		public static string PauseResumeHtml(bool paused)
		{
			if (paused)
				return IconHtml(Icon.Resume);
			else
				return IconHtml(Icon.Pause);
		}
		public static string IconHtml(Icon icon)
		{
			return IconHtml(icon, "", "");
		}
		public static string IconHtml(Icon icon, string overrideAlt, string attributes)
		{
			string src = "";
			string alt = "";

			switch (icon)
			{
				case Icon.Add:	
					src = "/gfx/icon-add.png";
					alt = "Add";
					break;
				case Icon.Article: 
					src = "/gfx/article.gif";
					break;
				case Icon.Cancel: 
					src = "/gfx/icon-cancel.png";
					alt = "Cancel";
					break;
				case Icon.Cross: 
					src = "/gfx/icon-cross-up.png";
					break;
				case Icon.Delete: 
					src = "/gfx/icon-cross-up.png";
					alt = "Delete";
					break;
				case Icon.Edit: 
					src = "/gfx/icon-edit.png";
					alt = "Edit";
					break;
				case Icon.Lock:
					src = "/gfx/icon-key.png";
					alt = "Locked";
					break;
				case Icon.Pause: 
					src = "/gfx/icon-pause.png";
					alt = "Pause";
					break;
				case Icon.Printer: 
					src = "/gfx/icon-print.png";
					alt = "Print";
					break;
				case Icon.Resume: 
					src = "/gfx/icon-resume.png";
					alt = "Resume";
					break;
				case Icon.Save: 
					src = "/gfx/icon-save.png";
					alt = "Save";
					break;
				case Icon.Stop: 
					src = "/gfx/icon-stop.png";
					alt = "Stop";
					break;
				case Icon.Tick: 
					src = "/gfx/icon-tick-up.png";
					break;
				case Icon.Tickets: 
					src = "/gfx/icon-tickets.png";
					alt = "Tickets";
					break;
				case Icon.View: 
					src = "/gfx/icon-view.png";
					alt = "View";
					break;
                case Icon.TickLocked:
                    src = "/gfx/button-ticklocked.gif";
                    break;
                case Icon.LockTicked:
                    src = "/gfx/icon-lock-ticked.png";
                    break;
				default: src = "";
					break;
			}

			if (overrideAlt.Length > 0)
				alt = overrideAlt;

			if (src == "")
				return "";
			else
				return "<img src=\"" + src + "\" alt=\"" + alt + "\" border=\"0\" height=\"21\" width=\"26\" align=\"absmiddle\"" + attributes + ">";
		}		
		#endregion

		#region Mask Card Number
		public const int DISPLAY_CARD_LAST_NUMBER_OF_DIGITS = 4;
		private const string CARD_NUMBER_MASK = "*";

		//public static string MaskCardNumber(string cardNumber)
		//{
		//    bool maskCardNumber = false;

		//    if (cardNumber.Length > DISPLAY_CARD_LAST_NUMBER_OF_DIGITS)
		//    {
		//        for (int i = 0; i < DISPLAY_CARD_LAST_NUMBER_OF_DIGITS; i++)
		//        {
		//            if (!cardNumber.Substring(i, 1).Equals(CARD_NUMBER_MASK))
		//            {
		//                maskCardNumber = true;
		//                break;
		//            }
		//        }
		//    }

		//    if (maskCardNumber)
		//    {
		//        StringBuilder sbCardNumberEnd = new StringBuilder();
		//        for (int i = 0; i < cardNumber.Length - DISPLAY_CARD_LAST_NUMBER_OF_DIGITS; i++)
		//        {
		//            sbCardNumberEnd.Append(CARD_NUMBER_MASK);
		//        }
		//        if (cardNumber.Length > DISPLAY_CARD_LAST_NUMBER_OF_DIGITS)
		//            sbCardNumberEnd.Append(cardNumber.Substring(cardNumber.Length - DISPLAY_CARD_LAST_NUMBER_OF_DIGITS));

		//        return sbCardNumberEnd.ToString();
		//    }

		//    return cardNumber;
		//}

		public static string MaskedCardNumber(int cardDigits, string cardNumberEnd)
		{
			StringBuilder sb = new StringBuilder();
			
			for (int i = 0; i < cardDigits - cardNumberEnd.Length; i++)
			{
				sb.Append(CARD_NUMBER_MASK);
			}
			sb.Append(cardNumberEnd);

			return sb.ToString();
		}
		#endregion

        #region Cardnet delay
        public static DateTime CardnetDelay()
        {
            return CardnetDelay(Time.Now);
        }
        public static DateTime CardnetDelay(DateTime dt)
        {
            dt = new DateTime(dt.Year, dt.Month, dt.Day);

            if(dt.DayOfWeek == DayOfWeek.Friday)
                dt = dt.AddDays(2);
            if(dt.DayOfWeek == DayOfWeek.Saturday)
                dt = dt.AddDays(1);

            return dt.AddDays(-15);
        }
        public static DateTime CardnetWait()
        {
            return CardnetWait(Time.Now);
        }
        public static DateTime CardnetWait(DateTime dt)
        {
            dt = new DateTime(dt.Year, dt.Month, dt.Day);
            if (dt.DayOfWeek == DayOfWeek.Friday)
                dt = dt.AddDays(-1);
			if (dt.DayOfWeek == DayOfWeek.Saturday)
                dt = dt.AddDays(-2);

            return dt.AddDays(15);
        }
        #endregion

        #region Export To Sage
		private const string CAMPAIGN_CREDIT_TRANSACTION_TYPE = "CC";
        private const string INVOICE_TRANSACTION_TYPE = "SI";
		private const string CREDIT_TRANSACTION_TYPE = "SC";
		private const string BANK_RECEIPT_TRANSACTION_TYPE = "BR";
		private const string SALES_RECEIPT_TRANSACTION_TYPE = "SR";
		private const string JOURNAL_CREDIT_TRANSACTION_TYPE = "JC";
		private const string JOURNAL_DEBIT_TRANSACTION_TYPE = "JD";
		private const string INVOICE_K_PREFIX = "IN";
		private const string CREDIT_K_PREFIX = "CR";
		private const string TRANSFER_K_PREFIX = "TR";
		private const string BANK_RECEIPT_K_PREFIX = "WB";
		private const string PREPAID_INCOME_PREFIX = "Prepaid income ";
		private const string DEPT_NBR = "0";
		private const string DELIMITER = ",";
		private const string NEWLINE = "\n";
		private const int DESC_MAX_LENGTH = 60;

		public enum ExportToSageType
		{
			SalesInvoices = 1,
			SalesCredits = 2,
			BankReceipts = 3,
			PrepaidIncome = 4,
			SalesReceipts = 5,
			CampaignCredits = 6
		};

		public static ListItem[] ExportToSageTypesAsListItemArray()
		{
			ListItem[] ListItems = new ListItem[4];
			ListItems[0] = new ListItem(Utilities.CamelCaseToString(ExportToSageType.SalesInvoices.ToString()), Convert.ToInt32(ExportToSageType.SalesInvoices).ToString());
			ListItems[1] = new ListItem(Utilities.CamelCaseToString(ExportToSageType.SalesCredits.ToString()), Convert.ToInt32(ExportToSageType.SalesCredits).ToString());
			ListItems[2] = new ListItem(Utilities.CamelCaseToString(ExportToSageType.BankReceipts.ToString()), Convert.ToInt32(ExportToSageType.BankReceipts).ToString());
			ListItems[3] = new ListItem(Utilities.CamelCaseToString(ExportToSageType.PrepaidIncome.ToString()), Convert.ToInt32(ExportToSageType.PrepaidIncome).ToString());
			ListItems[3] = new ListItem(Utilities.CamelCaseToString(ExportToSageType.SalesReceipts.ToString()), Convert.ToInt32(ExportToSageType.SalesReceipts).ToString());
			ListItems[3] = new ListItem(Utilities.CamelCaseToString(ExportToSageType.CampaignCredits.ToString()), Convert.ToInt32(ExportToSageType.CampaignCredits).ToString());

			return ListItems;
		}

		public static string ExportToSage(int month, int year)
		{
			DateTime startDate = new DateTime(year, month, 1);
			DateTime endDate = new DateTime(year, month + 1, 1).AddMilliseconds(-1);

			return ExportToSage(startDate, endDate);
		}

		public static string ExportToSage(int month, int year, ExportToSageType type)
		{
			DateTime startDate = new DateTime(year, month, 1);
			DateTime endDate = new DateTime(year, month + 1, 1).AddMilliseconds(-1);

			return ExportToSage(startDate, endDate, type);
		}

		public static string ExportToSage(DateTime startDate, DateTime endDate)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(ExportToSage(startDate, endDate, ExportToSageType.SalesInvoices));
			sb.Append(ExportToSage(startDate, endDate, ExportToSageType.SalesCredits));
			sb.Append(ExportToSage(startDate, endDate, ExportToSageType.BankReceipts));
			sb.Append(ExportToSage(startDate, endDate, ExportToSageType.PrepaidIncome));
		//	sb.Append(ExportToSage(startDate, endDate, ExportToSageType.SalesReceipts));
		//	sb.Append(ExportToSage(startDate, endDate, ExportToSageType.CampaignCredits));

			return sb.ToString();
		}

		public static string ExportToSage(DateTime startDate, DateTime endDate, ExportToSageType type)
		{
			StringBuilder sb = new StringBuilder();

			startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
			endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day).AddDays(1);


			if (type.Equals(ExportToSageType.SalesInvoices))
			{
				#region SalesInvoices
				// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
				Query InvoiceQuery = new Query(new And(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
													   new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, endDate),
													   new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
													   new Q(Invoice.Columns.IsImmediateCreditCardPayment, false)));
				InvoiceQuery.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.TaxDateTime, OrderBy.OrderDirection.Ascending),
								   new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Ascending));

				InvoiceSet invoiceSet = new InvoiceSet(InvoiceQuery);
				string invoiceItemVatCodeToString = "";
				string itemDescription = "";

				// Sales Invoices
				foreach (Invoice invoice in invoiceSet)
				{
					foreach (InvoiceItem invoiceItem in invoice.Items)
					{
						invoiceItemVatCodeToString = invoiceItem.VatCode.ToString();
						itemDescription = GetAsciiOnly(invoiceItem.Description, GetAsciiOptions.SageApprovedChars).Replace(DELIMITER, ";");
						if (!invoice.VatCode.Equals(Invoice.VATCodes.T1))
							invoiceItemVatCodeToString = invoice.VatCode.ToString();

						sb.Append(INVOICE_TRANSACTION_TYPE);
						sb.Append(DELIMITER);
						sb.Append(invoice.PromoterK);
						sb.Append(DELIMITER);
						sb.Append(invoiceItem.NominalCode);
						sb.Append(DELIMITER);
						sb.Append(DEPT_NBR);
						sb.Append(DELIMITER);
						// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
						sb.Append(invoice.TaxDateTime.ToString("dd/MM/yy"));
						sb.Append(DELIMITER);
						sb.Append(INVOICE_K_PREFIX);							// Prefix + K.ToString() <= 8 characters
						sb.Append(invoice.K.ToString("000000"));				// Up to 6 digit K number. for K of 10^6 or greater, reduce the prefix size
						sb.Append(DELIMITER);
						sb.Append(CropString(itemDescription, DESC_MAX_LENGTH));
						sb.Append(DELIMITER);
						sb.Append(invoiceItem.Price.ToString("0.00"));
						sb.Append(DELIMITER);
						sb.Append(invoiceItemVatCodeToString);
						sb.Append(DELIMITER);
						sb.Append(invoiceItem.Vat);
						sb.Append(NEWLINE);
					}
				}
				#endregion
			}
			else if (type.Equals(ExportToSageType.SalesCredits))
			{
				#region SalesCredits
				// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
				Query CreditQuery = new Query(new And(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
													  new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, endDate),
													  new Q(Invoice.Columns.Type, Invoice.Types.Credit)));
				CreditQuery.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.TaxDateTime, OrderBy.OrderDirection.Ascending),
								   new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Ascending));

				InvoiceSet creditSet = new InvoiceSet(CreditQuery);
				string creditItemVatCodeToString = "";
				string itemDescription = "";
				// Sales Invoices
				foreach (Invoice credit in creditSet)
				{
					foreach (InvoiceItem creditItem in credit.Items)
					{
						creditItemVatCodeToString = creditItem.VatCode.ToString();
						itemDescription = GetAsciiOnly(creditItem.Description, GetAsciiOptions.SageApprovedChars).Replace(DELIMITER, ";");

						if (!credit.VatCode.Equals(Invoice.VATCodes.T1))
							creditItemVatCodeToString = credit.VatCode.ToString();

						sb.Append(CREDIT_TRANSACTION_TYPE);
						sb.Append(DELIMITER);
						sb.Append(credit.PromoterK);
						sb.Append(DELIMITER);
						sb.Append(creditItem.NominalCode);
						sb.Append(DELIMITER);
						sb.Append(DEPT_NBR);
						sb.Append(DELIMITER);
						// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
						sb.Append(credit.TaxDateTime.ToString("dd/MM/yy"));
						sb.Append(DELIMITER);
						sb.Append(CREDIT_K_PREFIX);									// Prefix + K.ToString() <= 8 characters
						sb.Append(credit.K.ToString("000000"));						// Up to 6 digit K number. for K of 10^6 or greater, reduce the prefix size	
						sb.Append(DELIMITER);
						sb.Append(CropString(itemDescription, DESC_MAX_LENGTH));
						sb.Append(DELIMITER);
						sb.Append(Convert.ToDouble(Math.Abs(creditItem.Price)).ToString("0.00"));
						sb.Append(DELIMITER);
						sb.Append(creditItemVatCodeToString);
						sb.Append(DELIMITER);
						sb.Append(Convert.ToDouble(Math.Abs(creditItem.Vat)).ToString("0.00"));
						sb.Append(NEWLINE);
					}
				}
				#endregion
			}
			else if (type.Equals(ExportToSageType.BankReceipts))
			{
				#region BankReceipts
				// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
				Query InvoiceQuery = new Query(new And(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
													   new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, endDate),
													   new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
													   new Q(Invoice.Columns.IsImmediateCreditCardPayment, true)));
				InvoiceQuery.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.TaxDateTime, OrderBy.OrderDirection.Ascending),
												   new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Ascending));

				InvoiceSet invoiceSet = new InvoiceSet(InvoiceQuery);
				string invoiceItemVatCodeToString = "";
				string itemDescription = "";

				// Sales Invoices
				foreach (Invoice invoice in invoiceSet)
				{
					foreach (InvoiceItem invoiceItem in invoice.Items)
					{
						invoiceItemVatCodeToString = invoiceItem.VatCode.ToString();
						itemDescription = GetAsciiOnly(invoiceItem.Description, GetAsciiOptions.SageApprovedChars).Replace(DELIMITER, ";");

						if (!invoice.VatCode.Equals(Invoice.VATCodes.T1))
							invoiceItemVatCodeToString = invoice.VatCode.ToString();

						sb.Append(BANK_RECEIPT_TRANSACTION_TYPE);
						sb.Append(DELIMITER);
						sb.Append("1220");
						sb.Append(DELIMITER);
						sb.Append(invoiceItem.NominalCode);
						sb.Append(DELIMITER);
						sb.Append(DEPT_NBR);
						sb.Append(DELIMITER);
						// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
						sb.Append(invoice.TaxDateTime.ToString("dd/MM/yy"));
						sb.Append(DELIMITER);
						sb.Append(BANK_RECEIPT_K_PREFIX);							// Prefix + K.ToString() <= 8 characters
						sb.Append(invoice.K.ToString("000000"));					// Up to 6 digit K number. for K of 10^6 or greater, reduce the prefix size	
						sb.Append(DELIMITER);
						sb.Append(CropString(itemDescription, DESC_MAX_LENGTH));
						sb.Append(DELIMITER);
						sb.Append(invoiceItem.Price.ToString("0.00"));
						sb.Append(DELIMITER);
						sb.Append(invoiceItemVatCodeToString);
						sb.Append(DELIMITER);
						sb.Append(invoiceItem.Vat);
						sb.Append(NEWLINE);
					}
				}
				#endregion
			}
			else if (type.Equals(ExportToSageType.PrepaidIncome))
			{
				#region PrepaidIncome
				// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
				Query InvoiceQuery = new Query(new And(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
													   new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, endDate)));

				InvoiceSet invoiceSet = new InvoiceSet(InvoiceQuery);

				List<NameValueCollection> MonthsNominalcodesMoney = new List<NameValueCollection>();
				MonthsNominalcodesMoney.Add(new NameValueCollection());

				decimal sumPriceAll = 0;
				NameValueCollection sumPriceNominalCode = new NameValueCollection();

				foreach (Invoice invoice in invoiceSet)
				{
					// Sum for each month, but dividing item price over its revenue period
					foreach (InvoiceItem invoiceItem in invoice.Items)
					{
						sumPriceAll += Math.Round(invoiceItem.Price, 2);

						sumPriceNominalCode.Set(invoiceItem.NominalCode.ToString(), ((decimal)(invoiceItem.Price + Convert.ToDecimal(sumPriceNominalCode[invoiceItem.NominalCode.ToString()]))).ToString());

						var revenueMonthSpread = SpreadRevenueOverMonths(invoiceItem);
						DateTime sageStartDate = startDate;

						


						DateTime monthOfInvoiceItemRevenueStartDate = new DateTime(invoiceItem.RevenueStartDate.Year, invoiceItem.RevenueStartDate.Month, 1);
						DateTime monthOfInvoiceItemRevenueEndDate = new DateTime(invoiceItem.RevenueEndDate.Year, invoiceItem.RevenueEndDate.Month, 1);
						DateTime monthOfReportStartDate = new DateTime(startDate.Year, startDate.Month, 1);

						DateTime earliestMonth = monthOfReportStartDate < monthOfInvoiceItemRevenueStartDate ? monthOfReportStartDate : monthOfInvoiceItemRevenueStartDate;
						DateTime latestMonth = monthOfReportStartDate > monthOfInvoiceItemRevenueEndDate ? monthOfReportStartDate : monthOfInvoiceItemRevenueEndDate;

						int outputCounter = 0;
						int revenueMonthSpreadCounter = 0;
						for (DateTime thisMonth = earliestMonth; thisMonth <= latestMonth; thisMonth = thisMonth.AddMonths(1))
						{
							if (MonthsNominalcodesMoney.Count <= outputCounter || MonthsNominalcodesMoney[outputCounter] == null)
								MonthsNominalcodesMoney.Add(new NameValueCollection());

							if (thisMonth >= monthOfInvoiceItemRevenueStartDate && thisMonth <= monthOfInvoiceItemRevenueEndDate)
							{
								MonthsNominalcodesMoney[outputCounter].Set(invoiceItem.NominalCode.ToString(), ((double)((double)revenueMonthSpread[revenueMonthSpreadCounter] + Convert.ToDouble(MonthsNominalcodesMoney[outputCounter][invoiceItem.NominalCode.ToString()]))).ToString());
								revenueMonthSpreadCounter++;
							}

							if (thisMonth >= monthOfReportStartDate)
								outputCounter++;
						}

						
						
						//while (invoiceItem.RevenueStartDate.Month > sageStartDate.Month || invoiceItem.RevenueStartDate.Year > sageStartDate.Year)
						//{
						//    sageStartDate = sageStartDate.AddMonths(1);
						//    if (MonthsNominalcodesMoney.Count <= counter || MonthsNominalcodesMoney[counter] == null)
						//        MonthsNominalcodesMoney.Add(new NameValueCollection());
						//    counter++;
						//}

						//foreach (double d in revenueMonthSpread)
						//{
						//    if (MonthsNominalcodesMoney.Count <= counter || MonthsNominalcodesMoney[counter] == null)
						//        MonthsNominalcodesMoney.Add(new NameValueCollection());
						//    MonthsNominalcodesMoney[counter].Set(invoiceItem.NominalCode.ToString(), ((double)(d + Convert.ToDouble(MonthsNominalcodesMoney[counter][invoiceItem.NominalCode.ToString()]))).ToString());
						//    counter++;
						//}
					}
				}

				// First JC credit the code 2110 for sum of all invoices created in the month. 
				if (Math.Round(sumPriceAll, 2) >= 0)
					sb.Append(JOURNAL_CREDIT_TRANSACTION_TYPE);
				else
					sb.Append(JOURNAL_DEBIT_TRANSACTION_TYPE);
				sb.Append(DELIMITER);
				sb.Append(DELIMITER);									// null between delimiters
				sb.Append("2110");
				sb.Append(DELIMITER);
				sb.Append(DEPT_NBR);
				sb.Append(DELIMITER);
				sb.Append(startDate.ToString("dd/MM/yy"));
				sb.Append(DELIMITER);
				sb.Append(startDate.Month.ToString("00"));
				sb.Append(startDate.ToString("yy"));
				sb.Append(DELIMITER);
				sb.Append(PREPAID_INCOME_PREFIX);
				sb.Append(startDate.ToString("MM"));
				sb.Append(@"/");
				sb.Append(startDate.ToString("yy"));
				sb.Append(" - 2110");
				sb.Append(DELIMITER);
				sb.Append(Convert.ToDouble(Math.Abs(sumPriceAll)).ToString("0.00"));
				sb.Append(DELIMITER);
				sb.Append(InvoiceItem.VATCodes.T9.ToString());
				sb.Append(DELIMITER);
				sb.Append("0");
				sb.Append(NEWLINE);


				// Then JD debit the sum of each nominal code for all invoices created in the month
				string[] sumPriceNominalCodeAllKeys = sumPriceNominalCode.AllKeys;
				// Sort by Nominal Code Ascending
				Array.Sort(sumPriceNominalCodeAllKeys);

				for (int i = 0; i < sumPriceNominalCodeAllKeys.Length; i++)
				{
					double sumPrice = Convert.ToDouble(sumPriceNominalCode[sumPriceNominalCodeAllKeys[i]]);
					if (Math.Round(sumPrice, 2) >= 0)
						sb.Append(JOURNAL_DEBIT_TRANSACTION_TYPE);
					else
						sb.Append(JOURNAL_CREDIT_TRANSACTION_TYPE);
					sb.Append(DELIMITER);
					sb.Append(DELIMITER);									// null between delimiters
					sb.Append(sumPriceNominalCodeAllKeys[i]);
					sb.Append(DELIMITER);
					sb.Append(DEPT_NBR);
					sb.Append(DELIMITER);
					sb.Append(startDate.ToString("dd/MM/yy"));
					sb.Append(DELIMITER);
					sb.Append(startDate.Month.ToString("00"));
					sb.Append(startDate.ToString("yy"));
					sb.Append(DELIMITER);
					sb.Append(PREPAID_INCOME_PREFIX);
					sb.Append(startDate.ToString("MM"));
					sb.Append(@"/");
					sb.Append(startDate.ToString("yy"));
					sb.Append(" - ");
					sb.Append(sumPriceNominalCodeAllKeys[i]);
					sb.Append(DELIMITER);
					sb.Append(Convert.ToDouble(Math.Abs(sumPrice)).ToString("0.00"));
					sb.Append(DELIMITER);
					sb.Append(InvoiceItem.VATCodes.T9.ToString());
					sb.Append(DELIMITER);
					sb.Append("0");
					sb.Append(NEWLINE);
				}


				// Now JD debit the code 2110 for sum of all revenues for each month individually.
				for (int i = 0; i < MonthsNominalcodesMoney.Count; i++)
				{
					double sum = 0;
					for (int j = 0; j < MonthsNominalcodesMoney[i].Count; j++)
					{
						sum += Convert.ToDouble(MonthsNominalcodesMoney[i][j]);
					}

					if (Math.Round(sum, 2) != 0)
					{
						if (Math.Round(sum, 2) > 0)
							sb.Append(JOURNAL_DEBIT_TRANSACTION_TYPE);
						else
							sb.Append(JOURNAL_CREDIT_TRANSACTION_TYPE);
						sb.Append(DELIMITER);
						sb.Append(DELIMITER);									// null between delimiters
						sb.Append("2110");
						sb.Append(DELIMITER);
						sb.Append(DEPT_NBR);
						sb.Append(DELIMITER);
						sb.Append(startDate.AddMonths(i).ToString("dd/MM/yy"));
						sb.Append(DELIMITER);
						sb.Append(startDate.AddMonths(i).Month.ToString("00"));
						sb.Append(startDate.AddMonths(i).ToString("yy"));
						sb.Append(DELIMITER);
						sb.Append(PREPAID_INCOME_PREFIX);
						sb.Append(startDate.AddMonths(i).ToString("MM"));
						sb.Append(@"/");
						sb.Append(startDate.AddMonths(i).ToString("yy"));
						sb.Append(" - 2110");
						sb.Append(DELIMITER);
						sb.Append(Convert.ToDouble(Math.Abs(sum)).ToString("0.00"));
						sb.Append(DELIMITER);
						sb.Append(InvoiceItem.VATCodes.T9.ToString());
						sb.Append(DELIMITER);
						sb.Append("0");
						sb.Append(NEWLINE);
					}

					// Then JC credit each nominal code sum prices for that nominal codes for each month individually.
					string[] MonthsNominalcodesMoneyAllKeys = MonthsNominalcodesMoney[i].AllKeys;
					// Sort by Nominal Code Ascending
					Array.Sort(MonthsNominalcodesMoneyAllKeys);

					double monthNominalCodeAmount = 0;
					for (int j = 0; j < MonthsNominalcodesMoneyAllKeys.Length; j++)
					{
						monthNominalCodeAmount = Convert.ToDouble(MonthsNominalcodesMoney[i][MonthsNominalcodesMoneyAllKeys[j]]);
						if (Math.Round(monthNominalCodeAmount, 2) != 0)
						{
							if (Math.Round(monthNominalCodeAmount, 2) > 0)
								sb.Append(JOURNAL_CREDIT_TRANSACTION_TYPE);
							else
								sb.Append(JOURNAL_DEBIT_TRANSACTION_TYPE);
							sb.Append(DELIMITER);
							sb.Append(DELIMITER);									// null between delimiters
							sb.Append(MonthsNominalcodesMoneyAllKeys[j]);
							sb.Append(DELIMITER);
							sb.Append(DEPT_NBR);
							sb.Append(DELIMITER);
							sb.Append(startDate.AddMonths(i).ToString("dd/MM/yy"));
							sb.Append(DELIMITER);
							sb.Append(startDate.AddMonths(i).Month.ToString("00"));
							sb.Append(startDate.AddMonths(i).ToString("yy"));
							sb.Append(DELIMITER);
							sb.Append(PREPAID_INCOME_PREFIX);
							sb.Append(startDate.AddMonths(i).ToString("MM"));
							sb.Append(@"/");
							sb.Append(startDate.AddMonths(i).ToString("yy"));
							sb.Append(" - ");
							sb.Append(MonthsNominalcodesMoneyAllKeys[j]);
							sb.Append(DELIMITER);
							sb.Append(Convert.ToDouble(Math.Abs(monthNominalCodeAmount)).ToString("0.00"));
							sb.Append(DELIMITER);
							sb.Append(InvoiceItem.VATCodes.T9.ToString());
							sb.Append(DELIMITER);
							sb.Append("0");
							sb.Append(NEWLINE);
						}
					}
				}
				#endregion
			}
			else if (type.Equals(ExportToSageType.SalesReceipts))
			{
				#region SalesReceipts (InvoiceTransfer)
				Query TransferQuery	= new Query(new And(new Q(Transfer.Columns.DateTimeComplete, QueryOperator.GreaterThanOrEqualTo, startDate),
														new Q(Transfer.Columns.DateTimeComplete, QueryOperator.LessThan, endDate),
														new Q(Transfer.Columns.PromoterK, QueryOperator.GreaterThan, 0),
														new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success)));
				TransferQuery.OrderBy = new OrderBy(Transfer.Columns.DateTimeComplete, OrderBy.OrderDirection.Ascending);

				//TransferQuery.TableElement = new JoinLeft(Transfer.Columns.K, InvoiceTransfer.Columns.TransferK);

				TransferSet transferSet = new TransferSet(TransferQuery);
				
				// Transfers
				foreach (Transfer t in transferSet)
				{
					if (t.InvoiceTransfers.Count == 0)
						appendTransferAndInvoiceTransfer(sb, t, null);
					else
					{
						foreach (InvoiceTransfer it in t.InvoiceTransfers)
							appendTransferAndInvoiceTransfer(sb, t, it);
					}
				}
				#endregion
			}
			else if (type.Equals(ExportToSageType.CampaignCredits))
			{
				#region CampaignCredit
				Query q = new Query(new And(new Q(CampaignCredit.Columns.ActionDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
											new Q(CampaignCredit.Columns.ActionDateTime, QueryOperator.LessThan, endDate),
											new Q(CampaignCredit.Columns.Enabled, true)));
				q.OrderBy = new OrderBy(
					new OrderBy(CampaignCredit.Columns.ActionDateTime,OrderBy.OrderDirection.Ascending),
					new OrderBy(CampaignCredit.Columns.DisplayOrder,OrderBy.OrderDirection.Ascending),
					new OrderBy(CampaignCredit.Columns.K, OrderBy.OrderDirection.Ascending));

				CampaignCreditSet ccs = new CampaignCreditSet(q);

				// Transfers
				foreach (CampaignCredit cc in ccs)
				{
					sb.Append(CAMPAIGN_CREDIT_TRANSACTION_TYPE);
					sb.Append(DELIMITER);
					sb.Append(cc.PromoterK);
					sb.Append(DELIMITER);
					sb.Append(cc.BuyableObjectType.ToString());

					//Event
					//Banner
					//GuestlistCredit
					//CampaignCredit
					//Invoice

					sb.Append(DELIMITER);
					sb.Append(cc.InvoiceItemType.ToString());

					//Misc
					//EventDonate
					//BannerTop
					//BannerHotbox
					//BannerPhoto
					//BannerEmail
					//BannerSkyscraper
					//DesignBannerJpg
					//DesignBannerAnimatedGif
					//DesignBannerFlash
					//CampaignCredits

					//Combinations:
					//Event - EventDonate (event hilights) [-ve]
					//Banner - Misc (campaign credit refunds for underperforming banners) [+ve]
					//Banner - BannerTop, BannerHotbox, BannerPhoto, BannerEmail or BannerSkyscraper (booking banners, occasionally adjustments) [-ve, occasionally +ve]
					//Banner - DesignBannerJpg, DesignBannerAnimatedGif or DesignBannerFlash (booking banner design) [-ve]
					//GuestlistCredit - Misc (refunds of old guestlist credit balances as campaign credits) [+ve]
					//CampaignCredits - Misc (occasional adjustmants) [+ve and -ve]
					//Invoice - Misc (bulk purchased campaign credits or adjustmants) [+ve and -ve]
					//Invoice - CampaignCredits (instant banner purchases, 2-for-1 offer on ticket funds, manual invoices) [+ve and -ve]

					sb.Append(DELIMITER);
					if (cc.BuyableObjectType == Model.Entities.ObjectType.Invoice)
					{
						try
						{
							if (cc.BuyableObject != null)
							{
								sb.Append(INVOICE_K_PREFIX);
								sb.Append(cc.BuyableObjectK.ToString("000000"));
							}
						}
						catch { }
					}
					sb.Append(DELIMITER);
					sb.Append(cc.ActionDateTime.ToString("dd/MM/yy"));
					sb.Append(DELIMITER);
					sb.Append(cc.Credits.ToString());
					sb.Append(NEWLINE);
				}
				#endregion
			}

			return sb.ToString();
		}
		static void appendTransferAndInvoiceTransfer(StringBuilder sb, Transfer t, InvoiceTransfer it)
		{
			sb.Append(SALES_RECEIPT_TRANSACTION_TYPE);
			sb.Append(DELIMITER);
			sb.Append(t.PromoterK);
			sb.Append(DELIMITER);
			sb.Append(t.Type.ToString());
			sb.Append(DELIMITER);
			sb.Append(t.Method.ToString());
			sb.Append(DELIMITER);
			sb.Append(t.DateTimeComplete.ToString("dd/MM/yy"));
			sb.Append(DELIMITER);
			sb.Append(t.Amount.ToString("0.00"));
			sb.Append(DELIMITER);
			sb.Append(TRANSFER_K_PREFIX);							// Prefix + K.ToString() <= 8 characters
			sb.Append(t.K.ToString("000000"));				        // Up to 6 digit K number. for K of 10^6 or greater, reduce the prefix size
			if (it != null)
			{
				sb.Append(DELIMITER);
				sb.Append(INVOICE_K_PREFIX);
				sb.Append(it.InvoiceK.ToString("000000"));
				sb.Append(DELIMITER);
				sb.Append(it.Amount.ToString("0.00"));
			}
			else
			{
				sb.Append(DELIMITER);
				sb.Append(DELIMITER);
			}
			sb.Append(NEWLINE);
		}

		private static decimal[] SpreadRevenueOverMonths(InvoiceItem invoiceItem)
		{
			invoiceItem.RevenueEndDate = invoiceItem.RevenueEndDate < invoiceItem.RevenueStartDate ? invoiceItem.RevenueStartDate : invoiceItem.RevenueEndDate;

			// Set both DateTimes to the start of their respective days. This allows for easier calculations.
			invoiceItem.RevenueStartDate = new DateTime(invoiceItem.RevenueStartDate.Year, invoiceItem.RevenueStartDate.Month, invoiceItem.RevenueStartDate.Day);
			invoiceItem.RevenueEndDate = new DateTime(invoiceItem.RevenueEndDate.Year, invoiceItem.RevenueEndDate.Month, invoiceItem.RevenueEndDate.Day);

			int nbrOfMonths = (invoiceItem.RevenueEndDate.Year * 12 + invoiceItem.RevenueEndDate.Month) - (invoiceItem.RevenueStartDate.Year * 12 + invoiceItem.RevenueStartDate.Month) + 1;

			decimal[] priceSpreadOverMonths = new decimal[nbrOfMonths];//remember this array starts from the start date...
			// Have to add 1 day, cause EndDate also counts as 1 day
			TimeSpan spanOfDays = invoiceItem.RevenueEndDate.AddDays(1) - invoiceItem.RevenueStartDate;

			int startDayInMonth = invoiceItem.RevenueStartDate.Day;
			DateTime endOfMonth = new DateTime(invoiceItem.RevenueStartDate.Year, invoiceItem.RevenueStartDate.Month, 1).AddMonths(1).AddMilliseconds(-1);

			int counter = 0;
			bool isFinalMonth = false;
			//DateTime revEndDatePlusOneMonth = invoiceItem.RevenueEndDate.AddMonths(1);
			while (endOfMonth <= invoiceItem.RevenueEndDate.AddDays(1).AddMonths(1).AddDays(-1))
			{
				if (endOfMonth > invoiceItem.RevenueEndDate)
				{
					endOfMonth = invoiceItem.RevenueEndDate;
					isFinalMonth = true;
				}

				// Have to add 1 day, cause StartDate also counts as 1 day
				priceSpreadOverMonths[counter] = Math.Round((endOfMonth.Day - startDayInMonth + 1) * invoiceItem.Price / spanOfDays.Days, 2);

				if (isFinalMonth)
					break;

				// Get next end of month
				endOfMonth = endOfMonth.AddDays(1).AddMonths(1).AddDays(-1);
				startDayInMonth = 1;
				counter++;
			}

			decimal sum = 0;
			foreach (decimal d in priceSpreadOverMonths)
			{
				sum += d;
			}

			// If there are any pennies left over from rounding, then adjust the first month accordingly.
			priceSpreadOverMonths[0] += Math.Round(invoiceItem.Price - sum, 2);

			return priceSpreadOverMonths;
		}

		#endregion

		#region Export GridView to CSV
		public static void ExportGridViewToCsv(GridView gridView, HttpResponse response)
		{
			const string m_Delimiter_Column = ",";
			string m_Delimiter_Row = Environment.NewLine;

			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

			gridView.RenderControl(htmlWrite);
			string gridViewText = stringWrite.ToString();

			Regex m_RegEx = new Regex(@"(>\s+<)", RegexOptions.IgnoreCase);
			gridViewText = m_RegEx.Replace(gridViewText, "><");


			gridViewText = gridViewText.Replace(m_Delimiter_Row, String.Empty);
			gridViewText = gridViewText.Replace("</td></tr>", m_Delimiter_Row);
			gridViewText = gridViewText.Replace("</th></tr>", m_Delimiter_Row);
			gridViewText = gridViewText.Replace("<tr><td>", String.Empty);
			gridViewText = gridViewText.Replace("<tr><th>", String.Empty);
			gridViewText = gridViewText.Replace(m_Delimiter_Column, "\\" + m_Delimiter_Column);
			gridViewText = gridViewText.Replace("</th><th", m_Delimiter_Column + "<th");
			gridViewText = gridViewText.Replace("</td><td", m_Delimiter_Column + "<td");

			m_RegEx = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
			gridViewText = m_RegEx.Replace(gridViewText, String.Empty);

			//gridViewText = HttpUtility.HtmlDecode(gridViewText);
			gridViewText = gridViewText.Replace("\t", "");

			gridViewText = Utilities.GetAsciiOnly(gridViewText);

			response.ContentType = "application/csv";
			response.Write(gridViewText);
			response.End();
		}

		public static void ExportGridViewToExcel(GridView gridView, HttpResponse response)
		{
			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

			gridView.Width = Unit.Empty;
			gridView.RenderControl(htmlWrite);

			response.ContentType = "application/vnd.xls";	
			response.Write(Utilities.GetAsciiOnly(stringWrite.ToString()));
			response.End();
		}
		#endregion

		#region IBobReport Methods
		public static HtmlTextWriter GenerateReportHtmlTextWriter(bool linksEnabled, IBobReport iBobReportObject)
		{
			return new HtmlTextWriter(new StringWriter(iBobReportObject.GenerateReportStringBuilder(linksEnabled)));
		}   

		public static MemoryStream GenerateReportMemoryStream(bool linksEnabled, IBobReport iBobReportObject)
		{
			// Convert string to Stream
			string report = iBobReportObject.GenerateReportStringBuilder(linksEnabled).ToString();
			byte[] b = new byte[report.Length];
			Encoding.Default.GetBytes(report.ToCharArray(), 0, report.Length, b, 0);

			return new MemoryStream(b);
		}

		public static void DownloadAsWord(IBobReport iBobReportObject, string fileName, HttpResponse response)
		{
			response.Clear();

			response.AddHeader("content-disposition", "attachment; filename=" + fileName);

			System.IO.StringWriter stringWrite = new System.IO.StringWriter(iBobReportObject.GenerateReportStringBuilder(false));
			System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

			string output = HttpUtility.HtmlDecode(stringWrite.ToString());

			response.ContentType = "application/ms-word";
			response.Write(output);
			response.End();
		}
		#endregion

		#region Automated Emails
		#region GenerateHTMLHeaderRowString
		public static string GenerateHTMLHeaderRowString(string headerTitle)
		{
			return GenerateHTMLHeaderRowString(headerTitle, true);
		}

		public static string GenerateHTMLHeaderRowString(string headerTitle, bool includePostalDetails)
		{
			return @"<tr>
						<td align='left' valign='middle' width='300'>
							<table cellpadding='0' cellspacing='0'>
								<tr>
									<td style='padding-right:20px;'><img src='http://www.dontstayin.com/gfx/dsi-60.gif' align='left' valign='bottom' /></td>
									<td valign='top'><font size=1>" + (includePostalDetails ? Vars.DSI_POSTAL_DETAILS_HTML : "&nbsp;") + @"</font></td>
								</tr>
							</table>
							<td colspan=3 align='right' valign='middle'><h1>" + headerTitle.Replace(" ", "&nbsp;") + @"</h1></td>
						</tr>
						<tr><td colspan=4 valign='top'><hr><br><br></td></tr>";
		}
		#endregion

		#region GenerateHTMLFooterRowString
		public static string GenerateHTMLFooterRowString()
		{
			return GenerateHTMLFooterRowString(true);
		}
		public static string GenerateHTMLFooterRowString(bool showContactDetails)
		{
			return @"<tr><td valign='bottom'><hr>
						<table width='100%' cellpadding='0' cellspacing='0'><tr>" + (showContactDetails ? 
							@"<td style='height:1' valign='bottom'><font size=1><b>T</b>:&nbsp;&nbsp;<br><b>F</b>:&nbsp;&nbsp;<br><br><b>E</b>:&nbsp;&nbsp;</font></td>
							<td style='height:1' valign='bottom'><font size=1>0207 835 5599<br>020 7099 5886<br><br><a href='mailto:accounts@dontstayin.com'>accounts@dontstayin.com</a></font></td>" : "") + 
							@"<td style='height:1' align='right' valign='bottom'><font size=1>" + Vars.DSI_VATREG_DETAILS_HTML + @"<br>All business is undertaken subject to our standard terms and conditions</font></td></tr>
								</table></td></tr>";
		}
		#endregion

		public static string TimAylottSignatureHTML()
		{
			return @"<br>&nbsp;<br>
					<div style='border:solid 0px #000000;background-color:#333333;color:#ffffff;font-weight:bold;padding-top:2px;padding-bottom:2px;padding-left:4px;padding-right:4px;font-size:12px;'>The World's Largest Online Clubbing Community</div>
					<div style='padding:5px 8px 8px 8px;border:solid 1px #000000;margin-bottom:13px;background-color:FECA26;line-height:140%;'>
							<a href='http://www.dontstayin.com/'><img src='http://www.dontstayin.com/gfx/dsi-60.gif' width='142' height='60' border='0' align='right' style='margin:4px 0px 0px 0px;'></a>
							<b style='font-size:x-small;line-height:18px;'>Tim Aylott</b><br>
							<a href='tim@dontstayin.com'>tim@dontstayin.com</a><br>
							Tel: +44 (0)20 7835 5599<br>
						Fax: +44 (0)20 7099 5886
					</div>
					<style>
					.{
						font-family: Verdana, sans-serif;
					}
					div.ContentBorder
					{
						padding:5px 8px 8px 8px;
						border:solid 1px #000000;
						margin-bottom:13px;
						background-color:FECA26;
						line-height:140%;
					}
					input, select, textarea, button, p, td, th, li, h1, h2, h3, div
					{
						font-family: Verdana, sans-serif;
						font-size:xx-small;
					}
					h1
					{
						margin-bottom:5px;
						margin-top:10px;
						font-size:small;
						color:#000000;
						line-height:130%;
					}
					h2
					{
						margin-bottom:7px;
						margin-top:10px;
						font-size:xx-small;
						font-weight:bold;
						color:#000000;
						line-height:130%;
					}
					a:link 
					{
						color:#000000;
					}
					a:visited
					{
						color:#000000;
					}
					a:hover 
					{
						color:#ff0000; 
						TEXT-DECORATION: none;
					}
					</style>";
		}
		
		public static void EmailPromotersListingsReminder()
		{
			Console.WriteLine("Utilities.EmailPromotersListingsReminder();");


			Query q = new Query();
			q.QueryCondition = new Q(Promoter.Columns.Status, QueryOperator.NotEqualTo, Promoter.StatusEnum.Disabled);
			if (Vars.DevEnv)
				q.TopRecords = 10;
			PromoterSet bs = new PromoterSet(q);
			List<int> ks = new List<int>();

			for (int count = 0; count < bs.Count; count++)
			{
				Promoter c = bs[count];

				try
				{
					foreach (Usr u in c.AdminUsrs)
					{
						if (!ks.Contains(u.K))
						{
							ks.Add(u.K);
							
							Mailer mailer = new Mailer();
							mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
							mailer.Subject = "DSI LISTINGS DEADLINE - Wednesday 9AM! Get your events mailed to 200,000 clubbers for FREE!";

							mailer.Body = @"<p>Club promoters!</p>
<p>Make sure you have your events listed on Don't Stay In <b>before Wednesday at 9AM</b>. If you get your event listed before then, it will go into our weekly round-up of events being sent out to 200,000 raving revellers.</p>
<p>If you want your event included in this week's FREE mailer, then all you need to do is <a href=""[LOGIN(/pages/events/edit)]"">list your event here</a>.</p>
<p>It's free and only takes 2 minutes. You can always come back later if you want to tart it up a bit.</p>
<p>Cheers,</p>
<p>The DSI Team</p>";
							mailer.RedirectUrl = "/pages/events/edit";
							mailer.UsrRecipient = u;
							mailer.Send();
							Console.WriteLine("sent to {0}", u.Email);

						}
					}

					//if (count % 10 == 0)
					Console.WriteLine("Done " + count + "/" + bs.Count + " " + c.UrlName, 2);

				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception " + count + "/" + bs.Count + " - " + ex.ToString(), 3);
				}

				bs.Kill(count);

			}

			Console.WriteLine("All done!");
			Console.ReadLine();
		}

		#region Email All Promoters
		/// <summary>
		/// Sends email to all admin users for each promoter account that has a negative balance.
		/// </summary>
		/// <param name="monthYear">Month and year of statement to link to in email</param>
		public static void EmailAllPromoterOutstandingStatements(DateTime monthYear)
		{
			// Make datetime the last day (23:59:59.999) of the given month
			DateTime lastDayOfMonthYear = Utilities.GetEndOfMonth(monthYear);

			try
			{
				// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
				Query promoterOutstandingQuery = new Query(
					new And(
						new Or(
							new And(
								new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
								new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThanOrEqualTo, lastDayOfMonthYear),
								new Q(Invoice.Columns.Paid, false)),
							new And(new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment),
								new Q(Transfer.Columns.IsFullyApplied, false),
								new Q(Transfer.Columns.DateTimeCreated, QueryOperator.LessThanOrEqualTo, lastDayOfMonthYear),
								new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success)),
							new And(new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund),
								new Q(Transfer.Columns.TransferRefundedK, 0),
								new Q(Transfer.Columns.DateTimeCreated, QueryOperator.LessThanOrEqualTo, lastDayOfMonthYear),
								new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
									new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success))
								)
							)
						),
						new Q(Promoter.Columns.SuspendReminderEmails, false
					)
				);
				promoterOutstandingQuery.TableElement = new Join(Promoter.Columns.K, Invoice.Columns.PromoterK, QueryJoinType.Left);
				promoterOutstandingQuery.TableElement = new Join(promoterOutstandingQuery.TableElement, new TableElement(TablesEnum.Transfer), QueryJoinType.Left, Promoter.Columns.K, Transfer.Columns.PromoterK);
				promoterOutstandingQuery.Distinct = true;
				promoterOutstandingQuery.DistinctColumn = Promoter.Columns.K;

				PromoterSet promoters = new PromoterSet(promoterOutstandingQuery);

				foreach (Promoter promoter in promoters)
				{
					if (promoter.SuspendReminderEmails)
					{
						continue;
					}
					try
					{
						// first try to apply any available money to unpaid invoices.
						promoter.ApplyAvailableMoneyToUnpaidInvoices();
						// Dont need to send statements to promoters with balance == zero
						decimal promoterBalance = promoter.GetBalance();
						// We only send out statements to promoters who owe us money, as per Gee 15/11/06
						if (Math.Round(promoterBalance, 2) < 0)
						{
							Mailer mailer = new Mailer();
							mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
							mailer.Subject = "DontStayIn statement of outstanding funds for " + monthYear.ToString("MMM yyyy") + " for " + promoter.Name;

							mailer.Body = "<h2>DontStayIn statement of outstanding funds for " + monthYear.ToString("MMM yyyy") + "</h2>";
							mailer.Body += promoter.LinkEmailFull;
							mailer.Body += "<p><b>Account balance: " + Utilities.MoneyToHTML(Math.Abs(promoterBalance)) + "</b></p>";
							mailer.Body += @"<p><a href=""[LOGIN(" + promoter.UrlStatementReport(monthYear) + "\")]>Statement for " + monthYear.ToString("MMM yyyy") + "</a></p>";
							mailer.Body += @"<p><a href=""[LOGIN(" + promoter.UrlApp("invoices", "pay", "true") + "\")]>Please pay invoices now</a>.</p>";

							mailer.Attachments.Add(new System.Net.Mail.Attachment(promoter.GenerateMonthlyStatementMemoryStream(monthYear.Month, monthYear.Year, false), "DontStayIn Statement for " + monthYear.ToString("MMM yyyy") + ".doc", "application/word"));

							if (!Vars.DevEnv)
							{
								foreach (Usr usr in promoter.AdminUsrs)
								{
									try
									{
										mailer.UsrRecipient = usr;
										mailer.Send();
									}
									catch (Exception ex)
									{
										string additionalDetails = "Occurred in Utilities.EmailAllPromoterOutstandingStatements(): Usr K= ";
										if (usr != null)
											additionalDetails += usr.K.ToString();
										else
											additionalDetails += "null";
										List<IBobAsHTML> bobsAsHtml = new List<IBobAsHTML>();
										bobsAsHtml.Add(promoter);
										bobsAsHtml.Add(usr);
										EmailException(ex, additionalDetails, bobsAsHtml);
									}
								}
							}
							if (!Vars.DevEnv && promoter.AccountsEmail != null && promoter.AccountsEmail.Length > 0)
							{
								try
								{
									string body = "";
									body = "<h2>DontStayIn statement of outstanding funds for " + monthYear.ToString("MMM yyyy") + "</h2>";
									body += @"<p>Account: " + promoter.Name + " - <b>Account balance: " + Utilities.MoneyToHTML(Math.Abs(promoterBalance)) + "</b></p>";
									body += @"<p>Statement for " + monthYear.ToString("MMM yyyy") + "</p>";
									body += @"<p>Please pay invoices now.</p>";

									Utilities.EmailToNonUser(promoter.AccountsEmail, mailer.Subject, body, mailer.Attachments.ToArray());
								}
								catch (Exception ex)
								{
									string additionalDetails = "Occurred in Utilities.EmailAllPromoterOutstandingStatements(): Promoter.AccountsEmail = " + promoter.AccountsEmail;
									EmailException(ex, additionalDetails, promoter);
								}
							}

							// Change for internal use
							mailer.UsrRecipient = null;
							mailer.TemplateType = Mailer.TemplateTypes.AdminNote;

							if (Vars.DevEnv)
								mailer.Subject = "TEST - " + mailer.Subject;
							mailer.To = "accounts@dontstayin.com";
							mailer.Send();
						}
					}
					catch (Exception ex)
					{
						string additionalDetails = "Occurred in Utilities.EmailAllPromoterOutstandingStatements(): Promoter K= ";
						if (promoter != null)
							additionalDetails += promoter.K.ToString();
						else
							additionalDetails += "null";
						EmailException(ex, additionalDetails, promoter);
					}
				}
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailAllPromoterOutstandingStatements()");
			}
		}

		public static void EmailAllPromoterOutstandingInvoices()
		{
			// Email outstanding invoices that 7 days before due

			// Start date = today (00:00:00) + 7 days. For invoice due in 7 days
			DateTime fromDueDate = DateTime.Today.AddDays(7);

			// End date = today (23:59:59.999) + 7 days. For invoice due in 7 days
			DateTime toDueDate = fromDueDate.AddDays(1).AddMilliseconds(-1);

			EmailUnpaidPromotersInvoicesToPromotersAndAccounts(fromDueDate, toDueDate);
		}

		public static void EmailAllPromoterOverdueInvoices()
		{
			// Email overdue invoices

			// Where DueDateTime is from 1/1/1980 up til today (00:00:00)
			DateTime fromDueDate = new DateTime(1980, 1, 1);
			DateTime toDueDate = DateTime.Today;

			EmailUnpaidPromotersInvoicesToPromotersAndAccounts(fromDueDate, toDueDate);
		}

		private static void EmailUnpaidPromotersInvoicesToPromotersAndAccounts(DateTime fromDueDate, DateTime toDueDate)
		{
			try
			{
				// Get all promoterK's who have outstanding invoices within the date range
				Query promoterKQuery = new Query();
				promoterKQuery.QueryCondition = new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
														new Q(Invoice.Columns.Paid, false),
														new Q(Invoice.Columns.PromoterK, QueryOperator.NotEqualTo, 0),
														new Q(Invoice.Columns.DueDateTime, QueryOperator.GreaterThanOrEqualTo, fromDueDate),
														new Q(Invoice.Columns.DueDateTime, QueryOperator.LessThanOrEqualTo, toDueDate));

				promoterKQuery.Columns = new ColumnSet(Invoice.Columns.PromoterK);
				promoterKQuery.GroupBy = new GroupBy(Invoice.Columns.PromoterK);

				InvoiceSet promoterKInvoices = new InvoiceSet(promoterKQuery);

				Promoter promoter = null;
				// Foreach promoter, group together all their outstanding invoices within the date range and send one email with all invoices as attachments
				foreach (Invoice promoterKInvoice in promoterKInvoices)
				{

					try
					{
						promoter = new Promoter(promoterKInvoice.PromoterK);
						bool containsOverdue = false;
						promoter.ApplyAvailableMoneyToUnpaidInvoices();

						Query outstandingInvoicesQuery = new Query(new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
																		   new Q(Invoice.Columns.Paid, false),
																		   new Q(Invoice.Columns.DueDateTime, QueryOperator.GreaterThanOrEqualTo, fromDueDate),
																		   new Q(Invoice.Columns.DueDateTime, QueryOperator.LessThanOrEqualTo, toDueDate),
																		   new Q(Invoice.Columns.PromoterK, promoterKInvoice.PromoterK)));
						outstandingInvoicesQuery.OrderBy = new OrderBy(Invoice.Columns.DueDateTime, OrderBy.OrderDirection.Ascending);

						InvoiceSet outstandingInvoices = new InvoiceSet(outstandingInvoicesQuery);

						Mailer mailer = new Mailer();
						mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;

						decimal totalAmountDue = 0;

						foreach (Invoice invoice in outstandingInvoices)
						{
							totalAmountDue += Math.Round(invoice.AmountDue, 2);
							if (invoice.Paid == false && new DateTime(invoice.DueDateTime.Year, invoice.DueDateTime.Month, invoice.DueDateTime.Day) <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
							{
								containsOverdue = true;
							}
						}

						string nonUserEmailBody = "";
						// If there are no outstanding/overdue invoices, then do not send warning message
						if (outstandingInvoices.Count > 0)
						{
							// For overdue invoices
							if (containsOverdue == true)
							{
								mailer.Subject = "DontStayIn overdue invoices require payment of " + totalAmountDue.ToString("c");
								mailer.Body = "<p>DontStayIn overdue invoices. Amount due " + Utilities.MoneyToHTML(totalAmountDue) + ". Payment required immediately for the following invoices:</p>";

							}
							// For outstanding invoices
							else
							{
                                mailer.Subject = "DontStayIn outstanding invoices require payment of " + totalAmountDue.ToString("c");
								mailer.Body = "<p>DontStayIn outstanding invoices. Amount due " + Utilities.MoneyToHTML(totalAmountDue) + ". Payment required before due date for the following invoices:</p>";
							}

							nonUserEmailBody = mailer.Body + "<p>Promoter: " + promoter.Name + "</p><p>Please pay invoices now</p>";
							mailer.Body += promoter.LinkEmailFull;

							Utilities.AddInvoicesToEmail(mailer, outstandingInvoices);
							mailer.Body += @"<p><a href=""[LOGIN(" + promoter.UrlApp("invoices", "pay", "true") + "\")]>Please pay invoices now</a>.</p>";
							if (!Vars.DevEnv)
							{
								if (!promoter.SuspendReminderEmails)
								{
									foreach (Usr usr in promoter.AdminUsrs)
									{
										mailer.UsrRecipient = usr;
										mailer.RedirectUrl = promoter.UrlApp("invoices");
										mailer.Send();
									}
								}

							}

							if (!Vars.DevEnv && promoter.AccountsEmail != null && promoter.AccountsEmail.Length > 0)
							{
								try
								{
									Utilities.EmailToNonUser(promoter.AccountsEmail, mailer.Subject, nonUserEmailBody, mailer.Attachments.ToArray());
								}
								catch (Exception ex)
								{
									string additionalDetails = "Occurred in Utilities.EmailAllPromoterOutstandingStatements(): Promoter.AccountsEmail = " + promoter.AccountsEmail;
									EmailException(ex, additionalDetails, promoter);
								}
							}

							// Change for internal use
							mailer.UsrRecipient = null;
							mailer.TemplateType = Mailer.TemplateTypes.AdminNote;
							mailer.Subject += " (" + promoter.Name + ")";
							// now send to our accounts email address
							mailer.To = "accounts@dontstayin.com";
							mailer.Send();
						}
					}
					catch (Exception ex)
					{
						string additionalDetails = "Occurred in Utilities.EmailUnpaidPromotersInvoicesToPromotersAndAccounts(): Promoter K= ";
						if (promoterKInvoice != null)
							additionalDetails += promoterKInvoice.PromoterK.ToString();
						else
							additionalDetails += "null";
						EmailException(ex, additionalDetails, promoter);
					}
				}
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailUnpaidPromotersInvoicesToPromotersAndAccounts()");
			}
		}

		public static void EmailAllPromotersWithTicketsAndUnknownVatStatus()
		{
			Query q = new Query(new Or(new Q(Promoter.Columns.VatStatus, Promoter.VatStatusEnum.Unknown),
									   new Q(Promoter.Columns.VatStatus, QueryOperator.IsNull, null)));
			
			q.TableElement = new Join(Promoter.Columns.K, TicketRun.Columns.PromoterK);
			q.Distinct = true;
			q.DistinctColumn = Promoter.Columns.K;
			q.OrderBy = new OrderBy(Promoter.Columns.K);

			PromoterSet promoters = new PromoterSet(q);
			Random r = new Random();
			foreach (Promoter promoter in promoters)
			{
				System.Net.Mail.SmtpClient c = new System.Net.Mail.SmtpClient();
				c.Host = Common.Properties.GetDefaultSmtpServer(r);

				System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

				m.Body = "<p>Promoter: " + promoter.Name + @",<br><br>
								You have recently set up to sell tickets to your event on dontstayin.com, but in order to sell tickets for you correctly, we need to know whether you are registered for VAT.
								<br><br>								 
								It’s really important to let us know as soon as possible your VAT registration status, and if you are registered, your VAT registration number, so that you can continue to sell tickets on the site.
								<br><br>
								If you are not VAT registered, please reply to this email and let us know that this is the case. 
								<br><br>
								Thanks very much for your help.
								<br><br>
								Regards,
								<br><br>
								Tim</p>";
				m.Body += TimAylottSignatureHTML();

				m.From = new System.Net.Mail.MailAddress(Vars.EMAIL_ADDRESS_ACCOUNTS);
				m.IsBodyHtml = true;

				m.Subject = "Promoter: " + promoter.Name + ", VAT status unknown";

				if (promoter.K != 100 && promoter.K != 24)
				{
					m.To.Clear();

					if (Vars.DevEnv || Vars.IsBeta)
					{
						m.Subject = "Test - " + m.Subject;
						m.Subject += " (" + DateTime.Now.ToString() + ")";
						m.To.Add(Vars.EMAIL_ADDRESS_DEV_TEAM);
						m.From = new System.Net.Mail.MailAddress(Vars.EMAIL_ADDRESS_ACCOUNTS);
					}
					else
					{
						foreach (Usr adminUsr in promoter.AdminUsrs)
						{							
							m.To.Add(adminUsr.Email);
						}
						m.CC.Add(Vars.EMAIL_ADDRESS_ACCOUNTS);
					}
					
					c.Send(m);

					Log.Increment(Log.Items.EmailsSent);
				}				
			}
		}

		public static void EmailAccountsTicketFundsReserveAmount()
		{
			double ticketMoney = 0;
			double ticketMoneyCencelled = 0;
			double ticketMoneyWithCardnetDelay = 0;
			double ticketMoneyToPromoterBankAccounts = 0;
			double ticketMoneyAppliedToInvoices = 0;
			double ticketMoneyReserve = 0;

			/* money earnt */
			Query sumTicketMoneyQuery = new Query();
			//sumTicketMoneyQuery.ExtraSelectElements.Add("SumTotalFunds", "SUM([TicketPromoterEvent].[TotalFunds])");
			//sumTicketMoneyQuery.OverideSql = "SELECT SUM(TotalFunds) AS SumAmount FROM TicketPromoterEvent";
			sumTicketMoneyQuery.OverideSql = "SELECT SUM([Ticket].[Price]) AS SumAmount FROM [Ticket] WHERE [Ticket].[Enabled] = 1 AND [Ticket].[Quantity] > 0 AND [Ticket].[Cancelled] = 0 AND [Ticket].[K] >= 82 ";

			Query sumTicketMoneyCancelledQuery = new Query();
			sumTicketMoneyCancelledQuery.OverideSql = "SELECT SUM([Ticket].[Price]) AS SumAmount FROM [Ticket] WHERE [Ticket].[Enabled] = 1 AND [Ticket].[Quantity] > 0 AND [Ticket].[Cancelled] = 1 AND [Ticket].[K] >= 82 ";


			/* money to promoter bank accounts */
			Query sumMoneyToPromoterBankAccountsQuery = new Query();
			sumMoneyToPromoterBankAccountsQuery.OverideSql = @"SELECT SUM(RefundTransfer.Amount) AS SumAmount FROM Transfer PaymentTransfer INNER JOIN Transfer RefundTransfer ON PaymentTransfer.K = RefundTransfer.TransferRefundedK 
																WHERE PaymentTransfer.Method = 5 AND RefundTransfer.Type = 2 AND PaymentTransfer.Type = 1 AND RefundTransfer.Method = 2 AND RefundTransfer.Status = 2";

			/* money applied to DSI invoices */
			Query sumTicketMoneyAppliedToInvoicesQuery = new Query();
			sumTicketMoneyAppliedToInvoicesQuery.OverideSql = "SELECT SUM(IT.Amount) AS SumAmount FROM InvoiceTransfer IT INNER JOIN Transfer T ON IT.TransferK = T.K WHERE T.Method = 5 AND T.Type = 1 AND T.Status = 2";


			/* Addition of CardNet delay to figures, as requested by Gee on Jan 7, 08 */
			Query sumTicketMoneyCardnetDelayQuery = new Query();
			sumTicketMoneyCardnetDelayQuery.OverideSql = "SELECT SUM([Ticket].[Price]) AS SumAmount FROM [Ticket] WHERE [Ticket].[Enabled] = 1 AND [Ticket].[Quantity] > 0 AND ([Ticket].[Cancelled] = 0 OR [Ticket].[CancelledDateTime] >= " + Cambro.Misc.Db.Dt(Utilities.CardnetDelay()) + ") AND [Ticket].[K] >= 82 AND [Ticket].[BuyDateTime] < " + Cambro.Misc.Db.Dt(Utilities.CardnetDelay());


			try
			{
				TicketSet ts = new TicketSet(sumTicketMoneyQuery);
				if (ts.Count == 1)
				{
					ticketMoney = Convert.ToDouble(ts[0].ExtraSelectElements["SumAmount"]);
				}
			}
			catch { }
			try
			{
				TicketSet ts = new TicketSet(sumTicketMoneyCancelledQuery);
				if (ts.Count == 1)
				{
					ticketMoneyCencelled = Convert.ToDouble(ts[0].ExtraSelectElements["SumAmount"]);
				}
			}
			catch { }
			try
			{
				TicketPromoterEventSet tpes = new TicketPromoterEventSet(sumTicketMoneyCardnetDelayQuery);
				if (tpes.Count == 1)
				{
					ticketMoneyWithCardnetDelay = Convert.ToDouble(tpes[0].ExtraSelectElements["SumAmount"]);
				}
			}
			catch { }
			try
			{
				TransferSet trans = new TransferSet(sumMoneyToPromoterBankAccountsQuery);
				if (trans.Count == 1)
				{
					ticketMoneyToPromoterBankAccounts = -1 * Convert.ToDouble(trans[0].ExtraSelectElements["SumAmount"]);
				}
			}
			catch { }
			try
			{
				InvoiceTransferSet its = new InvoiceTransferSet(sumTicketMoneyAppliedToInvoicesQuery);
				if (its.Count == 1)
				{
					ticketMoneyAppliedToInvoices = Convert.ToDouble(its[0].ExtraSelectElements["SumAmount"]);
				}
			}
			catch { }

			ticketMoneyReserve = ticketMoneyWithCardnetDelay - ticketMoneyToPromoterBankAccounts - ticketMoneyAppliedToInvoices;

			System.Net.Mail.SmtpClient c = new System.Net.Mail.SmtpClient();
			c.Host = Common.Properties.GetDefaultSmtpServer();

			System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

			m.Body = "<p>Ticket money earnt: " + ticketMoney.ToString("c") + "</p>"
					+ "<p>Ticket money cancelled: " + ticketMoneyCencelled.ToString("c") + "</p>"
					+ "<p>Ticket money earnt with cardnet delay: " + ticketMoneyWithCardnetDelay.ToString("c") + "</p>"
					+ "<p>Ticket money to promoter bank accounts: " + ticketMoneyToPromoterBankAccounts.ToString("c") + "</p>"
					+ "<p>Ticket money applied to invoices: " + ticketMoneyAppliedToInvoices.ToString("c") + "</p>"
					+ "<p><br><b>Ticket money to be held in reserve: " + ticketMoneyReserve.ToString("c") + "</b></p>";

			m.From = new System.Net.Mail.MailAddress(Vars.EMAIL_ADDRESS_MAIL);

			if (Vars.DevEnv || Vars.IsBeta)
			{
				m.Subject = "Test - " + m.Subject;
				m.Subject += " (" + Time.Now.ToString() + ")";
				m.To.Add(Vars.EMAIL_ADDRESS_DEV_TEAM);
			}
			else
			{
				m.To.Add(Vars.EMAIL_ADDRESS_ACCOUNTS);
			}

			
//			m.To.Add(new System.Net.Mail.MailAddress(Vars.EMAIL_ADDRESS_ACCOUNTS));
			m.IsBodyHtml = true;

			m.Subject = "Money in reserve for ticket funds: " + ticketMoneyReserve.ToString("c");

			c.Send(m);

			Log.Increment(Log.Items.EmailsSent);
		}
		#endregion

		#region Email Ticket Successful for Yesterday's Ticket Events
		public static void EmailAfterEventTicketFeedback()
		{
			DateTime yesterday = DateTime.Today.AddDays(-1);
			DateTime today = DateTime.Today;

			try
			{
				Query yesterdaysEventTicketsQuery = new Query(new And(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, yesterday),
																	  new Q(Event.Columns.DateTime, QueryOperator.LessThan, today),
																	  Ticket.SoldTicketsQ));
				yesterdaysEventTicketsQuery.TableElement = new Join(Ticket.Columns.EventK, Event.Columns.K);
				yesterdaysEventTicketsQuery.Columns = new ColumnSet(Ticket.Columns.BuyerUsrK, Ticket.Columns.EventK);
				yesterdaysEventTicketsQuery.GroupBy = new GroupBy(new GroupBy(Ticket.Columns.BuyerUsrK), new GroupBy(Ticket.Columns.EventK));
				yesterdaysEventTicketsQuery.OrderBy = new OrderBy(Ticket.Columns.EventK);

				TicketSet eventTickets = new TicketSet(yesterdaysEventTicketsQuery);

				foreach (Ticket ticket in eventTickets)
				{
					try
					{
						Mailer mailer = new Mailer();
						mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;

						mailer.UsrRecipient = ticket.BuyerUsr;
						mailer.Subject = "DontStayIn ticket feedback for " + ticket.Event.FriendlyName;
						//string eventUrl = @"[LOGIN(" + ticket.Event.Url();
						mailer.Body = "<h2>" + mailer.Subject + @"</h2>
									<p>Thanks for buying your tickets with us!</p>
									<p>In order to provide a better service in future, we would like to know if everything went OK with getting into the event...</p>
									<p>Please click an option below</p> 
									<p style='font-size:14px; font-weight:bold; padding-left:12px;'><a href=""[LOGIN(" + ticket.Event.UrlTicketFeedback(Ticket.FeedbackEnum.Good) + @")]""><img src='http://www.dontstayin.com/gfx/icon-tick-up.png' border='0' height='21' width='26' style='vertical-align:middle;'/>Yes, all OK</a></p>
									<p style='font-size:14px; font-weight:bold; padding-left:12px;'><a href=""[LOGIN(" + ticket.Event.UrlTicketFeedback(Ticket.FeedbackEnum.Bad) + @")]""><img src='http://www.dontstayin.com/gfx/icon-cross-up.png' border='0' height='21' width='26' style='vertical-align:middle;'/>No, there was a problem</a></p>";
						mailer.Send();
					}
					catch (Exception ex)
					{
						string additionalDetails = "Occurred in Utilities.EmailAfterEventTicketFeedback(): EventK= " + ticket.EventK.ToString() + ", BuyerUsrK= " + ticket.BuyerUsrK.ToString();
						EmailException(ex, additionalDetails, ticket);
					}
				}
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailAfterEventTicketFeedback()");
			}
		}
		#endregion

		#region Email Promoter Reminder To Submit Ticket Application Form
		public static void EmailPromoterReminderToSubmitTicketApplicationForm(TicketPromoterEvent ticketPromoterEvent)
		{
			try
			{
				if (!ticketPromoterEvent.Promoter.EnableTickets)
				{
					Mailer mailer = new Mailer();

					mailer.Subject = "DontStayIn has not yet received your ticket application form, promoter: " + ticketPromoterEvent.Promoter.Name;
					mailer.Body = "<h2>" + mailer.Subject + "</h2>";
					mailer.Body += ticketPromoterEvent.Promoter.LinkEmailFull;
					mailer.Body += @"<p>You recently setup ticket sales for event: " + ticketPromoterEvent.Event.LinkEmail +
									"</p><p>We have not yet received your ticket application form. Without a processed ticket application form, you cannot receive any funds from ticket sales.</p><p>This is an automated email reminder. If you have already sent it in, please ignore this email.</p><p>"
									+ @"<a href=""[LOGIN(" + ticketPromoterEvent.Promoter.UrlApp("plus") + "\")]>Click here for ticket application form</a></p>";

					mailer.RedirectUrl = ticketPromoterEvent.Promoter.Url();

					mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;

					if (!Vars.DevEnv)
					{
						foreach (Usr usr in ticketPromoterEvent.Promoter.AdminUsrs)
						{
							mailer.UsrRecipient = usr;
							mailer.Send();
						}
					}
					// Change subject for internal use
					mailer.Subject += " (K=" + ticketPromoterEvent.PromoterK.ToString() + ")";
					// now send to our accounts email address
					mailer.UsrRecipient = null;
					mailer.TemplateType = Mailer.TemplateTypes.AdminNote;
					mailer.To = Vars.EMAIL_ADDRESS_TICKETS;
					try { mailer.To += ", " + ticketPromoterEvent.Promoter.SalesUsr.Email; }
					catch { }
					mailer.Send();
				}
			}
			catch (Exception ex)
			{
				List<IBobAsHTML> bobsAsHtml = new List<IBobAsHTML>();
				bobsAsHtml.Add(ticketPromoterEvent.Promoter);
				bobsAsHtml.Add(ticketPromoterEvent.Event);
				EmailException(ex, "Occurred in Utilities.EmailPromoterReminderToSubmitTicketApplicationForm()", bobsAsHtml);
			}
		}

		#endregion

		#region Email Invoices
		public static bool EmailInvoice(Invoice invoice, string emailAddress)
		{
			try
			{
				emailAddress = emailAddress.Trim();
				Regex EmailRegex = new Regex(Cambro.Misc.RegEx.Email);
				if (!EmailRegex.IsMatch(emailAddress))
					throw new Exception("Invalid email address: " + emailAddress);

				string emailSubject = "";
				if (Vars.DevEnv)
					emailSubject += "TEST - ";

				emailSubject += "DontStayIn ";
				if (invoice.IsImmediateCreditCardPayment)
					emailSubject += "WEB ";
                emailSubject += invoice.TypeToString + " #" + invoice.K.ToString() + ", " + Math.Abs(invoice.Total).ToString("c");

				Attachment[] attachments = new Attachment[] { new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, invoice), "DontStayIn " + invoice.TypeToString + " #" + invoice.K.ToString() + ".doc", "application/word") };

				string body = "<p>" + emailSubject + "</p>";
				if (invoice.Promoter != null)
					body += "<p>Account: " + invoice.Promoter.Name + "</p>";
				else if (invoice.Usr != null)
					body += "<p>Account: " + invoice.Usr.NickName + "</p>";

				Utilities.EmailToNonUser(emailAddress, emailSubject, body, attachments);
			}
			catch (Exception ex)
			{
				string additionalDetails = "Occurred in Utilities.EmailInvoice(invoice, emailAddress): Invoice K= ";
				if (invoice != null)
					additionalDetails += invoice.K.ToString();
				else
					additionalDetails += "null";

				EmailException(ex, additionalDetails, invoice);
				return false;
			}

			return true;
		}

		public static bool EmailInvoice(Invoice invoice, bool invoiceCreated)
		{
			string emailSubject = "";
			if (Vars.DevEnv)
				emailSubject += "TEST - ";

			emailSubject += "DontStayIn ";
			if (invoice.IsImmediateCreditCardPayment)
				emailSubject += "WEB ";
            emailSubject += invoice.TypeToString + " #" + invoice.K.ToString() + ", " + Math.Abs(invoice.Total).ToString("c");
			if (invoiceCreated == true)
				emailSubject += " created";
			else
				emailSubject += " updated";

			return EmailInvoice(invoice, emailSubject, invoiceCreated);
		}

		public static bool EmailInvoice(Invoice invoice, string emailSubject, bool invoiceCreated)
		{
			bool emailSuccessful = true;
			try
			{
				Mailer mailer = new Mailer();

				mailer.Subject = emailSubject;
				mailer.Body = "<h2>" + emailSubject + "</h2>";
				if (invoice.Promoter != null)
				{
					mailer.Body += invoice.Promoter.LinkEmailFull;
				}

				AddInvoicesToEmail(mailer, new InvoiceSet(new Query(new Q(Invoice.Columns.K, invoice.K))));

				if (invoice.Promoter != null)
				{
					mailer.Body += @"<p><a href=""[LOGIN(" + invoice.Promoter.UrlApp("invoices") + "\")]>Click here to view all " + invoice.Promoter.Name + " invoices</a></p>";
					mailer.RedirectUrl = invoice.Promoter.UrlApp("invoices");
				}

				try
				{
					// Send email to each user in the AdminUsrs for the Promoter account.  When invoice is paid, it will appear as an attachment to the transfer email that completed its payment
					if (!Vars.DevEnv && invoice.Promoter != null && Math.Round(invoice.Total) != 0)
					{
						mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;

						foreach (Usr usr in invoice.Promoter.AdminUsrs)
						{
							mailer.UsrRecipient = usr;
							mailer.Send();
						}

						if (invoice.Usr != null && !invoice.Usr.IsPromoterK(invoice.PromoterK))
						{
							mailer.UsrRecipient = invoice.Usr;
							mailer.Send();
						}

						if (invoice.Promoter.AccountsEmail != null && invoice.Promoter.AccountsEmail.Length > 0)
						{
							try
							{
								string body = "<p>" + emailSubject + "</p><p>Account: " + invoice.Promoter.Name + "</p>";

								Utilities.EmailToNonUser(invoice.Promoter.AccountsEmail, mailer.Subject, body, mailer.Attachments.ToArray());
							}
							catch (Exception ex)
							{
								emailSuccessful = false;
								string additionalDetails = "Occurred in Utilities.EmailInvoice(invoice, emailSubject, invoiceCreated): Promoter.AccountsEmail = " + invoice.Promoter.AccountsEmail;
								EmailException(ex, additionalDetails, invoice);
							}
						}
					}
				}
				catch (Exception ex)
				{
					emailSuccessful = false;
					string additionalDetails = "Occurred in Utilities.EmailInvoice(invoice, emailSubject, invoiceCreated): Promoter.AccountsEmail = " + invoice.Promoter.AccountsEmail;
					EmailException(ex, additionalDetails, invoice);
				}

				// Change subject for internal use
				mailer.Subject = "";
				if (Vars.DevEnv)
					mailer.Subject = "TEST - ";
				mailer.Subject += "DontStayIn ";
				if (invoice.IsImmediateCreditCardPayment)
					mailer.Subject += "WEB ";
                mailer.Subject += invoice.TypeToString + " #" + invoice.K.ToString() + ", " + Math.Abs(invoice.Total).ToString("c");
				if (invoice.Usr != null)
					mailer.Subject += " from " + invoice.Usr.Name;
				if (invoice.Promoter != null)
					mailer.Subject += " (" + invoice.Promoter.Name + ")";

				if (invoiceCreated == true)
					mailer.Subject += " created";
				else
					mailer.Subject += " updated";

				// now send to our accounts email address
				mailer.UsrRecipient = null;
				mailer.TemplateType = Mailer.TemplateTypes.AdminNote;
				mailer.To = "accounts@dontstayin.com";
				mailer.Send();
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailInvoice(invoice, emailSubject, invoiceCreated)", invoice);

				emailSuccessful = false;
			}
			return emailSuccessful;
		}

		public static void AddInvoicesToEmail(Mailer mailer, InvoiceSet invoiceSet)
		{
			List<Invoice> invoices = new List<Invoice>();
			foreach (Invoice invoice in invoiceSet)
				invoices.Add(invoice);

			AddInvoicesToEmail(mailer, invoices);
		}

		public static void AddInvoicesToEmail(Mailer mailer, Invoice invoice)
		{
			List<Invoice> invoices = new List<Invoice>();
			invoices.Add(invoice);

			AddInvoicesToEmail(mailer, invoices);
		}

		public static void AddInvoicesToEmail(Mailer mailer, List<Invoice> invoices)
		{
			foreach (Invoice invoice in invoices)
			{
				if (invoice.Promoter != null && invoice.PromoterK != invoices[0].PromoterK)
					throw new Exception("Trying to email invoices for different promoters.");

				mailer.Attachments.Add(new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, invoice), "DontStayIn " + invoice.TypeToString + " #" + invoice.K.ToString() + ".doc", "application/word"));
				mailer.Body += "<p>";
				if (invoice.Promoter != null)
					mailer.Body += @"<a href=""[LOGIN(" + invoice.UrlReport() + "\")]>";

                mailer.Body += invoice.TypeToString + " #" + invoice.K.ToString() + " for " + Utilities.MoneyToHTML(Math.Abs(invoice.Total));

				if (invoice.Type.Equals(Invoice.Types.Invoice))
				{
					mailer.Body += ", - status: " + invoice.Status.ToString();
					if (invoice.AmountDue > 0)
						mailer.Body += ", amount due: " + Utilities.MoneyToHTML(invoice.AmountDue);
					//mailer.Body += "</b>";
				}
				if (invoice.Promoter != null)
					mailer.Body += "</a>";

				if (invoice.Usr != null)
				{
					mailer.Body += @", user: " + invoice.Usr.LinkEmail();
				}
				mailer.Body += "</p>";

				if (invoice.Type.Equals(Invoice.Types.Credit))
				{
					Query invoicesCreditedQuery = new Query(new And(new Q(InvoiceCredit.Columns.CreditInvoiceK, invoice.K),
																	new Q(Invoice.Columns.Type, Invoice.Types.Invoice)));
					invoicesCreditedQuery.TableElement = new Join(Invoice.Columns.K, InvoiceCredit.Columns.InvoiceK);
					InvoiceSet invoicesCredited = new InvoiceSet(invoicesCreditedQuery);
					foreach (Invoice invoiceCredited in invoicesCredited)
					{
						mailer.Body += @"<p>";
						if (invoice.Promoter != null)
							mailer.Body += @"<a href=""[LOGIN(" + invoiceCredited.UrlReport() + "\")]>Crediting invoice #" + invoiceCredited.K.ToString() + "</a>";
						else
							mailer.Body += "Crediting invoice #" + invoiceCredited.K.ToString();
						mailer.Body += "</p>";
					}
				}
				
				foreach (InvoiceItem ii in invoice.Items)
				{
                    mailer.Body += "<ul><li>" + HttpUtility.HtmlEncode(ii.Description) + " - " + Utilities.MoneyToHTML(Math.Abs(ii.Total)) + "</li></ul>";
				}
			}
		}

		private static void AddAppliedInvoices(Mailer mailer, Transfer transfer)
		{
			Query invoiceQuery = new Query(new Q(InvoiceTransfer.Columns.TransferK, transfer.K));
			invoiceQuery.TableElement = new Join(InvoiceTransfer.Columns.InvoiceK, Invoice.Columns.K);
			invoiceQuery.OrderBy = new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Ascending);
			InvoiceSet invoices = new InvoiceSet(invoiceQuery);
			if (invoices.Count > 0)
			{
				mailer.Body += "<p>Applied to the following:</p>";
				AddInvoicesToEmail(mailer, invoices);
			}
		}
		#endregion

		#region Email Transfers
		public static void EmailTransfer(Transfer transfer, bool transferCreated, bool transferStatusChanged)
		{
			string emailSubject = "";
			if (Vars.DevEnv)
				emailSubject += "TEST - ";

			emailSubject += "DontStayIn ";
			if (transfer.DateTimeCreated.Equals(transfer.DateTimeComplete) && transfer.Method.Equals(Transfer.Methods.Card))
				emailSubject += "WEB ";
            emailSubject += transfer.TypeToString + " #" + transfer.K.ToString() + ", " + Math.Abs(transfer.Amount).ToString("c");

			if (transferStatusChanged == true)
				emailSubject += "";
			else if (transferCreated)
				emailSubject += " created";
			else
				emailSubject += " updated";

			emailSubject += ", Status: " + transfer.Status.ToString();

			EmailTransfer(transfer, emailSubject, transferCreated, transferStatusChanged);
		}

		public static void EmailTransfer(Transfer transfer, string emailSubject, bool transferCreated, bool transferStatusChanged)
		{
			try
			{
				Mailer mailer = new Mailer();

				//mailer.Attachments.Add(new System.Net.Mail.Attachment(transfer.GenerateReportMemoryStream(false), "DontStayIn " + transfer.Type.ToString() + " #" + transfer.K.ToString() + ".html", "html/plain"));
				mailer.Subject = emailSubject;

				mailer.Body = "<h2>" + emailSubject + "</h2>";
				if (transfer.Promoter != null)
				{
					mailer.Body += transfer.Promoter.LinkEmailFull;
				}
				if (transfer.Usr != null)
				{
					mailer.Body += transfer.Usr.LinkEmailFull;
				}

				AddTransferToEmail(mailer, transfer);

				if (transfer.Promoter != null)
				{
					mailer.Body += @"<br><p><a href=""[LOGIN(" + transfer.Promoter.UrlApp("invoices") + "\")]>Click here to view all " + transfer.Promoter.Name + " invoices</a></p>";
					mailer.RedirectUrl = transfer.Promoter.UrlApp("invoices");
				}

				// Who does it go to??? All go to accounts. Newly successful transfers go to payments & promoter.adminusrs

				// Send email to each user in the AdminUsrs for the Promoter account		
				if (!Vars.DevEnv && transfer.Promoter != null && ((transferStatusChanged || transferCreated) && transfer.Status.Equals(Transfer.StatusEnum.Success) ||
									(transferCreated && transfer.Status.Equals(Transfer.StatusEnum.Pending) && transfer.Type.Equals(Transfer.TransferTypes.Refund))))
				{
					try
					{
						mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;

						foreach (Usr usr in transfer.Promoter.AdminUsrs)
						{
							mailer.UsrRecipient = usr;
							mailer.Send();
						}
						if (transfer.Usr != null && !transfer.Usr.IsPromoterK(transfer.PromoterK))
						{
							mailer.UsrRecipient = transfer.Usr;
							mailer.Send();
						}

						if (transfer.Promoter.AccountsEmail != null && transfer.Promoter.AccountsEmail.Length > 0)
						{
							try
							{
								string body = "<p>" + emailSubject + "</p><p>Account: " + transfer.Promoter.Name + "</p>";

								Utilities.EmailToNonUser(transfer.Promoter.AccountsEmail, mailer.Subject, body, mailer.Attachments.ToArray());
							}
							catch (Exception ex)
							{
								string additionalDetails = "Occurred in Utilities.EmailTransfer(): Promoter.AccountsEmail = " + transfer.Promoter.AccountsEmail;
								EmailException(ex, additionalDetails, transfer);
							}
						}
					}
					catch (Exception ex)
					{
						string additionalDetails = "Occurred in Utilities.EmailTransfer(): Promoter.AccountsEmail = " + transfer.Promoter.AccountsEmail;
						EmailException(ex, additionalDetails, transfer);
					}
				}

				// Change for internal use
				mailer.UsrRecipient = null;
				mailer.TemplateType = Mailer.TemplateTypes.AdminNote;

				mailer.Subject = "";
				if (Vars.DevEnv)
					mailer.Subject = "TEST - ";
				mailer.Subject += "DontStayIn ";

				if (transfer.DateTimeCreated.Equals(transfer.DateTimeComplete) && transfer.Method.Equals(Transfer.Methods.Card))
					mailer.Subject += "WEB ";
                mailer.Subject += transfer.TypeToString + " #" + transfer.K.ToString() + ", " + Math.Abs(transfer.Amount).ToString("c");
				if (transfer.Usr != null)
					mailer.Subject += " from " + transfer.Usr.Name;
				if (transfer.Promoter != null)
					mailer.Subject += " (" + transfer.Promoter.Name + ")";

				if (!Vars.DevEnv && transfer.Status.Equals(Transfer.StatusEnum.Success) && (transferStatusChanged || transferCreated))
				{
					// Only send to payments@dontstayin.com when a payment has been just been made successful
					mailer.To = "payments@dontstayin.com";
					mailer.Send();
				}

                // now send to our accounts email address
                mailer.To = "accounts@dontstayin.com";

				if (transferCreated == true)
					mailer.Subject += " created";
				else
					mailer.Subject += " updated";
				mailer.Subject += ", Status: " + transfer.Status.ToString();

				mailer.Send();
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailTransfer()", transfer);
			}
		}

		private static void AddTransferToEmail(Mailer mailer, Transfer transfer)
		{
			if (transfer != null)
			{
				if (transfer.Status.Equals(Transfer.StatusEnum.Pending) && transfer.Type.Equals(Transfer.TransferTypes.Payment))
				{
					AddPendingPaymentToEmail(mailer, transfer);
				}
				else if (transfer.Status.Equals(Transfer.StatusEnum.Success) && transfer.Type.Equals(Transfer.TransferTypes.Payment))
				{
					AddSuccessfulPaymentToEmail(mailer, transfer);
				}
				else if ((transfer.Status.Equals(Transfer.StatusEnum.Pending) || transfer.Status.Equals(Transfer.StatusEnum.Success)) && transfer.Type.Equals(Transfer.TransferTypes.Refund))
				{
					AddRefundToEmail(mailer, transfer);
				}
			}
		}

		private static void AddPendingPaymentToEmail(Mailer mailer, Transfer transfer)
		{
			if (transfer.Status.Equals(Transfer.StatusEnum.Pending) && transfer.Type.Equals(Transfer.TransferTypes.Payment))
			{
				mailer.Attachments.Add(new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, transfer), "DontStayIn " + transfer.TypeToString + " #" + transfer.K.ToString() + ".doc", "application/word"));
				if (transfer.Promoter != null)
				{
					mailer.Body += @"<p><a href=""[LOGIN(" + transfer.UrlReport() + "\")]>" + transfer.Status.ToString() + " " + transfer.TypeToString.ToLower() + "</a> for "
                                + Utilities.MoneyToHTML(Math.Abs(transfer.Amount)) + " has been setup on your " + transfer.Promoter.LinkEmail() + " promoter account.</p>";

					if (transfer.Method.Equals(Transfer.Methods.Cheque))
						mailer.Body += "<p>Please make your cheque payable to:<br>Development Hell Limited<br>Greenhill House, Thorpe Road, Peterborough, PE3 6RU</p><p>Add reference #" + transfer.K.ToString() + " to the back of the cheque</p>";
					else if (transfer.Method.Equals(Transfer.Methods.BankTransfer))
						mailer.Body += "<p>Please make BACS transfer details:<br>Bank name: Barclays Bank PLC<br>Branch Name: Commercial Bank Basingstoke<br>Sort Code: 20-37-63<br>Account #: 00478377<br>(IBAN for international transfers: GB04 BARC 2037 6300 4783 77)</p><p>Add BACS reference #" + transfer.K.ToString() + "</p>";
					else
						mailer.Body += "<p>Please make payment to:<br>Development Hell Limited<br>Greenhill House, Thorpe Road, Peterborough, PE3 6RU</p>";
				}
				AddAppliedInvoices(mailer, transfer);
			}
		}

		private static void AddSuccessfulPaymentToEmail(Mailer mailer, Transfer transfer)
		{
			if (transfer.Status.Equals(Transfer.StatusEnum.Success) && transfer.Type.Equals(Transfer.TransferTypes.Payment))
			{
				mailer.Attachments.Add(new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, transfer), "DontStayIn " + transfer.TypeToString + " #" + transfer.K.ToString() + ".doc", "application/word"));
				if (transfer.Promoter != null)
				{
                    mailer.Body += @"<p><a href=""[LOGIN(" + transfer.UrlReport() + "\")]>Successful " + transfer.TypeToString.ToLower() + " for " + Utilities.MoneyToHTML(Math.Abs(transfer.Amount))
								+ "</a> has been processed on your " + transfer.Promoter.LinkEmail() + " promoter account.</p>";
				}
				else if (transfer.Usr != null)
				{
                    mailer.Body += "<p>Successful " + transfer.TypeToString.ToLower() + " for " + Utilities.MoneyToHTML(Math.Abs(transfer.Amount)) + " has been processed on your " + transfer.Usr.LinkEmail() + " user account.</p>";
				}
				AddAppliedInvoices(mailer, transfer);
			}
		}

		private static void AddRefundToEmail(Mailer mailer, Transfer transfer)
		{
			if ((transfer.Status.Equals(Transfer.StatusEnum.Pending) || transfer.Status.Equals(Transfer.StatusEnum.Success)) && transfer.Type.Equals(Transfer.TransferTypes.Refund))
			{
				mailer.Attachments.Add(new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, transfer), "DontStayIn " + transfer.TypeToString + " #" + transfer.K.ToString() + ".doc", "application/word"));

				if (transfer.Promoter != null)
                    mailer.Body += @"<p><a href=""[LOGIN(" + transfer.UrlReport() + "\")]>" + transfer.TypeToString + " for " + Utilities.MoneyToHTML(Math.Abs(transfer.Amount)) + "</a> has been ";
				else
                    mailer.Body += "<p>" + transfer.TypeToString + " for " + Utilities.MoneyToHTML(Math.Abs(transfer.Amount)) + " has been ";

				if (transfer.Status.Equals(Transfer.StatusEnum.Pending))
					mailer.Body += "setup";
				else if (transfer.Status.Equals(Transfer.StatusEnum.Success))
					mailer.Body += "successfully processed";

				if (transfer.Promoter != null)
				{
					mailer.Body += " on your " + transfer.Promoter.LinkEmail() + " promoter account.</p>";
				}
				else
				{
					mailer.Body += ".</p>";
				}

				if (transfer.TransferRefundedK != 0)
				{
					Transfer transferRefunded = new Transfer(transfer.TransferRefundedK);
					mailer.Attachments.Add(new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, transferRefunded), "DontStayIn " + transferRefunded.TypeToString + " #" + transferRefunded.K.ToString() + ".doc", "application/word"));
					if (transfer.Promoter != null)
						mailer.Body += @"<ul type=""circle""><li><a href=""[LOGIN(" + transferRefunded.UrlReport() + "\")]>Refunding transfer #" + transferRefunded.K.ToString() + "</a></li></ul>";
					else
						mailer.Body += @"<ul type=""circle""><li>Refunding " + transferRefunded.TypeToString.ToLower() + " #" + transferRefunded.K.ToString() + "</li></ul>";
				}
				AddAppliedInvoices(mailer, transfer);
			}
		}
		#endregion

		#region Email Ticket
		public static bool EmailTicket(Ticket ticket)
		{
			try
			{
				if(ticket.K == 0)
					throw new Exception("Ticket not saved. K=0");

				Mailer mailer = new Mailer();

				mailer.Attachments.Add(new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, ticket), "DontStayIn Ticket #" + ticket.K.ToString() + ".doc", "application/word"));
				
				string cancelledText = ticket.Cancelled ? "Cancelled " : "";

				mailer.Subject = cancelledText + "DontStayIn Ticket for " + Cambro.Misc.Utility.Snip(ticket.Event.Name, 40) + " on " + Utilities.DateToString(ticket.Event.DateTime);
				mailer.Body = "<h2>" + cancelledText + "DontStayIn Ticket for " + ticket.Event.FriendlyName + "</h2>";
				if (!ticket.Cancelled)
				{
					
					StringBuilder confirmationMessage = new StringBuilder();

					switch (ticket.TicketRun.DeliveryMethod)
					{
						case TicketRun.DeliveryMethodType.E_Ticket:
							confirmationMessage.Append(@"<p>Thank you for buying " + (ticket.Quantity > 1 ? "tickets" : "a ticket")	+ @" through DontStayIn.</p>
								<p><b>" + (ticket.Quantity > 1 ? Ticket.ETICKET_CARD_REMINDER_PLURAL : Ticket.ETICKET_CARD_REMINDER_SINGULAR) + ".</b></p>" +
								(ticket.Code.Length > 0 ? "<p><b>" + Ticket.ETICKET_CODE_REMINDER_SINGULAR + ".</b></p>" : "") + 
								"<p>" + Ticket.ETICKET_CARD_REMINDER_NOT_LET_YOU_IN + " The card you paid with ends with the digits \"<b>"
								+ ticket.CardNumberEnd + "</b>\".</p><p>Make sure you take this card with you!</p>");
							break;
						case TicketRun.DeliveryMethodType.SpecialDelivery:
							string[] address = ticket.AddressParts;
							confirmationMessage.Append("Thanks for buying " + (ticket.Quantity > 1 ? "tickets. " : "a ticket.") + " Your tickets will be delivered to: ");
							confirmationMessage.Append("<p><small>");
							foreach (string addressPart in address)
							{
								if (addressPart != "")
								{
									confirmationMessage.Append("<br>" + addressPart);
								}
							}
							confirmationMessage.Append("</small></p>");
							confirmationMessage.Append("<br>around " + ticket.TicketRun.DeliveryDate.ToLongDateString() + ".");
							break;

						default: throw new NotImplementedException();
					}

					
					mailer.Body += confirmationMessage;
				}
				else
				{
					mailer.Body += @"<p>You recently bought " + (ticket.Quantity > 1 ? "tickets" : "a ticket") + " through DontStayIn. Your " + (ticket.Quantity > 1 ? "tickets have" : "ticket has") 
									+  " been cancelled and your card has been refunded.</p>";
				}
				mailer.Body += @"<br><p><b>Your " + cancelledText.ToLower() + "ticket details</b></p>";
				mailer.Body += ticket.Event.LinkEmailFull;
				mailer.Body += "<p>Ticket ref: " + ticket.K.ToString() + "</p>";
				mailer.Body += "<p>Ticket" + (ticket.Quantity > 1 ? "s" : "") + ": " + ticket.Quantity.ToString() + " x " + Utilities.MoneyToHTML(ticket.TicketRun.Price) + (ticket.Cancelled ? "" : " + booking fee (" + Utilities.MoneyToHTML(ticket.TicketRun.BookingFee) + ")") + "</p>";
				if(ticket.Code.Length > 0)
					mailer.Body += "<p><font size='+1'><b>Ticket CODE: \"" + ticket.Code.ToString() + "\"</b></font></p>";
				if(ticket.TicketRun.Name.Length > 0)
					mailer.Body += "<p>Ticket type: " + ticket.TicketRun.Name + "</p>";
				if (ticket.TicketRun.Description.Length > 0)
					mailer.Body += "<p>Description: " + ticket.TicketRun.Description + "</p>";
				mailer.Body += "<br><p>You can find details of all your tickets on the \"<a href=\"[LOGIN(" + ticket.BuyerUsr.UrlApp("mytickets") + "\")]>My tickets</a>\" page (on the \"My DSI\" menu).</p>";
				mailer.Body += "<p><center><a href=\"[LOGIN(" + ticket.BuyerUsr.UrlApp("mytickets") + "\")]><img src=\"[WEB-ROOT]gfx/mytickets-menu.gif\" border=\"0\" align=\"center\"/></a></center></p><br>";

				if (ticket.Enabled)
				{
					// Send email to BuyerUsr for the ticket purchase. 
					mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
					mailer.UsrRecipient = ticket.BuyerUsr;
					mailer.Send();
				}

				// Change subject for internal use
				if (ticket.BuyerUsr != null)
					mailer.Subject += " for " + ticket.BuyerUsr.NickName;
				if(!ticket.Cancelled && ticket.Enabled)
					mailer.Subject += " was purchased";

				mailer.Body += ticket.AsHTML();

				// now send to our tickets email address
				mailer.UsrRecipient = null;
				mailer.TemplateType = Mailer.TemplateTypes.AdminNote;
				mailer.To = Vars.EMAIL_ADDRESS_TICKETS;
				mailer.Send();
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailTicket(ticket)", ticket);

				return false;
			}
			return true;
		}

		public static bool EmailStyledTicket(IStyledEventHolder styledObject, Ticket ticket)
		{
			try
			{
				if (ticket.K == 0)
					throw new Exception("Ticket not saved. K=0");

				System.Net.Mail.SmtpClient c = new System.Net.Mail.SmtpClient();
				c.Host = Common.Properties.GetDefaultSmtpServer();

				System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
				string cancelledText = ticket.Cancelled ? "Cancelled " : "";

				m.Subject = cancelledText + "Ticket for " + Cambro.Misc.Utility.Snip(ticket.Event.Name, 40) + " on " + Utilities.DateToString(ticket.Event.DateTime);
				m.Body = cancelledText + "Ticket for " + ticket.Event.FriendlyName;
				if (!ticket.Cancelled)
				{
					m.Body += "\nThank you for buying " + (ticket.Quantity > 1 ? "tickets" : "a ticket") +
								"\n" + (ticket.Quantity > 1 ? Ticket.ETICKET_CARD_REMINDER_PLURAL : Ticket.ETICKET_CARD_REMINDER_SINGULAR) + "." +
								(ticket.Code.Length > 0 ? "\n" + Ticket.ETICKET_CODE_REMINDER_SINGULAR + "." : "") +
								"\n" + Ticket.ETICKET_CARD_REMINDER_NOT_LET_YOU_IN + " The card you paid with ends with the digits \""
								+ ticket.CardNumberEnd + "\".\nMake sure you take this card with you!";
				}
				else
				{
					m.Body += "\nYou recently bought " + (ticket.Quantity > 1 ? "tickets" : "a ticket") + ". Your " + (ticket.Quantity > 1 ? "tickets have" : "ticket has")
								+ " been cancelled and your card has been refunded.";
				}
				m.Body += "\n\nYour " + cancelledText.ToLower() + "ticket details";
				m.Body += "\nTicket ref: " + ticket.K.ToString() + "";
				m.Body += "\nTicket" + (ticket.Quantity > 1 ? "s" : "") + ": " + ticket.Quantity.ToString() + " x " + ticket.TicketRun.Price.ToString("c") + (ticket.Cancelled ? "" : " + booking fee (" + ticket.TicketRun.BookingFee.ToString("c") + ")");
				if (ticket.Code.Length > 0)
					m.Body += "\nTicket CODE: \"" + ticket.Code.ToString() + "\"";
				if (ticket.TicketRun.Name.Length > 0)
					m.Body += "\nTicket type: " + ticket.TicketRun.Name;
				if (ticket.TicketRun.Description.Length > 0)
					m.Body += "\nDescription: " + ticket.TicketRun.Description;
				m.Body += "\nYou can find details of all your tickets on http://" + Vars.DomainName + "/" + styledObject.UrlStyledApp("mytickets");

				m.Body += "\n\nTickets powered by Development Hell Limited\nwww.dontstayin.com";
			
				m.Body = m.Body.Replace("\n", "\n\r");

				m.From = new System.Net.Mail.MailAddress(Vars.AdminReplyAddress);
				

				m.IsBodyHtml = false;

				if (Vars.DevEnv)
				{
					m.Subject = m.Subject + " (to:" + m.To + ")";
					m.Subject += " (" + DateTime.Now.ToString() + ")";
					m.To.Add(Vars.EMAIL_ADDRESS_DEV_TEAM);
					m.To.Add("neil@dontstayin.com");
					m.From = new System.Net.Mail.MailAddress("mail@davidbrophy.com");
					c.Send(m);
				}
				else if (Vars.IsBeta)
				{
					m.Subject = "BETA " + m.Subject;
					m.To.Add(new MailAddress(ticket.BuyerUsr.Email));
					m.Bcc.Add(Vars.EMAIL_ADDRESS_TICKETS);
					c.Send(m);
				}
				else
				{
					if (ticket.BuyerUsr.Email.EndsWith("@gmail.com") || ticket.BuyerUsr.Email.EndsWith("@dontstayin.com"))
						m.Subject += " (" + DateTime.Now.ToString() + ")";
					m.To.Add(new MailAddress(ticket.BuyerUsr.Email));
					m.Bcc.Add(Vars.EMAIL_ADDRESS_TICKETS);
					c.Send(m);
				}

				Log.Increment(Log.Items.EmailsSent);				
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailStyledTicket(ticket)", ticket);

				return false;
			}
			return true;
		}
		#endregion

		#region Email TicketPromoterEvent
		public static void EmailTicketPromoterEvent(TicketPromoterEvent ticketPromoterEvent)
		{
			// Email promoter: stating funds have been released and applied to their DSI promoter account
		    // Attach Transfer if != null
		    // Email copy to accounts
		    try
		    {
				if (ticketPromoterEvent.Promoter.EnableTickets && ticketPromoterEvent.FundsTransfer != null && Math.Round(ticketPromoterEvent.TotalFunds, 2) != 0)
				{
					Mailer mailer = new Mailer();
					string emailSubject = "DontStayIn " + Utilities.CamelCaseToString(Transfer.Methods.TicketSales.ToString()).ToLower() + " (" + ticketPromoterEvent.TotalFunds.ToString("c") + ") for " + ticketPromoterEvent.Event.FriendlyName;
					mailer.Subject = emailSubject;

					mailer.Body = "<h2>" + emailSubject + "</h2>";
					if (ticketPromoterEvent.Promoter != null)
					{
						mailer.Body += @"<p>Promoter: <a href=""[LOGIN(" + ticketPromoterEvent.Promoter.Url() + "\")]>" + ticketPromoterEvent.Promoter.Name + "</a></p>";
						mailer.Body += @"<p>Event: <a href=""[LOGIN(" + ticketPromoterEvent.Event.Url() + "\")]>" + ticketPromoterEvent.Event.FriendlyName + "</a></p>";

						mailer.RedirectUrl = ticketPromoterEvent.Promoter.UrlApp("allticketruns");
					}

					mailer.Body += "<p>Total funds: " + Utilities.MoneyToHTML(ticketPromoterEvent.TotalFunds) + "</p>";

					mailer.Body += @"<p>Tickets sold: " + ticketPromoterEvent.SoldTickets.ToString() + "<br>";

					int cancelledTicketCount = 0;

					foreach (TicketRun ticketRun in ticketPromoterEvent.TicketRuns)
					{
						mailer.Body += ticketRun.SoldTickets.ToString() + " x " + ticketRun.PriceBrandName + "<br>";

						cancelledTicketCount += ticketRun.CancelledTicketQuantity;
					}
					mailer.Body += "</p>";

					if (cancelledTicketCount > 0)
					{
						mailer.Body += @"<p>Tickets cancelled: " + cancelledTicketCount.ToString() + "<br>";
						foreach (TicketRun ticketRun in ticketPromoterEvent.TicketRuns)
						{
							mailer.Body += ticketRun.CancelledTicketQuantity.ToString() + " x " + ticketRun.PriceBrandName + "<br>";
						}
						mailer.Body += "</p>";
					}

					try
					{
						AddSelfBillingInvoiceToEmail(mailer, ticketPromoterEvent);
					}
					catch { }

					// Send email to each user in the AdminUsrs for the Promoter account		
					if (!Vars.DevEnv)
					{
						if (ticketPromoterEvent.Promoter != null)
						{
							mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;

							foreach (Usr usr in ticketPromoterEvent.Promoter.AdminUsrs)
							{
								mailer.UsrRecipient = usr;
								mailer.Send();
							}
						}
					}

					// Change for internal use
					mailer.UsrRecipient = null;
					mailer.TemplateType = Mailer.TemplateTypes.AdminNote;
                    if (!Vars.DevEnv && ticketPromoterEvent.Promoter.SalesUsr != null)
                    {
                        mailer.UsrRecipient = ticketPromoterEvent.Promoter.SalesUsr;
                        mailer.Send();
                    }

                    mailer.UsrRecipient = null;

					if (Vars.DevEnv)
						mailer.Subject = "TEST - " + mailer.Subject;

					// now send to our tickets email address
					mailer.To = Vars.EMAIL_ADDRESS_TICKETS;
					try { mailer.To += ", " + ticketPromoterEvent.Promoter.SalesUsr.Email; }
					catch { }

					mailer.Send();
				}
		    }
		    catch (Exception ex)
		    {
				EmailException(ex, "<p>Occurred in Utilities.EmailTicketPromoterEvent(): Promoter K= " + ticketPromoterEvent.PromoterK + ", Event K= " + ticketPromoterEvent.EventK + "</p>", ticketPromoterEvent);
		    }
		}

        public static void AddSelfBillingInvoiceToEmail(Mailer mailer, TicketPromoterEvent ticketPromoterEvent)
        {
            if (ticketPromoterEvent != null && ticketPromoterEvent.FundsTransfer != null && mailer != null)
            {
                mailer.Attachments.Add(new System.Net.Mail.Attachment(Utilities.GenerateReportMemoryStream(false, ticketPromoterEvent), "DontStayIn " + Utilities.CamelCaseToString(ticketPromoterEvent.FundsTransfer.Method.ToString()) + " #" + ticketPromoterEvent.FundsTransferK.ToString() + ".doc", "application/word"));
                if (ticketPromoterEvent.Promoter != null)
                {
                    mailer.Body += @"<p><a href=""[LOGIN(" + ticketPromoterEvent.UrlReport() + "\")]>" + ticketPromoterEvent.TypeAndK + "</a> for "
								+ Utilities.MoneyToHTML(Math.Abs(ticketPromoterEvent.FundsTransfer.Amount)) + " has been released to your " + ticketPromoterEvent.Promoter.LinkEmail() + " promoter account.</p>";

                }
            }
        }
		
		public static void EmailAllEndedTicketRuns()//DateTime fromDateTime, DateTime toDateTime)
		{
			Query getAllEndedTicketRunsQuery = new Query(new And(new Or(new Q(TicketRun.Columns.EmailSent, 0),
																		new Q(TicketRun.Columns.EmailSent, QueryOperator.IsNull, null)),
																 new Q(TicketRun.Columns.EndDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now)));
			getAllEndedTicketRunsQuery.TableElement = new Join(new TableElement(TablesEnum.TicketPromoterEvent), new TableElement(TablesEnum.TicketRun), QueryJoinType.Inner,
																new And(new Q(TicketPromoterEvent.Columns.PromoterK, TicketRun.Columns.PromoterK, true),
																		new Q(TicketPromoterEvent.Columns.EventK, TicketRun.Columns.EventK, true)));
			//getAllEndedTicketRunsQuery.Columns = new ColumnSet(TicketPromoterEvent.Columns.EventK, TicketPromoterEvent.Columns.PromoterK);
			TicketPromoterEventSet ticketPromoterEventsWithRecentlyEndedTicketRuns = new TicketPromoterEventSet(getAllEndedTicketRunsQuery);

			foreach (TicketPromoterEvent tpe in ticketPromoterEventsWithRecentlyEndedTicketRuns)
			{
				EmailTicketRunStatusUpdate(tpe);
			}
		}
		public static void EmailTicketRunStatusUpdate(TicketPromoterEvent ticketPromoterEvent)
		{
			// Email promoter: list of all ticket runs for the specified event detailing: status, tickets sold, money earned, end datetime. Includes links to ticket runs, event, promoter, and doorlist.
			// Email copy to accounts
			try
			{
				if (ticketPromoterEvent != null && ticketPromoterEvent.TicketRuns != null && ticketPromoterEvent.TicketRuns.Count > 0)	
				{
					Mailer mailer = new Mailer();
					string emailSubject = "DontStayIn ticket runs update for " + ticketPromoterEvent.Event.FriendlyName;
					mailer.Subject = emailSubject;

					mailer.Body = "<h2>" + emailSubject + "</h2>";
					if (ticketPromoterEvent.Promoter != null)
					{
						mailer.Body += @"<p>Promoter: <a href=""[LOGIN(" + ticketPromoterEvent.Promoter.Url() + "\")]>" + ticketPromoterEvent.Promoter.Name + "</a></p>";
						

						mailer.RedirectUrl = ticketPromoterEvent.Promoter.UrlApp("allticketruns");
					}
					mailer.Body += @"<p>Event: <a href=""[LOGIN(" + ticketPromoterEvent.Event.Url() + "\")]>" + ticketPromoterEvent.Event.FriendlyName + "</a></p>";
					mailer.Body += "<p>Ticket runs:</p><ul>";

					DateTime lastTicketRunEndDateTime = DateTime.MinValue;
					List<TicketRun> ticketRunsToUpdate = new List<TicketRun>();
					foreach (TicketRun ticketRun in ticketPromoterEvent.TicketRuns)
					{
						mailer.Body += @"<li><a href=""[LOGIN(" + ticketRun.Url() + "\")]>" + ticketRun.PriceBrandName + @"</a>
									<br>Status: " + Utilities.CamelCaseToString(ticketRun.Status.ToString())
									+ "<br>Sold: " + ticketRun.ValidTicketQuantity.ToString()
                                    + "<br>Money earned: " + Utilities.MoneyToHTML(ticketRun.ValidTicketQuantity * ticketRun.Price)
									+ "<br>Ends: " + ticketRun.EndDateTime.ToString("ddd dd/MM/yyyy HH:mm") + "</li>";

						lastTicketRunEndDateTime = lastTicketRunEndDateTime > ticketRun.EndDateTime ? lastTicketRunEndDateTime : ticketRun.EndDateTime;

						if (ticketRun.EndDateTime < DateTime.Now && !ticketRun.EmailSent)
						{
							ticketRun.EmailSent = true;
							ticketRunsToUpdate.Add(ticketRun);
						}
					}
					mailer.Body += "</ul>";

					mailer.Body += @"<p>Doorlist: <a href=""[LOGIN(" + ticketPromoterEvent.Event.DoorlistUrl + "\")]>" + ticketPromoterEvent.Event.Name + " doorlist</a></p>";

					if (lastTicketRunEndDateTime < DateTime.Now)
					{
						mailer.Body += "<p>Please note that all your current ticket runs for " + ticketPromoterEvent.Event.Name
									+ " have ended. Please print off the doorlist and make sure you verify card details at the door.</p>";
					}
					else
					{
						mailer.Body += "<p>Please note that your last ticket run for " + ticketPromoterEvent.Event.Name
                                       + " will end on " + lastTicketRunEndDateTime.ToString("ddd dd/MM/yyyy") + " at " + lastTicketRunEndDateTime.ToString("HH:mm") + ". Please wait for all ticket runs to end before printing off the doorlist. Make sure you verify card details at the door.</p>";
					}

                    if (!ticketPromoterEvent.Promoter.EnableTickets)
                    {
                        mailer.Body += "<br><p>We have not yet received your ticket application form. Without a processed ticket application form, you cannot receive any funds from ticket sales.</p><p>This is an automated email reminder. If you have already sent it in, please ignore this email.</p><p>"
                                    + @"<a href=""[LOGIN(" + ticketPromoterEvent.Promoter.UrlApp("plus") + "\")]>Click here for ticket application form</a></p>";
                    }
					
					// Send email to each user in the AdminUsrs for the Promoter account		
					if (!Vars.DevEnv)
					{
						if (ticketPromoterEvent.Promoter != null)
						{
							mailer.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;

							foreach (Usr usr in ticketPromoterEvent.Promoter.AdminUsrs)
							{
								mailer.UsrRecipient = usr;
								mailer.Send();
							}
						}
					}

					foreach (TicketRun ticketRun in ticketRunsToUpdate)
						ticketRun.Update();

					// Change for internal use
					mailer.UsrRecipient = null;
					mailer.TemplateType = Mailer.TemplateTypes.AdminNote;

					if (Vars.DevEnv)
						mailer.Subject = "TEST - " + mailer.Subject;

					// now send to our tickets email address
					mailer.To = Vars.EMAIL_ADDRESS_TICKETS;
					mailer.Send();
				}
			}
			catch (Exception ex)
			{
				EmailException(ex, "Occurred in Utilities.EmailTicketPromoterEvent(): Promoter K= " + ticketPromoterEvent.PromoterK + ", Event K= " + ticketPromoterEvent.EventK, ticketPromoterEvent);
			}
		}
		#endregion

		#region Email to Non-Users
		public static void EmailToNonUser(string toEmailAddress, string subject, string body, Attachment[] attachments)
		{
			body += "<p>Development Hell Limited<br>0207 835 5599<br>www.dontstayin.com</p>";
			System.Net.Mail.SmtpClient c = new System.Net.Mail.SmtpClient();
			c.Host = Common.Properties.GetDefaultSmtpServer();

			System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

			foreach (System.Net.Mail.Attachment a in attachments)
				m.Attachments.Add(a);

			m.Body = body.Replace("\n", "\n\r");

			m.From = new System.Net.Mail.MailAddress(Vars.AdminReplyAddress);

			m.IsBodyHtml = true;

			if (Vars.DevEnv)
			{
				m.Subject = subject + " (to:" + toEmailAddress + ")";
				m.Subject += " (" + DateTime.Now.ToString() + ")";
				m.To.Add(Vars.EMAIL_ADDRESS_DEV_TEAM);
				m.To.Add("neil@dontstayin.com");
				m.From = new System.Net.Mail.MailAddress("mail@davidbrophy.com");
				c.Send(m);
			}
			else if (Vars.IsBeta)
			{
				m.Subject = "BETA " + subject;
				m.To.Add(toEmailAddress);
				c.Send(m);
			}
			else
			{
				m.Subject = subject;
				if (toEmailAddress.EndsWith("@gmail.com") || toEmailAddress.EndsWith("@dontstayin.com"))
					m.Subject += " (" + DateTime.Now.ToString() + ")";
				m.To.Add(toEmailAddress);
				c.Send(m);
			}

			Log.Increment(Log.Items.EmailsSent);
		}
		#endregion

		#region Email Exception Handler
		private static void EmailException(Exception ex)
		{
			EmailException(ex, "");
		}

		private static void EmailException(Exception ex, string additionalDetails)
		{
			EmailException(ex, additionalDetails, new List<IBobAsHTML>());
		}
		private static void EmailException(Exception ex, string additionalDetails, IBobAsHTML bobAsHTML)
		{
			List<IBobAsHTML> bobsAsHTML = new List<IBobAsHTML>();
			bobsAsHTML.Add(bobAsHTML);
			EmailException(ex, additionalDetails, bobsAsHTML);
		}
		private static void EmailException(Exception ex, string additionalDetails, List<IBobAsHTML> bobsAsHTML)
		{
			string body = "<p>Exception occurred while emailing</p>";
			if (additionalDetails.Length > 0)
				body += "<p>" + additionalDetails + "</p>";
			body += "<p>" + ex.ToString() + "</p>";

			AdminEmailAlert(body, "Exception occurred while emailing", Vars.EMAIL_ADDRESS_DAVE + "," + Vars.EMAIL_ADDRESS_TIMI);
		}
		#endregion
		#endregion

		#region Get DateTime
		public static DateTime GetStartOfWeek(DateTime dt)
		{
			while (dt.DayOfWeek != DayOfWeek.Monday)
			{
				dt = dt.AddDays(-1);
			}
			return new DateTime(dt.Year, dt.Month, dt.Day);
		}

		public static DateTime GetEndOfWeek(DateTime dt)
		{
			return GetStartOfWeek(dt).AddDays(7).AddMilliseconds(-1);
		}

		public static DateTime GetStartOfMonth(DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, 1);
		}

		public static DateTime GetEndOfMonth(DateTime dt)
		{
			return GetStartOfMonth(dt).AddMonths(1).AddMilliseconds(-1);
		}

		public static DateTime GetStartOfDay(DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day);
		}

        public static DateTime AddBusinessDays(DateTime dt, int businessDays)
        {
            if (businessDays == 0)
                return dt;

            int increment = 1;
            if (businessDays < 0)
                increment = -1;

            while (businessDays != 0)
            {
                do 
                {
                   dt = dt.AddDays(increment);
                }
                while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday);

                businessDays -= increment;
            }

            return dt;
        }
		#endregion

		#region Validation
		public static bool IsMoneyText(string money)
		{
			bool result = false;
			money = money.Trim().Replace("£", "").Replace("$", "").Replace(",", "");
			try
			{
				double output = double.Parse(money);
				double roundedOutput = Math.Round(Convert.ToDouble(money), 2);
				result = Math.Round(output, 2) == Math.Round(roundedOutput, 2);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public static bool IsPositiveMoneyText(string money, bool allowZero)
		{
			bool result = false;
			money = money.Trim().Replace("£", "").Replace("$", "").Replace(",", "");
				
			try
			{
				double output = double.Parse(money);
				double roundedOutput = Math.Round(Convert.ToDouble(money), 2);
				result = (Math.Round(output, 2) > 0 || (Math.Round(output, 2) == 0 && allowZero)) && Math.Round(output, 2) == Math.Round(roundedOutput, 2);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public static bool IsNegativeMoneyText(string money, bool allowZero)
		{
			bool result = false;
			money = money.Trim().Replace("£", "").Replace("$", "").Replace(",", "");
			try
			{
				double output = double.Parse(money);
				double roundedOutput = Math.Round(Convert.ToDouble(money), 2);
				result = (Math.Round(output, 2) < 0 || (Math.Round(output, 2) == 0 && allowZero)) && Math.Round(output, 2) == Math.Round(roundedOutput, 2);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		#endregion

		#region IBuyable Methods

		#endregion

		#region AdminEmailAlert
		public static void AdminEmailAlert(string body, string subject)
		{
			AdminEmailAlert(body, subject, new string[] { Vars.EMAIL_ADDRESS_DAVE, Vars.EMAIL_ADDRESS_TIMI });
		}
		public static void AdminEmailAlert(string body, string subject, Exception ex)
		{
			AdminEmailAlert(body, subject, ex, new List<IBobAsHTML>());
		}
		public static void AdminEmailAlert(string body, string subject, Exception ex, IBobAsHTML bobAsHTML)
		{
			List<IBobAsHTML> bobsAsHTML = new List<IBobAsHTML>();
			bobsAsHTML.Add(bobAsHTML);
			AdminEmailAlert(body, subject, ex, bobsAsHTML);

		}
		public static void AdminEmailAlert(string body, string subject, Exception ex, List<IBobAsHTML> bobsAsHTML)
		{
			AdminEmailAlert(body, subject, ex, bobsAsHTML, new string[] { Vars.EMAIL_ADDRESS_DAVE, Vars.EMAIL_ADDRESS_TIMI, Vars.EMAIL_ADDRESS_HARRY });
		}
		
		public static void AdminEmailAlert(string body, string subject, Exception ex, List<IBobAsHTML> bobsAsHTML, string[] recipientAddresses)
		{
			body += "<p>Exception message: " + ex.Message + "</p>";

			body += "<p>Exception stack trace: " + ex.StackTrace + "</p>";

			foreach (IBobAsHTML bobAsHTML in bobsAsHTML)
			{
				try
				{
					if(bobsAsHTML != null)
						body += "<p>" + bobAsHTML.AsHTML() + "</p>";
				}
				catch{}
			}

			AdminEmailAlert(body, subject, recipientAddresses);
		}
		public static void AdminEmailAlert(string body, string subject, string[] recipientAddresses)
		{
			string recipients = "";
			for (int i = 0; i < recipientAddresses.Length; i++)
			{
				recipients += recipientAddresses[i];
				if (i < recipientAddresses.Length - 1)
					recipients += ",";
			}
			AdminEmailAlert(body, subject, recipients);
		}
		public static void AdminEmailAlert(string body, string subject, string recipientAddresses)
		{
			Mailer sm = new Mailer();
			sm.Body = "<p>" + body + "</p>";
			sm.TemplateType = Mailer.TemplateTypes.AdminNote;
			sm.Subject = subject;
			sm.To = recipientAddresses;
			sm.Send();
		}
		#endregion

		#region Control Helpers
		public static void AddOnClickJavascriptConfirmationForPostbackControl(HtmlControl control)
		{
			Utilities.AddOnClickJavascriptConfirmationForPostbackControl(control, "Are you sure?");
		}
		public static void AddOnClickJavascriptConfirmationForPostbackControl(HtmlControl control, string confirmationQuestion)
		{
			control.Attributes["onclick"] = "if(confirm('" + confirmationQuestion.Replace("'", "") + "')){__doPostBack('" + control.UniqueID + "','');return false;}else{return false;};";
		}
		#endregion

		#region HttpContext Utilities
		public static string GetCookieDataAsXml(HttpCookieCollection cookies)
		{
			StringBuilder cookieXmlSb = new StringBuilder("<cookies>");
			for (int i = 0; i < cookies.Count; i++)
			{
				cookieXmlSb.Append("<name=\"");
				cookieXmlSb.Append(cookies[i].Name);
				cookieXmlSb.Append("\" value=\"");
				cookieXmlSb.Append(cookies[i].Value);
				cookieXmlSb.Append("\"/>");
			}
			cookieXmlSb.Append("</cookies>");
			return cookieXmlSb.ToString();
		}
		public static string GetPostDataAsXml(System.Collections.Specialized.NameValueCollection formData)
		{
			StringBuilder formXml = new StringBuilder("<form>");
			foreach (string key in formData.Keys)
			{
				formXml.Append("<key=\"");
				formXml.Append(key);
				formXml.Append("\" value=\"");
				formXml.Append(formData[key]);
				formXml.Append("\"/>");
			}
			formXml.Append("</form>");
			return formXml.ToString();
		}
		#endregion

		#region Execute Batch File
		public static string ExecuteBatchFile(string workingDirectory, string batchFilePathAndName)
		{
			// Create the ProcessInfo object
			System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe");
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardInput = true;
			psi.RedirectStandardError = true;
			psi.WorkingDirectory = workingDirectory;

			// Start the process
			System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);


			// Open the batch file for reading
			System.IO.StreamReader strm = System.IO.File.OpenText(batchFilePathAndName);

			// Attach the output for reading
			System.IO.StreamReader sOut = proc.StandardOutput;

			// Attach the in for writing
			System.IO.StreamWriter sIn = proc.StandardInput;


			// Write each line of the batch file to standard input
			while (strm.Peek() != -1)
			{
				sIn.WriteLine(strm.ReadLine());
			}

			strm.Close();

			// Exit CMD.EXE
			string stEchoFmt = "# {0} run successfully. Exiting";

			sIn.WriteLine(String.Format(stEchoFmt, batchFilePathAndName));
			sIn.WriteLine("EXIT");

			// Close the process
			proc.Close();

			// Read the sOut to a string.
			string results = sOut.ReadToEnd().Trim();

			// Close the io Streams;
			sIn.Close();
			sOut.Close();

			return results;
		}
		#endregion

	}

	public static class StringBuilderExtentions
	{
		public static void AppendAttribute(this System.Text.StringBuilder sb, string name, string value)
		{
			sb.Append(" ");
			sb.Append(name);
			sb.Append("=\"");
			sb.Append(value.Replace("\"", "&#34;"));
			sb.Append("\"");
		}
	}
	public static class StringExtentions
	{
		public static string PrefixWithAOrAn(this string s, bool capital)
		{
			if (s == null || s.Length == 0)
				return s;

			string l = s.ToLower();

			string extraN = "";

			if (l.StartsWith("a") || l.StartsWith("e") || l.StartsWith("i") || l.StartsWith("o") || l.StartsWith("u"))
				extraN = "n";

			//Use an before unsounded h. 
			//an honorable peace 
			//an honest error 

			//When u makes the same sound as the y in you, or o makes the same sound as w in won, then a is used. 
			//a union 
			//a united front 
			//a unicorn 
			//a used napkin 
			//a U.S. ship 
			//a one-legged man 

			return (capital ? "A" : "a") + extraN + " " + s;

		}
		public static Guid UnPackGuid(this string s)
		{
			try
			{
				if (s.Length == 0)
					return Guid.Empty;

				return new Guid(Convert.FromBase64String(s.Replace("x3", "/").Replace("x2", "+").Replace("x1", "x") + "=="));
			}
			catch
			{
				return Guid.Empty;
			}
		}
	}
	public static class GuidExtentions
	{
		public static string Pack(this Guid g)
		{
			if (g == Guid.Empty)
				return "";
			
			return Convert.ToBase64String(g.ToByteArray()).Replace("x", "x1").Replace("+", "x2").Replace("/", "x3").Replace("==", string.Empty);
		}
	}
	[TestFixture]
	public class GuidExtentionsTests
	{
		[Test]
		public void Test()
		{
			Guid g = new Guid();
			string gIn = g.ToString();

			string gPacked = g.Pack();

			Guid gNew = gPacked.UnPackGuid();

			string gOut = gNew.ToString();

			Assert.IsTrue(gIn == gOut);
		}
	}



}
