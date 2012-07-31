<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Review.ascx.cs" Inherits="Spotted.Pages.Events.Review" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="Headers" TagName="Event" Src="/Controls/Headers/EventHeader.ascx" %>

<Headers:Event runat="server" />

<asp:Panel Runat="server" id="InfoPanel">
	<dsi:h1 runat="server" ID="H11">Add/Edit your review</dsi:h1>
	<div class="ContentBorder">
		<p style="font-weight:bold;">
			Please only post an event review here. Please use the 
			<a href="<%= CurrentEvent.UrlDiscussion() %>"><%= CurrentEvent.Name %> discussion board</a> if you have something else to say.
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="SummaryTextBox" 
				ErrorMessage="<p>Please enter a summary</p>"/>
		<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="ReviewHtml" 
				ErrorMessage="<p>Please enter a review</p>" ID="Requiredfieldvalidator2"/>
		<p>
			First summarise the event in less that 50 characters:
		</p>
		<p>
			<asp:TextBox Runat="server" ID="SummaryTextBox" MaxLength="50" Columns="60" TabIndex="1"></asp:TextBox>
		</p>
		<p>
			Now write your full review below:
		</p>
		<p>
			<dsi:Html runat="server" id="ReviewHtml" PreviewType="Comment" DisableContainer="true" SaveButtonText="Post review" OnSave="ReviewSave" TabInbexBase="2" />
		</p>
		<p>
			<asp:Label Runat="server" ID="StatusLabel" ForeColor="#0000ff" Visible="False">
				Review saved.
			</asp:Label>
		</p>
		<p>
			<small>Please note it can take up to 10 minutes for your review to appear in the "Latest..." box.</small>
		</p>
		<asp:Panel Runat="server" ID="DeleteReviewPanel" Visible="False">
			<p>
				<asp:Button Runat="server" CausesValidation="False" ID="DeleteButton" 
					OnClick="DeleteClick" Text="Delete this review" TabIndex="20"></asp:Button>
			</p>
		</asp:Panel>
	</div>
</asp:Panel>
<asp:Panel Runat="server" id="CantEditPanel" Visible="False">
	<dsi:h1 runat="server" ID="H12">Event hasn't happened!</dsi:h1>
	<div class="ContentBorder">
		<p>
			You can't add a review for an event that hasn't happened!
		</p>
		<p>
			Click to go back to the event page: <a href="<%= CurrentEvent.Url() %>"><%= CurrentEvent.FriendlyName %></a>
		</p>
	</div>
</asp:Panel>
