<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cropper.ascx.cs" Inherits="Spotted.Controls.Cropper" %>

<asp:Panel runat="server" ID="FlashPanel">

	<asp:PlaceHolder runat="server" ID="FlashPlaceHolder" EnableViewState="false"/>

	<input type="hidden" runat="server" id="cW" NAME="cW"/>
	<input type="hidden" runat="server" id="cH" NAME="cH"/>
	<input type="hidden" runat="server" id="iW" NAME="iW"/>
	<input type="hidden" runat="server" id="iH" NAME="iH"/>
	<input type="hidden" runat="server" id="xO" NAME="xO"/>
	<input type="hidden" runat="server" id="yO" NAME="yO"/>
	<input type="hidden" runat="server" id="sC" NAME="sC"/>

</asp:Panel>

<asp:Panel runat="server" ID="DartPanel">

	<asp:PlaceHolder runat="server" ID="DartPlaceHolder" EnableViewState="false"/>

    <input runat="server" type="hidden" id="cCropWidth" value="150" />
    <input runat="server" type="hidden" id="cCropHeight" value="150" />
    <input runat="server" type="hidden" id="cXOffset" value="0" />
    <input runat="server" type="hidden" id="cYOffset" value="0" />
    <input runat="server" type="hidden" id="cZoom" value="35" />
    
    <input runat="server" type="hidden" id="cImageUrl" value="http://pix-eu.dontstayin.com/9973f219-3a79-467d-bf9c-35e261a7ad23.jpg" />
    <input runat="server" type="hidden" id="cAllowCustomHeight" value="true" />
    <input runat="server" type="hidden" id="cAllowCustomWidth" value="true" />
    <input runat="server" type="hidden" id="cMaxWidth" value="200" />
    <input runat="server" type="hidden" id="cMaxHeight" value="200" />
    <input runat="server" type="hidden" id="cMinWidth" value="100" />
    <input runat="server" type="hidden" id="cMinHeight" value="100" />
    
    <div id="cropperMain" style="display:none; position:relative; width:600px; height:400px; overflow:hidden; background-color:#000000;">
    	<img id="cropperImage" style="position:absolute; z-index:1;" />
    	<canvas id="cropperCanvas" width="600" height="400" style="position:absolute; top:0px; left:0px; width:600px; height:400px; z-index:2;" />
    </div>
    <div style="position:relative; margin-top:10px;">
    	<input id="cropperSlider" type="range" max="100" value="50" style="position:absolute; width:300px; left:150px; z-index:3;" />
    </div>

    <script type="text/javascript" src="/misc/cropper/cropper.dart.js"></script>

</asp:Panel>
