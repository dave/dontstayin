Type.registerNamespace('SpottedScript.Pages.AutoLogin');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.AutoLogin.View
SpottedScript.Pages.AutoLogin.View = function SpottedScript_Pages_AutoLogin_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.AutoLogin.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.AutoLogin.View.prototype = {
    clientId: null,
    get_autoLogin_Value: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_Value() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_Value');
    },
    get_autoLogin_RedirectUrl: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_RedirectUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_RedirectUrl');
    },
    get_autoLogin_UsrK: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_UsrK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_UsrK');
    },
    get_autoLogin_String: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_String() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_String');
    },
    get_autoLogin_LogOutFirst: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_LogOutFirst() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_LogOutFirst');
    },
    get_autoLogin_UsrEmail: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_UsrEmail() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_UsrEmail');
    },
    get_autoLogin_UsrIsSkeleton: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_UsrIsSkeleton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_UsrIsSkeleton');
    },
    get_autoLogin_UsrIsEnhancedSecurity: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_UsrIsEnhancedSecurity() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_UsrIsEnhancedSecurity');
    },
    get_autoLogin_UsrIsFacebookNotConfirmed: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_UsrIsFacebookNotConfirmed() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_UsrIsFacebookNotConfirmed');
    },
    get_autoLogin_UsrNeedsCaptcha: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_UsrNeedsCaptcha() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_UsrNeedsCaptcha');
    },
    get_autoLogin_UsrCaptchaEncrypted: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_UsrCaptchaEncrypted() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_UsrCaptchaEncrypted');
    },
    get_autoLogin_HomePlaceName: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_HomePlaceName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_HomePlaceName');
    },
    get_autoLogin_HomeCountryName: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_HomeCountryName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_HomeCountryName');
    },
    get_autoLogin_HomePlaceK: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_HomePlaceK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_HomePlaceK');
    },
    get_autoLogin_HomeCountryK: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_HomeCountryK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_HomeCountryK');
    },
    get_autoLogin_HomeGoodMatch: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_HomeGoodMatch() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_HomeGoodMatch');
    },
    get_autoLogin_FavouriteMusicK: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_FavouriteMusicK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_FavouriteMusicK');
    },
    get_autoLogin_SendSpottedEmails: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_SendSpottedEmails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_SendSpottedEmails');
    },
    get_autoLogin_SendEflyers: function SpottedScript_Pages_AutoLogin_View$get_autoLogin_SendEflyers() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AutoLogin_SendEflyers');
    },
    get_genericContainerPage: function SpottedScript_Pages_AutoLogin_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.AutoLogin.View.registerClass('SpottedScript.Pages.AutoLogin.View', SpottedScript.DsiUserControl.View);
