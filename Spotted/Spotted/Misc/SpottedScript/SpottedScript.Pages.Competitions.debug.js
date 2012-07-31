Type.registerNamespace('SpottedScript.Pages.Competitions');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Competitions.View
SpottedScript.Pages.Competitions.View = function SpottedScript_Pages_Competitions_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Competitions.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Competitions.View.prototype = {
    clientId: null,
    get_h13: function SpottedScript_Pages_Competitions_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_topPhotosNewsPanel: function SpottedScript_Pages_Competitions_View$get_topPhotosNewsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopPhotosNewsPanel');
    },
    get_header: function SpottedScript_Pages_Competitions_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_placeImgCell: function SpottedScript_Pages_Competitions_View$get_placeImgCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceImgCell');
    },
    get_placeInfoTopPhotoCell: function SpottedScript_Pages_Competitions_View$get_placeInfoTopPhotoCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceInfoTopPhotoCell');
    },
    get_topPhotoPanel: function SpottedScript_Pages_Competitions_View$get_topPhotoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopPhotoPanel');
    },
    get_h11: function SpottedScript_Pages_Competitions_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_latestCell: function SpottedScript_Pages_Competitions_View$get_latestCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LatestCell');
    },
    get_spottedPanel: function SpottedScript_Pages_Competitions_View$get_spottedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpottedPanel');
    },
    get_h12: function SpottedScript_Pages_Competitions_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_moreInfoPanel: function SpottedScript_Pages_Competitions_View$get_moreInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreInfoPanel');
    },
    get_moreInfoPanel1: function SpottedScript_Pages_Competitions_View$get_moreInfoPanel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreInfoPanel1');
    },
    get_moreInfoLabel: function SpottedScript_Pages_Competitions_View$get_moreInfoLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreInfoLabel');
    },
    get_moreInfoLabel1: function SpottedScript_Pages_Competitions_View$get_moreInfoLabel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MoreInfoLabel1');
    },
    get_currentCompPanel: function SpottedScript_Pages_Competitions_View$get_currentCompPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CurrentCompPanel');
    },
    get_enteredPanel: function SpottedScript_Pages_Competitions_View$get_enteredPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnteredPanel');
    },
    get_entryPanel: function SpottedScript_Pages_Competitions_View$get_entryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryPanel');
    },
    get_finishedPanel: function SpottedScript_Pages_Competitions_View$get_finishedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FinishedPanel');
    },
    get_winnersPanel: function SpottedScript_Pages_Competitions_View$get_winnersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WinnersPanel');
    },
    get_noWinnersPanel: function SpottedScript_Pages_Competitions_View$get_noWinnersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoWinnersPanel');
    },
    get_youAreAWinnerPanel: function SpottedScript_Pages_Competitions_View$get_youAreAWinnerPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_YouAreAWinnerPanel');
    },
    get_questionLabel: function SpottedScript_Pages_Competitions_View$get_questionLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuestionLabel');
    },
    get_selectedAnswerLabel: function SpottedScript_Pages_Competitions_View$get_selectedAnswerLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectedAnswerLabel');
    },
    get_dateTimeCloseLabel: function SpottedScript_Pages_Competitions_View$get_dateTimeCloseLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateTimeCloseLabel');
    },
    get_dateTimeCloseLabel1: function SpottedScript_Pages_Competitions_View$get_dateTimeCloseLabel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DateTimeCloseLabel1');
    },
    get_correntAnswerLabel: function SpottedScript_Pages_Competitions_View$get_correntAnswerLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CorrentAnswerLabel');
    },
    get_enterLinkButton1: function SpottedScript_Pages_Competitions_View$get_enterLinkButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnterLinkButton1');
    },
    get_enterLinkButton2: function SpottedScript_Pages_Competitions_View$get_enterLinkButton2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnterLinkButton2');
    },
    get_enterLinkButton3: function SpottedScript_Pages_Competitions_View$get_enterLinkButton3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnterLinkButton3');
    },
    get_ownerAnchor: function SpottedScript_Pages_Competitions_View$get_ownerAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnerAnchor');
    },
    get_noCompPanel: function SpottedScript_Pages_Competitions_View$get_noCompPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoCompPanel');
    },
    get_compPanel: function SpottedScript_Pages_Competitions_View$get_compPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Competitions_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Competitions.View.registerClass('SpottedScript.Pages.Competitions.View', SpottedScript.DsiUserControl.View);
