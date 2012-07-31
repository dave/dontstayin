Type.registerNamespace('SpottedScript.Controls.ThreadControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ThreadControl.Controller
SpottedScript.Controls.ThreadControl.Controller = function SpottedScript_Controls_ThreadControl_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.ThreadControl.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.ThreadControl.View">
    /// </field>
    /// <field name="onThreadCreated" type="Sys.EventHandler">
    /// </field>
    /// <field name="onCommentPosted" type="Sys.EventHandler">
    /// </field>
    /// <field name="_duplicateGuid" type="String">
    /// </field>
    this._view = view;
    view.get_commentHtml().get__saveButton().setAttribute('onclick', '');
    $addHandler(view.get_commentHtml().get__saveButton(), 'click', Function.createDelegate(this, this._postCommentClick));
    this._createNewDuplicateGuid();
    if (view.get_addThreadAdvancedCheckBox() != null) {
        $addHandler(view.get_addThreadAdvancedCheckBox(), 'click', Function.createDelegate(this, this._advancedCheckBoxClicked));
    }
    view.get_uiCommentsDisplay()._threadCommentsProvider._onCommentPosted = Function.createDelegate(this, this._commentPosted);
    view.get_uiCommentsDisplay()._threadCommentsProvider.onThreadCreated = Function.createDelegate(this, this._threadCreated);
    view.get_uiCommentsDisplay()._onCommentsDisplayed = Function.createDelegate(this, this._commentsDisplayed);
}
SpottedScript.Controls.ThreadControl.Controller.prototype = {
    _view: null,
    get_uiCommentsDisplay: function SpottedScript_Controls_ThreadControl_Controller$get_uiCommentsDisplay() {
        /// <value type="SpottedScript.Controls.CommentsDisplay.Controller"></value>
        return this._view.get_uiCommentsDisplay();
    },
    onThreadCreated: null,
    onCommentPosted: null,
    _duplicateGuid: null,
    get_currentParentObjectK: function SpottedScript_Controls_ThreadControl_Controller$get_currentParentObjectK() {
        /// <value type="Number" integer="true"></value>
        try {
            return Number.parseInvariant(this._view.get_uiParentObjectK().value);
        }
        catch ($e1) {
            return 0;
        }
    },
    set_currentParentObjectK: function SpottedScript_Controls_ThreadControl_Controller$set_currentParentObjectK(value) {
        /// <value type="Number" integer="true"></value>
        this._view.get_uiParentObjectK().value = value.toString();
        return value;
    },
    get__currentParentObjectType: function SpottedScript_Controls_ThreadControl_Controller$get__currentParentObjectType() {
        /// <value type="Number" integer="true"></value>
        try {
            return Number.parseInvariant(this._view.get_uiParentObjectType().value);
        }
        catch ($e1) {
            return 0;
        }
    },
    _commentsDisplayed: function SpottedScript_Controls_ThreadControl_Controller$_commentsDisplayed(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this._view.get_commentHtml()._clearHtml();
        try {
            if (this._view.get_uiMultiBuddyChooser() != null) {
                this._view.get_uiMultiBuddyChooser()._clear();
            }
        }
        catch ($e1) {
        }
    },
    _commentPosted: function SpottedScript_Controls_ThreadControl_Controller$_commentPosted(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this.onCommentPosted != null) {
            this.onCommentPosted(null, e);
        }
        this._createNewDuplicateGuid();
        this._view.get_commentHtml()._clearHtml();
    },
    _advancedCheckBoxClicked: function SpottedScript_Controls_ThreadControl_Controller$_advancedCheckBoxClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        WhenLoggedIn(Function.createDelegate(this, function() {
            this._view.get_addThreadAdvancedPanel().style.display = (this._view.get_addThreadAdvancedCheckBox().checked) ? '' : 'none';
        }));
    },
    _createNewDuplicateGuid: function SpottedScript_Controls_ThreadControl_Controller$_createNewDuplicateGuid() {
        Spotted.WebServices.Controls.CommentsDisplay.Service.getNewGuid(Function.createDelegate(this, this._getNewGuidSuccess), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, -1);
    },
    _getNewGuidSuccess: function SpottedScript_Controls_ThreadControl_Controller$_getNewGuidSuccess(guid, userContext, methodName) {
        /// <param name="guid" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._duplicateGuid = guid;
    },
    _threadCreated: function SpottedScript_Controls_ThreadControl_Controller$_threadCreated(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this.onThreadCreated != null) {
            this.onThreadCreated(o, e);
        }
    },
    _postCommentClick: function SpottedScript_Controls_ThreadControl_Controller$_postCommentClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        WhenLoggedIn(Function.createDelegate(this, function() {
            this._postCommentClickInner();
        }));
    },
    _postCommentClickInner: function SpottedScript_Controls_ThreadControl_Controller$_postCommentClickInner() {
        var rawCommentHtml = this._view.get_commentHtml().get__rawHtml();
        if (rawCommentHtml.trim().length === 0) {
            return;
        }
        SpottedScript.Misc.showWaitingCursor();
        var inviteUsrOptions = this._view.get_uiMultiBuddyChooser().get_selectedValues();
        if (!this._view.get_addThreadAdvancedCheckBox().checked || this._view.get_addThreadPublicRadioButton().checked || this._view.get_addThreadNewPublicRadioButton().checked) {
            if (this._view.get_uiCommentsDisplay()._threadCommentsProvider.get__threadK() === 0) {
                this._view.get_uiCommentsDisplay()._threadCommentsProvider._createPublicThread(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get__formatting(), this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadNewsCheckBox().checked, inviteUsrOptions);
            }
            else if (this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadNewPublicRadioButton().checked) {
                this._view.get_uiCommentsDisplay()._threadCommentsProvider._createNewPublicThread(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get__formatting(), this._view.get_addThreadNewsCheckBox().checked, inviteUsrOptions);
            }
            else {
                this._view.get_uiCommentsDisplay()._threadCommentsProvider._createReply(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._view.get_uiCommentsDisplay()._threadCommentsProvider.get__threadK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get__formatting(), this._view.get_uiCommentsDisplay()._threadCommentsProvider.get__lastKnownCommentK(), inviteUsrOptions);
            }
        }
        else if (this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadPrivateRadioButton().checked) {
            Spotted.WebServices.Controls.CommentsDisplay.Service.createPrivateThread(this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get__formatting(), inviteUsrOptions, this._view.get_addThreadSealedCheckBox().checked, Function.createDelegate(this, this._createPrivateThreadSuccess), null, null, -1);
        }
        else if (this._view.get_addThreadAdvancedCheckBox().checked && this._view.get_addThreadGroupRadioButton().checked) {
            var groupK = Number.parseInvariant(this._view.get_addThreadGroupDropDown().value);
            Spotted.WebServices.Controls.CommentsDisplay.Service.createNewThreadInGroup(groupK, this.get__currentParentObjectType(), this.get_currentParentObjectK(), this._duplicateGuid, rawCommentHtml, this._view.get_commentHtml().get__formatting(), this._view.get_addThreadNewsCheckBox().checked, inviteUsrOptions, this._view.get_addThreadGroupPrivateCheckBox().checked, Function.createDelegate(this, this._createNewThreadInGroupSuccess), null, null, -1);
        }
    },
    _createPrivateThreadSuccess: function SpottedScript_Controls_ThreadControl_Controller$_createPrivateThreadSuccess(newThreadUrl, userContext, methodName) {
        /// <param name="newThreadUrl" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
        SpottedScript.Misc.redirect(newThreadUrl);
    },
    _createNewThreadInGroupSuccess: function SpottedScript_Controls_ThreadControl_Controller$_createNewThreadInGroupSuccess(newThreadUrl, userContext, methodName) {
        /// <param name="newThreadUrl" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
        SpottedScript.Misc.redirect(newThreadUrl);
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ThreadControl.View
SpottedScript.Controls.ThreadControl.View = function SpottedScript_Controls_ThreadControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.ThreadControl.View.prototype = {
    clientId: null,
    get_uiCommentsDisplay: function SpottedScript_Controls_ThreadControl_View$get_uiCommentsDisplay() {
        /// <value type="SpottedScript.Controls.CommentsDisplay.Controller"></value>
        return eval(this.clientId + '_uiCommentsDisplayController');
    },
    get_postCommentPanel: function SpottedScript_Controls_ThreadControl_View$get_postCommentPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PostCommentPanel');
    },
    get_h1: function SpottedScript_Controls_ThreadControl_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_threadWatchButtonHolder: function SpottedScript_Controls_ThreadControl_View$get_threadWatchButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadWatchButtonHolder');
    },
    get_inlineScript3: function SpottedScript_Controls_ThreadControl_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript3');
    },
    get_threadFavouriteButtonP: function SpottedScript_Controls_ThreadControl_View$get_threadFavouriteButtonP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadFavouriteButtonP');
    },
    get_threadFavouriteButtonHolder: function SpottedScript_Controls_ThreadControl_View$get_threadFavouriteButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadFavouriteButtonHolder');
    },
    get_inlineScript2: function SpottedScript_Controls_ThreadControl_View$get_inlineScript2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript2');
    },
    get_requiredFieldValidator1: function SpottedScript_Controls_ThreadControl_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_commentGroupMemberPanel: function SpottedScript_Controls_ThreadControl_View$get_commentGroupMemberPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentGroupMemberPanel');
    },
    get_commentGroupMemberAnchor: function SpottedScript_Controls_ThreadControl_View$get_commentGroupMemberAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentGroupMemberAnchor');
    },
    get_commentLoginPanel: function SpottedScript_Controls_ThreadControl_View$get_commentLoginPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentLoginPanel');
    },
    get_commentEmailVerifyPanel: function SpottedScript_Controls_ThreadControl_View$get_commentEmailVerifyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentEmailVerifyPanel');
    },
    get_commentHtml: function SpottedScript_Controls_ThreadControl_View$get_commentHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_CommentHtmlController');
    },
    get_addThreadAdvancedCheckBoxP: function SpottedScript_Controls_ThreadControl_View$get_addThreadAdvancedCheckBoxP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadAdvancedCheckBoxP');
    },
    get_addThreadAdvancedCheckBox: function SpottedScript_Controls_ThreadControl_View$get_addThreadAdvancedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadAdvancedCheckBox');
    },
    get_addThreadAdvancedPanel: function SpottedScript_Controls_ThreadControl_View$get_addThreadAdvancedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadAdvancedPanel');
    },
    get_addThreadPublicRadioButtonSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadPublicRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPublicRadioButtonSpan');
    },
    get_addThreadPublicRadioButton: function SpottedScript_Controls_ThreadControl_View$get_addThreadPublicRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPublicRadioButton');
    },
    get_addThreadNewPublicRadioButtonSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadNewPublicRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNewPublicRadioButtonSpan');
    },
    get_addThreadNewPublicRadioButton: function SpottedScript_Controls_ThreadControl_View$get_addThreadNewPublicRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNewPublicRadioButton');
    },
    get_addThreadPrivateRadioButtonSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadPrivateRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPrivateRadioButtonSpan');
    },
    get_addThreadPrivateRadioButton: function SpottedScript_Controls_ThreadControl_View$get_addThreadPrivateRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadPrivateRadioButton');
    },
    get_addThreadGroupRadioButtonSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadGroupRadioButtonSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupRadioButtonSpan');
    },
    get_addThreadGroupRadioButton: function SpottedScript_Controls_ThreadControl_View$get_addThreadGroupRadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupRadioButton');
    },
    get_addThreadGroupDropDown: function SpottedScript_Controls_ThreadControl_View$get_addThreadGroupDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupDropDown');
    },
    get_addThreadGroupPrivateCheckBoxSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadGroupPrivateCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBoxSpan');
    },
    get_addThreadGroupPrivateCheckBox: function SpottedScript_Controls_ThreadControl_View$get_addThreadGroupPrivateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadGroupPrivateCheckBox');
    },
    get_addThreadNewsCheckBoxSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadNewsCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNewsCheckBoxSpan');
    },
    get_addThreadNewsCheckBox: function SpottedScript_Controls_ThreadControl_View$get_addThreadNewsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadNewsCheckBox');
    },
    get_addThreadSealedCheckBoxSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadSealedCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadSealedCheckBoxSpan');
    },
    get_addThreadSealedCheckBox: function SpottedScript_Controls_ThreadControl_View$get_addThreadSealedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadSealedCheckBox');
    },
    get_addThreadInviteCheckBoxSpan: function SpottedScript_Controls_ThreadControl_View$get_addThreadInviteCheckBoxSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadInviteCheckBoxSpan');
    },
    get_addThreadInviteCheckBox: function SpottedScript_Controls_ThreadControl_View$get_addThreadInviteCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadInviteCheckBox');
    },
    get_addThreadInvitePanel: function SpottedScript_Controls_ThreadControl_View$get_addThreadInvitePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadInvitePanel');
    },
    get_uiMultiBuddyChooser: function SpottedScript_Controls_ThreadControl_View$get_uiMultiBuddyChooser() {
        /// <value type="SpottedScript.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiMultiBuddyChooserController');
    },
    get_inlineScript1: function SpottedScript_Controls_ThreadControl_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript1');
    },
    get_uiThreadK: function SpottedScript_Controls_ThreadControl_View$get_uiThreadK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiThreadK');
    },
    get_uiParentObjectK: function SpottedScript_Controls_ThreadControl_View$get_uiParentObjectK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiParentObjectK');
    },
    get_uiParentObjectType: function SpottedScript_Controls_ThreadControl_View$get_uiParentObjectType() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiParentObjectType');
    }
}
SpottedScript.Controls.ThreadControl.Controller.registerClass('SpottedScript.Controls.ThreadControl.Controller');
SpottedScript.Controls.ThreadControl.View.registerClass('SpottedScript.Controls.ThreadControl.View');
