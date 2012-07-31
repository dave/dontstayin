using System;//
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using Spotted.Controls;

namespace Spotted.Support
{
	public partial class Resize : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs eA)
		{
			Query q = new Query();
			q.TopRecords = 1;
			q.QueryCondition = new Q(Bobs.Event.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty);
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			EventSet es = new EventSet(q);
			
			IPic pic = (IPic)es[0];
			Response.ContentType = "image/jpg";
			Response.Clear();

			if (pic.PicMiscK > 0 && pic.PicMisc != null)
			{

				Bobs.Cropper c = new Bobs.Cropper();
				//c.ImageUrl = e.PicMisc.;
				c.ImageGuid = pic.PicMisc.Guid;
				c.ImageStore = Storage.Stores.Pix;
				c.SetState(pic.PicState);

				double factor = 1.79;

				int minDimension = pic.PicMisc.Height > pic.PicMisc.Width ? pic.PicMisc.Width : pic.PicMisc.Height;
				if (minDimension < (int)(100 * factor))
				{
					factor = factor * ((double)minDimension / (double)(int)(100 * factor));
				}

				c.CropHeight = 179;
				c.CropWidth = 179;
				c.Zoom = c.Zoom * 1.79;

				c.ResetStateToEnsureImageIsWithinCropArea();

				Response.BinaryWrite(c.GetBytes());

			}
			else
			{
				byte[] b = Storage.GetFromStore(Storage.Stores.Pix, pic.Pic, "jpg");
				Response.BinaryWrite(b);
			}
			Response.Flush();
			Response.End();


		}
	}
}
