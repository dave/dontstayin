Type.registerNamespace('SpottedScript.Blank.Questionairre');
SpottedScript.Blank.Questionairre.View=function(clientId){SpottedScript.Blank.Questionairre.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Questionairre.View.prototype={clientId:null,get_doneQuestionairrePanel:function(){return document.getElementById(this.clientId+'_DoneQuestionairrePanel');},get_questionairrePanel:function(){return document.getElementById(this.clientId+'_QuestionairrePanel');},get_questionairreDiv:function(){return document.getElementById(this.clientId+'_QuestionairreDiv');},get_drinkWater:function(){return document.getElementById(this.clientId+'_DrinkWater');},get_drinkSoft:function(){return document.getElementById(this.clientId+'_DrinkSoft');},get_drinkEnergy:function(){return document.getElementById(this.clientId+'_DrinkEnergy');},get_drinkDraftBeer:function(){return document.getElementById(this.clientId+'_DrinkDraftBeer');},get_drinkBottledBeer:function(){return document.getElementById(this.clientId+'_DrinkBottledBeer');},get_drinkSpirits:function(){return document.getElementById(this.clientId+'_DrinkSpirits');},get_drinkWine:function(){return document.getElementById(this.clientId+'_DrinkWine');},get_drinkAlcopops:function(){return document.getElementById(this.clientId+'_DrinkAlcopops');},get_drinkCider:function(){return document.getElementById(this.clientId+'_DrinkCider');},get_smoke1:function(){return document.getElementById(this.clientId+'_Smoke1');},get_smoke2:function(){return document.getElementById(this.clientId+'_Smoke2');},get_smoke3:function(){return document.getElementById(this.clientId+'_Smoke3');},get_eveningAllNight:function(){return document.getElementById(this.clientId+'_EveningAllNight');},get_eveningLateNight:function(){return document.getElementById(this.clientId+'_EveningLateNight');},get_eveningCoupleDrinks:function(){return document.getElementById(this.clientId+'_EveningCoupleDrinks');},get_eveningOther:function(){return document.getElementById(this.clientId+'_EveningOther');},get_eveningStayIn:function(){return document.getElementById(this.clientId+'_EveningStayIn');},get_employment1:function(){return document.getElementById(this.clientId+'_Employment1');},get_employment2:function(){return document.getElementById(this.clientId+'_Employment2');},get_employment3:function(){return document.getElementById(this.clientId+'_Employment3');},get_employment4:function(){return document.getElementById(this.clientId+'_Employment4');},get_salary1:function(){return document.getElementById(this.clientId+'_Salary1');},get_salary2:function(){return document.getElementById(this.clientId+'_Salary2');},get_salary3:function(){return document.getElementById(this.clientId+'_Salary3');},get_salary4:function(){return document.getElementById(this.clientId+'_Salary4');},get_salary5:function(){return document.getElementById(this.clientId+'_Salary5');},get_salary6:function(){return document.getElementById(this.clientId+'_Salary6');},get_salary7:function(){return document.getElementById(this.clientId+'_Salary7');},get_creditCardYes:function(){return document.getElementById(this.clientId+'_CreditCardYes');},get_creditCardNo:function(){return document.getElementById(this.clientId+'_CreditCardNo');},get_loanYes:function(){return document.getElementById(this.clientId+'_LoanYes');},get_loanNo:function(){return document.getElementById(this.clientId+'_LoanNo');},get_mortgageYes:function(){return document.getElementById(this.clientId+'_MortgageYes');},get_mortgageNo:function(){return document.getElementById(this.clientId+'_MortgageNo');},get_ownCar:function(){return document.getElementById(this.clientId+'_OwnCar');},get_ownBike:function(){return document.getElementById(this.clientId+'_OwnBike');},get_ownMp3:function(){return document.getElementById(this.clientId+'_OwnMp3');},get_ownPc:function(){return document.getElementById(this.clientId+'_OwnPc');},get_ownLaptop:function(){return document.getElementById(this.clientId+'_OwnLaptop');},get_ownMac:function(){return document.getElementById(this.clientId+'_OwnMac');},get_ownBroadband:function(){return document.getElementById(this.clientId+'_OwnBroadband');},get_ownConsole:function(){return document.getElementById(this.clientId+'_OwnConsole');},get_ownCamera:function(){return document.getElementById(this.clientId+'_OwnCamera');},get_ownDvd:function(){return document.getElementById(this.clientId+'_OwnDvd');},get_ownDvdRec:function(){return document.getElementById(this.clientId+'_OwnDvdRec');},get_buyCar:function(){return document.getElementById(this.clientId+'_BuyCar');},get_buyBike:function(){return document.getElementById(this.clientId+'_BuyBike');},get_buyMp3:function(){return document.getElementById(this.clientId+'_BuyMp3');},get_buyPc:function(){return document.getElementById(this.clientId+'_BuyPc');},get_buyLaptop:function(){return document.getElementById(this.clientId+'_BuyLaptop');},get_buyMac:function(){return document.getElementById(this.clientId+'_BuyMac');},get_buyBroadband:function(){return document.getElementById(this.clientId+'_BuyBroadband');},get_buyConsole:function(){return document.getElementById(this.clientId+'_BuyConsole');},get_buyCamera:function(){return document.getElementById(this.clientId+'_BuyCamera');},get_buyDvd:function(){return document.getElementById(this.clientId+'_BuyDvd');},get_buyDvdRec:function(){return document.getElementById(this.clientId+'_BuyDvdRec');},get_spendDesignerClothes:function(){return document.getElementById(this.clientId+'_SpendDesignerClothes');},get_spendHighStreetClothes:function(){return document.getElementById(this.clientId+'_SpendHighStreetClothes');},get_spendMusicCd:function(){return document.getElementById(this.clientId+'_SpendMusicCd');},get_spendMusicVinyl:function(){return document.getElementById(this.clientId+'_SpendMusicVinyl');},get_spendMusicDownload:function(){return document.getElementById(this.clientId+'_SpendMusicDownload');},get_spendDvd:function(){return document.getElementById(this.clientId+'_SpendDvd');},get_spendGames:function(){return document.getElementById(this.clientId+'_SpendGames');},get_spendMobile:function(){return document.getElementById(this.clientId+'_SpendMobile');},get_spendSms:function(){return document.getElementById(this.clientId+'_SpendSms');},get_spendCar:function(){return document.getElementById(this.clientId+'_SpendCar');},get_spendTravel:function(){return document.getElementById(this.clientId+'_SpendTravel');},get_holidays:function(){return document.getElementById(this.clientId+'_Holidays');},get_panelDemographics:function(){return document.getElementById(this.clientId+'_PanelDemographics');},get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_validationsummary2:function(){return document.getElementById(this.clientId+'_Validationsummary2');},get_customvalidator1:function(){return document.getElementById(this.clientId+'_Customvalidator1');},get_customvalidator2:function(){return document.getElementById(this.clientId+'_Customvalidator2');},get_customvalidator3:function(){return document.getElementById(this.clientId+'_Customvalidator3');},get_customvalidator4:function(){return document.getElementById(this.clientId+'_Customvalidator4');},get_customvalidator5:function(){return document.getElementById(this.clientId+'_Customvalidator5');},get_customvalidator6:function(){return document.getElementById(this.clientId+'_Customvalidator6');},get_customvalidator7:function(){return document.getElementById(this.clientId+'_Customvalidator7');},get_customvalidator8:function(){return document.getElementById(this.clientId+'_Customvalidator8');},get_customvalidator9:function(){return document.getElementById(this.clientId+'_Customvalidator9');},get_imagingManufacturer:function(){return document.getElementById(this.clientId+'_ImagingManufacturer');},get_imagingImportant1:function(){return document.getElementById(this.clientId+'_ImagingImportant1');},get_imagingImportant2:function(){return document.getElementById(this.clientId+'_ImagingImportant2');},get_imagingImportant3:function(){return document.getElementById(this.clientId+'_ImagingImportant3');},get_imagingImportant4:function(){return document.getElementById(this.clientId+'_ImagingImportant4');},get_imagingImportant5:function(){return document.getElementById(this.clientId+'_ImagingImportant5');},get_customvalidator10:function(){return document.getElementById(this.clientId+'_Customvalidator10');},get_imagingOpinionSony1:function(){return document.getElementById(this.clientId+'_ImagingOpinionSony1');},get_imagingOpinionSony2:function(){return document.getElementById(this.clientId+'_ImagingOpinionSony2');},get_imagingOpinionSony3:function(){return document.getElementById(this.clientId+'_ImagingOpinionSony3');},get_imagingOpinionSony4:function(){return document.getElementById(this.clientId+'_ImagingOpinionSony4');},get_imagingOpinionSony5:function(){return document.getElementById(this.clientId+'_ImagingOpinionSony5');},get_imagingOpinionNokia1:function(){return document.getElementById(this.clientId+'_ImagingOpinionNokia1');},get_imagingOpinionNokia2:function(){return document.getElementById(this.clientId+'_ImagingOpinionNokia2');},get_imagingOpinionNokia3:function(){return document.getElementById(this.clientId+'_ImagingOpinionNokia3');},get_imagingOpinionNokia4:function(){return document.getElementById(this.clientId+'_ImagingOpinionNokia4');},get_imagingOpinionNokia5:function(){return document.getElementById(this.clientId+'_ImagingOpinionNokia5');},get_imagingOpinionMotorola1:function(){return document.getElementById(this.clientId+'_ImagingOpinionMotorola1');},get_imagingOpinionMotorola2:function(){return document.getElementById(this.clientId+'_ImagingOpinionMotorola2');},get_imagingOpinionMotorola3:function(){return document.getElementById(this.clientId+'_ImagingOpinionMotorola3');},get_imagingOpinionMotorola4:function(){return document.getElementById(this.clientId+'_ImagingOpinionMotorola4');},get_imagingOpinionMotorola5:function(){return document.getElementById(this.clientId+'_ImagingOpinionMotorola5');},get_imagingOpinionSiemens1:function(){return document.getElementById(this.clientId+'_ImagingOpinionSiemens1');},get_imagingOpinionSiemens2:function(){return document.getElementById(this.clientId+'_ImagingOpinionSiemens2');},get_imagingOpinionSiemens3:function(){return document.getElementById(this.clientId+'_ImagingOpinionSiemens3');},get_imagingOpinionSiemens4:function(){return document.getElementById(this.clientId+'_ImagingOpinionSiemens4');},get_imagingOpinionSiemens5:function(){return document.getElementById(this.clientId+'_ImagingOpinionSiemens5');},get_imagingOpinionLg1:function(){return document.getElementById(this.clientId+'_ImagingOpinionLg1');},get_imagingOpinionLg2:function(){return document.getElementById(this.clientId+'_ImagingOpinionLg2');},get_imagingOpinionLg3:function(){return document.getElementById(this.clientId+'_ImagingOpinionLg3');},get_imagingOpinionLg4:function(){return document.getElementById(this.clientId+'_ImagingOpinionLg4');},get_imagingOpinionLg5:function(){return document.getElementById(this.clientId+'_ImagingOpinionLg5');},get_imagingOpinionSamsung1:function(){return document.getElementById(this.clientId+'_ImagingOpinionSamsung1');},get_imagingOpinionSamsung2:function(){return document.getElementById(this.clientId+'_ImagingOpinionSamsung2');},get_imagingOpinionSamsung3:function(){return document.getElementById(this.clientId+'_ImagingOpinionSamsung3');},get_imagingOpinionSamsung4:function(){return document.getElementById(this.clientId+'_ImagingOpinionSamsung4');},get_imagingOpinionSamsung5:function(){return document.getElementById(this.clientId+'_ImagingOpinionSamsung5');},get_customvalidator12:function(){return document.getElementById(this.clientId+'_Customvalidator12');},get_imagingCapabilitySony1:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySony1');},get_imagingCapabilitySony2:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySony2');},get_imagingCapabilitySony3:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySony3');},get_imagingCapabilitySony4:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySony4');},get_imagingCapabilitySony5:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySony5');},get_imagingCapabilityNokia1:function(){return document.getElementById(this.clientId+'_ImagingCapabilityNokia1');},get_imagingCapabilityNokia2:function(){return document.getElementById(this.clientId+'_ImagingCapabilityNokia2');},get_imagingCapabilityNokia3:function(){return document.getElementById(this.clientId+'_ImagingCapabilityNokia3');},get_imagingCapabilityNokia4:function(){return document.getElementById(this.clientId+'_ImagingCapabilityNokia4');},get_imagingCapabilityNokia5:function(){return document.getElementById(this.clientId+'_ImagingCapabilityNokia5');},get_imagingCapabilityMotorola1:function(){return document.getElementById(this.clientId+'_ImagingCapabilityMotorola1');},get_imagingCapabilityMotorola2:function(){return document.getElementById(this.clientId+'_ImagingCapabilityMotorola2');},get_imagingCapabilityMotorola3:function(){return document.getElementById(this.clientId+'_ImagingCapabilityMotorola3');},get_imagingCapabilityMotorola4:function(){return document.getElementById(this.clientId+'_ImagingCapabilityMotorola4');},get_imagingCapabilityMotorola5:function(){return document.getElementById(this.clientId+'_ImagingCapabilityMotorola5');},get_imagingCapabilitySiemens1:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySiemens1');},get_imagingCapabilitySiemens2:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySiemens2');},get_imagingCapabilitySiemens3:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySiemens3');},get_imagingCapabilitySiemens4:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySiemens4');},get_imagingCapabilitySiemens5:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySiemens5');},get_imagingCapabilityLg1:function(){return document.getElementById(this.clientId+'_ImagingCapabilityLg1');},get_imagingCapabilityLg2:function(){return document.getElementById(this.clientId+'_ImagingCapabilityLg2');},get_imagingCapabilityLg3:function(){return document.getElementById(this.clientId+'_ImagingCapabilityLg3');},get_imagingCapabilityLg4:function(){return document.getElementById(this.clientId+'_ImagingCapabilityLg4');},get_imagingCapabilityLg5:function(){return document.getElementById(this.clientId+'_ImagingCapabilityLg5');},get_imagingCapabilitySamsung1:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySamsung1');},get_imagingCapabilitySamsung2:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySamsung2');},get_imagingCapabilitySamsung3:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySamsung3');},get_imagingCapabilitySamsung4:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySamsung4');},get_imagingCapabilitySamsung5:function(){return document.getElementById(this.clientId+'_ImagingCapabilitySamsung5');},get_customvalidator13:function(){return document.getElementById(this.clientId+'_Customvalidator13');},get_imagingBuySony1:function(){return document.getElementById(this.clientId+'_ImagingBuySony1');},get_imagingBuySony2:function(){return document.getElementById(this.clientId+'_ImagingBuySony2');},get_imagingBuySony3:function(){return document.getElementById(this.clientId+'_ImagingBuySony3');},get_imagingBuySony4:function(){return document.getElementById(this.clientId+'_ImagingBuySony4');},get_imagingBuySony5:function(){return document.getElementById(this.clientId+'_ImagingBuySony5');},get_imagingBuyNokia1:function(){return document.getElementById(this.clientId+'_ImagingBuyNokia1');},get_imagingBuyNokia2:function(){return document.getElementById(this.clientId+'_ImagingBuyNokia2');},get_imagingBuyNokia3:function(){return document.getElementById(this.clientId+'_ImagingBuyNokia3');},get_imagingBuyNokia4:function(){return document.getElementById(this.clientId+'_ImagingBuyNokia4');},get_imagingBuyNokia5:function(){return document.getElementById(this.clientId+'_ImagingBuyNokia5');},get_imagingBuyMotorola1:function(){return document.getElementById(this.clientId+'_ImagingBuyMotorola1');},get_imagingBuyMotorola2:function(){return document.getElementById(this.clientId+'_ImagingBuyMotorola2');},get_imagingBuyMotorola3:function(){return document.getElementById(this.clientId+'_ImagingBuyMotorola3');},get_imagingBuyMotorola4:function(){return document.getElementById(this.clientId+'_ImagingBuyMotorola4');},get_imagingBuyMotorola5:function(){return document.getElementById(this.clientId+'_ImagingBuyMotorola5');},get_imagingBuySiemens1:function(){return document.getElementById(this.clientId+'_ImagingBuySiemens1');},get_imagingBuySiemens2:function(){return document.getElementById(this.clientId+'_ImagingBuySiemens2');},get_imagingBuySiemens3:function(){return document.getElementById(this.clientId+'_ImagingBuySiemens3');},get_imagingBuySiemens4:function(){return document.getElementById(this.clientId+'_ImagingBuySiemens4');},get_imagingBuySiemens5:function(){return document.getElementById(this.clientId+'_ImagingBuySiemens5');},get_imagingBuyLg1:function(){return document.getElementById(this.clientId+'_ImagingBuyLg1');},get_imagingBuyLg2:function(){return document.getElementById(this.clientId+'_ImagingBuyLg2');},get_imagingBuyLg3:function(){return document.getElementById(this.clientId+'_ImagingBuyLg3');},get_imagingBuyLg4:function(){return document.getElementById(this.clientId+'_ImagingBuyLg4');},get_imagingBuyLg5:function(){return document.getElementById(this.clientId+'_ImagingBuyLg5');},get_imagingBuySamsung1:function(){return document.getElementById(this.clientId+'_ImagingBuySamsung1');},get_imagingBuySamsung2:function(){return document.getElementById(this.clientId+'_ImagingBuySamsung2');},get_imagingBuySamsung3:function(){return document.getElementById(this.clientId+'_ImagingBuySamsung3');},get_imagingBuySamsung4:function(){return document.getElementById(this.clientId+'_ImagingBuySamsung4');},get_imagingBuySamsung5:function(){return document.getElementById(this.clientId+'_ImagingBuySamsung5');},get_customvalidator11:function(){return document.getElementById(this.clientId+'_Customvalidator11');},get_button3:function(){return document.getElementById(this.clientId+'_Button3');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Questionairre.View.registerClass('SpottedScript.Blank.Questionairre.View',SpottedScript.BlankUserControl.View);
