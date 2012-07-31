Type.registerNamespace('SpottedScript.Controls.Tabbing.Tabs');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Tabbing.Tabs.Controller
SpottedScript.Controls.Tabbing.Tabs.Controller = function SpottedScript_Controls_Tabbing_Tabs_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.Tabbing.Tabs.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.Tabbing.Tabs.View">
    /// </field>
    /// <field name="tabDisplayUpdated" type="Sys.EventHandler">
    /// </field>
    /// <field name="selectedTab" type="SpottedScript.Controls.Tabbing.Tab.ITabController">
    /// </field>
    this._view = view;
    for (var i = 0; i < view.get_tabs().length; i++) {
        var index = i;
        var controller = view.get_tabs()[index];
        controller.setHeader(view.get_tabTitles()[i]);
        index = null;;
        controller.set_updated(SpottedScript.Misc.combineEventHandler(controller.get_updated(), Function.createDelegate(this, this._onTabUpdated)));
    }
    this.selectedTab = view.get_tabs()[0];
    jQuery('.ui-tabs-nav').bind('tabsselect', Function.createDelegate(this, function(ev, ui1, ui2) {
        if (ui1 == null) {
            ui1 = ui2;
        }
        this._onTabSelect(ev, ui1);
    }));
}
SpottedScript.Controls.Tabbing.Tabs.Controller.prototype = {
    _view: null,
    _onTabUpdated: function SpottedScript_Controls_Tabbing_Tabs_Controller$_onTabUpdated(sender, ev) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="ev" type="Sys.EventArgs">
        /// </param>
        if (this.tabDisplayUpdated != null) {
            this.tabDisplayUpdated(sender, ev);
        }
    },
    _onTabSelect: function SpottedScript_Controls_Tabbing_Tabs_Controller$_onTabSelect(ev, ui) {
        /// <param name="ev" type="Object">
        /// </param>
        /// <param name="ui" type="JQ.TabsSelectEventArgs">
        /// </param>
        var tabIndex = 0;
        for (; tabIndex < this._view.get_tabs().length; tabIndex++) {
            if (ui.panel === this._view.get_tabs()[tabIndex].get_uiPanel()) {
                break;
            }
        }
        this.selectedTab = this._view.get_tabs()[tabIndex];
        if (this.tabDisplayUpdated != null) {
            Utils.Trace.write('Firing TabDisplayUpdated ');
            this.tabDisplayUpdated(this.selectedTab, Sys.EventArgs.Empty);
        }
        else {
            Utils.Trace.write('TabDisplayUpdated is null');
        }
    },
    tabDisplayUpdated: null,
    selectedTab: null,
    isSelected: function SpottedScript_Controls_Tabbing_Tabs_Controller$isSelected(tab) {
        /// <param name="tab" type="SpottedScript.Controls.Tabbing.Tab.ITabController">
        /// </param>
        /// <returns type="Boolean"></returns>
        return tab === this.selectedTab;
    },
    applyToTabs: function SpottedScript_Controls_Tabbing_Tabs_Controller$applyToTabs(func) {
        /// <param name="func" type="SpottedScript.Controls.Tabbing.Tabs.TabFunc">
        /// </param>
        for (var i = 0; i < this._view.get_tabs().length; i++) {
            func(this._view.get_tabs()[i]);
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Tabbing.Tabs.View
SpottedScript.Controls.Tabbing.Tabs.View = function SpottedScript_Controls_Tabbing_Tabs_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.Tabbing.Tabs.View.prototype = {
    get_tabs: function SpottedScript_Controls_Tabbing_Tabs_View$get_tabs() {
        /// <value type="Array" elementType="ITabController"></value>
        var tabs = [];
        var tabControllerNames = this.get_uiTabControllerNames().value.split('|');
        for (var i = 0; i < tabControllerNames.length; i++) {
            tabs[tabs.length] = eval(tabControllerNames[i]);
        }
        return tabs;
    },
    get_tabTitles: function SpottedScript_Controls_Tabbing_Tabs_View$get_tabTitles() {
        /// <value type="Array" elementType="Object" elementDomElement="true"></value>
        var tabTitles = [];
        var tabTitleIds = this.get_uiTabTitleIds().value.split('|');
        for (var i = 0; i < tabTitleIds.length; i++) {
            tabTitles[tabTitles.length] = document.getElementById(tabTitleIds[i]);
        }
        return tabTitles;
    },
    clientId: null,
    get_uiTabsDiv: function SpottedScript_Controls_Tabbing_Tabs_View$get_uiTabsDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTabsDiv');
    },
    get_uiTabsLinksContainer: function SpottedScript_Controls_Tabbing_Tabs_View$get_uiTabsLinksContainer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTabsLinksContainer');
    },
    get_uiTabControllerNames: function SpottedScript_Controls_Tabbing_Tabs_View$get_uiTabControllerNames() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTabControllerNames');
    },
    get_uiTabTitleIds: function SpottedScript_Controls_Tabbing_Tabs_View$get_uiTabTitleIds() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTabTitleIds');
    }
}
SpottedScript.Controls.Tabbing.Tabs.Controller.registerClass('SpottedScript.Controls.Tabbing.Tabs.Controller');
SpottedScript.Controls.Tabbing.Tabs.View.registerClass('SpottedScript.Controls.Tabbing.Tabs.View');
