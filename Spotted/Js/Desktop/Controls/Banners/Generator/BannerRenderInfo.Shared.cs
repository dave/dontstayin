using System;

namespace Js.Controls.Banners.Generator
{
	public class BannerRenderInfo
	{
		public BannerRenderInfoType bannerRenderInfoType;
		public int k;

		public string flashUrl;
		public int height;
		public int width;
		public string html;
		/// <summary>
		/// Number of ms to wait before changing banner
		/// </summary>
		public int duration;
		public bool miscNeedsClickHelper;
		public string flashVersion;
		public string linkTag;
		public string targetTag;
	}
	public enum BannerRenderInfoType
	{
		Flash = 1,
		Html = 2
	}
}
