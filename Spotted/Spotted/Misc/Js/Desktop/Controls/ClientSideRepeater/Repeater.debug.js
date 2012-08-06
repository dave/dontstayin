//! Repeater.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.ClientSideRepeater.Repeater');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ClientSideRepeater.Repeater.Controller

Js.Controls.ClientSideRepeater.Repeater.Controller = function Js_Controls_ClientSideRepeater_Repeater_Controller(view) {
    /// <param name="view" type="Js.Controls.ClientSideRepeater.Repeater.View">
    /// </param>
    /// <field name="view" type="Js.Controls.ClientSideRepeater.Repeater.View">
    /// </field>
    /// <field name="_groupByField" type="String">
    /// </field>
    this.view = view;
}
Js.Controls.ClientSideRepeater.Repeater.Controller.prototype = {
    view: null,
    _groupByField: 'Date',
    
    displayData: function Js_Controls_ClientSideRepeater_Repeater_Controller$displayData(data) {
        /// <param name="data" type="Array" elementType="Object">
        /// </param>
        var sb = new ss.StringBuilder();
        sb.append(unescape(this.view.get_uiHeaderTemplateHolder().innerHTML));
        if (data != null) {
            var currentGroupByFieldValue = '';
            for (var i = 0; i < data.length; i++) {
                var dataItem = data[i];
                if (!!this._groupByField) {
                    if (currentGroupByFieldValue !== dataItem[this._groupByField]) {
                        currentGroupByFieldValue = dataItem[this._groupByField];
                        sb.append("<div class='ClientSideRepeaterGroupHeader'>" + currentGroupByFieldValue + '</div>');
                    }
                }
                sb.append(this.view.get_uiItemTemplate().render(dataItem));
                if (i + 1 < data.length) {
                    sb.append(unescape(this.view.get_uiBetweenTemplateHolder().innerHTML));
                }
            }
        }
        sb.append(unescape(this.view.get_uiFooterTemplateHolder().innerHTML));
        this.view.get_uiContent().innerHTML = sb.toString();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ClientSideRepeater.Repeater.View

Js.Controls.ClientSideRepeater.Repeater.View = function Js_Controls_ClientSideRepeater_Repeater_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiHeaderTemplateHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiHeaderTemplateHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiBetweenTemplateHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiBetweenTemplateHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiFooterTemplateHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiFooterTemplateHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiContent" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiContentJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.ClientSideRepeater.Repeater.View.prototype = {
    clientId: null,
    
    get_uiHeaderTemplateHolder: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiHeaderTemplateHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiHeaderTemplateHolder == null) {
            this._uiHeaderTemplateHolder = document.getElementById(this.clientId + '_uiHeaderTemplateHolder');
        }
        return this._uiHeaderTemplateHolder;
    },
    
    _uiHeaderTemplateHolder: null,
    
    get_uiHeaderTemplateHolderJ: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiHeaderTemplateHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiHeaderTemplateHolderJ == null) {
            this._uiHeaderTemplateHolderJ = $('#' + this.clientId + '_uiHeaderTemplateHolder');
        }
        return this._uiHeaderTemplateHolderJ;
    },
    
    _uiHeaderTemplateHolderJ: null,
    
    get_uiBetweenTemplateHolder: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiBetweenTemplateHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiBetweenTemplateHolder == null) {
            this._uiBetweenTemplateHolder = document.getElementById(this.clientId + '_uiBetweenTemplateHolder');
        }
        return this._uiBetweenTemplateHolder;
    },
    
    _uiBetweenTemplateHolder: null,
    
    get_uiBetweenTemplateHolderJ: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiBetweenTemplateHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiBetweenTemplateHolderJ == null) {
            this._uiBetweenTemplateHolderJ = $('#' + this.clientId + '_uiBetweenTemplateHolder');
        }
        return this._uiBetweenTemplateHolderJ;
    },
    
    _uiBetweenTemplateHolderJ: null,
    
    get_uiFooterTemplateHolder: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiFooterTemplateHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiFooterTemplateHolder == null) {
            this._uiFooterTemplateHolder = document.getElementById(this.clientId + '_uiFooterTemplateHolder');
        }
        return this._uiFooterTemplateHolder;
    },
    
    _uiFooterTemplateHolder: null,
    
    get_uiFooterTemplateHolderJ: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiFooterTemplateHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiFooterTemplateHolderJ == null) {
            this._uiFooterTemplateHolderJ = $('#' + this.clientId + '_uiFooterTemplateHolder');
        }
        return this._uiFooterTemplateHolderJ;
    },
    
    _uiFooterTemplateHolderJ: null,
    
    get_uiItemTemplate: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiItemTemplate() {
        /// <value type="Js.Controls.ClientSideRepeater.Template.Controller"></value>
        return eval(this.clientId + '_uiItemTemplateController');
    },
    
    get_uiContent: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiContent() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiContent == null) {
            this._uiContent = document.getElementById(this.clientId + '_uiContent');
        }
        return this._uiContent;
    },
    
    _uiContent: null,
    
    get_uiContentJ: function Js_Controls_ClientSideRepeater_Repeater_View$get_uiContentJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiContentJ == null) {
            this._uiContentJ = $('#' + this.clientId + '_uiContent');
        }
        return this._uiContentJ;
    },
    
    _uiContentJ: null
}


Js.Controls.ClientSideRepeater.Repeater.Controller.registerClass('Js.Controls.ClientSideRepeater.Repeater.Controller');
Js.Controls.ClientSideRepeater.Repeater.View.registerClass('Js.Controls.ClientSideRepeater.Repeater.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
