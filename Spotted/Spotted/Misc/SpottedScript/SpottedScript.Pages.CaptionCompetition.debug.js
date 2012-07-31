Type.registerNamespace('SpottedScript.Pages.CaptionCompetition');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.CaptionCompetition.Controller
SpottedScript.Pages.CaptionCompetition.Controller = function SpottedScript_Pages_CaptionCompetition_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.CaptionCompetition.View">
    /// </param>
    view.get_uiCommentsDisplay().showComments((view.get_threadK().value !== '') ? Number.parseInvariant(view.get_threadK().value) : 0, (view.get_pageNumber().value !== '') ? Number.parseInvariant(view.get_pageNumber().value) : 0);
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.CaptionCompetition.View
SpottedScript.Pages.CaptionCompetition.View = function SpottedScript_Pages_CaptionCompetition_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.CaptionCompetition.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.CaptionCompetition.View.prototype = {
    clientId: null,
    get_uiPhotoDataList: function SpottedScript_Pages_CaptionCompetition_View$get_uiPhotoDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoDataList');
    },
    get_uiPhotoUrl: function SpottedScript_Pages_CaptionCompetition_View$get_uiPhotoUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoUrl');
    },
    get_uiPhoto: function SpottedScript_Pages_CaptionCompetition_View$get_uiPhoto() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhoto');
    },
    get_uiCaptionText: function SpottedScript_Pages_CaptionCompetition_View$get_uiCaptionText() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCaptionText');
    },
    get_uiPost: function SpottedScript_Pages_CaptionCompetition_View$get_uiPost() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPost');
    },
    get_uiCommentsDisplay: function SpottedScript_Pages_CaptionCompetition_View$get_uiCommentsDisplay() {
        /// <value type="SpottedScript.Controls.CommentsDisplay.Controller"></value>
        return eval(this.clientId + '_uiCommentsDisplayController');
    },
    get_threadK: function SpottedScript_Pages_CaptionCompetition_View$get_threadK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadK');
    },
    get_photoK: function SpottedScript_Pages_CaptionCompetition_View$get_photoK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoK');
    },
    get_pageNumber: function SpottedScript_Pages_CaptionCompetition_View$get_pageNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageNumber');
    },
    get_genericContainerPage: function SpottedScript_Pages_CaptionCompetition_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.CaptionCompetition.Controller.registerClass('SpottedScript.Pages.CaptionCompetition.Controller');
SpottedScript.Pages.CaptionCompetition.View.registerClass('SpottedScript.Pages.CaptionCompetition.View', SpottedScript.DsiUserControl.View);
