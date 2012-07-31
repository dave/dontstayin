<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapControl.ascx.cs" Inherits="Spotted.Controls.MapControl" %>
<script src="http://www.google.co.uk/jsapi?key=<%=GoogleKey %>" type="text/javascript"></script>
<script src="http://maps.google.co.uk/maps?file=api&amp;v=2&amp;key=<%=GoogleKey %>" type="text/javascript"></script>
<div runat="server" id="map" style="width:580px;height:300px;border:solid 1px black;"></div>

<asp:HiddenField ID="uiNorth" runat="server" Value="56" />
<asp:HiddenField ID="uiSouth" runat="server" Value="51.5" />
<asp:HiddenField ID="uiEast" runat="server" Value="0" />
<asp:HiddenField ID="uiWest" runat="server" Value="-3" />
