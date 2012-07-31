using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using Bobs;
using Js.Controls.PhotoControl;
using System.Collections.Generic;

namespace Spotted.WebServices.Controls.PhotoControl
{
	/// <summary>
	/// Summary description for PhotoBrowserService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public BannerStub[] GetBanners(string placeholderClientID)
		{
			var server = new Bobs.BannerServer.Server();
			return
				// TODO check isHttps
				server.GetBanners(Banner.Positions.PhotoBanner, false, 20, Bobs.BannerServer.Identity.Current, new Bobs.BannerServer.Rules.RequestRules())
				.ConvertAll(b => b == null ? null : b.Banner)
				.ConvertAll(b => b == null ? null : new BannerStub()
				{
					k = b.K,
					html = b.RenderAsHtml(),
					script = b.GetEmbedScript(placeholderClientID)
				}).ToArray();
		}


		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public void RegisterBannerHit(int bannerK)
		{
			new Banner(bannerK).RegisterHit();
			Log.Increment(Log.Items.PhotoBannerRender);
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public void IncrementViews(int photoK)
		{
			new Photo(photoK).IncrementViews();
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public void SetIsFavouritePhoto(int photoK, bool isFavourite)
		{
			new Photo(photoK).SetIsFavouritePhoto(isFavourite);
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public string[] SetCurrentUsrSpottedInPhoto(int photoK, bool isInPhoto)
		{
			return SerUsrSpottedInPhoto(Usr.Current, photoK, isInPhoto);
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public string[] SetUsrSpottedInPhoto(int spottedUsrK, int photoK, bool isInPhoto)
		{
			// do some validation to check current usr is allowed to spot spottedUsrK?
			return SerUsrSpottedInPhoto(new Usr(spottedUsrK), photoK, isInPhoto);
		}

		private string[] SerUsrSpottedInPhoto(Usr spottedUsr, int photoK, bool isInPhoto)
		{
			Photo p = new Photo(photoK);
			spottedUsr.SetSpottedInPhoto(p, Usr.Current, isInPhoto);
			return new string[] { p.UsrsInPhotoHtml, p.RolloverMouseOverText };
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public void SetAsCompetitionGroupPhoto(int photoK, bool isCompetitionPhoto)
		{
			if (Usr.Current != null && (Usr.Current.IsAdmin || Usr.Current.K == new Photo(photoK).UsrK))
			{
				if (isCompetitionPhoto)
				{
					GroupPhoto gp = new GroupPhoto()
					{
						PhotoK = photoK,
						GroupK = Vars.CompetitionGroupK,
						DateTime = DateTime.Now,
						AddedByUsrK = Usr.Current.K,
						ShowOnFrontPage = false
					};
					gp.Update();
				}
				else
				{
					try
					{
						GroupPhoto gp = new GroupPhoto(Vars.CompetitionGroupK, photoK);
						gp.Delete();
					}
					catch (BobNotFound)
					{ }
				}
			}
			else
			{
				throw new Exception("You don't have permission to do this!");
			}
		}

	}
}
