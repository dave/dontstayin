Type.registerNamespace('SpottedScript.Pages.Galleries.Add');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Galleries.Add.View
SpottedScript.Pages.Galleries.Add.View = function SpottedScript_Pages_Galleries_Add_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Galleries.Add.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Galleries.Add.View.prototype = {
    clientId: null,
    get_noEditArticlePanel: function SpottedScript_Pages_Galleries_Add_View$get_noEditArticlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoEditArticlePanel');
    },
    get_cantAddGallery: function SpottedScript_Pages_Galleries_Add_View$get_cantAddGallery() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CantAddGallery');
    },
    get_panelNoPhoto: function SpottedScript_Pages_Galleries_Add_View$get_panelNoPhoto() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoPhoto');
    },
    get_editCurrentGalleryLink: function SpottedScript_Pages_Galleries_Add_View$get_editCurrentGalleryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditCurrentGalleryLink');
    },
    get_eventHasGalleriesPanel: function SpottedScript_Pages_Galleries_Add_View$get_eventHasGalleriesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventHasGalleriesPanel');
    },
    get_galleriesDataGrid: function SpottedScript_Pages_Galleries_Add_View$get_galleriesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesDataGrid');
    },
    get_futureEventPanel: function SpottedScript_Pages_Galleries_Add_View$get_futureEventPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FutureEventPanel');
    },
    get_header: function SpottedScript_Pages_Galleries_Add_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_h14: function SpottedScript_Pages_Galleries_Add_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_h13: function SpottedScript_Pages_Galleries_Add_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_h12: function SpottedScript_Pages_Galleries_Add_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h11: function SpottedScript_Pages_Galleries_Add_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Pages_Galleries_Add_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Galleries.Add.View.registerClass('SpottedScript.Pages.Galleries.Add.View', SpottedScript.DsiUserControl.View);
