using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Collections.Generic;
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

	#region Misc
	[Serializable] 
	public partial class Misc : IDeleteAll, IBobType
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Misc.Columns.K] as int? ?? 0; }
			set { this[Misc.Columns.K] = value; }
		}
		/// <summary>
		/// The name of the file stored on the fileserver
		/// </summary>
		public override Guid Guid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Misc.Columns.Guid]); }
			set { this[Misc.Columns.Guid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// File extention
		/// </summary>
		public override string Extention
		{
			get { return (string)this[Misc.Columns.Extention]; }
			set { this[Misc.Columns.Extention] = value; }
		}
		/// <summary>
		/// The owner of the file
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Misc.Columns.UsrK]; }
			set { usr = null; this[Misc.Columns.UsrK] = value; }
		}
		/// <summary>
		/// If this file was uploaded by a promoter, this is the promoter account
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Misc.Columns.PromoterK]; }
			set { promoter = null; this[Misc.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// File size in bytes
		/// </summary>
		public override int Size
		{
			get { return (int)this[Misc.Columns.Size]; }
			set { this[Misc.Columns.Size] = value; }
		}
		/// <summary>
		/// DateTime the file was added
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[Misc.Columns.DateTime]; }
			set { this[Misc.Columns.DateTime] = value; }
		}
		/// <summary>
		/// DateTime the file expires (for /popup/misc/x retreiver)
		/// </summary>
		public override DateTime DateTimeExpires
		{
			get { return (DateTime)this[Misc.Columns.DateTimeExpires]; }
			set { this[Misc.Columns.DateTimeExpires] = value; }
		}
		/// <summary>
		/// A user can store files in folders
		/// </summary>
		public override string Folder
		{
			get { return (string)this[Misc.Columns.Folder]; }
			set { this[Misc.Columns.Folder] = value; }
		}
		/// <summary>
		/// The original name of the file
		/// </summary>
		public override string Name
		{
			get { return (string)this[Misc.Columns.Name]; }
			set { this[Misc.Columns.Name] = value; }
		}
		/// <summary>
		/// A note that is added to the file - editable and readable by the user that uploaded the file
		/// </summary>
		public override string Note
		{
			get { return (string)this[Misc.Columns.Note]; }
			set { this[Misc.Columns.Note] = value; }
		}
		/// <summary>
		/// Xml meta data - e.g. banner admin verification data
		/// </summary>
		public override string Xml
		{
			get { return (string)this[Misc.Columns.Xml]; }
			set { this[Misc.Columns.Xml] = value; }
		}
		/// <summary>
		/// Is this file waiting for admin to authorise it?
		/// </summary>
		public override bool NeedsAuth
		{
			get { return (bool)this[Misc.Columns.NeedsAuth]; }
			set { this[Misc.Columns.NeedsAuth] = value; }
		}
		/// <summary>
		/// Does the flash banner use the LinkTag arguement to link to a URL?
		/// </summary>
		public override bool BannerLinkTag
		{
			get { return (bool)this[Misc.Columns.BannerLinkTag]; }
			set { this[Misc.Columns.BannerLinkTag] = value; }
		}
		/// <summary>
		/// Does the flash banner use the LinkTarget as the target when linking?
		/// </summary>
		public override bool BannerTargetTag
		{
			get { return (bool)this[Misc.Columns.BannerTargetTag]; }
			set { this[Misc.Columns.BannerTargetTag] = value; }
		}
		/// <summary>
		/// Width in pixels if it's an image
		/// </summary>
		public override int Width
		{
			get { return (int)this[Misc.Columns.Width]; }
			set { this[Misc.Columns.Width] = value; }
		}
		/// <summary>
		/// Height in pixels if it's an image
		/// </summary>
		public override int Height
		{
			get { return (int)this[Misc.Columns.Height]; }
			set { this[Misc.Columns.Height] = value; }
		}
		/// <summary>
		/// Override the required flash version for SWF's
		/// </summary>
		public override string RequiredFlashVersion
		{
			get { return (string)this[Misc.Columns.RequiredFlashVersion]; }
			set { this[Misc.Columns.RequiredFlashVersion] = value; }
		}
		/// <summary>
		/// Manual flag that admin may set to disable banner artwork
		/// </summary>
		public override bool BannerBroken
		{
			get { return (bool)this[Misc.Columns.BannerBroken]; }
			set { this[Misc.Columns.BannerBroken] = value; }
		}
		/// <summary>
		/// String entered by admin to communicate to the promoter why the banner is broken
		/// </summary>
		public override string BannerBrokenReason
		{
			get { return (string)this[Misc.Columns.BannerBrokenReason]; }
			set { this[Misc.Columns.BannerBrokenReason] = value; }
		}
		#endregion

		#region FullNameWithK
		public string FullNameWithK
		{
			get
			{
				return FullName + " (" + K.ToString() + ")";
			}
		}
		#endregion

		#region DisplayType
		public Banner.DisplayTypes DisplayType
		{
			get
			{
				if (this.Extention.Equals("swf"))
					return Banner.DisplayTypes.FlashMovie;
				else if (this.Extention.Equals("gif"))
					return Banner.DisplayTypes.AnimatedGif;
				else if (this.Extention.Equals("jpg"))
					return Banner.DisplayTypes.Jpg;
				else
					throw new Exception();
			}
		}
		#endregion
		#region CanUseAsBannerReturn
		public class CanUseAsBannerReturn
		{
			public CanUseAsBannerReturn() { }
			public ArrayList Errors = new ArrayList();
			public bool CanUseNow;
			public bool CanUseAfterAdminCheck;
			public bool LinkTagWarning;
			public string BrokenError;
		}
		#endregion
		#region CanUseAsBanner
		public CanUseAsBannerReturn CanUseAsBanner(Banner.Positions position)
		{
			CanUseAsBannerReturn ret = new CanUseAsBannerReturn();
			ret.CanUseNow = true;
			ret.CanUseAfterAdminCheck = false;
			ret.LinkTagWarning = false;

			if (position.Equals(Banner.Positions.Leaderboard))
			{
				#region Leaderboard specs
				if (this.Extention.Equals("jpg"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					if (this.Height != 90 || this.Width != 728)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 728x90 pixels");
					}
				}
				else if (this.Extention.Equals("gif"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					if (this.Height != 90 || this.Width != 728)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 728x90 pixels");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
				}
				else if (this.Extention.Equals("swf"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
					else
					{
						if (!this.BannerLinkTag)
						{
							ret.LinkTagWarning = true;
						}
						if (this.BannerBroken)
						{
							ret.CanUseNow = false;
							ret.Errors.Add("One of our admins failed the banner");
							ret.BrokenError = this.BannerBrokenReason;
						}
					}
				}
				#endregion
			}
			else if (position.Equals(Banner.Positions.Hotbox))
			{
				#region Hotbox specs
				if (this.Extention.Equals("jpg"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					if ((this.Height != 191 && this.Height != 250) || (this.Width != 191 && this.Width != 300))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 300x250 pixels");
					}
				}
				else if (this.Extention.Equals("gif"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					if ((this.Height != 191 && this.Height != 250) || (this.Width != 191 && this.Width != 300))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 300x250 pixels");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
				}
				else if (this.Extention.Equals("swf"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
					else
					{
						if (!this.BannerLinkTag)
						{
							ret.LinkTagWarning = true;
						}
						if (this.BannerBroken)
						{
							ret.CanUseNow = false;
							ret.Errors.Add("One of our admins failed the banner");
							ret.BrokenError = this.BannerBrokenReason;
						}
					}
				}
				#endregion
			}
			else if (position.Equals(Banner.Positions.Skyscraper))
			{
				#region Skyscraper specs
				if (this.Extention.Equals("jpg"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					//XMAS
					if (this.Height != 250 || this.Width != 300)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 300x250 pixels");
					}
					//if (this.Height != 600 || (this.Width != 120 && this.Width != 160 && this.Width != 300))
					//{
					//    ret.CanUseNow = false;
					//    ret.Errors.Add("Image must be 300x600, 160x600, 120x600 pixels");
					//}
				}
				else if (this.Extention.Equals("gif"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					//XMAS
					if (this.Height != 250 || this.Width != 300)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 300x250 pixels");
					}
					//if (this.Height != 600 || (this.Width != 120 && this.Width != 160 && this.Width != 300))
					//{
					//    ret.CanUseNow = false;
					//    ret.Errors.Add("Image must be 300x600, 160x600, 120x600 pixels");
					//}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
				}
				else if (this.Extention.Equals("swf"))
				{
					if (this.Size > (150 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 100KB");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
					else
					{
						if (!this.BannerLinkTag)
						{
							ret.LinkTagWarning = true;
						}
						if (this.BannerBroken)
						{
							ret.CanUseNow = false;
							ret.Errors.Add("One of our admins failed the banner");
							ret.BrokenError = this.BannerBrokenReason;
						}
					}
				}
				#endregion
			}
			else if (position.Equals(Banner.Positions.PhotoBanner))
			{
				#region PhotoBanner specs
				if (this.Extention.Equals("jpg"))
				{
					if (this.Size > (30 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 20KB");
					}
					if (this.Height != 50 || this.Width != 450)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 450x50 pixels");
					}
				}
				else if (this.Extention.Equals("gif"))
				{
					if (this.Size > (30 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 20KB");
					}
					if (this.Height != 50 || this.Width != 450)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 450x50 pixels");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
				}
				else if (this.Extention.Equals("swf"))
				{
					if (this.Size > (30 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 20KB");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
					else
					{
						if (!this.BannerLinkTag)
						{
							ret.LinkTagWarning = true;
						}
						if (this.BannerBroken)
						{
							ret.CanUseNow = false;
							ret.Errors.Add("One of our admins failed the banner");
							ret.BrokenError = this.BannerBrokenReason;
						}
					}
				}
				#endregion
			}
			else if (position.Equals(Banner.Positions.EmailBanner))
			{
				#region EmailBanner specs
				if (this.Extention.Equals("jpg"))
				{
					if (this.Size > (40 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 30KB");
					}
					if (this.Height != 51 || this.Width != 331)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 331x51 pixels");
					}
				}
				else if (this.Extention.Equals("gif"))
				{
					if (this.Size > (40 * 1024))
					{
						ret.CanUseNow = false;
						ret.Errors.Add("File size must not be over 30KB");
					}
					if (this.Height != 51 || this.Width != 331)
					{
						ret.CanUseNow = false;
						ret.Errors.Add("Image must be 331x51 pixels");
					}
					if (this.NeedsAuth)
					{
						if (ret.CanUseNow)
						{
							ret.CanUseNow = false;
							ret.CanUseAfterAdminCheck = true;
						}
					}
				}
				else if (this.Extention.Equals("swf"))
				{
					ret.CanUseNow = false;
					ret.Errors.Add("Flash banners not supported in emails");
				}
				#endregion
			}
			return ret;
		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			BannerSet bs = new BannerSet(
				new Query(
					new Or(
						new Q(Banner.Columns.MiscGuid, this.Guid),
						new Q(Banner.Columns.MiscK, this.K),
						new Q(Banner.Columns.NewMiscK, this.K),
						new Q(Banner.Columns.FailedMiscK, this.K)
					)
				)
			);
			foreach (Banner b in bs)
			{
				if (b.MiscGuid.Equals(this.Guid))
					b.MiscGuid = Guid.Empty;
				if (b.MiscK == this.K)
					b.MiscK = 0;
				if (b.NewMiscK == this.K)
					b.NewMiscK = 0;
				if (b.FailedMiscK == this.K)
					b.FailedMiscK = 0;
				b.Update(transaction);
			}

			this.Delete(transaction);

			Storage.RemoveFromStore(Storage.Stores.Pix, this.Guid, this.Extention);
		}
		#endregion

		//#region FileSystemPaths
		//private string devPathRoot
		//{
		//    get { return @"C:\DontStayIn\pix\"; }
		//}

		//#region FileSystemPath
		//public string FileSystemPath
		//{
		//    get
		//    {
		//        if (Vars.DevEnv)
		//            return devPathRoot + Guid.ToString() + this.ExtentionWithDot;
		//        else
		//            return Photo.FileSystemPath(this.Guid, this.Extention, Photo.Locations.Pix);
		//            //return @"\\" + Vars.PixServerIp + @"\d$\DontStayIn\Live\pix\" + this.Guid.ToString().Substring(0, 2) + @"\" + this.Guid.ToString().Substring(2, 2) + @"\" + this.Guid.ToString() + this.ExtentionWithDot;
		//    }
		//}
		//#endregion

		//#endregion

		#region Url
		public string Url()
		{
			return Storage.Path(this.Guid, this.Extention);
		}
		#endregion

		public void DeleteFiles()
		{
			Storage.RemoveFromStore(Storage.Stores.Pix, this.Guid, this.Extention);
		}

		#region Duplicate
		public Misc Duplicate()
		{
			Misc m = new Misc();
			m.Guid = Guid.NewGuid();
			m.Extention = this.Extention;
			m.Update();
			try
			{
				Storage.AddToStore(
					Storage.GetFromStore(Storage.Stores.Pix, this.Guid, this.Extention),
					Storage.Stores.Pix,
					m.Guid,
					m.Extention,
					m,
					"");
				m.UsrK = this.UsrK;
				m.PromoterK = this.PromoterK;
				m.Size = this.Size;
				m.DateTime = this.DateTime;
				m.DateTimeExpires = this.DateTimeExpires;
				m.Folder = this.Folder;
				m.Name = this.Name;
				m.Note = this.Note;
				m.Xml = this.Xml;
				m.NeedsAuth = this.NeedsAuth;
				m.BannerLinkTag = this.BannerLinkTag;
				m.BannerBroken = this.BannerBroken;
				m.BannerBrokenReason = this.BannerBrokenReason;
				m.Width = this.Width;
				m.Height = this.Height;
				m.Update();
				return m;
			}
			catch (Exception ex)
			{
				m.Delete();
				throw ex;
			}

		}
		#endregion

		#region ViewUrl
		public string ViewUrl(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "mode", "view", "misck", this.K.ToString() }, par);
			if (this.PromoterK == 0)
				return UrlInfo.PageUrl("Files", fullParams);
			else
				return Promoter.UrlApp("files", fullParams);
		}
		#endregion

		#region OrderBy
		public static OrderBy OrderBy
		{
			get
			{
				return new OrderBy(new OrderBy(Misc.Columns.Folder), new OrderBy(Misc.Columns.Name), new OrderBy(Misc.Columns.DateTime));
			}
		}
		#endregion

		#region FullName
		public string FullName
		{
			get
			{
				string f = this.Folder;
				if (f.Length > 0)
				{
					if (!f.StartsWith("/"))
						f = "/" + f;
					if (!f.EndsWith("/"))
						f = f + "/";
				}
				else
					f = "/";
				return f + this.Name;
			}
		}
		#endregion

		#region ExtentionWithDot
		public string ExtentionWithDot
		{
			get
			{
				if (this.Extention.Length == 0)
					return "";
				else
					return "." + this.Extention;
			}
		}
		#endregion

		#region FriendlyDateTime
		public string FriendlyDateTime(bool Capital)
		{
			return Cambro.Misc.Utility.FriendlyTime(DateTime, Capital);
		}
		#endregion

		#region FileSizeString
		public string FileSizeString
		{
			get
			{
				if (Size > (1024 * 1024 * 1024))
				{
					double size = Size / (1024.0 * 1024.0 * 1024.0);
					return size.ToString("#.##") + " GB";
				}
				else if (Size > (1024 * 1024))
				{
					double size = Size / (1024.0 * 1024.0);
					return size.ToString("#.##") + " MB";
				}
				else if (Size > (1024))
				{
					double size = Size / (1024.0);
					return size.ToString("#.##") + " KB";
				}
				else
				{
					return Size.ToString("#.##") + " Bytes";
				}
			}
		}
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null && UsrK > 0)
					usr = new Usr(UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		private Usr usr;
		#endregion
		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter == null && PromoterK > 0)
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
		#endregion

		#region Upload
		public static Misc UploadFile(HtmlInputFile inputFile, Usr uploadUsr)
		{
			return UploadFile(inputFile, uploadUsr, null);
		}
		public static Misc UploadFile(HtmlInputFile inputFile, Usr uploadUsr, Promoter promoter)
		{
			return UploadFile(inputFile, uploadUsr, promoter, (Banner)null);
		}
		public static Misc UploadFile(HtmlInputFile inputFile, Usr uploadUsr, Promoter promoter, List<string> acceptedFileExtensions)
		{
			return UploadFile(inputFile, uploadUsr, promoter, null, "", acceptedFileExtensions);
		}
		public static Misc UploadFile(HtmlInputFile inputFile, Usr uploadUsr, Promoter promoter, Banner banner)
		{
			return UploadFile(inputFile, uploadUsr, promoter, banner, "");
		}
		public static Misc UploadFile(HtmlInputFile inputFile, Usr uploadUsr, Promoter promoter, Banner banner, string folder)
		{
			return UploadFile(inputFile, uploadUsr, promoter, banner, "", new List<string>(){"jpg", "gif", "swf"});
		}
		public static Misc UploadFile(HtmlInputFile inputFile, Usr uploadUsr, Promoter promoter, Banner banner, string folder, List<string> acceptedFileExtensions)
		{
			if (inputFile.PostedFile != null)
			{
				#region Upload file
				Misc m = new Misc();

				m.UsrK = uploadUsr.K;

				if (promoter != null)
					m.PromoterK = promoter.K;

				m.DateTime = DateTime.Now;
				m.Folder = folder;

				m.Guid = Guid.NewGuid();


				if (inputFile.PostedFile.FileName.IndexOf(".") == -1)
					m.Extention = "";
				else
					m.Extention = inputFile.PostedFile.FileName.Substring(inputFile.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();

				if (m.Extention.Equals("jpeg") || m.Extention.Equals("jpe"))
					m.Extention = "jpg";

				if (!acceptedFileExtensions.Contains(m.Extention))
				{
					string listOfFileExtensions = "";
					foreach(string s in acceptedFileExtensions)
						listOfFileExtensions += s + ", ";
					throw new DsiUserFriendlyException("You can only upload " + listOfFileExtensions.Substring(0, listOfFileExtensions.Length-2) + " files with this page.");
				}

				if (promoter != null && m.Extention.Equals("swf"))
				{
					if (m.Size <= 150 * 1024)
						m.NeedsAuth = true;
				}
				
				byte[] bytes = new byte[inputFile.PostedFile.InputStream.Length];
				inputFile.PostedFile.InputStream.Read(bytes, 0, (int)inputFile.PostedFile.InputStream.Length);
				
				m.Size = inputFile.PostedFile.ContentLength;

				m.Name = inputFile.PostedFile.FileName.Substring(inputFile.PostedFile.FileName.LastIndexOf("\\") + 1);

				if (m.Extention.Equals("jpg") || m.Extention.Equals("gif") || m.Extention.Equals("png"))
				{
					using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bytes)))
					{
						m.Width = image.Width;
						m.Height = image.Height;
					}
				}

				m.Update();

				try
				{
					Storage.AddToStore(bytes, Storage.Stores.Pix, m.Guid, m.Extention, m, "");
				}
				catch (Exception ex)
				{
					m.Delete();
					throw ex;
				}

				if (promoter != null)
				{
					if (promoter != null && m.NeedsAuth)
					{
						Mailer adminMail = new Mailer();
						adminMail.Subject = "New files waiting to be approved!!! uploaded by" + uploadUsr.NickNameSafe;
						adminMail.To = "promoters@dontstayin.com";
						adminMail.Body += "<p>New FILES uploaded by <a href=\"[LOGIN(" + uploadUsr.Url() + ")]\">" + uploadUsr.NickNameSafe + "</a></p>";
						if (promoter != null)
							adminMail.Body += "<p>... for promoter <a href=\"[LOGIN(" + promoter.Url() + ")]\">" + promoter.Name + "</a></p>";
						adminMail.Body += "<h2>Files:</h2>";
						adminMail.Body += "<p><a href=\"" + m.Url() + "\">" + HttpUtility.HtmlEncode(m.Name) + "</a> - " + m.FileSizeString + "</p>";
						adminMail.TemplateType = Mailer.TemplateTypes.AdminNote;
						adminMail.RedirectUrl = uploadUsr.Url();
						adminMail.Send();
					}
				}

				if (banner != null)
					banner.AssignMisc(m);

				return m;

				#endregion
			}
			else
				return null;
		}
		#endregion


		#region IBobType Members

		public string TypeName
		{
			get { return "Misc"; }
		}

		#endregion

		#region IHasObjectType Members

		public Model.Entities.ObjectType ObjectType
		{
			get { return Model.Entities.ObjectType.Misc; }
		}

		#endregion
	}
	#endregion

}

