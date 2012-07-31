<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="WhosOnline.ascx.cs" Inherits="Spotted.Controls.Navigation.WhosOnline" %>
<%@ Register TagPrefix="Navigation" TagName="Online" Src="Online.ascx" %>
<asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
	<ContentTemplate>
		<asp:Panel runat="server" id="OnlinePanel">
			<dsi:h1 runat="server" Type="Nav" ID="H12" NAME="H12">Who's online?</dsi:h1>
			<div class="NavRightBorder">
				<p>
					<asp:Label Runat="server" ID="OnlineLabel"/>
				</p>
				<p>
					<Navigation:Online runat="server" ID="OnlineList"/>
				</p>
				<p>
					Your buddies are in <b>bold</b>.
				</p>
				<p>
					This list slows page load times if there are lots of people online.
					<asp:LinkButton Runat="server" CausesValidation="False" ID="Linkbutton1" OnClick="HideOnlineLinkButtonClick">Click here to hide it</asp:LinkButton>.
				</p>
			</div>
		</asp:Panel>

		<asp:Panel runat="server" id="OnlineHiddenPanel">
			<dsi:h1 runat="server" Type="Nav" ID="H1" NAME="H12">Who's online?</dsi:h1>
			<div class="NavRightBorder">
				<p>
					<asp:Label Runat="server" ID="OnlineHiddenLabel"/> - <asp:LinkButton Runat="server" CausesValidation="False" ID="ShowOnlineLinkButton" OnClick="ShowOnlineLinkButtonClick">show a list</asp:LinkButton>
				</p>
			</div>
		</asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>
