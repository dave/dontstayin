<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddToGroup.ascx.cs" Inherits="Spotted.Pages.Photos.AddToGroup" %>

<a name="EmailToFriend"></a>
<dsi:h1 runat="server" ID="H11">Add this photo to my "top photos" section</dsi:h1>
<div class="ContentBorder">
	<p align="center">
		<a href="" runat="server" id="PhotoAnchor"><img src="" runat="server" id="PhotoImg" border="0" class="BorderBlack All" width="100" height="100"></a>
	</p>
	<asp:Panel Runat="server" ID="RepeaterPanel">
		<p>
			First choose which group you would like to include this photo on:
		</p>
		<ul>
			<asp:Repeater Runat="server" ID="GroupRepeater">
				<ItemTemplate>
					<li>
						<a href="<%#CurrentPhoto.UrlApp("addtogroup", "groupk", ((Bobs.Group)(Container.DataItem)).K.ToString())%>"><%#((Bobs.Group)(Container.DataItem)).Name%></a>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ul>
	</asp:Panel>
	<asp:Panel Runat="server" ID="GroupPanel">
		<h2>
			<asp:Label Runat="server" ID="GroupLabel"></asp:Label>
		</h2>
		<p>
			If you would like this photo to appear in the "top photos" list on 
			your group home-page, enter a caption and tick the box:
		</p>
		<p>
			<asp:CheckBox Runat="server" ID="ShowCheckBox" Text="Show in this group with the caption:"/> 
			<asp:TextBox Runat="server" ID="CaptionTextBox" Columns="60" MaxLength="200"/>
			<asp:CheckBox runat="server" ID="CaptionCompetitionCheckBox" Text="Enter into caption competition" Visible="false" />
		</p>
		<p>
			<asp:Button Runat="server" onclick="Update_Click" Text="Update" CausesValidation="False" ID="Button1" EnableViewState="False"/>
			<asp:Label Runat="server" ID="SentEmailsLabel" ForeColor="#0000ff" />
		</p>
		<p>
			<small>
				Up to 8 photos are listed on the group page, and ordered by the 
				date/time they are added to the list.
				If you remove a photo then add it again, it's date/time will be 
				reset to the present, so the photo will move to the front of the 
				list.
			</small>
		</p>
	</asp:Panel>

</div>
