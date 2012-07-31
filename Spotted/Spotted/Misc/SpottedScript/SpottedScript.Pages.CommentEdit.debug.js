Type.registerNamespace('SpottedScript.Pages.CommentEdit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.CommentEdit.View
SpottedScript.Pages.CommentEdit.View = function SpottedScript_Pages_CommentEdit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.CommentEdit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.CommentEdit.View.prototype = {
    clientId: null,
    get_subjectPanel: function SpottedScript_Pages_CommentEdit_View$get_subjectPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SubjectPanel');
    },
    get_threadSubjectEditBox: function SpottedScript_Pages_CommentEdit_View$get_threadSubjectEditBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSubjectEditBox');
    },
    get_commentEditHtml: function SpottedScript_Pages_CommentEdit_View$get_commentEditHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_CommentEditHtmlController');
    },
    get_genericContainerPage: function SpottedScript_Pages_CommentEdit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.CommentEdit.View.registerClass('SpottedScript.Pages.CommentEdit.View', SpottedScript.DsiUserControl.View);
