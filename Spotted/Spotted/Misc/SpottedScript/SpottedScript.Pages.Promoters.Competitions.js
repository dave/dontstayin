Type.registerNamespace('SpottedScript.Pages.Promoters.Competitions');
SpottedScript.Pages.Promoters.Competitions.View=function(clientId){SpottedScript.Pages.Promoters.Competitions.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.Competitions.View.prototype={clientId:null,get_panelEdit:function(){return document.getElementById(this.clientId+'_PanelEdit');},get_editPrizesFirstNumber:function(){return document.getElementById(this.clientId+'_EditPrizesFirstNumber');},get_editPrizesFirstDesc:function(){return document.getElementById(this.clientId+'_EditPrizesFirstDesc');},get_editPrizesSecondNumber:function(){return document.getElementById(this.clientId+'_EditPrizesSecondNumber');},get_editPrizesSecondDesc:function(){return document.getElementById(this.clientId+'_EditPrizesSecondDesc');},get_editPrizesThirdNumber:function(){return document.getElementById(this.clientId+'_EditPrizesThirdNumber');},get_editPrizesThirdDesc:function(){return document.getElementById(this.clientId+'_EditPrizesThirdDesc');},get_editPrizesValue:function(){return document.getElementById(this.clientId+'_EditPrizesValue');},get_editLinkNoneRadio:function(){return document.getElementById(this.clientId+'_EditLinkNoneRadio');},get_editLinkEventRadio:function(){return document.getElementById(this.clientId+'_EditLinkEventRadio');},get_editLinkBrandRadio:function(){return document.getElementById(this.clientId+'_EditLinkBrandRadio');},get_editLinkEventP:function(){return document.getElementById(this.clientId+'_EditLinkEventP');},get_editLinkBrandP:function(){return document.getElementById(this.clientId+'_EditLinkBrandP');},get_editLinkEventDropDown:function(){return document.getElementById(this.clientId+'_EditLinkEventDropDown');},get_editLinkBrandDropDown:function(){return document.getElementById(this.clientId+'_EditLinkBrandDropDown');},get_editLinkEventAnchor:function(){return document.getElementById(this.clientId+'_EditLinkEventAnchor');},get_editLinkBrandAnchor:function(){return document.getElementById(this.clientId+'_EditLinkBrandAnchor');},get_editQuestion:function(){return document.getElementById(this.clientId+'_EditQuestion');},get_editAnswer1:function(){return document.getElementById(this.clientId+'_EditAnswer1');},get_editAnswer2:function(){return document.getElementById(this.clientId+'_EditAnswer2');},get_editAnswer3:function(){return document.getElementById(this.clientId+'_EditAnswer3');},get_editCorrectRadio1:function(){return document.getElementById(this.clientId+'_EditCorrectRadio1');},get_editCorrectRadio2:function(){return document.getElementById(this.clientId+'_EditCorrectRadio2');},get_editCorrectRadio3:function(){return document.getElementById(this.clientId+'_EditCorrectRadio3');},get_editPrizeContact:function(){return document.getElementById(this.clientId+'_EditPrizeContact');},get_editDateClose:function(){return document.getElementById(this.clientId+'_EditDateClose');},get_editDateStart:function(){return document.getElementById(this.clientId+'_EditDateStart');},get_editLinkTr:function(){return document.getElementById(this.clientId+'_EditLinkTr');},get_panelPic:function(){return document.getElementById(this.clientId+'_PanelPic');},get_picUploadPanel:function(){return document.getElementById(this.clientId+'_PicUploadPanel');},get_panelPicSavedPanel:function(){return document.getElementById(this.clientId+'_PanelPicSavedPanel');},get_picUc:function(){return document.getElementById(this.clientId+'_PicUc');},get_picUploadDefaultPanel:function(){return document.getElementById(this.clientId+'_PicUploadDefaultPanel');},get_picUploadDefaultDataList:function(){return document.getElementById(this.clientId+'_PicUploadDefaultDataList');},get_panelList:function(){return document.getElementById(this.clientId+'_PanelList');},get_compDataGrid:function(){return document.getElementById(this.clientId+'_CompDataGrid');},get_promoterIntro:function(){return document.getElementById(this.clientId+'_PromoterIntro');},get_infoPanel:function(){return document.getElementById(this.clientId+'_InfoPanel');},get_h14:function(){return document.getElementById(this.clientId+'_H14');},get_validationsummary1:function(){return document.getElementById(this.clientId+'_Validationsummary1');},get_requiredFieldValidator1:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator1');},get_compareValidator1:function(){return document.getElementById(this.clientId+'_CompareValidator1');},get_requiredFieldValidator2:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator2');},get_regularExpressionValidator1:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator1');},get_compareValidator2:function(){return document.getElementById(this.clientId+'_CompareValidator2');},get_regularExpressionValidator2:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator2');},get_compareValidator3:function(){return document.getElementById(this.clientId+'_CompareValidator3');},get_regularExpressionValidator3:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator3');},get_requiredFieldValidator3:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator3');},get_regularExpressionValidator4:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator4');},get_editSponsorDescriptionHtml:function(){return eval(this.clientId+'_EditSponsorDescriptionHtmlController');},get_customValidator1:function(){return document.getElementById(this.clientId+'_CustomValidator1');},get_requiredFieldValidator4:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator4');},get_regularExpressionValidator5:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator5');},get_requiredFieldValidator5:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator5');},get_regularExpressionValidator6:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator6');},get_requiredFieldValidator6:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator6');},get_regularExpressionValidator7:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator7');},get_requiredFieldValidator7:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator7');},get_regularExpressionValidator8:function(){return document.getElementById(this.clientId+'_RegularExpressionValidator8');},get_customValidator2:function(){return document.getElementById(this.clientId+'_CustomValidator2');},get_customvalidator3:function(){return document.getElementById(this.clientId+'_Customvalidator3');},get_customvalidator4:function(){return document.getElementById(this.clientId+'_Customvalidator4');},get_customvalidator5:function(){return document.getElementById(this.clientId+'_Customvalidator5');},get_customvalidator6:function(){return document.getElementById(this.clientId+'_Customvalidator6');},get_customValidator7:function(){return document.getElementById(this.clientId+'_CustomValidator7');},get_button2:function(){return document.getElementById(this.clientId+'_Button2');},get_button3:function(){return document.getElementById(this.clientId+'_Button3');},get_validationsummary2:function(){return document.getElementById(this.clientId+'_Validationsummary2');},get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.Competitions.View.registerClass('SpottedScript.Pages.Promoters.Competitions.View',SpottedScript.Pages.Promoters.PromoterUserControl.View);
