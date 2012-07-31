<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttendedEventControl.ascx.cs" Inherits="Spotted.Controls.AttendedEventControl" %>

<asp:Label Runat="server" ID="UsrEventAttendFutureLabel">Are you coming to this event? </asp:Label>
<asp:Label Runat="server" ID="UsrEventAttendPastLabel">Were you at this event? </asp:Label>
<asp:RadioButton Runat="server" ID="UsrEventAttendYes" GroupName="UsrEventAttendRBGroup" OnCheckedChanged="UsrEventAttendClick" AutoPostBack="True" Text="I'll be there"></asp:RadioButton>
<asp:RadioButton Runat="server" ID="UsrEventAttendNo" GroupName="UsrEventAttendRBGroup" OnCheckedChanged="UsrEventAttendClick" AutoPostBack="True" Text="Not this one"></asp:RadioButton>
<small Runat="server" ID="GalleryUpdateP">
	<asp:CheckBox 
		Runat="server" 
		ID="GalleryUpdateCheckBox" 
		Text="Send me new gallery emails" 
		AutoPostBack="True" 
		OnCheckedChanged="GalleryUpdate_Change"/>
</small>
