using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.ThreadControl
{
	public class ThreadControlService
	{
		public GroupSet GetUsrsGroups(int usrK)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(GroupUsr.Columns.UsrK, usrK),
				new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
				);
			q.Columns = new ColumnSet(
				Group.Columns.Name,
				Group.Columns.K
				);
			q.OrderBy = new OrderBy(
				new OrderBy(GroupUsr.Columns.Favourite, OrderBy.OrderDirection.Descending),
				new OrderBy(Group.Columns.Name));
			q.TableElement = Group.UsrMemberJoin;
			GroupSet gs = new GroupSet(q);
			return gs;
		}
	}
}
