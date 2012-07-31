using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class Contact : DsiUserControl
	{
		protected HtmlImage JohnPic, TimPic, DavePic;
		protected HtmlAnchor JohnLink, TimLink, DaveLink;
		protected DataList SuperAdminDataList, SeniorAdminDataList, JuniorAdminDataList;

		#region User
		public Usr User(string nickname)
		{
			if (user == null)
				user = new Dictionary<string, Usr>();
			if (!user.ContainsKey(nickname))
				user[nickname] = Usr.GetFromNickName(nickname);
			return user[nickname];
		}
		public Usr User(int k)
		{
			if (userK == null)
				userK = new Dictionary<int, Usr>();
			try
			{
				if (!userK.ContainsKey(k))
					userK[k] = new Usr(k);
			}
			catch
			{
				return new Usr(4);
			}
			return userK[k];
		}
		Dictionary<string, Usr> user;
		Dictionary<int, Usr> userK;
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			ContainerPage.SetPageTitle("Contact us");
			//Usr john = new Usr(1);
			//Usr tim = new Usr(2);
			//Usr dave = new Usr(4);
			//JohnPic.Src = john.AnyPicPath;
			//john.MakeRolloverNoPic(JohnPic);
			//DavePic.Src = dave.AnyPicPath;
			//dave.MakeRolloverNoPic(DavePic);
			//TimPic.Src = tim.AnyPicPath;
			//tim.MakeRolloverNoPic(TimPic);

			//JohnLink.HRef = john.Url();
			//JohnLink.InnerText = john.NickName;
			//TimLink.HRef = tim.Url();
			//TimLink.InnerText = tim.NickName;
			//DaveLink.HRef = dave.Url();
			//DaveLink.InnerText = dave.NickName;

			Query qSuper = new Query();
			qSuper.NoLock = true;
			qSuper.QueryCondition = new And(
				new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Super),
				new Q(Usr.Columns.IsAdmin, false)
			);
			qSuper.OrderBy = new OrderBy(Usr.Columns.DateTimeLastPageRequest, OrderBy.OrderDirection.Descending);
			qSuper.Columns = Usr.LinkColumns;
			UsrSet usSuper = new UsrSet(qSuper);
			SuperAdminDataList.DataSource = usSuper;
			SuperAdminDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Default.ascx");
			SuperAdminDataList.DataBind();

			Query qSenior = new Query();
			qSenior.NoLock = true;
			qSenior.QueryCondition = new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Senior);
			qSenior.OrderBy = new OrderBy(Usr.Columns.DateTimeLastPageRequest, OrderBy.OrderDirection.Descending);
			qSenior.Columns = Usr.LinkColumns;
			UsrSet usSenior = new UsrSet(qSenior);
			SeniorAdminDataList.DataSource = usSenior;
			SeniorAdminDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Default.ascx");
			SeniorAdminDataList.DataBind();

			Query qJunior = new Query();
			qJunior.NoLock = true;
			qJunior.QueryCondition = new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Junior);
			qJunior.OrderBy = new OrderBy(Usr.Columns.DateTimeLastPageRequest, OrderBy.OrderDirection.Descending);
			qJunior.Columns = Usr.LinkColumns;
			UsrSet usJunior = new UsrSet(qJunior);
			JuniorAdminDataList.DataSource = usJunior;
			JuniorAdminDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Default.ascx");
			JuniorAdminDataList.DataBind();
		}

	}
}
