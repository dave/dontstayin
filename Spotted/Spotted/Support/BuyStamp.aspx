<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyStamp.aspx.cs" Inherits="Spotted.Support.BuyStamp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <center>
		<img src="<%= Bobs.Storage.Path(new Guid("8ad5c8dc-27c3-49cf-9179-cc5c105473f4"), "gif") %>" />
		<br />
		SPOTTER CODE<br />
		<%= Usr.Current == null ? "0000-0000" : Usr.Current.SpotterCode%>
		<br />
		<form name="product" action="http://www.stampsdirect.co.uk/index.asp?function=CART&mode=ADD&productid=855" method="post">
			<input type="hidden" name="optiontype_1" value="DROPDOWN" />
			<input type="hidden" name="options_1" value="1" />
			<input type="hidden" name="optiontype_2" value="DROPDOWN" />
			<input type="hidden" name="options_2" value="3" />
			<input type="hidden" name="optiontype_3" value="DROPDOWN" />
			<input type="hidden" name="options_3" value="56" />
			<input type="hidden" name="optiontype_4" value="TEXT" />
			<input type="hidden" name="options_4" value="SPOTTER CODE" />
			<input type="hidden" name="options_4_TEXTBOX" value="Line 1" />
			<input type="hidden" name="optiontype_5" value="TEXT" />
			<input type="hidden" name="options_5" value="<%= Usr.Current == null ? "0000-0000" : Usr.Current.SpotterCode %>" />
			<input type="hidden" name="options_5_TEXTBOX" value="Line 2" />
			<input type="hidden" name="optiontype_6" value="TEXT" />
			<input type="hidden" name="options_6" value="Ordered from DontStayIn" />
			<input type="hidden" name="options_6_TEXTBOX" value="Further instructions" />
			<input type="hidden" name="optiontype_7" value="DROPDOWN" />
			<input type="hidden" name="options_7" value="2765" />
			<input type="hidden" name="numberofoptions" value="7" />
			<input type="Submit" value="Buy a stamp">
		</form>
	</center>
</body>
</html>
