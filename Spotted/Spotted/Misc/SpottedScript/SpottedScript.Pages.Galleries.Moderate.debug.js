Type.registerNamespace('SpottedScript.Pages.Galleries.Moderate');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Galleries.Moderate.View
SpottedScript.Pages.Galleries.Moderate.View = function SpottedScript_Pages_Galleries_Moderate_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Galleries.Moderate.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Galleries.Moderate.View.prototype = {
    clientId: null,
    get_galleriesDataGrid: function SpottedScript_Pages_Galleries_Moderate_View$get_galleriesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesDataGrid');
    },
    get_photoDataList: function SpottedScript_Pages_Galleries_Moderate_View$get_photoDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoDataList');
    },
    get_photosPanel: function SpottedScript_Pages_Galleries_Moderate_View$get_photosPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosPanel');
    },
    get_deleteButton: function SpottedScript_Pages_Galleries_Moderate_View$get_deleteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteButton');
    },
    get_deleteSelectedButton: function SpottedScript_Pages_Galleries_Moderate_View$get_deleteSelectedButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteSelectedButton');
    },
    get_deleteButton1: function SpottedScript_Pages_Galleries_Moderate_View$get_deleteButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteButton1');
    },
    get_deleteSelectedButton1: function SpottedScript_Pages_Galleries_Moderate_View$get_deleteSelectedButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteSelectedButton1');
    },
    get_adminNoteTextBox: function SpottedScript_Pages_Galleries_Moderate_View$get_adminNoteTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminNoteTextBox');
    },
    get_galleriesPanel: function SpottedScript_Pages_Galleries_Moderate_View$get_galleriesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesPanel');
    },
    get_donePanel: function SpottedScript_Pages_Galleries_Moderate_View$get_donePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonePanel');
    },
    get_infoPanel: function SpottedScript_Pages_Galleries_Moderate_View$get_infoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoPanel');
    },
    get_selectedOutputP: function SpottedScript_Pages_Galleries_Moderate_View$get_selectedOutputP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedOutputP');
    },
    get_h16ds: function SpottedScript_Pages_Galleries_Moderate_View$get_h16ds() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16ds');
    },
    get_h18: function SpottedScript_Pages_Galleries_Moderate_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_button5: function SpottedScript_Pages_Galleries_Moderate_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_h11: function SpottedScript_Pages_Galleries_Moderate_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_h14: function SpottedScript_Pages_Galleries_Moderate_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_button1: function SpottedScript_Pages_Galleries_Moderate_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_h15: function SpottedScript_Pages_Galleries_Moderate_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_button2: function SpottedScript_Pages_Galleries_Moderate_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button3: function SpottedScript_Pages_Galleries_Moderate_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_h12: function SpottedScript_Pages_Galleries_Moderate_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h17: function SpottedScript_Pages_Galleries_Moderate_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_button4: function SpottedScript_Pages_Galleries_Moderate_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_button6: function SpottedScript_Pages_Galleries_Moderate_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_h13: function SpottedScript_Pages_Galleries_Moderate_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_genericContainerPage: function SpottedScript_Pages_Galleries_Moderate_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Galleries.Moderate.View.registerClass('SpottedScript.Pages.Galleries.Moderate.View', SpottedScript.DsiUserControl.View);
