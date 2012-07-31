using System;
using Bobs;

namespace Spotted.Support
{
	public partial class Log : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int logItemValue;
			if (int.TryParse(Request.QueryString["Item"], out logItemValue))
			{
				Bobs.Log.Items logItem = (Bobs.Log.Items)logItemValue;
				switch (logItem)
				{
					case Bobs.Log.Items.FindYourPhotoClicked:
						{
							Bobs.Log.Increment(logItem);
							Response.Redirect("/pages/findyourphoto");
							break;
						}
					case Bobs.Log.Items.FindYourFriendsClicked:
						{
							Bobs.Log.Increment(logItem);
							Response.Redirect("/pages/findyourfriends");
							break;
						}
					case Bobs.Log.Items.UploadPhotosClicked:
						{
							Bobs.Log.Increment(logItem);
							Response.Redirect("/pages/uploadphotos");
							break;
						}
					case Bobs.Log.Items.FindEventsClicked:
						{
							Bobs.Log.Increment(logItem);
							Response.Redirect("/pages/findevents");
							break;
						}
					case Bobs.Log.Items.FindTicketsClicked:
						{
							Bobs.Log.Increment(logItem);
							Response.Redirect("/pages/findtickets");
							break;
						}
					case Bobs.Log.Items.AddEventClicked:
						{
							Bobs.Log.Increment(logItem);
							Response.Redirect("/pages/events/edit");
							break;
						}
					case Bobs.Log.Items.FreeGuestlistClicked:
						{
							Bobs.Log.Increment(logItem);
							Response.Redirect("/pages/freeguestlist");
							break;
						}
					default:
						{
							Response.Redirect("/");
							break;
						}
				}
			}
			else
			{
				Response.Redirect("/");
			}
		}
	}
}
