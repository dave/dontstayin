Type.registerNamespace('SpottedScript.Pages.Promoters.BannerTargetting');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.BannerTargetting.Controller
SpottedScript.Pages.Promoters.BannerTargetting.Controller = function SpottedScript_Pages_Promoters_BannerTargetting_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.Promoters.BannerTargetting.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Pages.Promoters.BannerTargetting.View">
    /// </field>
    this._view = view;
    $addHandler(view.get_uiUseCustomBannerRotationRadio(), 'click', Function.createDelegate(this, this._onBannerRotationClick));
    $addHandler(view.get_uiUseDefaultBannerRotationRadio(), 'click', Function.createDelegate(this, this._onBannerRotationClick));
}
SpottedScript.Pages.Promoters.BannerTargetting.Controller.prototype = {
    _view: null,
    _onBannerRotationClick: function SpottedScript_Pages_Promoters_BannerTargetting_Controller$_onBannerRotationClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._view.get_uiCustomRotationValue().style.visibility = (this._view.get_uiUseDefaultBannerRotationRadio().checked) ? 'hidden' : '';
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.BannerTargetting.View
SpottedScript.Pages.Promoters.BannerTargetting.View = function SpottedScript_Pages_Promoters_BannerTargetting_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.BannerTargetting.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.BannerTargetting.View.prototype = {
    clientId: null,
    get_bannerListHeader2: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_bannerListHeader2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerListHeader2');
    },
    get_uiUseDefaultBannerRotationRadio: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_uiUseDefaultBannerRotationRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUseDefaultBannerRotationRadio');
    },
    get_uiUseCustomBannerRotationRadio: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_uiUseCustomBannerRotationRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUseCustomBannerRotationRadio');
    },
    get_uiCustomRotationValue: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_uiCustomRotationValue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCustomRotationValue');
    },
    get_bannerListHeader1: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_bannerListHeader1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerListHeader1');
    },
    get_pnlTargettingProperties: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_pnlTargettingProperties() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_pnlTargettingProperties');
    },
    get_cblGender: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblGender() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblGender');
    },
    get_cblAgeRange: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblAgeRange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblAgeRange');
    },
    get_cblPromoter: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblPromoter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblPromoter');
    },
    get_cblEmploymentStatus: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblEmploymentStatus() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblEmploymentStatus');
    },
    get_cblSalary: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblSalary() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblSalary');
    },
    get_cblCreditCard: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblCreditCard() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblCreditCard');
    },
    get_cblLoan: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblLoan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblLoan');
    },
    get_cblMortgage: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblMortgage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblMortgage');
    },
    get_cblDrinkWater: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkWater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkWater');
    },
    get_cblDrinkSoft: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkSoft() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkSoft');
    },
    get_cblDrinkEnergy: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkEnergy() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkEnergy');
    },
    get_cblDrinkDraftBeer: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkDraftBeer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkDraftBeer');
    },
    get_cblDrinkBottledBeer: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkBottledBeer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkBottledBeer');
    },
    get_cblDrinkSpirits: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkSpirits() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkSpirits');
    },
    get_cblDrinkWine: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkWine() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkWine');
    },
    get_cblDrinkAlcopops: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkAlcopops() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkAlcopops');
    },
    get_cblDrinkCider: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cblDrinkCider() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cblDrinkCider');
    },
    get_txtFrequencyCap: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_txtFrequencyCap() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_txtFrequencyCap');
    },
    get_txtPriority: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_txtPriority() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_txtPriority');
    },
    get_cbAlwaysShow: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_cbAlwaysShow() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_cbAlwaysShow');
    },
    get_btnSave: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_btnSave() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_btnSave');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_BannerTargetting_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.BannerTargetting.Controller.registerClass('SpottedScript.Pages.Promoters.BannerTargetting.Controller');
SpottedScript.Pages.Promoters.BannerTargetting.View.registerClass('SpottedScript.Pages.Promoters.BannerTargetting.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
