using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace MixmagSubscription
{
	public partial class Stats : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Dg.DataSource = Cambro.Misc.Db.Dr(@"
SELECT 
CONVERT(varchar, YEAR(DateTimeCreated)) + '-' + CONVERT(varchar, MONTH(DateTimeCreated)) + '-' + CONVERT(varchar,DAY(DateTimeCreated)) as Date,
count(*) as Subscriptions, 
(select count(*) from MixmagSubscription ms1 WHERE IsAddressComplete=1 and YEAR(ms.DateTimeCreated) = YEAR(ms1.DateTimeCreated) and MONTH(ms.DateTimeCreated)=MONTH(ms1.DateTimeCreated) and DAY(ms.DateTimeCreated) = DAY(ms1.DateTimeCreated)) as AddressComplete, 
(select count(*) from MixmagSubscription ms1 WHERE IsEmailVerified=1 and YEAR(ms.DateTimeCreated) = YEAR(ms1.DateTimeCreated) and MONTH(ms.DateTimeCreated)=MONTH(ms1.DateTimeCreated) and DAY(ms.DateTimeCreated) = DAY(ms1.DateTimeCreated)) as EmailVerified,
(select count(*) from MixmagRead mr1 WHERE YEAR(ms.DateTimeCreated) = YEAR(mr1.DateTimeRead) and MONTH(ms.DateTimeCreated)=MONTH(mr1.DateTimeRead) and DAY(ms.DateTimeCreated) = DAY(mr1.DateTimeRead)) as Reads,
(select count(*) from MixmagRead mr1 WHERE mr1.StoryPublished=1 AND YEAR(ms.DateTimeCreated) = YEAR(mr1.DateTimeRead) and MONTH(ms.DateTimeCreated)=MONTH(mr1.DateTimeRead) and DAY(ms.DateTimeCreated) = DAY(mr1.DateTimeRead)) as FacebookMessages
FROM MixmagSubscription ms GROUP BY YEAR(DateTimeCreated), MONTH(DateTimeCreated), DAY(DateTimeCreated) ORDER BY YEAR(DateTimeCreated) DESC, MONTH(DateTimeCreated) DESC, DAY(DateTimeCreated) DESC
");
			Dg.DataBind();

		}
	}
}
