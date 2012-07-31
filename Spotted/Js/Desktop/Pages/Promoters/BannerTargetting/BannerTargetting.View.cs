//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.BannerTargetting
{
	public partial class View
		 : Js.Pages.Promoters.PromoterUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element BannerListHeader2 {get {if (_BannerListHeader2 == null) {_BannerListHeader2 = (Element)Document.GetElementById(clientId + "_BannerListHeader2");}; return _BannerListHeader2;}} private Element _BannerListHeader2;
		public jQueryObject BannerListHeader2J {get {if (_BannerListHeader2J == null) {_BannerListHeader2J = jQuery.Select("#" + clientId + "_BannerListHeader2");}; return _BannerListHeader2J;}} private jQueryObject _BannerListHeader2J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement uiUseDefaultBannerRotationRadio {get {if (_uiUseDefaultBannerRotationRadio == null) {_uiUseDefaultBannerRotationRadio = (CheckBoxElement)Document.GetElementById(clientId + "_uiUseDefaultBannerRotationRadio");}; return _uiUseDefaultBannerRotationRadio;}} private CheckBoxElement _uiUseDefaultBannerRotationRadio;
		public jQueryObject uiUseDefaultBannerRotationRadioJ {get {if (_uiUseDefaultBannerRotationRadioJ == null) {_uiUseDefaultBannerRotationRadioJ = jQuery.Select("#" + clientId + "_uiUseDefaultBannerRotationRadio");}; return _uiUseDefaultBannerRotationRadioJ;}} private jQueryObject _uiUseDefaultBannerRotationRadioJ;
		public CheckBoxElement uiUseCustomBannerRotationRadio {get {if (_uiUseCustomBannerRotationRadio == null) {_uiUseCustomBannerRotationRadio = (CheckBoxElement)Document.GetElementById(clientId + "_uiUseCustomBannerRotationRadio");}; return _uiUseCustomBannerRotationRadio;}} private CheckBoxElement _uiUseCustomBannerRotationRadio;
		public jQueryObject uiUseCustomBannerRotationRadioJ {get {if (_uiUseCustomBannerRotationRadioJ == null) {_uiUseCustomBannerRotationRadioJ = jQuery.Select("#" + clientId + "_uiUseCustomBannerRotationRadio");}; return _uiUseCustomBannerRotationRadioJ;}} private jQueryObject _uiUseCustomBannerRotationRadioJ;
		public InputElement uiCustomRotationValue {get {if (_uiCustomRotationValue == null) {_uiCustomRotationValue = (InputElement)Document.GetElementById(clientId + "_uiCustomRotationValue");}; return _uiCustomRotationValue;}} private InputElement _uiCustomRotationValue;
		public jQueryObject uiCustomRotationValueJ {get {if (_uiCustomRotationValueJ == null) {_uiCustomRotationValueJ = jQuery.Select("#" + clientId + "_uiCustomRotationValue");}; return _uiCustomRotationValueJ;}} private jQueryObject _uiCustomRotationValueJ;
		public Element BannerListHeader1 {get {if (_BannerListHeader1 == null) {_BannerListHeader1 = (Element)Document.GetElementById(clientId + "_BannerListHeader1");}; return _BannerListHeader1;}} private Element _BannerListHeader1;
		public jQueryObject BannerListHeader1J {get {if (_BannerListHeader1J == null) {_BannerListHeader1J = jQuery.Select("#" + clientId + "_BannerListHeader1");}; return _BannerListHeader1J;}} private jQueryObject _BannerListHeader1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement pnlTargettingProperties {get {if (_pnlTargettingProperties == null) {_pnlTargettingProperties = (DivElement)Document.GetElementById(clientId + "_pnlTargettingProperties");}; return _pnlTargettingProperties;}} private DivElement _pnlTargettingProperties;
		public jQueryObject pnlTargettingPropertiesJ {get {if (_pnlTargettingPropertiesJ == null) {_pnlTargettingPropertiesJ = jQuery.Select("#" + clientId + "_pnlTargettingProperties");}; return _pnlTargettingPropertiesJ;}} private jQueryObject _pnlTargettingPropertiesJ;
		public Element cblGender {get {if (_cblGender == null) {_cblGender = (Element)Document.GetElementById(clientId + "_cblGender");}; return _cblGender;}} private Element _cblGender;
		public jQueryObject cblGenderJ {get {if (_cblGenderJ == null) {_cblGenderJ = jQuery.Select("#" + clientId + "_cblGender");}; return _cblGenderJ;}} private jQueryObject _cblGenderJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblAgeRange {get {if (_cblAgeRange == null) {_cblAgeRange = (Element)Document.GetElementById(clientId + "_cblAgeRange");}; return _cblAgeRange;}} private Element _cblAgeRange;
		public jQueryObject cblAgeRangeJ {get {if (_cblAgeRangeJ == null) {_cblAgeRangeJ = jQuery.Select("#" + clientId + "_cblAgeRange");}; return _cblAgeRangeJ;}} private jQueryObject _cblAgeRangeJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblPromoter {get {if (_cblPromoter == null) {_cblPromoter = (Element)Document.GetElementById(clientId + "_cblPromoter");}; return _cblPromoter;}} private Element _cblPromoter;
		public jQueryObject cblPromoterJ {get {if (_cblPromoterJ == null) {_cblPromoterJ = jQuery.Select("#" + clientId + "_cblPromoter");}; return _cblPromoterJ;}} private jQueryObject _cblPromoterJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblEmploymentStatus {get {if (_cblEmploymentStatus == null) {_cblEmploymentStatus = (Element)Document.GetElementById(clientId + "_cblEmploymentStatus");}; return _cblEmploymentStatus;}} private Element _cblEmploymentStatus;
		public jQueryObject cblEmploymentStatusJ {get {if (_cblEmploymentStatusJ == null) {_cblEmploymentStatusJ = jQuery.Select("#" + clientId + "_cblEmploymentStatus");}; return _cblEmploymentStatusJ;}} private jQueryObject _cblEmploymentStatusJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblSalary {get {if (_cblSalary == null) {_cblSalary = (Element)Document.GetElementById(clientId + "_cblSalary");}; return _cblSalary;}} private Element _cblSalary;
		public jQueryObject cblSalaryJ {get {if (_cblSalaryJ == null) {_cblSalaryJ = jQuery.Select("#" + clientId + "_cblSalary");}; return _cblSalaryJ;}} private jQueryObject _cblSalaryJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblCreditCard {get {if (_cblCreditCard == null) {_cblCreditCard = (Element)Document.GetElementById(clientId + "_cblCreditCard");}; return _cblCreditCard;}} private Element _cblCreditCard;
		public jQueryObject cblCreditCardJ {get {if (_cblCreditCardJ == null) {_cblCreditCardJ = jQuery.Select("#" + clientId + "_cblCreditCard");}; return _cblCreditCardJ;}} private jQueryObject _cblCreditCardJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblLoan {get {if (_cblLoan == null) {_cblLoan = (Element)Document.GetElementById(clientId + "_cblLoan");}; return _cblLoan;}} private Element _cblLoan;
		public jQueryObject cblLoanJ {get {if (_cblLoanJ == null) {_cblLoanJ = jQuery.Select("#" + clientId + "_cblLoan");}; return _cblLoanJ;}} private jQueryObject _cblLoanJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblMortgage {get {if (_cblMortgage == null) {_cblMortgage = (Element)Document.GetElementById(clientId + "_cblMortgage");}; return _cblMortgage;}} private Element _cblMortgage;
		public jQueryObject cblMortgageJ {get {if (_cblMortgageJ == null) {_cblMortgageJ = jQuery.Select("#" + clientId + "_cblMortgage");}; return _cblMortgageJ;}} private jQueryObject _cblMortgageJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkWater {get {if (_cblDrinkWater == null) {_cblDrinkWater = (Element)Document.GetElementById(clientId + "_cblDrinkWater");}; return _cblDrinkWater;}} private Element _cblDrinkWater;
		public jQueryObject cblDrinkWaterJ {get {if (_cblDrinkWaterJ == null) {_cblDrinkWaterJ = jQuery.Select("#" + clientId + "_cblDrinkWater");}; return _cblDrinkWaterJ;}} private jQueryObject _cblDrinkWaterJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkSoft {get {if (_cblDrinkSoft == null) {_cblDrinkSoft = (Element)Document.GetElementById(clientId + "_cblDrinkSoft");}; return _cblDrinkSoft;}} private Element _cblDrinkSoft;
		public jQueryObject cblDrinkSoftJ {get {if (_cblDrinkSoftJ == null) {_cblDrinkSoftJ = jQuery.Select("#" + clientId + "_cblDrinkSoft");}; return _cblDrinkSoftJ;}} private jQueryObject _cblDrinkSoftJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkEnergy {get {if (_cblDrinkEnergy == null) {_cblDrinkEnergy = (Element)Document.GetElementById(clientId + "_cblDrinkEnergy");}; return _cblDrinkEnergy;}} private Element _cblDrinkEnergy;
		public jQueryObject cblDrinkEnergyJ {get {if (_cblDrinkEnergyJ == null) {_cblDrinkEnergyJ = jQuery.Select("#" + clientId + "_cblDrinkEnergy");}; return _cblDrinkEnergyJ;}} private jQueryObject _cblDrinkEnergyJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkDraftBeer {get {if (_cblDrinkDraftBeer == null) {_cblDrinkDraftBeer = (Element)Document.GetElementById(clientId + "_cblDrinkDraftBeer");}; return _cblDrinkDraftBeer;}} private Element _cblDrinkDraftBeer;
		public jQueryObject cblDrinkDraftBeerJ {get {if (_cblDrinkDraftBeerJ == null) {_cblDrinkDraftBeerJ = jQuery.Select("#" + clientId + "_cblDrinkDraftBeer");}; return _cblDrinkDraftBeerJ;}} private jQueryObject _cblDrinkDraftBeerJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkBottledBeer {get {if (_cblDrinkBottledBeer == null) {_cblDrinkBottledBeer = (Element)Document.GetElementById(clientId + "_cblDrinkBottledBeer");}; return _cblDrinkBottledBeer;}} private Element _cblDrinkBottledBeer;
		public jQueryObject cblDrinkBottledBeerJ {get {if (_cblDrinkBottledBeerJ == null) {_cblDrinkBottledBeerJ = jQuery.Select("#" + clientId + "_cblDrinkBottledBeer");}; return _cblDrinkBottledBeerJ;}} private jQueryObject _cblDrinkBottledBeerJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkSpirits {get {if (_cblDrinkSpirits == null) {_cblDrinkSpirits = (Element)Document.GetElementById(clientId + "_cblDrinkSpirits");}; return _cblDrinkSpirits;}} private Element _cblDrinkSpirits;
		public jQueryObject cblDrinkSpiritsJ {get {if (_cblDrinkSpiritsJ == null) {_cblDrinkSpiritsJ = jQuery.Select("#" + clientId + "_cblDrinkSpirits");}; return _cblDrinkSpiritsJ;}} private jQueryObject _cblDrinkSpiritsJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkWine {get {if (_cblDrinkWine == null) {_cblDrinkWine = (Element)Document.GetElementById(clientId + "_cblDrinkWine");}; return _cblDrinkWine;}} private Element _cblDrinkWine;
		public jQueryObject cblDrinkWineJ {get {if (_cblDrinkWineJ == null) {_cblDrinkWineJ = jQuery.Select("#" + clientId + "_cblDrinkWine");}; return _cblDrinkWineJ;}} private jQueryObject _cblDrinkWineJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkAlcopops {get {if (_cblDrinkAlcopops == null) {_cblDrinkAlcopops = (Element)Document.GetElementById(clientId + "_cblDrinkAlcopops");}; return _cblDrinkAlcopops;}} private Element _cblDrinkAlcopops;
		public jQueryObject cblDrinkAlcopopsJ {get {if (_cblDrinkAlcopopsJ == null) {_cblDrinkAlcopopsJ = jQuery.Select("#" + clientId + "_cblDrinkAlcopops");}; return _cblDrinkAlcopopsJ;}} private jQueryObject _cblDrinkAlcopopsJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public Element cblDrinkCider {get {if (_cblDrinkCider == null) {_cblDrinkCider = (Element)Document.GetElementById(clientId + "_cblDrinkCider");}; return _cblDrinkCider;}} private Element _cblDrinkCider;
		public jQueryObject cblDrinkCiderJ {get {if (_cblDrinkCiderJ == null) {_cblDrinkCiderJ = jQuery.Select("#" + clientId + "_cblDrinkCider");}; return _cblDrinkCiderJ;}} private jQueryObject _cblDrinkCiderJ;//mappings.Add("System.Web.UI.WebControls.CheckBoxList", ElementGetter("Element"));
		public InputElement txtFrequencyCap {get {if (_txtFrequencyCap == null) {_txtFrequencyCap = (InputElement)Document.GetElementById(clientId + "_txtFrequencyCap");}; return _txtFrequencyCap;}} private InputElement _txtFrequencyCap;
		public jQueryObject txtFrequencyCapJ {get {if (_txtFrequencyCapJ == null) {_txtFrequencyCapJ = jQuery.Select("#" + clientId + "_txtFrequencyCap");}; return _txtFrequencyCapJ;}} private jQueryObject _txtFrequencyCapJ;
		public InputElement txtPriority {get {if (_txtPriority == null) {_txtPriority = (InputElement)Document.GetElementById(clientId + "_txtPriority");}; return _txtPriority;}} private InputElement _txtPriority;
		public jQueryObject txtPriorityJ {get {if (_txtPriorityJ == null) {_txtPriorityJ = jQuery.Select("#" + clientId + "_txtPriority");}; return _txtPriorityJ;}} private jQueryObject _txtPriorityJ;
		public CheckBoxElement cbAlwaysShow {get {if (_cbAlwaysShow == null) {_cbAlwaysShow = (CheckBoxElement)Document.GetElementById(clientId + "_cbAlwaysShow");}; return _cbAlwaysShow;}} private CheckBoxElement _cbAlwaysShow;
		public jQueryObject cbAlwaysShowJ {get {if (_cbAlwaysShowJ == null) {_cbAlwaysShowJ = jQuery.Select("#" + clientId + "_cbAlwaysShow");}; return _cbAlwaysShowJ;}} private jQueryObject _cbAlwaysShowJ;
		public Element btnSave {get {if (_btnSave == null) {_btnSave = (Element)Document.GetElementById(clientId + "_btnSave");}; return _btnSave;}} private Element _btnSave;
		public jQueryObject btnSaveJ {get {if (_btnSaveJ == null) {_btnSaveJ = jQuery.Select("#" + clientId + "_btnSave");}; return _btnSaveJ;}} private jQueryObject _btnSaveJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
