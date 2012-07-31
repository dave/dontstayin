<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Intro.ascx.cs" Inherits="Spotted.Pages.Promoters.Intro" %>


<asp:Panel Runat="server" ID="PanelPromoterList">
	<dsi:h1 runat="server" ID="H12">Promoters</dsi:h1>
	<div class="ContentBorder">
		<p>
			You're in several promoter accounts. Choose which you want to view below:
		</p>
		<asp:Repeater Runat="server" ID="PromoterRepeater">
			<ItemTemplate>
				<p class="BigCenter"><a href="<%#((Bobs.Promoter)(Container.DataItem)).Url()%>"><%#((Bobs.Promoter)(Container.DataItem)).Name%></a></p>
			</ItemTemplate>
		</asp:Repeater>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelNoAccount">
	
	<dsi:h1 runat="server" ID="H11">Promoters</dsi:h1>
	<div class="ContentBorder">
		<p>
			<center>
				<img src="/gfx/quote-bacardi.gif" width="387" height="174" style="margin-bottom:13px;">
				<img src="/gfx/quote-gods.gif" width="387" height="174" style="margin-bottom:13px;">
				<img src="/gfx/quote-frantic.gif" width="387" height="174" style="margin-bottom:13px;">
			</center>
		</p>
		<p class="BigCenter">
			Are you an <a href="" onclick="document.getElementById('GeneralAdvertisers').style.display='none';document.getElementById('EventPromoters').style.display='';return false;">event promoter</a> or a <a href="" onclick="document.getElementById('GeneralAdvertisers').style.display='';document.getElementById('EventPromoters').style.display='none';return false;">general advertiser</a>?
		</p>
		<div id="EventPromoters" style="display:none;">
			<p>		
				If you run events you can sign up for a promoter account - 
				it's a FREE account that will let you:
			</p>
			
			<h2>
				Increase exposure
			</h2>
			<p>
				DontStayIn is the world's largest nightlife community, so it's 
				easy to see how we can help promote your events!
			</p>
			
			<h2>
				Add events, and get full control
			</h2>
			<p>
				We'll talk you through the easy process of adding your events 
				to DontStayIn. You'll have full access to your events - so you 
				can make changes to get the best benefit.
			</p>
			
			<h2>
				Boost numbers by advertising
			</h2>
			<p>
				It's very easy to book banner adverts, which increase awareness 
				of you events. You don't need to pay expensive design fees, 
				and you can target your banners adverts to your town or music 
				type.
			</p>
			
			<h2>Manage your group</h2>
			<p>
				You might have noticed you already have a community forming 
				round your events. With a promoter account you'll be able to 
				take full control, and moderate your own group! All for FREE!
			</p>
			
			<h2>
				Post news releases
			</h2>
			<p>
				You'll be able to post news releases to your group - keeping our
				thousands of members up-to-date with info about your latest 
				promotions.
			</p>
			
			<h2>Run competitions</h2>
			<p>
				It's FREE to add a competition. You just enter the question, the answer 
				and the prizes. We'll draw your competition automatically, and we'll 
				send you a message with the details of the winner. 
			</p>
			
			<p class="BigCenter">
				Best of all, the promoter account is FREE!
			</p>
			
			<p class="BigCenter">
				<a href="/pages/promoters/edit" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Click here for your FREE promoter account</a>
			</p>
		</div>
		
		<div id="GeneralAdvertisers" style="display:none;">
			<p>
				You don't have to be advertising events to use the promoter 
				account - general advertising is booked using the same easy to use 
				pages:
			</p>
			
			<h2>
				Book banners
			</h2>
			<p>
				It's very easy to book banner adverts, which can be targetted by 
				town or music style. We have five banner positions to choose from, and 
				extremely competitive rates.
			</p>
			
			<h2>Run competitions</h2>
			<p>
				It's FREE to add a competition. You just enter the question, the answer 
				and the prizes. We'll draw your competition automatically, and we'll 
				send you a message with the details of the winner. 
			</p>
			
			<h2>
				Exposure for your brand
			</h2>
			<p>
				DontStayIn is the world's largest nightlife community, so it's 
				easy to see how we can help promote your brand!
			</p>
			
			
			<p class="BigCenter">
				Best of all, the promoter account is FREE!
			</p>
			
			<p class="BigCenter">
				<a href="/pages/promoters/edit" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Click here for your FREE promoter account</a>
			</p>
		</div>
		
		
		
		
		
	</div>
</asp:Panel>
