var	^Opera = navigator.userAgent.toLowerCase().indexOf("opera")	> -1;
var	^Moz = document.implementation && document.implementation.createDocument && !^Opera;
var	^IE	= navigator.userAgent.toLowerCase().indexOf("msie")	> -1 && !^Moz && !^Opera;
function ^CreateXmlDom(){
	return Sarissa.getDomDocument("http:/"+"/www.dontstayin.com/dbchat","doc");
}
function ^CreateXmlHttp(){
	return new XMLHttpRequest();
}
function ^GetResponseXML($objXmlHttp)
{
	return $objXmlHttp.responseXML;
}
function ^GetXML($objDOM)
{
	return new XMLSerializer().serializeToString($objDOM);
}
function ^CreateNode($xmlDom,$nodeName,$nodeData){
	var	$newNode = $xmlDom.createElement($nodeName);
	if ($nodeData!="")
	{
		$newNode.appendChild($xmlDom.createTextNode($nodeData));
	}
	return $newNode;
}
function ^AppendNode($xmlDom,$parentNode,$nodeName,$nodeData){
	var	$newNode = ^CreateNode($xmlDom,$nodeName,$nodeData);
	$parentNode.appendChild($newNode);
}
function ^NodeText($xmlNode){
	return Sarissa.getText($xmlNode);
}
function ^XmlInit($xmlDom){
//	if (_SARISSA_IS_SAFARI)
//	{
//		var $docTag = $xmlDom.createElement("doc");
//		$xmlDom.appendChild($docTag);
//		return $docTag;
//	}
//	else
	return $xmlDom.documentElement;
}
function ^UrlDecode($psEncodeString){
  var $lsRegExp = /\+/g;
  return unescape(String($psEncodeString).replace($lsRegExp," "));
}
