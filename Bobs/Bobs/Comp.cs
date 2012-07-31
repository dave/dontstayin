using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
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
using System.Collections.Generic;

namespace Bobs
{

	#region Comp
	[Serializable]
	public partial class Comp : IPage, IPic, IName, IReadableReference, IArchive, IDeleteAll, IBobType
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Comp.Columns.K] as int? ?? 0; }
			set { this[Comp.Columns.K] = value; }
		}
		/// <summary>
		/// DateTime that the competition was added to the system
		/// </summary>
		public override DateTime DateTimeAdded
		{
			get { return (DateTime)this[Comp.Columns.DateTimeAdded]; }
			set { this[Comp.Columns.DateTimeAdded] = value; }
		}
		/// <summary>
		/// DateTime that the competition starts
		/// </summary>
		public override DateTime DateTimeStart
		{
			get { return (DateTime)this[Comp.Columns.DateTimeStart]; }
			set { this[Comp.Columns.DateTimeStart] = value; }
		}
		/// <summary>
		/// DateTime that the competition closes
		/// </summary>
		public override DateTime DateTimeClose
		{
			get { return (DateTime)this[Comp.Columns.DateTimeClose]; }
			set { this[Comp.Columns.DateTimeClose] = value; }
		}
		/// <summary>
		/// Question
		/// </summary>
		public override string Question
		{
			get { return (string)this[Comp.Columns.Question]; }
			set { this[Comp.Columns.Question] = value; }
		}
		/// <summary>
		/// Multiple choice answer 1
		/// </summary>
		public override string Answer1
		{
			get { return (string)this[Comp.Columns.Answer1]; }
			set { this[Comp.Columns.Answer1] = value; }
		}
		/// <summary>
		/// Multiple choice answer 2
		/// </summary>
		public override string Answer2
		{
			get { return (string)this[Comp.Columns.Answer2]; }
			set { this[Comp.Columns.Answer2] = value; }
		}
		/// <summary>
		/// Multiple choice answer 3
		/// </summary>
		public override string Answer3
		{
			get { return (string)this[Comp.Columns.Answer3]; }
			set { this[Comp.Columns.Answer3] = value; }
		}
		/// <summary>
		/// The correct answer - 1, 2 or 3
		/// </summary>
		public override int CorrectAnswer
		{
			get { return (int)this[Comp.Columns.CorrectAnswer]; }
			set { this[Comp.Columns.CorrectAnswer] = value; }
		}
		/// <summary>
		/// Description of the first prize
		/// </summary>
		public override string Prize
		{
			get { return (string)this[Comp.Columns.Prize]; }
			set { this[Comp.Columns.Prize] = value; }
		}
		/// <summary>
		/// Description of the second prize
		/// </summary>
		public override string Prize2
		{
			get { return (string)this[Comp.Columns.Prize2]; }
			set { this[Comp.Columns.Prize2] = value; }
		}
		/// <summary>
		/// Description of the third prize
		/// </summary>
		public override string Prize3
		{
			get { return (string)this[Comp.Columns.Prize3]; }
			set { this[Comp.Columns.Prize3] = value; }
		}
		/// <summary>
		/// HTML snippet about the sponsor. May include logo, link etc.
		/// </summary>
		public override string SponsorDetails
		{
			get { return (string)this[Comp.Columns.SponsorDetails]; }
			set { this[Comp.Columns.SponsorDetails] = value; }
		}
		/// <summary>
		/// Number of first prize winners
		/// </summary>
		public override int Winners
		{
			get { return (int)this[Comp.Columns.Winners]; }
			set { this[Comp.Columns.Winners] = value; }
		}
		/// <summary>
		/// Number of second prize winners
		/// </summary>
		public override int Winners2
		{
			get { return (int)this[Comp.Columns.Winners2]; }
			set { this[Comp.Columns.Winners2] = value; }
		}
		/// <summary>
		/// Number of third prize winners
		/// </summary>
		public override int Winners3
		{
			get { return (int)this[Comp.Columns.Winners3]; }
			set { this[Comp.Columns.Winners3] = value; }
		}
		/// <summary>
		/// Have the winners been picked and notified yet?
		/// </summary>
		public override bool WinnersPicked
		{
			get { return (bool)this[Comp.Columns.WinnersPicked]; }
			set { this[Comp.Columns.WinnersPicked] = value; }
		}
		/// <summary>
		/// The owner that has access to the results
		/// </summary>
		public override int OwnerUsrK
		{
			get { return (int)this[Comp.Columns.OwnerUsrK]; }
			set { owner = null; this[Comp.Columns.OwnerUsrK] = value; }
		}
		/// <summary>
		/// The url of the icon (should be approx 30*30)
		/// </summary>
		public override string IconFilename
		{
			get { return (string)this[Comp.Columns.IconFilename]; }
			set { this[Comp.Columns.IconFilename] = value; }
		}
		/// <summary>
		/// Prize value range - 1=�0-�100, 2=�100+, 3=�500+
		/// </summary>
		public override int PrizeValueRange
		{
			get { return (int)this[Comp.Columns.PrizeValueRange]; }
			set { this[Comp.Columns.PrizeValueRange] = value; }
		}
		/// <summary>
		/// Number of entries
		/// </summary>
		public override int Entries
		{
			get { return (int)this[Comp.Columns.Entries]; }
			set { this[Comp.Columns.Entries] = value; }
		}
		/// <summary>
		/// Cropped image 100*100
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Comp.Columns.Pic]); }
			set { this[Comp.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Original file for cropped image
		/// </summary>
		public override Guid PicOriginal
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Comp.Columns.PicOriginal]); }
			set { this[Comp.Columns.PicOriginal] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Is this a new-style promoter competition?
		/// </summary>
		public override DisplayTypes DisplayType
		{
			get { return (DisplayTypes)this[Comp.Columns.DisplayType]; }
			set { this[Comp.Columns.DisplayType] = value; }
		}
		/// <summary>
		/// Status
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[Comp.Columns.Status]; }
			set { this[Comp.Columns.Status] = value; }
		}
		/// <summary>
		/// The owner promoter
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Comp.Columns.PromoterK]; }
			set { promoter = null; this[Comp.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// The relevant brand (if any)
		/// </summary>
		public override int BrandK
		{
			get { return (int)this[Comp.Columns.BrandK]; }
			set { brand = null; this[Comp.Columns.BrandK] = value; }
		}
		/// <summary>
		/// The relevant event (if any)
		/// </summary>
		public override int EventK
		{
			get { return (int)this[Comp.Columns.EventK]; }
			set { _event = null; this[Comp.Columns.EventK] = value; }
		}
		/// <summary>
		/// Where is this competition linking to?
		/// </summary>
		public override LinkTypes LinkType
		{
			get { return (LinkTypes)this[Comp.Columns.LinkType]; }
			set { this[Comp.Columns.LinkType] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Comp.Columns.PicState]; }
			set { this[Comp.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Comp.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Comp.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Comp.Columns.PicMiscK]; }
			set { picMisc = null; this[Comp.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// Is this competition an HTML override?
		/// </summary>
		public override bool IsHtmlOverride
		{
			get { return (bool)this[Comp.Columns.IsHtmlOverride]; }
			set { this[Comp.Columns.IsHtmlOverride] = value; }
		}
		#endregion

		#region PickAllWinners()
		public static int PickAllWinners()
		{
			//First lets get the competitions done with.
			Query q = new Query();
			q.QueryCondition=new And(
					new Q(Comp.Columns.Status,Comp.StatusEnum.Enabled),
					new Q(Comp.Columns.DateTimeClose,QueryOperator.LessThanOrEqualTo, DateTime.Now),
					new Or(
						new Q(Comp.Columns.WinnersPicked,false),
						new Q(Comp.Columns.WinnersPicked,QueryOperator.IsNull,null)
					),
					new Or(
						new Q(Comp.Columns.IsHtmlOverride,false),
						new Q(Comp.Columns.IsHtmlOverride,QueryOperator.IsNull,null)
					)
				);
			if (Vars.DevEnv)
				q.TopRecords=10;
			CompSet cs = new CompSet(q);
			int i=0;
			foreach (Comp c in cs)
			{
				string subject = "";
				string body = "";
				try
				{
					c.PickWinners();
					i++;
					subject = "Drawing competition...";
					body = "<h1>Drawing competition...</h1><p>"+c.Name+" (CompK="+c.K.ToString()+")</p><p>There were "+c.Entries.ToString()+" entries.</p>";
				}
				catch
				{

					subject = "FAILED drawing competition...";
					body = "<h1>FAILED drawing competition...</h1><p>" + c.Name + " (CompK=" + c.K.ToString() + ")</p><p>Check all details and re-draw!!!</p>";

					Mailer admin = new Mailer();
					admin.TemplateType = Mailer.TemplateTypes.AdminNote;
					admin.Body = body;
					admin.Subject = subject;
					admin.To = "d.brophy@dontstayin.com";
					admin.Send();
					
				}
				
			}
			return i;
		}
		#endregion
		#region PickWinners()
		public void PickWinners()
		{

			if (!this.Running && !this.WinnersPicked)
			{
				Query q = new Query();
				q.QueryCondition=new And(
					new Q(CompEntry.Columns.CompK,this.K),
					new Q(CompEntry.Columns.Answer,this.CorrectAnswer),
					new Q(CompEntry.Columns.Winner,false),
					new Q(Usr.Columns.IsAdmin,false),
					new Q(Usr.Columns.K,QueryOperator.NotEqualTo,this.OwnerUsrK)
				);
				q.TableElement=new Join(CompEntry.Columns.UsrK,Usr.Columns.K);
				q.OrderBy=new OrderBy(OrderBy.OrderDirection.Random);
				q.TopRecords=this.Winners+this.Winners2+this.Winners3;
				CompEntrySet cs = new CompEntrySet(q);
				int winnerCount = 0;
				foreach (CompEntry ce in cs)
				{
					ce.Winner=true;

					if (winnerCount<this.Winners)
						ce.Prize=1;
					else if (winnerCount<(this.Winners+this.Winners2))
						ce.Prize=2;
					else
						ce.Prize=3;
					
					ce.Update();

					winnerCount++;

				}
				this.WinnersPicked=true;
				this.Update();

				CompEntrySet csEmails = new CompEntrySet(new Query(new Q(CompEntry.Columns.CompK,this.K)));
				Usr DSI = new Usr(8);
				foreach (CompEntry ce in csEmails)
				{
					Mailer sm = new Mailer();
					int threadK = 0;
					if (ce.Winner)
					{
						string prize = this.Prize;
						if (ce.Prize==2)
							prize = this.Prize2;
						if (ce.Prize==3)
							prize = this.Prize3;

						#region Add a PM
						string body = @"<i>This private message has been sent automatically by the DontStayIn competition system.</i>

Congratulations! "+ce.Usr.NickName+@" has won <b>"+prize+@"</b> in the competition. "+this.Owner.NickName+@" is dealing with sorting the prizes out.

The full name of "+ce.Usr.NickName+@" is <i>"+ce.Usr.FirstName+@" "+ce.Usr.LastName+@"</i>. If any more details are needed, e.g. postal address, please post them here.

<a href="""+this.Url()+@""">Click here to go to the competition page</a>";

						Thread.Maker m = new Thread.Maker();
						m.Subject="Congratulations! "+ce.Usr.NickName+@" has won a prize!";
						m.Body=body;
						m.ParentType=Model.Entities.ObjectType.None;
						m.DuplicateGuid=Guid.NewGuid();
						m.Private=true;
						m.PostingUsr=DSI;
						m.InviteKs.Add(ce.Usr.K);
						m.InviteKs.Add(this.Owner.K);

						Thread.MakerReturn r = m.Post();

						threadK = r.Thread.K;
						ce.WinnerThreadK=threadK;
						ce.Update();
						#endregion

						sm.Subject="YOU'VE WON the DontStayIn competition!";
						sm.Body="<p>You have won <b>"+prize+"</b> in our competition. For details of how to claim your prize, click the login link below.</p>";
					}
					else
					{
						sm.Subject="Sorry, you didn't win the DontStayIn competition.";
						sm.Body="<p>Sorry, you didn't win <b>"+this.Prize+"</b> in our competition. To see the winners, click the login link below.</p>";
					}
					if (ce.Winner)
					{
						Thread t = new Thread(threadK);
						sm.RedirectUrl=t.Url();
					}
					else
						sm.RedirectUrl=this.Url();

					sm.TemplateType=Mailer.TemplateTypes.AnotherSiteUser;
					sm.UsrRecipient=ce.Usr;
					sm.Send();
				}
				Mailer smOwner = new Mailer();
				smOwner.Subject="We have drawn the winners in your competition";
				smOwner.Body="<p>We've automatically picked this winner(s) in the <b>"+this.Name+"</b> competition. We've automatically invited you to a private message with each of the winners. Please arrange delivery of their prizes ASAP. To see a list of the winners, please click the login link below.</p>";
				smOwner.RedirectUrl=this.Url();
				smOwner.TemplateType=Mailer.TemplateTypes.AnotherSiteUser;
				smOwner.To=this.Owner.Email;
				smOwner.UsrRecipient=this.Owner;
				smOwner.Send();
			}
		}
		#endregion
		public static void SendPromoterReminders()
		{
			Query q = new Query();
			q.QueryCondition=new Q(Comp.Columns.Status,Comp.StatusEnum.New);
			CompSet cs = new CompSet(q);
			foreach (Comp c in cs)
			{
				try
				{
					Mailer sm = new Mailer();
					sm.TemplateType=Mailer.TemplateTypes.AnotherSiteUser;
					sm.UsrRecipient=c.Owner;
					sm.Subject="You have a DontStayIn competition that needs to be published!";
					sm.Body="<p>You've recently added a competition to DontStayIn using your promoter account, but it's not been published. You should publish your competition AS SOON as the details are finished - DO NOT wait until the opening date.</p>";
					sm.RedirectUrl=c.Promoter.UrlApp("competitions");
					sm.Send();
				}
				catch
				{
					Mailer admin = new Mailer();
					admin.TemplateType=Mailer.TemplateTypes.AdminNote;
					admin.Body = "<p>Exception sending new competition reminder</p>";
					admin.Subject = "Exception sending new competition reminder";
					admin.To = "d.brophy@dontstayin.com";
					admin.Send();
				}
			}
		}

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			Delete d = new Delete(TablesEnum.CompEntry,new Q(CompEntry.Columns.CompK,this.K));
			d.Run(transaction);
			
			this.Delete(transaction);
			
		}
		#endregion

		#region IName Members
		public string Name
		{
			get
			{
				if (Winners>1)
					return Winners.ToString()+" x "+Prize;
				else
				{
					return Prize;
				}
			}
			set
			{
			}
		}
		
		public string NameCaps
		{
			get
			{
				return Name.Substring(0,1).ToUpper()+Name.Substring(1);
			}
		}

		public string FriendlyName
		{
			get
			{
				return Name;
			}
		}

		
		#endregion

		#region CurrentComp
		public static Comp CurrentComp
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Comp.Columns.DateTimeClose, QueryOperator.GreaterThan, DateTime.Now), 
					new Q(Comp.Columns.DateTimeAdded, QueryOperator.LessThanOrEqualTo, DateTime.Now)
					);
				q.TopRecords = 1;
				CompSet cs = new CompSet(q);
				if (cs.Count==1)
					return cs[0];
				else
					return null;

			}
		}
		#endregion
		#region CurrentComps
		public static CompSet AllCurrentComps
		{
			get
			{
				Query q = new Query();
				q.QueryCondition=new And(
					new Q(Comp.Columns.DateTimeClose, QueryOperator.GreaterThan, DateTime.Now),
					new Q(Comp.Columns.DateTimeAdded, QueryOperator.LessThanOrEqualTo, DateTime.Now)
					);
				q.OrderBy=new OrderBy(
					new OrderBy(Comp.Columns.PrizeValueRange, OrderBy.OrderDirection.Descending),
					new OrderBy(Comp.Columns.DateTimeAdded)
					);
				return new CompSet(q);
			}
		}
		public static CompSet CurrentComps
		{
			get
			{
				if (Usr.Current==null)
					return AllCurrentComps;
				Query q = new Query();
				q.QueryCondition=new And(
					new Q(Comp.Columns.DateTimeClose, QueryOperator.GreaterThan, DateTime.Now),
					new Q(Comp.Columns.DateTimeAdded, QueryOperator.LessThanOrEqualTo, DateTime.Now),
					new Q(CompEntry.Columns.CompK, QueryOperator.IsNull, null)
				);
				q.TableElement=new Join(
					new TableElement(TablesEnum.Comp),
					new TableElement(TablesEnum.CompEntry),
					QueryJoinType.Left,
					new And(
					new Q(Comp.Columns.K,CompEntry.Columns.CompK,true),
					new Q(CompEntry.Columns.UsrK,Usr.Current.K)
					)
				);
				q.OrderBy=new OrderBy(
					new OrderBy(Comp.Columns.PrizeValueRange, OrderBy.OrderDirection.Descending),
					new OrderBy(Comp.Columns.DateTimeAdded)
					);
				return new CompSet(q);
			}
		}
		public static CompSet PreviousComps
		{
			get
			{
				Query q = new Query();
				q.QueryCondition=new Q(Comp.Columns.DateTimeClose, QueryOperator.LessThanOrEqualTo, DateTime.Now);
				q.OrderBy=new OrderBy(Comp.Columns.DateTimeClose, OrderBy.OrderDirection.Descending);
				return new CompSet(q);
			}
		}
		#endregion

		#region Running
		public bool Running
		{
			get
			{
				return this.DateTimeClose>DateTime.Now && this.DateTimeStart<DateTime.Now;
			}
		}
		#endregion
		#region CorrectAnswerText
		public string CorrectAnswerText
		{
			get
			{
				return (CorrectAnswer==1?Answer1:(CorrectAnswer==2?Answer2:Answer3));
			}
		}
		#endregion
		#region UsrWinners
		public UsrSet UsrWinners
		{
			get
			{
				if (WinnersPicked)
				{
					Query q = new Query();
					q.TableElement = new Join(Usr.Columns.K,CompEntry.Columns.UsrK);
					q.QueryCondition=new And(new Q(CompEntry.Columns.CompK,this.K),new Q(CompEntry.Columns.Winner,true));
					return new UsrSet(q);
				}
				else
					return null;
			}
		}
		#endregion
		
		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter==null && PromoterK>0)
					promoter = new Promoter(PromoterK);
				return promoter;
			}
			set
			{
				promoter = value;
			}
		}
		private Promoter promoter;
		#endregion
		#region Brand
		public Brand Brand
		{
			get
			{
				if (brand==null && BrandK>0)
					brand = new Brand(BrandK,this,Comp.Columns.BrandK);
				return brand;
			}
			set
			{
				brand = value;
			}
		}
		private Brand brand;
		#endregion
		#region Event
		public Event Event
		{
			get
			{
				if (_event==null && EventK>0)
					_event = new Event(EventK,this,Comp.Columns.EventK);
				return _event;
			}
			set
			{
				_event = value;
			}
		}
		private Event _event;
		#endregion


		public string OptionsHtml
		{
			get
			{
				StringBuilder str = new StringBuilder();
				if (this.WinnersPicked)
				{
					str.Append(str.Length>0?"<br>":"");
					str.Append("<a href=\"");
					str.Append(this.Url());
					str.Append("\">Winners</a>");
				}
				else
				{
					if (this.Status.Equals(StatusEnum.New))
					{
						str.Append(str.Length>0?"<br>":"");
						str.Append("<a href=\"");
						str.Append(this.Promoter.UrlApp("competitions","mode","edit","compk",this.K.ToString()));
						str.Append("\">Edit</a>");
						
						if (this.HasPic)
						{
							str.Append(str.Length>0?"<br>":"");
							str.Append("<a href=\"");
							str.Append(this.Promoter.UrlApp("competitions","mode","pic","compk",this.K.ToString()));
							str.Append("\">Change pic</a>");
						}
						else
						{
							str.Append(str.Length>0?"<br>":"");
							str.Append("<a href=\"");
							str.Append(this.Promoter.UrlApp("competitions","mode","pic","compk",this.K.ToString()));
							str.Append("\">Add pic</a>");
						}

						str.Append(str.Length>0?"<br>":"");
						str.Append("<a href=\"");
						str.Append(this.Promoter.UrlApp("competitions","mode","publish","compk",this.K.ToString()));
						str.Append("\">Publish</a>");
					}
				}
				return str.ToString();
						 
			}
		}

		public string OptionsHtmlEvent(int EventK)
		{
			StringBuilder str = new StringBuilder();
			if (this.WinnersPicked)
			{
				str.Append(str.Length>0?"<br>":"");
				str.Append("<a href=\"");
				str.Append(this.Url());
				str.Append("\">Winners</a>");
			}
			else
			{
				if (this.Status.Equals(StatusEnum.New))
				{
					str.Append(str.Length>0?"<br>":"");
					str.Append("<a href=\"");
					str.Append(this.Promoter.UrlApp("competitions","mode","edit","compk",this.K.ToString(),"eventk",EventK.ToString()));
					str.Append("\">Edit</a>");
					
					if (this.HasPic)
					{
						str.Append(str.Length>0?"<br>":"");
						str.Append("<a href=\"");
						str.Append(this.Promoter.UrlApp("competitions","mode","pic","compk",this.K.ToString(),"eventk",EventK.ToString()));
						str.Append("\">Change pic</a>");
					}
					else
					{
						str.Append(str.Length>0?"<br>":"");
						str.Append("<a href=\"");
						str.Append(this.Promoter.UrlApp("competitions","mode","pic","compk",this.K.ToString(),"eventk",EventK.ToString()));
						str.Append("\">Add pic</a>");
					}

					str.Append(str.Length>0?"<br>":"");
					str.Append("<a href=\"");
					str.Append(this.Promoter.UrlApp("competitions","mode","publish","compk",this.K.ToString(),"eventk",EventK.ToString()));
					str.Append("\">Publish</a>");
				}
			}
			return str.ToString();
		}

		#region Links to Bobs
		#region Owner
		public Usr Owner
		{
			get
			{
				if (owner==null)
					owner = new Usr(OwnerUsrK);
				return owner;
			}
			set
			{
				owner = value;
			}
		}
		private Usr owner;
		#endregion
		#endregion

		#region IPage Members

		public string Url(params string[] par)
		{
			//string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] {"K",this.K.ToString()}, par);
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] {this.K.ToString(),""}, par);
			return UrlInfo.PageUrl("competitions",fullParams);
		}

		#endregion

		#region IPic Members

		public bool HasPic
		{
			get
			{
				return !Pic.Equals(Guid.Empty);
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

		#region AnyPic
		public bool HasAnyPic
		{
			get
			{
				return !AnyPic.Equals(Guid.Empty);
			}
		}
		public Guid AnyPic
		{
			get
			{
				if (HasPic)
					return Pic;
				else if (EventK>0 && Event.HasPic)
					return Event.Pic;
				else if (BrandK>0 && Brand.HasPic)
					return Brand.Pic;
				else
					return Guid.Empty;
			}
		}
		public string AnyPicPath
		{
			get
			{
				if (HasAnyPic)
					return Storage.Path(AnyPic);
				else
					return "/gfx/dsi-sign-100.png";
			}
		}
		#endregion

		#region IArchive Members

		public DateTime ArchiveDateTime
		{
			get
			{
				return this.DateTimeClose;
			}
		}

		public string ArchiveHtml(bool showCountry, bool showPlace, bool showVenue, bool showEvent, int size)
		{

			string rolloverHtml = "<div style=\"width:250px;\"><b>"+this.NameCaps+"</b><br>";
			rolloverHtml += "</div>";
			rolloverHtml = HttpUtility.UrlEncodeUnicode(rolloverHtml).Replace("'","\\'");
			string attribs = " onmouseover=\"stt('"+rolloverHtml+"');\" onmouseout=\"htm();\"";

			return "<a href=\"" + this.Url() + "\"><img " + attribs + " src=\"" + this.AnyPicPath + "\" border=\"0\" class=\"ArchiveItem BorderBlack All\" width=\"" + size.ToString() + "\" height=\"" + size.ToString() + "\"></a>";
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


		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion

		#region IHasObjectType Members

		public Model.Entities.ObjectType ObjectType
		{
			get { return Model.Entities.ObjectType.Comp; }
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get { return "Comp"; }
		}

		#endregion
	}
	#endregion

	#region CompEntry
	/// <summary>
	/// Links a user to many photos (photos of me)
	/// </summary>
	[Serializable] 
	public partial class CompEntry 
	{

		#region simple members
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[CompEntry.Columns.UsrK]; }
			set { usr = null; this[CompEntry.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to the Comp table
		/// </summary>
		public override int CompK
		{
			get { return (int)this[CompEntry.Columns.CompK]; }
			set { comp = null; this[CompEntry.Columns.CompK] = value; }
		}
		/// <summary>
		/// Answer - 1,2 or 3
		/// </summary>
		public override int Answer
		{
			get { return (int)this[CompEntry.Columns.Answer]; }
			set { comp = null; this[CompEntry.Columns.Answer] = value; }
		}
		/// <summary>
		/// Is this entry a winner?
		/// </summary>
		public override bool Winner
		{
			get { return (bool)this[CompEntry.Columns.Winner]; }
			set { this[CompEntry.Columns.Winner] = value; }
		}
		/// <summary>
		/// What prize did they win? 1,2 or 3?
		/// </summary>
		public override int Prize
		{
			get { return (int)this[CompEntry.Columns.Prize]; }
			set { this[CompEntry.Columns.Prize] = value; }
		}
		/// <summary>
		/// Link to PM thread with owner
		/// </summary>
		public override int WinnerThreadK
		{
			get { return (int)this[CompEntry.Columns.WinnerThreadK]; }
			set { winnerThread = null; this[CompEntry.Columns.WinnerThreadK] = value; }
		}
		#endregion

		public string AnswerText
		{
			get
			{
				return (Answer==1?Comp.Answer1:(Answer==2?Comp.Answer2:Comp.Answer3));
			}
		}
		

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region Comp
		public Comp Comp
		{
			get
			{
				if (comp==null)
					comp = new Comp(CompK);
				return comp;
			}
		}
		Comp comp;
		#endregion
		#region WinnerThread
		public Thread WinnerThread
		{
			get
			{
				if (winnerThread==null && WinnerThreadK>0)
					winnerThread=new Thread(WinnerThreadK);
				return winnerThread;
			}
			set
			{
				winnerThread = value;
			}
		}
		private Thread winnerThread;
		#endregion
		#endregion
		
	}
	#endregion

}
