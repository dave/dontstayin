Type.registerNamespace('SpottedScript.Pages.MyPicture');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyPicture.View
SpottedScript.Pages.MyPicture.View = function SpottedScript_Pages_MyPicture_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyPicture.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyPicture.View.prototype = {
    clientId: null,
    get_panelNoPhotosMe: function SpottedScript_Pages_MyPicture_View$get_panelNoPhotosMe() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoPhotosMe');
    },
    get_allPhotosPageP1: function SpottedScript_Pages_MyPicture_View$get_allPhotosPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllPhotosPageP1');
    },
    get_allPhotosPageP2: function SpottedScript_Pages_MyPicture_View$get_allPhotosPageP2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllPhotosPageP2');
    },
    get_allPhotosPrevPage1: function SpottedScript_Pages_MyPicture_View$get_allPhotosPrevPage1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllPhotosPrevPage1');
    },
    get_allPhotosNextPage1: function SpottedScript_Pages_MyPicture_View$get_allPhotosNextPage1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllPhotosNextPage1');
    },
    get_allPhotosPrevPage2: function SpottedScript_Pages_MyPicture_View$get_allPhotosPrevPage2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllPhotosPrevPage2');
    },
    get_allPhotosNextPage2: function SpottedScript_Pages_MyPicture_View$get_allPhotosNextPage2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllPhotosNextPage2');
    },
    get_cropper: function SpottedScript_Pages_MyPicture_View$get_cropper() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cropper');
    },
    get_panelUsrPic: function SpottedScript_Pages_MyPicture_View$get_panelUsrPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelUsrPic');
    },
    get_picImg: function SpottedScript_Pages_MyPicture_View$get_picImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicImg');
    },
    get_chatReCropButton: function SpottedScript_Pages_MyPicture_View$get_chatReCropButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatReCropButton');
    },
    get_reCropButton: function SpottedScript_Pages_MyPicture_View$get_reCropButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReCropButton');
    },
    get_cancelButton: function SpottedScript_Pages_MyPicture_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_picAnchor: function SpottedScript_Pages_MyPicture_View$get_picAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicAnchor');
    },
    get_panelImages: function SpottedScript_Pages_MyPicture_View$get_panelImages() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelImages');
    },
    get_panelCrop: function SpottedScript_Pages_MyPicture_View$get_panelCrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelCrop');
    },
    get_imagesDataList: function SpottedScript_Pages_MyPicture_View$get_imagesDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagesDataList');
    },
    get_h11: function SpottedScript_Pages_MyPicture_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_button1: function SpottedScript_Pages_MyPicture_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_panelChatPic: function SpottedScript_Pages_MyPicture_View$get_panelChatPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelChatPic');
    },
    get_h1: function SpottedScript_Pages_MyPicture_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_chatPicAnchor: function SpottedScript_Pages_MyPicture_View$get_chatPicAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatPicAnchor');
    },
    get_chatPicImg: function SpottedScript_Pages_MyPicture_View$get_chatPicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatPicImg');
    },
    get_button3: function SpottedScript_Pages_MyPicture_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyPicture_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyPicture.View.registerClass('SpottedScript.Pages.MyPicture.View', SpottedScript.DsiUserControl.View);
