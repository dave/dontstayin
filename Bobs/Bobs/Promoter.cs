using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;

using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;
using Bobs.Jobs;
using NVelocityTemplateEngine;


namespace Bobs
{

	#region Promoter
	/// <summary>
	/// e.g. Promoter / Event Promoter
	/// </summary>
	[Serializable]
	public partial class Promoter : IPage, IName, IReadableReference, IBobType, IObjectPage, IDeleteAll, IBuyer, ILinkable, IBobAsHTML
	{
		
		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Promoter.Columns.K] as int? ?? 0; }
			set { this[Promoter.Columns.K] = value; }
		}
		/// <summary>
		/// Name of the Promoter / Event Promoter
		/// </summary>
		public override string Name
		{
			get { return (string)this[Promoter.Columns.Name]; }
			set { this[Promoter.Columns.Name] = value; }
		}
		/// <summary>
		/// Cropped image 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Promoter.Columns.Pic]); }
			set { this[Promoter.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// The user that first signed up this promoter
		/// </summary>
		public override int PrimaryUsrK
		{
			get { return (int)this[Promoter.Columns.PrimaryUsrK]; }
			set { PrimaryUsr = null; this[Promoter.Columns.PrimaryUsrK] = value; }
		}
		/// <summary>
		/// Name of primary contact
		/// </summary>
		public override string ContactName
		{
			get { return (string)this[Promoter.Columns.ContactName]; }
			set { this[Promoter.Columns.ContactName] = value; }
		}
		/// <summary>
		/// Name of the company for billing purpouses
		/// </summary>
		public override string CompanyName
		{
			get { return (string)this[Promoter.Columns.CompanyName]; }
			set { this[Promoter.Columns.CompanyName] = value; }
		}
		/// <summary>
		/// The email address to send paypal payments to
		/// </summary>
		public override string PayPalAddress
		{
			get { return (string)this[Promoter.Columns.PayPalAddress]; }
			set { this[Promoter.Columns.PayPalAddress] = value; }
		}
		/// <summary>
		/// Contact phone number
		/// </summary>
		public override string PhoneNumber
		{
			get { return (string)this[Promoter.Columns.PhoneNumber]; }
			set { this[Promoter.Columns.PhoneNumber] = value; }
		}
		/// <summary>
		/// Billing address street
		/// </summary>
		public override string AddressStreet
		{
			get { return (string)this[Promoter.Columns.AddressStreet]; }
			set { this[Promoter.Columns.AddressStreet] = value; }
		}
		/// <summary>
		/// Billing address area
		/// </summary>
		public override string AddressArea
		{
			get { return (string)this[Promoter.Columns.AddressArea]; }
			set { this[Promoter.Columns.AddressArea] = value; }
		}
		/// <summary>
		/// Billing address town
		/// </summary>
		public override string AddressTown
		{
			get { return (string)this[Promoter.Columns.AddressTown]; }
			set { this[Promoter.Columns.AddressTown] = value; }
		}
		/// <summary>
		/// Billing address county
		/// </summary>
		public override string AddressCounty
		{
			get { return (string)this[Promoter.Columns.AddressCounty]; }
			set { this[Promoter.Columns.AddressCounty] = value; }
		}
		/// <summary>
		/// Billing address postcode
		/// </summary>
		public override string AddressPostcode
		{
			get { return (string)this[Promoter.Columns.AddressPostcode]; }
			set { this[Promoter.Columns.AddressPostcode] = value; }
		}
		/// <summary>
		/// Billing address country
		/// </summary>
		public override int AddressCountryK
		{
			get { return (int)this[Promoter.Columns.AddressCountryK]; }
			set { AddressCountry = null; this[Promoter.Columns.AddressCountryK] = value; }
		}
		/// <summary>
		/// Base pricng is multiplied by this figure.
		/// </summary>
		public override double PricingMultiplier
		{
			get { return (double)this[Promoter.Columns.PricingMultiplier]; }
			set { this[Promoter.Columns.PricingMultiplier] = value; }
		}
		/// <summary>
		/// When the promoter first signed up
		/// </summary>
		public override DateTime DateTimeSignUp
		{
			get { return (DateTime)this[Promoter.Columns.DateTimeSignUp]; }
			set { this[Promoter.Columns.DateTimeSignUp] = value; }
		}
		/// <summary>
		/// Status - AwaitingQuote=1, AwaitingPayment=2, Enabled=3, Disabled=4
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[Promoter.Columns.Status]; }
			set { this[Promoter.Columns.Status] = value; }
		}
		/// <summary>
		/// The total paid by this promoter for services
		/// </summary>
		public override decimal TotalPaid
		{
			get { return (decimal)this[Promoter.Columns.TotalPaid]; }
			set { this[Promoter.Columns.TotalPaid] = value; }
		}
		/// <summary>
		/// The date that the promoters account expires and drops to limited functionality.
		/// </summary>
		public override DateTime DateExpires
		{
			get { return (DateTime)this[Promoter.Columns.DateExpires]; }
			set { this[Promoter.Columns.DateExpires] = value; }
		}
		/// <summary>
		/// The fee for renewing membership
		/// </summary>
		public override decimal RenewalFee
		{
			get { return (decimal)this[Promoter.Columns.RenewalFee]; }
			set { this[Promoter.Columns.RenewalFee] = value; }
		}
		/// <summary>
		/// The number of months that the renewal fee is for
		/// </summary>
		public override int RenewalMonths
		{
			get { return (int)this[Promoter.Columns.RenewalMonths]; }
			set { this[Promoter.Columns.RenewalMonths] = value; }
		}
		/// <summary>
		/// Admin note
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Promoter.Columns.AdminNote]; }
			set { this[Promoter.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// Private message thread
		/// </summary>
		public override int QuestionsThreadK
		{
			get { return (int)this[Promoter.Columns.QuestionsThreadK]; }
			set { this[Promoter.Columns.QuestionsThreadK] = value; }
		}
		/// <summary>
		/// Guid used to ensure duplicate promoters don't get posted if the user refreshes the page after saving.
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Promoter.Columns.DuplicateGuid]); }
			set { this[Promoter.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Unique name used in the url
		/// </summary>
		public override string UrlName
		{
			get { return (string)this[Promoter.Columns.UrlName]; }
			set { this[Promoter.Columns.UrlName] = value; }
		}
		/// <summary>
		/// Can the promoter set up guestlist?
		/// </summary>
		public override bool HasGuestlist
		{
			get { return (bool)this[Promoter.Columns.HasGuestlist]; }
			set { this[Promoter.Columns.HasGuestlist] = value; }
		}
		/// <summary>
		/// Charge per name on the guestlist...
		/// </summary>
		public override decimal GuestlistCharge
		{
			get { return (decimal)this[Promoter.Columns.GuestlistCharge]; }
			set { this[Promoter.Columns.GuestlistCharge] = value; }
		}
		/// <summary>
		/// Number of guestlist credits that the promoter has
		/// </summary>
		public override int GuestlistCredit
		{
			get { return (int)this[Promoter.Columns.GuestlistCredit]; }
			set { this[Promoter.Columns.GuestlistCredit] = value; }
		}
		/// <summary>
		/// Amount that the promoter is alowed to go overdrawn on their guestlist credits
		/// </summary>
		public override int GuestlistCreditLimit
		{
			get { return (int)this[Promoter.Columns.GuestlistCreditLimit]; }
			set { this[Promoter.Columns.GuestlistCreditLimit] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Promoter.Columns.PicState]; }
			set { this[Promoter.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Promoter.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Promoter.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Promoter.Columns.PicMiscK]; }
			set { picMisc = null; this[Promoter.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// Calculated number of clients per month through the door
		/// </summary>
		public override int ClientsPerMonth
		{
			get { return (int)this[Promoter.Columns.ClientsPerMonth]; }
			set { this[Promoter.Columns.ClientsPerMonth] = value; }
		}
		/// <summary>
		/// Id of the last message that was successfully sent to this promoter (used in case PM sender fails)
		/// </summary>
		public override int LastMessage
		{
			get { return (int)this[Promoter.Columns.LastMessage]; }
			set { this[Promoter.Columns.LastMessage] = value; }
		}
		/// <summary>
		/// Plain text editable by sales person, only used when idle or proactive
		/// </summary>
		public override string ManualNote
		{
			get { return (string)this[Promoter.Columns.ManualNote]; }
			set { this[Promoter.Columns.ManualNote] = value; }
		}
		/// <summary>
		/// Credit limit in pounds
		/// </summary>
		public override decimal CreditLimit
		{
			get { return (decimal)this[Promoter.Columns.CreditLimit]; }
			set { this[Promoter.Columns.CreditLimit] = value; }
		}
		/// <summary>
		/// When are invoices due (days) 0 = default
		/// </summary>
		public override int InvoiceDueDays
		{
			get { return (int)this[Promoter.Columns.InvoiceDueDays]; }
			set { this[Promoter.Columns.InvoiceDueDays] = value; }
		}
		/// <summary>
		/// When was this promoter first enabled?
		/// </summary>
		public override DateTime EnabledDateTime
		{
			get { return (DateTime)this[Promoter.Columns.EnabledDateTime]; }
			set { this[Promoter.Columns.EnabledDateTime] = value; }
		}
		/// <summary>
		/// Whick admin user enabled this promoter?
		/// </summary>
		public override int EnabledByUsrK
		{
			get { return (int)this[Promoter.Columns.EnabledByUsrK]; }
			set { this[Promoter.Columns.EnabledByUsrK] = value; }
		}
		/// <summary>
		/// The sales person who owns the account / owned this account before expires date
		/// </summary>
		public override int SalesUsrK
		{
			get { return (int)this[Promoter.Columns.SalesUsrK]; }
			set { salesUsr = null; this[Promoter.Columns.SalesUsrK] = value; }
		}
		/// <summary>
		/// Status of this client before expires date (1 = New, 2 = Idle, 3 = Proactive, 4 = Active)
		/// </summary>
		public override SalesStatusEnum SalesStatus
		{
			get { return (SalesStatusEnum)this[Promoter.Columns.SalesStatus]; }
			set { this[Promoter.Columns.SalesStatus] = value; }
		}
		/// <summary>
		/// Date time when this client's sales status expires, and they become idle
		/// </summary>
		public override DateTime? SalesStatusExpires
		{
			get { return this[Promoter.Columns.SalesStatusExpires] as DateTime?; }
			set { this[Promoter.Columns.SalesStatusExpires] = value; }
		}
		/// <summary>
		/// When to make the next call - used when someone requests to be called back in a month or something
		/// </summary>
		public override DateTime SalesNextCall
		{
			get { return (DateTime)this[Promoter.Columns.SalesNextCall]; }
			set { this[Promoter.Columns.SalesNextCall] = value; }
		}
		/// <summary>
		/// What type of letter are we about to send this promoter? 1 = CurrentNewPromoter, 2 = CurrentIdlePromo
		/// </summary>
		public override LetterTypes LetterType
		{
			get { return (LetterTypes)this[Promoter.Columns.LetterType]; }
			set { this[Promoter.Columns.LetterType] = value; }
		}
		/// <summary>
		/// What is the printing status? 1 = New, 2 = Printing, 3 = Posted
		/// </summary>
		public override LetterStatusEnum LetterStatus
		{
			get { return (LetterStatusEnum)this[Promoter.Columns.LetterStatus]; }
			set { this[Promoter.Columns.LetterStatus] = value; }
		}
		/// <summary>
		/// Is the account a skeleton account? (missing some contact details)
		/// </summary>
		public override bool IsSkeleton
		{
			get { return (bool)this[Promoter.Columns.IsSkeleton]; }
			set { this[Promoter.Columns.IsSkeleton] = value; }
		}
		/// <summary>
		/// Four digit random number used to auth access code
		/// </summary>
		public override string AccessCodeRandom
		{
			get { return (string)this[Promoter.Columns.AccessCodeRandom]; }
			set { this[Promoter.Columns.AccessCodeRandom] = value; }
		}
		/// <summary>
		/// Which offer type are we showing?
		/// </summary>
		public override OfferTypes OfferType
		{
			get { return (OfferTypes)this[Promoter.Columns.OfferType]; }
			set { this[Promoter.Columns.OfferType] = value; }
		}
		/// <summary>
		/// When does the offer expire?
		/// </summary>
		public override DateTime OfferExpireDateTime
		{
			get { return (DateTime)this[Promoter.Columns.OfferExpireDateTime]; }
			set { this[Promoter.Columns.OfferExpireDateTime] = value; }
		}
		/// <summary>
		/// Estimation of how good the client will be 0=not rated, 1=crap, 2=ok, 3=good, 4=excellent
		/// </summary>
		public override SalesEstimateEnum SalesEstimate
		{
			get { return (SalesEstimateEnum)this[Promoter.Columns.SalesEstimate]; }
			set { this[Promoter.Columns.SalesEstimate] = value; }
		}
		/// <summary>
		/// Is this promoter account on hold? (No sales calls)
		/// </summary>
		public override bool SalesHold
		{
			get { return (bool)this[Promoter.Columns.SalesHold]; }
			set { this[Promoter.Columns.SalesHold] = value; }
		}
		/// <summary>
		/// Number of future events, updated overnight and when brands are added...
		/// </summary>
		public override int FutureEvents
		{
			get { return (int)this[Promoter.Columns.FutureEvents]; }
			set { this[Promoter.Columns.FutureEvents] = value; }
		}
		/// <summary>
		/// To disable the redirect when promoter account has overdue invoices for an extended period
		/// </summary>
		public override bool DisableOverdueRedirect
		{
			get { return (bool)this[Promoter.Columns.DisableOverdueRedirect]; }
			set { this[Promoter.Columns.DisableOverdueRedirect] = value; }
		}
		/// <summary>
		/// Email of primary contact
		/// </summary>
		public override string ContactEmail
		{
			get { return (string)this[Promoter.Columns.ContactEmail]; }
			set { this[Promoter.Columns.ContactEmail] = value; }
		}
		/// <summary>
		/// Title of primary contact
		/// </summary>
		public override string ContactTitle
		{
			get { return (string)this[Promoter.Columns.ContactTitle]; }
			set { this[Promoter.Columns.ContactTitle] = value; }
		}
		/// <summary>
		/// Personal title of primary contact
		/// </summary>
		public override string ContactPersonalTitle
		{
			get { return (string)this[Promoter.Columns.ContactPersonalTitle]; }
			set { this[Promoter.Columns.ContactPersonalTitle] = value; }
		}
		/// <summary>
		/// Promoter's 2nd phone number
		/// </summary>
		public override string PhoneNumber2
		{
			get { return (string)this[Promoter.Columns.PhoneNumber2]; }
			set { this[Promoter.Columns.PhoneNumber2] = value; }
		}
		/// <summary>
		/// Promoter's primary website address
		/// </summary>
		public override string WebAddress
		{
			get { return (string)this[Promoter.Columns.WebAddress]; }
			set { this[Promoter.Columns.WebAddress] = value; }
		}
		/// <summary>
		/// Alarm for SalesUsr when next call time arrives
		/// </summary>
		public override bool Alarm
		{
			get { return (bool)this[Promoter.Columns.Alarm]; }
			set { this[Promoter.Columns.Alarm] = value; }
		}
		/// <summary>
		/// Name of accounts contact
		/// </summary>
		public override string AccountsName
		{
			get { return (string)this[Promoter.Columns.AccountsName]; }
			set { this[Promoter.Columns.AccountsName] = value; }
		}
		/// <summary>
		/// Email of accounts contact
		/// </summary>
		public override string AccountsEmail
		{
			get { return (string)this[Promoter.Columns.AccountsEmail]; }
			set { this[Promoter.Columns.AccountsEmail] = value; }
		}
		/// <summary>
		/// Phone number of accounts contact
		/// </summary>
		public override string AccountsPhone
		{
			get { return (string)this[Promoter.Columns.AccountsPhone]; }
			set { this[Promoter.Columns.AccountsPhone] = value; }
		}
		/// <summary>
		/// Client Sector: Promoter, Agency, Mobile operator, etc.
		/// </summary>
		public override ClientSectorEnum ClientSector
		{
			get { return (ClientSectorEnum)this[Promoter.Columns.ClientSector]; }
			set { this[Promoter.Columns.ClientSector] = value; }
		}
		/// <summary>
		/// Has Promoter completed tickets/credit application form and been approved
		/// </summary>
		public override bool EnableTickets
		{
			get { return (bool)this[Promoter.Columns.EnableTickets]; }
			set { this[Promoter.Columns.EnableTickets] = value; }
		}
		/// <summary>
		/// Who was the promoter added by (e.g. for sales admins)
		/// </summary>
		public override int AddedByUsrK
		{
			get { return (int)this[Promoter.Columns.AddedByUsrK]; }
			set { this[Promoter.Columns.AddedByUsrK] = value; }
		}
		/// <summary>
		/// How was this promoter added to the site (1=By end user on the site, 2=By sales user in the backend, 
		/// </summary>
		public override AddedMedhods AddedMethod
		{
			get { return (AddedMedhods)this[Promoter.Columns.AddedMethod]; }
			set { this[Promoter.Columns.AddedMethod] = value; }
		}
		/// <summary>
		/// Enum for Promoter's VAT status: 0 = unknown, 1 = not registered, 2 = registered
		/// </summary>
		public override VatStatusEnum VatStatus
		{
			get { return (VatStatusEnum)this[Promoter.Columns.VatStatus]; }
			set { this[Promoter.Columns.VatStatus] = value; }
		}
		/// <summary>
		/// Promoter's VAT number
		/// </summary>
		public override string VatNumber
		{
			get { return (string)this[Promoter.Columns.VatNumber]; }
			set { this[Promoter.Columns.VatNumber] = value; }
		}
		/// <summary>
		/// Country K in which the Promoter is VAT registered
		/// </summary>
		public override int VatCountryK
		{
			get { return (int)this[Promoter.Columns.VatCountryK]; }
			set { this[Promoter.Columns.VatCountryK] = value; }
		}
        /// <summary>
        /// Promoter's bank name
        /// </summary>
        public override string BankName
        {
            get { return (string)this[Promoter.Columns.BankName]; }
            set { this[Promoter.Columns.BankName] = value.Trim(); }
        }
        /// <summary>
        /// Promoter's bank account name
        /// </summary>
        public override string BankAccountName
        {
            get { return (string)this[Promoter.Columns.BankAccountName]; }
            set { this[Promoter.Columns.BankAccountName] = value.Trim(); }
        }
        /// <summary>
        /// Promoter's bank account sort code
        /// </summary>
        public override string BankAccountSortCode
        {
            get { return (string)this[Promoter.Columns.BankAccountSortCode]; }
            set { this[Promoter.Columns.BankAccountSortCode] = value.Replace(" ", "").Replace("-", ""); }
        }
        /// <summary>
        /// Promoter's bank account number
        /// </summary>
        public override string BankAccountNumber
        {
            get { return (string)this[Promoter.Columns.BankAccountNumber]; }
            set { this[Promoter.Columns.BankAccountNumber] = value.Replace(" ", "").Replace("-", ""); }
        }
        /// <summary>
        /// Override applying of ticket funds to unpaid invoices
        /// </summary>
        public override bool OverrideApplyTicketFundsToInvoices
        {
            get { return (bool)this[Promoter.Columns.OverrideApplyTicketFundsToInvoices]; }
            set { this[Promoter.Columns.OverrideApplyTicketFundsToInvoices] = value; }
        }
		/// <summary>
		/// Number of sales calls made to this promoter
		/// </summary>
		public override int SalesCallCount
		{
			get { return (int)this[Promoter.Columns.SalesCallCount]; }
			set { this[Promoter.Columns.SalesCallCount] = value; }
		}
		/// <summary>
		/// Has this promoter been recently transferred to this sales user?
		/// </summary>
		public override bool RecentlyTransferred
		{
			get { return (bool)this[Promoter.Columns.RecentlyTransferred]; }
			set { this[Promoter.Columns.RecentlyTransferred] = value; }
		}
		/// <summary>
		/// if the promoter is an agency or not
		/// </summary>
		public override bool IsAgency
		{
			get { return (bool)this[Promoter.Columns.IsAgency]; }
			set { this[Promoter.Columns.IsAgency] = value; }
		}
		/// <summary>
		/// Discount percentage as an integer
		/// </summary>
		public override int Discount
		{
			get { return (int)this[Promoter.Columns.Discount]; }
			set { this[Promoter.Columns.Discount] = value; }
		}
		/// <summary>
		/// Add a random code to tickets, to be displayed on doorlists
		/// </summary>
		public override bool AddRandomCodeToTickets
		{
			get { return (bool)this[Promoter.Columns.AddRandomCodeToTickets]; }
			set { this[Promoter.Columns.AddRandomCodeToTickets] = value; }
		}
		/// <summary>
		/// Does this promoter want to confirm card details with us to avoid responsibility for card payments?
		/// </summary>
		public override bool WillCheckCardsForPurchasedTickets
		{
			get { return (bool)this[Promoter.Columns.WillCheckCardsForPurchasedTickets]; }
			set { this[Promoter.Columns.WillCheckCardsForPurchasedTickets] = value; }
		}
		/// <summary>
		/// If this promoter was added in a sales campaign, this is it
		/// </summary>
		public override int SalesCampaignK
		{
			get { return (int)this[Promoter.Columns.SalesCampaignK]; }
			set { this[Promoter.Columns.SalesCampaignK] = value; }
		}
		/// <summary>
		/// Cost per campaign credit
		/// </summary>
		public override decimal CostPerCampaignCredit
		{
			get
			{
				if ((decimal)this[Promoter.Columns.CostPerCampaignCredit] > 0.0m)
					return (decimal)this[Promoter.Columns.CostPerCampaignCredit];
				else
					return 1.0m;
			}
			set { this[Promoter.Columns.CostPerCampaignCredit] = value; }
		}
		/// <summary>
		/// Dont send this promoter reminder emails when they havent paid
		/// </summary>
		public override bool SuspendReminderEmails
		{
			get { return (bool)this[Promoter.Columns.SuspendReminderEmails]; }
			set { this[Promoter.Columns.SuspendReminderEmails] = value; }
		}
		#endregion

		#region ILinkable Members

		public string Link(params string[] par)
		{
			return ILinkableExtentions.Link(this, par);
		}
		public string LinkNewWindow(params string[] par)
		{
			return ILinkableExtentions.LinkNewWindow(this, par);
		}

		#endregion

		#region IBobAsHTML methods
		public string AsHTML()
        {
            string lineReturn = Vars.HTML_LINE_RETURN;
            StringBuilder sb = new StringBuilder();

            sb.Append(lineReturn);
            sb.Append(lineReturn);
            sb.Append("<u>Promoter details</u>");
			sb.Append(lineReturn);
            sb.Append("K: ");
            sb.Append(this.K.ToString());
            sb.Append(lineReturn);
            sb.Append("Name: ");
            sb.Append(this.Name);
            sb.Append(lineReturn);
            if(this.PrimaryUsr != null)
            {
                sb.Append("Primary usr: ");
                sb.Append(this.PrimaryUsr.NickName);
                sb.Append(" (K: ");
                sb.Append(this.PrimaryUsrK.ToString());
                sb.Append(")");
                sb.Append(lineReturn);
            }
            sb.Append("Address: ");
			sb.Append(lineReturn);
			sb.Append(this.AddressHtml);
            sb.Append(lineReturn);
            sb.Append(this.BankDetailsHTML);
            //sb.Append(lineReturn);
            sb.Append("Credit limit: ");
            sb.Append(this.CreditLimit.ToString("c"));
            sb.Append(lineReturn);
            sb.Append("Plus account: ");
            sb.Append(this.EnableTickets ? "enabled" : "disabled");
            sb.Append(lineReturn);
            if(this.SalesUsr != null)
            {
                sb.Append("Sales usr: ");
                sb.Append(this.SalesUsr.NickName);
                sb.Append(lineReturn);
            }
            sb.Append("Vat status: ");
            sb.Append(this.VatStatus.ToString());
            sb.Append(lineReturn);
            if (this.VatNumber != "")
            {
                sb.Append("Vat #: ");
                sb.Append(this.VatNumber);
                sb.Append(lineReturn);
            }

            return sb.ToString();
		}
		#endregion

		public int CampaignCredits
		{
			get
			{
				string sum = "CampaignCredits";
				Query q = new Query(new And(new Q(CampaignCredit.Columns.PromoterK, this.K),
										new Q(CampaignCredit.Columns.Enabled, true)));
				q.Columns = new ColumnSet();
				q.ExtraSelectElements.Add(sum, "Sum([Credits])");
				q.GroupBy = new GroupBy(CampaignCredit.Columns.PromoterK);

				CampaignCreditSet ccs = new CampaignCreditSet(q);
				if (ccs.Count == 0)
				{
					return 0;
				}
				return (int)ccs[0].ExtraSelectElements[sum];
			}
		}



		public bool IsNewLead
		{
			get
			{
				return (AddedMethod.Equals(AddedMedhods.SalesUser) && DateTimeSignUp.AddDays(7) > DateTime.Now);
			}
		}

		#region NewIdleOrder
		/// <summary>
		/// SalesEstimate = Excellent
		///	SalesEstimate = Good
		///	SalesEstimate = null, Status = Idle after active
		///	SalesEstimate = Average
		///	SalesEstimate = null, Status = New
		///	SalesEstimate = null, Status = Idle after proactive
		///	SalesEstimate = null, Status = Idle
		///	SalesEstimate = Poor
		///	SalesEstimate = Rubbish
		/// 
		/// Then SalesCall Ascending
		/// Then DateTimeSignUp Ascending

		/// </summary>
		public static OrderBy NewIdleOrder
		{
			get
			{
				return new OrderBy(
				new OrderBy(@"(CASE 
WHEN [Promoter].[SalesEstimate]=5 THEN 0 
WHEN [Promoter].[SalesEstimate]=4 THEN 1 
WHEN ([Promoter].[SalesEstimate]=0 OR [Promoter].[SalesEstimate] IS NULL) AND [Promoter].[SalesStatus]=4 THEN 2 
WHEN [Promoter].[SalesEstimate]=3 THEN 3 
WHEN ([Promoter].[SalesEstimate]=0 OR [Promoter].[SalesEstimate] IS NULL) AND [Promoter].[SalesStatus]=1 THEN 4 
WHEN ([Promoter].[SalesEstimate]=0 OR [Promoter].[SalesEstimate] IS NULL) AND [Promoter].[SalesStatus]=3 THEN 5 
WHEN ([Promoter].[SalesEstimate]=0 OR [Promoter].[SalesEstimate] IS NULL) AND [Promoter].[SalesStatus]=2 THEN 6 
WHEN [Promoter].[SalesEstimate]=2 THEN 7 
WHEN [Promoter].[SalesEstimate]=1 THEN 8 
ELSE 9 END)"),
				new OrderBy(@"(CASE WHEN [Promoter].[SalesNextCall] IS NULL THEN 1 ELSE 0 END)"),
				new OrderBy(Promoter.Columns.SalesNextCall, OrderBy.OrderDirection.Ascending),
				new OrderBy(Promoter.Columns.DateTimeSignUp, OrderBy.OrderDirection.Ascending));
			}
		}
		#endregion

		#region Enums
		#region OfferTypes
		#endregion

		#region LetterTypes
		#endregion

		#region LetterStatusEnum
		#endregion

		#region SalesStatusEnum

        //public static ListItem[] SalesStatusEnumAsListItemArray()
        //{
        //    List<ListItem> ListItems = new List<ListItem>();
        //    ListItems.Add(new ListItem(SalesStatusEnum.New.ToString(), Convert.ToInt32(SalesStatusEnum.New).ToString()));
        //    ListItems.Add(new ListItem(SalesStatusEnum.Idle.ToString(), Convert.ToInt32(SalesStatusEnum.Idle).ToString()));
        //    ListItems.Add(new ListItem(SalesStatusEnum.Proactive.ToString(), Convert.ToInt32(SalesStatusEnum.Proactive).ToString()));
        //    ListItems.Add(new ListItem(SalesStatusEnum.Active.ToString(), Convert.ToInt32(SalesStatusEnum.Active).ToString()));
        //    ListItems.Add(new ListItem(SalesStatusEnum.Sleeping.ToString(), Convert.ToInt32(SalesStatusEnum.Sleeping).ToString()));

        //    return ListItems.ToArray();
        //}
		#endregion

		#region StatusEnum
		#endregion

		#region ClientSectorEnum

        //public static ListItem[] ClientSectorEnumAsListItemArray()
        //{
        //    List<ListItem> ListItems = new List<ListItem>();
        //    ListItems.Add(new ListItem(ClientSectorEnum.Accomodation.ToString(), Convert.ToInt32(ClientSectorEnum.Accomodation).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Alcohol.ToString(), Convert.ToInt32(ClientSectorEnum.Alcohol).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Apparel.ToString(), Convert.ToInt32(ClientSectorEnum.Apparel).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Cameras.ToString(), Convert.ToInt32(ClientSectorEnum.Cameras).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.COIGov.ToString()), Convert.ToInt32(ClientSectorEnum.COIGov).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.FilmIndustry.ToString()), Convert.ToInt32(ClientSectorEnum.FilmIndustry).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Finance.ToString(), Convert.ToInt32(ClientSectorEnum.Finance).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Food.ToString(), Convert.ToInt32(ClientSectorEnum.Food).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.HomeEntertainment.ToString()), Convert.ToInt32(ClientSectorEnum.HomeEntertainment).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Insurance.ToString(), Convert.ToInt32(ClientSectorEnum.Insurance).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.MajorEvents.ToString()), Convert.ToInt32(ClientSectorEnum.MajorEvents).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.MediaAgency.ToString()), Convert.ToInt32(ClientSectorEnum.MediaAgency).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Miscellaneous.ToString(), Convert.ToInt32(ClientSectorEnum.Miscellaneous).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.MobileComms.ToString()), Convert.ToInt32(ClientSectorEnum.MobileComms).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.MultipleVenues.ToString()), Convert.ToInt32(ClientSectorEnum.MultipleVenues).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.MusicIndustry.ToString()), Convert.ToInt32(ClientSectorEnum.MusicIndustry).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.None.ToString(), Convert.ToInt32(ClientSectorEnum.None).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.PersonalHealth.ToString()), Convert.ToInt32(ClientSectorEnum.PersonalHealth).ToString()));
        //    ListItems.Add(new ListItem(ClientSectorEnum.Promoter.ToString(), Convert.ToInt32(ClientSectorEnum.Promoter).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.SoftDrink.ToString()), Convert.ToInt32(ClientSectorEnum.SoftDrink).ToString()));
        //    ListItems.Add(new ListItem(Utilities.CamelCaseToString(ClientSectorEnum.TravelTransport.ToString()), Convert.ToInt32(ClientSectorEnum.TravelTransport).ToString()));
        //    return ListItems.ToArray();
        //}
		#endregion

		#region VatStatusEnum

		public static ListItem[] VatStatusEnumAsListItemArray()
		{
			List<ListItem> ListItems = new List<ListItem>();
			ListItems.Add(new ListItem(Utilities.CamelCaseToString(VatStatusEnum.NotRegistered.ToString()), Convert.ToInt32(VatStatusEnum.NotRegistered).ToString()));
			ListItems.Add(new ListItem(VatStatusEnum.Registered.ToString(), Convert.ToInt32(VatStatusEnum.Registered).ToString()));
			ListItems.Add(new ListItem(VatStatusEnum.Unknown.ToString(), Convert.ToInt32(VatStatusEnum.Unknown).ToString()));

			return ListItems.ToArray();
		}
		#endregion
		#endregion

		#region RecomputeFutureEvents
		public static void RecomputeFutureEvents()
		{
			System.Console.WriteLine("Updating Promoter.RecomputeFutureEvents...");

			try
			{
				Db.Qu(@"
WITH Cte(Events, K) AS
	(
	SELECT COUNT(DISTINCT [Event].[K]) AS Events, [Promoter].[K] FROM [Event] WITH (NOLOCK)
	LEFT JOIN [EventBrand] ON [Event].[K] = [EventBrand].[EventK]
	LEFT JOIN [Brand] ON [EventBrand].[BrandK] = [Brand].[K]
	LEFT JOIN [Venue] ON [Event].[VenueK] = [Venue].[K]
	INNER JOIN [Promoter] ON ([Brand].[PromoterK] = [Promoter].[K] OR [Venue].[PromoterK] = [Promoter].[K])
	WHERE [Event].[DateTime] > GetDate() 
	GROUP BY [Promoter].[K]
	)
UPDATE [Promoter] WITH (ROWLOCK) 
SET [FutureEvents] = (SELECT Events from Cte WHERE [Promoter].[K] = Cte.K)
WHERE NOT [FutureEvents] = (SELECT Events from Cte WHERE [Promoter].[K] = Cte.K)
", 600);
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception recomputing Promoter.RecomputeFutureEvents</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception recomputing Promoter.RecomputeFutureEvents";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}
			System.Console.WriteLine("Done updating Promoter.RecomputeFutureEvents...", 1);
		}
		#endregion

		#region InitialiseSkeletonAndAddPrimaryUser
		public void InitialiseSkeletonAndAddPrimaryUser(Usr CurrentUsr, string CompanyNameUpdate, string ContactNameUpdate, string PhoneUpdate)
		{
			//update the promoter
			bool addToPromoter = true;
			foreach (Usr u in this.AdminUsrs)
			{
				if (u.K == CurrentUsr.K)
					addToPromoter = false;
			}

			if (addToPromoter)
			{
				PromoterUsr pu = new PromoterUsr();
				pu.PromoterK = this.K;
				pu.UsrK = CurrentUsr.K;
				pu.Update();
				this.AdminUsrs = null;
				if (this.PrimaryUsrK == 0)
				{
					this.PrimaryUsrK = CurrentUsr.K;
				}
			}

			if (this.LetterType.Equals(Promoter.LetterTypes.AutoVenue))
			{
				//conirm venues
				foreach (Venue v in this.AllVenues)
				{
					v.PromoterStatus = Venue.PromoterStatusEnum.Confirmed;
					v.Update();
				}
			}

			//	if (false) // When should be confirm party brands?
			//	{
			//		//conirm brands
			//		foreach (Brand b in CurrentPromoter.AllBrands)
			//		{
			//			b.PromoterStatus = Brand.PromoterStatusEnum.Confirmed;
			//			b.Update();
			//		}
			//	}

			if (CompanyNameUpdate.Length > 0)
				this.Name = Cambro.Web.Helpers.Strip(CompanyNameUpdate);

			if (ContactNameUpdate.Length > 0)
				this.ContactName = Cambro.Web.Helpers.Strip(ContactNameUpdate);

			if (PhoneUpdate.Length > 0)
				this.PhoneNumber = Cambro.Web.Helpers.Strip(PhoneUpdate);

			if (this.IsSkeleton)
			{
				this.IsSkeleton = false;
				this.DateTimeSignUp = DateTime.Now;
			}

			if (!this.OfferType.Equals(Promoter.OfferTypes.DoubleSlots))
			{
				this.OfferType = Promoter.OfferTypes.DoubleSlots;
				this.OfferExpireDateTime = DateTime.Now.AddDays(32);
			}

			this.Status = Promoter.StatusEnum.Active;
			this.SalesHold = false;
			this.SalesNextCall = DateTime.Today;

			if (this.QuestionsThreadK == 0)
				this.AddQuestionsThread(CurrentUsr, this.Name);

			this.Update();
			this.CreateUniqueUrlName();
			this.UpdateModerators();
		}
		#endregion

		#region InvoiceDueDaysEffective
		public int InvoiceDueDaysEffective
		{
			get
			{
				if (this.CreditLimit == 0)
					return 0;
				else if (InvoiceDueDays == 0)
					return Vars.InvoiceDueDaysDefault;
				else
					return InvoiceDueDays;
			}
		}
		#endregion

		#region IsUsrAllowedAccess
		public bool IsUsrAllowedAccess(Usr usr)
		{
			return usr.IsAdmin || (this.K > 0 && usr.IsPromoter && usr.IsPromoterK(this.K));
		}
		#endregion

		#region SalesEstimateEnum

		public static ListItem[] SalesEstimatesAsListItemArray()
		{
			List<ListItem> ListItems = new List<ListItem>();
			ListItems.Add(new ListItem(SalesEstimateEnum.Rubbish.ToString(), Convert.ToInt32(SalesEstimateEnum.Rubbish).ToString()));
			ListItems.Add(new ListItem(SalesEstimateEnum.Poor.ToString(), Convert.ToInt32(SalesEstimateEnum.Poor).ToString()));
			ListItems.Add(new ListItem(SalesEstimateEnum.Average.ToString(), Convert.ToInt32(SalesEstimateEnum.Average).ToString()));
			ListItems.Add(new ListItem(SalesEstimateEnum.Good.ToString(), Convert.ToInt32(SalesEstimateEnum.Good).ToString()));
			ListItems.Add(new ListItem(SalesEstimateEnum.Excellent.ToString(), Convert.ToInt32(SalesEstimateEnum.Excellent).ToString()));

			return ListItems.ToArray();
		}

		#endregion

		#region SalesEstimateString
		public string SalesEstimateString
		{
			get
			{
				if (this.SalesEstimate == SalesEstimateEnum.None)
					return "";
				else
					return this.SalesEstimate.ToString();
			}
		}
		#endregion

		#region AddNote
		public bool AddNote(string Note, Guid DuplicateGuid, Usr CurrentUsr)
		{
			return AddNote(Note, DuplicateGuid, CurrentUsr, false);
		}

		public bool AddNote(string Note, Guid DuplicateGuid, Usr CurrentUsr, bool isImportant)
		{
			Query qDup = new Query();
			qDup.QueryCondition = new Q(SalesCall.Columns.DuplicateGuid, DuplicateGuid);
			SalesCallSet scsDup = new SalesCallSet(qDup);
			if (scsDup.Count == 0)
			{
				SalesCall scNote = new SalesCall();
				scNote.DuplicateGuid = DuplicateGuid;
				scNote.UsrK = CurrentUsr.K;
				scNote.PromoterK = this.K;
				scNote.DateTimeStart = DateTime.Now;
				scNote.DateTimeEnd = scNote.DateTimeStart;
				scNote.Duration = 0;
				scNote.Dismissed = true;
				scNote.InProgress = false;
				scNote.Direction = SalesCall.Directions.Outgoing;
				if (this.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.Active))
					scNote.Type = SalesCall.Types.Active;
				else if (this.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.Proactive))
					scNote.Type = SalesCall.Types.ProactiveFollowUp;
				else
					scNote.Type = SalesCall.Types.Cold;
				scNote.IsCall = false;
				scNote.Note = Note;
				scNote.Effective = true;
				scNote.IsImportant = isImportant;
				scNote.Update();

				this.ManualNote = Note + " [" + CurrentUsr.NickName + " " + DateTime.Now.ToShortDateString() + "]\n" + this.ManualNote;

				this.UpdateSalesCallCount(false);

				this.Update();


				return true;
			}
			return false;
		}
		#endregion

		public void UpdateSalesCallCount(bool update)
		{
			Query q = new Query();
			q.QueryCondition = new Q(SalesCall.Columns.PromoterK, this.K);
			q.ReturnCountOnly = true;
			SalesCallSet scs = new SalesCallSet(q);

			if (this.SalesCallCount != scs.Count)
			{
				this.SalesCallCount = scs.Count;
				if (update)
					this.Update();
			}
		}
				
		#region PrimaryUsrLink
		public string PrimaryUsrLink
		{
			get
			{
				try
				{
					return PrimaryUsr.Link();
				}
				catch
				{
					return "";
				}
			}
		}
		#endregion		

		#region EffectiveSalesStatus
		public SalesStatusEnum EffectiveSalesStatus
		{
			get
			{
				if (this.SalesStatus.Equals(SalesStatusEnum.New))
					return SalesStatusEnum.New;
				else if (this.SalesStatus.Equals(SalesStatusEnum.Idle))
					return SalesStatusEnum.Idle;
				else if (this.SalesStatus.Equals(SalesStatusEnum.Active))
				{
					if (this.SalesStatusExpires > DateTime.Now)
						return SalesStatusEnum.Active;
					else
						return SalesStatusEnum.Idle;
				}
				else if (this.SalesStatus.Equals(SalesStatusEnum.Proactive))
				{
					if (this.SalesStatusExpires > DateTime.Now)
						return SalesStatusEnum.Proactive;
					else
						return SalesStatusEnum.Idle;
				}
				else
					return SalesStatusEnum.New;

			}
		}
		#endregion

		#region SalesNextCallRender
		public string SalesNextCallRender
		{
			get
			{
				if (this.SalesNextCall.Equals(DateTime.MinValue))
					return "";
				else
					return Cambro.Misc.Utility.FriendlyDate(this.SalesNextCall, true);
			}
		}
		#endregion

		#region SetSalesStatusExpires
		public void SetSalesStatusExpires(DateTime newDate)
		{
			if (newDate > this.SalesStatusExpires)
				this.SalesStatusExpires = newDate;
		}
		#endregion

		#region SalesUsr
		public Usr SalesUsr
		{
			get
			{
				if (salesUsr == null && SalesUsrK > 0)
					salesUsr = new Usr(SalesUsrK, this, Promoter.Columns.SalesUsrK);
				return salesUsr;
			}
			set
			{
				salesUsr = value;
			}
		}
		Usr salesUsr;
		#endregion

		#region ActivateRefresh
		public void ActivateRefresh()
		{
			if (!this.Status.Equals(Promoter.StatusEnum.Active))
			{
				this.EnabledDateTime = DateTime.Now;
				this.EnabledByUsrK = Usr.Current.K;
			}

			this.Status = Promoter.StatusEnum.Active;
			this.Update();
			this.UpdateModerators();
		}
		#endregion
		#region Disable
		public void Disable()
		{
			this.Status = Promoter.StatusEnum.Disabled;
			this.Update();
			this.UpdateModerators();
		}
		#endregion

		#region UpdateModerators
		public void UpdateModerators()
		{
			if (this.IsEnabled)
			{
				foreach (Brand brand in this.AllBrands)
				{
					if (brand.PromoterStatus == Brand.PromoterStatusEnum.Confirmed)
					{
						foreach (Usr u in this.AdminUsrs)
						{
							brand.Group.ChangeUsr(false, u.K, true, true, true, true, Bobs.GroupUsr.StatusEnum.Member, u.DateTimeSignUp, false);
						}
					}
					
					brand.Group.UpdateTotalMembers();
				}

				foreach (Usr u in this.AdminUsrs)
					u.UpdateIsPromoter();

				if (this.Status.Equals(Promoter.StatusEnum.Active))
				{
					Group dontStayInPromotersGroup = new Group(3684); //DontStayIn Promoters group
					foreach (Usr usr in this.AdminUsrs)
					{
						GroupUsr gu = usr.GetGroupUsr(dontStayInPromotersGroup.K);
						usr.AddToPromotersGroup(gu, dontStayInPromotersGroup);
					}
					dontStayInPromotersGroup.UpdateTotalMembers();
				}
			}
			//CreatePromoterXmlFragmentStart(); //Not sure why it doesn't work...
		}
		#endregion

		#region HasConfirmedBrands
		public bool HasConfirmedBrands()
		{
			ConfirmedBrands = null;
			return this.ConfirmedBrands.Count > 0;
		}
		#endregion
		#region HasConfirmedVenues
		public bool HasConfirmedVenues()
		{
			ConfirmedVenues = null;
			return this.ConfirmedVenues.Count > 0;
		}
		#endregion

		#region Maximizer XML export
		public System.Threading.Thread CreatePromoterXmlFragmentThread;

		public void CreatePromoterXmlFragmentStart()
		{
			//This doesn't work - not sure why...
			try
			{
				System.Threading.Thread thread = Utilities.GetSafeThread(CreatePromoterXmlFragment);
				thread.Start();
			}
			catch (Exception ex) { Bobs.Global.Log("d7c530b0-cdff-11da-a94d-0800200c9a66", ex); }

		}
		public void CreatePromoterXmlFragment()
		{
			try
			{
				CreatePromoterXml(this.K);
			}
			catch (Exception ex) { Bobs.Global.Log("60a58b20-cdfe-11da-a94d-0800200c9a66", ex); }
		}

		#region CreatePromoterXml
		public static void CreatePromoterXml()
		{
			CreatePromoterXml(0);
		}
		public static void CreatePromoterXml(int promoterK)
		{
			string fileSystemPath = "";

			Query qu = new Query();
			qu.QueryCondition = new Q(Promoter.Columns.Status, QueryOperator.NotEqualTo, Promoter.StatusEnum.Disabled);
			qu.OrderBy = new OrderBy(Promoter.Columns.Name);
			if (promoterK > 0)
			{
				qu.QueryCondition = new And(qu.QueryCondition, new Q(Promoter.Columns.K, promoterK));
				fileSystemPath = @"\\jabba\d$\Shared\Maximizer\Promoters\" + promoterK.ToString() + ".mxi";
				
				string fileSystemPathBatch = "";
				fileSystemPathBatch = @"\\jabba\d$\Shared\Maximizer\Promoters\import-" + promoterK.ToString() + ".bat";

				File.Delete(fileSystemPathBatch);

				using (StreamWriter swBatch = new StreamWriter(fileSystemPathBatch))
				{
					swBatch.Write(@"""c:\Program Files\Maximizer\Maxwin.exe"" /DATABASE ""DontStayIn"" /USERID ""MASTER"" /PASSWORD ""author72insert"" /FILE=""Z:\Maximizer\Promoters\" + promoterK.ToString() + @".mxi""");
				}

			}
			else
			{
				if (Vars.DevEnv)
					fileSystemPath = @"C:\inetpub\DontStayIn\PromoterXmlTmp\promoters.mxi";
				else
					fileSystemPath = @"\\" + Vars.ExtraIp + @"\d$\DontStayIn\Live\DontStayInTemp\promoters.mxi";
			}

			PromoterSet ps = new PromoterSet(qu);

			File.Delete(fileSystemPath);

			using (FileStream fs = File.OpenWrite(fileSystemPath))
			{
				XmlTextWriter x = new XmlTextWriter(fs, System.Text.Encoding.UTF8);
				x.Formatting = Formatting.Indented;

				#region AllData
				x.WriteStartElement("AllData");
				#region DetailDef NickName
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "NickName");
				x.WriteAttributeString("Type", "String");
				x.WriteAttributeString("Length", "20");
				x.WriteAttributeString("Companies", "No");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "Yes");
				x.WriteEndElement();	//DetailDef
				#endregion
				#region DetailDef Brands
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "Brands");
				x.WriteAttributeString("Type", "String");
				x.WriteAttributeString("Length", "255");
				x.WriteAttributeString("Companies", "Yes");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "No");
				x.WriteEndElement();	//DetailDef
				#endregion
				#region DetailDef Venues
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "Venues");
				x.WriteAttributeString("Type", "String");
				x.WriteAttributeString("Length", "255");
				x.WriteAttributeString("Companies", "Yes");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "No");
				x.WriteEndElement();	//DetailDef
				#endregion
				#region DetailDef Discount
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "Discount");
				x.WriteAttributeString("Type", "Number");
				x.WriteAttributeString("Length", "0");
				x.WriteAttributeString("Companies", "Yes");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "No");
				x.WriteEndElement();	//DetailDef
				#endregion
				#region DetailDef Enabled
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "Enabled");
				x.WriteAttributeString("Type", "Number");
				x.WriteAttributeString("Length", "0");
				x.WriteAttributeString("Companies", "Yes");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "No");
				x.WriteEndElement();	//DetailDef
				#endregion
				#region DetailDef SignedUp
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "SignedUp");
				x.WriteAttributeString("Type", "Date");
				x.WriteAttributeString("Length", "0");
				x.WriteAttributeString("Companies", "Yes");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "No");
				x.WriteEndElement();	//DetailDef
				#endregion
				#region DetailDef Revenue
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "Revenue");
				x.WriteAttributeString("Type", "Number");
				x.WriteAttributeString("Length", "2");
				x.WriteAttributeString("Companies", "Yes");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "No");
				x.WriteEndElement();	//DetailDef
				#endregion
				#region DetailDef ClientsPerMonth
				x.WriteStartElement("DetailDef");
				x.WriteAttributeString("Name", "ClientsPerMonth");
				x.WriteAttributeString("Type", "Number");
				x.WriteAttributeString("Length", "0");
				x.WriteAttributeString("Companies", "Yes");
				x.WriteAttributeString("Individuals", "No");
				x.WriteAttributeString("Contacts", "No");
				x.WriteEndElement();	//DetailDef
				#endregion
				for (DateTime d = new DateTime(2005, 1, 1); d < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); d = d.AddMonths(1))
				{
					#region DetailDef Sales\Revenue YYYY-MM
					x.WriteStartElement("DetailDef");
					x.WriteAttributeString("Name", "Sales\\Revenue " + d.ToString("yyyy-MM"));
					x.WriteAttributeString("Type", "Number");
					x.WriteAttributeString("Length", "2");  // ********
					x.WriteAttributeString("Companies", "Yes");
					x.WriteAttributeString("Individuals", "No");
					x.WriteAttributeString("Contacts", "No");
					x.WriteEndElement();	//DetailDef
					#endregion
				}
				for (int count = 0; count < ps.Count; count++)
				{
					Promoter p = ps[count];

					//if (count % 10 == 0)
					Cambro.Web.Helpers.WriteAlert("Processing promoter (" + count + "/" + ps.Count + "): " + p.UrlName, 1);

					#region Company
					x.WriteStartElement("Company");
					x.WriteElementString("Name", Cambro.Misc.Utility.Snip(p.Name, 41));
					x.WriteElementString("Id", "Promoter-" + p.K);
					x.WriteElementString("Website", "http://www.dontstayin.com" + p.Url());
					#region Phone
					x.WriteStartElement("Phone");
					x.WriteElementString("Number", PromoterXmlFixPhone(p.PhoneNumber));
					x.WriteEndElement();	//Phone
					#endregion
					try
					{
						string email = p.PrimaryUsr.Email;
						#region Email
						x.WriteStartElement("Email");
						x.WriteElementString("Address", email);
						x.WriteEndElement();	//Email
						#endregion
					}
					catch
					{

					}

					#region Address
					x.WriteStartElement("Address");
					x.WriteElementString("AddressLine1", p.AddressStreet);
					x.WriteElementString("AddressLine2", p.AddressArea);
					x.WriteElementString("City", p.AddressTown);
					x.WriteElementString("StateProvince", p.AddressCounty);
					x.WriteElementString("Country", p.AddressCountry.Name);
					x.WriteElementString("ZipCode", p.AddressPostcode.ToUpper());
					x.WriteEndElement();	//Address
					#endregion

					#region DetailDate SignedUp
					x.WriteStartElement("DetailDate");
					x.WriteAttributeString("Name", "SignedUp");
					x.WriteString(p.DateTimeSignUp.ToString("yyyy-MM-dd"));
					x.WriteEndElement();	//DetailDate
					#endregion

					#region DetailNumber Discount
					x.WriteStartElement("DetailNumber");
					x.WriteAttributeString("Name", "Discount");
					double discount = (1 - p.PricingMultiplier) * 100;
					x.WriteString(discount.ToString("0"));
					x.WriteEndElement();	//DetailString
					#endregion


					int ClientsPerMonth = 0;
					if (true)
					{
						//First event...
						Query qFirstEvent = new Query();
						qFirstEvent.TableElement = Event.PromoterJoinWithVenue;
						qFirstEvent.TopRecords = 1;
						qFirstEvent.QueryCondition = new Q(Promoter.Columns.K, p.K);
						qFirstEvent.OrderBy = new OrderBy(Event.Columns.DateTime);
						EventSet esFirstEvent = new EventSet(qFirstEvent);
						DateTime FirstEventDateTime = DateTime.Now;
						if (esFirstEvent.Count > 0)
						{
							Query q = new Query();
							q.Columns = new ColumnSet();
							q.ExtraSelectElements.Add("Capacity", "sum([Event].[Capacity])");
							q.TableElement = Event.PromoterJoinWithVenue;
							q.QueryCondition = new And(new Q(Promoter.Columns.K, p.K), new Q(Event.Columns.DateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now));
							if (esFirstEvent[0].DateTime < DateTime.Now.AddMonths(-3))
							{
								FirstEventDateTime = DateTime.Now.AddMonths(-3);
								q.QueryCondition = new And(q.QueryCondition, new Q(Event.Columns.DateTime, QueryOperator.GreaterThan, DateTime.Now.AddMonths(-3)));
							}
							else
								FirstEventDateTime = esFirstEvent[0].DateTime;
							EventSet es = new EventSet(q);

							if (es.Count > 0)
							{
								try
								{
									int capacity = (int)es[0].ExtraSelectElements["Capacity"];
									TimeSpan ts = DateTime.Now - FirstEventDateTime;
									double capPerDay = (double)capacity / ts.TotalDays;
									double capPerMonth = capPerDay * 30;
									ClientsPerMonth = (int)capPerMonth;
								}
								catch
								{

								}
							}
						}
					}

					#region DetailNumber
					x.WriteStartElement("DetailNumber");
					x.WriteAttributeString("Name", "ClientsPerMonth");
					x.WriteString(ClientsPerMonth.ToString());
					x.WriteEndElement();	//DetailNumber
					#endregion

					#region DetailNumber
					x.WriteStartElement("DetailNumber");
					x.WriteAttributeString("Name", "Enabled");
					x.WriteString(p.Status.Equals(Promoter.StatusEnum.Active) ? "1" : "0");
					x.WriteEndElement();	//DetailNumber
					#endregion

					double totalRevenue = 0.0;
					if (true)
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "SUM(Total)");
						q.ExtraSelectElements.Add("year", "YEAR(PaidDateTime)");
						q.ExtraSelectElements.Add("month", "MONTH(PaidDateTime)");
						q.QueryCondition = new Q(Invoice.Columns.PromoterK, p.K);
						q.GroupBy = new GroupBy("YEAR(PaidDateTime), MONTH(PaidDateTime)");
						q.OrderBy = new OrderBy("YEAR(PaidDateTime) DESC, MONTH(PaidDateTime) DESC");
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);

						foreach (Invoice i in ins)
						{
							try
							{
								double revenue = (double)i.ExtraSelectElements["sum"];
								totalRevenue += revenue;
								int month = (int)i.ExtraSelectElements["month"];
								int year = (int)i.ExtraSelectElements["year"];

								#region DetailNumber
								x.WriteStartElement("DetailNumber");
								x.WriteAttributeString("Name", "Sales\\Revenue " + year.ToString("0000") + "-" + month.ToString("00"));
								x.WriteString(revenue.ToString("0.00"));
								x.WriteEndElement();	//DetailNumber
								#endregion

							}
							catch { }
						}
					}

					#region DetailNumber
					x.WriteStartElement("DetailNumber");
					x.WriteAttributeString("Name", "Revenue");
					x.WriteString(totalRevenue.ToString("0.00"));
					x.WriteEndElement();	//DetailNumber
					#endregion


					if (p.ConfirmedBrands.Count > 0)
					{
						#region DetailString
						x.WriteStartElement("DetailString");
						x.WriteAttributeString("Name", "Brands");
						bool done = false;
						foreach (Brand b in p.ConfirmedBrands)
						{
							x.WriteString((done ? ", " : "") + b.Name);
							done = true;
						}
						x.WriteEndElement();	//DetailString
						#endregion
					}
					if (p.ConfirmedVenues.Count > 0)
					{
						#region DetailString
						x.WriteStartElement("DetailString");
						x.WriteAttributeString("Name", "Venues");
						bool done = false;
						foreach (Venue v in p.ConfirmedVenues)
						{
							x.WriteString((done ? ", " : "") + v.FriendlyName);
							done = true;
						}
						x.WriteEndElement();	//DetailString
						#endregion
					}

					foreach (Usr u in p.AdminUsrs)
					{
						#region Contact
						x.WriteStartElement("Contact");
						x.WriteElementString("FirstName", u.FirstName);
						x.WriteElementString("LastName", u.LastName);
						x.WriteElementString("Website", "http://www.dontstayin.com" + u.Url());
						if (u.Mobile.Length > 0)
						{
							#region Phone
							x.WriteStartElement("Phone");
							x.WriteElementString("Number", "+" + u.Mobile);
							x.WriteElementString("Description", "Mobile");
							x.WriteEndElement();	//Phone
							#endregion
						}
						#region Email
						x.WriteStartElement("Email");
						x.WriteElementString("Address", u.Email);
						x.WriteEndElement();	//Email
						#endregion
						#region DetailString
						x.WriteStartElement("DetailString");
						x.WriteAttributeString("Name", "NickName");
						x.WriteString(u.NickName);
						x.WriteEndElement();	//DetailString
						#endregion
						x.WriteEndElement();	//Contact
						#endregion
					}
					x.WriteEndElement();	//Company

					#endregion

					ps.Kill(count);
				}
				x.WriteEndElement();	//AllData
				#endregion

				x.Flush();
				x.Close();

			}
		}
		static string PromoterXmlFixPhone(string phoneIn)
		{
			string s = phoneIn;
			if (s.StartsWith("00"))
			{

			}
			else if (s.StartsWith("0"))
			{
				s = s.Substring(1);
				s = "+44" + s;
			}

			return s.Replace(" ", "");

		}
		#endregion
		#endregion

        #region AddressHtml 
        public string AddressHtml
        {
            get
            {
                if (addressHtml == "")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(this.AddressStreet);
                    if (this.AddressArea != null && this.AddressArea.Length > 0)
                    {
                        sb.Append("<br>");
                        sb.Append(this.AddressArea);
                    }
                    sb.Append("<br>");
                    sb.Append(this.AddressTown);
                    sb.Append("<br>");
                    sb.Append(this.AddressPostcode);
                    if (this.AddressCountryK != 224)
                    {
                        if (this.AddressCounty != null && this.AddressCounty.Length > 0)
                        {
                            sb.Append("<br>");
                            sb.Append(this.AddressCounty);
                        }
                        if (this.AddressCountry != null)
                        {
                            sb.Append("<br>");
                            sb.Append(this.AddressCountry.Name);
                        }
                    }
                    addressHtml = sb.ToString();
                }
                return addressHtml;
            }
        }
        string addressHtml = "";
        #endregion

        #region BankDetailsHTML
        public string BankDetailsHTML
        {
            get
            {
                if (bankDetailsHTML == "")
                {
                    StringBuilder sb = new StringBuilder();
                    if (this.BankName != "")
                    {
                        sb.Append("Bank name: ");
                        sb.Append(this.BankName);
                        sb.Append("<br>");
                    }
                    if (this.BankAccountName != "")
                    {
                        sb.Append("Bank account name: ");
                        sb.Append(this.BankAccountName);
                        sb.Append("<br>");
                    }
                    if (this.BankAccountSortCode != "")
                    {
                        sb.Append("Bank sort code: ");
                        sb.Append(this.BankAccountSortCode);
                        sb.Append("<br>");
                    }
                    if (this.BankAccountNumber != "")
                    {
                        sb.Append("Bank account #: ");
                        sb.Append(this.BankAccountNumber);
                        sb.Append("<br>");
                    }
                    bankDetailsHTML = sb.ToString();
                }
                return bankDetailsHTML;
            }
        }
        string bankDetailsHTML = "";
        #endregion

        #region PicHtml
        public static string PicHtml(IPic pic)
		{
			if (pic.HasPic)
				return "<img src=\"" + pic.PicPath + "\" width=\"30\" height=\"30\" border=\"0\" class=\"BorderBlack All\">";
			else
				return "<img src=\"/gfx/1pix.gif\" width=\"30\" height=\"30\" border=\"0\">";
		}
		#endregion

		#region UpcomingEvents
		public EventSet GetUpcomingEvents(bool confirmedBrandsOnly)
		{
			return GetUpcomingEvents(0, confirmedBrandsOnly);
		}
		public EventSet GetUpcomingEvents(int AdditionalEventK, bool confirmedBrandsOnly)
		{
			Query q = new Query();
			q.NoLock=true;
			if (AdditionalEventK>0)
			{
				q.QueryCondition=new And(
					new Or(
						new Q(Event.Columns.K,AdditionalEventK),
						Event.FutureEventsQueryCondition
					), 
					new Q(Promoter.Columns.K,this.K)
				);
			}
			else
			{
				q.QueryCondition=new And(
					Event.FutureEventsQueryCondition, 
					new Q(Promoter.Columns.K,this.K)
				);
			}
			q.Distinct=true;
			q.DistinctColumn=Event.Columns.K;
			if (confirmedBrandsOnly)
				q.TableElement=Event.PromoterJoinWithVenue;
			else
				q.TableElement=Event.PromoterJoinAllWithVenue;
			q.OrderBy=Event.FutureEventOrder;
			return new EventSet(q);
		}
		#endregion

		#region GuestlistTotalCreditAvailable
		public int GuestlistTotalCreditAvailable
		{
			get
			{
				return this.GuestlistCredit + this.GuestlistCreditLimit;
			}
		}
		#endregion
		#region GuestlistCreditAvailable
		public int GuestlistCreditAvailable
		{
			get
			{
				return GuestlistTotalCreditAvailable - GetTotalOpenGuestlistCredits(0);
			}
		}
		#endregion
		#region GetTotalOpenGuestlistCredits
		public int GetTotalOpenGuestlistCredits(int excludeEventK)
		{
			Q excluedQ = new Q(true);
			if (excludeEventK > 0)
				excluedQ = new Q(Event.Columns.K, QueryOperator.NotEqualTo, excludeEventK);

			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new And(
				new Q(Event.Columns.HasGuestlist, true),
				new Q(Event.Columns.GuestlistPromoterK, this.K),
				new Q(Event.Columns.GuestlistFinished, false),
				excluedQ
			);
			EventSet es = new EventSet(q);

			int count = 0;
			foreach (Event e in es)
				count += e.GuestlistLimit;

			return count;
		}

		#endregion
		#region DeleteAll
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;


			Delete PromoterUsrDelete = new Delete(
				TablesEnum.PromoterUsr,
				new Q(PromoterUsr.Columns.PromoterK, this.K)
				);
			PromoterUsrDelete.Run(transaction);

			foreach (Brand b in this.AllBrands)
			{
				b.PromoterK = 0;
				b.PromoterStatus = Brand.PromoterStatusEnum.Unconfirmed;
				b.Update(transaction);
			}

			foreach (Venue v in this.AllVenues)
			{
				v.PromoterK = 0;
				v.PromoterStatus = Venue.PromoterStatusEnum.Unconfirmed;
				v.Update(transaction);
			}

		}
		#endregion

		#region IsEnabled
		/// <summary>
		/// Everything except Status=Disabled - promoters are fully enabled unless we disable them.
		/// </summary>
		public bool IsEnabled
		{
			get
			{
				return !Status.Equals(StatusEnum.Disabled);
			}
		}
		#endregion

		#region EnabledQ
		public static Q EnabledQ
		{
			get
			{
				return new Q(Promoter.Columns.Status, QueryOperator.NotEqualTo, Promoter.StatusEnum.Disabled);
			}
		}
		#endregion
	
		#region NextEventSet
		public EventSet NextEventSet
		{
			get
			{
				Query q = new Query();
				q.NoLock = true;
				q.TableElement = Event.PromoterJoin;
				q.QueryCondition = new And(new Q(Promoter.Columns.K, this.K), Event.FutureEventsQueryCondition);
				q.OrderBy = new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Ascending);
				q.TopRecords = 1;
				return new EventSet(q);
			}
		}
		#endregion
		#region NextEvent
		public Event NextEvent
		{
			get
			{
				EventSet es = this.NextEventSet;
				if (es.Count == 1)
					return es[0];
				else
					return null;
			}
		}
		#endregion
		#region EventsCount()
		public int EventsCount()
		{
			Query q = new Query();
			q.NoLock = true;
			q.TableElement = Event.PromoterJoin;
			q.QueryCondition = new Q(Promoter.Columns.K, this.K);
			q.ReturnCountOnly = true;
			EventSet es = new EventSet(q);
			int i = es.Count;
			return i;
		}
		#endregion

		#region Links to Bobs
		#region PrimaryUsr
		public Usr PrimaryUsr
		{
			get
			{
				if (primaryUsr==null && PrimaryUsrK>0)
				{
					try
					{
					primaryUsr = new Usr(PrimaryUsrK);
					}
					catch 
					{ 
						return null; 
					}
				}
				return primaryUsr;
			}
			set
			{
				primaryUsr=value;
				//if (primaryUsr == null)
				//    PrimaryUsrK = 0;
				//else
				//    PrimaryUsrK = primaryUsr.K;
			}
		}
		Usr primaryUsr;
		#endregion
		#region AddressCountry
		public Country AddressCountry
		{
			get
			{
				if (addressCountry == null && AddressCountryK > 0)
				{
					try
					{
						addressCountry = new Country(AddressCountryK);
					}
					catch { }
				}
				return addressCountry;
			}
			set
			{
				addressCountry=value;
			}
		}
		Country addressCountry;
		#endregion

		#region VatCountry
		public Country VatCountry
		{
			get
			{
				if (vatCountry == null && VatCountryK > 0)
					vatCountry = new Country(VatCountryK);
				return vatCountry;
			}
			set
			{
				vatCountry = value;
				if (vatCountry == null)
					VatCountryK = 0;
				else
					VatCountryK = vatCountry.K;
			}
		}
		Country vatCountry;
		#endregion

		#endregion

		#region Links to BobSets

		#region AdminUsrs
		public UsrSet AdminUsrs
		{
			get
			{
				if (adminUsrs == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Promoter.Columns.K, this.K);
					q.OrderBy = new OrderBy(Usr.Columns.NickName);
					q.TableElement = Usr.PromoterJoin;
					adminUsrs = new UsrSet(q);
				}
				return adminUsrs;
			}
			set
			{
				adminUsrs = value;
			}
		}
		UsrSet adminUsrs;
		#endregion

		#region AllBrands
		public BrandSet AllBrands
		{
			get
			{
				if (allBrands == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Brand.Columns.PromoterK, this.K);
					q.OrderBy = new OrderBy(Brand.Columns.Name);
					allBrands = new BrandSet(q);
				}
				return allBrands;
			}
			set
			{
				allBrands = value;
			}
		}
		BrandSet allBrands;
		#endregion

		#region ConfirmedBrands
		public BrandSet ConfirmedBrands
		{
			get
			{
				if (confirmedBrands == null)
				{
					Query q = new Query();
					q.QueryCondition = new And(new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Confirmed), new Q(Brand.Columns.PromoterK, this.K));
					q.OrderBy = new OrderBy(Brand.Columns.Name, OrderBy.OrderDirection.Ascending);
					confirmedBrands = new BrandSet(q);
				}
				return confirmedBrands;
			}
			set
			{
				confirmedBrands = value;
			}
		}
		BrandSet confirmedBrands;
		#endregion

		#region AllVenues
		public VenueSet AllVenues
		{
			get
			{
				if (allVenues == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Venue.Columns.PromoterK, this.K);
					q.OrderBy = new OrderBy(Venue.Columns.Name);
					allVenues = new VenueSet(q);
				}
				return allVenues;
			}
			set
			{
				allVenues = value;
			}
		}
		VenueSet allVenues;
		#endregion

		#region Domains
		public DomainSet Domains
		{
			get
			{
				if (domains == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Domain.Columns.PromoterK, this.K);
					q.OrderBy = new OrderBy(Domain.Columns.DomainName);
					q.ExtraSelectElements.Add("hitsTotal", new Bobs.StringQueryCondition("(select sum([DomainStats].[Hits]) from [DomainStats] where [DomainStats].[DomainK] = [Domain].[K])"));
					q.ExtraSelectElements.Add("hitsMonth", new Bobs.StringQueryCondition("(select sum([DomainStats].[Hits]) from [DomainStats] where [DomainStats].[DomainK] = [Domain].[K] and [DomainStats].[Date] >= DATEADD(m,-1,GETDATE()))"));
					domains = new DomainSet(q);
				}
				return domains;
			}
			set
			{
				domains = value;
			}
		}
		DomainSet domains;
		#endregion

		#region ConfirmedVenues
		public VenueSet ConfirmedVenues
		{
			get
			{
				if (confirmedVenues == null)
				{
					Query q = new Query();
					q.QueryCondition = new And(new Q(Venue.Columns.PromoterStatus, Venue.PromoterStatusEnum.Confirmed), new Q(Venue.Columns.PromoterK, this.K));
					q.OrderBy = new OrderBy(Venue.Columns.Name);
					confirmedVenues = new VenueSet(q);
				}
				return confirmedVenues;
			}
			set
			{
				confirmedVenues = value;
			}
		}
		VenueSet confirmedVenues;
		#endregion

		#region Ticket Runs
		public TicketRunSet TicketRuns
		{
			get
			{
				if (ticketRuns == null)
				{
					Query q = new Query();
					q.QueryCondition = new And(new Q(TicketRun.Columns.PromoterK, this.K),
                                               new Q(TicketRun.Columns.StartDateTime, QueryOperator.GreaterThanOrEqualTo, Vars.TICKETS_NEW_SYSTEM_START_DATE));
					q.OrderBy = new OrderBy(TicketRun.Columns.K);
					ticketRuns = new TicketRunSet(q);
				}
				return ticketRuns;
			}
			set
			{
				ticketRuns = value;
			}
		}
		TicketRunSet ticketRuns;
		#endregion

		#region Ticket Promoter Events
		public TicketPromoterEventSet TicketPromoterEvents
		{
			get
			{
				if (ticketPromoterEvents == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(TicketPromoterEvent.Columns.PromoterK, this.K);
					q.OrderBy = new OrderBy(TicketPromoterEvent.Columns.EventK);
					ticketPromoterEvents = new TicketPromoterEventSet(q);
				}
				return ticketPromoterEvents;
			}
			set
			{
				ticketPromoterEvents = value;
			}
		}
		TicketPromoterEventSet ticketPromoterEvents;
		#endregion
		#endregion

		#region UsrJoin
		public static Join UsrJoin
		{
			get
			{
				return new Join(new Join(Promoter.Columns.K, PromoterUsr.Columns.PromoterK), Usr.Columns.K, PromoterUsr.Columns.UsrK);
			}
		}
		#endregion

		#region IPic Members

		public bool HasPic
		{
			get
			{
				return false;
			}
		}
		
		public string PicPath
		{
			get
			{
				if (HasPic)
					return Storage.Path(Pic);
				else
					return "/gfx/dsi-sign-100.png";
			}
		}

		#endregion

		#region IPage Members
		public void UpdateChildUrlFragments(bool Cascade)
		{
		}
		public string UrlFragment
		{
			get
			{
				return "promoters";
			}
		}
		public string UrlFilterPart
		{
			get
			{
				return UrlFragment+"/"+this.UrlName;
			}
		}
		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, null, par);
		}
		public string UrlApp(string Application, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart,Application,par);
		}
		public string UrlStatementReport(DateTime dt)
		{
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Blank, "reportgenerator", new string[] { "PK", this.K.ToString(), "M", dt.Month.ToString(), "Y", dt.Year.ToString(), "type", "statement" });
		}
        public string UrlEventOptions(Event ev)
        {
            return UrlEventOptions(ev.K);
        }
        public string UrlEventOptions(int eventK)
        {
            return UrlApp("eventoptions", "eventk", eventK.ToString());
        }
		#endregion

		#region CreateUniqueUrlName()
		public void CreateUniqueUrlName()
		{
			string urlName = UrlInfo.GetUrlName(this.Name);
			if (urlName.Length==0)
				urlName = "promoter-"+this.K.ToString();
			if (UrlInfo.IsReservedString(urlName))
				urlName = "promoter-"+urlName;

			PromoterSet ps = null;
			int namePost = 0;
			string newName = urlName;
			while (ps==null || ps.Count>0)
			{
				if (namePost>0)
					newName = urlName+"-"+namePost.ToString();
				Query q = new Query();
				q.NoLock=true;
				q.ReturnCountOnly=true;
				q.QueryCondition=new And(
					new Q(Promoter.Columns.UrlName,newName),
					new Q(Promoter.Columns.K,QueryOperator.NotEqualTo,this.K)
				);
				ps = new PromoterSet(q);
				namePost++;
			}
			
			this.UrlName = newName;
			this.Update();

			Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Promoter, this.K, true);
			job.ExecuteAsynchronously();
		}
		#endregion

		#region AddQuestionsThread
		public void AddQuestionsThread(Usr u, string promoterName)
		{
			#region Add a PM
			//string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			//string body = NVelocityEngineFactory.CreateNVelocityAssemblyEngine(assemblyName, true).Process(new Hashtable(), "Emails.NewPromoterEmail.vm");
			Comment c = new Comment(Vars.DevEnv ? 21036749 : 21053069);
			string body = c.Text;


			List<Usr> salesUsrs = new List<Usr>();
			if (this.SalesUsrK > 0)
				salesUsrs.Add(this.SalesUsr);
			else
			{
				foreach (Usr salesUsr in Usr.GetNewPromoterSalesUsrsNameAndK())
				{
					salesUsrs.Add(salesUsr);
				}
			}

			if (salesUsrs.Count > 0)
			{
				Thread.Maker m = new Thread.Maker();
				m.Subject = promoterName + " promoter questions";
				m.Body = body;
				m.ParentType = Model.Entities.ObjectType.None;
				m.DuplicateGuid = Guid.NewGuid();
				m.Private = true;
				m.PostingUsr = salesUsrs[0];
				for(int i=1; i<salesUsrs.Count; i++)
					m.InviteKs.Add(salesUsrs[i].K);
				m.InviteKs.Add(u.K);

				Thread.MakerReturn r = m.Post();

				this.QuestionsThreadK = r.Thread.K;
				Thread t = r.Thread;
			}
			#endregion
		}
		#endregion

		#region IName Members

		public string FriendlyName
		{
			get
			{
				return Name;
			}
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "Promoter";
			}
		}
		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Promoter;
			}
		}
		#endregion
		
		#region PicMisc and PicPhoto
		#region PicMisc
		public Misc PicMisc
		{
			get
			{
				if (picMisc==null && PicMiscK>0)
					picMisc = new Misc(PicMiscK);
				return picMisc;
			}
			set
			{
				picMisc = value;
			}
		}
		private Misc picMisc;
		#endregion
		#region PicPhoto
		public Photo PicPhoto
		{
			get
			{
				if (picPhoto==null && PicPhotoK>0)
					picPhoto = new Photo(PicPhotoK);
				return picPhoto;
			}
			set
			{
				picPhoto = value;
			}
		}
		private Photo picPhoto;
		#endregion
		#endregion

		#region Link
		public string Link()
		{
				return Utilities.Link(Url(), this.Name);
		}
		public string LinkNewWindow()
		{
				return Utilities.LinkNewWindow(Url(), this.Name);
		}
		public string LinkEmail()
		{
				return @"<a href=""[LOGIN(" + this.Url() + "\")]>" + this.Name + "</a>";
		}

		public string LinkEmailFull
		{
			get
			{
				return @"<p>Promoter: " + LinkEmail() + "</p>";
			}
		}

		#endregion

		#region AmountDue
		public decimal AmountDue(DateTime startDate, DateTime endDate)
		{
			decimal amountDue = 0;
			// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
			Query InvoiceQuery = new Query(new And(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
												   new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, endDate),
												   new Q(Invoice.Columns.Paid, false),
												   new Q(Invoice.Columns.PromoterK, this.K),
												   new Q(Invoice.Columns.Type, Invoice.Types.Invoice)));
			InvoiceSet invoiceSet = new InvoiceSet(InvoiceQuery);
			foreach (Invoice invoice in invoiceSet)
			{
				amountDue += invoice.Total - invoice.AmountPaid;
			}

			return amountDue;
		}
		#endregion

		#region GetBalance
		/// <summary>
		/// This calculates the total balance of the promoter's account based on (Invoices + Credit) totals - successful Transfer amounts.
		/// Note: Pending refunds are considered the same as successful refunds
		/// </summary>
		/// <returns></returns>
		public decimal GetBalance()
		{
			return GetBalance(DateTime.MinValue);
		}
		public decimal GetBalance(DateTime onlyUseItemsFromBefore)
		{
			decimal invoiceBalance = 0;
			decimal transferBalance = 0;

			Query qInvoiceBalance = new Query();
			qInvoiceBalance.Columns = new ColumnSet();
			qInvoiceBalance.ExtraSelectElements.Add("sum", "SUM([Invoice].[Total])");
			qInvoiceBalance.QueryCondition = 
				new And(
					new Q(Invoice.Columns.PromoterK, this.K),
					onlyUseItemsFromBefore == DateTime.MinValue ? new Q(true) : new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, onlyUseItemsFromBefore)
				);
			InvoiceSet isInvoiceBalance = new InvoiceSet(qInvoiceBalance);

			if (isInvoiceBalance.Count > 0 && isInvoiceBalance[0].ExtraSelectElements["sum"] != DBNull.Value)
				invoiceBalance = Convert.ToDecimal(isInvoiceBalance[0].ExtraSelectElements["sum"]);

			Query qTransferBalance = new Query();
			qTransferBalance.Columns = new ColumnSet();

			qTransferBalance.ExtraSelectElements.Add("sum", "SUM(ISNULL([Transfer].[Amount], 0))");
			qTransferBalance.QueryCondition = 
				new And(
					new Or(
						new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
						new And(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund))
					),
					new Q(Transfer.Columns.PromoterK, this.K),
					onlyUseItemsFromBefore == DateTime.MinValue ? new Q(true) : new Q(Transfer.Columns.DateTimeCreated, QueryOperator.LessThan, onlyUseItemsFromBefore)
				);

			if (this.OverrideApplyTicketFundsToInvoices)
			{
				qTransferBalance.TableElement = 
					new Join(
						new TableElement(TablesEnum.Transfer),
						new TableElement(new Column(Transfer.Columns.TransferRefundedK, Transfer.Columns.K)),
						QueryJoinType.Left,
						Transfer.Columns.TransferRefundedK,
						new Column(Transfer.Columns.TransferRefundedK, Transfer.Columns.K)
					);

				qTransferBalance.QueryCondition = 
					new And(
						qTransferBalance.QueryCondition,
						new Q(Transfer.Columns.Method, QueryOperator.NotEqualTo, Transfer.Methods.TicketSales),
						new Or(
							new Q(new Column(Transfer.Columns.TransferRefundedK, Transfer.Columns.K), QueryOperator.IsNull, null),
							new Q(new Column(Transfer.Columns.TransferRefundedK, Transfer.Columns.Method), QueryOperator.NotEqualTo, Transfer.Methods.TicketSales)
						)
					);

				Query qTicketSalesAppliedBalance = new Query();
				qTicketSalesAppliedBalance.Columns = new ColumnSet();

				qTicketSalesAppliedBalance.ExtraSelectElements.Add("sum", "SUM(ISNULL([InvoiceTransfer].[Amount], 0))");
				qTicketSalesAppliedBalance.QueryCondition = 
					new And(
						new Or(
							new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
							new And(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending), new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund))
						),
						new Q(Transfer.Columns.PromoterK, this.K),
						new Q(Transfer.Columns.Method, Transfer.Methods.TicketSales),
						onlyUseItemsFromBefore == DateTime.MinValue ? new Q(true) : new Q(Transfer.Columns.DateTimeCreated, QueryOperator.LessThan, onlyUseItemsFromBefore)
					);

				qTicketSalesAppliedBalance.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K);

				InvoiceTransferSet itsTicketSalesAppliedBalance = new InvoiceTransferSet(qTicketSalesAppliedBalance);

				if (itsTicketSalesAppliedBalance.Count > 0 && itsTicketSalesAppliedBalance[0].ExtraSelectElements["sum"] != DBNull.Value)
					transferBalance += Convert.ToDecimal(itsTicketSalesAppliedBalance[0].ExtraSelectElements["sum"]);

            }

			TransferSet tsTransferBalance = new TransferSet(qTransferBalance);
			if (tsTransferBalance.Count > 0 && tsTransferBalance[0].ExtraSelectElements["sum"] != DBNull.Value)
				transferBalance += Convert.ToDecimal(tsTransferBalance[0].ExtraSelectElements["sum"]);
			

			return Math.Round(transferBalance - invoiceBalance, 2);
		}
		#endregion

        #region PromoterAccountStatus

        public AccountStatus GetAccountStatus()
        {
			decimal balance = this.GetBalance();
            if (balance > 0)
                return AccountStatus.InCredit;
            if (balance < 0)
            {
                Query outstandingInvoiceQuery = new Query();
                outstandingInvoiceQuery.QueryCondition = new And(new Q(Invoice.Columns.PromoterK, this.K),
                                                                 new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
                                                                 new Q(Invoice.Columns.Paid, false));
                outstandingInvoiceQuery.Columns = new ColumnSet();
                outstandingInvoiceQuery.ExtraSelectElements.Add("MinDueDate", "MIN(DueDateTime)");

                InvoiceSet outstandingInvoices = new InvoiceSet(outstandingInvoiceQuery);
                if (outstandingInvoices.Count > 0 && outstandingInvoices[0].ExtraSelectElements["MinDueDate"] != DBNull.Value)
                {
                    if (((DateTime)outstandingInvoices[0].ExtraSelectElements["MinDueDate"]) < DateTime.Today)
                        return AccountStatus.Overdue;
                    else
                        return AccountStatus.Outstanding;
                }
            }

            return AccountStatus.ZeroBalance;
        }

        #endregion

        #region Available Money
        /// <summary>
		/// This calculates the total amount of money on the promoter's account that has not been applied
		/// </summary>
		/// <returns></returns>
		public decimal GetAvailableMoney()
		{
			var balance = GetBalance();
			Query unpaidInvoiceQuery = new Query(new And(new Q(Invoice.Columns.Paid, false),
														 new Q(Invoice.Columns.PromoterK, this.K)));
			InvoiceSet unpaidInvoices = new InvoiceSet(unpaidInvoiceQuery);

			foreach (Invoice unpaidInvoice in unpaidInvoices)
			{
				balance += unpaidInvoice.AmountDue;
			}

			return Math.Round(balance, 2);
		}

		public decimal GetAvailableTicketFunds()
		{
			return GetAvailableTicketFunds(DateTime.MinValue);
		}
		public decimal GetAvailableTicketFunds(DateTime onlyUseItemsFromBefore)
        {
            var availableTicketFunds = 0m;
            try
            {
                Query qTicketFundTransferWithUnappliedMoney = new Query();
                qTicketFundTransferWithUnappliedMoney.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.Amount);
                qTicketFundTransferWithUnappliedMoney.QueryCondition = 
					new And(
						new Q(Transfer.Columns.Method, Transfer.Methods.TicketSales),
						new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
						new Q(Transfer.Columns.PromoterK, this.K),
						new Q(Transfer.Columns.Amount, QueryOperator.NotEqualTo, 0),
						new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment),
						new Or(
							new Q(Transfer.Columns.IsFullyApplied, 0),
							new Q(Transfer.Columns.IsFullyApplied, QueryOperator.IsNull, null)
						),
						onlyUseItemsFromBefore == DateTime.MinValue ? new Q(true) : new Q(Transfer.Columns.DateTimeCreated, QueryOperator.LessThan, onlyUseItemsFromBefore)
					);

                TransferSet tsTicketFundTransferWithUnappliedMoney = new TransferSet(qTicketFundTransferWithUnappliedMoney);
                foreach (Transfer t in tsTicketFundTransferWithUnappliedMoney)
                    availableTicketFunds += t.AmountRemaining();

            }
            catch { }

            return Math.Round(availableTicketFunds, 2);
        }

		public TransferSet GetAvailableTicketFundTransfers()
		{			
			Query qTicketFundTransferWithUnappliedMoney = new Query();

			qTicketFundTransferWithUnappliedMoney.QueryCondition = new And(new Q(Transfer.Columns.Method, Transfer.Methods.TicketSales),
																		   new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
																		   new Q(Transfer.Columns.PromoterK, this.K),
																		   new Q(Transfer.Columns.Amount, QueryOperator.NotEqualTo, 0),
																		   new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment),
																		   new Or(new Q(Transfer.Columns.IsFullyApplied, 0),
																				  new Q(Transfer.Columns.IsFullyApplied, QueryOperator.IsNull, null)));

			TransferSet tsTicketFundTransferWithUnappliedMoney = new TransferSet(qTicketFundTransferWithUnappliedMoney);


			return tsTicketFundTransferWithUnappliedMoney;
		}


		public void ApplyAvailableMoneyToUnpaidInvoices()
		{
			Query unpaidInvoiceQuery = new Query(new And(new Q(Invoice.Columns.Paid, false),
														 new Q(Invoice.Columns.PromoterK, this.K),
														 new Q(Invoice.Columns.Type, Invoice.Types.Invoice)));
			unpaidInvoiceQuery.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.DueDateTime), new OrderBy(Invoice.Columns.K));

			InvoiceSet unpaidInvoices = new InvoiceSet(unpaidInvoiceQuery);

			foreach (Invoice unpaidInvoice in unpaidInvoices)
			{
				unpaidInvoice.UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney();
				if(unpaidInvoice.Paid == true)
					Utilities.EmailInvoice(unpaidInvoice, false);
				// If unable to pay off invoice, then wont have any available money for other invoices
				else
					break;
			}
		}

		/// <summary>
		/// This calculates the total funds on the promoter's account. Credit limit + balance
		/// </summary>
		/// <returns></returns>
		public decimal GetAvailableFunds()
		{
			return CreditLimit + GetBalance();
		}
		#endregion

        #region TicketsSoldTotal
        public int TicketsSoldTotal
        {
			get
			{
				if (ticketsSoldTotal == -1)
				{
                    ticketsSoldTotal = 0;
                    //if (ticketRuns == null)
                    //{
						Query q = new Query();
						q.ExtraSelectElements.Add("SumSoldTickets", "SUM([TicketRun].[SoldTickets])");
						q.Columns = new ColumnSet();
						q.QueryCondition = new And(new Q(TicketRun.Columns.PromoterK, this.K),
                                                   new Q(TicketRun.Columns.StartDateTime, QueryOperator.GreaterThanOrEqualTo, Vars.TICKETS_NEW_SYSTEM_START_DATE));

						TicketRunSet tickets = new TicketRunSet(q);
                        if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumSoldTickets"] != DBNull.Value)
                            ticketsSoldTotal = Convert.ToInt32(tickets[0].ExtraSelectElements["SumSoldTickets"]);
                    //}
                    //else
                    //{
                    //    foreach (TicketRun ticketRun in TicketRuns)
                    //        ticketsSoldTotal += ticketRun.SoldTickets;
                    //}
				}
				return ticketsSoldTotal;
			}
		}
		private int ticketsSoldTotal = -1;        
        #endregion

        #region TicketsCancelledTotal
        public int TicketsCancelledTotal
        {
            get
            {
                if (ticketsCancelledTotal == -1)
                {
                    ticketsCancelledTotal = 0;

                    Query q = new Query();
                    q.ExtraSelectElements.Add("SumCancelledTickets", "SUM([Ticket].[Quantity])");
                    q.Columns = new ColumnSet();
                    q.QueryCondition = new And(new Q(TicketRun.Columns.PromoterK, this.K),
                                               new Q(TicketRun.Columns.StartDateTime, QueryOperator.GreaterThanOrEqualTo, Vars.TICKETS_NEW_SYSTEM_START_DATE),
                                               Ticket.CancelledTicketsQ);
                    q.TableElement = new Join(Ticket.Columns.TicketRunK, TicketRun.Columns.K);

                    TicketSet tickets = new TicketSet(q);
                    if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumCancelledTickets"] != DBNull.Value)
                        ticketsCancelledTotal = Convert.ToInt32(tickets[0].ExtraSelectElements["SumCancelledTickets"]);    
                }
                return ticketsCancelledTotal;
            }
        }
        private int ticketsCancelledTotal = -1;
        #endregion

        #region TicketFundsReleased
        public double TicketFundsReleased
        {
            get
            {
                if (ticketFundsReleased == -1)
                {
                    ticketFundsReleased = 0;

                    Query q = new Query();
                    q.ExtraSelectElements.Add("SumTicketFundsReleased", "SUM([TicketPromoterEvent].[TotalFunds])");
                    q.Columns = new ColumnSet();
                    q.QueryCondition = new And(new Q(TicketPromoterEvent.Columns.PromoterK, this.K),
                                               new Q(TicketPromoterEvent.Columns.FundsReleased, true));

                    TicketPromoterEventSet ticketPromoterEvents = new TicketPromoterEventSet(q);
                    if (ticketPromoterEvents.Count > 0 && ticketPromoterEvents[0].ExtraSelectElements["SumTicketFundsReleased"] != DBNull.Value)
                        ticketFundsReleased = Convert.ToInt32(ticketPromoterEvents[0].ExtraSelectElements["SumTicketFundsReleased"]);
                }
                return ticketFundsReleased;
            }
        }
        private double ticketFundsReleased = -1;
        #endregion

		//#region TicketFundsAwaitingRelease
		//public double TicketFundsAwaitingRelease
		//{
		//    get
		//    {
		//        if (ticketFundsAwaitingRelease == -1)
		//        {
		//            ticketFundsAwaitingRelease = 0;

		//            Query q = new Query();
		//            q.ExtraSelectElements.Add("SumTicketFundsAwaitingRelease", "SUM([TicketPromoterEvent].[TotalFunds])");
		//            q.Columns = new ColumnSet();
		//            q.QueryCondition = new And(new Q(TicketPromoterEvent.Columns.PromoterK, this.K),
		//                                       new Or(new Q(TicketPromoterEvent.Columns.FundsReleased, false),
		//                                              new Q(TicketPromoterEvent.Columns.FundsReleased, QueryOperator.IsNull, null)));

		//            TicketPromoterEventSet ticketPromoterEvents = new TicketPromoterEventSet(q);
		//            if (ticketPromoterEvents.Count > 0 && ticketPromoterEvents[0].ExtraSelectElements["SumTicketFundsAwaitingRelease"] != DBNull.Value)
		//                ticketFundsAwaitingRelease = Convert.ToInt32(ticketPromoterEvents[0].ExtraSelectElements["SumTicketFundsAwaitingRelease"]);
		//        }
		//        return ticketFundsAwaitingRelease;
		//    }
		//}
		//private double ticketFundsAwaitingRelease = -1;
		//#endregion

		#region TicketFundsAwaitingReleaseAtDate
		public decimal GetTicketFundsAwaitingRelease()
		{
			return TicketFundsAwaitingReleaseAtDate(DateTime.MaxValue);
		}
		public decimal TicketFundsAwaitingReleaseAtDate(DateTime date)
		{
			decimal ticketFundsAwaitingRelease = 0;

			Query q = new Query();
			//q.ExtraSelectElements.Add("SumTicketFundsAwaitingRelease", "SUM([TicketPromoterEvent].[TotalFunds])");
			//q.Columns = new ColumnSet();
			q.QueryCondition = new Q(TicketPromoterEvent.Columns.PromoterK, this.K);
			TicketPromoterEventSet ticketPromoterEvents = new TicketPromoterEventSet(q);

			foreach (TicketPromoterEvent tpe in ticketPromoterEvents)
			{
				if (!tpe.FundsReleased || (tpe.FundsTransfer != null && tpe.FundsTransfer.DateTimeCreated >= date))
					ticketFundsAwaitingRelease += tpe.GetTotalFundsAtDate(date);
			}

			return ticketFundsAwaitingRelease;
		}
		#endregion

        #region GenerateMonthlyStatementInHTML
        public HtmlTextWriter GenerateMonthlyStatementHtmlTextWriter(int month, int year, bool linksEnabled)
		{
			return new HtmlTextWriter(new StringWriter(GenerateMonthlyStatementStringBuilder(month, year, linksEnabled)));
		}

		public MemoryStream GenerateMonthlyStatementMemoryStream(int month, int year, bool linksEnabled)
		{
			// Convert string to Stream
			string monthlyStatement = this.GenerateMonthlyStatementStringBuilder(month, year, linksEnabled).ToString();
			byte[] b = new byte[monthlyStatement.Length];
			Encoding.ASCII.GetBytes(monthlyStatement.ToCharArray(), 0, monthlyStatement.Length, b, 0);

			return new MemoryStream(b);
		}

		/// <summary>
		/// Get all outstanding invoices for the given month / year.  Also provide total amount of all overdue invoices.
		/// Then produce a report detailing the outstanding invoices summed with the overdue amount and provide details on how to pay
		/// </summary>
		/// <param name="month">Calendar month as integer: Jan = 1 ... Dec = 12</param>
		/// <param name="year">Calendar year as integer</param>
		/// <returns></returns>
		public StringBuilder GenerateMonthlyStatementStringBuilder(int month, int year, bool linksEnabled)
		{
			DateTime startDate = new DateTime(year, month, 1);
			DateTime endDate = startDate.AddMonths(1);

			StringBuilder sb = new StringBuilder();

			sb.Append(@"<form id='form1' runat='server'><div style='font-family:Verdana;'>
						<table width='100%' border='0' cellspacing='0' cellpadding='0' height='100%'>
						<tr><td valign='top'>
							<table width='100%'>");
			sb.Append(Utilities.GenerateHTMLHeaderRowString("STATEMENT"));

							//<tr>
							//    <td align='left' valign='middle'><img src='/gfx/dsi-1-126.gif'/></td>
							//    <td colspan=2 align='right' valign='middle'><br><h1>STATEMENT</h1></td>
							//</tr>
							//<tr><td colspan=3><hr><br></td></tr>
			sb.Append(@"<tr>
                                <td align='left' valign='top' width='380' style='padding-left:48px;'>");
			if (this.PrimaryUsr != null && this.PrimaryUsr.Name.Length > 0)
			{
				sb.Append(this.PrimaryUsr.FirstName);
				sb.Append(" ");
				sb.Append(this.PrimaryUsr.LastName);
				sb.Append("<br>");
			}
			else if (this.AccountsName.Length > 0)
			{
				sb.Append(this.AccountsName);
				sb.Append("<br>");
			}
			else
			{
				if (this.ContactPersonalTitle.Length > 0)
				{
					sb.Append(this.ContactPersonalTitle);
					sb.Append(" ");
				}
				sb.Append(this.ContactName);
				sb.Append("<br>");
			}
			if (this.Name.Length > 0)
			{
				sb.Append(this.Name);
				sb.Append("<br>");
            }
            sb.Append(this.AddressHtml);
			
			sb.Append(@"</td><td width='340'></td><td valign='top' width='145'><nobr>Acc No.</nobr><br><br><nobr>Statement Date</nobr></td>
							<td align='right' valign='top' width='145'>");

			sb.Append(this.K.ToString());
			sb.Append("<br><br><b>");
			sb.Append(startDate.ToString("MMMM") + "&nbsp;" + startDate.Year.ToString());
			sb.Append("</b></td></tr>");


			sb.Append(@"</table>
					<table width='100%'><tr><td align='center'><br><br>");

			// Get all Outstanding Invoices
			// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
			Query InvoiceQuery = new Query(new And(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, new DateTime(1900,1,1)),
												   new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, endDate),
												   new Q(Invoice.Columns.Paid, false),
												   new Q(Invoice.Columns.PromoterK, this.K),
												   new Q(Invoice.Columns.Type, Invoice.Types.Invoice)));
			InvoiceQuery.OrderBy = new OrderBy(Invoice.Columns.TaxDateTime, OrderBy.OrderDirection.Ascending);

			InvoiceSet invoiceSet = new InvoiceSet(InvoiceQuery);

			Query TransferQuery = new Query(new Or(new And(new Q(Transfer.Columns.IsFullyApplied, false),
														   new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment),
														   new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
														   new Q(Transfer.Columns.PromoterK, this.K)),
												   new And(new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund),
														   new Q(Transfer.Columns.PromoterK, this.K),
														   new Q(Transfer.Columns.TransferRefundedK, 0),
														   new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
																  new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success)))));
			TransferQuery.OrderBy = new OrderBy(Transfer.Columns.DateTimeCreated, OrderBy.OrderDirection.Ascending);

			TransferSet transferSet = new TransferSet(TransferQuery);

			decimal currentAmountDue = 0;

            if (invoiceSet.Count > 0 || transferSet.Count > 0)
            {
				sb.Append(@"<table width='600' border='0' cellspacing='0' cellspadding='3' class='BorderBlack Top Bottom Right'>
                                <tr>
                                    <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center' width='90'><nobr><b>Tax Date</b></nobr></td>
								    <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center' width='90'><nobr><b>Due Date</b></nobr></td>
                                    <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center' width='150'><nobr><b>Ref #</b></nobr></td>
                                    <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center' width='130'><nobr><b>Total</b></nobr></td>
								    <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center' width='140'><b>Amount Outstanding</b></td>
                                </tr>");

                foreach (Invoice invoice in invoiceSet)
                {
					decimal totalPaid = invoice.AmountPaid;

                    // If this invoice is in the current month, then add it to the current total
                    // Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
                    if (invoice.TaxDateTime >= startDate)
                        currentAmountDue += invoice.Total - totalPaid;

                    // Amount due of zero means its been paid, but for some reason the invoice has not been set to Paid.
                    if (invoice.Total - totalPaid != 0)
                    {
                        // Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
                        sb.Append(@"<tr>
                                <td class='BorderBlack Left' align='center'>" + invoice.TaxDateTime.ToString("dd/MM/yy") + @"</td>
								<td class='BorderBlack Left' align='center'>" + invoice.DueDateTime.ToString("dd/MM/yy") + @"</td>
							<td class='BorderBlack Left' align='left'>");
                        if (linksEnabled)
                            sb.Append(Utilities.Link(invoice.UrlReport(), "Invoice #" + invoice.K.ToString()));
                        else
                            sb.Append("Invoice #" + invoice.K.ToString());

                        sb.Append(@"</td>
                            <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(invoice.Total) + @"</td>
                            <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(invoice.Total - totalPaid) + @"</td>
                        </tr>");
                    }
                }

                foreach (Transfer transfer in transferSet)
                {
					decimal amountRemaining = transfer.Amount;

                    // Amount remaining for payments.  Refunds amount remaining will be counted as total refund amount
                    if (transfer.Type.Equals(Transfer.TransferTypes.Payment))
                        amountRemaining = transfer.AmountRemaining();

                    currentAmountDue -= amountRemaining;

                    // Amount remaining of zero means a payment been fully applied, but for some reason the transfer has not been set to isFullyApplied.
                    if (amountRemaining != 0)
                    {
                        sb.Append(@"<tr>
                                <td class='BorderBlack Left' align='center'>" + transfer.DateTimeCreated.ToString("dd/MM/yy") + @"</td>
								<td class='BorderBlack Left' align='center'>&nbsp;</td>
							<td class='BorderBlack Left' align='left'>");
                        if (linksEnabled)
                            sb.Append(Utilities.Link(transfer.UrlReport(), transfer.Type.ToString() + " #" + transfer.K.ToString()));
                        else
                            sb.Append(transfer.Type.ToString() + " #" + transfer.K.ToString());

                        sb.Append(@"</td>
                            <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(-1 * transfer.Amount) + @"</td>
                            <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(-1 * amountRemaining) + @"</td>
                        </tr>");
                    }
                }

                sb.Append("</table>");
            }

			// Now get sum for outstanding invoices for previous months ago
			var previousMonthsAmountOwed = new decimal[] { 0, 0, 0, 0 };
			for (int i = 1; i <= 3; i++)
			{
				startDate = startDate.AddMonths(-1);
				endDate = startDate.AddMonths(1);
				previousMonthsAmountOwed[i - 1] = AmountDue(startDate, endDate);				
			}

			// Now get sum for outstanding invoices for 3+ months ago
			endDate = startDate.AddMilliseconds(-1);
			startDate = new DateTime(1900,1,1);
			previousMonthsAmountOwed[3] = AmountDue(startDate, endDate);

			decimal total = previousMonthsAmountOwed[0] + previousMonthsAmountOwed[1] + previousMonthsAmountOwed[2] + previousMonthsAmountOwed[3] + currentAmountDue;

			sb.Append(@"<br><table border='0' cellspacing='0' cellpadding='3' width='600' class='BorderBlack Right Top Bottom'>
						<tr><td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center'><b>3+ MONTHS</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center'><b>3 MONTHS</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center'><b>2 MONTHS</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center'><b>1 MONTH</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='center'><b>CURRENT</b></td>
							</tr>
						<tr><td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(previousMonthsAmountOwed[3]) + @"</td>
                            <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(previousMonthsAmountOwed[2]) + @"</td>
                            <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(previousMonthsAmountOwed[1]) + @"</td>
                            <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(previousMonthsAmountOwed[0]) + @"</td>
							<td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(currentAmountDue) + @"</td>
                        </tr></table>
						<br><table border='0' cellspacing='0' cellpadding='3' width='600'>
							<tr><td width=460></td>
								<td style='vertical-align:bottom;' class='BorderBlack All' align='center'><b>TOTAL</b></td></tr>
							<tr><td width=460></td>
								<td class='BorderBlack Bottom Left Right' align='right'>" + total.ToString("c") + @"</td></tr></table>");

			//sb.Append("<br><br><a href='http://");
			//sb.Append(Vars.DomainName);
			//sb.Append("/promoters/");
			//sb.Append(this.UrlName);
			//sb.Append("/invoices'><font size='+1'><b>Click Here To Pay Invoices</b></font></a></td></tr></table>");			

			sb.Append(@"</td></tr></table></td></tr>");

			// DSI Registration Footer
			sb.Append(Utilities.GenerateHTMLFooterRowString());
//            sb.Append(@"<tr><td valign='bottom'><hr>
//						<table width='100%' cellpadding='0' cellspacing='0'><tr>
			//							<td style='height:1' valign='bottom' align='left'><font size=1>Greenhill House, Thorpe Road, Peterborough, PE3 6RU</font></td>
//							<td style='height:1' valign='bottom' align='left'><font size=1><b>T</b>:&nbsp;&nbsp;<br><b>F</b>:&nbsp;&nbsp;<br><br><b>E</b>:&nbsp;&nbsp;</font></td>
//							<td style='height:1' valign='bottom' align='left'><font size=1>0207 835 5599<br>0870 068 8822<br><br><a href='mailto:accounts@dontstayin.com'>accounts@dontstayin.com</a></font></td>
			//							<td style='height:1' align='right' valign='bottom'><font size=1>Development Hell Limited<br>VAT Reg. No. 796 5005 04<br>Registered in England No. 04333049<br>Greenhill House, Thorpe Road, Peterborough, PE3 6RU<br>All business is undertaken subject to our standard terms and conditions</font></td></tr>
//								</table>");

			sb.Append(@"</table></div></form>");

			return sb;
		}
		#endregion

		#region AssignSalesUsrAndUpdate
		public void AssignSalesUsrAndUpdate(int newSalesUsrK)
		{
			AssignSalesUsrAndUpdate(new Usr(newSalesUsrK));
		}

		public void AssignSalesUsrAndUpdate(Usr newSalesUsr)
		{
			try
			{
				if (newSalesUsr.SalesTeam <= 0)
				{
					throw new Exception("Failed to assign " + newSalesUsr.NickName + " (" + newSalesUsr.K.ToString() + ") to promoter " + this.Name
										+ " (" + this.K.ToString() + ") as new sales user because they are not on a sales team.");
				}

				this.SalesUsrK = newSalesUsr.K;
				this.SalesUsr = newSalesUsr;
				this.Update();

				this.FixQuestionsThreadUsrs();
			}

			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception occurred in AssignSalesUsr() for promoter " + this.Name + " (" + this.K.ToString() + ")", 
											"Exception occurred in AssignSalesUsr() for promoter " + this.Name + " (" + this.K.ToString() + ")", ex, this);
			}
		}

		public void FixQuestionsThreadUsrs()
		{
			if (this.QuestionsThreadK > 0 && this.AdminUsrs.Count > 0)
			{
				try
				{
					Thread questionsThread = new Thread(this.QuestionsThreadK);

					Query questionsQuery = new Query(new Q(ThreadUsr.Columns.ThreadK, this.QuestionsThreadK));
					questionsQuery.Columns = new ColumnSet(ThreadUsr.Columns.ThreadK, ThreadUsr.Columns.UsrK);
					ThreadUsrSet threadUsrs = new ThreadUsrSet(questionsQuery);
					UsrSet salesTeam2 = Usr.GetNewPromoterSalesUsrsNameAndK();
					bool removeUsr;
					for (int i = threadUsrs.Count - 1; i >= 0; i--)
					{
						removeUsr = true;
						if (this.SalesUsrK > 0 && this.SalesUsrK == threadUsrs[i].UsrK)
						{
							removeUsr = false;
						}
						else
						{
							if (this.SalesUsrK == 0)
							{
								salesTeam2.Reset();
								foreach (Usr salesUsr in salesTeam2)
								{
									if (threadUsrs[i].UsrK == salesUsr.K)
									{
										removeUsr = false;
										break;
									}
								}
							}
							if (removeUsr)
							{
								this.AdminUsrs.Reset();
								foreach (Usr promoterUsr in this.AdminUsrs)
								{
									if (threadUsrs[i].UsrK == promoterUsr.K)
									{
										removeUsr = false;
										break;
									}
								}
							}
						}
						if (removeUsr)
							threadUsrs[i].Delete();
					}

					if (this.SalesUsrK > 0)
						questionsThread.AddThreadUsrWithoutInvite(this.SalesUsrK);
					else
					{
						salesTeam2.Reset();
						foreach (Usr salesUsr in salesTeam2)
						{
							questionsThread.AddThreadUsrWithoutInvite(salesUsr.K);
						}
					}

					this.AdminUsrs.Reset();
					foreach (Usr promoterUsr in this.AdminUsrs)
					{
						questionsThread.AddThreadUsrWithoutInvite(promoterUsr.K);
					}
					
					UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob(questionsThread);
					job.ExecuteSynchronously();
				}
				catch(Exception ex)
				{
					Utilities.AdminEmailAlert("Exception occurred in Promoter.FixQuestionsThreadUsrs() for promoter " + this.Name + " (" + this.K.ToString() + ")", 
												"Exception occurred in Promoter.FixQuestionsThreadUsrs() for promoter " + this.Name + " (" + this.K.ToString() + ")", ex, this);
				}
			}
		}
		#endregion

		#region UpdateTicketInvoiceItemTaxCode
		public void UpdateTicketInvoiceItemTaxCode()
		{
			if(this.VatStatus == VatStatusEnum.Registered)
			{
				InvoiceItem.VATCodes vatCode = InvoiceItem.VATCodes.T1;
				foreach (TicketPromoterEvent ticketPromoterEvent in TicketPromoterEvents)
				{
					bool updated = false;

					foreach (TicketRun ticketRun in ticketPromoterEvent.TicketRuns)
					{
						foreach (Ticket ticket in ticketRun.Tickets)
						{
							try
							{
								InvoiceItem ticketInvoiceItem = new InvoiceItem(ticket.InvoiceItemK);
								if (ticketInvoiceItem.VatCode != vatCode)
								{
									decimal total = ticketInvoiceItem.Total;
									ticketInvoiceItem.VatCode = vatCode;
									ticketInvoiceItem.SetTotal(total);
									ticketInvoiceItem.Update();
									ticketInvoiceItem.Invoice.UpdatePrice();
									ticketInvoiceItem.Invoice.Update();

									if (ticket.Cancelled)
									{
										foreach (Invoice credit in ticketInvoiceItem.Invoice.CreditsApplied)
										{
											foreach (InvoiceItem creditItem in credit.Items)
											{
												if (creditItem.Type == ticketInvoiceItem.Type && Math.Abs(Math.Round(creditItem.Total, 2)) == Math.Round(ticketInvoiceItem.Total, 2))
												{
													creditItem.VatCode = vatCode;
													creditItem.SetTotal(creditItem.Total);
													creditItem.Update();
													creditItem.Invoice.UpdatePrice();
													creditItem.Invoice.Update();
												}
											}
										}
									}

									updated = true;
								}
							}
							catch (Exception ex)
							{
								Utilities.AdminEmailAlert("Exception occurred for TicketK= " + ticket.K.ToString(), "Exception occurred in UpdateTicketInvoiceItemTaxCode()", ex, this);
							}
						}
					}

					if (updated)
					{
						ticketPromoterEvent.CalculateTotalFundsAndVat();
						ticketPromoterEvent.Update();
						if (ticketPromoterEvent.FundsTransfer != null && ticketPromoterEvent.FundsTransfer.Amount != ticketPromoterEvent.TotalFunds)
						{
							// Admin email alert
							Utilities.AdminEmailAlert("Funds do not match release transfer funds.<br>TransferK= " + ticketPromoterEvent.FundsTransfer.K.ToString()
														+ ", PromoterK= " + ticketPromoterEvent.PromoterK.ToString() + ", EventK= " + ticketPromoterEvent.EventK.ToString(), 
													  "Exception occurred in UpdateTicketInvoiceItemTaxCode()", new Exception(), this);
						}
					}
				}
			}
		}
		#endregion

		public BannerFolderSet BannerFolders
		{
			get
			{
					Query query = new Query(new Q(Bobs.BannerFolder.Columns.PromoterK, this.K));
					query.OrderBy = new OrderBy(Bobs.BannerFolder.Columns.K, OrderBy.OrderDirection.Descending);
					return new BannerFolderSet(query);
			}
		}

		#region HasUpcomingEventsWithGuestlists
		public bool HasUpcomingEventsWithGuestlists
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new And(
									new Q(Event.Columns.HasGuestlist, true),
									new Q(Event.Columns.GuestlistPromoterK, K), 
									new Q(Event.Columns.DateTime,QueryOperator.GreaterThanOrEqualTo, DateTime.Now));
				q.NoLock = true;
				EventSet es = new EventSet(q);
				return es.Count > 0;
			}
		}
		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion
		#region IIcon Members

		public string IconUrl
		{
			get { return ""; }
		}

		#endregion
	}
	#endregion

	#region PromoterUsr

	#endregion

}
