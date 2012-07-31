Type.registerNamespace('SpottedScript.Blank.DrinkingAge');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.DrinkingAge.View
SpottedScript.Blank.DrinkingAge.View = function SpottedScript_Blank_DrinkingAge_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.DrinkingAge.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.DrinkingAge.View.prototype = {
    clientId: null,
    get_day: function SpottedScript_Blank_DrinkingAge_View$get_day() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Day');
    },
    get_month: function SpottedScript_Blank_DrinkingAge_View$get_month() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Month');
    },
    get_year: function SpottedScript_Blank_DrinkingAge_View$get_year() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Year');
    },
    get_countryDrop: function SpottedScript_Blank_DrinkingAge_View$get_countryDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CountryDrop');
    },
    get_entryVal: function SpottedScript_Blank_DrinkingAge_View$get_entryVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryVal');
    },
    get_genericContainerPage: function SpottedScript_Blank_DrinkingAge_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.DrinkingAge.View.registerClass('SpottedScript.Blank.DrinkingAge.View', SpottedScript.BlankUserControl.View);
