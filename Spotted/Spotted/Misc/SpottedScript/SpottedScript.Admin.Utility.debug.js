Type.registerNamespace('SpottedScript.Admin.Utility');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Utility.View
SpottedScript.Admin.Utility.View = function SpottedScript_Admin_Utility_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Utility.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Utility.View.prototype = {
    clientId: null,
    get_button21: function SpottedScript_Admin_Utility_View$get_button21() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button21');
    },
    get_button1: function SpottedScript_Admin_Utility_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Admin_Utility_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button3: function SpottedScript_Admin_Utility_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_button4: function SpottedScript_Admin_Utility_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_button5: function SpottedScript_Admin_Utility_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_button7: function SpottedScript_Admin_Utility_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_button8: function SpottedScript_Admin_Utility_View$get_button8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button8');
    },
    get_button9: function SpottedScript_Admin_Utility_View$get_button9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button9');
    },
    get_button10: function SpottedScript_Admin_Utility_View$get_button10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button10');
    },
    get_button11: function SpottedScript_Admin_Utility_View$get_button11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button11');
    },
    get_button12: function SpottedScript_Admin_Utility_View$get_button12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button12');
    },
    get_button14: function SpottedScript_Admin_Utility_View$get_button14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button14');
    },
    get_button15: function SpottedScript_Admin_Utility_View$get_button15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button15');
    },
    get_button16: function SpottedScript_Admin_Utility_View$get_button16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button16');
    },
    get_button17: function SpottedScript_Admin_Utility_View$get_button17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button17');
    },
    get_button18: function SpottedScript_Admin_Utility_View$get_button18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button18');
    },
    get_button19: function SpottedScript_Admin_Utility_View$get_button19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button19');
    },
    get_button20: function SpottedScript_Admin_Utility_View$get_button20() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button20');
    },
    get_button23: function SpottedScript_Admin_Utility_View$get_button23() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button23');
    },
    get_button24: function SpottedScript_Admin_Utility_View$get_button24() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button24');
    },
    get_button25: function SpottedScript_Admin_Utility_View$get_button25() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button25');
    },
    get_button26: function SpottedScript_Admin_Utility_View$get_button26() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button26');
    },
    get_button27: function SpottedScript_Admin_Utility_View$get_button27() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button27');
    },
    get_ticketRunK: function SpottedScript_Admin_Utility_View$get_ticketRunK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunK');
    },
    get_button22: function SpottedScript_Admin_Utility_View$get_button22() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button22');
    },
    get_button28: function SpottedScript_Admin_Utility_View$get_button28() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button28');
    },
    get_genericContainerPage: function SpottedScript_Admin_Utility_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Utility.View.registerClass('SpottedScript.Admin.Utility.View', SpottedScript.AdminUserControl.View);
