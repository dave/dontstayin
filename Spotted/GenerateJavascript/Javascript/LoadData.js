function ^HtmlEncode($str) {
  return $str.replace(/</g,"&lt;").replace(/>/g,"&gt;")
}
function ^LoadData($firstRequest,$resetRequestId){
	
	if (!^XmlhttpRefresh)
		return;
		
	if (^XmlhttpRefresh==null)
		return;
		
	if (^XmlhttpRefresh.readyState==4)
	{
		try
		{
			if (^XmlhttpRefresh.status==200)
			{
				var $doc = ^XmlhttpRefresh.responseXML.documentElement;
				
				try
				{
					var $redirect = $doc.getAttribute("redirect");
					if ($redirect.length > 10)
					{
						document.location = $redirect;
						return;
					}
				}
				catch(ex1){}
				
				var $resetTicks = $doc.getAttribute("resetTicks");
				var $results = $doc.getAttribute("results");
				if ($resetTicks=="1")
				{
					var $ticks = $doc.getAttribute("ticks");
					DbChatTicks = $ticks;
				}
				var $items = $doc.getElementsByTagName("chatItem");
				var $gotItems = false;
				if ($items.length>0)
					$gotItems = true;
				var $playSound = false;
				var $privateMessageAlert = false;
				var $privateMessageAlertName = "";
				var $privateMessageAlertText = "";
				var $privateMessageAlertThreadK = "";
				var $privateMessageAlertUsrK = "";
				var $newChatHtml = "";
				var $newMessageHtml = "";
				var $newChatLastDateTime = "";
				var $newMessageLastDateTime = "";
				
				for (var i=0;i<$items.length;i++)
				{
					var $item = $items.item(i);
					var $type = $item.getAttribute("type");
					var $dateTime = $item.getAttribute("dateTime");
					var $pic = "";
					var $picPhotoK = "";
					var $picIsPhoto = false;
					var $picIsGroup = false;
					var $name = "";
					var $usrK = "";
					var $threadK = "";
					var $privateChatUsrK = "";
					
					if ($type=="1")//Chat message
					{
						var $this = $item.getElementsByTagName("chatMessage").item(0);
						$name = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $k = $this.getAttribute("k");
						var $text = ^NodeText($this);
						$playSound = true;
						$pic = $this.getAttribute("pic");
						$picIsPhoto = false;
						$picIsGroup = false;
						$usrK = $this.getAttribute("usrK");
						$newChatLastDateTime = $item.getAttribute("dateTime");
						
						$newChatHtml = "<div class=\"ChatName\"><a href=\"/members/"+$name.toLowerCase()+"\" onmouseover=\"stmu('"+$pic+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $name + "</a>:</div><div class=\"ChatMessage\">" + $text + "</div>" + $newChatHtml;
						document.getElementById("ChatMessages").innerHTML=$k+" messages posted"
					}
					else if ($type=="2")//Private chat message
					{
						var $this = $item.getElementsByTagName("privateMessage").item(0);
						
						var $stmu = $this.getAttribute("stmu");
						var $k = $this.getAttribute("k");
						$threadK = $this.getAttribute("t");
						$privateChatUsrK = $this.getAttribute("u");
						var $subject = $this.getAttribute("subject");
						var $shortText = $this.getAttribute("short");
						var $alert = $this.getAttribute("alert");
						var $text = ^NodeText($this);
						$newChatLastDateTime = $item.getAttribute("dateTime");
						$usrK = $this.getAttribute("usrK");
						$nameLocal = $this.getAttribute("nickName");
						$picLocal = $this.getAttribute("pic");
						
						if (
							(
								typeof(DbChatPrivate)!="undefined" && 
								DbChatPrivateThreadK!="0" && 
								$threadK!="0" && 
								DbChatPrivateThreadK == $threadK
							) ||
							(
								typeof(DbChatPrivate)!="undefined" && 
								DbChatPrivateUsrK != "0" && 
								^CurrentUsrK != "0" &&
								$privateChatUsrK != "0" && 
								(DbChatPrivateUsrK == $usrK || (^CurrentUsrK == $usrK && DbChatPrivateUsrK == $privateChatUsrK))
							)
						)
						{
							$name = $this.getAttribute("nickName");
							$pic = $this.getAttribute("pic");
							
							^ChatDivPrivate.innerHTML = "<div class=\"ChatName\"><a href=\"/members/"+$name.toLowerCase()+"\" onmouseover=\"stmu('"+$pic+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $name + "</a>:</div><div class=\"ChatMessage\">" + $text + "</div>" + ^ChatDivPrivate.innerHTML;
						}
						else if ($threadK!="0") 
						{
							if ($alert == "0" || $alert == "3")
							{
								$privateMessageAlert = true;
								$privateMessageAlertName = $nameLocal;
								$privateMessageAlertText = $text;
								$privateMessageAlertThreadK = $threadK;
								$privateMessageAlertUsrK = "0";
							}
							if ($alert == "0" || $alert == "2" || $alert == "3")
							{
								$newMessageHtml = "<div class=\"ChatName\">Private chat '"+$subject+"' from <a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a>:</div><div class=\"ChatMessage\"><a href=\"/chat/k-" + $threadK + "\">" + $shortText + "</a></div>" + $newMessageHtml;
								//^PrivateMessages[$threadK] = "<div class=\"ChatName\">"+$subject+"<br><a href=\"/members/"+$name.toLowerCase()+"\" onmouseover=\"stmu('"+$pic+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $name + "</a>:</div><div class=\"ChatMessage\"><a href=\"/chat/k-" + $threadK + "\">" + $shortText + "</a></div>";
							}
						}
						else if ($privateChatUsrK!="0")
						{
							if ($alert == "0" || $alert == "3")
							{
								$privateMessageAlert = true;
								$privateMessageAlertName = $nameLocal;
								$privateMessageAlertText = $text;
								$privateMessageAlertThreadK = "0";
								$privateMessageAlertUsrK = $usrK;
							}
							if (($alert == "0" || $alert == "2" || $alert == "3") && ^CurrentUsrK != $usrK)
							{
								$newMessageHtml = "<div class=\"ChatName\">Private chat from <a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a>:</div><div class=\"ChatMessage\"><a href=\"/members/"+$nameLocal.toLowerCase()+"\">" + $shortText + "</a></div>" + $newMessageHtml;
								//^PrivateMessages["u"+$usrK] = "<div class=\"ChatName\">Message from <a href=\"/members/"+$name.toLowerCase()+"\" onmouseover=\"stmu('"+$pic+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $name + "</a>:</div><div class=\"ChatMessage\"><a href=\"/members/"+$name.toLowerCase()+"\">" + $shortText + "</a></div>";
							}
						}
						document.getElementById("ChatMessages").innerHTML=$k+" messages posted";
					}
					else if ($type=="3")//Comment alert
					{
						var $this = $item.getElementsByTagName("commentAlert").item(0);
						var $nameLocal = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $k = $this.getAttribute("k");
						var $thread = $this.getAttribute("thread");
						var $url = ^NodeText($this);
						var $picLocal = $this.getAttribute("pic");
						var $usrKLocal = $this.getAttribute("usrK");
						$newMessageLastDateTime = $item.getAttribute("dateTime");

						$newMessageHtml = "<div class=\"ChatName\"><a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a>:</div><div class=\"ChatMessage\"><a href=\"" + $url + "\">New "+($thread=="1"?"topic":"comment")+"</a></div>" + $newMessageHtml;
					}
					else if ($type=="4")//Private message alert
					{
						var $this = $item.getElementsByTagName("privateMessageAlert").item(0);
						var $nameLocal = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $k = $this.getAttribute("k");
						var $invite = $this.getAttribute("invite");
						var $pmText = "New private message";
						if ($invite=="1")
							$pmText = "Private message invite";
						var $url = ^NodeText($this);
						var $picLocal = $this.getAttribute("pic");
						var $usrKLocal = $this.getAttribute("usrK");
						$newMessageLastDateTime = $item.getAttribute("dateTime");
						
						$newMessageHtml = "<div class=\"ChatName\"><a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a>:</div><div class=\"ChatMessage\"><a href=\"" + $url + "\">" + $pmText + "</a></div>" + $newMessageHtml;
					}
					else if ($type=="9")//Invite
					{
						var $this = $item.getElementsByTagName("privateMessageAlert").item(0);
						var $nameLocal = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $k = $this.getAttribute("k");
						var $pmText = "New invite";
						var $url = ^NodeText($this);
						var $picLocal = $this.getAttribute("pic");
						var $usrKLocal = $this.getAttribute("usrK");
						$newMessageLastDateTime = $item.getAttribute("dateTime");
						
						$newMessageHtml = "<div class=\"ChatName\"><a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a>:</div><div class=\"ChatMessage\"><a href=\"" + $url + "\">" + $pmText + "</a></div>" + $newMessageHtml;
					}
					else if ($type=="10")//Group news alert
					{
						var $this = $item.getElementsByTagName("groupNewsAlert").item(0);
						var $groupName = $this.getAttribute("groupName");
						var $groupUrl = $this.getAttribute("groupUrl");
						var $subject = $this.getAttribute("subject");
						var $url = ^NodeText($this);
						var $picLocal = $this.getAttribute("pic");
						$pic = $this.getAttribute("pic");
						$picIsPhoto = false;
						$picIsGroup = true;
						$newMessageLastDateTime = $item.getAttribute("dateTime");
						
						$newChatHtml = "<div class=\"ChatName\"><a href=\""+$groupUrl.toLowerCase()+"\" class=\"NameAnchor\">" + $groupName + "</a> news:</div><div class=\"ChatMessage\"><a href=\""+$url+"\">" + $subject + "</a></div>" + $newChatHtml;
					}
					else if ($type=="5")//Lol alert
					{
						var $this = $item.getElementsByTagName("lolAlert").item(0);
						var $nameLocal = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $k = $this.getAttribute("k");
						var $link = ^NodeText($this);
						var $picLocal = $this.getAttribute("pic");
						var $usrKLol = $this.getAttribute("usrK");
						$newMessageLastDateTime = $item.getAttribute("dateTime");
						
						$newMessageHtml = "<div class=\"ChatName\"><a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a> laughed at:</div><div class=\"ChatMessage\">" + $link + "</div>" + $newMessageHtml;
					//	^LolDiv.style.display="";
					//	^LolDiv.innerHTML = "<span class=\"ChatName\"><a href=\"/members/"+$name.toLowerCase()+"\" onmouseover=\"stmu('"+$picLol+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $name + "</a> laughed at:</span> <span class=\"ChatMessage\">" + $link + "</span>";
					}
					else if ($type=="6")//Sign on
					{
						var $this = $item.getElementsByTagName("signOn").item(0);
						var $nameLocal = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $picLocal = $this.getAttribute("pic");
						var $usrKLocal = $this.getAttribute("usrK");
						$newMessageLastDateTime = $item.getAttribute("dateTime");
						
						$newMessageHtml = "<div class=\"ChatAlert\"><a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a> logged on.</div>" + $newMessageHtml;
					}
					else if ($type=="8")//Sign off
					{
						var $this = $item.getElementsByTagName("signOff").item(0);
						var $nameLocal = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $picLocal = $this.getAttribute("pic");
						var $usrKLocal = $this.getAttribute("usrK");
						$newMessageLastDateTime = $item.getAttribute("dateTime");
						
						$newMessageHtml = "<div class=\"ChatAlert\"><a href=\"/members/"+$nameLocal.toLowerCase()+"\" onmouseover=\"stmu('"+$picLocal+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $nameLocal + "</a> logged off.</div>" + $newMessageHtml;
					}
					else if ($type=="7")//Instant photo
					{
						var $this = $item.getElementsByTagName("instantPhoto").item(0);
						$name = $this.getAttribute("nickName");
						var $stmu = $this.getAttribute("stmu");
						var $k = $this.getAttribute("k");
						var $eventName = ^NodeText($this);
						$playSound = true;
						$pic = $this.getAttribute("icon");
						$picIsPhoto = true;
						$picIsGroup = false;
						$picPhotoK = $this.getAttribute("k");
						$usrK = $this.getAttribute("usrK");
						var $usrPic = $this.getAttribute("pic");
						$newChatLastDateTime = $item.getAttribute("dateTime");
						
						$newChatHtml = "<div class=\"ChatName\"><a href=\"/members/"+$name.toLowerCase()+"\" onmouseover=\"stmu('"+$usrPic+"',"+$stmu+");\" onmouseout=\"htm();\" class=\"NameAnchor\">" + $name + "</a>:</div><div class=\"ChatMessage\"><a href=\"/photo-" + $k + "\">New instant photo</a> <small>from " + $eventName + "</small></div>" + $newChatHtml;
					}
					
					if (!DbChatHidePic)
					{
						if ($picIsPhoto && (^CurrentPic != $pic) && ($pic != "null") && ($pic != ""))
						{
							^PicDiv.innerHTML = "<a href=\"/photo-" + $picPhotoK + "\"><img border=\"0\" src=\"" + StoragePath($pic) + "\" style=\"border-right:1px solid black;border-left:1px solid black;\" width=\"100\" height=\"100\"></a>";
							^PicDiv.style.display="";
							^CurrentPic = $pic;
						}
						else if ($picIsGroup && (^CurrentPic != $pic) && ($pic != "null") && ($pic != ""))
						{
							if ( $pic != "0")
								^PicDiv.innerHTML = "<a href=\""+$groupUrl.toLowerCase()+"\"><img border=\"0\" src=\"" + StoragePath($pic) + "\" style=\"border-right:1px solid black;border-left:1px solid black;\" width=\"100\" height=\"100\"></a>";
							else
								^PicDiv.innerHTML = "<a href=\""+$groupUrl.toLowerCase()+"\"><img border=\"0\" src=\"/gfx/dsi-sign-100.png\" style=\"border-right:1px solid black;border-left:1px solid black;\" width=\"100\" height=\"100\"></a>";
							^PicDiv.style.display="";
							^CurrentPic = $pic;
						}
						else
						{
							if ( $type!="2" && (^CurrentPic != $pic) && ($pic != "null") && ($pic != ""))
							{
								if ( $pic != "0")
									^PicDiv.innerHTML = "<a href=\"/members/"+$name.toLowerCase()+"\"><img border=\"0\" src=\"" + StoragePath($pic) + "\" style=\"border-right:1px solid black;border-left:1px solid black;\" width=\"100\" height=\"100\"></a>";
								else
									^PicDiv.innerHTML = "<a href=\"/members/"+$name.toLowerCase()+"\"><img border=\"0\" src=\"/gfx/dsi-sign-100.png\" style=\"border-right:1px solid black;border-left:1px solid black;\" width=\"100\" height=\"100\"></a>";
								^PicDiv.style.display="";
								^CurrentPic = $pic;
							}
						}
					}
					
					if ( 
							typeof(DbChatPrivate)!="undefined" && 
							$type=="2" && 
							(
								(DbChatPrivateThreadK!="0" && DbChatPrivateThreadK == $threadK) || 
								(
									DbChatPrivateUsrK!="0" && 
									^CurrentUsrK!="0" &&
									(DbChatPrivateUsrK == $usrK || (^CurrentUsrK == $usrK && DbChatPrivateUsrK == $privateChatUsrK))
								)
							) && 
							(^CurrentPicPrivate != $pic) && 
							($pic != "null") && 
							($pic != "")
						)
					{
						if ( $pic != "0")
							^PicDivPrivate.innerHTML = "<a href=\"/members/"+$name.toLowerCase()+"\"><img border=\"0\" src=\"" + StoragePath($pic) + "\" style=\"border-right:1px solid black;border-left:1px solid black;\" width=\"100\" height=\"100\"></a>";
						else
							^PicDivPrivate.innerHTML = "<a href=\"/members/"+$name.toLowerCase()+"\"><img border=\"0\" src=\"/gfx/dsi-sign-100.png\" style=\"border-right:1px solid black;border-left:1px solid black;\" width=\"100\" height=\"100\"></a>";
						^PicDivPrivate.style.display="";
						^CurrentPicPrivate = $pic;
					}
				}
				if ($newChatHtml!="")
				{
					var $class = "DbChatOldDiv";
					if (^PreviousChatLastDateTime!="")
					{
						document.getElementById("DbChatNew-" + ^PreviousChatLastDateTime).className="DbChatOldDiv";
						$class = "DbChatNewDiv";
					}
					if ($newChatLastDateTime!="")
						^PreviousChatLastDateTime = $newChatLastDateTime;
					var $notLoggedInHtml = "";
					//if (DbChatLoggedIn==0)
					//{
					//	$notLoggedInHtml = "<div class=\"ChatName\">DontStayIn:</div><div class=\"ChatMessage\">This is a snap-shot of the current chat. When you're registered, it updates in real-time as people chat.</div>";
					//}
					^ChatDiv.innerHTML = "<div id=\"DbChatNew-" + ^PreviousChatLastDateTime + "\">" + $notLoggedInHtml + $newChatHtml + "</div>" + ^ChatDiv.innerHTML;
					document.getElementById("DbChatNew-" + ^PreviousChatLastDateTime).className=$class;
				}
				if ($newMessageHtml!="")
				{
					
					var $class = "DbChatOldDiv";
					if (^PreviousMessageLastDateTime!="")
					{
						document.getElementById("DbChatNewMessage-" + ^PreviousMessageLastDateTime).className="DbChatOldDiv";
						$class = "DbChatNewDiv";
					}
					if ($newMessageLastDateTime!="")
						^PreviousMessageLastDateTime = $newMessageLastDateTime;
						
					^MessageDiv.innerHTML = "<div id=\"DbChatNewMessage-" + ^PreviousMessageLastDateTime + "\">" + $newMessageHtml + "</div>" + ^MessageDiv.innerHTML;
					document.getElementById("DbChatNewMessage-" + ^PreviousMessageLastDateTime).className=$class;
				}
				^PrivateChatSpan.innerHTML = "";
				for (var $privateMessageThreadK in ^PrivateMessages)
				{
					^PrivateChatDiv.style.display="";
					^PrivateChatSpan.innerHTML = ^PrivateMessages[$privateMessageThreadK]+^PrivateChatSpan.innerHTML;
				}
				if ($results=="1")
				{
					^LastGuid = $doc.getAttribute("lastGuid");
					^LastGuidPublic = $doc.getAttribute("lastGuidPublic");
				}
				
				if ($privateMessageAlert && !$firstRequest)
				{
					if ($privateMessageAlertThreadK!="0")
					{
						if(confirm("Private chat message from " + $privateMessageAlertName + ":\n\n" + $privateMessageAlertText + "\n\nDo you want to continue receiving pop-up alerts for this chat?\n(OK = Yes, Cancel = No)"))
						{
							//document.location = [thread $privateMessageAlertThreadK];
						}
						else
						{
							DbChatKillPopup($privateMessageAlertThreadK);
						}
					}
					else if (^CurrentUsrK != $privateMessageAlertUsrK)
					{
						if(confirm("Private chat message from " + $privateMessageAlertName + ":\n\n" + $privateMessageAlertText + "\n\nDo you want to continue receiving pop-up alerts for this buddy?\n(OK = Yes, Cancel = Stop pop-ups for 15 mins)"))
						{
							//document.location = [usr $privateMessageAlertUsrK];
						}
						else
						{
							DbChatKillPopupBuddy($privateMessageAlertUsrK);
						}
					}
				}
				
				if($doc.getAttribute("timeout")=="0" && $doc.getAttribute("wrongSession")=="0")
				{
					document.getElementById("ChatDivBlur").style.display="none";
					document.getElementById("ChatDivStopped").style.display="none";
					document.getElementById("ChatDivMain").style.display="";
					if (typeof(DbChatPrivate)!="undefined")
					{
						document.getElementById("ChatDivBlurPrivate").style.display="none";
						document.getElementById("ChatDivStoppedPrivate").style.display="none";
						document.getElementById("ChatDivMainPrivate").style.display="";
					}
					
					
					if ($gotItems)
					{
						^Timer = 2000;
					}
					else
					{
						^Timer = ^Timer * 1.4;
						if (^Timer>8000)
							^Timer = 8000;
					}
					
					var $currentRequestId = ^CurrentRequestId;
					if ($resetRequestId)
					{
						var $tmp = Math.random();
						$currentRequestId = $tmp;
						^CurrentRequestId = $tmp;
					}
					//if (DbChatLoggedIn==1)
					setTimeout("DbChatRefresh(false, "+$currentRequestId+");",^Timer);
				}
				else if($doc.getAttribute("wrongSession")=="1")
				{
					document.getElementById("ChatDivMain").style.display="none";
					document.getElementById("ChatDivBlur").style.display="";
					if (typeof(DbChatPrivate)!="undefined")
					{
						document.getElementById("ChatDivMainPrivate").style.display="none";
						document.getElementById("ChatDivBlurPrivate").style.display="";
					}
				}
				else if($doc.getAttribute("timeout")=="1")
				{
					document.getElementById("ChatDivMain").style.display="none";
					document.getElementById("ChatDivStopped").style.display="";
					if (typeof(DbChatPrivate)!="undefined")
					{
						document.getElementById("ChatDivMainPrivate").style.display="none";
						document.getElementById("ChatDivStoppedPrivate").style.display="";
					}
				}
				try
				{
					if($doc.getAttribute("exception")!=null && $doc.getAttribute("exception").length>0)
					{
						DbChatDebug("[Exception: "+$doc.getAttribute("exception")+"]");
					}
				}
				catch(ex){}
			}
		}
		catch(ex)
		{
		    DbChatDebug(^GetXML($doc));
			DbChatDebug("[Error: "+ex.message+"]");
			var $currentRequestId = ^CurrentRequestId;
			//if (DbChatLoggedIn==1)
			setTimeout("DbChatRefresh(false, "+$currentRequestId+");",5000);
		}
	}
}
function ^NewPicName($pic){
	return $pic.substr(0,2)+"/"+$pic.substr(2,2)+"/"+$pic;
}
function ^NewPicDomain($pic){
	return "s" + $pic.substr(0,1);
}
function DbChatDebug($str){
	^MessageDiv.innerHTML = "<div>"+$str+"</div>" + ^MessageDiv.innerHTML;
}
function DbChatContinue(){
	document.getElementById("ChatDivStopped").style.display="none";
	document.getElementById("ChatDivBlur").style.display="none";
	document.getElementById("ChatDivMain").style.display="";
	^TextBox.value="";
	if (typeof(DbChatPrivate)!="undefined")
	{
		document.getElementById("ChatDivStoppedPrivate").style.display="none";
		document.getElementById("ChatDivBlurPrivate").style.display="none";
		document.getElementById("ChatDivMainPrivate").style.display="";
		^TextBoxPrivate.value="";
	}
	DbChatSendMessage();
}
function DbChatFocus(){
	if (^TextBox.value=="Enter your message here...")
	{
		^TextBox.value = "";
	}
	^HasFocus = true;
}
function DbChatFocusPrivate(){
	if (^TextBoxPrivate.value=="Enter your message here...")
	{
		^TextBoxPrivate.value = "";
	}
	^HasFocusPrivate = true;
}
function DbChatBlur(){
	^HasFocus = false;
}
function DbChatBlurPrivate(){
	^HasFocusPrivate = false;
}
