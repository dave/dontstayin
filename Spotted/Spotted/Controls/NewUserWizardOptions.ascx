<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewUserWizardOptions.ascx.cs" Inherits="Spotted.Controls.NewUserWizardOptions" %>

<dsi:h1 runat="server">Explore Don't Stay In</dsi:h1>
<div class="ContentBorder">
	<p>Use the buttons below to navigate around the site:</p>
	<p>
		<table width="100%">
			<tr>
				<td align="center" style="width:16%;" valign="top">
					<%= SelectedOption == Option.FreeGuestlist ? "<b>" : "<a href='/Support/Log.aspx?Item=" + (int)Log.Items.FreeGuestlistClicked + "' onclick='try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }'>"%>
					<img border="0" src="/gfx/new-user-freeguestlist.png" width="69" height="43" class="BackgroundWhite Block" style="margin-bottom:5px;" /><nobr>Free Guestlist</nobr>
					<%= SelectedOption == Option.FreeGuestlist ? "</b>" : "</a>"%>
				</td>
				<td align="center" style="width:16%;" valign="top">
					<%= SelectedOption == Option.FindYourPhoto ? "<b>" : "<a href='/Support/Log.aspx?Item=" + (int)Log.Items.FindYourPhotoClicked + "' onclick='try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }'>"%>
					<img border="0" src="/gfx/new-user-find-photos.png" width="69" height="43" class="BackgroundWhite Block" style="margin-bottom:5px;" /><nobr>Find your photo</nobr>
					<%= SelectedOption == Option.FindYourPhoto ? "</b>" : "</a>" %>
				</td>
				<td align="center" style="width:16%;" valign="top">
					<%= SelectedOption == Option.UploadPhotos ? "<b>" : "<a href='/Support/Log.aspx?Item=" + (int)Log.Items.UploadPhotosClicked + "' onclick='try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }'>"%>
					<img border="0" src="/gfx/new-user-upload-photos.png" width="69" height="43" class="BackgroundWhite Block" style="margin-bottom:5px;" /><nobr>Upload photos</nobr>
					<%= SelectedOption == Option.UploadPhotos ? "</b>" : "</a>"%>
				</td>
				<td align="center" style="width:16%;" valign="top">
					<%= SelectedOption == Option.FindEvents ? "<b>" : "<a href='/Support/Log.aspx?Item=" + (int)Log.Items.FindEventsClicked + "' onclick='try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }'>"%>
					<img border="0" src="/gfx/new-user-find-events.png" width="69" height="43" class="BackgroundWhite Block" style="margin-bottom:5px;" /><nobr>Find events</nobr>
					<%= SelectedOption == Option.FindEvents ? "</b>" : "</a>" %>
				</td>
				<td align="center" style="width:16%;" valign="top">
					<%= SelectedOption == Option.FindYourFriends ? "<b>" : "<a href='/Support/Log.aspx?Item=" + (int)Log.Items.FindYourFriendsClicked + "' onclick='try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }'>"%>
					<img border="0" src="/gfx/new-user-find-friends.png" width="69" height="43" class="BackgroundWhite Block" style="margin-bottom:5px;" /><nobr>Find your friends</nobr>
					<%= SelectedOption == Option.FindYourFriends ? "</b>" : "</a>" %>
				</td>
				<td align="center" style="width:16%;" valign="top">
					<%= SelectedOption == Option.AddEvent ? "<b>" : "<a href='/Support/Log.aspx?Item=" + (int)Log.Items.AddEventClicked + "' onclick='try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }'>"%>
					<img border="0" src="/gfx/new-user-add-event.png" width="69" height="43" class="BackgroundWhite Block" style="margin-bottom:5px;" /><nobr>Add an event</nobr>
					<%= SelectedOption == Option.AddEvent ? "</b>" : "</a>"%>
				</td>
			</tr>
		</table>
	</p>
</div>
