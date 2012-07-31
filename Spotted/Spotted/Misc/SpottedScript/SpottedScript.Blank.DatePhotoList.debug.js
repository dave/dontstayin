Type.registerNamespace('SpottedScript.Blank.DatePhotoList');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.DatePhotoList.View
SpottedScript.Blank.DatePhotoList.View = function SpottedScript_Blank_DatePhotoList_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.DatePhotoList.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.DatePhotoList.View.prototype = {
    clientId: null,
    get_datePhotoListContent: function SpottedScript_Blank_DatePhotoList_View$get_datePhotoListContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DatePhotoListContent');
    },
    get_genericContainerPage: function SpottedScript_Blank_DatePhotoList_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.DatePhotoList.View.registerClass('SpottedScript.Blank.DatePhotoList.View', SpottedScript.BlankUserControl.View);
