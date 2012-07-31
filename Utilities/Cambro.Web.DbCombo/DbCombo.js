/*
	This code is copyright Cambro Limited 2002-2003.
	Please contact David Brophy - d.brophy@cambro.net for licensing details.
*/
var DbComboServerExists = true;
function DbComboStringToEval(
		c,
		debug,
		setLanguage,
		selectSingleItemOnEnter, 
		selectSingleItemOnTab, 
		tabToNextFieldOnEnter,
		autoPostBack,
		reQueryDisabled,
		clearQueryOnUpLevelSearchButton,
		closeResultsOnBlur,
		closeResultsOnClick,
		closeResultsOnEnter,
		closeResultsOnTab,
		invertArrow,
		hideIntersectingSelectTags,
		hideAllSelectTags,
		showDbComboLink,
		assemblyName,
		typeName,
		methodName,
		dataMember,
		dataValueField,
		dataTextField,
		errorBoxType,
		errorBoxCustomText,
		parentAssembly,
		parentType,
		parentBaseAssembly,
		parentBaseType,
		pageAssembly,
		pageType,
		pageBaseAssembly,
		pageBaseType,
		tweakX,
		tweakY,
		textLoading,
		textNoResults,
		clientStateFunction,
		clientOnSelectFunction,
		initialResultCount,
		latency,
		supportFilesDir,
		serverState,
		serverStateHash){
	var _c=c.replace(/\$/g,"_");
	var string="";
	string+="var "+_c+"_xmlHttp = new ActiveXObject(\"Microsoft.XMLHTTP\");";
	string+="var "+_c+"_xmlDoc = new ActiveXObject(\"Microsoft.XMLDOM\");";
	string+="var "+_c+"_hiddenSelect = new Array();";
	string+="var "+_c+"_skipRecords=0;";
	string+="var "+_c+"_keyPressNumber=0;";
	string+="var "+_c+"_initialNumRows="+initialResultCount+";";
	string+="var "+_c+"_clientStateFunction=\""+clientStateFunction+"\";";
	string+="var "+_c+"_clientOnSelectFunction=\""+clientOnSelectFunction+"\";";
	string+="var "+_c+"_numRows="+_c+"_initialNumRows;";
	string+="var "+_c+"_lastQuery=\"\";";
	string+="var "+_c+"_lastState=new Object();";
	string+="var "+_c+"_tmpSelectedIndex=-1;";
	string+="var "+_c+"_debug="+debug+";";
	string+="var "+_c+"_setLanguage="+setLanguage+";";
	string+="var "+_c+"_autoPostBack="+autoPostBack+";";
	string+="var "+_c+"_reQueryDisabled="+reQueryDisabled+";";
	string+="var "+_c+"_clearQueryOnUpLevelSearchButton="+clearQueryOnUpLevelSearchButton+";";
	string+="var "+_c+"_closeResultsOnBlur="+closeResultsOnBlur+";";
	string+="var "+_c+"_closeResultsOnClick="+closeResultsOnClick+";";
	string+="var "+_c+"_closeResultsOnEnter="+closeResultsOnEnter+";";
	string+="var "+_c+"_closeResultsOnTab="+closeResultsOnTab+";";
	string+="var "+_c+"_invertArrow="+invertArrow+";";
	string+="var "+_c+"_hideIntersectingSelectTags="+hideIntersectingSelectTags+";";
	string+="var "+_c+"_hideAllSelectTags="+hideAllSelectTags+";";
	string+="var "+_c+"_showDbComboLink="+showDbComboLink+";";
	string+="var "+_c+"_latency="+latency+";";
	string+="var "+_c+"_hideLookupVar=true;";
	string+="var "+_c+"_movedResultSpanUp=false;";
	string+="var "+_c+"_requestInProgress=false;";
	string+="var "+_c+"_assemblyName=\""+assemblyName+"\";";
	string+="var "+_c+"_typeName=\""+typeName+"\";";
	string+="var "+_c+"_methodName=\""+methodName+"\";";
	string+="var "+_c+"_dataMember=\""+dataMember+"\";";
	string+="var "+_c+"_dataValueField=\""+dataValueField+"\";";
	string+="var "+_c+"_dataTextField=\""+dataTextField+"\";";
	string+="var "+_c+"_errorBoxType=\""+errorBoxType+"\";";
	string+="var "+_c+"_errorBoxCustomText=unescape(\""+errorBoxCustomText+"\");";
	string+="var "+_c+"_selectSingleItemOnEnter="+selectSingleItemOnEnter+";";
	string+="var "+_c+"_selectSingleItemOnTab="+selectSingleItemOnTab+";";
	string+="var "+_c+"_tabToNextFieldOnEnter="+tabToNextFieldOnEnter+";";
	string+="var "+_c+"_abortLookup=false;";
	
	string+="var "+_c+"_parentAssembly=\""+parentAssembly+"\";";
	string+="var "+_c+"_parentType=\""+parentType+"\";";
	string+="var "+_c+"_parentBaseAssembly=\""+parentBaseAssembly+"\";";
	string+="var "+_c+"_parentBaseType=\""+parentBaseType+"\";";
	
	string+="var "+_c+"_pageAssembly=\""+pageAssembly+"\";";
	string+="var "+_c+"_pageType=\""+pageType+"\";";
	string+="var "+_c+"_pageBaseAssembly=\""+pageBaseAssembly+"\";";
	string+="var "+_c+"_pageBaseType=\""+pageBaseType+"\";";
	
	string+="var "+_c+"_tweakX="+tweakX+";";
	string+="var "+_c+"_tweakY="+tweakY+";";
	
	string+="var "+_c+"_searchAvailable=false;";
	
	string+="var "+_c+"_textLoading=unescape(\""+textLoading+"\");";
	string+="var "+_c+"_textNoResults=unescape(\""+textNoResults+"\");";
	string+="var "+_c+"_supportFilesDir=\""+supportFilesDir+"\";";
	
	string+="var "+_c+"_serverState=\""+serverState+"\";";
	string+="var "+_c+"_serverStateHash=\""+serverStateHash+"\";";
	
	string+="function "+_c+"_DbComboLoadDataStub(){ var c=\""+c+"\"; DbComboLoadData(c,false,true,false); }";
	string+="function "+_c+"_DbComboLoadDataStubReQuery(){ var c=\""+c+"\"; DbComboLoadData(c,false,false,true); }";
	string+="function "+_c+"_DbComboLoadDataStubClear(){ var c=\""+c+"\"; DbComboLoadData(c,true,true,false); }";
	string+="function "+_c+"_DbComboLoadDataStubClearNoFocus(){ var c=\""+c+"\"; DbComboLoadData(c,true,false,false); }";
	return string;
}
function DbComboChangedSelection(c, selectionType){
	print2(c,"DbComboChangedSelection("+selectionType+")");
	var _c=c.replace(/\$/g,"_");
	var dropDown = document.all[_c+"_ulbDropDown"];
	if (eval(_c+"_clientOnSelectFunction") != "")
	{
		try
		{
			var text = dropDown[dropDown.selectedIndex].text;
			var value = dropDown[dropDown.selectedIndex].value;
			eval(eval(_c+"_clientOnSelectFunction")+"(value, text, "+selectionType+");");
		}
		catch(er)
		{
			
		}
	}
	
	if (eval(_c+"_autoPostBack"))
	{
		if( selectionType==2 || selectionType==3 )
		{
			__doPostBack(c,"autoPostBack");
		}
	}
	
	if (typeof(Page_Validators) != "undefined")
	{
		if ( typeof(document.all[_c+"_ulbTextBox"].Validators) == "object" )
		{
			var vals = document.all[_c+"_ulbTextBox"].Validators;
			var i;
			for (i = 0; i < vals.length; i++) {
				ValidatorValidate(vals[i]);
			}
			ValidatorUpdateIsValid();  
		}
		
		if ( typeof(document.all[_c+"_ulbValueHidden"].Validators) == "object" )
		{
			var vals = document.all[_c+"_ulbValueHidden"].Validators;
			var i;
			for (i = 0; i < vals.length; i++) {
				ValidatorValidate(vals[i]);
			}
			ValidatorUpdateIsValid();  
		}

	}
	
}
function DbComboClearedSelection(c, selectionType){
	print2(c,"DbComboClearedSelection");
	var _c=c.replace(/\$/g,"_");
	var textBox = document.all[_c+"_ulbTextBox"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	if (eval(_c+"_clientOnSelectFunction") != "")
	{
		try
		{
			var text = textBox.value;
			eval(eval(_c+"_clientOnSelectFunction")+"(\"\", text, "+selectionType+");");
		}
		catch(er)
		{
			
		}
	}
}
function DbComboLoad(c){
	print2(c,"DbComboLoad");
	var _c=c.replace(/\$/g,"_");
	
	DbComboResizeResults(c,false);
	
}
function DbComboInit(c){
	print2(c,"DbComboInit");
	var _c=c.replace(/\$/g,"_");
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];
	var textBox = document.all[_c+"_ulbTextBox"];
	var dropDown = document.all[_c+"_ulbDropDown"];
	
	//Preload the up-arrow image.
	preloadImg1 = new Image(9,6); 
	preloadImg1.src = eval(_c+"_supportFilesDir")+"DbComboServer.aspx?UpArrow";
	//Preload the down-arrow image.
	preloadImg2 = new Image(9,6); 
	preloadImg2.src = eval(_c+"_supportFilesDir")+"DbComboServer.aspx?DownArrow";
	//Preload the DbCombo logo image.
	preloadImg3 = new Image(94,17); 
	preloadImg3.src = eval(_c+"_supportFilesDir")+"DbComboServer.aspx?f";
	
	DbComboHideResults(c);
	dropDown.style.display="none";
	document.all[_c+"_ulbStatusDisplaySpan"].style.display="none";
	DbComboHideMoreResultsButton(c);
	if (eval(_c+"_clientStateFunction") != ""){
		try
		{
			eval(_c+"_lastState = "+eval(_c+"_clientStateFunction")+";");
		}
		catch(er){}
	}
	
	eval("var DbComboResizeTmp"+_c+" = window.onresize; function DbComboResize"+_c+"(){if(DbComboResizeTmp"+_c+"){DbComboResizeTmp"+_c+"();}DbComboPositionResults(\""+c+"\");}window.onresize=DbComboResize"+_c+";");
	
	document.all[_c+"_ulbSpacer"].style.display="none";
	
	
	if ( document.all[_c+"_ulbReQueryOnLoad"].value == "true" &! eval(_c+"_reQueryDisabled") )
	{
		eval(_c+"_skipRecords = 0");
		eval(_c+"_numRows = parseInt(document.all[_c+\"_ulbReQueryRecordsHidden\"].value);");
		eval(_c+"_lastQuery = document.all[_c+\"_ulbQueryHidden\"].value;");
		
		DbComboShowResults(c,false);
		DbComboSendRequest(c,false,false,true);
	}
}

function DbComboClear(c){
	print2(c,"DbComboClear");
	var _c=c.replace(/\$/g,"_");
	DbComboHideResults(c);
	document.all[_c+"_ulbDropDown"].style.display="none";
	document.all[_c+"_ulbStatusDisplaySpan"].style.display="none";
	DbComboHideMoreResultsButton(c);
	document.all[_c+"_ulbSpacer"].style.height = 1;
	document.all[_c+"_ulbQueryHidden"].value = "";
	document.all[_c+"_ulbValueHidden"].value = "";
	DbComboClearedSelection(c);
	document.all[_c+"_ulbTextBox"].value = "";
	
	DbComboClearSelect(c);
	
	eval(_c+"_lastQuery=\"\"");
	eval(_c+"_numRows="+_c+"_initialNumRows;");
	eval(_c+"_skipRecords=0;");
	eval(_c+"_keyPressNumber=0;");
}
function DbComboHiddenSearchAvailable(c){
	print2(c,"DbComboHiddenSearchAvailable");
	var _c=c.replace(/\$/g,"_");
	if (eval(_c+"_searchAvailable") && document.all[_c+"_ulbResultsSpan"].style.display=="none")
	{
		return true;
	}
	else
	{
		return false
	}
}
function DbComboSendRequestButton(c){
	print2(c,"DbComboSendRequestButton");
	var _c=c.replace(/\$/g,"_");
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];
	var queryHidden = document.all[_c+"_ulbQueryHidden"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	var textBox = document.all[_c+"_ulbTextBox"];
	var dropDown = document.all[_c+"_ulbDropDown"];
	
	if (DbComboHiddenSearchAvailable(c))
	{//If we have already done a search, and hidden the results, redisplay them
		DbComboShowResults(c,true);
		DbComboSetCaretToEnd(c);
	}
	else if (document.all[_c+"_ulbResultsSpan"].style.display=="")
	{
		eval(_c+"_hideLookupVar = true");
		eval(_c+"_abortLookup = true");
		DbComboHideResults(c);
		document.all[_c+"_ulbSearchButton"].blur();
	}
	else
	{//If not, then do a search.
		if (eval(_c+"_clearQueryOnUpLevelSearchButton"))
		{
			//We may want to clear the search query first.
			dropDown.selectedIndex = -1;
			textBox.value="";
			valueHidden.value = "";
			DbComboClearedSelection(c,6);
		}
		DbComboShowResults(c,false);
		queryHidden.value=textBox.value;
		eval(_c+"_lastQuery=queryHidden.value;");
		eval(_c+"_numRows="+_c+"_initialNumRows;");
		eval(_c+"_skipRecords=0;");
		eval(_c+"_keyPressNumber=0;");
		DbComboSendRequest(c,true,true,false);
		DbComboSetCaretToEnd(c); 
	}
}
function DbComboTextBoxKeyUp(c){
	print2(c,"DbComboTextBoxKeyUp "+event.keyCode);
	var _c=c.replace(/\$/g,"_");
	var dropDown = document.all[_c+"_ulbDropDown"];
	var textBox = document.all[_c+"_ulbTextBox"];
	var queryHidden = document.all[_c+"_ulbQueryHidden"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];
	
	if (
		( event.ctrlKey &! ( // Don't run query on all ctrl keys
		    event.keyCode == 86 || event.keyCode == 88 // except V & X
		))
		|| event.keyCode == 40 //down arrow
		|| event.keyCode == 38 //up arrow
		|| event.keyCode == 37 //left arrow
		|| event.keyCode == 39 //right arrow
		|| event.keyCode == 34 //page down
		|| event.keyCode == 33 //page up
		|| event.keyCode == 27 //esc
		|| event.keyCode == 16 //shift
		|| event.keyCode == 17 //control
		|| event.keyCode == 35 //end
		|| event.keyCode == 36 //home
		|| event.keyCode == 36 //insert
		|| event.keyCode == 20 //capslock?
		|| event.keyCode == 18 //alt
		|| event.keyCode == 145//scroll lock?
		|| event.keyCode == 19 //pause?
		|| event.keyCode == 144//num lock?
		|| event.keyCode == 13 //enter
		|| event.keyCode == 116 //F5
		)
	{//Don't run query - navigating.
	}
	else if (event.keyCode == 9){} //tab
	else
	{//Run query.
		valueHidden.value="";
		DbComboClearedSelection(c,4);
		queryHidden.value=textBox.value;
		eval(_c+"_keyPressNumber="+_c+"_keyPressNumber+1;");
		
		if (eval(_c+"_lastQuery")!=document.all[_c+"_ulbQueryHidden"].value){
			eval(_c+"_searchAvailable=false;"); //???
			var keypressnumber=eval(_c+"_keyPressNumber");
			if (eval(_c+"_latency") != -1)
			{
				eval(_c+"_abortLookup = false");
				setTimeout("DbComboSendRequestIfIdle(\""+c+"\","+keypressnumber+")", eval(_c+"_latency") );
			}
		}
		else
		{
			DbComboShowResults(c,false);
		}
	}
}
function DbComboTextBoxKeyDown(c){
	print2(c,"DbComboTextBoxKeyDown "+event.keyCode);
	var _c=c.replace(/\$/g,"_");
	var dropDown = document.all[_c+"_ulbDropDown"];
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];
	var moreResultsButton = document.all[_c+"_ulbMoreResultsButton"];
	var textBox = document.all[_c+"_ulbTextBox"];
	var queryHidden = document.all[_c+"_ulbQueryHidden"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	
	if ( (event.keyCode==40 || event.keyCode==34) &! eval(_c+"_requestInProgress") && eval(_c+"_keyPressNumber")==0 ){//Down arrow or page down
		// With the "&! eval(_c+"_requestInProgress") && eval(_c+"_keyPressNumber")==0" above, 
		// we are trying to disable scrolling through the results during latency or request
		var moveRows = 1;
		if (event.keyCode==34)//page down
			moveRows = eval(_c+"_initialNumRows");
		if (resultsSpan.style.display==""){
			if (dropDown.selectedIndex==-1)
			{
				dropDown.selectedIndex=0;
			}
			else
			{
				var tmpSelectedIndex = dropDown.selectedIndex;
				dropDown.selectedIndex += moveRows;
				if (dropDown.selectedIndex==-1)
				{
					//scrolled off the bottom of the list...
					if (moreResultsButton.style.display == "" || eval(_c+"_requestInProgress"))//more results are available, or downloading data...
					{
						if (tmpSelectedIndex == dropDown.length-1 &! eval(_c+"_requestInProgress")) // get more rows if last cursor position was last row, but not if during request
							DbComboGetMoreResults(c);
						dropDown.selectedIndex = dropDown.length-1;
					}
					else
					{
						if (tmpSelectedIndex == dropDown.length-1)
						{
							textBox.value=queryHidden.value;
							valueHidden.value = "";
							DbComboClearedSelection(c,5);
						}
						else
						{
							dropDown.selectedIndex = dropDown.length-1;
						}
					}
				}
			}
			if (dropDown.selectedIndex != -1){
				textBox.value = dropDown[dropDown.selectedIndex].text;
				valueHidden.value = dropDown[dropDown.selectedIndex].value;
				DbComboChangedSelection(c,1);
			}
			DbComboSetCaretToEnd(c);
			event.returnValue=false;
		}
		else
		{
			if (DbComboHiddenSearchAvailable(c))
			{//If we have already done a search, and hidden the results, redisplay them
				DbComboShowResults(c,true);
			}
			else
			{//If not, then do a search.
				queryHidden.value=textBox.value;
				eval(_c+"_keyPressNumber="+_c+"_keyPressNumber+1;");
				if (eval(_c+"_lastQuery")!=queryHidden.value || queryHidden.value==""){
					var keypressnumber=eval(_c+"_keyPressNumber");
					eval(_c+"_abortLookup = false");
					DbComboSendRequestIfIdle(c,keypressnumber);
				}
			}
			
		}
	}
	else if ( (event.keyCode==38 || event.keyCode==33) &! eval(_c+"_requestInProgress") && eval(_c+"_keyPressNumber")==0 ){//Up arrow or page up
		// With the "&! eval(_c+"_requestInProgress") && eval(_c+"_keyPressNumber")==0" above, 
		// we are trying to disable scrolling through the results during latency or request
		var moveRows = 1;
		if (event.keyCode==33)//page up
			moveRows = eval(_c+"_initialNumRows");
		if (resultsSpan.style.display==""){
			if (dropDown.selectedIndex==-1)
				dropDown.selectedIndex=dropDown.length-1; 
			else
			{
				var tmpSelectedIndex = dropDown.selectedIndex;
				dropDown.selectedIndex -= moveRows;
				if (dropDown.selectedIndex==-1)
				{
					if (tmpSelectedIndex!=0)
						dropDown.selectedIndex=0;
					else
					{
						textBox.value=queryHidden.value;
						valueHidden.value="";
						DbComboClearedSelection(c,5);
					}
				}
			}
		}
		if (dropDown.selectedIndex != -1)
		{
			textBox.value = dropDown[dropDown.selectedIndex].text;
			valueHidden.value = dropDown[dropDown.selectedIndex].value;
			DbComboChangedSelection(c,1);
		}
		DbComboSetCaretToEnd(c);
		event.returnValue=false;
	}
	else if (event.keyCode == 27)//Escape
	{//Reset to stored text.
		dropDown.selectedIndex = -1;
		textBox.value=queryHidden.value;
		valueHidden.value = "";
		DbComboClearedSelection(c,6);
		DbComboHideResults(c);
		DbComboSelectAll(c);
		event.returnValue=false;
	}
	else if (event.keyCode == 13)//Enter
	{
		var selectSingleItemOnEnter=true;
		if (dropDown.selectedIndex!=-1)
		{
			textBox.value = dropDown[dropDown.selectedIndex].text;
			valueHidden.value = dropDown[dropDown.selectedIndex].value;
			DbComboChangedSelection(c,2);
			if (eval(_c+"_closeResultsOnEnter"))
				DbComboHideResults(c);
		}
		else if (dropDown.length==1 && eval(_c+"_selectSingleItemOnEnter"))
		{
			dropDown.selectedIndex = 0;
			textBox.value = dropDown[0].text;
			valueHidden.value = dropDown[0].value;
			DbComboChangedSelection(c,2);
			if (eval(_c+"_closeResultsOnEnter"))
				DbComboHideResults(c);
		}
		if (eval(_c+"_tabToNextFieldOnEnter"))
			event.keyCode = 9;
		else
			event.returnValue=false;
	}
	else if (event.keyCode == 9)//Tab
	{
		if (dropDown.selectedIndex!=-1)
		{
			if (!eval(_c+"_requestInProgress") && eval(_c+"_keyPressNumber")==0 )
			{
				textBox.value = dropDown[dropDown.selectedIndex].text;
				valueHidden.value = dropDown[dropDown.selectedIndex].value;
				DbComboChangedSelection(c,2);
				if (eval(_c+"_closeResultsOnEnter"))
					DbComboHideResults(c);
			}
		}
		else if (dropDown.length==1 && eval(_c+"_selectSingleItemOnTab"))
		{
			dropDown.selectedIndex = 0;
			textBox.value = dropDown[0].text;
			valueHidden.value = dropDown[0].value;
			DbComboChangedSelection(c,2);
			if (eval(_c+"_closeResultsOnEnter"))
				DbComboHideResults(c);
		}
	}
}
function DbComboSendRequestIfIdle(c,keyPressNumber){
	print2(c,"DbComboSendRequestIfIdle");
	var _c=c.replace(/\$/g,"_");
	if ( eval(_c+"_keyPressNumber") == keyPressNumber &! eval(_c+"_abortLookup") ){
		DbComboShowResults(c,false);
		eval(_c+"_lastQuery=document.all[\""+_c+"_ulbQueryHidden\"].value;");
		eval(_c+"_numRows="+_c+"_initialNumRows;");
		eval(_c+"_skipRecords=0;");
		eval(_c+"_keyPressNumber=0;");
		DbComboSendRequest(c,true,true,false);
	}
	if (eval(_c+"_abortLookup"))
	{
		eval(_c+"_keyPressNumber=0"); 
	}
}
function DbComboClick(c){
	print2(c,"DbComboClick");
	var _c=c.replace(/\$/g,"_");
	var dropDown = document.all[_c+"_ulbDropDown"];
	var textBox = document.all[_c+"_ulbTextBox"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];

	textBox.focus();
	if (dropDown.selectedIndex != -1)
	{
		textBox.value = dropDown[dropDown.selectedIndex].text;
		valueHidden.value = dropDown[dropDown.selectedIndex].value;
		DbComboChangedSelection(c,3);
		if (eval(_c+"_closeResultsOnClick"))
			DbComboHideResults(c);
	}
}
function DbComboTextBoxFocus(c){
	print2(c,"DbComboTextBoxFocus");
	var _c=c.replace(/\$/g,"_");
		
	DbComboGenericFocus(c);
	DbComboStateUnChanged(c);
	//If we have already done a search, and hidden the results, redisplay them
	if (DbComboHiddenSearchAvailable(c))
		DbComboShowResults(c,true);
}
function DbComboGenericFocus(c){
	print2(c,"DbComboGenericFocus");
	var _c=c.replace(/\$/g,"_");
	eval(_c+"_hideLookupVar = false");
	eval(_c+"_abortLookup = false");
}
function DbComboGenericBlur(c){
	print2(c,"DbComboGenericBlur");
	
	var _c=c.replace(/\$/g,"_");
	eval(_c+"_hideLookupVar = true");
	eval(_c+"_abortLookup = true");
	if(eval(_c+"_closeResultsOnBlur"))
		setTimeout("DbComboHideLookup(\""+c+"\")", 20 );
}
function DbComboGenericBlurTextBox(c){
	print2(c,"DbComboGenericBlurTextBox");
	
	var _c=c.replace(/\$/g,"_");
	eval(_c+"_hideLookupVar = true");
	eval(_c+"_abortLookup = true");
	if(eval(_c+"_closeResultsOnBlur"))
		setTimeout("DbComboHideLookup(\""+c+"\")", 20 );
}
function DbComboFocus(c){
	print2(c,"DbComboFocus");
	var _c=c.replace(/\$/g,"_");
	
	var dropDown = document.all[_c+"_ulbDropDown"];
	var textBox = document.all[_c+"_ulbTextBox"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	
	DbComboGenericFocus(c);

	textBox.focus();
}

function DbComboHideLookup(c){
	print2(c,"DbComboHideLookup");
	var _c=c.replace(/\$/g,"_");
	if (eval(_c+"_hideLookupVar") && eval(_c+"_closeResultsOnBlur")){
		DbComboHideResults(c);//resultsSpan.style.display="none";
	}
}

function DbComboStateUnChanged(c){
	print2(c,"DbComboStateUnChanged");
	return DbComboStateUnChangedGeneric(c,true);
}
function DbComboStateChanged(c){
	print2(c,"DbComboStateChanged");
	return DbComboStateUnChangedGeneric(c,false);
}
function DbComboStateUnChangedGeneric(c,setFocus){
	print2(c,"DbComboStateUnChangedGeneric");
	var _c=c.replace(/\$/g,"_");
	var stateChanged = false;
	//Is the current client state the same as it was on the last query?
	if (eval(_c+"_clientStateFunction") != ""){
		var state = new Object();
		var lastState = new Object();
		try{
			eval("state = "+eval(_c+"_clientStateFunction")+";");
			lastState = eval(_c+"_lastState");
		}
		catch(er){
			return true;
		}
		try{
			for (var k in state)
				if ( state[k] != lastState[k] )
					stateChanged = true;
			for (var k in lastState)
				if ( state[k] != lastState[k] )
					stateChanged = true;
		}
		catch(er){
			stateChanged = true;
		}
		if (!stateChanged)
			return true;
	}
	else
		return true;

	if (stateChanged)
	{
		if (DbComboHiddenSearchAvailable(c) || document.all[_c+"_ulbResultsSpan"].style.display=="" )
		{
		
			if (document.all[_c+"_ulbTextBox"].value != document.all[_c+"_ulbQueryHidden"].value)
			{
				document.all[_c+"_ulbTextBox"].value=document.all[_c+"_ulbQueryHidden"].value;
				if (setFocus)
					DbComboSetCaretToEnd(c);
			}
			document.all[_c+"_ulbValueHidden"].value = "";
			DbComboClearedSelection(c,7);
			
			eval(_c+"_numRows="+_c+"_initialNumRows;");
			eval(_c+"_skipRecords=0;");
			eval(_c+"_keyPressNumber=0;");
			DbComboHideMoreResultsButton(c);
			eval(_c+"_abortLookup = false;");
			DbComboSendRequest(c,true,setFocus,false);
		}
		return false;
	}
}
function DbComboHideSelectTags(c){
	print2(c,"DbComboHideSelectTags");	
	var _c=c.replace(/\$/g,"_");
	
	var hideIntersectingSelectTags = eval(_c+"_hideIntersectingSelectTags");
	var hideAllSelectTags = eval(_c+"_hideAllSelectTags");
	if (hideAllSelectTags)
	{
		var hiddenSelect = eval(_c+"_hiddenSelect");
		
		var e = document.all.tags("SELECT");
		for (var i=0; i<e.length; i++)
		{
			if (e[i] != document.all[_c+"_ulbDropDown"])
			{
				var found_one=false;
				for (var j=0; j<hiddenSelect.length; j=j+2)
				{
					if (hiddenSelect[j]==e[i])
						found_one=true;
				}
				if (!found_one)
				{
					hiddenSelect.push(e[i]);
					hiddenSelect.push(e[i].style.visibility);
				}
				e[i].style.visibility="hidden";
			}
		}
	}
	else if (hideIntersectingSelectTags)
	{
		var hiddenSelect = eval(_c+"_hiddenSelect");
		
		var e = document.all.tags("SELECT");
		for (var i=0; i<e.length; i++)
		{
			if (e[i] != document.all[_c+"_ulbDropDown"])
			{
				if (DbComboIntersectionTest(c,e[i]))
				{
				
					var found_one=false;
					for (var j=0; j<hiddenSelect.length; j=j+2)
					{
						if (hiddenSelect[j]==e[i])
							found_one=true;
					}
					if (!found_one)
					{
						hiddenSelect.push(e[i]);
						hiddenSelect.push(e[i].style.visibility);
					}
					
					e[i].style.visibility="hidden";
				}
			}
		}
	}
}
function DbComboIntersectionTest(c,selectTag){
	print2(c,"DbComboIntersectionTest");
	var _c=c.replace(/\$/g,"_");	
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];
	var dropDown = document.all[_c+"_ulbDropDown"];

	var comboTop = DbComboGetDimPage(resultsSpan).y ;
	var comboBot = DbComboGetDimPage(resultsSpan).y + resultsSpan.clientHeight + parseInt(DbComboStripNonNumbers(resultsSpan.currentStyle.borderBottomWidth)) + resultsSpan.clientTop;
	var comboLeft = DbComboGetDimPage(resultsSpan).x ;
	var comboRight = DbComboGetDimPage(resultsSpan).x + resultsSpan.clientWidth + parseInt(DbComboStripNonNumbers(resultsSpan.currentStyle.borderRightWidth)) + resultsSpan.clientLeft;
	
	var selectTop = DbComboGetDimPage(selectTag).y ;
	var selectBot = DbComboGetDimPage(selectTag).y + selectTag.clientHeight + parseInt(DbComboStripNonNumbers(selectTag.currentStyle.borderBottomWidth)) + selectTag.clientTop;
	var selectLeft = DbComboGetDimPage(selectTag).x ;
	var selectRight = DbComboGetDimPage(selectTag).x + selectTag.clientWidth + parseInt(DbComboStripNonNumbers(selectTag.currentStyle.borderRightWidth)) + selectTag.clientLeft;
	
	if (comboTop < selectBot && comboBot > selectTop && comboLeft < selectRight && comboRight > selectLeft)
		return true;
	else
		return false;
}
function DbComboHideResults(c){
	print2(c,"DbComboHideResults ***");
	var _c=c.replace(/\$/g,"_");
	if ( typeof(document.all[_c+"_ulbSearchButton"]) != "undefined" && eval(_c+"_invertArrow") )
		document.all[_c+"_ulbSearchButton"].innerHTML = document.all[_c+"_ulbSearchButton"].innerHTML.replace(/\?UpArrow/g,"?DownArrow");
		
	document.all[_c+"_ulbResultsSpan"].style.display="none";
	var hiddenSelect = eval(_c+"_hiddenSelect");
	var e = document.all;
	for (var i=0; i<hiddenSelect.length; i=i+2)
	{
		hiddenSelect[i].style.visibility = hiddenSelect[i+1];
	}
	eval(_c+"_hiddenSelect = new Array();");
}
function DbComboShowResults(c, resize){
	print2(c,"DbComboShowResults");
	var _c=c.replace(/\$/g,"_");
	if ( typeof(document.all[_c+"_ulbSearchButton"]) != "undefined" && eval(_c+"_invertArrow") )
		document.all[_c+"_ulbSearchButton"].innerHTML = document.all[_c+"_ulbSearchButton"].innerHTML.replace(/\?DownArrow/g,"?UpArrow");
	
	if (document.all[_c+"_ulbResultsSpan"].style.display=="none")
	{
		document.all[_c+"_ulbResultsSpan"].style.display="";
		if (resize)
			DbComboResizeResults(c,false);
		DbComboHideSelectTags(c);
	}
}
function DbComboHideMoreResultsButton(c){
	print2(c,"DbComboHideMoreResultsButton");
	var _c=c.replace(/\$/g,"_");
	document.all[_c+"_ulbMoreResultsButton"].style.display="none";
	if (!eval(_c+"_showDbComboLink"))
		document.all[_c+"_ulbStatusBar"].style.display="none";
}
function DbComboShowMoreResultsButton(c){
	print2(c,"DbComboShowMoreResultsButton");
	var _c=c.replace(/\$/g,"_");
	if (!eval(_c+"_showDbComboLink"))
		document.all[_c+"_ulbStatusBar"].style.display="";
	document.all[_c+"_ulbMoreResultsButton"].style.display="";
}
function DbComboSendRequest(c,clearlist,setFocus,reQuery){
	print2(c,"DbComboSendRequest");
	var _c=c.replace(/\$/g,"_");
	try{
		if (reQuery)
		{
			DbComboHideResults(c);
		}
		
		eval(_c+"_requestInProgress=true;");
		DbComboHideMoreResultsButton(c);
		
		var xmlRequest = new ActiveXObject("Microsoft.XMLDOM");
		xmlRequest.appendChild(eval(_c+"_xmlDoc").createElement("doc"));
		var docTag = xmlRequest.getElementsByTagName("doc").item(0);
		
		var rowsTag= xmlRequest.createElement("rows");
		rowsTag.text=eval(_c+"_numRows");
		docTag.appendChild(rowsTag);
		
		var debugTag= xmlRequest.createElement("debug");
		debugTag.text=eval(_c+"_debug");
		docTag.appendChild(debugTag);
		
		var assemblyTag= xmlRequest.createElement("serverAssembly");
		assemblyTag.text=eval(_c+"_assemblyName");
		docTag.appendChild(assemblyTag);

		var stateTag= xmlRequest.createElement("clientState");
		if (eval(_c+"_clientStateFunction") != ""){
			var state = new Object();
			try
			{
				eval("state = "+eval(_c+"_clientStateFunction")+";");
				eval(_c+"_lastState = "+eval(_c+"_clientStateFunction")+";");
			}
			catch(er)
			{
				var stateItemTag = xmlRequest.createElement("stateItem");
				var stateKeyTag = xmlRequest.createElement("stateKey");
				stateKeyTag.text="clientStateFunctionError";
				var stateDataTag = xmlRequest.createElement("stateData");
				stateDataTag.text=er.description;
				stateItemTag.appendChild(stateKeyTag);
				stateItemTag.appendChild(stateDataTag);
				stateTag.appendChild(stateItemTag);
			}
			for (var k in state)
			{
				var stateItemTag = xmlRequest.createElement("stateItem");
				var stateKeyTag = xmlRequest.createElement("stateKey");
				stateKeyTag.text=k;
				var stateDataTag = xmlRequest.createElement("stateData");
				stateDataTag.text=state[k];
				stateItemTag.appendChild(stateKeyTag);
				stateItemTag.appendChild(stateDataTag);
				stateTag.appendChild(stateItemTag);
			}
		}
		docTag.appendChild(stateTag);
		
		var typeTag= xmlRequest.createElement("serverType");
		typeTag.text=eval(_c+"_typeName");
		docTag.appendChild(typeTag);
		
		var serverStateTag= xmlRequest.createElement("serverState");
		serverStateTag.text=eval(_c+"_serverState");
		docTag.appendChild(serverStateTag);
		var serverStateHashTag= xmlRequest.createElement("serverStateHash");
		serverStateHashTag.text=eval(_c+"_serverStateHash");
		docTag.appendChild(serverStateHashTag);
		
		var methodTag= xmlRequest.createElement("serverMethod");
		methodTag.text=eval(_c+"_methodName");
		docTag.appendChild(methodTag);
		
		var dataMemberTag= xmlRequest.createElement("dataMember");
		dataMemberTag.text=eval(_c+"_dataMember");
		docTag.appendChild(dataMemberTag);
		
		var dataValueFieldTag = xmlRequest.createElement("dataValueField");
		dataValueFieldTag.text=eval(_c+"_dataValueField");
		docTag.appendChild(dataValueFieldTag);
		var dataTextFieldTag= xmlRequest.createElement("dataTextField");
		dataTextFieldTag.text=eval(_c+"_dataTextField");
		docTag.appendChild(dataTextFieldTag);
		
		var parentAssembly = xmlRequest.createElement("parentAssembly");
		parentAssembly.text=eval(_c+"_parentAssembly");
		docTag.appendChild(parentAssembly);
		var parentType= xmlRequest.createElement("parentType");
		parentType.text=eval(_c+"_parentType");
		docTag.appendChild(parentType);
		var parentBaseAssembly= xmlRequest.createElement("parentBaseAssembly");
		parentBaseAssembly.text=eval(_c+"_parentBaseAssembly");
		docTag.appendChild(parentBaseAssembly);
		var parentBaseType= xmlRequest.createElement("parentBaseType");
		parentBaseType.text=eval(_c+"_parentBaseType");
		docTag.appendChild(parentBaseType);
		
		var pageAssembly = xmlRequest.createElement("pageAssembly");
		pageAssembly.text=eval(_c+"_pageAssembly");
		docTag.appendChild(pageAssembly);
		var pageType= xmlRequest.createElement("pageType");
		pageType.text=eval(_c+"_pageType");
		docTag.appendChild(pageType);
		var pageBaseAssembly= xmlRequest.createElement("pageBaseAssembly");
		pageBaseAssembly.text=eval(_c+"_pageBaseAssembly");
		docTag.appendChild(pageBaseAssembly);
		var pageBaseType= xmlRequest.createElement("pageBaseType");
		pageBaseType.text=eval(_c+"_pageBaseType");
		docTag.appendChild(pageBaseType);
		
		var skipTag = xmlRequest.createElement("skip");
		skipTag.text=eval(_c+"_skipRecords");
		docTag.appendChild(skipTag);
		
		var queryTag = xmlRequest.createElement("query");
		queryTag.text=document.all[_c+"_ulbQueryHidden"].value;
		docTag.appendChild(queryTag);
		
		eval(_c+"_xmlHttp").open("POST", eval(_c+"_supportFilesDir")+"DbComboServer.aspx?g", true);
		eval(_c+"_xmlHttp").setRequestHeader("Content-Type","text/xml");
		if (eval(_c+"_setLanguage"))
			eval(_c+"_xmlHttp").setRequestHeader("Accept-Language","en/us");
		
		if (reQuery)
			eval(_c+"_xmlHttp.onreadystatechange = "+_c+"_DbComboLoadDataStubReQuery;");
		else
			if (clearlist){
				if (setFocus)
					eval(_c+"_xmlHttp.onreadystatechange = "+_c+"_DbComboLoadDataStubClear;");
				else
					eval(_c+"_xmlHttp.onreadystatechange = "+_c+"_DbComboLoadDataStubClearNoFocus;");
			}
			else
			{
				eval(_c+"_xmlHttp.onreadystatechange = "+_c+"_DbComboLoadDataStub;");
			}
		
		eval(_c+"_xmlHttp").send(xmlRequest);
		
	}
	catch(er)
	{
		if(confirm("DbCombo error: Exception detected in 'Send Request' function.\n\nClick OK to revert to non-scripted (HTML 3.2 only) functionality.\nClick Cancel to ignore the error.\n\nNote to developers - this message can be caused by an exception in your ClientStateFunction.")){__doPostBack(c,'revert');}
		//__doPostBack(c,'revert');
	}
}
function DbComboLoadData(c,clearlist,setFocus,reQuery){
	print2(c,"DbComboLoadData");
	var _c=c.replace(/\$/g,"_");
	var dropDown = document.all[_c+"_ulbDropDown"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	
	
	try
	{
		if (!eval(_c+"_abortLookup"))
		{
		
			if (eval(_c+"_xmlHttp").readyState==4 && eval(_c+"_xmlHttp").responseText!="")
			{//all xml has been received.
				
				document.all[_c+"_ulbStatusDisplaySpan"].style.display = "";
				document.all[_c+"_ulbStatusDisplaySpan"].innerText = eval(_c+"_textLoading");//+eval(_c+"_xmlHttp").readyState+"/4";
				
				var tmpSelectedIndex = document.all[_c+"_ulbDropDown"].selectedIndex;
				if (clearlist){
					DbComboClearSelect(c);
				}
				var inString = eval(_c+"_xmlHttp").responseText;
				if (eval(_c+"_debug")){
					var navWindow;
					navWindow = window.open("","DebugPopup","height=400, width=500, location=0, menubar=0, status=0, toolbar=0, resizable=1, scrollbars=1");
					navWindow.document.open();
					navWindow.document.write("<div style=\"background-color:#eeeeee;padding:5px;font-family:Verdana;font-size:11px;\"><b>Debug mode</b><br>You have set debug=true. Below is displayed the raw output from the ServerMethod. This may be an exception, or if DbCombo is functioning correctly, a quantity of XML (the results). <br>Note: Refreshing this page will NOT update it's contents. Ensure you close it before you retry. Note: text values are enclosed in |'s to preserve whitespace.</div>"+inString);
					navWindow.document.focus();
					return;
				}
				
				eval(_c+"_xmlDoc").loadXML(inString);
				var exception=eval(_c+"_xmlDoc").documentElement.getElementsByTagName("exception").item(0);
				if (exception.text != "")
				{
					DbComboHideResults(c);
					if (eval(_c+"_errorBoxType")=="Auto")
						alert("Error - an exception occured on the server while attempting to get your results. To further investigate this error, try setting Debug=true in your Combo control.\nThis will display a page containing the full exception details. The exception text is:\n\n"+exception.text);
					else if (eval(_c+"_errorBoxType")=="Custom")
						alert(eval(_c+"_errorBoxCustomText"));
				}
				else
				{
					var results=eval(_c+"_xmlDoc").documentElement.getElementsByTagName("results").item(0);
					if (results.text == "-1")
					{//more results
						DbComboShowMoreResultsButton(c);
					}
					else
					{//no more results
						DbComboHideMoreResultsButton(c);
						document.all[_c+"_ulbStatusDisplaySpan"].innerText="";
						document.all[_c+"_ulbStatusDisplaySpan"].style.display = "";
					}
					
					var count = eval(_c+"_xmlDoc").documentElement.getElementsByTagName("a").length;
					if (count==0 && clearlist)
					{//zero results delivered and list has been cleared - e.g. zero results total.
						DbComboHideMoreResultsButton(c);
						document.all[_c+"_ulbStatusDisplaySpan"].innerText=eval(_c+"_textNoResults");//"No results.";
						document.all[_c+"_ulbStatusDisplaySpan"].style.display = "";
					}
					else
					{
						document.all[_c+"_ulbStatusDisplaySpan"].style.display = "none";
						document.all[_c+"_ulbReQueryOnLoad"].value = "true";
					}
					
					var QueryFoundInStream=false;
					var QueryFoundInStreamCount = 0;
					var ValueFoundInStream=false;
					var ValueFoundInStreamCount = 0;
					var QueryValueMatch = false;
					
					if (count!=0)
					{//some results delivered
						
						var i, D=document, x=document.all[_c+"_ulbDropDown"], z=x.cloneNode(true);
						for (var i_counter=0; i_counter<eval(_c+"_xmlDoc").documentElement.getElementsByTagName("a").length; i_counter++){
							var b=eval(_c+"_xmlDoc").documentElement.getElementsByTagName("a").item(i_counter);
							var newOption = z.appendChild(D.createElement('option'))
							var thisText = b.text.slice(1,-1);
							var thisValue = b.attributes(0).value
							newOption.value = thisValue;
							newOption.text = thisText;
							
							var QueryMatchThisRecord = false;
							if (document.all[_c+"_ulbTextBox"].value == thisText && thisText!="")
							{
								QueryFoundInStream = true;
								QueryFoundInStreamCount++;
								QueryMatchThisRecord = true;
							}
							if (document.all[_c+"_ulbValueHidden"].value == thisValue && thisValue!="")
							{
								ValueFoundInStream = true;
								ValueFoundInStreamCount++;
								if (QueryMatchThisRecord)
									QueryValueMatch = true;
							}
						}
						x.replaceNode(z)
					}
					if (QueryValueMatch)
					{
						var el=document.all[_c+"_ulbDropDown"];
						for(var x=0;x<el.options.length;x++){
							if(el.options[x].text==document.all[_c+"_ulbTextBox"].value && el.options[x].value==document.all[_c+"_ulbValueHidden"].value)
							{
								el.selectedIndex=x;
								DbComboChangedSelection(c,9);
								break;
							}
						}
					}
					else if (QueryFoundInStreamCount == 1 && valueHidden.value == "")
					{
						var el=document.all[_c+"_ulbDropDown"];
						for(var x=0;x<el.options.length;x++){
							if(el.options[x].text==document.all[_c+"_ulbTextBox"].value && el.options[x].text!="")
							{
								el.selectedIndex=x;
								valueHidden.value = el[el.selectedIndex].value;
								DbComboChangedSelection(c,8);
								break;
							}
						}
					}
					else if (ValueFoundInStreamCount == 1 && document.all[_c+"_ulbTextBox"].value == "")
					{
						var el=document.all[_c+"_ulbDropDown"];
						for(var x=0;x<el.options.length;x++){
							if(el.options[x].value==document.all[_c+"_ulbValueHidden"].value && el.options[x].value!="")
							{
								el.selectedIndex=x;
								document.all[_c+"_ulbTextBox"].value = el[el.selectedIndex].text;
								DbComboChangedSelection(c,8);
								break;
							}
						}
					}
					
					if (count!=0)
					{
						if (document.all[_c+"_ulbDropDown"].length<=1){
							document.all[_c+"_ulbDropDown"].size=2;
						}
						else if (document.all[_c+"_ulbDropDown"].length<eval(_c+"_initialNumRows")){
							document.all[_c+"_ulbDropDown"].size=document.all[_c+"_ulbDropDown"].length;
						}
						else{
							document.all[_c+"_ulbDropDown"].size=eval(_c+"_initialNumRows");
						}
					}
					
					var drop = document.all[_c+"_ulbDropDown"];
					var clipper = document.all[_c+"_ulbClipper"];
					var spacer = document.all[_c+"_ulbSpacer"];
					var resultsSpan = document.all[_c+"_ulbResultsSpan"];
					var textBox = document.all[_c+"_ulbTextBox"];
					
					var hideSelectTags=true;
					if (reQuery)
						hideSelectTags=false;
					DbComboResizeResults(c,hideSelectTags); 
					
					if (!(count==0 && clearlist))
					{
						if(setFocus)
							document.all[_c+"_ulbTextBox"].focus();
						
						if (!clearlist &! QueryFoundInStream &! ValueFoundInStream)
							document.all[_c+"_ulbDropDown"].selectedIndex = tmpSelectedIndex;
						
						eval(_c+"_searchAvailable=true;");
					}
					else
					{
						spacer.style.display="none";
					}
				}
				
				eval(_c+"_requestInProgress=false;");
				if (reQuery)
				{
					eval(_c+"_skipRecords = parseInt(document.all[_c+\"_ulbReQueryRecordsHidden\"].value);");
					eval(_c+"_numRows = (parseInt(document.all[_c+\"_ulbReQueryRecordsHidden\"].value)+"+_c+"_initialNumRows)/2;");
					document.all[_c+"_ulbReQueryRecordsHidden"].value = eval(_c+"_skipRecords");
					
				}
				else
				{
					eval(_c+"_skipRecords="+_c+"_numRows * 2 - "+_c+"_initialNumRows;");
					document.all[_c+"_ulbReQueryRecordsHidden"].value = eval(_c+"_skipRecords");
				} 
			}
			else
			{//xml not yet fully delivered
				document.all[_c+"_ulbStatusDisplaySpan"].style.display = "";
				document.all[_c+"_ulbStatusDisplaySpan"].innerText = eval(_c+"_textLoading");//+eval(_c+"_xmlHttp").readyState+"/4";
			}
		}
		else
		{
			eval(_c+"_keyPressNumber=0");
		}
	}
	catch(er)
	{
		if(confirm("DbCombo error: Exception detected in 'Load Data' function.\n\nClick OK to revert to non-scripted (HTML 3.2 only) functionality.\nClick Cancel to ignore the error.")){__doPostBack(c,'revert');}
		//__doPostBack(c,'revert');
	}
}
function DbComboStripNonNumbers(txt){
	print2("n/a","DbComboStripNonNumbers");
	var txtout="";
	for(var i = 0; i<txt.length; i++)
	{
		var t = txt.charAt(i);
		if (t=="0" || t=="1" || t=="2" || t=="3" || t=="4" || t=="5" || t=="6" || t=="7" || t=="8" || t=="9" || t=="." || t=="-")
			txtout+=t;
	}
	if (txtout=="")
		return "0";
	else
		return txtout;
}
function DbComboGetDim(el){
	print2("n/a","DbComboGetDim");
	var i=0;
	for (var lx=0,ly=0;el!=null;i++){
		var borderLeftWidth=0;
		var borderTopWidth=0;

		if (el.tagName!="TABLE" && el.tagName!="BODY" && i>0)
		{
			borderLeftWidth = parseInt(el.clientLeft);
			borderTopWidth = parseInt(el.clientTop);
		}
				
		if (el.currentStyle.position=="absolute" || el.currentStyle.position=="relative")
			break;
			
		if (
			( el.currentStyle.overflow == "scroll" || el.currentStyle.overflowX == "scroll" || el.currentStyle.overflowY == "scroll" || el.currentStyle.overflow == "auto" || el.currentStyle.overflowX == "auto" || el.currentStyle.overflowY == "auto" )
			&&
			( el.currentStyle.height != "auto" || el.currentStyle.width != "auto" )
			)
			break; 
		
		if (el.tagName=="MultiPage")
			break;
		
		lx+=el.offsetLeft+borderLeftWidth;
		ly+=el.offsetTop+borderTopWidth;
		el=el.offsetParent;
	}
	return {x:lx,y:ly}
}
function DbComboGetDimPage(el){
	print2("n/a","DbComboGetDimPage");
	var i=0;
	for (var lx=0,ly=0;el!=null;i++){
		var borderLeftWidth=0;
		var borderTopWidth=0;

		if (el.tagName!="TABLE" && el.tagName!="BODY" && i>0)
		{
			borderLeftWidth = parseInt(el.clientLeft);
			borderTopWidth = parseInt(el.clientTop);
		}
		
		lx+=el.offsetLeft+borderLeftWidth;
		ly+=el.offsetTop+borderTopWidth;
		el=el.offsetParent;
	}
	return {x:lx,y:ly}
}
function DbComboPositionResults(c){
	print2(c,"DbComboPositionResults");
	var _c=c.replace(/\$/g,"_");
	var textBox = document.all[_c+"_ulbTextBox"];
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];
	
	resultsSpan.style.left = DbComboGetDim(textBox).x + eval(_c+"_tweakX"); 
	resultsSpan.style.top = DbComboGetDim(textBox).y + textBox.offsetHeight - 1 + eval(_c+"_tweakY");
	
}
function DbComboResizeResults(c,hideSelectTags){
	print2(c,"DbComboResizeResults");
	var _c=c.replace(/\$/g,"_");
	
	var drop = document.all[_c+"_ulbDropDown"];
	var clipper = document.all[_c+"_ulbClipper"];
	var spacer = document.all[_c+"_ulbSpacer"];
	var resultsSpan = document.all[_c+"_ulbResultsSpan"];
	var textBox = document.all[_c+"_ulbTextBox"];
	var searchButton = document.all[_c+"_ulbSearchButton"];
	var revertButton = document.all[_c+"_ulbRevertButton"];
	var helpButton = document.all[_c+"_ulbHelpButton"];
	
	if (searchButton!=null)
		searchButton.style.height = textBox.offsetHeight;
	if (revertButton!=null)
		revertButton.style.height = textBox.offsetHeight;
	if (helpButton!=null)
		helpButton.style.height = textBox.offsetHeight;
	
	DbComboPositionResults(c);
	
	spacer.style.display="";
	drop.style.display="";
	drop.style.width="auto";
	resultsSpan.style.width = "auto";
	
	
	if (resultsSpan.offsetWidth < textBox.offsetWidth+4 && drop.offsetWidth < textBox.offsetWidth+4)
	{
		resultsSpan.style.width = textBox.offsetWidth;
	}
	else
	{
		resultsSpan.style.width = "auto";
	}
	
	
	var width = parseInt(drop.clientWidth)-3;
	var height = 0;
	
	var version = parseFloat(navigator.appVersion.split("MSIE")[1]);
	
	if (version >= 7){
		height = parseInt(drop.clientHeight)+3;
	}
	else{
		height = parseInt(drop.clientHeight)-3;
	}

	print1("width="+width);
	print1("height="+height);


	if (resultsSpan.clientWidth+3 > width)
	{
		width = resultsSpan.clientWidth+3;
		drop.style.width = resultsSpan.clientWidth+6;
	}
	else
	{
		drop.style.width = "auto";
	}
	if (drop.length<=1)
	{
		if (version >= 7){
			height = (  (parseInt(drop.clientHeight)-6)/2)+6;
		}
		else {
			height = (  (parseInt(drop.clientHeight)-6)/2)+3;
		}
	}

	print1("width="+width);
	print1("height="+height);

	
	if (width > 3 && height > 3)
	{
		spacer.style.width=width-3;
		spacer.style.height=height-3;
		clipper.style.clip="rect(3px "+width+"px "+height+"px 3px)";
	}
	
	print1("drop.length="+drop.length);
	
	if (drop.length==0)
		spacer.style.display="none";
		
	print1("End...");
	print1("resultsSpan.offsetWidth="+resultsSpan.offsetWidth);
	print1("textBox.offsetWidth="+textBox.offsetWidth);
	print1("drop.offsetWidth="+drop.offsetWidth);

	if (resultsSpan.offsetWidth < textBox.offsetWidth+4 && drop.offsetWidth < textBox.offsetWidth+4)
	{
		print1("resize");
		resultsSpan.style.width = textBox.offsetWidth;
	}
	else
	{
		resultsSpan.style.width = "auto";
	}
	
	if (hideSelectTags)
	{
		DbComboHideSelectTags(c);
	}
		
}
function DbComboGetMoreResults(c){
	print2(c,"DbComboGetMoreResults");
	var _c=c.replace(/\$/g,"_");
	if (DbComboStateUnChanged(c)){
		eval(_c+"_numRows="+_c+"_numRows*2;");
		DbComboHideMoreResultsButton(c);
		DbComboSendRequest(c,false,true,false);
	}
}
function DbComboClearSelect(c){
	print2(c,"DbComboClearSelect");
	var _c=c.replace(/\$/g,"_");
	var the_select = document.all[_c+"_ulbDropDown"];
	var x=the_select;
	var z=x.cloneNode(false);
	x.replaceNode(z);
	eval(_c+"_searchAvailable=false;");
	document.all[_c+"_ulbReQueryOnLoad"].value = "false";
}
function DbComboSetCaretToEnd(c){
	print2(c,"DbComboSetCaretToEnd");
	var _c=c.replace(/\$/g,"_");
	var el = document.all[_c+"_ulbTextBox"];
	
	if (el.createTextRange) {
		var v = el.value;
		var r = el.createTextRange();
		r.moveStart('character', v.length);
		r.select();
	}
}
function DbComboSelectAll(c){
	print2(c,"DbComboSelectAll");
	var _c=c.replace(/\$/g,"_");
	var el = document.all[_c+"_ulbTextBox"];
	
	if (el.createTextRange) {
		var v = el.value;
		var r = el.createTextRange();
		r.select();
	}
}
function DbComboGetText(c){
	print2(c,"DbComboGetText");
	var _c=c.replace(/\$/g,"_");
	var textBox = document.all[_c+"_ulbTextBox"];
	return textBox.value;
}
function DbComboGetQuery(c){
	print2(c,"DbComboGetQuery");
	var _c=c.replace(/\$/g,"_");
	var queryHidden = document.all[_c+"_ulbQueryHidden"];
	return queryHidden.value;
}
function DbComboGetValue(c){
	print2(c,"DbComboGetValue");
	var _c=c.replace(/\$/g,"_");
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	return valueHidden.value;
}
function DbComboChangeText(c,text){
	print2(c,"DbComboChangeText");
	var _c=c.replace(/\$/g,"_");
	var textBox = document.all[_c+"_ulbTextBox"];
	textBox.value=text;
	event.keyCode=999;
	DbComboTextBoxKeyUp(c);
	DbComboSetCaretToEnd(c);
}
function DbComboSelectByValue(c,value){
	print2(c,"DbComboSelectByValue");
	var _c=c.replace(/\$/g,"_");
	var textBox = document.all[_c+"_ulbTextBox"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	var el=document.all[_c+"_ulbDropDown"];
	for(var x=0;x<el.options.length;x++){
		if(el.options[x].value==value)
		{
			el.selectedIndex=x;
			valueHidden.value = el[el.selectedIndex].value;
			textBox.value = el[el.selectedIndex].text;
			DbComboChangedSelection(c,10);
			return true;
		}
	}
	return false;
}
function DbComboSelectByText(c,text){
	print2(c,"DbComboSelectByText");
	var _c=c.replace(/\$/g,"_");
	var textBox = document.all[_c+"_ulbTextBox"];
	var valueHidden = document.all[_c+"_ulbValueHidden"];
	var el=document.all[_c+"_ulbDropDown"];
	for(var x=0;x<el.options.length;x++){
		if(el.options[x].text==text)
		{
			el.selectedIndex=x;
			valueHidden.value = el[el.selectedIndex].value;
			textBox.value = el[el.selectedIndex].text;
			DbComboChangedSelection(c,11);
			return true;
		}
	}
	return false;
}
function print2(c,thisVal){
	if (typeof(document.all["foo"]) != "undefined")
		document.all["foo"].value = c+" "+(new Date()).getHours()+":"+(new Date()).getMinutes()+":"+(new Date()).getSeconds()+" "+thisVal+"\n" + document.all["foo"].value;
}
function print1(thisVal){
	print2(thisVal);
}
