
function DbComboMultiUsrRefresh(ClientID, UniqueID)
{

	var SelectBox = document.getElementById(ClientID+"_SelectBox");
	var Values = document.forms[0].elements[UniqueID+"$Values"].value;
	var Texts = document.forms[0].elements[UniqueID+"$Texts"].value;
	var ValueAry = Values.split("&");
	var TextAry = Texts.split("&");
	
	SelectBox.innerHTML="";
	
	var z=SelectBox.cloneNode(true);
	//repeat here
	for (var i=0; i<ValueAry.length; i++)
	{
		if (ValueAry[i]!="")
		{
			var newOption = z.appendChild(document.createElement('option'));
			newOption.value = unescape(ValueAry[i]);
			newOption.text = unescape(TextAry[i]);
		}
	}
	//repeat here
	
	SelectBox.replaceNode(z);
	
}
function DbComboMultiUsrNewPicName(pic){
	return pic.substr(0,2)+"/"+pic.substr(2,2)+"/"+pic;
}
function DbComboMultiUsrNewPicDomain(pic){
	return "s" + pic.substr(0,1);
}
function DbComboMultiUsrSelectChange(ClientID, UniqueID)
{
	var ob = document.forms[0].elements[UniqueID+"$SelectBox"];
	var selected = new Array();
	var done = false;
	var singleSelected = 0;
	for (var i = 0; i < ob.options.length; i++)
	{
		if (ob.options[ i ].selected)
		{
			if (done)
			{
				DbComboMultiUsrClearPicture(ClientID, UniqueID, "");
				return;
			}
			done = true;
			singleSelected = i;
		}
	}
	
	var PicHolder = document.getElementById(ClientID+"_PicHolder");
	var value = ob.options[singleSelected].value;
	var nickname = ob.options[singleSelected].text.toLowerCase();
	var valueAry = value.split("$");
	var pic = valueAry[1];
	if (pic == undefined || pic == "" || pic == "00000000-0000-0000-0000-000000000000")
		DbComboMultiUsrClearPicture(ClientID, UniqueID, nickname);
	else if (nickname.indexOf("@")==-1)
		PicHolder.innerHTML="<a href=\"/members/"+nickname+"\" target=\"_blank\"><img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\"></a>";
	else
		PicHolder.innerHTML="<img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\">";
	
}
function DbComboMultiUsrDisplayPicture(ClientID, UniqueID, Value, Text)
{
	var PicHolder = document.getElementById(ClientID+"_PicHolder");
	var value = Value;//DbComboGetValue(UniqueID+"$Combo");
	var nickname = Text.toLowerCase();
	var valueAry = value.split("$");
	var pic = valueAry[1];
	if (pic == undefined || pic == "" || pic == "00000000-0000-0000-0000-000000000000")
		DbComboMultiUsrClearPicture(ClientID, UniqueID, nickname);
	else if (nickname.indexOf("@")==-1)
		PicHolder.innerHTML="<a href=\"/members/"+nickname+"\" target=\"_blank\"><img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\"></a>";
	else
		PicHolder.innerHTML="<img src=\"" + StoragePath(valueAry[1]) + "\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\">";
	
}
function DbComboMultiUsrClearPicture(ClientID, UniqueID, NickName)
{
	var PicHolder = document.getElementById(ClientID+"_PicHolder");
	if (NickName.length>0)
	{
		if (NickName.indexOf("@")==-1)
			PicHolder.innerHTML="<a href=\"/members/"+NickName+"\" target=\"_blank\"><img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\"></a>";
		else
			PicHolder.innerHTML="<img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\" border=\"0\">";
	}
	else
		PicHolder.innerHTML="<img src=\"/gfx/dsi-sign-100.png\" width=\"100\" height=\"100\" style=\"border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;\">";
}
function DbComboMultiUsrRefreshHidden(ClientID, UniqueID, TextAry, ValueAry)
{
	var Texts = "";
	var Values = "";
	for (var i=0; i<ValueAry.length; i++)
	{
		Texts += (i>0?"&":"")+TextAry[i];
		Values += (i>0?"&":"")+ValueAry[i];
	}
	document.forms[0].elements[UniqueID+"$Values"].value = Values;
	document.forms[0].elements[UniqueID+"$Texts"].value = Texts;
	
}
function DbComboMultiUsrAddItem(ClientID, UniqueID)
{
	DbComboMultiUsrAddItemGeneric(ClientID, UniqueID, DbComboGetValue(UniqueID+"$Combo"), DbComboGetText(UniqueID+"$Combo"));
}
function DbComboMultiUsrAddItemGeneric(ClientID, UniqueID, Value, Text)
{
	var Values = document.forms[0].elements[UniqueID+"$Values"].value;
	var Texts = document.forms[0].elements[UniqueID+"$Texts"].value;
	var ValueAry = Values.split("&");
	var TextAry = Texts.split("&");
	
	var found = false;
	for (var i=0; i<ValueAry.length; i++)
	{
		if (unescape(ValueAry[i])==Value)
		{
			found = true;
			TextAry[i]=escape(Text);
			break;
		}
	}
	if (!found)
	{
		ValueAry.length++;
		ValueAry[ValueAry.length-1]=escape(Value);
		TextAry.length++;
		TextAry[TextAry.length-1]=escape(Text);
	}
	DbComboMultiUsrRefreshHidden(ClientID, UniqueID, TextAry, ValueAry);
	DbComboMultiUsrRefresh(ClientID, UniqueID);
}
function DbComboMultiUsrRemoveItem(ClientID, UniqueID)
{
	var Values = document.forms[0].elements[UniqueID+"$Values"].value;
	var Texts = document.forms[0].elements[UniqueID+"$Texts"].value;
	var ValueAry = Values.split("&");
	var TextAry = Texts.split("&");
	var removed = false;
	try
	{
		var ob = document.forms[0].elements[UniqueID+"$SelectBox"];
		var selected = new Array();
		for (var i = 0; i < ob.options.length; i++)
			if (ob.options[ i ].selected)
				selected.push(ob.options[i].value);
		
		for (var i=0; i<ValueAry.length; i++)
		{
			for (var j=0; j<selected.length; j++)
			{
				if (unescape(ValueAry[i])==selected[j])
				{
					ValueAry.splice(i,1);
					TextAry.splice(i,1);
					removed = true;
				}
			}
		}
		if (removed)
			DbComboMultiUsrClearPicture(ClientID, UniqueID, "");
		DbComboMultiUsrRefreshHidden(ClientID, UniqueID, TextAry, ValueAry);
		DbComboMultiUsrRefresh(ClientID, UniqueID);
	}
	catch(ex){}
}
function Debug(txt)
{
	var debugHolder = document.getElementById("debugHolder");
	debugHolder.innerHTML += txt+"<br>";
}
function DbComboMultiUsrInit(ClientID, UniqueID)
{
	DbComboMultiUsrRefresh(ClientID, UniqueID);
}


