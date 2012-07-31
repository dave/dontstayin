using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.Xml;

namespace Spotted.Support
{
	public partial class HitPhoto : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HttpContext.Current.Items["VisitPhotos"] = 1;
			Response.ContentType="text/xml";
			string photoKString = "0";
			bool currentUsrInPhoto = false;
			bool currentUsrFavourite = false;
			string message = null;
			string exceptionMessage = null;
			try
			{
				XmlDocument requestXml = new XmlDocument();
				requestXml.Load(Request.InputStream);
				photoKString = requestXml.DocumentElement.Attributes["k"].Value;
				Photo currentPhoto = new Photo(int.Parse(photoKString));
				currentPhoto.IncrementViews();

				try
				{
					if (currentPhoto.Overlay.Equals(Photo.Overlays.DsiLogoBottomRightThinkBottomLeft) || currentPhoto.Overlay.Equals(Photo.Overlays.DsiLogoBottomRightThinkTextBottomLeft))
					{
						//int bannerK, Banner.Positions position, DateTime date, int hits, int uniqueHits, int clicks
						Bobs.BannerStat.Log(currentPhoto.Overlay.Equals(Photo.Overlays.DsiLogoBottomRightThinkBottomLeft) ? 9295 : 9296, Banner.Positions.Hotbox, DateTime.Now, 1, 0, 0);
					}
				}
				catch { }

				if (Usr.Current != null)
				{
					try
					{
						UsrPhotoMe upm = new UsrPhotoMe(Usr.Current.K, currentPhoto.K);
						currentUsrInPhoto = true;
					}
					catch { }
					try
					{
						UsrPhotoFavourite upf = new UsrPhotoFavourite(Usr.Current.K, currentPhoto.K);
						currentUsrFavourite = true;
					}
					catch { }
				}
				message = currentPhoto.UsrHtml;
			}
			catch(Exception ex)
			{
				currentUsrInPhoto = false;
				currentUsrFavourite = false;
				exceptionMessage = ex.Message;
				message = "Error!";
			}


			XmlTextWriter x = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
			try
			{
				x.WriteStartElement("doc");
				x.WriteAttributeString("k", photoKString);
				x.WriteAttributeString("me", currentUsrInPhoto ? "1" : "0");
				x.WriteAttributeString("fav", currentUsrFavourite ? "1" : "0");
				if (exceptionMessage != null) { x.WriteAttributeString("ex", exceptionMessage); }
				x.WriteString(message);
			}
			finally
			{
				x.WriteEndElement();//doc
				x.Close();
			}
		}
	}
}
