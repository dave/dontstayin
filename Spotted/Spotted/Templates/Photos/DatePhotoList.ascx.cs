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
	public partial class DatePhotoList : System.Web.UI.UserControl
	{
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
