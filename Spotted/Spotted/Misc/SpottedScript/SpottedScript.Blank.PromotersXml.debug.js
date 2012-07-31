Type.registerNamespace('SpottedScript.Blank.PromotersXml');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.PromotersXml.View
SpottedScript.Blank.PromotersXml.View = function SpottedScript_Blank_PromotersXml_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.PromotersXml.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.PromotersXml.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_PromotersXml_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.PromotersXml.View.registerClass('SpottedScript.Blank.PromotersXml.View', SpottedScript.BlankUserControl.View);
