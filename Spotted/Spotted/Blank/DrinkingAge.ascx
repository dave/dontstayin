<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DrinkingAge.ascx.cs" Inherits="Spotted.Blank.DrinkingAge" %>
<!-- Welcome to DontStayIn -->
<link rel="stylesheet" type="text/css" href="/support/style.css?a=29"/>
&nbsp;<br>&nbsp;
<center>
<table width=285 cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td valign=bottom align=left width="285" rowspan="2">
				
				
<center>
<a href="/"><img src="/gfx/dsi-1-126.gif" border=0 width="126" height="53"></a>
</center>

<div style="padding:10px;">
<div style="width:100%;border:solid 1px #000000;padding:2px 4px 2px 4px; margin:0px 0px 13px 0px;">
	<p>
		Please enter your date of birth and the country you're accessing from:
	</p>
	<p>
		Birthday: 
		<asp:DropDownList ID="Day" runat="server">
			<asp:ListItem Text="day" Value="0" />
			<asp:ListItem Text="1" Value="1" />
			<asp:ListItem Text="2" Value="2" />
			<asp:ListItem Text="3" Value="3" />
			<asp:ListItem Text="4" Value="4" />
			<asp:ListItem Text="5" Value="5" />
			<asp:ListItem Text="6" Value="6" />
			<asp:ListItem Text="7" Value="7" />
			<asp:ListItem Text="8" Value="8" />
			<asp:ListItem Text="9" Value="9" />
			<asp:ListItem Text="10" Value="10" />
			<asp:ListItem Text="11" Value="11" />
			<asp:ListItem Text="12" Value="12" />
			<asp:ListItem Text="13" Value="13" />
			<asp:ListItem Text="14" Value="14" />
			<asp:ListItem Text="15" Value="15" />
			<asp:ListItem Text="16" Value="16" />
			<asp:ListItem Text="17" Value="17" />
			<asp:ListItem Text="18" Value="18" />
			<asp:ListItem Text="19" Value="19" />
			<asp:ListItem Text="20" Value="20" />
			<asp:ListItem Text="21" Value="21" />
			<asp:ListItem Text="22" Value="22" />
			<asp:ListItem Text="23" Value="23" />
			<asp:ListItem Text="24" Value="24" />
			<asp:ListItem Text="25" Value="25" />
			<asp:ListItem Text="26" Value="26" />
			<asp:ListItem Text="27" Value="27" />
			<asp:ListItem Text="28" Value="28" />
			<asp:ListItem Text="29" Value="29" />
			<asp:ListItem Text="30" Value="30" />
			<asp:ListItem Text="31" Value="31" />
		</asp:DropDownList>
		<asp:DropDownList ID="Month" runat="server">
			<asp:ListItem Text="month" Value="0" />
			<asp:ListItem Text="1" Value="1" />
			<asp:ListItem Text="2" Value="2" />
			<asp:ListItem Text="3" Value="3" />
			<asp:ListItem Text="4" Value="4" />
			<asp:ListItem Text="5" Value="5" />
			<asp:ListItem Text="6" Value="6" />
			<asp:ListItem Text="7" Value="7" />
			<asp:ListItem Text="8" Value="8" />
			<asp:ListItem Text="9" Value="9" />
			<asp:ListItem Text="10" Value="10" />
			<asp:ListItem Text="11" Value="11" />
			<asp:ListItem Text="12" Value="12" />
		</asp:DropDownList>
		<asp:DropDownList ID="Year" runat="server">
			<asp:ListItem Text="year" Value="0" />
			<asp:ListItem Text="2000" Value="2000" />
			<asp:ListItem Text="1999" Value="1999" />
			<asp:ListItem Text="1998" Value="1998" />
			<asp:ListItem Text="1997" Value="1997" />
			<asp:ListItem Text="1996" Value="1996" />
			<asp:ListItem Text="1995" Value="1995" />
			<asp:ListItem Text="1994" Value="1994" />
			<asp:ListItem Text="1993" Value="1993" />
			<asp:ListItem Text="1992" Value="1992" />
			<asp:ListItem Text="1991" Value="1991" />
			<asp:ListItem Text="1990" Value="1990" />
			<asp:ListItem Text="1989" Value="1989" />
			<asp:ListItem Text="1988" Value="1988" />
			<asp:ListItem Text="1987" Value="1987" />
			<asp:ListItem Text="1986" Value="1986" />
			<asp:ListItem Text="1985" Value="1985" />
			<asp:ListItem Text="1984" Value="1984" />
			<asp:ListItem Text="1983" Value="1983" />
			<asp:ListItem Text="1982" Value="1982" />
			<asp:ListItem Text="1981" Value="1981" />
			<asp:ListItem Text="1980" Value="1980" />
			<asp:ListItem Text="1979" Value="1979" />
			<asp:ListItem Text="1978" Value="1978" />
			<asp:ListItem Text="1977" Value="1977" />
			<asp:ListItem Text="1976" Value="1976" />
			<asp:ListItem Text="1975" Value="1975" />
			<asp:ListItem Text="1974" Value="1974" />
			<asp:ListItem Text="1973" Value="1973" />
			<asp:ListItem Text="1972" Value="1972" />
			<asp:ListItem Text="1971" Value="1971" />
			<asp:ListItem Text="1970" Value="1970" />
			<asp:ListItem Text="1969" Value="1969" />
			<asp:ListItem Text="1968" Value="1968" />
			<asp:ListItem Text="1967" Value="1967" />
			<asp:ListItem Text="1966" Value="1966" />
			<asp:ListItem Text="1965" Value="1965" />
			<asp:ListItem Text="1964" Value="1964" />
			<asp:ListItem Text="1963" Value="1963" />
			<asp:ListItem Text="1962" Value="1962" />
			<asp:ListItem Text="1961" Value="1961" />
			<asp:ListItem Text="1960" Value="1960" />
			<asp:ListItem Text="1959" Value="1959" />
			<asp:ListItem Text="1958" Value="1958" />
			<asp:ListItem Text="1957" Value="1957" />
			<asp:ListItem Text="1956" Value="1956" />
			<asp:ListItem Text="1955" Value="1955" />
			<asp:ListItem Text="1954" Value="1954" />
			<asp:ListItem Text="1953" Value="1953" />
			<asp:ListItem Text="1952" Value="1952" />
			<asp:ListItem Text="1951" Value="1951" />
			<asp:ListItem Text="1950" Value="1950" />
			<asp:ListItem Text="1949" Value="1949" />
			<asp:ListItem Text="1948" Value="1948" />
			<asp:ListItem Text="1947" Value="1947" />
			<asp:ListItem Text="1946" Value="1946" />
			<asp:ListItem Text="1945" Value="1945" />
			<asp:ListItem Text="1944" Value="1944" />
			<asp:ListItem Text="1943" Value="1943" />
			<asp:ListItem Text="1942" Value="1942" />
			<asp:ListItem Text="1941" Value="1941" />
			<asp:ListItem Text="1940" Value="1940" />
		</asp:DropDownList>
	</p>
	<p>
		Country:
		<asp:DropDownList ID="CountryDrop" runat="server" style="width:194px;" />
	</p>
	<p style="text-align:right;">
		<asp:Button runat="server" OnClick="Continue_Click" Text="Continue -&gt;" />
	</p>
	<asp:CustomValidator Display="dynamic" ID="EntryVal" runat="server" EnableClientScript="false" OnServerValidate="Entry_Val" ErrorMessage="<p>Please select your date of birth and your home country above.</p>"></asp:CustomValidator>
	
</div>
</td></tr></table>
</center>
