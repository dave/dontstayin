Type.registerNamespace('SpottedScript.Pages.Galleries.Paged');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Galleries.Paged.View
SpottedScript.Pages.Galleries.Paged.View = function SpottedScript_Pages_Galleries_Paged_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Galleries.Paged.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Galleries.Paged.View.prototype = {
    clientId: null,
    get_header: function SpottedScript_Pages_Galleries_Paged_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_galleryPicImg: function SpottedScript_Pages_Galleries_Paged_View$get_galleryPicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleryPicImg');
    },
    get_eventLink: function SpottedScript_Pages_Galleries_Paged_View$get_eventLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLink');
    },
    get_eventVenueLink: function SpottedScript_Pages_Galleries_Paged_View$get_eventVenueLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventVenueLink');
    },
    get_eventPlaceLink: function SpottedScript_Pages_Galleries_Paged_View$get_eventPlaceLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventPlaceLink');
    },
    get_discussionLink: function SpottedScript_Pages_Galleries_Paged_View$get_discussionLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLink');
    },
    get_ownerLink: function SpottedScript_Pages_Galleries_Paged_View$get_ownerLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnerLink');
    },
    get_linkBack: function SpottedScript_Pages_Galleries_Paged_View$get_linkBack() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkBack');
    },
    get_quickBrowserLink: function SpottedScript_Pages_Galleries_Paged_View$get_quickBrowserLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuickBrowserLink');
    },
    get_eventDate: function SpottedScript_Pages_Galleries_Paged_View$get_eventDate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventDate');
    },
    get_picCell: function SpottedScript_Pages_Galleries_Paged_View$get_picCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicCell');
    },
    get_eventLinkP: function SpottedScript_Pages_Galleries_Paged_View$get_eventLinkP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLinkP');
    },
    get_articleLinkP: function SpottedScript_Pages_Galleries_Paged_View$get_articleLinkP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleLinkP');
    },
    get_articleLink: function SpottedScript_Pages_Galleries_Paged_View$get_articleLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleLink');
    },
    get_discussionLinkCommentsLabel: function SpottedScript_Pages_Galleries_Paged_View$get_discussionLinkCommentsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLinkCommentsLabel');
    },
    get_discussionLinkTargetLabel: function SpottedScript_Pages_Galleries_Paged_View$get_discussionLinkTargetLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLinkTargetLabel');
    },
    get_noPhotosPanel: function SpottedScript_Pages_Galleries_Paged_View$get_noPhotosPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPhotosPanel');
    },
    get_photosPanel: function SpottedScript_Pages_Galleries_Paged_View$get_photosPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosPanel');
    },
    get_photosDataList: function SpottedScript_Pages_Galleries_Paged_View$get_photosDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosDataList');
    },
    get_photoPageLinksP: function SpottedScript_Pages_Galleries_Paged_View$get_photoPageLinksP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoPageLinksP');
    },
    get_photoPageLinksP1: function SpottedScript_Pages_Galleries_Paged_View$get_photoPageLinksP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoPageLinksP1');
    },
    get_miscInfoPanel: function SpottedScript_Pages_Galleries_Paged_View$get_miscInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MiscInfoPanel');
    },
    get_h11: function SpottedScript_Pages_Galleries_Paged_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Pages_Galleries_Paged_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Galleries.Paged.View.registerClass('SpottedScript.Pages.Galleries.Paged.View', SpottedScript.DsiUserControl.View);
