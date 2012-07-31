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
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using Caching;
using Bobs.Tagging;
using Bobs.ChildInterfaces;
using System.Collections.Generic;
using Bobs.CachedDataAccess;
using SpottedScript.Controls.ChatClient.Shared;
using AmazonS3;
using Pair = System.Collections.Generic.KeyValuePair<object, Bobs.OrderBy.OrderDirection>;
using System.Reflection;

namespace Bobs
{

	#region Photo
	/// <summary>
	/// Photo taken at an event
	/// </summary>
	[Serializable]
	public partial class Photo : IPage, IBobType, IHasPrimaryThread, IDiscussable, ICanView, IObjectPage, IRelevanceContributor, IDeleteAll, IConnectedTo, ITaggable, IHasChildTags, IReadableReference, IHasParent
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Photo.Columns.K] as int? ?? 0; }
			set { this[Photo.Columns.K] = value; }
		}
		/// <summary>
		/// Links to one Event
		/// </summary>
		public override int EventK
		{
			get { return (int)this[Photo.Columns.EventK]; }
			set { _event = null; this[Photo.Columns.EventK] = value; }
		}
		/// <summary>
		/// The usr that uploaded the photo
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Photo.Columns.UsrK]; }
			set { usr = null; this[Photo.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The mobile that sent the photo
		/// </summary>
		public override int MobileK
		{
			get { return (int)this[Photo.Columns.MobileK]; }
			set { mobile = null; this[Photo.Columns.MobileK] = value; }
		}
		/// <summary>
		/// Order
		/// </summary>
		public override double Order
		{
			get { return (double)this[Photo.Columns.Order]; }
			set { this[Photo.Columns.Order] = value; }
		}
		/// <summary>
		/// Filename of master original image (without logo)
		/// </summary>
		public override Guid Master
		{
			get
			{
				if (ContentDisabled)
					return ContentDisabledWeb;
				return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Master]);
			}
			set { this[Photo.Columns.Master] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Filename of original image
		/// </summary>
		public override Guid Original
		{
			get
			{
				if (ContentDisabled)
					return ContentDisabledWeb;
				return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Original]);
			}
			set { this[Photo.Columns.Original] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Filename of icon (35*26)
		/// </summary>
		public override Guid Icon
		{
			get
			{
				if (ContentDisabled)
					return ContentDisabledIcon;
				return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Icon]);
			}
			set { this[Photo.Columns.Icon] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Filename of thumbnail (106*80)
		/// </summary>
		public override Guid Thumb
		{
			get
			{
				if (ContentDisabled)
					return ContentDisabledThumb;
				return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Thumb]);
			}
			set { this[Photo.Columns.Thumb] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Filename of web-res (640*480)
		/// </summary>
		public override Guid Web
		{
			get
			{
				if (ContentDisabled)
					return ContentDisabledWeb;
				return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Web]);
			}
			set { this[Photo.Columns.Web] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Filename of cropping image (max side=1000, no logo)
		/// </summary>
		public override Guid Crop
		{
			get
			{
				if (ContentDisabled)
					return ContentDisabledBlank;

				if (HasCrop)
					return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Crop]);
				else
					return CreateCrop();

				//Guid g = Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Crop]);
				//if (g.Equals(Guid.Empty))
				//{
				//    g = CreateCrop();
				//}
				//return g;
			}
			set { this[Photo.Columns.Crop] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Discussion thread about this photo - only created when first message is posted?
		/// </summary>
		public override int? ThreadK
		{
			get { return (int?) this[Photo.Columns.ThreadK]; }
			set { thread = null; this[Photo.Columns.ThreadK] = value; }
		}
		/// <summary>
		/// Date / time the photo was taken (from exif data)
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[Photo.Columns.DateTime]; }
			set { this[Photo.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Number of views
		/// </summary>
		public override int Views
		{
			get { return (int)this[Photo.Columns.Views]; }
			set { this[Photo.Columns.Views] = value; }
		}
		/// <summary>
		/// Average cool rating
		/// </summary>
		public override double AverageCoolRating
		{
			get { return (double)this[Photo.Columns.AverageCoolRating]; }
			set { this[Photo.Columns.AverageCoolRating] = value; }
		}
		/// <summary>
		/// Average sexy rating
		/// </summary>
		public override double AverageSexyRating
		{
			get { return (double)this[Photo.Columns.AverageSexyRating]; }
			set { this[Photo.Columns.AverageSexyRating] = value; }
		}
		/// <summary>
		/// Total number of comments
		/// </summary>
		public override int TotalComments
		{
			get { return (int)this[Photo.Columns.TotalComments]; }
			set { this[Photo.Columns.TotalComments] = value; }
		}
		/// <summary>
		/// Total number of cool ratings
		/// </summary>
		public override int TotalCoolRatings
		{
			get { return (int)this[Photo.Columns.TotalCoolRatings]; }
			set { this[Photo.Columns.TotalCoolRatings] = value; }
		}
		/// <summary>
		/// Total number of sexy ratings
		/// </summary>
		public override int TotalSexyRatings
		{
			get { return (int)this[Photo.Columns.TotalSexyRatings]; }
			set { this[Photo.Columns.TotalSexyRatings] = value; }
		}
		/// <summary>
		/// Weighted cool rating = ( sum(Ratings) + 50 ) / ( count(Ratings) + 10 )
		/// </summary>
		public override double WeightedCoolRating
		{
			get { return (double)this[Photo.Columns.WeightedCoolRating]; }
			set { this[Photo.Columns.WeightedCoolRating] = value; }
		}
		/// <summary>
		/// Weighted sexy rating = ( sum(Ratings) + 50 ) / ( count(Ratings) + 10 )
		/// </summary>
		public override double WeightedSexyRating
		{
			get { return (double)this[Photo.Columns.WeightedSexyRating]; }
			set { this[Photo.Columns.WeightedSexyRating] = value; }
		}
		/// <summary>
		/// Width of the original image
		/// </summary>
		public override int OriginalWidth
		{
			get
			{
				if (ContentDisabled)
					return 450;
				return (int)this[Photo.Columns.OriginalWidth];
			}
			set { this[Photo.Columns.OriginalWidth] = value; }
		}
		/// <summary>
		/// Height of the original image
		/// </summary>
		public override int OriginalHeight
		{
			get
			{
				if (ContentDisabled)
					return 300;
				return (int)this[Photo.Columns.OriginalHeight];
			}
			set { this[Photo.Columns.OriginalHeight] = value; }
		}
		/// <summary>
		/// Width of the web image
		/// </summary>
		public override int WebWidth
		{
			get
			{
				if (ContentDisabled)
					return 450;
				return (int)this[Photo.Columns.WebWidth];
			}
			set { this[Photo.Columns.WebWidth] = value; }
		}
		/// <summary>
		/// Height of the web image
		/// </summary>
		public override int WebHeight
		{
			get
			{
				if (ContentDisabled)
					return 300;
				return (int)this[Photo.Columns.WebHeight];
			}
			set { this[Photo.Columns.WebHeight] = value; }
		}

		/// <summary>
		/// Width of the thumbnail image
		/// </summary>
		public override int ThumbWidth
		{
			get
			{
				if (ContentDisabled)
					return 124;
				return (int)this[Photo.Columns.ThumbWidth];
			}
			set { this[Photo.Columns.ThumbWidth] = value; }
		}
		/// <summary>
		/// Height of the thumbnail image
		/// </summary>
		public override int ThumbHeight
		{
			get
			{
				if (ContentDisabled)
					return 51;
				return (int)this[Photo.Columns.ThumbHeight];
			}
			set { this[Photo.Columns.ThumbHeight] = value; }
		}
		/// <summary>
		/// Is the image in landscae orientation? (If the image is square, this is true)
		/// </summary>
		public override bool IsLandscape
		{
			get
			{
				if (ContentDisabled)
					return true;
				return (bool)this[Photo.Columns.IsLandscape];
			}
			set { this[Photo.Columns.IsLandscape] = value; }
		}
		/// <summary>
		/// Note for admin
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Photo.Columns.AdminNote]; }
			set { this[Photo.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// EquipmentMake from exif data
		/// </summary>
		public override string EquipmentMake
		{
			get { return (string)this[Photo.Columns.EquipmentMake]; }
			set { this[Photo.Columns.EquipmentMake] = value; }
		}
		/// <summary>
		/// CameraModel from exif data
		/// </summary>
		public override string CameraModel
		{
			get { return (string)this[Photo.Columns.CameraModel]; }
			set { this[Photo.Columns.CameraModel] = value; }
		}
		/// <summary>
		/// Size of original file in bytes
		/// </summary>
		public override int OriginalFileSize
		{
			get
			{
				if (ContentDisabled)
					return 24727;
				return (int)this[Photo.Columns.OriginalFileSize];
			}
			set { this[Photo.Columns.OriginalFileSize] = value; }
		}
		/// <summary>
		/// Size of master original file in bytes
		/// </summary>
		public override int MasterFileSize
		{
			get
			{
				if (ContentDisabled)
					return 24727;
				return (int)this[Photo.Columns.MasterFileSize];
			}
			set { this[Photo.Columns.MasterFileSize] = value; }
		}
		/// <summary>
		/// The date/time of the last post that was posted in this board (including child objects)
		/// </summary>
		public override DateTime LastPost
		{
			get { return (DateTime)this[Photo.Columns.LastPost]; }
			set { this[Photo.Columns.LastPost] = value; }
		}
		/// <summary>
		/// The average date.time of all comments posted in this board (including child objects)
		/// </summary>
		public override DateTime AverageCommentDateTime
		{
			get { return (DateTime)this[Photo.Columns.AverageCommentDateTime]; }
			set { this[Photo.Columns.AverageCommentDateTime] = value; }
		}
		/// <summary>
		/// Has the photo been converted to dsi logos?
		/// </summary>
		public override bool DsiConverted
		{
			get { return (bool)this[Photo.Columns.DsiConverted]; }
			set { this[Photo.Columns.DsiConverted] = value; }
		}
		/// <summary>
		/// Should this photo go on the Photo of the Week section?
		/// </summary>
		public override bool PhotoOfWeek
		{
			get { return (bool)this[Photo.Columns.PhotoOfWeek]; }
			set { this[Photo.Columns.PhotoOfWeek] = value; }
		}
		/// <summary>
		/// The latest 3 photos of the week are displayed... This is the datetime
		/// </summary>
		public override DateTime PhotoOfWeekDateTime
		{
			get { return (DateTime)this[Photo.Columns.PhotoOfWeekDateTime]; }
			set { this[Photo.Columns.PhotoOfWeekDateTime] = value; }
		}
		/// <summary>
		/// This is the caption for the photo of the week section
		/// </summary>
		public override string PhotoOfWeekCaption
		{
			get { return (string)this[Photo.Columns.PhotoOfWeekCaption]; }
			set { this[Photo.Columns.PhotoOfWeekCaption] = value; }
		}
		/// <summary>
		/// Random float less than 1 used for fast pseudo-random ordering
		/// </summary>
		public override double RandomNumber
		{
			get { return (double)this[Photo.Columns.RandomNumber]; }
			set { this[Photo.Columns.RandomNumber] = value; }
		}
		/// <summary>
		/// True if the content has been disabled because of a complaint
		/// </summary>
		public override bool ContentDisabled
		{
			get { return (bool)this[Photo.Columns.ContentDisabled]; }
			set { this[Photo.Columns.ContentDisabled] = value; }
		}
		/// <summary>
		/// 1=New, 2=Enabled, 3=Disabled
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[Photo.Columns.Status]; }
			set { this[Photo.Columns.Status] = value; }
		}
		/// <summary>
		/// Link to the gallery table - the gallery that this photo is in.
		/// </summary>
		public override int GalleryK
		{
			get { return (int)this[Photo.Columns.GalleryK]; }
			set { gallery = null; this[Photo.Columns.GalleryK] = value; }
		}
		/// <summary>
		/// Order in the gallery when ordered by datetime
		/// </summary>
		public override int GalleryTimeOrder
		{
			get { return (int)this[Photo.Columns.GalleryTimeOrder]; }
			set { this[Photo.Columns.GalleryTimeOrder] = value; }
		}
		/// <summary>
		/// Order in the gallery when ordered by rating
		/// </summary>
		public override int GalleryRatingOrder
		{
			get { return (int)this[Photo.Columns.GalleryRatingOrder]; }
			set { this[Photo.Columns.GalleryRatingOrder] = value; }
		}
		/// <summary>
		/// Which admin user enabled this photo?
		/// </summary>
		public override int EnabledByUsrK
		{
			get { return (int)this[Photo.Columns.EnabledByUsrK]; }
			set { enabledByUsr = null; this[Photo.Columns.EnabledByUsrK] = value; }
		}
		/// <summary>
		/// When was this photo enabled?
		/// </summary>
		public override DateTime EnabledDateTime
		{
			get { return (DateTime)this[Photo.Columns.EnabledDateTime]; }
			set { this[Photo.Columns.EnabledDateTime] = value; }
		}
		/// <summary>
		/// Link to the article if this photo is in a gallery that is in an event
		/// </summary>
		public override int ArticleK
		{
			get { return (int)this[Photo.Columns.ArticleK]; }
			set { article = null; this[Photo.Columns.ArticleK] = value; }
		}
		/// <summary>
		/// The DateTime of the parent object - e.g. datetime of event, or createdate of the article
		/// </summary>
		public override DateTime ParentDateTime
		{
			get { return (DateTime)this[Photo.Columns.ParentDateTime]; }
			set { this[Photo.Columns.ParentDateTime] = value; }
		}
		/// <summary>
		/// The next photo in the gallery (1)
		/// </summary>
		public override int NextPhoto1K
		{
			get { return (int)this[Photo.Columns.NextPhoto1K]; }
			set { nextPhoto1 = null; this[Photo.Columns.NextPhoto1K] = value; }
		}
		/// <summary>
		/// The next photo in the gallery (2)
		/// </summary>
		public override int NextPhoto2K
		{
			get { return (int)this[Photo.Columns.NextPhoto2K]; }
			set { nextPhoto2 = null; this[Photo.Columns.NextPhoto2K] = value; }
		}
		/// <summary>
		/// The next photo in the gallery (3)
		/// </summary>
		public override int NextPhoto3K
		{
			get { return (int)this[Photo.Columns.NextPhoto3K]; }
			set { nextPhoto3 = null; this[Photo.Columns.NextPhoto3K] = value; }
		}
		/// <summary>
		/// The Previous photo in the gallery (1)
		/// </summary>
		public override int PreviousPhoto1K
		{
			get { return (int)this[Photo.Columns.PreviousPhoto1K]; }
			set { previousPhoto1 = null; this[Photo.Columns.PreviousPhoto1K] = value; }
		}
		/// <summary>
		/// The Previous photo in the gallery (2)
		/// </summary>
		public override int PreviousPhoto2K
		{
			get { return (int)this[Photo.Columns.PreviousPhoto2K]; }
			set { previousPhoto2 = null; this[Photo.Columns.PreviousPhoto2K] = value; }
		}
		/// <summary>
		/// The Previous photo in the gallery (3)
		/// </summary>
		public override int PreviousPhoto3K
		{
			get { return (int)this[Photo.Columns.PreviousPhoto3K]; }
			set { previousPhoto3 = null; this[Photo.Columns.PreviousPhoto3K] = value; }
		}
		/// <summary>
		/// The url fragment - so that the url can be generated without accessing parent database records
		/// </summary>
		public override string UrlFragment
		{
			get { return (string)this[Photo.Columns.UrlFragment]; }
			set { this[Photo.Columns.UrlFragment] = value; }
		}
		/// <summary>
		/// Number of users in this photo - read this before accessing UsrsInThisPhoto UsrSet!
		/// </summary>
		public override int UsrCount
		{
			get { return (int)this[Photo.Columns.UsrCount]; }
			set { this[Photo.Columns.UsrCount] = value; }
		}
		/// <summary>
		/// The first usr that's in this photo
		/// </summary>
		public override int FirstUsrK
		{
			get { return (int)this[Photo.Columns.FirstUsrK]; }
			set { firstUsr = null; this[Photo.Columns.FirstUsrK] = value; }
		}
		/// <summary>
		/// Has the master Jpg been compressed?
		/// </summary>
		public override bool IsMasterCompressed
		{
			get { return (bool)this[Photo.Columns.IsMasterCompressed]; }
			set { this[Photo.Columns.IsMasterCompressed] = value; }
		}
		/// <summary>
		/// Has the processing started yet?
		/// </summary>
		public override bool IsProcessing
		{
			get { return (bool)this[Photo.Columns.IsProcessing]; }
			set { this[Photo.Columns.IsProcessing] = value; }
		}
		/// <summary>
		/// Media type - Image, Video or Audio
		/// </summary>
		public override MediaTypes MediaType
		{
			get { return (MediaTypes)this[Photo.Columns.MediaType]; }
			set { this[Photo.Columns.MediaType] = value; }
		}
		/// <summary>
		/// Estimated percentage complete
		/// </summary>
		public override int ProcessingProgress
		{
			get { return (int)this[Photo.Columns.ProcessingProgress]; }
			set { this[Photo.Columns.ProcessingProgress] = value; }
		}
		/// <summary>
		/// When the processing started
		/// </summary>
		public override DateTime? ProcessingStartDateTime
		{
			get { return (DateTime?) this[Photo.Columns.ProcessingStartDateTime]; }
			set { this[Photo.Columns.ProcessingStartDateTime] = value; }
		}
		/// <summary>
		/// Guid filename of the 256Kb/sec video FLV
		/// </summary>
		public override Guid VideoLo
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.VideoLo]); }
			set { this[Photo.Columns.VideoLo] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Guid filename of the 512Kb/sec video FLV
		/// </summary>
		public override Guid VideoMed
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.VideoMed]); }
			set { this[Photo.Columns.VideoMed] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Guid filename of the 1024Kb/sec video FLV
		/// </summary>
		public override Guid VideoHi
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.VideoHi]); }
			set { this[Photo.Columns.VideoHi] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Guid filename of the 64Kb/sec audio FLV
		/// </summary>
		public override Guid AudioLo
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.AudioLo]); }
			set { this[Photo.Columns.AudioLo] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Guid filename of the 128Kb/sec audio FLV
		/// </summary>
		public override Guid AudioMed
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.AudioMed]); }
			set { this[Photo.Columns.AudioMed] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Guid filename of the 192Kb/sec audio FLV
		/// </summary>
		public override Guid AudioHi
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.AudioHi]); }
			set { this[Photo.Columns.AudioHi] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Guid filename of the audio master file
		/// </summary>
		public override Guid AudioMaster
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.AudioMaster]); }
			set { this[Photo.Columns.AudioMaster] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Guid filename of the video master file
		/// </summary>
		public override Guid VideoMaster
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.VideoMaster]); }
			set { this[Photo.Columns.VideoMaster] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// File extention of the audio master file
		/// </summary>
		public override string AudioFileExtention
		{
			get { return (string)this[Photo.Columns.AudioFileExtention]; }
			set { this[Photo.Columns.AudioFileExtention] = value; }
		}
		/// <summary>
		/// File extention of the video master file
		/// </summary>
		public override string VideoFileExtention
		{
			get { return (string)this[Photo.Columns.VideoFileExtention]; }
			set { this[Photo.Columns.VideoFileExtention] = value; }
		}
		/// <summary>
		/// Video master file size in bytes
		/// </summary>
		public override int VideoMasterFileSize
		{
			get { return (int)this[Photo.Columns.VideoMasterFileSize]; }
			set { this[Photo.Columns.VideoMasterFileSize] = value; }
		}
		/// <summary>
		/// Audio master file size in bytes
		/// </summary>
		public override int AudioMasterFileSize
		{
			get { return (int)this[Photo.Columns.AudioMasterFileSize]; }
			set { this[Photo.Columns.AudioMasterFileSize] = value; }
		}
		/// <summary>
		/// Video frames per second
		/// </summary>
		public override double VideoMasterFramerate
		{
			get { return (double)this[Photo.Columns.VideoMasterFramerate]; }
			set { this[Photo.Columns.VideoMasterFramerate] = value; }
		}
		/// <summary>
		/// Video height in pixels
		/// </summary>
		public override int VideoMasterHeight
		{
			get { return (int)this[Photo.Columns.VideoMasterHeight]; }
			set { this[Photo.Columns.VideoMasterHeight] = value; }
		}
		/// <summary>
		/// Video width in pixels
		/// </summary>
		public override int VideoMasterWidth
		{
			get { return (int)this[Photo.Columns.VideoMasterWidth]; }
			set { this[Photo.Columns.VideoMasterWidth] = value; }
		}
		/// <summary>
		/// Duration of the video in miliseconds
		/// </summary>
		public override int VideoDuration
		{
			get { return (int)this[Photo.Columns.VideoDuration]; }
			set { this[Photo.Columns.VideoDuration] = value; }
		}
		/// <summary>
		/// Duration of the audio in miliseconds
		/// </summary>
		public override int AudioDuration
		{
			get { return (int)this[Photo.Columns.AudioDuration]; }
			set { this[Photo.Columns.AudioDuration] = value; }
		}
		/// <summary>
		/// DateTime that the media processing progress last changed (for abort timeout)
		/// </summary>
		public override DateTime? ProcessingLastChange
		{
			get { return (DateTime?)this[Photo.Columns.ProcessingLastChange]; }
			set { this[Photo.Columns.ProcessingLastChange] = value; }
		}
		/// <summary>
		/// Framerate of the 256Kb/sec video FLV
		/// </summary>
		public override double VideoLoFramerate
		{
			get { return (double)this[Photo.Columns.VideoLoFramerate]; }
			set { this[Photo.Columns.VideoLoFramerate] = value; }
		}
		/// <summary>
		/// Framerate of the 512Kb/sec video FLV
		/// </summary>
		public override double VideoMedFramerate
		{
			get { return (double)this[Photo.Columns.VideoMedFramerate]; }
			set { this[Photo.Columns.VideoMedFramerate] = value; }
		}
		/// <summary>
		/// Framerate of the 1024Kb/sec video FLV
		/// </summary>
		public override double VideoHiFramerate
		{
			get { return (double)this[Photo.Columns.VideoHiFramerate]; }
			set { this[Photo.Columns.VideoHiFramerate] = value; }
		}
		/// <summary>
		/// Height of the 256Kb/sec video FLV
		/// </summary>
		public override int VideoLoHeight
		{
			get { return (int)this[Photo.Columns.VideoLoHeight]; }
			set { this[Photo.Columns.VideoLoHeight] = value; }
		}
		/// <summary>
		/// Height of the 512Kb/sec video FLV
		/// </summary>
		public override int VideoMedHeight
		{
			get { return (int)this[Photo.Columns.VideoMedHeight]; }
			set { this[Photo.Columns.VideoMedHeight] = value; }
		}
		/// <summary>
		/// Height of the 1024Kb/sec video FLV
		/// </summary>
		public override int VideoHiHeight
		{
			get { return (int)this[Photo.Columns.VideoHiHeight]; }
			set { this[Photo.Columns.VideoHiHeight] = value; }
		}
		/// <summary>
		/// Width of the 256Kb/sec video FLV
		/// </summary>
		public override int VideoLoWidth
		{
			get { return (int)this[Photo.Columns.VideoLoWidth]; }
			set { this[Photo.Columns.VideoLoWidth] = value; }
		}
		/// <summary>
		/// Width of the 512Kb/sec video FLV
		/// </summary>
		public override int VideoMedWidth
		{
			get { return (int)this[Photo.Columns.VideoMedWidth]; }
			set { this[Photo.Columns.VideoMedWidth] = value; }
		}
		/// <summary>
		/// Width of the 1024Kb/sec video FLV
		/// </summary>
		public override int VideoHiWidth
		{
			get { return (int)this[Photo.Columns.VideoHiWidth]; }
			set { this[Photo.Columns.VideoHiWidth] = value; }
		}
		/// <summary>
		/// How many times has the encoder tried to encode this file?
		/// </summary>
		public override int ProcessingAttempts
		{
			get { return (int)this[Photo.Columns.ProcessingAttempts]; }
			set { this[Photo.Columns.ProcessingAttempts] = value; }
		}
		/// <summary>
		/// How many times has the original image been generated today?
		/// </summary>
		public override int OriginalHitsToday
		{
			get { return (int)this[Photo.Columns.OriginalHitsToday]; }
			set { this[Photo.Columns.OriginalHitsToday] = value; }
		}
		/// <summary>
		/// Original image generator hit counter date
		/// </summary>
		public override DateTime OriginalHitsDate
		{
			get { return (DateTime)this[Photo.Columns.OriginalHitsDate]; }
			set { this[Photo.Columns.OriginalHitsDate] = value; }
		}
		/// <summary>
		/// Which server is processing the video
		/// </summary>
		public override string ProcessingServerName
		{
			get { return (string)this[Photo.Columns.ProcessingServerName]; }
			set { this[Photo.Columns.ProcessingServerName] = value; }
		}
		/// <summary>
		/// Was this photo taken with a Sony K800i?
		/// </summary>
		public override bool IsSonyK800i
		{
			get { return (bool)this[Photo.Columns.IsSonyK800i]; }
			set { this[Photo.Columns.IsSonyK800i] = value; }
		}
		/// <summary>
		/// Is this thread in a caption competition?
		/// </summary>
		public override bool IsInCaptionCompetition
		{
			get { return (bool)this[Photo.Columns.IsInCaptionCompetition]; }
			set { this[Photo.Columns.IsInCaptionCompetition] = value; }
		}
		/// <summary>
		/// Rotation transformation when the photo was uploaded
		/// </summary>
		public override int Rotate
		{
			get { return (int)this[Photo.Columns.Rotate]; }
			set { this[Photo.Columns.Rotate] = value; }
		}
		/// <summary>
		/// Location of the temporary uploaded file (in PixMaster)
		/// </summary>
		public override Guid UploadTemporary
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.UploadTemporary]); }
			set { this[Photo.Columns.UploadTemporary] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Location of the temporary uploaded file (in PixMaster) - extention
		/// </summary>
		public override string UploadTemporaryExtention
		{
			get { return (string)this[Photo.Columns.UploadTemporaryExtention]; }
			set { this[Photo.Columns.UploadTemporaryExtention] = value; }
		}
		/// <summary>
		/// Enum for photo overlays
		/// </summary>
		public override Overlays Overlay
		{
			get { return (Overlays)this[Photo.Columns.Overlay]; }
			set { this[Photo.Columns.Overlay] = value; }
		}
		/// <summary>
		/// Tags from the uploader control - prior to processing
		/// </summary>
		public override string UploadTemporaryTags
		{
			get { return (string)this[Photo.Columns.UploadTemporaryTags]; }
			set { this[Photo.Columns.UploadTemporaryTags] = value; }
		}
		 
		/// <summary>
		/// Have the PixMaster files been uploaded to Amazon?
		/// </summary>
		public override bool DoneAmazonPixMaster
		{
			get { return (bool)this[Photo.Columns.DoneAmazonPixMaster]; }
			set { this[Photo.Columns.DoneAmazonPixMaster] = value; }
		}
		/// <summary>
		/// Was this photo taken by the Sony C902?
		/// </summary>
		public override bool IsSonyC902
		{
			get { return (bool)this[Photo.Columns.IsSonyC902]; }
			set { this[Photo.Columns.IsSonyC902] = value; }
		}
		/// <summary>
		/// Photo of week selected by users
		/// </summary>
		public override bool PhotoOfWeekUser
		{
			get { return (bool)this[Photo.Columns.PhotoOfWeekUser]; }
			set { this[Photo.Columns.PhotoOfWeekUser] = value; }
		}
		/// <summary>
		/// Photo of week selected by users caption
		/// </summary>
		public override string PhotoOfWeekUserCaption
		{
			get { return (string)this[Photo.Columns.PhotoOfWeekUserCaption]; }
			set { this[Photo.Columns.PhotoOfWeekUserCaption] = value; }
		}
		/// <summary>
		/// Photo of week selected by users date time
		/// </summary>
		public override DateTime? PhotoOfWeekUserDateTime
		{
			get { return (DateTime?)this[Photo.Columns.PhotoOfWeekUserDateTime]; }
			set { this[Photo.Columns.PhotoOfWeekUserDateTime] = value; }
		}
		/// <summary>
		/// Has this photo been blocked from being User Photo of the week?
		/// </summary>
		public override bool BlockedFromPhotoOfWeekUser
		{
			get { return (bool)(this[Photo.Columns.BlockedFromPhotoOfWeekUser] ?? false); }
			set { this[Photo.Columns.BlockedFromPhotoOfWeekUser] = value; }
		}
		/// <summary>
		/// Pic for the front page (600 x 250) image.
		/// </summary>
		public override Guid? FrontPagePic
		{
			get { return (Guid?)this[Photo.Columns.FrontPagePic]; }
			set { this[Photo.Columns.FrontPagePic] = value; }
		}
		/// <summary>
		/// Cropper state for the front page pic.
		/// </summary>
		public override string FrontPagePicState
		{
			get { return (string)this[Photo.Columns.FrontPagePicState]; }
			set { this[Photo.Columns.FrontPagePicState] = value; }
		}
		/// <summary>
		/// CSS class for the front page caption - for colour, alignment etc.
		/// </summary>
		public override string FrontPageCaptionClass
		{
			get { return (string)this[Photo.Columns.FrontPageCaptionClass]; }
			set { this[Photo.Columns.FrontPageCaptionClass] = value; }
		}
		#endregion

		public bool HasFrontPagePic
		{
			get
			{
				return FrontPagePic.HasValue && FrontPagePic.Value != Guid.Empty;
			}
		}

		public static int MaxFileSizeInMB
		{
			get
			{
				return 256;
			}
		}
		public static int MaxFileSizeInBytes
		{
			get
			{
				return MaxFileSizeInMB * 1024 * 1024;
			}
		}

		public bool HasCrop
		{
			get
			{
				Guid g = Cambro.Misc.Db.GuidConvertor(this[Photo.Columns.Crop]);
				return !g.Equals(Guid.Empty);
			}
		}

		public void UpdateChildUrlFragments(bool Cascade)
		{
		}

		#region LinkedTables
		public CachedSqlSelect<Tag> ChildTags()
		{
			return this.ChildTags(null, null);
		}
		public CachedSqlSelect<Tag> ChildTags(Q where)
		{
			return this.ChildTags(where, null);
		}
		public CachedSqlSelect<Tag> ChildTags(params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			return this.ChildTags(null, orderBy);
		}
		public CachedSqlSelect<Tag> ChildTags(Q where, params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			TagTableDef def = new TagTableDef();
			TagPhotoTableDef def2 = new TagPhotoTableDef();
			return new CachedSqlSelect<Tag>(
			new LinkedChildren<Tag>
				(
					TablesEnum.Photo,
					this.K,
					TablesEnum.Tag,
					def.TableCacheKey,
					dr =>
					{
						Tag newTag = new Tag();
						newTag.Initialise(dr);
						return newTag;
					},
					where,
					orderBy,
					TablesEnum.TagPhoto,
					def2.TableCacheKey
				)
			);
		}
		#endregion

		#region Videos
		public static Common.IPagedDataService<Photo> GetRecentVideos()
		{
			return new PhotoSetPagedDataService(new Query
			{
				QueryCondition = new And
					(
						new Q(Photo.Columns.MediaType, Photo.MediaTypes.Video),
                    	new Q(Photo.Columns.Status, Photo.StatusEnum.Enabled)
                    ),
				OrderBy = new OrderBy(Photo.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending),
				CacheDuration = new TimeSpan(1, 0, 0)
			});
		}
		#endregion

		#region //removed
		//#region IsInCaptionGroup
		//public bool IsInCaptionGroup
		//{
		//    get
		//    {
		//        if (!isInCaptionGroupDone)
		//        {
		//            if (Vars.CaptionIsBrand)
		//            {
		//                //See if the event is in the brand
		//                try
		//                {
		//                    EventBrand eb = new EventBrand(this.EventK, Vars.CaptionBrandK);
		//                    isInCaptionGroup = true;
		//                    return true;
		//                }
		//                catch
		//                {
		//                }

		//            }
		//            else
		//            {
		//                //See if the event is linked to the group
		//                try
		//                {
		//                    GroupEvent ge = new GroupEvent(Vars.CaptionGroupK, this.EventK);
		//                    isInCaptionGroup = true;
		//                    return true;
		//                }
		//                catch
		//                {
		//                }
		//            }

		//            isInCaptionGroup = false;
		//        }
		//        return isInCaptionGroup;

		//    }
		//    set
		//    {
		//        isInCaptionGroup = value;
		//    }
		//}
		//private bool isInCaptionGroup;
		//private bool isInCaptionGroupDone=false;
		//#endregion

		//#region IsInCaptionCompetition
		//public bool IsInCaptionCompetition
		//{
		//    get
		//    {
		//        if (!isInCaptionCompetitionDone)
		//        {
		//            //See if the photo is in the group top photos section (the caption competition)
		//            try
		//            {
		//                GroupPhoto gp = new GroupPhoto(Vars.CaptionGroupK, this.K);
		//                isInCaptionCompetition = true;
		//            }
		//            catch
		//            {
		//                isInCaptionCompetition = false;
		//            }
		//        }
		//        return isInCaptionCompetition;
		//    }
		//    set
		//    {
		//        isInCaptionCompetition = value;
		//    }
		//}
		//private bool isInCaptionCompetition;
		//private bool isInCaptionCompetitionDone=false;
		//#endregion
		#endregion

		#region ProcessingSummaryHtml
		public string ProcessingSummaryHtml
		{
			get
			{
				if (this.Status.Equals(Photo.StatusEnum.Processing))
				{
					string html = "";
					if (this.MediaType.Equals(Photo.MediaTypes.Image))
						html += "Photo ";
					else if (this.MediaType.Equals(Photo.MediaTypes.Video))
						html += "Video ";
					else if (this.MediaType.Equals(Photo.MediaTypes.Audio))
						html += "Audio ";

					if (this.IsProcessing)
					{
						TimeSpan processingTime = DateTime.Now - this.ProcessingStartDateTime.Value;
						TimeSpan stuckTime = DateTime.Now - this.ProcessingLastChange.Value;
						html += "processing for " + processingTime.TotalSeconds.ToString("#,##0") + " sec - " + this.ProcessingProgress.ToString() + "% complete (" + stuckTime.TotalSeconds.ToString("#,##0") + " sec since last change).";
					}
					else
					{
						
						if (this.ProcessingAttempts == 0)
							html += "in the queue.";
						else if (this.ProcessingAttempts > 1)
							html += "still in the queue after " + this.ProcessingAttempts + " attempt" + (this.ProcessingAttempts == 1 ? "" : "s") + ".";

						if (HasGivenUpProcessing)
							html += " I've given up! ";
					}
					return html;
				}
				else
					return "not processing.";
			}
		}
		#endregion

		#region HasGivenUpProcessing
		public bool HasGivenUpProcessing
		{
			get
			{
				return (this.MediaType.Equals(Photo.MediaTypes.Image) && this.ProcessingAttempts >= 8) || (this.MediaType.Equals(Photo.MediaTypes.Video) && this.ProcessingAttempts >= 2);
			}
		}
		#endregion

		#region AdminMouseOver
		public string AdminMouseOver
		{
			get
			{
				if (this.MediaType.Equals(MediaTypes.Video))
				{
					if (this.Status == StatusEnum.Processing)
						return "stt('No preview available.')";
					else
						return "stmv('" + this.VideoMedPath + "', " + this.VideoMedWidth + ", " + this.VideoMedHeight + ");";
					//return "stmv('" + this.VideoMedPath + "', 450, 357);";
				}
				else
				{
					if (this.Status == StatusEnum.Processing)
						return "stt('No preview available.')";//stm('<img src=" + Photo.Path(this.UploadTemporary, this.UploadTemporaryExtention, true) + " width=200>');";
					else
						return "stm('<img src=" + this.WebPath + " width=" + this.WebWidth + " height=" + this.WebHeight + " class=Block />');";
				}
			}
		}
		#endregion

		public object ThreadTableColumnToBeSet { get { return Thread.Columns.PhotoK; } }

		#region JoinedGroupPhoto
		public GroupPhoto JoinedGroupPhoto
		{
			get
			{
				if (joinedGroupPhoto == null)
				{
					joinedGroupPhoto = new GroupPhoto(this, Photo.Columns.K);
				}
				return joinedGroupPhoto;
			}
			set
			{
				joinedGroupPhoto = value;
			}
		}
		private GroupPhoto joinedGroupPhoto;
		#endregion

		#region Rollover
		#region mouseOverText
		string mouseOverText
		{
			get
			{
				string rolloverHtml = "";

				if (this.UsrCount > 0 && this.UsrString != null)
				{
					rolloverHtml = "This is: " + this.UsrString;
				}

				string totalsText = "";
				if (this.Views > 0 || this.TotalComments > 0)
				{
					if (this.Views > 0)
						totalsText += this.Views.ToString() + " view" + (this.Views == 1 ? "" : "s");
					if (this.Views > 0 && this.TotalComments > 0)
						totalsText += ", ";
					if (this.TotalComments > 0)
						totalsText += this.TotalComments.ToString() + " comment" + (this.TotalComments == 1 ? "" : "s");
				}

				if (totalsText.Length > 0)
					rolloverHtml = rolloverHtml + (rolloverHtml.Length > 0 ? "<br>" : "") + totalsText;

				if (rolloverHtml.Length > 0)
				{
					rolloverHtml = "stt('" + HttpUtility.UrlEncodeUnicode("<b>" + rolloverHtml + "</b>").Replace("'", "\\'") + "');";
					//rolloverHtml = " onmouseover=\"stt('"+rolloverHtml+"');\" onmouseout=\"htm();\"";
				}
				return rolloverHtml;
			}

		}
		#endregion

		public string Rollover
		{
			get
			{
				return " onmouseover=\"" + mouseOverText + "\" onmouseout=\"htm();\"";
			}
		}

		public string RolloverMouseOverText
		{
			get
			{
				return mouseOverText;
			}
		}


		public void MakeRollover(HtmlControl c)
		{
			c.Attributes["onmouseover"] = mouseOverText;
			c.Attributes["onmouseout"] = "htm();";
		}
		public void MakeRollover(WebControl c)
		{
			c.Attributes["onmouseover"] = mouseOverText;
			c.Attributes["onmouseout"] = "htm();";
		}
		#endregion

		#region UpdateNextPreviousCache()
		public void UpdateNextPreviousCache()
		{
			Query q = new Query();
			q.QueryCondition = new And(
				Photo.EnabledQueryCondition,
				new Q(Photo.Columns.GalleryK, this.GalleryK)
				);
			q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
			q.NoLock = true;
			q.Columns = new ColumnSet(Photo.Columns.K, Photo.Columns.Icon, Photo.Columns.ContentDisabled);
			PhotoSet ps = new PhotoSet(q);
			int currentIndex = 0;
			for (int i = 0; i < ps.Count; i++)
			{
				if (ps[i].K == this.K)
				{
					currentIndex = i;
					break;
				}
			}
			for (int offset = 1; offset <= 3; offset++)
			{
				if (currentIndex - offset < 0)
				{
					if (ps.Count + currentIndex - offset >= ps.Count || ps.Count + currentIndex - offset < 0)
						this.SetNextPrevCache(0, offset, false);
					else
						this.SetNextPrevCache(ps[ps.Count + currentIndex - offset].K, offset, false);
				}
				else
					this.SetNextPrevCache(ps[currentIndex - offset].K, offset, false);

				if (currentIndex + offset >= ps.Count)
				{
					if (offset - ps.Count + currentIndex >= ps.Count || offset - ps.Count + currentIndex < 0)
						this.SetNextPrevCache(0, offset, true);
					else
						this.SetNextPrevCache(ps[offset - ps.Count + currentIndex].K, offset, true);
				}
				else
					this.SetNextPrevCache(ps[currentIndex + offset].K, offset, true);
			}
			this.Update();
		}

		public void SetNextPrevCache(int photoK, int offset, bool next)
		{
			if (offset == 1)
			{
				if (next)
				{
					this.NextPhoto1K = photoK;
				}
				else
				{
					this.PreviousPhoto1K = photoK;
				}
			}
			else if (offset == 2)
			{
				if (next)
				{
					this.NextPhoto2K = photoK;
				}
				else
				{
					this.PreviousPhoto2K = photoK;
				}
			}
			else if (offset == 3)
			{
				if (next)
				{
					this.NextPhoto3K = photoK;
				}
				else
				{
					this.PreviousPhoto3K = photoK;
				}
			}
		}
		#endregion

		#region MediaTypes
		public static MediaTypes GetMediaType(string filename)
		{
			//*.jpg, *.jpeg, *.jpe, *.gif, *.png
			//*.avi, *.dv, *.mov, *.qt, *.mpg, *.mpeg, *.mp4, *.3gp, *.asf, *.wmv
			//*.mp3, *.wav, *.wma

			filename = filename.ToLower();

			if (filename.EndsWith(".jpg")
				|| filename.EndsWith(".jpeg")
				|| filename.EndsWith(".jpe")
				|| filename.EndsWith(".gif")
				|| filename.EndsWith(".png"))
				return MediaTypes.Image;
			else if (filename.EndsWith(".avi")
				|| filename.EndsWith(".dv")
				|| filename.EndsWith(".mov")
				|| filename.EndsWith(".qt")
				|| filename.EndsWith(".mpg")
				|| filename.EndsWith(".mpeg")
				|| filename.EndsWith(".mp4")
				|| filename.EndsWith(".3gp")
				|| filename.EndsWith(".3g2")
				|| filename.EndsWith(".asf")
				|| filename.EndsWith(".wmv"))
				return MediaTypes.Video;
			else if (filename.EndsWith(".mp3")
				|| filename.EndsWith(".wav")
				|| filename.EndsWith(".wma")
				|| filename.EndsWith(".aac")
				|| filename.EndsWith(".aif")
				|| filename.EndsWith(".aiff"))
				return MediaTypes.Audio;
			else
				return MediaTypes.Unknown;
		}
		#endregion

		#region ProcessUploadedFile
		public static void ProcessUploadedFile(HttpPostedFile httpPostedFile, Gallery gallery, Random random, Usr usr, int rotate, string tags)
		{
			if (usr.CanUploadTo(gallery))
			{

				Photo photo = new Photo();
				try
				{

					#region Set general stuff
					photo.UsrK = usr.K;
					photo.RandomNumber = random.NextDouble();
					photo.WeightedCoolRating = 5.0;
					photo.WeightedSexyRating = 5.0;
					photo.Order = 5.0;
					if (tags.Length > 512)
						tags = tags.Substring(0, 512);
					photo.UploadTemporaryTags = tags;
					photo.Status = Photo.StatusEnum.Processing;
					#endregion
					#region Set gallery / event / article / url fragment stuff
					photo.GalleryK = gallery.K;
					if (gallery.Event != null)
					{
						photo.EventK = gallery.EventK;
						photo.DateTime = gallery.Event.DateTime;
						photo.ParentDateTime = gallery.Event.DateTime;
					}
					else if (gallery.Article != null)
					{
						photo.ArticleK = gallery.ArticleK;
						photo.DateTime = gallery.Article.AddedDateTime;
						photo.ParentDateTime = gallery.Article.AddedDateTime;
					}
					else
					{
						photo.DateTime = DateTime.Now;
						photo.ParentDateTime = gallery.CreateDateTime;
					}
					photo.UpdateUrlFragmentNoUpdate();
					#endregion

					photo.MediaType = Photo.GetMediaType(httpPostedFile.FileName);

					if (photo.MediaType.Equals(Photo.MediaTypes.Image))
					{
						#region Initialise Guids
						photo.UploadTemporary = Guid.NewGuid();
						photo.Master = Guid.NewGuid();
						photo.Web = Guid.NewGuid();
						photo.Thumb = Guid.NewGuid();
						photo.Icon = Guid.NewGuid();
						#endregion

						photo.Rotate = rotate;
						#region Extention
						try
						{
							string s = httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(".") + 1).ToLower();
							if (s.Length < 10)
							{
								if (s == "jpeg" || s == "jpe")
									photo.UploadTemporaryExtention = "jpg";
								else
									photo.UploadTemporaryExtention = s;
							}
							else
								photo.UploadTemporaryExtention = "jpg";
						}
						catch
						{
							photo.UploadTemporaryExtention = "jpg";
						}
						#endregion
						
					}
					else if (photo.MediaType.Equals(Photo.MediaTypes.Video))
					{
						#region Initialise Guids
						photo.UploadTemporary = Guid.NewGuid();
						photo.VideoMaster = Guid.NewGuid();
						photo.VideoMed = Guid.NewGuid();
						photo.Master = Guid.NewGuid();
						photo.Web = Guid.NewGuid();
						photo.Thumb = Guid.NewGuid();
						photo.Icon = Guid.NewGuid();
						#endregion

						#region Extention
						photo.VideoFileExtention = httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(".") + 1).ToLower();
						photo.UploadTemporaryExtention = photo.VideoFileExtention;
						#endregion

					}

					httpPostedFile.SaveAs(Storage.TemporaryFilesystemPath(photo.UploadTemporary, photo.UploadTemporaryExtention));

					photo.Status = Photo.StatusEnum.Processing;
					photo.Update();

					if (!gallery.WatchUploads.HasValue || gallery.WatchUploads.Value)
					{
						CommentAlert.Enable(usr, photo.K, Model.Entities.ObjectType.Photo);
					}


				}
				catch (Exception exc)
				{
					#region Send an email to admin
					try
					{
					    Mailer m = new Mailer();
					    m.Subject = "Exception uploading photo from " + System.Environment.MachineName + "...";
					    m.Body = "<p>" + exc.ToString() + "</p>";
					    try
					    {
					        m.Body += "<p>The content length was " + httpPostedFile.ContentLength.ToString("#,##0") + " bytes</p>";
					    }
					    catch { }
					    m.UsrRecipient = new Usr(4);
					    m.To = "dave@dontstayin.com";
					    m.Send();
					}
					catch { }
					#endregion

					#region Delete file
					try
					{
						Storage.RemoveFromStore(Storage.Stores.Temporary, photo.UploadTemporary, photo.UploadTemporaryExtention);
					}
					catch { }
					#endregion

					photo.Delete();

					//try
					//{
					//    if (httpPostedFile.FileName.LastIndexOf(".") > -1)
					//        httpPostedFile.SaveAs(@"C:\FailedPix\" + Guid.NewGuid() + httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf(".")));
					//    else
					//        httpPostedFile.SaveAs(@"C:\FailedPix\" + Guid.NewGuid());
					//}
					//catch (Exception ex)
					//{
					//    Utilities.AdminEmailAlert("Error writing failed photo file to disk", "Error writing failed photo file to disk", ex);
					//}

				}
				//finally
				//{
				//    CurrentGallery.UpdateStats(null, true);
				//    CurrentGallery.UpdatePhotoOrder(null);
				//    if (CurrentGallery.Event != null) CurrentGallery.Event.UpdateTotalPhotos(null);
				//}
			}
		}

		#region FinishProcessUploadedFile - updates status and photo / gallery / usr stats
		public static void FinishProcessUploadedFile(Photo p, Gallery g, Usr u)
		{

			bool reEncoded = false;
			if (p.EnabledByUsrK > 0)
				reEncoded = true;

			#region Update status
			if ((u.TotalPhotoUploads > 50 && u.AbuseAccusationsPending == 0 && !u.ModeratePhotos) || reEncoded || u.IsAdmin)
			{
				p.Status = Photo.StatusEnum.Enabled;
				if (!reEncoded)
				{
					p.EnabledDateTime = DateTime.Now;
					p.EnabledByUsrK = u.K;
				}
			}
			else
			{
				p.Status = Photo.StatusEnum.Moderate;
			}
			#endregion

			#region Set tags
			string[] tagsFromClient = p.UploadTemporaryTags.Split(',', ';', '"', '\n');
			foreach (string tagText in tagsFromClient)
			{
				string s = tagText.Trim();
				if (s.Length > 0)
				{
					try
					{
						Tag.AddTag(s, p, u);
					}
					catch { }

				}
			}
			#endregion

			p.Update();

			#region Update gallery other photos in the gallery
			if (g.TotalPhotos == 0)
				g.MainPhotoK = p.K;

			g.UpdateStats(null, false);

			if (p.Status.Equals(Photo.StatusEnum.Enabled))
			{
				g.UpdatePhotoOrder(null);
				if (!reEncoded)
					g.LastLiveDateTime = DateTime.Now;

				if (g.Event != null)
					g.Event.UpdateTotalPhotos(null);
			}

			g.Update();
			#endregion

			if (!reEncoded)
				u.LastPhotoUpload = DateTime.Now;

			u.UpdateTotalPhotos(null);

			if (p.Status == Photo.StatusEnum.Enabled)
			{
				p.SendPhotoChatAlerts();
			}

		}
		#endregion

		#endregion

		public void SendPhotoChatAlerts()
		{
			PhotoStub ps = new PhotoStub(
				Guid.NewGuid().Pack(),
				ItemType.PhotoAlert,
				DateTime.Now.Ticks.ToString(),
				//new Chat.RoomSpec(RoomType.NewPhotosAll).Guid.Pack(),
				new Chat.RoomSpec(RoomType.PublicStream).Guid.Pack(),
				this.WebWidth,
				this.WebHeight,
				this.Url(),
				this.Web.ToString(),
				this.Icon.ToString(),
				this.Thumb.ToString(),
				this.ThumbWidth,
				this.ThumbHeight,
				false);
			Chat.SendJsonChatItem(ps);

			//ps.guid = Guid.NewGuid().Pack();
			//ps.roomGuid = new Chat.RoomSpec(RoomType.PublicStream).Guid.Pack();
			//Chat.SendJsonChatItem(ps);

			//if (this.Usr.IsSpotter && !this.Usr.IsProSpotter)
			//{
			//	ps.guid = Guid.NewGuid().Pack();
			//	ps.roomGuid = new Chat.RoomSpec(RoomType.NewPhotosSpotters).Guid.Pack();
			//	Chat.SendJsonChatItem(ps);
			//}

			//if (this.Usr.IsProSpotter)
			//{
			//    ps.guid = Guid.NewGuid().Pack();
			//    ps.roomGuid = new Chat.RoomSpec(RoomType.NewPhotosProSpotters).Guid.Pack();
			//    Chat.SendJsonChatItem(ps);
			//}

			//if (this.MediaType == MediaTypes.Video)
			//{
			//    ps.guid = Guid.NewGuid().Pack();
			//    ps.roomGuid = new Chat.RoomSpec(RoomType.NewVideosAll).Guid.Pack();
			//    Chat.SendJsonChatItem(ps);
			//}

			//ps.guid = Guid.NewGuid().Pack();
			//ps.roomGuid = new Chat.RoomSpec(RoomType.BuddyAlerts, Model.Entities.ObjectType.Usr, this.UsrK).Guid.Pack();
			//ps.buddyAlert = true;
			//Chat.SendJsonChatItem(ps);
		
		}

		#region AddInstantPhotoChatItems()
		public void AddInstantPhotoChatItems()
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlNode n = xmlDoc.CreateElement("instantPhoto");
			n.InnerText = "<a href=\"" + this.Event.Url() + "\">" + this.Event.Name + "</a> @ <a href=\"" + this.Event.Venue.Url() + "\">" + this.Event.Venue.Name + "</a> in <a href=\"" + this.Event.Venue.Place.Url() + "\">" + this.Event.Venue.Place.FriendlyName + "</a>";
			n.AddAttribute("nickName", this.Usr.NickNameSafe);
			n.AddAttribute("stmu", this.Usr.StmuParams);
			n.AddAttribute("usrK", this.Usr.K.ToString());
			if (this.Usr.HasPic)
				n.AddAttribute("pic", this.Usr.Pic.ToString());
			else
				n.AddAttribute("pic", "0");
			n.AddAttribute("icon", this.Icon.ToString());

			n.AddAttribute("k", this.K.ToString());


			DateTime dt = DateTime.Now;

		//	Chat.SendChatItem(ItemType.PhotoAlert, n, dt.Ticks, this.Usr.K, Chat.PUBLIC_ALERTS_CHAT_ROOM_GUID);

			//Usr.AddChatItemStatic(ChatMessage.ItemTypes.InstantPhotoAlert, n, dt.Ticks, chatRoomGuid, this.Usr.K, us);
		}

		static XmlAttribute ReturnAttribute(XmlNode node, XmlDocument xmlDoc, string key, string val)
		{
			XmlAttribute att = xmlDoc.CreateAttribute(key);
			att.Value = val;
			node.Attributes.Append(att);
			return att;
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
			if (objectType.Equals(Model.Entities.ObjectType.Photo) && this.K == objectK)
				return true;

			if (objectType.Equals(Model.Entities.ObjectType.Gallery) && this.GalleryK == objectK)
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
			if (o.Equals(Model.Entities.ObjectType.Gallery))
				return true;

			if (Gallery.CanBeConnectedToStatic(o))
				return true;

			return false;
		}
		public bool CanBeConnectedTo(Model.Entities.ObjectType o)
		{
			return Photo.CanBeConnectedToStatic(o);
		}
		#endregion


		#region SaveWeb()
		public void SaveWeb(SaveWebFileSourceLocations fileSource, int rotate)
		{
			SaveWeb(fileSource, rotate, null, null, false, true);
		}
		public void SaveWeb(SaveWebFileSourceLocations fileSource, int rotate, Photo.EncoderStatusDelegate Status, Photo.MeaningfulActivityDelegate Active)
		{
			SaveWeb(fileSource, rotate, Status, Active, Status != null, true);
		}
		public void SaveWeb(SaveWebFileSourceLocations fileSource, int rotate, Photo.EncoderStatusDelegate Status, Photo.MeaningfulActivityDelegate Active, bool allowEnlargedWeb)
		{
			SaveWeb(fileSource, rotate, Status, Active, Status != null, allowEnlargedWeb);
		}
		private void SaveWeb(SaveWebFileSourceLocations fileSource, int rotate, Photo.EncoderStatusDelegate Status, Photo.MeaningfulActivityDelegate Active, bool updateStatus, bool allowEnlargedWeb)
		{
			int webMaxSide = 600;
			int thumbMaxSide = 180;

			System.Drawing.Image image = null;

			#region Get input image
			if (updateStatus)
			{
				Active();
				Status(this.K + " Getting image...");
				this.ProcessingProgress = 10;
				this.ProcessingLastChange = DateTime.Now;
				this.Update();
			}

			if (fileSource == SaveWebFileSourceLocations.UploadTemporary)
			{
				image = System.Drawing.Image.FromStream(new MemoryStream(Storage.GetFromStore(Storage.Stores.Temporary, this.UploadTemporary, this.UploadTemporaryExtention)));
			}
			else
			{
				image = System.Drawing.Image.FromStream(new MemoryStream(Storage.GetFromStore(Storage.Stores.Master, this.Master, "jpg")));
			}
			#endregion

			try
			{

				#region Convert any image with an indexed pixel format to JPG
				if (image.PixelFormat.Equals(PixelFormat.Format1bppIndexed)
					|| image.PixelFormat.Equals(PixelFormat.Format4bppIndexed)
					|| image.PixelFormat.Equals(PixelFormat.Format8bppIndexed)
					|| image.PixelFormat.Equals(PixelFormat.Undefined)
					|| image.PixelFormat.Equals(PixelFormat.DontCare)
					|| image.PixelFormat.Equals(PixelFormat.Format16bppArgb1555)
					|| image.PixelFormat.Equals(PixelFormat.Format16bppGrayScale))
				{

					if (updateStatus)
					{
						Active();
						Status(this.K + " Converting to indexed pixel format...");
						this.ProcessingProgress = 11;
						this.ProcessingLastChange = DateTime.Now;
						this.Update();
					}

					MemoryStream converterStream = new MemoryStream();

					ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageEncoders()[1];
					EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(2);
					encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
					encoderParams.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, 24L);
					image.Save(converterStream, jpgEncoder, encoderParams);

					image.Dispose();
					image = null;
					image = System.Drawing.Image.FromStream(converterStream);
				}
				#endregion

				#region Rotate image
				if (rotate > 0 && updateStatus)
				{
					Active();
					Status(this.K + " Rotating...");
					this.ProcessingProgress = 12;
					this.ProcessingLastChange = DateTime.Now;
					this.Update();
				}
				if (rotate == 90) { image.RotateFlip(RotateFlipType.Rotate90FlipNone); }
				else if (rotate == 180) { image.RotateFlip(RotateFlipType.Rotate180FlipNone); }
				else if (rotate == 270) { image.RotateFlip(RotateFlipType.Rotate270FlipNone); }
				#endregion

				#region Save Master
				if (fileSource != SaveWebFileSourceLocations.Master)
				{
					if (updateStatus)
					{
						Active();
						Status(this.K + " Saving master...");
						this.ProcessingProgress = 20;
						this.ProcessingLastChange = DateTime.Now;
						this.Update();
					}

					byte[] masterBytes = SaveJPGWithCompressionSetting(image, (long)85.0);
					Storage.AddToStore(masterBytes, Storage.Stores.Master, this.Master, "jpg", this, "Master");
					this.MasterFileSize = masterBytes.Length;
				}
				#endregion

				#region Extract metadata
				if (fileSource == SaveWebFileSourceLocations.UploadTemporary)
				{
					try
					{
						if (updateStatus)
						{
							Active();
							Status(this.K + " Extracting exif meta data...");
							this.ProcessingProgress = 30;
							this.ProcessingLastChange = DateTime.Now;
							this.Update();
						}

						Cambro.Misc.ExifMetadata MyExifMetadata = new Cambro.Misc.ExifMetadata();
						Cambro.Misc.ExifMetadata.Metadata MyMetaData;
						MyMetaData = MyExifMetadata.GetExifMetadata(image);

						string date = MyMetaData.DatePictureTaken.DisplayValue;

						try
						{
							this.CameraModel = MyMetaData.CameraModel.DisplayValue.Truncate(100);
						}
						catch { }

						try
						{
							this.EquipmentMake = MyMetaData.EquipmentMake.DisplayValue.Truncate(100);
						}
						catch { }

						if (this.EquipmentMake.StartsWith("Sony Ericsson") && this.CameraModel.StartsWith("K800i"))
							this.IsSonyK800i = true;

						if (this.EquipmentMake.StartsWith("Sony Ericsson") && this.CameraModel.StartsWith("C902"))
							this.IsSonyC902 = true;

						int year = int.Parse(date.Substring(0, 4));
						int month = int.Parse(date.Substring(5, 2));
						int day = int.Parse(date.Substring(8, 2));
						int hour = int.Parse(date.Substring(11, 2));
						int min = int.Parse(date.Substring(14, 2));
						int sec = int.Parse(date.Substring(17, 2));

						DateTime newDateTime = new DateTime(year, month, day, hour, min, sec);
						DateTime parentDateTime = DateTime.Now;
						if (this.Event != null)
						{
							parentDateTime = this.Event.DateTime;
							TimeSpan difference = this.DateTime - newDateTime;
							if (Math.Abs(difference.TotalDays) < 10)
							{
								this.DateTime = newDateTime;
							}
						}
						else if (this.Article != null)
						{
							parentDateTime = this.Article.AddedDateTime;
							TimeSpan difference = this.DateTime - newDateTime;
							if (Math.Abs(difference.TotalDays) < 60)
							{
								this.DateTime = newDateTime;
							}
						}
					}
					catch
					{
						if (updateStatus)
						{
							Active();
							Status(this.K + " Assigning default meta data...");
							this.ProcessingProgress = 35;
							this.ProcessingLastChange = DateTime.Now;
							this.Update();
						}

						if (this.Event != null)
						{ this.DateTime = this.Event.DateTime; }
						else if (this.Article != null)
						{ this.DateTime = this.Article.AddedDateTime; }
						else
						{ this.DateTime = DateTime.Now; }
					}
				}
				#endregion

				#region Calculate the dimensions and landscape/portrait
				if (updateStatus)
				{
					Active();
					Status(this.K + " Calculating sizes of new images...");
					this.ProcessingProgress = 40;
					this.ProcessingLastChange = DateTime.Now;
					this.Update();
				}

				this.OriginalHeight = image.Height;
				this.OriginalWidth = image.Width;
				this.IsLandscape = !(image.Height > image.Width);
				#endregion

				#region Save Web, Thumb and Icon
				#region Overlays...
				Overlays webOverlay = Overlays.DsiLogoBottomRight;
				if (this.IsSonyK800i)
					webOverlay = Overlays.DsiLogoBottomRightSonyBottomLeft;
				else if (this.IsSonyC902)
					webOverlay = Overlays.DsiLogoBottomRightSonyC902BottomLeft;

				this.Overlay = webOverlay;
				Overlays thumbOverlay = Overlays.None;
				Overlays iconOverlay = Overlays.None;
				if (this.MediaType.Equals(MediaTypes.Video))
				{
					webOverlay = Overlays.PlayButtonLarge;
					thumbOverlay = Overlays.PlayButtonSmall;
					iconOverlay = Overlays.PlayButtonSmall;
				}
				#endregion

				#region Web
				if (updateStatus)
				{
					Active();
					Status(this.K + " Creating web image...");
					this.ProcessingProgress = 50;
					this.ProcessingLastChange = DateTime.Now;
					this.Update();
				}
				OperationReturn web = Operation(image, OperationType.MaxSide, new OperationParams() { MaxSide = webMaxSide, Overlay = webOverlay, ReturnBytes = true, AllowMaxSideToEnlarge = allowEnlargedWeb });
				this.WebWidth = web.ImageSize.Width;
				this.WebHeight = web.ImageSize.Height;
				Storage.AddToStore(web.Bytes, Storage.Stores.Pix, this.Web, "jpg", this, "Web");
				#endregion

				#region Thumb
				if (updateStatus)
				{
					Active();
					Status(this.K + " Creating thumb image...");
					this.ProcessingProgress = 60;
					this.ProcessingLastChange = DateTime.Now;
					this.Update();
				}
				OperationReturn thumb = Operation(image, OperationType.MaxSide, new OperationParams() { MaxSide = thumbMaxSide, Overlay = thumbOverlay, ReturnBytes = true, AllowMaxSideToEnlarge = true });
				this.ThumbWidth = thumb.ImageSize.Width;
				this.ThumbHeight = thumb.ImageSize.Height;
				Storage.AddToStore(thumb.Bytes, Storage.Stores.Pix, this.Thumb, "jpg", this, "Thumb");
				#endregion

				#region Icon
				if (updateStatus)
				{
					Active();
					Status(this.K + " Creating icon image...");
					this.ProcessingProgress = 70;
					this.ProcessingLastChange = DateTime.Now;
					this.Update();
				}
				OperationReturn icon = Operation(image, OperationType.FixedSize, new OperationParams() { FixedSize = new Size(100, 100), Overlay = iconOverlay, ReturnBytes = true });
				Storage.AddToStore(icon.Bytes, Storage.Stores.Pix, this.Icon, "jpg", this, "Icon");
				#endregion
				#endregion

				#region Delete temporary upload file
				if (fileSource == SaveWebFileSourceLocations.UploadTemporary)
				{
					if (updateStatus)
					{
						Active();
						Status(this.K + " Deleting temporary upload file...");
						this.ProcessingProgress = 80;
						this.ProcessingLastChange = DateTime.Now;
						this.Update();
					}

					//Delete will fail unless we dispose here as well as in the finally block!!!
					image.Dispose();

					Storage.RemoveFromStore(Storage.Stores.Temporary, this.UploadTemporary, this.UploadTemporaryExtention);
				}
				#endregion

			}
			finally
			{
				if (image != null) image.Dispose();
			}

			this.Update();

			#region Done!
			if (updateStatus)
			{
				Active();
				Status(this.K + " Done!");
				this.ProcessingProgress = 100;
				this.ProcessingLastChange = DateTime.Now;
				this.Update();
			}
			#endregion
		}
		#endregion

		#region Operation
		public class OperationParams
		{
			public int MaxSide { get; set; }
			public Size FixedSize { get; set; }
			public Size CropSize { get; set; }
			public Size CropResize { get; set; }
			public Size CropOffset { get; set; }
			public Overlays Overlay { get; set; }
			public string FileName { get; set; }
			public bool ReturnBytes { get; set; }
			public bool AllowMaxSideToEnlarge { get; set; }

		}
		public class OperationReturn
		{
			public Size ImageSize { get; set; }
			public byte[] Bytes { get; set; }
		}
		public static OperationReturn Operation(System.Drawing.Image image, OperationType type, OperationParams parameters)
		{
			System.Drawing.Image cropImage = null;
			System.Drawing.Image newImage = null;
			System.Drawing.Image zoomedImage = null;
			System.Drawing.Image overlayImage = null;
			System.Drawing.Image sonyOverlayImage = null;
			System.Drawing.Image thinkBottomLeftOverlayImage = null;
			System.Drawing.Image thinkTextBottomLeftOverlayImage = null;
			Graphics cropGraphic = null;
			Graphics newFullGraphic = null;
			Graphics zoomedGraphic = null;

			try
			{

				#region Convert any image with an indexed pixel format to JPG
				if (image.PixelFormat.Equals(PixelFormat.Format1bppIndexed)
					|| image.PixelFormat.Equals(PixelFormat.Format4bppIndexed)
					|| image.PixelFormat.Equals(PixelFormat.Format8bppIndexed)
					|| image.PixelFormat.Equals(PixelFormat.Undefined)
					|| image.PixelFormat.Equals(PixelFormat.DontCare)
					|| image.PixelFormat.Equals(PixelFormat.Format16bppArgb1555)
					|| image.PixelFormat.Equals(PixelFormat.Format16bppGrayScale))
				{

					MemoryStream converterStream = new MemoryStream();

					ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageEncoders()[1];
					EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(2);
					encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
					encoderParams.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, 24L);
					
					image.Save(converterStream, jpgEncoder, encoderParams);

					image.Dispose();
					image = null;
					image = System.Drawing.Image.FromStream(converterStream);
				}
				#endregion

				int CropWidth = 0;
				int CropHeight = 0;
				int CropX = 0;
				int CropY = 0;
				int ResizeWidth = 0;
				int ResizeHeight = 0;

				bool portrait = image.Height > image.Width;

				#region Work out crop / zoom image sizes from input data
				if (type == OperationType.Crop)
				{
					CropWidth = parameters.CropSize.Width;
					CropHeight = parameters.CropSize.Height;
					CropX = parameters.CropOffset.Width;
					CropY = parameters.CropOffset.Height;
					ResizeWidth = parameters.CropResize.Width;
					ResizeHeight = parameters.CropResize.Height;
					if (CropWidth > ResizeWidth || CropHeight > ResizeHeight)
					{
						double horizontalScale = (double)CropWidth / (double)ResizeWidth;
						double verticalScale = (double)CropHeight / (double)ResizeHeight;
						if (horizontalScale > verticalScale)
						{
							ResizeWidth = CropWidth;
							ResizeHeight = (int)Math.Ceiling(ResizeHeight * horizontalScale);
						}
						else
						{
							ResizeWidth = (int)Math.Ceiling(ResizeWidth * verticalScale);
							ResizeHeight = CropHeight;
						}
					}
					if (CropX + CropWidth > ResizeWidth)
						CropX = ResizeWidth - CropWidth;
					if (CropX < 0)
						CropX = 0;
					if (CropY + CropHeight > ResizeHeight)
						CropY = ResizeHeight - CropHeight;
					if (CropY < 0)
						CropY = 0;
				}
				else if (type == OperationType.MaxSide)
				{
					if (portrait)
					{
						if (parameters.AllowMaxSideToEnlarge || image.Height > parameters.MaxSide)
						{
							CropHeight = parameters.MaxSide;
							CropWidth = (int)Math.Floor(((double)parameters.MaxSide / (double)image.Height) * image.Width);
						}
						else
						{
							CropHeight = image.Height;
							CropWidth = image.Width;
						}
					}
					else
					{
						if (parameters.AllowMaxSideToEnlarge || image.Width > parameters.MaxSide)
						{
							CropWidth = parameters.MaxSide;
							CropHeight = (int)Math.Floor(((double)parameters.MaxSide / (double)image.Width) * image.Height);
						}
						else
						{
							CropHeight = image.Height;
							CropWidth = image.Width;
						}
					}
					ResizeWidth = CropWidth;
					ResizeHeight = CropHeight;
					CropX = 0;
					CropY = 0;
				}
				else
				{
					CropHeight = parameters.FixedSize.Height;
					CropWidth = parameters.FixedSize.Width;
					if (portrait)
					{
						ResizeWidth = CropWidth;
						ResizeHeight = (int)Math.Floor(((double)parameters.FixedSize.Width / (double)image.Width) * image.Height);
					}
					else
					{
						ResizeHeight = CropHeight;
						ResizeWidth = (int)Math.Floor(((double)parameters.FixedSize.Height / (double)image.Height) * image.Width);
					}
					CropX = (int)Math.Floor((ResizeWidth - CropWidth) / 2.0);
					CropY = (int)Math.Floor((ResizeHeight - CropHeight) / 2.0);
				}
				#endregion

				#region If we are within 2 pixels of the edge of the image, we have to do this nasty kludge to avoid a black halo
				if (CropX < 2 || CropY < 2 || (CropX + CropWidth > ResizeWidth - 2) || (CropY + CropHeight > ResizeHeight - 2))
				{

					int size = (int)Math.Ceiling((((double)image.Width / (double)ResizeWidth) * 5));
					newImage = new Bitmap(image.Width + (2 * size), image.Height + (2 * size), image.PixelFormat);

					newFullGraphic = Graphics.FromImage(newImage);
					newFullGraphic.CompositingMode = CompositingMode.SourceCopy;
					newFullGraphic.PixelOffsetMode = PixelOffsetMode.None;
					newFullGraphic.CompositingQuality = CompositingQuality.Default;
					newFullGraphic.SmoothingMode = SmoothingMode.None;
					newFullGraphic.InterpolationMode = InterpolationMode.NearestNeighbor;

					newFullGraphic.DrawImage(image, new Rectangle(size, size, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);//Full image

					newFullGraphic.DrawImage(image, new Rectangle(0, size, size, image.Height), new Rectangle(0, 0, 1, image.Height), GraphicsUnit.Pixel);//left
					newFullGraphic.DrawImage(image, new Rectangle(image.Width + size, size, size, image.Height), new Rectangle(image.Width - 2, 0, 1, image.Height), GraphicsUnit.Pixel);//right
					newFullGraphic.DrawImage(image, new Rectangle(size, 0, image.Width, size), new Rectangle(0, 0, image.Width, 1), GraphicsUnit.Pixel);//top
					newFullGraphic.DrawImage(image, new Rectangle(size, image.Height + size, image.Width, size), new Rectangle(0, image.Height - 2, image.Width, 1), GraphicsUnit.Pixel);//bottom

					newFullGraphic.DrawImage(image, new Rectangle(0, 0, size, size), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);//top-left
					newFullGraphic.DrawImage(image, new Rectangle(image.Width + size, 0, size, size), new Rectangle(image.Width - 2, 0, 1, 1), GraphicsUnit.Pixel);//top-right
					newFullGraphic.DrawImage(image, new Rectangle(0, image.Height + size, size, size), new Rectangle(0, image.Height - 2, 1, 1), GraphicsUnit.Pixel);//bottom-left
					newFullGraphic.DrawImage(image, new Rectangle(image.Width + size, image.Height + size, size, size), new Rectangle(image.Width - 2, image.Height - 2, 1, 1), GraphicsUnit.Pixel);//bottom-right

					CropX += 5;
					CropY += 5;
					ResizeWidth += 10;
					ResizeHeight += 10;

				}
				else
				{
					newImage = image;
				}
				#endregion

				#region Resize...
				zoomedImage = new Bitmap(ResizeWidth, ResizeHeight, newImage.PixelFormat);
				zoomedGraphic = Graphics.FromImage(zoomedImage);
				ApplyGraphicQualityProperties(zoomedGraphic);
				Rectangle zoomedRectangle = new Rectangle(0, 0, ResizeWidth, ResizeHeight);
				zoomedGraphic.DrawImage(newImage, zoomedRectangle);
				#endregion

				#region Crop...
				Rectangle cropRectangle = new Rectangle(CropX, CropY, CropWidth, CropHeight);
				cropImage = new Bitmap(CropWidth, CropHeight, newImage.PixelFormat);
				cropGraphic = Graphics.FromImage(cropImage);
				ApplyGraphicQualityProperties(cropGraphic);
				Rectangle destRectangle = new Rectangle(0, 0, CropWidth, CropHeight);
				cropGraphic.DrawImage(zoomedImage, destRectangle, cropRectangle, GraphicsUnit.Pixel);
				#endregion

				#region Overlay...
				if (
					parameters.Overlay.Equals(Overlays.DsiLogoBottomRight) //||
					//parameters.Overlay.Equals(Overlays.DsiLogoBottomRightSonyBottomLeft) ||
					//parameters.Overlay.Equals(Overlays.DsiLogoBottomRightSonyC902BottomLeft) ||
					//parameters.Overlay.Equals(Overlays.DsiLogoBottomRightThinkBottomLeft) ||
					//parameters.Overlay.Equals(Overlays.DsiLogoBottomRightThinkTextBottomLeft)
					)
				{
					overlayImage = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Bobs.Overlay.new-logo-overlay.png"));
					Rectangle overlayRectangle = new Rectangle(0, 0, overlayImage.Width, overlayImage.Height);
					Rectangle destinationRectangle = new Rectangle(6, CropHeight - overlayImage.Height - 6, overlayImage.Width, overlayImage.Height);
					cropGraphic.DrawImage(overlayImage, destinationRectangle, overlayRectangle, GraphicsUnit.Pixel);

					//if (parameters.Overlay.Equals(Overlays.DsiLogoBottomRightSonyC902BottomLeft) &&
					//    (Vars.DevEnv || (DateTime.Now >= new DateTime(2008, 07, 01) && DateTime.Now < new DateTime(2008, 10, 01)))
					//    )
					//{
					//    string sonyOverlayFullFileName = Vars.MapPath("~/gfx/overlay-sonyc902.png");
					//    sonyOverlayImage = new Bitmap(sonyOverlayFullFileName);
					//    Rectangle sonyOverlayRectangle = new Rectangle(0, 0, sonyOverlayImage.Width, sonyOverlayImage.Height);
					//    Rectangle sonyDestinationRectangle = new Rectangle(5, CropHeight - sonyOverlayImage.Height - 5, sonyOverlayImage.Width, sonyOverlayImage.Height);
					//    cropGraphic.DrawImage(sonyOverlayImage, sonyDestinationRectangle, sonyOverlayRectangle, GraphicsUnit.Pixel);
					//}

					//if (parameters.Overlay.Equals(Overlays.DsiLogoBottomRightSonyBottomLeft) &&
					//    (Vars.DevEnv || (DateTime.Now >= new DateTime(2006, 08, 01) && DateTime.Now < new DateTime(2006, 10, 01)))
					//    )
					//{
					//    string sonyOverlayFullFileName = Vars.MapPath("~/gfx/sony-overlay.png");
					//    sonyOverlayImage = new Bitmap(sonyOverlayFullFileName);
					//    Rectangle sonyOverlayRectangle = new Rectangle(0, 0, sonyOverlayImage.Width, sonyOverlayImage.Height);
					//    Rectangle sonyDestinationRectangle = new Rectangle(0, CropHeight - sonyOverlayImage.Height, sonyOverlayImage.Width, sonyOverlayImage.Height);
					//    cropGraphic.DrawImage(sonyOverlayImage, sonyDestinationRectangle, sonyOverlayRectangle, GraphicsUnit.Pixel);
					//}

					//if (parameters.Overlay.Equals(Overlays.DsiLogoBottomRightThinkBottomLeft))
					//{
					//    string extraOverlayFullFileName = Vars.MapPath("~/gfx/think-overlay.png");
					//    thinkBottomLeftOverlayImage = new Bitmap(extraOverlayFullFileName);
					//    Rectangle extraOverlayRectangle = new Rectangle(0, 0, thinkBottomLeftOverlayImage.Width, thinkBottomLeftOverlayImage.Height);
					//    Rectangle extraDestinationRectangle = new Rectangle(5, CropHeight - thinkBottomLeftOverlayImage.Height - 5, thinkBottomLeftOverlayImage.Width, thinkBottomLeftOverlayImage.Height);
					//    cropGraphic.DrawImage(thinkBottomLeftOverlayImage, extraDestinationRectangle, extraOverlayRectangle, GraphicsUnit.Pixel);
					//}

					//if (parameters.Overlay.Equals(Overlays.DsiLogoBottomRightThinkTextBottomLeft))
					//{
					//    string extraOverlayFullFileName = Vars.MapPath("~/gfx/thinktext-overlay.png");
					//    thinkTextBottomLeftOverlayImage = new Bitmap(extraOverlayFullFileName);
					//    Rectangle extraOverlayRectangle = new Rectangle(0, 0, thinkTextBottomLeftOverlayImage.Width, thinkTextBottomLeftOverlayImage.Height);
					//    Rectangle extraDestinationRectangle = new Rectangle(0, CropHeight - thinkTextBottomLeftOverlayImage.Height - 12, thinkTextBottomLeftOverlayImage.Width, thinkTextBottomLeftOverlayImage.Height);
					//    cropGraphic.DrawImage(thinkTextBottomLeftOverlayImage, extraDestinationRectangle, extraOverlayRectangle, GraphicsUnit.Pixel);
					//}

				}
				else if (parameters.Overlay.Equals(Overlays.PlayButtonLarge) || parameters.Overlay.Equals(Overlays.PlayButtonSmall))
				{
					
					if (parameters.Overlay.Equals(Overlays.PlayButtonLarge))
						overlayImage = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Bobs.Overlay.video-button.png"));
					else
						overlayImage = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Bobs.Overlay.video-button-small.png"));

					Rectangle overlayRectangle = new Rectangle(0, 0, overlayImage.Width, overlayImage.Height);
					Rectangle destinationRectangle = new Rectangle((int)Math.Floor((CropWidth / 2.0) - (overlayImage.Width / 2.0)), (int)Math.Floor((CropHeight / 2.0) - (overlayImage.Height / 2.0)), overlayImage.Width, overlayImage.Height);
					cropGraphic.DrawImage(overlayImage, destinationRectangle, overlayRectangle, GraphicsUnit.Pixel);
				}
				#endregion

				if (parameters.FileName != null && parameters.FileName.Length > 0)
					SaveJPGWithCompressionSetting(cropImage, parameters.FileName, (long)85.0);

				OperationReturn operationReturn = new OperationReturn();

				operationReturn.ImageSize = new Size(cropImage.Width, cropImage.Height);

				if (parameters.ReturnBytes)
					operationReturn.Bytes = SaveJPGWithCompressionSetting(cropImage, (long)85.0);

				return operationReturn;
			}
			finally
			{
				if (cropImage != null) cropImage.Dispose();
				if (newImage != null) newImage.Dispose();
				if (zoomedImage != null) zoomedImage.Dispose();
				if (overlayImage != null) overlayImage.Dispose();
				if (sonyOverlayImage != null) sonyOverlayImage.Dispose();
				if (thinkBottomLeftOverlayImage != null) thinkBottomLeftOverlayImage.Dispose();
				if (thinkTextBottomLeftOverlayImage != null) thinkTextBottomLeftOverlayImage.Dispose();
				if (cropGraphic != null) cropGraphic.Dispose();
				if (newFullGraphic != null) newFullGraphic.Dispose();
				if (zoomedGraphic != null) zoomedGraphic.Dispose();
			}
		}
		public static void ApplyGraphicQualityProperties(Graphics g)
		{
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
		}

		public static void SaveJPGWithCompressionSetting(System.Drawing.Image image, string szFileName, long lCompression)
		{
			SaveJPGWithCompressionSetting(image, szFileName, null, lCompression);
		}

		public static void SaveJPGWithCompressionSetting(System.Drawing.Image image, Stream szStream, long lCompression)
		{
			SaveJPGWithCompressionSetting(image, "", szStream, lCompression);
		}

		public static byte[] SaveJPGWithCompressionSetting(System.Drawing.Image image, long lCompression)
		{
			MemoryStream ms = new MemoryStream(4096);
			SaveJPGWithCompressionSetting(image, "", ms, lCompression);
			return ms.ToArray();
		}

		public static void SaveJPGWithCompressionSetting(System.Drawing.Image image, string filename, Stream stream, long lCompression)
		{
			using (EncoderParameters eps = new EncoderParameters(1))
			{
				eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, lCompression);
				ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
				if (stream == null)
					image.Save(filename, ici, eps);
				else
					image.Save(stream, ici, eps);
			}
		}

		public static ImageCodecInfo GetEncoderInfo(String mimeType)
		{
			int j;
			ImageCodecInfo[] encoders;
			encoders = ImageCodecInfo.GetImageEncoders();
			for (j = 0; j < encoders.Length; ++j)
			{
				if (encoders[j].MimeType == mimeType)
					return encoders[j];
			}
			return null;
		}
		#endregion

		#region IncrementViews()
		public void IncrementViews()
		{
			this.Views++;
			this.Update();

			Log.Increment(Log.Items.PhotoImpressions);
			try
			{
				if (!Visit.Current.IsCrawler)
				{
					Log.Increment(Log.Items.PhotoImpressionsNoCrawlers);
				}
			}
			catch{}

		}
		#endregion

		#region GalleryUrl
		public string GalleryUrl(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "photo", this.K.ToString() }, par);
			return this.Gallery.Url(fullParams);
		}
		#endregion

		//#region PagedGalleryUrl
		//public string PagedGalleryUrl(params string[] par)
		//{
		//    string order = "DateTime";
		//    if (Prefs.Current["PhotoOrder"].Exists)
		//        order = Prefs.Current["PhotoOrder"];

		//    int orderIndex = this.GalleryRatingOrder;
		//    if (order.Equals("DateTime"))
		//        orderIndex = this.GalleryTimeOrder;

		//    int pageSize = Vars.GalleryPageSize;

		//    int pageNumber = (orderIndex / pageSize) + 1;

		//    string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "P", pageNumber.ToString() }, par);
		//    return this.Gallery.PagedUrl(fullParams);
		//}
		//#endregion

		#region StatusEnum
		#endregion

		#region EnabledQueryCondition
		public static Q EnabledQueryCondition
		{
			get
			{
				return new Q(Photo.Columns.Status, Photo.StatusEnum.Enabled);
			}
		}
		#endregion

		public static Pair[] DefaultOrder
		{
			get
			{
				return new List<Pair>(){
					new Pair(Photo.Columns.ParentDateTime, OrderBy.OrderDirection.Ascending),
					new Pair(Photo.Columns.DateTime, OrderBy.OrderDirection.Ascending),
					new Pair(Photo.Columns.K, OrderBy.OrderDirection.Ascending)
				}.ToArray();
			}
		}

		#region ContentDisabled image guids
		public static Guid ContentDisabledIcon
		{
			get
			{
				return new Guid("00000000-0000-0000-b916-000000000001");
			}
		}
		public static Guid ContentDisabledThumb
		{
			get
			{
				return new Guid("00000000-0000-0000-b916-000000000003");
			}
		}
		public static Guid ContentDisabledWeb
		{
			get
			{
				return new Guid("00000000-0000-0000-b916-000000000002");
			}
		}
		public static Guid ContentDisabledBlank
		{
			get
			{
				return new Guid("00000000-0000-0000-b916-000000000004");
			}
		}
		#endregion

		#region DeleteAll()
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			this.ContentDisabled = false;
			this.Update(transaction);

			try
			{
				UsrSet usPhotoPic = new UsrSet(new Query(new Q(Usr.Columns.PicPhotoK, this.K)));
				foreach (Usr u in usPhotoPic)
				{
					try
					{
						Guid picToDelete = u.Pic;

						u.Pic = Guid.Empty;
						u.PicPhotoK = 0;
						u.PicState = "";
						u.Update(transaction);

						Storage.RemoveFromStore(Storage.Stores.Pix, picToDelete, "jpg");

					}
					catch { }
				}
			}
			catch { }


			try
			{
				if (this.Gallery.MainPhotoK == this.K)
				{
					Query q = new Query();
					q.QueryCondition = new And(
						new Q(Photo.Columns.GalleryK, this.GalleryK),
						Photo.EnabledQueryCondition
					);
					q.OrderBy = new OrderBy(Photo.Columns.DateTime);
					q.Columns = new ColumnSet(Photo.Columns.K, Photo.Columns.Status);
					PhotoSet psMain = new PhotoSet(q);
			
					int firstDisabled = 0;
					foreach (Photo p in psMain)
					{
						if (p.K != this.K)
						{
							if (p.Status == StatusEnum.Enabled)
							{
								this.Gallery.MainPhotoK = p.K;
								break;
							}
							else if (firstDisabled == 0)
								firstDisabled = p.K;
						}
					}
					if (firstDisabled > 0)
						this.Gallery.MainPhotoK = firstDisabled;
					
					this.Gallery.Update(transaction);
					
					//PhotoSet psMain = new PhotoSet(
					//                            new Query(
					//                                new And(
					//                                new Q(Photo.Columns.K, QueryOperator.NotEqualTo, this.K),
					//                                new Q(Photo.Columns.GalleryK, this.GalleryK),
					//                                Photo.EnabledQueryCondition),
					//                                new OrderBy(Photo.Columns.DateTime),
					//                                1
					//                            )
					//                        );
					//if (psMain.Count > 0)
					//{
					//    this.Gallery.MainPhotoK = psMain[0].K;
					//}
					//else
					//{
					//    PhotoSet psMain1 = new PhotoSet(
					//        new Query(
					//            new And(
					//                new Q(Photo.Columns.K, QueryOperator.NotEqualTo, this.K),
					//                new Q(Photo.Columns.GalleryK, this.GalleryK)
					//            ),
					//            new OrderBy(Photo.Columns.DateTime),
					//            1
					//        )
					//    );
					//    if (psMain1.Count > 0)
					//    {
					//        this.Gallery.MainPhotoK = psMain1[0].K;
					//    }
					//    else
					//    {
					//        this.Gallery.MainPhotoK = 0;
					//    }
					//}
					//this.Gallery.Update(transaction);
				}
			}
			catch { }

			try
			{
				Delete UsrPhotoFavouriteDelete = new Delete(
					TablesEnum.UsrPhotoFavourite,
					new Q(UsrPhotoFavourite.Columns.PhotoK, this.K)
					);
				UsrPhotoFavouriteDelete.Run(transaction);
			}
			catch { }

			try
			{
				Delete UsrPhotoMeDelete = new Delete(
					TablesEnum.UsrPhotoMe,
					new Q(UsrPhotoMe.Columns.PhotoK, this.K)
					);
				UsrPhotoMeDelete.Run(transaction);
			}
			catch { }

			try
			{
				Delete CommentAlertDelete = new Delete(
					TablesEnum.CommentAlert,
					new And(
					new Q(CommentAlert.Columns.ParentObjectK, this.K),
					new Q(CommentAlert.Columns.ParentObjectType, Model.Entities.ObjectType.Photo)
					)
				);
				CommentAlertDelete.Run(transaction);
			}
			catch { }

			try
			{
				if (this.ThreadK.HasValue && this.ThreadK.Value > 0)
					this.Thread.DeleteAll(transaction);
			}
			catch { }

			try
			{
				ThreadSet ts = new ThreadSet(new Query(new Q(Thread.Columns.PhotoK, this.K)));
				foreach (Thread t in ts)
				{
					t.DeleteAll(transaction);
				}
			}
			catch { }

			try
			{
				Delete PhotoReviewDelete = new Delete(
					TablesEnum.PhotoReview,
					new Q(PhotoReview.Columns.PhotoK, this.K)
					);
				PhotoReviewDelete.Run(transaction);
			}
			catch { }

			try
			{
				ParaSet ps = new ParaSet(new Query(new Q(Para.Columns.PhotoK, this.K)));
				foreach (Para p in ps)
				{
					if (p.Type.Equals(Para.TypeEnum.Photo))
						p.DeleteAll(transaction);
					else
					{
						Guid oldPic = p.Pic;

						p.Pic = Guid.Empty;
						p.PhotoK = 0;
						p.Update(transaction);

						if (oldPic != Guid.Empty)
							Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");
					}
				}
			}
			catch { }

			try
			{
				foreach (var tagPhoto in this.ChildTagPhotos())
				{
					foreach (var tagPhotoHistory in tagPhoto.ChildTagPhotoHistorys())
					{
						tagPhotoHistory.Delete(transaction);
					}
					tagPhoto.Delete(transaction);
				}
			}
			catch { }

			Guid uploadTemporary = this.UploadTemporary;
			string uploadTemporaryExtention = this.UploadTemporaryExtention;
			Guid master = this.Master;
			Guid web = this.Web;
			Guid thumb = this.Thumb;
			Guid icon = this.Icon;
			bool isVideo = this.MediaType == MediaTypes.Video;
			Guid videoMaster = this.VideoMaster;
			string videoFileExtention = this.VideoFileExtention;
			Guid videoMed = this.VideoMed;
			bool hasCrop = this.HasCrop;
			Guid crop = this.HasCrop ? this.Crop : Guid.Empty;


			try
			{
				this.Delete(transaction);
			}
			catch { }

			try
			{

				Storage.RemoveFromStore(Storage.Stores.Temporary, uploadTemporary, uploadTemporaryExtention);
				Storage.RemoveFromStore(Storage.Stores.Master, master, "jpg");
				Storage.RemoveFromStore(Storage.Stores.Pix, web, "jpg");
				Storage.RemoveFromStore(Storage.Stores.Pix, thumb, "jpg");
				Storage.RemoveFromStore(Storage.Stores.Pix, icon, "jpg");
				if (isVideo)
				{
					Storage.RemoveFromStore(Storage.Stores.Master, videoMaster, videoFileExtention);
					Storage.RemoveFromStore(Storage.Stores.Pix, videoMed, "flv");
				}
				if (hasCrop)
					Storage.RemoveFromStore(Storage.Stores.Pix, crop, "jpg");
			}
			catch { }

			try
			{
				this.Usr.UpdateTotalPhotos(transaction);
			}
			catch { }

			try
			{
				this.Gallery.UpdateStats(transaction, true);
			}
			catch { }

			try
			{
				this.Gallery.UpdatePhotoOrder(transaction);
			}
			catch { }

			try
			{
				if (this.Event != null)
					this.Event.UpdateTotalPhotos(transaction);
			}
			catch { }

		}
		#endregion

		#region OrderBy's
		public static OrderBy DateTimeOrder(OrderBy.OrderDirection direction)
		{
			return new OrderBy(
				new OrderBy(Photo.Columns.ParentDateTime, direction),
				new OrderBy(Photo.Columns.DateTime, direction),
				new OrderBy(Photo.Columns.K, direction)
			);
		}
		#endregion

		#region Joins
		public static Join UsrMeJoin
		{
			get
			{
				return new Join(new Join(Photo.Columns.K, UsrPhotoMe.Columns.PhotoK), Usr.Columns.K, UsrPhotoMe.Columns.UsrK);
			}
		}
		//		public static Join EventAllJoin
		//		{
		//			get
		//			{
		//				return new Join(Photo.Columns.EventK,Event.Columns.K);
		//			}
		//		}
		public static Join EventLeftJoin
		{
			get
			{
				return new JoinLeft(Photo.Columns.EventK, Event.Columns.K);
			}
		}
		public static Join EventJoin
		{
			get
			{
				return new Join(Photo.Columns.EventK, Event.Columns.K);
				//				return new Join(
				//					new TableElement(Bobs.TablesEnum.Photo),
				//					new TableElement(Bobs.TablesEnum.Event),
				//					QueryJoinType.Inner,
				//					new And(
				//					new Q(Photo.Columns.EventK,Event.Columns.K,true),
				//					Event.EnabledQueryCondition
				//					)
				//				);
			}
		}
		//		public static Join VenueAllJoin
		//		{
		//			get
		//			{
		//				return new Join(Photo.EventJoin,Venue.Columns.K,Event.Columns.VenueK);
		//			}
		//		}
		public static Join VenueLeftJoin
		{
			get
			{
				return new Join(
					Photo.EventLeftJoin,
					new TableElement(TablesEnum.Venue),
					QueryJoinType.Left,
					Event.Columns.VenueK,
					Venue.Columns.K);
			}
		}
		public static Join VenueJoin
		{
			get
			{
				return new Join(Photo.EventJoin, Venue.Columns.K, Event.Columns.VenueK);
				//				return new Join(
				//					Photo.EventJoin,
				//					new TableElement(Bobs.TablesEnum.Venue),
				//					QueryJoinType.Inner,
				//					new And(
				//					new Q(Venue.Columns.K,Event.Columns.VenueK,true),
				//					Venue.EnabledQueryCondition
				//					)
				//				);
			}
		}
		public static Join PlaceLeftJoin
		{
			get
			{
				return new Join(
					Photo.VenueLeftJoin,
					new TableElement(TablesEnum.Place),
					QueryJoinType.Left,
					Venue.Columns.PlaceK,
					Place.Columns.K);
			}
		}
		public static Join PlaceJoin
		{
			get
			{
				return new Join(Photo.VenueJoin, Place.Columns.K, Venue.Columns.PlaceK);
			}
		}
		//		public static Join PlaceAllJoin
		//		{
		//			get
		//			{
		//				return new Join(Photo.VenueAllJoin, Place.Columns.K, Venue.Columns.PlaceK);
		//			}
		//		}
		public static Join UsrFavouritesJoin
		{
			get
			{
				return new Join(
					new Join(Photo.Columns.K, UsrPhotoFavourite.Columns.PhotoK),
					Usr.Columns.K,
					UsrPhotoFavourite.Columns.UsrK);
			}
		}
		#endregion

		#region LastPostFriendlyTime
		public string LastPostFriendlyTime(bool Capital)
		{
			return Cambro.Misc.Utility.FriendlyTime(LastPost, Capital);
		}
		#endregion
		#region UrlDiscussion
		public string UrlDiscussion(params string[] par)
		{
			if (this.ThreadK.HasValue && this.ThreadK.Value > 0)
				return Thread.UrlDiscussion(par);
			else
				return Url(par);
		}
		#endregion

		#region CanView(Usr u)
		/// <summary>
		/// Validates the current user to be able to view a photo.
		/// </summary>
		public bool CanView(Usr u)
		{
			return Validate();
		}
		#endregion

		#region Validate()
		/// <summary>
		/// Validates the current user to be able to view a photo.
		/// </summary>
		public bool Validate()
		{
			return this.Status.Equals(Photo.StatusEnum.Enabled);
		}
		#endregion

		#region Url
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

				return urlFragment + "/photo-" + this.K.ToString();
			}
		}
		public string UrlOfPhotoInSameParentFilterPart(int newK)
		{
			string urlFragment = "";
			if (Common.Settings.DynamicUrlFragments)
				urlFragment = GetUrlFragment();
			else
				urlFragment = UrlFragment;

			return urlFragment + "/photo-" + newK.ToString();
		}
		public string UrlOfPhotoInSameParent(int newK, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlOfPhotoInSameParentFilterPart(newK), null, par);
		}
		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, null, par);
		}
		public string UrlOfPhotoInSameGallery(int PhotoK)
		{
			if (this.Event != null)
				return UrlInfo.MakeUrl(this.Event.UrlFilterPartVenueDate + "/photo-" + PhotoK.ToString(), null, null);
			else if (this.Article != null)
				return UrlInfo.MakeUrl(this.Article.UrlFilterPart + "/photo-" + PhotoK.ToString(), null, null);
			else
				return "";
		}
		public string UrlApp(string Application, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, Application, par);
		}
		#endregion

		#region UrlColumns
		public static ColumnSet UrlColumns
		{
			get
			{
				return new ColumnSet(Photo.Columns.K, Photo.Columns.UrlFragment, Photo.Columns.EventK, Photo.Columns.ArticleK);
			}
		}
		#endregion

		#region UpdateStats(Transaction transaction)
		public void UpdateStats(Transaction transaction)
		{

			DataView dvCool = Db.Dv("SELECT SUM(Rating) AS theSum, COUNT(K) AS theCount  FROM PhotoReview WHERE PhotoK=" + K + " AND RatingType=" + ((int)PhotoReview.RatingTypes.Cool).ToString());
			int coolCount = (int)dvCool[0]["theCount"];
			int coolRating = 0;
			double averageCool = 0;
			if (coolCount > 0)
			{
				coolRating = (int)dvCool[0]["theSum"];
				averageCool = (double)coolRating / (double)coolCount;
			}
			double weightedCool = ((double)coolRating + 50.0) / ((double)coolCount + 10.0);

			DataView dvSexy = Db.Dv("SELECT SUM(Rating) AS theSum, COUNT(K) AS theCount  FROM PhotoReview WHERE PhotoK=" + K + " AND RatingType=" + ((int)PhotoReview.RatingTypes.Sexy).ToString());
			int sexyCount = (int)dvSexy[0]["theCount"];
			int sexyRating = 0;
			double averageSexy = 0;
			if (sexyCount > 0)
			{
				sexyRating = (int)dvSexy[0]["theSum"];
				averageSexy = (double)sexyRating / (double)sexyCount;
			}
			double weightedSexy = ((double)sexyRating + 50.0) / ((double)sexyCount + 10.0);

			double totalAverage = 0;
			if ((sexyCount + coolCount) > 0)
				totalAverage = ((double)sexyRating + (double)coolRating) / ((double)sexyCount + (double)coolCount);

			double totalWeightedAverage = ((double)sexyRating + (double)coolRating + 100.0) / ((double)sexyCount + (double)coolCount + 20.0);

			this.AverageCoolRating = averageCool;
			this.AverageSexyRating = averageSexy;
			this.TotalCoolRatings = coolCount;
			this.TotalSexyRatings = sexyCount;
			this.WeightedCoolRating = weightedCool;
			this.WeightedSexyRating = weightedSexy;

			double order = totalWeightedAverage;

			if (this.TotalComments > 10)
				order += 10.0;
			else
				order += (double)this.TotalComments;

			this.Order = order;
			this.Update(transaction);
			this.Gallery.UpdatePhotoOrder(transaction);
		}
		#endregion

		#region UpdateTotalComments()
		public void UpdateTotalComments(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(Thread.Columns.PhotoK, this.K),
				new Q(Thread.Columns.Private, false),
				new Q(Thread.Columns.GroupPrivate, false),
				new Q(Thread.Columns.PrivateGroup, false)
			);
			q.ExtraSelectElements = ForumStats.ExtraSelectElements;
			q.Columns = new ColumnSet();
			ForumStats cs = new ForumStats(q);
			this.TotalComments = cs.TotalComments;
			this.AverageCommentDateTime = cs.AverageCommentDateTime;
			this.LastPost = cs.LastPost;

			this.UpdateStats(transaction);
			this.Update(transaction);
			if (this.Event != null)
				this.Event.UpdateTotalComments(transaction);
			if (this.Article != null)
				this.Article.UpdateTotalComments(transaction);
		}
		#endregion

		#region Paths
		public string IconPath { get { return Storage.Path(Icon); } }
		public string ThumbPath { get { return Storage.Path(Thumb); } }
		public string WebPath { get { return Storage.Path(Web); } }
		public string MasterPath { get { return Storage.Path(Master, Storage.Stores.Master); } }
		public string CropPath { get { return Storage.Path(Crop); } }

		public string VideoMasterPath { get { return Storage.Path(VideoMaster, VideoFileExtention, Storage.Stores.Master); } }
		public string VideoMedPath { get { return Storage.Path(this.VideoMed, "flv"); } }
		public string VideoHiPath { get { return Storage.Path(this.VideoHi, "flv"); } }


		#endregion

		#region FileSystemPaths
		//public string FileSystemPathIcon { get { return FileSystemPath(Icon); } }
		//public string FileSystemPathThumb { get { return FileSystemPath(Thumb); } }
		//public string FileSystemPathWeb { get { return FileSystemPath(Web); } }
		//public string FileSystemPathCrop { get { return FileSystemPath(Crop); } }
		//public string FileSystemPathMaster { get { return FileSystemPath(Master, Photo.Locations.Master); } }
		//public string FileSystemPathUploadTemporary { get { return FileSystemPath(UploadTemporary, UploadTemporaryExtention, Photo.Locations.Master); } }
		
		//public string FileSystemPathVideoMaster { get { return FileSystemPath(VideoMaster, VideoFileExtention, Photo.Locations.Master); } }
		//public string FileSystemPathVideoMed { get { return FileSystemPath(this.VideoMed, "flv"); } }

		public static bool HasNewPixMasterFileSystemLocation
		{
			get
			{
				return false;
			}
		}
		public static bool HasNewPixFileSystemLocation
		{
			get
			{
				return false;
			}
		}
		

		//#region Delete
		//public static void Delete(Guid Name)
		//{
		//    Photo.Delete(Name, "jpg", Locations.Pix);
		//}
		//public static void Delete(Guid Name, Locations Location)
		//{
		//    Photo.Delete(Name, "jpg", Location);
		//}
		//public static void Delete(Guid Name, string Extention)
		//{
		//    Photo.Delete(Name, Extention, Locations.Pix);
		//}
		//public static void Delete(Guid Name, string Extention, Locations Location)
		//{
		//    if (Location.Equals(Photo.Locations.Pix))
		//    {
		//        try
		//        {
		//            Storage.Delete(Name, Extention, Locations.Pix);
		//        }
		//        catch { }

		//        if (Photo.HasNewPixFileSystemLocation)
		//        {
		//            try
		//            {
		//                Storage.Delete(Name, Extention, Locations.PixNew);
		//            }
		//            catch { }
		//        }

		//        try
		//        {
		//            Storage.Delete(Name, Extention, Locations.Static);
		//        }
		//        catch { }

		//        try
		//        {
		//            Storage.Delete(Name, Extention, Locations.PixAmazonEU);
		//        }
		//        catch { }

		//        try
		//        {
		//            Storage.Delete(Name, Extention, Locations.PixAmazonUS);
		//        }
		//        catch { }
		//    }
		//    else if (Location.Equals(Photo.Locations.Master))
		//    {
		//        try
		//        {
		//            Storage.Delete(Name, Extention, Locations.Master);
		//        }
		//        catch { }

		//        if (Photo.HasNewPixMasterFileSystemLocation)
		//        {
		//            try
		//            {
		//                Storage.Delete(Name, Extention, Locations.MasterNew);
		//            }
		//            catch { }
		//        }

		//        try
		//        {
		//            Storage.Delete(Name, Extention, Locations.MasterAmazonEU);
		//        }
		//        catch { }
		//    }
		//    else
		//    {
		//        throw new Exception("Invalid location for Photo.Delete... must be Pix or Master");
		//    }
		//}
		//#endregion

		//#region Replicate
		//public static void Replicate(Guid name, Bob metaDataObject, string metaDataObjectData)
		//{
		//    Photo.Replicate(name, "jpg", metaDataObject, metaDataObjectData);
		//}
		//public static void Replicate(Guid name, string extention, Bob metaDataObject, string metaDataObjectData)
		//{
		//    if (!Vars.DevEnv)
		//    {
		//        File.Copy(
		//            Photo.FileSystemPath(name, extention),
		//            Photo.FileSystemPath(name, extention, Photo.Locations.Static),
		//            true);

		//        if (Photo.HasNewPixFileSystemLocation)
		//        {
		//            File.Copy(
		//                Photo.FileSystemPath(name, extention),
		//                Photo.FileSystemPath(name, extention, Photo.Locations.PixNew),
		//                true);
		//        }
		//    }

		//    SortedList metaData = null;
		//    if (metaDataObject != null && metaDataObject is IBobType)
		//    {
		//        metaData = new SortedList();
		//        metaData.Add("ObjectType", ((IHasObjectType)metaDataObject).ObjectType);
		//        metaData.Add("ObjectK", ((IHasSinglePrimaryKey)metaDataObject).K);
		//        if (metaDataObjectData != null && metaDataObjectData.Length > 0)
		//            metaData.Add("ObjectData", metaDataObjectData);
		//    }

		//    Storage.Copy(name, extention, Locations.Pix, Locations.PixAmazonEU, metaData);
		//    try
		//    {
		//        Storage.Copy(name, extention, Locations.Pix, Locations.PixAmazonUS, metaData);
		//    }
		//    catch(Exception ex)
		//    {
		//        Storage.Delete(name, extention, Locations.PixAmazonEU);
		//        throw ex;
		//    }
		//}

		

		//public static void ReplicateMaster(Guid name, Bob metaDataObject, string metaDataObjectData)
		//{
		//    Photo.ReplicateMaster(name, "jpg", metaDataObject, metaDataObjectData);
		//}
		//public static void ReplicateMaster(Guid name, string extention, Bob metaDataObject, string metaDataObjectData)
		//{
		//    if (!Vars.DevEnv)
		//    {
		//        if (Photo.HasNewPixMasterFileSystemLocation)
		//        {
		//            File.Copy(
		//                Photo.FileSystemPath(name, extention, Photo.Locations.Master),
		//                Photo.FileSystemPath(name, extention, Photo.Locations.MasterNew),
		//                true);
		//        }
		//    }

		//    if (metaDataObjectData == "Master" || metaDataObjectData == "VideoMaster")
		//    {
		//        SortedList metaData = null;
		//        if (metaDataObject != null && metaDataObject is IHasObjectType && metaDataObject is IHasSinglePrimaryKey)
		//        {
		//            metaData = new SortedList();
		//            metaData.Add("ObjectType", ((IHasObjectType)metaDataObject).ObjectType);
		//            metaData.Add("ObjectK", ((IHasSinglePrimaryKey)metaDataObject).K);
		//            if (metaDataObjectData != null && metaDataObjectData.Length > 0)
		//                metaData.Add("ObjectData", metaDataObjectData);
		//        }

		//        Storage.Copy(name, extention, Locations.Master, Locations.MasterAmazonEU, metaData);

		//        //if (metaDataObject is Photo)
		//        //{
		//        //    Photo p = (Photo)metaDataObject;
		//        //    if (!p.DoneAmazonPixMaster)
		//        //    {
		//        //        p.DoneAmazonPixMaster = true;
		//        //        p.Update();
		//        //    }
		//        //}
		//    }
		//}
		

		#endregion

		//public static string FileSystemPath(Guid Name)
		//{
		//    return FileSystemPath(Name, "jpg", Locations.Pix);
		//}
		//public static string FileSystemPath(Guid Name, string Extention)
		//{
		//    return FileSystemPath(Name, Extention, Locations.Pix);
		//}
		//public static string FileSystemPath(Guid Name, Storage.Locations Location)
		//{
		//    return FileSystemPath(Name, "jpg", Location);
		//}
		//public static string FileSystemPath(Guid Name, string Extention, Storage.Locations Location)
		//{
		//    if (Common.Properties.IsDevelopmentEnvironment)
		//        return @"c:\dontstayin\" + (Location.Equals(Locations.Master) ? "pixmaster" : "pix") + @"\" + Name.ToString() + "." + Extention;
		//    else
		//    {
		//        if (Location.Equals(Locations.Pix))
		//            return @"\\" + Vars.PixIp + @"\C$\DontStayIn\Live\Pix\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
		//        else if (Location.Equals(Locations.PixNew))
		//            throw new Exception("PixNew is undefined!");//return @"\\" + Vars.PixServerIp + @"\C$\DontStayIn\Live\Pix\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
		//        else if (Location.Equals(Locations.Master))
		//            return @"\\" + Vars.PixMasterIp + @"\C$\DontStayIn\Live\PixMaster\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
		//            //return @"\\" + Vars.PixMasterIp + @"\C$\DontStayIn\Live\PixMaster\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
		//        else if (Location.Equals(Locations.MasterNew))
		//            return @"\\" + Vars.PixMasterIp + @"\C$\DontStayIn\Live\PixMasterOffice\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
		//            //throw new Exception("MasterNew is undefined!");
		//        else //if (Location.Equals(Locations.Static))
		//            return @"\\" + Vars.StaticContentIp(Name) + @"\C$\DontStayIn\Live\Pix" + Name.ToString().Substring(0, 1).ToUpper() + @"\" + Name.ToString().Substring(0, 2) + @"\" + Name.ToString().Substring(2, 2) + @"\" + Name.ToString() + "." + Extention;
		//    }
		//}

		#region StatsText
		public string StatsText
		{
			get
			{
				string tmp = "";
				if (this.Views > 0)
					tmp += this.Views.ToString() + " view" + (this.Views == 1 ? "" : "s");
				if (this.TotalComments > 0)
					tmp += (tmp.Length == 0 ? "" : ", ") + this.TotalComments.ToString() + " comment" + (this.TotalComments == 1 ? "" : "s");
				if (this.WeightedCoolRating > 6.5 && this.AverageCoolRating > 7.5)
					tmp += (tmp.Length == 0 ? "" : ", ") + ((double)((this.AverageCoolRating) * 10.0)).ToString("###") + "% cool";
				if (this.WeightedSexyRating > 6.5 && this.AverageSexyRating > 7.5)
					tmp += (tmp.Length == 0 ? "" : ", ") + ((double)((this.AverageSexyRating) * 10.0)).ToString("###") + "% sexy";
				if (this.WeightedCoolRating < 2.5)
					tmp += (tmp.Length == 0 ? "" : ", ") + ((double)((10.0 - (this.AverageCoolRating)) * 10.0)).ToString("###") + "% loser";
				return tmp;
			}
		}
		public static ColumnSet StatsTextColumns
		{
			get
			{
				return new ColumnSet(
					Photo.Columns.Views,
					Photo.Columns.TotalComments,
					Photo.Columns.AverageCoolRating,
					Photo.Columns.AverageSexyRating,
					Photo.Columns.WeightedCoolRating,
					Photo.Columns.WeightedSexyRating
					);
			}
		}
		#endregion
		#region SimpleThumbImgTag
		public string SimpleThumbImgTag
		{
			get
			{
				return "<img src=\"" + this.ThumbPath + "\">";
			}
		}
		#endregion

		#region Links to Bobs
		#region NextPhoto1
		public Photo NextPhoto1
		{
			get
			{
				if (nextPhoto1 == null && NextPhoto1K > 0)
					nextPhoto1 = new Photo(NextPhoto1K);
				return nextPhoto1;
			}
			set
			{
				nextPhoto1 = value;
			}
		}
		private Photo nextPhoto1;
		#endregion
		#region NextPhoto2
		public Photo NextPhoto2
		{
			get
			{
				if (nextPhoto2 == null && NextPhoto2K > 0)
					nextPhoto2 = new Photo(NextPhoto2K);
				return nextPhoto2;
			}
			set
			{
				nextPhoto2 = value;
			}
		}
		private Photo nextPhoto2;
		#endregion
		#region NextPhoto3
		public Photo NextPhoto3
		{
			get
			{
				if (nextPhoto3 == null && NextPhoto3K > 0)
					nextPhoto3 = new Photo(NextPhoto3K);
				return nextPhoto3;
			}
			set
			{
				nextPhoto3 = value;
			}
		}
		private Photo nextPhoto3;
		#endregion

		#region PreviousPhoto1
		public Photo PreviousPhoto1
		{
			get
			{
				if (previousPhoto1 == null && PreviousPhoto1K > 0)
					previousPhoto1 = new Photo(PreviousPhoto1K);
				return previousPhoto1;
			}
			set
			{
				previousPhoto1 = value;
			}
		}
		private Photo previousPhoto1;
		#endregion
		#region PreviousPhoto2
		public Photo PreviousPhoto2
		{
			get
			{
				if (previousPhoto2 == null && PreviousPhoto2K > 0)
					previousPhoto2 = new Photo(PreviousPhoto2K);
				return previousPhoto2;
			}
			set
			{
				previousPhoto2 = value;
			}
		}
		private Photo previousPhoto2;
		#endregion
		#region PreviousPhoto3
		public Photo PreviousPhoto3
		{
			get
			{
				if (previousPhoto3 == null && PreviousPhoto3K > 0)
					previousPhoto3 = new Photo(PreviousPhoto3K);
				return previousPhoto3;
			}
			set
			{
				previousPhoto3 = value;
			}
		}
		private Photo previousPhoto3;
		#endregion

		#region FirstUsr
		public Usr FirstUsr
		{
			get
			{
				if (firstUsr == null)
					firstUsr = new Usr(FirstUsrK, this, Photo.Columns.FirstUsrK);
				return firstUsr;
			}
			set
			{
				firstUsr = value;
			}
		}
		private Usr firstUsr;
		#endregion
		#region Event
		public Event Event
		{
			get
			{
				if (_event == null && EventK > 0)
					_event = new Event(EventK);
				return _event;
			}
		}
		Event _event;
		#endregion
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region Thread
		public Thread Thread
		{
			get
			{
				if (thread == null && this.ThreadK.HasValue && this.ThreadK.Value > 0)
					thread = new Thread(ThreadK.Value);
				return thread;
			}
		}
		Thread thread;
		#endregion
		#region Gallery
		public Gallery Gallery
		{
			get
			{
				if (gallery == null)
					gallery = new Gallery(GalleryK, this, Photo.Columns.GalleryK);
				return gallery;
			}
			set
			{
				gallery = value;
			}
		}
		private Gallery gallery;
		#endregion
		#region EnabledByUsr
		public Usr EnabledByUsr
		{
			get
			{
				if (enabledByUsr == null && EnabledByUsrK > 0)
					enabledByUsr = new Usr(EnabledByUsrK);
				return enabledByUsr;
			}
			set
			{
				enabledByUsr = value;
			}
		}
		private Usr enabledByUsr;
		#endregion
		#region Article
		public Article Article
		{
			get
			{
				if (article == null && ArticleK > 0)
					article = new Article(ArticleK);
				return article;
			}
		}
		Article article;
		#endregion
		#region Mobile
		public Mobile Mobile
		{
			get
			{
				if (mobile == null && MobileK > 0)
					mobile = new Mobile(MobileK);
				return mobile;
			}
			set
			{
				mobile = value;
			}
		}
		private Mobile mobile;
		#endregion
		#endregion

		#region UsrString / UsrHtml
		#region UsrString
		public string UsrString
		{
			get
			{
				if (usrString == null)
				{
					if (UsrCount == 0)
					{
						usrString = "";
					}
					else if (UsrCount == 1)
					{
						usrString = FirstUsr.NickName;
					}
					else
					{
						usrString = "";
						for (int i = 0; i < UsrHtmlUsrSet.Count; i++)
						{
							usrString += (i > 0 ? (i == UsrHtmlUsrSet.Count - 1 ? " and " : ", ") : "") + UsrHtmlUsrSet[i].NickName;
						}
					}
				}
				return usrString;
			}
			set
			{
				usrString = value;
			}
		}
		private string usrString;
		#endregion
		#region UsrHtml
		public string UsrHtml
		{
			get
			{
				if (usrHtml == null)
				{
					if (UsrCount == 0)
					{
						usrHtml = "";
					}
					else if (UsrCount == 1)
					{
						usrHtml = "<a href=\"" + FirstUsr.Url() + "\" " + FirstUsr.Rollover + ">" + FirstUsr.NickName + "</a>";
					}
					else
					{
						usrHtml = "";
						for (int i = 0; i < UsrHtmlUsrSet.Count; i++)
						{
							usrHtml += (i > 0 ? (i == UsrHtmlUsrSet.Count - 1 ? " and " : ", ") : "") + "<a href=\"" + UsrHtmlUsrSet[i].Url() + "\" " + UsrHtmlUsrSet[i].Rollover + ">" + UsrHtmlUsrSet[i].NickName + "</a>";
						}
					}
				}
				return usrHtml;
			}
			set
			{
				usrHtml = value;
			}
		}
		private string usrHtml;
		#endregion
		#region UsrHtmlUsrSet
		public UsrSet UsrHtmlUsrSet
		{
			get
			{
				if (usrHtmlUsrSet == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.Columns = Usr.LinkColumns;
					q.TableElement = Usr.PhotoMeJoin;
					q.QueryCondition = new And(
						Usr.IsDisplayedInUsrLists,
						new Q(UsrPhotoMe.Columns.PhotoK, K));
					q.OrderBy = new OrderBy(Usr.Columns.DateTimeSignUp);
					usrHtmlUsrSet = new UsrSet(q);
				}
				return usrHtmlUsrSet;
			}
			set
			{
				usrHtmlUsrSet = value;
			}
		}
		private UsrSet usrHtmlUsrSet;
		#endregion
		#region UsrHtmlKList
		public string UsrHtmlKList
		{
			get
			{
				if (UsrCount == 0)
				{
					return "";
				}
				else if (UsrCount == 1)
				{
					return "|" + FirstUsrK.ToString() + "|";
				}
				else
				{
					StringBuilder sb = new StringBuilder();
					foreach (Usr u in UsrHtmlUsrSet)
					{
						sb.Append("|");
						sb.Append(u.K.ToString());
						sb.Append("|");
					}
					return sb.ToString();
				}
			}
		}
		#endregion
		#endregion

		#region UpdateUsrCount
		public void UpdateUsrCount(Transaction transaction)
		{
			Query q = new Query();
			q.NoLock = true;
			q.Columns = new ColumnSet(Usr.Columns.K);
			q.TableElement = Usr.PhotoMeJoin;
			q.QueryCondition = new Q(UsrPhotoMe.Columns.PhotoK, K);
			q.OrderBy = new OrderBy(Usr.Columns.DateTimeSignUp);
			UsrSet us = new UsrSet(q);
			if (us.Count > 0)
				this.FirstUsrK = us[0].K;
			else
				this.FirstUsrK = 0;
			this.UsrCount = us.Count;
			this.Update(transaction);
		}
		#endregion

		#region CreateCrop()
		Guid CreateCrop()
		{
			this.Crop = Guid.NewGuid();
			
			using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(Storage.GetFromStore(Storage.Stores.Master, this.Master, "jpg"))))
			{
				OperationReturn crop = Operation(image, OperationType.MaxSide, new OperationParams() { MaxSide = 2880, ReturnBytes = true, AllowMaxSideToEnlarge = true });
				Storage.AddToStore(crop.Bytes, Storage.Stores.Pix, this.Crop, "jpg", this, "Crop");
			}

			this.Update();
			return this.Crop;
			
		}
		#endregion

		#region IHasParent Members
		public Model.Entities.ObjectType ParentObjectType
		{
			get
			{
				if (this.Event != null)
					return Model.Entities.ObjectType.Event;
				else if (this.Article != null)
					return Model.Entities.ObjectType.Article;
				throw new Exception("Photo should be either event or article...");
			}
			set
			{
				throw new Exception("Shouldn't set this for Photo...");
			}
		}

		public IBob ParentObject
		{
			get
			{
				if (this.Event != null)
					return this.Event;
				else if (this.Article != null)
					return this.Article;
				throw new Exception("Photo should be either event or article...");
			}
		}

		public int ParentObjectK
		{
			get
			{
				if (this.Event != null)
					return this.EventK;
				else if (this.Article != null)
					return this.ArticleK;
				throw new Exception("Photo should be either event or article...");
			}
			set
			{
				throw new Exception("Shouldn't set this for Photo...");
			}
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "Photo";
			}
		}
		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Photo;
			}
		}
		#endregion


		#region ITaggable Members

		public int ItemOwnerUsrK
		{
			get { return this.UsrK; }
		}

		#endregion
		public delegate void EncoderStatusDelegate(string message);
		public delegate void MeaningfulActivityDelegate();

		Q IDiscussable.QueryConditionForGettingThreads
		{
			get
			{
				if (this.ThreadK.HasValue && this.ThreadK.Value > 0)
				{
					return new And
					(
						new Q(Thread.Columns.PhotoK, K),
						new Q(Thread.Columns.K, QueryOperator.NotEqualTo, ThreadK.Value)
					);
				}
				else
				{
					return new Q(Thread.Columns.PhotoK, K);
				}
			}
		}
		bool IDiscussable.ShowPrivateThreads { get { return true; } }
		IDiscussable IDiscussable.UsedDiscussable { get { return this; } }
		bool IDiscussable.OnlyShowThreads { get { return true; } }
		#region IReadableReference Members

		public string ReadableReference
		{
			get { return "Photo-" + K.ToString(); }
		}

		#endregion



		#region PhotoControl helpers
		string eventInfoHtml = null;
		public string EventInfoHtml
		{
			get { return eventInfoHtml ?? (eventInfoHtml = (Event != null) ? Event.FriendlyHtml(true, true, true, false) : ""); }
		}
		string eventInfoHtmlNoDate = null;
		public string EventInfoHtmlNoDate
		{
			get { return eventInfoHtmlNoDate ?? (eventInfoHtmlNoDate = (Event != null) ? Event.FriendlyHtml(true, true, false, false) : ""); }
		}
		string articleInfoHtml = null;
		public string ArticleInfoHtml
		{
			get
			{
				return articleInfoHtml ?? (articleInfoHtml = (Article != null) ? Article.Link() : "");
			}
		}

		string takenByDetailsHtml;
		public string TakenByDetailsHtml(bool includeEventInfo)
		{
			return takenByDetailsHtml ?? (takenByDetailsHtml = 
				string.Format("<small>Taken by {0} on {1}{2}. {3} views.</small>",
					this.Usr.Link(), 
					Utilities.DateToString(this.DateTime),
					includeEventInfo && this.EventInfoHtmlNoDate.Length > 0 ? " at " + this.EventInfoHtmlNoDate : "",
					this.Views));
		}
		public string Link { get { return "http://" + Vars.DomainName + this.Url(); } }

		public string EmbedHtml
		{
			get
			{
				return (this.MediaType == MediaTypes.Image) ?
					"<dsi:object type=\"photo\" ref=\"" + this.K.ToString() + "\" style=\"photo: web; \" />" :
					"<dsi:video type=\"dsi\" ref=\"" + this.K.ToString() + "\" />";
			}
		}

		public string PhotoVideoLabel { get { return IsPhoto ? "photo" : "video"; } }
		public bool IsPhoto { get { return this.MediaType == MediaTypes.Image; } }
		public bool IsVideo { get { return this.MediaType == MediaTypes.Video; } }


		#region UsrsInPhoto
		UsrSet usrsInPhoto;
		public UsrSet UsrsInPhoto
		{
			get
			{
				if (usrsInPhoto == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.TableElement = Usr.PhotoMeJoin;
					q.QueryCondition = new Q(UsrPhotoMe.Columns.PhotoK, this.K);
					q.OrderBy = new OrderBy(Usr.Columns.DateTimeSignUp);
					usrsInPhoto = new UsrSet(q);
				}
				return usrsInPhoto;
			}
		}
		private string usrsInPhotoHtml;
		public string UsrsInPhotoHtml
		{
			get
			{
				if (usrsInPhotoHtml == null)
				{
					List<string> links = UsrsInPhoto.ToList().ConvertAll(u => getSpottedDisplayHtml(u));
					links.RemoveAll(s => s == null);
					if (links.Count > 0)
					{
						StringBuilder sb = new StringBuilder("This is ");
						sb.Append(links[0]);
						for (int i = 1; i < links.Count - 1; i++)
						{
							sb.Append(", ");
							sb.Append(links[i]);
						}
						if (links.Count > 1)
						{
							sb.Append(" and ");
							sb.Append(links[links.Count - 1]);
						}
						usrsInPhotoHtml = sb.ToString();
					}
					else
					{
						usrsInPhotoHtml = "";
					}
				}
				return usrsInPhotoHtml;
			}
		}

		private string getSpottedDisplayHtml(Usr usr)
		{
			if (usr.NickName != "")
			{
				return usr.Link();
			}
			if (Usr.Current != null)
			{
				try
				{
					Buddy b = new Buddy(Usr.Current.K, usr.K);
					return "<span onmouseover=\"stt('" + usr.Email + "');\" onmouseout=\"htm();\">" + (b.SkeletonName != "" ? b.SkeletonName : usr.Email.TruncateWithDots(10) )+ "</span>";
					
				}
				catch (BobNotFound) { }
			}

			return null;
		}

		
		public string DownloadPhotoLinkHtml
		{
			get
			{
				return string.Format("<a href=\"{0}\" target=\"_blank\" id=\"DownloadPhotoAnchor\">Click here to download this photo ({1}x{2})</a>",
					MasterPath, OriginalWidth, OriginalHeight);
			}
		}

		public string QuickBrowserUrl
		{
			get
			{
				return this.Gallery.Url("photo", this.K.ToString());
			}
		}


		public void SetAsPhotoOfWeek(bool isPhotoOfWeek, string photoOfWeekCaption, bool admin, bool resetDateTime)
		{
			//if (this.EventK > 0 && this.Event != null)
			//{
			//    if (!this.Event.Venue.Place.Country.IsCurrentUsrAdmin)
			//        throw new DsiUserFriendlyException("Sorry, admin only!!");
			//}
			//else 
			if (this.ArticleK > 0 && this.Article != null)
			{
				throw new DsiUserFriendlyException("Sorry, no Article photos on the front page!!");
			}

			if (admin)
			{
				if (isPhotoOfWeek)
				{
					if (photoOfWeekCaption.Length > 0)
					{
						this.PhotoOfWeek = true;
						this.PhotoOfWeekCaption = photoOfWeekCaption;
						if (resetDateTime)
							this.PhotoOfWeekDateTime = DateTime.Now;
						this.Update();
					}
				}
				else
				{
					this.PhotoOfWeek = false;
					this.PhotoOfWeekCaption = photoOfWeekCaption;
					this.Update();
				}
			}
			else
			{

				Photo latestPhotoOfWeek = null;
				if (isPhotoOfWeek)
				{
					this.PhotoOfWeekUser = true;
					this.PhotoOfWeekUserCaption = photoOfWeekCaption;
					if (resetDateTime)
						this.PhotoOfWeekUserDateTime = DateTime.Now;
					this.Update();

					if (resetDateTime)
						latestPhotoOfWeek = this;
					else
						latestPhotoOfWeek = GetLatestTopPhoto(false, false);
				}
				else
				{
					this.PhotoOfWeekUser = false;
					this.PhotoOfWeekUserCaption = photoOfWeekCaption;
					this.Update();

					latestPhotoOfWeek = GetLatestTopPhoto(false, false);
				}

				if (latestPhotoOfWeek != null)
				{
					Caching.Instances.Main.Set(new CacheKey(CacheKeyPrefix.LatestTopPhoto), latestPhotoOfWeek.K.ToString());

					//TopPhotoStub s = new TopPhotoStub(
					//    Guid.NewGuid().Pack(),
					//    ItemType.TopPhoto,
					//    DateTime.Now.Ticks.ToString(),
					//    new Chat.RoomSpec(RoomType.Normal).Guid.Pack(),
					//    latestPhotoOfWeek.K,
					//    latestPhotoOfWeek.Url(),
					//    latestPhotoOfWeek.Icon.ToString(),
					//    latestPhotoOfWeek.Web.ToString(),
					//    latestPhotoOfWeek.WebWidth,
					//    latestPhotoOfWeek.WebHeight,
					//    latestPhotoOfWeek.Thumb.ToString(),
					//    latestPhotoOfWeek.ThumbWidth,
					//    latestPhotoOfWeek.ThumbHeight);

					//Chat.SendJsonChatItem(s);
				}

			}
		}
		public static Photo GetLatestTopPhoto(bool useCache, bool setCacheIfEmpty)
		{
			if (useCache)
			{
				try
				{
					int k = Caching.Instances.Main.Get(new CacheKey(CacheKeyPrefix.LatestTopPhoto), () => 0);
					if (k > 0)
					{
						Photo p = new Photo(k);
						if (p.PhotoOfWeekUser)
							return p;
					}
				}
				catch { }
			}

			Query q = new Query();
			q.TopRecords = 1;
			q.QueryCondition = new Q(Photo.Columns.PhotoOfWeekUser, true);
			q.OrderBy = new OrderBy(Photo.Columns.PhotoOfWeekUserDateTime, OrderBy.OrderDirection.Descending);
			PhotoSet ps = new PhotoSet(q);

			if (setCacheIfEmpty && ps.Count > 0)
			{
				Caching.Instances.Main.Set(new CacheKey(CacheKeyPrefix.LatestTopPhoto), ps[0].K.ToString());
			}

			return ps.Count > 0 ? ps[0] : null;
		}

		public void SetIsFavouritePhoto(bool isFavourite)
		{
			try
			{
				UsrPhotoFavourite u = new UsrPhotoFavourite(Usr.Current.K, this.K);
				if (!isFavourite)
				{
					u.Delete();
					u.Update();
				}
			}
			catch
			{
				if (isFavourite)
				{
					UsrPhotoFavourite newU = new UsrPhotoFavourite();
					newU.DateTimeAdded = DateTime.Now;
					newU.UsrK = Usr.Current.K;
					newU.PhotoK = this.K;
					newU.Update();

					if (Usr.Current.FacebookConnected && Usr.Current.FacebookStoryFavourite)
					{
						FacebookPost.CreateFavouritePhoto(Usr.Current, this);
					}
				}
			}
			if (isFavourite)
			{
				if (this.ThreadK.HasValue && this.ThreadK.Value > 0 && this.Thread != null)
					CommentAlert.Enable(Usr.Current, this.ThreadK.Value, Model.Entities.ObjectType.Thread);
				else
					CommentAlert.Enable(Usr.Current, this.K, Model.Entities.ObjectType.Photo);
			}
		}
		public bool IsFavourite
		{
			get
			{
				if (Usr.Current == null)
				{
					return false;
				}
				try
				{
					new UsrPhotoFavourite(Usr.Current.K, this.K);
					return true;
				}
				catch (BobNotFound)
				{
					return false;
				}
			}
		}
		public bool IsInCompetitionGroup
		{
			get
			{
				if (this.CanEnterCompetition)
				{
					try
					{
						new GroupPhoto(Vars.CompetitionGroupK, this.K);
						return true;
					}
					catch (BobNotFound)
					{ }
				}
				return false;
			}
		}
		public bool CanEnterCompetition
		{
			get
			{
				return Vars.IsCompetitionActive && Usr.Current != null && (Usr.Current.IsAdmin || Usr.Current.K == this.UsrK);
			}
		}
		#endregion

		#endregion


		public bool Highlight { get; set; }

	}
	#endregion
}
	

