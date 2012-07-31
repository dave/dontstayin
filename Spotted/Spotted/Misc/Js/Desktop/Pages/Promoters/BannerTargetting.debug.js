//! BannerTargetting.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.Promoters.BannerTargetting');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Promoters.BannerTargetting.Controller

Js.Pages.Promoters.BannerTargetting.Controller = function Js_Pages_Promoters_BannerTargetting_Controller(view) {
    /// <param name="view" type="Js.Pages.Promoters.BannerTargetting.View">
    /// </param>
    /// <field name="_view" type="Js.Pages.Promoters.BannerTargetting.View">
    /// </field>
    this._view = view;
    view.get_uiUseCustomBannerRotationRadioJ().click(ss.Delegate.create(this, this._onBannerRotationClick));
    view.get_uiUseDefaultBannerRotationRadioJ().click(ss.Delegate.create(this, this._onBannerRotationClick));
}
Js.Pages.Promoters.BannerTargetting.Controller.prototype = {
    _view: null,
    
    _onBannerRotationClick: function Js_Pages_Promoters_BannerTargetting_Controller$_onBannerRotationClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._view.get_uiCustomRotationValue().style.visibility = (this._view.get_uiUseDefaultBannerRotationRadio().checked) ? 'hidden' : '';
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Promoters.BannerTargetting.View

Js.Pages.Promoters.BannerTargetting.View = function Js_Pages_Promoters_BannerTargetting_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_BannerListHeader2$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_BannerListHeader2J$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiUseDefaultBannerRotationRadio$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUseDefaultBannerRotationRadioJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiUseCustomBannerRotationRadio$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUseCustomBannerRotationRadioJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiCustomRotationValue$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCustomRotationValueJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_BannerListHeader1$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_BannerListHeader1J$3" type="jQueryObject">
    /// </field>
    /// <field name="_pnlTargettingProperties$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_pnlTargettingPropertiesJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblGender$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblGenderJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblAgeRange$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblAgeRangeJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblPromoter$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblPromoterJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblEmploymentStatus$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblEmploymentStatusJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblSalary$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblSalaryJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblCreditCard$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblCreditCardJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblLoan$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblLoanJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblMortgage$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblMortgageJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkWater$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkWaterJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkSoft$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkSoftJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkEnergy$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkEnergyJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkDraftBeer$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkDraftBeerJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkBottledBeer$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkBottledBeerJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkSpirits$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkSpiritsJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkWine$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkWineJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkAlcopops$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkAlcopopsJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cblDrinkCider$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cblDrinkCiderJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_txtFrequencyCap$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_txtFrequencyCapJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_txtPriority$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_txtPriorityJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_cbAlwaysShow$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_cbAlwaysShowJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_btnSave$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_btnSaveJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$3" type="jQueryObject">
    /// </field>
    Js.Pages.Promoters.BannerTargetting.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.Promoters.BannerTargetting.View.prototype = {
    clientId: null,
    
    get_bannerListHeader2: function Js_Pages_Promoters_BannerTargetting_View$get_bannerListHeader2() {
        /// <value type="Object" domElement="true"></value>
        if (this._BannerListHeader2$3 == null) {
            this._BannerListHeader2$3 = document.getElementById(this.clientId + '_BannerListHeader2');
        }
        return this._BannerListHeader2$3;
    },
    
    _BannerListHeader2$3: null,
    
    get_bannerListHeader2J: function Js_Pages_Promoters_BannerTargetting_View$get_bannerListHeader2J() {
        /// <value type="jQueryObject"></value>
        if (this._BannerListHeader2J$3 == null) {
            this._BannerListHeader2J$3 = $('#' + this.clientId + '_BannerListHeader2');
        }
        return this._BannerListHeader2J$3;
    },
    
    _BannerListHeader2J$3: null,
    
    get_uiUseDefaultBannerRotationRadio: function Js_Pages_Promoters_BannerTargetting_View$get_uiUseDefaultBannerRotationRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUseDefaultBannerRotationRadio$3 == null) {
            this._uiUseDefaultBannerRotationRadio$3 = document.getElementById(this.clientId + '_uiUseDefaultBannerRotationRadio');
        }
        return this._uiUseDefaultBannerRotationRadio$3;
    },
    
    _uiUseDefaultBannerRotationRadio$3: null,
    
    get_uiUseDefaultBannerRotationRadioJ: function Js_Pages_Promoters_BannerTargetting_View$get_uiUseDefaultBannerRotationRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUseDefaultBannerRotationRadioJ$3 == null) {
            this._uiUseDefaultBannerRotationRadioJ$3 = $('#' + this.clientId + '_uiUseDefaultBannerRotationRadio');
        }
        return this._uiUseDefaultBannerRotationRadioJ$3;
    },
    
    _uiUseDefaultBannerRotationRadioJ$3: null,
    
    get_uiUseCustomBannerRotationRadio: function Js_Pages_Promoters_BannerTargetting_View$get_uiUseCustomBannerRotationRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUseCustomBannerRotationRadio$3 == null) {
            this._uiUseCustomBannerRotationRadio$3 = document.getElementById(this.clientId + '_uiUseCustomBannerRotationRadio');
        }
        return this._uiUseCustomBannerRotationRadio$3;
    },
    
    _uiUseCustomBannerRotationRadio$3: null,
    
    get_uiUseCustomBannerRotationRadioJ: function Js_Pages_Promoters_BannerTargetting_View$get_uiUseCustomBannerRotationRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUseCustomBannerRotationRadioJ$3 == null) {
            this._uiUseCustomBannerRotationRadioJ$3 = $('#' + this.clientId + '_uiUseCustomBannerRotationRadio');
        }
        return this._uiUseCustomBannerRotationRadioJ$3;
    },
    
    _uiUseCustomBannerRotationRadioJ$3: null,
    
    get_uiCustomRotationValue: function Js_Pages_Promoters_BannerTargetting_View$get_uiCustomRotationValue() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCustomRotationValue$3 == null) {
            this._uiCustomRotationValue$3 = document.getElementById(this.clientId + '_uiCustomRotationValue');
        }
        return this._uiCustomRotationValue$3;
    },
    
    _uiCustomRotationValue$3: null,
    
    get_uiCustomRotationValueJ: function Js_Pages_Promoters_BannerTargetting_View$get_uiCustomRotationValueJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCustomRotationValueJ$3 == null) {
            this._uiCustomRotationValueJ$3 = $('#' + this.clientId + '_uiCustomRotationValue');
        }
        return this._uiCustomRotationValueJ$3;
    },
    
    _uiCustomRotationValueJ$3: null,
    
    get_bannerListHeader1: function Js_Pages_Promoters_BannerTargetting_View$get_bannerListHeader1() {
        /// <value type="Object" domElement="true"></value>
        if (this._BannerListHeader1$3 == null) {
            this._BannerListHeader1$3 = document.getElementById(this.clientId + '_BannerListHeader1');
        }
        return this._BannerListHeader1$3;
    },
    
    _BannerListHeader1$3: null,
    
    get_bannerListHeader1J: function Js_Pages_Promoters_BannerTargetting_View$get_bannerListHeader1J() {
        /// <value type="jQueryObject"></value>
        if (this._BannerListHeader1J$3 == null) {
            this._BannerListHeader1J$3 = $('#' + this.clientId + '_BannerListHeader1');
        }
        return this._BannerListHeader1J$3;
    },
    
    _BannerListHeader1J$3: null,
    
    get_pnlTargettingProperties: function Js_Pages_Promoters_BannerTargetting_View$get_pnlTargettingProperties() {
        /// <value type="Object" domElement="true"></value>
        if (this._pnlTargettingProperties$3 == null) {
            this._pnlTargettingProperties$3 = document.getElementById(this.clientId + '_pnlTargettingProperties');
        }
        return this._pnlTargettingProperties$3;
    },
    
    _pnlTargettingProperties$3: null,
    
    get_pnlTargettingPropertiesJ: function Js_Pages_Promoters_BannerTargetting_View$get_pnlTargettingPropertiesJ() {
        /// <value type="jQueryObject"></value>
        if (this._pnlTargettingPropertiesJ$3 == null) {
            this._pnlTargettingPropertiesJ$3 = $('#' + this.clientId + '_pnlTargettingProperties');
        }
        return this._pnlTargettingPropertiesJ$3;
    },
    
    _pnlTargettingPropertiesJ$3: null,
    
    get_cblGender: function Js_Pages_Promoters_BannerTargetting_View$get_cblGender() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblGender$3 == null) {
            this._cblGender$3 = document.getElementById(this.clientId + '_cblGender');
        }
        return this._cblGender$3;
    },
    
    _cblGender$3: null,
    
    get_cblGenderJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblGenderJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblGenderJ$3 == null) {
            this._cblGenderJ$3 = $('#' + this.clientId + '_cblGender');
        }
        return this._cblGenderJ$3;
    },
    
    _cblGenderJ$3: null,
    
    get_cblAgeRange: function Js_Pages_Promoters_BannerTargetting_View$get_cblAgeRange() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblAgeRange$3 == null) {
            this._cblAgeRange$3 = document.getElementById(this.clientId + '_cblAgeRange');
        }
        return this._cblAgeRange$3;
    },
    
    _cblAgeRange$3: null,
    
    get_cblAgeRangeJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblAgeRangeJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblAgeRangeJ$3 == null) {
            this._cblAgeRangeJ$3 = $('#' + this.clientId + '_cblAgeRange');
        }
        return this._cblAgeRangeJ$3;
    },
    
    _cblAgeRangeJ$3: null,
    
    get_cblPromoter: function Js_Pages_Promoters_BannerTargetting_View$get_cblPromoter() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblPromoter$3 == null) {
            this._cblPromoter$3 = document.getElementById(this.clientId + '_cblPromoter');
        }
        return this._cblPromoter$3;
    },
    
    _cblPromoter$3: null,
    
    get_cblPromoterJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblPromoterJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblPromoterJ$3 == null) {
            this._cblPromoterJ$3 = $('#' + this.clientId + '_cblPromoter');
        }
        return this._cblPromoterJ$3;
    },
    
    _cblPromoterJ$3: null,
    
    get_cblEmploymentStatus: function Js_Pages_Promoters_BannerTargetting_View$get_cblEmploymentStatus() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblEmploymentStatus$3 == null) {
            this._cblEmploymentStatus$3 = document.getElementById(this.clientId + '_cblEmploymentStatus');
        }
        return this._cblEmploymentStatus$3;
    },
    
    _cblEmploymentStatus$3: null,
    
    get_cblEmploymentStatusJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblEmploymentStatusJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblEmploymentStatusJ$3 == null) {
            this._cblEmploymentStatusJ$3 = $('#' + this.clientId + '_cblEmploymentStatus');
        }
        return this._cblEmploymentStatusJ$3;
    },
    
    _cblEmploymentStatusJ$3: null,
    
    get_cblSalary: function Js_Pages_Promoters_BannerTargetting_View$get_cblSalary() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblSalary$3 == null) {
            this._cblSalary$3 = document.getElementById(this.clientId + '_cblSalary');
        }
        return this._cblSalary$3;
    },
    
    _cblSalary$3: null,
    
    get_cblSalaryJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblSalaryJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblSalaryJ$3 == null) {
            this._cblSalaryJ$3 = $('#' + this.clientId + '_cblSalary');
        }
        return this._cblSalaryJ$3;
    },
    
    _cblSalaryJ$3: null,
    
    get_cblCreditCard: function Js_Pages_Promoters_BannerTargetting_View$get_cblCreditCard() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblCreditCard$3 == null) {
            this._cblCreditCard$3 = document.getElementById(this.clientId + '_cblCreditCard');
        }
        return this._cblCreditCard$3;
    },
    
    _cblCreditCard$3: null,
    
    get_cblCreditCardJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblCreditCardJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblCreditCardJ$3 == null) {
            this._cblCreditCardJ$3 = $('#' + this.clientId + '_cblCreditCard');
        }
        return this._cblCreditCardJ$3;
    },
    
    _cblCreditCardJ$3: null,
    
    get_cblLoan: function Js_Pages_Promoters_BannerTargetting_View$get_cblLoan() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblLoan$3 == null) {
            this._cblLoan$3 = document.getElementById(this.clientId + '_cblLoan');
        }
        return this._cblLoan$3;
    },
    
    _cblLoan$3: null,
    
    get_cblLoanJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblLoanJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblLoanJ$3 == null) {
            this._cblLoanJ$3 = $('#' + this.clientId + '_cblLoan');
        }
        return this._cblLoanJ$3;
    },
    
    _cblLoanJ$3: null,
    
    get_cblMortgage: function Js_Pages_Promoters_BannerTargetting_View$get_cblMortgage() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblMortgage$3 == null) {
            this._cblMortgage$3 = document.getElementById(this.clientId + '_cblMortgage');
        }
        return this._cblMortgage$3;
    },
    
    _cblMortgage$3: null,
    
    get_cblMortgageJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblMortgageJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblMortgageJ$3 == null) {
            this._cblMortgageJ$3 = $('#' + this.clientId + '_cblMortgage');
        }
        return this._cblMortgageJ$3;
    },
    
    _cblMortgageJ$3: null,
    
    get_cblDrinkWater: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkWater() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkWater$3 == null) {
            this._cblDrinkWater$3 = document.getElementById(this.clientId + '_cblDrinkWater');
        }
        return this._cblDrinkWater$3;
    },
    
    _cblDrinkWater$3: null,
    
    get_cblDrinkWaterJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkWaterJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkWaterJ$3 == null) {
            this._cblDrinkWaterJ$3 = $('#' + this.clientId + '_cblDrinkWater');
        }
        return this._cblDrinkWaterJ$3;
    },
    
    _cblDrinkWaterJ$3: null,
    
    get_cblDrinkSoft: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkSoft() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkSoft$3 == null) {
            this._cblDrinkSoft$3 = document.getElementById(this.clientId + '_cblDrinkSoft');
        }
        return this._cblDrinkSoft$3;
    },
    
    _cblDrinkSoft$3: null,
    
    get_cblDrinkSoftJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkSoftJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkSoftJ$3 == null) {
            this._cblDrinkSoftJ$3 = $('#' + this.clientId + '_cblDrinkSoft');
        }
        return this._cblDrinkSoftJ$3;
    },
    
    _cblDrinkSoftJ$3: null,
    
    get_cblDrinkEnergy: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkEnergy() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkEnergy$3 == null) {
            this._cblDrinkEnergy$3 = document.getElementById(this.clientId + '_cblDrinkEnergy');
        }
        return this._cblDrinkEnergy$3;
    },
    
    _cblDrinkEnergy$3: null,
    
    get_cblDrinkEnergyJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkEnergyJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkEnergyJ$3 == null) {
            this._cblDrinkEnergyJ$3 = $('#' + this.clientId + '_cblDrinkEnergy');
        }
        return this._cblDrinkEnergyJ$3;
    },
    
    _cblDrinkEnergyJ$3: null,
    
    get_cblDrinkDraftBeer: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkDraftBeer() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkDraftBeer$3 == null) {
            this._cblDrinkDraftBeer$3 = document.getElementById(this.clientId + '_cblDrinkDraftBeer');
        }
        return this._cblDrinkDraftBeer$3;
    },
    
    _cblDrinkDraftBeer$3: null,
    
    get_cblDrinkDraftBeerJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkDraftBeerJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkDraftBeerJ$3 == null) {
            this._cblDrinkDraftBeerJ$3 = $('#' + this.clientId + '_cblDrinkDraftBeer');
        }
        return this._cblDrinkDraftBeerJ$3;
    },
    
    _cblDrinkDraftBeerJ$3: null,
    
    get_cblDrinkBottledBeer: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkBottledBeer() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkBottledBeer$3 == null) {
            this._cblDrinkBottledBeer$3 = document.getElementById(this.clientId + '_cblDrinkBottledBeer');
        }
        return this._cblDrinkBottledBeer$3;
    },
    
    _cblDrinkBottledBeer$3: null,
    
    get_cblDrinkBottledBeerJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkBottledBeerJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkBottledBeerJ$3 == null) {
            this._cblDrinkBottledBeerJ$3 = $('#' + this.clientId + '_cblDrinkBottledBeer');
        }
        return this._cblDrinkBottledBeerJ$3;
    },
    
    _cblDrinkBottledBeerJ$3: null,
    
    get_cblDrinkSpirits: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkSpirits() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkSpirits$3 == null) {
            this._cblDrinkSpirits$3 = document.getElementById(this.clientId + '_cblDrinkSpirits');
        }
        return this._cblDrinkSpirits$3;
    },
    
    _cblDrinkSpirits$3: null,
    
    get_cblDrinkSpiritsJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkSpiritsJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkSpiritsJ$3 == null) {
            this._cblDrinkSpiritsJ$3 = $('#' + this.clientId + '_cblDrinkSpirits');
        }
        return this._cblDrinkSpiritsJ$3;
    },
    
    _cblDrinkSpiritsJ$3: null,
    
    get_cblDrinkWine: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkWine() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkWine$3 == null) {
            this._cblDrinkWine$3 = document.getElementById(this.clientId + '_cblDrinkWine');
        }
        return this._cblDrinkWine$3;
    },
    
    _cblDrinkWine$3: null,
    
    get_cblDrinkWineJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkWineJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkWineJ$3 == null) {
            this._cblDrinkWineJ$3 = $('#' + this.clientId + '_cblDrinkWine');
        }
        return this._cblDrinkWineJ$3;
    },
    
    _cblDrinkWineJ$3: null,
    
    get_cblDrinkAlcopops: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkAlcopops() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkAlcopops$3 == null) {
            this._cblDrinkAlcopops$3 = document.getElementById(this.clientId + '_cblDrinkAlcopops');
        }
        return this._cblDrinkAlcopops$3;
    },
    
    _cblDrinkAlcopops$3: null,
    
    get_cblDrinkAlcopopsJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkAlcopopsJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkAlcopopsJ$3 == null) {
            this._cblDrinkAlcopopsJ$3 = $('#' + this.clientId + '_cblDrinkAlcopops');
        }
        return this._cblDrinkAlcopopsJ$3;
    },
    
    _cblDrinkAlcopopsJ$3: null,
    
    get_cblDrinkCider: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkCider() {
        /// <value type="Object" domElement="true"></value>
        if (this._cblDrinkCider$3 == null) {
            this._cblDrinkCider$3 = document.getElementById(this.clientId + '_cblDrinkCider');
        }
        return this._cblDrinkCider$3;
    },
    
    _cblDrinkCider$3: null,
    
    get_cblDrinkCiderJ: function Js_Pages_Promoters_BannerTargetting_View$get_cblDrinkCiderJ() {
        /// <value type="jQueryObject"></value>
        if (this._cblDrinkCiderJ$3 == null) {
            this._cblDrinkCiderJ$3 = $('#' + this.clientId + '_cblDrinkCider');
        }
        return this._cblDrinkCiderJ$3;
    },
    
    _cblDrinkCiderJ$3: null,
    
    get_txtFrequencyCap: function Js_Pages_Promoters_BannerTargetting_View$get_txtFrequencyCap() {
        /// <value type="Object" domElement="true"></value>
        if (this._txtFrequencyCap$3 == null) {
            this._txtFrequencyCap$3 = document.getElementById(this.clientId + '_txtFrequencyCap');
        }
        return this._txtFrequencyCap$3;
    },
    
    _txtFrequencyCap$3: null,
    
    get_txtFrequencyCapJ: function Js_Pages_Promoters_BannerTargetting_View$get_txtFrequencyCapJ() {
        /// <value type="jQueryObject"></value>
        if (this._txtFrequencyCapJ$3 == null) {
            this._txtFrequencyCapJ$3 = $('#' + this.clientId + '_txtFrequencyCap');
        }
        return this._txtFrequencyCapJ$3;
    },
    
    _txtFrequencyCapJ$3: null,
    
    get_txtPriority: function Js_Pages_Promoters_BannerTargetting_View$get_txtPriority() {
        /// <value type="Object" domElement="true"></value>
        if (this._txtPriority$3 == null) {
            this._txtPriority$3 = document.getElementById(this.clientId + '_txtPriority');
        }
        return this._txtPriority$3;
    },
    
    _txtPriority$3: null,
    
    get_txtPriorityJ: function Js_Pages_Promoters_BannerTargetting_View$get_txtPriorityJ() {
        /// <value type="jQueryObject"></value>
        if (this._txtPriorityJ$3 == null) {
            this._txtPriorityJ$3 = $('#' + this.clientId + '_txtPriority');
        }
        return this._txtPriorityJ$3;
    },
    
    _txtPriorityJ$3: null,
    
    get_cbAlwaysShow: function Js_Pages_Promoters_BannerTargetting_View$get_cbAlwaysShow() {
        /// <value type="Object" domElement="true"></value>
        if (this._cbAlwaysShow$3 == null) {
            this._cbAlwaysShow$3 = document.getElementById(this.clientId + '_cbAlwaysShow');
        }
        return this._cbAlwaysShow$3;
    },
    
    _cbAlwaysShow$3: null,
    
    get_cbAlwaysShowJ: function Js_Pages_Promoters_BannerTargetting_View$get_cbAlwaysShowJ() {
        /// <value type="jQueryObject"></value>
        if (this._cbAlwaysShowJ$3 == null) {
            this._cbAlwaysShowJ$3 = $('#' + this.clientId + '_cbAlwaysShow');
        }
        return this._cbAlwaysShowJ$3;
    },
    
    _cbAlwaysShowJ$3: null,
    
    get_btnSave: function Js_Pages_Promoters_BannerTargetting_View$get_btnSave() {
        /// <value type="Object" domElement="true"></value>
        if (this._btnSave$3 == null) {
            this._btnSave$3 = document.getElementById(this.clientId + '_btnSave');
        }
        return this._btnSave$3;
    },
    
    _btnSave$3: null,
    
    get_btnSaveJ: function Js_Pages_Promoters_BannerTargetting_View$get_btnSaveJ() {
        /// <value type="jQueryObject"></value>
        if (this._btnSaveJ$3 == null) {
            this._btnSaveJ$3 = $('#' + this.clientId + '_btnSave');
        }
        return this._btnSaveJ$3;
    },
    
    _btnSaveJ$3: null,
    
    get_genericContainerPage: function Js_Pages_Promoters_BannerTargetting_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$3 == null) {
            this._GenericContainerPage$3 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$3;
    },
    
    _GenericContainerPage$3: null,
    
    get_genericContainerPageJ: function Js_Pages_Promoters_BannerTargetting_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$3 == null) {
            this._GenericContainerPageJ$3 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$3;
    },
    
    _GenericContainerPageJ$3: null
}


Js.Pages.Promoters.BannerTargetting.Controller.registerClass('Js.Pages.Promoters.BannerTargetting.Controller');
Js.Pages.Promoters.BannerTargetting.View.registerClass('Js.Pages.Promoters.BannerTargetting.View', Js.Pages.Promoters.PromoterUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
