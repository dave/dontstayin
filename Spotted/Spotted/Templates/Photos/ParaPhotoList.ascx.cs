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

namespace Spotted.Templates.Photos
{
	public partial class ParaPhotoList : System.Web.UI.UserControl
	{
		protected string Attribs
		{
			get
			{
				string html = "onclick=\"SwitchPhoto(" + CurrentPhoto.K + ",'" + CurrentPhoto.IconPath + "','" + CurrentPhoto.ThumbPath + "','" + CurrentPhoto.WebPath + "');return false;\";";
				return html;
			}
		}


		protected Photo CurrentPhoto
		{
			get
			{
				if (currentPhoto == null)
					currentPhoto = ((Photo)((RepeaterItem)NamingContainer).DataItem);
				return currentPhoto;
			}
		}
		Photo currentPhoto;
	}
}
