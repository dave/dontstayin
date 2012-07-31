<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Plus.ascx.cs" Inherits="Spotted.Pages.Promoters.Plus" %>


<dsi:PromoterIntro runat="server" ID="PromoterIntro1" Header="Promoter Plus application">
	<p>
		The Promoter Plus account is free. It gives you full use of 
		the tickets system, and an optional credit facility. To apply for 
		a Promoter Plus account, complete the instructions below:
	</p>
</dsi:PromoterIntro>

<table>
	<tr>
		<td width="50%" style="padding-right:7px;" valign="top">
			<dsi:h1 runat="server" ID="H1fd4">Limited companies</dsi:h1>
			<div class="ContentBorder">
				<p>
					This is ONLY for Limited companies.
				</p>
				<ul style="margin-left:18px;">
					<li>
						Fill in the <a href="/misc/plus-account-application-ltd.pdf">Limited company application form</a> - 
						you'll need your Promoter ID, which is <%=  ((Spotted.Master.DsiPage)Page).Url.ObjectFilterPromoter.K.ToString()  %>.
					</li>
					<li>
						Include a company letterhead
					</li>
					<li>
						Include a void cheque
					</li>
					<li>
						Send the completed application form, along with the above items to:<br /><br />
						Promoter Plus application <%=  ((Spotted.Master.DsiPage)Page).Url.ObjectFilterPromoter.K.ToString()  %>,<br />
						Development Hell Limited,<br />
						Greenhill House, Thorpe Road,<br />
						Peterborough,<br />
						PE3 6RU,<br />
						UK
					</li>
				</ul>
			</div>
		</td>
		<td width="50%" style="padding-left:7px;" valign="top">
			<dsi:h1 runat="server" ID="H14">Individuals</dsi:h1>
			<div class="ContentBorder">
				<p>
					This is for all non Limited companies - e.g. individuals, sole traders, partnerships etc.
				</p>
				<ul style="margin-left:18px;">
					<li>
						Fill in the <a href="/misc/plus-account-application-personal.pdf">individual application form</a> - 
						you'll need your Promoter ID, which is <%=  ((Spotted.Master.DsiPage)Page).Url.ObjectFilterPromoter.K.ToString()  %>.
					</li>
					<li>
						Include a copy of your passport or driving license
					</li>
					<li>
						Include a copy of a utility bill dated within the last 3 months
					</li>
					<li>
						Include a void cheque
					</li>
					<li>
						Send the completed application form, along with the above items to:<br /><br />
						Promoter Plus application <%=  ((Spotted.Master.DsiPage)Page).Url.ObjectFilterPromoter.K.ToString()  %>,<br />
						Development Hell Limited,<br />
						Greenhill House, Thorpe Road,<br />
						Peterborough,<br />
						PE3 6RU,
						UK
					</li>
				</ul>
			</div>
		</td>
	</tr>
</table>
