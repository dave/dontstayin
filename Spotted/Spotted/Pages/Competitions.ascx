<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Competitions.ascx.cs" Inherits="Spotted.Pages.Competitions" %>


<asp:Panel Runat="server" ID="NoCompPanel">
	<dsi:h1 runat="server" ID="H13">Error</dsi:h1>
	<div class="ContentBorder">
		<p>
			This competiton isn't ready yet.
		</p>
	</div>
</asp:Panel>

<asp:PlaceHolder Runat="server" ID="HtmlOverridePh"></asp:PlaceHolder>

<asp:Panel Runat="server" ID="CompPanel">
	<asp:Panel Runat="server" ID="TopPhotosNewsPanel">
		<dsi:h1 runat="server" ID="Header"><%= CurrentComp.NameCaps %></dsi:h1>
		<div class="ContentBorder">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td valign="top" runat="server" id="PlaceImgCell" style="padding-right:7px;">
						<p>
							<img src="<%= CurrentComp.AnyPicPath %>" class="BorderBlack All"/>
						</p>
					</td>
					<td width="100%" valign="top">
						<p>
							This competition runs from <%= Cambro.Misc.Utility.FriendlyDate(CurrentComp.DateTimeStart,false) %> to 
							<%= Cambro.Misc.Utility.FriendlyDate(CurrentComp.DateTimeClose,false) %>. The prizes will be drawn at midday 
							<%= Cambro.Misc.Utility.FriendlyDate(CurrentComp.DateTimeClose,false) %>, and everyone that entered will 
							get an email showing who the winners are.
						</p>
						<h2>Prizes:</h2>
						<asp:PlaceHolder Runat="server" ID="PrizesPh"></asp:PlaceHolder>
						
						<asp:Panel Runat="server" ID="MoreInfoPanel">
							<h2>More info:</h2>
							<p>
								<asp:Label Runat="server" ID="MoreInfoLabel"/>
							</p>
						</asp:Panel>
					</td>
				</tr>
			</table>
		</div>
		
		<table cellspacing="0" cellpadding="0" border="0" width="100%">
			<tr>
				<td valign="top" width="50%" style="padding-right:5px;" runat="server" id="PlaceInfoTopPhotoCell">
					<asp:Panel Runat="server" ID="TopPhotoPanel">
						<dsi:h1 runat="server" id="H11">Question</dsi:h1>
						<div class="ContentBorder">
							<h2>
								<asp:Label Runat="server" ID="QuestionLabel"/>
							</h2>
							<asp:Panel Runat="server" ID="EntryPanel">
								<ul>
									<li><h2><asp:LinkButton Runat="server" OnCommand="Enter" CommandArgument="1" ID="EnterLinkButton1" OnClientClick="try { return WhenLoggedInButton(this); } catch(ex) { return false; }" /></h2></li>
									<li><h2><asp:LinkButton Runat="server" OnCommand="Enter" CommandArgument="2" ID="EnterLinkButton2" OnClientClick="try { return WhenLoggedInButton(this); } catch(ex) { return false; }" /></h2></li>
									<li><h2><asp:LinkButton Runat="server" OnCommand="Enter" CommandArgument="3" ID="EnterLinkButton3" OnClientClick="try { return WhenLoggedInButton(this); } catch(ex) { return false; }" /></h2></li>
								</ul>
								<p>
									To enter this competition, simply click one of the answers above. This competition closes <asp:Label Runat="server" ID="DateTimeCloseLabel1"/>.
								</p>
							</asp:Panel>
							<asp:Panel Runat="server" ID="EnteredPanel">
								<h2>Your entry</h2>
								<p>
									You have chosen <b><asp:Label Runat="server" ID="SelectedAnswerLabel"/></b>. 
									This competition closes <asp:Label Runat="server" ID="DateTimeCloseLabel"/> at midday.
								</p>
							</asp:Panel>
							<asp:Panel Runat="server" ID="FinishedPanel">
								<h2>Competition finished</h2>
								<p>
									This competition has finished. The correct answer is: 
									<b><asp:Label Runat="server" ID="CorrentAnswerLabel"/></b>
									
								</p>
								<h2>Winners</h2>
								<asp:Panel Runat="server" ID="WinnersPanel">
									<asp:PlaceHolder Runat="server" ID="WinnersPh"></asp:PlaceHolder>
									<asp:Panel Runat="server" ID="YouAreAWinnerPanel">
										<h2>YOU ARE A WINNER!</h2>
										<p>
											Please contact <a href="" runat="server" id="OwnerAnchor"></a> to claim your prize.
										</p>
									</asp:Panel>
								</asp:Panel>
								<asp:Panel Runat="server" ID="NoWinnersPanel">
									<p>
										The winners will be announced shortly. Check back later.
									</p>
								</asp:Panel>
							</asp:Panel>
						</div>
					</asp:Panel>
				</td>
				<td valign="top" width="50%" style="padding-left:5px;" runat="server" id="LatestCell">
					<asp:Panel Runat="server" ID="SpottedPanel" EnableViewState="false">
						<dsi:h1 runat="server" ID="H12" NAME="H12">Prize donated by...</dsi:h1>
						<div class="ContentBorder" style="width:295px">
							<asp:PlaceHolder Runat="server" ID="PromoterDetailsPh"/>
							<asp:Panel Runat="server" ID="MoreInfoPanel1">
								<h2>More info:</h2>
								<p>
									<asp:Label Runat="server" ID="MoreInfoLabel1"/>
								</p>
							</asp:Panel>
						</div>
					</asp:Panel>
				</td>
			</tr>
		</table>
	</asp:Panel>
</asp:Panel>
