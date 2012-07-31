Type.registerNamespace('SpottedScript.Controls.ChatClient.Items');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.IHasPostingUsr
SpottedScript.Controls.ChatClient.Items.IHasPostingUsr = function() { 
};
SpottedScript.Controls.ChatClient.Items.IHasPostingUsr.prototype = {
    get_postingUsrK : null
}
SpottedScript.Controls.ChatClient.Items.IHasPostingUsr.registerInterface('SpottedScript.Controls.ChatClient.Items.IHasPostingUsr');
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.MessagePinLocation
SpottedScript.Controls.ChatClient.Items.MessagePinLocation = function() { 
    /// <field name="afterName" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="afterSubhead" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="afterBody" type="Number" integer="true" static="true">
    /// </field>
};
SpottedScript.Controls.ChatClient.Items.MessagePinLocation.prototype = {
    afterName: 1, 
    afterSubhead: 2, 
    afterBody: 3
}
SpottedScript.Controls.ChatClient.Items.MessagePinLocation.registerEnum('SpottedScript.Controls.ChatClient.Items.MessagePinLocation', false);
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.TopPhoto
SpottedScript.Controls.ChatClient.Items.TopPhoto = function SpottedScript_Controls_ChatClient_Items_TopPhoto(topPhotoStub, parent) {
    /// <param name="topPhotoStub" type="SpottedScript.Controls.ChatClient.Shared.TopPhotoStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <field name="photoK" type="Number" integer="true">
    /// </field>
    /// <field name="photoUrl" type="String">
    /// </field>
    /// <field name="photoIcon" type="String">
    /// </field>
    /// <field name="photoWeb" type="String">
    /// </field>
    /// <field name="photoWebWidth" type="Number" integer="true">
    /// </field>
    /// <field name="photoWebHeight" type="Number" integer="true">
    /// </field>
    /// <field name="photoThumb" type="String">
    /// </field>
    /// <field name="photoThumbWidth" type="Number" integer="true">
    /// </field>
    /// <field name="photoThumbHeight" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.TopPhoto.initializeBase(this, [ topPhotoStub, parent ]);
    this.photoK = topPhotoStub.photoK;
    this.photoUrl = topPhotoStub.photoUrl;
    this.photoIcon = topPhotoStub.photoIcon;
    this.photoWeb = topPhotoStub.photoWeb;
    this.photoWebWidth = topPhotoStub.photoWebWidth;
    this.photoWebHeight = topPhotoStub.photoWebHeight;
    this.photoThumb = topPhotoStub.photoThumb;
    this.photoThumbWidth = topPhotoStub.photoThumbWidth;
    this.photoThumbHeight = topPhotoStub.photoThumbHeight;
}
SpottedScript.Controls.ChatClient.Items.TopPhoto.prototype = {
    photoK: 0,
    photoUrl: null,
    photoIcon: null,
    photoWeb: null,
    photoWebWidth: 0,
    photoWebHeight: 0,
    photoThumb: null,
    photoThumbWidth: 0,
    photoThumbHeight: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.CommentMessage
SpottedScript.Controls.ChatClient.Items.CommentMessage = function SpottedScript_Controls_ChatClient_Items_CommentMessage(commentStub, parent, serverRequestIndex) {
    /// <param name="commentStub" type="SpottedScript.Controls.ChatClient.Shared.CommentMessageStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="subject" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.CommentMessage.initializeBase(this, [ commentStub, parent, serverRequestIndex ]);
    this.url = commentStub.url;
    this.subject = commentStub.subject;
    this.showReadButton = this.url.length > 0;
}
SpottedScript.Controls.ChatClient.Items.CommentMessage.prototype = {
    url: null,
    subject: null,
    getReadButtonUrl: function SpottedScript_Controls_ChatClient_Items_CommentMessage$getReadButtonUrl() {
        /// <returns type="String"></returns>
        return this.url;
    },
    getHtmlAfterBody: function SpottedScript_Controls_ChatClient_Items_CommentMessage$getHtmlAfterBody() {
        /// <returns type="String"></returns>
        return '';
    },
    getSubhead: function SpottedScript_Controls_ChatClient_Items_CommentMessage$getSubhead() {
        /// <returns type="String"></returns>
        if (this.subject.length > 0 && this.url.length > 0) {
            return '<a href=\"' + this.url + '\" class=\"ChatClientCommentMessageSubhead\">' + this.subject + '</a>';
        }
        else {
            return this.subject;
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Alert
SpottedScript.Controls.ChatClient.Items.Alert = function SpottedScript_Controls_ChatClient_Items_Alert(alertStub, parent, serverRequestIndex) {
    /// <param name="alertStub" type="SpottedScript.Controls.ChatClient.Shared.AlertStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="usrK" type="Number" integer="true">
    /// </field>
    /// <field name="_nickName$3" type="String">
    /// </field>
    /// <field name="_stmuParams$3" type="String">
    /// </field>
    /// <field name="_pic$3" type="String">
    /// </field>
    /// <field name="_anyPic$3" type="String">
    /// </field>
    /// <field name="_picID$3" type="String">
    /// </field>
    /// <field name="_pinID$3" type="String">
    /// </field>
    /// <field name="_picElement$3" type="Object" domElement="true">
    /// </field>
    /// <field name="showChatButton" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Alert.initializeBase(this, [ alertStub, parent, serverRequestIndex ]);
    this._nickName$3 = alertStub.nickName;
    this._stmuParams$3 = alertStub.stmuParams;
    this.usrK = alertStub.usrK;
    this._pic$3 = alertStub.pic;
    this._anyPic$3 = (this._pic$3 === '0') ? '00000000-0000-0000-b916-000000000001' : this._pic$3;
    this.set_isInNewSection(false);
    this.set_isTopOfSection(false);
    this.set_isBottomOfSection(false);
    this._picID$3 = this.get_clientID() + '_Pic';
    this._pinID$3 = this.get_clientID() + '_Pin';
}
SpottedScript.Controls.ChatClient.Items.Alert.prototype = {
    usrK: 0,
    _nickName$3: null,
    _stmuParams$3: null,
    _pic$3: null,
    _anyPic$3: null,
    _picID$3: null,
    _pinID$3: null,
    _picElement$3: null,
    showChatButton: false,
    initialiseElements: function SpottedScript_Controls_ChatClient_Items_Alert$initialiseElements() {
        this.initialiseElementsInternal(true);
    },
    initialiseElementsInternal: function SpottedScript_Controls_ChatClient_Items_Alert$initialiseElementsInternal(setElementsInitialisedFlagOnFinish) {
        /// <param name="setElementsInitialisedFlagOnFinish" type="Boolean">
        /// </param>
        SpottedScript.Controls.ChatClient.Items.Alert.callBaseMethod(this, 'initialiseElementsInternal', [ false ]);
        this._picElement$3 = (this.get_showPic()) ? document.getElementById(this._picID$3) : null;
        if (setElementsInitialisedFlagOnFinish) {
            this.elementsInitialised = true;
        }
    },
    updateUI: function SpottedScript_Controls_ChatClient_Items_Alert$updateUI() {
        this._updateMessageHolder$3();
    },
    _updateMessageHolder$3: function SpottedScript_Controls_ChatClient_Items_Alert$_updateMessageHolder$3() {
        if (this.elementsInitialised) {
            var cssClass = 'ChatClientMessageHolder';
            cssClass += (this.get_isInNewSection()) ? ' ChatClientMessageHolderNew' : ' ChatClientMessageHolderOld';
            cssClass += (this.get_isTopOfSection()) ? ' ChatClientMessageHolderTop' : '';
            cssClass += (this.get_isBottomOfSection()) ? ' ChatClientMessageHolderBot' : '';
            cssClass += ' ClearAfter';
            this.itemElement.className = cssClass;
        }
    },
    getHtml: function SpottedScript_Controls_ChatClient_Items_Alert$getHtml() {
        /// <returns type="String"></returns>
        return '';
    },
    get_showPic: function SpottedScript_Controls_ChatClient_Items_Alert$get_showPic() {
        /// <value type="Boolean"></value>
        return true;
    },
    appendHtml: function SpottedScript_Controls_ChatClient_Items_Alert$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        var size = 33;
        sb.append('<div');
        sb.appendAttribute('id', this.get_clientID());
        sb.append(' class=\"ChatClientMessageHolder');
        sb.append((this.get_isInNewSection()) ? ' ChatClientMessageHolderNew' : ' ChatClientMessageHolderOld');
        sb.append((this.get_isTopOfSection()) ? ' ChatClientMessageHolderTop' : '');
        sb.append((this.get_isBottomOfSection()) ? ' ChatClientMessageHolderBot' : '');
        sb.append(' ClearAfter\">');
        if (this.get_showPic()) {
            sb.append('<a');
            sb.appendAttribute('href', '/members/' + this._nickName$3.toLowerCase());
            sb.appendAttribute('onmouseover', 'stmu(\'' + this._pic$3 + '\',' + this._stmuParams$3 + ');');
            sb.appendAttribute('onmouseout', 'htm();');
            sb.append('>');
            sb.append('<img');
            sb.appendAttribute('id', this._picID$3);
            sb.appendAttribute('src', SpottedScript.Misc.getPicUrlFromGuid(this._anyPic$3));
            sb.appendAttribute('width', size.toString());
            sb.appendAttribute('height', size.toString());
            sb.appendAttribute('hspace', '0');
            sb.appendAttribute('class', 'ChatClientMessagePic');
            sb.appendAttribute('align', 'left');
            sb.append(' />');
            sb.append('</a>');
        }
        if (this.showChatButton) {
            sb.append('<div class=\"ChatClientMessageChatButtonHolder\" align=\"right\">');
            if (this.showChatButton) {
                sb.append('<button');
                sb.appendAttribute('class', 'ChatClientMessageChatButton');
                sb.appendAttribute('onclick', 'chatClientPinRoom(\'' + this.roomGuid + '\', null, false);return false;');
                sb.append('>chat</button>');
            }
            sb.append('</div>');
        }
        sb.append('<div class=\"ChatClientMessageHeader\">');
        sb.append('<a');
        sb.appendAttribute('href', '/members/' + this._nickName$3.toLowerCase());
        sb.appendAttribute('onmouseover', 'stmu(\'' + this._pic$3 + '\',' + this._stmuParams$3 + ');');
        sb.appendAttribute('onmouseout', 'htm();');
        sb.append('>');
        sb.append(this._nickName$3);
        sb.append('</a> ');
        sb.append(this.getHtml());
        sb.append('</div>');
        sb.append('</div>');
    },
    get_postingUsrK: function SpottedScript_Controls_ChatClient_Items_Alert$get_postingUsrK() {
        /// <value type="Number" integer="true"></value>
        return this.usrK;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Laugh
SpottedScript.Controls.ChatClient.Items.Laugh = function SpottedScript_Controls_ChatClient_Items_Laugh(laughStub, parent, serverRequestIndex) {
    /// <param name="laughStub" type="SpottedScript.Controls.ChatClient.Shared.LaughStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    SpottedScript.Controls.ChatClient.Items.Laugh.initializeBase(this, [ laughStub, parent, serverRequestIndex ]);
}
SpottedScript.Controls.ChatClient.Items.Laugh.prototype = {
    getHtmlAfterName: function SpottedScript_Controls_ChatClient_Items_Laugh$getHtmlAfterName() {
        /// <returns type="String"></returns>
        return ' laughed at:';
    },
    getRoomGuidForChatClickAction: function SpottedScript_Controls_ChatClient_Items_Laugh$getRoomGuidForChatClickAction() {
        /// <returns type="String"></returns>
        return this._roomGuid$3;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Logout
SpottedScript.Controls.ChatClient.Items.Logout = function SpottedScript_Controls_ChatClient_Items_Logout(alertStub, parent, serverRequestIndex) {
    /// <param name="alertStub" type="SpottedScript.Controls.ChatClient.Shared.AlertStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    SpottedScript.Controls.ChatClient.Items.Logout.initializeBase(this, [ alertStub, parent, serverRequestIndex ]);
}
SpottedScript.Controls.ChatClient.Items.Logout.prototype = {
    getHtml: function SpottedScript_Controls_ChatClient_Items_Logout$getHtml() {
        /// <returns type="String"></returns>
        return 'logged out';
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Login
SpottedScript.Controls.ChatClient.Items.Login = function SpottedScript_Controls_ChatClient_Items_Login(alertStub, parent, serverRequestIndex) {
    /// <param name="alertStub" type="SpottedScript.Controls.ChatClient.Shared.AlertStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    SpottedScript.Controls.ChatClient.Items.Login.initializeBase(this, [ alertStub, parent, serverRequestIndex ]);
}
SpottedScript.Controls.ChatClient.Items.Login.prototype = {
    getHtml: function SpottedScript_Controls_ChatClient_Items_Login$getHtml() {
        /// <returns type="String"></returns>
        return 'logged in';
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Note
SpottedScript.Controls.ChatClient.Items.Note = function SpottedScript_Controls_ChatClient_Items_Note(text, parent, roomGuid, serverRequestIndex) {
    /// <param name="text" type="String">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="_text$2" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Note.initializeBase(this, [ new SpottedScript.Controls.ChatClient.Shared.ItemStub(Math.round(Math.random() * 100000).toString(), SpottedScript.Controls.ChatClient.Shared.ItemType.error, '0', roomGuid), parent, serverRequestIndex ]);
    this._text$2 = text;
}
SpottedScript.Controls.ChatClient.Items.Note.prototype = {
    _text$2: null,
    appendHtml: function SpottedScript_Controls_ChatClient_Items_Note$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<p>NOTE:<br />');
        sb.append(this._text$2);
        sb.append('</p>');
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Newable
SpottedScript.Controls.ChatClient.Items.Newable = function SpottedScript_Controls_ChatClient_Items_Newable(itemStub, parent, serverRequestIndex) {
    /// <param name="itemStub" type="SpottedScript.Controls.ChatClient.Shared.ItemStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="isInNewSection" type="Boolean">
    /// </field>
    /// <field name="isTopOfSection" type="Boolean">
    /// </field>
    /// <field name="isBottomOfSection" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Newable.initializeBase(this, [ itemStub, parent, serverRequestIndex ]);
}
SpottedScript.Controls.ChatClient.Items.Newable.prototype = {
    get_isInNewSection: function SpottedScript_Controls_ChatClient_Items_Newable$get_isInNewSection() {
        /// <value type="Boolean"></value>
        return this.isInNewSection;
    },
    set_isInNewSection: function SpottedScript_Controls_ChatClient_Items_Newable$set_isInNewSection(value) {
        /// <value type="Boolean"></value>
        this.isInNewSection = value;
        this.updateUI();
        return value;
    },
    isInNewSection: false,
    get_isTopOfSection: function SpottedScript_Controls_ChatClient_Items_Newable$get_isTopOfSection() {
        /// <value type="Boolean"></value>
        return this.isTopOfSection;
    },
    set_isTopOfSection: function SpottedScript_Controls_ChatClient_Items_Newable$set_isTopOfSection(value) {
        /// <value type="Boolean"></value>
        this.isTopOfSection = value;
        this.updateUI();
        return value;
    },
    isTopOfSection: false,
    get_isBottomOfSection: function SpottedScript_Controls_ChatClient_Items_Newable$get_isBottomOfSection() {
        /// <value type="Boolean"></value>
        return this.isBottomOfSection;
    },
    set_isBottomOfSection: function SpottedScript_Controls_ChatClient_Items_Newable$set_isBottomOfSection(value) {
        /// <value type="Boolean"></value>
        this.isBottomOfSection = value;
        this.updateUI();
        return value;
    },
    isBottomOfSection: false,
    updateClassModifiersAllAtOnce: function SpottedScript_Controls_ChatClient_Items_Newable$updateClassModifiersAllAtOnce(isTopOfSectionValue, isBottomOfSectionValue, isInNewSectionValue) {
        /// <param name="isTopOfSectionValue" type="Boolean">
        /// </param>
        /// <param name="isBottomOfSectionValue" type="Boolean">
        /// </param>
        /// <param name="isInNewSectionValue" type="Boolean">
        /// </param>
        if (isTopOfSectionValue !== this.get_isTopOfSection() || isBottomOfSectionValue !== this.get_isBottomOfSection() || isInNewSectionValue !== this.get_isInNewSection()) {
            this.isTopOfSection = isTopOfSectionValue;
            this.isBottomOfSection = isBottomOfSectionValue;
            this.isInNewSection = isInNewSectionValue;
            this.updateUI();
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Photo
SpottedScript.Controls.ChatClient.Items.Photo = function SpottedScript_Controls_ChatClient_Items_Photo(photoStub, parent, serverRequestIndex) {
    /// <param name="photoStub" type="SpottedScript.Controls.ChatClient.Shared.PhotoStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="width" type="Number" integer="true">
    /// </field>
    /// <field name="height" type="Number" integer="true">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="web" type="String">
    /// </field>
    /// <field name="icon" type="String">
    /// </field>
    /// <field name="thumb" type="String">
    /// </field>
    /// <field name="thumbWidth" type="Number" integer="true">
    /// </field>
    /// <field name="thumbHeight" type="Number" integer="true">
    /// </field>
    /// <field name="buddyAlert" type="Boolean">
    /// </field>
    /// <field name="_imageID$3" type="String">
    /// </field>
    /// <field name="_imageElement$3" type="Object" domElement="true">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Photo.initializeBase(this, [ photoStub, parent, serverRequestIndex ]);
    this.width = photoStub.width;
    this.height = photoStub.height;
    this.url = photoStub.url;
    this.web = photoStub.web;
    this.icon = photoStub.icon;
    this.thumb = photoStub.thumb;
    this.thumbWidth = photoStub.thumbWidth;
    this.thumbHeight = photoStub.thumbHeight;
    this.buddyAlert = photoStub.buddyAlert;
    this._imageID$3 = this.get_clientID() + '_Image';
}
SpottedScript.Controls.ChatClient.Items.Photo.prototype = {
    width: 0,
    height: 0,
    url: null,
    web: null,
    icon: null,
    thumb: null,
    thumbWidth: 0,
    thumbHeight: 0,
    buddyAlert: false,
    _imageID$3: null,
    _imageElement$3: null,
    initialiseElements: function SpottedScript_Controls_ChatClient_Items_Photo$initialiseElements() {
        this.initialiseElementsInternal(true);
    },
    initialiseElementsInternal: function SpottedScript_Controls_ChatClient_Items_Photo$initialiseElementsInternal(setElementsInitialisedFlagOnFinish) {
        /// <param name="setElementsInitialisedFlagOnFinish" type="Boolean">
        /// </param>
        SpottedScript.Controls.ChatClient.Items.Photo.callBaseMethod(this, 'initialiseElementsInternal', [ false ]);
        this._imageElement$3 = document.getElementById(this._imageID$3);
        if (setElementsInitialisedFlagOnFinish) {
            this.elementsInitialised = true;
        }
    },
    updateUI: function SpottedScript_Controls_ChatClient_Items_Photo$updateUI() {
        this._updateItem$3();
    },
    _updateItem$3: function SpottedScript_Controls_ChatClient_Items_Photo$_updateItem$3() {
        if (this.elementsInitialised) {
        }
    },
    get__size$3: function SpottedScript_Controls_ChatClient_Items_Photo$get__size$3() {
        /// <value type="Number" integer="true"></value>
        return (this.get_isInNewSection()) ? 200 : 50;
    },
    get__top$3: function SpottedScript_Controls_ChatClient_Items_Photo$get__top$3() {
        /// <value type="Number" integer="true"></value>
        return (this.get__size$3() - 300) / 2;
    },
    get__left$3: function SpottedScript_Controls_ChatClient_Items_Photo$get__left$3() {
        /// <value type="Number" integer="true"></value>
        return 0;
    },
    appendHtml: function SpottedScript_Controls_ChatClient_Items_Photo$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<div class=\"ChatMessagePhotoHolder\"');
        sb.appendAttribute('id', this.get_clientID());
        sb.append('>');
        sb.append('<a');
        sb.appendAttribute('href', this.url);
        sb.appendAttribute('onclick', 'event.cancelBubble = true; if (event.stopPropagation) { event.stopPropagation(); } document.location = \"' + this.url + '\";return false;');
        sb.append('>');
        sb.append('<img');
        sb.appendAttribute('id', this._imageID$3);
        sb.appendAttribute('src', SpottedScript.Misc.getPicUrlFromGuid(this.thumb));
        sb.appendAttribute('width', this.thumbWidth.toString());
        sb.appendAttribute('height', this.thumbHeight.toString());
        sb.appendAttribute('class', 'ChatClientPhotoImage');
        sb.appendAttribute('style', 'top:' + this.get__top$3().toString() + 'px; left: ' + this.get__left$3().toString() + 'px;');
        sb.appendAttribute('border', '0');
        sb.append(' />');
        sb.append('</a>');
        sb.append('</div>');
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Error
SpottedScript.Controls.ChatClient.Items.Error = function SpottedScript_Controls_ChatClient_Items_Error(err, method, parent, roomGuid, serverRequestIndex) {
    /// <param name="err" type="Sys.Net.WebServiceError">
    /// </param>
    /// <param name="method" type="String">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="_method$2" type="String">
    /// </field>
    /// <field name="_exceptionType$2" type="String">
    /// </field>
    /// <field name="_message$2" type="String">
    /// </field>
    /// <field name="_stackTrace$2" type="String">
    /// </field>
    /// <field name="_statusCode$2" type="Number" integer="true">
    /// </field>
    /// <field name="_timedOut$2" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Error.initializeBase(this, [ new SpottedScript.Controls.ChatClient.Shared.ItemStub(Math.round(Math.random() * 100000).toString(), SpottedScript.Controls.ChatClient.Shared.ItemType.error, '0', roomGuid), parent, serverRequestIndex ]);
    this._method$2 = method;
    this._exceptionType$2 = err.get_exceptionType();
    this._message$2 = err.get_message();
    this._stackTrace$2 = err.get_stackTrace();
    this._timedOut$2 = err.get_timedOut();
    this._statusCode$2 = err.get_statusCode();
}
SpottedScript.Controls.ChatClient.Items.Error.prototype = {
    _method$2: null,
    _exceptionType$2: null,
    _message$2: null,
    _stackTrace$2: null,
    _statusCode$2: 0,
    _timedOut$2: false,
    appendHtml: function SpottedScript_Controls_ChatClient_Items_Error$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<p>ERROR during ' + this._method$2 + ':<br />');
        if (this._exceptionType$2.length > 0) {
            sb.append('<small>' + this._exceptionType$2 + '</small><br />');
        }
        if (this._message$2.length > 0) {
            sb.append(this._message$2 + '<br />');
        }
        if (this._statusCode$2 !== 0) {
            sb.append('<small>Status: ' + this._statusCode$2.toString() + '</small><br />');
        }
        if (this._timedOut$2) {
            sb.append('<small>(timed out)</small>');
        }
        sb.append('</p>');
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Html
SpottedScript.Controls.ChatClient.Items.Html = function SpottedScript_Controls_ChatClient_Items_Html(itemStub, parent, serverRequestIndex) {
    /// <param name="itemStub" type="SpottedScript.Controls.ChatClient.Shared.ItemStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="instance" type="Number" integer="true">
    /// </field>
    /// <field name="itemElement" type="Object" domElement="true">
    /// </field>
    /// <field name="elementsInitialised" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Html.initializeBase(this, [ itemStub, parent ]);
    this.elementsInitialised = false;
    this.serverRequestIndex = serverRequestIndex;
}
SpottedScript.Controls.ChatClient.Items.Html.prototype = {
    instance: 0,
    itemElement: null,
    elementsInitialised: false,
    get_clientID: function SpottedScript_Controls_ChatClient_Items_Html$get_clientID() {
        /// <value type="String"></value>
        return this.parent.clientID + '_ServerRequestIndex_' + this.serverRequestIndex + '_Item_' + this.guid + '_Instance_' + this.instance;
    },
    getRoomGuidForChatClickAction: function SpottedScript_Controls_ChatClient_Items_Html$getRoomGuidForChatClickAction() {
        /// <returns type="String"></returns>
        return this.roomGuid;
    },
    get_isElementsInitialised: function SpottedScript_Controls_ChatClient_Items_Html$get_isElementsInitialised() {
        /// <value type="Boolean"></value>
        return this.elementsInitialised;
    },
    initialiseElements: function SpottedScript_Controls_ChatClient_Items_Html$initialiseElements() {
        this.initialiseElementsInternal(true);
    },
    initialiseElementsInternal: function SpottedScript_Controls_ChatClient_Items_Html$initialiseElementsInternal(setElementsInitialisedFlagOnFinish) {
        /// <param name="setElementsInitialisedFlagOnFinish" type="Boolean">
        /// </param>
        this.itemElement = document.getElementById(this.get_clientID());
        if (setElementsInitialisedFlagOnFinish) {
            this.elementsInitialised = true;
        }
    },
    toHtml: function SpottedScript_Controls_ChatClient_Items_Html$toHtml() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        this.appendHtml(sb);
        return sb.toString();
    },
    appendHtml: function SpottedScript_Controls_ChatClient_Items_Html$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<p>');
        sb.append(SpottedScript.Controls.ChatClient.Shared.ItemType.toString(this.type));
        sb.append(' - ');
        sb.append(this.guid);
        sb.append('</p>');
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Item
SpottedScript.Controls.ChatClient.Items.Item = function SpottedScript_Controls_ChatClient_Items_Item(itemStub, parent) {
    /// <param name="itemStub" type="SpottedScript.Controls.ChatClient.Shared.ItemStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <field name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </field>
    /// <field name="guid" type="String">
    /// </field>
    /// <field name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </field>
    /// <field name="_dateTime" type="String">
    /// </field>
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="serverRequestIndex" type="Number" integer="true">
    /// This tells us which server request this item was downloaded in (for batch ui updates)
    /// </field>
    /// <field name="fromGuestRefresh" type="Boolean">
    /// </field>
    /// <field name="_instance" type="Number" integer="true">
    /// </field>
    this.parent = parent;
    this.guid = itemStub.guid;
    this.type = itemStub.type;
    this._dateTime = itemStub.dateTime;
    this.roomGuid = itemStub.roomGuid;
    this.fromGuestRefresh = false;
}
SpottedScript.Controls.ChatClient.Items.Item.create = function SpottedScript_Controls_ChatClient_Items_Item$create(itemStub, parent, serverRequestIndex, fromGuestRefresh, instance) {
    /// <param name="itemStub" type="SpottedScript.Controls.ChatClient.Shared.ItemStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <param name="fromGuestRefresh" type="Boolean">
    /// </param>
    /// <param name="instance" type="Number" integer="true">
    /// </param>
    /// <returns type="SpottedScript.Controls.ChatClient.Items.Item"></returns>
    var type = itemStub.type;
    var item;
    if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.loginAlert) {
        item = new SpottedScript.Controls.ChatClient.Items.Login(itemStub, parent, serverRequestIndex);
    }
    else if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.logoutAlert) {
        item = new SpottedScript.Controls.ChatClient.Items.Logout(itemStub, parent, serverRequestIndex);
    }
    else if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.laughAlert) {
        item = new SpottedScript.Controls.ChatClient.Items.Laugh(itemStub, parent, serverRequestIndex);
    }
    else if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.privateChatMessage) {
        item = new SpottedScript.Controls.ChatClient.Items.Private(itemStub, parent, serverRequestIndex);
    }
    else if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.commentChatMessage) {
        item = new SpottedScript.Controls.ChatClient.Items.CommentMessage(itemStub, parent, serverRequestIndex);
    }
    else if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.publicChatMessage) {
        item = new SpottedScript.Controls.ChatClient.Items.Message(itemStub, parent, serverRequestIndex);
    }
    else if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.photoAlert) {
        item = new SpottedScript.Controls.ChatClient.Items.Photo(itemStub, parent, serverRequestIndex);
    }
    else if (type === SpottedScript.Controls.ChatClient.Shared.ItemType.topPhoto) {
        item = new SpottedScript.Controls.ChatClient.Items.TopPhoto(itemStub, parent);
    }
    else {
        item = new SpottedScript.Controls.ChatClient.Items.Item(itemStub, parent);
    }
    item._instance = instance;
    item.serverRequestIndex = serverRequestIndex;
    item.fromGuestRefresh = fromGuestRefresh;
    return item;
}
SpottedScript.Controls.ChatClient.Items.Item.prototype = {
    parent: null,
    guid: null,
    type: 0,
    _dateTime: null,
    roomGuid: null,
    serverRequestIndex: 0,
    fromGuestRefresh: false,
    _instance: 0,
    getAgeInMinutes: function SpottedScript_Controls_ChatClient_Items_Item$getAgeInMinutes() {
        /// <returns type="Number" integer="true"></returns>
        return Math.floor(this.parent.getMessageAge(this._dateTime) / 60000);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Private
SpottedScript.Controls.ChatClient.Items.Private = function SpottedScript_Controls_ChatClient_Items_Private(privateStub, parent, serverRequestIndex) {
    /// <param name="privateStub" type="SpottedScript.Controls.ChatClient.Shared.PrivateStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="buddy" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Private.initializeBase(this, [ privateStub, parent, serverRequestIndex ]);
    this.buddy = privateStub.buddy;
}
SpottedScript.Controls.ChatClient.Items.Private.prototype = {
    buddy: false
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Items.Message
SpottedScript.Controls.ChatClient.Items.Message = function SpottedScript_Controls_ChatClient_Items_Message(messageStub, parent, serverRequestIndex) {
    /// <param name="messageStub" type="SpottedScript.Controls.ChatClient.Shared.MessageStub">
    /// </param>
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="usrK" type="Number" integer="true">
    /// </field>
    /// <field name="nickName" type="String">
    /// </field>
    /// <field name="stmuParams" type="String">
    /// </field>
    /// <field name="_pic$3" type="String">
    /// </field>
    /// <field name="_chatPic$3" type="String">
    /// </field>
    /// <field name="_text$3" type="String">
    /// </field>
    /// <field name="_pinRoomGuid$3" type="String">
    /// </field>
    /// <field name="_roomGuid$3" type="String">
    /// </field>
    /// <field name="_anyPic$3" type="String">
    /// </field>
    /// <field name="anyChatPic" type="String">
    /// </field>
    /// <field name="_picID$3" type="String">
    /// </field>
    /// <field name="_pinID$3" type="String">
    /// </field>
    /// <field name="_messageBodyID$3" type="String">
    /// </field>
    /// <field name="_picElement$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_messageBodyElement$3" type="Object" domElement="true">
    /// </field>
    /// <field name="showChatButton" type="Boolean">
    /// </field>
    /// <field name="showReadButton" type="Boolean">
    /// </field>
    /// <field name="showSubHead" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Items.Message.initializeBase(this, [ messageStub, parent, serverRequestIndex ]);
    this.nickName = messageStub.nickName;
    this.stmuParams = messageStub.stmuParams;
    this.usrK = messageStub.usrK;
    this._pic$3 = messageStub.pic;
    this._chatPic$3 = messageStub.chatPic;
    this._text$3 = messageStub.text;
    this._pinRoomGuid$3 = messageStub.pinRoomGuid;
    this._roomGuid$3 = messageStub.roomGuid;
    this._anyPic$3 = (this._pic$3 === '0') ? '00000000-0000-0000-b916-000000000001' : this._pic$3;
    this.anyChatPic = (this._chatPic$3 === '0') ? '00000000-0000-0000-b916-000000000005' : this._chatPic$3;
    this.set_isInNewSection(false);
    this.set_isTopOfSection(false);
    this.set_isBottomOfSection(false);
    this._picID$3 = this.get_clientID() + '_Pic';
    this._pinID$3 = this.get_clientID() + '_RoomPin';
    this._messageBodyID$3 = this.get_clientID() + '_MessageBody';
}
SpottedScript.Controls.ChatClient.Items.Message.prototype = {
    usrK: 0,
    nickName: null,
    stmuParams: null,
    _pic$3: null,
    _chatPic$3: null,
    _text$3: null,
    _pinRoomGuid$3: null,
    _roomGuid$3: null,
    _anyPic$3: null,
    anyChatPic: null,
    _picID$3: null,
    _pinID$3: null,
    _messageBodyID$3: null,
    _picElement$3: null,
    _messageBodyElement$3: null,
    showChatButton: false,
    showReadButton: false,
    showSubHead: false,
    initialiseElements: function SpottedScript_Controls_ChatClient_Items_Message$initialiseElements() {
        this.initialiseElementsInternal(true);
    },
    initialiseElementsInternal: function SpottedScript_Controls_ChatClient_Items_Message$initialiseElementsInternal(setElementsInitialisedFlagOnFinish) {
        /// <param name="setElementsInitialisedFlagOnFinish" type="Boolean">
        /// </param>
        SpottedScript.Controls.ChatClient.Items.Message.callBaseMethod(this, 'initialiseElementsInternal', [ false ]);
        this._picElement$3 = document.getElementById(this._picID$3);
        this._messageBodyElement$3 = document.getElementById(this._messageBodyID$3);
        if (setElementsInitialisedFlagOnFinish) {
            this.elementsInitialised = true;
        }
    },
    updateUI: function SpottedScript_Controls_ChatClient_Items_Message$updateUI() {
        this._updateMessageHolder$3();
    },
    _updateMessageHolder$3: function SpottedScript_Controls_ChatClient_Items_Message$_updateMessageHolder$3() {
        if (this.elementsInitialised) {
            var cssClass = 'ChatClientMessageHolder';
            cssClass += (this.get_isInNewSection()) ? ' ChatClientMessageHolderNew' : ' ChatClientMessageHolderOld';
            cssClass += (this.get_isTopOfSection()) ? ' ChatClientMessageHolderTop' : '';
            cssClass += (this.get_isBottomOfSection()) ? ' ChatClientMessageHolderBot' : '';
            cssClass += ' ClearAfter';
            this.itemElement.className = cssClass;
        }
    },
    getReadButtonUrl: function SpottedScript_Controls_ChatClient_Items_Message$getReadButtonUrl() {
        /// <returns type="String"></returns>
        return '';
    },
    getHtmlAfterBody: function SpottedScript_Controls_ChatClient_Items_Message$getHtmlAfterBody() {
        /// <returns type="String"></returns>
        return '';
    },
    getHtmlAfterName: function SpottedScript_Controls_ChatClient_Items_Message$getHtmlAfterName() {
        /// <returns type="String"></returns>
        return '';
    },
    getSubhead: function SpottedScript_Controls_ChatClient_Items_Message$getSubhead() {
        /// <returns type="String"></returns>
        return '';
    },
    getRoomGuidForChatClickAction: function SpottedScript_Controls_ChatClient_Items_Message$getRoomGuidForChatClickAction() {
        /// <returns type="String"></returns>
        return (this._pinRoomGuid$3.length === 0) ? this._roomGuid$3 : this._pinRoomGuid$3;
    },
    appendHtml: function SpottedScript_Controls_ChatClient_Items_Message$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        var size = 33;
        sb.append('<div');
        sb.appendAttribute('id', this.get_clientID());
        sb.append(' class=\"ChatClientMessageHolder');
        sb.append((this.get_isInNewSection()) ? ' ChatClientMessageHolderNew' : ' ChatClientMessageHolderOld');
        sb.append((this.get_isTopOfSection()) ? ' ChatClientMessageHolderTop' : '');
        sb.append((this.get_isBottomOfSection()) ? ' ChatClientMessageHolderBot' : '');
        sb.append(' ClearAfter\">');
        sb.append('<a');
        sb.appendAttribute('href', '/members/' + this.nickName.toLowerCase());
        sb.appendAttribute('onmouseover', 'stmu(\'' + this._pic$3 + '\',' + this.stmuParams + ');');
        sb.appendAttribute('onmouseout', 'htm();');
        sb.append('>');
        sb.append('<img');
        sb.appendAttribute('id', this._picID$3);
        sb.appendAttribute('src', SpottedScript.Misc.getPicUrlFromGuid(this._anyPic$3));
        sb.appendAttribute('width', size.toString());
        sb.appendAttribute('height', size.toString());
        sb.appendAttribute('hspace', '0');
        sb.appendAttribute('class', 'ChatClientMessagePic');
        sb.appendAttribute('align', 'left');
        sb.append(' />');
        sb.append('</a>');
        if (this.showChatButton || this.showReadButton) {
            sb.append('<div class=\"ChatClientMessageChatButtonHolder\" align=\"right\">');
            if (this.showChatButton) {
                sb.append('<button');
                sb.appendAttribute('class', 'ChatClientMessageChatButton');
                sb.appendAttribute('onclick', 'chatClientPinRoom(\'' + this.getRoomGuidForChatClickAction() + '\', null, false);return false;');
                sb.append('>chat</button>');
            }
            if (this.showChatButton && this.showReadButton) {
                sb.append('<br />');
            }
            if (this.showReadButton) {
                sb.append('<button');
                sb.appendAttribute('class', 'ChatClientMessageChatButton');
                sb.appendAttribute('onclick', 'event.cancelBubble = true; if (event.stopPropagation) { event.stopPropagation(); } document.location = \"' + this.getReadButtonUrl() + '\";return false;');
                sb.append('>read</button>');
            }
            sb.append('</div>');
        }
        sb.append('<div class=\"ChatClientMessageHeader\">');
        sb.append('<a');
        sb.appendAttribute('href', '/members/' + this.nickName.toLowerCase());
        sb.appendAttribute('onmouseover', 'stmu(\'' + this._pic$3 + '\',' + this.stmuParams + ');');
        sb.appendAttribute('onmouseout', 'htm();');
        sb.append('>');
        sb.append(this.nickName);
        sb.append('</a>');
        var htmlAfterName = this.getHtmlAfterName();
        if (this._roomGuid$3 !== 'AQEFAAAAAAUAAAAAvVaVmQ') {
            var age = this.getAgeInMinutes();
            if (age >= 5) {
                sb.append(' (');
                if (age > 525600) {
                    var years = Math.floor(age / 525600);
                    sb.append(years.toString());
                    sb.append(' yr');
                    sb.append((years === 1) ? '' : 's');
                }
                else if (age > 43200) {
                    var months = Math.floor(age / 43200);
                    sb.append(months.toString());
                    sb.append(' month');
                    sb.append((months === 1) ? '' : 's');
                }
                else if (age > 10080) {
                    var weeks = Math.floor(age / 10080);
                    sb.append(weeks.toString());
                    sb.append(' wk');
                    sb.append((weeks === 1) ? '' : 's');
                }
                else if (age > 1440) {
                    var days = Math.floor(age / 1440);
                    sb.append(days.toString());
                    sb.append(' day');
                    sb.append((days === 1) ? '' : 's');
                }
                else if (age > 60) {
                    var hrs = Math.floor(age / 60);
                    sb.append(hrs.toString());
                    sb.append(' hr');
                    sb.append((hrs === 1) ? '' : 's');
                }
                else {
                    sb.append(age.toString());
                    sb.append(' min');
                    sb.append((age === 1) ? '' : 's');
                }
                sb.append(' ago) ');
            }
        }
        if (htmlAfterName.length > 0) {
            sb.append(htmlAfterName);
        }
        sb.append('</div>');
        if (this.showSubHead && this.getSubhead().length > 0) {
            sb.append('<div');
            sb.append(' class=\"ChatClientMessageSubHead\">');
            sb.append(this.getSubhead());
            sb.append('</div>');
        }
        sb.append('<div');
        sb.appendAttribute('id', this._messageBodyID$3);
        sb.append(' class=\"ChatClientMessageBody\">');
        sb.append(this._text$3);
        sb.append(this.getHtmlAfterBody());
        sb.append('</div>');
        sb.append('</div>');
    },
    get_postingUsrK: function SpottedScript_Controls_ChatClient_Items_Message$get_postingUsrK() {
        /// <value type="Number" integer="true"></value>
        return this.usrK;
    }
}
Type.registerNamespace('SpottedScript.Controls.ChatClient');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.PopupArea
SpottedScript.Controls.ChatClient.PopupArea = function SpottedScript_Controls_ChatClient_PopupArea(view) {
    /// <param name="view" type="SpottedScript.Controls.ChatClient.View">
    /// </param>
    /// <field name="popups" type="Array" elementType="Popup">
    /// </field>
    /// <field name="_areaHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="popupWidth" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="popupHeight" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="animate" type="Boolean">
    /// </field>
    /// <field name="_mouseIsOverPopup" type="Boolean">
    /// </field>
    /// <field name="_cancelMouseOut" type="Boolean">
    /// </field>
    this.popups = [];
    this.animate = Boolean.parse(view.get_initAnimatePopups().value);
    this._areaHolder = document.getElementById('ChatClientPopupAreaHolder');
    this._areaHolder.style.width = SpottedScript.Controls.ChatClient.PopupArea.popupWidth.toString() + 'px';
}
SpottedScript.Controls.ChatClient.PopupArea.prototype = {
    popups: null,
    _areaHolder: null,
    animate: false,
    add: function SpottedScript_Controls_ChatClient_PopupArea$add(popup) {
        /// <param name="popup" type="SpottedScript.Controls.ChatClient.Popup">
        /// </param>
        popup.area = this;
        var positionIndex = this.popups.length;
        this.popups[positionIndex] = popup;
        this._areaHolder.appendChild(popup.holder);
        $addHandler(popup.holder, 'mouseover', Function.createDelegate(this, this._onMouseOver));
        $addHandler(popup.holder, 'mouseout', Function.createDelegate(this, this._onMouseOut));
        popup.repositionImmediate(positionIndex);
        popup.show();
        popup.setRemoveTimeout(10000);
    },
    _mouseIsOverPopup: false,
    _cancelMouseOut: false,
    remove: function SpottedScript_Controls_ChatClient_PopupArea$remove(popup, force, forceNoAnimation) {
        /// <param name="popup" type="SpottedScript.Controls.ChatClient.Popup">
        /// </param>
        /// <param name="force" type="Boolean">
        /// </param>
        /// <param name="forceNoAnimation" type="Boolean">
        /// </param>
        if (!this._mouseIsOverPopup || force) {
            this._removeNow(popup, forceNoAnimation);
        }
    },
    _removeNow: function SpottedScript_Controls_ChatClient_PopupArea$_removeNow(popup, forceNoAnimation) {
        /// <param name="popup" type="SpottedScript.Controls.ChatClient.Popup">
        /// </param>
        /// <param name="forceNoAnimation" type="Boolean">
        /// </param>
        if (!popup.removed) {
            popup.hide(forceNoAnimation);
            popup.removed = true;
            var popupsNew = [];
            for (var i = 0; i < this.popups.length; i++) {
                if (this.popups[i].id !== popup.id) {
                    popupsNew[popupsNew.length] = this.popups[i];
                }
            }
            this.popups = popupsNew;
            this._repositionPopups();
        }
    },
    _removeAllThatNeedRemoval: function SpottedScript_Controls_ChatClient_PopupArea$_removeAllThatNeedRemoval() {
        var popupsNew = [];
        for (var i = 0; i < this.popups.length; i++) {
            if (this.popups[i].needsRemoval) {
                this.popups[i].hide(false);
            }
            else {
                popupsNew[popupsNew.length] = this.popups[i];
            }
        }
        this.popups = popupsNew;
        this._repositionPopups();
    },
    _onMouseOver: function SpottedScript_Controls_ChatClient_PopupArea$_onMouseOver(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._mouseIsOverPopup = true;
        this._cancelMouseOut = true;
    },
    _onMouseOut: function SpottedScript_Controls_ChatClient_PopupArea$_onMouseOut(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._cancelMouseOut = false;
        window.setTimeout(Function.createDelegate(this, this._onMouseOutAfterDelay), 100);
    },
    _onMouseOutAfterDelay: function SpottedScript_Controls_ChatClient_PopupArea$_onMouseOutAfterDelay() {
        if (!this._cancelMouseOut) {
            this._mouseIsOverPopup = false;
            this._mouseOut();
        }
    },
    _mouseOut: function SpottedScript_Controls_ChatClient_PopupArea$_mouseOut() {
        this._removeAllThatNeedRemoval();
    },
    _repositionPopups: function SpottedScript_Controls_ChatClient_PopupArea$_repositionPopups() {
        for (var i = 0; i < this.popups.length; i++) {
            this.popups[i].repositionSlide(i);
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Popup
SpottedScript.Controls.ChatClient.Popup = function SpottedScript_Controls_ChatClient_Popup(controller, title, room, items) {
    /// <param name="controller" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="title" type="String">
    /// </param>
    /// <param name="room" type="SpottedScript.Controls.ChatClient.Room">
    /// </param>
    /// <param name="items" type="Array" elementType="Html">
    /// </param>
    /// <field name="title" type="String">
    /// </field>
    /// <field name="holder" type="Object" domElement="true">
    /// </field>
    /// <field name="itemsList" type="Object" domElement="true">
    /// </field>
    /// <field name="clickAction" type="Sys.EventHandler">
    /// </field>
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="area" type="SpottedScript.Controls.ChatClient.PopupArea">
    /// </field>
    /// <field name="id" type="String">
    /// </field>
    /// <field name="_jHolder" type="JQ.JQueryObject">
    /// </field>
    /// <field name="needsRemoval" type="Boolean">
    /// </field>
    /// <field name="removed" type="Boolean">
    /// </field>
    /// <field name="roomGuidForClickAction" type="String">
    /// </field>
    /// <field name="hasRelevantItems" type="Boolean">
    /// </field>
    /// <field name="_itemsListCancelMouseOut" type="Boolean">
    /// </field>
    this.title = title;
    this.roomGuid = room.guid;
    this.removed = false;
    this.roomGuidForClickAction = room.guid;
    this.holder = document.createElement('div');
    this.holder.className = 'ChatClientPopup';
    this.holder.style.width = SpottedScript.Controls.ChatClient.PopupArea.popupWidth.toString() + 'px';
    this.holder.style.height = SpottedScript.Controls.ChatClient.PopupArea.popupHeight.toString() + 'px';
    var header = document.createElement('div');
    header.className = 'ChatClientPopupHeader';
    header.style.overflow = 'hidden';
    var div = document.createElement('div');
    div.className = 'ChatClientPopupCloseLinkHolder';
    var link = document.createElement('a');
    link.innerHTML = 'Close';
    link.href = '';
    $addHandler(link, 'click', Function.createDelegate(this, this._closeButtonClick));
    div.appendChild(link);
    header.appendChild(div);
    var div = document.createElement('div');
    div.className = 'ChatClientPopupTitle';
    div.innerHTML = this.title;
    header.appendChild(div);
    this.holder.appendChild(header);
    if (items != null) {
        this.itemsList = document.createElement('div');
        this.itemsList.className = 'ChatClientPopupItemsList';
        $addHandler(this.itemsList, 'click', Function.createDelegate(this, this._holderClick));
        $addHandler(this.itemsList, 'mouseover', Function.createDelegate(this, this._holderMouseOver));
        $addHandler(this.itemsList, 'mouseout', Function.createDelegate(this, this._holderMouseOut));
        var hasMultipleRelevantItems = false;
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (SpottedScript.Controls.ChatClient.Items.IHasPostingUsr.isInstanceOfType(item)) {
                if ((item).get_postingUsrK() === controller.usrK) {
                    continue;
                }
            }
            if (this.hasRelevantItems) {
                hasMultipleRelevantItems = true;
                break;
            }
            this.hasRelevantItems = true;
        }
        if (this.hasRelevantItems) {
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                if (SpottedScript.Controls.ChatClient.Items.IHasPostingUsr.isInstanceOfType(item)) {
                    if ((item).get_postingUsrK() === controller.usrK) {
                        continue;
                    }
                }
                if (!hasMultipleRelevantItems) {
                    this.roomGuidForClickAction = item.getRoomGuidForChatClickAction();
                }
                var previousInstance = item.instance;
                var previousNewStatus = false;
                if (SpottedScript.Controls.ChatClient.Items.Newable.isInstanceOfType(item)) {
                    previousNewStatus = (item).get_isInNewSection();
                    (item).set_isInNewSection(false);
                }
                item.instance = 2;
                var previousShowChatButton = false;
                if (!hasMultipleRelevantItems && SpottedScript.Controls.ChatClient.Items.Message.isInstanceOfType(item)) {
                    previousShowChatButton = (item).showChatButton;
                    (item).showChatButton = false;
                }
                var itemNode = document.createElement('span');
                itemNode.innerHTML = item.toHtml();
                if (this.itemsList.hasChildNodes()) {
                    this.itemsList.insertBefore(itemNode, this.itemsList.childNodes[0]);
                }
                else {
                    this.itemsList.appendChild(itemNode);
                }
                item.instance = previousInstance;
                if (SpottedScript.Controls.ChatClient.Items.Newable.isInstanceOfType(item)) {
                    (item).set_isInNewSection(previousNewStatus);
                }
                if (!hasMultipleRelevantItems && SpottedScript.Controls.ChatClient.Items.Message.isInstanceOfType(item)) {
                    (item).showChatButton = previousShowChatButton;
                }
            }
            this.holder.appendChild(this.itemsList);
        }
    }
    this.id = Math.random().toString();
    this._jHolder = jQuery(this.holder);
}
SpottedScript.Controls.ChatClient.Popup.prototype = {
    title: null,
    holder: null,
    itemsList: null,
    clickAction: null,
    roomGuid: null,
    area: null,
    id: null,
    _jHolder: null,
    needsRemoval: false,
    removed: false,
    roomGuidForClickAction: null,
    hasRelevantItems: false,
    setRemoveTimeout: function SpottedScript_Controls_ChatClient_Popup$setRemoveTimeout(timeout) {
        /// <param name="timeout" type="Number" integer="true">
        /// </param>
        window.setTimeout(Function.createDelegate(this, this._removeAfterTimeout), timeout);
    },
    _removeAfterTimeout: function SpottedScript_Controls_ChatClient_Popup$_removeAfterTimeout() {
        this.needsRemoval = true;
        this.area.remove(this, false, false);
    },
    _getTop: function SpottedScript_Controls_ChatClient_Popup$_getTop(positionIndex) {
        /// <param name="positionIndex" type="Number" integer="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return 0 - ((positionIndex + 1) * SpottedScript.Controls.ChatClient.PopupArea.popupHeight);
    },
    repositionImmediate: function SpottedScript_Controls_ChatClient_Popup$repositionImmediate(positionIndex) {
        /// <param name="positionIndex" type="Number" integer="true">
        /// </param>
        this.holder.style.top = this._getTop(positionIndex).toString() + 'px';
    },
    repositionSlide: function SpottedScript_Controls_ChatClient_Popup$repositionSlide(positionIndex) {
        /// <param name="positionIndex" type="Number" integer="true">
        /// </param>
        if (this.area.animate) {
            var options = {};
            options['top'] = this._getTop(positionIndex).toString() + 'px';
            this._jHolder.animate(options, 500, 'easeOutQuint', null);
        }
        else {
            this.repositionImmediate(positionIndex);
        }
    },
    show: function SpottedScript_Controls_ChatClient_Popup$show() {
        if (this.area.animate) {
            var options = {};
            options['direction'] = 'down';
            options['easing'] = 'easeOutQuint';
            this._jHolder.show('drop', options, 500, null);
        }
        else {
            this.holder.style.display = 'block';
        }
    },
    hide: function SpottedScript_Controls_ChatClient_Popup$hide(forceNoAnimation) {
        /// <param name="forceNoAnimation" type="Boolean">
        /// </param>
        if (this.area.animate && !forceNoAnimation) {
            var options = {};
            options['direction'] = 'down';
            options['easing'] = 'easeOutQuint';
            this._jHolder.hide('drop', options, 500, null);
        }
        else {
            this.holder.style.display = 'none';
        }
    },
    _holderClick: function SpottedScript_Controls_ChatClient_Popup$_holderClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this.area.animate) {
            try {
                var options = {};
                options['to'] = '#ChatClient_MessagesMain';
                this._jHolder.effect('transfer', options, 500, null);
            }
            catch (ex) {
            }
        }
        if (this.clickAction != null) {
            this.clickAction(this.roomGuidForClickAction, null);
        }
        this.area.remove(this, true, true);
        e.preventDefault();
    },
    _holderMouseOver: function SpottedScript_Controls_ChatClient_Popup$_holderMouseOver(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._itemsListCancelMouseOut = true;
        if (this.itemsList != null) {
            this.itemsList.className = 'ChatClientPopupItemsList ChatClientPopupItemsListMouseOver';
        }
        e.preventDefault();
    },
    _holderMouseOut: function SpottedScript_Controls_ChatClient_Popup$_holderMouseOut(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._itemsListCancelMouseOut = false;
        window.setTimeout(Function.createDelegate(this, this._itemsListMouseOutAfterDelay), 0);
        e.preventDefault();
    },
    _itemsListCancelMouseOut: false,
    _itemsListMouseOutAfterDelay: function SpottedScript_Controls_ChatClient_Popup$_itemsListMouseOutAfterDelay() {
        if (this.itemsList != null && !this._itemsListCancelMouseOut) {
            this.itemsList.className = 'ChatClientPopupItemsList';
        }
    },
    _closeButtonClick: function SpottedScript_Controls_ChatClient_Popup$_closeButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this.area.remove(this, true, false);
        e.preventDefault();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Controller
SpottedScript.Controls.ChatClient.Controller = function SpottedScript_Controls_ChatClient_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.ChatClient.View">
    /// </param>
    /// <field name="_sessionID" type="Number" integer="true">
    /// </field>
    /// <field name="_lastActionTicks" type="String">
    /// </field>
    /// <field name="_server" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    /// <field name="_rooms" type="Object">
    /// </field>
    /// <field name="_roomsListOrder" type="Array">
    /// </field>
    /// <field name="_state" type="Array" elementType="StateStub">
    /// </field>
    /// <field name="_lastKeyDown" type="Number" integer="true">
    /// </field>
    /// <field name="_systemMessagesRoomGuid" type="String">
    /// </field>
    /// <field name="_inboxUpdatesRoomGuid" type="String">
    /// </field>
    /// <field name="_buddyStreamRoomGuid" type="String">
    /// </field>
    /// <field name="_publicStreamRoomGuid" type="String">
    /// </field>
    /// <field name="_privateChatRequestsRoomGuid" type="String">
    /// </field>
    /// <field name="_chatClientIsPaused" type="Boolean">
    /// </field>
    /// <field name="_view" type="SpottedScript.Controls.ChatClient.View">
    /// </field>
    /// <field name="_popups" type="SpottedScript.Controls.ChatClient.PopupArea">
    /// </field>
    /// <field name="_serverTicksAtPageLoad" type="Number" integer="true">
    /// </field>
    /// <field name="_clientTicksAtPageLoad" type="Number" integer="true">
    /// </field>
    /// <field name="usrK" type="Number" integer="true">
    /// </field>
    /// <field name="loggedIn" type="Boolean">
    /// </field>
    /// <field name="clientID" type="String">
    /// </field>
    /// <field name="instance" type="SpottedScript.Controls.ChatClient.Controller" static="true">
    /// </field>
    /// <field name="hasFocus" type="Boolean">
    /// </field>
    /// <field name="streamList" type="Object" domElement="true">
    /// </field>
    /// <field name="_selectedRoomGuid" type="String">
    /// </field>
    /// <field name="_previouslySelectedRoomGuid" type="String">
    /// </field>
    /// <field name="_addWatermark" type="Boolean">
    /// </field>
    /// <field name="_loadingIcon" type="Object" domElement="true">
    /// </field>
    this._view = view;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Controls.ChatClient.Controller.prototype = {
    _sessionID: 0,
    _lastActionTicks: null,
    _server: null,
    _rooms: null,
    _roomsListOrder: null,
    _state: null,
    _lastKeyDown: 0,
    _systemMessagesRoomGuid: null,
    _inboxUpdatesRoomGuid: null,
    _buddyStreamRoomGuid: null,
    _publicStreamRoomGuid: null,
    _privateChatRequestsRoomGuid: null,
    _chatClientIsPaused: false,
    _view: null,
    _popups: null,
    _serverTicksAtPageLoad: 0,
    _clientTicksAtPageLoad: 0,
    usrK: 0,
    loggedIn: false,
    clientID: null,
    hasFocus: false,
    streamList: null,
    _getClientTicksSincePageLoad: function SpottedScript_Controls_ChatClient_Controller$_getClientTicksSincePageLoad() {
        /// <returns type="Number" integer="true"></returns>
        var clientTicksNow = this._getRelevantDigits(new Date().getTime().toString());
        var clientTicksSincePageLoad = clientTicksNow - this._clientTicksAtPageLoad;
        return (clientTicksSincePageLoad > 0) ? clientTicksSincePageLoad : 0;
    },
    getMessageAge: function SpottedScript_Controls_ChatClient_Controller$getMessageAge(serverTicksMessageString) {
        /// <param name="serverTicksMessageString" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var serverTicksMessage = this._getRelevantDigits(serverTicksMessageString);
        var serverTicksNow = this._serverTicksAtPageLoad + (this._getClientTicksSincePageLoad() * 10000);
        var messageAge = (serverTicksNow - serverTicksMessage) / 10000;
        return (messageAge > 0) ? messageAge : 0;
    },
    _getRelevantDigits: function SpottedScript_Controls_ChatClient_Controller$_getRelevantDigits(stringIn) {
        /// <param name="stringIn" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return Number.parseInvariant(stringIn);
    },
    _initialise: function SpottedScript_Controls_ChatClient_Controller$_initialise() {
        if (this._view.get_initGo().value === '0') {
            return;
        }
        this._serverTicksAtPageLoad = this._getRelevantDigits(this._view.get_initLastActionTicks().value);
        this._clientTicksAtPageLoad = this._getRelevantDigits(new Date().getTime().toString());
        this.streamList = this._view.get_streamList();
        SpottedScript.Controls.ChatClient.Controller.instance = this;
        this.usrK = Number.parseInvariant(this._view.get_initUsrK().value);
        this.loggedIn = this.usrK > 0;
        this.clientID = this._view.get_initClientID().value;
        this._lastActionTicks = this._view.get_initLastActionTicks().value;
        this._sessionID = Math.round(Math.random() * 10000);
        this._systemMessagesRoomGuid = this._view.get_initSystemMessagesRoomGuid().value;
        this._inboxUpdatesRoomGuid = this._view.get_initInboxUpdatesRoomGuid().value;
        this._buddyStreamRoomGuid = this._view.get_initBuddyStreamRoomGuid().value;
        this._publicStreamRoomGuid = 'ARAFAAAAAAUAAAAANdfH9w';
        this._privateChatRequestsRoomGuid = 'ARIFAAAAAAUAAAAAO3ZY0A';
        this._lastKeyDown = -1;
        this._chatClientIsPaused = false;
        this._state = new Array(0);
        this._roomsListOrder = [];
        this._popups = new SpottedScript.Controls.ChatClient.PopupArea(this._view);
        $addHandler(this._view.get_textBox(), 'focus', Function.createDelegate(this, this._textBoxFocus));
        $addHandler(this._view.get_textBox(), 'blur', Function.createDelegate(this, this._textBoxBlur));
        $addHandler(this._view.get_textBox(), 'keypress', Function.createDelegate(this, this._textBoxKeyPress));
        $addHandler(this._view.get_textBox(), 'keydown', Function.createDelegate(this, this._textBoxKeyDown));
        $addHandler(this._view.get_outerMain(), 'keydown', Function.createDelegate(this, this._outerKeyDown));
        $addHandler(this._view.get_roomsMain(), 'click', Function.createDelegate(this, this._roomListClick));
        $addHandler(this._view.get_roomsMain(), 'mousedown', Function.createDelegate(this, this._roomListMouseDown));
        if (this.loggedIn) {
            $addHandler(this._view.get_privateChatDrop(), 'change', Function.createDelegate(this, this._privateChatDropChange));
        }
        $addHandler(this._view.get_wrongSessionResumeLink(), 'click', Function.createDelegate(this, this._resumeLinkClick));
        $addHandler(this._view.get_timeoutResumeLink(), 'click', Function.createDelegate(this, this._resumeLinkClick));
        $addHandler(this._view.get_deleteArchiveAnchor(), 'click', Function.createDelegate(this, this._deleteArchive));
        this._rooms = {};
        this._selectedRoomGuid = '';
        for (var i = 0; i < this._view.get_roomList().childNodes.length + this._view.get_roomPrivateList().childNodes.length + this._view.get_roomGuestList().childNodes.length; i++) {
            var child = null;
            if (i < this._view.get_roomList().childNodes.length) {
                child = this._view.get_roomList().childNodes[i];
            }
            else if (i < this._view.get_roomList().childNodes.length + this._view.get_roomPrivateList().childNodes.length) {
                child = this._view.get_roomPrivateList().childNodes[i - this._view.get_roomList().childNodes.length];
            }
            else {
                child = this._view.get_roomGuestList().childNodes[i - this._view.get_roomList().childNodes.length - this._view.get_roomPrivateList().childNodes.length];
            }
            if (child.nodeType === 1 && child.className.startsWith('ChatClientRoomHolder')) {
                var r = new SpottedScript.Controls.ChatClient.Room(this, this._view);
                r.initialiseFromElement(child, this._state);
                this._initialiseRoomEvents(r);
                if (r.get_selected()) {
                    if (this._selectedRoomGuid.length > 0) {
                        r.set_selected(false);
                    }
                    else {
                        this._selectedRoomGuid = r.guid;
                    }
                }
                this._rooms[r.guid] = r;
                this._roomsListOrder[this._roomsListOrder.length] = r.guid;
                r.setListOrder(this._roomsListOrder.length - 1);
            }
        }
        if (this._view.get_initTopPhoto().value.length > 0 && this._rooms['AQEFAAAAAAUAAAAAvVaVmQ'] != null) {
            var topPhotoParts = this._view.get_initTopPhoto().value.split(',');
            var p = new SpottedScript.Controls.ChatClient.Items.TopPhoto(new SpottedScript.Controls.ChatClient.Shared.TopPhotoStub('', SpottedScript.Controls.ChatClient.Shared.ItemType.topPhoto, '', 'AQEFAAAAAAUAAAAAvVaVmQ', Number.parseInvariant(topPhotoParts[0]), topPhotoParts[1], topPhotoParts[2], topPhotoParts[3], Number.parseInvariant(topPhotoParts[4]), Number.parseInvariant(topPhotoParts[5]), topPhotoParts[6], Number.parseInvariant(topPhotoParts[7]), Number.parseInvariant(topPhotoParts[8])), this);
            (this._rooms['AQEFAAAAAAUAAAAAvVaVmQ']).addItem(p, null);
        }
        this._updateDraggable();
        this._server = new SpottedScript.Controls.ChatClient.ServerClass(this, this._sessionID, this._lastActionTicks, this._state);
        this._server.gotItems = Function.createDelegate(this, this._gotItems);
        this._server.gotNoItems = Function.createDelegate(this, this._gotNoItems);
        this._server.gotWrongSessionException = Function.createDelegate(this, this._gotWrongSessionException);
        this._server.gotTimeoutException = Function.createDelegate(this, this._gotTimeoutException);
        this._server.gotGenericException = Function.createDelegate(this, this._gotGenericException);
        this._server.gotRoom = Function.createDelegate(this, this._gotRoom);
        this._server.gotNewPhotoRoom = Function.createDelegate(this, this._gotNewPhotoRoom);
        this._server.gotRoomState = Function.createDelegate(this, this._gotRoomState);
        this._server.showLoadingIcon = Function.createDelegate(this, this._showLoadingIcon);
        this._server.hideLoadingIcon = Function.createDelegate(this, this._hideLoadingIcon);
        this._server.gotMoreInfo = Function.createDelegate(this, this._gotRoomMoreInfoHtml);
        this._server.gotArchiveItems = Function.createDelegate(this, this._gotRoomArchiveItems);
        this._server.debugPrint = Function.createDelegate(this, this._debugEventHandler);
        this._server.doneDeleteArchive = Function.createDelegate(this, this._doneDeleteArchive);
        this._server.start();
        if (this._selectedRoomGuid.length === 0 && this._roomsListOrder.length > 0) {
            this.set__selectedRoom(this._rooms[this._roomsListOrder[0]]);
        }
        this._debug('Controller started successfully.');
    },
    _initialiseRoomEvents: function SpottedScript_Controls_ChatClient_Controller$_initialiseRoomEvents(r) {
        /// <param name="r" type="SpottedScript.Controls.ChatClient.Room">
        /// </param>
        r.roomPinAction = Function.createDelegate(this, this._roomPinAction);
        r.roomStarAction = Function.createDelegate(this, this._roomStarAction);
        r.getMoreInfoHtml = Function.createDelegate(this, this._getRoomMoreInfoHtml);
        r.getArchiveItems = Function.createDelegate(this, this._getRoomArchiveItems);
        if (r.get_guest()) {
            r.guestRoomPinAction = Function.createDelegate(this, this._guestRoomPinAction);
        }
    },
    _updateDraggable: function SpottedScript_Controls_ChatClient_Controller$_updateDraggable() {
    },
    chatClientUpdateRoomOrder: function SpottedScript_Controls_ChatClient_Controller$chatClientUpdateRoomOrder(e, ui) {
        /// <param name="e" type="Object">
        /// </param>
        /// <param name="ui" type="JQ.JQueryObject">
        /// </param>
        htm();;
        try {
            var draggedElement = ui.item[0];
            var r = this._getRoomFromID(draggedElement.id);
            if (r.get_guest() && !r.get_pinned()) {
                r.set_pinned(true);
            }
            var roomListJq = jQuery('.ChatClientRoomPrivateList');
            var seraliseOptions = {};
            var order = roomListJq.sortable('serialize', seraliseOptions);
            var pairs = order.split('&');
            for (var i = 0; i < pairs.length; i++) {
                var guid = pairs[i].split('=')[1];
                this._roomsListOrder[i] = guid;
                (this._rooms[guid]).setListOrder(i);
            }
            this._server.storeUpdatedRoomListOrder();
        }
        catch ($e1) {
            this._debug('Serailise failed.');
        }
    },
    get__selectedRoom: function SpottedScript_Controls_ChatClient_Controller$get__selectedRoom() {
        /// <value type="SpottedScript.Controls.ChatClient.Room"></value>
        if (this._selectedRoomGuid.length === 0) {
            return null;
        }
        else {
            return this._rooms[this._selectedRoomGuid];
        }
    },
    set__selectedRoom: function SpottedScript_Controls_ChatClient_Controller$set__selectedRoom(value) {
        /// <value type="SpottedScript.Controls.ChatClient.Room"></value>
        this._setSelectedRoom(value, true);
        return value;
    },
    _selectedRoomGuid: null,
    _previouslySelectedRoomGuid: null,
    _setSelectedRoom: function SpottedScript_Controls_ChatClient_Controller$_setSelectedRoom(room, focus) {
        /// <param name="room" type="SpottedScript.Controls.ChatClient.Room">
        /// </param>
        /// <param name="focus" type="Boolean">
        /// </param>
        if (this._chatClientIsPaused) {
            this._previouslySelectedRoomGuid = (room == null) ? '' : room.guid;
            return;
        }
        this._selectedRoomGuid = (room == null) ? '' : room.guid;
        var $dict1 = this._rooms;
        for (var $key2 in $dict1) {
            var entry = { key: $key2, value: $dict1[$key2] };
            var r = entry.value;
            if (room != null && r.guid === room.guid) {
                if (!r.get_selected()) {
                    r.set_selected(true);
                    this._selectedRoomGuid = r.guid;
                }
            }
            else if (r.get_selected()) {
                r.set_selected(false);
            }
        }
        this._view.get_textBox().style.display = (room == null || room.readOnly) ? 'none' : '';
        this._view.get_deleteArchiveHolder().style.display = (room == null || !room.isPrivateChatRoom) ? 'none' : '';
        this._view.get_deleteArchiveDoneLabel().style.display = 'none';
        if (room != null && focus) {
            this._focusNow(room);
        }
    },
    _focusNow: function SpottedScript_Controls_ChatClient_Controller$_focusNow(r) {
        /// <param name="r" type="SpottedScript.Controls.ChatClient.Room">
        /// </param>
        if (!r.readOnly && this.usrK > 0) {
            this._view.get_textBox().focus();
        }
        else {
            this._view.get_tabsChatLink().focus();
        }
    },
    _gotItems: function SpottedScript_Controls_ChatClient_Controller$_gotItems(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var args = e;
        var itemTracker = {};
        for (var i = 0; i < args.items.length; i++) {
            var item = args.items[i];
            if (this._rooms[item.roomGuid] != null && ((this._rooms[item.roomGuid]).get_pinned() || (this._rooms[item.roomGuid]).get_guest())) {
                var r = this._rooms[item.roomGuid];
                if (item.type === SpottedScript.Controls.ChatClient.Shared.ItemType.commentChatMessage && (item.roomGuid === this._publicStreamRoomGuid || item.roomGuid === this._buddyStreamRoomGuid)) {
                    var c = item;
                    c.showSubHead = true;
                }
                else if (item.type === SpottedScript.Controls.ChatClient.Shared.ItemType.laughAlert) {
                    var l = item;
                    l.showSubHead = true;
                }
                r.addItem(item, itemTracker);
            }
            else if (item.type === SpottedScript.Controls.ChatClient.Shared.ItemType.privateChatMessage) {
                var p = args.items[i];
                if (p.usrK !== this.usrK) {
                    if (this._rooms[this._privateChatRequestsRoomGuid] != null) {
                        p.showChatButton = true;
                        var r = this._rooms[this._privateChatRequestsRoomGuid];
                        r.addItem(item, itemTracker);
                    }
                }
            }
            else if (item.type === SpottedScript.Controls.ChatClient.Shared.ItemType.commentChatMessage && item.roomGuid !== this._publicStreamRoomGuid && item.roomGuid !== this._buddyStreamRoomGuid) {
                var c = args.items[i];
                if (this._rooms[this._inboxUpdatesRoomGuid] != null && c.usrK !== this.usrK) {
                    c.showSubHead = true;
                    var r = this._rooms[this._inboxUpdatesRoomGuid];
                    r.addItem(item, itemTracker);
                }
            }
            else {
                if (this._rooms['AQ0FAAAAAAUAAAAA5wHGJw'] != null) {
                    var r = this._rooms['AQ0FAAAAAAUAAAAA5wHGJw'];
                    r.addItem(args.items[i], itemTracker);
                }
            }
        }
        var $dict1 = this._rooms;
        for (var $key2 in $dict1) {
            var de = { key: $key2, value: $dict1[$key2] };
            var r = this._rooms[de.key];
            if (args.serverRequestIndex === 0 || r.requestIndex === 0 || itemTracker[r.guid] != null) {
                r.finaliseRequest(args.serverRequestIndex);
                if (args.serverRequestIndex > 0 && r.requestIndex > 0 && itemTracker[r.guid] != null && r.get_starred() && (!this.hasFocus || !r.get_selected())) {
                    var p = new SpottedScript.Controls.ChatClient.Popup(this, r.name, r, itemTracker[r.guid]);
                    if (p.hasRelevantItems) {
                        p.clickAction = Function.createDelegate(this, this._popupClickAction);
                        this._popups.add(p);
                    }
                }
            }
        }
    },
    _gotNoItems: function SpottedScript_Controls_ChatClient_Controller$_gotNoItems(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var args = e;
        var $dict1 = this._rooms;
        for (var $key2 in $dict1) {
            var de = { key: $key2, value: $dict1[$key2] };
            var r = this._rooms[de.key];
            if (args.serverRequestIndex === 0 || r.requestIndex === 0) {
                r.finaliseRequest(args.serverRequestIndex);
            }
        }
    },
    _gotWrongSessionException: function SpottedScript_Controls_ChatClient_Controller$_gotWrongSessionException(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        this._previouslySelectedRoomGuid = (this.get__selectedRoom() == null) ? '' : this.get__selectedRoom().guid;
        this.set__selectedRoom(null);
        this._chatClientIsPaused = true;
        this._view.get_wrongSessionHolder().style.display = '';
        this._view.get_timeoutHolder().style.display = 'none';
    },
    _gotTimeoutException: function SpottedScript_Controls_ChatClient_Controller$_gotTimeoutException(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        this._previouslySelectedRoomGuid = (this.get__selectedRoom() == null) ? '' : this.get__selectedRoom().guid;
        this.set__selectedRoom(null);
        this._chatClientIsPaused = true;
        this._view.get_wrongSessionHolder().style.display = 'none';
        this._view.get_timeoutHolder().style.display = '';
    },
    _gotGenericException: function SpottedScript_Controls_ChatClient_Controller$_gotGenericException(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        if (a.error.get_exceptionType().endsWith('+SelfPrivateChatRoomException')) {
            alert('Trying to have a private chat with yourself? Are you MAD?');
        }
        if (a.error.get_exceptionType().endsWith('+WritePermissionException')) {
            alert('Error from the chat server while sending a message... Do you have permission to post into this room? If it\'s a group chat room, you need to be a member to chat.');
        }
        if (this._rooms[this._systemMessagesRoomGuid] != null) {
            var err = new SpottedScript.Controls.ChatClient.Items.Error(a.error, a.method, this, this._systemMessagesRoomGuid, 0);
            (this._rooms[this._systemMessagesRoomGuid]).addItem(err, null);
            (this._rooms[this._systemMessagesRoomGuid]).finaliseRequest(1);
        }
    },
    _popupClickAction: function SpottedScript_Controls_ChatClient_Controller$_popupClickAction(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this._view.get_tabsChatLink().focus();
        var roomGuid = o;
        this.pinNewRoom(roomGuid);
    },
    _roomListMouseDown: function SpottedScript_Controls_ChatClient_Controller$_roomListMouseDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        window.setTimeout(Function.createDelegate(this, this._textBoxStopWatermark), 20);
    },
    _roomListClick: function SpottedScript_Controls_ChatClient_Controller$_roomListClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.target.id.endsWith('PrivateChatDrop')) {
            return;
        }
        if (e.target.id.endsWith('_Link') || e.target.id.endsWith('_Icon')) {
            var r = this._getRoomFromChildID(e.target.id);
            this.set__selectedRoom(r);
            this._unPauseChatClient(true);
        }
        else if (e.target.id.endsWith('_Cross')) {
            var r = this._getRoomFromChildID(e.target.id);
            if (r.get_pinned()) {
                this._unPauseChatClient(false);
                this.set__selectedRoom(null);
                r.set_pinned(false);
            }
            else {
                this._unPauseChatClient(true);
            }
        }
        else if (e.target.id.endsWith('_Star')) {
            var r = this._getRoomFromChildID(e.target.id);
            if (r.starrable) {
                r.set_starred(!r.get_starred());
            }
            else if (r.get_starred()) {
                alert('This room can\'t be un-starred');
            }
            else {
                alert('This room can\'t be starred');
            }
            this._unPauseChatClient(true);
        }
        if (this.get__selectedRoom() != null) {
            this._focusNow(this.get__selectedRoom());
        }
        e.preventDefault();
    },
    _privateChatDropChange: function SpottedScript_Controls_ChatClient_Controller$_privateChatDropChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this._view.get_privateChatDrop().value !== '0') {
            this.pinNewRoom(this._view.get_privateChatDrop().value);
            this._view.get_privateChatDrop().selectedIndex = 0;
        }
    },
    _getRoomFromChildID: function SpottedScript_Controls_ChatClient_Controller$_getRoomFromChildID(ID) {
        /// <param name="ID" type="String">
        /// </param>
        /// <returns type="SpottedScript.Controls.ChatClient.Room"></returns>
        var a = ID.split('_');
        var guid = a[a.length - 2];
        return this._rooms[guid];
    },
    _getRoomFromID: function SpottedScript_Controls_ChatClient_Controller$_getRoomFromID(ID) {
        /// <param name="ID" type="String">
        /// </param>
        /// <returns type="SpottedScript.Controls.ChatClient.Room"></returns>
        var a = ID.split('_');
        var guid = a[a.length - 1];
        return this._rooms[guid];
    },
    _getSelectedRoomListIndex: function SpottedScript_Controls_ChatClient_Controller$_getSelectedRoomListIndex() {
        /// <returns type="Number" integer="true"></returns>
        for (var i = 0; i < this._roomsListOrder.length; i++) {
            if ((this._rooms[this._roomsListOrder[i].toString()]).get_selected()) {
                return i;
            }
        }
        return 0;
    },
    _getRoomArchiveItems: function SpottedScript_Controls_ChatClient_Controller$_getRoomArchiveItems(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (e != null) {
            var a = e;
            this._server.getArchiveItems(a.roomGuid);
        }
    },
    _gotRoomArchiveItems: function SpottedScript_Controls_ChatClient_Controller$_gotRoomArchiveItems(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (e != null) {
            var a = e;
            this._unPauseChatClient(true);
            if (this._rooms[a.roomGuid] != null) {
                var r = this._rooms[a.roomGuid];
                if (!r.get_selected()) {
                    return;
                }
                r.showArchiveItems(a.archiveItems);
            }
        }
    },
    _deleteArchive: function SpottedScript_Controls_ChatClient_Controller$_deleteArchive(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._unPauseChatClient(true);
        this._server.deleteArchive(this.get__selectedRoom().guid);
        e.preventDefault();
    },
    _doneDeleteArchive: function SpottedScript_Controls_ChatClient_Controller$_doneDeleteArchive(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (e != null) {
            var a = e;
            this._unPauseChatClient(true);
            if (this._rooms[a.roomGuid] != null) {
                var r = this._rooms[a.roomGuid];
                r.clearItems();
                this._view.get_deleteArchiveDoneLabel().style.display = '';
                window.setTimeout(Function.createDelegate(this, function() {
                    this._view.get_deleteArchiveDoneLabel().style.display = 'none';
                }), 2000);
            }
        }
    },
    _getRoomMoreInfoHtml: function SpottedScript_Controls_ChatClient_Controller$_getRoomMoreInfoHtml(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (e != null) {
            var a = e;
            this._server.getMoreInfo(a.roomGuid);
        }
    },
    _gotRoomMoreInfoHtml: function SpottedScript_Controls_ChatClient_Controller$_gotRoomMoreInfoHtml(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (e != null) {
            var a = e;
            this._unPauseChatClient(true);
            if (this._rooms[a.roomGuid] != null) {
                var r = this._rooms[a.roomGuid];
                if (!r.get_selected()) {
                    return;
                }
                r.storeMoreInfoHtmlAndShowMoreInfo(a.moreInfoHtml);
            }
        }
    },
    _moreInfoClick: function SpottedScript_Controls_ChatClient_Controller$_moreInfoClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._unPauseChatClient(true);
        if (this.get__selectedRoom() != null && !this.get__selectedRoom().get_moreInfoVisible()) {
            this.get__selectedRoom().showMoreInfo(false);
        }
        e.preventDefault();
    },
    _resumeLinkClick: function SpottedScript_Controls_ChatClient_Controller$_resumeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._unPauseChatClient(true);
        e.preventDefault();
    },
    _unPauseChatClient: function SpottedScript_Controls_ChatClient_Controller$_unPauseChatClient(selectPreviousRoom) {
        /// <param name="selectPreviousRoom" type="Boolean">
        /// </param>
        if (this._chatClientIsPaused) {
            this._chatClientIsPaused = false;
            this._view.get_wrongSessionHolder().style.display = 'none';
            this._view.get_timeoutHolder().style.display = 'none';
            if (selectPreviousRoom && this._previouslySelectedRoomGuid.length > 0) {
                this.set__selectedRoom(this._rooms[this._previouslySelectedRoomGuid]);
            }
            this._server.resumeAfterPause();
        }
    },
    _gotRoomState: function SpottedScript_Controls_ChatClient_Controller$_gotRoomState(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        if (a.roomState == null) {
            return;
        }
        for (var i = 0; i < a.roomState.length; i++) {
            var ss = a.roomState[i];
            var ob = this._rooms[ss.guid];
            if (ob != null) {
                var r = ob;
                r.updateStatsAfterUnPause(ss);
            }
        }
    },
    changePhoto: function SpottedScript_Controls_ChatClient_Controller$changePhoto(newRoomGuid) {
        /// <param name="newRoomGuid" type="String">
        /// </param>
        this._unPauseChatClient(false);
        var ob = this._rooms[newRoomGuid];
        if (ob == null) {
            this._server.switchPhotoRoom(newRoomGuid);
        }
        else {
            var newRoomShouldBeSelected = this._removeAllUnPinnedGuestPhotoRoomsExceptSpecified(newRoomGuid);
            var r = ob;
            if (newRoomShouldBeSelected) {
                this._setSelectedRoom(r, false);
            }
        }
    },
    _removeRoom: function SpottedScript_Controls_ChatClient_Controller$_removeRoom(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        var r = this._rooms[roomGuid];
        r.prepareForRemoval();
        delete this._rooms[roomGuid];
        var newRoomsListOrder = [];
        for (var i = 0; i < this._roomsListOrder.length; i++) {
            if (roomGuid !== this._roomsListOrder[i]) {
                newRoomsListOrder[newRoomsListOrder.length] = this._roomsListOrder[i];
            }
        }
        this._roomsListOrder = newRoomsListOrder;
    },
    _removeAllUnPinnedGuestPhotoRoomsExceptSpecified: function SpottedScript_Controls_ChatClient_Controller$_removeAllUnPinnedGuestPhotoRoomsExceptSpecified(exceptThisRoomGuid) {
        /// <param name="exceptThisRoomGuid" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        var oneIsSelected = false;
        var $dict1 = this._rooms;
        for (var $key2 in $dict1) {
            var entry = { key: $key2, value: $dict1[$key2] };
            var r = entry.value;
            if (r.isPhotoChatRoom && r.get_guest() && r.guid !== exceptThisRoomGuid && !r.get_pinned()) {
                if (r.get_selected()) {
                    oneIsSelected = true;
                }
                this._removeRoom(r.guid);
            }
        }
        return oneIsSelected;
    },
    _gotNewPhotoRoom: function SpottedScript_Controls_ChatClient_Controller$_gotNewPhotoRoom(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        /// <returns type="Boolean"></returns>
        var a = e;
        var newRoomShouldBeSelected = this._removeAllUnPinnedGuestPhotoRoomsExceptSpecified(a.roomStub.guid);
        var ob = this._rooms[a.roomStub.guid];
        if (ob == null) {
            var r = new SpottedScript.Controls.ChatClient.Room(this, this._view);
            r.initialiseFromStub(a.roomStub, (a.roomStub.isPrivateChatRoom) ? this._view.get_roomPrivateList() : (a.roomStub.guest) ? this._view.get_roomGuestList() : this._view.get_roomList(), this._state);
            this._initialiseRoomEvents(r);
            this._rooms[r.guid] = r;
            this._roomsListOrder[this._roomsListOrder.length] = r.guid;
            r.setListOrder(this._roomsListOrder.length - 1);
            if (newRoomShouldBeSelected) {
                this._setSelectedRoom(r, false);
            }
            this._updateDraggable();
            this._updateRoomUI();
            return false;
        }
        else {
            var r = ob;
            if (newRoomShouldBeSelected) {
                this._setSelectedRoom(r, false);
            }
            return true;
        }
    },
    pinNewRoom: function SpottedScript_Controls_ChatClient_Controller$pinNewRoom(newRoomGuid) {
        /// <param name="newRoomGuid" type="String">
        /// </param>
        this._unPauseChatClient(false);
        this._view.get_tabsChatLink().focus();
        var ob = this._rooms[newRoomGuid];
        if (ob == null) {
            this._server.pinRoom(newRoomGuid);
        }
        else {
            var r = ob;
            if (!r.get_pinned()) {
                r.set_pinned(true);
            }
            this.set__selectedRoom(r);
        }
    },
    _roomPinAction: function SpottedScript_Controls_ChatClient_Controller$_roomPinAction(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        if (a.pinned) {
            this._server.rePinRoom(a.roomGuid);
        }
        else {
            this._server.unPinRoom(a.roomGuid);
        }
    },
    _roomStarAction: function SpottedScript_Controls_ChatClient_Controller$_roomStarAction(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        this._server.starRoom(a.roomGuid, a.starred);
    },
    _guestRoomPinAction: function SpottedScript_Controls_ChatClient_Controller$_guestRoomPinAction(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        var r = this._rooms[a.roomGuid];
        if (r != null && r.get_guest()) {
            r.removeFromRoomsList();
            r.addToRoomsList((!r.get_pinned()) ? this._view.get_roomGuestList() : (r.isPrivateChatRoom) ? this._view.get_roomPrivateList() : this._view.get_roomList());
            r.updateUIAfterGuestPinAction();
        }
        this._updateRoomUI();
    },
    _updateRoomUI: function SpottedScript_Controls_ChatClient_Controller$_updateRoomUI() {
        var hasGuestRooms = false;
        var hasPrivateRooms = false;
        var $dict1 = this._rooms;
        for (var $key2 in $dict1) {
            var entry = { key: $key2, value: $dict1[$key2] };
            var r = entry.value;
            if (r.get_guest() && !r.get_pinned()) {
                hasGuestRooms = true;
            }
            if (r.isPrivateChatRoom && r.get_pinned()) {
                hasPrivateRooms = true;
            }
            if (hasGuestRooms && hasPrivateRooms) {
                break;
            }
        }
        this._updateRoomPrivateListVisibility(hasPrivateRooms);
        this._updateRoomGuestListVisibility(hasGuestRooms);
    },
    _gotRoom: function SpottedScript_Controls_ChatClient_Controller$_gotRoom(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        /// <returns type="Boolean"></returns>
        var a = e;
        var ob = this._rooms[a.roomStub.guid];
        if (ob == null) {
            var r = new SpottedScript.Controls.ChatClient.Room(this, this._view);
            r.initialiseFromStub(a.roomStub, (a.roomStub.guest) ? this._view.get_roomGuestList() : (a.roomStub.isPrivateChatRoom) ? this._view.get_roomPrivateList() : this._view.get_roomList(), this._state);
            this._initialiseRoomEvents(r);
            this._rooms[r.guid] = r;
            this._roomsListOrder[this._roomsListOrder.length] = r.guid;
            r.setListOrder(this._roomsListOrder.length - 1);
            this.set__selectedRoom(r);
            this._updateDraggable();
            if (a.roomStub.guest) {
                this._updateRoomGuestListVisibility(true);
            }
            else if (a.roomStub.isPrivateChatRoom) {
                this._updateRoomPrivateListVisibility(true);
            }
            return false;
        }
        else {
            var r = ob;
            if (!r.get_pinned()) {
                r.set_pinned(true);
            }
            this.set__selectedRoom(r);
            return true;
        }
    },
    _updateRoomGuestListVisibility: function SpottedScript_Controls_ChatClient_Controller$_updateRoomGuestListVisibility(hasGuestRooms) {
        /// <param name="hasGuestRooms" type="Boolean">
        /// </param>
        this._view.get_roomGuestListDivider().style.display = (hasGuestRooms) ? '' : 'none';
        this._view.get_roomGuestList().style.display = (hasGuestRooms) ? '' : 'none';
    },
    _updateRoomPrivateListVisibility: function SpottedScript_Controls_ChatClient_Controller$_updateRoomPrivateListVisibility(hasPrivateRooms) {
        /// <param name="hasPrivateRooms" type="Boolean">
        /// </param>
        this._view.get_roomPrivateList().style.display = (hasPrivateRooms) ? '' : 'none';
    },
    _textBoxKeyDown: function SpottedScript_Controls_ChatClient_Controller$_textBoxKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._lastKeyDown = e.keyCode;
        if (e.keyCode === Sys.UI.Key.esc) {
            this._unPauseChatClient(true);
            this._view.get_textBox().value = '';
        }
        else if (e.keyCode === Sys.UI.Key.enter) {
            if (this.get__selectedRoom().get_guest() && !this.get__selectedRoom().get_pinned()) {
                this.get__selectedRoom().set_pinned(true);
            }
            if (this._view.get_textBox().value.trim().length > 0) {
                this._unPauseChatClient(true);
                WhenLoggedIn(Function.createDelegate(this, function() {
                    this._server.sendMessage(this._view.get_textBox().value, this._selectedRoomGuid);
                    this._view.get_textBox().value = '';
                }));
            }
            else {
                this._unPauseChatClient(true);
            }
            if (!this.get__selectedRoom().get_pinned()) {
                this.get__selectedRoom().set_pinned(true);
            }
            e.preventDefault();
        }
    },
    _outerKeyDown: function SpottedScript_Controls_ChatClient_Controller$_outerKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.up || e.keyCode === Sys.UI.Key.down) {
            var selectedRoomIndex = this._getSelectedRoomListIndex();
            var newRoom = null;
            var count = 0;
            while ((count <= this._roomsListOrder.length + 1) && (newRoom == null || !newRoom.get_pinned() || newRoom.guid === this._publicStreamRoomGuid)) {
                if (e.keyCode === Sys.UI.Key.up) {
                    if (selectedRoomIndex === 0) {
                        selectedRoomIndex = this._roomsListOrder.length;
                    }
                    selectedRoomIndex--;
                }
                else if (e.keyCode === Sys.UI.Key.down) {
                    if (selectedRoomIndex === this._roomsListOrder.length - 1) {
                        selectedRoomIndex = -1;
                    }
                    selectedRoomIndex++;
                }
                newRoom = this._rooms[this._roomsListOrder[selectedRoomIndex].toString()];
                count++;
            }
            this.set__selectedRoom(newRoom);
            this._unPauseChatClient(true);
            e.preventDefault();
        }
    },
    _textBoxKeyPress: function SpottedScript_Controls_ChatClient_Controller$_textBoxKeyPress(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this._lastKeyDown === Sys.UI.Key.enter) {
            e.preventDefault();
        }
    },
    _addWatermark: false,
    _textBoxFocus: function SpottedScript_Controls_ChatClient_Controller$_textBoxFocus(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (!this._attributeExists(this._view.get_textBox(), 'readonly')) {
            this.hasFocus = true;
            this._addWatermark = false;
            if (this._view.get_textBox().value === 'Enter your message here...') {
                this._view.get_textBox().value = '';
            }
            this._view.get_textBox().className = 'ChatClientTextBox';
        }
    },
    _textBoxBlur: function SpottedScript_Controls_ChatClient_Controller$_textBoxBlur(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (!this._attributeExists(this._view.get_textBox(), 'readonly')) {
            this.hasFocus = false;
            this._addWatermark = true;
            window.setTimeout(Function.createDelegate(this, this._textBoxAddWatermark), 50);
        }
    },
    _attributeExists: function SpottedScript_Controls_ChatClient_Controller$_attributeExists(el, attributeName) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        /// <param name="attributeName" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        var d = null;
        try {
            d = el.attributes.getNamedItem(attributeName);
        }
        catch ($e1) {
            return false;
        }
        if (d == null) {
            return false;
        }
        try {
            if (!d.specified) {
                return false;
            }
        }
        catch ($e2) {
            return false;
        }
        return true;
    },
    _textBoxAddWatermark: function SpottedScript_Controls_ChatClient_Controller$_textBoxAddWatermark() {
        if (this._view.get_textBox().value === '' && this._addWatermark) {
            this._view.get_textBox().value = 'Enter your message here...';
            this._view.get_textBox().className = 'ChatClientTextBoxWatermark';
        }
    },
    _textBoxStopWatermark: function SpottedScript_Controls_ChatClient_Controller$_textBoxStopWatermark() {
        this._addWatermark = false;
    },
    _tabsChatLinkFocus: function SpottedScript_Controls_ChatClient_Controller$_tabsChatLinkFocus(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this.hasFocus = true;
    },
    _tabsChatLinkBlur: function SpottedScript_Controls_ChatClient_Controller$_tabsChatLinkBlur(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this.hasFocus = false;
    },
    _loadingIcon: null,
    _hideLoadingIcon: function SpottedScript_Controls_ChatClient_Controller$_hideLoadingIcon(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this._loadingIcon != null) {
            this._loadingIcon.style.display = 'none';
        }
    },
    _showLoadingIcon: function SpottedScript_Controls_ChatClient_Controller$_showLoadingIcon(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this._view.get_textBox().style.display === '') {
            if (this._loadingIcon == null) {
                this._loadingIcon = document.createElement('img');
                this._loadingIcon.src = '/gfx/autocomplete-loading.gif';
                this._loadingIcon.style.height = '16px';
                this._loadingIcon.style.width = '16px';
                this._loadingIcon.style.position = 'absolute';
                this._loadingIcon.style.zIndex = 200;
                document.body.appendChild(this._loadingIcon);
            }
            var offset = jQuery(this._view.get_textBox()).offset();
            this._loadingIcon.style.left = (offset.left + this._view.get_textBox().clientWidth - 20) + 'px';
            this._loadingIcon.style.top = (offset.top + 2) + 'px';
            this._loadingIcon.style.display = '';
        }
    },
    _debug: function SpottedScript_Controls_ChatClient_Controller$_debug(text) {
        /// <param name="text" type="String">
        /// </param>
        if (this._rooms[this._systemMessagesRoomGuid] != null) {
            try {
                var n = new SpottedScript.Controls.ChatClient.Items.Note(text.replace('<', '&lt;'), this, this._systemMessagesRoomGuid, 0);
                (this._rooms[this._systemMessagesRoomGuid]).addItem(n, null);
                (this._rooms[this._systemMessagesRoomGuid]).finaliseRequest(1);
            }
            catch ($e1) {
                var n = new SpottedScript.Controls.ChatClient.Items.Note('<small>NULL</small>', this, this._systemMessagesRoomGuid, 0);
                (this._rooms[this._systemMessagesRoomGuid]).addItem(n, null);
                (this._rooms[this._systemMessagesRoomGuid]).finaliseRequest(1);
            }
        }
    },
    _debugEventHandler: function SpottedScript_Controls_ChatClient_Controller$_debugEventHandler(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
        this._debug(a.html);
    },
    _debugObject: function SpottedScript_Controls_ChatClient_Controller$_debugObject(o) {
        /// <param name="o" type="Object">
        /// </param>
        var d = 0;
        var $dict1 = d;
        for (var $key2 in $dict1) {
            var de = { key: $key2, value: $dict1[$key2] };
            this._debug(de.key + ': ' + de.value.toString() + '<br /><br />');
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.PageImplementation
function chatClientPinRoom(roomGuid, transferSelector, transfer) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="transferSelector" type="Object">
    /// </param>
    /// <param name="transfer" type="Boolean">
    /// </param>
    SpottedScript.Controls.ChatClient.Controller.instance.pinNewRoom(roomGuid);
    if (transfer) {
        try {
            var options = {};
            options['to'] = '#ChatClient_MessagesMain';
            jQuery(transferSelector).effect('transfer', options, 500, null);
        }
        catch (ex) {
        }
    }
}
function chatClientChangePhoto(photoRoomGuid) {
    /// <param name="photoRoomGuid" type="String">
    /// </param>
    SpottedScript.Controls.ChatClient.Controller.instance.changePhoto(photoRoomGuid);
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Room
SpottedScript.Controls.ChatClient.Room = function SpottedScript_Controls_ChatClient_Room(parent, view) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="view" type="SpottedScript.Controls.ChatClient.View">
    /// </param>
    /// <field name="_parent" type="SpottedScript.Controls.ChatClient.Controller">
    /// </field>
    /// <field name="_outerElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_roomElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_linkElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_crossElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_presenceElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_totalElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_statsSeperatorElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_unreadElement" type="Object" domElement="true">
    /// </field>
    /// <field name="guid" type="String">
    /// </field>
    /// <field name="name" type="String">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="pinable" type="Boolean">
    /// </field>
    /// <field name="starrable" type="Boolean">
    /// </field>
    /// <field name="readOnly" type="Boolean">
    /// </field>
    /// <field name="_stub" type="SpottedScript.Controls.ChatClient.Shared.RoomStub">
    /// </field>
    /// <field name="_state" type="SpottedScript.Controls.ChatClient.Shared.StateStub">
    /// </field>
    /// <field name="_html" type="SpottedScript.Controls.ChatClient.Shared.RoomHtml">
    /// </field>
    /// <field name="_elementsInitialised" type="Boolean">
    /// </field>
    /// <field name="_items" type="Array" elementType="Item">
    /// </field>
    /// <field name="_messsagesElementHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_messsagesElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_moreInfoElement" type="Object" domElement="true">
    /// </field>
    /// <field name="_needsNewStatusUpdate" type="Boolean">
    /// </field>
    /// <field name="_doneFullUpdateOfNewStatus" type="Boolean">
    /// </field>
    /// <field name="roomPinAction" type="Sys.EventHandler">
    /// </field>
    /// <field name="roomStarAction" type="Sys.EventHandler">
    /// </field>
    /// <field name="guestRoomPinAction" type="Sys.EventHandler">
    /// </field>
    /// <field name="getMoreInfoHtml" type="Sys.EventHandler">
    /// </field>
    /// <field name="getArchiveItems" type="Sys.EventHandler">
    /// </field>
    /// <field name="requestIndex" type="Number" integer="true">
    /// </field>
    /// <field name="isPhotoChatRoom" type="Boolean">
    /// </field>
    /// <field name="isPrivateChatRoom" type="Boolean">
    /// </field>
    /// <field name="isNewPhotoAlertsRoom" type="Boolean">
    /// </field>
    /// <field name="presence" type="SpottedScript.Controls.ChatClient.Shared.PresenceState">
    /// </field>
    /// <field name="icon" type="String">
    /// </field>
    /// <field name="_tokenDateTimeTicks" type="String">
    /// </field>
    /// <field name="_token" type="String">
    /// </field>
    /// <field name="_loggedIn" type="Boolean">
    /// </field>
    /// <field name="_onlyRenderItemsWhenSelected" type="Boolean">
    /// </field>
    /// <field name="_onlyRenderItemsWhenSelectedMaxItems" type="Number" integer="true">
    /// </field>
    /// <field name="_hasTopPhoto" type="Boolean">
    /// </field>
    /// <field name="_topPhoto" type="SpottedScript.Controls.ChatClient.Items.TopPhoto">
    /// </field>
    /// <field name="_topPhotoHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_topPhotoImage" type="Object" domElement="true">
    /// </field>
    /// <field name="_topPhotoAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_hasChatPic" type="Boolean">
    /// </field>
    /// <field name="_chatPic" type="String">
    /// </field>
    /// <field name="_chatPicStmuParams" type="String">
    /// </field>
    /// <field name="_chatPicUrl" type="String">
    /// </field>
    /// <field name="_chatPicHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_chatPicImage" type="Object" domElement="true">
    /// </field>
    /// <field name="_chatPicAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_latestHtmlItem" type="SpottedScript.Controls.ChatClient.Items.Html">
    /// </field>
    /// <field name="_haveCheckedArchive" type="Boolean">
    /// </field>
    /// <field name="_hasArchive" type="Boolean">
    /// </field>
    /// <field name="_hasArchiveItems" type="Boolean">
    /// </field>
    /// <field name="_hiddenFromRoomList" type="Boolean">
    /// </field>
    /// <field name="_isStreamRoom" type="Boolean">
    /// </field>
    /// <field name="_pinned" type="Boolean">
    /// </field>
    /// <field name="_starred" type="Boolean">
    /// </field>
    /// <field name="_isStarredByDefault" type="Boolean">
    /// </field>
    /// <field name="_onlyRenderItemsWhenSelectedLatestItemWhenLastRefreshed" type="String">
    /// </field>
    /// <field name="_moreInfoVisible" type="Boolean">
    /// </field>
    /// <field name="_hideMoreInfoOnNextReceivedItem" type="Boolean">
    /// </field>
    /// <field name="_showMoreInfoIfNoMessagesLastRandom" type="Number" integer="true">
    /// </field>
    /// <field name="_getArchiveLastRandom" type="Number" integer="true">
    /// </field>
    this._parent = parent;
    this._items = [];
    this._loggedIn = parent.loggedIn;
    this._elementsInitialised = false;
    this._needsNewStatusUpdate = false;
    this._doneFullUpdateOfNewStatus = false;
    this._messsagesElementHolder = document.createElement('div');
    this._messsagesElementHolder.style.display = 'none';
    view.get_messageList().appendChild(this._messsagesElementHolder);
    this._messsagesElement = document.createElement('div');
    this._messsagesElementHolder.appendChild(this._messsagesElement);
    this._moreInfoElement = document.createElement('div');
    this._moreInfoElement.className = 'ChatClientMessageListHiddenHolder';
    view.get_messageList().appendChild(this._moreInfoElement);
    this.requestIndex = 0;
}
SpottedScript.Controls.ChatClient.Room.prototype = {
    _parent: null,
    _outerElement: null,
    _roomElement: null,
    _linkElement: null,
    _crossElement: null,
    _presenceElement: null,
    _totalElement: null,
    _statsSeperatorElement: null,
    _unreadElement: null,
    guid: null,
    name: null,
    url: null,
    pinable: false,
    starrable: false,
    readOnly: false,
    _stub: null,
    _state: null,
    _html: null,
    _elementsInitialised: false,
    _items: null,
    _messsagesElementHolder: null,
    _messsagesElement: null,
    _moreInfoElement: null,
    _needsNewStatusUpdate: false,
    _doneFullUpdateOfNewStatus: false,
    roomPinAction: null,
    roomStarAction: null,
    guestRoomPinAction: null,
    getMoreInfoHtml: null,
    getArchiveItems: null,
    requestIndex: 0,
    isPhotoChatRoom: false,
    isPrivateChatRoom: false,
    isNewPhotoAlertsRoom: false,
    presence: 0,
    icon: null,
    _tokenDateTimeTicks: null,
    _token: null,
    _loggedIn: false,
    _onlyRenderItemsWhenSelected: false,
    _onlyRenderItemsWhenSelectedMaxItems: 0,
    _hasTopPhoto: false,
    _topPhoto: null,
    _topPhotoHolder: null,
    _topPhotoImage: null,
    _topPhotoAnchor: null,
    _hasChatPic: false,
    _chatPic: null,
    _chatPicStmuParams: null,
    _chatPicUrl: null,
    _chatPicHolder: null,
    _chatPicImage: null,
    _chatPicAnchor: null,
    _latestHtmlItem: null,
    _haveCheckedArchive: false,
    _hasArchive: false,
    _hasArchiveItems: false,
    _hiddenFromRoomList: false,
    _isStreamRoom: false,
    get_selected: function SpottedScript_Controls_ChatClient_Room$get_selected() {
        /// <value type="Boolean"></value>
        return this._state.selected;
    },
    set_selected: function SpottedScript_Controls_ChatClient_Room$set_selected(value) {
        /// <value type="Boolean"></value>
        if (this._state.selected !== value) {
            this._state.selected = value;
            if (this._state.selected) {
                this._resetItemsOnSelect();
                if (!this._hasArchive || (this._haveCheckedArchive && !this._hasArchiveItems)) {
                    this._showMoreInfoAfterDelayIfNoMessages();
                }
                else {
                    this._getArchiveAfterDelayIfLessThan20Messages();
                }
                if (this._onlyRenderItemsWhenSelected) {
                    this._onlyRenderItemsWhenSelectedRenderItemsNow();
                }
            }
            else {
                this._moreInfoVisible = false;
            }
            this._updateUI();
        }
        return value;
    },
    _resetItemsOnSelect: function SpottedScript_Controls_ChatClient_Room$_resetItemsOnSelect() {
        this.set__latestItemSeen(this.get__latestItem());
        this.set__newMessages(0);
    },
    get_pinned: function SpottedScript_Controls_ChatClient_Room$get_pinned() {
        /// <value type="Boolean"></value>
        if (this.pinable) {
            return this._pinned;
        }
        else {
            return true;
        }
    },
    set_pinned: function SpottedScript_Controls_ChatClient_Room$set_pinned(value) {
        /// <value type="Boolean"></value>
        if (this.pinable) {
            if (this._pinned !== value) {
                this._pinned = value;
                if (this.roomPinAction != null) {
                    this.roomPinAction(this, new SpottedScript.Controls.ChatClient.PinActionEventArgs(this.guid, this._pinned));
                }
                if (this.get_guest()) {
                    if (this.guestRoomPinAction != null) {
                        this.guestRoomPinAction(this, new SpottedScript.Controls.ChatClient.PinActionEventArgs(this.guid, this._pinned));
                    }
                }
                if (this._pinned) {
                    this.requestIndex = 0;
                }
                else {
                    this._starred = this._isStarredByDefault;
                }
                if (!this._pinned && !this.get_guest()) {
                    this.clearItems();
                }
                this._updateUI();
            }
        }
        return value;
    },
    _pinned: false,
    get_starred: function SpottedScript_Controls_ChatClient_Room$get_starred() {
        /// <value type="Boolean"></value>
        return this._starred;
    },
    set_starred: function SpottedScript_Controls_ChatClient_Room$set_starred(value) {
        /// <value type="Boolean"></value>
        if (this._starred !== value) {
            this._starred = value;
            if (this.roomStarAction != null) {
                this.roomStarAction(this, new SpottedScript.Controls.ChatClient.StarActionEventArgs(this.guid, this._starred));
            }
            this._updateUI();
        }
        return value;
    },
    _starred: false,
    _isStarredByDefault: false,
    get_guest: function SpottedScript_Controls_ChatClient_Room$get_guest() {
        /// <value type="Boolean"></value>
        return this._state.guest;
    },
    set_guest: function SpottedScript_Controls_ChatClient_Room$set_guest(value) {
        /// <value type="Boolean"></value>
        this._state.guest = value;
        return value;
    },
    get__newMessages: function SpottedScript_Controls_ChatClient_Room$get__newMessages() {
        /// <value type="Number" integer="true"></value>
        return this._state.newMessages;
    },
    set__newMessages: function SpottedScript_Controls_ChatClient_Room$set__newMessages(value) {
        /// <value type="Number" integer="true"></value>
        this._state.newMessages = value;
        return value;
    },
    get__totalMessages: function SpottedScript_Controls_ChatClient_Room$get__totalMessages() {
        /// <value type="Number" integer="true"></value>
        return this._state.totalMessages;
    },
    set__totalMessages: function SpottedScript_Controls_ChatClient_Room$set__totalMessages(value) {
        /// <value type="Number" integer="true"></value>
        this._state.totalMessages = value;
        return value;
    },
    get__latestItem: function SpottedScript_Controls_ChatClient_Room$get__latestItem() {
        /// <value type="String"></value>
        return this._state.latestItem;
    },
    set__latestItem: function SpottedScript_Controls_ChatClient_Room$set__latestItem(value) {
        /// <value type="String"></value>
        this._state.latestItem = value;
        return value;
    },
    get__latestItemSeen: function SpottedScript_Controls_ChatClient_Room$get__latestItemSeen() {
        /// <value type="String"></value>
        return this._state.latestItemSeen;
    },
    set__latestItemSeen: function SpottedScript_Controls_ChatClient_Room$set__latestItemSeen(value) {
        /// <value type="String"></value>
        this._state.latestItemSeen = value;
        return value;
    },
    get__latestItemOld: function SpottedScript_Controls_ChatClient_Room$get__latestItemOld() {
        /// <value type="String"></value>
        return this._state.latestItemOld;
    },
    set__latestItemOld: function SpottedScript_Controls_ChatClient_Room$set__latestItemOld(value) {
        /// <value type="String"></value>
        this._state.latestItemOld = value;
        return value;
    },
    get_itemCount: function SpottedScript_Controls_ChatClient_Room$get_itemCount() {
        /// <value type="Number" integer="true"></value>
        return (this._items == null) ? 0 : this._items.length;
    },
    initialiseFromElement: function SpottedScript_Controls_ChatClient_Room$initialiseFromElement(e, controllerStateStore) {
        /// <param name="e" type="Object" domElement="true">
        /// </param>
        /// <param name="controllerStateStore" type="Array" elementType="StateStub">
        /// </param>
        this.guid = e.attributes.getNamedItem('roomGuid').value;
        this.name = e.attributes.getNamedItem('roomName').value;
        this.url = e.attributes.getNamedItem('roomUrl').value;
        this.pinable = Boolean.parse(e.attributes.getNamedItem('roomPinable').value);
        this._pinned = Boolean.parse(e.attributes.getNamedItem('roomPinned').value);
        this._starred = Boolean.parse(e.attributes.getNamedItem('roomStarred').value);
        this.starrable = Boolean.parse(e.attributes.getNamedItem('roomStarrable').value);
        this._isStarredByDefault = Boolean.parse(e.attributes.getNamedItem('roomIsStarredByDefault').value);
        this.readOnly = Boolean.parse(e.attributes.getNamedItem('roomReadOnly').value);
        this.isPhotoChatRoom = Boolean.parse(e.attributes.getNamedItem('roomIsPhotoChatRoom').value);
        this.isPrivateChatRoom = Boolean.parse(e.attributes.getNamedItem('roomIsPrivateChatRoom').value);
        this.isNewPhotoAlertsRoom = Boolean.parse(e.attributes.getNamedItem('roomIsNewPhotoAlertsRoom').value);
        this.presence = Number.parseInvariant(e.attributes.getNamedItem('roomPresence').value);
        this.icon = e.attributes.getNamedItem('roomIcon').value;
        this._tokenDateTimeTicks = e.attributes.getNamedItem('roomTokenDateTimeTicks').value;
        this._token = e.attributes.getNamedItem('roomToken').value;
        this._hasArchive = Boolean.parse(e.attributes.getNamedItem('roomHasArchive').value);
        this._hiddenFromRoomList = Boolean.parse(e.attributes.getNamedItem('roomHiddenFromRoomList').value);
        this._isStreamRoom = Boolean.parse(e.attributes.getNamedItem('roomisStreamRoom').value);
        this._state = new SpottedScript.Controls.ChatClient.Shared.StateStub();
        this._state.initialise(this.guid, Boolean.parse(e.attributes.getNamedItem('roomSelected').value), Boolean.parse(e.attributes.getNamedItem('roomGuest').value), Number.parseInvariant(e.attributes.getNamedItem('roomNewMessages').value), Number.parseInvariant(e.attributes.getNamedItem('roomTotalMessages').value), e.attributes.getNamedItem('roomLatestItem').value, e.attributes.getNamedItem('roomLatestItemSeen').value, e.attributes.getNamedItem('roomLatestItemOld').value, Number.parseInvariant(e.attributes.getNamedItem('roomListOrder').value), this._tokenDateTimeTicks, this._token);
        this._addToStateStoreIfNotAlreadyThere(controllerStateStore, this._state);
        this._stub = new SpottedScript.Controls.ChatClient.Shared.RoomStub(this._parent.clientID, this.guid, this.name, this.url, this.get_pinned(), this.get_starred(), this._isStarredByDefault, this.pinable, this.starrable, this.get_selected(), this.get_guest(), this.get__newMessages(), this.get__totalMessages(), this.get__latestItem(), this.get__latestItemSeen(), this.get__latestItemOld(), this.readOnly, this._state.listOrder, this.isPhotoChatRoom, this.isPrivateChatRoom, this.isNewPhotoAlertsRoom, this.presence, this.icon, this._tokenDateTimeTicks, this._token, this._hasArchive, this._hiddenFromRoomList, this._isStreamRoom);
        this._html = new SpottedScript.Controls.ChatClient.Shared.RoomHtml(this._stub, this._loggedIn);
        this.initialiseElements('');
        this._genericInitialise();
    },
    initialiseFromStub: function SpottedScript_Controls_ChatClient_Room$initialiseFromStub(roomStub, roomList, controllerStateStore) {
        /// <param name="roomStub" type="SpottedScript.Controls.ChatClient.Shared.RoomStub">
        /// </param>
        /// <param name="roomList" type="Object" domElement="true">
        /// </param>
        /// <param name="controllerStateStore" type="Array" elementType="StateStub">
        /// </param>
        this._stub = roomStub;
        this.guid = this._stub.guid;
        this.name = this._stub.name;
        this.url = this._stub.url;
        this.pinable = this._stub.pinable;
        this._pinned = this._stub.pinned;
        this._starred = this._stub.starred;
        this.starrable = this._stub.starrable;
        this._isStarredByDefault = this._stub.isStarredByDefault;
        this.readOnly = this._stub.readOnly;
        this.isPhotoChatRoom = this._stub.isPhotoChatRoom;
        this.isPrivateChatRoom = this._stub.isPrivateChatRoom;
        this.isNewPhotoAlertsRoom = this._stub.isNewPhotoAlertsRoom;
        this.presence = this._stub.presence;
        this._tokenDateTimeTicks = this._stub.tokenDateTimeTicks;
        this._token = this._stub.token;
        this._hasArchive = this._stub.hasArchive;
        this._state = new SpottedScript.Controls.ChatClient.Shared.StateStub();
        this._state.initialise(this._stub.guid, this._stub.selected, this._stub.guest, this._stub.newMessages, this._stub.totalMessages, this._stub.latestItem, this._stub.latestItemSeen, this._stub.latestItemOld, this._stub.listOrder, this._stub.tokenDateTimeTicks, this._stub.token);
        this._addToStateStoreIfNotAlreadyThere(controllerStateStore, this._state);
        this._html = new SpottedScript.Controls.ChatClient.Shared.RoomHtml(this._stub, this._loggedIn);
        var outerClientID = roomStub.parentClientID + '_Room_' + this.guid + '_Outer';
        var newNode = document.createElement('span');
        newNode.id = outerClientID;
        newNode.innerHTML = this._html.toHtml();
        roomList.appendChild(newNode.firstChild);
        this.initialiseElements(outerClientID);
        this._genericInitialise();
    },
    _genericInitialise: function SpottedScript_Controls_ChatClient_Room$_genericInitialise() {
        if (this.isNewPhotoAlertsRoom) {
            this._onlyRenderItemsWhenSelected = true;
            this._onlyRenderItemsWhenSelectedMaxItems = 10;
        }
        else {
            this._onlyRenderItemsWhenSelected = false;
            this._onlyRenderItemsWhenSelectedMaxItems = 0;
        }
    },
    _addToStateStoreIfNotAlreadyThere: function SpottedScript_Controls_ChatClient_Room$_addToStateStoreIfNotAlreadyThere(stateStore, state) {
        /// <param name="stateStore" type="Array" elementType="StateStub">
        /// </param>
        /// <param name="state" type="SpottedScript.Controls.ChatClient.Shared.StateStub">
        /// </param>
        for (var k = 0; k < stateStore.length; k++) {
            var ss = stateStore[k];
            if (ss.guid === state.guid) {
                return;
            }
        }
        stateStore[stateStore.length] = state;
    },
    removeFromRoomsList: function SpottedScript_Controls_ChatClient_Room$removeFromRoomsList() {
        if (this._outerElement != null) {
            this._outerElement.parentNode.removeChild(this._outerElement);
        }
        else {
            this._roomElement.parentNode.removeChild(this._roomElement);
        }
    },
    addToRoomsList: function SpottedScript_Controls_ChatClient_Room$addToRoomsList(parent) {
        /// <param name="parent" type="Object" domElement="true">
        /// </param>
        parent.appendChild(this._roomElement);
    },
    prepareForRemoval: function SpottedScript_Controls_ChatClient_Room$prepareForRemoval() {
        this.removeFromRoomsList();
        this._messsagesElementHolder.parentNode.removeChild(this._messsagesElementHolder);
        this._moreInfoElement.parentNode.removeChild(this._moreInfoElement);
    },
    setListOrder: function SpottedScript_Controls_ChatClient_Room$setListOrder(listOrder) {
        /// <param name="listOrder" type="Number" integer="true">
        /// </param>
        this._state.listOrder = listOrder;
    },
    updateUIAfterGuestPinAction: function SpottedScript_Controls_ChatClient_Room$updateUIAfterGuestPinAction() {
        this._updateUI();
    },
    addItem: function SpottedScript_Controls_ChatClient_Room$addItem(item, itemTracker) {
        /// <param name="item" type="SpottedScript.Controls.ChatClient.Items.Item">
        /// </param>
        /// <param name="itemTracker" type="Object">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this.get_guest() && !item.fromGuestRefresh) {
            return false;
        }
        if (!this.get_guest() && !this.get_pinned()) {
            return false;
        }
        this.set__latestItem(item.guid);
        if (SpottedScript.Controls.ChatClient.Items.Html.isInstanceOfType(item)) {
            this._latestHtmlItem = item;
            if (!this._isStreamRoom) {
                if (SpottedScript.Controls.ChatClient.Items.Newable.isInstanceOfType(item)) {
                    this._needsNewStatusUpdate = true;
                }
                if (item.serverRequestIndex > 0) {
                    if (SpottedScript.Controls.ChatClient.Items.Newable.isInstanceOfType(item)) {
                        (item).set_isInNewSection(true);
                    }
                }
            }
            else {
                if (SpottedScript.Controls.ChatClient.Items.Newable.isInstanceOfType(item)) {
                    (item).set_isInNewSection(false);
                }
            }
            if (!this._onlyRenderItemsWhenSelected) {
                this._renderItemToHtmlNow(item, true);
            }
            if (itemTracker != null) {
                if (itemTracker[this.guid] == null) {
                    itemTracker[this.guid] = [];
                }
                var roomTracker = itemTracker[this.guid];
                roomTracker[roomTracker.length] = item;
            }
            if (this._hideMoreInfoOnNextReceivedItem && this.get_moreInfoVisible()) {
                this.hideMoreInfo(false);
            }
        }
        else if (SpottedScript.Controls.ChatClient.Items.TopPhoto.isInstanceOfType(item)) {
            this._hasTopPhoto = true;
            this._topPhoto = item;
            this._updateUI();
        }
        this._items[this._items.length] = item;
        return true;
    },
    finaliseRequest: function SpottedScript_Controls_ChatClient_Room$finaliseRequest(serverRequestIndex) {
        /// <param name="serverRequestIndex" type="Number" integer="true">
        /// </param>
        if (this._onlyRenderItemsWhenSelected && this.get_selected()) {
            this._onlyRenderItemsWhenSelectedRenderItemsNow();
        }
        if (serverRequestIndex === 0 || this.requestIndex === 0) {
            if (this.get__latestItemSeen() !== this.get__latestItem()) {
                this.set__latestItemOld(this.get__latestItemSeen());
                if (this.get_selectedAndMessagesVisible()) {
                    this.set__latestItemSeen(this.get__latestItem());
                }
            }
        }
        else {
            if (this.get_selectedAndMessagesVisible()) {
                this.set__latestItemOld(this.get__latestItemSeen());
                this.set__latestItemSeen(this.get__latestItem());
            }
            else {
                if (this.get__latestItemOld() !== this.get__latestItemSeen()) {
                    this.set__latestItemOld(this.get__latestItemSeen());
                }
            }
        }
        if (serverRequestIndex === 0 && this.get_selected()) {
            if (!this._hasArchive || (this._haveCheckedArchive && !this._hasArchiveItems)) {
                this._showMoreInfoAfterDelayIfNoMessages();
            }
            else {
                this._getArchiveAfterDelayIfLessThan20Messages();
            }
        }
        if (this._latestHtmlItem != null) {
            if (SpottedScript.Controls.ChatClient.Items.Message.isInstanceOfType(this._latestHtmlItem)) {
                var m = this._latestHtmlItem;
                this._hasChatPic = true;
                this._chatPic = m.anyChatPic;
                this._chatPicStmuParams = m.stmuParams;
                this._chatPicUrl = '/members/' + m.nickName.toLowerCase();
            }
            else {
                this._hasChatPic = false;
            }
        }
        else {
            this._hasChatPic = false;
        }
        this._updateNewStatus();
        this._updateUI();
        this.requestIndex++;
    },
    _renderItemToHtmlNow: function SpottedScript_Controls_ChatClient_Room$_renderItemToHtmlNow(item, insertAtTop) {
        /// <param name="item" type="SpottedScript.Controls.ChatClient.Items.Html">
        /// </param>
        /// <param name="insertAtTop" type="Boolean">
        /// </param>
        var newNode = document.createElement('span');
        newNode.innerHTML = (item).toHtml();
        var messageList = (this._isStreamRoom) ? this._parent.streamList : this._messsagesElement;
        if (messageList.hasChildNodes() && insertAtTop) {
            messageList.insertBefore(newNode, messageList.childNodes[0]);
        }
        else {
            messageList.appendChild(newNode);
        }
        (item).initialiseElements();
    },
    _onlyRenderItemsWhenSelectedLatestItemWhenLastRefreshed: null,
    _onlyRenderItemsWhenSelectedRenderItemsNow: function SpottedScript_Controls_ChatClient_Room$_onlyRenderItemsWhenSelectedRenderItemsNow() {
        if (this._onlyRenderItemsWhenSelectedLatestItemWhenLastRefreshed !== this.get__latestItem()) {
            this._onlyRenderItemsWhenSelectedLatestItemWhenLastRefreshed = this.get__latestItem();
            this._messsagesElement.innerHTML = '';
            var htmlItemCount = 0;
            for (var i = this._items.length - 1; i >= 0; i--) {
                var item = this._items[i];
                if (SpottedScript.Controls.ChatClient.Items.Html.isInstanceOfType(item)) {
                    htmlItemCount++;
                    this._renderItemToHtmlNow(item, false);
                    if (this._onlyRenderItemsWhenSelectedMaxItems > 0 && htmlItemCount >= this._onlyRenderItemsWhenSelectedMaxItems) {
                        break;
                    }
                }
            }
        }
    },
    clearItems: function SpottedScript_Controls_ChatClient_Room$clearItems() {
        this._items = [];
        this._messsagesElement.innerHTML = '';
        this._hasChatPic = false;
        this._updateUI();
    },
    get_selectedAndMessagesVisible: function SpottedScript_Controls_ChatClient_Room$get_selectedAndMessagesVisible() {
        /// <value type="Boolean"></value>
        return this.get_selected() && !this.get_moreInfoVisible();
    },
    get_moreInfoVisible: function SpottedScript_Controls_ChatClient_Room$get_moreInfoVisible() {
        /// <value type="Boolean"></value>
        return this._moreInfoVisible;
    },
    _moreInfoVisible: false,
    showMoreInfo: function SpottedScript_Controls_ChatClient_Room$showMoreInfo(hideMoreInfoOnNextReceivedItem) {
        /// <param name="hideMoreInfoOnNextReceivedItem" type="Boolean">
        /// </param>
        this._hideMoreInfoOnNextReceivedItem = hideMoreInfoOnNextReceivedItem;
        if (this._moreInfoElement.innerHTML.length > 0) {
            this._moreInfoVisible = true;
            this._updateUI();
        }
        else {
            if (this.getMoreInfoHtml != null) {
                this.getMoreInfoHtml(this, new SpottedScript.Controls.ChatClient.RoomGuidEventArgs(this.guid));
            }
        }
    },
    _hideMoreInfoOnNextReceivedItem: false,
    storeMoreInfoHtmlAndShowMoreInfo: function SpottedScript_Controls_ChatClient_Room$storeMoreInfoHtmlAndShowMoreInfo(moreInfoHtml) {
        /// <param name="moreInfoHtml" type="String">
        /// </param>
        if (moreInfoHtml.length > 0) {
            this._moreInfoElement.innerHTML = moreInfoHtml;
            this._moreInfoVisible = true;
            this._updateUI();
        }
    },
    hideMoreInfo: function SpottedScript_Controls_ChatClient_Room$hideMoreInfo(resetItems) {
        /// <param name="resetItems" type="Boolean">
        /// </param>
        this._moreInfoVisible = false;
        if (this.get_selected() && resetItems) {
            this._resetItemsOnSelect();
        }
        this._updateUI();
    },
    _showMoreInfoIfNoMessagesLastRandom: 0,
    _showMoreInfoIfNoMessages: function SpottedScript_Controls_ChatClient_Room$_showMoreInfoIfNoMessages(showMoreInfoIfNoMessagesRandom) {
        /// <param name="showMoreInfoIfNoMessagesRandom" type="Number" integer="true">
        /// </param>
        if (this.get_selected() && this.get_itemCount() === 0 && !this.get_moreInfoVisible() && this._showMoreInfoIfNoMessagesLastRandom === showMoreInfoIfNoMessagesRandom) {
            this.showMoreInfo(true);
        }
    },
    _showMoreInfoAfterDelayIfNoMessages: function SpottedScript_Controls_ChatClient_Room$_showMoreInfoAfterDelayIfNoMessages() {
        this._showMoreInfoIfNoMessagesLastRandom = Math.floor(Math.random() * 10000);
        window.setTimeout(Function.createDelegate(this, function() {
            this._showMoreInfoIfNoMessages(this._showMoreInfoIfNoMessagesLastRandom);
        }), 500);
    },
    showArchiveItems: function SpottedScript_Controls_ChatClient_Room$showArchiveItems(itemsJson) {
        /// <param name="itemsJson" type="String">
        /// </param>
        var htmlItemCount = 0;
        this._haveCheckedArchive = true;
        if (itemsJson.length > 0) {
            var itemStubArray = eval(' [ ' + itemsJson + ' ] ');
            var itemsArray = [];
            var firstNewable = null;
            var lastNewable = null;
            for (var i = 0; i < itemStubArray.length; i++) {
                var alreadyHaveThisItem = false;
                for (var j = 0; j < this._items.length; j++) {
                    if (this._items[j].guid === itemStubArray[i].guid) {
                        alreadyHaveThisItem = true;
                        break;
                    }
                }
                if (!alreadyHaveThisItem) {
                    var item = SpottedScript.Controls.ChatClient.Items.Item.create(itemStubArray[i], this._parent, this.requestIndex, false, 1);
                    if (SpottedScript.Controls.ChatClient.Items.Html.isInstanceOfType(item)) {
                        if (htmlItemCount === 0) {
                            var newNode = document.createElement('div');
                            newNode.className = 'ChatClientArchiveHeading';
                            newNode.innerHTML = 'Archived messages:';
                            this._messsagesElement.appendChild(newNode);
                        }
                        this._hasArchiveItems = true;
                        htmlItemCount++;
                        this._renderItemToHtmlNow(item, false);
                        if (SpottedScript.Controls.ChatClient.Items.Newable.isInstanceOfType(item)) {
                            if (firstNewable == null) {
                                firstNewable = item;
                            }
                            lastNewable = item;
                        }
                    }
                }
            }
            if (firstNewable != null) {
                firstNewable.updateClassModifiersAllAtOnce(true, false, false);
            }
            if (lastNewable != null) {
                lastNewable.updateClassModifiersAllAtOnce(false, true, false);
            }
        }
        if (htmlItemCount === 0 && this.get_selected() && this.get_itemCount() === 0 && !this.get_moreInfoVisible()) {
            this.showMoreInfo(true);
        }
    },
    getArchiveItemsNow: function SpottedScript_Controls_ChatClient_Room$getArchiveItemsNow() {
        if (this._haveCheckedArchive || !this._hasArchive) {
            return;
        }
        if (this.getArchiveItems != null) {
            this.getArchiveItems(this, new SpottedScript.Controls.ChatClient.RoomGuidEventArgs(this.guid));
        }
    },
    _getArchiveLastRandom: 0,
    _getArchiveIfLessThan20Messages: function SpottedScript_Controls_ChatClient_Room$_getArchiveIfLessThan20Messages(getArchiveRandom) {
        /// <param name="getArchiveRandom" type="Number" integer="true">
        /// </param>
        if (this._haveCheckedArchive || !this._hasArchive) {
            return;
        }
        if (this.get_selected() && this.get_itemCount() < 20 && this._getArchiveLastRandom === getArchiveRandom) {
            this.getArchiveItemsNow();
        }
    },
    _getArchiveAfterDelayIfLessThan20Messages: function SpottedScript_Controls_ChatClient_Room$_getArchiveAfterDelayIfLessThan20Messages() {
        if (this._haveCheckedArchive || !this._hasArchive) {
            return;
        }
        this._getArchiveLastRandom = Math.floor(Math.random() * 10000);
        window.setTimeout(Function.createDelegate(this, function() {
            this._getArchiveIfLessThan20Messages(this._getArchiveLastRandom);
        }), 500);
    },
    _updateNewStatus: function SpottedScript_Controls_ChatClient_Room$_updateNewStatus() {
        var unseenCount = 0;
        if (this._items.length > 0 && this._needsNewStatusUpdate) {
            var newableCount = 0;
            var itemIndexOfLastNewable = -1;
            var itemIndexOfLastNewNewable = -1;
            var previousNewableWasInNewSection = false;
            var foundFirstOldNewableThatWasPreviouslyOld = false;
            var foundFirstOldItem = false;
            var foundFirstSeenItem = false;
            for (var i = this._items.length - 1; i >= 0; i--) {
                if (this._items[i].guid === this.get__latestItemOld()) {
                    foundFirstOldItem = true;
                }
                if (this._items[i].guid === this.get__latestItemSeen()) {
                    foundFirstSeenItem = true;
                }
                if (SpottedScript.Controls.ChatClient.Items.Newable.isInstanceOfType(this._items[i])) {
                    var n = this._items[i];
                    var isTopOfSection = false;
                    var isBottomOfSection = false;
                    var isInNewSection = false;
                    if (newableCount === 0) {
                        isTopOfSection = true;
                    }
                    itemIndexOfLastNewable = i;
                    if (!foundFirstSeenItem) {
                        unseenCount++;
                    }
                    if (!foundFirstOldItem) {
                        isInNewSection = true;
                        itemIndexOfLastNewNewable = i;
                        previousNewableWasInNewSection = true;
                    }
                    else {
                        isInNewSection = false;
                        if (previousNewableWasInNewSection) {
                            isTopOfSection = true;
                        }
                        previousNewableWasInNewSection = false;
                        if (!n.get_isInNewSection()) {
                            foundFirstOldNewableThatWasPreviouslyOld = true;
                        }
                    }
                    n.updateClassModifiersAllAtOnce(isTopOfSection, isBottomOfSection, isInNewSection);
                    newableCount++;
                    if (this._doneFullUpdateOfNewStatus && foundFirstOldNewableThatWasPreviouslyOld) {
                        break;
                    }
                }
            }
            if (!this._doneFullUpdateOfNewStatus && itemIndexOfLastNewable > -1) {
                (this._items[itemIndexOfLastNewable]).set_isBottomOfSection(true);
            }
            if (itemIndexOfLastNewNewable > -1) {
                (this._items[itemIndexOfLastNewNewable]).set_isBottomOfSection(true);
            }
            this._doneFullUpdateOfNewStatus = true;
            this._needsNewStatusUpdate = false;
        }
        if (unseenCount !== this.get__newMessages() || this.get__totalMessages() !== this._items.length) {
            this.set__totalMessages(this._items.length);
            this.set__newMessages(unseenCount);
            this._updateUI();
        }
    },
    initialiseElements: function SpottedScript_Controls_ChatClient_Room$initialiseElements(outerClientID) {
        /// <param name="outerClientID" type="String">
        /// </param>
        if (outerClientID.length > 0) {
            this._outerElement = document.getElementById(outerClientID);
        }
        this._roomElement = document.getElementById(this._html.clientID);
        this._linkElement = document.getElementById(this._html.linkID);
        this._crossElement = document.getElementById(this._html.crossID);
        this._presenceElement = document.getElementById(this._html.presenceID);
        this._totalElement = document.getElementById(this._html.totalID);
        this._statsSeperatorElement = document.getElementById(this._html.statsSeperatorID);
        this._unreadElement = document.getElementById(this._html.unreadID);
        this._elementsInitialised = true;
        this._updateUI();
    },
    _updateUI: function SpottedScript_Controls_ChatClient_Room$_updateUI() {
        this._updateTopPhotoUI();
        this._updateChatPicUI();
        this._updateRoomUI();
        this._updatePresenceUI();
        this._updateLinkUI();
        this._updateStatsUI();
        this._updateMessagesUI();
    },
    _updateTopPhotoUI: function SpottedScript_Controls_ChatClient_Room$_updateTopPhotoUI() {
        if (this._elementsInitialised) {
            if (this._hasTopPhoto) {
                if (this._topPhotoHolder == null) {
                    this._topPhotoHolder = document.createElement('div');
                    this._topPhotoHolder.style.position = 'relative';
                    this._topPhotoHolder.style.width = '280px';
                    this._topPhotoHolder.style.height = '120px';
                    this._messsagesElementHolder.insertBefore(this._topPhotoHolder, this._messsagesElementHolder.childNodes[0]);
                    this._topPhotoAnchor = document.createElement('a');
                    this._topPhotoAnchor.style.position = 'absolute';
                    this._topPhotoAnchor.style.top = '9px';
                    this._topPhotoAnchor.style.left = '96px';
                    this._topPhotoImage = document.createElement('img');
                    this._topPhotoImage.className = 'BorderBlack All';
                    this._topPhotoImage.style.width = '100px';
                    this._topPhotoImage.style.height = '100px';
                    this._topPhotoAnchor.appendChild(this._topPhotoImage);
                    this._topPhotoHolder.appendChild(this._topPhotoAnchor);
                    var txtSpan = document.createElement('span');
                    txtSpan.style.textAlign = 'right';
                    txtSpan.innerHTML = '<small><a href=\"/pages/frontpagephotos\">Get&nbsp;yours<br />here!</a></small>';
                    txtSpan.style.position = 'absolute';
                    txtSpan.style.top = '7px';
                    txtSpan.style.left = '225px';
                    txtSpan.className = 'CleanLinks';
                    this._topPhotoHolder.appendChild(txtSpan);
                    var txtSpan1 = document.createElement('span');
                    txtSpan1.style.textAlign = 'left';
                    txtSpan1.innerHTML = '<small>Chat&nbsp;about<br />this:</small>';
                    txtSpan1.style.position = 'absolute';
                    txtSpan1.style.top = '7px';
                    txtSpan1.style.left = '5px';
                    this._topPhotoHolder.appendChild(txtSpan1);
                }
                this._topPhotoImage.src = SpottedScript.Misc.getPicUrlFromGuid(this._topPhoto.photoIcon);
                $clearHandlers(this._topPhotoImage);
                $addHandler(this._topPhotoImage, 'mouseover', Function.createDelegate(this, function() {
                    eval('stm(\'<img src=' + SpottedScript.Misc.getPicUrlFromGuid(this._topPhoto.photoWeb) + ' width=' + this._topPhoto.photoWebWidth.toString() + ' height=' + this._topPhoto.photoWebHeight.toString() + ' class=Block />\');');
                }));
                $addHandler(this._topPhotoImage, 'mouseout', Function.createDelegate(this, function() {
                    htm();;
                }));
                this._topPhotoAnchor.href = this._topPhoto.photoUrl;
            }
        }
    },
    _updateChatPicUI: function SpottedScript_Controls_ChatClient_Room$_updateChatPicUI() {
        if (this._elementsInitialised) {
            if (this._hasTopPhoto) {
                if (this._chatPicHolder != null) {
                    this._chatPicHolder.style.display = 'none';
                }
                return;
            }
            if (this._hasChatPic) {
                if (this._chatPicHolder == null) {
                    this._chatPicHolder = document.createElement('div');
                    this._messsagesElementHolder.insertBefore(this._chatPicHolder, this._messsagesElementHolder.childNodes[0]);
                    this._chatPicAnchor = document.createElement('a');
                    this._chatPicImage = document.createElement('img');
                    this._chatPicImage.className = 'ChatClientChatPicImage';
                    this._chatPicAnchor.appendChild(this._chatPicImage);
                    this._chatPicHolder.appendChild(this._chatPicAnchor);
                }
                this._chatPicImage.src = SpottedScript.Misc.getPicUrlFromGuid(this._chatPic);
                $clearHandlers(this._chatPicImage);
                $addHandler(this._chatPicImage, 'mouseover', Function.createDelegate(this, function() {
                    eval('stmun(' + this._chatPicStmuParams + ');');
                }));
                $addHandler(this._chatPicImage, 'mouseout', Function.createDelegate(this, function() {
                    htm();;
                }));
                this._chatPicAnchor.href = this._chatPicUrl;
            }
            else {
                if (this._chatPicHolder != null) {
                    this._chatPicHolder.style.display = 'none';
                }
            }
        }
    },
    _updateRoomUI: function SpottedScript_Controls_ChatClient_Room$_updateRoomUI() {
        if (this._elementsInitialised) {
            this._roomElement.className = (!this.get_pinned()) ? 'ChatClientRoomHolder ChatClientRoomUnpinned' : (this.get_selected()) ? 'ChatClientRoomHolder ChatClientRoomSelected' : 'ChatClientRoomHolder';
        }
    },
    _updatePresenceUI: function SpottedScript_Controls_ChatClient_Room$_updatePresenceUI() {
        if (this._elementsInitialised) {
            if (this._loggedIn && this.isPrivateChatRoom && (this.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.chatting || this.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.online)) {
                this._presenceElement.setAttribute('src', (this.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.chatting) ? '/gfx/chat-chatting.png' : '/gfx/chat-online.png');
                this._presenceElement.style.width = (this.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.chatting) ? '13' : '9';
            }
        }
    },
    _updateLinkUI: function SpottedScript_Controls_ChatClient_Room$_updateLinkUI() {
        if (this._elementsInitialised) {
            if (this.get_pinned() || this.get_guest()) {
                this._linkElement.style.textDecoration = '';
                this._linkElement.className = (this.get_selected()) ? 'ChatClientRoomLink ChatClientRoomLinkSelected' : ((this.get__newMessages() === 0) ? 'ChatClientRoomLink ChatClientRoomLinkNoUnread' : 'ChatClientRoomLink');
            }
            else {
                this._linkElement.style.textDecoration = 'line-through';
                this._linkElement.className = 'ChatClientRoomLink ChatClientRoomLinkNoUnread';
            }
        }
    },
    _updateStatsUI: function SpottedScript_Controls_ChatClient_Room$_updateStatsUI() {
        if (this._elementsInitialised) {
            if (this.get_pinned() || this.get_guest()) {
                this._totalElement.innerHTML = (this.get__totalMessages() > 0) ? this.get__totalMessages().toString() : '&nbsp;';
                this._unreadElement.innerHTML = (this.get__newMessages() > 0) ? this.get__newMessages().toString() : '&nbsp;';
                this._statsSeperatorElement.innerHTML = (this.get__newMessages() > 0) ? '/' : '&nbsp;';
            }
            else {
                this._totalElement.innerHTML = '&nbsp;';
                this._unreadElement.innerHTML = '&nbsp;';
                this._statsSeperatorElement.innerHTML = '&nbsp;';
            }
        }
    },
    _updateMessagesUI: function SpottedScript_Controls_ChatClient_Room$_updateMessagesUI() {
        if (this._messsagesElementHolder.style.display !== ((this.get_selected() && !this.get_moreInfoVisible()) ? '' : 'none')) {
            this._messsagesElementHolder.style.display = (this.get_selected() && !this.get_moreInfoVisible()) ? '' : 'none';
        }
        if (this._moreInfoElement.style.display !== ((this.get_selected() && this.get_moreInfoVisible()) ? '' : 'none')) {
            this._moreInfoElement.style.display = (this.get_selected() && this.get_moreInfoVisible()) ? '' : 'none';
        }
    },
    updateStatsAfterUnPause: function SpottedScript_Controls_ChatClient_Room$updateStatsAfterUnPause(ss) {
        /// <param name="ss" type="SpottedScript.Controls.ChatClient.Shared.StateStub">
        /// </param>
        this.set__latestItemOld(ss.latestItemOld);
        this.set__latestItemSeen(ss.latestItemSeen);
        this.set__newMessages(ss.newMessages);
        this.set__totalMessages(ss.totalMessages);
        this._updateUI();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.StarActionEventArgs
SpottedScript.Controls.ChatClient.StarActionEventArgs = function SpottedScript_Controls_ChatClient_StarActionEventArgs(roomGuid, starred) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="starred" type="Boolean">
    /// </param>
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="starred" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.StarActionEventArgs.initializeBase(this);
    this.roomGuid = roomGuid;
    this.starred = starred;
}
SpottedScript.Controls.ChatClient.StarActionEventArgs.prototype = {
    roomGuid: null,
    starred: false
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.PinActionEventArgs
SpottedScript.Controls.ChatClient.PinActionEventArgs = function SpottedScript_Controls_ChatClient_PinActionEventArgs(roomGuid, pinned) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="pinned" type="Boolean">
    /// </param>
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="pinned" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.PinActionEventArgs.initializeBase(this);
    this.roomGuid = roomGuid;
    this.pinned = pinned;
}
SpottedScript.Controls.ChatClient.PinActionEventArgs.prototype = {
    roomGuid: null,
    pinned: false
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.RoomGuidEventArgs
SpottedScript.Controls.ChatClient.RoomGuidEventArgs = function SpottedScript_Controls_ChatClient_RoomGuidEventArgs(roomGuid) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="roomGuid" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.RoomGuidEventArgs.initializeBase(this);
    this.roomGuid = roomGuid;
}
SpottedScript.Controls.ChatClient.RoomGuidEventArgs.prototype = {
    roomGuid: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.ServerClass
SpottedScript.Controls.ChatClient.ServerClass = function SpottedScript_Controls_ChatClient_ServerClass(controller, sessionID, lastActionTicks, controllerRoomState) {
    /// <param name="controller" type="SpottedScript.Controls.ChatClient.Controller">
    /// </param>
    /// <param name="sessionID" type="Number" integer="true">
    /// </param>
    /// <param name="lastActionTicks" type="String">
    /// </param>
    /// <param name="controllerRoomState" type="Array" elementType="StateStub">
    /// </param>
    /// <field name="_controller" type="SpottedScript.Controls.ChatClient.Controller">
    /// </field>
    /// <field name="_sessionID" type="Number" integer="true">
    /// </field>
    /// <field name="_lastActionTicks" type="String">
    /// </field>
    /// <field name="_lastItemGuid" type="String">
    /// </field>
    /// <field name="_requestIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_webRequestIndex" type="Number" integer="true">
    /// Each call to a web service has a uniquer call id.
    /// </field>
    /// <field name="_roomState" type="Array" elementType="StateStub">
    /// </field>
    /// <field name="_criticalRequestQueue" type="Array">
    /// </field>
    /// <field name="_criticalRequestInProgress" type="Boolean">
    /// </field>
    /// <field name="_periodicBackgroundRefreshInProgress" type="Boolean">
    /// </field>
    /// <field name="_cancelCurrentPeriodicBackgroundRefresh" type="Boolean">
    /// </field>
    /// <field name="_periodicBackgroundRefreshIsPaused" type="Boolean">
    /// </field>
    /// <field name="gotItems" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotNoItems" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotWrongSessionException" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotTimeoutException" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotGenericException" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotRoom" type="SpottedScript.Controls.ChatClient.GotRoomHandler">
    /// </field>
    /// <field name="gotNewPhotoRoom" type="SpottedScript.Controls.ChatClient.GotRoomHandler">
    /// </field>
    /// <field name="gotMoreInfo" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotArchiveItems" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotRoomState" type="Sys.EventHandler">
    /// </field>
    /// <field name="debugPrint" type="Sys.EventHandler">
    /// </field>
    /// <field name="showLoadingIcon" type="Sys.EventHandler">
    /// </field>
    /// <field name="hideLoadingIcon" type="Sys.EventHandler">
    /// </field>
    /// <field name="doneDeleteArchive" type="Sys.EventHandler">
    /// </field>
    this._sessionID = sessionID;
    this._lastActionTicks = lastActionTicks;
    this._lastItemGuid = '';
    this._requestIndex = 0;
    this._controller = controller;
    this._criticalRequestQueue = [];
    this._criticalRequestInProgress = false;
    this._periodicBackgroundRefreshInProgress = false;
    this._cancelCurrentPeriodicBackgroundRefresh = false;
    this._periodicBackgroundRefreshIsPaused = false;
    this._roomState = controllerRoomState;
}
SpottedScript.Controls.ChatClient.ServerClass.prototype = {
    _controller: null,
    _sessionID: 0,
    _lastActionTicks: null,
    _lastItemGuid: null,
    _requestIndex: 0,
    _webRequestIndex: 0,
    _roomState: null,
    _criticalRequestQueue: null,
    _criticalRequestInProgress: false,
    _periodicBackgroundRefreshInProgress: false,
    _cancelCurrentPeriodicBackgroundRefresh: false,
    _periodicBackgroundRefreshIsPaused: false,
    gotItems: null,
    gotNoItems: null,
    gotWrongSessionException: null,
    gotTimeoutException: null,
    gotGenericException: null,
    gotRoom: null,
    gotNewPhotoRoom: null,
    gotMoreInfo: null,
    gotArchiveItems: null,
    gotRoomState: null,
    debugPrint: null,
    showLoadingIcon: null,
    hideLoadingIcon: null,
    doneDeleteArchive: null,
    start: function SpottedScript_Controls_ChatClient_ServerClass$start() {
        this._startChatRefreshIfIdle();
        window.setInterval(Function.createDelegate(this, this._startChatRefreshIfIdle), 1000);
    },
    _sendOrQueue: function SpottedScript_Controls_ChatClient_ServerClass$_sendOrQueue(criticalRequest) {
        /// <param name="criticalRequest" type="SpottedScript.Controls.ChatClient.CriticalRequest">
        /// </param>
        if (this.showLoadingIcon != null) {
            this.showLoadingIcon(this, null);
        }
        if (this._criticalRequestQueue.length === 0 && !this._criticalRequestInProgress) {
            if (this._periodicBackgroundRefreshInProgress) {
                this._cancelCurrentPeriodicBackgroundRefresh = true;
            }
            this._criticalRequestInProgress = true;
            criticalRequest.sendNow();
            this._debug('Processing request immediately');
        }
        else {
            Array.enqueue(this._criticalRequestQueue, criticalRequest);
            this._debug('Queueing request...');
        }
    },
    _continueProcessingCriticalRequestQueue: function SpottedScript_Controls_ChatClient_ServerClass$_continueProcessingCriticalRequestQueue() {
        if (this._criticalRequestQueue.length > 0) {
            if (this.showLoadingIcon != null) {
                this.showLoadingIcon(this, null);
            }
            var criticalRequest = Array.dequeue(this._criticalRequestQueue);
            criticalRequest.sendNow();
            this._debug('Processing queued request (' + this._criticalRequestQueue.length + ' in the queue)');
        }
        else {
            if (this.hideLoadingIcon != null) {
                this.hideLoadingIcon(this, null);
            }
            this._criticalRequestInProgress = false;
        }
    },
    _startChatRefreshIfIdle: function SpottedScript_Controls_ChatClient_ServerClass$_startChatRefreshIfIdle() {
        if (this._periodicBackgroundRefreshIsPaused) {
            return;
        }
        if (this._criticalRequestQueue.length === 0 && !this._criticalRequestInProgress && !this._periodicBackgroundRefreshInProgress) {
            this._periodicBackgroundRefreshInProgress = true;
            this.invokeRefreshChat();
        }
        if (this._criticalRequestQueue.length === 0 && !this._criticalRequestInProgress) {
            if (this.hideLoadingIcon != null) {
                this.hideLoadingIcon(this, null);
            }
        }
        else {
            if (this.showLoadingIcon != null) {
                this.showLoadingIcon(this, null);
            }
        }
    },
    sendMessage: function SpottedScript_Controls_ChatClient_ServerClass$sendMessage(message, roomGuid) {
        /// <param name="message" type="String">
        /// </param>
        /// <param name="roomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.SendMessageRequest(this, message, roomGuid);
        this._sendOrQueue(r);
    },
    invokeSendMessage: function SpottedScript_Controls_ChatClient_ServerClass$invokeSendMessage(message, roomGuid) {
        /// <param name="message" type="String">
        /// </param>
        /// <param name="roomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.send(message, roomGuid, this._lastItemGuid, this._sessionID, '', this._roomState, Function.createDelegate(this, this.sendSuccessCallback), Function.createDelegate(this, this.sendFailureCallback), ++this._webRequestIndex, 2500);
    },
    sendSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$sendSuccessCallback(s, userContext, methodName) {
        /// <param name="s" type="SpottedScript.Controls.ChatClient.Shared.RefreshStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (s != null) {
            this._processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, '', false);
        }
        this._continueProcessingCriticalRequestQueue();
    },
    sendFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$sendFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    deleteArchive: function SpottedScript_Controls_ChatClient_ServerClass$deleteArchive(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.DeleteArchiveRequest(this, roomGuid);
        this._sendOrQueue(r);
    },
    invokeDeleteArchive: function SpottedScript_Controls_ChatClient_ServerClass$invokeDeleteArchive(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.deleteArchive(roomGuid, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.deleteArchiveSuccessCallback), Function.createDelegate(this, this.deleteArchiveFailureCallback), ++this._webRequestIndex, 5000);
    },
    deleteArchiveSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$deleteArchiveSuccessCallback(s, userContext, methodName) {
        /// <param name="s" type="SpottedScript.Controls.ChatClient.Shared.DeleteArchiveStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (s != null) {
            if (this.doneDeleteArchive != null) {
                this.doneDeleteArchive(this, new SpottedScript.Controls.ChatClient.RoomGuidEventArgs(s.roomGuid));
            }
            this._processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, '', false);
        }
        this._continueProcessingCriticalRequestQueue();
    },
    deleteArchiveFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$deleteArchiveFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    getArchiveItems: function SpottedScript_Controls_ChatClient_ServerClass$getArchiveItems(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.ArchiveItemsRequest(this, roomGuid);
        this._sendOrQueue(r);
    },
    invokeGetArchiveItems: function SpottedScript_Controls_ChatClient_ServerClass$invokeGetArchiveItems(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.getArchive(roomGuid, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.getArchiveItemsSuccessCallback), Function.createDelegate(this, this.getArchiveItemsFailureCallback), ++this._webRequestIndex, 5000);
    },
    getArchiveItemsSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$getArchiveItemsSuccessCallback(s, userContext, methodName) {
        /// <param name="s" type="SpottedScript.Controls.ChatClient.Shared.ArchiveStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (s != null) {
            this._processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, '', false);
            if (this.gotArchiveItems != null) {
                this.gotArchiveItems(this, new SpottedScript.Controls.ChatClient.GotArchiveItemsEventArgs(s.roomGuid, s.archiveItems));
            }
        }
        this._continueProcessingCriticalRequestQueue();
    },
    getArchiveItemsFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$getArchiveItemsFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    getMoreInfo: function SpottedScript_Controls_ChatClient_ServerClass$getMoreInfo(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.MoreInfoRequest(this, roomGuid);
        this._sendOrQueue(r);
    },
    invokeGetMoreInfo: function SpottedScript_Controls_ChatClient_ServerClass$invokeGetMoreInfo(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.moreInfo(roomGuid, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.getMoreInfoSuccessCallback), Function.createDelegate(this, this.getMoreInfoFailureCallback), ++this._webRequestIndex, 2500);
    },
    getMoreInfoSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$getMoreInfoSuccessCallback(s, userContext, methodName) {
        /// <param name="s" type="SpottedScript.Controls.ChatClient.Shared.MoreInfoStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (s != null) {
            if (this.gotMoreInfo != null) {
                this.gotMoreInfo(this, new SpottedScript.Controls.ChatClient.GotMoreInfoEventArgs(s.roomGuid, s.moreInfoHtml));
            }
            this._processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, '', false);
        }
        this._continueProcessingCriticalRequestQueue();
    },
    getMoreInfoFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$getMoreInfoFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    switchPhotoRoom: function SpottedScript_Controls_ChatClient_ServerClass$switchPhotoRoom(newRoomGuid) {
        /// <param name="newRoomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.SwitchPhotoRoomRequest(this, newRoomGuid);
        this._sendOrQueue(r);
    },
    invokeSwitchPhotoRoom: function SpottedScript_Controls_ChatClient_ServerClass$invokeSwitchPhotoRoom(newRoomGuid) {
        /// <param name="newRoomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.switchPhotoRoom(this._controller.clientID, newRoomGuid, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.switchPhotoRoomSuccessCallback), Function.createDelegate(this, this.switchPhotoRoomFailureCallback), ++this._webRequestIndex, 2500);
    },
    switchPhotoRoomSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$switchPhotoRoomSuccessCallback(p, userContext, methodName) {
        /// <param name="p" type="SpottedScript.Controls.ChatClient.Shared.PinStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (p != null) {
            var excludeItemsFromPinnedRoom = false;
            if (this.gotNewPhotoRoom != null) {
                excludeItemsFromPinnedRoom = this.gotNewPhotoRoom(this, new SpottedScript.Controls.ChatClient.GotRoomEventArgs(p.roomStub));
            }
            this._processItems(p.itemsJson, p.lastActionTicks, p.lastItemGuidReturned, methodName, p.guestRefreshStubs, p.roomStub.guid, excludeItemsFromPinnedRoom);
        }
        this._continueProcessingCriticalRequestQueue();
    },
    switchPhotoRoomFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$switchPhotoRoomFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    pinRoom: function SpottedScript_Controls_ChatClient_ServerClass$pinRoom(newRoomGuid) {
        /// <param name="newRoomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.PinRoomRequest(this, newRoomGuid);
        this._sendOrQueue(r);
    },
    invokePinRoom: function SpottedScript_Controls_ChatClient_ServerClass$invokePinRoom(newRoomGuid) {
        /// <param name="newRoomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.pin(this._controller.clientID, newRoomGuid, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.pinSuccessCallback), Function.createDelegate(this, this.pinFailureCallback), ++this._webRequestIndex, 2500);
    },
    pinSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$pinSuccessCallback(p, userContext, methodName) {
        /// <param name="p" type="SpottedScript.Controls.ChatClient.Shared.PinStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (p != null) {
            var excludeItemsFromPinnedRoom = false;
            if (this.gotRoom != null) {
                excludeItemsFromPinnedRoom = this.gotRoom(this, new SpottedScript.Controls.ChatClient.GotRoomEventArgs(p.roomStub));
            }
            this._processItems(p.itemsJson, p.lastActionTicks, p.lastItemGuidReturned, methodName, p.guestRefreshStubs, p.roomStub.guid, excludeItemsFromPinnedRoom);
        }
        this._continueProcessingCriticalRequestQueue();
    },
    pinFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$pinFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    unPinRoom: function SpottedScript_Controls_ChatClient_ServerClass$unPinRoom(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.UnPinRoomRequest(this, roomGuid);
        this._sendOrQueue(r);
    },
    invokeUnPinRoom: function SpottedScript_Controls_ChatClient_ServerClass$invokeUnPinRoom(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.unPin(this._controller.clientID, roomGuid, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.unPinSuccessCallback), Function.createDelegate(this, this.unPinFailureCallback), ++this._webRequestIndex, 2500);
    },
    unPinSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$unPinSuccessCallback(u, userContext, methodName) {
        /// <param name="u" type="SpottedScript.Controls.ChatClient.Shared.UnPinStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._processItems(u.itemsJson, u.lastActionTicks, u.lastItemGuidReturned, methodName, u.guestRefreshStubs, u.roomGuid, true);
        this._continueProcessingCriticalRequestQueue();
    },
    unPinFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$unPinFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    starRoom: function SpottedScript_Controls_ChatClient_ServerClass$starRoom(roomGuid, starred) {
        /// <param name="roomGuid" type="String">
        /// </param>
        /// <param name="starred" type="Boolean">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.StarRoomRequest(this, roomGuid, starred);
        this._sendOrQueue(r);
    },
    invokeStarRoom: function SpottedScript_Controls_ChatClient_ServerClass$invokeStarRoom(roomGuid, starred) {
        /// <param name="roomGuid" type="String">
        /// </param>
        /// <param name="starred" type="Boolean">
        /// </param>
        Spotted.WebServices.ChatService.star(this._controller.clientID, roomGuid, starred, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.starSuccessCallback), Function.createDelegate(this, this.starFailureCallback), ++this._webRequestIndex, 2500);
    },
    starSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$starSuccessCallback(r, userContext, methodName) {
        /// <param name="r" type="SpottedScript.Controls.ChatClient.Shared.RefreshStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, '', false);
        this._continueProcessingCriticalRequestQueue();
    },
    starFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$starFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    rePinRoom: function SpottedScript_Controls_ChatClient_ServerClass$rePinRoom(newRoomGuid) {
        /// <param name="newRoomGuid" type="String">
        /// </param>
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.RePinRoomRequest(this, newRoomGuid);
        this._sendOrQueue(r);
    },
    invokeRePinRoom: function SpottedScript_Controls_ChatClient_ServerClass$invokeRePinRoom(roomGuid) {
        /// <param name="roomGuid" type="String">
        /// </param>
        Spotted.WebServices.ChatService.rePin(this._controller.clientID, roomGuid, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.rePinSuccessCallback), Function.createDelegate(this, this.rePinFailureCallback), ++this._webRequestIndex, 2500);
    },
    rePinSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$rePinSuccessCallback(r, userContext, methodName) {
        /// <param name="r" type="SpottedScript.Controls.ChatClient.Shared.RefreshStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, '', false);
        this._continueProcessingCriticalRequestQueue();
    },
    rePinFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$rePinFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    storeUpdatedRoomListOrder: function SpottedScript_Controls_ChatClient_ServerClass$storeUpdatedRoomListOrder() {
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.StoreUpdatedRoomListOrderRequest(this);
        this._sendOrQueue(r);
    },
    invokeStoreUpdatedRoomListOrder: function SpottedScript_Controls_ChatClient_ServerClass$invokeStoreUpdatedRoomListOrder() {
        Spotted.WebServices.ChatService.storeUpdatedRoomListOrder(this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.storeUpdatedRoomListOrderSuccessCallback), Function.createDelegate(this, this.storeUpdatedRoomListOrderFailureCallback), ++this._webRequestIndex, 2500);
    },
    storeUpdatedRoomListOrderSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$storeUpdatedRoomListOrderSuccessCallback(s, userContext, methodName) {
        /// <param name="s" type="SpottedScript.Controls.ChatClient.Shared.RefreshStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, '', false);
        this._continueProcessingCriticalRequestQueue();
    },
    storeUpdatedRoomListOrderFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$storeUpdatedRoomListOrderFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    resumeAfterPause: function SpottedScript_Controls_ChatClient_ServerClass$resumeAfterPause() {
        this._periodicBackgroundRefreshIsPaused = false;
        var r = new SpottedScript.Controls.ChatClient.ForceResetSessionAndGetState(this);
        this._sendOrQueue(r);
    },
    invokeForceResetSessionAndGetState: function SpottedScript_Controls_ChatClient_ServerClass$invokeForceResetSessionAndGetState() {
        Spotted.WebServices.ChatService.resetSessionAndGetState(this._requestIndex === 0, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.forceResetSessionAndGetStateSuccessCallback), Function.createDelegate(this, this.forceResetSessionAndGetStateFailureCallback), ++this._webRequestIndex, 2500);
    },
    forceResetSessionAndGetStateSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$forceResetSessionAndGetStateSuccessCallback(result, userContext, methodName) {
        /// <param name="result" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        var r = result;
        if (this.gotRoomState != null) {
            this.gotRoomState(this, new SpottedScript.Controls.ChatClient.GotRoomStateEventArgs(r.roomState));
        }
        this._processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, '', false);
        this._continueProcessingCriticalRequestQueue();
    },
    forceResetSessionAndGetStateFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$forceResetSessionAndGetStateFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
        }
        this._continueProcessingCriticalRequestQueue();
    },
    invokeRefreshChat: function SpottedScript_Controls_ChatClient_ServerClass$invokeRefreshChat() {
        Spotted.WebServices.ChatService.refresh(this._requestIndex === 0, this._lastItemGuid, this._sessionID, this._lastActionTicks, '', this._roomState, Function.createDelegate(this, this.refreshSuccessCallback), Function.createDelegate(this, this.refreshFailureCallback), ++this._webRequestIndex, 2500);
    },
    refreshSuccessCallback: function SpottedScript_Controls_ChatClient_ServerClass$refreshSuccessCallback(result, userContext, methodName) {
        /// <param name="result" type="Object">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (!this._cancelCurrentPeriodicBackgroundRefresh) {
            var r = result;
            this._processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, '', false);
        }
        else {
            this._debug('Cancelling periodic background refresh...');
        }
        this._cancelCurrentPeriodicBackgroundRefresh = false;
        this._periodicBackgroundRefreshInProgress = false;
    },
    refreshFailureCallback: function SpottedScript_Controls_ChatClient_ServerClass$refreshFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._cancelCurrentPeriodicBackgroundRefresh = false;
        this._periodicBackgroundRefreshInProgress = false;
        if (error.get_exceptionType().endsWith('+WrongSessionException')) {
            this._periodicBackgroundRefreshIsPaused = true;
            if (this.gotWrongSessionException != null) {
                this.gotWrongSessionException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
            }
        }
        else if (error.get_exceptionType().endsWith('+TimeoutException')) {
            this._periodicBackgroundRefreshIsPaused = true;
            if (this.gotTimeoutException != null) {
                this.gotTimeoutException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
            }
        }
        else {
            if (this.gotGenericException != null) {
                this.gotGenericException(this, new SpottedScript.Controls.ChatClient.GotExceptionEventArgs(error, methodName));
            }
        }
    },
    _processItems: function SpottedScript_Controls_ChatClient_ServerClass$_processItems(itemsJson, lastActionTicks, lastItemGuidReturned, methodName, guestRefreshStubs, pinResultRoomGuid, excludeItemsFromPinnedRoom) {
        /// <param name="itemsJson" type="String">
        /// </param>
        /// <param name="lastActionTicks" type="String">
        /// </param>
        /// <param name="lastItemGuidReturned" type="String">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        /// <param name="guestRefreshStubs" type="Array" elementType="GuestRefreshStub">
        /// </param>
        /// <param name="pinResultRoomGuid" type="String">
        /// </param>
        /// <param name="excludeItemsFromPinnedRoom" type="Boolean">
        /// </param>
        var itemStubArray = eval(' [ ' + itemsJson + ' ] ');
        var itemsArray = [];
        for (var i = 0; i < itemStubArray.length; i++) {
            if (!excludeItemsFromPinnedRoom || itemStubArray[i].roomGuid !== pinResultRoomGuid) {
                var item = SpottedScript.Controls.ChatClient.Items.Item.create(itemStubArray[i], this._controller, this._requestIndex, false, 1);
                itemsArray[itemsArray.length] = item;
            }
        }
        var items = itemsArray;
        if (items.length > 0 && lastItemGuidReturned.length > 0) {
            this._lastItemGuid = lastItemGuidReturned;
        }
        if (lastActionTicks.length > 0) {
            this._lastActionTicks = lastActionTicks;
        }
        if (guestRefreshStubs != null) {
            for (var i = 0; i < guestRefreshStubs.length; i++) {
                var g = guestRefreshStubs[i];
                if (g.itemsJson.length > 0) {
                    var guestItemStubArray = eval(' [ ' + g.itemsJson + ' ] ');
                    for (var j = 0; j < guestItemStubArray.length; j++) {
                        var item = SpottedScript.Controls.ChatClient.Items.Item.create(guestItemStubArray[j], this._controller, this._requestIndex, true, 1);
                        items[items.length] = item;
                    }
                    if (g.lastItemGuidReturned.length > 0) {
                        for (var k = 0; k < this._roomState.length; k++) {
                            var ss = this._roomState[k];
                            if (ss.guid === g.roomGuid) {
                                ss.latestItem = g.lastItemGuidReturned;
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (items.length > 0) {
            if (this.gotItems != null) {
                this.gotItems(this, new SpottedScript.Controls.ChatClient.GotItemsEventArgs(items, this._requestIndex));
            }
        }
        else {
            if (this.gotNoItems != null) {
                this.gotNoItems(this, new SpottedScript.Controls.ChatClient.GotNoItemsEventArgs(this._requestIndex));
            }
        }
        this._requestIndex++;
    },
    _debug: function SpottedScript_Controls_ChatClient_ServerClass$_debug(html) {
        /// <param name="html" type="String">
        /// </param>
        if (this.debugPrint != null) {
            this.debugPrint(this, new SpottedScript.Controls.ChatClient.DebugPrintEventArgs(html));
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.StoreUpdatedRoomListOrderRequest
SpottedScript.Controls.ChatClient.StoreUpdatedRoomListOrderRequest = function SpottedScript_Controls_ChatClient_StoreUpdatedRoomListOrderRequest(parent) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.StoreUpdatedRoomListOrderRequest.initializeBase(this);
    this._parent$1 = parent;
}
SpottedScript.Controls.ChatClient.StoreUpdatedRoomListOrderRequest.prototype = {
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_StoreUpdatedRoomListOrderRequest$sendNow() {
        this._parent$1.invokeStoreUpdatedRoomListOrder();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.SendMessageRequest
SpottedScript.Controls.ChatClient.SendMessageRequest = function SpottedScript_Controls_ChatClient_SendMessageRequest(parent, message, roomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="message" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="_message$1" type="String">
    /// </field>
    /// <field name="_roomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.SendMessageRequest.initializeBase(this);
    this._parent$1 = parent;
    this._message$1 = message;
    this._roomGuid$1 = roomGuid;
}
SpottedScript.Controls.ChatClient.SendMessageRequest.prototype = {
    _message$1: null,
    _roomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_SendMessageRequest$sendNow() {
        this._parent$1.invokeSendMessage(this._message$1, this._roomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.MoreInfoRequest
SpottedScript.Controls.ChatClient.MoreInfoRequest = function SpottedScript_Controls_ChatClient_MoreInfoRequest(parent, roomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="_roomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.MoreInfoRequest.initializeBase(this);
    this._parent$1 = parent;
    this._roomGuid$1 = roomGuid;
}
SpottedScript.Controls.ChatClient.MoreInfoRequest.prototype = {
    _roomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_MoreInfoRequest$sendNow() {
        this._parent$1.invokeGetMoreInfo(this._roomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.ArchiveItemsRequest
SpottedScript.Controls.ChatClient.ArchiveItemsRequest = function SpottedScript_Controls_ChatClient_ArchiveItemsRequest(parent, roomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="_roomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.ArchiveItemsRequest.initializeBase(this);
    this._parent$1 = parent;
    this._roomGuid$1 = roomGuid;
}
SpottedScript.Controls.ChatClient.ArchiveItemsRequest.prototype = {
    _roomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_ArchiveItemsRequest$sendNow() {
        this._parent$1.invokeGetArchiveItems(this._roomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.DeleteArchiveRequest
SpottedScript.Controls.ChatClient.DeleteArchiveRequest = function SpottedScript_Controls_ChatClient_DeleteArchiveRequest(parent, roomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="_roomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.DeleteArchiveRequest.initializeBase(this);
    this._parent$1 = parent;
    this._roomGuid$1 = roomGuid;
}
SpottedScript.Controls.ChatClient.DeleteArchiveRequest.prototype = {
    _roomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_DeleteArchiveRequest$sendNow() {
        this._parent$1.invokeDeleteArchive(this._roomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.UnPinRoomRequest
SpottedScript.Controls.ChatClient.UnPinRoomRequest = function SpottedScript_Controls_ChatClient_UnPinRoomRequest(parent, roomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="_roomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.UnPinRoomRequest.initializeBase(this);
    this._parent$1 = parent;
    this._roomGuid$1 = roomGuid;
}
SpottedScript.Controls.ChatClient.UnPinRoomRequest.prototype = {
    _roomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_UnPinRoomRequest$sendNow() {
        this._parent$1.invokeUnPinRoom(this._roomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.PinRoomRequest
SpottedScript.Controls.ChatClient.PinRoomRequest = function SpottedScript_Controls_ChatClient_PinRoomRequest(parent, newRoomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="newRoomGuid" type="String">
    /// </param>
    /// <field name="_newRoomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.PinRoomRequest.initializeBase(this);
    this._parent$1 = parent;
    this._newRoomGuid$1 = newRoomGuid;
}
SpottedScript.Controls.ChatClient.PinRoomRequest.prototype = {
    _newRoomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_PinRoomRequest$sendNow() {
        this._parent$1.invokePinRoom(this._newRoomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.StarRoomRequest
SpottedScript.Controls.ChatClient.StarRoomRequest = function SpottedScript_Controls_ChatClient_StarRoomRequest(parent, roomGuid, starred) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="starred" type="Boolean">
    /// </param>
    /// <field name="_roomGuid$1" type="String">
    /// </field>
    /// <field name="_starred$1" type="Boolean">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.StarRoomRequest.initializeBase(this);
    this._parent$1 = parent;
    this._roomGuid$1 = roomGuid;
    this._starred$1 = starred;
}
SpottedScript.Controls.ChatClient.StarRoomRequest.prototype = {
    _roomGuid$1: null,
    _starred$1: false,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_StarRoomRequest$sendNow() {
        this._parent$1.invokeStarRoom(this._roomGuid$1, this._starred$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.SwitchPhotoRoomRequest
SpottedScript.Controls.ChatClient.SwitchPhotoRoomRequest = function SpottedScript_Controls_ChatClient_SwitchPhotoRoomRequest(parent, newRoomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="newRoomGuid" type="String">
    /// </param>
    /// <field name="_newRoomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.SwitchPhotoRoomRequest.initializeBase(this);
    this._parent$1 = parent;
    this._newRoomGuid$1 = newRoomGuid;
}
SpottedScript.Controls.ChatClient.SwitchPhotoRoomRequest.prototype = {
    _newRoomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_SwitchPhotoRoomRequest$sendNow() {
        this._parent$1.invokeSwitchPhotoRoom(this._newRoomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.RePinRoomRequest
SpottedScript.Controls.ChatClient.RePinRoomRequest = function SpottedScript_Controls_ChatClient_RePinRoomRequest(parent, roomGuid) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="_roomGuid$1" type="String">
    /// </field>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.RePinRoomRequest.initializeBase(this);
    this._parent$1 = parent;
    this._roomGuid$1 = roomGuid;
}
SpottedScript.Controls.ChatClient.RePinRoomRequest.prototype = {
    _roomGuid$1: null,
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_RePinRoomRequest$sendNow() {
        this._parent$1.invokeRePinRoom(this._roomGuid$1);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.ForceResetSessionAndGetState
SpottedScript.Controls.ChatClient.ForceResetSessionAndGetState = function SpottedScript_Controls_ChatClient_ForceResetSessionAndGetState(parent) {
    /// <param name="parent" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </param>
    /// <field name="_parent$1" type="SpottedScript.Controls.ChatClient.ServerClass">
    /// </field>
    SpottedScript.Controls.ChatClient.ForceResetSessionAndGetState.initializeBase(this);
    this._parent$1 = parent;
}
SpottedScript.Controls.ChatClient.ForceResetSessionAndGetState.prototype = {
    _parent$1: null,
    sendNow: function SpottedScript_Controls_ChatClient_ForceResetSessionAndGetState$sendNow() {
        this._parent$1.invokeForceResetSessionAndGetState();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.CriticalRequest
SpottedScript.Controls.ChatClient.CriticalRequest = function SpottedScript_Controls_ChatClient_CriticalRequest() {
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.GotNoItemsEventArgs
SpottedScript.Controls.ChatClient.GotNoItemsEventArgs = function SpottedScript_Controls_ChatClient_GotNoItemsEventArgs(serverRequestIndex) {
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="serverRequestIndex" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.ChatClient.GotNoItemsEventArgs.initializeBase(this);
    this.serverRequestIndex = serverRequestIndex;
}
SpottedScript.Controls.ChatClient.GotNoItemsEventArgs.prototype = {
    serverRequestIndex: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.GotMoreInfoEventArgs
SpottedScript.Controls.ChatClient.GotMoreInfoEventArgs = function SpottedScript_Controls_ChatClient_GotMoreInfoEventArgs(roomGuid, moreInfoHtml) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="moreInfoHtml" type="String">
    /// </param>
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="moreInfoHtml" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.GotMoreInfoEventArgs.initializeBase(this);
    this.roomGuid = roomGuid;
    this.moreInfoHtml = moreInfoHtml;
}
SpottedScript.Controls.ChatClient.GotMoreInfoEventArgs.prototype = {
    roomGuid: null,
    moreInfoHtml: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.GotArchiveItemsEventArgs
SpottedScript.Controls.ChatClient.GotArchiveItemsEventArgs = function SpottedScript_Controls_ChatClient_GotArchiveItemsEventArgs(roomGuid, archiveItems) {
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="archiveItems" type="String">
    /// </param>
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="archiveItems" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.GotArchiveItemsEventArgs.initializeBase(this);
    this.roomGuid = roomGuid;
    this.archiveItems = archiveItems;
}
SpottedScript.Controls.ChatClient.GotArchiveItemsEventArgs.prototype = {
    roomGuid: null,
    archiveItems: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.GotItemsEventArgs
SpottedScript.Controls.ChatClient.GotItemsEventArgs = function SpottedScript_Controls_ChatClient_GotItemsEventArgs(items, serverRequestIndex) {
    /// <param name="items" type="Array" elementType="Item">
    /// </param>
    /// <param name="serverRequestIndex" type="Number" integer="true">
    /// </param>
    /// <field name="items" type="Array" elementType="Item">
    /// </field>
    /// <field name="serverRequestIndex" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.ChatClient.GotItemsEventArgs.initializeBase(this);
    this.items = items;
    this.serverRequestIndex = serverRequestIndex;
}
SpottedScript.Controls.ChatClient.GotItemsEventArgs.prototype = {
    items: null,
    serverRequestIndex: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.GotRoomEventArgs
SpottedScript.Controls.ChatClient.GotRoomEventArgs = function SpottedScript_Controls_ChatClient_GotRoomEventArgs(roomStub) {
    /// <param name="roomStub" type="SpottedScript.Controls.ChatClient.Shared.RoomStub">
    /// </param>
    /// <field name="roomStub" type="SpottedScript.Controls.ChatClient.Shared.RoomStub">
    /// </field>
    SpottedScript.Controls.ChatClient.GotRoomEventArgs.initializeBase(this);
    this.roomStub = roomStub;
}
SpottedScript.Controls.ChatClient.GotRoomEventArgs.prototype = {
    roomStub: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.GotRoomStateEventArgs
SpottedScript.Controls.ChatClient.GotRoomStateEventArgs = function SpottedScript_Controls_ChatClient_GotRoomStateEventArgs(roomState) {
    /// <param name="roomState" type="Array" elementType="StateStub">
    /// </param>
    /// <field name="roomState" type="Array" elementType="StateStub">
    /// </field>
    SpottedScript.Controls.ChatClient.GotRoomStateEventArgs.initializeBase(this);
    this.roomState = roomState;
}
SpottedScript.Controls.ChatClient.GotRoomStateEventArgs.prototype = {
    roomState: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.GotExceptionEventArgs
SpottedScript.Controls.ChatClient.GotExceptionEventArgs = function SpottedScript_Controls_ChatClient_GotExceptionEventArgs(error, method) {
    /// <param name="error" type="Sys.Net.WebServiceError">
    /// </param>
    /// <param name="method" type="String">
    /// </param>
    /// <field name="error" type="Sys.Net.WebServiceError">
    /// </field>
    /// <field name="method" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.GotExceptionEventArgs.initializeBase(this);
    this.error = error;
    this.method = method;
}
SpottedScript.Controls.ChatClient.GotExceptionEventArgs.prototype = {
    error: null,
    method: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.DebugPrintEventArgs
SpottedScript.Controls.ChatClient.DebugPrintEventArgs = function SpottedScript_Controls_ChatClient_DebugPrintEventArgs(html) {
    /// <param name="html" type="String">
    /// </param>
    /// <field name="html" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.DebugPrintEventArgs.initializeBase(this);
    this.html = html;
}
SpottedScript.Controls.ChatClient.DebugPrintEventArgs.prototype = {
    html: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.View
SpottedScript.Controls.ChatClient.View = function SpottedScript_Controls_ChatClient_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.ChatClient.View.prototype = {
    clientId: null,
    get_outerMain: function SpottedScript_Controls_ChatClient_View$get_outerMain() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OuterMain');
    },
    get_tabsChatLink: function SpottedScript_Controls_ChatClient_View$get_tabsChatLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TabsChatLink');
    },
    get_tabChatHolder: function SpottedScript_Controls_ChatClient_View$get_tabChatHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TabChatHolder');
    },
    get_roomsMain: function SpottedScript_Controls_ChatClient_View$get_roomsMain() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RoomsMain');
    },
    get_roomList: function SpottedScript_Controls_ChatClient_View$get_roomList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RoomList');
    },
    get_privateChatDrop: function SpottedScript_Controls_ChatClient_View$get_privateChatDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrivateChatDrop');
    },
    get_roomPrivateListDivider: function SpottedScript_Controls_ChatClient_View$get_roomPrivateListDivider() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RoomPrivateListDivider');
    },
    get_roomPrivateList: function SpottedScript_Controls_ChatClient_View$get_roomPrivateList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RoomPrivateList');
    },
    get_roomGuestListDivider: function SpottedScript_Controls_ChatClient_View$get_roomGuestListDivider() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RoomGuestListDivider');
    },
    get_roomGuestList: function SpottedScript_Controls_ChatClient_View$get_roomGuestList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RoomGuestList');
    },
    get_privateChatDropMain: function SpottedScript_Controls_ChatClient_View$get_privateChatDropMain() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrivateChatDropMain');
    },
    get_messagesMain: function SpottedScript_Controls_ChatClient_View$get_messagesMain() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MessagesMain');
    },
    get_textBoxHolder: function SpottedScript_Controls_ChatClient_View$get_textBoxHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TextBoxHolder');
    },
    get_textBox: function SpottedScript_Controls_ChatClient_View$get_textBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TextBox');
    },
    get_messageList: function SpottedScript_Controls_ChatClient_View$get_messageList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MessageList');
    },
    get_wrongSessionHolder: function SpottedScript_Controls_ChatClient_View$get_wrongSessionHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WrongSessionHolder');
    },
    get_wrongSessionResumeLink: function SpottedScript_Controls_ChatClient_View$get_wrongSessionResumeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WrongSessionResumeLink');
    },
    get_timeoutHolder: function SpottedScript_Controls_ChatClient_View$get_timeoutHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TimeoutHolder');
    },
    get_timeoutResumeLink: function SpottedScript_Controls_ChatClient_View$get_timeoutResumeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TimeoutResumeLink');
    },
    get_deleteArchiveHolder: function SpottedScript_Controls_ChatClient_View$get_deleteArchiveHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteArchiveHolder');
    },
    get_deleteArchiveAnchor: function SpottedScript_Controls_ChatClient_View$get_deleteArchiveAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteArchiveAnchor');
    },
    get_deleteArchiveDoneLabel: function SpottedScript_Controls_ChatClient_View$get_deleteArchiveDoneLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteArchiveDoneLabel');
    },
    get_streamList: function SpottedScript_Controls_ChatClient_View$get_streamList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StreamList');
    },
    get_popupsCheckBox: function SpottedScript_Controls_ChatClient_View$get_popupsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PopupsCheckBox');
    },
    get_stickyCheckBox: function SpottedScript_Controls_ChatClient_View$get_stickyCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StickyCheckBox');
    },
    get_downlevelMain: function SpottedScript_Controls_ChatClient_View$get_downlevelMain() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DownlevelMain');
    },
    get_initGo: function SpottedScript_Controls_ChatClient_View$get_initGo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitGo');
    },
    get_initUsrK: function SpottedScript_Controls_ChatClient_View$get_initUsrK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitUsrK');
    },
    get_initClientID: function SpottedScript_Controls_ChatClient_View$get_initClientID() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitClientID');
    },
    get_initLastActionTicks: function SpottedScript_Controls_ChatClient_View$get_initLastActionTicks() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitLastActionTicks');
    },
    get_initSystemMessagesRoomGuid: function SpottedScript_Controls_ChatClient_View$get_initSystemMessagesRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitSystemMessagesRoomGuid');
    },
    get_initInboxUpdatesRoomGuid: function SpottedScript_Controls_ChatClient_View$get_initInboxUpdatesRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitInboxUpdatesRoomGuid');
    },
    get_initBuddyAlertsRoomGuid: function SpottedScript_Controls_ChatClient_View$get_initBuddyAlertsRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitBuddyAlertsRoomGuid');
    },
    get_initBuddyStreamRoomGuid: function SpottedScript_Controls_ChatClient_View$get_initBuddyStreamRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitBuddyStreamRoomGuid');
    },
    get_initAnimatePopups: function SpottedScript_Controls_ChatClient_View$get_initAnimatePopups() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitAnimatePopups');
    },
    get_initTopPhoto: function SpottedScript_Controls_ChatClient_View$get_initTopPhoto() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitTopPhoto');
    }
}
Type.registerNamespace('SpottedScript.Controls.ChatClient.Shared');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.RoomType
SpottedScript.Controls.ChatClient.Shared.RoomType = function() { 
    /// <field name="normal" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="inboxUpdates" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="newPhotosAll" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="newPhotosProSpotters" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="newPhotosSpotters" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="newPhotosBuddies" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="systemMessages" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="privateChat" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="privateChatRequestsBuddies" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="privateChatRequestsStrangers" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="randomChat" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="buddyAlerts" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="orphans" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="laughs" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="newVideosAll" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="publicStream" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="buddyStream" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="privateChatRequests" type="Number" integer="true" static="true">
    /// </field>
};
SpottedScript.Controls.ChatClient.Shared.RoomType.prototype = {
    normal: 1, 
    inboxUpdates: 2, 
    newPhotosAll: 3, 
    newPhotosProSpotters: 4, 
    newPhotosSpotters: 5, 
    newPhotosBuddies: 6, 
    systemMessages: 7, 
    privateChat: 8, 
    privateChatRequestsBuddies: 9, 
    privateChatRequestsStrangers: 10, 
    randomChat: 11, 
    buddyAlerts: 12, 
    orphans: 13, 
    laughs: 14, 
    newVideosAll: 15, 
    publicStream: 16, 
    buddyStream: 17, 
    privateChatRequests: 18
}
SpottedScript.Controls.ChatClient.Shared.RoomType.registerEnum('SpottedScript.Controls.ChatClient.Shared.RoomType', false);
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.ItemType
SpottedScript.Controls.ChatClient.Shared.ItemType = function() { 
    /// <field name="publicChatMessage" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="privateChatMessage" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="commentAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="privateMessageAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="laughAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="loginAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="photoAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="logoutAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="invite" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="groupNewsAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="error" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="debug" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="commentChatMessage" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="buddyLaughAlert" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="topPhoto" type="Number" integer="true" static="true">
    /// </field>
};
SpottedScript.Controls.ChatClient.Shared.ItemType.prototype = {
    publicChatMessage: 1, 
    privateChatMessage: 2, 
    commentAlert: 3, 
    privateMessageAlert: 4, 
    laughAlert: 5, 
    loginAlert: 6, 
    photoAlert: 7, 
    logoutAlert: 8, 
    invite: 9, 
    groupNewsAlert: 10, 
    error: 11, 
    debug: 12, 
    commentChatMessage: 13, 
    buddyLaughAlert: 14, 
    topPhoto: 15
}
SpottedScript.Controls.ChatClient.Shared.ItemType.registerEnum('SpottedScript.Controls.ChatClient.Shared.ItemType', false);
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.PresenceState
SpottedScript.Controls.ChatClient.Shared.PresenceState = function() { 
    /// <field name="none" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="offline" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="online" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="chatting" type="Number" integer="true" static="true">
    /// </field>
};
SpottedScript.Controls.ChatClient.Shared.PresenceState.prototype = {
    none: 0, 
    offline: 1, 
    online: 2, 
    chatting: 3
}
SpottedScript.Controls.ChatClient.Shared.PresenceState.registerEnum('SpottedScript.Controls.ChatClient.Shared.PresenceState', false);
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.ArchiveStub
SpottedScript.Controls.ChatClient.Shared.ArchiveStub = function SpottedScript_Controls_ChatClient_Shared_ArchiveStub() {
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="archiveItems" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.ArchiveStub.initializeBase(this);
}
SpottedScript.Controls.ChatClient.Shared.ArchiveStub.prototype = {
    roomGuid: null,
    archiveItems: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.DeleteArchiveStub
SpottedScript.Controls.ChatClient.Shared.DeleteArchiveStub = function SpottedScript_Controls_ChatClient_Shared_DeleteArchiveStub() {
    /// <field name="roomGuid" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.DeleteArchiveStub.initializeBase(this);
}
SpottedScript.Controls.ChatClient.Shared.DeleteArchiveStub.prototype = {
    roomGuid: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.TopPhotoStub
SpottedScript.Controls.ChatClient.Shared.TopPhotoStub = function SpottedScript_Controls_ChatClient_Shared_TopPhotoStub(guid, type, dateTime, roomGuid, photoK, photoUrl, photoIcon, photoWeb, photoWebWidth, photoWebHeight, photoThumb, photoThumbWidth, photoThumbHeight) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="photoK" type="Number" integer="true">
    /// </param>
    /// <param name="photoUrl" type="String">
    /// </param>
    /// <param name="photoIcon" type="String">
    /// </param>
    /// <param name="photoWeb" type="String">
    /// </param>
    /// <param name="photoWebWidth" type="Number" integer="true">
    /// </param>
    /// <param name="photoWebHeight" type="Number" integer="true">
    /// </param>
    /// <param name="photoThumb" type="String">
    /// </param>
    /// <param name="photoThumbWidth" type="Number" integer="true">
    /// </param>
    /// <param name="photoThumbHeight" type="Number" integer="true">
    /// </param>
    /// <field name="photoK" type="Number" integer="true">
    /// </field>
    /// <field name="photoUrl" type="String">
    /// </field>
    /// <field name="photoIcon" type="String">
    /// </field>
    /// <field name="photoWeb" type="String">
    /// </field>
    /// <field name="photoWebWidth" type="Number" integer="true">
    /// </field>
    /// <field name="photoWebHeight" type="Number" integer="true">
    /// </field>
    /// <field name="photoThumb" type="String">
    /// </field>
    /// <field name="photoThumbWidth" type="Number" integer="true">
    /// </field>
    /// <field name="photoThumbHeight" type="Number" integer="true">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.TopPhotoStub.initializeBase(this, [ guid, type, dateTime, roomGuid ]);
    this.photoK = photoK;
    this.photoUrl = photoUrl;
    this.photoIcon = photoIcon;
    this.photoWeb = photoWeb;
    this.photoWebWidth = photoWebWidth;
    this.photoWebHeight = photoWebHeight;
    this.photoThumb = photoThumb;
    this.photoThumbWidth = photoThumbWidth;
    this.photoThumbHeight = photoThumbHeight;
}
SpottedScript.Controls.ChatClient.Shared.TopPhotoStub.prototype = {
    photoK: 0,
    photoUrl: null,
    photoIcon: null,
    photoWeb: null,
    photoWebWidth: 0,
    photoWebHeight: 0,
    photoThumb: null,
    photoThumbWidth: 0,
    photoThumbHeight: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.UnPinStub
SpottedScript.Controls.ChatClient.Shared.UnPinStub = function SpottedScript_Controls_ChatClient_Shared_UnPinStub() {
    /// <field name="roomGuid" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.UnPinStub.initializeBase(this);
}
SpottedScript.Controls.ChatClient.Shared.UnPinStub.prototype = {
    roomGuid: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.CommentMessageStub
SpottedScript.Controls.ChatClient.Shared.CommentMessageStub = function SpottedScript_Controls_ChatClient_Shared_CommentMessageStub(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid, url, subject) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="nickName" type="String">
    /// </param>
    /// <param name="stmuParams" type="String">
    /// </param>
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pic" type="String">
    /// </param>
    /// <param name="chatPic" type="String">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="pinRoomGuid" type="String">
    /// </param>
    /// <param name="url" type="String">
    /// </param>
    /// <param name="subject" type="String">
    /// </param>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="subject" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.CommentMessageStub.initializeBase(this, [ guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid ]);
    this.url = url;
    this.subject = subject;
}
SpottedScript.Controls.ChatClient.Shared.CommentMessageStub.prototype = {
    url: null,
    subject: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.MoreInfoStub
SpottedScript.Controls.ChatClient.Shared.MoreInfoStub = function SpottedScript_Controls_ChatClient_Shared_MoreInfoStub() {
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="moreInfoHtml" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.MoreInfoStub.initializeBase(this);
}
SpottedScript.Controls.ChatClient.Shared.MoreInfoStub.prototype = {
    roomGuid: null,
    moreInfoHtml: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.AlertStub
SpottedScript.Controls.ChatClient.Shared.AlertStub = function SpottedScript_Controls_ChatClient_Shared_AlertStub(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="nickName" type="String">
    /// </param>
    /// <param name="stmuParams" type="String">
    /// </param>
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pic" type="String">
    /// </param>
    /// <field name="nickName" type="String">
    /// </field>
    /// <field name="stmuParams" type="String">
    /// </field>
    /// <field name="usrK" type="Number" integer="true">
    /// </field>
    /// <field name="pic" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.AlertStub.initializeBase(this, [ guid, type, dateTime, roomGuid ]);
    this.nickName = nickName;
    this.stmuParams = stmuParams;
    this.usrK = usrK;
    this.pic = pic;
}
SpottedScript.Controls.ChatClient.Shared.AlertStub.prototype = {
    nickName: null,
    stmuParams: null,
    usrK: 0,
    pic: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.LaughStub
SpottedScript.Controls.ChatClient.Shared.LaughStub = function SpottedScript_Controls_ChatClient_Shared_LaughStub(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid, url, subject) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="nickName" type="String">
    /// </param>
    /// <param name="stmuParams" type="String">
    /// </param>
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pic" type="String">
    /// </param>
    /// <param name="chatPic" type="String">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="pinRoomGuid" type="String">
    /// </param>
    /// <param name="url" type="String">
    /// </param>
    /// <param name="subject" type="String">
    /// </param>
    SpottedScript.Controls.ChatClient.Shared.LaughStub.initializeBase(this, [ guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid, url, subject ]);
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.GuestRefreshStub
SpottedScript.Controls.ChatClient.Shared.GuestRefreshStub = function SpottedScript_Controls_ChatClient_Shared_GuestRefreshStub() {
    /// <field name="roomGuid" type="String">
    /// </field>
    /// <field name="lastItemGuidReturned" type="String">
    /// </field>
    /// <field name="itemsJson" type="String">
    /// </field>
}
SpottedScript.Controls.ChatClient.Shared.GuestRefreshStub.prototype = {
    roomGuid: null,
    lastItemGuidReturned: null,
    itemsJson: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.GetStateStub
SpottedScript.Controls.ChatClient.Shared.GetStateStub = function SpottedScript_Controls_ChatClient_Shared_GetStateStub() {
    /// <field name="roomState" type="Array" elementType="StateStub">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.GetStateStub.initializeBase(this);
}
SpottedScript.Controls.ChatClient.Shared.GetStateStub.prototype = {
    roomState: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.PrivateStub
SpottedScript.Controls.ChatClient.Shared.PrivateStub = function SpottedScript_Controls_ChatClient_Shared_PrivateStub(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid, buddy) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="nickName" type="String">
    /// </param>
    /// <param name="stmuParams" type="String">
    /// </param>
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pic" type="String">
    /// </param>
    /// <param name="chatPic" type="String">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="pinRoomGuid" type="String">
    /// </param>
    /// <param name="buddy" type="Boolean">
    /// </param>
    /// <field name="buddy" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.PrivateStub.initializeBase(this, [ guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid ]);
    this.buddy = buddy;
}
SpottedScript.Controls.ChatClient.Shared.PrivateStub.prototype = {
    buddy: false
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.StateStub
SpottedScript.Controls.ChatClient.Shared.StateStub = function SpottedScript_Controls_ChatClient_Shared_StateStub() {
    /// <field name="guid" type="String">
    /// </field>
    /// <field name="selected" type="Boolean">
    /// </field>
    /// <field name="guest" type="Boolean">
    /// </field>
    /// <field name="newMessages" type="Number" integer="true">
    /// </field>
    /// <field name="totalMessages" type="Number" integer="true">
    /// </field>
    /// <field name="latestItem" type="String">
    /// </field>
    /// <field name="latestItemOld" type="String">
    /// </field>
    /// <field name="latestItemSeen" type="String">
    /// </field>
    /// <field name="listOrder" type="Number" integer="true">
    /// </field>
    /// <field name="tokenDateTimeTicks" type="String">
    /// </field>
    /// <field name="token" type="String">
    /// </field>
}
SpottedScript.Controls.ChatClient.Shared.StateStub.prototype = {
    guid: null,
    selected: false,
    guest: false,
    newMessages: 0,
    totalMessages: 0,
    latestItem: null,
    latestItemOld: null,
    latestItemSeen: null,
    listOrder: 0,
    tokenDateTimeTicks: null,
    token: null,
    initialise: function SpottedScript_Controls_ChatClient_Shared_StateStub$initialise(guid, selected, guest, newMessages, totalMessages, latestItem, latestItemSeen, latestItemOld, listOrder, tokenDateTimeTicks, token) {
        /// <param name="guid" type="String">
        /// </param>
        /// <param name="selected" type="Boolean">
        /// </param>
        /// <param name="guest" type="Boolean">
        /// </param>
        /// <param name="newMessages" type="Number" integer="true">
        /// </param>
        /// <param name="totalMessages" type="Number" integer="true">
        /// </param>
        /// <param name="latestItem" type="String">
        /// </param>
        /// <param name="latestItemSeen" type="String">
        /// </param>
        /// <param name="latestItemOld" type="String">
        /// </param>
        /// <param name="listOrder" type="Number" integer="true">
        /// </param>
        /// <param name="tokenDateTimeTicks" type="String">
        /// </param>
        /// <param name="token" type="String">
        /// </param>
        this.guid = guid;
        this.selected = selected;
        this.guest = guest;
        this.newMessages = newMessages;
        this.totalMessages = totalMessages;
        this.latestItem = latestItem;
        this.latestItemOld = latestItemOld;
        this.latestItemSeen = latestItemSeen;
        this.listOrder = listOrder;
        this.tokenDateTimeTicks = tokenDateTimeTicks;
        this.token = token;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.RoomHtml
SpottedScript.Controls.ChatClient.Shared.RoomHtml = function SpottedScript_Controls_ChatClient_Shared_RoomHtml(roomStub, loggedIn) {
    /// <param name="roomStub" type="SpottedScript.Controls.ChatClient.Shared.RoomStub">
    /// </param>
    /// <param name="loggedIn" type="Boolean">
    /// </param>
    /// <field name="roomStub" type="SpottedScript.Controls.ChatClient.Shared.RoomStub">
    /// </field>
    /// <field name="clientID" type="String">
    /// </field>
    /// <field name="linkID" type="String">
    /// </field>
    /// <field name="presenceID" type="String">
    /// </field>
    /// <field name="crossID" type="String">
    /// </field>
    /// <field name="totalID" type="String">
    /// </field>
    /// <field name="statsSeperatorID" type="String">
    /// </field>
    /// <field name="unreadID" type="String">
    /// </field>
    /// <field name="_loggedIn" type="Boolean">
    /// </field>
    this.roomStub = roomStub;
    this.clientID = this.roomStub.parentClientID + '_Room_' + this.roomStub.guid;
    this.linkID = this.clientID + '_Link';
    this.presenceID = this.clientID + '_Presence';
    this.crossID = this.clientID + '_Cross';
    this.totalID = this.clientID + '_Total';
    this.statsSeperatorID = this.clientID + '_StatsSeperator';
    this.unreadID = this.clientID + '_Unread';
    this._loggedIn = loggedIn;
}
SpottedScript.Controls.ChatClient.Shared.RoomHtml.get__isFireFox2 = function SpottedScript_Controls_ChatClient_Shared_RoomHtml$get__isFireFox2() {
    /// <value type="Boolean"></value>
    return SpottedScript.Misc.get_browserIsFirefox() && SpottedScript.Misc.get_browserVersion() >= 2 && SpottedScript.Misc.get_browserVersion() < 3;
}
SpottedScript.Controls.ChatClient.Shared.RoomHtml.get__isIE = function SpottedScript_Controls_ChatClient_Shared_RoomHtml$get__isIE() {
    /// <value type="Boolean"></value>
    return SpottedScript.Misc.get_browserIsIE();
}
SpottedScript.Controls.ChatClient.Shared.RoomHtml.prototype = {
    roomStub: null,
    clientID: null,
    linkID: null,
    presenceID: null,
    crossID: null,
    totalID: null,
    statsSeperatorID: null,
    unreadID: null,
    _loggedIn: false,
    toHtml: function SpottedScript_Controls_ChatClient_Shared_RoomHtml$toHtml() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        this.appendHtml(sb);
        return sb.toString();
    },
    appendHtml: function SpottedScript_Controls_ChatClient_Shared_RoomHtml$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<div');
        sb.appendAttribute('id', this.clientID);
        sb.appendAttribute('class', (this.roomStub.selected) ? 'ChatClientRoomHolder ChatClientRoomSelected' : 'ChatClientRoomHolder');
        sb.appendAttribute('roomGuid', this.roomStub.guid);
        sb.appendAttribute('roomName', this.roomStub.name);
        sb.appendAttribute('roomUrl', this.roomStub.url);
        sb.appendAttribute('roomPinned', this.roomStub.pinned.toString().toLowerCase());
        sb.appendAttribute('roomIsStarredByDefault', this.roomStub.isStarredByDefault.toString().toLowerCase());
        sb.appendAttribute('roomStarred', this.roomStub.starred.toString().toLowerCase());
        sb.appendAttribute('roomStarrable', this.roomStub.starrable.toString().toLowerCase());
        sb.appendAttribute('roomPinable', this.roomStub.pinable.toString().toLowerCase());
        sb.appendAttribute('roomSelected', this.roomStub.selected.toString().toLowerCase());
        sb.appendAttribute('roomGuest', this.roomStub.guest.toString().toLowerCase());
        sb.appendAttribute('roomNewMessages', this.roomStub.newMessages.toString());
        sb.appendAttribute('roomTotalMessages', this.roomStub.totalMessages.toString());
        sb.appendAttribute('roomLatestItem', this.roomStub.latestItem);
        sb.appendAttribute('roomLatestItemSeen', this.roomStub.latestItemSeen);
        sb.appendAttribute('roomLatestItemOld', this.roomStub.latestItemOld);
        sb.appendAttribute('roomReadOnly', this.roomStub.readOnly.toString().toLowerCase());
        sb.appendAttribute('roomListOrder', this.roomStub.listOrder.toString());
        sb.appendAttribute('roomIsPhotoChatRoom', this.roomStub.isPhotoChatRoom.toString().toLowerCase());
        sb.appendAttribute('roomIsPrivateChatRoom', this.roomStub.isPrivateChatRoom.toString().toLowerCase());
        sb.appendAttribute('roomIsNewPhotoAlertsRoom', this.roomStub.isNewPhotoAlertsRoom.toString().toLowerCase());
        sb.appendAttribute('roomPresence', (this.roomStub.presence).toString());
        sb.appendAttribute('roomIcon', this.roomStub.icon);
        sb.appendAttribute('roomTokenDateTimeTicks', this.roomStub.tokenDateTimeTicks);
        sb.appendAttribute('roomToken', this.roomStub.token);
        sb.appendAttribute('roomHasArchive', this.roomStub.hasArchive.toString().toLowerCase());
        if (this.roomStub.hiddenFromRoomList) {
            sb.appendAttribute('style', 'display:none;');
        }
        sb.appendAttribute('roomHiddenFromRoomList', this.roomStub.hiddenFromRoomList.toString().toLowerCase());
        sb.appendAttribute('roomIsStreamRoom', this.roomStub.isStreamRoom.toString().toLowerCase());
        sb.append('>');
        sb.append('<div class=\"ChatClientRoomLinkHolder\">');
        sb.append('<span');
        sb.appendAttribute('id', this.linkID);
        sb.appendAttribute('class', (this.roomStub.selected) ? 'ChatClientRoomLink ChatClientRoomLinkSelected' : ((this.roomStub.newMessages === 0) ? 'ChatClientRoomLink ChatClientRoomLinkNoUnread' : 'ChatClientRoomLink'));
        if (this.roomStub.icon.length > 0) {
            sb.appendAttribute('onmouseover', 'stma(\'' + this.roomStub.icon + '\');');
            sb.appendAttribute('onmouseout', 'htm();');
        }
        sb.append('>');
        sb.append(this.roomStub.name);
        sb.append('</span>');
        if (this.roomStub.isPrivateChatRoom && (this.roomStub.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.online || this.roomStub.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.chatting)) {
            sb.append('<img');
            sb.appendAttribute('src', (this.roomStub.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.chatting) ? '/gfx/chat-chatting.png' : '/gfx/chat-online.png');
            sb.appendAttribute('width', (this.roomStub.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.chatting) ? '13' : '9');
            sb.appendAttribute('height', '11');
            sb.appendAttribute('id', this.presenceID);
            sb.appendAttribute('onmouseover', (this.roomStub.presence === SpottedScript.Controls.ChatClient.Shared.PresenceState.online) ? 'sttd(3);' : 'sttd(4);');
            sb.appendAttribute('onmouseout', 'htm();');
            sb.appendAttribute('class', 'ChatClientRoomPresence');
            sb.append(' />');
        }
        sb.append('</div>');
        sb.append('<div class=\"ChatClientRoomPinHolder\">');
        var showCross = this._loggedIn && this.roomStub.pinable;
        sb.append('<img');
        sb.appendAttribute('id', this.crossID);
        sb.appendAttribute('src', (showCross) ? '/gfx/chat-cross.png' : '/gfx/1pix.gif');
        sb.append(' width=\"11\" height=\"11\"');
        sb.appendAttribute('class', (showCross) ? 'ChatClientRoomCross' : 'ChatClientRoomNoCross');
        if (showCross) {
            sb.appendAttribute('onmouseover', 'sttd(1);');
            sb.appendAttribute('onmouseout', 'htm();');
        }
        sb.append(' />');
        sb.append('</div>');
        sb.append('<div class=\"ChatClientRoomStatsHolder\">');
        sb.append('<span class=\"ChatClientRoomUnreadHolder\"');
        sb.appendAttribute('id', this.unreadID);
        sb.append('>');
        sb.append((this.roomStub.newMessages === 0) ? '&nbsp;' : this.roomStub.newMessages.toString());
        sb.append('</span>');
        sb.append('<span class=\"ChatClientRoomSeperatorHolder\"');
        sb.appendAttribute('id', this.statsSeperatorID);
        sb.append('>');
        sb.append((this.roomStub.newMessages > 0) ? '/' : '&nbsp;');
        sb.append('</span>');
        sb.append('<span class=\"ChatClientRoomTotalHolder\"');
        sb.appendAttribute('id', this.totalID);
        sb.append('>');
        sb.append((this.roomStub.totalMessages === 0) ? '&nbsp;' : this.roomStub.totalMessages.toString());
        sb.append('</span>');
        sb.append('</div>');
        sb.append('</div>');
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.SendStub
SpottedScript.Controls.ChatClient.Shared.SendStub = function SpottedScript_Controls_ChatClient_Shared_SendStub() {
    /// <field name="itemGuid" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.SendStub.initializeBase(this);
}
SpottedScript.Controls.ChatClient.Shared.SendStub.prototype = {
    itemGuid: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.PinStub
SpottedScript.Controls.ChatClient.Shared.PinStub = function SpottedScript_Controls_ChatClient_Shared_PinStub() {
    /// <field name="roomStub" type="SpottedScript.Controls.ChatClient.Shared.RoomStub">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.PinStub.initializeBase(this);
}
SpottedScript.Controls.ChatClient.Shared.PinStub.prototype = {
    roomStub: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.ItemStub
SpottedScript.Controls.ChatClient.Shared.ItemStub = function SpottedScript_Controls_ChatClient_Shared_ItemStub(guid, type, dateTime, roomGuid) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <field name="guid" type="String">
    /// </field>
    /// <field name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </field>
    /// <field name="dateTime" type="String">
    /// </field>
    /// <field name="roomGuid" type="String">
    /// </field>
    this.guid = guid;
    this.type = type;
    this.dateTime = dateTime;
    this.roomGuid = roomGuid;
}
SpottedScript.Controls.ChatClient.Shared.ItemStub.prototype = {
    guid: null,
    type: 0,
    dateTime: null,
    roomGuid: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.MessageStub
SpottedScript.Controls.ChatClient.Shared.MessageStub = function SpottedScript_Controls_ChatClient_Shared_MessageStub(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="nickName" type="String">
    /// </param>
    /// <param name="stmuParams" type="String">
    /// </param>
    /// <param name="usrK" type="Number" integer="true">
    /// </param>
    /// <param name="pic" type="String">
    /// </param>
    /// <param name="chatPic" type="String">
    /// </param>
    /// <param name="text" type="String">
    /// </param>
    /// <param name="pinRoomGuid" type="String">
    /// </param>
    /// <field name="nickName" type="String">
    /// </field>
    /// <field name="stmuParams" type="String">
    /// </field>
    /// <field name="usrK" type="Number" integer="true">
    /// </field>
    /// <field name="pic" type="String">
    /// </field>
    /// <field name="chatPic" type="String">
    /// </field>
    /// <field name="text" type="String">
    /// </field>
    /// <field name="pinRoomGuid" type="String">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.MessageStub.initializeBase(this, [ guid, type, dateTime, roomGuid ]);
    this.nickName = nickName;
    this.stmuParams = stmuParams;
    this.usrK = usrK;
    this.pic = pic;
    this.chatPic = chatPic;
    this.text = text;
    this.pinRoomGuid = pinRoomGuid;
}
SpottedScript.Controls.ChatClient.Shared.MessageStub.prototype = {
    nickName: null,
    stmuParams: null,
    usrK: 0,
    pic: null,
    chatPic: null,
    text: null,
    pinRoomGuid: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.PhotoStub
SpottedScript.Controls.ChatClient.Shared.PhotoStub = function SpottedScript_Controls_ChatClient_Shared_PhotoStub(guid, type, dateTime, roomGuid, width, height, url, web, icon, thumb, thumbWidth, thumbHeight, buddyAlert) {
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="type" type="SpottedScript.Controls.ChatClient.Shared.ItemType">
    /// </param>
    /// <param name="dateTime" type="String">
    /// </param>
    /// <param name="roomGuid" type="String">
    /// </param>
    /// <param name="width" type="Number" integer="true">
    /// </param>
    /// <param name="height" type="Number" integer="true">
    /// </param>
    /// <param name="url" type="String">
    /// </param>
    /// <param name="web" type="String">
    /// </param>
    /// <param name="icon" type="String">
    /// </param>
    /// <param name="thumb" type="String">
    /// </param>
    /// <param name="thumbWidth" type="Number" integer="true">
    /// </param>
    /// <param name="thumbHeight" type="Number" integer="true">
    /// </param>
    /// <param name="buddyAlert" type="Boolean">
    /// </param>
    /// <field name="width" type="Number" integer="true">
    /// </field>
    /// <field name="height" type="Number" integer="true">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="web" type="String">
    /// </field>
    /// <field name="icon" type="String">
    /// </field>
    /// <field name="thumb" type="String">
    /// </field>
    /// <field name="thumbWidth" type="Number" integer="true">
    /// </field>
    /// <field name="thumbHeight" type="Number" integer="true">
    /// </field>
    /// <field name="buddyAlert" type="Boolean">
    /// </field>
    SpottedScript.Controls.ChatClient.Shared.PhotoStub.initializeBase(this, [ guid, type, dateTime, roomGuid ]);
    this.width = width;
    this.height = height;
    this.url = url;
    this.web = web;
    this.icon = icon;
    this.thumb = thumb;
    this.thumbWidth = thumbWidth;
    this.thumbHeight = thumbHeight;
    this.buddyAlert = buddyAlert;
}
SpottedScript.Controls.ChatClient.Shared.PhotoStub.prototype = {
    width: 0,
    height: 0,
    url: null,
    web: null,
    icon: null,
    thumb: null,
    thumbWidth: 0,
    thumbHeight: 0,
    buddyAlert: false
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.RefreshStub
SpottedScript.Controls.ChatClient.Shared.RefreshStub = function SpottedScript_Controls_ChatClient_Shared_RefreshStub() {
    /// <field name="lastItemGuidReturned" type="String">
    /// </field>
    /// <field name="lastActionTicks" type="String">
    /// </field>
    /// <field name="itemsJson" type="String">
    /// </field>
    /// <field name="guestRefreshStubs" type="Array" elementType="GuestRefreshStub">
    /// </field>
}
SpottedScript.Controls.ChatClient.Shared.RefreshStub.prototype = {
    lastItemGuidReturned: null,
    lastActionTicks: null,
    itemsJson: null,
    guestRefreshStubs: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ChatClient.Shared.RoomStub
SpottedScript.Controls.ChatClient.Shared.RoomStub = function SpottedScript_Controls_ChatClient_Shared_RoomStub(parentClientID, guid, name, url, pinned, starred, isStarredByDefault, pinable, starrable, selected, guest, newMessages, totalMessages, latestItem, latestItemSeen, latestItemOld, readOnly, listOrder, isPhotoChatRoom, isPrivateChatRoom, isNewPhotoAlertsRoom, presence, icon, tokenDateTimeTicks, token, hasArchive, hiddenFromRoomList, isStreamRoom) {
    /// <param name="parentClientID" type="String">
    /// </param>
    /// <param name="guid" type="String">
    /// </param>
    /// <param name="name" type="String">
    /// </param>
    /// <param name="url" type="String">
    /// </param>
    /// <param name="pinned" type="Boolean">
    /// </param>
    /// <param name="starred" type="Boolean">
    /// </param>
    /// <param name="isStarredByDefault" type="Boolean">
    /// </param>
    /// <param name="pinable" type="Boolean">
    /// </param>
    /// <param name="starrable" type="Boolean">
    /// </param>
    /// <param name="selected" type="Boolean">
    /// </param>
    /// <param name="guest" type="Boolean">
    /// </param>
    /// <param name="newMessages" type="Number" integer="true">
    /// </param>
    /// <param name="totalMessages" type="Number" integer="true">
    /// </param>
    /// <param name="latestItem" type="String">
    /// </param>
    /// <param name="latestItemSeen" type="String">
    /// </param>
    /// <param name="latestItemOld" type="String">
    /// </param>
    /// <param name="readOnly" type="Boolean">
    /// </param>
    /// <param name="listOrder" type="Number" integer="true">
    /// </param>
    /// <param name="isPhotoChatRoom" type="Boolean">
    /// </param>
    /// <param name="isPrivateChatRoom" type="Boolean">
    /// </param>
    /// <param name="isNewPhotoAlertsRoom" type="Boolean">
    /// </param>
    /// <param name="presence" type="SpottedScript.Controls.ChatClient.Shared.PresenceState">
    /// </param>
    /// <param name="icon" type="String">
    /// </param>
    /// <param name="tokenDateTimeTicks" type="String">
    /// </param>
    /// <param name="token" type="String">
    /// </param>
    /// <param name="hasArchive" type="Boolean">
    /// </param>
    /// <param name="hiddenFromRoomList" type="Boolean">
    /// </param>
    /// <param name="isStreamRoom" type="Boolean">
    /// </param>
    /// <field name="parentClientID" type="String">
    /// </field>
    /// <field name="guid" type="String">
    /// </field>
    /// <field name="name" type="String">
    /// </field>
    /// <field name="url" type="String">
    /// </field>
    /// <field name="pinned" type="Boolean">
    /// </field>
    /// <field name="pinable" type="Boolean">
    /// </field>
    /// <field name="starred" type="Boolean">
    /// </field>
    /// <field name="starrable" type="Boolean">
    /// </field>
    /// <field name="isStarredByDefault" type="Boolean">
    /// </field>
    /// <field name="selected" type="Boolean">
    /// </field>
    /// <field name="guest" type="Boolean">
    /// </field>
    /// <field name="newMessages" type="Number" integer="true">
    /// </field>
    /// <field name="totalMessages" type="Number" integer="true">
    /// </field>
    /// <field name="latestItem" type="String">
    /// </field>
    /// <field name="latestItemSeen" type="String">
    /// </field>
    /// <field name="latestItemOld" type="String">
    /// </field>
    /// <field name="readOnly" type="Boolean">
    /// </field>
    /// <field name="listOrder" type="Number" integer="true">
    /// </field>
    /// <field name="isPhotoChatRoom" type="Boolean">
    /// </field>
    /// <field name="isPrivateChatRoom" type="Boolean">
    /// </field>
    /// <field name="isNewPhotoAlertsRoom" type="Boolean">
    /// </field>
    /// <field name="presence" type="SpottedScript.Controls.ChatClient.Shared.PresenceState">
    /// </field>
    /// <field name="icon" type="String">
    /// </field>
    /// <field name="tokenDateTimeTicks" type="String">
    /// </field>
    /// <field name="token" type="String">
    /// </field>
    /// <field name="hasArchive" type="Boolean">
    /// </field>
    /// <field name="hiddenFromRoomList" type="Boolean">
    /// </field>
    /// <field name="isStreamRoom" type="Boolean">
    /// </field>
    this.parentClientID = parentClientID;
    this.guid = guid;
    this.name = name;
    this.url = url;
    this.pinned = pinned;
    this.starred = starred;
    this.isStarredByDefault = isStarredByDefault;
    this.pinable = pinable;
    this.starrable = starrable;
    this.selected = selected;
    this.guest = guest;
    this.newMessages = newMessages;
    this.totalMessages = totalMessages;
    this.latestItem = latestItem;
    this.latestItemSeen = latestItemSeen;
    this.latestItemOld = latestItemOld;
    this.readOnly = readOnly;
    this.listOrder = listOrder;
    this.isPhotoChatRoom = isPhotoChatRoom;
    this.isPrivateChatRoom = isPrivateChatRoom;
    this.isNewPhotoAlertsRoom = isNewPhotoAlertsRoom;
    this.presence = presence;
    this.icon = icon;
    this.tokenDateTimeTicks = tokenDateTimeTicks;
    this.token = token;
    this.hasArchive = hasArchive;
    this.hiddenFromRoomList = hiddenFromRoomList;
    this.isStreamRoom = isStreamRoom;
}
SpottedScript.Controls.ChatClient.Shared.RoomStub.prototype = {
    parentClientID: null,
    guid: null,
    name: null,
    url: null,
    pinned: false,
    pinable: false,
    starred: false,
    starrable: false,
    isStarredByDefault: false,
    selected: false,
    guest: false,
    newMessages: 0,
    totalMessages: 0,
    latestItem: null,
    latestItemSeen: null,
    latestItemOld: null,
    readOnly: false,
    listOrder: 0,
    isPhotoChatRoom: false,
    isPrivateChatRoom: false,
    isNewPhotoAlertsRoom: false,
    presence: 0,
    icon: null,
    tokenDateTimeTicks: null,
    token: null,
    hasArchive: false,
    hiddenFromRoomList: false,
    isStreamRoom: false
}
SpottedScript.Controls.ChatClient.Items.Item.registerClass('SpottedScript.Controls.ChatClient.Items.Item');
SpottedScript.Controls.ChatClient.Items.TopPhoto.registerClass('SpottedScript.Controls.ChatClient.Items.TopPhoto', SpottedScript.Controls.ChatClient.Items.Item);
SpottedScript.Controls.ChatClient.Items.Html.registerClass('SpottedScript.Controls.ChatClient.Items.Html', SpottedScript.Controls.ChatClient.Items.Item);
SpottedScript.Controls.ChatClient.Items.Newable.registerClass('SpottedScript.Controls.ChatClient.Items.Newable', SpottedScript.Controls.ChatClient.Items.Html);
SpottedScript.Controls.ChatClient.Items.Message.registerClass('SpottedScript.Controls.ChatClient.Items.Message', SpottedScript.Controls.ChatClient.Items.Newable, SpottedScript.Controls.ChatClient.Items.IHasPostingUsr);
SpottedScript.Controls.ChatClient.Items.CommentMessage.registerClass('SpottedScript.Controls.ChatClient.Items.CommentMessage', SpottedScript.Controls.ChatClient.Items.Message);
SpottedScript.Controls.ChatClient.Items.Alert.registerClass('SpottedScript.Controls.ChatClient.Items.Alert', SpottedScript.Controls.ChatClient.Items.Newable, SpottedScript.Controls.ChatClient.Items.IHasPostingUsr);
SpottedScript.Controls.ChatClient.Items.Laugh.registerClass('SpottedScript.Controls.ChatClient.Items.Laugh', SpottedScript.Controls.ChatClient.Items.CommentMessage);
SpottedScript.Controls.ChatClient.Items.Logout.registerClass('SpottedScript.Controls.ChatClient.Items.Logout', SpottedScript.Controls.ChatClient.Items.Alert);
SpottedScript.Controls.ChatClient.Items.Login.registerClass('SpottedScript.Controls.ChatClient.Items.Login', SpottedScript.Controls.ChatClient.Items.Alert);
SpottedScript.Controls.ChatClient.Items.Note.registerClass('SpottedScript.Controls.ChatClient.Items.Note', SpottedScript.Controls.ChatClient.Items.Html);
SpottedScript.Controls.ChatClient.Items.Photo.registerClass('SpottedScript.Controls.ChatClient.Items.Photo', SpottedScript.Controls.ChatClient.Items.Newable);
SpottedScript.Controls.ChatClient.Items.Error.registerClass('SpottedScript.Controls.ChatClient.Items.Error', SpottedScript.Controls.ChatClient.Items.Html);
SpottedScript.Controls.ChatClient.Items.Private.registerClass('SpottedScript.Controls.ChatClient.Items.Private', SpottedScript.Controls.ChatClient.Items.Message);
SpottedScript.Controls.ChatClient.PopupArea.registerClass('SpottedScript.Controls.ChatClient.PopupArea');
SpottedScript.Controls.ChatClient.Popup.registerClass('SpottedScript.Controls.ChatClient.Popup');
SpottedScript.Controls.ChatClient.Controller.registerClass('SpottedScript.Controls.ChatClient.Controller');
SpottedScript.Controls.ChatClient.Room.registerClass('SpottedScript.Controls.ChatClient.Room');
SpottedScript.Controls.ChatClient.StarActionEventArgs.registerClass('SpottedScript.Controls.ChatClient.StarActionEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.PinActionEventArgs.registerClass('SpottedScript.Controls.ChatClient.PinActionEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.RoomGuidEventArgs.registerClass('SpottedScript.Controls.ChatClient.RoomGuidEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.ServerClass.registerClass('SpottedScript.Controls.ChatClient.ServerClass');
SpottedScript.Controls.ChatClient.CriticalRequest.registerClass('SpottedScript.Controls.ChatClient.CriticalRequest');
SpottedScript.Controls.ChatClient.StoreUpdatedRoomListOrderRequest.registerClass('SpottedScript.Controls.ChatClient.StoreUpdatedRoomListOrderRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.SendMessageRequest.registerClass('SpottedScript.Controls.ChatClient.SendMessageRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.MoreInfoRequest.registerClass('SpottedScript.Controls.ChatClient.MoreInfoRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.ArchiveItemsRequest.registerClass('SpottedScript.Controls.ChatClient.ArchiveItemsRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.DeleteArchiveRequest.registerClass('SpottedScript.Controls.ChatClient.DeleteArchiveRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.UnPinRoomRequest.registerClass('SpottedScript.Controls.ChatClient.UnPinRoomRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.PinRoomRequest.registerClass('SpottedScript.Controls.ChatClient.PinRoomRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.StarRoomRequest.registerClass('SpottedScript.Controls.ChatClient.StarRoomRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.SwitchPhotoRoomRequest.registerClass('SpottedScript.Controls.ChatClient.SwitchPhotoRoomRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.RePinRoomRequest.registerClass('SpottedScript.Controls.ChatClient.RePinRoomRequest', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.ForceResetSessionAndGetState.registerClass('SpottedScript.Controls.ChatClient.ForceResetSessionAndGetState', SpottedScript.Controls.ChatClient.CriticalRequest);
SpottedScript.Controls.ChatClient.GotNoItemsEventArgs.registerClass('SpottedScript.Controls.ChatClient.GotNoItemsEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.GotMoreInfoEventArgs.registerClass('SpottedScript.Controls.ChatClient.GotMoreInfoEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.GotArchiveItemsEventArgs.registerClass('SpottedScript.Controls.ChatClient.GotArchiveItemsEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.GotItemsEventArgs.registerClass('SpottedScript.Controls.ChatClient.GotItemsEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.GotRoomEventArgs.registerClass('SpottedScript.Controls.ChatClient.GotRoomEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.GotRoomStateEventArgs.registerClass('SpottedScript.Controls.ChatClient.GotRoomStateEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.GotExceptionEventArgs.registerClass('SpottedScript.Controls.ChatClient.GotExceptionEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.DebugPrintEventArgs.registerClass('SpottedScript.Controls.ChatClient.DebugPrintEventArgs', Sys.EventArgs);
SpottedScript.Controls.ChatClient.View.registerClass('SpottedScript.Controls.ChatClient.View');
SpottedScript.Controls.ChatClient.Shared.RefreshStub.registerClass('SpottedScript.Controls.ChatClient.Shared.RefreshStub');
SpottedScript.Controls.ChatClient.Shared.ArchiveStub.registerClass('SpottedScript.Controls.ChatClient.Shared.ArchiveStub', SpottedScript.Controls.ChatClient.Shared.RefreshStub);
SpottedScript.Controls.ChatClient.Shared.DeleteArchiveStub.registerClass('SpottedScript.Controls.ChatClient.Shared.DeleteArchiveStub', SpottedScript.Controls.ChatClient.Shared.RefreshStub);
SpottedScript.Controls.ChatClient.Shared.ItemStub.registerClass('SpottedScript.Controls.ChatClient.Shared.ItemStub');
SpottedScript.Controls.ChatClient.Shared.TopPhotoStub.registerClass('SpottedScript.Controls.ChatClient.Shared.TopPhotoStub', SpottedScript.Controls.ChatClient.Shared.ItemStub);
SpottedScript.Controls.ChatClient.Shared.UnPinStub.registerClass('SpottedScript.Controls.ChatClient.Shared.UnPinStub', SpottedScript.Controls.ChatClient.Shared.RefreshStub);
SpottedScript.Controls.ChatClient.Shared.MessageStub.registerClass('SpottedScript.Controls.ChatClient.Shared.MessageStub', SpottedScript.Controls.ChatClient.Shared.ItemStub);
SpottedScript.Controls.ChatClient.Shared.CommentMessageStub.registerClass('SpottedScript.Controls.ChatClient.Shared.CommentMessageStub', SpottedScript.Controls.ChatClient.Shared.MessageStub);
SpottedScript.Controls.ChatClient.Shared.MoreInfoStub.registerClass('SpottedScript.Controls.ChatClient.Shared.MoreInfoStub', SpottedScript.Controls.ChatClient.Shared.RefreshStub);
SpottedScript.Controls.ChatClient.Shared.AlertStub.registerClass('SpottedScript.Controls.ChatClient.Shared.AlertStub', SpottedScript.Controls.ChatClient.Shared.ItemStub);
SpottedScript.Controls.ChatClient.Shared.LaughStub.registerClass('SpottedScript.Controls.ChatClient.Shared.LaughStub', SpottedScript.Controls.ChatClient.Shared.CommentMessageStub);
SpottedScript.Controls.ChatClient.Shared.GuestRefreshStub.registerClass('SpottedScript.Controls.ChatClient.Shared.GuestRefreshStub');
SpottedScript.Controls.ChatClient.Shared.GetStateStub.registerClass('SpottedScript.Controls.ChatClient.Shared.GetStateStub', SpottedScript.Controls.ChatClient.Shared.RefreshStub);
SpottedScript.Controls.ChatClient.Shared.PrivateStub.registerClass('SpottedScript.Controls.ChatClient.Shared.PrivateStub', SpottedScript.Controls.ChatClient.Shared.MessageStub);
SpottedScript.Controls.ChatClient.Shared.StateStub.registerClass('SpottedScript.Controls.ChatClient.Shared.StateStub');
SpottedScript.Controls.ChatClient.Shared.RoomHtml.registerClass('SpottedScript.Controls.ChatClient.Shared.RoomHtml');
SpottedScript.Controls.ChatClient.Shared.SendStub.registerClass('SpottedScript.Controls.ChatClient.Shared.SendStub', SpottedScript.Controls.ChatClient.Shared.RefreshStub);
SpottedScript.Controls.ChatClient.Shared.PinStub.registerClass('SpottedScript.Controls.ChatClient.Shared.PinStub', SpottedScript.Controls.ChatClient.Shared.RefreshStub);
SpottedScript.Controls.ChatClient.Shared.PhotoStub.registerClass('SpottedScript.Controls.ChatClient.Shared.PhotoStub', SpottedScript.Controls.ChatClient.Shared.ItemStub);
SpottedScript.Controls.ChatClient.Shared.RoomStub.registerClass('SpottedScript.Controls.ChatClient.Shared.RoomStub');
SpottedScript.Controls.ChatClient.PopupArea.popupWidth = 250;
SpottedScript.Controls.ChatClient.PopupArea.popupHeight = 170;
SpottedScript.Controls.ChatClient.Controller.instance = null;
