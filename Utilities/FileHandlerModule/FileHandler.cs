using System;
using System.IO;
using System.Net;
using System.Web;
using Bobs;

namespace FileHandlerModule
{
	public class FileHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			string[] splitPath = context.Request.Path.Split('/');
			if (splitPath.Length == 4 && splitPath[1].ToLower() == "images" && splitPath[2].ToLower() == "flyer")
			{
				string fileName = splitPath[3];
				int flyerK = int.Parse(fileName.Substring(0, fileName.IndexOf(".")));
				Flyer f = new Flyer(flyerK);
				
				if (f.MiscK == 0)
					Storage.WriteFileToHttpResponse(context, Storage.Stores.Pix, new Guid("5a106560-1989-4ecb-86bd-e52ba8689352"), "gif");
				else
					Storage.WriteFileToHttpResponse(context, Storage.Stores.Pix, f.Misc.Guid, f.Misc.Extention);

				Utilities.GetSafeThread(() => f.LogView()).Start();
			}
			else if ((splitPath.Length == 4 || splitPath.Length == 6) && splitPath[1].ToLower() == "images" && (splitPath[2].ToLower() == "pix" || splitPath[2].ToLower() == "master"))
			{
				string fileName = splitPath[splitPath.Length - 1];
				Guid guid = new Guid(fileName.Substring(0, fileName.IndexOf(".")));
				string extention = fileName.Substring(fileName.IndexOf(".") + 1);
				Storage.WriteFileToHttpResponse(context, splitPath[2].ToLower() == "pix" ? Storage.Stores.Pix : Storage.Stores.Master, guid, extention);
			}
			else if ((splitPath.Length == 4 || splitPath.Length == 6) && splitPath[1].ToLower() == "images" && (splitPath[2].ToLower() == "pixredirect" || splitPath[2].ToLower() == "masterredirect"))
			{
				string fileName = splitPath[splitPath.Length - 1];
				Guid guid = new Guid(fileName.Substring(0, fileName.IndexOf(".")));
				string extention = fileName.Substring(fileName.IndexOf(".") + 1);
				HttpContext.Current.Response.Redirect(Storage.Path(guid, extention, splitPath[2].ToLower() == "pixredirect" ? Storage.Stores.Pix : Storage.Stores.Master)); 
				//Storage.WriteFileToHttpResponse(context, splitPath[2].ToLower() == "pix" ? Storage.Stores.Pix : Storage.Stores.Master, guid, extention);
			}
			else if (splitPath.Length == 4 && splitPath[1].ToLower() == "files" && splitPath[2].ToLower() == "styledcss" && splitPath[3].ToLower().EndsWith(".css"))
			{
				string[] filename = splitPath[3].ToLower().Split('-');
				string objectType = filename[0];
				int K = int.Parse(filename[1].Substring(0, filename[1].Length - 4));

				if (objectType == "brand")
				{
					Brand b = new Brand(K);
					context.Response.Write(b.StyledCss);
				}
				else if (objectType == "venue")
				{
					Venue v = new Venue(K);
					context.Response.Write(v.StyledCss);
				}
			}
		}

		public bool IsReusable
		{
			get { return true; }
		}
	}
}
