Type.registerNamespace('SpottedScript.Controls.MultiUsrDrop');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MultiUsrDrop.Controller
SpottedScript.Controls.MultiUsrDrop.Controller = function SpottedScript_Controls_MultiUsrDrop_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.MultiUsrDrop.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.MultiUsrDrop.View">
    /// </field>
    this._view = view;
}
SpottedScript.Controls.MultiUsrDrop.Controller.prototype = {
    _view: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MultiUsrDrop.View
SpottedScript.Controls.MultiUsrDrop.View = function SpottedScript_Controls_MultiUsrDrop_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.MultiUsrDrop.View.prototype = {
    clientId: null,
    get_drop: function SpottedScript_Controls_MultiUsrDrop_View$get_drop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Drop');
    },
    get_selectBox: function SpottedScript_Controls_MultiUsrDrop_View$get_selectBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectBox');
    },
    get_addButton: function SpottedScript_Controls_MultiUsrDrop_View$get_addButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddButton');
    },
    get_removeButton: function SpottedScript_Controls_MultiUsrDrop_View$get_removeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RemoveButton');
    },
    get_buddiesTable: function SpottedScript_Controls_MultiUsrDrop_View$get_buddiesTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesTable');
    },
    get_tr1: function SpottedScript_Controls_MultiUsrDrop_View$get_tr1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Tr1');
    },
    get_td1: function SpottedScript_Controls_MultiUsrDrop_View$get_td1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Td1');
    },
    get_clipSpan: function SpottedScript_Controls_MultiUsrDrop_View$get_clipSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClipSpan');
    },
    get_td2: function SpottedScript_Controls_MultiUsrDrop_View$get_td2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Td2');
    },
    get_picHolder: function SpottedScript_Controls_MultiUsrDrop_View$get_picHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicHolder');
    },
    get_moreButton: function SpottedScript_Controls_MultiUsrDrop_View$get_moreButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreButton');
    },
    get_buddiesRemovedPanel: function SpottedScript_Controls_MultiUsrDrop_View$get_buddiesRemovedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesRemovedPanel');
    },
    get_buddiesRemovedLabel: function SpottedScript_Controls_MultiUsrDrop_View$get_buddiesRemovedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesRemovedLabel');
    },
    get_buddiesRemovedGroupPanel: function SpottedScript_Controls_MultiUsrDrop_View$get_buddiesRemovedGroupPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesRemovedGroupPanel');
    },
    get_buddiesRemovedGroupLabel: function SpottedScript_Controls_MultiUsrDrop_View$get_buddiesRemovedGroupLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesRemovedGroupLabel');
    },
    get_noBuddiesPanel: function SpottedScript_Controls_MultiUsrDrop_View$get_noBuddiesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoBuddiesPanel');
    },
    get_noBuddiesThreadPanel: function SpottedScript_Controls_MultiUsrDrop_View$get_noBuddiesThreadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoBuddiesThreadPanel');
    },
    get_addAllPanel: function SpottedScript_Controls_MultiUsrDrop_View$get_addAllPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddAllPanel');
    },
    get_addAllPlaceDrop: function SpottedScript_Controls_MultiUsrDrop_View$get_addAllPlaceDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddAllPlaceDrop');
    },
    get_addAllMusicDrop: function SpottedScript_Controls_MultiUsrDrop_View$get_addAllMusicDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddAllMusicDrop');
    },
    get_button1: function SpottedScript_Controls_MultiUsrDrop_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_addAllShowAllItemsCheck: function SpottedScript_Controls_MultiUsrDrop_View$get_addAllShowAllItemsCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddAllShowAllItemsCheck');
    },
    get_addMorePanel: function SpottedScript_Controls_MultiUsrDrop_View$get_addMorePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddMorePanel');
    },
    get_addMoreP: function SpottedScript_Controls_MultiUsrDrop_View$get_addMoreP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddMoreP');
    },
    get_addMoreTextBox: function SpottedScript_Controls_MultiUsrDrop_View$get_addMoreTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddMoreTextBox');
    },
    get_addMoreP1: function SpottedScript_Controls_MultiUsrDrop_View$get_addMoreP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddMoreP1');
    },
    get_addMoreButton: function SpottedScript_Controls_MultiUsrDrop_View$get_addMoreButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddMoreButton');
    },
    get_addMoreAddBuddyCheckBox: function SpottedScript_Controls_MultiUsrDrop_View$get_addMoreAddBuddyCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddMoreAddBuddyCheckBox');
    },
    get_values: function SpottedScript_Controls_MultiUsrDrop_View$get_values() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Values');
    },
    get_texts: function SpottedScript_Controls_MultiUsrDrop_View$get_texts() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Texts');
    }
}
SpottedScript.Controls.MultiUsrDrop.Controller.registerClass('SpottedScript.Controls.MultiUsrDrop.Controller');
SpottedScript.Controls.MultiUsrDrop.View.registerClass('SpottedScript.Controls.MultiUsrDrop.View');
