<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.MixmagGreatest.Home" %>

<asp:HiddenField runat="server" ID="PageIdToLike" />
<asp:HiddenField runat="server" ID="LikedPage" />
<asp:HiddenField runat="server" ID="MixmagGreatestDjK" />
<asp:HiddenField runat="server" ID="FacebookSource" />

<style>
/*
a.NoStyle:link div.BoxInnerHeader , 
a.NoStyle:visited div.BoxInnerHeader span   { text-decoration:none!important; color:#000000!important; background-color:#B39B00!important; }
a.NoStyle:hover div.BoxInnerHeader span     { text-decoration:none!important; color:#000000!important; background-color:#B39B00!important; }
	*/	
	
a.White:link, 
a.White:visited  { text-decoration:none!important; color:#ffffff!important; background-color:#ffffff!important; }
a.White:hover    { text-decoration:none!important; color:#ffffff!important; background-color:#ffffff!important; }
	
.BoxOuter {height:160px; width:160px; float:left; position:relative; margin-right:10px; margin-bottom:10px; }
.BoxOuterEnd {height:160px; width:160px; float:left; position:relative; margin-right:0px; margin-bottom:10px; }
.BoxInnerLink {position:absolute; top:0px; left:0px; }
.BoxInnerHeader 
{
	position:absolute; bottom:10px; left:10px; text-align:left; width:125px; border-left:3px solid #B39B00; 
	font-size:14px;
}
.BoxInnerHeaderWord 
{
	color:#ffffff;
	background-color:#B39B00; padding-right:4px; margin-right:-4px;
	font-family:'ProximaNovaBold',Helvetica, Arial, sans-serif;
	font-size:14px;
}
</style>
<style type="text/css">
button { padding-left: 4px; padding-right: 4px; overflow: visible; }
</style>

<input type="hidden" runat="server" id="SignedRequest" />
<input type="hidden" runat="server" id="DoneRefresh" />
<input type="hidden" runat="server" id="SafariKludge" />

<p runat="server" id="TestP" />



<div runat="server" id="NominationsPanel">
	
	<div runat="server" id="NominationsLikeButtonHolder" style="position:relavtive;">
		<img src="/gfx/greatest/mm_greatest_like_withoutbutton.jpg" width="500" height="400" />

		<div style="position:absolute; top:56px; left:405px;">
			<fb:like href="http://www.facebook.com/<%= Facebook.FacebookCommon.Common(Facebook.Apps.MixmagGreatest).PageName %>" layout="button_count" font="verdana" width="90px"></fb:like>
		</div>
	</div>
	
	<div runat="server" id="NominationsHolder">

		<div runat="server" id="NotRunningPanel">
			<div style="margin-bottom:15px;">
				<a href="http://www.mixmag.net/digital" class="NoStyle" target="_top"><img src="/gfx/greatest/mixmag_greatest_close_ad.jpg" width="500" height="691" border="0" class="rounded-corners" /></a>
			</div>
		</div>
		<div runat="server" id="RunningPanel">
			<div style="margin-bottom:15px;">
				<a href="/" class="NoStyle" target="_top"><img src="/gfx/greatest/mm_greatest_header.jpg" width="500" height="155" border="0" class="rounded-corners" /></a>
			</div>

			<!--<h1>WHO IS THE GREATEST DANCE ACT OF ALL TIME?</h1>-->
 			<p style="xtext-align:justify;">
				Welcome to the largest poll in the history of dance music. Right here is where YOU can 
				decide on the winner of the most prestigious title that Mixmag – the world’s oldest, 
				biggest and most respected dance music magazine – has ever awarded.
			</p>
			<p style="xtext-align:justify;">
				Below is the shortlist of 100 bands, beatmakers and producer / performers who we think have a 
				shot at being named The Greatest Dance Act Of All Time. 
				<b>Click on each for more info, then vote for your favourite.</b>
			</p>
	
			<p style="xtext-align:justify;">
				And remember, if the act that changed your life isn’t there don’t despair. Email your personal 
				nomination to <a href="mailto:greatest@mixmag.net">greatest@mixmag.net</a> and state your case. 
				If we think you’ve got a point, we’ll add your act to the ‘public nominations’ at the foot of 
				this page. Then it’s up to dance music to decide.
			</p>
	
			<p><b>YOU ONLY HAVE ONE VOTE, SO CHOOSE WISELY!</b></p>
	
			<p>VOTING CLOSES MIDNIGHT ON DECEMBER 23, 2011</p>
	
			<p align="right" style="padding-bottom:7px; padding-top:7px; text-align:center; background-color:#000000; color:#ffffff;" class="rounded-corners">Quick search: <asp:PlaceHolder runat="server" ID="QuickSearchPh"></asp:PlaceHolder></p>

			<img style="margin-bottom:12px;" src="/gfx/greatest/mm_greatest_nominations.jpg" width="500" height="45" />

			<asp:PlaceHolder runat="server" ID="DjsPh" />
			<div runat="server" id="PublicDjsHolder">
				<div style="clear:both;"></div>
				<img style="margin-bottom:12px;" src="/gfx/greatest/mm_greatest_nominations_public.jpg" width="500" height="45" />
				<asp:PlaceHolder runat="server" ID="PublicDjsPh" />
			</div>
		</div>
	</div>
</div>



<div runat="server" id="VotePanel" style="position:relative;">

	<div style="margin-bottom:15px;">
		<a href="/" class="NoStyle" target="_top"><img src="/gfx/greatest/mm_greatest_header.jpg" width="500" height="155" border="0" class="rounded-corners" /></a>
	</div>

	<p align="right" style="margin-top:-3px; padding-bottom:7px; padding-top:7px; text-align:center; background-color:#000000; color:#ffffff;" class="rounded-corners">Quick search: <asp:PlaceHolder runat="server" ID="VoteQuickSearchPh"></asp:PlaceHolder></p>

	<img runat="server" id="VoteImg" border="0" width="200" height="200" class="rounded-corners" style="margin-right:20px; position:absolute;" />
	<h1 runat="server" id="VoteName" style="margin-left:220px;" />
	<p runat="server" id="VoteDescriptionP" style="margin-top:-15px; margin-left:220px; margin-bottom:25px;"></p>
	
	<div style="clear:both;"></div>

	<div style="width:160px; height:160px; float:right; background-color:#B39B00; padding:20px;" class="rounded-corners">
		<h2 style="margin-top:0px;">Back</h2>
		<a href="/" target="_top">See the full list of nominees</a>
	</div>
	<div style="width:240px; height:160px; background-color:#B39B00; float:left; padding:20px;" class="rounded-corners">
		<h2 style="margin-top:0px;">Vote here</h2>
		<div runat="server" id="VoteLikeHolder" style="display:none;">
			<p>
				Step 1 - Click the Like button:
			</p>
			<p>
				<fb:like href="http://www.facebook.com/<%= Facebook.FacebookCommon.Common(Facebook.Apps.MixmagGreatest).PageName %>" layout="button_count" font="verdana" width="90px"></fb:like>
			</p>
		</div>
		<div runat="server" id="VoteFollowHolder" style="display:none;">
			<p runat="server" id="VoteFollowPrompt"></p>
			<p>
				<a href="http://twitter.com/mixmag" target="_blank" class="twitter-follow-button" data-show-count="false">Follow @mixmag</a>
				<script>
				//	var followed = false;
				//	twttr.events.bind('follow', function (event) {
				//		var followed_user_id = event.data.user_id;
				//		var followed_screen_name = event.data.screen_name;
				//		document.getElementById("<%= VoteTwitterSkipButton.ClientID %>").click();
				//	});

				//	twttr.events.bind('click', function (event) {
				//		var click_type = event.region;
				//		if (click_type != "screen_name")
				//			document.getElementById("<%= VoteTwitterSkipButton.ClientID %>").click();
				//	});
				</script>
			</p>
			<p>
				Don't use Twitter? <a href="/" runat="server" id="VoteFollowSkipLink">Skip this bit</a>.<button runat="server" id="VoteTwitterSkipButton" style="display:none;">skip</button>
			</p>
		</div>
		<div runat="server" id="VoteTweetHolder" style="display:none;">
			<p runat="server" id="VoteTweetPrompt"></p>
			<p>
				<a runat="server" id="VoteTweetButton" href="http://twitter.com/share" target="_blank" class="twitter-share-button"
					data-count="none"
					data-url=""
					data-text="">Tweet</a>
				<script>
				//	var tweeted = false;
				//	twttr.events.bind('tweet', function (event) {
				//		var followed_user_id = event.data.user_id;
				//		var followed_screen_name = event.data.screen_name;
				//		document.getElementById("<%= VoteTwitterSkipButton.ClientID %>").click();
				//	});
				</script>
			</p>
			<p>
				Don't use Twitter? <a href="/" runat="server" id="VoteTweetSkipLink">Skip this bit</a>.
			</p>
		</div>
		<div runat="server" id="VoteButtonHolder" style="display:none;">
			<p runat="server" id="VoteButtonPrompt"></p>
			<p>
				<button runat="server" id="VoteButton">Vote now</button>
			</p>
			<p>
				<asp:CheckBox runat="server" ID="VoteFacebookUpdateCheckbox1" Checked="true" Text="Update my facebook wall" />
			</p>
		</div>
		<div runat="server" ID="VoteConfirmHolder" style="display:none;">
			<p>
				Is this you?
			</p>
			<p>
				<img runat="server" id="Confirm_Img" src="" width="50" height="50" border="0" align="absmiddle" />
				<a runat="server" id="Confirm_Link" href="" target="_blank"></a>
			</p>
			<p>
				<button runat="server" id="Confirm_YesButton">Yes, this is me</button>
				<button runat="server" id="Confirm_NoButton">Nope, not me</button>
			</p>
		</div>
		<div runat="server" ID="VoteLoggedOutHolder" style="display:none;">
			<p>
				Click below to continue:
			</p>
			<p>
				<button runat="server" id="LoggedOutButton">Log in</button>
			</p>
		</div>
	</div>

	<div style="clear:both; height:20px;"></div>

	<iframe runat="server" id="VoteVideo1" width="500" height="380" src="http://www.youtube.com/embed/jATnwb2DviY" frameborder="0" allowfullscreen></iframe>
	
	<div style="clear:both; height:20px;"></div>

	<iframe runat="server" id="VoteVideo2" width="500" height="380" src="http://www.youtube.com/embed/tf_ZIKNUnMs" frameborder="0" allowfullscreen></iframe>

	<div style="clear:both; height:20px;"></div>

	<div class="fb-comments" data-href="mixmag-greatest.com/air" data-num-posts="10" data-width="500" runat="server" id="FacebookComments"></div>

</div>





<asp:Panel runat="server" ID="VoteCompletePanel" style="display:none;">
	<div style="margin-bottom:15px;">
		<a href="/" class="NoStyle" target="_top"><img src="/gfx/greatest/mm_greatest_header.jpg" width="500" height="155" border="0" class="rounded-corners" /></a>
	</div>
	<asp:PlaceHolder runat="server" ID="EndPh" />
	<div style="clear:both; height:20px;"></div>

	<div class="fb-comments" data-href="mixmag-greatest.com/air" data-num-posts="10" data-width="500" runat="server" id="FacebookComments2"></div>

</asp:Panel>

	

<pre runat="server" id="DebugOutput" style="position:fixed; padding:10px; border:1px solid #ff0000; margin:10px; top:0px; left:0px; background-color:#ffffff; display:none; width:500px;"></pre>

<div runat="server" id="LoadingLabel" class="ui-state-highlight ui-corner-all" style="position:absolute; right:20px; top:20px; display:none; z-index:995; padding-left:20px; padding-right:20px;">
	<p>
		Loading...
	</p>
</div>

<!-- Welcome to DontStayIn (this string is needed to pass the build tests) -->
