Type.registerNamespace('SpottedScript.Pages.MyGalleries');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyGalleries.View
SpottedScript.Pages.MyGalleries.View = function SpottedScript_Pages_MyGalleries_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyGalleries.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyGalleries.View.prototype = {
    clientId: null,
    get_galleriesPanel: function SpottedScript_Pages_MyGalleries_View$get_galleriesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesPanel');
    },
    get_h11: function SpottedScript_Pages_MyGalleries_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_galleriesDataGrid: function SpottedScript_Pages_MyGalleries_View$get_galleriesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesDataGrid');
    },
    get_noGalleriesPanel: function SpottedScript_Pages_MyGalleries_View$get_noGalleriesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoGalleriesPanel');
    },
    get_h12: function SpottedScript_Pages_MyGalleries_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyGalleries_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyGalleries.View.registerClass('SpottedScript.Pages.MyGalleries.View', SpottedScript.DsiUserControl.View);
