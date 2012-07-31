//! EventBox.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.EventBox');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.TabType

Js.Controls.EventBox.TabType = function() { 
    /// <field name="future" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="past" type="Number" integer="true" static="true">
    /// </field>
    /// <field name="tickets" type="Number" integer="true" static="true">
    /// </field>
};
Js.Controls.EventBox.TabType.prototype = {
    future: 1, 
    past: 2, 
    tickets: 3
}
Js.Controls.EventBox.TabType.registerEnum('Js.Controls.EventBox.TabType', false);


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.Controller

Js.Controls.EventBox.Controller = function Js_Controls_EventBox_Controller(view) {
    /// <param name="view" type="Js.Controls.EventBox.View">
    /// </param>
    /// <field name="view" type="Js.Controls.EventBox.View">
    /// </field>
    /// <field name="server" type="Js.Controls.EventBox.ServerClass">
    /// </field>
    /// <field name="initParentObjectType" type="Js.Library.ObjectType">
    /// </field>
    /// <field name="initParentObjectK" type="Number" integer="true">
    /// </field>
    /// <field name="initMusicTypeK" type="Number" integer="true">
    /// </field>
    /// <field name="initTabType" type="Js.Controls.EventBox.TabType">
    /// </field>
    /// <field name="initPageIndex" type="Number" integer="true">
    /// </field>
    /// <field name="currentParentObjectType" type="Js.Library.ObjectType">
    /// </field>
    /// <field name="currentParentObjectK" type="Number" integer="true">
    /// </field>
    /// <field name="currentMusicTypeK" type="Number" integer="true">
    /// </field>
    /// <field name="currentTabType" type="Js.Controls.EventBox.TabType">
    /// </field>
    /// <field name="currentPageIndex" type="Number" integer="true">
    /// </field>
    /// <field name="clientID" type="String">
    /// </field>
    /// <field name="enableEffects" type="Boolean">
    /// </field>
    /// <field name="_eventPageCache" type="Object">
    /// </field>
    /// <field name="eventInfoHolderOuterJ" type="jQueryObject">
    /// </field>
    /// <field name="eventInfoHolderOuterElement" type="Object" domElement="true">
    /// </field>
    /// <field name="currentlySelectedEvent" type="Js.Controls.EventBox.EventDetails">
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
    this.view = view;
    this.server = new Js.Controls.EventBox.ServerClass(this);
    this.server.gotEventPage = ss.Delegate.create(this, this.gotEventPage);
    this.server.gotGenericException = ss.Delegate.create(this, this._gotGenericException);
    if (Js.Library.Misc.get_browserIsIE()) {
        $(ss.Delegate.create(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
Js.Controls.EventBox.Controller.prototype = {
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
    eventInfoHolderOuterJ: null,
    eventInfoHolderOuterElement: null,
    currentlySelectedEvent: null,
    
    _initialise: function Js_Controls_EventBox_Controller$_initialise() {
        /// <summary>
        /// Anything that affects the DOM goes in here.
        /// </summary>
        $(window).bind('hashchange', ss.Delegate.create(this, this._application_Navigate));
        this.clientID = this.view.get_initClientID().value;
        this.enableEffects = Boolean.parse(this.view.get_initEnableEffects().value);
        this.eventInfoHolderOuterJ = this.view.get_eventInfoHolderOuterJ();
        this.view.get_eventIconsNavigationForwardHolderJ().click(ss.Delegate.create(this, this._pageChangeForwardClick));
        this.view.get_eventIconsNavigationBackHolderJ().click(ss.Delegate.create(this, this._pageChangeBackClick));
        this.view.get_musicDropDownControl().view.get_dropDownJ().change(ss.Delegate.create(this, this._musicChangeClick));
        this.view.get_pastEventsTabJ().click(ss.Delegate.create(this, this._tabClickPast));
        this.view.get_futureEventsTabJ().click(ss.Delegate.create(this, this._tabClickFuture));
        this.view.get_ticketsTabJ().click(ss.Delegate.create(this, this._tabClickTickets));
        this._eventPageCache = {};
        var firstPageData = (eval(' [ ' + this.view.get_initFirstPage().value + ' ] '))[0];
        var firstPage = new Js.Controls.EventBox.EventPageDetails(this, firstPageData, false);
        firstPage.set_selected(true);
        firstPage.html.initialiseElements(true, false, false, true, true, false, true);
        for (var i = 0; i < firstPage.events.length; i++) {
            firstPage.events[i].changeSelectedState(!i, false, '');
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
        if (Js.Library.Misc.get_browserIsIE()) {
            $.bbq.pushState({});
        }
    },
    
    _application_Navigate: function Js_Controls_EventBox_Controller$_application_Navigate(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        var keyOb = $.bbq.getState('EventBox_PageKey', false);
        var key = (keyOb == null) ? '' : keyOb.toString();
        if (key.length > 0) {
            this._restorePageState(key);
        }
        else {
            this._restorePageState(null);
        }
    },
    
    _restorePageState: function Js_Controls_EventBox_Controller$_restorePageState(key) {
        /// <param name="key" type="String">
        /// </param>
        if (key == null) {
            var d = new Js.Controls.EventBox.EventPageDetails(this, new Js.Controls.EventBox.EventPageStub(this.initParentObjectType, this.initParentObjectK, this.initTabType, this.initMusicTypeK, this.initPageIndex, this.initPageIndex, null), false);
            key = d.getKey();
        }
        if (this.get_currentEventPage().getKey() === key) {
            return;
        }
        this.get_currentEventPage().changeSelectedState(false, false, '');
        var s = Js.Controls.EventBox.EventPageDetails.getStubFromKey(key);
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
    
    _pageChangeForwardClick: function Js_Controls_EventBox_Controller$_pageChangeForwardClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._pageChange(true);
    },
    
    _pageChangeBackClick: function Js_Controls_EventBox_Controller$_pageChangeBackClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._pageChange(false);
    },
    
    _pageChange: function Js_Controls_EventBox_Controller$_pageChange(forward) {
        /// <param name="forward" type="Boolean">
        /// </param>
        var newPageIndex = this.currentPageIndex + ((forward) ? 1 : ((!this.currentPageIndex) ? 0 : -1));
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
        $.bbq.pushState(d);
    },
    
    _tabClickPast: function Js_Controls_EventBox_Controller$_tabClickPast(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._changeTabType(2);
    },
    
    _tabClickFuture: function Js_Controls_EventBox_Controller$_tabClickFuture(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._changeTabType(1);
    },
    
    _tabClickTickets: function Js_Controls_EventBox_Controller$_tabClickTickets(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._changeTabType(3);
    },
    
    _getTabLocation: function Js_Controls_EventBox_Controller$_getTabLocation(tab) {
        /// <param name="tab" type="Js.Controls.EventBox.TabType">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return (tab === 1) ? 1 : (tab === 2) ? 2 : 3;
    },
    
    _changeTabType: function Js_Controls_EventBox_Controller$_changeTabType(tabType) {
        /// <param name="tabType" type="Js.Controls.EventBox.TabType">
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
        $.bbq.pushState(d);
    },
    
    _updateTabsUI: function Js_Controls_EventBox_Controller$_updateTabsUI() {
        this.view.get_futureEventsTab().className = (this.currentTabType === 1) ? 'TabbedHeading Selected' : 'TabbedHeading';
        this.view.get_pastEventsTab().className = (this.currentTabType === 2) ? 'TabbedHeading Selected' : 'TabbedHeading';
        this.view.get_ticketsTab().className = (this.currentTabType === 3) ? 'TabbedHeading Selected' : 'TabbedHeading';
    },
    
    _musicChangeClick: function Js_Controls_EventBox_Controller$_musicChangeClick(e) {
        /// <param name="e" type="jQueryEvent">
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
        this.currentMusicTypeK = parseInt(this.view.get_musicDropDownControl().view.get_dropDown().value);
        this.get_currentEventPage().changeSelectedState(true, true, movementDirection);
        this.changeEventNow(this.get_currentEventPage().events[0], true, movementDirection);
        var d = {};
        d['EventBox_PageKey'] = this.get_currentEventPage().getKey();
        $.bbq.pushState(d);
    },
    
    performOrQueueAnimationTask: function Js_Controls_EventBox_Controller$performOrQueueAnimationTask(task, taskType) {
        /// <summary>
        /// task is a Action[]. [0] is the default animation action. Optional [1] is the action to perform if we don't have time to do the animation.
        /// </summary>
        /// <param name="task" type="Array" elementType="Function">
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
    
    finishedAnimationTask: function Js_Controls_EventBox_Controller$finishedAnimationTask(taskType) {
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
    
    eventIconMouseOut: function Js_Controls_EventBox_Controller$eventIconMouseOut(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._eventIconMouseOutID++;
    },
    
    eventIconMouseOver: function Js_Controls_EventBox_Controller$eventIconMouseOver(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._eventIconMouseOverID++;
        var overID = this._eventIconMouseOverID;
        var outID = this._eventIconMouseOutID;
        this.eventIconMouseOverAfterDelay(e, overID, outID);
    },
    
    eventIconMouseOverAfterDelay: function Js_Controls_EventBox_Controller$eventIconMouseOverAfterDelay(e, mouseOverID, mouseOutID) {
        /// <param name="e" type="jQueryEvent">
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
    
    changeEventNow: function Js_Controls_EventBox_Controller$changeEventNow(newSelectedEvent, animate, movementDirection) {
        /// <param name="newSelectedEvent" type="Js.Controls.EventBox.EventDetails">
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
    
    _findEventFromMouseOverEvent: function Js_Controls_EventBox_Controller$_findEventFromMouseOverEvent(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        /// <returns type="Js.Controls.EventBox.EventDetails"></returns>
        try {
            var el = e.target;
            while (el != null) {
                if (el.id.endsWith('_Icon_Image')) {
                    var parts = e.target.id.split('_');
                    var index = parseInt(parts[parts.length - 3]);
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
    
    get_currentEventPage: function Js_Controls_EventBox_Controller$get_currentEventPage() {
        /// <value type="Js.Controls.EventBox.EventPageDetails"></value>
        var data = new Js.Controls.EventBox.EventPageStub(this.currentParentObjectType, this.currentParentObjectK, this.currentTabType, this.currentMusicTypeK, this.currentPageIndex, this.currentPageIndex, null);
        var key = Js.Controls.EventBox.EventPageDetails.getKeyStatic(data);
        if (this._eventPageCache[key] == null) {
            this.server.getEventPage(key);
        }
        if (this._eventPageCache[key] == null) {
            var eventPage = new Js.Controls.EventBox.EventPageDetails(this, data, true);
            eventPage.requestInProgress = true;
            eventPage.html.initialiseElements(true, true, false, true, true, false, true);
            this._eventPageCache[key] = eventPage;
        }
        return this._eventPageCache[key];
    },
    
    gotEventPage: function Js_Controls_EventBox_Controller$gotEventPage(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        if (o != null) {
            var stub = o;
            var newPage = new Js.Controls.EventBox.EventPageDetails(this, stub, false);
            this._updatePage(newPage);
            if (stub.requestedPageIndex !== stub.pageIndex) {
                var requestedStub = new Js.Controls.EventBox.EventPageStub(stub.parentObjectType, stub.parentObjectK, stub.tabType, stub.musicTypeK, stub.requestedPageIndex, stub.requestedPageIndex, null);
                var requestedPage = new Js.Controls.EventBox.EventPageDetails(this, requestedStub, false);
                this._updatePage(requestedPage);
            }
        }
    },
    
    _updatePage: function Js_Controls_EventBox_Controller$_updatePage(newPage) {
        /// <param name="newPage" type="Js.Controls.EventBox.EventPageDetails">
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
                    this.currentlySelectedEvent.html.resizeInfoHolderImmediate();
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
    
    _gotGenericException: function Js_Controls_EventBox_Controller$_gotGenericException(o, e) {
        /// <param name="o" type="Object">
        /// </param>
        /// <param name="e" type="ss.EventArgs">
        /// </param>
        var a = e;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.EventDetails

Js.Controls.EventBox.EventDetails = function Js_Controls_EventBox_EventDetails(positionIndex, page, e, isLoading) {
    /// <param name="positionIndex" type="Number" integer="true">
    /// </param>
    /// <param name="page" type="Js.Controls.EventBox.EventPageDetails">
    /// </param>
    /// <param name="e" type="Js.Controls.EventBox.EventStub">
    /// </param>
    /// <param name="isLoading" type="Boolean">
    /// </param>
    /// <field name="controller" type="Js.Controls.EventBox.Controller">
    /// </field>
    /// <field name="page" type="Js.Controls.EventBox.EventPageDetails">
    /// </field>
    /// <field name="parentClientID" type="String">
    /// </field>
    /// <field name="data" type="Js.Controls.EventBox.EventStub">
    /// </field>
    /// <field name="html" type="Js.Controls.EventBox.EventHtml">
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
    this.html = new Js.Controls.EventBox.EventHtml(this);
}
Js.Controls.EventBox.EventDetails.prototype = {
    controller: null,
    page: null,
    parentClientID: null,
    data: null,
    html: null,
    positionIndex: 0,
    hasData: false,
    isLoading: false,
    
    get_selected: function Js_Controls_EventBox_EventDetails$get_selected() {
        /// <value type="Boolean"></value>
        return this._selected;
    },
    set_selected: function Js_Controls_EventBox_EventDetails$set_selected(value) {
        /// <value type="Boolean"></value>
        if (this._selected !== value) {
            this._selected = value;
            this.html.updateUI();
        }
        return value;
    },
    
    _selected: false,
    
    changeSelectedState: function Js_Controls_EventBox_EventDetails$changeSelectedState(state, animate, direction) {
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
        if (state) {
            this.html.showInfoImmediate();
            this.html.resizeInfoHolderImmediate();
        }
        else {
            this.html.hideInfoImmediate();
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.EventHtml

Js.Controls.EventBox.EventHtml = function Js_Controls_EventBox_EventHtml(details) {
    /// <param name="details" type="Js.Controls.EventBox.EventDetails">
    /// </param>
    /// <field name="_details" type="Js.Controls.EventBox.EventDetails">
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
    /// <field name="infoHolderInnerJQ" type="jQueryObject">
    /// </field>
    /// <field name="infoTextHolderJQ" type="jQueryObject">
    /// </field>
    this._details = details;
    this.iconKeylineClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Icon_Keyline';
    this.iconImageClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Icon_Image';
    this.infoHolderInnerClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Info_Holder';
    this.infoTextHolderClientID = this._details.parentClientID + '_Event_' + this._details.positionIndex + '_Info_TextHolder';
}
Js.Controls.EventBox.EventHtml.prototype = {
    _details: null,
    elementsInitialised: false,
    iconKeylineClientID: null,
    iconImageClientID: null,
    infoHolderInnerClientID: null,
    infoTextHolderClientID: null,
    
    appendHtmlIcon: function Js_Controls_EventBox_EventHtml$appendHtmlIcon(sb) {
        /// <param name="sb" type="Js.Library.StringBuilderJs">
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
            sb.appendAttribute('src', Js.Library.Misc.getPicUrlFromGuid(this._details.data.eventPicGuid));
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
    
    toHtmlIcon: function Js_Controls_EventBox_EventHtml$toHtmlIcon() {
        /// <returns type="String"></returns>
        var sb = new Js.Library.StringBuilderJs();
        this.appendHtmlIcon(sb);
        return sb.toString();
    },
    
    appendHtmlInfo: function Js_Controls_EventBox_EventHtml$appendHtmlInfo(sb) {
        /// <param name="sb" type="Js.Library.StringBuilderJs">
        /// </param>
        sb.append('<div');
        sb.appendAttribute('id', this.infoHolderInnerClientID);
        sb.appendAttribute('class', 'EventBoxInfoHolderInner');
        sb.append('>');
        this.appendHtmlInfoInner(sb);
        sb.append('</div>');
    },
    
    appendHtmlInfoInner: function Js_Controls_EventBox_EventHtml$appendHtmlInfoInner(sb) {
        /// <param name="sb" type="Js.Library.StringBuilderJs">
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
            sb.appendAttribute('src', Js.Library.Misc.getPicUrlFromGuid(this._details.data.eventPicGuid));
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
            sb.append('<center>Sorry, no events - <a href="/pages/events/edit">click here to add one</a>.</center>');
        }
        sb.append('</div>');
    },
    
    toHtmlInfo: function Js_Controls_EventBox_EventHtml$toHtmlInfo() {
        /// <returns type="String"></returns>
        var sb = new Js.Library.StringBuilderJs();
        this.appendHtmlInfo(sb);
        return sb.toString();
    },
    
    toHtmlInfoInner: function Js_Controls_EventBox_EventHtml$toHtmlInfoInner() {
        /// <returns type="String"></returns>
        var sb = new Js.Library.StringBuilderJs();
        this.appendHtmlInfoInner(sb);
        return sb.toString();
    },
    
    hideInfoImmediate: function Js_Controls_EventBox_EventHtml$hideInfoImmediate() {
        this.infoHolderInnerElement.style.display = 'none';
    },
    
    showInfoImmediate: function Js_Controls_EventBox_EventHtml$showInfoImmediate() {
        this.infoHolderInnerElement.style.display = 'block';
    },
    
    resizeInfoHolderImmediate: function Js_Controls_EventBox_EventHtml$resizeInfoHolderImmediate() {
        this._details.controller.view.get_eventInfoHolderOuter().style.height = this.getInfoHeight().toString() + 'px';
    },
    
    getInfoHeight: function Js_Controls_EventBox_EventHtml$getInfoHeight() {
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
    
    kill: function Js_Controls_EventBox_EventHtml$kill(element) {
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
    
    initialiseElements: function Js_Controls_EventBox_EventHtml$initialiseElements(initialiseIcon, createInfoHtml, refreshInfoHtml, initialiseInfo) {
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
            $(this.iconImageElement).mouseover(ss.Delegate.create(this._details.controller, this._details.controller.eventIconMouseOver));
            $(this.iconImageElement).mouseover(ss.Delegate.create(this._details.controller, this._details.controller.eventIconMouseOut));
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
            this.infoHolderInnerJQ = $(this.infoHolderInnerElement);
            this.infoTextHolderElement = document.getElementById(this.infoTextHolderClientID);
            this.infoTextHolderJQ = $(this.infoTextHolderElement);
        }
        this.elementsInitialised = true;
        this.updateUI();
    },
    
    updateUI: function Js_Controls_EventBox_EventHtml$updateUI() {
        if (this.elementsInitialised) {
            this.updateIconUI();
            this.updateInfoHolderUI();
        }
    },
    
    updateIconUI: function Js_Controls_EventBox_EventHtml$updateIconUI() {
        this.iconKeylineElement.className = (this._details.get_selected() && !this._details.page.isEmpty) ? 'EventBoxIconKeyline Selected' : 'EventBoxIconKeyline';
    },
    
    updateInfoHolderUI: function Js_Controls_EventBox_EventHtml$updateInfoHolderUI() {
        this.infoHolderInnerElement.style.display = (this._details.get_selected()) ? 'block' : 'none';
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.EventStub

Js.Controls.EventBox.EventStub = function Js_Controls_EventBox_EventStub(eventK, eventName, eventUrl, venueK, venueName, venueUrl, placeK, placeName, placeUrl, friendlyDateString, eventPicGuid, eventShortDescription, eventMusicText, eventMembersAttending, eventInInTheFuture) {
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
Js.Controls.EventBox.EventStub.prototype = {
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
// Js.Controls.EventBox.EventPageDetails

Js.Controls.EventBox.EventPageDetails = function Js_Controls_EventBox_EventPageDetails(controller, data, isLoading) {
    /// <param name="controller" type="Js.Controls.EventBox.Controller">
    /// </param>
    /// <param name="data" type="Js.Controls.EventBox.EventPageStub">
    /// </param>
    /// <param name="isLoading" type="Boolean">
    /// </param>
    /// <field name="events" type="Array" elementType="EventDetails">
    /// </field>
    /// <field name="controller" type="Js.Controls.EventBox.Controller">
    /// </field>
    /// <field name="parentClientID" type="String">
    /// </field>
    /// <field name="clientID" type="String">
    /// </field>
    /// <field name="data" type="Js.Controls.EventBox.EventPageStub">
    /// </field>
    /// <field name="html" type="Js.Controls.EventBox.EventPageHtml">
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
        this.events[i] = new Js.Controls.EventBox.EventDetails(i, this, (this.data.events != null && this.data.events.length > i && this.data.events[i] != null) ? this.data.events[i] : null, isLoading);
        if (this.data.events == null || this.data.events.length <= i || this.data.events[i] == null) {
            gotNullEvent = true;
        }
        else {
            gotEvent = true;
        }
    }
    this.hasIncompleteEventData = gotNullEvent;
    this.isEmpty = !gotEvent;
    this.html = new Js.Controls.EventBox.EventPageHtml(this);
}
Js.Controls.EventBox.EventPageDetails.getKeyStatic = function Js_Controls_EventBox_EventPageDetails$getKeyStatic(data) {
    /// <param name="data" type="Js.Controls.EventBox.EventPageStub">
    /// </param>
    /// <returns type="String"></returns>
    return (data.parentObjectType).toString() + '_' + data.parentObjectK.toString() + '_' + (data.tabType).toString() + '_' + data.musicTypeK.toString() + '_' + data.pageIndex.toString();
}
Js.Controls.EventBox.EventPageDetails.getStubFromKey = function Js_Controls_EventBox_EventPageDetails$getStubFromKey(key) {
    /// <param name="key" type="String">
    /// </param>
    /// <returns type="Js.Controls.EventBox.EventPageStub"></returns>
    var keyArr = key.split('_');
    return new Js.Controls.EventBox.EventPageStub(parseInt(keyArr[0]), parseInt(keyArr[1]), parseInt(keyArr[2]), parseInt(keyArr[3]), parseInt(keyArr[4]), parseInt(keyArr[4]), null);
}
Js.Controls.EventBox.EventPageDetails.prototype = {
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
    
    get_selected: function Js_Controls_EventBox_EventPageDetails$get_selected() {
        /// <value type="Boolean"></value>
        return this._selected;
    },
    set_selected: function Js_Controls_EventBox_EventPageDetails$set_selected(value) {
        /// <value type="Boolean"></value>
        if (this._selected !== value) {
            this._selected = value;
            this.html.updateUI();
        }
        return value;
    },
    
    _selected: false,
    
    getKey: function Js_Controls_EventBox_EventPageDetails$getKey() {
        /// <returns type="String"></returns>
        return Js.Controls.EventBox.EventPageDetails.getKeyStatic(this.data);
    },
    
    changeSelectedState: function Js_Controls_EventBox_EventPageDetails$changeSelectedState(state, animate, direction) {
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
        if (state) {
            this.html.showImmediate();
        }
        else {
            this.html.hideImmediate();
        }
    },
    
    getEventsIconsHtml: function Js_Controls_EventBox_EventPageDetails$getEventsIconsHtml() {
        /// <returns type="String"></returns>
        var sb = new Js.Library.StringBuilderJs();
        for (var i = 0; i < 8; i++) {
            this.events[i].html.appendHtmlIcon(sb);
        }
        return sb.toString();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.EventPageHtml

Js.Controls.EventBox.EventPageHtml = function Js_Controls_EventBox_EventPageHtml(details) {
    /// <param name="details" type="Js.Controls.EventBox.EventPageDetails">
    /// </param>
    /// <field name="_details" type="Js.Controls.EventBox.EventPageDetails">
    /// </field>
    /// <field name="elementsInitialised" type="Boolean">
    /// </field>
    /// <field name="holderClientID" type="String">
    /// </field>
    /// <field name="holderElement" type="Object" domElement="true">
    /// </field>
    /// <field name="holderJQ" type="jQueryObject">
    /// </field>
    this._details = details;
    this.holderClientID = this._details.parentClientID + '_EventPage_' + this._details.getKey() + '_Holder';
}
Js.Controls.EventBox.EventPageHtml.prototype = {
    _details: null,
    elementsInitialised: false,
    holderClientID: null,
    
    appendHtml: function Js_Controls_EventBox_EventPageHtml$appendHtml(sb) {
        /// <param name="sb" type="Js.Library.StringBuilderJs">
        /// </param>
        sb.append('<div');
        sb.appendAttribute('id', this.holderClientID);
        sb.appendAttribute('class', (this._details.get_selected()) ? 'EventBoxPageHolder Selected' : 'EventBoxPageHolder');
        sb.append('>');
        this.appendHtmlInner(sb);
        sb.append('</div>');
    },
    
    appendHtmlInner: function Js_Controls_EventBox_EventPageHtml$appendHtmlInner(sb) {
        /// <param name="sb" type="Js.Library.StringBuilderJs">
        /// </param>
        for (var i = 0; i < 8; i++) {
            this._details.events[i].html.appendHtmlIcon(sb);
        }
    },
    
    toHtml: function Js_Controls_EventBox_EventPageHtml$toHtml() {
        /// <returns type="String"></returns>
        var sb = new Js.Library.StringBuilderJs();
        this.appendHtml(sb);
        return sb.toString();
    },
    
    toHtmlInner: function Js_Controls_EventBox_EventPageHtml$toHtmlInner() {
        /// <returns type="String"></returns>
        var sb = new Js.Library.StringBuilderJs();
        this.appendHtmlInner(sb);
        return sb.toString();
    },
    
    hideImmediate: function Js_Controls_EventBox_EventPageHtml$hideImmediate() {
        this.holderElement.style.display = 'none';
    },
    
    showImmediate: function Js_Controls_EventBox_EventPageHtml$showImmediate() {
        this.holderElement.style.display = 'block';
    },
    
    holderElement: null,
    holderJQ: null,
    
    initialiseElements: function Js_Controls_EventBox_EventPageHtml$initialiseElements(initialiseHolder, createIconsHtml, refreshIconsHtml, initialiseIcons, createInfoHtml, refreshInfoHtml, initialiseInfo) {
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
            this.holderJQ = $(this.holderElement);
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
    
    updateUI: function Js_Controls_EventBox_EventPageHtml$updateUI() {
        if (this.elementsInitialised) {
            this.updateHolderUI();
        }
    },
    
    updateHolderUI: function Js_Controls_EventBox_EventPageHtml$updateHolderUI() {
        this.holderElement.className = (this._details.get_selected()) ? 'EventBoxPageHolder Selected' : 'EventBoxPageHolder';
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.EventPageStub

Js.Controls.EventBox.EventPageStub = function Js_Controls_EventBox_EventPageStub(parentObjectType, parentObjectK, tabType, musicTypeK, pageIndex, requestedPageIndex, events) {
    /// <param name="parentObjectType" type="Js.Library.ObjectType">
    /// </param>
    /// <param name="parentObjectK" type="Number" integer="true">
    /// </param>
    /// <param name="tabType" type="Js.Controls.EventBox.TabType">
    /// </param>
    /// <param name="musicTypeK" type="Number" integer="true">
    /// </param>
    /// <param name="pageIndex" type="Number" integer="true">
    /// </param>
    /// <param name="requestedPageIndex" type="Number" integer="true">
    /// </param>
    /// <param name="events" type="Array" elementType="EventStub">
    /// </param>
    /// <field name="parentObjectType" type="Js.Library.ObjectType">
    /// </field>
    /// <field name="parentObjectK" type="Number" integer="true">
    /// </field>
    /// <field name="tabType" type="Js.Controls.EventBox.TabType">
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
Js.Controls.EventBox.EventPageStub.prototype = {
    parentObjectType: 0,
    parentObjectK: 0,
    tabType: 0,
    musicTypeK: 0,
    pageIndex: 0,
    requestedPageIndex: 0,
    events: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.ServerClass

Js.Controls.EventBox.ServerClass = function Js_Controls_EventBox_ServerClass(controller) {
    /// <param name="controller" type="Js.Controls.EventBox.Controller">
    /// </param>
    /// <field name="_controller" type="Js.Controls.EventBox.Controller">
    /// </field>
    /// <field name="gotEventPage" type="Function">
    /// </field>
    /// <field name="gotGenericException" type="Function">
    /// </field>
    this._controller = controller;
}
Js.Controls.EventBox.ServerClass.prototype = {
    _controller: null,
    gotEventPage: null,
    gotGenericException: null,
    
    getEventPage: function Js_Controls_EventBox_ServerClass$getEventPage(key) {
        /// <param name="key" type="String">
        /// </param>
        Js.Controls.EventBox.Service.getEventPage(key, ss.Delegate.create(this, this.getEventPageSuccessCallback), ss.Delegate.create(this, this.getEventPageFailureCallback), '', 2000);
    },
    
    getEventPageSuccessCallback: function Js_Controls_EventBox_ServerClass$getEventPageSuccessCallback(page, userContext, methodName) {
        /// <param name="page" type="Js.Controls.EventBox.EventPageStub">
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
    
    getEventPageFailureCallback: function Js_Controls_EventBox_ServerClass$getEventPageFailureCallback(error, userContext, methodName) {
        /// <param name="error" type="Js.Library.WebServiceError">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this.gotGenericException != null) {
            this.gotGenericException(this, new Js.Controls.EventBox.GotExceptionEventArgs(error, methodName));
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.GotExceptionEventArgs

Js.Controls.EventBox.GotExceptionEventArgs = function Js_Controls_EventBox_GotExceptionEventArgs(error, method) {
    /// <param name="error" type="Js.Library.WebServiceError">
    /// </param>
    /// <param name="method" type="String">
    /// </param>
    /// <field name="error" type="Js.Library.WebServiceError">
    /// </field>
    /// <field name="method" type="String">
    /// </field>
    Js.Controls.EventBox.GotExceptionEventArgs.initializeBase(this);
    this.error = error;
    this.method = method;
}
Js.Controls.EventBox.GotExceptionEventArgs.prototype = {
    error: null,
    method: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.Service

Js.Controls.EventBox.Service = function Js_Controls_EventBox_Service() {
}
Js.Controls.EventBox.Service.getEventPage = function Js_Controls_EventBox_Service$getEventPage(key, success, failure, userContext, timeout) {
    /// <param name="key" type="String">
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
    p['key'] = key;
    var o = Js.Library.WebServiceHelper.options('GetEventPage', '/WebServices/Controls/EventBox/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetEventPage');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.EventBox.View

Js.Controls.EventBox.View = function Js_Controls_EventBox_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_FutureEventsTab" type="Object" domElement="true">
    /// </field>
    /// <field name="_FutureEventsTabJ" type="jQueryObject">
    /// </field>
    /// <field name="_PastEventsTab" type="Object" domElement="true">
    /// </field>
    /// <field name="_PastEventsTabJ" type="jQueryObject">
    /// </field>
    /// <field name="_TicketsTab" type="Object" domElement="true">
    /// </field>
    /// <field name="_TicketsTabJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitEnableEffects" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitEnableEffectsJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitClientID" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitClientIDJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitFirstPage" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitFirstPageJ" type="jQueryObject">
    /// </field>
    /// <field name="_TitleHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_TitleHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_MusicDropDownHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_MusicDropDownHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventIconsHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventIconsHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventIconsNavigationBackHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventIconsNavigationBackHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventIconsNavigationForwardHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventIconsNavigationForwardHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_EventInfoHolderOuter" type="Object" domElement="true">
    /// </field>
    /// <field name="_EventInfoHolderOuterJ" type="jQueryObject">
    /// </field>
    /// <field name="_BottomNavigationTitle" type="Object" domElement="true">
    /// </field>
    /// <field name="_BottomNavigationTitleJ" type="jQueryObject">
    /// </field>
    /// <field name="_BottomNavigationHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_BottomNavigationHolderJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.EventBox.View.prototype = {
    clientId: null,
    
    get_futureEventsTab: function Js_Controls_EventBox_View$get_futureEventsTab() {
        /// <value type="Object" domElement="true"></value>
        if (this._FutureEventsTab == null) {
            this._FutureEventsTab = document.getElementById(this.clientId + '_FutureEventsTab');
        }
        return this._FutureEventsTab;
    },
    
    _FutureEventsTab: null,
    
    get_futureEventsTabJ: function Js_Controls_EventBox_View$get_futureEventsTabJ() {
        /// <value type="jQueryObject"></value>
        if (this._FutureEventsTabJ == null) {
            this._FutureEventsTabJ = $('#' + this.clientId + '_FutureEventsTab');
        }
        return this._FutureEventsTabJ;
    },
    
    _FutureEventsTabJ: null,
    
    get_pastEventsTab: function Js_Controls_EventBox_View$get_pastEventsTab() {
        /// <value type="Object" domElement="true"></value>
        if (this._PastEventsTab == null) {
            this._PastEventsTab = document.getElementById(this.clientId + '_PastEventsTab');
        }
        return this._PastEventsTab;
    },
    
    _PastEventsTab: null,
    
    get_pastEventsTabJ: function Js_Controls_EventBox_View$get_pastEventsTabJ() {
        /// <value type="jQueryObject"></value>
        if (this._PastEventsTabJ == null) {
            this._PastEventsTabJ = $('#' + this.clientId + '_PastEventsTab');
        }
        return this._PastEventsTabJ;
    },
    
    _PastEventsTabJ: null,
    
    get_ticketsTab: function Js_Controls_EventBox_View$get_ticketsTab() {
        /// <value type="Object" domElement="true"></value>
        if (this._TicketsTab == null) {
            this._TicketsTab = document.getElementById(this.clientId + '_TicketsTab');
        }
        return this._TicketsTab;
    },
    
    _TicketsTab: null,
    
    get_ticketsTabJ: function Js_Controls_EventBox_View$get_ticketsTabJ() {
        /// <value type="jQueryObject"></value>
        if (this._TicketsTabJ == null) {
            this._TicketsTabJ = $('#' + this.clientId + '_TicketsTab');
        }
        return this._TicketsTabJ;
    },
    
    _TicketsTabJ: null,
    
    get_initEnableEffects: function Js_Controls_EventBox_View$get_initEnableEffects() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitEnableEffects == null) {
            this._InitEnableEffects = document.getElementById(this.clientId + '_InitEnableEffects');
        }
        return this._InitEnableEffects;
    },
    
    _InitEnableEffects: null,
    
    get_initEnableEffectsJ: function Js_Controls_EventBox_View$get_initEnableEffectsJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitEnableEffectsJ == null) {
            this._InitEnableEffectsJ = $('#' + this.clientId + '_InitEnableEffects');
        }
        return this._InitEnableEffectsJ;
    },
    
    _InitEnableEffectsJ: null,
    
    get_initClientID: function Js_Controls_EventBox_View$get_initClientID() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitClientID == null) {
            this._InitClientID = document.getElementById(this.clientId + '_InitClientID');
        }
        return this._InitClientID;
    },
    
    _InitClientID: null,
    
    get_initClientIDJ: function Js_Controls_EventBox_View$get_initClientIDJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitClientIDJ == null) {
            this._InitClientIDJ = $('#' + this.clientId + '_InitClientID');
        }
        return this._InitClientIDJ;
    },
    
    _InitClientIDJ: null,
    
    get_initFirstPage: function Js_Controls_EventBox_View$get_initFirstPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitFirstPage == null) {
            this._InitFirstPage = document.getElementById(this.clientId + '_InitFirstPage');
        }
        return this._InitFirstPage;
    },
    
    _InitFirstPage: null,
    
    get_initFirstPageJ: function Js_Controls_EventBox_View$get_initFirstPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitFirstPageJ == null) {
            this._InitFirstPageJ = $('#' + this.clientId + '_InitFirstPage');
        }
        return this._InitFirstPageJ;
    },
    
    _InitFirstPageJ: null,
    
    get_titleHolder: function Js_Controls_EventBox_View$get_titleHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._TitleHolder == null) {
            this._TitleHolder = document.getElementById(this.clientId + '_TitleHolder');
        }
        return this._TitleHolder;
    },
    
    _TitleHolder: null,
    
    get_titleHolderJ: function Js_Controls_EventBox_View$get_titleHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._TitleHolderJ == null) {
            this._TitleHolderJ = $('#' + this.clientId + '_TitleHolder');
        }
        return this._TitleHolderJ;
    },
    
    _TitleHolderJ: null,
    
    get_musicDropDownHolder: function Js_Controls_EventBox_View$get_musicDropDownHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._MusicDropDownHolder == null) {
            this._MusicDropDownHolder = document.getElementById(this.clientId + '_MusicDropDownHolder');
        }
        return this._MusicDropDownHolder;
    },
    
    _MusicDropDownHolder: null,
    
    get_musicDropDownHolderJ: function Js_Controls_EventBox_View$get_musicDropDownHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._MusicDropDownHolderJ == null) {
            this._MusicDropDownHolderJ = $('#' + this.clientId + '_MusicDropDownHolder');
        }
        return this._MusicDropDownHolderJ;
    },
    
    _MusicDropDownHolderJ: null,
    
    get_musicDropDownControl: function Js_Controls_EventBox_View$get_musicDropDownControl() {
        /// <value type="Js.Controls.MusicDropDown.Controller"></value>
        return eval(this.clientId + '_MusicDropDownControlController');
    },
    
    get_eventIconsHolder: function Js_Controls_EventBox_View$get_eventIconsHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventIconsHolder == null) {
            this._EventIconsHolder = document.getElementById(this.clientId + '_EventIconsHolder');
        }
        return this._EventIconsHolder;
    },
    
    _EventIconsHolder: null,
    
    get_eventIconsHolderJ: function Js_Controls_EventBox_View$get_eventIconsHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventIconsHolderJ == null) {
            this._EventIconsHolderJ = $('#' + this.clientId + '_EventIconsHolder');
        }
        return this._EventIconsHolderJ;
    },
    
    _EventIconsHolderJ: null,
    
    get_eventIconsNavigationBackHolder: function Js_Controls_EventBox_View$get_eventIconsNavigationBackHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventIconsNavigationBackHolder == null) {
            this._EventIconsNavigationBackHolder = document.getElementById(this.clientId + '_EventIconsNavigationBackHolder');
        }
        return this._EventIconsNavigationBackHolder;
    },
    
    _EventIconsNavigationBackHolder: null,
    
    get_eventIconsNavigationBackHolderJ: function Js_Controls_EventBox_View$get_eventIconsNavigationBackHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventIconsNavigationBackHolderJ == null) {
            this._EventIconsNavigationBackHolderJ = $('#' + this.clientId + '_EventIconsNavigationBackHolder');
        }
        return this._EventIconsNavigationBackHolderJ;
    },
    
    _EventIconsNavigationBackHolderJ: null,
    
    get_eventIconsNavigationForwardHolder: function Js_Controls_EventBox_View$get_eventIconsNavigationForwardHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventIconsNavigationForwardHolder == null) {
            this._EventIconsNavigationForwardHolder = document.getElementById(this.clientId + '_EventIconsNavigationForwardHolder');
        }
        return this._EventIconsNavigationForwardHolder;
    },
    
    _EventIconsNavigationForwardHolder: null,
    
    get_eventIconsNavigationForwardHolderJ: function Js_Controls_EventBox_View$get_eventIconsNavigationForwardHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventIconsNavigationForwardHolderJ == null) {
            this._EventIconsNavigationForwardHolderJ = $('#' + this.clientId + '_EventIconsNavigationForwardHolder');
        }
        return this._EventIconsNavigationForwardHolderJ;
    },
    
    _EventIconsNavigationForwardHolderJ: null,
    
    get_eventInfoHolderOuter: function Js_Controls_EventBox_View$get_eventInfoHolderOuter() {
        /// <value type="Object" domElement="true"></value>
        if (this._EventInfoHolderOuter == null) {
            this._EventInfoHolderOuter = document.getElementById(this.clientId + '_EventInfoHolderOuter');
        }
        return this._EventInfoHolderOuter;
    },
    
    _EventInfoHolderOuter: null,
    
    get_eventInfoHolderOuterJ: function Js_Controls_EventBox_View$get_eventInfoHolderOuterJ() {
        /// <value type="jQueryObject"></value>
        if (this._EventInfoHolderOuterJ == null) {
            this._EventInfoHolderOuterJ = $('#' + this.clientId + '_EventInfoHolderOuter');
        }
        return this._EventInfoHolderOuterJ;
    },
    
    _EventInfoHolderOuterJ: null,
    
    get_bottomNavigationTitle: function Js_Controls_EventBox_View$get_bottomNavigationTitle() {
        /// <value type="Object" domElement="true"></value>
        if (this._BottomNavigationTitle == null) {
            this._BottomNavigationTitle = document.getElementById(this.clientId + '_BottomNavigationTitle');
        }
        return this._BottomNavigationTitle;
    },
    
    _BottomNavigationTitle: null,
    
    get_bottomNavigationTitleJ: function Js_Controls_EventBox_View$get_bottomNavigationTitleJ() {
        /// <value type="jQueryObject"></value>
        if (this._BottomNavigationTitleJ == null) {
            this._BottomNavigationTitleJ = $('#' + this.clientId + '_BottomNavigationTitle');
        }
        return this._BottomNavigationTitleJ;
    },
    
    _BottomNavigationTitleJ: null,
    
    get_bottomNavigationHolder: function Js_Controls_EventBox_View$get_bottomNavigationHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._BottomNavigationHolder == null) {
            this._BottomNavigationHolder = document.getElementById(this.clientId + '_BottomNavigationHolder');
        }
        return this._BottomNavigationHolder;
    },
    
    _BottomNavigationHolder: null,
    
    get_bottomNavigationHolderJ: function Js_Controls_EventBox_View$get_bottomNavigationHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._BottomNavigationHolderJ == null) {
            this._BottomNavigationHolderJ = $('#' + this.clientId + '_BottomNavigationHolder');
        }
        return this._BottomNavigationHolderJ;
    },
    
    _BottomNavigationHolderJ: null
}


Js.Controls.EventBox.Controller.registerClass('Js.Controls.EventBox.Controller');
Js.Controls.EventBox.EventDetails.registerClass('Js.Controls.EventBox.EventDetails');
Js.Controls.EventBox.EventHtml.registerClass('Js.Controls.EventBox.EventHtml');
Js.Controls.EventBox.EventStub.registerClass('Js.Controls.EventBox.EventStub');
Js.Controls.EventBox.EventPageDetails.registerClass('Js.Controls.EventBox.EventPageDetails');
Js.Controls.EventBox.EventPageHtml.registerClass('Js.Controls.EventBox.EventPageHtml');
Js.Controls.EventBox.EventPageStub.registerClass('Js.Controls.EventBox.EventPageStub');
Js.Controls.EventBox.ServerClass.registerClass('Js.Controls.EventBox.ServerClass');
Js.Controls.EventBox.GotExceptionEventArgs.registerClass('Js.Controls.EventBox.GotExceptionEventArgs', ss.EventArgs);
Js.Controls.EventBox.Service.registerClass('Js.Controls.EventBox.Service');
Js.Controls.EventBox.View.registerClass('Js.Controls.EventBox.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
