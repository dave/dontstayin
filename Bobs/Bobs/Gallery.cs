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

namespace Bobs
{

	#region Gallery
	[Serializable]
	public partial class Gallery : IBobType, IPic, IPage, IObjectPage, IRelevanceContributor, IArchive, IDeleteAll, IConnectedTo, ILinkable
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Gallery.Columns.K] as int? ?? 0; }
			set { this[Gallery.Columns.K] = value; }
		}
		/// <summary>
		/// The event that these photos were taken at
		/// </summary>
		public override int EventK
		{
			get { return (int)this[Gallery.Columns.EventK]; }
			set
			{
				_event = null; 
				this[Gallery.Columns.EventK] = value;
			}
		}
		/// <summary>
		/// Name
		/// </summary>
		public override string Name
		{
			get { return (string)this[Gallery.Columns.Name]; }
			set { this[Gallery.Columns.Name] = value; }
		}
		/// <summary>
		/// Link to the main photo - used for the title image.
		/// </summary>
		public override int MainPhotoK
		{
			get { return (int)this[Gallery.Columns.MainPhotoK]; }
			set { mainPhoto = null; this[Gallery.Columns.MainPhotoK] = value; }
		}
		/// <summary>
		/// Link to the Usr table.
		/// </summary>
		public override int OwnerUsrK
		{
			get { return (int)this[Gallery.Columns.OwnerUsrK]; }
			set { owner = null; this[Gallery.Columns.OwnerUsrK] = value; }
		}
		/// <summary>
		/// Total number of photos (live + disabled + new) in the gallery
		/// </summary>
		public override int TotalPhotos
		{
			get { return (int)this[Gallery.Columns.TotalPhotos]; }
			set { this[Gallery.Columns.TotalPhotos] = value; }
		}
		/// <summary>
		/// Total number of live photos in the gallery
		/// </summary>
		public override int LivePhotos
		{
			get { return (int)this[Gallery.Columns.LivePhotos]; }
			set { this[Gallery.Columns.LivePhotos] = value; }
		}
		/// <summary>
		/// DateTime when the Gallery was created
		/// </summary>
		public override DateTime CreateDateTime
		{
			get { return (DateTime)this[Gallery.Columns.CreateDateTime]; }
			set { this[Gallery.Columns.CreateDateTime] = value; }
		}
		/// <summary>
		/// DateTime when the last photo was made live
		/// </summary>
		public override DateTime LastLiveDateTime
		{
			get { return (DateTime)this[Gallery.Columns.LastLiveDateTime]; }
			set { this[Gallery.Columns.LastLiveDateTime] = value; }
		}
		/// <summary>
		/// Admin note only visible to photo admins
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Gallery.Columns.AdminNote]; }
			set { this[Gallery.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// If the gallery is in an article, this is the key
		/// </summary>
		public override int ArticleK
		{
			get { return (int)this[Gallery.Columns.ArticleK]; }
			set {
				this.Article = null; 
				this[Gallery.Columns.ArticleK] = value;
			}
		}
		/// <summary>
		/// Is this a mobile gallery (with mms photos)?
		/// </summary>
		public override bool IsMobile
		{
			get { return (bool)this[Gallery.Columns.IsMobile]; }
			set { this[Gallery.Columns.IsMobile] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Gallery.Columns.PicState]; }
			set { this[Gallery.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Gallery.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Gallery.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Gallery.Columns.PicMiscK]; }
			set { picMisc = null; this[Gallery.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// The url fragment - so that the url can be generated without accessing parent database records
		/// </summary>
		public override string UrlFragment
		{
			get { return (string)this[Gallery.Columns.UrlFragment]; }
			set { this[Gallery.Columns.UrlFragment] = value; }
		}
		/// <summary>
		/// The moderator assigned to this gallery
		/// </summary>
		public override int ModeratorUsrK
		{
			get { return (int)this[Gallery.Columns.ModeratorUsrK]; }
			set { moderatorUsr = null; this[Gallery.Columns.ModeratorUsrK] = value; }
		}
		/// <summary>
		/// The count of packages in the current upload
		/// </summary>
		public override int CurrentPackageCount
		{
			get { return (int)this[Gallery.Columns.CurrentPackageCount]; }
			set { this[Gallery.Columns.CurrentPackageCount] = value; }
		}
		/// <summary>
		/// When did the last package complete?
		/// </summary>
		public override DateTime LastPackageDateTime
		{
			get { return (DateTime)this[Gallery.Columns.LastPackageDateTime]; }
			set { this[Gallery.Columns.LastPackageDateTime] = value; }
		}
		/// <summary>
		/// What is the package index of the last package to be completed? (success or fail)
		/// </summary>
		public override int LastPackageIndex
		{
			get { return (int)this[Gallery.Columns.LastPackageIndex]; }
			set { this[Gallery.Columns.LastPackageIndex] = value; }
		}
		/// <summary>
		/// Is an upload in progress?
		/// </summary>
		public override bool UploadInProgress
		{
			get { return (bool)this[Gallery.Columns.UploadInProgress]; }
			set { this[Gallery.Columns.UploadInProgress] = value; }
		}
		/// <summary>
		/// How many times has a photo upload failed?
		/// </summary>
		public override int UploadFails
		{
			get { return (int)this[Gallery.Columns.UploadFails]; }
			set { this[Gallery.Columns.UploadFails] = value; }
		}
		/// <summary>
		/// Watch uploads for comments? (default = true)
		/// </summary>
		public override bool? WatchUploads
		{
			get { return (bool?)this[Gallery.Columns.WatchUploads]; }
			set { this[Gallery.Columns.WatchUploads] = value; }
		}
		/// <summary>
		/// Has the FinishedUploading task run on this gallery?
		/// </summary>
		public override bool? RunFinishedUploadingTask
		{
			get { return (bool?)this[Gallery.Columns.RunFinishedUploadingTask]; }
			set { this[Gallery.Columns.RunFinishedUploadingTask] = value; }
		}
		#endregion

		public string NameWithNewInfo
		{
			get
			{
				if (IsNew)
				{
					string newString = "NEW " + Name;

					if (this.JoinedGalleryUsr != null && this.JoinedGalleryUsr.ViewPhotosInUse > 0)
					{
						int newPhotos = this.LivePhotos - this.JoinedGalleryUsr.ViewPhotosInUse;
						newString += " (" + newPhotos.ToString() + " new photo" + (newPhotos == 1 ? "" : "s") + ")";
					}
					return newString;
				}
				else
				{
					return Name;
				}
			}
		}

		public void UpdateChildUrlFragments(bool Cascade)
		{ 
		}

		public int GetTotalPhotosIncludingProcessing()
		{
			Query q = new Query();
			q.ReturnCountOnly = true;
			q.QueryCondition = new Q(Photo.Columns.GalleryK, this.K);
			PhotoSet ps = new PhotoSet(q);
			return ps.Count;
		}

		public static void DailySendNewGalleryEmails()
		{
			DateTime StartDateTime = DateTime.Now;
			int UsrCount = 0;
			int FailCount = 0;
			try
			{
				Query q = new Query();
				if (Vars.DevEnv)
					q.TopRecords=100;
				q.TableElement = new TableElement(TablesEnum.Gallery);
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.UsrEventAttended),
					QueryJoinType.Inner,
					new And(
						new Q(Gallery.Columns.EventK, UsrEventAttended.Columns.EventK, true),
						new Q(UsrEventAttended.Columns.SendUpdate, true)
					)
				);
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(new Column(UsrEventAttended.Columns.UsrK, Usr.Columns.K)),
					QueryJoinType.Inner,
					new And(
						new Q(UsrEventAttended.Columns.UsrK, new Column(UsrEventAttended.Columns.UsrK, Usr.Columns.K), true),
						new Q(new Column(UsrEventAttended.Columns.UsrK, Usr.Columns.IsSkeleton),false),
						new Q(new Column(UsrEventAttended.Columns.UsrK, Usr.Columns.IsEmailVerified),true)
					)
				);
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.Event),
					QueryJoinType.Inner,
					new Q(Gallery.Columns.EventK, Event.Columns.K, true)
					);
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.GalleryUsr),
					QueryJoinType.Left,
					new And(
						new Q(Gallery.Columns.K, GalleryUsr.Columns.GalleryK, true),
						new Q(UsrEventAttended.Columns.UsrK, GalleryUsr.Columns.UsrK, true)
					)
				);
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.Usr),
					QueryJoinType.Inner,
					new Q(Gallery.Columns.OwnerUsrK, Usr.Columns.K, true)
					);
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.Photo),
					QueryJoinType.Left,
					new Q(Gallery.Columns.MainPhotoK, Photo.Columns.K, true)
					);
				q.Columns = new ColumnSet(
						Gallery.Columns.K,
						Gallery.Columns.Name,
						Gallery.Columns.UrlFragment,
						Gallery.Columns.ArticleK,
						Gallery.Columns.MainPhotoK,
						Gallery.Columns.LivePhotos,
						Gallery.Columns.CreateDateTime, 
						Gallery.Columns.EventK, 
						Gallery.Columns.OwnerUsrK,
						Photo.Columns.K,
						Photo.Columns.Icon,
						Photo.Columns.ContentDisabled,
						Photo.Columns.Status,
						Usr.LinkColumns,
						UsrEventAttended.Columns.UsrK
					);
				int daysPast = -3;
				if (Vars.DevEnv)
					daysPast = -30;
				q.QueryCondition=new And(
					new Q(Gallery.Columns.LastLiveDateTime,QueryOperator.GreaterThan,DateTime.Now.AddDays(daysPast)),
					Gallery.ShowOnSiteQ,
					new Or(
						new Q(GalleryUsr.Columns.ViewPhotosLatest,QueryOperator.IsNull,null),
						new Q(Gallery.Columns.LivePhotos,QueryOperator.GreaterThan,GalleryUsr.Columns.ViewPhotosLatest,true)
					)
				);
				q.OrderBy=new OrderBy(
					new OrderBy(UsrEventAttended.Columns.UsrK),
					new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending),
					new OrderBy(Event.Columns.K),
					new OrderBy(Gallery.Columns.LivePhotos, OrderBy.OrderDirection.Descending),
					new OrderBy(Gallery.Columns.K)
				);
				GallerySet gs = new GallerySet(q);
				Usr CurrentUsr = null;
				Event CurrentEvent = null;
				Mailer CurrentMail = null;
				int RowCount = 0;
				int CurrentGalleryCount = 0;
				for (int count=0; count<gs.Count; count++)
				{
					try
					{
						Gallery CurrentGallery = gs[count];

						if (CurrentUsr==null || CurrentGallery.JoinedUsrEventAttend.UsrK!=CurrentUsr.K)
						{
							if (CurrentMail!=null)
							{
								Console.WriteLine(CurrentUsr.Email+" - "+CurrentGalleryCount.ToString()+", IsSkeleton="+CurrentUsr.IsSkeleton+", IsEmailVerified="+CurrentUsr.IsEmailVerified);
								CurrentGalleryCount=0;

								CurrentMail.Body+="</tr></table>";
								CurrentMail.Send();
							}

							CurrentEvent = null;
							CurrentUsr = new Usr(CurrentGallery.JoinedUsrEventAttend.UsrK);
							UsrCount++;
							
					
							CurrentMail = new Mailer();
							CurrentMail.Subject="New DontStayIn galleries "+DateTime.Today.ToString("ddddd dd MMMM yyyy");
							CurrentMail.UsrRecipient = CurrentUsr;
							CurrentMail.Body="<p>Here are some galleries you might like to check out:</p>";
							CurrentMail.Bulk=true;

						}
						if (CurrentEvent==null || CurrentGallery.EventK!=CurrentEvent.K)
						{
							if (CurrentEvent!=null)
							{
								CurrentMail.Body+="</tr></table>";
							}
							CurrentEvent = new Event(CurrentGallery.EventK);
							CurrentMail.Body+="<p style=\"margin:15px 0px 1px 0px;\"><center><a href=\"[LOGIN("+CurrentEvent.Url()+")]\" style=\"line-height:21px;font-size:18px;font-weight:bold;\">"+CurrentEvent.Name+"</a></center></p>";
							CurrentMail.Body+="<div style=\"margin:0px 0px 3px 0px;\"><center><small><a href=\"[LOGIN("+CurrentEvent.Venue.Url()+")]\">"+CurrentEvent.Venue.Name+"</a>, "+CurrentEvent.FriendlyDate(false)+", <a href=\"[LOGIN("+CurrentEvent.Url("ignore","")+")]\">click to ignore new galleries</a></small></center></div>";
							RowCount = 0;
							CurrentMail.Body+="<table cellspacing=\"0\" cellpadding=\"7\" width=\"100%\" style=\"margin:0px 0px 3px 0px;\"><tr>";
						}
						if (RowCount==3)
						{
							RowCount = 0;
							CurrentMail.Body+="</tr><tr>";
						}
						CurrentMail.Body += "<td align=\"center\" valign=\"top\" width=\"33%\"><a href=\"[LOGIN(" + CurrentGallery.Url() + ")]\"><img src=\"" + CurrentGallery.PicPathAbsolute + "\" width=\"100\" height=\"100\" class=\"BorderBlack All\" border=\"0\"><div style=\"padding:5px 0px 0px 0px;\">" + CurrentGallery.NameSafe + "</div></a><div style=\"padding:5px 0px 0px 0px;\"><small>" + CurrentGallery.LivePhotos.ToString("#,##0") + " photo" + (CurrentGallery.LivePhotos == 1 ? "" : "s") + ". Added by <a href=\"[LOGIN(" + CurrentGallery.Owner.Url() + ")]\">" + CurrentGallery.Owner.NickName + "</a></small></div></td>";
						RowCount++;
						CurrentGalleryCount++;
					}
					catch
					{
						FailCount++;
						CurrentUsr = null;
						CurrentEvent = null;
						CurrentMail = null;
					}
					gs.Kill(count);
				}
			}
			finally
			{
				string summary = "<p>Started: "+StartDateTime.ToLongTimeString()+"</p>";
				summary += "<p>Ending: "+DateTime.Now.ToLongTimeString()+"</p>";
				TimeSpan timeTaken = (DateTime.Now - StartDateTime);
				summary += "<p>Total time: "+timeTaken.TotalMinutes.ToString("0.##")+" min</p>";
				summary += "<p><b>Users with unseen galleries: "+UsrCount.ToString("#,##0")+"</b></p>";
				summary += "<p><b>Exceptions: "+FailCount.ToString("#,##0")+"</b></p>";

				Mailer smAdmin = new Mailer();
				smAdmin.TemplateType=Mailer.TemplateTypes.AdminNote;

				smAdmin.Body += "<h1>Summary</h1>";
				smAdmin.Body += summary;
			
				smAdmin.Subject="Gallery email sent "+DateTime.Now.ToString();
				smAdmin.To="d.brophy@dontstayin.com";
				smAdmin.Send();
			}
			
		}
		#region JoinedUsrEventAttend
		public UsrEventAttended JoinedUsrEventAttend
		{
			get
			{
				if (joinedUsrEventAttend==null)
				{
					joinedUsrEventAttend = new UsrEventAttended(this, Gallery.Columns.EventK);
				}
				return joinedUsrEventAttend;
			}
			set
			{
				joinedUsrEventAttend = value;
			}
		}
		private UsrEventAttended joinedUsrEventAttend;
		#endregion

		#region JoinedGalleryUsr
		public GalleryUsr JoinedGalleryUsr
		{
			get
			{
				if (joinedGalleryUsr==null)
				{
					joinedGalleryUsr = new GalleryUsr(this, Gallery.Columns.K);
				}
				return joinedGalleryUsr;
			}
			set
			{
				joinedGalleryUsr = value;
			}
		}
		private GalleryUsr joinedGalleryUsr;
		#endregion
		#region IsNew
		public bool IsNew
		{
			get
			{
				if (Usr.Current==null)
					return false;
				if (this.JoinedGalleryUsr!=null)
				{
					if (this.JoinedGalleryUsr.UsrK==0)
						return true;
					else
						return this.LivePhotos>this.JoinedGalleryUsr.ViewPhotosInUse;
				}
				throw new Exception("can't find JoinedGalleryUsr!");
			}
		}
		#endregion
		public static OrderBy Order
		{
			get
			{
				return new OrderBy(
					new OrderBy(new Column(Gallery.Columns.OwnerUsrK,Usr.Columns.IsProSpotter),OrderBy.OrderDirection.Descending),
					new OrderBy(new Column(Gallery.Columns.OwnerUsrK,Usr.Columns.SpottingsTotal),OrderBy.OrderDirection.Descending),
					new OrderBy(new Column(Gallery.Columns.OwnerUsrK,Usr.Columns.K)),
					new OrderBy(Gallery.Columns.K));
			}
		}
		public static Q ShowOnSiteQ
		{
			get
			{
				return new And(
					new Q(Gallery.Columns.LivePhotos,QueryOperator.GreaterThan,0),
					new Or(
						new Q(Gallery.Columns.UploadInProgress, false),
						new Q(Gallery.Columns.UploadInProgress, QueryOperator.IsNull, null),
						new Q(Gallery.Columns.LastPackageDateTime, QueryOperator.LessThan, DateTime.Now.AddMinutes(-10))));
			}
		}
		#region SetGalleryUsr
		public void SetGalleryUsr(int numberOfPhotosViewed)
		{
			if (Usr.Current != null)
			{
				GalleryUsr galleryUsr = GetGalleryUsr(Usr.Current);
				galleryUsr.Set(numberOfPhotosViewed);
			}
		}
		#endregion
		#region GetGalleryUsr
		/// <summary>
		/// Returns the current (or creates a new) GalleryUsr for the current gtallery and the logged in usr.
		/// </summary>
		public GalleryUsr GetGalleryUsr(Usr u)
		{
			GalleryUsr galleryUsr = null;
			if (u!=null)
			{
				try
				{
					galleryUsr = new GalleryUsr(this.K,u.K);
				}
				catch (BobNotFound)
				{
					try
					{
						galleryUsr = new GalleryUsr();
						galleryUsr.IsNew = true;
						galleryUsr.GalleryK = this.K;
						galleryUsr.UsrK = u.K;
						galleryUsr.Update();
					}
					catch
					{
						try
						{
							galleryUsr = new GalleryUsr(this.K,u.K);
						}
						catch
						{
							
						}
					}
				}
				if (galleryUsr==null)
					throw new Exception("Still can't get GalleryUsr after silly complex mess!");
			}
			return galleryUsr;
		}
		#endregion

		#region UpdatePhotoOrder
		public void UpdatePhotoOrder(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition=new And(
				Photo.EnabledQueryCondition,
				new Q(Photo.Columns.GalleryK,this.K)
				);
			q.OrderBy=Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
			q.NoLock=true;
			q.Columns=new ColumnSet(
				Photo.Columns.K, 
				Photo.Columns.ContentDisabled,
				Photo.Columns.NextPhoto1K,
				Photo.Columns.NextPhoto2K,
				Photo.Columns.NextPhoto3K,
				Photo.Columns.PreviousPhoto1K,
				Photo.Columns.PreviousPhoto2K,
				Photo.Columns.PreviousPhoto3K,
				Photo.Columns.GalleryTimeOrder
				);
			PhotoSet ps = new PhotoSet(q);
			#region declare backup vars for compare before update
			int bN1K = 0;
			int bN2K = 0;
			int bN3K = 0;
			int bP1K = 0;
			int bP2K = 0;
			int bP3K = 0;
			Guid bN1I = Guid.Empty;
			Guid bN2I = Guid.Empty;
			Guid bN3I = Guid.Empty;
			Guid bP1I = Guid.Empty;
			Guid bP2I = Guid.Empty;
			Guid bP3I = Guid.Empty;
			int bO = 0;
			#endregion
			for(int currentIndex=0;currentIndex<ps.Count;currentIndex++)
			{
				#region store backup vals for compare before update
				bN1K = ps[currentIndex].NextPhoto1K;
				bN2K = ps[currentIndex].NextPhoto2K;
				bN3K = ps[currentIndex].NextPhoto3K;
				bP1K = ps[currentIndex].PreviousPhoto1K;
				bP2K = ps[currentIndex].PreviousPhoto2K;
				bP3K = ps[currentIndex].PreviousPhoto3K;
				bO = ps[currentIndex].GalleryTimeOrder;
				#endregion
				ps[currentIndex].GalleryTimeOrder=currentIndex;
				for (int offset=1; offset<=3; offset++)
				{
					if (currentIndex-offset<0)
					{
						if (ps.Count+currentIndex-offset>=ps.Count || ps.Count+currentIndex-offset<0)
							ps[currentIndex].SetNextPrevCache(0,offset,false);
						else
							ps[currentIndex].SetNextPrevCache(ps[ps.Count+currentIndex-offset].K,offset,false);
					}
					else
						ps[currentIndex].SetNextPrevCache(ps[currentIndex-offset].K,offset,false);

					if (currentIndex+offset>=ps.Count)
					{
						if (offset-ps.Count+currentIndex>=ps.Count || offset-ps.Count+currentIndex<0)
							ps[currentIndex].SetNextPrevCache(0,offset,true);
						else
							ps[currentIndex].SetNextPrevCache(ps[offset-ps.Count+currentIndex].K,offset,true);
					}
					else
						ps[currentIndex].SetNextPrevCache(ps[currentIndex+offset].K,offset,true);
				}
				#region compare, then update if changed
				if (
					bN1K == 0 || bN1K != ps[currentIndex].NextPhoto1K ||
					bN2K == 0 || bN2K != ps[currentIndex].NextPhoto2K ||
					bN3K == 0 || bN3K != ps[currentIndex].NextPhoto3K ||
					bP1K == 0 || bP1K != ps[currentIndex].PreviousPhoto1K ||
					bP2K == 0 || bP2K != ps[currentIndex].PreviousPhoto2K ||
					bP3K == 0 || bP3K != ps[currentIndex].PreviousPhoto3K ||
					bO == 0 || bO != ps[currentIndex].GalleryTimeOrder)
				{
					ps[currentIndex].Update(transaction);
				}
				#endregion
			}
		}
		#endregion

		#region Rollover

		string mouseOverText
		{
			get
			{
				return "stma('"+MainPhoto.Icon.ToString()+"');";
			}
		}

		public string Rollover
		{
			get
			{
				if (HasPic)
					return "onmouseover=\""+mouseOverText+"\" onmouseout=\"htm();\"";
				else
					return "";
			}
		}

		public void MakeRollover(HtmlControl c)
		{
			if (HasPic)
			{
				c.Attributes["onmouseover"]=mouseOverText;
				c.Attributes["onmouseout"]="htm();";
			}
		}
		public void MakeRollover(WebControl c)
		{
			if (HasPic)
			{
				c.Attributes["onmouseover"]=mouseOverText;
				c.Attributes["onmouseout"]="htm();";
			}
		}
		#endregion

		#region OwnerJoin
		public static Join OwnerJoin
		{
			get
			{
				return new Join(Gallery.Columns.OwnerUsrK,Usr.Columns.K);
			}
		}
		public static Join EventVenueJoin
		{
			get
			{
				return new Join(new TableElement(TablesEnum.Gallery),Event.VenueAllJoin,QueryJoinType.Inner,Gallery.Columns.EventK,Event.Columns.K);
			}
		}
		public static Join EventVenuePlaceJoin
		{
			get
			{
				return new Join(new Join(new TableElement(TablesEnum.Gallery),Event.VenueAllJoin,QueryJoinType.Inner,Gallery.Columns.EventK,Event.Columns.K),Place.Columns.K,Venue.Columns.PlaceK);
			}
		}
		#endregion

		#region AddRelevant
		public void AddRelevant(IRelevanceHolder ContainerPage)
		{
			if (this.Event != null)
			{
				this.Event.AddRelevant(ContainerPage);
			}
			else if (this.Article != null)
			{
				this.Article.AddRelevant(ContainerPage);
			}
		}
		#endregion

		#region IsConnectedTo(ObjectType objectType, int objectK)
		public bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK)
		{
			if (objectType.Equals(Model.Entities.ObjectType.Gallery) && this.K == objectK)
				return true;

			if (this.EventK > 0)
			{
				if (objectType.Equals(Model.Entities.ObjectType.Event) && this.EventK == objectK)
					return true;

				if (Event.CanBeConnectedToStatic(objectType) && this.Event.IsConnectedTo(objectType, objectK))
					return true;
			}
			else if (this.ArticleK > 0)
			{
				if (objectType.Equals(Model.Entities.ObjectType.Article) && this.ArticleK == objectK)
					return true;

				if (Article.CanBeConnectedToStatic(objectType) && this.Article.IsConnectedTo(objectType, objectK))
					return true;
			}

			return false;
		}
		public static bool CanBeConnectedToStatic(Model.Entities.ObjectType o)
		{
			if (o.Equals(Model.Entities.ObjectType.Event) ||
				o.Equals(Model.Entities.ObjectType.Article))
				return true;

			if (Event.CanBeConnectedToStatic(o))
				return true;

			if (Article.CanBeConnectedToStatic(o))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Gallery.CanBeConnectedToStatic(o);
		}
		#endregion

		#region ParentObjectHtml
		public string ParentObjectHtml(bool capital) { return ParentObjectHtml(capital, true); }
		public string ParentObjectHtml(bool capital, bool small)
		{
			if (this.Event != null)
				return (capital ? "E" : "e") + "vent: <a href=\"" + this.Event.Url() + "\">" + this.Event.Name + "</a> " + (small ? "<small>" : "") + "@ <a href=\"" + this.Event.Venue.Url() + "\">" + this.Event.Venue.Name + "</a>, " + this.Event.FriendlyDate(false) + (small ? "</small>" : "");
			else if (this.Article != null)
				return (capital ? "A" : "a") + "rticle: <a href=\"" + this.Article.Url() + "\">" + this.Article.Title + "</a>";
			else
				return "";
		}
		#endregion

		#region SendNewGalleryEmails
		public void SendNewGalleryEmails()
		{
			//Send emails...
			if (this.Event!=null)
			{
				Query q = new Query();
				q.QueryCondition=new And(
					Usr.IsDisplayedInUsrLists, 
					new Q(Event.Columns.K,this.Event.K));
				q.TableElement=Usr.EventAttendedJoin;
				q.OrderBy=new OrderBy(Usr.Columns.DateTimeSignUp);
				q.NoLock=true;
				q.Columns=Usr.EmailColumns;
				UsrSet usrAttended = new UsrSet(q);

				foreach (Usr u in usrAttended)
				{
					Mailer m = new Mailer();
					m.Subject="New photo gallery from "+this.Event.FriendlyName;
					m.Body+="<p>"+this.Owner.NickNameSafe+" has uploaded new photos from "+this.Event.Name+". The gallery is called <i>"+HttpUtility.HtmlEncode(this.Name)+"</i> - click the link below to check it out.</p>";
					m.Body+="<p>If you see yourself, click 'I've been spotted!' under your photo.</p>";
					m.RedirectUrl=this.Url();
					m.TemplateType=Mailer.TemplateTypes.AnotherSiteUser;
					m.To=u.Email;
					m.UsrRecipient=u;
					m.Bulk=true;
					m.Send();
				}
			}
		}
		#endregion

		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			PhotoSet ps = new PhotoSet(new Query(new Q(Photo.Columns.GalleryK,this.K)));
			foreach (Photo p in ps)
				p.DeleteAll(transaction);

			this.Delete(transaction);
		}

		public void UpdateStats(Transaction transaction, bool update)
		{
			Query qAll = new Query();
			qAll.QueryCondition=new And(new Q(Photo.Columns.GalleryK,K),new Q(Photo.Columns.Status, QueryOperator.NotEqualTo, Photo.StatusEnum.Processing));
			qAll.ReturnCountOnly=true;
			PhotoSet psAll = new PhotoSet(qAll);
			this.TotalPhotos = psAll.Count;

			Query qEnabled = new Query();
			qEnabled.QueryCondition=new And(new Q(Photo.Columns.GalleryK,K),Photo.EnabledQueryCondition);
			qEnabled.ReturnCountOnly=true;
			PhotoSet psEnabled = new PhotoSet(qEnabled);
			this.LivePhotos = psEnabled.Count;

			if (this.TotalPhotos > this.LivePhotos)
				this.ModeratorUsrK = Usr.GetPhotoModeratorUsrK();
			
			if (update)
				this.Update(transaction);

		}

		public bool ShowOnSite
		{
			get
			{
				return LivePhotos>0;
			}
		}

		public string NameSafe
		{
			get{return HttpUtility.HtmlEncode(Name);}
		}


		#region Links to Bobs
		#region Event
		public Event Event
		{
			get
			{
				if (_event==null && EventK>0)
					_event = new Event(EventK,this,Gallery.Columns.EventK);
				return _event;
			}
			set
			{
				_event = value;
			}
		}
		private Event _event;
		#endregion
		#region Owner
		public Usr Owner
		{
			get
			{
				if (owner==null)
					owner = new Usr(OwnerUsrK,this,Gallery.Columns.OwnerUsrK);
				return owner;
			}
			set
			{
				owner = value;
			}
		}
		private Usr owner;
		#endregion
		#region MainPhoto
		public Photo MainPhoto
		{
			get
			{		
				if (mainPhoto==null)
				{
					try
					{
						mainPhoto = new Photo(MainPhotoK, this, Gallery.Columns.MainPhotoK);
					}
					catch
					{
						MainPhotoK = 0;
						//Dodgy MainPhotoK... return null?
					}
				}
				return mainPhoto;
			}
			set
			{
				mainPhoto = value;
			}
		}
		private Photo mainPhoto;
		#endregion
		#region Article
		public Article Article
		{
			get
			{
				if (article==null && ArticleK>0)
					article = new Article(ArticleK,this,Gallery.Columns.ArticleK);
				return article;
			}
			set
			{
				article = value;
			}
		}
		private Article article;
		#endregion
		#region ModeratorUsr
		public Usr ModeratorUsr
		{
			get
			{
				if (moderatorUsr==null && ModeratorUsrK>0)
					moderatorUsr = new Usr(ModeratorUsrK);
				return moderatorUsr;
			}
			set
			{
				moderatorUsr = value;
			}
		}
		private Usr moderatorUsr;
		#endregion
		#endregion

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "Gallery";
			}
		}
		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Gallery;
			}
		}
		#endregion

		#region IPic Members

		public Guid Pic
		{
			get
			{
				if (HasPic)
					return MainPhoto.Icon;
				else
					return Photo.ContentDisabledThumb;
			}
			set
			{
				throw new Exception("Shouldn't set this!");
			}
		}

		public bool HasPic
		{
			get
			{
				return (MainPhotoK>0 && MainPhoto!=null && MainPhoto.Status.Equals(Photo.StatusEnum.Enabled));
			}
		}

		public string PicPath
		{
			get
			{
				if (HasPic)
					return MainPhoto.IconPath;
				else
					return "/gfx/dsi-sign-100.png";
			}
		}

		public string PicPathAbsolute
		{
			get
			{
				if (HasPic)
					return MainPhoto.IconPath;
				else
					return Vars.UrlScheme + "://www.dontstayin.com/gfx/dsi-sign-100.png";
			}
		}

		#endregion

		#region IPage Members
		public void UpdateUrlFragmentNoUpdate()
		{
			UrlFragment = GetUrlFragment();
		}
		public string GetUrlFragment()
		{
			if (this.Event != null)
				return this.Event.UrlFilterPartVenueDate;
			else if (this.Article != null)
				return this.Article.UrlFilterPart;
			else
				return "";
		}
		public string UrlFilterPart
		{
			get
			{
				string urlFragment = "";
				if (Common.Settings.DynamicUrlFragments)
					urlFragment = GetUrlFragment();
				else
					urlFragment = UrlFragment;

				if (urlFragment.Length > 0)
					return urlFragment + "/gallery-" + this.K.ToString();
				else
					return "gallery-" + this.K.ToString();
			}
		}
		public string Url(params string[] par)
		{
			//if (this.HasPic)
			//{
			//    bool hasPhotoK = false;
			//    foreach (string s in par)
			//    {
			//        if (s.ToLower().Equals("photok"))
			//        {
			//            hasPhotoK = true;
			//            break;
			//        }
			//    }
			//    if (!hasPhotoK)
			//        par = Cambro.Misc.Utility.JoinStringArrays(new string[] {"photok",this.MainPhotoK.ToString()}, par);
			//}

			return UrlInfo.MakeUrl(UrlFilterPart, null, par);
		}
		public string UrlNoSkip(params string[] par)
		{	
			return UrlInfo.MakeUrl(UrlFilterPart, null, par);
		}
		public string UrlApp(string Application, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, Application, par);
		}
		#endregion

		public string PagedUrl(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, "paged", par);
		}

		#region IArchive Members

		public DateTime ArchiveDateTime
		{
			get
			{
				return this.CreateDateTime;
			}
		}

		public string ArchiveHtml(bool showCountry, bool showPlace, bool showVenue, bool showEvent, int size)
		{
			string rolloverHtml = "<div style=\"width:250px;\"><b>"+this.Name+"</b><br>";
			if (this.Event!=null)
			{
				if (showEvent)
					rolloverHtml += "From "+this.Event.Name;
				if (showVenue)
					rolloverHtml += " @ "+this.Event.Venue.Name;
				if (showPlace)
					rolloverHtml += " in "+this.Event.Venue.Place.NamePlainRegion;
				if (showCountry && Country.FilterK!=this.Event.Venue.Place.CountryK)
					rolloverHtml += " ("+this.Event.Venue.Place.Country.FriendlyName+")";
				
				rolloverHtml += ", "+this.Event.FriendlyDate(false);
			}
			rolloverHtml += "<br><b>"+this.LivePhotos+" photo"+(this.LivePhotos==1?"":"s")+"</b>";
			rolloverHtml += "</div>";
			rolloverHtml = HttpUtility.UrlEncodeUnicode(rolloverHtml).Replace("'","\\'");
			string attribs = " onmouseover=\"stt('"+rolloverHtml+"');\" onmouseout=\"htm();\"";

			return "<a href=\"" + this.Url() + "\"><img " + attribs + " src=\"" + this.PicPath + "\" border=\"0\" class=\"ArchiveItem BorderBlack All\" width=\"" + size.ToString() + "\" height=\"" + size.ToString() + "\"></a>";
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



		public string Link(params string[] par)
		{
			return ILinkableExtentions.Link(this, par);
		}

		public string LinkNewWindow(params string[] par)
		{
			return ILinkableExtentions.LinkNewWindow(this, par);
		}

		public string ReadableReference
		{
			get { return Name; }
		}

		 

		 
	}
	#endregion

	#region GalleryUsr
	/// <summary>
	/// Links a private thread to many users
	/// </summary>
	[Serializable] 
	public partial class GalleryUsr
	{

		#region simple members
		/// <summary>
		/// The thread
		/// </summary>
		public override int GalleryK
		{
			get { return (int)this[GalleryUsr.Columns.GalleryK]; }
			set { gallery = null; this[GalleryUsr.Columns.GalleryK] = value; }
		}
		/// <summary>
		/// The user that has been invited
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[GalleryUsr.Columns.UsrK]; }
			set { usr = null; this[GalleryUsr.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The datetime that the gallery was last viewed
		/// </summary>
		public override DateTime ViewDateTime
		{
			get { return (DateTime)this[GalleryUsr.Columns.ViewDateTime]; }
			set { this[GalleryUsr.Columns.ViewDateTime] = value; }
		}
		/// <summary>
		/// The new datetime (when this is set, it's value is copied to the DateTime if it is more than 5 mins ago) 
		/// </summary>
		public override DateTime ViewDateTimeLatest
		{
			get { return (DateTime)this[GalleryUsr.Columns.ViewDateTimeLatest]; }
			set { this[GalleryUsr.Columns.ViewDateTimeLatest] = value; }
		}
		/// <summary>
		/// The number of photos that have been viewed at the time of the ViewDateTime
		/// </summary>
		public override int ViewPhotos
		{
			get { return (int)this[GalleryUsr.Columns.ViewPhotos]; }
			set { this[GalleryUsr.Columns.ViewPhotos] = value; }
		}
		/// <summary>
		/// The number of photos that have been viewed at the time of the ViewDateTimeLatest
		/// </summary>
		public override int ViewPhotosLatest
		{
			get { return (int)this[GalleryUsr.Columns.ViewPhotosLatest]; }
			set { this[GalleryUsr.Columns.ViewPhotosLatest] = value; }
		}
		/// <summary>
		/// Favourite gallery?
		/// </summary>
		public override bool Favourite
		{
			get { return (bool)this[GalleryUsr.Columns.Favourite]; }
			set { this[GalleryUsr.Columns.Favourite] = value; }
		}
		#endregion

		#region IsNew
		/// <summary>
		/// This is set to true by GetGalleryUsr() when we've created a new 
		/// GalleryUsr. It's not persisted in the database.
		/// </summary>
		public bool IsNew
		{
			get
			{
				return isNew;
			}
			set
			{
				isNew = value;
			}
		}
		private bool isNew = false;
		#endregion

		#region JoinedBuddy
		public Buddy JoinedBuddy
		{
			get
			{
				if (joinedBuddy==null)
				{
					joinedBuddy = new Buddy(this, GalleryUsr.Columns.UsrK);
				}
				return joinedBuddy;
			}
			set
			{
				joinedBuddy = value;
			}
		}
		private Buddy joinedBuddy;
		#endregion

		#region Set
		/// <summary>
		/// Use this to set the ViewDateTime(s).
		/// </summary>
		public void Set(int numberOfPhotosViewed)
		{
			if (ViewDateTimeLatest.AddMinutes(5) < DateTime.Now)
			{
				ViewDateTime = ViewDateTimeLatest;
				ViewPhotos = ViewPhotosLatest;
			}

			ViewDateTimeLatest = DateTime.Now;

			if (numberOfPhotosViewed > ViewPhotosLatest)
				ViewPhotosLatest = numberOfPhotosViewed;

			this.Update();
		}
		#endregion
		#region ViewDateTimeInUse
		/// <summary>
		/// This is the DateTime to use.
		/// </summary>
		public DateTime ViewDateTimeInUse
		{
			get
			{
				if (ViewDateTimeLatest < DateTime.Now && IsInitialised)
					return ViewDateTimeLatest;
				else
					return ViewDateTime;
			}
		}
		#endregion
		#region ViewPhotosInUse
		/// <summary>
		/// This is the number of photos to use.
		/// </summary>
		public int ViewPhotosInUse
		{
			get
			{
				//if (ViewDateTimeLatest.AddMinutes(5)<DateTime.Now)
				if (ViewDateTimeLatest < DateTime.Now)
					return ViewPhotosLatest;
				else if (IsInitialised)
					return ViewPhotos;
				else
					return 0;
			}
		}
		#endregion
		#region IsInitialised
		public bool IsInitialised
		{
			get
			{
				return !this.ViewDateTime.Equals(DateTime.MinValue);
			}
		}
		#endregion

		#region Links to Bobs
		#region Thread
		/// <summary>
		/// The thread
		/// </summary>
		public Gallery Gallery
		{
			get
			{
				if (gallery==null)
					gallery = new Gallery(GalleryK);
				return gallery;
			}
		}
		Gallery gallery;
		#endregion

		#region Usr
		/// <summary>
		/// The user that has been invited
		/// </summary>
		public Usr Usr
		{
			get
			{
				if (usr==null && UsrK>0)
					usr = new Usr(UsrK, this, GalleryUsr.Columns.UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion

		#endregion

	}
	#endregion

}
