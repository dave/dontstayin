// Product title: Gurt Calendar JavaScript
// Product version details: 1.2.0, 03-01-2006 (mm-dd-yyyy)
// Product URL: http://gurtom.com/products/calendars/js
// Contact info: gurt-feedback@gurtom.com (specify product title in the subject)
// Notes: This script is free. Feel free to copy, use and change this script as 
// long as this head part remains unchanged.  Visit official site for details.
// Copyright: (c) 2006 by Gurtom.Com

var agC= [],ogB,oD;
function gCalendar(oSettings){
var _ = this;
_.nCId = agC.length;
agC[_.nCId] = _;
if (!ogB) ogB = new _UserAgent();
if (!oD) oD = new _DManager();
_.sNameControl = oSettings.dataArea ? oSettings.dataArea :'dataArea'+_.nCId;
_.sIcoName = 'icoCls'+_.nCId;
_.sPosName = 'icoPos'+_.nCId;
_.sDivName = 'clsDiv'+_.nCId;
_.sBoxName = 'clsBox'+_.nCId;
_.sTitleName =  'clsTitle'+_.nCId;
_.sPmiName = 'clsPMI'+_.nCId;
_.sNmiName = 'clsNMI'+_.nCId;
_.sPyiName = 'clsPYI'+_.nCId;
_.sNyiName = 'clsNYI'+_.nCId;
_.sPmaName = 'clsPMA'+_.nCId;
_.sNmaName = 'clsNMA'+_.nCId;
_.sPyaName = 'clsPYA'+_.nCId;
_.sNyaName = 'clsNYA'+_.nCId;
_.onChange = oSettings.onChange;
//_.oChiefDate = new Date();
_.oChiefDate = oSettings.dateInit ? oSettings.dateInit : new Date();
_.sChiefFormat = !oSettings.dateFormat ? 'd/m/Y' : oSettings.dateFormat;
_.oApp = oSettings['appearance'];
gBuildControl(_, oSettings['disabled']);
if (oSettings.dateInit) _.oDataArea.value = oD.GenerateDate(_.oChiefDate,_.sChiefFormat);else{_.oDataArea.value="DD/MM/YYYY";}
}
function gBuildControl(_, disabled) {
var oWbuf = new writer();

gBuildCalendar(_);

//oWbuf.into  ('<table cellpadding="0" cellspacing="0" border="0" ><tr><td><input type="Text" id="' , _.sNameControl , '"  name="' , _.sNameControl , '" value=""  class ="',_.oApp['DataArea'],'" onFocus="if(this.value==\'DD/MM/YYYY\')this.value=\'\';" ></td><td align="right" width="25"><a href="javascript:gDisplayCalendar(agC[' + _.nCId + ']);" ><img src="',_.oApp['IcoCalVis'],'" alt="" name="'+ _.sIcoName +'" id="'+ _.sIcoName +'" width="23" height="19" border="0"></a></td></tr><tr><td align="left" colspan="2"><img src="/gfx/1pix.gif" alt="" name="'+ _.sPosName +'" id="'+ _.sPosName +'" width="1" height="1" border="0"></td></tr></table>');
oWbuf.into  ('<img src="/gfx/1pix.gif" alt="" name="'+ _.sPosName +'" id="'+ _.sPosName +'" width="1" height="1" border="0" style="vertical-align:bottom;"><input type="Text" id="' , _.sNameControl , '" name="' , _.sNameControl , '" value=""  class="',_.oApp['DataArea'],'" maxlength="10" ' + (_.onChange ? ' onKeyUp="' + _.onChange + '" ' : '') + ' onFocus="if(this.value==\'DD/MM/YYYY\')this.value=\'\';" ');
if(disabled == 'True')
    oWbuf.into  (' disabled="true"');
  
 oWbuf.into  (' align="bottom">');
if(disabled == 'False')
    oWbuf.into('<button onclick="gDisplayCalendar(agC[' + _.nCId + ']);return false;">...</button>');
document.write(oWbuf.out());

gSetControl(_);
gUpdateCalendarControl(_);
}
function gBuildCalendar(_){
var oWbuf = new writer();
oWbuf.into('<div id="',_.sDivName,'" name="',_.sDivName,'" style="position: absolute; background-color:beige; visibility:hidden; width:186; height:1; z-index: ',_.nCId+1,'"><table width="100%" cellpadding="0" cellspacing="1" border="0" class="',_.oApp['OuterFrame'],'"><tr><td ><table  width="100%" cellpadding="0" cellspacing="0" border="0" class="',_.oApp['InnerFrame'],'"  ><tr><td  colspan="3" class="',_.oApp['TopPartNavpanel'],'"><img src="/gfx/1pix.gif" width="1 px" height="1 px"></td></tr><tr><td  width="100%"  colspan="3" class="',_.oApp['Navpanel'],'"><table cellpadding="1" cellspacing="1" border="0" ><tr><td><a href="#" name="',_.sPyaName,'" id="',_.sPyaName,'"><img src="',_.oApp['PrevYear'],'" alt="',_.oApp['messages']['AltPrevYear'],'" name="',_.pyiName,'" id="',_.pyiName,'" width="18" height="21" border="0"></a></td><td><a href="#" name="',_.sPmaName,'" id="',_.sPmaName,'"><img src="',_.oApp['PrevMonth'],'" alt="',_.oApp['messages']['AltPrevMonth'],'" name="',_.sPmiName,'" id="',_.sPmiName,'" width="18" height="21" border="0"></a></td><td  width="100%" class="',_.oApp['InfoTitle'],'" id="',_.sTitleName,'" name="',_.sTitleName,'">',_.oApp['longmonth'][_.oChiefDate.getMonth()],'&nbsp;',_.oChiefDate.getFullYear(),'</td><td><a href="#" name="',_.sNmaName,'" id="',_.sNmaName,'"><img src="',_.oApp['NextMonth'],'" alt="',_.oApp['messages']['AltNextMonth'],'" name="',_.sNmiName,'" id="',_.sNmiName,'" width="18" height="21" border="0"></a></td><td><a href="#" name="',_.sNyaName,'" id="',_.sNyaName,'"><img src="',_.oApp['NextYear'],'" alt="',_.oApp['messages']['AltNextYear'],'" name="',_.sNyiName,'" id="',_.sNyiName,'" width="18" height="21" border="0"></a></td></tr></table></td></tr><tr><td colspan="3" class="',_.oApp['BottomPartNavpanel'],'"><img src="/gfx/1pix.gif" width="1 px" height="1 px"></td></tr><tr class="',_.oApp['MidRow'],'"><td><img src="/gfx/1pix.gif"  width="4 px"height="1 px"></td><td  align="center" id="',_.sBoxName,'" name="',_.sBoxName,'">',gUnitedGrid(_),'</td><td width="4 px"><img src="/gfx/1pix.gif"  width="4 px" height="1 px"></td></tr><tr><td colspan="3" class="',_.oApp['BottomPartNavpanel'],'"><img src="/gfx/1pix.gif" width="1 px" height="1 px"></td></tr></table></td ></tr></table></div>');
if(ogB.ie6){
oWbuf.into('<iframe id="IE6bug',_.sDivName,'" src="/gfx/1pix.gif"  name="IE6bug',_.sDivName,'" style="position: absolute; left:0; top:0; width:0; height:0; visibility:hidden; filter:alpha(opacity=0); z-index: ' ,_.nCId, '"></iframe>');
}
document.write(oWbuf.out());
}
function gDateReset(oInDate){
var oTmpDate = oInDate ? new Date(oInDate) : new Date();
oTmpDate.setHours(0);oTmpDate.setMinutes(0);oTmpDate.setSeconds(0);oTmpDate.setMilliseconds(0);
return oTmpDate;
}
function gDateType(c,oInDate){
var nResType = 1,oTmpDate = new Date(oInDate);
oTmpDate = gDateReset(oTmpDate);
var oChiefDateTmp = c.oChiefDate;
if (gDateReset(oChiefDateTmp).valueOf() == oTmpDate.valueOf()) nResType |= 2;
if (oTmpDate.getMonth() != oChiefDateTmp.getMonth() || oTmpDate.getFullYear() != oChiefDateTmp.getFullYear())	nResType |= 8;
if (oTmpDate.getDay() == 0 || oTmpDate.getDay() == 6)	nResType |= 4;
return nResType;
}
function gDisplayCalendar(_) {
if(_.oDataArea.value=="DD/MM/YYYY")
_.oDataArea.value="";
var sVis = String(_.oDiv.style.visibility).toLowerCase();
if (sVis == 'visible' || sVis == 'show') {
_.oDiv.style.visibility = 'hidden';
if(ogB.ie6) {_.oDiv2.style.visibility = 'hidden';}
//_.oIco.src = _.oApp['IcoCalVis'];
}
else {
gRePosition(_);
if(gVerifyDataArea(_))gUpdateCalendarData(_);
_.oDiv.style.visibility = 'visible';
if(ogB.ie6) {
_.oDiv2.style.width = _.oDiv.offsetWidth;
_.oDiv2.style.height  = _.oDiv.offsetHeight;
_.oDiv2.style.visibility = 'visible';
}
//_.oIco.src = _.oApp['IcoCalUnVis'];
}
}
function gUserClickHandler(_,inDa,typeClick) {
var tmpDa = inDa ? new Date(inDa) : new Date(_.oChiefDate); 
_.oChiefDate = new Date(tmpDa);
if(!typeClick) {gDisplayCalendar(_);_.oDataArea.value = oD.GenerateDate(_.oChiefDate,_.sChiefFormat);}
gUpdateCalendarData(_);
if (_.onChange)eval(_.onChange);
}
function gUpdateCalendarData(_){
gUpdateCalendarControl(_);
_.oInfoTitle.innerHTML = _.oApp['longmonth'][_.oChiefDate.getMonth()]+'&nbsp;'+_.oChiefDate.getFullYear();
_.oUnitedGrid.innerHTML = '';
_.oUnitedGrid.innerHTML =  gUnitedGrid(_);
}
function gSetControl(_){
var oTmpDate;
_.oDiv = gObja(_,_.sDivName);
if(ogB.ie6) _.oDiv2 = gObja(_,'IE6bug'+_.sDivName);
_.oIco = gObja(_,_.sIcoName);
_.oPos  = gObja(_,_.sPosName);
_.oDataArea = gObja(_,_.sNameControl);
_.oUnitedGrid = gObja(_,_.sBoxName);
_.oInfoTitle = gObja(_,_.sTitleName);
_.oPMI = gObja(_,_.sPmiName);
_.oNMI = gObja(_,_.sNmiName);
_.oPYI = gObja(_,_.pyiName);
_.oNYI = gObja(_,_.sNyiName);
_.oPMA = gObja(_,_.sPmaName);
_.oNMA = gObja(_,_.sNmaName);
_.oPYA = gObja(_,_.sPyaName);
_.oNYA = gObja(_,_.sNyaName);
}
function gShiftDate (oInDate, sShiftYear, sShiftMonth ,sShiftHour,sShiftMinute,sShiftSecond) {
var oTmpDate = new Date(oInDate);
if (sShiftYear) oTmpDate.setFullYear(oTmpDate.getFullYear() + sShiftYear);
if (sShiftMonth) {oTmpDate.setMonth(oTmpDate.getMonth() + sShiftMonth);}
if (sShiftHour) {oTmpDate.setHours(oTmpDate.getHours() + sShiftHour);}
if (sShiftMinute) {oTmpDate.setMinutes(oTmpDate.getMinutes() + sShiftMinute);}
if (sShiftSecond) {oTmpDate.setSeconds(oTmpDate.getSeconds() + sShiftSecond);}
if(!(sShiftHour||sShiftMinute||sShiftSecond)) {
if (oTmpDate.getDate() != oInDate.getDate()) {oTmpDate.setDate(0);}
}
return oTmpDate.valueOf();
}
function gUpdateCalendarControl(_){
_.oPYA.href = "javascript:  gUserClickHandler(agC["+_.nCId+"],"+gShiftDate (_.oChiefDate,-1)+",2);";
_.oNYA.href = "javascript:  gUserClickHandler(agC["+_.nCId+"],"+gShiftDate (_.oChiefDate,1)+",2);";
_.oPMA.href = "javascript: gUserClickHandler(agC["+_.nCId+"],"+gShiftDate (_.oChiefDate,null,-1)+",1);";
_.oNMA.href = "javascript: gUserClickHandler(agC["+_.nCId+"],"+gShiftDate (_.oChiefDate,null,1)+",1);";
}
function gVerifyDataArea(_){
if (_.oDataArea.value)	{
oTmpDate = oD.ParseDate(_.oDataArea.value+'',_.sChiefFormat);
if(!oTmpDate) {
alert(_.oApp['messages']['Warning']);
oTmpDate=new Date()
};
if(oTmpDate.valueOf() != _.oChiefDate.valueOf()) {
_.oChiefDate = new Date(oTmpDate);
return true;
}
else  {
_.oDataArea.value = oD.GenerateDate(_.oChiefDate,_.sChiefFormat);
return false;
}
}
else return false;
}
function gDayTitle (o) {
var oWbuf = new writer();
oWbuf.into('<tr  class="',o.oApp['WeekDay'],'">');
for (var nWD = 0; nWD < 7; nWD++) oWbuf.into('<td>',o.oApp.weekdays[(nWD+1)%7],'</td>');
oWbuf.into('</tr>');
return(oWbuf.out());
}
function gDayGrid(a){
var oWbuf = new writer(),oFDay = new Date(a.oChiefDate);
oFDay.setDate(1);
oFDay.setDate(1 - (6 + oFDay.getDay()) % 7);
var oTDay = new Date(oFDay);
while (oTDay.getMonth() == a.oChiefDate.getMonth() || oTDay.getMonth() == oFDay.getMonth()) {
oWbuf.into('<tr>');
for (var nWD = 0; nWD < 7; nWD++) {
oWbuf.into(gDayCell(a,oTDay));
oTDay.setDate(oTDay.getDate() + 1);
}
oWbuf.into('</tr>\n');
}
return(oWbuf.out());
}
function gUnitedGrid(_){
var oWbuf = new writer();
oWbuf.into('<table cellpadding="2"  cellspacing="1" border="0" width="100%"   class="',_.oApp['DateGrid'],'">');
oWbuf.into(gDayTitle(_));
oWbuf.into(gDayGrid(_));
oWbuf.into('</table>');
return(oWbuf.out());
}
function _UserAgent() {
var _ = this,br = navigator.appName,v = _.version = navigator.appVersion,ua=_.uas = navigator.userAgent.toLowerCase(),re_num = /opera/;
_.op = re_num.exec(ua)?true:false;
_.ie = (br == "Microsoft Internet Explorer");
if(_.op) {_.ie = false;}
_.v = parseInt(v);
if (_.ie) {
_.ie4 = _.ie5 = _.ie55 = _.ie6 = false;
if (v.indexOf('MSIE 6') > 0) {_.ie6 = true; _.v = 6;}
}
_.win32 = ua.indexOf("win")>-1;
_.mac = ua.indexOf("mac")>-1;
}
function dmMakeWorkTemplate(inFormat){
var _=this,sCh,nKey=0,aTmp=[],aDel=["\\\\","\\/","\\.","\\+","\\*","\\?","\\$","\\^","\\|"];
for(nI = 0; nI < inFormat.length; nI ++){
sCh = inFormat.substr(nI,1);
if(_.dmFormatChar.indexOf(sCh) != -1 && sCh != ''){
aTmp[nKey]=sCh;
_.dmTmpFormat[nKey++]=sCh;
}
}
nKey=1;
for(nI in aDel) {
inFormat=inFormat.replace(eval("/"+aDel[nI]+"/g"),aDel[nI]);
}
for(nI=0;nI<aTmp.length;nI++){
re=new RegExp(aTmp[nI]);
inFormat=inFormat.replace(re,_.dmRegFormatChar[aTmp[nI]])
}
return new RegExp("^"+inFormat.replace(/\s+/g,"\\s+")+"$");
}
function dmGenerateDate(inData,inFormat){
var _=this,nKeyCh,nI=0,sTr='',sTo='',dt_d=new Date(inData);
do{
nKeyCh = inFormat.substr(nI,1);
if(_.dmFormatChar.indexOf(nKeyCh)!=-1&&nKeyCh!=''){
if(typeof(dt_d[_.dmCallChar[nKeyCh][1]])!='function')	sTo=new String(_.dmCallChar[nKeyCh][1](dt_d));
else sTo=new String(dt_d[_.dmCallChar[nKeyCh][1]]());
sTr+=sTo
}
else sTr+=nKeyCh;
nI++
} while (nI < inFormat.length)
return sTr;
}
function dmParseDate(oInDate,inFormat){ 
var _=this,aOut = [], nI, nK = 1,workTemplate = _.MakeWorkTemplate(inFormat),tmpData= _.DateReset(),nI,flag_date = false,oDate=null,tmpData = new Date(0,0,1),chKey,oRe = workTemplate.exec(oInDate);
if (!oRe || typeof(oRe) != 'object') {
return null;
}
for (nI in _.dmTmpFormat) {
aOut[nI] = [oRe[nK++], _.dmTmpFormat[nI]]
}
oAdate = aOut;
for(nI in oAdate){
if(_.dmSignFormatChar.indexOf(oAdate[nI][1])!=-1){
	chKey=oAdate[nI][1];
	var oTmp=_.dmCallChar[oAdate[nI][1]][2](oAdate[nI][0]);
	if(chKey == 'd') { chDate = chKey; oDate = oTmp;}
	tmpData[_.dmCallChar[chKey][0]](oTmp);
	if(oDate) tmpData[_.dmCallChar[chDate][0]](oDate);
}
}
return tmpData;
}

function gDayCell(oB,oInDate){
var oTDay = new Date (oInDate),nTD = gDateType(oB,oTDay),stTName,sLink,sCell;
if(nTD&2) stTName = 'SelectedDay';
else if(nTD&8) stTName = 'OtherMonthDay';
else stTName = 'CurrentMonthDay';
sLink = 'javascript: gUserClickHandler(agC['+oB.nCId+'],'+oInDate.valueOf()+');';
sCell = '<a href="'+sLink+'" class="'+oB.oApp[stTName]+'">' + oInDate.getDate() + '</a>';
if(nTD&2) stTName = 'SelectedDay';
else if(nTD&4) stTName = 'HoliDay';
else if(nTD&8) stTName = 'OtherMonthDay';
else stTName = 'WorkDay';
sCell = '<td class="'+oB.oApp[stTName+'Cell']+'" align="center">' + sCell + '</td>';
return sCell;
}
function _DManager(){
var _=this;
_.dmFormatChar='dmY';
_.dmSignFormatChar='dmY';
_.dmRegFormatChar = {'d' : "([0-9]{0,2})",'m' : "([0-9]{0,2})",'Y' : "([0-9]{4})"};
_.dmCallChar={'d':['setDate',function(_v,_m){_v=_v.getDate();if(_v<10)return('0'+_v);else return _v},function(_v){return _v*1}],'m':['setMonth',function(_v){_v=_v.getMonth()+1;if(_v<10)return('0'+_v);else return _v},function(_v){return(_v*1-1)}],'Y':['setFullYear','getFullYear',function(_v){return _v*1}]};
_.dmTmpFormat =[]; 	
_.DateReset = gDateReset;
_.MakeWorkTemplate = dmMakeWorkTemplate;
_.GenerateDate = dmGenerateDate;
_.ParseDate = dmParseDate;
}
function gRePosition (_) {
_.oDiv.style.left = (gGlobalPosition(_,'Left') + 2)+'px';
_.oDiv.style.top  = gGlobalPosition(_,'Top')+'px';
if(ogB.ie6){
_.oDiv2.style.left	= _.oDiv.style.left;
_.oDiv2.style.top	= _.oDiv.style.top;
}
}
function gGlobalPosition (_,displace) {
var nPos = 0, tPos = _.oPos;
while (tPos)	{

if (tPos.style.position=="absolute" || tPos.style.position=="relative")
	return nPos;

nPos += tPos["offset" + displace];
tPos = tPos.offsetParent;
}
return nPos;
}
function gObja (_,_id) {
if (document.images && document.images[_id]) return document.images[_id];
else if (_.formName &&  document.forms[_.formName].elements[_id])  return document.forms[_.formName].elements[_id];
else if (document.all && document.all[_id])  return document.all[_id];
else if (document.getElementById)  return document.getElementById(_id);
else return null;
}
function writer() {
var _ = this;
_.bufArray = [];
_.into = function () {
var n = arguments.length;
for (var nI = 0; nI < n; nI++)
_.bufArray[_.bufArray.length] = arguments[nI];
};
_.out = function () {
return _.bufArray.join('');
};
}

var GC_APPEARANCE = {
	'weekdays':  ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'], 
	'longmonth' : ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'],
	'messages' : {
				'Warning' : 'Invalid date',
				'AltPrevYear' : 'previous year',
				'AltNextYear' : 'next year',
				'AltPrevMonth' : 'previous month',
				'AltNextMonth' : 'next month'
	},
	'CalDiv' : 'clsCalDiv',
	'OuterFrame' : 'clsOuterFrame',
	'InnerFrame' : 'clsInnerFrame',
	'TopPartNavpanel' : 'clsTopPartNavpanel',
	'BottomPartNavpanel' : 'clsBottomPartNavpanel',
	'Navpanel' : 'clsNavpanel',
	'MidRow' : 'clsMidRow',
	'DateGrid':'clsDateGrid',
	'WeekDay':'clsWeekDay',
	'WorkDayCell': 'clsWorkDayCell',
	'HoliDayCell': 'clsHoliDayCell',
	'OtherMonthDayCell': 'clsOtherMonthDayCell',
	'SelectedDayCell': 'clsSelectedDayCell',
	'CurrentMonthDay': 'clsCurrentMonthDay',
	'OtherMonthDay': 'clsOtherMonthDay',
	'SelectedDay': 'clsSelectedDay',
	'InfoTitle':'clsInfoTitle',
	'DataArea':'clsDataArea',
	'PrevYear':'/gfx/calendar-prev-year.gif',
	'PrevMonth':'/gfx/calendar-prev-month.gif',
	'NextYear':'/gfx/calendar-next-year.gif',
	'NextMonth':'/gfx/calendar-next-month.gif'

};

try{Sys.Application.notifyScriptLoaded();}catch(ex){}
