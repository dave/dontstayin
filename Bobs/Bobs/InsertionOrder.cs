using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI;

using System.Collections;
using NVelocityTemplateEngine.Interfaces;
using NVelocityTemplateEngine;

namespace Bobs
{
	/// <summary>
	/// Track campaign credits and outstanding corporate IOs and Insertion Order Credits "IOCs"
	/// </summary>
	[Serializable]
	public partial class InsertionOrder : IBobReport, ILinkableAdmin, IAdminPage, IReadableReference
	{

		#region Simple members
		/// <summary>
		/// auto incrementing primary key
		/// </summary>
		public override int K
		{
			get { return this[InsertionOrder.Columns.K] as int? ?? 0; }
			set { this[InsertionOrder.Columns.K] = value; }
		}
		/// <summary>
		/// Status - Proforma = 1, Enabled = 2, Disabled = 3
		/// </summary>
		public override InsertionOrderStatus Status
		{
			get { return (InsertionOrderStatus)this[InsertionOrder.Columns.Status]; }
			set
			{
				this[InsertionOrder.Columns.Status] = value;
			}
		}
		/// <summary>
		/// (in corporate IOs, we calculate this from the banner types and impressions)
		/// </summary>
		public override int CampaignCredits
		{
			get { return (int)this[InsertionOrder.Columns.CampaignCredits]; }
			set { this[InsertionOrder.Columns.CampaignCredits] = value; }
		}
		/// <summary>
		/// this is a reminder that can be set. When the next invoice is due, this IO will pop up in the admin, 
		/// </summary>
		public override DateTime NextInvoiceDue
		{
			get { return (DateTime)this[InsertionOrder.Columns.NextInvoiceDue]; }
			set { this[InsertionOrder.Columns.NextInvoiceDue] = value; }
		}
		/// <summary>
		/// the K of the promoter to whom the insertion order applies
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[InsertionOrder.Columns.PromoterK]; }
			set { this[InsertionOrder.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// the K of the usr for the promoter to whom the insertion order applies
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[InsertionOrder.Columns.UsrK]; }
			set { this[InsertionOrder.Columns.UsrK] = value; }
		}
		/// <summary>
		/// the name the insertion order report is pertinent to if the usrK is set to -1
		/// </summary>
		public override string UsrNameOverride
		{
			get { return (string)this[InsertionOrder.Columns.UsrNameOverride]; }
			set { this[InsertionOrder.Columns.UsrNameOverride] = value; }
		}
		/// <summary>
		/// When the insertion order was created
		/// </summary>
		public override DateTime DateTimeCreated
		{
			get { return (DateTime)this[InsertionOrder.Columns.DateTimeCreated]; }
			set { this[InsertionOrder.Columns.DateTimeCreated] = value; }
		}
		/// <summary>
		/// The clients reference code for the insertion order
		/// </summary>
		public override string ClientRef
		{
			get { return (string)this[InsertionOrder.Columns.ClientRef]; }
			set { this[InsertionOrder.Columns.ClientRef] = value; }
		}
		/// <summary>
		/// The start date of the campaign
		/// </summary>
		public override DateTime CampaignStartDate
		{
			get { return (DateTime)this[InsertionOrder.Columns.CampaignStartDate]; }
			set { this[InsertionOrder.Columns.CampaignStartDate] = value; }
		}
		/// <summary>
		/// The end date of the campaign
		/// </summary>
		public override DateTime CampaignEndDate
		{
			get { return (DateTime)this[InsertionOrder.Columns.CampaignEndDate]; }
			set { this[InsertionOrder.Columns.CampaignEndDate] = value; }
		}
		/// <summary>
		/// the K of the user to be used as a traffic contact
		/// </summary>
		public override int TrafficUsrK
		{
			get { return (int)this[InsertionOrder.Columns.TrafficUsrK]; }
			set { this[InsertionOrder.Columns.TrafficUsrK] = value; }
		}
		/// <summary>
		/// Misc notes
		/// </summary>
		public override string Notes
		{
			get { return (string)this[InsertionOrder.Columns.Notes]; }
			set { this[InsertionOrder.Columns.Notes] = value; }
		}
		/// <summary>
		/// The K of the user who made the changes
		/// </summary>
		public override int ActionUsrK
		{
			get { return (int)this[InsertionOrder.Columns.ActionUsrK]; }
			set { this[InsertionOrder.Columns.ActionUsrK] = value; }
		}
		/// <summary>
		/// AgencyDiscount
		/// </summary>
		public override double AgencyDiscount
		{
			get { return (double)this[InsertionOrder.Columns.AgencyDiscount]; }
			set { this[InsertionOrder.Columns.AgencyDiscount] = value; }
		}
		/// <summary>
		/// PaymentTerms e.g. "30 days from date of invoice"
		/// </summary>
		public override string PaymentTerms
		{
			get { return (string)this[InsertionOrder.Columns.PaymentTerms]; }
			set { this[InsertionOrder.Columns.PaymentTerms] = value; }
		}
		/// <summary>
		/// InvoicePeriod  e.g. "Monthly" or "Campaign"
		/// </summary>
		public override string InvoicePeriod
		{
			get { return (string)this[InsertionOrder.Columns.InvoicePeriod]; }
			set { this[InsertionOrder.Columns.InvoicePeriod] = value; }
		}
		/// <summary>
		/// Name of the campaign
		/// </summary>
		public override string CampaignName
		{
			get { return (string)this[InsertionOrder.Columns.CampaignName]; }
			set { this[InsertionOrder.Columns.CampaignName] = value; }

		}
		/// <summary>
		/// used to prevent the InsertionOrder from being raised twice
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[InsertionOrder.Columns.DuplicateGuid]); }
			set { this[InsertionOrder.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}

		/// <summary>
		/// true if the campaign credit value was overridden
		/// </summary>
		public override bool CampaignCreditsOverriden
		{
			get { return (bool)this[InsertionOrder.Columns.CampaignCreditsOverriden]; }
			set { this[InsertionOrder.Columns.CampaignCreditsOverriden] = value; }
		}
		#endregion

		#region ILinkableAdmin Members

		public string AdminLink(params string[] par)
		{
			return ILinkableAdminExtentions.AdminLink(this, par);
		}
		public string AdminLinkNewWindow(params string[] par)
		{
			return ILinkableAdminExtentions.AdminLinkNewWindow(this, par);
		}

		#endregion

		public decimal Price
		{
			get
			{
				decimal price = 0m;
				foreach (InsertionOrderItem ioi in this.InsertionOrderItems)
				{
					price += ioi.Price;
				}
				return price;
			}
		}
	
		public decimal PriceBeforeAgencyDiscount
		{
			get
			{
				decimal price = 0m;
				foreach (InsertionOrderItem ioi in this.InsertionOrderItems)
				{
					price += ioi.PriceBeforeAgencyDiscount;
				}
				return price;
			}
		}
		
		public Usr ActionUsr { get { return new Usr(ActionUsrK); } }
		public Usr TrafficUsr { get { return new Usr(TrafficUsrK); } }
		public Usr Usr { get { return new Usr(UsrK); } }
		
		public Promoter Promoter { get { return new Promoter(PromoterK); } }
		

		public InsertionOrderItemSet InsertionOrderItems
		{
			get
			{
				Query q = new Query(new Q(InsertionOrderItem.Columns.InsertionOrderK, this.K));
				InsertionOrderItemSet insertionOrderItems = new InsertionOrderItemSet(q);
				return insertionOrderItems;
			}
		}



		#region URLs
		public static string UrlAdminNewInsertionOrder()
		{
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "insertionorderscreen");
		}

		public string UrlAdminInsertionOrderReport()
		{
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Blank, "reportgenerator", "K", this.K.ToString(), "type", "insertionorder");
		}

		public string UrlAdmin(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "insertionorderscreen", fullParams);
		}

		#endregion

		#region IBobReport Members

		public StringBuilder GenerateReportStringBuilder(bool linksEnabled)
		{
			StringBuilder sb = new StringBuilder();
			string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			INVelocityEngine embeddedEngine = NVelocityEngineFactory.CreateNVelocityAssemblyEngine(assemblyName, true);
			IDictionary context = new Hashtable();
			context["InsertionOrder"] = this;
			context["PageHeader"] = Utilities.GenerateHTMLHeaderRowString("Insertion Order");
			context["PhoneNumber"] = "020 7835 5599";
			context["FaxNumber"] = "020 7099 5886";
			string usrName = "";
			int usrK = this.UsrK;
			if (usrK == -1) usrName = this.UsrNameOverride;
			else if (usrK == 0) usrName = Promoter.AccountsName.Length > 0 ? Promoter.AccountsName : Promoter.ContactName;
			else if (usrK > 0) usrName = this.Usr.Name;
			context["Agency_ContactName"] = usrName;
			sb.Append(embeddedEngine.Process(context, "Reports.InsertionOrderReport.vm"));
			return sb;
		}

		private static string Format(string s, params string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Length == 0) args[i] = "&nbsp;";
			}
			return string.Format(s, args);
		}

		public bool IsUsrAllowedAccess(Usr usr)
		{
			return usr.IsAdmin || ((this.PromoterK > 0 && usr.IsPromoter && usr.IsPromoterK(this.PromoterK)) || (this.PromoterK == 0 && this.UsrK > 0 && usr.K == this.UsrK));

		}

		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return "IO #" + this.K; }
		}

		#endregion
	}
}
