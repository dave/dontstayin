Type.registerNamespace('SpottedScript.Blank.BannerDesignAnimatedGif');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerDesignAnimatedGif.View
SpottedScript.Blank.BannerDesignAnimatedGif.View = function SpottedScript_Blank_BannerDesignAnimatedGif_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerDesignAnimatedGif.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerDesignAnimatedGif.View.prototype = {
    clientId: null,
    get_h13dx: function SpottedScript_Blank_BannerDesignAnimatedGif_View$get_h13dx() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13dx');
    },
    get_genericContainerPage: function SpottedScript_Blank_BannerDesignAnimatedGif_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerDesignAnimatedGif.View.registerClass('SpottedScript.Blank.BannerDesignAnimatedGif.View', SpottedScript.BlankUserControl.View);
