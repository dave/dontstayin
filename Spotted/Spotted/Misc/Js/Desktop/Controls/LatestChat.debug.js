//! LatestChat.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.LatestChat');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.LatestChat.Service

Js.Controls.LatestChat.Service = function Js_Controls_LatestChat_Service() {
}
Js.Controls.LatestChat.Service.getThreads = function Js_Controls_LatestChat_Service$getThreads(objectType, objectK, threadsCount, hasGroupObjectFilter, success, failure, userContext, timeout) {
    /// <param name="objectType" type="Number" integer="true">
    /// </param>
    /// <param name="objectK" type="Number" integer="true">
    /// </param>
    /// <param name="threadsCount" type="Number" integer="true">
    /// </param>
    /// <param name="hasGroupObjectFilter" type="Boolean">
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
    p['objectType'] = objectType;
    p['objectK'] = objectK;
    p['threadsCount'] = threadsCount;
    p['hasGroupObjectFilter'] = hasGroupObjectFilter;
    var o = Js.Library.WebServiceHelper.options('GetThreads', '/WebServices/Controls/LatestChat/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetThreads');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.LatestChat.ThreadStub

Js.Controls.LatestChat.ThreadStub = function Js_Controls_LatestChat_ThreadStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="watchingHtml" type="String">
    /// </field>
    /// <field name="watchingScript" type="String">
    /// </field>
    /// <field name="favouriteHtml" type="String">
    /// </field>
    /// <field name="favouriteScript" type="String">
    /// </field>
    /// <field name="threadUrlSimple" type="String">
    /// </field>
    /// <field name="iconsHtml" type="String">
    /// </field>
    /// <field name="commentHtmlStart" type="String">
    /// </field>
    /// <field name="rollover" type="String">
    /// </field>
    /// <field name="subjectSafe" type="String">
    /// </field>
    /// <field name="commentHtmlEnd" type="String">
    /// </field>
    /// <field name="pagesHtml" type="String">
    /// </field>
    /// <field name="authorHtml" type="String">
    /// </field>
    /// <field name="repliesHtml" type="String">
    /// </field>
}
Js.Controls.LatestChat.ThreadStub.prototype = {
    k: 0,
    watchingHtml: null,
    watchingScript: null,
    favouriteHtml: null,
    favouriteScript: null,
    threadUrlSimple: null,
    iconsHtml: null,
    commentHtmlStart: null,
    rollover: null,
    subjectSafe: null,
    commentHtmlEnd: null,
    pagesHtml: null,
    authorHtml: null,
    repliesHtml: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.LatestChat.Controller

Js.Controls.LatestChat.Controller = function Js_Controls_LatestChat_Controller(view) {
    /// <param name="view" type="Js.Controls.LatestChat.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.LatestChat.View">
    /// </field>
    /// <field name="_latestThreadsProvider" type="Js.Controls.LatestChat._latestThreadsProvider">
    /// </field>
    this._view = view;
    this._latestThreadsProvider = new Js.Controls.LatestChat._latestThreadsProvider(parseInt(view.get_uiThreadsCount().value), Boolean.parse(view.get_uiHasGroupObjectFilter().value), parseInt(view.get_uiObjectType().value));
    this._latestThreadsProvider.onLoaded = ss.Delegate.create(this, this._loaded);
}
Js.Controls.LatestChat.Controller._createRow = function Js_Controls_LatestChat_Controller$_createRow(thread) {
    /// <param name="thread" type="Js.Controls.LatestChat.ThreadStub">
    /// </param>
    /// <returns type="Object" domElement="true"></returns>
    var tr = document.createElement('tr');
    tr.style.verticalAlign = 'top';
    tr.appendChild(Js.Controls.LatestChat.Controller._createTableCell(thread.watchingHtml, 'dataGridThreadTitlesTight', null));
    tr.appendChild(Js.Controls.LatestChat.Controller._createTableCell(thread.favouriteHtml, 'dataGridThreadTitlesTight', null));
    tr.appendChild(Js.Controls.LatestChat.Controller._createTableCell(thread.iconsHtml + thread.commentHtmlStart + '<a href="' + thread.threadUrlSimple + '" ' + thread.rollover + '>' + thread.subjectSafe + '</a>' + thread.commentHtmlEnd + thread.pagesHtml, 'dataGridThreadTitles', null));
    tr.childNodes[2].style.width = '100%';
    tr.appendChild(Js.Controls.LatestChat.Controller._createTableCell('<small>' + thread.authorHtml + '</small>', 'dataGridThread', '3px'));
    tr.appendChild(Js.Controls.LatestChat.Controller._createTableCell('<small>' + thread.repliesHtml + '</small>', 'dataGridThread', '3px'));
    return tr;
}
Js.Controls.LatestChat.Controller._createHeader = function Js_Controls_LatestChat_Controller$_createHeader() {
    /// <returns type="Object" domElement="true"></returns>
    var th = document.createElement('tr');
    th.className = 'dataGridHeader';
    var td = document.createElement('td');
    td.colSpan = 3;
    th.appendChild(td);
    th.appendChild(Js.Controls.LatestChat.Controller._createTableCell('Author', null, null));
    th.appendChild(Js.Controls.LatestChat.Controller._createTableCell('Replies&nbsp;/&nbsp;last', null, null));
    return th;
}
Js.Controls.LatestChat.Controller._createTableCell = function Js_Controls_LatestChat_Controller$_createTableCell(innerHTML, className, cellPadding) {
    /// <param name="innerHTML" type="String">
    /// </param>
    /// <param name="className" type="String">
    /// </param>
    /// <param name="cellPadding" type="String">
    /// </param>
    /// <returns type="Object" domElement="true"></returns>
    var td = document.createElement('td');
    td.innerHTML = innerHTML;
    if (cellPadding != null) {
        td.style.padding = cellPadding;
    }
    if (className != null) {
        td.className = className;
    }
    return td;
}
Js.Controls.LatestChat.Controller.prototype = {
    _view: null,
    _latestThreadsProvider: null,
    
    get__objectK: function Js_Controls_LatestChat_Controller$get__objectK() {
        /// <value type="Number" integer="true"></value>
        try {
            return parseInt(this._view.get_uiObjectK().value);
        }
        catch ($e1) {
            return 0;
        }
    },
    set__objectK: function Js_Controls_LatestChat_Controller$set__objectK(value) {
        /// <value type="Number" integer="true"></value>
        this._view.get_uiObjectK().value = value.toString();
        return value;
    },
    
    hide: function Js_Controls_LatestChat_Controller$hide() {
        this._view.get_holder().style.display = 'none';
    },
    
    show: function Js_Controls_LatestChat_Controller$show(objectK) {
        /// <param name="objectK" type="Number" integer="true">
        /// </param>
        this.set__objectK(objectK);
        this._latestThreadsProvider.loadThreads(this.get__objectK());
    },
    
    update: function Js_Controls_LatestChat_Controller$update(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._latestThreadsProvider.reloadThreads(this.get__objectK());
    },
    
    _loaded: function Js_Controls_LatestChat_Controller$_loaded(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        this._displayThreads();
    },
    
    _displayThreads: function Js_Controls_LatestChat_Controller$_displayThreads() {
        var threads = this._latestThreadsProvider.get_currentThreads();
        if (threads.length > 0) {
            this._removeAllChildren(this._view.get_threadsPanel());
            this._view.get_threadsPanel().appendChild(this._createTable(threads));
            for (var i = 0; i < threads.length; i++) {
                eval(threads[i].watchingScript);
                eval(threads[i].favouriteScript);
            }
            this._view.get_holder().style.display = '';
            this._view.get_threadsPanel().style.display = '';
        }
    },
    
    _removeAllChildren: function Js_Controls_LatestChat_Controller$_removeAllChildren(dOMElement) {
        /// <param name="dOMElement" type="Object" domElement="true">
        /// </param>
        dOMElement.innerHTML = '';
    },
    
    _createTable: function Js_Controls_LatestChat_Controller$_createTable(threads) {
        /// <param name="threads" type="Array" elementType="ThreadStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var t = document.createElement('table');
        t.style.border = '0px';
        t.style.width = '100%';
        t.style.borderCollapse = 'collapse';
        var tb = document.createElement('tbody');
        t.appendChild(tb);
        tb.appendChild(Js.Controls.LatestChat.Controller._createHeader());
        for (var i = 0; i < threads.length; i++) {
            tb.appendChild(Js.Controls.LatestChat.Controller._createRow(threads[i]));
        }
        return t;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.LatestChat.View

Js.Controls.LatestChat.View = function Js_Controls_LatestChat_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_Holder" type="Object" domElement="true">
    /// </field>
    /// <field name="_HolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_Header" type="Object" domElement="true">
    /// </field>
    /// <field name="_HeaderJ" type="jQueryObject">
    /// </field>
    /// <field name="_InnerHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_InnerHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_ThreadsNoPermissionPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_ThreadsNoPermissionPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_ThreadsNoPermissionJoinAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_ThreadsNoPermissionJoinAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_ThreadsPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_ThreadsPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_BrandChatControlsP" type="Object" domElement="true">
    /// </field>
    /// <field name="_BrandChatControlsPJ" type="jQueryObject">
    /// </field>
    /// <field name="_ShowGroupChatEnabled" type="Object" domElement="true">
    /// </field>
    /// <field name="_ShowGroupChatEnabledJ" type="jQueryObject">
    /// </field>
    /// <field name="_ShowGroupChatLinkButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_ShowGroupChatLinkButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_ShowBrandChatLinkButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_ShowBrandChatLinkButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_ShowBrandChatEnabled" type="Object" domElement="true">
    /// </field>
    /// <field name="_ShowBrandChatEnabledJ" type="jQueryObject">
    /// </field>
    /// <field name="_InlineScript1" type="Object" domElement="true">
    /// </field>
    /// <field name="_InlineScript1J" type="jQueryObject">
    /// </field>
    /// <field name="_ThreadsDataGrid" type="Object" domElement="true">
    /// </field>
    /// <field name="_ThreadsDataGridJ" type="jQueryObject">
    /// </field>
    /// <field name="_CommentsFooter" type="Object" domElement="true">
    /// </field>
    /// <field name="_CommentsFooterJ" type="jQueryObject">
    /// </field>
    /// <field name="_MoreThreadsAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_MoreThreadsAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_MoreThreadsCountLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_MoreThreadsCountLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiObjectType" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiObjectTypeJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiObjectK" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiObjectKJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiThreadsCount" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiThreadsCountJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiHasGroupObjectFilter" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiHasGroupObjectFilterJ" type="jQueryObject">
    /// </field>
    /// <field name="_ExternalHeader" type="Object" domElement="true">
    /// </field>
    /// <field name="_ExternalHeaderJ" type="jQueryObject">
    /// </field>
    /// <field name="_ExternalHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_ExternalHolderJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.LatestChat.View.prototype = {
    clientId: null,
    
    get_holder: function Js_Controls_LatestChat_View$get_holder() {
        /// <value type="Object" domElement="true"></value>
        if (this._Holder == null) {
            this._Holder = document.getElementById(this.clientId + '_Holder');
        }
        return this._Holder;
    },
    
    _Holder: null,
    
    get_holderJ: function Js_Controls_LatestChat_View$get_holderJ() {
        /// <value type="jQueryObject"></value>
        if (this._HolderJ == null) {
            this._HolderJ = $('#' + this.clientId + '_Holder');
        }
        return this._HolderJ;
    },
    
    _HolderJ: null,
    
    get_header: function Js_Controls_LatestChat_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        if (this._Header == null) {
            this._Header = document.getElementById(this.clientId + '_Header');
        }
        return this._Header;
    },
    
    _Header: null,
    
    get_headerJ: function Js_Controls_LatestChat_View$get_headerJ() {
        /// <value type="jQueryObject"></value>
        if (this._HeaderJ == null) {
            this._HeaderJ = $('#' + this.clientId + '_Header');
        }
        return this._HeaderJ;
    },
    
    _HeaderJ: null,
    
    get_innerHolder: function Js_Controls_LatestChat_View$get_innerHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._InnerHolder == null) {
            this._InnerHolder = document.getElementById(this.clientId + '_InnerHolder');
        }
        return this._InnerHolder;
    },
    
    _InnerHolder: null,
    
    get_innerHolderJ: function Js_Controls_LatestChat_View$get_innerHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._InnerHolderJ == null) {
            this._InnerHolderJ = $('#' + this.clientId + '_InnerHolder');
        }
        return this._InnerHolderJ;
    },
    
    _InnerHolderJ: null,
    
    get_threadsNoPermissionPanel: function Js_Controls_LatestChat_View$get_threadsNoPermissionPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._ThreadsNoPermissionPanel == null) {
            this._ThreadsNoPermissionPanel = document.getElementById(this.clientId + '_ThreadsNoPermissionPanel');
        }
        return this._ThreadsNoPermissionPanel;
    },
    
    _ThreadsNoPermissionPanel: null,
    
    get_threadsNoPermissionPanelJ: function Js_Controls_LatestChat_View$get_threadsNoPermissionPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._ThreadsNoPermissionPanelJ == null) {
            this._ThreadsNoPermissionPanelJ = $('#' + this.clientId + '_ThreadsNoPermissionPanel');
        }
        return this._ThreadsNoPermissionPanelJ;
    },
    
    _ThreadsNoPermissionPanelJ: null,
    
    get_threadsNoPermissionJoinAnchor: function Js_Controls_LatestChat_View$get_threadsNoPermissionJoinAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._ThreadsNoPermissionJoinAnchor == null) {
            this._ThreadsNoPermissionJoinAnchor = document.getElementById(this.clientId + '_ThreadsNoPermissionJoinAnchor');
        }
        return this._ThreadsNoPermissionJoinAnchor;
    },
    
    _ThreadsNoPermissionJoinAnchor: null,
    
    get_threadsNoPermissionJoinAnchorJ: function Js_Controls_LatestChat_View$get_threadsNoPermissionJoinAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._ThreadsNoPermissionJoinAnchorJ == null) {
            this._ThreadsNoPermissionJoinAnchorJ = $('#' + this.clientId + '_ThreadsNoPermissionJoinAnchor');
        }
        return this._ThreadsNoPermissionJoinAnchorJ;
    },
    
    _ThreadsNoPermissionJoinAnchorJ: null,
    
    get_threadsPanel: function Js_Controls_LatestChat_View$get_threadsPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._ThreadsPanel == null) {
            this._ThreadsPanel = document.getElementById(this.clientId + '_ThreadsPanel');
        }
        return this._ThreadsPanel;
    },
    
    _ThreadsPanel: null,
    
    get_threadsPanelJ: function Js_Controls_LatestChat_View$get_threadsPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._ThreadsPanelJ == null) {
            this._ThreadsPanelJ = $('#' + this.clientId + '_ThreadsPanel');
        }
        return this._ThreadsPanelJ;
    },
    
    _ThreadsPanelJ: null,
    
    get_brandChatControlsP: function Js_Controls_LatestChat_View$get_brandChatControlsP() {
        /// <value type="Object" domElement="true"></value>
        if (this._BrandChatControlsP == null) {
            this._BrandChatControlsP = document.getElementById(this.clientId + '_BrandChatControlsP');
        }
        return this._BrandChatControlsP;
    },
    
    _BrandChatControlsP: null,
    
    get_brandChatControlsPJ: function Js_Controls_LatestChat_View$get_brandChatControlsPJ() {
        /// <value type="jQueryObject"></value>
        if (this._BrandChatControlsPJ == null) {
            this._BrandChatControlsPJ = $('#' + this.clientId + '_BrandChatControlsP');
        }
        return this._BrandChatControlsPJ;
    },
    
    _BrandChatControlsPJ: null,
    
    get_showGroupChatEnabled: function Js_Controls_LatestChat_View$get_showGroupChatEnabled() {
        /// <value type="Object" domElement="true"></value>
        if (this._ShowGroupChatEnabled == null) {
            this._ShowGroupChatEnabled = document.getElementById(this.clientId + '_ShowGroupChatEnabled');
        }
        return this._ShowGroupChatEnabled;
    },
    
    _ShowGroupChatEnabled: null,
    
    get_showGroupChatEnabledJ: function Js_Controls_LatestChat_View$get_showGroupChatEnabledJ() {
        /// <value type="jQueryObject"></value>
        if (this._ShowGroupChatEnabledJ == null) {
            this._ShowGroupChatEnabledJ = $('#' + this.clientId + '_ShowGroupChatEnabled');
        }
        return this._ShowGroupChatEnabledJ;
    },
    
    _ShowGroupChatEnabledJ: null,
    
    get_showGroupChatLinkButton: function Js_Controls_LatestChat_View$get_showGroupChatLinkButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._ShowGroupChatLinkButton == null) {
            this._ShowGroupChatLinkButton = document.getElementById(this.clientId + '_ShowGroupChatLinkButton');
        }
        return this._ShowGroupChatLinkButton;
    },
    
    _ShowGroupChatLinkButton: null,
    
    get_showGroupChatLinkButtonJ: function Js_Controls_LatestChat_View$get_showGroupChatLinkButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._ShowGroupChatLinkButtonJ == null) {
            this._ShowGroupChatLinkButtonJ = $('#' + this.clientId + '_ShowGroupChatLinkButton');
        }
        return this._ShowGroupChatLinkButtonJ;
    },
    
    _ShowGroupChatLinkButtonJ: null,
    
    get_showBrandChatLinkButton: function Js_Controls_LatestChat_View$get_showBrandChatLinkButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._ShowBrandChatLinkButton == null) {
            this._ShowBrandChatLinkButton = document.getElementById(this.clientId + '_ShowBrandChatLinkButton');
        }
        return this._ShowBrandChatLinkButton;
    },
    
    _ShowBrandChatLinkButton: null,
    
    get_showBrandChatLinkButtonJ: function Js_Controls_LatestChat_View$get_showBrandChatLinkButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._ShowBrandChatLinkButtonJ == null) {
            this._ShowBrandChatLinkButtonJ = $('#' + this.clientId + '_ShowBrandChatLinkButton');
        }
        return this._ShowBrandChatLinkButtonJ;
    },
    
    _ShowBrandChatLinkButtonJ: null,
    
    get_showBrandChatEnabled: function Js_Controls_LatestChat_View$get_showBrandChatEnabled() {
        /// <value type="Object" domElement="true"></value>
        if (this._ShowBrandChatEnabled == null) {
            this._ShowBrandChatEnabled = document.getElementById(this.clientId + '_ShowBrandChatEnabled');
        }
        return this._ShowBrandChatEnabled;
    },
    
    _ShowBrandChatEnabled: null,
    
    get_showBrandChatEnabledJ: function Js_Controls_LatestChat_View$get_showBrandChatEnabledJ() {
        /// <value type="jQueryObject"></value>
        if (this._ShowBrandChatEnabledJ == null) {
            this._ShowBrandChatEnabledJ = $('#' + this.clientId + '_ShowBrandChatEnabled');
        }
        return this._ShowBrandChatEnabledJ;
    },
    
    _ShowBrandChatEnabledJ: null,
    
    get_inlineScript1: function Js_Controls_LatestChat_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        if (this._InlineScript1 == null) {
            this._InlineScript1 = document.getElementById(this.clientId + '_InlineScript1');
        }
        return this._InlineScript1;
    },
    
    _InlineScript1: null,
    
    get_inlineScript1J: function Js_Controls_LatestChat_View$get_inlineScript1J() {
        /// <value type="jQueryObject"></value>
        if (this._InlineScript1J == null) {
            this._InlineScript1J = $('#' + this.clientId + '_InlineScript1');
        }
        return this._InlineScript1J;
    },
    
    _InlineScript1J: null,
    
    get_threadsDataGrid: function Js_Controls_LatestChat_View$get_threadsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        if (this._ThreadsDataGrid == null) {
            this._ThreadsDataGrid = document.getElementById(this.clientId + '_ThreadsDataGrid');
        }
        return this._ThreadsDataGrid;
    },
    
    _ThreadsDataGrid: null,
    
    get_threadsDataGridJ: function Js_Controls_LatestChat_View$get_threadsDataGridJ() {
        /// <value type="jQueryObject"></value>
        if (this._ThreadsDataGridJ == null) {
            this._ThreadsDataGridJ = $('#' + this.clientId + '_ThreadsDataGrid');
        }
        return this._ThreadsDataGridJ;
    },
    
    _ThreadsDataGridJ: null,
    
    get_commentsFooter: function Js_Controls_LatestChat_View$get_commentsFooter() {
        /// <value type="Object" domElement="true"></value>
        if (this._CommentsFooter == null) {
            this._CommentsFooter = document.getElementById(this.clientId + '_CommentsFooter');
        }
        return this._CommentsFooter;
    },
    
    _CommentsFooter: null,
    
    get_commentsFooterJ: function Js_Controls_LatestChat_View$get_commentsFooterJ() {
        /// <value type="jQueryObject"></value>
        if (this._CommentsFooterJ == null) {
            this._CommentsFooterJ = $('#' + this.clientId + '_CommentsFooter');
        }
        return this._CommentsFooterJ;
    },
    
    _CommentsFooterJ: null,
    
    get_moreThreadsAnchor: function Js_Controls_LatestChat_View$get_moreThreadsAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._MoreThreadsAnchor == null) {
            this._MoreThreadsAnchor = document.getElementById(this.clientId + '_MoreThreadsAnchor');
        }
        return this._MoreThreadsAnchor;
    },
    
    _MoreThreadsAnchor: null,
    
    get_moreThreadsAnchorJ: function Js_Controls_LatestChat_View$get_moreThreadsAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._MoreThreadsAnchorJ == null) {
            this._MoreThreadsAnchorJ = $('#' + this.clientId + '_MoreThreadsAnchor');
        }
        return this._MoreThreadsAnchorJ;
    },
    
    _MoreThreadsAnchorJ: null,
    
    get_moreThreadsCountLabel: function Js_Controls_LatestChat_View$get_moreThreadsCountLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._MoreThreadsCountLabel == null) {
            this._MoreThreadsCountLabel = document.getElementById(this.clientId + '_MoreThreadsCountLabel');
        }
        return this._MoreThreadsCountLabel;
    },
    
    _MoreThreadsCountLabel: null,
    
    get_moreThreadsCountLabelJ: function Js_Controls_LatestChat_View$get_moreThreadsCountLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._MoreThreadsCountLabelJ == null) {
            this._MoreThreadsCountLabelJ = $('#' + this.clientId + '_MoreThreadsCountLabel');
        }
        return this._MoreThreadsCountLabelJ;
    },
    
    _MoreThreadsCountLabelJ: null,
    
    get_uiObjectType: function Js_Controls_LatestChat_View$get_uiObjectType() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiObjectType == null) {
            this._uiObjectType = document.getElementById(this.clientId + '_uiObjectType');
        }
        return this._uiObjectType;
    },
    
    _uiObjectType: null,
    
    get_uiObjectTypeJ: function Js_Controls_LatestChat_View$get_uiObjectTypeJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiObjectTypeJ == null) {
            this._uiObjectTypeJ = $('#' + this.clientId + '_uiObjectType');
        }
        return this._uiObjectTypeJ;
    },
    
    _uiObjectTypeJ: null,
    
    get_uiObjectK: function Js_Controls_LatestChat_View$get_uiObjectK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiObjectK == null) {
            this._uiObjectK = document.getElementById(this.clientId + '_uiObjectK');
        }
        return this._uiObjectK;
    },
    
    _uiObjectK: null,
    
    get_uiObjectKJ: function Js_Controls_LatestChat_View$get_uiObjectKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiObjectKJ == null) {
            this._uiObjectKJ = $('#' + this.clientId + '_uiObjectK');
        }
        return this._uiObjectKJ;
    },
    
    _uiObjectKJ: null,
    
    get_uiThreadsCount: function Js_Controls_LatestChat_View$get_uiThreadsCount() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiThreadsCount == null) {
            this._uiThreadsCount = document.getElementById(this.clientId + '_uiThreadsCount');
        }
        return this._uiThreadsCount;
    },
    
    _uiThreadsCount: null,
    
    get_uiThreadsCountJ: function Js_Controls_LatestChat_View$get_uiThreadsCountJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiThreadsCountJ == null) {
            this._uiThreadsCountJ = $('#' + this.clientId + '_uiThreadsCount');
        }
        return this._uiThreadsCountJ;
    },
    
    _uiThreadsCountJ: null,
    
    get_uiHasGroupObjectFilter: function Js_Controls_LatestChat_View$get_uiHasGroupObjectFilter() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiHasGroupObjectFilter == null) {
            this._uiHasGroupObjectFilter = document.getElementById(this.clientId + '_uiHasGroupObjectFilter');
        }
        return this._uiHasGroupObjectFilter;
    },
    
    _uiHasGroupObjectFilter: null,
    
    get_uiHasGroupObjectFilterJ: function Js_Controls_LatestChat_View$get_uiHasGroupObjectFilterJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiHasGroupObjectFilterJ == null) {
            this._uiHasGroupObjectFilterJ = $('#' + this.clientId + '_uiHasGroupObjectFilter');
        }
        return this._uiHasGroupObjectFilterJ;
    },
    
    _uiHasGroupObjectFilterJ: null,
    
    get_externalHeader: function Js_Controls_LatestChat_View$get_externalHeader() {
        /// <value type="Object" domElement="true"></value>
        if (this._ExternalHeader == null) {
            this._ExternalHeader = document.getElementById(this.clientId + '_ExternalHeader');
        }
        return this._ExternalHeader;
    },
    
    _ExternalHeader: null,
    
    get_externalHeaderJ: function Js_Controls_LatestChat_View$get_externalHeaderJ() {
        /// <value type="jQueryObject"></value>
        if (this._ExternalHeaderJ == null) {
            this._ExternalHeaderJ = $('#' + this.clientId + '_ExternalHeader');
        }
        return this._ExternalHeaderJ;
    },
    
    _ExternalHeaderJ: null,
    
    get_externalHolder: function Js_Controls_LatestChat_View$get_externalHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._ExternalHolder == null) {
            this._ExternalHolder = document.getElementById(this.clientId + '_ExternalHolder');
        }
        return this._ExternalHolder;
    },
    
    _ExternalHolder: null,
    
    get_externalHolderJ: function Js_Controls_LatestChat_View$get_externalHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._ExternalHolderJ == null) {
            this._ExternalHolderJ = $('#' + this.clientId + '_ExternalHolder');
        }
        return this._ExternalHolderJ;
    },
    
    _ExternalHolderJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.LatestChat._latestThreadsProvider

Js.Controls.LatestChat._latestThreadsProvider = function Js_Controls_LatestChat__latestThreadsProvider(threadsCount, hasGroupObjectFilter, objectType) {
    /// <param name="threadsCount" type="Number" integer="true">
    /// </param>
    /// <param name="hasGroupObjectFilter" type="Boolean">
    /// </param>
    /// <param name="objectType" type="Number" integer="true">
    /// </param>
    /// <field name="_threadsCount" type="Number" integer="true">
    /// </field>
    /// <field name="_hasGroupObjectFilter" type="Boolean">
    /// </field>
    /// <field name="_objectType" type="Number" integer="true">
    /// </field>
    /// <field name="_threads" type="Array">
    /// </field>
    /// <field name="_objectK" type="Number" integer="true">
    /// </field>
    /// <field name="onLoaded" type="Function">
    /// </field>
    this._threadsCount = threadsCount;
    this._hasGroupObjectFilter = hasGroupObjectFilter;
    this._objectType = objectType;
    this._threads = [];
}
Js.Controls.LatestChat._latestThreadsProvider.prototype = {
    _threadsCount: 0,
    _hasGroupObjectFilter: false,
    _objectType: 0,
    _threads: null,
    _objectK: 0,
    onLoaded: null,
    
    get_currentThreads: function Js_Controls_LatestChat__latestThreadsProvider$get_currentThreads() {
        /// <value type="Array" elementType="ThreadStub"></value>
        return this._threads[this._objectK];
    },
    set_currentThreads: function Js_Controls_LatestChat__latestThreadsProvider$set_currentThreads(value) {
        /// <value type="Array" elementType="ThreadStub"></value>
        this._threads[this._objectK] = value;
        return value;
    },
    
    loadThreads: function Js_Controls_LatestChat__latestThreadsProvider$loadThreads(objectK) {
        /// <param name="objectK" type="Number" integer="true">
        /// </param>
        this._objectK = objectK;
        if (this.get_currentThreads() != null) {
            this._loaded();
        }
        else {
            this._loadThreadsViaWebService();
        }
    },
    
    _loadThreadsViaWebService: function Js_Controls_LatestChat__latestThreadsProvider$_loadThreadsViaWebService() {
        Js.Controls.LatestChat.Service.getThreads(this._objectType, this._objectK, this._threadsCount, this._hasGroupObjectFilter, ss.Delegate.create(this, this._getThreadsSuccess), Js.Library.Trace.webServiceFailure, null, -1);
    },
    
    _getThreadsSuccess: function Js_Controls_LatestChat__latestThreadsProvider$_getThreadsSuccess(threads, context, methodName) {
        /// <param name="threads" type="Array" elementType="ThreadStub">
        /// </param>
        /// <param name="context" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.set_currentThreads(threads);
        this._loaded();
    },
    
    _loaded: function Js_Controls_LatestChat__latestThreadsProvider$_loaded() {
        if (this.onLoaded != null) {
            this.onLoaded(this, ss.EventArgs.Empty);
        }
    },
    
    reloadThreads: function Js_Controls_LatestChat__latestThreadsProvider$reloadThreads(objectK) {
        /// <param name="objectK" type="Number" integer="true">
        /// </param>
        this._objectK = objectK;
        this._loadThreadsViaWebService();
    }
}


Js.Controls.LatestChat.Service.registerClass('Js.Controls.LatestChat.Service');
Js.Controls.LatestChat.ThreadStub.registerClass('Js.Controls.LatestChat.ThreadStub');
Js.Controls.LatestChat.Controller.registerClass('Js.Controls.LatestChat.Controller');
Js.Controls.LatestChat.View.registerClass('Js.Controls.LatestChat.View');
Js.Controls.LatestChat._latestThreadsProvider.registerClass('Js.Controls.LatestChat._latestThreadsProvider');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
