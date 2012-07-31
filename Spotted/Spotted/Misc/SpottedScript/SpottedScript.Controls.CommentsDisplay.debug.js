Type.registerNamespace('SpottedScript.Controls.CommentsDisplay');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.CommentsDisplay.CommentStub
SpottedScript.Controls.CommentsDisplay.CommentStub = function SpottedScript_Controls_CommentsDisplay_CommentStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="usrUrl" type="String">
    /// </field>
    /// <field name="usrK" type="Number" integer="true">
    /// </field>
    /// <field name="usrRollover" type="String">
    /// </field>
    /// <field name="usrPicSrc" type="String">
    /// </field>
    /// <field name="usrName" type="String">
    /// </field>
    /// <field name="isNew" type="Boolean">
    /// </field>
    /// <field name="html" type="String">
    /// </field>
    /// <field name="script" type="String">
    /// </field>
    /// <field name="lolHtml" type="String">
    /// </field>
    /// <field name="haveAlreadyLold" type="Boolean">
    /// </field>
    /// <field name="friendlyTimeNoCaps" type="String">
    /// </field>
    /// <field name="editLinkVisible" type="Boolean">
    /// </field>
    /// <field name="editedHtml" type="String">
    /// </field>
    /// <field name="deleteLinkVisible" type="Boolean">
    /// </field>
    /// <field name="deleteLinkOnClickConfirmText" type="String">
    /// </field>
    /// <field name="threadK" type="Number" integer="true">
    /// </field>
}
SpottedScript.Controls.CommentsDisplay.CommentStub.prototype = {
    k: 0,
    usrUrl: null,
    usrK: 0,
    usrRollover: null,
    usrPicSrc: null,
    usrName: null,
    isNew: false,
    html: null,
    script: null,
    lolHtml: null,
    haveAlreadyLold: false,
    friendlyTimeNoCaps: null,
    editLinkVisible: false,
    editedHtml: null,
    deleteLinkVisible: false,
    deleteLinkOnClickConfirmText: null,
    threadK: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.CommentsDisplay.CommentResult
SpottedScript.Controls.CommentsDisplay.CommentResult = function SpottedScript_Controls_CommentsDisplay_CommentResult() {
    /// <field name="initialComment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
    /// </field>
    /// <field name="comments" type="Array" elementType="CommentStub">
    /// </field>
    /// <field name="lastPage" type="Number" integer="true">
    /// </field>
    /// <field name="currentPage" type="Number" integer="true">
    /// </field>
    /// <field name="firstUnreadPage" type="Number" integer="true">
    /// </field>
    /// <field name="viewComments" type="Number" integer="true">
    /// </field>
    /// <field name="totalComments" type="Number" integer="true">
    /// </field>
}
SpottedScript.Controls.CommentsDisplay.CommentResult.prototype = {
    initialComment: null,
    comments: null,
    lastPage: 0,
    currentPage: 0,
    firstUnreadPage: 0,
    viewComments: 0,
    totalComments: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.CommentsDisplay.Controller
SpottedScript.Controls.CommentsDisplay.Controller = function SpottedScript_Controls_CommentsDisplay_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.CommentsDisplay.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.CommentsDisplay.View">
    /// </field>
    /// <field name="_threadCommentsProvider" type="SpottedScript.Controls.CommentsDisplay._threadCommentsProvider">
    /// </field>
    /// <field name="_uiCommentsCount" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPaging1Div" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCommentsDiv" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPaging2Div" type="Object" domElement="true">
    /// </field>
    /// <field name="_onCommentsDisplayed" type="Sys.EventHandler">
    /// </field>
    /// <field name="onThreadDeleted" type="Sys.EventHandler">
    /// </field>
    this._view = view;
    this._threadCommentsProvider = new SpottedScript.Controls.CommentsDisplay._threadCommentsProvider(this.get__commentsPerPage());
    this._threadCommentsProvider._commentsAnchorName = view.get_uiCommentsAnchor().getAttribute('name');
    this._threadCommentsProvider.onLoaded = Function.createDelegate(this, this._display);
    this._hideCommentsPanelServerSide();
    this._uiCommentsCount = document.createElement('center');
    view.get_uiCommentsPanelClientSide().appendChild(this._uiCommentsCount);
    this._uiPaging1Div = document.createElement('div');
    this._uiPaging1Div.style.textAlign = 'right';
    view.get_uiCommentsPanelClientSide().appendChild(this._uiPaging1Div);
    this._uiCommentsDiv = document.createElement('div');
    view.get_uiCommentsPanelClientSide().appendChild(this._uiCommentsDiv);
    this._uiPaging2Div = document.createElement('div');
    this._uiPaging2Div.style.textAlign = 'right';
    view.get_uiCommentsPanelClientSide().appendChild(this._uiPaging2Div);
}
SpottedScript.Controls.CommentsDisplay.Controller._removeAllChildren = function SpottedScript_Controls_CommentsDisplay_Controller$_removeAllChildren(domElement) {
    /// <param name="domElement" type="Object" domElement="true">
    /// </param>
    for (var childNodeIndex = 0; childNodeIndex < domElement.childNodes.length; childNodeIndex++) {
        domElement.removeChild(domElement.childNodes[childNodeIndex]);
    }
    domElement.innerHTML = '';
}
SpottedScript.Controls.CommentsDisplay.Controller.prototype = {
    _view: null,
    _threadCommentsProvider: null,
    get__uniqueID: function SpottedScript_Controls_CommentsDisplay_Controller$get__uniqueID() {
        /// <value type="String"></value>
        return this._view.get_uiClientID().value + '_controls_';
    },
    _uiCommentsCount: null,
    _uiPaging1Div: null,
    _uiCommentsDiv: null,
    _uiPaging2Div: null,
    get__commentsPerPage: function SpottedScript_Controls_CommentsDisplay_Controller$get__commentsPerPage() {
        /// <value type="Number" integer="true"></value>
        return Number.parseInvariant(this._view.get_uiCommentsPerPage().value);
    },
    _hideCommentsPanelServerSide: function SpottedScript_Controls_CommentsDisplay_Controller$_hideCommentsPanelServerSide() {
        if (this._view.get_uiInitialCommentDataList() != null) {
            this._view.get_uiInitialCommentDataList().style.display = 'none';
        }
        this._view.get_uiCommentsPanelServerSide().style.display = 'none';
        this._view.get_uiCommentsPanelClientSide().style.display = '';
    },
    setCommentsCount: function SpottedScript_Controls_CommentsDisplay_Controller$setCommentsCount(commentsCount) {
        /// <param name="commentsCount" type="Number" integer="true">
        /// </param>
        if (commentsCount === 0) {
            this._uiCommentsCount.innerHTML = '<p>No comments</p>';
            this._view.get_uiInitialCommentPanel().style.display = 'none';
            this._view.get_uiCommentsPanel().style.display = 'none';
        }
        else {
            this._uiCommentsCount.innerHTML = (commentsCount === 1) ? '<p>1 comment loading...</p>' : '<p>' + commentsCount + ' comments loading...</p>';
            this._view.get_uiCommentsPanelClientSide().style.display = '';
            this._view.get_uiCommentsPanel().style.display = '';
        }
        this._setCommentsAreaVisible(false);
        this._uiCommentsCount.style.display = '';
    },
    _setCommentsAreaVisible: function SpottedScript_Controls_CommentsDisplay_Controller$_setCommentsAreaVisible(vis) {
        /// <param name="vis" type="Boolean">
        /// </param>
        var display = (vis) ? '' : 'none';
        var displayPaging = (vis && this._threadCommentsProvider.get__lastPage() > 1) ? '' : 'none';
        this._uiPaging1Div.style.display = displayPaging;
        this._uiCommentsDiv.style.display = display;
        this._uiPaging2Div.style.display = displayPaging;
        if (vis) {
            this._view.get_uiCommentsPanel().style.display = '';
        }
    },
    _hideCommentsCount: function SpottedScript_Controls_CommentsDisplay_Controller$_hideCommentsCount() {
        this._uiCommentsCount.style.display = 'none';
    },
    showComments: function SpottedScript_Controls_CommentsDisplay_Controller$showComments(threadK, pageNumber) {
        /// <param name="threadK" type="Number" integer="true">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        this._threadCommentsProvider.set__threadK(threadK);
        this._threadCommentsProvider.set__pageNumber(pageNumber);
        this._threadCommentsProvider._loadThreadComments();
    },
    _onCommentsDisplayed: null,
    _display: function SpottedScript_Controls_CommentsDisplay_Controller$_display(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (this._threadCommentsProvider.get__totalComments() > 0) {
            this._displayAll();
        }
        else {
            this._view.get_uiCommentsPanel().style.display = 'none';
        }
        if (this._onCommentsDisplayed != null) {
            this._onCommentsDisplayed(this, e);
        }
    },
    _displayAll: function SpottedScript_Controls_CommentsDisplay_Controller$_displayAll() {
        this._displayInitialComment();
        this._displayPaging();
        this._view.get_uiCommentsPanelClientSide().style.display = '';
        this._displayComments();
        this._hideCommentsCount();
    },
    _displayComments: function SpottedScript_Controls_CommentsDisplay_Controller$_displayComments() {
        SpottedScript.Controls.CommentsDisplay.Controller._removeAllChildren(this._uiCommentsDiv);
        this._setCommentsAreaVisible(true);
        var comments = this._threadCommentsProvider.get__comments();
        for (var commentIndex = 0; commentIndex < comments.length; commentIndex++) {
            this._addComment(comments[commentIndex]);
        }
        for (var commentIndex = 0; commentIndex < comments.length; commentIndex++) {
            try {
                eval(comments[commentIndex].script);
            }
            catch ($e1) {
            }
        }
    },
    _addComment: function SpottedScript_Controls_CommentsDisplay_Controller$_addComment(comment) {
        /// <param name="comment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        this._uiCommentsDiv.appendChild(this._createAnchor(comment));
        this._uiCommentsDiv.appendChild(this._createComment(comment));
    },
    _displayPaging: function SpottedScript_Controls_CommentsDisplay_Controller$_displayPaging() {
        if (this._threadCommentsProvider.get__lastPage() === 1) {
            this._uiPaging1Div.style.display = 'none';
            this._uiPaging2Div.style.display = 'none';
            return;
        }
        this._uiPaging1Div.style.display = '';
        SpottedScript.Controls.CommentsDisplay.Controller._removeAllChildren(this._uiPaging1Div);
        this._uiPaging1Div.appendChild(this._threadCommentsProvider._createPrevNextPaging());
        this._uiPaging1Div.appendChild(this._threadCommentsProvider._createNumberedPaging());
        this._uiPaging1Div.style.display = '';
        SpottedScript.Controls.CommentsDisplay.Controller._removeAllChildren(this._uiPaging2Div);
        this._uiPaging2Div.appendChild(this._threadCommentsProvider._createNumberedPaging());
        this._uiPaging2Div.appendChild(this._threadCommentsProvider._createPrevNextPaging());
    },
    _displayInitialComment: function SpottedScript_Controls_CommentsDisplay_Controller$_displayInitialComment() {
        if (this._threadCommentsProvider.get__initialComment() != null && this._threadCommentsProvider.get__pageNumber() > 1) {
            this._view.get_uiInitialCommentPanel().style.display = '';
            this._view.get_uiInitialComment().style.display = '';
            for (var childNodeIndex = 0; childNodeIndex < this._view.get_uiInitialComment().childNodes.length; childNodeIndex++) {
                this._view.get_uiInitialComment().removeChild(this._view.get_uiInitialComment().childNodes[childNodeIndex]);
            }
            this._view.get_uiInitialComment().appendChild(this._createComment(this._threadCommentsProvider.get__initialComment()));
            this._setCommentsSubject('Replies');
        }
        else {
            this._view.get_uiInitialCommentPanel().style.display = 'none';
            this._setCommentsSubject('Comments');
        }
    },
    _setCommentsSubject: function SpottedScript_Controls_CommentsDisplay_Controller$_setCommentsSubject(subject) {
        /// <param name="subject" type="String">
        /// </param>
        this._view.get_commentsSubjectH1().childNodes[0].innerHTML = subject;
    },
    _updateLols: function SpottedScript_Controls_CommentsDisplay_Controller$_updateLols(commentK, newLolHtml) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <param name="newLolHtml" type="String">
        /// </param>
        document.getElementById(this._getLolAnchorControlID(commentK)).style.display = 'none';
        (document.getElementById(this._getLolSpanControlID(commentK))).innerHTML = newLolHtml;
    },
    _getCommentControlID: function SpottedScript_Controls_CommentsDisplay_Controller$_getCommentControlID(commentK) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this.get__uniqueID() + 'C' + commentK;
    },
    _getLolAnchorControlID: function SpottedScript_Controls_CommentsDisplay_Controller$_getLolAnchorControlID(commentK) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this.get__uniqueID() + 'L' + commentK;
    },
    _getLolSpanControlID: function SpottedScript_Controls_CommentsDisplay_Controller$_getLolSpanControlID(commentK) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this.get__uniqueID() + 'H' + commentK;
    },
    _createAnchor: function SpottedScript_Controls_CommentsDisplay_Controller$_createAnchor(comment) {
        /// <param name="comment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var anchor = document.createElement('a');
        anchor.id = 'Anchor-CommentK-' + comment.k;
        return anchor;
    },
    _createComment: function SpottedScript_Controls_CommentsDisplay_Controller$_createComment(comment) {
        /// <param name="comment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var div = document.createElement('div');
        div.className = 'CommentOuter ClearAfter';
        div.appendChild(this._createCommentLeft(comment));
        div.appendChild(this._createCommentBody(comment));
        return div;
    },
    _createCommentLeft: function SpottedScript_Controls_CommentsDisplay_Controller$_createCommentLeft(comment) {
        /// <param name="comment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var div = document.createElement('div');
        div.className = 'CommentLeft';
        var aimg = document.createElement('a');
        aimg.href = comment.usrUrl;
        this._createMouseOverAndOut(aimg, comment.usrRollover, 'htm();');
        var img = document.createElement('img');
        img.src = comment.usrPicSrc;
        img.style.width = '100px';
        img.style.height = '100px';
        img.style.marginBottom = '2px';
        img.style.marginTop = '0px';
        img.className = 'BorderBlack All Block';
        aimg.appendChild(img);
        div.appendChild(aimg);
        var aname = document.createElement('a');
        aname.href = comment.usrUrl;
        aname.innerHTML = comment.usrName;
        div.appendChild(aname);
        return div;
    },
    _createCommentBody: function SpottedScript_Controls_CommentsDisplay_Controller$_createCommentBody(comment) {
        /// <param name="comment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var div = document.createElement('div');
        div.className = 'CommentBody';
        div.innerHTML = ((comment.isNew) ? '<a name=\"Unread\"></a><span class=\"Unread\">NEW</span> ' : '') + comment.html;
        var admin = document.createElement('div');
        admin.className = 'CommentAdmin';
        var small = document.createElement('small');
        var lolSpan = document.createElement('span');
        lolSpan.className = 'CleanLinks';
        lolSpan.innerHTML = comment.lolHtml;
        lolSpan.id = this._getLolSpanControlID(comment.k);
        small.appendChild(lolSpan);
        if (!comment.haveAlreadyLold && Boolean.parse(this._view.get_uiUsrIsLoggedIn().value)) {
            small.appendChild(document.createTextNode(' '));
            var lolAnchorSpan = document.createElement('div');
            var lolAnchor = document.createElement('a');
            lolAnchor.href = '#';
            lolAnchor.innerHTML = 'This made me laugh!';
            lolAnchor.setAttribute(SpottedScript.Controls.CommentsDisplay._properties._commentK, comment.k);
            $addHandler(lolAnchor, 'click', Function.createDelegate(this, this._lolClick));
            lolAnchorSpan.appendChild(lolAnchor);
            lolAnchorSpan.id = this._getLolAnchorControlID(comment.k);
            small.appendChild(lolAnchorSpan);
        }
        small.appendChild(document.createTextNode(' '));
        var reply = document.createElement('a');
        reply.href = '#PostComment';
        reply.innerHTML = 'Reply';
        small.appendChild(reply);
        small.appendChild(document.createTextNode(' '));
        var quote = document.createElement('a');
        quote.innerHTML = 'Quote';
        quote.href = '#';
        $addHandler(quote, 'mousedown', Function.createDelegate(this, this._quoteMouseDown));
        $addHandler(quote, 'click', Function.createDelegate(this, this._quoteClick));
        quote.setAttribute(SpottedScript.Controls.CommentsDisplay._properties._usrK, comment.usrK);
        small.appendChild(quote);
        if (comment.editLinkVisible) {
            small.appendChild(document.createTextNode(' '));
            var edit = document.createElement('a');
            edit.innerHTML = 'Edit';
            edit.href = '/pages/commentedit/k-' + comment.k;
            small.appendChild(edit);
        }
        if (comment.deleteLinkVisible) {
            small.appendChild(document.createTextNode(' '));
            var deleteAnchor = document.createElement('a');
            deleteAnchor.innerHTML = 'Delete';
            deleteAnchor.href = '#';
            $addHandler(deleteAnchor, 'click', Function.createDelegate(this, this._deleteClick));
            deleteAnchor.setAttribute(SpottedScript.Controls.CommentsDisplay._properties._deleteConfirmText, comment.deleteLinkOnClickConfirmText);
            deleteAnchor.setAttribute(SpottedScript.Controls.CommentsDisplay._properties._commentK, comment.k);
            small.appendChild(deleteAnchor);
        }
        small.appendChild(document.createElement('br'));
        var postDetails = document.createElement('span');
        this._createMouseOverAndOut(postDetails, 'stt(\'' + comment.k + '\');', 'htm();');
        postDetails.innerHTML = 'Posted ' + comment.friendlyTimeNoCaps;
        small.appendChild(postDetails);
        if (comment.editedHtml != null && comment.editedHtml.length > 0) {
            var edited = document.createElement('span');
            edited.innerHTML = comment.editedHtml;
            small.appendChild(edited);
        }
        admin.appendChild(small);
        div.appendChild(admin);
        return div;
    },
    _createMouseOverAndOut: function SpottedScript_Controls_CommentsDisplay_Controller$_createMouseOverAndOut(domElement, mouseover, mouseout) {
        /// <param name="domElement" type="Object" domElement="true">
        /// </param>
        /// <param name="mouseover" type="String">
        /// </param>
        /// <param name="mouseout" type="String">
        /// </param>
        domElement.setAttribute('mouseover', mouseover);
        $addHandler(domElement, 'mouseover', Function.createDelegate(this, this._mouseOver));
        domElement.setAttribute('mouseout', mouseout);
        $addHandler(domElement, 'mouseout', Function.createDelegate(this, this._mouseOut));
    },
    _mouseOver: function SpottedScript_Controls_CommentsDisplay_Controller$_mouseOver(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        eval(e.target.getAttribute('mouseover'));
    },
    _mouseOut: function SpottedScript_Controls_CommentsDisplay_Controller$_mouseOut(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        eval('htm();');
    },
    _lolClick: function SpottedScript_Controls_CommentsDisplay_Controller$_lolClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        var commentK = e.target.getAttribute(SpottedScript.Controls.CommentsDisplay._properties._commentK);
        Spotted.WebServices.Controls.CommentsDisplay.Service.lolAtComment(commentK, Function.createDelegate(this, this._lolAtCommentSuccess), Function.createDelegate(null, Utils.Trace.webServiceFailure), commentK, -1);
    },
    _lolAtCommentSuccess: function SpottedScript_Controls_CommentsDisplay_Controller$_lolAtCommentSuccess(newLolHtml, commentK, methodName) {
        /// <param name="newLolHtml" type="String">
        /// </param>
        /// <param name="commentK" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._updateLols(commentK, newLolHtml);
    },
    _quoteMouseDown: function SpottedScript_Controls_CommentsDisplay_Controller$_quoteMouseDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        var usrK = e.target.getAttribute(SpottedScript.Controls.CommentsDisplay._properties._usrK);
        eval('QuoteNow(' + usrK.toString() + ');');
    },
    _quoteClick: function SpottedScript_Controls_CommentsDisplay_Controller$_quoteClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        eval('FocusNow();');
    },
    _deleteClick: function SpottedScript_Controls_CommentsDisplay_Controller$_deleteClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        SpottedScript.Misc.showWaitingCursor();
        if (confirm(e.target.getAttribute(SpottedScript.Controls.CommentsDisplay._properties._deleteConfirmText))) {
            var commentK = e.target.getAttribute(SpottedScript.Controls.CommentsDisplay._properties._commentK);
            Spotted.WebServices.Controls.CommentsDisplay.Service.deleteComment(commentK, Function.createDelegate(this, this._deleteCommentSuccess), Function.createDelegate(this, this._deleteCommentFailure), commentK, -1);
        }
    },
    onThreadDeleted: null,
    _deleteCommentSuccess: function SpottedScript_Controls_CommentsDisplay_Controller$_deleteCommentSuccess(commentDeleted, commentK, methodName) {
        /// <param name="commentDeleted" type="Boolean">
        /// </param>
        /// <param name="commentK" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
        if (commentDeleted) {
            if (this._threadCommentsProvider.get__totalComments() === 1) {
                if (this.onThreadDeleted != null) {
                    this.onThreadDeleted(this, new SpottedScript.IntEventArgs(this._threadCommentsProvider.get__threadK()));
                }
                this._threadCommentsProvider.set__threadK(0);
                this._view.get_uiCommentsPanel().style.display = 'none';
            }
            this._threadCommentsProvider._decrementCurrentThreadTotalComments();
            this._threadCommentsProvider._reloadComments();
        }
    },
    _deleteCommentFailure: function SpottedScript_Controls_CommentsDisplay_Controller$_deleteCommentFailure(error, context, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="context" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.CommentsDisplay._properties
SpottedScript.Controls.CommentsDisplay._properties = function SpottedScript_Controls_CommentsDisplay__properties() {
    /// <field name="_deleteConfirmText" type="String" static="true">
    /// </field>
    /// <field name="_commentK" type="String" static="true">
    /// </field>
    /// <field name="_usrK" type="String" static="true">
    /// </field>
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.CommentsDisplay._threadCommentsProvider
SpottedScript.Controls.CommentsDisplay._threadCommentsProvider = function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider(commentsPerPage) {
    /// <param name="commentsPerPage" type="Number" integer="true">
    /// </param>
    /// <field name="_commentsPerPage" type="Number" integer="true">
    /// </field>
    /// <field name="_threadK" type="Number" integer="true">
    /// </field>
    /// <field name="_pageNumber" type="Number" integer="true">
    /// </field>
    /// <field name="_initialComments" type="Array">
    /// </field>
    /// <field name="_comments" type="Array">
    /// </field>
    /// <field name="_lastPage" type="Array">
    /// </field>
    /// <field name="_lastKnownCommentK" type="Array">
    /// </field>
    /// <field name="_firstUnreadPage" type="Array">
    /// </field>
    /// <field name="_viewComments" type="Array">
    /// </field>
    /// <field name="_totalComments" type="Array">
    /// </field>
    /// <field name="onLoaded" type="Sys.EventHandler">
    /// </field>
    /// <field name="onThreadCreated" type="Sys.EventHandler">
    /// </field>
    /// <field name="_commentsAnchorName" type="String">
    /// </field>
    /// <field name="_onCommentPosted" type="Sys.EventHandler">
    /// </field>
    this._commentsPerPage = commentsPerPage;
    this._comments = [];
    this._initialComments = [];
    this._lastPage = [];
    this._firstUnreadPage = [];
    this._viewComments = [];
    this._totalComments = [];
    this._lastKnownCommentK = [];
}
SpottedScript.Controls.CommentsDisplay._threadCommentsProvider.prototype = {
    _commentsPerPage: 0,
    _threadK: 0,
    get__threadK: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__threadK() {
        /// <value type="Number" integer="true"></value>
        return this._threadK;
    },
    set__threadK: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__threadK(value) {
        /// <value type="Number" integer="true"></value>
        this._threadK = value;
        return value;
    },
    _pageNumber: 0,
    get__pageNumber: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__pageNumber() {
        /// <value type="Number" integer="true"></value>
        return this._pageNumber;
    },
    set__pageNumber: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__pageNumber(value) {
        /// <value type="Number" integer="true"></value>
        this._pageNumber = value;
        return value;
    },
    _initialComments: null,
    get__initialComment: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__initialComment() {
        /// <value type="SpottedScript.Controls.CommentsDisplay.CommentStub"></value>
        return this._initialComments[this._threadK];
    },
    set__initialComment: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__initialComment(value) {
        /// <value type="SpottedScript.Controls.CommentsDisplay.CommentStub"></value>
        this._initialComments[this._threadK] = value;
        return value;
    },
    _comments: null,
    get__comments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__comments() {
        /// <value type="Array" elementType="CommentStub"></value>
        return this._getCommentsByThreadAndPage(this._threadK, this._pageNumber);
    },
    set__comments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__comments(value) {
        /// <value type="Array" elementType="CommentStub"></value>
        if (this._comments[this._threadK] == null) {
            this._comments[this._threadK] = [];
        }
        (this._comments[this._threadK])[this._pageNumber] = value;
        return value;
    },
    _getCommentsByThreadAndPage: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_getCommentsByThreadAndPage(threadK, pageNumber) {
        /// <param name="threadK" type="Number" integer="true">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        /// <returns type="Array" elementType="CommentStub"></returns>
        if (this._comments[threadK] == null) {
            return null;
        }
        if ((this._comments[threadK])[pageNumber] == null) {
            return null;
        }
        return (this._comments[threadK])[pageNumber];
    },
    _lastPage: null,
    get__lastPage: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__lastPage() {
        /// <value type="Number" integer="true"></value>
        return (this._lastPage[this._threadK] != null) ? this._lastPage[this._threadK] : 0;
    },
    set__lastPage: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__lastPage(value) {
        /// <value type="Number" integer="true"></value>
        this._lastPage[this._threadK] = value;
        return value;
    },
    _lastKnownCommentK: null,
    get__lastKnownCommentK: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__lastKnownCommentK() {
        /// <value type="Number" integer="true"></value>
        return (this._lastKnownCommentK[this._threadK] != null) ? this._lastKnownCommentK[this._threadK] : 0;
    },
    set__lastKnownCommentK: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__lastKnownCommentK(value) {
        /// <value type="Number" integer="true"></value>
        this._lastKnownCommentK[this._threadK] = value;
        return value;
    },
    _firstUnreadPage: null,
    get__firstUnreadPage: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__firstUnreadPage() {
        /// <value type="Number" integer="true"></value>
        return (this._firstUnreadPage[this._threadK] != null) ? this._firstUnreadPage[this._threadK] : 0;
    },
    set__firstUnreadPage: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__firstUnreadPage(value) {
        /// <value type="Number" integer="true"></value>
        this._firstUnreadPage[this._threadK] = value;
        return value;
    },
    _viewComments: null,
    get__viewComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__viewComments() {
        /// <value type="Number" integer="true"></value>
        return (this._viewComments[this._threadK] != null) ? this._viewComments[this._threadK] : 0;
    },
    set__viewComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__viewComments(value) {
        /// <value type="Number" integer="true"></value>
        this._viewComments[this._threadK] = value;
        return value;
    },
    _totalComments: null,
    get__totalComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$get__totalComments() {
        /// <value type="Number" integer="true"></value>
        return (this._totalComments[this._threadK] != null) ? this._totalComments[this._threadK] : 0;
    },
    set__totalComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$set__totalComments(value) {
        /// <value type="Number" integer="true"></value>
        this._totalComments[this._threadK] = value;
        return value;
    },
    onLoaded: null,
    onThreadCreated: null,
    _appendComment: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_appendComment(newComment) {
        /// <param name="newComment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        if (this.get__pageNumber() !== this.get__lastPage()) {
            this.set__pageNumber(this.get__lastPage());
        }
        if (this._threadK === 0) {
            this._threadK = newComment.threadK;
        }
        if (this.get__comments() == null) {
            this.set__comments([ newComment ]);
        }
        else if (this.get__comments().length === this._commentsPerPage) {
            this._pageNumber++;
            this.set__lastPage(this.get__lastPage() + 1) - 1;
            this.set__comments([ newComment ]);
        }
        else {
            this.get__comments()[this.get__comments().length] = newComment;
        }
    },
    _appendComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_appendComments(newComments) {
        /// <param name="newComments" type="Array" elementType="CommentStub">
        /// </param>
        for (var i = 0; i < newComments.length; i++) {
            this._appendComment(newComments[i]);
        }
    },
    _loadThreadComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_loadThreadComments() {
        if (this._threadK > 0) {
            if (this.get__comments() == null) {
                this._loadThreadCommentsViaWebRequest();
            }
            else {
                this._loaded();
            }
        }
    },
    _reloadThreadComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_reloadThreadComments(threadK, pageNumber) {
        /// <param name="threadK" type="Number" integer="true">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        this._threadK = threadK;
        this._pageNumber = pageNumber;
        this._loadThreadCommentsViaWebRequest();
    },
    _loadThreadCommentsViaWebRequest: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_loadThreadCommentsViaWebRequest() {
        Spotted.WebServices.Controls.CommentsDisplay.Service.getThreadComments(this._threadK, this._pageNumber, ((this.get__initialComment() != null) ? true : false) && (this.get__lastPage() > 0), Function.createDelegate(this, this._getThreadCommentsSuccess), Function.createDelegate(this, this._getThreadCommentsFailure), null, -1);
    },
    _getThreadCommentsSuccess: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_getThreadCommentsSuccess(result, userContext, methodName) {
        /// <param name="result" type="SpottedScript.Controls.CommentsDisplay.CommentResult">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._pageNumber = result.currentPage;
        this.set__comments(result.comments);
        if (this.get__initialComment() == null) {
            this.set__initialComment((result.initialComment != null) ? result.initialComment : result.comments[0]);
        }
        if (this.get__lastPage() === 0) {
            this.set__lastPage(result.lastPage);
        }
        if (this.get__firstUnreadPage() === 0) {
            this.set__firstUnreadPage(result.firstUnreadPage);
        }
        if (this.get__totalComments() === 0) {
            this.set__totalComments(result.totalComments);
        }
        if (this.get__viewComments() === 0) {
            this.set__viewComments(result.viewComments);
        }
        this._updateLastKnownCommentK(result.comments[result.comments.length - 1].k);
        this._loaded();
        Spotted.WebServices.Controls.CommentsDisplay.Service.setThreadUsr(this._threadK, this._pageNumber, null, null, null, -1);
    },
    _getThreadCommentsFailure: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_getThreadCommentsFailure(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._pageNumber = 0;
        this.set__comments(null);
        this.set__firstUnreadPage(0);
        this.set__initialComment(null);
        this.set__lastKnownCommentK(0);
        this.set__lastPage(0);
        this.set__totalComments(0);
        this.set__viewComments(0);
        this._loaded();
    },
    _loaded: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_loaded() {
        if (this.onLoaded != null) {
            this.onLoaded(this, Sys.EventArgs.Empty);
        }
    },
    _commentsAnchorName: null,
    _prevPageClick: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_prevPageClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._refreshBanners();
        this._pageNumber--;
        this._loadThreadComments();
        SpottedScript.Misc.redirectToAnchor(this._commentsAnchorName);
    },
    _refreshBanners: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_refreshBanners() {
        SpottedScript.Controls.Banners.Generator.Controller.refreshAllBanners();
    },
    _nextPageClick: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_nextPageClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._refreshBanners();
        this._pageNumber++;
        this._loadThreadComments();
        SpottedScript.Misc.redirectToAnchor(this._commentsAnchorName);
    },
    _pageClick: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_pageClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._refreshBanners();
        this._pageNumber = e.target.getAttribute('pagenumber');
        this._loadThreadComments();
        SpottedScript.Misc.redirectToAnchor(this._commentsAnchorName);
    },
    _createPrevNextPaging: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createPrevNextPaging() {
        /// <returns type="Object" domElement="true"></returns>
        var pagingDiv = document.createElement('p');
        pagingDiv.style.textAlign = 'right';
        var prevPage = document.createElement('a');
        var bckImg = document.createElement('img');
        bckImg.src = '/gfx/icon-back-12.png';
        bckImg.style.border = '0';
        bckImg.style.verticalAlign = 'middle';
        prevPage.appendChild(bckImg);
        var prevText = document.createElement('span');
        prevText.innerHTML = 'prev page';
        prevPage.appendChild(prevText);
        if (this._pageNumber > 1 && this.get__lastPage() > 1) {
            prevPage.href = '#';
            $addHandler(prevPage, 'click', Function.createDelegate(this, this._prevPageClick));
        }
        else {
            prevPage.disabled = true;
            prevPage.className = 'DisabledAnchor';
        }
        pagingDiv.appendChild(prevPage);
        var elipses = document.createElement('span');
        elipses.innerHTML = '&nbsp;...&nbsp;';
        pagingDiv.appendChild(elipses);
        var nextPage = document.createElement('a');
        var nextText = document.createElement('span');
        nextText.innerHTML = 'next page';
        nextPage.appendChild(nextText);
        var fwdImg = document.createElement('img');
        fwdImg.src = '/gfx/icon-forward-12.png';
        fwdImg.style.border = '0';
        fwdImg.style.verticalAlign = 'middle';
        nextPage.appendChild(fwdImg);
        if (this._pageNumber < this.get__lastPage()) {
            nextPage.href = '#';
            $addHandler(nextPage, 'click', Function.createDelegate(this, this._nextPageClick));
        }
        else {
            nextPage.disabled = true;
            nextPage.className = 'DisabledAnchor';
        }
        pagingDiv.appendChild(nextPage);
        return pagingDiv;
    },
    _createNumberedPaging: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createNumberedPaging() {
        /// <returns type="Object" domElement="true"></returns>
        var uiPaging = document.createElement('p');
        var pagesSpan = document.createElement('span');
        pagesSpan.innerHTML = 'Pages: ';
        uiPaging.appendChild(pagesSpan);
        var renderPageNumber = this._getPageNumbersToRender();
        var renderedEllipsis = false;
        for (var i = 1; i <= this.get__lastPage(); i++) {
            if (renderPageNumber[i]) {
                var unread = this.get__firstUnreadPage() > 0 && this.get__firstUnreadPage() <= i && this.get__viewComments() < this.get__totalComments();
                var uiPageNumber;
                if (i === this._pageNumber) {
                    uiPageNumber = document.createElement('span');
                    uiPageNumber.className = (unread) ? 'CurrentPageUnread' : 'CurrentPage';
                }
                else {
                    uiPageNumber = document.createElement('a');
                    uiPageNumber.setAttribute('pagenumber', i);
                    (uiPageNumber).href = '#Comments';
                    $addHandler(uiPageNumber, 'click', Function.createDelegate(this, this._pageClick));
                    if (unread) {
                        uiPageNumber.className = 'Unread';
                    }
                }
                uiPageNumber.innerHTML = i.toString();
                uiPaging.appendChild(uiPageNumber);
                var space = document.createElement('span');
                space.innerHTML = '&nbsp;';
                uiPaging.appendChild(space);
                renderedEllipsis = false;
            }
            else {
                if (!renderedEllipsis) {
                    var ellipsis = document.createElement('span');
                    ellipsis.innerHTML = '...&nbsp;';
                    if (this.get__firstUnreadPage() > 0 && i > this.get__firstUnreadPage()) {
                        ellipsis.className = 'Unread';
                    }
                    uiPaging.appendChild(ellipsis);
                    renderedEllipsis = true;
                }
            }
        }
        return uiPaging;
    },
    _getPageNumbersToRender: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_getPageNumbersToRender() {
        /// <returns type="Array" elementType="Boolean"></returns>
        var endLinks = 3;
        var midLinks = 4;
        var midLinksUnread = 2;
        var renderPageNumber = new Array(this.get__lastPage() + 1);
        this._setRenderPageNumbers(renderPageNumber, 1, endLinks);
        this._setRenderPageNumbers(renderPageNumber, this.get__lastPage() - endLinks + 1, this.get__lastPage());
        if (this.get__firstUnreadPage() > 0) {
            this._setRenderPageNumbers(renderPageNumber, this.get__firstUnreadPage() - midLinksUnread, this.get__firstUnreadPage() + midLinksUnread - 1);
        }
        this._setRenderPageNumbers(renderPageNumber, this._pageNumber - midLinks, this._pageNumber + midLinks);
        for (var i = 1; i < renderPageNumber.length - 1; i++) {
            if (renderPageNumber[i - 1] && !renderPageNumber[i] && renderPageNumber[i + 1]) {
                renderPageNumber[i] = true;
            }
        }
        return renderPageNumber;
    },
    _setRenderPageNumbers: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_setRenderPageNumbers(renderPageNumber, start, end) {
        /// <param name="renderPageNumber" type="Array" elementType="Boolean">
        /// </param>
        /// <param name="start" type="Number" integer="true">
        /// </param>
        /// <param name="end" type="Number" integer="true">
        /// </param>
        for (var i = start; i <= end; i++) {
            renderPageNumber[i] = true;
        }
    },
    _onCommentPosted: null,
    _appendNewComment: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_appendNewComment(comment) {
        /// <param name="comment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        this._appendNewComments([ comment ]);
    },
    _appendNewComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_appendNewComments(comments) {
        /// <param name="comments" type="Array" elementType="CommentStub">
        /// </param>
        this._appendComments(comments);
        this.set__totalComments(this.get__totalComments() + comments.length);
        this._updateLastKnownCommentK(comments[comments.length - 1].k);
        this._loaded();
    },
    _updateLastKnownCommentK: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_updateLastKnownCommentK(lastCommentK) {
        /// <param name="lastCommentK" type="Number" integer="true">
        /// </param>
        if (lastCommentK > this.get__lastKnownCommentK()) {
            this.set__lastKnownCommentK(lastCommentK);
        }
    },
    _createPublicThread: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions) {
        /// <param name="currentParentObjectType" type="Number" integer="true">
        /// </param>
        /// <param name="currentParentObjectK" type="Number" integer="true">
        /// </param>
        /// <param name="duplicateGuid" type="String">
        /// </param>
        /// <param name="rawCommentHtml" type="String">
        /// </param>
        /// <param name="formatting" type="Boolean">
        /// </param>
        /// <param name="isNews" type="Boolean">
        /// </param>
        /// <param name="inviteUsrOptions" type="Array" elementType="String">
        /// </param>
        Spotted.WebServices.Controls.CommentsDisplay.Service.createPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, Function.createDelegate(this, this._createPublicThreadSuccess), null, null, -1);
    },
    _createPublicThreadSuccess: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createPublicThreadSuccess(newComment, userContext, methodName) {
        /// <param name="newComment" type="SpottedScript.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
        if (newComment != null) {
            if (this._threadK === 0) {
                this._threadK = newComment.threadK;
            }
            if (this._onCommentPosted != null) {
                this._onCommentPosted(null, new SpottedScript.IntEventArgs(this._threadK));
            }
            if (this.onThreadCreated != null) {
                this.onThreadCreated(null, new SpottedScript.IntEventArgs(this._threadK));
            }
            this._appendNewComment(newComment);
        }
    },
    _createNewPublicThread: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createNewPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions) {
        /// <param name="currentParentObjectType" type="Number" integer="true">
        /// </param>
        /// <param name="currentParentObjectK" type="Number" integer="true">
        /// </param>
        /// <param name="duplicateGuid" type="String">
        /// </param>
        /// <param name="rawCommentHtml" type="String">
        /// </param>
        /// <param name="formatting" type="Boolean">
        /// </param>
        /// <param name="isNews" type="Boolean">
        /// </param>
        /// <param name="inviteUsrOptions" type="Array" elementType="String">
        /// </param>
        Spotted.WebServices.Controls.CommentsDisplay.Service.createNewPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, Function.createDelegate(this, this._createNewPublicThreadSuccess), null, null, -1);
    },
    _createNewPublicThreadSuccess: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createNewPublicThreadSuccess(newThreadUrl, userContext, methodName) {
        /// <param name="newThreadUrl" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
        SpottedScript.Misc.redirect(newThreadUrl);
    },
    _createReply: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createReply(currentParentObjectType, currentParentObjectK, currentThreadK, duplicateGuid, rawCommentHtml, formatting, currentThreadLastKnownCommentK, inviteUsrOptions) {
        /// <param name="currentParentObjectType" type="Number" integer="true">
        /// </param>
        /// <param name="currentParentObjectK" type="Number" integer="true">
        /// </param>
        /// <param name="currentThreadK" type="Number" integer="true">
        /// </param>
        /// <param name="duplicateGuid" type="String">
        /// </param>
        /// <param name="rawCommentHtml" type="String">
        /// </param>
        /// <param name="formatting" type="Boolean">
        /// </param>
        /// <param name="currentThreadLastKnownCommentK" type="Number" integer="true">
        /// </param>
        /// <param name="inviteUsrOptions" type="Array" elementType="String">
        /// </param>
        Spotted.WebServices.Controls.CommentsDisplay.Service.createReply(currentParentObjectType, currentParentObjectK, currentThreadK, duplicateGuid, rawCommentHtml, formatting, currentThreadLastKnownCommentK, inviteUsrOptions, Function.createDelegate(this, this._createReplySuccess), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, -1);
    },
    _createReplySuccess: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_createReplySuccess(newComments, userContext, methodName) {
        /// <param name="newComments" type="Array" elementType="CommentStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        SpottedScript.Misc.hideWaitingCursor();
        if (newComments != null) {
            this._appendNewComments(newComments);
            if (this._onCommentPosted != null) {
                this._onCommentPosted(this, Sys.EventArgs.Empty);
            }
            SpottedScript.Misc.redirectToAnchor('Anchor-CommentK-' + newComments[0].k);
        }
    },
    _reloadComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_reloadComments() {
        this._initialComments = [];
        this._comments = [];
        this._lastPage = [];
        this._loadThreadComments();
    },
    _decrementCurrentThreadTotalComments: function SpottedScript_Controls_CommentsDisplay__threadCommentsProvider$_decrementCurrentThreadTotalComments() {
        this.set__totalComments(this.get__totalComments() - 1) + 1;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.CommentsDisplay.View
SpottedScript.Controls.CommentsDisplay.View = function SpottedScript_Controls_CommentsDisplay_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.CommentsDisplay.View.prototype = {
    clientId: null,
    get_uiInitialCommentPanel: function SpottedScript_Controls_CommentsDisplay_View$get_uiInitialCommentPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInitialCommentPanel');
    },
    get_initialCommentH1: function SpottedScript_Controls_CommentsDisplay_View$get_initialCommentH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitialCommentH1');
    },
    get_uiInitialComment: function SpottedScript_Controls_CommentsDisplay_View$get_uiInitialComment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInitialComment');
    },
    get_uiInitialCommentDataList: function SpottedScript_Controls_CommentsDisplay_View$get_uiInitialCommentDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInitialCommentDataList');
    },
    get_uiCommentsAnchor: function SpottedScript_Controls_CommentsDisplay_View$get_uiCommentsAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCommentsAnchor');
    },
    get_uiCommentsPanel: function SpottedScript_Controls_CommentsDisplay_View$get_uiCommentsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCommentsPanel');
    },
    get_commentsSubjectH1: function SpottedScript_Controls_CommentsDisplay_View$get_commentsSubjectH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsSubjectH1');
    },
    get_uiCommentsPanelClientSide: function SpottedScript_Controls_CommentsDisplay_View$get_uiCommentsPanelClientSide() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCommentsPanelClientSide');
    },
    get_uiCommentsPanelServerSide: function SpottedScript_Controls_CommentsDisplay_View$get_uiCommentsPanelServerSide() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCommentsPanelServerSide');
    },
    get_commentsPageP1: function SpottedScript_Controls_CommentsDisplay_View$get_commentsPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPageP1');
    },
    get_commentsPrevPageLink1: function SpottedScript_Controls_CommentsDisplay_View$get_commentsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPrevPageLink1');
    },
    get_commentsNextPageLink1: function SpottedScript_Controls_CommentsDisplay_View$get_commentsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsNextPageLink1');
    },
    get_commentsPagesP1: function SpottedScript_Controls_CommentsDisplay_View$get_commentsPagesP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPagesP1');
    },
    get_commentsDataList: function SpottedScript_Controls_CommentsDisplay_View$get_commentsDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsDataList');
    },
    get_commentsPagesP2: function SpottedScript_Controls_CommentsDisplay_View$get_commentsPagesP2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPagesP2');
    },
    get_commentsPageP2: function SpottedScript_Controls_CommentsDisplay_View$get_commentsPageP2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPageP2');
    },
    get_commentsPrevPageLink: function SpottedScript_Controls_CommentsDisplay_View$get_commentsPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPrevPageLink');
    },
    get_commentsNextPageLink: function SpottedScript_Controls_CommentsDisplay_View$get_commentsNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsNextPageLink');
    },
    get_uiPageNumber: function SpottedScript_Controls_CommentsDisplay_View$get_uiPageNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPageNumber');
    },
    get_uiClientID: function SpottedScript_Controls_CommentsDisplay_View$get_uiClientID() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiClientID');
    },
    get_uiCommentsPerPage: function SpottedScript_Controls_CommentsDisplay_View$get_uiCommentsPerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCommentsPerPage');
    },
    get_uiUsrIsLoggedIn: function SpottedScript_Controls_CommentsDisplay_View$get_uiUsrIsLoggedIn() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrIsLoggedIn');
    }
}
SpottedScript.Controls.CommentsDisplay.CommentStub.registerClass('SpottedScript.Controls.CommentsDisplay.CommentStub');
SpottedScript.Controls.CommentsDisplay.CommentResult.registerClass('SpottedScript.Controls.CommentsDisplay.CommentResult');
SpottedScript.Controls.CommentsDisplay.Controller.registerClass('SpottedScript.Controls.CommentsDisplay.Controller');
SpottedScript.Controls.CommentsDisplay._properties.registerClass('SpottedScript.Controls.CommentsDisplay._properties');
SpottedScript.Controls.CommentsDisplay._threadCommentsProvider.registerClass('SpottedScript.Controls.CommentsDisplay._threadCommentsProvider');
SpottedScript.Controls.CommentsDisplay.View.registerClass('SpottedScript.Controls.CommentsDisplay.View');
SpottedScript.Controls.CommentsDisplay._properties._deleteConfirmText = 'ConfirmText';
SpottedScript.Controls.CommentsDisplay._properties._commentK = 'CommentK';
SpottedScript.Controls.CommentsDisplay._properties._usrK = 'UsrK';
