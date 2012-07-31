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

namespace Spotted.Blank
{
	public partial class BannerChat : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Banner b = new Banner(ContainerPage.Url[0]);
			if (b.PromoterK == 7950 || (Vars.DevEnv && b.PromoterK == 1622))
			{
				b.RegisterClick();
				//do something in here to register a click with unruly
				if (Usr.Current != null)
				{

					try
					{
						Thread t = new Thread(int.Parse(b.LinkUrl.Substring(b.LinkUrl.LastIndexOf('-') + 1)));


						Group g = t.Group;// new Group(4307);
						GroupUsr gu = g.GetGroupUsr(Usr.Current);

						if (gu == null || !g.IsMember(gu))
						{
							try
							{
								g.Join(Usr.Current, gu);
							}
							catch { }
						}
					}
					catch { }

				}
				Response.Redirect(b.LinkUrl.Replace("http://www.dontstayin.com", string.Empty));
			}
			else
				throw new Exception();
		}
	}
}
