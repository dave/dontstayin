//! CommentsDisplay.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.CommentsDisplay');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.CommentsDisplay.Service

Js.Controls.CommentsDisplay.Service = function Js_Controls_CommentsDisplay_Service() {
}
Js.Controls.CommentsDisplay.Service.getThreadComments = function Js_Controls_CommentsDisplay_Service$getThreadComments(threadK, pageNumber, getCommentsOnly, success, failure, userContext, timeout) {
    /// <param name="threadK" type="Number" integer="true">
    /// </param>
    /// <param name="pageNumber" type="Number" integer="true">
    /// </param>
    /// <param name="getCommentsOnly" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['threadK'] = threadK;
    p['pageNumber'] = pageNumber;
    p['getCommentsOnly'] = getCommentsOnly;
    var o = Js.Library.WebServiceHelper.options('GetThreadComments', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetThreadComments');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.lolAtComment = function Js_Controls_CommentsDisplay_Service$lolAtComment(commentK, success, failure, userContext, timeout) {
    /// <param name="commentK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['commentK'] = commentK;
    var o = Js.Library.WebServiceHelper.options('LolAtComment', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'LolAtComment');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.deleteComment = function Js_Controls_CommentsDisplay_Service$deleteComment(commentK, success, failure, userContext, timeout) {
    /// <param name="commentK" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['commentK'] = commentK;
    var o = Js.Library.WebServiceHelper.options('DeleteComment', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'DeleteComment');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.setThreadUsr = function Js_Controls_CommentsDisplay_Service$setThreadUsr(threadK, page, success, failure, userContext, timeout) {
    /// <param name="threadK" type="Number" integer="true">
    /// </param>
    /// <param name="page" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['threadK'] = threadK;
    p['page'] = page;
    var o = Js.Library.WebServiceHelper.options('SetThreadUsr', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'SetThreadUsr');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.createNewThreadInGroup = function Js_Controls_CommentsDisplay_Service$createNewThreadInGroup(groupK, discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, isPrivate, success, failure, userContext, timeout) {
    /// <param name="groupK" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
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
    /// <param name="isPrivate" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['groupK'] = groupK;
    p['discussableObjectType'] = discussableObjectType;
    p['discussableObjectK'] = discussableObjectK;
    p['duplicateGuid'] = duplicateGuid;
    p['rawCommentHtml'] = rawCommentHtml;
    p['formatting'] = formatting;
    p['isNews'] = isNews;
    p['inviteUsrOptions'] = inviteUsrOptions;
    p['isPrivate'] = isPrivate;
    var o = Js.Library.WebServiceHelper.options('CreateNewThreadInGroup', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CreateNewThreadInGroup');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.createPrivateThread = function Js_Controls_CommentsDisplay_Service$createPrivateThread(discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, inviteUsrOptions, isSealed, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="duplicateGuid" type="String">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="inviteUsrOptions" type="Array" elementType="String">
    /// </param>
    /// <param name="isSealed" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['discussableObjectType'] = discussableObjectType;
    p['discussableObjectK'] = discussableObjectK;
    p['duplicateGuid'] = duplicateGuid;
    p['rawCommentHtml'] = rawCommentHtml;
    p['formatting'] = formatting;
    p['inviteUsrOptions'] = inviteUsrOptions;
    p['isSealed'] = isSealed;
    var o = Js.Library.WebServiceHelper.options('CreatePrivateThread', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CreatePrivateThread');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.createReply = function Js_Controls_CommentsDisplay_Service$createReply(discussableObjectType, discussableObjectK, threadK, duplicateGuid, rawCommentHtml, formatting, lastKnownCommentK, inviteUsrOptions, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="threadK" type="Number" integer="true">
    /// </param>
    /// <param name="duplicateGuid" type="String">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="lastKnownCommentK" type="Number" integer="true">
    /// </param>
    /// <param name="inviteUsrOptions" type="Array" elementType="String">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['discussableObjectType'] = discussableObjectType;
    p['discussableObjectK'] = discussableObjectK;
    p['threadK'] = threadK;
    p['duplicateGuid'] = duplicateGuid;
    p['rawCommentHtml'] = rawCommentHtml;
    p['formatting'] = formatting;
    p['lastKnownCommentK'] = lastKnownCommentK;
    p['inviteUsrOptions'] = inviteUsrOptions;
    var o = Js.Library.WebServiceHelper.options('CreateReply', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CreateReply');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.createNewPublicThread = function Js_Controls_CommentsDisplay_Service$createNewPublicThread(discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
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
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['discussableObjectType'] = discussableObjectType;
    p['discussableObjectK'] = discussableObjectK;
    p['duplicateGuid'] = duplicateGuid;
    p['rawCommentHtml'] = rawCommentHtml;
    p['formatting'] = formatting;
    p['isNews'] = isNews;
    p['inviteUsrOptions'] = inviteUsrOptions;
    var o = Js.Library.WebServiceHelper.options('CreateNewPublicThread', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CreateNewPublicThread');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.createPublicThread = function Js_Controls_CommentsDisplay_Service$createPublicThread(discussableObjectType, discussableObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, success, failure, userContext, timeout) {
    /// <param name="discussableObjectType" type="Number" integer="true">
    /// </param>
    /// <param name="discussableObjectK" type="Number" integer="true">
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
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['discussableObjectType'] = discussableObjectType;
    p['discussableObjectK'] = discussableObjectK;
    p['duplicateGuid'] = duplicateGuid;
    p['rawCommentHtml'] = rawCommentHtml;
    p['formatting'] = formatting;
    p['isNews'] = isNews;
    p['inviteUsrOptions'] = inviteUsrOptions;
    var o = Js.Library.WebServiceHelper.options('CreatePublicThread', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CreatePublicThread');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.getNewGuid = function Js_Controls_CommentsDisplay_Service$getNewGuid(success, failure, userContext, timeout) {
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    var o = Js.Library.WebServiceHelper.options('GetNewGuid', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetNewGuid');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.cleanHtml = function Js_Controls_CommentsDisplay_Service$cleanHtml(html, success, failure, userContext, timeout) {
    /// <param name="html" type="String">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['html'] = html;
    var o = Js.Library.WebServiceHelper.options('CleanHtml', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'CleanHtml');
    };
    $.ajax(o);
}
Js.Controls.CommentsDisplay.Service.getPreviewHtml = function Js_Controls_CommentsDisplay_Service$getPreviewHtml(previewType, rawCommentHtml, formatting, success, failure, userContext, timeout) {
    /// <param name="previewType" type="Number" integer="true">
    /// </param>
    /// <param name="rawCommentHtml" type="String">
    /// </param>
    /// <param name="formatting" type="Boolean">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['previewType'] = previewType;
    p['rawCommentHtml'] = rawCommentHtml;
    p['formatting'] = formatting;
    var o = Js.Library.WebServiceHelper.options('GetPreviewHtml', '/WebServices/Controls/CommentsDisplay/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPreviewHtml');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.CommentsDisplay.ThreadCommentsProvider

Js.Controls.CommentsDisplay.ThreadCommentsProvider = function Js_Controls_CommentsDisplay_ThreadCommentsProvider(commentsPerPage) {
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
    /// <field name="onLoaded" type="Function">
    /// </field>
    /// <field name="onThreadCreated" type="Function">
    /// </field>
    /// <field name="commentsAnchorName" type="String">
    /// </field>
    /// <field name="onCommentPosted" type="Function">
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
Js.Controls.CommentsDisplay.ThreadCommentsProvider.prototype = {
    _commentsPerPage: 0,
    _threadK: 0,
    
    get_threadK: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_threadK() {
        /// <value type="Number" integer="true"></value>
        return this._threadK;
    },
    set_threadK: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_threadK(value) {
        /// <value type="Number" integer="true"></value>
        this._threadK = value;
        return value;
    },
    
    _pageNumber: 0,
    
    get_pageNumber: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_pageNumber() {
        /// <value type="Number" integer="true"></value>
        return this._pageNumber;
    },
    set_pageNumber: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_pageNumber(value) {
        /// <value type="Number" integer="true"></value>
        this._pageNumber = value;
        return value;
    },
    
    _initialComments: null,
    
    get_initialComment: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_initialComment() {
        /// <value type="Js.Controls.CommentsDisplay.CommentStub"></value>
        return this._initialComments[this._threadK];
    },
    set_initialComment: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_initialComment(value) {
        /// <value type="Js.Controls.CommentsDisplay.CommentStub"></value>
        this._initialComments[this._threadK] = value;
        return value;
    },
    
    _comments: null,
    
    get_comments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_comments() {
        /// <value type="Array" elementType="CommentStub"></value>
        return this._getCommentsByThreadAndPage(this._threadK, this._pageNumber);
    },
    set_comments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_comments(value) {
        /// <value type="Array" elementType="CommentStub"></value>
        if (this._comments[this._threadK] == null) {
            this._comments[this._threadK] = [];
        }
        (this._comments[this._threadK])[this._pageNumber] = value;
        return value;
    },
    
    _getCommentsByThreadAndPage: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_getCommentsByThreadAndPage(threadK, pageNumber) {
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
    
    get_lastPage: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_lastPage() {
        /// <value type="Number" integer="true"></value>
        return (this._lastPage[this._threadK] != null) ? this._lastPage[this._threadK] : 0;
    },
    set_lastPage: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_lastPage(value) {
        /// <value type="Number" integer="true"></value>
        this._lastPage[this._threadK] = value;
        return value;
    },
    
    _lastKnownCommentK: null,
    
    get_lastKnownCommentK: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_lastKnownCommentK() {
        /// <value type="Number" integer="true"></value>
        return (this._lastKnownCommentK[this._threadK] != null) ? this._lastKnownCommentK[this._threadK] : 0;
    },
    set_lastKnownCommentK: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_lastKnownCommentK(value) {
        /// <value type="Number" integer="true"></value>
        this._lastKnownCommentK[this._threadK] = value;
        return value;
    },
    
    _firstUnreadPage: null,
    
    get_firstUnreadPage: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_firstUnreadPage() {
        /// <value type="Number" integer="true"></value>
        return (this._firstUnreadPage[this._threadK] != null) ? this._firstUnreadPage[this._threadK] : 0;
    },
    set_firstUnreadPage: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_firstUnreadPage(value) {
        /// <value type="Number" integer="true"></value>
        this._firstUnreadPage[this._threadK] = value;
        return value;
    },
    
    _viewComments: null,
    
    get_viewComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_viewComments() {
        /// <value type="Number" integer="true"></value>
        return (this._viewComments[this._threadK] != null) ? this._viewComments[this._threadK] : 0;
    },
    set_viewComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_viewComments(value) {
        /// <value type="Number" integer="true"></value>
        this._viewComments[this._threadK] = value;
        return value;
    },
    
    _totalComments: null,
    
    get_totalComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$get_totalComments() {
        /// <value type="Number" integer="true"></value>
        return (this._totalComments[this._threadK] != null) ? this._totalComments[this._threadK] : 0;
    },
    set_totalComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$set_totalComments(value) {
        /// <value type="Number" integer="true"></value>
        this._totalComments[this._threadK] = value;
        return value;
    },
    
    onLoaded: null,
    onThreadCreated: null,
    
    appendComment: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$appendComment(newComment) {
        /// <param name="newComment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        if (this.get_pageNumber() !== this.get_lastPage()) {
            this.set_pageNumber(this.get_lastPage());
        }
        if (!this._threadK) {
            this._threadK = newComment.threadK;
        }
        if (this.get_comments() == null) {
            this.set_comments([ newComment ]);
        }
        else if (this.get_comments().length === this._commentsPerPage) {
            this._pageNumber++;
            this.set_lastPage(this.get_lastPage() + 1) - 1;
            this.set_comments([ newComment ]);
        }
        else {
            this.get_comments()[this.get_comments().length] = newComment;
        }
    },
    
    appendComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$appendComments(newComments) {
        /// <param name="newComments" type="Array" elementType="CommentStub">
        /// </param>
        for (var i = 0; i < newComments.length; i++) {
            this.appendComment(newComments[i]);
        }
    },
    
    loadThreadComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$loadThreadComments() {
        if (this._threadK > 0) {
            if (this.get_comments() == null) {
                this._loadThreadCommentsViaWebRequest();
            }
            else {
                this._loaded();
            }
        }
    },
    
    reloadThreadComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$reloadThreadComments(threadK, pageNumber) {
        /// <param name="threadK" type="Number" integer="true">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        this._threadK = threadK;
        this._pageNumber = pageNumber;
        this._loadThreadCommentsViaWebRequest();
    },
    
    _loadThreadCommentsViaWebRequest: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_loadThreadCommentsViaWebRequest() {
        Js.Controls.CommentsDisplay.Service.getThreadComments(this._threadK, this._pageNumber, ((this.get_initialComment() != null) ? true : false) && (this.get_lastPage() > 0), ss.Delegate.create(this, this._getThreadCommentsSuccess), ss.Delegate.create(this, this._getThreadCommentsFailure), null, -1);
    },
    
    _getThreadCommentsSuccess: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_getThreadCommentsSuccess(result, userContext, methodName) {
        /// <param name="result" type="Js.Controls.CommentsDisplay.CommentResult">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._pageNumber = result.currentPage;
        this.set_comments(result.comments);
        if (this.get_initialComment() == null) {
            this.set_initialComment((result.initialComment != null) ? result.initialComment : result.comments[0]);
        }
        if (!this.get_lastPage()) {
            this.set_lastPage(result.lastPage);
        }
        if (!this.get_firstUnreadPage()) {
            this.set_firstUnreadPage(result.firstUnreadPage);
        }
        if (!this.get_totalComments()) {
            this.set_totalComments(result.totalComments);
        }
        if (!this.get_viewComments()) {
            this.set_viewComments(result.viewComments);
        }
        this._updateLastKnownCommentK(result.comments[result.comments.length - 1].k);
        this._loaded();
        Js.Controls.CommentsDisplay.Service.setThreadUsr(this._threadK, this._pageNumber, null, null, null, -1);
    },
    
    _getThreadCommentsFailure: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_getThreadCommentsFailure(error, userContext, methodName) {
        /// <param name="error" type="Js.Library.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._pageNumber = 0;
        this.set_comments(null);
        this.set_firstUnreadPage(0);
        this.set_initialComment(null);
        this.set_lastKnownCommentK(0);
        this.set_lastPage(0);
        this.set_totalComments(0);
        this.set_viewComments(0);
        this._loaded();
    },
    
    _loaded: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_loaded() {
        if (this.onLoaded != null) {
            this.onLoaded(this, ss.EventArgs.Empty);
        }
    },
    
    commentsAnchorName: null,
    
    _prevPageClick: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_prevPageClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._refreshBanners();
        this._pageNumber--;
        this.loadThreadComments();
        Js.Library.Misc.redirectToAnchor(this.commentsAnchorName);
    },
    
    _refreshBanners: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_refreshBanners() {
        Js.Controls.Banners.Generator.Controller.refreshAllBanners();
    },
    
    _nextPageClick: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_nextPageClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._refreshBanners();
        this._pageNumber++;
        this.loadThreadComments();
        Js.Library.Misc.redirectToAnchor(this.commentsAnchorName);
    },
    
    _pageClick: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_pageClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._refreshBanners();
        this._pageNumber = e.target.getAttribute('pagenumber');
        this.loadThreadComments();
        Js.Library.Misc.redirectToAnchor(this.commentsAnchorName);
    },
    
    createPrevNextPaging: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$createPrevNextPaging() {
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
        if (this._pageNumber > 1 && this.get_lastPage() > 1) {
            prevPage.href = '#';
            $(prevPage).click(ss.Delegate.create(this, this._prevPageClick));
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
        if (this._pageNumber < this.get_lastPage()) {
            nextPage.href = '#';
            $(nextPage).click(ss.Delegate.create(this, this._nextPageClick));
        }
        else {
            nextPage.disabled = true;
            nextPage.className = 'DisabledAnchor';
        }
        pagingDiv.appendChild(nextPage);
        return pagingDiv;
    },
    
    createNumberedPaging: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$createNumberedPaging() {
        /// <returns type="Object" domElement="true"></returns>
        var uiPaging = document.createElement('p');
        var pagesSpan = document.createElement('span');
        pagesSpan.innerHTML = 'Pages: ';
        uiPaging.appendChild(pagesSpan);
        var renderPageNumber = this._getPageNumbersToRender();
        var renderedEllipsis = false;
        for (var i = 1; i <= this.get_lastPage(); i++) {
            if (renderPageNumber[i]) {
                var unread = this.get_firstUnreadPage() > 0 && this.get_firstUnreadPage() <= i && this.get_viewComments() < this.get_totalComments();
                var uiPageNumber;
                if (i === this._pageNumber) {
                    uiPageNumber = document.createElement('span');
                    uiPageNumber.className = (unread) ? 'CurrentPageUnread' : 'CurrentPage';
                }
                else {
                    uiPageNumber = document.createElement('a');
                    uiPageNumber.setAttribute('pagenumber', i);
                    (uiPageNumber).href = '#Comments';
                    $(uiPageNumber).click(ss.Delegate.create(this, this._pageClick));
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
                    if (this.get_firstUnreadPage() > 0 && i > this.get_firstUnreadPage()) {
                        ellipsis.className = 'Unread';
                    }
                    uiPaging.appendChild(ellipsis);
                    renderedEllipsis = true;
                }
            }
        }
        return uiPaging;
    },
    
    _getPageNumbersToRender: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_getPageNumbersToRender() {
        /// <returns type="Array" elementType="Boolean"></returns>
        var endLinks = 3;
        var midLinks = 4;
        var midLinksUnread = 2;
        var renderPageNumber = new Array(this.get_lastPage() + 1);
        this._setRenderPageNumbers(renderPageNumber, 1, endLinks);
        this._setRenderPageNumbers(renderPageNumber, this.get_lastPage() - endLinks + 1, this.get_lastPage());
        if (this.get_firstUnreadPage() > 0) {
            this._setRenderPageNumbers(renderPageNumber, this.get_firstUnreadPage() - midLinksUnread, this.get_firstUnreadPage() + midLinksUnread - 1);
        }
        this._setRenderPageNumbers(renderPageNumber, this._pageNumber - midLinks, this._pageNumber + midLinks);
        for (var i = 1; i < renderPageNumber.length - 1; i++) {
            if (!!renderPageNumber[i - 1] && !renderPageNumber[i] && !!renderPageNumber[i + 1]) {
                renderPageNumber[i] = true;
            }
        }
        return renderPageNumber;
    },
    
    _setRenderPageNumbers: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_setRenderPageNumbers(renderPageNumber, start, end) {
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
    
    onCommentPosted: null,
    
    _appendNewComment: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_appendNewComment(comment) {
        /// <param name="comment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        this._appendNewComments([ comment ]);
    },
    
    _appendNewComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_appendNewComments(comments) {
        /// <param name="comments" type="Array" elementType="CommentStub">
        /// </param>
        this.appendComments(comments);
        this.set_totalComments(this.get_totalComments() + comments.length);
        this._updateLastKnownCommentK(comments[comments.length - 1].k);
        this._loaded();
    },
    
    _updateLastKnownCommentK: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_updateLastKnownCommentK(lastCommentK) {
        /// <param name="lastCommentK" type="Number" integer="true">
        /// </param>
        if (lastCommentK > this.get_lastKnownCommentK()) {
            this.set_lastKnownCommentK(lastCommentK);
        }
    },
    
    createPublicThread: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$createPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions) {
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
        Js.Controls.CommentsDisplay.Service.createPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, ss.Delegate.create(this, this._createPublicThreadSuccess), null, null, -1);
    },
    
    _createPublicThreadSuccess: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_createPublicThreadSuccess(newComment, userContext, methodName) {
        /// <param name="newComment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
        if (newComment != null) {
            if (!this._threadK) {
                this._threadK = newComment.threadK;
            }
            if (this.onCommentPosted != null) {
                this.onCommentPosted(null, new Js.Library.IntEventArgs(this._threadK));
            }
            if (this.onThreadCreated != null) {
                this.onThreadCreated(null, new Js.Library.IntEventArgs(this._threadK));
            }
            this._appendNewComment(newComment);
        }
    },
    
    createNewPublicThread: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$createNewPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions) {
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
        Js.Controls.CommentsDisplay.Service.createNewPublicThread(currentParentObjectType, currentParentObjectK, duplicateGuid, rawCommentHtml, formatting, isNews, inviteUsrOptions, ss.Delegate.create(this, this._createNewPublicThreadSuccess), null, null, -1);
    },
    
    _createNewPublicThreadSuccess: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_createNewPublicThreadSuccess(newThreadUrl, userContext, methodName) {
        /// <param name="newThreadUrl" type="String">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
        Js.Library.Misc.redirect(newThreadUrl);
    },
    
    createReply: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$createReply(currentParentObjectType, currentParentObjectK, currentThreadK, duplicateGuid, rawCommentHtml, formatting, currentThreadLastKnownCommentK, inviteUsrOptions) {
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
        Js.Controls.CommentsDisplay.Service.createReply(currentParentObjectType, currentParentObjectK, currentThreadK, duplicateGuid, rawCommentHtml, formatting, currentThreadLastKnownCommentK, inviteUsrOptions, ss.Delegate.create(this, this._createReplySuccess), Js.Library.Trace.webServiceFailure, null, -1);
    },
    
    _createReplySuccess: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$_createReplySuccess(newComments, userContext, methodName) {
        /// <param name="newComments" type="Array" elementType="CommentStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
        if (newComments != null) {
            this._appendNewComments(newComments);
            if (this.onCommentPosted != null) {
                this.onCommentPosted(this, ss.EventArgs.Empty);
            }
            Js.Library.Misc.redirectToAnchor('Anchor-CommentK-' + newComments[0].k);
        }
    },
    
    reloadComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$reloadComments() {
        this._initialComments = [];
        this._comments = [];
        this._lastPage = [];
        this.loadThreadComments();
    },
    
    decrementCurrentThreadTotalComments: function Js_Controls_CommentsDisplay_ThreadCommentsProvider$decrementCurrentThreadTotalComments() {
        this.set_totalComments(this.get_totalComments() - 1) + 1;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.CommentsDisplay.View

Js.Controls.CommentsDisplay.View = function Js_Controls_CommentsDisplay_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiInitialCommentPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiInitialCommentPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitialCommentH1" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitialCommentH1J" type="jQueryObject">
    /// </field>
    /// <field name="_uiInitialComment" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiInitialCommentJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiInitialCommentDataList" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiInitialCommentDataListJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCommentsAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCommentsAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCommentsPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCommentsPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsSubjectH1" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsSubjectH1J" type="jQueryObject">
    /// </field>
    /// <field name="_uiCommentsPanelClientSide" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCommentsPanelClientSideJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCommentsPanelServerSide" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCommentsPanelServerSideJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsPageP1" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsPageP1J" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsPrevPageLink1" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsPrevPageLink1J" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsNextPageLink1" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsNextPageLink1J" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsPagesP1" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsPagesP1J" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsDataList" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsDataListJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsPagesP2" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsPagesP2J" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsPageP2" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsPageP2J" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsPrevPageLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsPrevPageLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsNextPageLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsNextPageLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPageNumber" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPageNumberJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiClientID" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiClientIDJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCommentsPerPage" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCommentsPerPageJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiUsrIsLoggedIn" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUsrIsLoggedInJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.CommentsDisplay.View.prototype = {
    clientId: null,
    
    get_uiInitialCommentPanel: function Js_Controls_CommentsDisplay_View$get_uiInitialCommentPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiInitialCommentPanel == null) {
            this._uiInitialCommentPanel = document.getElementById(this.clientId + '_uiInitialCommentPanel');
        }
        return this._uiInitialCommentPanel;
    },
    
    _uiInitialCommentPanel: null,
    
    get_uiInitialCommentPanelJ: function Js_Controls_CommentsDisplay_View$get_uiInitialCommentPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiInitialCommentPanelJ == null) {
            this._uiInitialCommentPanelJ = $('#' + this.clientId + '_uiInitialCommentPanel');
        }
        return this._uiInitialCommentPanelJ;
    },
    
    _uiInitialCommentPanelJ: null,
    
    get_initialCommentH1: function Js_Controls_CommentsDisplay_View$get_initialCommentH1() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitialCommentH1 == null) {
            this._InitialCommentH1 = document.getElementById(this.clientId + '_InitialCommentH1');
        }
        return this._InitialCommentH1;
    },
    
    _InitialCommentH1: null,
    
    get_initialCommentH1J: function Js_Controls_CommentsDisplay_View$get_initialCommentH1J() {
        /// <value type="jQueryObject"></value>
        if (this._InitialCommentH1J == null) {
            this._InitialCommentH1J = $('#' + this.clientId + '_InitialCommentH1');
        }
        return this._InitialCommentH1J;
    },
    
    _InitialCommentH1J: null,
    
    get_uiInitialComment: function Js_Controls_CommentsDisplay_View$get_uiInitialComment() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiInitialComment == null) {
            this._uiInitialComment = document.getElementById(this.clientId + '_uiInitialComment');
        }
        return this._uiInitialComment;
    },
    
    _uiInitialComment: null,
    
    get_uiInitialCommentJ: function Js_Controls_CommentsDisplay_View$get_uiInitialCommentJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiInitialCommentJ == null) {
            this._uiInitialCommentJ = $('#' + this.clientId + '_uiInitialComment');
        }
        return this._uiInitialCommentJ;
    },
    
    _uiInitialCommentJ: null,
    
    get_uiInitialCommentDataList: function Js_Controls_CommentsDisplay_View$get_uiInitialCommentDataList() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiInitialCommentDataList == null) {
            this._uiInitialCommentDataList = document.getElementById(this.clientId + '_uiInitialCommentDataList');
        }
        return this._uiInitialCommentDataList;
    },
    
    _uiInitialCommentDataList: null,
    
    get_uiInitialCommentDataListJ: function Js_Controls_CommentsDisplay_View$get_uiInitialCommentDataListJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiInitialCommentDataListJ == null) {
            this._uiInitialCommentDataListJ = $('#' + this.clientId + '_uiInitialCommentDataList');
        }
        return this._uiInitialCommentDataListJ;
    },
    
    _uiInitialCommentDataListJ: null,
    
    get_uiCommentsAnchor: function Js_Controls_CommentsDisplay_View$get_uiCommentsAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCommentsAnchor == null) {
            this._uiCommentsAnchor = document.getElementById(this.clientId + '_uiCommentsAnchor');
        }
        return this._uiCommentsAnchor;
    },
    
    _uiCommentsAnchor: null,
    
    get_uiCommentsAnchorJ: function Js_Controls_CommentsDisplay_View$get_uiCommentsAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCommentsAnchorJ == null) {
            this._uiCommentsAnchorJ = $('#' + this.clientId + '_uiCommentsAnchor');
        }
        return this._uiCommentsAnchorJ;
    },
    
    _uiCommentsAnchorJ: null,
    
    get_uiCommentsPanel: function Js_Controls_CommentsDisplay_View$get_uiCommentsPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCommentsPanel == null) {
            this._uiCommentsPanel = document.getElementById(this.clientId + '_uiCommentsPanel');
        }
        return this._uiCommentsPanel;
    },
    
    _uiCommentsPanel: null,
    
    get_uiCommentsPanelJ: function Js_Controls_CommentsDisplay_View$get_uiCommentsPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCommentsPanelJ == null) {
            this._uiCommentsPanelJ = $('#' + this.clientId + '_uiCommentsPanel');
        }
        return this._uiCommentsPanelJ;
    },
    
    _uiCommentsPanelJ: null,
    
    get_commentsSubjectH1: function Js_Controls_CommentsDisplay_View$get_commentsSubjectH1() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsSubjectH1 == null) {
            this._CommentsSubjectH1 = document.getElementById(this.clientId + '_CommentsSubjectH1');
        }
        return this._CommentsSubjectH1;
    },
    
    _CommentsSubjectH1: null,
    
    get_commentsSubjectH1J: function Js_Controls_CommentsDisplay_View$get_commentsSubjectH1J() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsSubjectH1J == null) {
            this._CommentsSubjectH1J = $('#' + this.clientId + '_CommentsSubjectH1');
        }
        return this._CommentsSubjectH1J;
    },
    
    _CommentsSubjectH1J: null,
    
    get_uiCommentsPanelClientSide: function Js_Controls_CommentsDisplay_View$get_uiCommentsPanelClientSide() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCommentsPanelClientSide == null) {
            this._uiCommentsPanelClientSide = document.getElementById(this.clientId + '_uiCommentsPanelClientSide');
        }
        return this._uiCommentsPanelClientSide;
    },
    
    _uiCommentsPanelClientSide: null,
    
    get_uiCommentsPanelClientSideJ: function Js_Controls_CommentsDisplay_View$get_uiCommentsPanelClientSideJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCommentsPanelClientSideJ == null) {
            this._uiCommentsPanelClientSideJ = $('#' + this.clientId + '_uiCommentsPanelClientSide');
        }
        return this._uiCommentsPanelClientSideJ;
    },
    
    _uiCommentsPanelClientSideJ: null,
    
    get_uiCommentsPanelServerSide: function Js_Controls_CommentsDisplay_View$get_uiCommentsPanelServerSide() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCommentsPanelServerSide == null) {
            this._uiCommentsPanelServerSide = document.getElementById(this.clientId + '_uiCommentsPanelServerSide');
        }
        return this._uiCommentsPanelServerSide;
    },
    
    _uiCommentsPanelServerSide: null,
    
    get_uiCommentsPanelServerSideJ: function Js_Controls_CommentsDisplay_View$get_uiCommentsPanelServerSideJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCommentsPanelServerSideJ == null) {
            this._uiCommentsPanelServerSideJ = $('#' + this.clientId + '_uiCommentsPanelServerSide');
        }
        return this._uiCommentsPanelServerSideJ;
    },
    
    _uiCommentsPanelServerSideJ: null,
    
    get_commentsPageP1: function Js_Controls_CommentsDisplay_View$get_commentsPageP1() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsPageP1 == null) {
            this._CommentsPageP1 = document.getElementById(this.clientId + '_CommentsPageP1');
        }
        return this._CommentsPageP1;
    },
    
    _CommentsPageP1: null,
    
    get_commentsPageP1J: function Js_Controls_CommentsDisplay_View$get_commentsPageP1J() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsPageP1J == null) {
            this._CommentsPageP1J = $('#' + this.clientId + '_CommentsPageP1');
        }
        return this._CommentsPageP1J;
    },
    
    _CommentsPageP1J: null,
    
    get_commentsPrevPageLink1: function Js_Controls_CommentsDisplay_View$get_commentsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsPrevPageLink1 == null) {
            this._CommentsPrevPageLink1 = document.getElementById(this.clientId + '_CommentsPrevPageLink1');
        }
        return this._CommentsPrevPageLink1;
    },
    
    _CommentsPrevPageLink1: null,
    
    get_commentsPrevPageLink1J: function Js_Controls_CommentsDisplay_View$get_commentsPrevPageLink1J() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsPrevPageLink1J == null) {
            this._CommentsPrevPageLink1J = $('#' + this.clientId + '_CommentsPrevPageLink1');
        }
        return this._CommentsPrevPageLink1J;
    },
    
    _CommentsPrevPageLink1J: null,
    
    get_commentsNextPageLink1: function Js_Controls_CommentsDisplay_View$get_commentsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsNextPageLink1 == null) {
            this._CommentsNextPageLink1 = document.getElementById(this.clientId + '_CommentsNextPageLink1');
        }
        return this._CommentsNextPageLink1;
    },
    
    _CommentsNextPageLink1: null,
    
    get_commentsNextPageLink1J: function Js_Controls_CommentsDisplay_View$get_commentsNextPageLink1J() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsNextPageLink1J == null) {
            this._CommentsNextPageLink1J = $('#' + this.clientId + '_CommentsNextPageLink1');
        }
        return this._CommentsNextPageLink1J;
    },
    
    _CommentsNextPageLink1J: null,
    
    get_commentsPagesP1: function Js_Controls_CommentsDisplay_View$get_commentsPagesP1() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsPagesP1 == null) {
            this._CommentsPagesP1 = document.getElementById(this.clientId + '_CommentsPagesP1');
        }
        return this._CommentsPagesP1;
    },
    
    _CommentsPagesP1: null,
    
    get_commentsPagesP1J: function Js_Controls_CommentsDisplay_View$get_commentsPagesP1J() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsPagesP1J == null) {
            this._CommentsPagesP1J = $('#' + this.clientId + '_CommentsPagesP1');
        }
        return this._CommentsPagesP1J;
    },
    
    _CommentsPagesP1J: null,
    
    get_commentsDataList: function Js_Controls_CommentsDisplay_View$get_commentsDataList() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsDataList == null) {
            this._CommentsDataList = document.getElementById(this.clientId + '_CommentsDataList');
        }
        return this._CommentsDataList;
    },
    
    _CommentsDataList: null,
    
    get_commentsDataListJ: function Js_Controls_CommentsDisplay_View$get_commentsDataListJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsDataListJ == null) {
            this._CommentsDataListJ = $('#' + this.clientId + '_CommentsDataList');
        }
        return this._CommentsDataListJ;
    },
    
    _CommentsDataListJ: null,
    
    get_commentsPagesP2: function Js_Controls_CommentsDisplay_View$get_commentsPagesP2() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsPagesP2 == null) {
            this._CommentsPagesP2 = document.getElementById(this.clientId + '_CommentsPagesP2');
        }
        return this._CommentsPagesP2;
    },
    
    _CommentsPagesP2: null,
    
    get_commentsPagesP2J: function Js_Controls_CommentsDisplay_View$get_commentsPagesP2J() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsPagesP2J == null) {
            this._CommentsPagesP2J = $('#' + this.clientId + '_CommentsPagesP2');
        }
        return this._CommentsPagesP2J;
    },
    
    _CommentsPagesP2J: null,
    
    get_commentsPageP2: function Js_Controls_CommentsDisplay_View$get_commentsPageP2() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsPageP2 == null) {
            this._CommentsPageP2 = document.getElementById(this.clientId + '_CommentsPageP2');
        }
        return this._CommentsPageP2;
    },
    
    _CommentsPageP2: null,
    
    get_commentsPageP2J: function Js_Controls_CommentsDisplay_View$get_commentsPageP2J() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsPageP2J == null) {
            this._CommentsPageP2J = $('#' + this.clientId + '_CommentsPageP2');
        }
        return this._CommentsPageP2J;
    },
    
    _CommentsPageP2J: null,
    
    get_commentsPrevPageLink: function Js_Controls_CommentsDisplay_View$get_commentsPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsPrevPageLink == null) {
            this._CommentsPrevPageLink = document.getElementById(this.clientId + '_CommentsPrevPageLink');
        }
        return this._CommentsPrevPageLink;
    },
    
    _CommentsPrevPageLink: null,
    
    get_commentsPrevPageLinkJ: function Js_Controls_CommentsDisplay_View$get_commentsPrevPageLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsPrevPageLinkJ == null) {
            this._CommentsPrevPageLinkJ = $('#' + this.clientId + '_CommentsPrevPageLink');
        }
        return this._CommentsPrevPageLinkJ;
    },
    
    _CommentsPrevPageLinkJ: null,
    
    get_commentsNextPageLink: function Js_Controls_CommentsDisplay_View$get_commentsNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsNextPageLink == null) {
            this._CommentsNextPageLink = document.getElementById(this.clientId + '_CommentsNextPageLink');
        }
        return this._CommentsNextPageLink;
    },
    
    _CommentsNextPageLink: null,
    
    get_commentsNextPageLinkJ: function Js_Controls_CommentsDisplay_View$get_commentsNextPageLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsNextPageLinkJ == null) {
            this._CommentsNextPageLinkJ = $('#' + this.clientId + '_CommentsNextPageLink');
        }
        return this._CommentsNextPageLinkJ;
    },
    
    _CommentsNextPageLinkJ: null,
    
    get_uiPageNumber: function Js_Controls_CommentsDisplay_View$get_uiPageNumber() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPageNumber == null) {
            this._uiPageNumber = document.getElementById(this.clientId + '_uiPageNumber');
        }
        return this._uiPageNumber;
    },
    
    _uiPageNumber: null,
    
    get_uiPageNumberJ: function Js_Controls_CommentsDisplay_View$get_uiPageNumberJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPageNumberJ == null) {
            this._uiPageNumberJ = $('#' + this.clientId + '_uiPageNumber');
        }
        return this._uiPageNumberJ;
    },
    
    _uiPageNumberJ: null,
    
    get_uiClientID: function Js_Controls_CommentsDisplay_View$get_uiClientID() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiClientID == null) {
            this._uiClientID = document.getElementById(this.clientId + '_uiClientID');
        }
        return this._uiClientID;
    },
    
    _uiClientID: null,
    
    get_uiClientIDJ: function Js_Controls_CommentsDisplay_View$get_uiClientIDJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiClientIDJ == null) {
            this._uiClientIDJ = $('#' + this.clientId + '_uiClientID');
        }
        return this._uiClientIDJ;
    },
    
    _uiClientIDJ: null,
    
    get_uiCommentsPerPage: function Js_Controls_CommentsDisplay_View$get_uiCommentsPerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCommentsPerPage == null) {
            this._uiCommentsPerPage = document.getElementById(this.clientId + '_uiCommentsPerPage');
        }
        return this._uiCommentsPerPage;
    },
    
    _uiCommentsPerPage: null,
    
    get_uiCommentsPerPageJ: function Js_Controls_CommentsDisplay_View$get_uiCommentsPerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCommentsPerPageJ == null) {
            this._uiCommentsPerPageJ = $('#' + this.clientId + '_uiCommentsPerPage');
        }
        return this._uiCommentsPerPageJ;
    },
    
    _uiCommentsPerPageJ: null,
    
    get_uiUsrIsLoggedIn: function Js_Controls_CommentsDisplay_View$get_uiUsrIsLoggedIn() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUsrIsLoggedIn == null) {
            this._uiUsrIsLoggedIn = document.getElementById(this.clientId + '_uiUsrIsLoggedIn');
        }
        return this._uiUsrIsLoggedIn;
    },
    
    _uiUsrIsLoggedIn: null,
    
    get_uiUsrIsLoggedInJ: function Js_Controls_CommentsDisplay_View$get_uiUsrIsLoggedInJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUsrIsLoggedInJ == null) {
            this._uiUsrIsLoggedInJ = $('#' + this.clientId + '_uiUsrIsLoggedIn');
        }
        return this._uiUsrIsLoggedInJ;
    },
    
    _uiUsrIsLoggedInJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.CommentsDisplay.CommentStub

Js.Controls.CommentsDisplay.CommentStub = function Js_Controls_CommentsDisplay_CommentStub() {
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
Js.Controls.CommentsDisplay.CommentStub.prototype = {
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
// Js.Controls.CommentsDisplay.CommentResult

Js.Controls.CommentsDisplay.CommentResult = function Js_Controls_CommentsDisplay_CommentResult() {
    /// <field name="initialComment" type="Js.Controls.CommentsDisplay.CommentStub">
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
Js.Controls.CommentsDisplay.CommentResult.prototype = {
    initialComment: null,
    comments: null,
    lastPage: 0,
    currentPage: 0,
    firstUnreadPage: 0,
    viewComments: 0,
    totalComments: 0
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.CommentsDisplay.Controller

Js.Controls.CommentsDisplay.Controller = function Js_Controls_CommentsDisplay_Controller(view) {
    /// <param name="view" type="Js.Controls.CommentsDisplay.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.CommentsDisplay.View">
    /// </field>
    /// <field name="threadCommentsProvider" type="Js.Controls.CommentsDisplay.ThreadCommentsProvider">
    /// </field>
    /// <field name="_uiCommentsCount" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPaging1Div" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCommentsDiv" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPaging2Div" type="Object" domElement="true">
    /// </field>
    /// <field name="onCommentsDisplayed" type="Function">
    /// </field>
    /// <field name="onThreadDeleted" type="Function">
    /// </field>
    this._view = view;
    this.threadCommentsProvider = new Js.Controls.CommentsDisplay.ThreadCommentsProvider(this.get__commentsPerPage());
    this.threadCommentsProvider.commentsAnchorName = view.get_uiCommentsAnchor().getAttribute('name');
    this.threadCommentsProvider.onLoaded = ss.Delegate.create(this, this._display);
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
Js.Controls.CommentsDisplay.Controller._removeAllChildren = function Js_Controls_CommentsDisplay_Controller$_removeAllChildren(domElement) {
    /// <param name="domElement" type="Object" domElement="true">
    /// </param>
    for (var childNodeIndex = 0; childNodeIndex < domElement.childNodes.length; childNodeIndex++) {
        domElement.removeChild(domElement.childNodes[childNodeIndex]);
    }
    domElement.innerHTML = '';
}
Js.Controls.CommentsDisplay.Controller.prototype = {
    _view: null,
    threadCommentsProvider: null,
    
    get__uniqueID: function Js_Controls_CommentsDisplay_Controller$get__uniqueID() {
        /// <value type="String"></value>
        return this._view.get_uiClientID().value + '_controls_';
    },
    
    _uiCommentsCount: null,
    _uiPaging1Div: null,
    _uiCommentsDiv: null,
    _uiPaging2Div: null,
    
    get__commentsPerPage: function Js_Controls_CommentsDisplay_Controller$get__commentsPerPage() {
        /// <value type="Number" integer="true"></value>
        return parseInt(this._view.get_uiCommentsPerPage().value);
    },
    
    _hideCommentsPanelServerSide: function Js_Controls_CommentsDisplay_Controller$_hideCommentsPanelServerSide() {
        if (this._view.get_uiInitialCommentDataList() != null) {
            this._view.get_uiInitialCommentDataList().style.display = 'none';
        }
        this._view.get_uiCommentsPanelServerSide().style.display = 'none';
        this._view.get_uiCommentsPanelClientSide().style.display = '';
    },
    
    setCommentsCount: function Js_Controls_CommentsDisplay_Controller$setCommentsCount(commentsCount) {
        /// <param name="commentsCount" type="Number" integer="true">
        /// </param>
        if (!commentsCount) {
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
    
    _setCommentsAreaVisible: function Js_Controls_CommentsDisplay_Controller$_setCommentsAreaVisible(vis) {
        /// <param name="vis" type="Boolean">
        /// </param>
        var display = (vis) ? '' : 'none';
        var displayPaging = (vis && this.threadCommentsProvider.get_lastPage() > 1) ? '' : 'none';
        this._uiPaging1Div.style.display = displayPaging;
        this._uiCommentsDiv.style.display = display;
        this._uiPaging2Div.style.display = displayPaging;
        if (vis) {
            this._view.get_uiCommentsPanel().style.display = '';
        }
    },
    
    _hideCommentsCount: function Js_Controls_CommentsDisplay_Controller$_hideCommentsCount() {
        this._uiCommentsCount.style.display = 'none';
    },
    
    showComments: function Js_Controls_CommentsDisplay_Controller$showComments(threadK, pageNumber) {
        /// <param name="threadK" type="Number" integer="true">
        /// </param>
        /// <param name="pageNumber" type="Number" integer="true">
        /// </param>
        this.threadCommentsProvider.set_threadK(threadK);
        this.threadCommentsProvider.set_pageNumber(pageNumber);
        this.threadCommentsProvider.loadThreadComments();
    },
    
    onCommentsDisplayed: null,
    
    _display: function Js_Controls_CommentsDisplay_Controller$_display(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (this.threadCommentsProvider.get_totalComments() > 0) {
            this._displayAll();
        }
        else {
            this._view.get_uiCommentsPanel().style.display = 'none';
        }
        if (this.onCommentsDisplayed != null) {
            this.onCommentsDisplayed(this, e);
        }
    },
    
    _displayAll: function Js_Controls_CommentsDisplay_Controller$_displayAll() {
        this._displayInitialComment();
        this._displayPaging();
        this._view.get_uiCommentsPanelClientSide().style.display = '';
        this._displayComments();
        this._hideCommentsCount();
    },
    
    _displayComments: function Js_Controls_CommentsDisplay_Controller$_displayComments() {
        Js.Controls.CommentsDisplay.Controller._removeAllChildren(this._uiCommentsDiv);
        this._setCommentsAreaVisible(true);
        var comments = this.threadCommentsProvider.get_comments();
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
    
    _addComment: function Js_Controls_CommentsDisplay_Controller$_addComment(comment) {
        /// <param name="comment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        this._uiCommentsDiv.appendChild(this._createAnchor(comment));
        this._uiCommentsDiv.appendChild(this._createComment(comment));
    },
    
    _displayPaging: function Js_Controls_CommentsDisplay_Controller$_displayPaging() {
        if (this.threadCommentsProvider.get_lastPage() === 1) {
            this._uiPaging1Div.style.display = 'none';
            this._uiPaging2Div.style.display = 'none';
            return;
        }
        this._uiPaging1Div.style.display = '';
        Js.Controls.CommentsDisplay.Controller._removeAllChildren(this._uiPaging1Div);
        this._uiPaging1Div.appendChild(this.threadCommentsProvider.createPrevNextPaging());
        this._uiPaging1Div.appendChild(this.threadCommentsProvider.createNumberedPaging());
        this._uiPaging1Div.style.display = '';
        Js.Controls.CommentsDisplay.Controller._removeAllChildren(this._uiPaging2Div);
        this._uiPaging2Div.appendChild(this.threadCommentsProvider.createNumberedPaging());
        this._uiPaging2Div.appendChild(this.threadCommentsProvider.createPrevNextPaging());
    },
    
    _displayInitialComment: function Js_Controls_CommentsDisplay_Controller$_displayInitialComment() {
        if (this.threadCommentsProvider.get_initialComment() != null && this.threadCommentsProvider.get_pageNumber() > 1) {
            this._view.get_uiInitialCommentPanel().style.display = '';
            this._view.get_uiInitialComment().style.display = '';
            for (var childNodeIndex = 0; childNodeIndex < this._view.get_uiInitialComment().childNodes.length; childNodeIndex++) {
                this._view.get_uiInitialComment().removeChild(this._view.get_uiInitialComment().childNodes[childNodeIndex]);
            }
            this._view.get_uiInitialComment().appendChild(this._createComment(this.threadCommentsProvider.get_initialComment()));
            this._setCommentsSubject('Replies');
        }
        else {
            this._view.get_uiInitialCommentPanel().style.display = 'none';
            this._setCommentsSubject('Comments');
        }
    },
    
    _setCommentsSubject: function Js_Controls_CommentsDisplay_Controller$_setCommentsSubject(subject) {
        /// <param name="subject" type="String">
        /// </param>
        this._view.get_commentsSubjectH1().childNodes[0].innerHTML = subject;
    },
    
    _updateLols: function Js_Controls_CommentsDisplay_Controller$_updateLols(commentK, newLolHtml) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <param name="newLolHtml" type="String">
        /// </param>
        document.getElementById(this._getLolAnchorControlID(commentK)).style.display = 'none';
        (document.getElementById(this._getLolSpanControlID(commentK))).innerHTML = newLolHtml;
    },
    
    _getCommentControlID: function Js_Controls_CommentsDisplay_Controller$_getCommentControlID(commentK) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this.get__uniqueID() + 'C' + commentK;
    },
    
    _getLolAnchorControlID: function Js_Controls_CommentsDisplay_Controller$_getLolAnchorControlID(commentK) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this.get__uniqueID() + 'L' + commentK;
    },
    
    _getLolSpanControlID: function Js_Controls_CommentsDisplay_Controller$_getLolSpanControlID(commentK) {
        /// <param name="commentK" type="Number" integer="true">
        /// </param>
        /// <returns type="String"></returns>
        return this.get__uniqueID() + 'H' + commentK;
    },
    
    _createAnchor: function Js_Controls_CommentsDisplay_Controller$_createAnchor(comment) {
        /// <param name="comment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var anchor = document.createElement('a');
        anchor.id = 'Anchor-CommentK-' + comment.k;
        return anchor;
    },
    
    _createComment: function Js_Controls_CommentsDisplay_Controller$_createComment(comment) {
        /// <param name="comment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var div = document.createElement('div');
        div.className = 'CommentOuter ClearAfter';
        div.appendChild(this._createCommentLeft(comment));
        div.appendChild(this._createCommentBody(comment));
        return div;
    },
    
    _createCommentLeft: function Js_Controls_CommentsDisplay_Controller$_createCommentLeft(comment) {
        /// <param name="comment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var div = document.createElement('div');
        div.className = 'CommentLeft';
        var aimg = document.createElement('a');
        aimg.href = comment.usrUrl;
        this._createMouseOverAndOut($(aimg), comment.usrRollover, 'htm();');
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
    
    _createCommentBody: function Js_Controls_CommentsDisplay_Controller$_createCommentBody(comment) {
        /// <param name="comment" type="Js.Controls.CommentsDisplay.CommentStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var div = document.createElement('div');
        div.className = 'CommentBody';
        div.innerHTML = ((comment.isNew) ? '<a name="Unread"></a><span class="Unread">NEW</span> ' : '') + comment.html;
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
            lolAnchor.setAttribute(Js.Controls.CommentsDisplay._properties.commentK, comment.k);
            $(lolAnchor).click(ss.Delegate.create(this, this._lolClick));
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
        $(quote).mousedown(ss.Delegate.create(this, this._quoteMouseDown)).click(ss.Delegate.create(this, this._quoteClick));
        quote.setAttribute(Js.Controls.CommentsDisplay._properties.usrK, comment.usrK);
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
            $(deleteAnchor).click(ss.Delegate.create(this, this._deleteClick));
            deleteAnchor.setAttribute(Js.Controls.CommentsDisplay._properties.deleteConfirmText, comment.deleteLinkOnClickConfirmText);
            deleteAnchor.setAttribute(Js.Controls.CommentsDisplay._properties.commentK, comment.k);
            small.appendChild(deleteAnchor);
        }
        small.appendChild(document.createElement('br'));
        var postDetails = document.createElement('span');
        this._createMouseOverAndOut($(postDetails), "stt('" + comment.k + "');", 'htm();');
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
    
    _createMouseOverAndOut: function Js_Controls_CommentsDisplay_Controller$_createMouseOverAndOut(element, mouseover, mouseout) {
        /// <param name="element" type="jQueryObject">
        /// </param>
        /// <param name="mouseover" type="String">
        /// </param>
        /// <param name="mouseout" type="String">
        /// </param>
        element.removeAttr('onmouseover').removeAttr('onmouseout').mouseover(function(e) {
            eval(mouseover);
        }).mouseout(function(e) {
            eval(mouseout);
        });
    },
    
    _lolClick: function Js_Controls_CommentsDisplay_Controller$_lolClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        var commentK = e.target.getAttribute(Js.Controls.CommentsDisplay._properties.commentK);
        Js.Controls.CommentsDisplay.Service.lolAtComment(commentK, ss.Delegate.create(this, this._lolAtCommentSuccess), Js.Library.Trace.webServiceFailure, commentK, -1);
    },
    
    _lolAtCommentSuccess: function Js_Controls_CommentsDisplay_Controller$_lolAtCommentSuccess(newLolHtml, commentK, methodName) {
        /// <param name="newLolHtml" type="String">
        /// </param>
        /// <param name="commentK" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this._updateLols(commentK, newLolHtml);
    },
    
    _quoteMouseDown: function Js_Controls_CommentsDisplay_Controller$_quoteMouseDown(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        var usrK = e.target.getAttribute(Js.Controls.CommentsDisplay._properties.usrK);
        eval('QuoteNow(' + usrK.toString() + ');');
    },
    
    _quoteClick: function Js_Controls_CommentsDisplay_Controller$_quoteClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        eval('FocusNow();');
    },
    
    _deleteClick: function Js_Controls_CommentsDisplay_Controller$_deleteClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        Js.Library.Misc.showWaitingCursor();
        if (confirm(e.target.getAttribute(Js.Controls.CommentsDisplay._properties.deleteConfirmText))) {
            var commentK = e.target.getAttribute(Js.Controls.CommentsDisplay._properties.commentK);
            Js.Controls.CommentsDisplay.Service.deleteComment(commentK, ss.Delegate.create(this, this._deleteCommentSuccess), ss.Delegate.create(this, this._deleteCommentFailure), commentK, -1);
        }
    },
    
    onThreadDeleted: null,
    
    _deleteCommentSuccess: function Js_Controls_CommentsDisplay_Controller$_deleteCommentSuccess(commentDeleted, commentK, methodName) {
        /// <param name="commentDeleted" type="Boolean">
        /// </param>
        /// <param name="commentK" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
        if (commentDeleted) {
            if (this.threadCommentsProvider.get_totalComments() === 1) {
                if (this.onThreadDeleted != null) {
                    this.onThreadDeleted(this, new Js.Library.IntEventArgs(this.threadCommentsProvider.get_threadK()));
                }
                this.threadCommentsProvider.set_threadK(0);
                this._view.get_uiCommentsPanel().style.display = 'none';
            }
            this.threadCommentsProvider.decrementCurrentThreadTotalComments();
            this.threadCommentsProvider.reloadComments();
        }
    },
    
    _deleteCommentFailure: function Js_Controls_CommentsDisplay_Controller$_deleteCommentFailure(error, context, methodName) {
        /// <param name="error" type="Js.Library.WebServiceError">
        /// </param>
        /// <param name="context" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        Js.Library.Misc.hideWaitingCursor();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.CommentsDisplay._properties

Js.Controls.CommentsDisplay._properties = function Js_Controls_CommentsDisplay__properties() {
    /// <field name="deleteConfirmText" type="String" static="true">
    /// </field>
    /// <field name="commentK" type="String" static="true">
    /// </field>
    /// <field name="usrK" type="String" static="true">
    /// </field>
}


Js.Controls.CommentsDisplay.Service.registerClass('Js.Controls.CommentsDisplay.Service');
Js.Controls.CommentsDisplay.ThreadCommentsProvider.registerClass('Js.Controls.CommentsDisplay.ThreadCommentsProvider');
Js.Controls.CommentsDisplay.View.registerClass('Js.Controls.CommentsDisplay.View');
Js.Controls.CommentsDisplay.CommentStub.registerClass('Js.Controls.CommentsDisplay.CommentStub');
Js.Controls.CommentsDisplay.CommentResult.registerClass('Js.Controls.CommentsDisplay.CommentResult');
Js.Controls.CommentsDisplay.Controller.registerClass('Js.Controls.CommentsDisplay.Controller');
Js.Controls.CommentsDisplay._properties.registerClass('Js.Controls.CommentsDisplay._properties');
Js.Controls.CommentsDisplay._properties.deleteConfirmText = 'ConfirmText';
Js.Controls.CommentsDisplay._properties.commentK = 'CommentK';
Js.Controls.CommentsDisplay._properties.usrK = 'UsrK';
})(jQuery);

//! This script was generated using Script# v0.7.4.0
