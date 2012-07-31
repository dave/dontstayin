Type.registerNamespace('SpottedScript.Blank.BannerDesignStaticJpg');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerDesignStaticJpg.View
SpottedScript.Blank.BannerDesignStaticJpg.View = function SpottedScript_Blank_BannerDesignStaticJpg_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerDesignStaticJpg.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerDesignStaticJpg.View.prototype = {
    clientId: null,
    get_h13dx: function SpottedScript_Blank_BannerDesignStaticJpg_View$get_h13dx() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13dx');
    },
    get_genericContainerPage: function SpottedScript_Blank_BannerDesignStaticJpg_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerDesignStaticJpg.View.registerClass('SpottedScript.Blank.BannerDesignStaticJpg.View', SpottedScript.BlankUserControl.View);
