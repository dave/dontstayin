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

namespace Spotted.Admin
{
	public partial class SalesStats : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataView dvCalls = Cambro.Misc.Db.Dv(@"select 
CONVERT(varchar(10), NickName) AS SalesPerson, 
DATENAME(dw, DateTimeStart) + ' ' + CONVERT(varchar(2), DAY(DateTimeStart)) AS Date,
(select sum(DATEDIFF(n, DateTimeStart, DateTimeEnd)) from SalesCall AS Ssc1 where YEAR(Ssc1.DateTimeStart) = YEAR(Sc.DateTimeStart) AND MONTH(Ssc1.DateTimeStart) = MONTH(Sc.DateTimeStart) AND DAY(Ssc1.DateTimeStart) = DAY(Sc.DateTimeStart) AND Ssc1.UsrK = Sc.UsrK AND Ssc1.[IsCall]=1) as Minutes,
(select count(*) from SalesStatusChange AS Ssc1 where YEAR(Ssc1.DateTime) = YEAR(Sc.DateTimeStart) AND MONTH(Ssc1.DateTime) = MONTH(Sc.DateTimeStart) AND DAY(Ssc1.DateTime) = DAY(Sc.DateTimeStart) AND Ssc1.UsrK = Sc.UsrK AND Ssc1.[Type]=1) as NewProactive,
(select count(*) from SalesStatusChange AS Ssc1 where YEAR(Ssc1.DateTime) = YEAR(Sc.DateTimeStart) AND MONTH(Ssc1.DateTime) = MONTH(Sc.DateTimeStart) AND DAY(Ssc1.DateTime) = DAY(Sc.DateTimeStart) AND Ssc1.UsrK = Sc.UsrK AND Ssc1.[Type]=2) as NewActive,
(select count(*) from SalesCall AS Sc1 where YEAR(Sc1.DateTimeStart) = YEAR(Sc.DateTimeStart) AND MONTH(Sc1.DateTimeStart) = MONTH(Sc.DateTimeStart) AND DAY(Sc1.DateTimeStart) = DAY(Sc.DateTimeStart) AND Sc1.UsrK = Sc.UsrK AND Sc1.IsCall=1 AND Sc1.Direction=2) as Incoming,
(select count(*) from SalesCall AS Sc1 where YEAR(Sc1.DateTimeStart) = YEAR(Sc.DateTimeStart) AND MONTH(Sc1.DateTimeStart) = MONTH(Sc.DateTimeStart) AND DAY(Sc1.DateTimeStart) = DAY(Sc.DateTimeStart) AND Sc1.UsrK = Sc.UsrK AND Sc1.IsCall=1 AND sc1.Direction=1) as Outgoing,
(select count(*) from SalesCall AS Sc1 where YEAR(Sc1.DateTimeStart) = YEAR(Sc.DateTimeStart) AND MONTH(Sc1.DateTimeStart) = MONTH(Sc.DateTimeStart) AND DAY(Sc1.DateTimeStart) = DAY(Sc.DateTimeStart) AND Sc1.UsrK = Sc.UsrK AND Sc1.IsCall=1 AND sc1.Direction=1 AND Sc1.Effective=1) as Effective,
(select count(*) from SalesCall AS Sc1 where YEAR(Sc1.DateTimeStart) = YEAR(Sc.DateTimeStart) AND MONTH(Sc1.DateTimeStart) = MONTH(Sc.DateTimeStart) AND DAY(Sc1.DateTimeStart) = DAY(Sc.DateTimeStart) AND Sc1.UsrK = Sc.UsrK AND Sc1.IsCall=1 AND sc1.Direction=1 AND Sc1.Effective=1 AND [Type]=1) as Cold,
(select count(*) from SalesCall AS Sc1 where YEAR(Sc1.DateTimeStart) = YEAR(Sc.DateTimeStart) AND MONTH(Sc1.DateTimeStart) = MONTH(Sc.DateTimeStart) AND DAY(Sc1.DateTimeStart) = DAY(Sc.DateTimeStart) AND Sc1.UsrK = Sc.UsrK AND Sc1.IsCall=1 AND sc1.Direction=1 AND Sc1.Effective=1 AND [Type]=2) as FollowUp,
(select count(*) from SalesCall AS Sc1 where YEAR(Sc1.DateTimeStart) = YEAR(Sc.DateTimeStart) AND MONTH(Sc1.DateTimeStart) = MONTH(Sc.DateTimeStart) AND DAY(Sc1.DateTimeStart) = DAY(Sc.DateTimeStart) AND Sc1.UsrK = Sc.UsrK AND Sc1.IsCall=1 AND sc1.Direction=1 AND Sc1.Effective=1 AND [Type]=3) as Active
FROM SalesCall AS Sc inner join Usr on Sc.UsrK=Usr.K
WHERE IsCall=1 AND DateTimeStart > DATEADD(week, -1, DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))) 
group by NickName, DAY(DateTimeStart), MONTH(DateTimeStart), YEAR(DateTimeStart), Sc.UsrK, DATENAME(dw, DateTimeStart)
order by NickName, YEAR(DateTimeStart) desc, MONTH(DateTimeStart) desc, DAY(DateTimeStart) desc");

			CallsDataGrid.DataSource = dvCalls;
			CallsDataGrid.DataBind();


			DataView dvSales = Cambro.Misc.Db.Dv(@"SELECT  
CONVERT(varchar(10), NickName) AS SalesPerson,
DATENAME(month, PaidDateTime) AS Date,
sum(SalesUsrAmount) as Sales
FROM Invoice inner join Usr on SalesUsrK=Usr.K
WHERE Paid=1 AND PaidDateTime> DATEADD(month, -6, DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))) 
group by NickName, MONTH(PaidDateTime), YEAR(PaidDateTime), DATENAME(month, PaidDateTime)
order by YEAR(PaidDateTime) desc, MONTH(PaidDateTime) desc, NickName, Sales desc");

			MonthlySalesDataGrid.DataSource = dvSales;
			MonthlySalesDataGrid.DataBind();


			DataView dvSalesDaily = Cambro.Misc.Db.Dv(@"SELECT  
CONVERT(varchar(10), NickName) AS SalesPerson,
DATENAME(dw, PaidDateTime) + ' ' + CONVERT(varchar(2), DAY(PaidDateTime)) AS Date,
sum(SalesUsrAmount) as Sales
FROM Invoice inner join Usr on SalesUsrK=Usr.K
WHERE Paid=1 AND PaidDateTime > DATEADD(week, -1, DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))) 
group by NickName, DAY(PaidDateTime), MONTH(PaidDateTime), YEAR(PaidDateTime), DATENAME(dw, PaidDateTime)
order by NickName, YEAR(PaidDateTime) desc, MONTH(PaidDateTime) desc, DAY(PaidDateTime) desc, Sales desc");

			DailySalesDataGrid.DataSource = dvSalesDaily;
			DailySalesDataGrid.DataBind();
		}
	}
}
