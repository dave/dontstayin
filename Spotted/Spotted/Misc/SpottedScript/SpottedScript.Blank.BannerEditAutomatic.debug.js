Type.registerNamespace('SpottedScript.Blank.BannerEditAutomatic');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerEditAutomatic.View
SpottedScript.Blank.BannerEditAutomatic.View = function SpottedScript_Blank_BannerEditAutomatic_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerEditAutomatic.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerEditAutomatic.View.prototype = {
    clientId: null,
    get_h120: function SpottedScript_Blank_BannerEditAutomatic_View$get_h120() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H120');
    },
    get_customiseFirstLine: function SpottedScript_Blank_BannerEditAutomatic_View$get_customiseFirstLine() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomiseFirstLine');
    },
    get_customiseFirstLineSize: function SpottedScript_Blank_BannerEditAutomatic_View$get_customiseFirstLineSize() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomiseFirstLineSize');
    },
    get_customiseSecondLine: function SpottedScript_Blank_BannerEditAutomatic_View$get_customiseSecondLine() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomiseSecondLine');
    },
    get_customiseThirdLine: function SpottedScript_Blank_BannerEditAutomatic_View$get_customiseThirdLine() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomiseThirdLine');
    },
    get_button9: function SpottedScript_Blank_BannerEditAutomatic_View$get_button9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button9');
    },
    get_saveButton: function SpottedScript_Blank_BannerEditAutomatic_View$get_saveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SaveButton');
    },
    get_removeButton: function SpottedScript_Blank_BannerEditAutomatic_View$get_removeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RemoveButton');
    },
    get_genericContainerPage: function SpottedScript_Blank_BannerEditAutomatic_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerEditAutomatic.View.registerClass('SpottedScript.Blank.BannerEditAutomatic.View', SpottedScript.BlankUserControl.View);
