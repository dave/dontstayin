Type.registerNamespace('SpottedScript.Controls.PagedData.Display');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Display.Controller
SpottedScript.Controls.PagedData.Display.Controller = function SpottedScript_Controls_PagedData_Display_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PagedData.Display.View">
    /// </param>
    /// <field name="_updated" type="Sys.EventHandler">
    /// </field>
    /// <field name="_view" type="SpottedScript.Controls.PagedData.Display.View">
    /// </field>
    /// <field name="currentData" type="Array" elementType="MapItem">
    /// </field>
    /// <field name="_provider" type="SpottedScript.Utils._cachedPagedProvider">
    /// </field>
    /// <field name="_parameterSources" type="Array" elementType="_iParameterSource">
    /// </field>
    /// <field name="header" type="Object" domElement="true">
    /// </field>
    this._view = view;
    this._view.get_uiPager().set__currentPage(1);
    this._view.get_uiPager()._onPageChanged = Function.createDelegate(this, function() {
        this.displayInfoForTab();
    });
    this._provider = new SpottedScript.Utils._cachedPagedProvider(new SpottedScript.Utils._pagedProviderService(view.get_uiServicePath().value, view.get_uiServiceMethod().value, Number.parseInvariant(view.get_uiTimeout().value)));
    for (var i = 0; i < this.get__parameterSources().length; i++) {
        this.get__parameterSources()[i].set_parametersUpdated(SpottedScript.Misc.combineEventHandler(this.get__parameterSources()[i].get_parametersUpdated(), Function.createDelegate(this, this._onParametersUpdated)));
    }
}
SpottedScript.Controls.PagedData.Display.Controller._getDataFromMapItems = function SpottedScript_Controls_PagedData_Display_Controller$_getDataFromMapItems(mapItem) {
    /// <param name="mapItem" type="Array" elementType="MapItem">
    /// </param>
    /// <returns type="Array" elementType="Object"></returns>
    var data = new Array(mapItem.length);
    for (var i = 0; i < data.length; i++) {
        data[i] = mapItem[i].data;
    }
    return data;
}
SpottedScript.Controls.PagedData.Display.Controller.prototype = {
    _updated: null,
    get_updated: function SpottedScript_Controls_PagedData_Display_Controller$get_updated() {
        /// <value type="Sys.EventHandler"></value>
        return this._updated;
    },
    set_updated: function SpottedScript_Controls_PagedData_Display_Controller$set_updated(value) {
        /// <value type="Sys.EventHandler"></value>
        this._updated = value;
        return value;
    },
    _view: null,
    _onParametersUpdated: function SpottedScript_Controls_PagedData_Display_Controller$_onParametersUpdated(sender, e) {
        /// <param name="sender" type="Object">
        /// </param>
        /// <param name="e" type="Sys.EventArgs">
        /// </param>
        this.displayInfoForTab();
    },
    currentData: null,
    _dataReturned: function SpottedScript_Controls_PagedData_Display_Controller$_dataReturned(result, totalData, moreDataAvailable) {
        /// <param name="result" type="Array" elementType="MapItem">
        /// </param>
        /// <param name="totalData" type="Number" integer="true">
        /// </param>
        /// <param name="moreDataAvailable" type="Boolean">
        /// </param>
        this.currentData = result;
        Utils.Trace.write('MapInfoReturned');
        this._view.get_uiRepeater().displayData(SpottedScript.Controls.PagedData.Display.Controller._getDataFromMapItems(this.currentData));
        this._view.get_uiPager().set__lastPage((moreDataAvailable) ? -1 : Math.ceil(totalData / this.get__pageSize()));
        if (this.header != null) {
            this.header.innerHTML = '<center>' + this._view.get_uiTabName().value + '<br>(' + totalData + ((moreDataAvailable) ? '+' : '') + ')' + '</center>';
        }
        if (this.get_updated() != null) {
            this.get_updated()(this, null);
        }
    },
    get__pageSize: function SpottedScript_Controls_PagedData_Display_Controller$get__pageSize() {
        /// <value type="Number" integer="true"></value>
        return Number.parseInvariant(this._view.get_uiPageSize().value);
    },
    get_uiPanel: function SpottedScript_Controls_PagedData_Display_Controller$get_uiPanel() {
        /// <value type="Object" domElement="true"></value>
        return this._view.get_uiPanel();
    },
    _provider: null,
    displayInfoForTab: function SpottedScript_Controls_PagedData_Display_Controller$displayInfoForTab() {
        var parameterSourceParameters = {};
        for (var i = 0; i < this.get__parameterSources().length; i++) {
            var $dict1 = this.get__parameterSources()[i].get_parameters();
            for (var $key2 in $dict1) {
                var de = { key: $key2, value: $dict1[$key2] };
                parameterSourceParameters[de.key] = de.value;
            }
        }
        this._provider._get(this._view.get_uiPager().get__currentPage(), this.get__pageSize(), parameterSourceParameters, Function.createDelegate(this, this._onResultsGot), Function.createDelegate(this, this._onFailure));
    },
    _parameterSources: null,
    get__parameterSources: function SpottedScript_Controls_PagedData_Display_Controller$get__parameterSources() {
        /// <value type="Array" elementType="_iParameterSource"></value>
        if (this._parameterSources == null) {
            var parameterSourceNames = this._view.get_uiParameterSourceNames().value.split(',');
            this._parameterSources = new Array(parameterSourceNames.length);
            for (var i = 0; i < parameterSourceNames.length; i++) {
                this._parameterSources[i] = eval(parameterSourceNames[i]);
            }
        }
        return this._parameterSources;
    },
    _onResultsGot: function SpottedScript_Controls_PagedData_Display_Controller$_onResultsGot(results, totalData, moreDataAvailable) {
        /// <param name="results" type="Array" elementType="Object">
        /// </param>
        /// <param name="totalData" type="Number" integer="true">
        /// </param>
        /// <param name="moreDataAvailable" type="Boolean">
        /// </param>
        this._dataReturned(results, totalData, moreDataAvailable);
    },
    _onFailure: function SpottedScript_Controls_PagedData_Display_Controller$_onFailure() {
        this._dataReturned(new Array(0), 0, false);
    },
    header: null,
    setHeader: function SpottedScript_Controls_PagedData_Display_Controller$setHeader(el) {
        /// <param name="el" type="Object" domElement="true">
        /// </param>
        this.header = el;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Display.View
SpottedScript.Controls.PagedData.Display.View = function SpottedScript_Controls_PagedData_Display_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.PagedData.Display.View.prototype = {
    clientId: null,
    get_uiHeaderControl: function SpottedScript_Controls_PagedData_Display_View$get_uiHeaderControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiHeaderControl');
    },
    get_uiPanel: function SpottedScript_Controls_PagedData_Display_View$get_uiPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPanel');
    },
    get_uiPager: function SpottedScript_Controls_PagedData_Display_View$get_uiPager() {
        /// <value type="SpottedScript.Controls.PaginationControl2.Controller"></value>
        return eval(this.clientId + '_uiPagerController');
    },
    get_uiRepeater: function SpottedScript_Controls_PagedData_Display_View$get_uiRepeater() {
        /// <value type="SpottedScript.Controls.ClientSideRepeater.Repeater.Controller"></value>
        return eval(this.clientId + '_uiRepeaterController');
    },
    get_uiDefaultTop: function SpottedScript_Controls_PagedData_Display_View$get_uiDefaultTop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDefaultTop');
    },
    get_uiPageSize: function SpottedScript_Controls_PagedData_Display_View$get_uiPageSize() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPageSize');
    },
    get_uiServicePath: function SpottedScript_Controls_PagedData_Display_View$get_uiServicePath() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiServicePath');
    },
    get_uiServiceMethod: function SpottedScript_Controls_PagedData_Display_View$get_uiServiceMethod() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiServiceMethod');
    },
    get_uiParameterSourceNames: function SpottedScript_Controls_PagedData_Display_View$get_uiParameterSourceNames() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiParameterSourceNames');
    },
    get_uiTimeout: function SpottedScript_Controls_PagedData_Display_View$get_uiTimeout() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTimeout');
    },
    get_uiTabName: function SpottedScript_Controls_PagedData_Display_View$get_uiTabName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTabName');
    }
}
SpottedScript.Controls.PagedData.Display.Controller.registerClass('SpottedScript.Controls.PagedData.Display.Controller', null, SpottedScript.Controls.Tabbing.Tab.ITabController);
SpottedScript.Controls.PagedData.Display.View.registerClass('SpottedScript.Controls.PagedData.Display.View');
