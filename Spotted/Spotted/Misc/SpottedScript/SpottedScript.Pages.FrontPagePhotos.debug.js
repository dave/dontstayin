Type.registerNamespace('SpottedScript.Pages.FrontPagePhotos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.FrontPagePhotos.View
SpottedScript.Pages.FrontPagePhotos.View = function SpottedScript_Pages_FrontPagePhotos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.FrontPagePhotos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.FrontPagePhotos.View.prototype = {
    clientId: null,
    get_h18: function SpottedScript_Pages_FrontPagePhotos_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_h19: function SpottedScript_Pages_FrontPagePhotos_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_genericContainerPage: function SpottedScript_Pages_FrontPagePhotos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.FrontPagePhotos.View.registerClass('SpottedScript.Pages.FrontPagePhotos.View', SpottedScript.DsiUserControl.View);
