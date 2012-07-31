
function DbComboMultiRefresh(ClientID, UniqueID)
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
function DbComboMultiRefreshHidden(ClientID, UniqueID, TextAry, ValueAry)
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
function DbComboMultiAddItem(ClientID, UniqueID)
{
	DbComboMultiAddItemGeneric(ClientID, UniqueID, DbComboGetValue(UniqueID+"$Combo"), DbComboGetText(UniqueID+"$Combo"));
//	DbComboClear(UniqueID+"$Combo");
}
function DbComboMultiAddItemGeneric(ClientID, UniqueID, Value, Text)
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
	DbComboMultiRefreshHidden(ClientID, UniqueID, TextAry, ValueAry);
	DbComboMultiRefresh(ClientID, UniqueID);
}
function DbComboMultiRemoveItem(ClientID, UniqueID)
{
	var Values = document.forms[0].elements[UniqueID+"$Values"].value;
	var Texts = document.forms[0].elements[UniqueID+"$Texts"].value;
	var ValueAry = Values.split("&");
	var TextAry = Texts.split("&");
	try
	{
		var ob = document.forms[0].elements[UniqueID+"$SelectBox"];
		var selected = new Array();
		for (var i = 0; i < ob.options.length; i++)
			if (ob.options[ i ].selected)
				selected.push(ob.options[i].value);
		
	//	var Value = document.forms[0].elements[UniqueID+"$SelectBox"][document.forms[0].elements[UniqueID+"$SelectBox"].selectedIndex].value;
	//	alert(Value);
		for (var i=0; i<ValueAry.length; i++)
		{
			for (var j=0; j<selected.length; j++)
			{
				if (unescape(ValueAry[i])==selected[j])
				{
					ValueAry.splice(i,1);
					TextAry.splice(i,1);
				}
			}
		}
		DbComboMultiRefreshHidden(ClientID, UniqueID, TextAry, ValueAry);
		DbComboMultiRefresh(ClientID, UniqueID);
	}
	catch(ex){}
}
function DbComboMultiInit(ClientID, UniqueID)
{
	DbComboMultiRefresh(ClientID, UniqueID);
}
