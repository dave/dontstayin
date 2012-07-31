Type.registerNamespace('SpottedScript.Blank.Questionairre');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Questionairre.View
SpottedScript.Blank.Questionairre.View = function SpottedScript_Blank_Questionairre_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Questionairre.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Questionairre.View.prototype = {
    clientId: null,
    get_doneQuestionairrePanel: function SpottedScript_Blank_Questionairre_View$get_doneQuestionairrePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneQuestionairrePanel');
    },
    get_questionairrePanel: function SpottedScript_Blank_Questionairre_View$get_questionairrePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuestionairrePanel');
    },
    get_questionairreDiv: function SpottedScript_Blank_Questionairre_View$get_questionairreDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuestionairreDiv');
    },
    get_drinkWater: function SpottedScript_Blank_Questionairre_View$get_drinkWater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkWater');
    },
    get_drinkSoft: function SpottedScript_Blank_Questionairre_View$get_drinkSoft() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkSoft');
    },
    get_drinkEnergy: function SpottedScript_Blank_Questionairre_View$get_drinkEnergy() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkEnergy');
    },
    get_drinkDraftBeer: function SpottedScript_Blank_Questionairre_View$get_drinkDraftBeer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkDraftBeer');
    },
    get_drinkBottledBeer: function SpottedScript_Blank_Questionairre_View$get_drinkBottledBeer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkBottledBeer');
    },
    get_drinkSpirits: function SpottedScript_Blank_Questionairre_View$get_drinkSpirits() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkSpirits');
    },
    get_drinkWine: function SpottedScript_Blank_Questionairre_View$get_drinkWine() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkWine');
    },
    get_drinkAlcopops: function SpottedScript_Blank_Questionairre_View$get_drinkAlcopops() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkAlcopops');
    },
    get_drinkCider: function SpottedScript_Blank_Questionairre_View$get_drinkCider() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DrinkCider');
    },
    get_smoke1: function SpottedScript_Blank_Questionairre_View$get_smoke1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Smoke1');
    },
    get_smoke2: function SpottedScript_Blank_Questionairre_View$get_smoke2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Smoke2');
    },
    get_smoke3: function SpottedScript_Blank_Questionairre_View$get_smoke3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Smoke3');
    },
    get_eveningAllNight: function SpottedScript_Blank_Questionairre_View$get_eveningAllNight() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EveningAllNight');
    },
    get_eveningLateNight: function SpottedScript_Blank_Questionairre_View$get_eveningLateNight() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EveningLateNight');
    },
    get_eveningCoupleDrinks: function SpottedScript_Blank_Questionairre_View$get_eveningCoupleDrinks() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EveningCoupleDrinks');
    },
    get_eveningOther: function SpottedScript_Blank_Questionairre_View$get_eveningOther() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EveningOther');
    },
    get_eveningStayIn: function SpottedScript_Blank_Questionairre_View$get_eveningStayIn() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EveningStayIn');
    },
    get_employment1: function SpottedScript_Blank_Questionairre_View$get_employment1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Employment1');
    },
    get_employment2: function SpottedScript_Blank_Questionairre_View$get_employment2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Employment2');
    },
    get_employment3: function SpottedScript_Blank_Questionairre_View$get_employment3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Employment3');
    },
    get_employment4: function SpottedScript_Blank_Questionairre_View$get_employment4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Employment4');
    },
    get_salary1: function SpottedScript_Blank_Questionairre_View$get_salary1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Salary1');
    },
    get_salary2: function SpottedScript_Blank_Questionairre_View$get_salary2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Salary2');
    },
    get_salary3: function SpottedScript_Blank_Questionairre_View$get_salary3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Salary3');
    },
    get_salary4: function SpottedScript_Blank_Questionairre_View$get_salary4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Salary4');
    },
    get_salary5: function SpottedScript_Blank_Questionairre_View$get_salary5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Salary5');
    },
    get_salary6: function SpottedScript_Blank_Questionairre_View$get_salary6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Salary6');
    },
    get_salary7: function SpottedScript_Blank_Questionairre_View$get_salary7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Salary7');
    },
    get_creditCardYes: function SpottedScript_Blank_Questionairre_View$get_creditCardYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditCardYes');
    },
    get_creditCardNo: function SpottedScript_Blank_Questionairre_View$get_creditCardNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CreditCardNo');
    },
    get_loanYes: function SpottedScript_Blank_Questionairre_View$get_loanYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoanYes');
    },
    get_loanNo: function SpottedScript_Blank_Questionairre_View$get_loanNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoanNo');
    },
    get_mortgageYes: function SpottedScript_Blank_Questionairre_View$get_mortgageYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MortgageYes');
    },
    get_mortgageNo: function SpottedScript_Blank_Questionairre_View$get_mortgageNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MortgageNo');
    },
    get_ownCar: function SpottedScript_Blank_Questionairre_View$get_ownCar() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnCar');
    },
    get_ownBike: function SpottedScript_Blank_Questionairre_View$get_ownBike() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnBike');
    },
    get_ownMp3: function SpottedScript_Blank_Questionairre_View$get_ownMp3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnMp3');
    },
    get_ownPc: function SpottedScript_Blank_Questionairre_View$get_ownPc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnPc');
    },
    get_ownLaptop: function SpottedScript_Blank_Questionairre_View$get_ownLaptop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnLaptop');
    },
    get_ownMac: function SpottedScript_Blank_Questionairre_View$get_ownMac() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnMac');
    },
    get_ownBroadband: function SpottedScript_Blank_Questionairre_View$get_ownBroadband() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnBroadband');
    },
    get_ownConsole: function SpottedScript_Blank_Questionairre_View$get_ownConsole() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnConsole');
    },
    get_ownCamera: function SpottedScript_Blank_Questionairre_View$get_ownCamera() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnCamera');
    },
    get_ownDvd: function SpottedScript_Blank_Questionairre_View$get_ownDvd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnDvd');
    },
    get_ownDvdRec: function SpottedScript_Blank_Questionairre_View$get_ownDvdRec() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnDvdRec');
    },
    get_buyCar: function SpottedScript_Blank_Questionairre_View$get_buyCar() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyCar');
    },
    get_buyBike: function SpottedScript_Blank_Questionairre_View$get_buyBike() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyBike');
    },
    get_buyMp3: function SpottedScript_Blank_Questionairre_View$get_buyMp3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyMp3');
    },
    get_buyPc: function SpottedScript_Blank_Questionairre_View$get_buyPc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyPc');
    },
    get_buyLaptop: function SpottedScript_Blank_Questionairre_View$get_buyLaptop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyLaptop');
    },
    get_buyMac: function SpottedScript_Blank_Questionairre_View$get_buyMac() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyMac');
    },
    get_buyBroadband: function SpottedScript_Blank_Questionairre_View$get_buyBroadband() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyBroadband');
    },
    get_buyConsole: function SpottedScript_Blank_Questionairre_View$get_buyConsole() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyConsole');
    },
    get_buyCamera: function SpottedScript_Blank_Questionairre_View$get_buyCamera() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyCamera');
    },
    get_buyDvd: function SpottedScript_Blank_Questionairre_View$get_buyDvd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyDvd');
    },
    get_buyDvdRec: function SpottedScript_Blank_Questionairre_View$get_buyDvdRec() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyDvdRec');
    },
    get_spendDesignerClothes: function SpottedScript_Blank_Questionairre_View$get_spendDesignerClothes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendDesignerClothes');
    },
    get_spendHighStreetClothes: function SpottedScript_Blank_Questionairre_View$get_spendHighStreetClothes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendHighStreetClothes');
    },
    get_spendMusicCd: function SpottedScript_Blank_Questionairre_View$get_spendMusicCd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendMusicCd');
    },
    get_spendMusicVinyl: function SpottedScript_Blank_Questionairre_View$get_spendMusicVinyl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendMusicVinyl');
    },
    get_spendMusicDownload: function SpottedScript_Blank_Questionairre_View$get_spendMusicDownload() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendMusicDownload');
    },
    get_spendDvd: function SpottedScript_Blank_Questionairre_View$get_spendDvd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendDvd');
    },
    get_spendGames: function SpottedScript_Blank_Questionairre_View$get_spendGames() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendGames');
    },
    get_spendMobile: function SpottedScript_Blank_Questionairre_View$get_spendMobile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendMobile');
    },
    get_spendSms: function SpottedScript_Blank_Questionairre_View$get_spendSms() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendSms');
    },
    get_spendCar: function SpottedScript_Blank_Questionairre_View$get_spendCar() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendCar');
    },
    get_spendTravel: function SpottedScript_Blank_Questionairre_View$get_spendTravel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpendTravel');
    },
    get_holidays: function SpottedScript_Blank_Questionairre_View$get_holidays() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Holidays');
    },
    get_panelDemographics: function SpottedScript_Blank_Questionairre_View$get_panelDemographics() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDemographics');
    },
    get_button1: function SpottedScript_Blank_Questionairre_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_validationsummary2: function SpottedScript_Blank_Questionairre_View$get_validationsummary2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary2');
    },
    get_customvalidator1: function SpottedScript_Blank_Questionairre_View$get_customvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator1');
    },
    get_customvalidator2: function SpottedScript_Blank_Questionairre_View$get_customvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator2');
    },
    get_customvalidator3: function SpottedScript_Blank_Questionairre_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_customvalidator4: function SpottedScript_Blank_Questionairre_View$get_customvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator4');
    },
    get_customvalidator5: function SpottedScript_Blank_Questionairre_View$get_customvalidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator5');
    },
    get_customvalidator6: function SpottedScript_Blank_Questionairre_View$get_customvalidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator6');
    },
    get_customvalidator7: function SpottedScript_Blank_Questionairre_View$get_customvalidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator7');
    },
    get_customvalidator8: function SpottedScript_Blank_Questionairre_View$get_customvalidator8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator8');
    },
    get_customvalidator9: function SpottedScript_Blank_Questionairre_View$get_customvalidator9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator9');
    },
    get_imagingManufacturer: function SpottedScript_Blank_Questionairre_View$get_imagingManufacturer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingManufacturer');
    },
    get_imagingImportant1: function SpottedScript_Blank_Questionairre_View$get_imagingImportant1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingImportant1');
    },
    get_imagingImportant2: function SpottedScript_Blank_Questionairre_View$get_imagingImportant2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingImportant2');
    },
    get_imagingImportant3: function SpottedScript_Blank_Questionairre_View$get_imagingImportant3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingImportant3');
    },
    get_imagingImportant4: function SpottedScript_Blank_Questionairre_View$get_imagingImportant4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingImportant4');
    },
    get_imagingImportant5: function SpottedScript_Blank_Questionairre_View$get_imagingImportant5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingImportant5');
    },
    get_customvalidator10: function SpottedScript_Blank_Questionairre_View$get_customvalidator10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator10');
    },
    get_imagingOpinionSony1: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSony1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSony1');
    },
    get_imagingOpinionSony2: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSony2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSony2');
    },
    get_imagingOpinionSony3: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSony3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSony3');
    },
    get_imagingOpinionSony4: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSony4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSony4');
    },
    get_imagingOpinionSony5: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSony5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSony5');
    },
    get_imagingOpinionNokia1: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionNokia1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionNokia1');
    },
    get_imagingOpinionNokia2: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionNokia2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionNokia2');
    },
    get_imagingOpinionNokia3: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionNokia3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionNokia3');
    },
    get_imagingOpinionNokia4: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionNokia4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionNokia4');
    },
    get_imagingOpinionNokia5: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionNokia5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionNokia5');
    },
    get_imagingOpinionMotorola1: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionMotorola1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionMotorola1');
    },
    get_imagingOpinionMotorola2: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionMotorola2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionMotorola2');
    },
    get_imagingOpinionMotorola3: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionMotorola3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionMotorola3');
    },
    get_imagingOpinionMotorola4: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionMotorola4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionMotorola4');
    },
    get_imagingOpinionMotorola5: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionMotorola5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionMotorola5');
    },
    get_imagingOpinionSiemens1: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSiemens1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSiemens1');
    },
    get_imagingOpinionSiemens2: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSiemens2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSiemens2');
    },
    get_imagingOpinionSiemens3: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSiemens3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSiemens3');
    },
    get_imagingOpinionSiemens4: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSiemens4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSiemens4');
    },
    get_imagingOpinionSiemens5: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSiemens5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSiemens5');
    },
    get_imagingOpinionLg1: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionLg1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionLg1');
    },
    get_imagingOpinionLg2: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionLg2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionLg2');
    },
    get_imagingOpinionLg3: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionLg3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionLg3');
    },
    get_imagingOpinionLg4: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionLg4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionLg4');
    },
    get_imagingOpinionLg5: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionLg5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionLg5');
    },
    get_imagingOpinionSamsung1: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSamsung1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSamsung1');
    },
    get_imagingOpinionSamsung2: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSamsung2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSamsung2');
    },
    get_imagingOpinionSamsung3: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSamsung3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSamsung3');
    },
    get_imagingOpinionSamsung4: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSamsung4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSamsung4');
    },
    get_imagingOpinionSamsung5: function SpottedScript_Blank_Questionairre_View$get_imagingOpinionSamsung5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingOpinionSamsung5');
    },
    get_customvalidator12: function SpottedScript_Blank_Questionairre_View$get_customvalidator12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator12');
    },
    get_imagingCapabilitySony1: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySony1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySony1');
    },
    get_imagingCapabilitySony2: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySony2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySony2');
    },
    get_imagingCapabilitySony3: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySony3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySony3');
    },
    get_imagingCapabilitySony4: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySony4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySony4');
    },
    get_imagingCapabilitySony5: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySony5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySony5');
    },
    get_imagingCapabilityNokia1: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityNokia1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityNokia1');
    },
    get_imagingCapabilityNokia2: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityNokia2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityNokia2');
    },
    get_imagingCapabilityNokia3: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityNokia3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityNokia3');
    },
    get_imagingCapabilityNokia4: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityNokia4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityNokia4');
    },
    get_imagingCapabilityNokia5: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityNokia5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityNokia5');
    },
    get_imagingCapabilityMotorola1: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityMotorola1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityMotorola1');
    },
    get_imagingCapabilityMotorola2: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityMotorola2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityMotorola2');
    },
    get_imagingCapabilityMotorola3: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityMotorola3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityMotorola3');
    },
    get_imagingCapabilityMotorola4: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityMotorola4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityMotorola4');
    },
    get_imagingCapabilityMotorola5: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityMotorola5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityMotorola5');
    },
    get_imagingCapabilitySiemens1: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySiemens1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySiemens1');
    },
    get_imagingCapabilitySiemens2: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySiemens2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySiemens2');
    },
    get_imagingCapabilitySiemens3: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySiemens3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySiemens3');
    },
    get_imagingCapabilitySiemens4: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySiemens4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySiemens4');
    },
    get_imagingCapabilitySiemens5: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySiemens5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySiemens5');
    },
    get_imagingCapabilityLg1: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityLg1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityLg1');
    },
    get_imagingCapabilityLg2: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityLg2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityLg2');
    },
    get_imagingCapabilityLg3: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityLg3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityLg3');
    },
    get_imagingCapabilityLg4: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityLg4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityLg4');
    },
    get_imagingCapabilityLg5: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilityLg5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilityLg5');
    },
    get_imagingCapabilitySamsung1: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySamsung1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySamsung1');
    },
    get_imagingCapabilitySamsung2: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySamsung2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySamsung2');
    },
    get_imagingCapabilitySamsung3: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySamsung3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySamsung3');
    },
    get_imagingCapabilitySamsung4: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySamsung4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySamsung4');
    },
    get_imagingCapabilitySamsung5: function SpottedScript_Blank_Questionairre_View$get_imagingCapabilitySamsung5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingCapabilitySamsung5');
    },
    get_customvalidator13: function SpottedScript_Blank_Questionairre_View$get_customvalidator13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator13');
    },
    get_imagingBuySony1: function SpottedScript_Blank_Questionairre_View$get_imagingBuySony1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySony1');
    },
    get_imagingBuySony2: function SpottedScript_Blank_Questionairre_View$get_imagingBuySony2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySony2');
    },
    get_imagingBuySony3: function SpottedScript_Blank_Questionairre_View$get_imagingBuySony3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySony3');
    },
    get_imagingBuySony4: function SpottedScript_Blank_Questionairre_View$get_imagingBuySony4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySony4');
    },
    get_imagingBuySony5: function SpottedScript_Blank_Questionairre_View$get_imagingBuySony5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySony5');
    },
    get_imagingBuyNokia1: function SpottedScript_Blank_Questionairre_View$get_imagingBuyNokia1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyNokia1');
    },
    get_imagingBuyNokia2: function SpottedScript_Blank_Questionairre_View$get_imagingBuyNokia2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyNokia2');
    },
    get_imagingBuyNokia3: function SpottedScript_Blank_Questionairre_View$get_imagingBuyNokia3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyNokia3');
    },
    get_imagingBuyNokia4: function SpottedScript_Blank_Questionairre_View$get_imagingBuyNokia4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyNokia4');
    },
    get_imagingBuyNokia5: function SpottedScript_Blank_Questionairre_View$get_imagingBuyNokia5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyNokia5');
    },
    get_imagingBuyMotorola1: function SpottedScript_Blank_Questionairre_View$get_imagingBuyMotorola1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyMotorola1');
    },
    get_imagingBuyMotorola2: function SpottedScript_Blank_Questionairre_View$get_imagingBuyMotorola2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyMotorola2');
    },
    get_imagingBuyMotorola3: function SpottedScript_Blank_Questionairre_View$get_imagingBuyMotorola3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyMotorola3');
    },
    get_imagingBuyMotorola4: function SpottedScript_Blank_Questionairre_View$get_imagingBuyMotorola4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyMotorola4');
    },
    get_imagingBuyMotorola5: function SpottedScript_Blank_Questionairre_View$get_imagingBuyMotorola5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyMotorola5');
    },
    get_imagingBuySiemens1: function SpottedScript_Blank_Questionairre_View$get_imagingBuySiemens1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySiemens1');
    },
    get_imagingBuySiemens2: function SpottedScript_Blank_Questionairre_View$get_imagingBuySiemens2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySiemens2');
    },
    get_imagingBuySiemens3: function SpottedScript_Blank_Questionairre_View$get_imagingBuySiemens3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySiemens3');
    },
    get_imagingBuySiemens4: function SpottedScript_Blank_Questionairre_View$get_imagingBuySiemens4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySiemens4');
    },
    get_imagingBuySiemens5: function SpottedScript_Blank_Questionairre_View$get_imagingBuySiemens5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySiemens5');
    },
    get_imagingBuyLg1: function SpottedScript_Blank_Questionairre_View$get_imagingBuyLg1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyLg1');
    },
    get_imagingBuyLg2: function SpottedScript_Blank_Questionairre_View$get_imagingBuyLg2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyLg2');
    },
    get_imagingBuyLg3: function SpottedScript_Blank_Questionairre_View$get_imagingBuyLg3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyLg3');
    },
    get_imagingBuyLg4: function SpottedScript_Blank_Questionairre_View$get_imagingBuyLg4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyLg4');
    },
    get_imagingBuyLg5: function SpottedScript_Blank_Questionairre_View$get_imagingBuyLg5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuyLg5');
    },
    get_imagingBuySamsung1: function SpottedScript_Blank_Questionairre_View$get_imagingBuySamsung1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySamsung1');
    },
    get_imagingBuySamsung2: function SpottedScript_Blank_Questionairre_View$get_imagingBuySamsung2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySamsung2');
    },
    get_imagingBuySamsung3: function SpottedScript_Blank_Questionairre_View$get_imagingBuySamsung3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySamsung3');
    },
    get_imagingBuySamsung4: function SpottedScript_Blank_Questionairre_View$get_imagingBuySamsung4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySamsung4');
    },
    get_imagingBuySamsung5: function SpottedScript_Blank_Questionairre_View$get_imagingBuySamsung5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImagingBuySamsung5');
    },
    get_customvalidator11: function SpottedScript_Blank_Questionairre_View$get_customvalidator11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator11');
    },
    get_button3: function SpottedScript_Blank_Questionairre_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_genericContainerPage: function SpottedScript_Blank_Questionairre_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Questionairre.View.registerClass('SpottedScript.Blank.Questionairre.View', SpottedScript.BlankUserControl.View);
