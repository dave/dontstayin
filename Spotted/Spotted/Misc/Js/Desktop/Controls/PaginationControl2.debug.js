//! PaginationControl2.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.PaginationControl2');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PaginationControl2.Controller

Js.Controls.PaginationControl2.Controller = function Js_Controls_PaginationControl2_Controller(view) {
    /// <param name="view" type="Js.Controls.PaginationControl2.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.PaginationControl2.View">
    /// </field>
    /// <field name="_lastPage" type="Number" integer="true">
    /// </field>
    /// <field name="_currentPage" type="Number" integer="true">
    /// </field>
    /// <field name="onPageChanged" type="Function">
    /// </field>
    this._view = view;
    view.get_uiPrevPage().setAttribute('onclick', '');
    view.get_uiPrevPageJ().click(ss.Delegate.create(this, this._prevClick));
    view.get_uiNextPage().setAttribute('onclick', '');
    view.get_uiNextPageJ().click(ss.Delegate.create(this, this._nextClick));
    this.set_lastPage(parseInt(view.get_uiLastPage().innerHTML));
    this.set_currentPage(parseInt(view.get_uiCurrentPage().innerHTML));
}
Js.Controls.PaginationControl2.Controller.prototype = {
    _view: null,
    _lastPage: 0,
    
    get_lastPage: function Js_Controls_PaginationControl2_Controller$get_lastPage() {
        /// <value type="Number" integer="true"></value>
        return this._lastPage;
    },
    set_lastPage: function Js_Controls_PaginationControl2_Controller$set_lastPage(value) {
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
    
    get_currentPage: function Js_Controls_PaginationControl2_Controller$get_currentPage() {
        /// <value type="Number" integer="true"></value>
        return this._currentPage;
    },
    set_currentPage: function Js_Controls_PaginationControl2_Controller$set_currentPage(value) {
        /// <value type="Number" integer="true"></value>
        this._currentPage = value;
        this._view.get_uiCurrentPage().innerHTML = this._currentPage.toString();
        return value;
    },
    
    onPageChanged: null,
    
    _prevClick: function Js_Controls_PaginationControl2_Controller$_prevClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this.moveToPreviousPage();
    },
    
    _nextClick: function Js_Controls_PaginationControl2_Controller$_nextClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this.moveToNextPage();
    },
    
    moveToNextPage: function Js_Controls_PaginationControl2_Controller$moveToNextPage() {
        this._moveToPage((this.get_currentPage() === this.get_lastPage()) ? 1 : (this.get_currentPage() + 1));
    },
    
    moveToPreviousPage: function Js_Controls_PaginationControl2_Controller$moveToPreviousPage() {
        if (this.get_currentPage() > 1 || this.get_lastPage() > 0) {
            this._moveToPage((this.get_currentPage() === 1) ? this.get_lastPage() : (this.get_currentPage() - 1));
        }
    },
    
    _moveToPage: function Js_Controls_PaginationControl2_Controller$_moveToPage(page) {
        /// <param name="page" type="Number" integer="true">
        /// </param>
        if ((page > this.get_lastPage() && this.get_lastPage() > 0) || page < 1) {
            page = 1;
        }
        this.set_currentPage(page);
        if (this.onPageChanged != null) {
            this.onPageChanged(this, new Js.Library.IntEventArgs(page));
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PaginationControl2.View

Js.Controls.PaginationControl2.View = function Js_Controls_PaginationControl2_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiContainer" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiContainerJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiPrevPage" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPrevPageJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiCurrentPage" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCurrentPageJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiLastPage" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiLastPageJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiNextPage" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiNextPageJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.PaginationControl2.View.prototype = {
    clientId: null,
    
    get_uiContainer: function Js_Controls_PaginationControl2_View$get_uiContainer() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiContainer == null) {
            this._uiContainer = document.getElementById(this.clientId + '_uiContainer');
        }
        return this._uiContainer;
    },
    
    _uiContainer: null,
    
    get_uiContainerJ: function Js_Controls_PaginationControl2_View$get_uiContainerJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiContainerJ == null) {
            this._uiContainerJ = $('#' + this.clientId + '_uiContainer');
        }
        return this._uiContainerJ;
    },
    
    _uiContainerJ: null,
    
    get_uiPrevPage: function Js_Controls_PaginationControl2_View$get_uiPrevPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPrevPage == null) {
            this._uiPrevPage = document.getElementById(this.clientId + '_uiPrevPage');
        }
        return this._uiPrevPage;
    },
    
    _uiPrevPage: null,
    
    get_uiPrevPageJ: function Js_Controls_PaginationControl2_View$get_uiPrevPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPrevPageJ == null) {
            this._uiPrevPageJ = $('#' + this.clientId + '_uiPrevPage');
        }
        return this._uiPrevPageJ;
    },
    
    _uiPrevPageJ: null,
    
    get_uiCurrentPage: function Js_Controls_PaginationControl2_View$get_uiCurrentPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCurrentPage == null) {
            this._uiCurrentPage = document.getElementById(this.clientId + '_uiCurrentPage');
        }
        return this._uiCurrentPage;
    },
    
    _uiCurrentPage: null,
    
    get_uiCurrentPageJ: function Js_Controls_PaginationControl2_View$get_uiCurrentPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCurrentPageJ == null) {
            this._uiCurrentPageJ = $('#' + this.clientId + '_uiCurrentPage');
        }
        return this._uiCurrentPageJ;
    },
    
    _uiCurrentPageJ: null,
    
    get_uiLastPage: function Js_Controls_PaginationControl2_View$get_uiLastPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiLastPage == null) {
            this._uiLastPage = document.getElementById(this.clientId + '_uiLastPage');
        }
        return this._uiLastPage;
    },
    
    _uiLastPage: null,
    
    get_uiLastPageJ: function Js_Controls_PaginationControl2_View$get_uiLastPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiLastPageJ == null) {
            this._uiLastPageJ = $('#' + this.clientId + '_uiLastPage');
        }
        return this._uiLastPageJ;
    },
    
    _uiLastPageJ: null,
    
    get_uiNextPage: function Js_Controls_PaginationControl2_View$get_uiNextPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiNextPage == null) {
            this._uiNextPage = document.getElementById(this.clientId + '_uiNextPage');
        }
        return this._uiNextPage;
    },
    
    _uiNextPage: null,
    
    get_uiNextPageJ: function Js_Controls_PaginationControl2_View$get_uiNextPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiNextPageJ == null) {
            this._uiNextPageJ = $('#' + this.clientId + '_uiNextPage');
        }
        return this._uiNextPageJ;
    },
    
    _uiNextPageJ: null
}


Js.Controls.PaginationControl2.Controller.registerClass('Js.Controls.PaginationControl2.Controller');
Js.Controls.PaginationControl2.View.registerClass('Js.Controls.PaginationControl2.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
