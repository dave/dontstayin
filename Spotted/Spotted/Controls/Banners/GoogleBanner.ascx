<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleBanner.ascx.cs" Inherits="Spotted.Controls.Banners.GoogleBanner" %>
<script type="text/javascript"><!--
google_ad_client = "pub-3401092463304336";
google_hints = "nightlife clubbing clubs music house electro techno hardcore hardstyle trance guestlist tickets photos photography digicam";
google_alternate_color = "FFFFFF";
google_ad_width = "<%= Width.ToString() %>";
google_ad_height = "<%= Height.ToString() %>";
google_ad_format = "<%= Width.ToString() %>x<%= Height.ToString() %>_as";
google_ad_type = "text_image";
google_ad_channel = "";
google_color_border = "FFFFFF";
google_color_bg = "FFFFFF";
google_color_link = "333333";
google_color_text = "333333";
google_color_url = "333333";
<asp:PlaceHolder runat="server" ID="uiPlaceHolder"></asp:PlaceHolder>
//--></script>
<script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
