Type.registerNamespace('SpottedScript.Blank.ParaPhotoList');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.ParaPhotoList.View
SpottedScript.Blank.ParaPhotoList.View = function SpottedScript_Blank_ParaPhotoList_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.ParaPhotoList.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.ParaPhotoList.View.prototype = {
    clientId: null,
    get_photosDataList: function SpottedScript_Blank_ParaPhotoList_View$get_photosDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosDataList');
    },
    get_noPhotosDiv: function SpottedScript_Blank_ParaPhotoList_View$get_noPhotosDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPhotosDiv');
    },
    get_genericContainerPage: function SpottedScript_Blank_ParaPhotoList_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.ParaPhotoList.View.registerClass('SpottedScript.Blank.ParaPhotoList.View', SpottedScript.BlankUserControl.View);
