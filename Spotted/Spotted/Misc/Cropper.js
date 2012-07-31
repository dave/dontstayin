function pVars(pT,cW,cH,xO,yO,iW,iH,sC){
	if (false)
		alert(
			"pT:"+pT+"\n"+
			"cW:"+cW+"\n"+
			"cH:"+cH+"\n"+
			"iW:"+iW+"\n"+
			"iH:"+iH+"\n"+
			"xO:"+xO+"\n"+
			"yO:"+yO+"\n"+
			"sC:"+sC+"\n"
		);
	document.forms[0].elements[pT+"$cW"].value = cW;
	document.forms[0].elements[pT+"$cH"].value = cH;
	document.forms[0].elements[pT+"$iW"].value = iW;
	document.forms[0].elements[pT+"$iH"].value = iH;
	document.forms[0].elements[pT+"$xO"].value = xO;
	document.forms[0].elements[pT+"$yO"].value = yO;
	document.forms[0].elements[pT+"$sC"].value = sC;
}
