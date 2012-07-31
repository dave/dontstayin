Type.registerNamespace('SpottedScript.Pages.Promoters.BannersPending');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.BannersPending.View
SpottedScript.Pages.Promoters.BannersPending.View = function SpottedScript_Pages_Promoters_BannersPending_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.BannersPending.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.BannersPending.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_BannersPending_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_h1Title: function SpottedScript_Pages_Promoters_BannersPending_View$get_h1Title() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Title');
    },
    get_noPendingBannersLabel: function SpottedScript_Pages_Promoters_BannersPending_View$get_noPendingBannersLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPendingBannersLabel');
    },
    get_bookBannersPanel: function SpottedScript_Pages_Promoters_BannersPending_View$get_bookBannersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookBannersPanel');
    },
    get_bannerGrid: function SpottedScript_Pages_Promoters_BannersPending_View$get_bannerGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerGrid');
    },
    get_bookBannersButton: function SpottedScript_Pages_Promoters_BannersPending_View$get_bookBannersButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookBannersButton');
    },
    get_ensureBannersSelectedValidator: function SpottedScript_Pages_Promoters_BannersPending_View$get_ensureBannersSelectedValidator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnsureBannersSelectedValidator');
    },
    get_paymentPanel: function SpottedScript_Pages_Promoters_BannersPending_View$get_paymentPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaymentPanel');
    },
    get_payment: function SpottedScript_Pages_Promoters_BannersPending_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_cancelButton: function SpottedScript_Pages_Promoters_BannersPending_View$get_cancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CancelButton');
    },
    get_confirmedPanel: function SpottedScript_Pages_Promoters_BannersPending_View$get_confirmedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ConfirmedPanel');
    },
    get_bookedBannersGridView: function SpottedScript_Pages_Promoters_BannersPending_View$get_bookedBannersGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BookedBannersGridView');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_BannersPending_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.BannersPending.View.registerClass('SpottedScript.Pages.Promoters.BannersPending.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
