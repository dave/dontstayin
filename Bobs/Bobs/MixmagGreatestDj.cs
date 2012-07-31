using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region MixmagGreatestDj
	/// <summary>
	/// DJs in the Greatest DJ poll
	/// </summary>
	[Serializable]
	public partial class MixmagGreatestDj
	{

		#region Simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return (int)this[MixmagGreatestDj.Columns.K] as int? ?? 0; }
			set { this[MixmagGreatestDj.Columns.K] = value; }
		}
		/// <summary>
		/// Name in the url
		/// </summary>
		public override string UrlName
		{
			get { return (string)this[MixmagGreatestDj.Columns.UrlName]; }
			set { this[MixmagGreatestDj.Columns.UrlName] = value; }
		}
		/// <summary>
		/// Name of the DJ
		/// </summary>
		public override string Name
		{
			get { return (string)this[MixmagGreatestDj.Columns.Name]; }
			set { this[MixmagGreatestDj.Columns.Name] = value; }
		}
		/// <summary>
		/// Url of a small image
		/// </summary>
		public override string ImageUrl
		{
			get { return (string)this[MixmagGreatestDj.Columns.ImageUrl]; }
			set { this[MixmagGreatestDj.Columns.ImageUrl] = value; }
		}
		/// <summary>
		/// Youtube id of the video
		/// </summary>
		public override string YoutubeId
		{
			get { return (string)this[MixmagGreatestDj.Columns.YoutubeId]; }
			set { this[MixmagGreatestDj.Columns.YoutubeId] = value; }
		}
		/// <summary>
		/// Description
		/// </summary>
		public override string Description
		{
			get { return (string)this[MixmagGreatestDj.Columns.Description]; }
			set { this[MixmagGreatestDj.Columns.Description] = value; }
		}
		/// <summary>
		/// Long description
		/// </summary>
		public override string LongDescription
		{
			get { return (string)this[MixmagGreatestDj.Columns.LongDescription]; }
			set { this[MixmagGreatestDj.Columns.LongDescription] = value; }
		}
		/// <summary>
		/// Short description
		/// </summary>
		public override string ShortDescription
		{
			get { return (string)this[MixmagGreatestDj.Columns.ShortDescription]; }
			set { this[MixmagGreatestDj.Columns.ShortDescription] = value; }
		}
		/// <summary>
		/// Large image - should be 200px square
		/// </summary>
		public override string LargeImageUrl
		{
			get { return (string)this[MixmagGreatestDj.Columns.LargeImageUrl]; }
			set { this[MixmagGreatestDj.Columns.LargeImageUrl] = value; }
		}
		/// <summary>
		/// Image of the interviewer
		/// </summary>
		public override string InterviewImageUrl
		{
			get { return (string)this[MixmagGreatestDj.Columns.InterviewImageUrl]; }
			set { this[MixmagGreatestDj.Columns.InterviewImageUrl] = value; }
		}
		/// <summary>
		/// Twitter name of the artist
		/// </summary>
		public override string TwitterName
		{
			get { return (string)this[MixmagGreatestDj.Columns.TwitterName]; }
			set { this[MixmagGreatestDj.Columns.TwitterName] = value; }
		}
		/// <summary>
		/// Second youtube clip
		/// </summary>
		public override string YoutubeId2
		{
			get { return (string)this[MixmagGreatestDj.Columns.YoutubeId2]; }
			set { this[MixmagGreatestDj.Columns.YoutubeId2] = value; }	
		}
		/// <summary>
		/// Total votes
		/// </summary>
		public override int? TotalVotes
		{
			get { return (int?)this[MixmagGreatestDj.Columns.TotalVotes]; }
			set { this[MixmagGreatestDj.Columns.TotalVotes] = value; }
		}
		/// <summary>
		/// Is this in the public nominated section?
		/// </summary>
		public override bool? IsPublicNominated
		{
			get { return (bool?)this[MixmagGreatestDj.Columns.IsPublicNominated]; }
			set { this[MixmagGreatestDj.Columns.IsPublicNominated] = value; }
		}
		/// <summary>
		/// Plural word - is / are
		/// </summary>
		public override string PluralWord
		{
			get { return (string)this[MixmagGreatestDj.Columns.PluralWord]; }
			set { this[MixmagGreatestDj.Columns.PluralWord] = value; }
		}
		/// <summary>
		/// Name short version
		/// </summary>
		public override string ShortName
		{
			get { return (string)this[MixmagGreatestDj.Columns.ShortName]; }
			set { this[MixmagGreatestDj.Columns.ShortName] = value; }
		}
		/// <summary>
		/// Stealth mode for wildly unpopular acts
		/// </summary>
		public override bool StealthMode
		{
			get { return (bool)this[MixmagGreatestDj.Columns.StealthMode]; }
			set { this[MixmagGreatestDj.Columns.StealthMode] = value; }
		}
		#endregion

		public bool IsHidden
		{
			get
			{
				return (this.IsPublicNominated.HasValue && this.IsPublicNominated.Value && this.TotalVotes <= 10) || (this.StealthMode);
			}
		}

		public string Image200Url
		{
			get
			{
				return "http://mixmag-greatest.com/gfx/greatest/img1/" + this.UrlName + "_200x200.jpg";
			}
		}
		public string Image160Url
		{
			get
			{
				return "http://mixmag-greatest.com/gfx/greatest/img1/" + this.UrlName + "_160x160.jpg";
			}
		}

		public string Image90Url
		{
			get
			{
				return "http://mixmag-greatest.com/gfx/greatest/img1/" + this.UrlName + "_90x90.jpg";
			}
		}
	}
	#endregion
}
