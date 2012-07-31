Type.registerNamespace('SpottedScript.Blank.QuickBrowserPhotoList');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.QuickBrowserPhotoList.View
SpottedScript.Blank.QuickBrowserPhotoList.View = function SpottedScript_Blank_QuickBrowserPhotoList_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.QuickBrowserPhotoList.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.QuickBrowserPhotoList.View.prototype = {
    clientId: null,
    get_photoListContent: function SpottedScript_Blank_QuickBrowserPhotoList_View$get_photoListContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoListContent');
    },
    get_genericContainerPage: function SpottedScript_Blank_QuickBrowserPhotoList_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.QuickBrowserPhotoList.View.registerClass('SpottedScript.Blank.QuickBrowserPhotoList.View', SpottedScript.BlankUserControl.View);
