Type.registerNamespace('SpottedScript.Controls.LinkCloud');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.LinkCloud.View
SpottedScript.Controls.LinkCloud.View = function SpottedScript_Controls_LinkCloud_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.LinkCloud.View.prototype = {
    clientId: null,
    get_panel1: function SpottedScript_Controls_LinkCloud_View$get_panel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Panel1');
    }
}
SpottedScript.Controls.LinkCloud.View.registerClass('SpottedScript.Controls.LinkCloud.View');
