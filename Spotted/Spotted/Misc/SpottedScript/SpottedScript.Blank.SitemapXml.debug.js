Type.registerNamespace('SpottedScript.Blank.SitemapXml');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.SitemapXml.View
SpottedScript.Blank.SitemapXml.View = function SpottedScript_Blank_SitemapXml_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.SitemapXml.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.SitemapXml.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_SitemapXml_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.SitemapXml.View.registerClass('SpottedScript.Blank.SitemapXml.View', SpottedScript.BlankUserControl.View);
