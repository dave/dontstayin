var HtmlControlSelectionRange;
	
function HtmlControlShowHide(div, clientId)
{
	var prefix = clientId + (clientId.length == 0 ? "" : "_");
	document.getElementById(prefix + 'LinkDiv').style.display = (div == 'Link' ? '' : 'none');
	document.getElementById(prefix + 'ImageDiv').style.display = (div == 'Image' ? '' : 'none');
	document.getElementById(prefix + 'VideoDiv').style.display = (div == 'Video' ? '' : 'none');
	document.getElementById(prefix + 'MixmagDiv').style.display = (div == 'Mixmag' ? '' : 'none');
	document.getElementById(prefix + 'FlashDiv').style.display = (div == 'Flash' ? '' : 'none');
	document.getElementById(prefix + 'AdvancedDiv').style.display = (div == 'Advanced' ? '' : 'none');
	
	HtmlControlSetHelperAnchor(document.getElementById(prefix + 'LinkAnchor'), div == 'Link');
	HtmlControlSetHelperAnchor(document.getElementById(prefix + 'ImageAnchor'), div == 'Image');
	HtmlControlSetHelperAnchor(document.getElementById(prefix + 'VideoAnchor'), div == 'Video');
	HtmlControlSetHelperAnchor(document.getElementById(prefix + 'MixmagAnchor'), div == 'Mixmag');
	HtmlControlSetHelperAnchor(document.getElementById(prefix + 'FlashAnchor'), div == 'Flash');
	HtmlControlSetHelperAnchor(document.getElementById(prefix + 'AdvancedAnchor'), div == 'Advanced');
	
	document.getElementById(prefix + 'HelperPanelDisplayState').value = div;
	
}
function HtmlControlSetHelperAnchor(btn, state)
{
	if (state)
	{
		btn.className = "HelperAnchor Selected";
	}
	else
	{
		btn.className = "HelperAnchor";
	}
}
function HtmlControlReplaceText(clientId, replacementType, isInsert)
{
	var prefix = clientId + (clientId.length == 0 ? "" : "_");
	var textBox = document.getElementById(prefix + "HtmlTextBox");
	
	var isIE = false;
	if (document.all && window.ActiveXObject && navigator.userAgent.toLowerCase().indexOf("msie") > -1  && navigator.userAgent.toLowerCase().indexOf("opera") == -1)
		isIE = true;
		
	var replacement = "";
	
	if (isIE)
	{
	
		var range = document.selection.createRange();
		HtmlControlSelectionRange = range;
		var type = document.selection.type;
		var pe = type=="Control" ? range.item(0).parentElement : range.parentElement();

		if (pe!=textBox)
		{
			alert("Make sure your cursor is in the comment textbox first");
			return;
		}
		if (!isInsert)
		{
			replacement = range.text;
			if (replacement=="")
			{
				alert("First select some text");
				return;
			}
		}
	}
	else
	{
		var start = textBox.selectionStart;
		if (!isInsert)
		{
			replacement = textBox.value.substring(start, textBox.selectionEnd);
			if (replacement=="")
			{
				alert("First select some text");
				return;
			}
		}
	}
	
	if (replacementType != "MakeBold" && replacementType != "MakeItalic" && replacementType != "MakeSmall")
		ShowWaitingCursor();
	
	if (replacementType == "MakeBold")
	{
		HtmlControlReplaceTextSucceededCallback("<b>" + HtmlControlMakeEnd(replacement, "</b>"), clientId, null);
	}
	else if (replacementType == "MakeItalic")
	{
		HtmlControlReplaceTextSucceededCallback("<i>" + HtmlControlMakeEnd(replacement, "</i>"), clientId, null);
	}
	else if (replacementType == "MakeSmall")
	{
		HtmlControlReplaceTextSucceededCallback("<small>" + HtmlControlMakeEnd(replacement, "</small>"), clientId, null);
	}
	else if (replacementType == "LinkUrl")
	{	
		Spotted.WebServices.Html.LinkUrl(
			document.getElementById(prefix + 'LinkUrlTextBox').value, 
			replacement, 
			HtmlControlReplaceTextSucceededCallback, 
			HtmlControlReplaceTextFailedCallback, 
			clientId, 
			null);
	}
	else if (replacementType == "LinkProfile")
	{
		Spotted.WebServices.Html.LinkProfile(
			buddyChooserGetNickname(prefix + 'LinkProfileBuddyChooser'), 
			HtmlControlReplaceTextSucceededCallback, 
			HtmlControlReplaceTextFailedCallback, 
			clientId, 
			null);
	}
	else if (replacementType == "ImageUrl")
	{
		Spotted.WebServices.Html.ImageUrl(
			document.getElementById(prefix + 'ImageUrlSrcTextBox').value, 
			document.getElementById(prefix + 'ImageUrlHrefTextBox').value, 
			HtmlControlReplaceTextSucceededCallback, 
			HtmlControlReplaceTextFailedCallback, 
			clientId, 
			null);
	}
	else if (replacementType == "VideoFlv")
	{
		Spotted.WebServices.Html.VideoFlv(
			document.getElementById(prefix + 'VideoFlvUrlTextBox').value, 
			document.getElementById(prefix + 'VideoFlvWidthTextBox').value, 
			document.getElementById(prefix + 'VideoFlvHeightTextBox').value,
			HtmlControlReplaceTextSucceededCallback, 
			HtmlControlReplaceTextFailedCallback, 
			clientId, 
			null);
	}
	else if (replacementType == "FlashSwfUrl")
	{
		Spotted.WebServices.Html.FlashSwfUrl(
			document.getElementById(prefix + 'FlashSwfUrlUrlTextBox').value, 
			document.getElementById(prefix + 'FlashSwfUrlWidthTextBox').value, 
			document.getElementById(prefix + 'FlashSwfUrlHeightTextBox').value,
			document.getElementById(prefix + 'FlashSwfUrlDrawDropDownList').selectedIndex,
			HtmlControlReplaceTextSucceededCallback, 
			HtmlControlReplaceTextFailedCallback, 
			clientId, 
			null);
	}

}
function HtmlControlMakeEnd(str,end)
{
	if (str.substring(str.length-1)==" ")
		return str.substring(0,str.length-1)+end+" ";
	else
		return str+end;
}
function HtmlControlReplaceTextSucceededCallback(result, clientId, methodName)
{
	HideWaitingCursor();
	
	var prefix = clientId + (clientId.length == 0 ? "" : "_");
	var textBox = document.getElementById(prefix + "HtmlTextBox");
	
	var isIE = false;
	if (document.all && window.ActiveXObject && navigator.userAgent.toLowerCase().indexOf("msie") > -1  && navigator.userAgent.toLowerCase().indexOf("opera") == -1)
		isIE = true;
		
	if (isIE)
	{ 
		HtmlControlSelectionRange.text = result;
		HtmlControlSelectionRange.collapse(true);
		textBox.focus();
		HtmlControlSelectionRange.select();
	}
	else
	{
		var start = textBox.selectionStart;
		textBox.value = textBox.value.substring(0, start) + result + textBox.value.substring(textBox.selectionEnd, textBox.value.length);
		textBox.focus();
		textBox.setSelectionRange(start + result.length, start + result.length);
	}
	
}
function HtmlControlReplaceTextFailedCallback(error)
{
	HideWaitingCursor();
	//alert(error);
}
