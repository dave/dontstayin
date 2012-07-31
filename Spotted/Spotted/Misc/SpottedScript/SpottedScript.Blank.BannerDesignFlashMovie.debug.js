Type.registerNamespace('SpottedScript.Blank.BannerDesignFlashMovie');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerDesignFlashMovie.View
SpottedScript.Blank.BannerDesignFlashMovie.View = function SpottedScript_Blank_BannerDesignFlashMovie_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerDesignFlashMovie.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerDesignFlashMovie.View.prototype = {
    clientId: null,
    get_h13dx: function SpottedScript_Blank_BannerDesignFlashMovie_View$get_h13dx() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13dx');
    },
    get_genericContainerPage: function SpottedScript_Blank_BannerDesignFlashMovie_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerDesignFlashMovie.View.registerClass('SpottedScript.Blank.BannerDesignFlashMovie.View', SpottedScript.BlankUserControl.View);
