using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bobs
{


	public class MixmagComp
	{
		public MixmagComp() { }

		public int K;
		public string Name;
		public bool Enabled;
		public DateTime StartDate;
		public DateTime EndDate;

		public long PageIdToLike;

		public string FacebookPostName;
		public string FacebookPostCaption;
		public string FacebookPostDescription;
		public string FacebookEntryMessageDefault;
		public string FacebookVoteMessage;

		public string GlobalHeader;
		
		public string EntryHeader;
		public string EntryFooter;
		public string Entry1Top;
		public string Entry1Middle;
		public string Entry1ButtonText;
		public string Entry1DailyEmailTickBoxText;
		public string Entry1Bottom;

		
		public bool DailyEmailEnabled;
		public string DailyEmailHtmlCompSubject;
		public string DailyEmailHtmlEntry;
		public string DailyEmailHtmlComp;
		public string DailyEmailHtmlEntryMultiple;

		public string Entry2Top;
		public string Entry2Middle;
		public string Entry2LikeButton;
		public string Entry2Bottom;

		public string VoteHeaderFacebook;
		public string VoteHeaderMicrosite;
		public string VoteFooterFacebook;
		public string VoteFooterMicrosite;
		public string Vote1Top;
		public string Vote1Middle;
		public string Vote1ButtonText;
		public string Vote1Bottom;

		public string VoteLike;

		public string Vote2;

		public string RepostHeader;
		public string RepostFooter;
		public string Repost1Top;
		public string Repost1Middle;
		public string Repost1Bottom;
		public string Repost1ButtonText;
		public string Repost2;

		public static MixmagComp GetByK(int k)
		{
			if (k == 1)
			{
				MixmagComp c = new MixmagComp();
				c.K = 1;

				#region Globals
				c.Name = "Armani fashion";
				c.Enabled = true;
				c.StartDate = new DateTime(2011, 6, 1);
				c.EndDate = new DateTime(2012, 1, 1);

				c.PageIdToLike = 18658586341;
				c.GlobalHeader = @"
<link href='http://fonts.googleapis.com/css?family=Hammersmith+One&v1' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Bangers&v1' rel='stylesheet' type='text/css'>
<style>
	.fb_edge_widget_with_comment span.fb_edge_comment_widget iframe.fb_ltr { display: none !important; }
</style>
<style>
	body
	{
		background-color:#000000;
	}
	h1
	{
		margin-top:25px;
		margin-bottom:15px;
		font-size:50px;
		font-family: 'Bangers', arial, serif;
		-webkit-transform: rotate(-0.0000000001deg);

	}
	p
	{
		font-family: 'Hammersmith One', arial, serif;
		-webkit-transform: rotate(-0.0000000001deg);
	}
	.MainHolder
	{
		background-color:#ffffff;
		
		position: fixed;
		top:30px;
		padding-bottom:15px;
		
		left: 50%;
		width:500px;
		margin-left: -270px;
		padding-left:20px;
		padding-right:20px;
		
		overflow:auto;
		text-align:center;
		
	}
</style>
";


				#endregion

				#region Email
				c.DailyEmailHtmlCompSubject = "Armani DJ competition";
				c.DailyEmailHtmlComp = @"
<p>
	<b>Armani DJ competition.</b>
</p>
<p>
	The winner is the entry with the most votes! We only count votes for 30 days after you enter the competition.
</p>
";
				c.DailyEmailHtmlEntryMultiple = "<p><b>Entry %1</b></p>";
				c.DailyEmailHtmlEntry = @"
<p>
	<img src=""%1"" width=""150"">
</p>
<p>
	You have %2 vote%3 so far, and %4 day%5 left to get 
	votes.
</p>
<p>
	<a href=""%6"">Click here</a> to post the voting link to facebook. You can send this voting link to your friends:
</p>
<p>
	%7
</p>
";
				#endregion

				#region Facebook posts
				c.FacebookPostName = "Armani DJ competition";
				c.FacebookPostCaption = "";
				c.FacebookPostDescription = "You can help your friend win a fantastic prize by voting in the competition. The winner is the one with the most votes.";
				#endregion

				#region Entry
				c.EntryHeader = "<div class=\"MainHolder\">";
				{
					c.Entry1Top = "<h1>Enter the competition</h1>";
					c.Entry1Middle = "<p>Write a message to your friends, telling them why they should vote for your photo:</p>";
					c.FacebookEntryMessageDefault = "OMG I'm in the final of the Armani style competiton, but I need your vote. If I win I get to take ten mates to an exclusive VIP Armani party. Please vote for me! PLEEEEEASE!?!";
					c.DailyEmailEnabled = true;
					c.Entry1DailyEmailTickBoxText = "Email me daily for 30 days about my votes";
					c.Entry1ButtonText = "Enter competition now";
					c.Entry1Bottom = "";
				}
				{
					c.Entry2Top = "<h1>Done!</h1>";
					c.Entry2Middle =
						"<p>Thanks for entering the competition!</p>" +
						"<p>Here's a voting link you can send to your friends:</p>";
					c.Entry2LikeButton =
						"<p>Now click the like button to be kept up to date with Armani news and offers:</p>" +
						"<p><fb:like href=\"http://www.facebook.com/armani\" layout=\"box_count\" font=\"verdana\" width=\"200px\"></fb:like></p>";
					c.Entry2Bottom = "";
				}
				c.EntryFooter = "</div>";
				#endregion

				#region Vote
				c.VoteHeaderMicrosite = "<div class=\"MainHolder\">"; // should have less branding (will open in a popup over the already branded microsite)
				c.VoteHeaderFacebook = "<div class=\"MainHolder\">"; // should have more branding (will open in a full window over facebook)
				{
					c.Vote1Top = "<h1>Vote</h1>";
					c.Vote1Middle = "<p>First click the vote button to vote for %1:</p>";
					c.Vote1ButtonText = "Vote for %1 now";
					c.Vote1Bottom = "";

					c.FacebookVoteMessage = "I've voted for my friend in the Armani DJ competition. Please vote too and we could all win entry to an exclusive VIP Armani party!";
				}
				{
					c.VoteLike = "<p>Now click the like button to confirm your vote, and be kept up to date with Armani news and offers:</p>" +
						"<p><fb:like href=\"http://www.facebook.com/armani\" layout=\"box_count\" font=\"verdana\" width=\"200px\"></fb:like></p>";
				}
				{
					c.Vote2 =
						"<h1>Thanks!</h1>" +
						"<p>You voted for %1</p>";
				}
				c.VoteFooterMicrosite = "</div>"; // should have less branding (will open in a popup over the already branded microsite)
				c.VoteFooterFacebook = "</div>"; // should have more branding (will open in a full window over facebook)
				#endregion

				#region Repost
				c.RepostHeader = "<div class=\"MainHolder\">";
				{
					c.Repost1Top = "<h1>Repost your vote link to Facebook</h1><p>Remember, the entry with the most votes wins the competition, so repost and get all your mates to vote for you!</p>";
					c.Repost1Middle = "<p>Write a message to your friends, telling them why they should vote for your photo:</p>";
					c.Repost1Bottom = "";
					c.Repost1ButtonText = "Post to Facebook now";
				}
				{
					c.Repost2 = "<h1>Done!</h2><p>We posted your message to Facebook.</p>";
				}
				c.RepostFooter = "</div>";
				#endregion

				return c;

			}
			else if (k == 2)
			{
				MixmagComp c = new MixmagComp();
				c.K = 2;

				#region Globals
				c.Name = "Armani A|X Style Remix competition";
				c.Enabled = true;
				c.StartDate = new DateTime(2011, 6, 1);
				c.EndDate = new DateTime(2011, 11, 25);

				c.PageIdToLike = 39329648596;
				c.GlobalHeader = @"
<style>
	
	body
	{
		background:none!important;
		background-color:#373737!important;
		line-height: 1.5;
		color: #333;
		font-family: Arial, Helvetica, Verdana, sans-serif;
		text-align:center;
		text-transform: uppercase;
	}
	button{text-transform: uppercase;}
	h1
	{
		margin-top:16px;
		margin-bottom:10px;

		font-family: Gotham, arial;
		font-size: 20px;
		font-weight: bold;
		text-transform: uppercase;

		-webkit-transform: rotate(-0.0000000001deg);

	}
	P
	{
		text-align: center;
	}
	p, input
	{
		font-size: 12px;
	}
	div.Subhead
	{
		font-size: 10px;
		text-transform: uppercase;
		font-family: arial;
		margin-bottom: 10px;
		margin-top: -16px;
	}
	div.Navbar
	{
		font-size: 10px;
		text-transform: uppercase;
		font-family: arial;
		color:#ffffff;
		font-weight:bold;
		padding-top:5px;
		padding-left:8px;
		text-align:left;
	}
	.MainHolder
	{
		
		position:absolute;
		top:30px;
		left: 50%;
		width:540px;
		margin-left: -270px;
		padding:0px;
		
		
	}
	.MainHolderInner
	{
		background-color:#ffffff;
		padding-bottom:15px;
		padding-left:20px;
		padding-right:20px;
		margin-bottom:30px;
		overflow:auto;
		/*text-align:center;*/
		
	}
	

	
</style>
";


				#endregion

				#region Entry
				c.EntryHeader = @"
<div class=""MainHolder"">
	<img src=""http://pix-eu.dontstayin.com/2d86e650-50d7-42b0-ba51-ee8880b78fcb.jpg"" width=""540"" height=""154"" border=""0"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">A|X STYLE REMIX COMPETITION</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">";
				{
					c.Entry1Top = @"<h1>ENTER THE A|X STYLE REMIX COMPETITION</h1><!--<div class=""Subhead"">Subhead</div>-->";
					c.Entry1Middle = "<p>GET YOUR FRIENDS TO VOTE FOR YOU. WRITE THEM A MESSAGE:</p>";
					c.FacebookEntryMessageDefault = "I’m in the final of the A|X Armani Exchange Style Remix competition, and I need your vote. I could win a trip for two to the Miami Music Week. Please vote and spread the word!";
					c.DailyEmailEnabled = true;
					c.Entry1DailyEmailTickBoxText = "EMAIL ME UPDATES ON MY VOTES";
					c.Entry1ButtonText = "ENTER COMPETITON NOW";
					c.Entry1Bottom = "";
				}
				{
					c.Entry2Top = @"<h1>Done!</h1><p>You're now entered into the competition</p>";
					c.Entry2Middle =
						"<p>THANKS FOR ENTERING THE COMPETITON. SEND THIS LINK TO YOUR FRIENDS TO GET THEM TO VOTE:</p>";
					c.Entry2LikeButton =
						"<p>NOW CLICK THE ARMANI EXCHANGE LIKE BUTTON:</p>" +
						"<p><fb:like href=\"http://www.facebook.com/armaniexchange\" layout=\"box_count\" font=\"verdana\" width=\"200px\"></fb:like></p>";
					c.Entry2Bottom = "";
				}
				c.EntryFooter = @"
	</div>
</div>";
				#endregion

				#region Vote
				c.VoteHeaderMicrosite = @"
<div class=""MainHolder"">
	<img src=""http://pix-eu.dontstayin.com/2d86e650-50d7-42b0-ba51-ee8880b78fcb.jpg"" width=""540"" height=""154"" border=""0"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">A|X STYLE REMIX COMPETITION</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">"; // should have less branding (will open in a popup over the already branded microsite)
				c.VoteHeaderFacebook = @"
<div class=""MainHolder"">
	<img src=""http://pix-eu.dontstayin.com/2d86e650-50d7-42b0-ba51-ee8880b78fcb.jpg"" width=""540"" height=""154"" border=""0"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">A|X STYLE REMIX COMPETITION</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">"; // should have more branding (will open in a full window over facebook)
				{
					c.Vote1Top = @"<h1>VOTE FOR THIS PHOTO</h1>";
					c.Vote1Middle = "<p>CLICK THE BUTTON BELOW TO VOTE FOR %1:</p>";
					c.Vote1ButtonText = "VOTE FOR %1 NOW";
					c.Vote1Bottom = "";

					c.FacebookVoteMessage = "I’ve voted for my friend in the Armani Exchange style competition. Please vote too and we could win a trip to Miami and entry into an exclusive VIP party.";
				}
				{
					c.VoteLike = @"
<h1>One more thing...</h1>
<p>NOW CLICK THE LIKE BUTTON TO CONFIRM YOUR VOTE:</p>
<p><fb:like href=""http://www.facebook.com/armaniexchange"" layout=""box_count"" font=""verdana"" width=""200px""></fb:like></p>";
				}
				{
					c.Vote2 =
						@"
<h1>Thanks!</h1>
<p>THANKS! YOU’VE VOTED FOR %1</p>";
				}
				c.VoteFooterMicrosite = "</div></div>"; // should have less branding (will open in a popup over the already branded microsite)
				c.VoteFooterFacebook = "</div></div>"; // should have more branding (will open in a full window over facebook)
				#endregion

				#region Repost
				c.RepostHeader = @"
<div class=""MainHolder"">
	<img src=""http://pix-eu.dontstayin.com/2d86e650-50d7-42b0-ba51-ee8880b78fcb.jpg"" width=""540"" height=""154"" border=""0"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">A|X STYLE REMIX COMPETITION</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">";
				{
					c.Repost1Top = "<h1>REPOST YOUR PHOTO TO FACEBOOK</h1><p>THE ENTRY WITH THE MOST VOTES WINS THE COMPETITION</p>";
					c.Repost1Middle = "<p>GET YOUR FRIENDS TO VOTE FOR YOU. WRITE THEM A MESSAGE:</p>";
					c.Repost1Bottom = "";
					c.Repost1ButtonText = "POST TO FACEBOOK NOW";
				}
				{
					c.Repost2 = "<h1>Done!</h2><p>WE POSTED YOUR MESSAGE TO FACEBOOK</p>";
				}
				c.RepostFooter = "</div></div>";
				#endregion

				#region Email
				c.DailyEmailHtmlCompSubject = "Armani Exchange Style Remix Competition";
				c.DailyEmailHtmlComp = @"
<p>
	<b>Armani Exchange Style Remix Competition</b>
</p>
<p>
	The winner is the photo with most votes, so keep getting your friends to vote for you! 
</p>
";
				c.DailyEmailHtmlEntryMultiple = "<p><b>Entry %1</b></p>";
				c.DailyEmailHtmlEntry = @"
<p>
	<img src=""%1"" width=""150"">
</p>
<p>
	You have %2 vote%3 so far, and %4 day%5 left to get votes.
</p>
<p>
	<a href=""%6"">Click here</a> to post the voting link to facebook. You can send this voting link to your friends:
</p>
<p>
	%7
</p>
";
				#endregion

				#region Facebook posts
				c.FacebookPostName = "Armani Exchange Style Remix Competition";
				c.FacebookPostCaption = "";
				c.FacebookPostDescription = "You can help your friend to win a fantastic trip to Miami and tickets to the Armani Exchange VIP party. You might even be chosen to go with them! Vote for your friend to win.";
				#endregion

				return c;

			}
			else if (k == 3)
			{
				MixmagComp c = new MixmagComp();
				c.K = 3;

				#region Globals
				c.Name = "Like my Look";
				c.Enabled = true;
				c.StartDate = new DateTime(2011, 6, 1);
				c.EndDate = new DateTime(2099, 1, 1);

				c.PageIdToLike = 12120996025;
				c.GlobalHeader = @"
<style>
	
	body
	{
		background:none!important;
		background-color:#373737!important;
		line-height: 1.5;
		color: #333;
		font-family: Arial, Helvetica, Verdana, sans-serif;
		text-align:center;
		text-transform: uppercase;
	}
	button{text-transform: uppercase;}
	h1
	{
		margin-top:16px;
		margin-bottom:10px;

		font-family: Gotham, arial;
		font-size: 20px;
		font-weight: bold;
		text-transform: uppercase;

		-webkit-transform: rotate(-0.0000000001deg);

	}
	P
	{
		text-align: center;
	}
	p, input
	{
		font-size: 12px;
	}
	div.Subhead
	{
		font-size: 10px;
		text-transform: uppercase;
		font-family: arial;
		margin-bottom: 10px;
		margin-top: -16px;
	}
	div.Navbar
	{
		font-size: 10px;
		text-transform: uppercase;
		font-family: arial;
		color:#ffffff;
		font-weight:bold;
		padding-top:5px;
		padding-left:8px;
		text-align:left;
	}
	.MainHolder
	{
		
		position:absolute;
		top:30px;
		left: 50%;
		width:540px;
		margin-left: -270px;
		padding:0px;
		
		
	}
	.MainHolderInner
	{
		background-color:#ffffff;
		padding-bottom:15px;
		padding-left:20px;
		padding-right:20px;
		margin-bottom:30px;
		overflow:auto;
		/*text-align:center;*/
		
	}
	

	
</style>
";


				#endregion

				#region Entry
				c.EntryHeader = @"
<div class=""MainHolder"">
	<img src=""http://pix-eu.dontstayin.com/2d86e650-50d7-42b0-ba51-ee8880b78fcb.jpg"" width=""540"" height=""154"" border=""0"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">A|X STYLE REMIX COMPETITION</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">";
				{
					c.Entry1Top = @"<h1>ENTER THE A|X STYLE REMIX COMPETITION</h1><!--<div class=""Subhead"">Subhead</div>-->";
					c.Entry1Middle = "<p>GET YOUR FRIENDS TO VOTE FOR YOU. WRITE THEM A MESSAGE:</p>";
					c.FacebookEntryMessageDefault = "I’m in the final of the A|X Armani Exchange Style Remix competition, and I need your vote. I could win a trip for two to the Miami Music Week. Please vote and spread the word!";
					c.DailyEmailEnabled = true;
					c.Entry1DailyEmailTickBoxText = "EMAIL ME UPDATES ON MY VOTES";
					c.Entry1ButtonText = "ENTER COMPETITON NOW";
					c.Entry1Bottom = "";
				}
				{
					c.Entry2Top = @"<h1>Done!</h1><p>You're now entered into the competition</p>";
					c.Entry2Middle =
						"<p>THANKS FOR ENTERING THE COMPETITON. SEND THIS LINK TO YOUR FRIENDS TO GET THEM TO VOTE:</p>";
					c.Entry2LikeButton =
						"<p>NOW CLICK THE ARMANI EXCHANGE LIKE BUTTON:</p>" +
						"<p><fb:like href=\"http://www.facebook.com/armaniexchange\" layout=\"box_count\" font=\"verdana\" width=\"200px\"></fb:like></p>";
					c.Entry2Bottom = "";
				}
				c.EntryFooter = @"
	</div>
</div>";
				#endregion

				#region Vote
				c.VoteHeaderMicrosite = @"
<div class=""MainHolder"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">Like my Look</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">"; // should have less branding (will open in a popup over the already branded microsite)
				c.VoteHeaderFacebook = @"
<div class=""MainHolder"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">Like my Look</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">"; // should have more branding (will open in a full window over facebook)
				{
					c.Vote1Top = @"<h1>VOTE FOR THIS PHOTO</h1>";
					c.Vote1Middle = "<p>CLICK THE BUTTON BELOW TO VOTE FOR %1:</p>";
					c.Vote1ButtonText = "VOTE FOR %1 NOW";
					c.Vote1Bottom = "";

					c.FacebookVoteMessage = "";
				}
				{
					c.VoteLike = @"
<h1>One more thing...</h1>
<p>NOW CLICK THE LIKE BUTTON TO CONFIRM YOUR VOTE:</p>
<p><fb:like href=""http://www.facebook.com/MixmagMagazine"" layout=""box_count"" font=""verdana"" width=""200px""></fb:like></p>";
				}
				{
					c.Vote2 =
						@"
<h1>Thanks!</h1>
<p>THANKS! YOU’VE VOTED FOR %1</p>";
				}
				c.VoteFooterMicrosite = "</div></div>"; // should have less branding (will open in a popup over the already branded microsite)
				c.VoteFooterFacebook = "</div></div>"; // should have more branding (will open in a full window over facebook)
				#endregion

				#region Repost
				c.RepostHeader = @"
<div class=""MainHolder"">
	<img src=""http://pix-eu.dontstayin.com/2d86e650-50d7-42b0-ba51-ee8880b78fcb.jpg"" width=""540"" height=""154"" border=""0"">
	<div style=""width:540px; height:5px; background-color:#ffffff;""></div>
	<div style=""width:540px; height:26px; background-color:#000000;""><div class=""Navbar"">A|X STYLE REMIX COMPETITION</div></div>
	<div style=""width:540px; overflow:hidden; background-color:#ffffff;""><img src=""http://www.mixmagfashion.com/armaniexchange/wp-content/themes/blueprint-theme/images/strip.png"" width=""960"" height=""16"" /></div>
	<div class=""MainHolderInner"">";
				{
					c.Repost1Top = "<h1>REPOST YOUR PHOTO TO FACEBOOK</h1><p>THE ENTRY WITH THE MOST VOTES WINS THE COMPETITION</p>";
					c.Repost1Middle = "<p>GET YOUR FRIENDS TO VOTE FOR YOU. WRITE THEM A MESSAGE:</p>";
					c.Repost1Bottom = "";
					c.Repost1ButtonText = "POST TO FACEBOOK NOW";
				}
				{
					c.Repost2 = "<h1>Done!</h2><p>WE POSTED YOUR MESSAGE TO FACEBOOK</p>";
				}
				c.RepostFooter = "</div></div>";
				#endregion

				#region Email
				c.DailyEmailHtmlCompSubject = "Armani Exchange Style Remix Competition";
				c.DailyEmailHtmlComp = @"
<p>
	<b>Armani Exchange Style Remix Competition</b>
</p>
<p>
	The winner is the photo with most votes, so keep getting your friends to vote for you! 
</p>
";
				c.DailyEmailHtmlEntryMultiple = "<p><b>Entry %1</b></p>";
				c.DailyEmailHtmlEntry = @"
<p>
	<img src=""%1"" width=""150"">
</p>
<p>
	You have %2 vote%3 so far, and %4 day%5 left to get votes.
</p>
<p>
	<a href=""%6"">Click here</a> to post the voting link to facebook. You can send this voting link to your friends:
</p>
<p>
	%7
</p>
";
				#endregion

				#region Facebook posts
				c.FacebookPostName = "Mixmag Fashion Like my Look";
				c.FacebookPostCaption = "";
				c.FacebookPostDescription = "";
				#endregion

				return c;

			}
			else
				return null;
		}

	}
}
