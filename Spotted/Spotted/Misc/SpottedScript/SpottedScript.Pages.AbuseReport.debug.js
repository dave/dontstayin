Type.registerNamespace('SpottedScript.Pages.AbuseReport');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.AbuseReport.View
SpottedScript.Pages.AbuseReport.View = function SpottedScript_Pages_AbuseReport_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.AbuseReport.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.AbuseReport.View.prototype = {
    clientId: null,
    get_header: function SpottedScript_Pages_AbuseReport_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_h11: function SpottedScript_Pages_AbuseReport_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_AbuseReport_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_button1: function SpottedScript_Pages_AbuseReport_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_panelNone: function SpottedScript_Pages_AbuseReport_View$get_panelNone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNone');
    },
    get_panelAbuse: function SpottedScript_Pages_AbuseReport_View$get_panelAbuse() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAbuse');
    },
    get_photoKLabel: function SpottedScript_Pages_AbuseReport_View$get_photoKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoKLabel');
    },
    get_photoStringLabel: function SpottedScript_Pages_AbuseReport_View$get_photoStringLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoStringLabel');
    },
    get_photoAnchor: function SpottedScript_Pages_AbuseReport_View$get_photoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoAnchor');
    },
    get_photoImg: function SpottedScript_Pages_AbuseReport_View$get_photoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoImg');
    },
    get_photoPanel: function SpottedScript_Pages_AbuseReport_View$get_photoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoPanel');
    },
    get_noPhotoPanel: function SpottedScript_Pages_AbuseReport_View$get_noPhotoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPhotoPanel');
    },
    get_abuseByP: function SpottedScript_Pages_AbuseReport_View$get_abuseByP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AbuseByP');
    },
    get_reportByP: function SpottedScript_Pages_AbuseReport_View$get_reportByP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReportByP');
    },
    get_galleriesP: function SpottedScript_Pages_AbuseReport_View$get_galleriesP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesP');
    },
    get_thisGalleryP: function SpottedScript_Pages_AbuseReport_View$get_thisGalleryP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThisGalleryP');
    },
    get_reportDescriptionP: function SpottedScript_Pages_AbuseReport_View$get_reportDescriptionP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReportDescriptionP');
    },
    get_resolveDescriptionP: function SpottedScript_Pages_AbuseReport_View$get_resolveDescriptionP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResolveDescriptionP');
    },
    get_actionsPanel: function SpottedScript_Pages_AbuseReport_View$get_actionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ActionsPanel');
    },
    get_resolvedPanel: function SpottedScript_Pages_AbuseReport_View$get_resolvedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResolvedPanel');
    },
    get_thisGalleryPanel: function SpottedScript_Pages_AbuseReport_View$get_thisGalleryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThisGalleryPanel');
    },
    get_resolvedLabel: function SpottedScript_Pages_AbuseReport_View$get_resolvedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResolvedLabel');
    },
    get_overturnRadio: function SpottedScript_Pages_AbuseReport_View$get_overturnRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OverturnRadio');
    },
    get_noAbuseRadio: function SpottedScript_Pages_AbuseReport_View$get_noAbuseRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoAbuseRadio');
    },
    get_noAbuseDeleteRadio: function SpottedScript_Pages_AbuseReport_View$get_noAbuseDeleteRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoAbuseDeleteRadio');
    },
    get_abuseDeleteRadio: function SpottedScript_Pages_AbuseReport_View$get_abuseDeleteRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AbuseDeleteRadio');
    },
    get_abuseDeleteWatchRadio: function SpottedScript_Pages_AbuseReport_View$get_abuseDeleteWatchRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AbuseDeleteWatchRadio');
    },
    get_abuseDeleteBanRadio: function SpottedScript_Pages_AbuseReport_View$get_abuseDeleteBanRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AbuseDeleteBanRadio');
    },
    get_abuseDeleteBanModerateRadio: function SpottedScript_Pages_AbuseReport_View$get_abuseDeleteBanModerateRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AbuseDeleteBanModerateRadio');
    },
    get_resolveDescriptionTextBox: function SpottedScript_Pages_AbuseReport_View$get_resolveDescriptionTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ResolveDescriptionTextBox');
    },
    get_genericContainerPage: function SpottedScript_Pages_AbuseReport_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.AbuseReport.View.registerClass('SpottedScript.Pages.AbuseReport.View', SpottedScript.DsiUserControl.View);
