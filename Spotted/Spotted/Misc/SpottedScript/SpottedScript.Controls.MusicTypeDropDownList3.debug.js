Type.registerNamespace('SpottedScript.Controls.MusicTypeDropDownList3');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MusicTypeDropDownList3.Controller
SpottedScript.Controls.MusicTypeDropDownList3.Controller = function SpottedScript_Controls_MusicTypeDropDownList3_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.MusicTypeDropDownList3.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Controls.MusicTypeDropDownList3.View">
    /// </field>
    this.view = view;
}
SpottedScript.Controls.MusicTypeDropDownList3.Controller.prototype = {
    view: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MusicTypeDropDownList3.View
SpottedScript.Controls.MusicTypeDropDownList3.View = function SpottedScript_Controls_MusicTypeDropDownList3_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.MusicTypeDropDownList3.View.prototype = {
    clientId: null,
    get_dropDown: function SpottedScript_Controls_MusicTypeDropDownList3_View$get_dropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DropDown');
    }
}
SpottedScript.Controls.MusicTypeDropDownList3.Controller.registerClass('SpottedScript.Controls.MusicTypeDropDownList3.Controller');
SpottedScript.Controls.MusicTypeDropDownList3.View.registerClass('SpottedScript.Controls.MusicTypeDropDownList3.View');
