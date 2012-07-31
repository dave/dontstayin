Type.registerNamespace('SpottedScript.Blank.PrintPromoterLetters');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.PrintPromoterLetters.View
SpottedScript.Blank.PrintPromoterLetters.View = function SpottedScript_Blank_PrintPromoterLetters_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.PrintPromoterLetters.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.PrintPromoterLetters.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_PrintPromoterLetters_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.PrintPromoterLetters.View.registerClass('SpottedScript.Blank.PrintPromoterLetters.View', SpottedScript.BlankUserControl.View);
