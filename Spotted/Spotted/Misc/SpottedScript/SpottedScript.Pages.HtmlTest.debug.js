Type.registerNamespace('SpottedScript.Pages.HtmlTest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.HtmlTest.View
SpottedScript.Pages.HtmlTest.View = function SpottedScript_Pages_HtmlTest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.HtmlTest.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.HtmlTest.View.prototype = {
    clientId: null,
    get_uiEventGetter: function SpottedScript_Pages_HtmlTest_View$get_uiEventGetter() {
        /// <value type="SpottedScript.Controls.EventGetter.Controller"></value>
        return eval(this.clientId + '_uiEventGetterController');
    },
    get_uiVenueGetter: function SpottedScript_Pages_HtmlTest_View$get_uiVenueGetter() {
        /// <value type="SpottedScript.Controls.VenueGetter.Controller"></value>
        return eval(this.clientId + '_uiVenueGetterController');
    },
    get_multiBuddyChooser1: function SpottedScript_Pages_HtmlTest_View$get_multiBuddyChooser1() {
        /// <value type="SpottedScript.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_MultiBuddyChooser1Controller');
    },
    get_button1: function SpottedScript_Pages_HtmlTest_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_gridView1: function SpottedScript_Pages_HtmlTest_View$get_gridView1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GridView1');
    },
    get_genericContainerPage: function SpottedScript_Pages_HtmlTest_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.HtmlTest.View.registerClass('SpottedScript.Pages.HtmlTest.View', SpottedScript.DsiUserControl.View);
