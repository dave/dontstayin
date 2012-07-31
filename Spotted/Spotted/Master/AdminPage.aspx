<%@ Page EnableEventValidation="false" Trace="false" Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Spotted.Master.AdminPage" %>
<link rel="stylesheet" type="text/css" href="/support/style.css" />
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<%= Bobs.Storage.PathJavascriptFunction() %>
<%= Spotted.Main15Script.Register %>
<head runat="server" />
<SCRIPT LANGUAGE="javascript" SRC="/Misc/Flash.js"></SCRIPT>
<SCRIPT LANGUAGE="javascript" SRC="/Misc/Utilities.js"></SCRIPT>
<STYLE>
/*div.ContentBorder
{
	border:solid 1px #000000;
	padding:2px 8px 2px 8px;
	margin:0px 0px 13px 0px;
	width:100%;
}*/
p.LeftNavPara
{
	margin-bottom:2px;
	margin-top:2px;
	line-height:130%;
}

</STYLE>
<body>
	<DIV id="TipLayer" style="visibility:hidden;position:absolute;z-index:1000;top:-100px"></DIV>
    <form id="TemplateForm" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" />
		<table>
			<tr>
				<td valign="top" style="padding-left:15px;">
					<dsi:h1 runat="server">Admin</dsi:h1>
					<div class="ContentBorder" style="width:140px; margin-right:15px;">
<h2>Utility</h2>
<p class="LeftNavPara"><a class="headLink" href="/">Back to the site</a></p>

<p class="LeftNavPara"><a class="headLink" href="/admin/newobjects">New Objects</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/logs">Logs</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/multidelete">Multi delete</a></p>

						<h2>Mailing</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/mailing">Mailing</a></p>

<h2>Mixmag listings</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/mixmaglistings">Mixmag listings</a></p>

<h2>Tags</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/tags">Tag Admin</a></p>

<h2>Photos</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/photoofweekuser">User photo of week admin</a></p>

<h2>Donation Icons</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/donationicons">Donation icons</a></p>

						<h2>Promoters</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/salesfind">Find promoter</a></p>
<!--<p class="LeftNavPara"><a class="headLink" href="/admin/salesletterfollowup"><b>Letter follow-ups (shared)</b></a></p>-->
<p class="LeftNavPara"><a class="headLink" href="/admin/salesactive">My active</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/salesproactive">My proactive</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/salesnew">My new</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/salesidle">Idle (shared)</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/salesnew/all">All new (shared) (promoter sector only)</a></p>

						<h2>Sales stuff</h2>
<p class="LeftNavPara"><a class="headLink" href="/pages/promoters/edit"><nobr>Create promoter</nobr></a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/addbulkskeletonpromoters">Add bulk skeleton promoters</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/salescallsandstats">Sales stats</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/salescalls">Sales stats - calls</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/salescampaigns">Sales campaigns</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/EventsWithNoSpendPromoters">Events with no spend promoters</a>


<h2>Banners</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/bannerpositionavailability">Banner position availability</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/bannerhitviewer">Banner hit viewer</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/recentlyendedbanners">Recently ended banners</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/bannerimpressionstats">Target/actual Page hits/Impressions</a></p>
<p class="LeftNavPara"><a class="headLink" href="/pages/tagtest">Banner tag test</a></p>

<h2>Eflyers</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/flyeredit">eFlyers</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/flyerstats">eFlyer Stats</a></p>

<h2>Domains</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/registernewdomain">Register new Domain</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/domainstats">Domain Stats</a></p>


						<h2>Accounting</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/adminmainaccounting"><nobr>Admin accounting</nobr></a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/BankExport"><nobr>Bank export</nobr></a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/cardprocessingreport"><nobr>Card processing</nobr></a></p>

						<h2>Tickets</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/ticketsearch"><nobr>Ticket search</nobr></a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/ticketruns"><nobr>Ticket run search</nobr></a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/ticketfundsrelease"><nobr>Funds release</nobr></a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/PromoterWithTicketRunsVatStatus"><nobr>Promoter VAT info</nobr></a></p>


						<h2>Promoters more</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/createinvoice">Create&nbsp;invoice</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/promoterpm">Send&nbsp;promoter&nbsp;PM</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/promoterexport">PromotersXML</a></p>

						<h2>Other stuff</h2>
<p class="LeftNavPara"><a class="headLink" href="/admin/utility">Daves utilities</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/sql">Sql</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/emailunverify">Un-verify bounced emails</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/stats">Stats</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/weightedpages">Weighted page views</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/weightedrevenue">Weighted revenue</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/incomepaymentdate">Income - payment date</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/incomeearningdate">Income - earning date</a></p>
<p class="LeftNavPara"><a class="headLink" href="/admin/memcachedstats">Memcached stats</a></p>						
<p class="LeftNavPara"><a class="headLink" href="/admin/memcachedutils">Memcached utils</a></p>	
<p class="LeftNavPara"><a class="headLink" href="/admin/settings">Settings</a></p>	
<p class="LeftNavPara"><a class="headLink" href="/admin/adminstats">Admin stats</a></p>

						<h2>Old admin - DO NOT TOUCH!!!</h2>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/usr">Users</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/event">Events</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/venue">Venues</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/place">Places</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/promoter">Promoters</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/brand">Brands</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/banner">Banners</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/invoiceadmin">Invoices</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/article">Articles</a></p>
<p class="LeftNavPara"><a class="headLink" href="http://old.dontstayin.com/login-<%= Bobs.Usr.Current.K %>-<%= Bobs.Usr.Current.LoginString %>/admin/comp">Competitions</a></p>

					</div>
				</td>
				<td valign="top" width="100%">
					<asp:PlaceHolder runat="server" ID="SalesCallPlaceHolder"/>
					<div class="ContentBorderFlat">
						<asp:PlaceHolder Runat="server" ID="MainContentPlaceHolder"/>
					</div>
				</td>
			</tr>
			
			
		</table>
    </form>
	
</body>
</html>
