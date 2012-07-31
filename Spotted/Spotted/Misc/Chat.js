
var AdvancedPanel;
var AdvancedCheckBox;

var PublicRadio;
var NewPublicRadio;
var PrivateRadio;
var GroupRadio;

var GroupPrivateCheckBox;
var EventCheckBox;
var NewsCheckBox;
var SealedCheckBox;
var InviteCheckBox;

var PublicSpan;
var PrivateSpan;
var GroupSpan;
var GroupPrivateSpan;
var EventSpan;
var NewsSpan;
var SealedSpan;
var InviteSpan;

var changedPrivate;
var previousPrivate;

var changedGroupPrivate;
var previousGroupPrivate;

var changedNews;
var previousNews;

var changedSealed;
var previousSealed;

var changedInvite;
var previousInvite;

var InvitePanel;
var GroupDropDown;
var EventDropDown;

var GroupMode;



function InitAddThread(ClientId, previousPrivateInit, previousGroupPrivateInit, previousNewsInit, previousSealedInit, previousInviteInit, groupModeInit){

	
	AdvancedPanel = document.getElementById(ClientId + "AddThreadAdvancedPanel");
	AdvancedCheckBox = document.getElementById(ClientId + "AddThreadAdvancedCheckBox");
			
	PublicRadio = document.getElementById(ClientId + "AddThreadPublicRadioButton");
	PrivateRadio = document.getElementById(ClientId + "AddThreadPrivateRadioButton");
	GroupRadio = document.getElementById(ClientId + "AddThreadGroupRadioButton");
	
	GroupPrivateCheckBox = document.getElementById(ClientId + "AddThreadGroupPrivateCheckBox");
	EventCheckBox = document.getElementById(ClientId + "AddThreadEventCheckBox");
	NewsCheckBox = document.getElementById(ClientId + "AddThreadNewsCheckBox");
	SealedCheckBox = document.getElementById(ClientId + "AddThreadSealedCheckBox");
	InviteCheckBox = document.getElementById(ClientId + "AddThreadInviteCheckBox");
	
	PublicSpan = document.getElementById(ClientId + "AddThreadPublicRadioButtonSpan");
	PrivateSpan = document.getElementById(ClientId + "AddThreadPrivateRadioButtonSpan");
	GroupSpan = document.getElementById(ClientId + "AddThreadGroupRadioButtonSpan");
	GroupPrivateSpan = document.getElementById(ClientId + "AddThreadGroupPrivateCheckBoxSpan");
	EventSpan = document.getElementById(ClientId + "AddThreadEventCheckBoxSpan");
	NewsSpan = document.getElementById(ClientId + "AddThreadNewsCheckBoxSpan");
	SealedSpan = document.getElementById(ClientId + "AddThreadSealedCheckBoxSpan");
	InviteSpan = document.getElementById(ClientId + "AddThreadInviteCheckBoxSpan");
	
	changedPrivate = false;
	previousPrivate = previousPrivateInit;
	
	changedGroupPrivate = false;
	previousGroupPrivate = previousGroupPrivateInit;
	
	changedNews = false;
	previousNews = previousNewsInit;
	
	changedSealed = false;
	previousSealed = previousSealedInit;
	
	changedInvite = false;
	previousInvite = previousInviteInit;
	
	InvitePanel = document.getElementById(ClientId + "AddThreadInvitePanel");
	GroupDropDown = document.getElementById(ClientId + "AddThreadGroupDropDown");
	EventDropDown = document.getElementById(ClientId + "AddThreadEventDropDown");
	
	GroupMode = groupModeInit;
	
	PaintAddThread();
	
}

function InitThreadControl(ClientId, previousGroupPrivateInit, previousNewsInit, previousSealedInit, previousInviteInit){
	
	AdvancedPanel = document.getElementById(ClientId + "AddThreadAdvancedPanel");
	AdvancedCheckBox = document.getElementById(ClientId + "AddThreadAdvancedCheckBox");
			
	PublicRadio = document.getElementById(ClientId + "AddThreadPublicRadioButton");
	NewPublicRadio = document.getElementById(ClientId + "AddThreadNewPublicRadioButton");
	PrivateRadio = document.getElementById(ClientId + "AddThreadPrivateRadioButton");
	GroupRadio = document.getElementById(ClientId + "AddThreadGroupRadioButton");
	
	GroupPrivateCheckBox = document.getElementById(ClientId + "AddThreadGroupPrivateCheckBox");
	NewsCheckBox = document.getElementById(ClientId + "AddThreadNewsCheckBox");
	SealedCheckBox = document.getElementById(ClientId + "AddThreadSealedCheckBox");
	InviteCheckBox = document.getElementById(ClientId + "AddThreadInviteCheckBox");
	
	PublicSpan = document.getElementById(ClientId + "AddThreadPublicRadioButtonSpan");
	PrivateSpan = document.getElementById(ClientId + "AddThreadPrivateRadioButtonSpan");
	GroupSpan = document.getElementById(ClientId + "AddThreadGroupRadioButtonSpan");
	GroupPrivateSpan = document.getElementById(ClientId + "AddThreadGroupPrivateCheckBoxSpan");
	NewsSpan = document.getElementById(ClientId + "AddThreadNewsCheckBoxSpan");
	SealedSpan = document.getElementById(ClientId + "AddThreadSealedCheckBoxSpan");
	InviteSpan = document.getElementById(ClientId + "AddThreadInviteCheckBoxSpan");
	
	changedGroupPrivate = false;
	previousGroupPrivate = previousGroupPrivateInit;
	
	changedNews = false;
	previousNews = previousNewsInit;
	
	changedSealed = false;
	previousSealed = previousSealedInit;
	
	changedInvite = false;
	previousInvite = previousInviteInit;
	
	InvitePanel = document.getElementById(ClientId + "AddThreadInvitePanel");
	GroupDropDown = document.getElementById(ClientId + "AddThreadGroupDropDown");
	
	PaintThreadControl();
}

function PaintAddThread(){
	try{ AdvancedPanel.style.display=AdvancedCheckBox.checked?'':'none';}catch(e){};
	if (GroupMode)
	{
		InvitePanel.style.display=InviteCheckBox.checked?'':'none';
		return;
	}
	
	if (PublicRadio.checked)
	{
		if (GroupRadio!=null) 
		{
			GroupDropDown.disabled=true;
		
			changedGroupPrivate = true;
			if (!GroupPrivateSpan.disabled) previousGroupPrivate = GroupPrivateCheckBox.checked;
			GroupPrivateCheckBox.checked=false;
			GroupPrivateSpan.disabled=true;
			GroupPrivateSpan.className="Disabled";
			GroupPrivateCheckBox.disabled=true;
		}
		
		//if (changedNews) NewsCheckBox.checked=previousNews;
		//NewsSpan.disabled=false;
		//NewsSpan.className="";
		//NewsCheckBox.disabled=false;

		changedNews = true;
		if (!NewsSpan.disabled) previousNews = NewsCheckBox.checked;
		NewsSpan.disabled = true;
		NewsSpan.className = "Disabled";
		NewsCheckBox.disabled = true;
		NewsCheckBox.checked = false;
		
		changedSealed = true;
		if (!SealedSpan.disabled) previousSealed = SealedCheckBox.checked;
		SealedSpan.disabled=true;
		SealedSpan.className="Disabled";
		SealedCheckBox.disabled=true;
		SealedCheckBox.checked=false;
		
		if (changedInvite) InviteCheckBox.checked=previousInvite;
		InviteSpan.disabled=false;
		InviteSpan.className="";
		InviteCheckBox.disabled=false;

		InvitePanel.style.display=InviteCheckBox.checked?'':'none';
	}
	else if (PrivateRadio.checked)
	{
		if (GroupRadio!=null) 
		{
			GroupDropDown.disabled=true;
	
			changedGroupPrivate = true;
			if (!GroupPrivateSpan.disabled) previousGroupPrivate = GroupPrivateCheckBox.checked;
			GroupPrivateSpan.disabled=true;
			GroupPrivateSpan.className="Disabled";
			GroupPrivateCheckBox.disabled=true;
			GroupPrivateCheckBox.checked=false;
		}
		
		changedNews = true;
		if (!NewsSpan.disabled) previousNews = NewsCheckBox.checked;
		NewsSpan.disabled=true;
		NewsSpan.className="Disabled";
		NewsCheckBox.disabled=true;
		NewsCheckBox.checked=false;
		
		if (changedSealed) SealedCheckBox.checked=previousSealed;
		SealedSpan.disabled=false;
		SealedSpan.className="";
		SealedCheckBox.disabled=false;

		changedInvite = true;
		if (!InviteSpan.disabled) previousInvite = InviteCheckBox.checked;
		InviteSpan.disabled=true;
		InviteSpan.className="Disabled";
		InviteCheckBox.disabled=true;
		InviteCheckBox.checked=true;
		
		InvitePanel.style.display='';
	}
	else if (GroupRadio!=null && GroupRadio.checked)
	{
		if (!GroupSpan.disabled)
			GroupDropDown.disabled=false;
	
		if (changedGroupPrivate) GroupPrivateCheckBox.checked=previousGroupPrivate;
		GroupPrivateSpan.disabled=false;
		GroupPrivateSpan.className="";
		GroupPrivateCheckBox.disabled=false;
		
		if (changedNews) NewsCheckBox.checked=previousNews;
		NewsSpan.disabled=false;
		NewsSpan.className="";
		NewsCheckBox.disabled=false;
		
		changedSealed = true;
		if (!SealedSpan.disabled) previousSealed = SealedCheckBox.checked;
		SealedSpan.disabled=true;
		SealedSpan.className="Disabled";
		SealedCheckBox.disabled=true;
		SealedCheckBox.checked=false;
		
		if (changedInvite) InviteCheckBox.checked=previousInvite;
		InviteSpan.disabled=false;
		InviteSpan.className="";
		InviteCheckBox.disabled=false;

		InvitePanel.style.display=InviteCheckBox.checked?'':'none';
	}
}

function PaintThreadControl(){
	if (AdvancedPanel != null){
		AdvancedPanel.style.display=AdvancedCheckBox.checked?'':'none';
	}
	if (PublicRadio.checked || NewPublicRadio.checked)
	{
		if (GroupRadio!=null) 
		{
			GroupDropDown.disabled=true;
			
			changedGroupPrivate = true;
			if (!GroupPrivateSpan.disabled) previousGroupPrivate = GroupPrivateCheckBox.checked;
			GroupPrivateSpan.disabled=true;
			GroupPrivateSpan.className="Disabled";
			GroupPrivateCheckBox.disabled=true;
			GroupPrivateCheckBox.checked=false;
		}
		
//		if (PublicRadio.checked)
//		{
//			changedNews = true;
//			if (!NewsSpan.disabled) previousNews = NewsCheckBox.checked;
//			NewsSpan.disabled=true;
//			NewsSpan.className="Disabled";
//			NewsCheckBox.disabled=true;
//			NewsCheckBox.checked=false;
//		}
//		else
//		{
//			if (changedNews) NewsCheckBox.checked=previousNews;
//			NewsSpan.disabled=false;
//			NewsSpan.className="";
//			NewsCheckBox.disabled=false;
//		}
		changedNews = true;
		if (!NewsSpan.disabled) previousNews = NewsCheckBox.checked;
		NewsSpan.disabled = true;
		NewsSpan.className = "Disabled";
		NewsCheckBox.disabled = true;
		NewsCheckBox.checked = false;
		
		changedSealed = true;
		if (!SealedSpan.disabled) previousSealed = SealedCheckBox.checked;
		SealedSpan.disabled=true;
		SealedSpan.className="Disabled";
		SealedCheckBox.disabled=true;
		SealedCheckBox.checked=false;
		
		if (changedInvite) InviteCheckBox.checked=previousInvite;
		InviteSpan.disabled=false;
		InviteSpan.className="";
		InviteCheckBox.disabled=false;

		InvitePanel.style.display=InviteCheckBox.checked?'':'none';
	}
	else if (PrivateRadio.checked)
	{
		if (GroupRadio!=null) 
		{
			GroupDropDown.disabled=true;

			changedGroupPrivate = true;
			if (!GroupPrivateSpan.disabled) previousGroupPrivate = GroupPrivateCheckBox.checked;
			GroupPrivateSpan.disabled=true;
			GroupPrivateSpan.className="Disabled";
			GroupPrivateCheckBox.disabled=true;
			GroupPrivateCheckBox.checked=false;
			
		}
		
		changedNews = true;
		if (!NewsSpan.disabled) previousNews = NewsCheckBox.checked;
		NewsSpan.disabled=true;
		NewsSpan.className="Disabled";
		NewsCheckBox.disabled=true;
		NewsCheckBox.checked=false;
		
		if (changedSealed) SealedCheckBox.checked=previousSealed;
		SealedSpan.disabled=false;
		SealedSpan.className="";
		SealedCheckBox.disabled=false;

		changedInvite = true;
		if (!InviteSpan.disabled) previousInvite = InviteCheckBox.checked;
		InviteSpan.disabled=true;
		InviteSpan.className="Disabled";
		InviteCheckBox.disabled=true;
		InviteCheckBox.checked=true;
		
		InvitePanel.style.display='';
	}
	else if (GroupRadio!=null && GroupRadio.checked)
	{
		GroupDropDown.disabled=false;

		if (changedGroupPrivate) GroupPrivateCheckBox.checked=previousGroupPrivate;
		GroupPrivateSpan.disabled=false;
		GroupPrivateSpan.className="";
		GroupPrivateCheckBox.disabled=false;
		
		if (changedNews) NewsCheckBox.checked=previousNews;
		NewsSpan.disabled=false;
		NewsSpan.className="";
		NewsCheckBox.disabled=false;
		
		changedSealed = true;
		if (!SealedSpan.disabled) previousSealed = SealedCheckBox.checked;
		SealedSpan.disabled=true;
		SealedSpan.className="Disabled";
		SealedCheckBox.disabled=true;
		SealedCheckBox.checked=false;
		
		if (changedInvite) InviteCheckBox.checked=previousInvite;
		InviteSpan.disabled=false;
		InviteSpan.className="";
		InviteCheckBox.disabled=false;

		InvitePanel.style.display=InviteCheckBox.checked?'':'none';
	}
}

try{Sys.Application.notifyScriptLoaded();}catch(ex){}
