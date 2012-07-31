<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerSlots.ascx.cs" Inherits="Spotted.Controls.Admin.BannerSlots" %>
<style>
.BannerCalendarTitle td
{
	color:#ffffff;
	font-weight:bold;
	padding-top:2px;
	padding-bottom:2px;
	padding-left:4px;
	padding-right:4px;
	font-size:75%;
	background-color:#333333;
}
.BannerCalendarDayHeader
{
	padding:4px;
	text-align:left;
}

.BannerCalendar
{
	border:0px solid #333333;
}

.BannerCalendarDay
{
	width:80px;
	height:40px;
	border:1px solid #999999;
	vertical-align:top;
	text-align:left;
	padding:4px;
}
.BannerCalendarOtherMonthDay
{
	border:0px solid #999999;
}
</style>
<asp:Table Runat="server" ID="Table">
<asp:TableRow Runat="server" ID="Row"></asp:TableRow>
</asp:Table>
