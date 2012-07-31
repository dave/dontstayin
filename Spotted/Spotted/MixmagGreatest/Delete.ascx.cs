using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using Newtonsoft.Json.Linq;
using Bobs;

namespace Spotted.MixmagGreatest
{
	public partial class Delete : MixmagGreatestUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.MixmagGreatest);
			JObject user = facebook.GetObject("me", null);

			MixmagGreatestVoteSet vs = new MixmagGreatestVoteSet(new Query(new Q(MixmagGreatestVote.Columns.FacebookUid, facebook.Uid)));

			if (vs.Count > 0)
			{

				try
				{

					Update u = new Update();
					u.Table = TablesEnum.MixmagGreatestDj;
					u.Changes.Add(new Assign.Subtraction(MixmagGreatestDj.Columns.TotalVotes, 1));
					u.Where = new Q(MixmagGreatestDj.Columns.K, vs[0].MixmagGreatestDjK);
					u.Run();
				}
				catch { }

				vs[0].Delete();
				Response.Write("Done.");

			}
			else
				Response.Write("Nothing to delete!");



		}
	}
}
