Type.registerNamespace('SpottedScript.Pages.Photos.FrontPageCrop');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Photos.FrontPageCrop.View
SpottedScript.Pages.Photos.FrontPageCrop.View = function SpottedScript_Pages_Photos_FrontPageCrop_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Photos.FrontPageCrop.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Photos.FrontPageCrop.View.prototype = {
    clientId: null,
    get_parentName: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_parentName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ParentName');
    },
    get_currentImage: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_currentImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CurrentImage');
    },
    get_cropper: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_cropper() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cropper');
    },
    get_checkBox: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_checkBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CheckBox');
    },
    get_date: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_date() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_DateController');
    },
    get_caption: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_caption() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Caption');
    },
    get_colourBlackOnWhite: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_colourBlackOnWhite() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ColourBlackOnWhite');
    },
    get_colourWhiteOnBlack: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_colourWhiteOnBlack() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ColourWhiteOnBlack');
    },
    get_cornerTopLeft: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_cornerTopLeft() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CornerTopLeft');
    },
    get_cornerTopRight: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_cornerTopRight() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CornerTopRight');
    },
    get_cornerBottomLeft: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_cornerBottomLeft() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CornerBottomLeft');
    },
    get_cornerBottomRight: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_cornerBottomRight() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CornerBottomRight');
    },
    get_button3: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_genericContainerPage: function SpottedScript_Pages_Photos_FrontPageCrop_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Photos.FrontPageCrop.View.registerClass('SpottedScript.Pages.Photos.FrontPageCrop.View', SpottedScript.DsiUserControl.View);
