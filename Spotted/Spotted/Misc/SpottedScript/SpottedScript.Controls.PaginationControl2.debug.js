Type.registerNamespace('SpottedScript.Controls.PaginationControl2');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PaginationControl2.Controller
SpottedScript.Controls.PaginationControl2.Controller = function SpottedScript_Controls_PaginationControl2_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PaginationControl2.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.PaginationControl2.View">
    /// </field>
    /// <field name="_lastPage" type="Number" integer="true">
    /// </field>
    /// <field name="_currentPage" type="Number" integer="true">
    /// </field>
    /// <field name="_onPageChanged" type="Sys.EventHandler">
    /// </field>
    this._view = view;
    view.get_uiPrevPage().setAttribute('onclick', '');
    $addHandler(view.get_uiPrevPage(), 'click', Function.createDelegate(this, this._prevClick));
    view.get_uiNextPage().setAttribute('onclick', '');
    $addHandler(view.get_uiNextPage(), 'click', Function.createDelegate(this, this._nextClick));
    this.set__lastPage(Number.parseInvariant(view.get_uiLastPage().innerHTML));
    this.set__currentPage(Number.parseInvariant(view.get_uiCurrentPage().innerHTML));
}
SpottedScript.Controls.PaginationControl2.Controller.prototype = {
    _view: null,
    _lastPage: 0,
    get__lastPage: function SpottedScript_Controls_PaginationControl2_Controller$get__lastPage() {
        /// <value type="Number" integer="true"></value>
        return this._lastPage;
    },
    set__lastPage: function SpottedScript_Controls_PaginationControl2_Controller$set__lastPage(value) {
        /// <value type="Number" integer="true"></value>
        this._lastPage = value;
        if (this._lastPage === 1) {
            this._view.get_uiContainer().style.display = 'none';
        }
        else if (this._lastPage < 0) {
            this._view.get_uiLastPage().innerHTML = 'many';
        }
        else {
            this._view.get_uiContainer().style.display = '';
            this._view.get_uiLastPage().innerHTML = this._lastPage.toString();
        }
        return value;
    },
    _currentPage: 0,
    get__currentPage: function SpottedScript_Controls_PaginationControl2_Controller$get__currentPage() {
        /// <value type="Number" integer="true"></value>
        return this._currentPage;
    },
    set__currentPage: function SpottedScript_Controls_PaginationControl2_Controller$set__currentPage(value) {
        /// <value type="Number" integer="true"></value>
        this._currentPage = value;
        this._view.get_uiCurrentPage().innerHTML = this._currentPage.toString();
        return value;
    },
    _onPageChanged: null,
    _prevClick: function SpottedScript_Controls_PaginationControl2_Controller$_prevClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._moveToPreviousPage();
    },
    _nextClick: function SpottedScript_Controls_PaginationControl2_Controller$_nextClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._moveToNextPage();
    },
    _moveToNextPage: function SpottedScript_Controls_PaginationControl2_Controller$_moveToNextPage() {
        this._moveToPage((this.get__currentPage() === this.get__lastPage()) ? 1 : (this.get__currentPage() + 1));
    },
    _moveToPreviousPage: function SpottedScript_Controls_PaginationControl2_Controller$_moveToPreviousPage() {
        if (this.get__currentPage() > 1 || this.get__lastPage() > 0) {
            this._moveToPage((this.get__currentPage() === 1) ? this.get__lastPage() : (this.get__currentPage() - 1));
        }
    },
    _moveToPage: function SpottedScript_Controls_PaginationControl2_Controller$_moveToPage(page) {
        /// <param name="page" type="Number" integer="true">
        /// </param>
        if ((page > this.get__lastPage() && this.get__lastPage() > 0) || page < 1) {
            page = 1;
        }
        this.set__currentPage(page);
        if (this._onPageChanged != null) {
            this._onPageChanged(this, new SpottedScript.IntEventArgs(page));
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PaginationControl2.View
SpottedScript.Controls.PaginationControl2.View = function SpottedScript_Controls_PaginationControl2_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.PaginationControl2.View.prototype = {
    clientId: null,
    get_uiContainer: function SpottedScript_Controls_PaginationControl2_View$get_uiContainer() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiContainer');
    },
    get_uiPrevPage: function SpottedScript_Controls_PaginationControl2_View$get_uiPrevPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPrevPage');
    },
    get_uiCurrentPage: function SpottedScript_Controls_PaginationControl2_View$get_uiCurrentPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCurrentPage');
    },
    get_uiLastPage: function SpottedScript_Controls_PaginationControl2_View$get_uiLastPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiLastPage');
    },
    get_uiNextPage: function SpottedScript_Controls_PaginationControl2_View$get_uiNextPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNextPage');
    }
}
SpottedScript.Controls.PaginationControl2.Controller.registerClass('SpottedScript.Controls.PaginationControl2.Controller');
SpottedScript.Controls.PaginationControl2.View.registerClass('SpottedScript.Controls.PaginationControl2.View');
