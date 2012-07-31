Type.registerNamespace('SpottedScript.Controls.ClientSideRepeater.Repeater');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ClientSideRepeater.Repeater.Controller
SpottedScript.Controls.ClientSideRepeater.Repeater.Controller = function SpottedScript_Controls_ClientSideRepeater_Repeater_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.ClientSideRepeater.Repeater.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Controls.ClientSideRepeater.Repeater.View">
    /// </field>
    /// <field name="_groupByField" type="String">
    /// </field>
    this.view = view;
}
SpottedScript.Controls.ClientSideRepeater.Repeater.Controller.prototype = {
    view: null,
    _groupByField: 'Date',
    displayData: function SpottedScript_Controls_ClientSideRepeater_Repeater_Controller$displayData(data) {
        /// <param name="data" type="Array" elementType="Object">
        /// </param>
        var sb = new Sys.StringBuilder();
        sb.append(unescape(this.view.get_uiHeaderTemplateHolder().innerHTML));
        if (data != null) {
            var currentGroupByFieldValue = '';
            for (var i = 0; i < data.length; i++) {
                var dataItem = data[i];
                if (this._groupByField !== '') {
                    if (currentGroupByFieldValue !== dataItem[this._groupByField]) {
                        currentGroupByFieldValue = dataItem[this._groupByField];
                        sb.append('<div class=\'ClientSideRepeaterGroupHeader\'>' + currentGroupByFieldValue + '</div>');
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
// SpottedScript.Controls.ClientSideRepeater.Repeater.View
SpottedScript.Controls.ClientSideRepeater.Repeater.View = function SpottedScript_Controls_ClientSideRepeater_Repeater_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.ClientSideRepeater.Repeater.View.prototype = {
    clientId: null,
    get_uiItemTemplate: function SpottedScript_Controls_ClientSideRepeater_Repeater_View$get_uiItemTemplate() {
        /// <value type="SpottedScript.Controls.ClientSideRepeater.Template.Controller"></value>
        return eval(this.clientId + '_uiItemTemplateController');
    },
    get_uiContent: function SpottedScript_Controls_ClientSideRepeater_Repeater_View$get_uiContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiContent');
    },
    get_uiHeaderTemplateHolder: function SpottedScript_Controls_ClientSideRepeater_Repeater_View$get_uiHeaderTemplateHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiHeaderTemplateHolder');
    },
    get_uiBetweenTemplateHolder: function SpottedScript_Controls_ClientSideRepeater_Repeater_View$get_uiBetweenTemplateHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBetweenTemplateHolder');
    },
    get_uiFooterTemplateHolder: function SpottedScript_Controls_ClientSideRepeater_Repeater_View$get_uiFooterTemplateHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFooterTemplateHolder');
    }
}
SpottedScript.Controls.ClientSideRepeater.Repeater.Controller.registerClass('SpottedScript.Controls.ClientSideRepeater.Repeater.Controller');
SpottedScript.Controls.ClientSideRepeater.Repeater.View.registerClass('SpottedScript.Controls.ClientSideRepeater.Repeater.View');
