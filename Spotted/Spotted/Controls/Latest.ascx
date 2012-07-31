<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Latest.ascx.cs" Inherits="Spotted.Controls.Latest" %>
<%@ Register TagPrefix="Spotted" TagName="LatestContent" Src="/Controls/LatestContent.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="LatestChat" Src="/Controls/LatestChat.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="LatestChatHotTopics" Src="/Controls/LatestChatHotTopics.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="AddThread" Src="/Controls/AddThread.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="LatestEventList" Src="/Controls/LatestEventList.ascx" %>
<%@ Register TagPrefix="Controls" TagName="EventBox" Src="/Controls/EventBox.ascx" %>

<Controls:EventBox runat="server" ID="EventBox" />

<a name="LatestBoxAnchor"></a>
<div id="LatestBox">
	<asp:Panel runat="server" ID="ChatHolderOuter">
		<h1 class="TabHolder">
			<a href="/" class="TabbedHeading Selected" runat="server" causesvalidation="false" id="ChatHeader" onclick="latestChatTabClick(0);return false;">Chat</a>
			<a href="/" class="TabbedHeading" runat="server" causesvalidation="false" id="HotTopicsHeader" onclick="latestChatTabClick(1);return false;">Hot topics</a>
			<span class="TabbedHeading Selected" runat="server" id="ChatHeaderSpan" style="display:none;">Chat</span>
		</h1>
		<div class="ContentBorder">
			<asp:Panel runat="server" ID="ChatHolder">
				<asp:Panel Runat="server" ID="AddThreadLinkPanel">
					<dsi:InlineScript ID="InlineScript1" runat="server">
						<script>
							function ShowNewThread() {
								document.forms[0]["<%= AddThreadStatusHidden.ClientID %>"].value = '1';
								document.getElementById('<%= AddThreadLinkP.ClientID %>').style.display = 'none';
								document.getElementById('<%= AddThreadPanel.ClientID %>').style.display = '';
								try {
									document.getElementById('<%= AddThread.AddThreadSubjectTextBox.ClientID %>').focus();
								}
								catch (ex) { }
							}
						</script>
					</dsi:InlineScript>
					<input type="hidden" runat="server" id="AddThreadStatusHidden" NAME="AddThreadStatusHidden"/>
					
					<p class="BigCenter" runat="server" id="AddThreadLinkP">
						<a href="/" onclick="ShowNewThread();return false;">Post your own comment</a>
					</p>
					<asp:Panel Runat="server" ID="AddThreadPanel" style="display:none;">
						<Spotted:AddThread runat="server" ID="AddThread"/>
					</asp:Panel>
				</asp:Panel>
				
				<div runat="server" id="LatestChatUcHolder">
					<Spotted:LatestChat Runat="server" ID="LatestChatUc" EnableViewState="False" />
				</div>
				<div runat="server" id="LatestHotTopicsUcHolder" style="display:none;">
					<Spotted:LatestChatHotTopics Runat="server" ID="LatestHotTopicsUc" EnableViewState="False" />
				</div>
			</asp:Panel>
			<dsi:InlineScript runat="server" ID="TabClientScript">
				<script>

					if (document.getElementById("<%= this.ClientID %>_LatestHotTopicsUc_LatestChatUc_ThreadsPanel").style.display == "none")
					{
						document.getElementById("<%= HotTopicsHeader.ClientID %>").style.display = "none";
						document.getElementById("<%= ChatHeader.ClientID %>").style.display = "none";
						document.getElementById("<%= ChatHeaderSpan.ClientID %>").style.display = "";
					}
					else
					{
						Sys.Application.add_navigate(latestChatTabNavigate);
					}

					function latestChatTabClick(tabID)
					{
						Sys.Application.addHistoryPoint({ Latest_ChatTab: tabID }, tabID == 0 ? "Chat" : "Hot topics");
					}
					function latestChatTabNavigate(sender, e)
					{
						var tabID = e.get_state().Latest_ChatTab;
						document.getElementById("<%= LatestChatUcHolder.ClientID %>").style.display = (tabID == 1 ? "none" : "");
						document.getElementById("<%= LatestHotTopicsUcHolder.ClientID %>").style.display = (tabID == 1 ? "" : "none");

						try
						{
							document.getElementById("<%= ChatHeader.ClientID %>").className = (tabID == 1 ? "TabbedHeading" : "TabbedHeading Selected");
						}
						catch(ex){}
						
						try
						{
							document.getElementById("<%= HotTopicsHeader.ClientID %>").className = (tabID == 1 ? "TabbedHeading Selected" : "TabbedHeading");
						}
						catch(ex){}

					}
				</script>
			</dsi:InlineScript>
		</div>
	</asp:Panel>
	
	<Spotted:LatestContent Runat="server" ID="LatestContentUc" EnableViewState="False" />
	
	<Spotted:LatestEventList Runat="server" ID="LatestEventList1" EnableViewState="False" />
	
	
</div>
