using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Bobs;
using Common;

namespace Bobs
{
	#region TicketRun
	/// <summary>
	/// Run of tickets offered for sale
	/// </summary>
	[Serializable] 
	public partial class TicketRun : IBobAsHTML
	{
		#region Simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return this[TicketRun.Columns.K] as int? ?? 0; }
			set { this[TicketRun.Columns.K] = value; }
		}
		/// <summary>
		/// Event this ticket is for
		/// </summary>
		public override int EventK
		{
			get { return (int)this[TicketRun.Columns.EventK]; }
			set { this[TicketRun.Columns.EventK] = value; }
		}
		/// <summary>
		/// Promoter selling the ticket
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[TicketRun.Columns.PromoterK]; }
			set { this[TicketRun.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Brand this ticket is for (zero if not relevant)
		/// </summary>
		public override int BrandK
		{
			get { return (int)this[TicketRun.Columns.BrandK]; }
			set { this[TicketRun.Columns.BrandK] = value; }
		}
		/// <summary>
		/// Brief name for ticket run: i.e. VIP, Early bird, etc
		/// </summary>
		public override string Name
		{
			get { return (string)this[TicketRun.Columns.Name]; }
			set { this[TicketRun.Columns.Name] = value; }
		}
		/// <summary>
		/// Description short description e.g. "Early Bird"
		/// </summary>
		public override string Description
		{
			get { return (string)this[TicketRun.Columns.Description]; }
			set { this[TicketRun.Columns.Description] = value; }
		}
		/// <summary>
		/// Price in pounds
		/// </summary>
		public override decimal Price
		{
			get { return (decimal)this[TicketRun.Columns.Price]; }
			set { this[TicketRun.Columns.Price] = value; }
		}
		/// <summary>
		/// Our booking fee
		/// </summary>
		public override decimal BookingFee
		{
			get { return (decimal)this[TicketRun.Columns.BookingFee]; }
			set { this[TicketRun.Columns.BookingFee] = value; }
		}
		/// <summary>
		/// If locked, the promoter won't be able to edit the price
		/// </summary>
		public override bool LockPrice
		{
			get { return (bool)this[TicketRun.Columns.LockPrice]; }
			set { this[TicketRun.Columns.LockPrice] = value; }
		}
		/// <summary>
		/// If this is specified, these tickets aren't offered until a different ticket type sells out or date e
		/// </summary>
		public override int FollowsTicketRunK
		{
			get { return (int)this[TicketRun.Columns.FollowsTicketRunK]; }
			set { this[TicketRun.Columns.FollowsTicketRunK] = value; }
		}
		/// <summary>
		/// Tickets are offered from this DateTime onward
		/// </summary>
		public override DateTime StartDateTime
		{
			get { return (DateTime)this[TicketRun.Columns.StartDateTime]; }
			set { this[TicketRun.Columns.StartDateTime] = value; }
		}
		/// <summary>
		/// Tickets are unavailable after this DateTime
		/// </summary>
		public override DateTime EndDateTime
		{
			get { return (DateTime)this[TicketRun.Columns.EndDateTime]; }
			set { this[TicketRun.Columns.EndDateTime] = value; }
		}
		/// <summary>
		/// Is this TicketRun enabled? E.g. may want to disable it early.
		/// </summary>
		//public override bool Enabled
		//{
		//    get { return (bool)this[TicketRun.Columns.Enabled]; }
		//    set { this[TicketRun.Columns.Enabled] = value; }
		//}
		/// <summary>
		/// Maximum number of tickets to sell
		/// </summary>
		public override int MaxTickets
		{
			get { return (int)this[TicketRun.Columns.MaxTickets]; }
			set { this[TicketRun.Columns.MaxTickets] = value; }
		}
		/// <summary>
		/// Number of tickets sold at the moment
		/// </summary>
		public override int SoldTickets
		{
			get { return (int)this[TicketRun.Columns.SoldTickets]; }
			set { this[TicketRun.Columns.SoldTickets] = value; }
		}
		/// <summary>
		/// Order in the list on the event page
		/// </summary>
		public override double ListOrder
		{
			get { return (double)this[TicketRun.Columns.ListOrder]; }
			set { this[TicketRun.Columns.ListOrder] = value; }
		}
		/// <summary>
		/// Has the selling of this ticket run been paused
		/// </summary>
		public override bool Paused
		{
			get { return (bool)this[TicketRun.Columns.Paused]; }
			set { this[TicketRun.Columns.Paused] = value; }
		}
		/// <summary>
		/// Guid to catch duplicate "save" clicks
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[TicketRun.Columns.DuplicateGuid]); }
			set { this[TicketRun.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Bit flag to note when email has been sent to promoter after ticket run has ended
		/// </summary>
		public override bool EmailSent
		{
			get { return (bool)this[TicketRun.Columns.EmailSent]; }
			set { this[TicketRun.Columns.EmailSent] = value; }
		}
		/// <summary>
		/// Method for delivering tickets
		/// </summary>
		public override DeliveryMethodType DeliveryMethod
		{
			get { return (DeliveryMethodType)this[TicketRun.Columns.DeliveryMethod]; }
			set { this[TicketRun.Columns.DeliveryMethod] = value; }
		}
		/// <summary>
		/// Charge for delivering tickets
		/// </summary>
		public override decimal DeliveryCharge
		{
			get { return (decimal)this[TicketRun.Columns.DeliveryCharge]; }
			set { this[TicketRun.Columns.DeliveryCharge] = value; }
		}

		/// <summary>
		/// Approximate date tickets usrs will be told tickets will be delivered
		/// </summary>
		public override DateTime DeliveryDate
		{
			get { return (DateTime)this[TicketRun.Columns.DeliveryDate]; }
			set { this[TicketRun.Columns.DeliveryDate] = value; }
		}


		#endregion

		#region IBobAsHTML methods
		public string AsHTML()
		{
			string lineReturn = Vars.HTML_LINE_RETURN;
			StringBuilder sb = new StringBuilder();

			sb.Append(lineReturn);
			sb.Append(lineReturn);
			sb.Append("<u>Ticket run details</u>");
			sb.Append(lineReturn);
			sb.Append("K: ");
			sb.Append(this.K.ToString());
			sb.Append(lineReturn);
			sb.Append(this.PriceBrandName);
			sb.Append(lineReturn);
			if (this.Promoter != null)
			{
				sb.Append("Promoter: ");
				sb.Append(this.Promoter.Name);
				sb.Append(" (K: ");
				sb.Append(this.PromoterK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			if (this.Event != null)
			{
				sb.Append("Event: ");
				sb.Append(this.Event.FriendlyName);
				sb.Append(" (K: ");
				sb.Append(this.EventK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			sb.Append("Status: ");
			sb.Append(this.Status.ToString());
			sb.Append(lineReturn);
			sb.Append("Starts: ");
			sb.Append(this.StartDateTime.ToString("ddd dd/MM/yy HH:mm"));
			sb.Append(lineReturn);
			sb.Append("Ends: ");
			sb.Append(this.EndDateTime.ToString("ddd dd/MM/yy HH:mm"));
			sb.Append(lineReturn);
			sb.Append("Booking fee: ");
			sb.Append(this.BookingFee.ToString("c"));
			sb.Append(lineReturn);
			sb.Append("Description: ");
			sb.Append(this.Description.ToString());
			sb.Append(lineReturn);
			sb.Append("Max tickets: ");
			sb.Append(this.MaxTickets.ToString("N0"));
			sb.Append(lineReturn);

			return sb.ToString();
		}
		#endregion

		#region Enums
		
		#endregion

		#region Properties
		#region Status
		public TicketRunStatus Status
		{
			get
			{
				if (EndDateTime <= DateTime.Now)
				{
					if (SoldTickets > 0 && CancelledTicketQuantity == SoldTickets)
						return TicketRunStatus.Refunded;
					else
						return TicketRunStatus.Ended;
				}
				else if (SoldTickets >= MaxTickets)
					return TicketRunStatus.SoldOut;
				else if (Paused)
					return TicketRunStatus.Paused;
				else if (StartDateTime > DateTime.Now)
					return TicketRunStatus.WaitingStartDate;
				// Follows another ticket run if that other ticket run hasnt finished or sold out
				else if (FollowsTicketRunK > 0 && !(FollowsTicketRun.EndDateTime <= DateTime.Now || FollowsTicketRun.SoldTickets == FollowsTicketRun.MaxTickets))
					return TicketRunStatus.WaitingToFollowOtherTicketRun;
				else
					return TicketRunStatus.Running;
			}
		}
		#endregion

		#region IsUpdateable
		public bool IsUpdateable
		{
			get
			{
				return this.K == 0 || EndDateTime > DateTime.Now;
			}
		}
		#endregion

		#region CurrentNumberOfTicketsSold
		public int CurrentNumberOfTicketsSold
		{
			get
			{
				Query q = new Query(new And(new Q(Ticket.Columns.TicketRunK, this.K),
										    Ticket.CurrentTicketsQ));
				q.ExtraSelectElements.Add("SumSoldTickets", "SUM([Ticket].[Quantity])");
				q.Columns = new ColumnSet();

				TicketSet tickets = new TicketSet(q);
				if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumSoldTickets"] != DBNull.Value)
					return Convert.ToInt32(tickets[0].ExtraSelectElements["SumSoldTickets"]);
				else
					return 0;
			}
		}
		#endregion

		#region SoldTicketsQ
		public Q SoldTicketsQ
		{
			get
			{
				return new And(new Q(Ticket.Columns.TicketRunK, this.K),
							   Ticket.SoldTicketsQ);
			}
		}
		#endregion

		#region ValidTicketsQ
		public Q ValidTicketsQ
		{
			get
			{
				return new And(new Q(Ticket.Columns.TicketRunK, this.K),
							   Ticket.ValidTicketsQ);
			}
		}
		#endregion

		#region CancelledTicketsQ
		public Q CancelledTicketsQ
		{
			get
			{
				return new And(new Q(Ticket.Columns.TicketRunK, this.K),
							   Ticket.CancelledTicketsQ);
			}
		}
		#endregion

		#region AllTicketsQ
		public Q AllTicketsQ
		{
			get
			{
				return new And(new Q(Ticket.Columns.TicketRunK, this.K),
							   new Q(Ticket.Columns.Enabled, true));
			}
		}
		#endregion
	

		#region PauseResumeIconHtml
		public string PauseResumeIconHtml
		{
			get
			{
				if (this.IsUpdateable)
				{
					if (this.Paused)
						return Utilities.IconHtml(Utilities.Icon.Resume);
					else
						return Utilities.IconHtml(Utilities.Icon.Pause);
				}
				else
					return "";
			}
		}
		#endregion

		#region StopIconHtml
		public string StopIconHtml
		{
			get
			{
				if (this.IsUpdateable)
					return Utilities.IconHtml(Utilities.Icon.Stop);
				else
					return Utilities.IconHtml(Utilities.Icon.Cross);
			}
		}
		#endregion

		#region PriceBrandName
		public string PriceBrandName
		{
			get
			{
				string output = this.Price.ToString("c");
				string brandAndName = this.BrandAndName;
				if (brandAndName.Length > 0)
					output += " : " + brandAndName;
				return output;
			}
		}

		public string BrandAndName
		{
			get
			{
				//string output = "";
				//if (this.Brand != null)
				//{
				//    if (this.Event.TicketRunBrandCount > 1)
				//        output += this.Brand.Name;
				//}
				//if (this.Name.Length > 0)
				//{
				//    if (output.Length > 0)
				//        output += " : ";
				//    output += this.Name;
				//}
				//return output;


				// Disabling the displaying of Brands as per request by Dave Brophy on May 10, 2007.
				// No need for brand selection, as promoter will include ticket name details and all brands associated with an event will be available to join after positive feedback
				return this.Name;
			}
		}

		public string LinkPriceBrandName
		{
			get
			{
				return Utilities.Link(this.Url(), this.PriceBrandName);
			}
		}

        public string LinkNewWindowPriceBrandName
        {
            get
            {
                return Utilities.Link(this.Url(), this.PriceBrandName);
            }
        }


		#endregion

		#region LinkEditIconHtml
		public string LinkEditIconHtml
		{
			get
			{
				return Utilities.Link(this.Url(), Utilities.IconHtml(Utilities.Icon.Edit));
			}
		}
		#endregion

		#region LinkTicketsIcon
		public string LinkTicketsIcon
		{
			get
			{
				return Utilities.Link(this.UrlAdminTickets, Utilities.IconHtml(Utilities.Icon.Tickets));
			}
		}
		#endregion

		#region PriceEventAndName
		public string PriceEventAndName
		{
			get
			{
				string output = this.Price.ToString("c");
				if (this.Event != null)
				{
					if (this.Event.Name.Length > 0)
						output += " : " + Cambro.Misc.Utility.Snip(this.Event.Name, 30) + " on " + Utilities.DateToString(this.Event.DateTime);
				}
				if (this.Name.Length > 0)
					output += " : " + this.Name;
				return output;
			}
		}
		#endregion

		#region Promoter
		public Promoter Promoter
		{
		    get
		    {
				if (promoter == null && PromoterK > 0)
					promoter = new Promoter(PromoterK, this, TicketRun.Columns.PromoterK);
				
				return promoter;				
		    }
		    set
		    {
		        promoter = value;
		        if (promoter != null)
		            this.PromoterK = promoter.K;
		        else
		            this.PromoterK = 0;
		    }
		}
		Promoter promoter;
		#endregion

		#region Event
		public Event Event
		{
			get
			{
				if (_Event == null && EventK > 0)
					_Event = new Event(EventK, this, TicketRun.Columns.EventK);
				return _Event;
			}
			set
			{
				_Event = value;
				if (_Event != null)
					this.EventK = _Event.K;
				else
					this.EventK = 0;
			}
		}
		Event _Event;
		#endregion

		#region Brand
		public Brand Brand
		{
			get
			{
				if (brand == null && BrandK > 0)
					brand = new Brand(BrandK, this, TicketRun.Columns.BrandK);
				return brand;
			}
			set
			{
				brand = value;
				if (brand != null)
					this.BrandK = brand.K;
				else
					this.BrandK = 0;
			}
		}
		Brand brand;
		#endregion

		#region FollowsTicketRun
		public TicketRun FollowsTicketRun
		{
			get
			{
				if (followsTicketRun == null && FollowsTicketRunK > 0)
					followsTicketRun = new TicketRun(FollowsTicketRunK, this, TicketRun.Columns.FollowsTicketRunK);
				return followsTicketRun;
			}
			set
			{
				followsTicketRun = value;
				if (followsTicketRun != null)
					this.FollowsTicketRunK = followsTicketRun.K;
				else
					this.FollowsTicketRunK = 0;
			}
		}
		TicketRun followsTicketRun;
		#endregion

		#region Joins
		public static Join EventJoin
		{
			get
			{
				return new Join(TicketRun.Columns.EventK, Event.Columns.K);
			}
		}
		#endregion

		#region TicketPromoterEvent
		public TicketPromoterEvent TicketPromoterEvent
		{
			get
			{
				if (ticketPromoterEvent == null)
				{
					try
					{
						ticketPromoterEvent = new TicketPromoterEvent(this.PromoterK, this.EventK);
					}
					catch 
					{
						return null;
					}
				}

				return ticketPromoterEvent;
			}
		}
		private TicketPromoterEvent ticketPromoterEvent;
		#endregion

		#region Tickets
		public TicketSet Tickets
		{
			get
			{
				if (tickets == null)
				{
					tickets = new TicketSet(new Query(this.SoldTicketsQ));
				}

				return tickets;
			}
		}
		private TicketSet tickets;
		#endregion

		#region CancelledTickets
		public TicketSet CancelledTickets
		{
			get
			{
				if (cancelledTickets == null)
				{
					cancelledTickets = new TicketSet(new Query(this.CancelledTicketsQ));
				}

				return cancelledTickets;
			}

			set
			{
				cancelledTickets = value;
			}
		}
		private TicketSet cancelledTickets;

		public int CancelledTicketQuantity
		{
			get
			{
				if (cancelledTicketQuantity == -1)
				{
					if(cancelledTickets == null)
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("SumSoldTickets", "SUM([Ticket].[Quantity])");
						q.Columns = new ColumnSet();
						q.QueryCondition = new And(new Q(Ticket.Columns.TicketRunK, this.K),
												   Ticket.CancelledTicketsQ);

						TicketSet tickets = new TicketSet(q);
						if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumSoldTickets"] != DBNull.Value)
							cancelledTicketQuantity = Convert.ToInt32(tickets[0].ExtraSelectElements["SumSoldTickets"]);
						else
							cancelledTicketQuantity = 0;
					}
					else
					{
						foreach(Ticket cancelledTicket in cancelledTickets)
						{
							cancelledTicketQuantity += cancelledTicket.Quantity;
						}
					}					
				}

				return cancelledTicketQuantity;
			}
		}
		private int cancelledTicketQuantity = -1;

		public int CancelledTicketBeforeFundReleaseQuantity
		{
			get
			{
				if (cancelledTicketBeforeFundReleaseQuantity == -1)
				{
					//if (cancelledTickets == null)
					//{
						Query q = new Query();
						q.ExtraSelectElements.Add("SumSoldTickets", "SUM([Ticket].[Quantity])");
						q.Columns = new ColumnSet();
						q.QueryCondition = new And(new Q(Ticket.Columns.TicketRunK, this.K),
												   Ticket.CancelledTicketsBeforeFundsReleaseQ);

						TicketSet tickets = new TicketSet(q);
						if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumSoldTickets"] != DBNull.Value)
							cancelledTicketBeforeFundReleaseQuantity = Convert.ToInt32(tickets[0].ExtraSelectElements["SumSoldTickets"]);
						else
							cancelledTicketBeforeFundReleaseQuantity = 0;
					//}
					//else
					//{
					//    foreach (Ticket cancelledTicket in cancelledTickets)
					//    {
					//        cancelledTicketBeforeFundReleaseQuantity += cancelledTicket.Quantity;
					//    }
					//}
				}

				return cancelledTicketBeforeFundReleaseQuantity;
			}
		}
		private int cancelledTicketBeforeFundReleaseQuantity = -1;
		#endregion

		#region ValidTicketQuantity
		public int ValidTicketQuantity
		{
			get
			{
				return SoldTickets - CancelledTicketQuantity;
			}
		}
		#endregion

		#region FundsReleased
		public bool FundsReleased
		{
			get
			{
				if (fundsReleased == false)
				{
					Query ticketRunFundsReleasedQuery = new Query(new And(new Q(TicketPromoterEvent.Columns.PromoterK, this.PromoterK),
																		  new Q(TicketPromoterEvent.Columns.EventK, this.EventK),
																		  new Q(TicketPromoterEvent.Columns.FundsReleased, true)));

					ticketRunFundsReleasedQuery.ReturnCountOnly = true;
					fundsReleased = new TicketPromoterEventSet(ticketRunFundsReleasedQuery).Count > 0;
				}

				return fundsReleased;
			}
		}
		private bool fundsReleased = false;

		#endregion
		#endregion

		#region Methods
		public static ListItem[] StatusesAsListItemArray()
		{
			List<ListItem> listItems = new List<ListItem>();

			listItems.Add(new ListItem(Utilities.CamelCaseToString(TicketRunStatus.Ended.ToString()), Convert.ToInt32(TicketRunStatus.Ended).ToString()));
			listItems.Add(new ListItem(Utilities.CamelCaseToString(TicketRunStatus.Paused.ToString()), Convert.ToInt32(TicketRunStatus.Paused).ToString()));
			listItems.Add(new ListItem(Utilities.CamelCaseToString(TicketRunStatus.Refunded.ToString()), Convert.ToInt32(TicketRunStatus.Refunded).ToString()));
			listItems.Add(new ListItem(Utilities.CamelCaseToString(TicketRunStatus.Running.ToString()), Convert.ToInt32(TicketRunStatus.Running).ToString()));
			listItems.Add(new ListItem(Utilities.CamelCaseToString(TicketRunStatus.SoldOut.ToString()), Convert.ToInt32(TicketRunStatus.SoldOut).ToString()));
			listItems.Add(new ListItem(Utilities.CamelCaseToString(TicketRunStatus.WaitingStartDate.ToString()), Convert.ToInt32(TicketRunStatus.WaitingStartDate).ToString()));
			listItems.Add(new ListItem(Utilities.CamelCaseToString(TicketRunStatus.WaitingToFollowOtherTicketRun.ToString()), Convert.ToInt32(TicketRunStatus.WaitingToFollowOtherTicketRun).ToString()));

			return listItems.ToArray();
		}

		/// <summary>
		/// Checks if this ticket run has already been saved, based on the ViewState "DuplicateGuid".
		/// This is used to prevent a new ticket run being saved twice and creating 2 DB records
		/// </summary>
		/// <returns></returns>
		public static bool DoesDuplicateGuidExistInDb(Guid checkGuid)
		{
			//select duplicate invoices
			Query duplicateTicketRunQuery = new Query(new Q(TicketRun.Columns.DuplicateGuid, checkGuid));
			duplicateTicketRunQuery.ReturnCountOnly = true;
			return new TicketRunSet(duplicateTicketRunQuery).Count > 0;
		}

		public void CalculateSoldTicketsAndUpdate()
		{
			Query q = new Query(this.SoldTicketsQ);
			q.ExtraSelectElements.Add("SumSoldTickets", "SUM([Ticket].[Quantity])");
			q.Columns = new ColumnSet();

			TicketSet tickets = new TicketSet(q);
			if (tickets.Count > 0 && tickets[0].ExtraSelectElements["SumSoldTickets"] != DBNull.Value)
				this.SoldTickets = Convert.ToInt32(tickets[0].ExtraSelectElements["SumSoldTickets"]);
			else
				this.SoldTickets = 0;

			this.Update();

			this.TicketPromoterEvent.CalculateTicketsAndFunds();
		}

		public void SetBookingFee()
		{
			this.SetBookingFee(0.5m + (this.Price * 0.1m));
		}
		public void SetBookingFee(decimal fee)
		{
			if (fee < 0)
				throw new Exception("Invalid booking fee");
			this.BookingFee = Math.Round(fee, 2);
		}
		public void SetPrice(decimal price)
		{
			if (price < 0)
				throw new Exception("Invalid price");
			this.Price = Math.Round(price);
		}

		public Ticket CreateTicket(Usr buyUsr)
		{
			return CreateTicket(buyUsr, 1);
		}

        public Ticket CreateTicket(Usr buyUsr, int quantity)
		{
			Ticket ticket = new Ticket();
			ticket.BookingFee = Math.Round(this.BookingFee * quantity, 2);
			ticket.ReserveDateTime = DateTime.Now;
			ticket.BuyerUsrK = buyUsr.K;
            // First and last name will be overwritten from payment control
            ticket.FirstName = buyUsr.FirstName;
            ticket.LastName = buyUsr.LastName;
			ticket.EventK = this.EventK;
			ticket.Price = Math.Round(this.Price * quantity, 2);
			ticket.Quantity = quantity;
			ticket.TicketRun = this;

			if (Domain.CurrentK > 0)
			{
				ticket.DomainK = Domain.CurrentK;
			}

			return ticket;
		}
		#region ListItem
		public ListItem ToListItem(bool includeDate)
		{
			return this.ToListItem(30000, includeDate);
		}
		public ListItem ToListItem(int maxNameLength, bool includeDate)
		{
			string ticketRunText = this.Price.ToString("c");
			if (this.Name.Length > 0)
				ticketRunText += " " + (this.Name.Length > maxNameLength ? this.Name.Substring(0, maxNameLength - 2) + "..." : this.Name);
			if (includeDate)
				ticketRunText += " " + Utilities.DateToString(this.StartDateTime) + "-" + Utilities.DateToString(this.EndDateTime);

			return new ListItem(ticketRunText, this.K.ToString());
		}
		#endregion

		#region IsCircularDependancy
		public bool IsCircularDependancy()
		{
			try
			{
				if (this.K == 0 || this.FollowsTicketRunK == 0)
					return false;
				else
				{
					TicketRun followsTicketRun = this.FollowsTicketRun;
					while (followsTicketRun.FollowsTicketRunK != 0)
					{
						if (followsTicketRun.FollowsTicketRunK == this.K)
							return true;
						else
							followsTicketRun = followsTicketRun.FollowsTicketRun;
					}

					return false;
				}
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region EndTicketRun
		public void EndTicketRun()
		{
			if (this.EndDateTime > DateTime.Now)
			{
				this.EndDateTime = DateTime.Now;
				this.Update();

				try
				{
					TicketPromoterEvent tpe = new TicketPromoterEvent(this.PromoterK, this.EventK);
					Utilities.EmailTicketRunStatusUpdate(tpe);

					this.EmailSent = true;
					this.Update();
				}
				catch (Exception ex)
				{
					Utilities.AdminEmailAlert("Exception in TicketRun.EndTicketRun(). TicketRun K=" + this.K.ToString(), "Exception in TicketRun.EndTicketRun()", ex, this);
				}
			}
		}
		#endregion

		#region Refunds
		public void RefundAllTickets(Usr actionUsr)
		{
			if (this.SoldTickets == 0)
				throw new Exception("Cannot refund this ticket run, it has not sold any tickets");

			if (this.Status == TicketRunStatus.Refunded)
				throw new Exception("Cannot refund this ticket run, it has already been refunded.");

			// If it hasnt ended, end it now. Refunding all tickets ends the ticket run.
			if (this.EndDateTime > DateTime.Now)
			{
				this.EndDateTime = DateTime.Now;
				this.Update();
			}

            Ticket.Refund(actionUsr, this.Tickets);

			// Reset cancelled tickets
			this.CancelledTickets = null;
		}
		#endregion

		#region Url
		public string UrlFilterPart
		{
			get
			{
				return this.Promoter.UrlFilterPart + "/TicketRun/K-" + this.K.ToString();
			}
		}
		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, null, par);
		}
		public string UrlAdminTickets
		{
			get
			{
				return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "ticketsearch", "ticketrunk", this.K.ToString());
			}
		}
		#endregion
		#endregion

	 

		public string GetPaymentControlConfirmationMessage()
		{
			switch (DeliveryMethod)
			{
				case TicketRun.DeliveryMethodType.E_Ticket: 
					return "I understand that I will NOT get in unless I take my credit/debit card with me to the event!"; 
				case TicketRun.DeliveryMethodType.SpecialDelivery: 
					return "I understand that these tickets will be sent to me by " + EnumExtensions.GetDescription(DeliveryMethod) + " at the address I enter here. This is the registered address for my credit/debit card. The tickets will arrive on or after " + DeliveryDate.ToLongDateString() + "."; 
				default: throw new NotImplementedException();
			}
		}
	}
	#endregion
}
