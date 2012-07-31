//! ThreadControl.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.ThreadControl');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ThreadControl.Controller

Js.Controls.ThreadControl.Controller = function Js_Controls_ThreadControl_Controller(view) {
    /// <param name="view" type="Js.Controls.ThreadControl.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.ThreadControl.View">
    /// </field>
    /// <field name="onThreadCreated" type="Function">
    /// </field>
    /// <field name="onCommentPosted" type="Function">
    /// </field>
    /// <field name="_duplicateGuid" type="String">
    /// </field>
    this._view = view;
    view.get_commentHtml().get_saveButton().setAttribute('onclick', '');
    view.get_commentHtml().get_saveButtonJ().click(ss.Delegate.create(this, this._postCommentClick));
    this._createNewDuplicateGuid();
    if (view.get_addThreadAdvancedCheckBox() != null) {
        view.get_addThreadAdvancedCheckBoxJ().click(ss.Delegate.create(this, this._advancedCheckBoxClicked));
    }
    view.get_uiCommentsDisplay().threadCommentsProvider.onCommentPosted = ss.Delegate.create(this, this._commentPosted);
    view.get_uiCommentsDisplay().threadCommentsProvider.onThreadCreated = ss.Delegate.create(this, this._threadCreated);
    view.get_uiCommentsDisplay().onCommentsDisplayed = ss.Delegate.create(this, this._commentsDisplayed);
}
Js.Controls.ThreadControl.Controller.prototype = {
    _view: null,
    
    get_uiCommentsDisplay: function Js_Controls_ThreadControl_Controller$get_uiCommentsDisplay() {
        /// <value type="Js.Controls.CommentsDisplay.Controller"></value>
        return this._view.get_uiCommentsDisplay();
    },
    
    onThreadCreated: null,
    onCommentPosted: null,
    _duplicateGuid: null,
    
    get_currentParentObjectK: function Js_Controls_ThreadControl_Controller$get_currentParentObjectK() {
        /// <value type="Number" integer="true"></value>
        try {
            return parseInt(this._view.get_uiParentObjectK().value);
        }
        catch ($e1) {
            return 0;
        }
    },
    set_currentParentObjectK: function Js_Controls_ThreadControl_Controller$set_currentParentObjectK(value) {
        /// <value type="Number" integer="true"></value>
        this._view.get_uiParentObjectK().value = value.toString();
        return value;
    },
    
    get__currentParentObjectType: function Js_Controls_ThreadControl_Controller$get__currentParentObjectType() {
        /// <value type="Number" integer="true"></value>
        try {
            return parseInt(this._view.get_uiParentObjectType().value);
        }
        catch ($e1) {
            return 0;
        }
    },
    
    _commentsDisplayed: function Js_Controls_ThreadControl_Controller$_commentsDisplayed(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._view.get_commentHtml().clearHtml();
        try {
            if (this._view.get_uiMultiBuddyChooser() != null) {
                this._view.get_uiMultiBuddyChooser().clear();
            }
        }
        catch ($e1) {
        }
    },
    
    _commentPosted: function Js_Controls_ThreadControl_Controller$_commentPosted(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.onCommentPosted != null) {
            this.onCommentPosted(null, e);
        }
        this._createNewDuplicateGuid();
        this._view.get_commentHtml().clearHtml();
    },
    
    _advancedCheckBoxClicked: function Js_Controls_ThreadControl_Controller$_advancedCheckBoxClicked(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        WhenLoggedIn(ss.Delegate.create(this, function() {
            this._view.get_addThreadAdvancedPanel().style.display = (this._view.get_addThreadAdvancedCheckBox().checked) ? '' : 'none';
        }));
    },
    
    _createNewDuplicateGuid: function Js_Controls_ThreadControl_Controller$_createNewDuplicateGuid() {
        Js.Controls.CommentsDisplay.Service.getNewGuid(ss.Delegate.create(this, this._getNewGuidSuccess), Js.Library.Trace.webServiceFailure, null, -1);
    },
    
    _getNewGuidSuccess: function Js_Controls_ThreadControl_Controller$_getNewGuidSuccess(guid, userContext, methodName) {
        /// <param name="guid" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._duplicateGuid = guid;
    },
    
    _threadCreated: function Js_Controls_ThreadControl_Controller$_threadCreated(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.onThreadCreated != null) {
            this.onThreadCreated(o, e);
        }
    },
    
    _postCommentClick: function Js_Controls_ThreadControl_Controller$_postCommentClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(ss.Delegate.create(this, function() {
            this._postCommentClickInner();
        }));
    },
    
    _postCommentClickInner: function Js_Controls_ThreadControl_Controller$_postCommentClickInner() {
        var rawCommentHtml = this._view.get_commentHtml().get_rawHtml();
        if (!rawCommentHtml.trim().length) {
            return;
        }
        Js.Library.Misc.showWaitingCursor();
        var inviteUsrOptions = this._view.get_uiMultiBuddyChooser().get_selectedValues();
        if (!this._view.get_addThreadAdvancedCheckBox().checked || this._view.get_addThreadPublicRadioButton().checked || this._view.get_addThreadNewPublicRadioButton().checked) {
            if (!this._view.get_uiCommentsDisplay().threadCommentsProvider.get_threadK()) {
                this._view.get_uiCommentsDisplay().threadCommentsProvider.createPublicThread(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get_formatting(), this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadNewsCheckBox().checked, inviteUsrOptions);
            }
            else if (this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadNewPublicRadioButton().checked) {
                this._view.get_uiCommentsDisplay().threadCommentsProvider.createNewPublicThread(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get_formatting(), this._view.get_addThreadNewsCheckBox().checked, inviteUsrOptions);
            }
            else {
                this._view.get_uiCommentsDisplay().threadCommentsProvider.createReply(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._view.get_uiCommentsDisplay().threadCommentsProvider.get_threadK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get_formatting(), this._view.get_uiCommentsDisplay().threadCommentsProvider.get_lastKnownCommentK(), inviteUsrOptions);
            }
        }
        else if (this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadPrivateRadioButton().checked) {
            Js.Controls.CommentsDisplay.Service.createPrivateThread(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get_formatting(), inviteUsrOptions, this._view.get_addThreadSealedCheckBox().checked, ss.Delegate.create(this, this._createPrivateThreadSuccess), null, null, -1);
        }
        else if (this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadGroupRadioButton().checked) {
            var groupK = parseInt(this._view.get_addThreadGroupDropDown().value);
            Js.Controls.CommentsDisplay.Service.createNewThreadInGroup(groupK, this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get_formatting(), this._view.get_addThreadNewsCheckBox().checked, inviteUsrOptions, this._view.get_addThreadGroupPrivateCheckBox().checked, ss.Delegate.create(this, this._createNewThreadInGroupSuccess), null, null, -1);
        }
    },
    
    _createPrivateThreadSuccess: function Js_Controls_ThreadControl_Controller$_createPrivateThreadSuccess(newThreadUrl, userContext, methodName) {
        /// <param name="newThreadUrl" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
        Js.Library.Misc.redirect(newThreadUrl);
    },
    
    _createNewThreadInGroupSuccess: function Js_Controls_ThreadControl_Controller$_createNewThreadInGroupSuccess(newThreadUrl, userContext, methodName) {
        /// <param name="newThreadUrl" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
        Js.Library.Misc.redirect(newThreadUrl);
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ThreadControl.View

Js.Controls.ThreadControl.View = function Js_Controls_ThreadControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_PostCommentPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_PostCommentPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_H1" type="Object" domElement="true">
    /// </field>
    /// <field name="_H1J" type="jQueryObject">
    /// </field>
    /// <field name="_ThreadWatchButtonHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_ThreadWatchButtonHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_InlineScript3" type="Object" domElement="true">
    /// </field>
    /// <field name="_InlineScript3J" type="jQueryObject">
    /// </field>
    /// <field name="_ThreadFavouriteButtonP" type="Object" domElement="true">
    /// </field>
    /// <field name="_ThreadFavouriteButtonPJ" type="jQueryObject">
    /// </field>
    /// <field name="_ThreadFavouriteButtonHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_ThreadFavouriteButtonHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_InlineScript2" type="Object" domElement="true">
    /// </field>
    /// <field name="_InlineScript2J" type="jQueryObject">
    /// </field>
    /// <field name="_RequiredFieldValidator1" type="Object" domElement="true">
    /// </field>
    /// <field name="_RequiredFieldValidator1J" type="jQueryObject">
    /// </field>
    /// <field name="_CommentGroupMemberPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentGroupMemberPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentGroupMemberAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentGroupMemberAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentLoginPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentLoginPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentEmailVerifyPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentEmailVerifyPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadAdvancedCheckBoxP" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadAdvancedCheckBoxPJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadAdvancedCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadAdvancedCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadAdvancedPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadAdvancedPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPublicRadioButtonSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPublicRadioButtonSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPublicRadioButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPublicRadioButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadNewPublicRadioButtonSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNewPublicRadioButtonSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadNewPublicRadioButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNewPublicRadioButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButtonSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButtonSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadPrivateRadioButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupRadioButtonSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupRadioButtonSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupRadioButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupRadioButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadGroupPrivateCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadNewsCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNewsCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadNewsCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadNewsCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadSealedCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadSealedCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadSealedCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadSealedCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadInviteCheckBoxSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadInviteCheckBoxSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadInviteCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadInviteCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_AddThreadInvitePanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_AddThreadInvitePanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_InlineScript1" type="Object" domElement="true">
    /// </field>
    /// <field name="_InlineScript1J" type="jQueryObject">
    /// </field>
    /// <field name="_uiThreadK" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiThreadKJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiParentObjectK" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiParentObjectKJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiParentObjectType" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiParentObjectTypeJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.ThreadControl.View.prototype = {
    clientId: null,
    
    get_uiCommentsDisplay: function Js_Controls_ThreadControl_View$get_uiCommentsDisplay() {
        /// <value type="Js.Controls.CommentsDisplay.Controller"></value>
        return eval(this.clientId + '_uiCommentsDisplayController');
    },
    
    get_postCommentPanel: function Js_Controls_ThreadControl_View$get_postCommentPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._PostCommentPanel == null) {
            this._PostCommentPanel = document.getElementById(this.clientId + '_PostCommentPanel');
        }
        return this._PostCommentPanel;
    },
    
    _PostCommentPanel: null,
    
    get_postCommentPanelJ: function Js_Controls_ThreadControl_View$get_postCommentPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._PostCommentPanelJ == null) {
            this._PostCommentPanelJ = $('#' + this.clientId + '_PostCommentPanel');
        }
        return this._PostCommentPanelJ;
    },
    
    _PostCommentPanelJ: null,
    
    get_h1: function Js_Controls_ThreadControl_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        if (this._H1 == null) {
            this._H1 = document.getElementById(this.clientId + '_H1');
        }
        return this._H1;
    },
    
    _H1: null,
    
    get_h1J: function Js_Controls_ThreadControl_View$get_h1J() {
        /// <value type="jQueryObject"></value>
        if (this._H1J == null) {
            this._H1J = $('#' + this.clientId + '_H1');
        }
        return this._H1J;
    },
    
    _H1J: null,
    
    get_threadWatchButtonHolder: function Js_Controls_ThreadControl_View$get_threadWatchButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._ThreadWatchButtonHolder == null) {
            this._ThreadWatchButtonHolder = document.getElementById(this.clientId + '_ThreadWatchButtonHolder');
        }
        return this._ThreadWatchButtonHolder;
    },
    
    _ThreadWatchButtonHolder: null,
    
    get_threadWatchButtonHolderJ: function Js_Controls_ThreadControl_View$get_threadWatchButtonHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._ThreadWatchButtonHolderJ == null) {
            this._ThreadWatchButtonHolderJ = $('#' + this.clientId + '_ThreadWatchButtonHolder');
        }
        return this._ThreadWatchButtonHolderJ;
    },
    
    _ThreadWatchButtonHolderJ: null,
    
    get_inlineScript3: function Js_Controls_ThreadControl_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        if (this._InlineScript3 == null) {
            this._InlineScript3 = document.getElementById(this.clientId + '_InlineScript3');
        }
        return this._InlineScript3;
    },
    
    _InlineScript3: null,
    
    get_inlineScript3J: function Js_Controls_ThreadControl_View$get_inlineScript3J() {
        /// <value type="jQueryObject"></value>
        if (this._InlineScript3J == null) {
            this._InlineScript3J = $('#' + this.clientId + '_InlineScript3');
        }
        return this._InlineScript3J;
    },
    
    _InlineScript3J: null,
    
    get_threadFavouriteButtonP: function Js_Controls_ThreadControl_View$get_threadFavouriteButtonP() {
        /// <value type="Object" domElement="true"></value>
        if (this._ThreadFavouriteButtonP == null) {
            this._ThreadFavouriteButtonP = document.getElementById(this.clientId + '_ThreadFavouriteButtonP');
        }
        return this._ThreadFavouriteButtonP;
    },
    
    _ThreadFavouriteButtonP: null,
    
    get_threadFavouriteButtonPJ: function Js_Controls_ThreadControl_View$get_threadFavouriteButtonPJ() {
        /// <value type="jQueryObject"></value>
        if (this._ThreadFavouriteButtonPJ == null) {
            this._ThreadFavouriteButtonPJ = $('#' + this.clientId + '_ThreadFavouriteButtonP');
        }
        return this._ThreadFavouriteButtonPJ;
    },
    
    _ThreadFavouriteButtonPJ: null,
    
    get_threadFavouriteButtonHolder: function Js_Controls_ThreadControl_View$get_threadFavouriteButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._ThreadFavouriteButtonHolder == null) {
            this._ThreadFavouriteButtonHolder = document.getElementById(this.clientId + '_ThreadFavouriteButtonHolder');
        }
        return this._ThreadFavouriteButtonHolder;
    },
    
    _ThreadFavouriteButtonHolder: null,
    
    get_threadFavouriteButtonHolderJ: function Js_Controls_ThreadControl_View$get_threadFavouriteButtonHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._ThreadFavouriteButtonHolderJ == null) {
            this._ThreadFavouriteButtonHolderJ = $('#' + this.clientId + '_ThreadFavouriteButtonHolder');
        }
        return this._ThreadFavouriteButtonHolderJ;
    },
    
    _ThreadFavouriteButtonHolderJ: null,
    
    get_inlineScript2: function Js_Controls_ThreadControl_View$get_inlineScript2() {
        /// <value type="Object" domElement="true"></value>
        if (this._InlineScript2 == null) {
            this._InlineScript2 = document.getElementById(this.clientId + '_InlineScript2');
        }
        return this._InlineScript2;
    },
    
    _InlineScript2: null,
    
    get_inlineScript2J: function Js_Controls_ThreadControl_View$get_inlineScript2J() {
        /// <value type="jQueryObject"></value>
        if (this._InlineScript2J == null) {
            this._InlineScript2J = $('#' + this.clientId + '_InlineScript2');
        }
        return this._InlineScript2J;
    },
    
    _InlineScript2J: null,
    
    get_requiredFieldValidator1: function Js_Controls_ThreadControl_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        if (this._RequiredFieldValidator1 == null) {
            this._RequiredFieldValidator1 = document.getElementById(this.clientId + '_RequiredFieldValidator1');
        }
        return this._RequiredFieldValidator1;
    },
    
    _RequiredFieldValidator1: null,
    
    get_requiredFieldValidator1J: function Js_Controls_ThreadControl_View$get_requiredFieldValidator1J() {
        /// <value type="jQueryObject"></value>
        if (this._RequiredFieldValidator1J == null) {
            this._RequiredFieldValidator1J = $('#' + this.clientId + '_RequiredFieldValidator1');
        }
        return this._RequiredFieldValidator1J;
    },
    
    _RequiredFieldValidator1J: null,
    
    get_commentGroupMemberPanel: function Js_Controls_ThreadControl_View$get_commentGroupMemberPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentGroupMemberPanel == null) {
            this._CommentGroupMemberPanel = document.getElementById(this.clientId + '_CommentGroupMemberPanel');
        }
        return this._CommentGroupMemberPanel;
    },
    
    _CommentGroupMemberPanel: null,
    
    get_commentGroupMemberPanelJ: function Js_Controls_ThreadControl_View$get_commentGroupMemberPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentGroupMemberPanelJ == null) {
            this._CommentGroupMemberPanelJ = $('#' + this.clientId + '_CommentGroupMemberPanel');
        }
        return this._CommentGroupMemberPanelJ;
    },
    
    _CommentGroupMemberPanelJ: null,
    
    get_commentGroupMemberAnchor: function Js_Controls_ThreadControl_View$get_commentGroupMemberAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentGroupMemberAnchor == null) {
            this._CommentGroupMemberAnchor = document.getElementById(this.clientId + '_CommentGroupMemberAnchor');
        }
        return this._CommentGroupMemberAnchor;
    },
    
    _CommentGroupMemberAnchor: null,
    
    get_commentGroupMemberAnchorJ: function Js_Controls_ThreadControl_View$get_commentGroupMemberAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentGroupMemberAnchorJ == null) {
            this._CommentGroupMemberAnchorJ = $('#' + this.clientId + '_CommentGroupMemberAnchor');
        }
        return this._CommentGroupMemberAnchorJ;
    },
    
    _CommentGroupMemberAnchorJ: null,
    
    get_commentLoginPanel: function Js_Controls_ThreadControl_View$get_commentLoginPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentLoginPanel == null) {
            this._CommentLoginPanel = document.getElementById(this.clientId + '_CommentLoginPanel');
        }
        return this._CommentLoginPanel;
    },
    
    _CommentLoginPanel: null,
    
    get_commentLoginPanelJ: function Js_Controls_ThreadControl_View$get_commentLoginPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentLoginPanelJ == null) {
            this._CommentLoginPanelJ = $('#' + this.clientId + '_CommentLoginPanel');
        }
        return this._CommentLoginPanelJ;
    },
    
    _CommentLoginPanelJ: null,
    
    get_commentEmailVerifyPanel: function Js_Controls_ThreadControl_View$get_commentEmailVerifyPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentEmailVerifyPanel == null) {
            this._CommentEmailVerifyPanel = document.getElementById(this.clientId + '_CommentEmailVerifyPanel');
        }
        return this._CommentEmailVerifyPanel;
    },
    
    _CommentEmailVerifyPanel: null,
    
    get_commentEmailVerifyPanelJ: function Js_Controls_ThreadControl_View$get_commentEmailVerifyPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentEmailVerifyPanelJ == null) {
            this._CommentEmailVerifyPanelJ = $('#' + this.clientId + '_CommentEmailVerifyPanel');
        }
        return this._CommentEmailVerifyPanelJ;
    },
    
    _CommentEmailVerifyPanelJ: null,
    
    get_commentHtml: function Js_Controls_ThreadControl_View$get_commentHtml() {
        /// <value type="Js.Controls.Html.Controller"></value>
        return eval(this.clientId + '_CommentHtmlController');
    },
    
    get_addThreadAdvancedCheckBoxP: function Js_Controls_ThreadControl_View$get_addThreadAdvancedCheckBoxP() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadAdvancedCheckBoxP == null) {
            this._AddThreadAdvancedCheckBoxP = document.getElementById(this.clientId + '_AddThreadAdvancedCheckBoxP');
        }
        return this._AddThreadAdvancedCheckBoxP;
    },
    
    _AddThreadAdvancedCheckBoxP: null,
    
    get_addThreadAdvancedCheckBoxPJ: function Js_Controls_ThreadControl_View$get_addThreadAdvancedCheckBoxPJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadAdvancedCheckBoxPJ == null) {
            this._AddThreadAdvancedCheckBoxPJ = $('#' + this.clientId + '_AddThreadAdvancedCheckBoxP');
        }
        return this._AddThreadAdvancedCheckBoxPJ;
    },
    
    _AddThreadAdvancedCheckBoxPJ: null,
    
    get_addThreadAdvancedCheckBox: function Js_Controls_ThreadControl_View$get_addThreadAdvancedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadAdvancedCheckBox == null) {
            this._AddThreadAdvancedCheckBox = document.getElementById(this.clientId + '_AddThreadAdvancedCheckBox');
        }
        return this._AddThreadAdvancedCheckBox;
    },
    
    _AddThreadAdvancedCheckBox: null,
    
    get_addThreadAdvancedCheckBoxJ: function Js_Controls_ThreadControl_View$get_addThreadAdvancedCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadAdvancedCheckBoxJ == null) {
            this._AddThreadAdvancedCheckBoxJ = $('#' + this.clientId + '_AddThreadAdvancedCheckBox');
        }
        return this._AddThreadAdvancedCheckBoxJ;
    },
    
    _AddThreadAdvancedCheckBoxJ: null,
    
    get_addThreadAdvancedPanel: function Js_Controls_ThreadControl_View$get_addThreadAdvancedPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadAdvancedPanel == null) {
            this._AddThreadAdvancedPanel = document.getElementById(this.clientId + '_AddThreadAdvancedPanel');
        }
        return this._AddThreadAdvancedPanel;
    },
    
    _AddThreadAdvancedPanel: null,
    
    get_addThreadAdvancedPanelJ: function Js_Controls_ThreadControl_View$get_addThreadAdvancedPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadAdvancedPanelJ == null) {
            this._AddThreadAdvancedPanelJ = $('#' + this.clientId + '_AddThreadAdvancedPanel');
        }
        return this._AddThreadAdvancedPanelJ;
    },
    
    _AddThreadAdvancedPanelJ: null,
    
    get_addThreadPublicRadioButtonSpan: function Js_Controls_ThreadControl_View$get_addThreadPublicRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPublicRadioButtonSpan == null) {
            this._AddThreadPublicRadioButtonSpan = document.getElementById(this.clientId + '_AddThreadPublicRadioButtonSpan');
        }
        return this._AddThreadPublicRadioButtonSpan;
    },
    
    _AddThreadPublicRadioButtonSpan: null,
    
    get_addThreadPublicRadioButtonSpanJ: function Js_Controls_ThreadControl_View$get_addThreadPublicRadioButtonSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPublicRadioButtonSpanJ == null) {
            this._AddThreadPublicRadioButtonSpanJ = $('#' + this.clientId + '_AddThreadPublicRadioButtonSpan');
        }
        return this._AddThreadPublicRadioButtonSpanJ;
    },
    
    _AddThreadPublicRadioButtonSpanJ: null,
    
    get_addThreadPublicRadioButton: function Js_Controls_ThreadControl_View$get_addThreadPublicRadioButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPublicRadioButton == null) {
            this._AddThreadPublicRadioButton = document.getElementById(this.clientId + '_AddThreadPublicRadioButton');
        }
        return this._AddThreadPublicRadioButton;
    },
    
    _AddThreadPublicRadioButton: null,
    
    get_addThreadPublicRadioButtonJ: function Js_Controls_ThreadControl_View$get_addThreadPublicRadioButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPublicRadioButtonJ == null) {
            this._AddThreadPublicRadioButtonJ = $('#' + this.clientId + '_AddThreadPublicRadioButton');
        }
        return this._AddThreadPublicRadioButtonJ;
    },
    
    _AddThreadPublicRadioButtonJ: null,
    
    get_addThreadNewPublicRadioButtonSpan: function Js_Controls_ThreadControl_View$get_addThreadNewPublicRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNewPublicRadioButtonSpan == null) {
            this._AddThreadNewPublicRadioButtonSpan = document.getElementById(this.clientId + '_AddThreadNewPublicRadioButtonSpan');
        }
        return this._AddThreadNewPublicRadioButtonSpan;
    },
    
    _AddThreadNewPublicRadioButtonSpan: null,
    
    get_addThreadNewPublicRadioButtonSpanJ: function Js_Controls_ThreadControl_View$get_addThreadNewPublicRadioButtonSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNewPublicRadioButtonSpanJ == null) {
            this._AddThreadNewPublicRadioButtonSpanJ = $('#' + this.clientId + '_AddThreadNewPublicRadioButtonSpan');
        }
        return this._AddThreadNewPublicRadioButtonSpanJ;
    },
    
    _AddThreadNewPublicRadioButtonSpanJ: null,
    
    get_addThreadNewPublicRadioButton: function Js_Controls_ThreadControl_View$get_addThreadNewPublicRadioButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNewPublicRadioButton == null) {
            this._AddThreadNewPublicRadioButton = document.getElementById(this.clientId + '_AddThreadNewPublicRadioButton');
        }
        return this._AddThreadNewPublicRadioButton;
    },
    
    _AddThreadNewPublicRadioButton: null,
    
    get_addThreadNewPublicRadioButtonJ: function Js_Controls_ThreadControl_View$get_addThreadNewPublicRadioButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNewPublicRadioButtonJ == null) {
            this._AddThreadNewPublicRadioButtonJ = $('#' + this.clientId + '_AddThreadNewPublicRadioButton');
        }
        return this._AddThreadNewPublicRadioButtonJ;
    },
    
    _AddThreadNewPublicRadioButtonJ: null,
    
    get_addThreadPrivateRadioButtonSpan: function Js_Controls_ThreadControl_View$get_addThreadPrivateRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPrivateRadioButtonSpan == null) {
            this._AddThreadPrivateRadioButtonSpan = document.getElementById(this.clientId + '_AddThreadPrivateRadioButtonSpan');
        }
        return this._AddThreadPrivateRadioButtonSpan;
    },
    
    _AddThreadPrivateRadioButtonSpan: null,
    
    get_addThreadPrivateRadioButtonSpanJ: function Js_Controls_ThreadControl_View$get_addThreadPrivateRadioButtonSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPrivateRadioButtonSpanJ == null) {
            this._AddThreadPrivateRadioButtonSpanJ = $('#' + this.clientId + '_AddThreadPrivateRadioButtonSpan');
        }
        return this._AddThreadPrivateRadioButtonSpanJ;
    },
    
    _AddThreadPrivateRadioButtonSpanJ: null,
    
    get_addThreadPrivateRadioButton: function Js_Controls_ThreadControl_View$get_addThreadPrivateRadioButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadPrivateRadioButton == null) {
            this._AddThreadPrivateRadioButton = document.getElementById(this.clientId + '_AddThreadPrivateRadioButton');
        }
        return this._AddThreadPrivateRadioButton;
    },
    
    _AddThreadPrivateRadioButton: null,
    
    get_addThreadPrivateRadioButtonJ: function Js_Controls_ThreadControl_View$get_addThreadPrivateRadioButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadPrivateRadioButtonJ == null) {
            this._AddThreadPrivateRadioButtonJ = $('#' + this.clientId + '_AddThreadPrivateRadioButton');
        }
        return this._AddThreadPrivateRadioButtonJ;
    },
    
    _AddThreadPrivateRadioButtonJ: null,
    
    get_addThreadGroupRadioButtonSpan: function Js_Controls_ThreadControl_View$get_addThreadGroupRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupRadioButtonSpan == null) {
            this._AddThreadGroupRadioButtonSpan = document.getElementById(this.clientId + '_AddThreadGroupRadioButtonSpan');
        }
        return this._AddThreadGroupRadioButtonSpan;
    },
    
    _AddThreadGroupRadioButtonSpan: null,
    
    get_addThreadGroupRadioButtonSpanJ: function Js_Controls_ThreadControl_View$get_addThreadGroupRadioButtonSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupRadioButtonSpanJ == null) {
            this._AddThreadGroupRadioButtonSpanJ = $('#' + this.clientId + '_AddThreadGroupRadioButtonSpan');
        }
        return this._AddThreadGroupRadioButtonSpanJ;
    },
    
    _AddThreadGroupRadioButtonSpanJ: null,
    
    get_addThreadGroupRadioButton: function Js_Controls_ThreadControl_View$get_addThreadGroupRadioButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupRadioButton == null) {
            this._AddThreadGroupRadioButton = document.getElementById(this.clientId + '_AddThreadGroupRadioButton');
        }
        return this._AddThreadGroupRadioButton;
    },
    
    _AddThreadGroupRadioButton: null,
    
    get_addThreadGroupRadioButtonJ: function Js_Controls_ThreadControl_View$get_addThreadGroupRadioButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupRadioButtonJ == null) {
            this._AddThreadGroupRadioButtonJ = $('#' + this.clientId + '_AddThreadGroupRadioButton');
        }
        return this._AddThreadGroupRadioButtonJ;
    },
    
    _AddThreadGroupRadioButtonJ: null,
    
    get_addThreadGroupDropDown: function Js_Controls_ThreadControl_View$get_addThreadGroupDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupDropDown == null) {
            this._AddThreadGroupDropDown = document.getElementById(this.clientId + '_AddThreadGroupDropDown');
        }
        return this._AddThreadGroupDropDown;
    },
    
    _AddThreadGroupDropDown: null,
    
    get_addThreadGroupDropDownJ: function Js_Controls_ThreadControl_View$get_addThreadGroupDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupDropDownJ == null) {
            this._AddThreadGroupDropDownJ = $('#' + this.clientId + '_AddThreadGroupDropDown');
        }
        return this._AddThreadGroupDropDownJ;
    },
    
    _AddThreadGroupDropDownJ: null,
    
    get_addThreadGroupPrivateCheckBoxSpan: function Js_Controls_ThreadControl_View$get_addThreadGroupPrivateCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupPrivateCheckBoxSpan == null) {
            this._AddThreadGroupPrivateCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBoxSpan');
        }
        return this._AddThreadGroupPrivateCheckBoxSpan;
    },
    
    _AddThreadGroupPrivateCheckBoxSpan: null,
    
    get_addThreadGroupPrivateCheckBoxSpanJ: function Js_Controls_ThreadControl_View$get_addThreadGroupPrivateCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupPrivateCheckBoxSpanJ == null) {
            this._AddThreadGroupPrivateCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadGroupPrivateCheckBoxSpan');
        }
        return this._AddThreadGroupPrivateCheckBoxSpanJ;
    },
    
    _AddThreadGroupPrivateCheckBoxSpanJ: null,
    
    get_addThreadGroupPrivateCheckBox: function Js_Controls_ThreadControl_View$get_addThreadGroupPrivateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadGroupPrivateCheckBox == null) {
            this._AddThreadGroupPrivateCheckBox = document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBox');
        }
        return this._AddThreadGroupPrivateCheckBox;
    },
    
    _AddThreadGroupPrivateCheckBox: null,
    
    get_addThreadGroupPrivateCheckBoxJ: function Js_Controls_ThreadControl_View$get_addThreadGroupPrivateCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadGroupPrivateCheckBoxJ == null) {
            this._AddThreadGroupPrivateCheckBoxJ = $('#' + this.clientId + '_AddThreadGroupPrivateCheckBox');
        }
        return this._AddThreadGroupPrivateCheckBoxJ;
    },
    
    _AddThreadGroupPrivateCheckBoxJ: null,
    
    get_addThreadNewsCheckBoxSpan: function Js_Controls_ThreadControl_View$get_addThreadNewsCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNewsCheckBoxSpan == null) {
            this._AddThreadNewsCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadNewsCheckBoxSpan');
        }
        return this._AddThreadNewsCheckBoxSpan;
    },
    
    _AddThreadNewsCheckBoxSpan: null,
    
    get_addThreadNewsCheckBoxSpanJ: function Js_Controls_ThreadControl_View$get_addThreadNewsCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNewsCheckBoxSpanJ == null) {
            this._AddThreadNewsCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadNewsCheckBoxSpan');
        }
        return this._AddThreadNewsCheckBoxSpanJ;
    },
    
    _AddThreadNewsCheckBoxSpanJ: null,
    
    get_addThreadNewsCheckBox: function Js_Controls_ThreadControl_View$get_addThreadNewsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadNewsCheckBox == null) {
            this._AddThreadNewsCheckBox = document.getElementById(this.clientId + '_AddThreadNewsCheckBox');
        }
        return this._AddThreadNewsCheckBox;
    },
    
    _AddThreadNewsCheckBox: null,
    
    get_addThreadNewsCheckBoxJ: function Js_Controls_ThreadControl_View$get_addThreadNewsCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadNewsCheckBoxJ == null) {
            this._AddThreadNewsCheckBoxJ = $('#' + this.clientId + '_AddThreadNewsCheckBox');
        }
        return this._AddThreadNewsCheckBoxJ;
    },
    
    _AddThreadNewsCheckBoxJ: null,
    
    get_addThreadSealedCheckBoxSpan: function Js_Controls_ThreadControl_View$get_addThreadSealedCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadSealedCheckBoxSpan == null) {
            this._AddThreadSealedCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadSealedCheckBoxSpan');
        }
        return this._AddThreadSealedCheckBoxSpan;
    },
    
    _AddThreadSealedCheckBoxSpan: null,
    
    get_addThreadSealedCheckBoxSpanJ: function Js_Controls_ThreadControl_View$get_addThreadSealedCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadSealedCheckBoxSpanJ == null) {
            this._AddThreadSealedCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadSealedCheckBoxSpan');
        }
        return this._AddThreadSealedCheckBoxSpanJ;
    },
    
    _AddThreadSealedCheckBoxSpanJ: null,
    
    get_addThreadSealedCheckBox: function Js_Controls_ThreadControl_View$get_addThreadSealedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadSealedCheckBox == null) {
            this._AddThreadSealedCheckBox = document.getElementById(this.clientId + '_AddThreadSealedCheckBox');
        }
        return this._AddThreadSealedCheckBox;
    },
    
    _AddThreadSealedCheckBox: null,
    
    get_addThreadSealedCheckBoxJ: function Js_Controls_ThreadControl_View$get_addThreadSealedCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadSealedCheckBoxJ == null) {
            this._AddThreadSealedCheckBoxJ = $('#' + this.clientId + '_AddThreadSealedCheckBox');
        }
        return this._AddThreadSealedCheckBoxJ;
    },
    
    _AddThreadSealedCheckBoxJ: null,
    
    get_addThreadInviteCheckBoxSpan: function Js_Controls_ThreadControl_View$get_addThreadInviteCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadInviteCheckBoxSpan == null) {
            this._AddThreadInviteCheckBoxSpan = document.getElementById(this.clientId + '_AddThreadInviteCheckBoxSpan');
        }
        return this._AddThreadInviteCheckBoxSpan;
    },
    
    _AddThreadInviteCheckBoxSpan: null,
    
    get_addThreadInviteCheckBoxSpanJ: function Js_Controls_ThreadControl_View$get_addThreadInviteCheckBoxSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadInviteCheckBoxSpanJ == null) {
            this._AddThreadInviteCheckBoxSpanJ = $('#' + this.clientId + '_AddThreadInviteCheckBoxSpan');
        }
        return this._AddThreadInviteCheckBoxSpanJ;
    },
    
    _AddThreadInviteCheckBoxSpanJ: null,
    
    get_addThreadInviteCheckBox: function Js_Controls_ThreadControl_View$get_addThreadInviteCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadInviteCheckBox == null) {
            this._AddThreadInviteCheckBox = document.getElementById(this.clientId + '_AddThreadInviteCheckBox');
        }
        return this._AddThreadInviteCheckBox;
    },
    
    _AddThreadInviteCheckBox: null,
    
    get_addThreadInviteCheckBoxJ: function Js_Controls_ThreadControl_View$get_addThreadInviteCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadInviteCheckBoxJ == null) {
            this._AddThreadInviteCheckBoxJ = $('#' + this.clientId + '_AddThreadInviteCheckBox');
        }
        return this._AddThreadInviteCheckBoxJ;
    },
    
    _AddThreadInviteCheckBoxJ: null,
    
    get_addThreadInvitePanel: function Js_Controls_ThreadControl_View$get_addThreadInvitePanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._AddThreadInvitePanel == null) {
            this._AddThreadInvitePanel = document.getElementById(this.clientId + '_AddThreadInvitePanel');
        }
        return this._AddThreadInvitePanel;
    },
    
    _AddThreadInvitePanel: null,
    
    get_addThreadInvitePanelJ: function Js_Controls_ThreadControl_View$get_addThreadInvitePanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._AddThreadInvitePanelJ == null) {
            this._AddThreadInvitePanelJ = $('#' + this.clientId + '_AddThreadInvitePanel');
        }
        return this._AddThreadInvitePanelJ;
    },
    
    _AddThreadInvitePanelJ: null,
    
    get_uiMultiBuddyChooser: function Js_Controls_ThreadControl_View$get_uiMultiBuddyChooser() {
        /// <value type="Js.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiMultiBuddyChooserController');
    },
    
    get_inlineScript1: function Js_Controls_ThreadControl_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        if (this._InlineScript1 == null) {
            this._InlineScript1 = document.getElementById(this.clientId + '_InlineScript1');
        }
        return this._InlineScript1;
    },
    
    _InlineScript1: null,
    
    get_inlineScript1J: function Js_Controls_ThreadControl_View$get_inlineScript1J() {
        /// <value type="jQueryObject"></value>
        if (this._InlineScript1J == null) {
            this._InlineScript1J = $('#' + this.clientId + '_InlineScript1');
        }
        return this._InlineScript1J;
    },
    
    _InlineScript1J: null,
    
    get_uiThreadK: function Js_Controls_ThreadControl_View$get_uiThreadK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiThreadK == null) {
            this._uiThreadK = document.getElementById(this.clientId + '_uiThreadK');
        }
        return this._uiThreadK;
    },
    
    _uiThreadK: null,
    
    get_uiThreadKJ: function Js_Controls_ThreadControl_View$get_uiThreadKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiThreadKJ == null) {
            this._uiThreadKJ = $('#' + this.clientId + '_uiThreadK');
        }
        return this._uiThreadKJ;
    },
    
    _uiThreadKJ: null,
    
    get_uiParentObjectK: function Js_Controls_ThreadControl_View$get_uiParentObjectK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiParentObjectK == null) {
            this._uiParentObjectK = document.getElementById(this.clientId + '_uiParentObjectK');
        }
        return this._uiParentObjectK;
    },
    
    _uiParentObjectK: null,
    
    get_uiParentObjectKJ: function Js_Controls_ThreadControl_View$get_uiParentObjectKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiParentObjectKJ == null) {
            this._uiParentObjectKJ = $('#' + this.clientId + '_uiParentObjectK');
        }
        return this._uiParentObjectKJ;
    },
    
    _uiParentObjectKJ: null,
    
    get_uiParentObjectType: function Js_Controls_ThreadControl_View$get_uiParentObjectType() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiParentObjectType == null) {
            this._uiParentObjectType = document.getElementById(this.clientId + '_uiParentObjectType');
        }
        return this._uiParentObjectType;
    },
    
    _uiParentObjectType: null,
    
    get_uiParentObjectTypeJ: function Js_Controls_ThreadControl_View$get_uiParentObjectTypeJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiParentObjectTypeJ == null) {
            this._uiParentObjectTypeJ = $('#' + this.clientId + '_uiParentObjectType');
        }
        return this._uiParentObjectTypeJ;
    },
    
    _uiParentObjectTypeJ: null
}


Js.Controls.ThreadControl.Controller.registerClass('Js.Controls.ThreadControl.Controller');
Js.Controls.ThreadControl.View.registerClass('Js.Controls.ThreadControl.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
