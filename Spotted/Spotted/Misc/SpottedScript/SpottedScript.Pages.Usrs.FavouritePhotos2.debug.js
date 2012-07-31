Type.registerNamespace('SpottedScript.Pages.Usrs.FavouritePhotos2');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.FavouritePhotos2.View
SpottedScript.Pages.Usrs.FavouritePhotos2.View = function SpottedScript_Pages_Usrs_FavouritePhotos2_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.FavouritePhotos2.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.FavouritePhotos2.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h13: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_usrIntro: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_usrIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrIntro');
    },
    get_cal: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_cal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cal');
    },
    get_panelPhotos: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_panelPhotos() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPhotos');
    },
    get_photosPanel: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosPanel');
    },
    get_noPhotosPanel: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_noPhotosPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPhotosPanel');
    },
    get_photosPageP: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosPageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosPageP');
    },
    get_photosPageP1: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosPageP1');
    },
    get_photosNextPageLink: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosNextPageLink');
    },
    get_photosNextPageLink1: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosNextPageLink1');
    },
    get_photosPrevPageLink: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosPrevPageLink');
    },
    get_photosPrevPageLink1: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosPrevPageLink1');
    },
    get_photosDataList: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_photosDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosDataList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_FavouritePhotos2_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.FavouritePhotos2.View.registerClass('SpottedScript.Pages.Usrs.FavouritePhotos2.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
