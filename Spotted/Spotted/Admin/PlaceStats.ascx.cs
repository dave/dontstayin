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

namespace Spotted.Admin
{
	public partial class PlaceStats : AdminUserControl
	{
		#region CurrentPlace
		public Place CurrentPlace
		{
			get
			{
				if (currentPlace == null && ContainerPage.Url["PlaceK"].IsInt)
					currentPlace = new Place(ContainerPage.Url["PlaceK"]);
				return currentPlace;
			}
			set
			{
				currentPlace = value;
			}
		}
		private Place currentPlace;
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			PlaceName.Text = CurrentPlace.Name;
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new Q(MusicType.Columns.ParentK, QueryOperator.LessThanOrEqualTo, 1);
			q.OrderBy = new OrderBy(MusicType.Columns.Order);
			MusicTypeSet mts = new MusicTypeSet(q);
			int i = 0;
			foreach (MusicType mt in mts)
			{
				HtmlTableRow row = new HtmlTableRow();
				HtmlTableCell cellName = new HtmlTableCell();
				cellName.InnerText = mt.Name;
				row.Cells.Add(cellName);

				Q MusicQ = null;
				TableElement MyJoin = null;
				Q ActivityQ = new Q(true);

				if (mt.K == 1)
				{
					MusicQ = new Q(true);
					MyJoin = new JoinLeft(Usr.Columns.K, UsrPlaceVisit.Columns.UsrK);
				}
				else
				{
					ArrayList musicTypesQ = new ArrayList();
					ArrayList musicTypesK = new ArrayList();
					musicTypesQ.Add(new Q(Usr.Columns.FavouriteMusicTypeK, mt.K));
					musicTypesQ.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, mt.K));
					musicTypesK.Add(mt.K);
					AddMusicTypeQChildren(musicTypesQ, mt, ref musicTypesK);
					MusicQ = new Or((Q[])musicTypesQ.ToArray(typeof(Q)));
					MyJoin = new Join(
						new JoinLeft(Usr.Columns.K, UsrPlaceVisit.Columns.UsrK),
						new TableElement(TablesEnum.UsrMusicTypeFavourite),
						QueryJoinType.Left,
						Usr.Columns.K,
						UsrMusicTypeFavourite.Columns.UsrK);
				}
				Q PlaceQ = new Or(
					new Q(Usr.Columns.HomePlaceK, CurrentPlace.K),
					new Q(UsrPlaceVisit.Columns.PlaceK, CurrentPlace.K));


				#region All users
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ
					);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Emails
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ,
						new Q(Usr.Columns.SendSpottedEmails, true)
					);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Flyers
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ,
						new Q(Usr.Columns.SendFlyers, true)
						);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Invites
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ,
						new Q(Usr.Columns.SendInvites, true)
						);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Texts
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						new Q[]{
							Usr.IsNotSkeletonQ,
							Usr.IsEmailVerifiedQ,
							ActivityQ,
							MusicQ,
							PlaceQ,
							new Q(Usr.Columns.SendSpottedTexts,true),
							new Q(Usr.Columns.Mobile,QueryOperator.NotEqualTo,"")
						}
					);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion

				HtmlTableCell cellMid = new HtmlTableCell();
				cellMid.Align = "center";
				cellMid.InnerHtml = "&nbsp;";
				row.Cells.Add(cellMid);

				ActivityQ = new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.GreaterThan, DateTime.Now.AddMonths(-1));

				#region All users
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ
						);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Emails
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ,
						new Q(Usr.Columns.SendSpottedEmails, true)
						);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Flyers
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ,
						new Q(Usr.Columns.SendFlyers, true)
						);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Invites
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						Usr.IsNotSkeletonQ,
						Usr.IsEmailVerifiedQ,
						ActivityQ,
						MusicQ,
						PlaceQ,
						new Q(Usr.Columns.SendInvites, true)
						);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion
				#region Texts
				if (true)
				{
					Query q1 = new Query();
					q1.Columns = new ColumnSet(Usr.Columns.K);
					q1.DistinctColumn = Usr.Columns.K;
					q1.Distinct = true;
					q1.ReturnCountOnly = true;
					q1.QueryCondition = new And(
						new Q[]{
								   Usr.IsNotSkeletonQ,
								   Usr.IsEmailVerifiedQ,
								   ActivityQ,
								   MusicQ,
								   PlaceQ,
								   new Q(Usr.Columns.SendSpottedTexts,true),
								   new Q(Usr.Columns.Mobile,QueryOperator.NotEqualTo,"")
							   }
						);
					q1.TableElement = MyJoin;
					UsrSet us1 = new UsrSet(q1);
					HtmlTableCell cell1 = new HtmlTableCell();
					cell1.InnerText = us1.Count.ToString();
					row.Cells.Add(cell1);
				}
				#endregion

				if (i % 2 == 0)
					row.Attributes["class"] = "dataGridAltItem";

				i++;

				Tab.Rows.Add(row);
			}

		}
		static void AddMusicTypeQChildren(ArrayList musicTypesQ, MusicType mt, ref ArrayList musicTypesK)
		{
			if (mt.Children.Count > 0)
			{
				foreach (MusicType mtChild in mt.Children)
				{
					if (!musicTypesK.Contains(mtChild.K))
					{
						musicTypesQ.Add(new Q(Usr.Columns.FavouriteMusicTypeK, mtChild.K));
						musicTypesQ.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, mtChild.K));
						musicTypesK.Add(mtChild.K);
					}
				}
			}
		}
	}
}
