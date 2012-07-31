Type.registerNamespace('SpottedScript.Controls.EventBox');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Controller
SpottedScript.Controls.EventBox.Controller = function SpottedScript_Controls_EventBox_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.EventBox.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Controls.EventBox.View">
    /// </field>
    /// <field name="server" type="SpottedScript.Controls.EventBox.ServerClass">
    /// </field>
    /// <field name="initParentObjectType" type="Model.Entities.ObjectType">
    /// </field>
    /// <field name="initParentObjectK" type="Number" integer="true">
    /// </field>
    /// <field name="initMusicTypeK" type="Number" integer="true">
    /// </field>
    /// <field name="initTabType" type="SpottedScript.Controls.EventBox.Shared.TabType">
    /// </field>
    /// <field name="initPageIndex" type="Number" integer="true">
    /// </field>
    /// <field name="currentParentObjectType" type="Model.Entities.ObjectType">
    /// </field>
    /// <field name="currentParentObjectK" type="Number" integer="true">
    /// </field>
    /// <field name="currentMusicTypeK" type="Number" integer="true">
    /// </field>
    /// <field name="currentTabType" type="SpottedScript.Controls.EventBox.Shared.TabType">
    /// </field>
    /// <field name="currentPageIndex" type="Number" integer="true">
    /// </field>
    /// <field name="clientID" type="String">
    /// </field>
    /// <field name="enableEffects" type="Boolean">
    /// </field>
    /// <field name="_eventPageCache" type="Object">
    /// </field>
    /// <field name="eventInfoHolderOuterJQ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="eventInfoHolderOuterElement" type="Object" domElement="true">
    /// </field>
    /// <field name="currentlySelectedEvent" type="SpottedScript.Controls.EventBox.Shared.EventDetails">
    /// </field>
    /// <field name="_animationTaskQueue" type="Object">
    /// </field>
    /// <field name="_animationTasksInProgress" type="Object">
    /// </field>
    /// <field name="_eventIconMouseOverID" type="Number" integer="true">
    /// </field>
    /// <field name="_eventIconMouseOutID" type="Number" integer="true">
    /// </field>
    this._animationTaskQueue = {};
    this._animationTasksInProgress = {};
    Sys.Application.set_enableHistory(true);
    this.view = view;
    this.server = new SpottedScript.Controls.EventBox.ServerClass(this);
    this.server.gotEventPage = Function.createDelegate(this, this.gotEventPage);
    this.server.gotGenericException = Function.createDelegate(this, this._gotGenericException);
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Controls.EventBox.Controller.prototype = {
    view: null,
    server: null,
    initParentObjectType: 0,
    initParentObjectK: 0,
    initMusicTypeK: 0,
    initTabType: 0,
    initPageIndex: 0,
    currentParentObjectType: 0,
    currentParentObjectK: 0,
    currentMusicTypeK: 0,
    currentTabType: 0,
    currentPageIndex: 0,
    clientID: null,
    enableEffects: false,
    _eventPageCache: null,
    eventInfoHolderOuterJQ: null,
    eventInfoHolderOuterElement: null,
    currentlySelectedEvent: null,
    _initialise: function SpottedScript_Controls_EventBox_Controller$_initialise() {
        /// <summary>
        /// Anything that affects the DOM goes in here.
        /// </summary>
        Sys.Application.add_navigate(Function.createDelegate(this, this._application_Navigate));
        this.clientID = this.view.get_initClientID().value;
        this.enableEffects = Boolean.parse(this.view.get_initEnableEffects().value);
        this.eventInfoHolderOuterJQ = jQuery(this.view.get_eventInfoHolderOuter());
        $addHandler(this.view.get_eventIconsNavigationForwardHolder(), 'click', Function.createDelegate(this, this._pageChangeForwardClick));
        $addHandler(this.view.get_eventIconsNavigationBackHolder(), 'click', Function.createDelegate(this, this._pageChangeBackClick));
        $addHandler(this.view.get_musicDropDownControl().view.get_dropDown(), 'change', Function.createDelegate(this, this._musicChangeClick));
        $addHandler(this.view.get_pastEventsTab(), 'click', Function.createDelegate(this, this._tabClickPast));
        $addHandler(this.view.get_futureEventsTab(), 'click', Function.createDelegate(this, this._tabClickFuture));
        $addHandler(this.view.get_ticketsTab(), 'click', Function.createDelegate(this, this._tabClickTickets));
        this._eventPageCache = {};
        var firstPageData = (eval(' [ ' + this.view.get_initFirstPage().value + ' ] '))[0];
        var firstPage = new SpottedScript.Controls.EventBox.Shared.EventPageDetails(this, firstPageData, false);
        firstPage.set_selected(true);
        firstPage.html.initialiseElements(true, false, false, true, true, false, true);
        for (var i = 0; i < firstPage.events.length; i++) {
            firstPage.events[i].changeSelectedState(i === 0, false, '');
        }
        this.currentlySelectedEvent = firstPage.events[0];
        this._eventPageCache[firstPage.getKey()] = firstPage;
        this.initParentObjectType = firstPage.data.parentObjectType;
        this.initParentObjectK = firstPage.data.parentObjectK;
        this.initTabType = firstPage.data.tabType;
        this.initMusicTypeK = firstPage.data.musicTypeK;
        this.initPageIndex = firstPage.data.pageIndex;
        this.currentParentObjectType = this.initParentObjectType;
        this.currentParentObjectK = this.initParentObjectK;
        this.currentTabType = this.initTabType;
        this.currentMusicTypeK = this.initMusicTypeK;
        this.currentPageIndex = this.initPageIndex;
        if (SpottedScript.Misc.get_browserIsIE()) {
            Sys.Application.addHistoryPoint({});
        }
    },
    _application_Navigate: function SpottedScript_Controls_EventBox_Controller$_application_Navigate(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Sys.HistoryEventArgs">
        /// </param>
        if (e.get_state()['EventBox_PageKey'] != null && e.get_state()['EventBox_PageKey'].toString().length > 0) {
            this._restorePageState(e.get_state()['EventBox_PageKey'].toString());
        }
        else {
            this._restorePageState(null);
        }
    },
    _restorePageState: function SpottedScript_Controls_EventBox_Controller$_restorePageState(key) {
        /// <param name="key" type="String">
        /// </param>
        if (key == null) {
            var d = new SpottedScript.Controls.EventBox.Shared.EventPageDetails(this, new SpottedScript.Controls.EventBox.Shared.EventPageStub(this.initParentObjectType, this.initParentObjectK, this.initTabType, this.initMusicTypeK, this.initPageIndex, this.initPageIndex, null), false);
            key = d.getKey();
        }
        if (this.get_currentEventPage().getKey() === key) {
            return;
        }
        this.get_currentEventPage().changeSelectedState(false, false, '');
        var s = SpottedScript.Controls.EventBox.Shared.EventPageDetails.getStubFromKey(key);
        this.currentParentObjectType = s.parentObjectType;
        this.currentParentObjectK = s.parentObjectK;
        this.currentMusicTypeK = s.musicTypeK;
        this.currentTabType = s.tabType;
        this.currentPageIndex = s.pageIndex;
        this.get_currentEventPage().changeSelectedState(true, false, '');
        this.changeEventNow(this.get_currentEventPage().events[0], false, '');
        this.view.get_musicDropDownControl().view.get_dropDown().value = this.currentMusicTypeK.toString();
        this._updateTabsUI();
    },
    _pageChangeForwardClick: function SpottedScript_Controls_EventBox_Controller$_pageChangeForwardClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._pageChange(true);
    },
    _pageChangeBackClick: function SpottedScript_Controls_EventBox_Controller$_pageChangeBackClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._pageChange(false);
    },
    _pageChange: function SpottedScript_Controls_EventBox_Controller$_pageChange(forward) {
        /// <param name="forward" type="Boolean">
        /// </param>
        var newPageIndex = this.currentPageIndex + ((forward) ? 1 : ((this.currentPageIndex === 0) ? 0 : -1));
        if (this.currentPageIndex === newPageIndex) {
            return;
        }
        var movementDirection = (forward) ? 'right' : 'left';
        this.get_currentEventPage().changeSelectedState(false, true, movementDirection);
        this.currentPageIndex = newPageIndex;
        this.get_currentEventPage().changeSelectedState(true, true, movementDirection);
        this.changeEventNow(this.get_currentEventPage().events[0], true, movementDirection);
        var d = {};
        d['EventBox_PageKey'] = this.get_currentEventPage().getKey();
        Sys.Application.addHistoryPoint(d, 'Event box - page ' + (newPageIndex + 1).toString());
    },
    _tabClickPast: function SpottedScript_Controls_EventBox_Controller$_tabClickPast(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._changeTabType(SpottedScript.Controls.EventBox.Shared.TabType.past);
    },
    _tabClickFuture: function SpottedScript_Controls_EventBox_Controller$_tabClickFuture(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._changeTabType(SpottedScript.Controls.EventBox.Shared.TabType.future);
    },
    _tabClickTickets: function SpottedScript_Controls_EventBox_Controller$_tabClickTickets(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._changeTabType(SpottedScript.Controls.EventBox.Shared.TabType.tickets);
    },
    _getTabLocation: function SpottedScript_Controls_EventBox_Controller$_getTabLocation(tab) {
        /// <param name="tab" type="SpottedScript.Controls.EventBox.Shared.TabType">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return (tab === SpottedScript.Controls.EventBox.Shared.TabType.future) ? 1 : (tab === SpottedScript.Controls.EventBox.Shared.TabType.past) ? 2 : 3;
    },
    _changeTabType: function SpottedScript_Controls_EventBox_Controller$_changeTabType(tabType) {
        /// <param name="tabType" type="SpottedScript.Controls.EventBox.Shared.TabType">
        /// </param>
        if (this.currentTabType === tabType) {
            return;
        }
        var currentTab = this._getTabLocation(this.currentTabType);
        var newTab = this._getTabLocation(tabType);
        var movementDirection = 'left';
        if (currentTab < newTab) {
            movementDirection = 'right';
        }
        this.get_currentEventPage().changeSelectedState(false, true, movementDirection);
        this.currentPageIndex = 0;
        this.currentTabType = tabType;
        this.get_currentEventPage().changeSelectedState(true, true, movementDirection);
        this.changeEventNow(this.get_currentEventPage().events[0], true, movementDirection);
        this._updateTabsUI();
        var d = {};
        d['EventBox_PageKey'] = this.get_currentEventPage().getKey();
        Sys.Application.addHistoryPoint(d, 'Event box - ' + ((this.currentTabType === SpottedScript.Controls.EventBox.Shared.TabType.future) ? 'future events' : (this.currentTabType === SpottedScript.Controls.EventBox.Shared.TabType.future) ? 'past events' : 'tickets'));
    },
    _updateTabsUI: function SpottedScript_Controls_EventBox_Controller$_updateTabsUI() {
        this.view.get_futureEventsTab().className = (this.currentTabType === SpottedScript.Controls.EventBox.Shared.TabType.future) ? 'TabbedHeading Selected' : 'TabbedHeading';
        this.view.get_pastEventsTab().className = (this.currentTabType === SpottedScript.Controls.EventBox.Shared.TabType.past) ? 'TabbedHeading Selected' : 'TabbedHeading';
        this.view.get_ticketsTab().className = (this.currentTabType === SpottedScript.Controls.EventBox.Shared.TabType.tickets) ? 'TabbedHeading Selected' : 'TabbedHeading';
    },
    _musicChangeClick: function SpottedScript_Controls_EventBox_Controller$_musicChangeClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        var movementDirection = 'up';
        for (var i = 0; i < this.view.get_musicDropDownControl().view.get_dropDown().options.length; i++) {
            var option = this.view.get_musicDropDownControl().view.get_dropDown().options[i];
            if (option.value === this.currentMusicTypeK.toString()) {
                movementDirection = 'down';
                break;
            }
            if (option.value === this.view.get_musicDropDownControl().view.get_dropDown().value) {
                break;
            }
        }
        this.get_currentEventPage().changeSelectedState(false, true, movementDirection);
        this.currentPageIndex = 0;
        this.currentMusicTypeK = Number.parseInvariant(this.view.get_musicDropDownControl().view.get_dropDown().value);
        this.get_currentEventPage().changeSelectedState(true, true, movementDirection);
        this.changeEventNow(this.get_currentEventPage().events[0], true, movementDirection);
        var d = {};
        d['EventBox_PageKey'] = this.get_currentEventPage().getKey();
        Sys.Application.addHistoryPoint(d, 'Event box - ' + this.view.get_musicDropDownControl().view.get_dropDown().options[this.view.get_musicDropDownControl().view.get_dropDown().selectedIndex].innerHTML);
    },
    performOrQueueAnimationTask: function SpottedScript_Controls_EventBox_Controller$performOrQueueAnimationTask(task, taskType) {
        /// <summary>
        /// task is a Action[]. [0] is the default animation action. Optional [1] is the action to perform if we don't have time to do the animation.
        /// </summary>
        /// <param name="task" type="Array" elementType="Action">
        /// </param>
        /// <param name="taskType" type="String">
        /// </param>
        if (this._animationTasksInProgress[taskType] == null || !this._animationTasksInProgress[taskType]) {
            this._animationTasksInProgress[taskType] = true;
            var action = task[0];
            action();
        }
        else {
            if (this._animationTaskQueue[taskType] != null) {
                var prevTask = this._animationTaskQueue[taskType];
                if (prevTask.length === 2) {
                    var action = prevTask[1];
                    action();
                }
            }
            this._animationTaskQueue[taskType] = task;
        }
    },
    finishedAnimationTask: function SpottedScript_Controls_EventBox_Controller$finishedAnimationTask(taskType) {
        /// <param name="taskType" type="String">
        /// </param>
        if (this._animationTaskQueue[taskType] != null) {
            var task = this._animationTaskQueue[taskType];
            this._animationTasksInProgress[taskType] = true;
            var action = task[0];
            action();
            this._animationTaskQueue[taskType] = null;
        }
        else {
            this._animationTasksInProgress[taskType] = false;
        }
    },
    _eventIconMouseOverID: 0,
    _eventIconMouseOutID: 0,
    eventIconMouseOut: function SpottedScript_Controls_EventBox_Controller$eventIconMouseOut(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._eventIconMouseOutID++;
    },
    eventIconMouseOver: function SpottedScript_Controls_EventBox_Controller$eventIconMouseOver(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._eventIconMouseOverID++;
        var overID = this._eventIconMouseOverID;
        var outID = this._eventIconMouseOutID;
        this.eventIconMouseOverAfterDelay(e, overID, outID);
    },
    eventIconMouseOverAfterDelay: function SpottedScript_Controls_EventBox_Controller$eventIconMouseOverAfterDelay(e, mouseOverID, mouseOutID) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        /// <param name="mouseOverID" type="Number" integer="true">
        /// </param>
        /// <param name="mouseOutID" type="Number" integer="true">
        /// </param>
        if (this._eventIconMouseOverID !== mouseOverID || this._eventIconMouseOutID !== mouseOutID) {
            return;
        }
        var newSelectedEvent = this._findEventFromMouseOverEvent(e);
        if (newSelectedEvent.hasData) {
            this.changeEventNow(newSelectedEvent, true, null);
        }
    },
    changeEventNow: function SpottedScript_Controls_EventBox_Controller$changeEventNow(newSelectedEvent, animate, movementDirection) {
        /// <param name="newSelectedEvent" type="SpottedScript.Controls.EventBox.Shared.EventDetails">
        /// </param>
        /// <param name="animate" type="Boolean">
        /// </param>
        /// <param name="movementDirection" type="String">
        /// </param>
        if (newSelectedEvent != null && !newSelectedEvent.get_selected()) {
            if (movementDirection == null) {
                movementDirection = (this.currentlySelectedEvent == null) ? 'right' : (newSelectedEvent.positionIndex > this.currentlySelectedEvent.positionIndex) ? 'right' : 'left';
            }
            if (this.currentlySelectedEvent != null) {
                this.currentlySelectedEvent.changeSelectedState(false, animate, movementDirection);
            }
            if (newSelectedEvent != null) {
                newSelectedEvent.changeSelectedState(true, animate, movementDirection);
            }
            this.currentlySelectedEvent = newSelectedEvent;
        }
    },
    _findEventFromMouseOverEvent: function SpottedScript_Controls_EventBox_Controller$_findEventFromMouseOverEvent(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        /// <returns type="SpottedScript.Controls.EventBox.Shared.EventDetails"></returns>
        try {
            var el = e.target;
            while (el != null) {
                if (el.id.endsWith('_Icon_Image')) {
                    var parts = e.target.id.split('_');
                    var index = Number.parseInvariant(parts[parts.length - 3]);
                    return this.get_currentEventPage().events[index];
                }
                else if (el.parentNode != null) {
                    el = el.parentNode;
                }
            }
            return null;
        }
        catch ($e1) {
            return null;
        }
    },
    get_currentEventPage: function SpottedScript_Controls_EventBox_Controller$get_currentEventPage() {
        /// <value type="SpottedScript.Controls.EventBox.Shared.EventPageDetails"></value>
        var data = new SpottedScript.Controls.EventBox.Shared.EventPageStub(this.currentParentObjectType, this.currentParentObjectK, this.currentTabType, this.currentMusicTypeK, this.currentPageIndex, this.currentPageIndex, null);
        var key = SpottedScript.Controls.EventBox.Shared.EventPageDetails.getKeyStatic(data);
        if (this._eventPageCache[key] == null) {
            this.server.getEventPage(key);
        }
        if (this._eventPageCache[key] == null) {
            var eventPage = new SpottedScript.Controls.EventBox.Shared.EventPageDetails(this, data, true);
            eventPage.requestInProgress = true;
            eventPage.html.initialiseElements(true, true, false, true, true, false, true);
            this._eventPageCache[key] = eventPage;
        }
        return this._eventPageCache[key];
    },
    gotEventPage: function SpottedScript_Controls_EventBox_Controller$gotEventPage(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        if (o != null) {
            var stub = o;
            var newPage = new SpottedScript.Controls.EventBox.Shared.EventPageDetails(this, stub, false);
            this._updatePage(newPage);
            if (stub.requestedPageIndex !== stub.pageIndex) {
                var requestedStub = new SpottedScript.Controls.EventBox.Shared.EventPageStub(stub.parentObjectType, stub.parentObjectK, stub.tabType, stub.musicTypeK, stub.requestedPageIndex, stub.requestedPageIndex, null);
                var requestedPage = new SpottedScript.Controls.EventBox.Shared.EventPageDetails(this, requestedStub, false);
                this._updatePage(requestedPage);
            }
        }
    },
    _updatePage: function SpottedScript_Controls_EventBox_Controller$_updatePage(newPage) {
        /// <param name="newPage" type="SpottedScript.Controls.EventBox.Shared.EventPageDetails">
        /// </param>
        var key = newPage.getKey();
        if (this._eventPageCache[key] != null) {
            var page = this._eventPageCache[key];
            if (page.hasIncompleteEventData) {
                if (page.get_selected()) {
                    newPage.set_selected(true);
                }
                var selectedEventIsOnThisPage = false;
                for (var i = 0; i < 8; i++) {
                    if (page.events[i].get_selected()) {
                        newPage.events[i].set_selected(true);
                        this.currentlySelectedEvent = newPage.events[i];
                        selectedEventIsOnThisPage = true;
                    }
                }
                newPage.html.initialiseElements(true, false, false, true, false, false, true);
                newPage.html.initialiseElements(false, false, true, true, false, true, true);
                if (selectedEventIsOnThisPage) {
                    if (this.enableEffects) {
                        this.performOrQueueAnimationTask([ Function.createDelegate(this, function() {
                            this.currentlySelectedEvent.html.resizeInfoHolderAnimate();
                        }) ], 'ResizeInfoHolderAnimate');
                    }
                    else {
                        this.currentlySelectedEvent.html.resizeInfoHolderImmediate();
                    }
                }
            }
            else {
                return;
            }
        }
        else {
            newPage.html.initialiseElements(true, true, false, true, true, false, true);
        }
        this._eventPageCache[key] = newPage;
    },
    _gotGenericException: function SpottedScript_Controls_EventBox_Controller$_gotGenericException(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        var a = e;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.ServerClass
SpottedScript.Controls.EventBox.ServerClass = function SpottedScript_Controls_EventBox_ServerClass(controller) {
    /// <param name="controller" type="SpottedScript.Controls.EventBox.Controller">
    /// </param>
    /// <field name="_controller" type="SpottedScript.Controls.EventBox.Controller">
    /// </field>
    /// <field name="gotEventPage" type="Sys.EventHandler">
    /// </field>
    /// <field name="gotGenericException" type="Sys.EventHandler">
    /// </field>
    this._controller = controller;
}
SpottedScript.Controls.EventBox.ServerClass.prototype = {
    _controller: null,
    gotEventPage: null,
    gotGenericException: null,
    getEventPage: function SpottedScript_Controls_EventBox_ServerClass$getEventPage(key) {
        /// <param name="key" type="String">
        /// </param>
        Spotted.WebServices.Controls.EventBox.Service.getEventPage(key, Function.createDelegate(this, this.getEventPageSuccessCallback), Function.createDelegate(this, this.getEventPageFailureCallback), '', 2000);
    },
    getEventPageSuccessCallback: function SpottedScript_Controls_EventBox_ServerClass$getEventPageSuccessCallback(page, userContext, methodName) {
        /// <param name="page" type="SpottedScript.Controls.EventBox.Shared.EventPageStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (page != null) {
            if (this.gotEventPage != null) {
                this.gotEventPage(page, null);
            }
        }
    },
    getEventPageFailureCallback: function SpottedScript_Controls_EventBox_ServerClass$getEventPageFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Sys.Net.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new SpottedScript.Controls.EventBox.GotExceptionEventArgs(error, methodName));
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.GotExceptionEventArgs
SpottedScript.Controls.EventBox.GotExceptionEventArgs = function SpottedScript_Controls_EventBox_GotExceptionEventArgs(error, method) {
    /// <param name="error" type="Sys.Net.WebServiceError">
    /// </param>
    /// <param name="method" type="String">
    /// </param>
    /// <field name="error" type="Sys.Net.WebServiceError">
    /// </field>
    /// <field name="method" type="String">
    /// </field>
    SpottedScript.Controls.EventBox.GotExceptionEventArgs.initializeBase(this);
    this.error = error;
    this.method = method;
}
SpottedScript.Controls.EventBox.GotExceptionEventArgs.prototype = {
    error: null,
    method: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.View
SpottedScript.Controls.EventBox.View = function SpottedScript_Controls_EventBox_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.EventBox.View.prototype = {
    clientId: null,
    get_futureEventsTab: function SpottedScript_Controls_EventBox_View$get_futureEventsTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FutureEventsTab');
    },
    get_pastEventsTab: function SpottedScript_Controls_EventBox_View$get_pastEventsTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PastEventsTab');
    },
    get_ticketsTab: function SpottedScript_Controls_EventBox_View$get_ticketsTab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsTab');
    },
    get_initEnableEffects: function SpottedScript_Controls_EventBox_View$get_initEnableEffects() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitEnableEffects');
    },
    get_initClientID: function SpottedScript_Controls_EventBox_View$get_initClientID() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitClientID');
    },
    get_initFirstPage: function SpottedScript_Controls_EventBox_View$get_initFirstPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitFirstPage');
    },
    get_titleHolder: function SpottedScript_Controls_EventBox_View$get_titleHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TitleHolder');
    },
    get_musicDropDownHolder: function SpottedScript_Controls_EventBox_View$get_musicDropDownHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicDropDownHolder');
    },
    get_musicDropDownControl: function SpottedScript_Controls_EventBox_View$get_musicDropDownControl() {
        /// <value type="SpottedScript.Controls.MusicTypeDropDownList3.Controller"></value>
        return eval(this.clientId + '_MusicDropDownControlController');
    },
    get_eventIconsHolder: function SpottedScript_Controls_EventBox_View$get_eventIconsHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventIconsHolder');
    },
    get_eventIconsNavigationBackHolder: function SpottedScript_Controls_EventBox_View$get_eventIconsNavigationBackHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventIconsNavigationBackHolder');
    },
    get_eventIconsNavigationForwardHolder: function SpottedScript_Controls_EventBox_View$get_eventIconsNavigationForwardHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventIconsNavigationForwardHolder');
    },
    get_eventInfoHolderOuter: function SpottedScript_Controls_EventBox_View$get_eventInfoHolderOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventInfoHolderOuter');
    },
    get_bottomNavigationTitle: function SpottedScript_Controls_EventBox_View$get_bottomNavigationTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BottomNavigationTitle');
    },
    get_bottomNavigationHolder: function SpottedScript_Controls_EventBox_View$get_bottomNavigationHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BottomNavigationHolder');
    }
}
Type.registerNamespace('SpottedScript.Controls.EventBox.Shared');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Shared.TabType
SpottedScript.Controls.EventBox.Shared.TabType = function() { 
    /// <field name="future" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="past" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="tickets" type="Number" integer="true" static="true">
    /// </field>
};
SpottedScript.Controls.EventBox.Shared.TabType.prototype = {
    future: 1, 
    past: 2, 
    tickets: 3
}
SpottedScript.Controls.EventBox.Shared.TabType.registerEnum('SpottedScript.Controls.EventBox.Shared.TabType', false);
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Shared.EventDetails
SpottedScript.Controls.EventBox.Shared.EventDetails = function SpottedScript_Controls_EventBox_Shared_EventDetails(positionIndex, page, e, isLoading) {
    /// <param name="positionIndex" type="Number" integer="true">
    /// </param>
    /// <param name="page" type="SpottedScript.Controls.EventBox.Shared.EventPageDetails">
    /// </param>
    /// <param name="e" type="SpottedScript.Controls.EventBox.Shared.EventStub">
    /// </param>
    /// <param name="isLoading" type="Boolean">
    /// </param>
    /// <field name="controller" type="SpottedScript.Controls.EventBox.Controller">
    /// </field>
    /// <field name="page" type="SpottedScript.Controls.EventBox.Shared.EventPageDetails">
    /// </field>
    /// <field name="parentClientID" type="String">
    /// </field>
    /// <field name="data" type="SpottedScript.Controls.EventBox.Shared.EventStub">
    /// </field>
    /// <field name="html" type="SpottedScript.Controls.EventBox.Shared.EventHtml">
    /// </field>
    /// <field name="positionIndex" type="Number" integer="true">
    /// </field>
    /// <field name="hasData" type="Boolean">
    /// </field>
    /// <field name="isLoading" type="Boolean">
    /// </field>
    /// <field name="_selected" type="Boolean">
    /// </field>
    this.positionIndex = positionIndex;
    this.page = page;
    this.parentClientID = this.page.clientID;
    this.hasData = e != null;
    this.isLoading = isLoading;
    this.controller = this.page.controller;
    this.data = e;
    this.html = new SpottedScript.Controls.EventBox.Shared.EventHtml(this);
}
SpottedScript.Controls.EventBox.Shared.EventDetails.prototype = {
    controller: null,
    page: null,
    parentClientID: null,
    data: null,
    html: null,
    positionIndex: 0,
    hasData: false,
    isLoading: false,
    get_selected: function SpottedScript_Controls_EventBox_Shared_EventDetails$get_selected() {
        /// <value type="Boolean"></value>
        return this._selected;
    },
    set_selected: function SpottedScript_Controls_EventBox_Shared_EventDetails$set_selected(value) {
        /// <value type="Boolean"></value>
        if (this._selected !== value) {
            this._selected = value;
            this.html.updateUI();
        }
        return value;
    },
    _selected: false,
    changeSelectedState: function SpottedScript_Controls_EventBox_Shared_EventDetails$changeSelectedState(state, animate, direction) {
        /// <param name="state" type="Boolean">
        /// </param>
        /// <param name="animate" type="Boolean">
        /// </param>
        /// <param name="direction" type="String">
        /// </param>
        if (this._selected === state) {
            return;
        }
        this._selected = state;
        this.html.updateIconUI();
        if (!state) {
            if (direction === 'left') {
                direction = 'right';
            }
            else if (direction === 'right') {
                direction = 'left';
            }
            else if (direction === 'up') {
                direction = 'down';
            }
            else if (direction === 'down') {
                direction = 'up';
            }
        }
        if (animate && this.controller.enableEffects) {
            if (state) {
                this.controller.performOrQueueAnimationTask([ Function.createDelegate(this, function() {
                    this.html.showInfoAnimate(direction);
                }) ], 'ShowInfoAnimate');
                if (this.hasData) {
                    this.controller.performOrQueueAnimationTask([ Function.createDelegate(this, function() {
                        this.html.resizeInfoHolderAnimate();
                    }) ], 'ResizeInfoHolderAnimate');
                }
            }
            else {
                this.controller.performOrQueueAnimationTask([ Function.createDelegate(this, function() {
                    this.html.hideInfoAnimate(direction);
                }), Function.createDelegate(this, function() {
                    this.html.hideInfoImmediate();
                }) ], 'HideInfoAnimate');
            }
        }
        else {
            if (state) {
                this.html.showInfoImmediate();
                this.html.resizeInfoHolderImmediate();
            }
            else {
                this.html.hideInfoImmediate();
            }
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Shared.EventHtml
SpottedScript.Controls.EventBox.Shared.EventHtml = function SpottedScript_Controls_EventBox_Shared_EventHtml(details) {
    /// <param name="details" type="SpottedScript.Controls.EventBox.Shared.EventDetails">
    /// </param>
    /// <field name="_details" type="SpottedScript.Controls.EventBox.Shared.EventDetails">
    /// </field>
    /// <field name="elementsInitialised" type="Boolean">
    /// </field>
    /// <field name="iconKeylineClientID" type="String">
    /// </field>
    /// <field name="iconImageClientID" type="String">
    /// </field>
    /// <field name="infoHolderInnerClientID" type="String">
    /// </field>
    /// <field name="infoTextHolderClientID" type="String">
    /// </field>
    /// <field name="iconKeylineElement" type="Object" domElement="true">
    /// </field>
    /// <field name="iconImageElement" type="Object" domElement="true">
    /// </field>
    /// <field name="infoHolderInnerElement" type="Object" domElement="true">
    /// </field>
    /// <field name="infoTextHolderElement" type="Object" domElement="true">
    /// </field>
    /// <field name="infoHolderInnerJQ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="infoTextHolderJQ" type="JQ.JQueryObject">
    /// </field>
    this._details = details;
    this.iconKeylineClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Icon_Keyline';
    this.iconImageClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Icon_Image';
    this.infoHolderInnerClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Info_Holder';
    this.infoTextHolderClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Info_TextHolder';
}
SpottedScript.Controls.EventBox.Shared.EventHtml.prototype = {
    _details: null,
    elementsInitialised: false,
    iconKeylineClientID: null,
    iconImageClientID: null,
    infoHolderInnerClientID: null,
    infoTextHolderClientID: null,
    appendHtmlIcon: function SpottedScript_Controls_EventBox_Shared_EventHtml$appendHtmlIcon(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<div');
        sb.appendAttribute('class', 'EventBoxIconHolder');
        sb.append('>');
        sb.append('<div');
        sb.appendAttribute('id', this.iconKeylineClientID);
        sb.appendAttribute('class', (this._details.get_selected()) ? 'EventBoxIconKeyline Selected' : 'EventBoxIconKeyline');
        sb.append('>&nbsp;');
        sb.append('</div>');
        sb.append('<img');
        sb.appendAttribute('id', this.iconImageClientID);
        if (this._details.hasData) {
            sb.appendAttribute('src', SpottedScript.Misc.getPicUrlFromGuid(this._details.data.eventPicGuid));
            sb.appendAttribute('class', 'BorderBlack All EventBoxIconImage');
        }
        else {
            sb.appendAttribute('src', '/gfx/1pix.gif');
        }
        sb.appendAttribute('width', '50');
        sb.appendAttribute('height', '50');
        sb.append('/>');
        sb.append('</div>');
    },
    toHtmlIcon: function SpottedScript_Controls_EventBox_Shared_EventHtml$toHtmlIcon() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        this.appendHtmlIcon(sb);
        return sb.toString();
    },
    appendHtmlInfo: function SpottedScript_Controls_EventBox_Shared_EventHtml$appendHtmlInfo(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<div');
        sb.appendAttribute('id', this.infoHolderInnerClientID);
        sb.appendAttribute('class', 'EventBoxInfoHolderInner');
        sb.append('>');
        this.appendHtmlInfoInner(sb);
        sb.append('</div>');
    },
    appendHtmlInfoInner: function SpottedScript_Controls_EventBox_Shared_EventHtml$appendHtmlInfoInner(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<div');
        sb.appendAttribute('id', this.infoTextHolderClientID);
        sb.appendAttribute('class', 'EventBoxInfoTextHolder');
        sb.append('>');
        if (this._details.hasData) {
            sb.append('<a');
            sb.appendAttribute('href', this._details.data.eventUrl);
            sb.append('>');
            sb.append('<img');
            sb.appendAttribute('src', SpottedScript.Misc.getPicUrlFromGuid(this._details.data.eventPicGuid));
            sb.appendAttribute('class', 'BorderBlack All EventBoxInfoImage');
            sb.appendAttribute('width', '100');
            sb.appendAttribute('height', '100');
            sb.appendAttribute('align', 'right');
            sb.append('/>');
            sb.append('</a>');
            sb.append('<b>');
            sb.append('<a');
            sb.appendAttribute('href', this._details.data.eventUrl);
            sb.append('>');
            sb.append(this._details.data.eventName);
            sb.append('</a>');
            sb.append('</b>');
            sb.append(' @ ');
            sb.append('<a');
            sb.appendAttribute('href', this._details.data.venueUrl);
            sb.append('>');
            sb.append(this._details.data.venueName);
            sb.append('</a>');
            sb.append(' in ');
            sb.append('<a');
            sb.appendAttribute('href', this._details.data.placeUrl);
            sb.append('>');
            sb.append(this._details.data.placeName);
            sb.append('</a>');
            sb.append(', ');
            sb.append(this._details.data.friendlyDateString);
            sb.append('.<br />');
            sb.append(this._details.data.eventShortDescription);
            sb.append('<br />');
            sb.append('(');
            sb.append(this._details.data.eventMusicText);
            sb.append(')');
            sb.append('<br />');
            sb.append(this._details.data.eventMembersAttending.toString());
            sb.append((this._details.data.eventMembersAttending === 1) ? ' member' : ' members');
            sb.append((this._details.data.eventInInTheFuture) ? ' attending' : ' attended');
        }
        else if (this._details.isLoading) {
            sb.append('<center>Loading details...</center>');
        }
        else if (this._details.page.isEmpty) {
            sb.append('<center>Sorry, no events - <a href=\"/pages/events/edit\">click here to add one</a>.</center>');
        }
        sb.append('</div>');
    },
    toHtmlInfo: function SpottedScript_Controls_EventBox_Shared_EventHtml$toHtmlInfo() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        this.appendHtmlInfo(sb);
        return sb.toString();
    },
    toHtmlInfoInner: function SpottedScript_Controls_EventBox_Shared_EventHtml$toHtmlInfoInner() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        this.appendHtmlInfoInner(sb);
        return sb.toString();
    },
    hideInfoImmediate: function SpottedScript_Controls_EventBox_Shared_EventHtml$hideInfoImmediate() {
        this.infoHolderInnerElement.style.display = 'none';
    },
    hideInfoAnimate: function SpottedScript_Controls_EventBox_Shared_EventHtml$hideInfoAnimate(direction) {
        /// <param name="direction" type="String">
        /// </param>
        var options = {};
        options['direction'] = direction;
        options['easing'] = 'easeOutQuint';
        this.infoHolderInnerJQ.hide('drop', options, 250, Function.createDelegate(this, function() {
            this._details.controller.finishedAnimationTask('HideInfoAnimate');
        }));
    },
    showInfoImmediate: function SpottedScript_Controls_EventBox_Shared_EventHtml$showInfoImmediate() {
        this.infoHolderInnerElement.style.display = 'block';
    },
    showInfoAnimate: function SpottedScript_Controls_EventBox_Shared_EventHtml$showInfoAnimate(direction) {
        /// <param name="direction" type="String">
        /// </param>
        this.infoHolderInnerElement.style.display = 'block';
        var options = {};
        options['direction'] = direction;
        options['easing'] = 'easeOutQuint';
        this.infoHolderInnerJQ.show('drop', options, 250, Function.createDelegate(this, function() {
            this.resizeInfoHolderImmediate();
            this._details.controller.finishedAnimationTask('ShowInfoAnimate');
        }));
    },
    resizeInfoHolderImmediate: function SpottedScript_Controls_EventBox_Shared_EventHtml$resizeInfoHolderImmediate() {
        this._details.controller.view.get_eventInfoHolderOuter().style.height = this.getInfoHeight().toString() + 'px';
    },
    resizeInfoHolderAnimate: function SpottedScript_Controls_EventBox_Shared_EventHtml$resizeInfoHolderAnimate() {
        var options = {};
        options['height'] = this.getInfoHeight().toString() + 'px';
        this._details.controller.eventInfoHolderOuterJQ.animate(options, 250, 'easeOutQuint', Function.createDelegate(this, function() {
            this._details.controller.finishedAnimationTask('ResizeInfoHolderAnimate');
        }));
    },
    getInfoHeight: function SpottedScript_Controls_EventBox_Shared_EventHtml$getInfoHeight() {
        /// <returns type="Number" integer="true"></returns>
        var textHeight = this.infoTextHolderJQ.height();
        textHeight = (textHeight > 100) ? textHeight : 100;
        return textHeight;
    },
    iconKeylineElement: null,
    iconImageElement: null,
    infoHolderInnerElement: null,
    infoTextHolderElement: null,
    infoHolderInnerJQ: null,
    infoTextHolderJQ: null,
    kill: function SpottedScript_Controls_EventBox_Shared_EventHtml$kill(element) {
        /// <param name="element" type="Object" domElement="true">
        /// </param>
        if (element == null) {
            return;
        }
        while (element.childNodes.length > 0) {
            this.kill(element.childNodes[element.childNodes.length - 1]);
        }
        if (element.parentNode != null) {
            element.parentNode.removeChild(element);
        }
    },
    initialiseElements: function SpottedScript_Controls_EventBox_Shared_EventHtml$initialiseElements(initialiseIcon, createInfoHtml, refreshInfoHtml, initialiseInfo) {
        /// <param name="initialiseIcon" type="Boolean">
        /// </param>
        /// <param name="createInfoHtml" type="Boolean">
        /// </param>
        /// <param name="refreshInfoHtml" type="Boolean">
        /// </param>
        /// <param name="initialiseInfo" type="Boolean">
        /// </param>
        if (initialiseIcon) {
            this.iconKeylineElement = document.getElementById(this.iconKeylineClientID);
            this.iconImageElement = document.getElementById(this.iconImageClientID);
            $addHandler(this.iconImageElement, 'mouseover', Function.createDelegate(this._details.controller, this._details.controller.eventIconMouseOver));
            $addHandler(this.iconImageElement, 'mouseout', Function.createDelegate(this._details.controller, this._details.controller.eventIconMouseOut));
        }
        if (createInfoHtml) {
            this.infoHolderInnerElement = document.getElementById(this.infoHolderInnerClientID);
            this.kill(this.infoHolderInnerElement);
            this.infoHolderInnerElement = document.createElement('div');
            this.infoHolderInnerElement.innerHTML = this.toHtmlInfo();
            this._details.controller.view.get_eventInfoHolderOuter().appendChild(this.infoHolderInnerElement);
        }
        if (refreshInfoHtml) {
            this.infoHolderInnerElement = document.getElementById(this.infoHolderInnerClientID);
            this.infoHolderInnerElement.innerHTML = this.toHtmlInfoInner();
        }
        if (initialiseInfo) {
            this.infoHolderInnerElement = document.getElementById(this.infoHolderInnerClientID);
            this.infoHolderInnerJQ = jQuery(this.infoHolderInnerElement);
            this.infoTextHolderElement = document.getElementById(this.infoTextHolderClientID);
            this.infoTextHolderJQ = jQuery(this.infoTextHolderElement);
        }
        this.elementsInitialised = true;
        this.updateUI();
    },
    updateUI: function SpottedScript_Controls_EventBox_Shared_EventHtml$updateUI() {
        if (this.elementsInitialised) {
            this.updateIconUI();
            this.updateInfoHolderUI();
        }
    },
    updateIconUI: function SpottedScript_Controls_EventBox_Shared_EventHtml$updateIconUI() {
        this.iconKeylineElement.className = (this._details.get_selected() && !this._details.page.isEmpty) ? 'EventBoxIconKeyline Selected' : 'EventBoxIconKeyline';
    },
    updateInfoHolderUI: function SpottedScript_Controls_EventBox_Shared_EventHtml$updateInfoHolderUI() {
        this.infoHolderInnerElement.style.display = (this._details.get_selected()) ? 'block' : 'none';
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Shared.EventStub
SpottedScript.Controls.EventBox.Shared.EventStub = function SpottedScript_Controls_EventBox_Shared_EventStub(eventK, eventName, eventUrl, venueK, venueName, venueUrl, placeK, placeName, placeUrl, friendlyDateString, eventPicGuid, eventShortDescription, eventMusicText, eventMembersAttending, eventInInTheFuture) {
    /// <param name="eventK" type="Number" integer="true">
    /// </param>
    /// <param name="eventName" type="String">
    /// </param>
    /// <param name="eventUrl" type="String">
    /// </param>
    /// <param name="venueK" type="Number" integer="true">
    /// </param>
    /// <param name="venueName" type="String">
    /// </param>
    /// <param name="venueUrl" type="String">
    /// </param>
    /// <param name="placeK" type="Number" integer="true">
    /// </param>
    /// <param name="placeName" type="String">
    /// </param>
    /// <param name="placeUrl" type="String">
    /// </param>
    /// <param name="friendlyDateString" type="String">
    /// </param>
    /// <param name="eventPicGuid" type="String">
    /// </param>
    /// <param name="eventShortDescription" type="String">
    /// </param>
    /// <param name="eventMusicText" type="String">
    /// </param>
    /// <param name="eventMembersAttending" type="Number" integer="true">
    /// </param>
    /// <param name="eventInInTheFuture" type="Boolean">
    /// </param>
    /// <field name="eventK" type="Number" integer="true">
    /// </field>
    /// <field name="eventName" type="String">
    /// </field>
    /// <field name="eventUrl" type="String">
    /// </field>
    /// <field name="venueK" type="Number" integer="true">
    /// </field>
    /// <field name="venueName" type="String">
    /// </field>
    /// <field name="venueUrl" type="String">
    /// </field>
    /// <field name="placeK" type="Number" integer="true">
    /// </field>
    /// <field name="placeName" type="String">
    /// </field>
    /// <field name="placeUrl" type="String">
    /// </field>
    /// <field name="friendlyDateString" type="String">
    /// </field>
    /// <field name="eventPicGuid" type="String">
    /// </field>
    /// <field name="eventShortDescription" type="String">
    /// </field>
    /// <field name="eventMusicText" type="String">
    /// </field>
    /// <field name="eventMembersAttending" type="Number" integer="true">
    /// </field>
    /// <field name="eventInInTheFuture" type="Boolean">
    /// </field>
    this.eventK = eventK;
    this.eventName = eventName;
    this.eventUrl = eventUrl;
    this.venueK = venueK;
    this.venueName = venueName;
    this.venueUrl = venueUrl;
    this.placeK = placeK;
    this.placeName = placeName;
    this.placeUrl = placeUrl;
    this.friendlyDateString = friendlyDateString;
    this.eventPicGuid = eventPicGuid;
    this.eventShortDescription = eventShortDescription;
    this.eventMusicText = eventMusicText;
    this.eventMembersAttending = eventMembersAttending;
    this.eventInInTheFuture = eventInInTheFuture;
}
SpottedScript.Controls.EventBox.Shared.EventStub.prototype = {
    eventK: 0,
    eventName: null,
    eventUrl: null,
    venueK: 0,
    venueName: null,
    venueUrl: null,
    placeK: 0,
    placeName: null,
    placeUrl: null,
    friendlyDateString: null,
    eventPicGuid: null,
    eventShortDescription: null,
    eventMusicText: null,
    eventMembersAttending: 0,
    eventInInTheFuture: false
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Shared.EventPageDetails
SpottedScript.Controls.EventBox.Shared.EventPageDetails = function SpottedScript_Controls_EventBox_Shared_EventPageDetails(controller, data, isLoading) {
    /// <param name="controller" type="SpottedScript.Controls.EventBox.Controller">
    /// </param>
    /// <param name="data" type="SpottedScript.Controls.EventBox.Shared.EventPageStub">
    /// </param>
    /// <param name="isLoading" type="Boolean">
    /// </param>
    /// <field name="events" type="Array" elementType="EventDetails">
    /// </field>
    /// <field name="controller" type="SpottedScript.Controls.EventBox.Controller">
    /// </field>
    /// <field name="parentClientID" type="String">
    /// </field>
    /// <field name="clientID" type="String">
    /// </field>
    /// <field name="data" type="SpottedScript.Controls.EventBox.Shared.EventPageStub">
    /// </field>
    /// <field name="html" type="SpottedScript.Controls.EventBox.Shared.EventPageHtml">
    /// </field>
    /// <field name="requestInProgress" type="Boolean">
    /// </field>
    /// <field name="hasIncompleteEventData" type="Boolean">
    /// </field>
    /// <field name="isLoading" type="Boolean">
    /// </field>
    /// <field name="isEmpty" type="Boolean">
    /// </field>
    /// <field name="_selected" type="Boolean">
    /// </field>
    this.data = data;
    this.isLoading = isLoading;
    this.controller = controller;
    this.parentClientID = this.controller.clientID;
    this.clientID = this.parentClientID + '_' + this.getKey();
    this.events = new Array(8);
    var gotEvent = false;
    var gotNullEvent = false;
    for (var i = 0; i < 8; i++) {
        this.events[i] = new SpottedScript.Controls.EventBox.Shared.EventDetails(i, this, (this.data.events != null && this.data.events.length > i && this.data.events[i] != null) ? this.data.events[i] : null, isLoading);
        if (this.data.events == null || this.data.events.length <= i || this.data.events[i] == null) {
            gotNullEvent = true;
        }
        else {
            gotEvent = true;
        }
    }
    this.hasIncompleteEventData = gotNullEvent;
    this.isEmpty = !gotEvent;
    this.html = new SpottedScript.Controls.EventBox.Shared.EventPageHtml(this);
}
SpottedScript.Controls.EventBox.Shared.EventPageDetails.getKeyStatic = function SpottedScript_Controls_EventBox_Shared_EventPageDetails$getKeyStatic(data) {
    /// <param name="data" type="SpottedScript.Controls.EventBox.Shared.EventPageStub">
    /// </param>
    /// <returns type="String"></returns>
    return (data.parentObjectType).toString() + '_' + data.parentObjectK.toString() + '_' + (data.tabType).toString() + '_' + data.musicTypeK.toString() + '_' + data.pageIndex.toString();
}
SpottedScript.Controls.EventBox.Shared.EventPageDetails.getStubFromKey = function SpottedScript_Controls_EventBox_Shared_EventPageDetails$getStubFromKey(key) {
    /// <param name="key" type="String">
    /// </param>
    /// <returns type="SpottedScript.Controls.EventBox.Shared.EventPageStub"></returns>
    var keyArr = key.split('_');
    return new SpottedScript.Controls.EventBox.Shared.EventPageStub(Number.parseInvariant(keyArr[0]), Number.parseInvariant(keyArr[1]), Number.parseInvariant(keyArr[2]), Number.parseInvariant(keyArr[3]), Number.parseInvariant(keyArr[4]), Number.parseInvariant(keyArr[4]), null);
}
SpottedScript.Controls.EventBox.Shared.EventPageDetails.prototype = {
    events: null,
    controller: null,
    parentClientID: null,
    clientID: null,
    data: null,
    html: null,
    requestInProgress: false,
    hasIncompleteEventData: false,
    isLoading: false,
    isEmpty: false,
    get_selected: function SpottedScript_Controls_EventBox_Shared_EventPageDetails$get_selected() {
        /// <value type="Boolean"></value>
        return this._selected;
    },
    set_selected: function SpottedScript_Controls_EventBox_Shared_EventPageDetails$set_selected(value) {
        /// <value type="Boolean"></value>
        if (this._selected !== value) {
            this._selected = value;
            this.html.updateUI();
        }
        return value;
    },
    _selected: false,
    getKey: function SpottedScript_Controls_EventBox_Shared_EventPageDetails$getKey() {
        /// <returns type="String"></returns>
        return SpottedScript.Controls.EventBox.Shared.EventPageDetails.getKeyStatic(this.data);
    },
    changeSelectedState: function SpottedScript_Controls_EventBox_Shared_EventPageDetails$changeSelectedState(state, animate, direction) {
        /// <param name="state" type="Boolean">
        /// </param>
        /// <param name="animate" type="Boolean">
        /// </param>
        /// <param name="direction" type="String">
        /// </param>
        if (this._selected === state) {
            return;
        }
        this._selected = state;
        if (!state) {
            if (direction === 'left') {
                direction = 'right';
            }
            else if (direction === 'right') {
                direction = 'left';
            }
            else if (direction === 'up') {
                direction = 'down';
            }
            else if (direction === 'down') {
                direction = 'up';
            }
        }
        if (animate && this.controller.enableEffects) {
            if (state) {
                this.controller.performOrQueueAnimationTask([ Function.createDelegate(this, function() {
                    this.html.showAnimate(direction);
                }) ], 'EventPage_ShowAnimate');
            }
            else {
                this.controller.performOrQueueAnimationTask([ Function.createDelegate(this, function() {
                    this.html.hideAnimate(direction);
                }), Function.createDelegate(this, function() {
                    this.html.hideImmediate();
                }) ], 'EventPage_HideAnimate');
            }
        }
        else {
            if (state) {
                this.html.showImmediate();
            }
            else {
                this.html.hideImmediate();
            }
        }
    },
    getEventsIconsHtml: function SpottedScript_Controls_EventBox_Shared_EventPageDetails$getEventsIconsHtml() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        for (var i = 0; i < 8; i++) {
            this.events[i].html.appendHtmlIcon(sb);
        }
        return sb.toString();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Shared.EventPageHtml
SpottedScript.Controls.EventBox.Shared.EventPageHtml = function SpottedScript_Controls_EventBox_Shared_EventPageHtml(details) {
    /// <param name="details" type="SpottedScript.Controls.EventBox.Shared.EventPageDetails">
    /// </param>
    /// <field name="_details" type="SpottedScript.Controls.EventBox.Shared.EventPageDetails">
    /// </field>
    /// <field name="elementsInitialised" type="Boolean">
    /// </field>
    /// <field name="holderClientID" type="String">
    /// </field>
    /// <field name="holderElement" type="Object" domElement="true">
    /// </field>
    /// <field name="holderJQ" type="JQ.JQueryObject">
    /// </field>
    this._details = details;
    this.holderClientID = this._details.parentClientID + '_EventPage_' + this._details.getKey() + '_Holder';
}
SpottedScript.Controls.EventBox.Shared.EventPageHtml.prototype = {
    _details: null,
    elementsInitialised: false,
    holderClientID: null,
    appendHtml: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$appendHtml(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        sb.append('<div');
        sb.appendAttribute('id', this.holderClientID);
        sb.appendAttribute('class', (this._details.get_selected()) ? 'EventBoxPageHolder Selected' : 'EventBoxPageHolder');
        sb.append('>');
        this.appendHtmlInner(sb);
        sb.append('</div>');
    },
    appendHtmlInner: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$appendHtmlInner(sb) {
        /// <param name="sb" type="Spotted.System.Text.StringBuilder">
        /// </param>
        for (var i = 0; i < 8; i++) {
            this._details.events[i].html.appendHtmlIcon(sb);
        }
    },
    toHtml: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$toHtml() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        this.appendHtml(sb);
        return sb.toString();
    },
    toHtmlInner: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$toHtmlInner() {
        /// <returns type="String"></returns>
        var sb = new Spotted.System.Text.StringBuilder();
        this.appendHtmlInner(sb);
        return sb.toString();
    },
    hideImmediate: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$hideImmediate() {
        this.holderElement.style.display = 'none';
    },
    hideAnimate: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$hideAnimate(direction) {
        /// <param name="direction" type="String">
        /// </param>
        var options = {};
        options['direction'] = direction;
        options['easing'] = 'easeOutQuint';
        this.holderJQ.hide('drop', options, 500, Function.createDelegate(this, function() {
            this._details.controller.finishedAnimationTask('EventPage_HideAnimate');
        }));
    },
    showImmediate: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$showImmediate() {
        this.holderElement.style.display = 'block';
    },
    showAnimate: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$showAnimate(direction) {
        /// <param name="direction" type="String">
        /// </param>
        this.holderElement.style.display = 'block';
        var options = {};
        options['direction'] = direction;
        options['easing'] = 'easeOutQuint';
        this.holderJQ.show('drop', options, 500, Function.createDelegate(this, function() {
            this._details.controller.finishedAnimationTask('EventPage_ShowAnimate');
        }));
    },
    holderElement: null,
    holderJQ: null,
    initialiseElements: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$initialiseElements(initialiseHolder, createIconsHtml, refreshIconsHtml, initialiseIcons, createInfoHtml, refreshInfoHtml, initialiseInfo) {
        /// <param name="initialiseHolder" type="Boolean">
        /// </param>
        /// <param name="createIconsHtml" type="Boolean">
        /// </param>
        /// <param name="refreshIconsHtml" type="Boolean">
        /// </param>
        /// <param name="initialiseIcons" type="Boolean">
        /// </param>
        /// <param name="createInfoHtml" type="Boolean">
        /// </param>
        /// <param name="refreshInfoHtml" type="Boolean">
        /// </param>
        /// <param name="initialiseInfo" type="Boolean">
        /// </param>
        if (createIconsHtml) {
            var element = document.createElement('div');
            element.innerHTML = this.toHtml();
            this._details.controller.view.get_eventIconsHolder().appendChild(element);
        }
        if (initialiseHolder) {
            this.holderElement = document.getElementById(this.holderClientID);
            this.holderJQ = jQuery(this.holderElement);
        }
        if (refreshIconsHtml) {
            this.holderElement.innerHTML = this._details.getEventsIconsHtml();
        }
        if (initialiseIcons || createInfoHtml || refreshInfoHtml || initialiseInfo) {
            for (var i = 0; i < 8; i++) {
                this._details.events[i].html.initialiseElements(initialiseIcons, createInfoHtml, refreshInfoHtml, initialiseInfo);
            }
        }
        this.elementsInitialised = true;
        this.updateUI();
    },
    updateUI: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$updateUI() {
        if (this.elementsInitialised) {
            this.updateHolderUI();
        }
    },
    updateHolderUI: function SpottedScript_Controls_EventBox_Shared_EventPageHtml$updateHolderUI() {
        this.holderElement.className = (this._details.get_selected()) ? 'EventBoxPageHolder Selected' : 'EventBoxPageHolder';
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventBox.Shared.EventPageStub
SpottedScript.Controls.EventBox.Shared.EventPageStub = function SpottedScript_Controls_EventBox_Shared_EventPageStub(parentObjectType, parentObjectK, tabType, musicTypeK, pageIndex, requestedPageIndex, events) {
    /// <param name="parentObjectType" type="Model.Entities.ObjectType">
    /// </param>
    /// <param name="parentObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="tabType" type="SpottedScript.Controls.EventBox.Shared.TabType">
    /// </param>
    /// <param name="musicTypeK" type="Number" integer="true">
    /// </param>
    /// <param name="pageIndex" type="Number" integer="true">
    /// </param>
    /// <param name="requestedPageIndex" type="Number" integer="true">
    /// </param>
    /// <param name="events" type="Array" elementType="EventStub">
    /// </param>
    /// <field name="parentObjectType" type="Model.Entities.ObjectType">
    /// </field>
    /// <field name="parentObjectK" type="Number" integer="true">
    /// </field>
    /// <field name="tabType" type="SpottedScript.Controls.EventBox.Shared.TabType">
    /// </field>
    /// <field name="musicTypeK" type="Number" integer="true">
    /// </field>
    /// <field name="pageIndex" type="Number" integer="true">
    /// </field>
    /// <field name="requestedPageIndex" type="Number" integer="true">
    /// </field>
    /// <field name="events" type="Array" elementType="EventStub">
    /// </field>
    this.parentObjectType = parentObjectType;
    this.parentObjectK = parentObjectK;
    this.tabType = tabType;
    this.musicTypeK = musicTypeK;
    this.pageIndex = pageIndex;
    this.requestedPageIndex = requestedPageIndex;
    this.events = events;
}
SpottedScript.Controls.EventBox.Shared.EventPageStub.prototype = {
    parentObjectType: 0,
    parentObjectK: 0,
    tabType: 0,
    musicTypeK: 0,
    pageIndex: 0,
    requestedPageIndex: 0,
    events: null
}
SpottedScript.Controls.EventBox.Controller.registerClass('SpottedScript.Controls.EventBox.Controller');
SpottedScript.Controls.EventBox.ServerClass.registerClass('SpottedScript.Controls.EventBox.ServerClass');
SpottedScript.Controls.EventBox.GotExceptionEventArgs.registerClass('SpottedScript.Controls.EventBox.GotExceptionEventArgs', Sys.EventArgs);
SpottedScript.Controls.EventBox.View.registerClass('SpottedScript.Controls.EventBox.View');
SpottedScript.Controls.EventBox.Shared.EventDetails.registerClass('SpottedScript.Controls.EventBox.Shared.EventDetails');
SpottedScript.Controls.EventBox.Shared.EventHtml.registerClass('SpottedScript.Controls.EventBox.Shared.EventHtml');
SpottedScript.Controls.EventBox.Shared.EventStub.registerClass('SpottedScript.Controls.EventBox.Shared.EventStub');
SpottedScript.Controls.EventBox.Shared.EventPageDetails.registerClass('SpottedScript.Controls.EventBox.Shared.EventPageDetails');
SpottedScript.Controls.EventBox.Shared.EventPageHtml.registerClass('SpottedScript.Controls.EventBox.Shared.EventPageHtml');
SpottedScript.Controls.EventBox.Shared.EventPageStub.registerClass('SpottedScript.Controls.EventBox.Shared.EventPageStub');
