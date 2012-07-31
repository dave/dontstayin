<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Spam.ascx.cs" Inherits="Spotted.Pages.Spam" %>

<script>
	function JavascriptConfirmArchive() { return confirm('Are you sure you want to remove these topics from your inbox?\nYou can still view them by click \'Watching\'.');}
	function JavascriptConfirmIgnore() { return confirm('Are you sure you want to ignore these topics in your inbox?\nThey will be removed from your \'Inbox\' and \'Watching\'.');}
</script>

<dsi:h1 runat="Server">Where's all this spam coming from?</dsi:h1>
<div class="ContentBorder">
	<p>
		The DontStayIn inbox is a wonderful thing when you use it properly. But if 
		you were using it properly you wouldn't be here, right?
	</p>
	<p>
		This page shows you how to use your inbox properly, and gives you a rough idea 
		of where all the spam has come from.
	</p>
	<h2>
		How does the inbox work?
	</h2>
	<p>
		Before we start, we'll have a quick recap on how the inbox works. If it's 
		a hundred pages long, you're probably not using it right! Whenever you go 
		through your inbox, make sure you <b>deal with every topic</b>, so it's 
		empty! Don't worry if you've got a million pages right now - this page 
		will help you clear them out.
	</p>
	<p>
		For each topic, think to yourself: <b>Am I interested in this topic?</b>
	</p>
	<p>
		<b>No - it's rubbish</b> - click the watch/ignore button <img src="/gfx/icon-eye-up.png" border="0" align="absmiddle" height="21" width="26"> so it turns <img src="/gfx/icon-eye-dn.png" border="0" align="absmiddle" height="21" width="26">
	</p>
	<p>
		<b>Yes - and i've read it all</b> - click the inbox button <img src="/gfx/icon-inbox-up.png" border="0" align="absmiddle" height="21" width="26"> so it turns <img src="/gfx/icon-inbox-dn.png" border="0" align="absmiddle" height="21" width="26">
	</p>
	<p>
		<b>Yes - but I need to read more</b> - click the favourite button so it turns <img src="/gfx/icon-star-26-up.png" border="0" align="absmiddle" height="21" width="26"> then the inbox button
	</p>

</div>


<dsi:h1 runat="Server">What do the inbox buttons do?</dsi:h1>
<div class="ContentBorder">
	
	<h2>
		<img src="/gfx/icon-eye-up.png" border="0" align="absmiddle" height="21" width="26" style="margin-right:3px;">The watch / ignore button
	</h2>
	<p>
		If you've had enough of a topic, click this button so it turns <img src="/gfx/icon-eye-dn.png" border="0" align="absmiddle" height="21" width="26">.
		It'll be removed from your inbox, and it <b>won't</b> come back.
	</p>
	
	<h2>
		<img src="/gfx/icon-inbox-up.png" border="0" align="absmiddle" height="21" width="26" style="margin-right:3px;">The inbox button
	</h2>
	<p>
		Once you've read a topic, click this button so it turns <img src="/gfx/icon-inbox-dn.png" border="0" align="absmiddle" height="21" width="26">. 
		It'll <b>come back</b> into your inbox when the next messages is posted.
	</p>
	
	
	<h2>
		<img src="/gfx/icon-star-26-up.png" border="0" align="absmiddle" height="21" width="26" style="margin-right:3px;">The favourite button
	</h2>
	<p>
		If you need to find a topic later, don't leave it in your inbox! Click this button so it turns <img src="/gfx/icon-star-26-up.png" border="0" align="absmiddle" height="21" width="26">.
		It'll be listed on your favourite topics page.
	</p>
</div>

<asp:Panel ID="InboxSpamPanel" runat="server">
<dsi:h1 runat="Server">But where does it all come from!</dsi:h1>
<div class="ContentBorder">
	<p>
		We can give you a break-down of what's in your inbox, and how it got there.
	</p>
	<p>
		<h2>Topic options</h2>
	</p>
	<p>
		<b>Remove all from inbox</b> - removes topics from your <a href="/pages/inbox">inbox</a>. Topics go to your <a href="/pages/watching">watching</a> page until a new comment is posted.
	</p>
	<p>
		<b>Smart delete*</b> - removes topics from your <a href="/pages/inbox">inbox</a>. Topics with <%= SmartDeleteThreadUsrJob.SMART_DELETE_CUT_OFF_NUMBER_OF_PEOPLE_WATCHING.ToString() %> people watching or less go to your <a href="/pages/watching">watching</a> page until a new comment is posted. Topics with more than <%= SmartDeleteThreadUsrJob.SMART_DELETE_CUT_OFF_NUMBER_OF_PEOPLE_WATCHING.ToString() %> people watching you will not see again.
	</p>
	<p>
		<b>Ignore all</b> - removes topics from your <a href="/pages/inbox">inbox</a> and your <a href="/pages/watching">watching</a> page. You will not see these topics again.
	</p>
	<asp:Panel runat="server" ID="InvitePanel">
		<br />
		<a ID="UsrWatchPanelAnchor"></a>
		<h2>
			Buddy invites
		</h2>
		<p>
			Anyone can send you a private message using the box on your profile page - this 
			isn't usually a source of much spam, because you have to send each message 
			separately.
		</p>
		<p>
			Your buddies can invite you to topics - this is the source of most in
			box spam. 
			A good way to reduce the amount of spam you get without deleting all your 
			buddies is to stop them from bulk-inviting. They can still invite you 
			personally, but you won't be selected when they click "invite all my buddies".
		</p>
		<p class="CleanLinks">			
			<asp:GridView Runat="server" ID="InviteGridView" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView_PageIndexChanging"
				AlternatingRowStyle-CssClass="GridViewAltItem" HeaderStyle-CssClass="GridViewHeader" SelectedRowStyle-CssClass="GridViewSelectedItem" ShowFooter="true"
				RowStyle-VerticalAlign="Top" OnRowDataBound="InviteGridView_RowDataBound">
				<Columns>
					<asp:TemplateField HeaderText="Buddy" ItemStyle-Width="170px">
						<ItemTemplate>
							<%#((Bobs.Usr)((Bobs.ThreadUsr)Container.DataItem).StatusChangeObject).Link()%>
							<asp:TextBox ID="InvitingUsrKTextBox" runat="server" Text="<%#((Bobs.ThreadUsr)Container.DataItem).InvitingUsrK%>" Visible="false"></asp:TextBox>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topics" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="34px">
						<ItemTemplate>
							<span style="margin-right:4px;"><%# Utilities.Link(UrlInfo.PageUrl("inbox", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Usr).ToString(), "statuschangeobjectk", ((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK.ToString()), ((Bobs.ThreadUsr)Container.DataItem).ExtraSelectElements["count"].ToString())%></span>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Buddy options">
						<ItemTemplate>
							<asp:DropDownList ID="OptionsDropDownList" Width="115" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="InviteNotBuddyLabel" runat="server" Visible="false" Text="<small>Not your buddy</small>"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topic options" FooterStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:DropDownList ID="TopicOptionsDropDownList" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="TopicOptionsLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
						<FooterTemplate>						
							<button onserverclick="RefreshButton_Click" ID="RefreshButton" runat="server" style="margin-right:4px;" causesvalidation="false">Refresh</button>
							<button id="OptionsUpdateButton" runat="server" ValidationGroup="OptionsValidationGroup" onserverclick="OptionsUpdateButton_Click">Update</button>							
						</FooterTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="">
						<ItemTemplate>
							<asp:TextBox ID="StatusChangeObjectTypeTextBox" runat="server" Text="<%#Convert.ToInt32(((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectType)%>" Visible="false"></asp:TextBox>
							<asp:TextBox ID="StatusChangeObjectKTextBox" runat="server" Text="<%#((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK%>" Visible="false"></asp:TextBox>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</p>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="VenueWatchPanel">
		<br />
		<a ID="VenueWatchPanelAnchor"></a>
		<h2>
			Watching venues
		</h2>
		<p>
			If you click the watch button on a venue page, all the topics posted in that 
			venue (and in all events at that venue) will go into your inbox. Topics in 
			your inbox from watching venues are:
		</p>
		<p class="CleanLinks">
			<asp:GridView Runat="server" ID="VenueWatchGridView" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView_PageIndexChanging" 
				AlternatingRowStyle-CssClass="GridViewAltItem" HeaderStyle-CssClass="GridViewHeader" SelectedRowStyle-CssClass="GridViewSelectedItem" ShowFooter="true" 
				RowStyle-VerticalAlign="Top" OnRowDataBound="VenueWatchGridView_RowDataBound">
				<Columns>
					<asp:TemplateField HeaderText="Venue" ItemStyle-Width="170px">
						<ItemTemplate>
							<%#((ILinkable)((Bobs.ThreadUsr)Container.DataItem).StatusChangeObject).Link(null)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topics" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="34px">
						<ItemTemplate>
							<span style="margin-right:4px;"><%# Utilities.Link(UrlInfo.PageUrl("inbox", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Venue).ToString(), "statuschangeobjectk", ((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK.ToString()), ((Bobs.ThreadUsr)Container.DataItem).ExtraSelectElements["count"].ToString())%></span>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Venue options">
						<ItemTemplate>
							<asp:DropDownList ID="OptionsDropDownList" Width="115" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="NotWatchingLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topic options" FooterStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:DropDownList ID="TopicOptionsDropDownList" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="TopicOptionsLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<button onserverclick="RefreshButton_Click" ID="RefreshButton" runat="server" style="margin-right:4px;" causesvalidation="false">Refresh</button>
							<button id="OptionsUpdateButton" runat="server" ValidationGroup="OptionsValidationGroup" onserverclick="OptionsUpdateButton_Click">Update</button>
						</FooterTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="">
						<ItemTemplate>
							<asp:TextBox ID="StatusChangeObjectTypeTextBox" runat="server" Text="<%#Convert.ToInt32(((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectType)%>" Visible="false"></asp:TextBox>
							<asp:TextBox ID="StatusChangeObjectKTextBox" runat="server" Text="<%#((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK%>" Visible="false"></asp:TextBox>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</p>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="BrandWatchPanel">
		<br />
		<a ID="BrandWatchPanelAnchor"></a>
		<h2>
			Watching party-brands
		</h2>
		<p>
			If you click the watch button on a public party-brand chat page, all the topics 
			posted in their events will go into your inbox. Topics in your inbox from 
			watching party-brands are:
		</p>
		<p class="CleanLinks">
			<asp:GridView Runat="server" ID="BrandWatchGridView" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView_PageIndexChanging" 
				AlternatingRowStyle-CssClass="GridViewAltItem" HeaderStyle-CssClass="GridViewHeader" SelectedRowStyle-CssClass="GridViewSelectedItem" ShowFooter="true"
				RowStyle-VerticalAlign="Top" OnRowDataBound="BrandWatchGridView_RowDataBound">
				<Columns>
					<asp:TemplateField HeaderText="Party-brand" ItemStyle-Width="170px">
						<ItemTemplate>
							<%#((Bobs.Brand)((Bobs.ThreadUsr)Container.DataItem).StatusChangeObject).Link()%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topics" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="34px">
						<ItemTemplate>
							<span style="margin-right:4px;"><%# Utilities.Link(UrlInfo.PageUrl("inbox", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Brand).ToString(), "statuschangeobjectk", ((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK.ToString()), ((Bobs.ThreadUsr)Container.DataItem).ExtraSelectElements["count"].ToString())%></span>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Party-brand options">
						<ItemTemplate>
							<asp:DropDownList ID="OptionsDropDownList" Width="115" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="NotWatchingLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topic options" FooterStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:DropDownList ID="TopicOptionsDropDownList" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="TopicOptionsLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<button onserverclick="RefreshButton_Click" ID="RefreshButton" runat="server" style="margin-right:4px;" causesvalidation="false">Refresh</button>
							<button id="OptionsUpdateButton" runat="server" ValidationGroup="OptionsValidationGroup" onserverclick="OptionsUpdateButton_Click">Update</button>
						</FooterTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="">
						<ItemTemplate>
							<asp:TextBox ID="StatusChangeObjectTypeTextBox" runat="server" Text="<%#Convert.ToInt32(((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectType)%>" Visible="false"></asp:TextBox>
							<asp:TextBox ID="StatusChangeObjectKTextBox" runat="server" Text="<%#((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK%>" Visible="false"></asp:TextBox>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</p>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="EventWatchPanel">
		<br />
		<a ID="EventWatchPanelAnchor"></a>
		<h2>
			Watching events
		</h2>
		<p>
			If you click the watch button on an event page, all the topics posted in that 
			event will go into your inbox. Topics in your inbox from watching events are:
		</p>
		<p class="CleanLinks">
			<asp:GridView Runat="server" ID="EventWatchGridView" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView_PageIndexChanging" 
				AlternatingRowStyle-CssClass="GridViewAltItem" HeaderStyle-CssClass="GridViewHeader" SelectedRowStyle-CssClass="GridViewSelectedItem" ShowFooter="true"
				RowStyle-VerticalAlign="Top" OnRowDataBound="EventWatchGridView_RowDataBound">
				<Columns>
					<asp:TemplateField HeaderText="Event" ItemStyle-Width="170px">
						<ItemTemplate>
							<%#((Bobs.Event)((Bobs.ThreadUsr)Container.DataItem).StatusChangeObject).Link()%>, 
							<%#((Bobs.Event)((Bobs.ThreadUsr)Container.DataItem).StatusChangeObject).FriendlyDate(false)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topics" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="34px">
						<ItemTemplate>
							<span style="margin-right:4px;"><%# Utilities.Link(UrlInfo.PageUrl("inbox", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Event).ToString(), "statuschangeobjectk", ((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK.ToString()), ((Bobs.ThreadUsr)Container.DataItem).ExtraSelectElements["count"].ToString())%></span>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Event options">
						<ItemTemplate>
							<asp:DropDownList ID="OptionsDropDownList" Width="115" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="NotWatchingLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topic options" FooterStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:DropDownList ID="TopicOptionsDropDownList" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="TopicOptionsLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<button onserverclick="RefreshButton_Click" ID="RefreshButton" runat="server" style="margin-right:4px;" causesvalidation="false">Refresh</button>
							<button id="OptionsUpdateButton" runat="server" ValidationGroup="OptionsValidationGroup" onserverclick="OptionsUpdateButton_Click">Update</button>
						</FooterTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="">
						<ItemTemplate>
							<asp:TextBox ID="StatusChangeObjectTypeTextBox" runat="server" Text="<%#Convert.ToInt32(((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectType)%>" Visible="false"></asp:TextBox>
							<asp:TextBox ID="StatusChangeObjectKTextBox" runat="server" Text="<%#((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK%>" Visible="false"></asp:TextBox>
							
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</p>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="GroupWatchPanel">
		<br />
		<a ID="GroupWatchPanelAnchor"></a>
		<h2>
			Watching groups
		</h2>
		<p>
			If you click the watch button on a group page, or you choose to watch the group 
			when you join, all the topics posted in that group will go into your inbox. This 
			is where most inbox spam comes from! Topics in your inbox from watching groups 
			are:
		</p>
		<p class="CleanLinks">
			<asp:GridView Runat="server" ID="GroupWatchGridView" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView_PageIndexChanging" 
				AlternatingRowStyle-CssClass="GridViewAltItem" HeaderStyle-CssClass="GridViewHeader" SelectedRowStyle-CssClass="GridViewSelectedItem" ShowFooter="true"
				RowStyle-VerticalAlign="Top" OnRowDataBound="GroupWatchGridView_RowDataBound">
				<Columns>
					<asp:TemplateField HeaderText="Group" ItemStyle-Width="170px">
						<ItemTemplate>
							<%#((Bobs.Group)((Bobs.ThreadUsr)Container.DataItem).StatusChangeObject).Link()%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topics" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="34px">
						<ItemTemplate>
							<span style="margin-right:4px;"><%# Utilities.Link(UrlInfo.PageUrl("inbox", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Group).ToString(), "statuschangeobjectk", ((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK.ToString()), ((Bobs.ThreadUsr)Container.DataItem).ExtraSelectElements["count"].ToString())%></span>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Group options">
						<ItemTemplate>
							<asp:DropDownList ID="OptionsDropDownList" Width="115" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="NotWatchingLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topic options" FooterStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:DropDownList ID="TopicOptionsDropDownList" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="TopicOptionsLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<button onserverclick="RefreshButton_Click" ID="RefreshButton" runat="server" style="margin-right:4px;" causesvalidation="false">Refresh</button>
							<button id="OptionsUpdateButton" runat="server" ValidationGroup="OptionsValidationGroup" onserverclick="OptionsUpdateButton_Click">Update</button>
						</FooterTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="">
						<ItemTemplate>
							<asp:TextBox ID="StatusChangeObjectTypeTextBox" runat="server" Text="<%#Convert.ToInt32(((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectType)%>" Visible="false"></asp:TextBox>
							<asp:TextBox ID="StatusChangeObjectKTextBox" runat="server" Text="<%#((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK%>" Visible="false"></asp:TextBox>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</p>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="GroupNewsPanel">
		<br />
		<a ID="GroupNewsPanelAnchor"></a>
		<h2>
			Group news
		</h2>
		<p>
			If you're in a group, all the group news will automatically go into your inbox.
			If you're getting too much, just exit the group and it'll stop. Group news topics 
			in your inbox are:
		</p>
		<p class="CleanLinks">
			<asp:GridView Runat="server" ID="GroupNewsGridView" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass="GridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView_PageIndexChanging" 
				AlternatingRowStyle-CssClass="GridViewAltItem" HeaderStyle-CssClass="GridViewHeader" SelectedRowStyle-CssClass="GridViewSelectedItem" ShowFooter="true"
				RowStyle-VerticalAlign="Top" OnRowDataBound="GroupWatchGridView_RowDataBound">
				<Columns>
					<asp:TemplateField HeaderText="Group" ItemStyle-Width="170px">
						<ItemTemplate>
							<%#((Bobs.Group)((Bobs.ThreadUsr)Container.DataItem).StatusChangeObject).Link()%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="&nbsp;News" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="34px">
						<ItemTemplate>
							<span style="margin-right:4px;"><%# Utilities.Link(UrlInfo.PageUrl("inbox", "groupnews", "true", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Group).ToString(), "statuschangeobjectk", ((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK.ToString()), ((Bobs.ThreadUsr)Container.DataItem).ExtraSelectElements["count"].ToString())%></span>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Group options">
						<ItemTemplate>
							<asp:DropDownList ID="OptionsDropDownList" Width="115" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="NotWatchingLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Topic options" FooterStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:DropDownList ID="TopicOptionsDropDownList" runat="server" ValidationGroup="OptionsValidationGroup"></asp:DropDownList>
							<asp:Label ID="TopicOptionsLabel" runat="server" Visible="false"></asp:Label>
						</ItemTemplate>
						<FooterTemplate>
							<button onserverclick="RefreshButton_Click" ID="RefreshButton" runat="server" style="margin-right:4px;" causesvalidation="false">Refresh</button>
							<button id="OptionsUpdateButton" runat="server" ValidationGroup="OptionsValidationGroup" onserverclick="OptionsUpdateButton_Click">Update</button>
						</FooterTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="">
						<ItemTemplate>
							<asp:TextBox ID="StatusChangeObjectTypeTextBox" runat="server" Text="<%#Convert.ToInt32(((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectType)%>" Visible="false"></asp:TextBox>
							<asp:TextBox ID="StatusChangeObjectKTextBox" runat="server" Text="<%#((Bobs.ThreadUsr)Container.DataItem).StatusChangeObjectK%>" Visible="false"></asp:TextBox>
							<asp:TextBox ID="StatusChangeObjectIsNewsTextBox" runat="server" Text="true" Visible="false"></asp:TextBox>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</p>
	</asp:Panel>

	<a ID="AfterGridViewsAnchor"></a>
	<asp:Panel runat="server" ID="WatchPhotoPanel">
		<br />
		<a ID="WatchPhotoPanelAnchorAnchor"></a>
		<h2>
			Topics from watching photos
		</h2>
		<p>
			When you spot yourself in a photo or click the "Watch" button, any future topics about it will go into your inbox.
		</p>
		<p>
			You have <asp:Label ID="NumberOfTopicsFromWatchingPhotosLabel" runat="server"></asp:Label> in your inbox from watching photos.
		</p>
		<p>
			<asp:Panel ID="WatchPhotoTopicOptionsPanel" runat="server">
				Options: <asp:DropDownList ID="PhotoTopicOptionsDropDownList" runat="server" style="margin-right:4px;"></asp:DropDownList><button id="PhotoOptionsUpdateButton" runat="server" ValidationGroup="PhotoOptionsValidationGroup" onserverclick="PhotoTopicOptionsUpdateButton_Click">Update</button>
			</asp:Panel>
			<asp:Label ID="WatchPhotoTopicOptionsLabel" runat="server" style="vertical-align:bottom;" Visible="false"></asp:Label>
		</p>
	</asp:Panel>
	<asp:Panel runat="server" ID="WatchArticlePanel">
		<br />
		<a ID="WatchArticlePanelAnchor"></a>
		<h2>Topics from watching articles</h2>
		<p>
			When you click the "Watch" button on an article, any future topics about it will go into your inbox.
		</p>
		<p>
			You have <asp:Label ID="NumberOfTopicsFromWatchingArticlesLabel" runat="server"></asp:Label> in your inbox from watching articles.
		</p>
		<p>
			<asp:Panel ID="WatchArticleTopicOptionsPanel" runat="server">
				Options: <asp:DropDownList ID="ArticleTopicOptionsDropDownList" runat="server" style="margin-right:4px;"></asp:DropDownList><button id="ArticleOptionsUpdateButton" runat="server" ValidationGroup="ArticleOptionsValidationGroup" onserverclick="ArticleTopicOptionsUpdateButton_Click">Update</button>
			</asp:Panel>
			<asp:Label ID="WatchArticleTopicOptionsLabel" runat="server" style="vertical-align:bottom;" Visible="false"></asp:Label>
		</p>
		
	</asp:Panel>
	<asp:Panel runat="server" ID="WatchOtherThreadPanel">
		<br />
		<a ID="WatchOtherThreadPanelAnchor"></a>
		<h2>Other topics</h2>
		<p>
			Once you remove a topic from your inbox, we can't tell how it got there in the first place. These 
			topics are either:
		</p>	
		<ul>
			<li>Topics you removed from your inbox with the "Inbox" button.</li>
			<li>Topics you manually watched using the "Watch" button.</li>
			<li>Topics you've posted a comment in.</li>
		</ul>
		<p>
			You have <asp:Label ID="NumberOfTopicsFromWatchingOtherThreadLabel" runat="server"></asp:Label> in your inbox.
		</p>
		<p>
			<asp:Panel ID="WatchOtherTopicOptionsPanel" runat="server">
				Options: <asp:DropDownList ID="OtherTopicOptionsDropDownList" runat="server" style="margin-right:4px;"></asp:DropDownList><button id="OtherOptionsUpdateButton" runat="server" ValidationGroup="OtherOptionsValidationGroup" onserverclick="OtherTopicOptionsUpdateButton_Click">Update</button>
			</asp:Panel>
			<asp:Label ID="WatchOtherTopicOptionsLabel" runat="server" style="vertical-align:bottom;" Visible="false"></asp:Label>
		</p>
	</asp:Panel>
	<p align="center">
		<br />
		<button onserverclick="RefreshButton_Click" ID="RefreshButton" runat="server" causesvalidation="false">Refresh</button>
	</p>
	<asp:PlaceHolder runat="server" ID="StatusPlaceHolder" EnableViewState="false"></asp:PlaceHolder>
</div>
</asp:Panel>
<asp:Panel ID="NoInboxSpamPanel" runat="server" Visible="false">
	<dsi:h1 ID="H1" runat="Server">No spam in your inbox</dsi:h1>
	<div class="ContentBorder">
		<p align="center">
			There is no spam in your inbox.
		</p>
		<p align="center">
			<button onserverclick="RefreshButton_Click" ID="RefreshButton2" runat="server" causesvalidation="false">Refresh</button>
		</p>
	</div>
</asp:Panel>

