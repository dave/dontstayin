Type.registerNamespace('SpottedScript.Controls.LatestChat');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.LatestChat.Controller
SpottedScript.Controls.LatestChat.Controller = function SpottedScript_Controls_LatestChat_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.LatestChat.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.LatestChat.View">
    /// </field>
    /// <field name="_latestThreadsProvider" type="SpottedScript.Controls.LatestChat._latestThreadsProvider">
    /// </field>
    this._view = view;
    this._latestThreadsProvider = new SpottedScript.Controls.LatestChat._latestThreadsProvider(Number.parseInvariant(view.get_uiThreadsCount().value), Boolean.parse(view.get_uiHasGroupObjectFilter().value), Number.parseInvariant(view.get_uiObjectType().value));
    this._latestThreadsProvider._onLoaded = Function.createDelegate(this, this._loaded);
}
SpottedScript.Controls.LatestChat.Controller._createRow = function SpottedScript_Controls_LatestChat_Controller$_createRow(thread) {
    /// <param name="thread" type="SpottedScript.Controls.LatestChat.ThreadStub">
    /// </param>
    /// <returns type="Object" domElement="true"></returns>
    var tr = document.createElement('tr');
    tr.style.verticalAlign = 'top';
    tr.appendChild(SpottedScript.Controls.LatestChat.Controller._createTableCell(thread.watchingHtml, 'dataGridThreadTitlesTight', null));
    tr.appendChild(SpottedScript.Controls.LatestChat.Controller._createTableCell(thread.favouriteHtml, 'dataGridThreadTitlesTight', null));
    tr.appendChild(SpottedScript.Controls.LatestChat.Controller._createTableCell(thread.iconsHtml + thread.commentHtmlStart + '<a href=\"' + thread.threadUrlSimple + '\" ' + thread.rollover + '>' + thread.subjectSafe + '</a>' + thread.commentHtmlEnd + thread.pagesHtml, 'dataGridThreadTitles', null));
    tr.childNodes[2].style.width = '100%';
    tr.appendChild(SpottedScript.Controls.LatestChat.Controller._createTableCell('<small>' + thread.authorHtml + '</small>', 'dataGridThread', '3px'));
    tr.appendChild(SpottedScript.Controls.LatestChat.Controller._createTableCell('<small>' + thread.repliesHtml + '</small>', 'dataGridThread', '3px'));
    return tr;
}
SpottedScript.Controls.LatestChat.Controller._createHeader = function SpottedScript_Controls_LatestChat_Controller$_createHeader() {
    /// <returns type="Object" domElement="true"></returns>
    var th = document.createElement('tr');
    th.className = 'dataGridHeader';
    var td = document.createElement('td');
    td.colSpan = 3;
    th.appendChild(td);
    th.appendChild(SpottedScript.Controls.LatestChat.Controller._createTableCell('Author', null, null));
    th.appendChild(SpottedScript.Controls.LatestChat.Controller._createTableCell('Replies&nbsp;/&nbsp;last', null, null));
    return th;
}
SpottedScript.Controls.LatestChat.Controller._createTableCell = function SpottedScript_Controls_LatestChat_Controller$_createTableCell(innerHTML, className, cellPadding) {
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
SpottedScript.Controls.LatestChat.Controller.prototype = {
    _view: null,
    _latestThreadsProvider: null,
    get__objectK: function SpottedScript_Controls_LatestChat_Controller$get__objectK() {
        /// <value type="Number" integer="true"></value>
        try {
            return Number.parseInvariant(this._view.get_uiObjectK().value);
        }
        catch ($e1) {
            return 0;
        }
    },
    set__objectK: function SpottedScript_Controls_LatestChat_Controller$set__objectK(value) {
        /// <value type="Number" integer="true"></value>
        this._view.get_uiObjectK().value = value.toString();
        return value;
    },
    _hide: function SpottedScript_Controls_LatestChat_Controller$_hide() {
        this._view.get_holder().style.display = 'none';
    },
    _show: function SpottedScript_Controls_LatestChat_Controller$_show(objectK) {
        /// <param name="objectK" type="Number" integer="true">
        /// </param>
        this.set__objectK(objectK);
        this._latestThreadsProvider.loadThreads(this.get__objectK());
    },
    _update: function SpottedScript_Controls_LatestChat_Controller$_update(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this._latestThreadsProvider._reloadThreads(this.get__objectK());
    },
    _loaded: function SpottedScript_Controls_LatestChat_Controller$_loaded(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this._displayThreads();
    },
    _displayThreads: function SpottedScript_Controls_LatestChat_Controller$_displayThreads() {
        var threads = this._latestThreadsProvider.get__currentThreads();
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
    _removeAllChildren: function SpottedScript_Controls_LatestChat_Controller$_removeAllChildren(dOMElement) {
        /// <param name="dOMElement" type="Object" domElement="true">
        /// </param>
        dOMElement.innerHTML = '';
    },
    _createTable: function SpottedScript_Controls_LatestChat_Controller$_createTable(threads) {
        /// <param name="threads" type="Array" elementType="ThreadStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var t = document.createElement('table');
        t.style.border = '0px';
        t.style.width = '100%';
        t.style.borderCollapse = 'collapse';
        var tb = document.createElement('tbody');
        t.appendChild(tb);
        tb.appendChild(SpottedScript.Controls.LatestChat.Controller._createHeader());
        for (var i = 0; i < threads.length; i++) {
            tb.appendChild(SpottedScript.Controls.LatestChat.Controller._createRow(threads[i]));
        }
        return t;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.LatestChat._latestThreadsProvider
SpottedScript.Controls.LatestChat._latestThreadsProvider = function SpottedScript_Controls_LatestChat__latestThreadsProvider(threadsCount, hasGroupObjectFilter, objectType) {
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
    /// <field name="_onLoaded" type="Sys.EventHandler">
    /// </field>
    this._threadsCount = threadsCount;
    this._hasGroupObjectFilter = hasGroupObjectFilter;
    this._objectType = objectType;
    this._threads = [];
}
SpottedScript.Controls.LatestChat._latestThreadsProvider.prototype = {
    _threadsCount: 0,
    _hasGroupObjectFilter: false,
    _objectType: 0,
    _threads: null,
    _objectK: 0,
    _onLoaded: null,
    get__currentThreads: function SpottedScript_Controls_LatestChat__latestThreadsProvider$get__currentThreads() {
        /// <value type="Array" elementType="ThreadStub"></value>
        return this._threads[this._objectK];
    },
    set__currentThreads: function SpottedScript_Controls_LatestChat__latestThreadsProvider$set__currentThreads(value) {
        /// <value type="Array" elementType="ThreadStub"></value>
        this._threads[this._objectK] = value;
        return value;
    },
    loadThreads: function SpottedScript_Controls_LatestChat__latestThreadsProvider$loadThreads(objectK) {
        /// <param name="objectK" type="Number" integer="true">
        /// </param>
        this._objectK = objectK;
        if (this.get__currentThreads() != null) {
            this._loaded();
        }
        else {
            this._loadThreadsViaWebService();
        }
    },
    _loadThreadsViaWebService: function SpottedScript_Controls_LatestChat__latestThreadsProvider$_loadThreadsViaWebService() {
        Spotted.WebServices.Controls.LatestChat.Service.getThreads(this._objectType, this._objectK, this._threadsCount, this._hasGroupObjectFilter, Function.createDelegate(this, this._getThreadsSuccess), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, -1);
    },
    _getThreadsSuccess: function SpottedScript_Controls_LatestChat__latestThreadsProvider$_getThreadsSuccess(threads, context, methodName) {
        /// <param name="threads" type="Array" elementType="ThreadStub">
        /// </param>
        /// <param name="context" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        this.set__currentThreads(threads);
        this._loaded();
    },
    _loaded: function SpottedScript_Controls_LatestChat__latestThreadsProvider$_loaded() {
        if (this._onLoaded != null) {
            this._onLoaded(this, Sys.EventArgs.Empty);
        }
    },
    _reloadThreads: function SpottedScript_Controls_LatestChat__latestThreadsProvider$_reloadThreads(objectK) {
        /// <param name="objectK" type="Number" integer="true">
        /// </param>
        this._objectK = objectK;
        this._loadThreadsViaWebService();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.LatestChat.ThreadStub
SpottedScript.Controls.LatestChat.ThreadStub = function SpottedScript_Controls_LatestChat_ThreadStub() {
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
SpottedScript.Controls.LatestChat.ThreadStub.prototype = {
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
// SpottedScript.Controls.LatestChat.View
SpottedScript.Controls.LatestChat.View = function SpottedScript_Controls_LatestChat_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.LatestChat.View.prototype = {
    clientId: null,
    get_externalHeader: function SpottedScript_Controls_LatestChat_View$get_externalHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExternalHeader');
    },
    get_externalHolder: function SpottedScript_Controls_LatestChat_View$get_externalHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExternalHolder');
    },
    get_holder: function SpottedScript_Controls_LatestChat_View$get_holder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Holder');
    },
    get_header: function SpottedScript_Controls_LatestChat_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_innerHolder: function SpottedScript_Controls_LatestChat_View$get_innerHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InnerHolder');
    },
    get_threadsNoPermissionPanel: function SpottedScript_Controls_LatestChat_View$get_threadsNoPermissionPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNoPermissionPanel');
    },
    get_threadsNoPermissionJoinAnchor: function SpottedScript_Controls_LatestChat_View$get_threadsNoPermissionJoinAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNoPermissionJoinAnchor');
    },
    get_threadsPanel: function SpottedScript_Controls_LatestChat_View$get_threadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPanel');
    },
    get_brandChatControlsP: function SpottedScript_Controls_LatestChat_View$get_brandChatControlsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandChatControlsP');
    },
    get_showGroupChatEnabled: function SpottedScript_Controls_LatestChat_View$get_showGroupChatEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ShowGroupChatEnabled');
    },
    get_showGroupChatLinkButton: function SpottedScript_Controls_LatestChat_View$get_showGroupChatLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ShowGroupChatLinkButton');
    },
    get_showBrandChatLinkButton: function SpottedScript_Controls_LatestChat_View$get_showBrandChatLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ShowBrandChatLinkButton');
    },
    get_showBrandChatEnabled: function SpottedScript_Controls_LatestChat_View$get_showBrandChatEnabled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ShowBrandChatEnabled');
    },
    get_inlineScript1: function SpottedScript_Controls_LatestChat_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript1');
    },
    get_threadsDataGrid: function SpottedScript_Controls_LatestChat_View$get_threadsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsDataGrid');
    },
    get_commentsFooter: function SpottedScript_Controls_LatestChat_View$get_commentsFooter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsFooter');
    },
    get_moreThreadsAnchor: function SpottedScript_Controls_LatestChat_View$get_moreThreadsAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreThreadsAnchor');
    },
    get_moreThreadsCountLabel: function SpottedScript_Controls_LatestChat_View$get_moreThreadsCountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreThreadsCountLabel');
    },
    get_uiObjectType: function SpottedScript_Controls_LatestChat_View$get_uiObjectType() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiObjectType');
    },
    get_uiObjectK: function SpottedScript_Controls_LatestChat_View$get_uiObjectK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiObjectK');
    },
    get_uiThreadsCount: function SpottedScript_Controls_LatestChat_View$get_uiThreadsCount() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiThreadsCount');
    },
    get_uiHasGroupObjectFilter: function SpottedScript_Controls_LatestChat_View$get_uiHasGroupObjectFilter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiHasGroupObjectFilter');
    }
}
SpottedScript.Controls.LatestChat.Controller.registerClass('SpottedScript.Controls.LatestChat.Controller');
SpottedScript.Controls.LatestChat._latestThreadsProvider.registerClass('SpottedScript.Controls.LatestChat._latestThreadsProvider');
SpottedScript.Controls.LatestChat.ThreadStub.registerClass('SpottedScript.Controls.LatestChat.ThreadStub');
SpottedScript.Controls.LatestChat.View.registerClass('SpottedScript.Controls.LatestChat.View');
