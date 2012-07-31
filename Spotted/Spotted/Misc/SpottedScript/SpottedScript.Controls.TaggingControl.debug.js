Type.registerNamespace('SpottedScript.Controls.TaggingControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.TaggingControl.Controller
SpottedScript.Controls.TaggingControl.Controller = function SpottedScript_Controls_TaggingControl_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.TaggingControl.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.TaggingControl.View">
    /// </field>
    /// <field name="_currentPhotoK" type="Number" integer="true">
    /// </field>
    /// <field name="_tagSets" type="Object">
    /// </field>
    this._view = view;
    this._tagSets = {};
    if (view.get_uiAddTagButton() != null) {
        $addHandler(view.get_uiAddTagButton(), 'click', Function.createDelegate(this, this._addTagButtonClick));
        view.get_uiTagAutoSuggest().itemChosen = Function.createDelegate(this, this._addTagFromAutoSuggest);
    }
}
SpottedScript.Controls.TaggingControl.Controller.prototype = {
    _view: null,
    _currentPhotoK: 0,
    get__currentPhotoK: function SpottedScript_Controls_TaggingControl_Controller$get__currentPhotoK() {
        /// <value type="Number" integer="true"></value>
        return this._currentPhotoK;
    },
    set__currentPhotoK: function SpottedScript_Controls_TaggingControl_Controller$set__currentPhotoK(value) {
        /// <value type="Number" integer="true"></value>
        this._currentPhotoK = value;
        this._displayTags();
        return value;
    },
    _tagSets: null,
    _addTagButtonClick: function SpottedScript_Controls_TaggingControl_Controller$_addTagButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._addTag(this._view.get_uiTagAutoSuggest().get_text());
        this._view.get_uiTagAutoSuggest().set_text('');
    },
    _addTagFromAutoSuggest: function SpottedScript_Controls_TaggingControl_Controller$_addTagFromAutoSuggest(pair) {
        /// <param name="pair" type="ScriptSharpLibrary.KeyStringPair">
        /// </param>
        this._addTag(pair.value);
        this._view.get_uiTagAutoSuggest().set_text('');
    },
    _addTag: function SpottedScript_Controls_TaggingControl_Controller$_addTag(tagText) {
        /// <param name="tagText" type="String">
        /// </param>
        Spotted.WebServices.Controls.TaggingControl.Service.addTagToPhoto(tagText, this._currentPhotoK, Function.createDelegate(this, this._addTagSuccessCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, -1);
    },
    _addTagSuccessCallback: function SpottedScript_Controls_TaggingControl_Controller$_addTagSuccessCallback(tag, context, methodName) {
        /// <param name="tag" type="SpottedScript.Controls.TaggingControl.TagStub">
        /// </param>
        /// <param name="context" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (tag != null) {
            (this._tagSets[this.get__currentPhotoK().toString()])[tag.k.toString()] = tag;
            this._displayTags();
        }
    },
    _loadTagsForPhotoKs: function SpottedScript_Controls_TaggingControl_Controller$_loadTagsForPhotoKs(photoKs) {
        /// <param name="photoKs" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        var photoKsNotGot = this._getPhotoKsNotAlreadyLoaded(photoKs);
        if (photoKsNotGot.length > 0) {
            Spotted.WebServices.Controls.TaggingControl.Service.getTagsForPhotoKs(photoKsNotGot, Function.createDelegate(this, this._getTagsForPhotoKsSuccessCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, -1);
        }
        else {
            this._displayTags();
        }
    },
    _getTagsForPhotoKsSuccessCallback: function SpottedScript_Controls_TaggingControl_Controller$_getTagsForPhotoKsSuccessCallback(photoTags, context, methodName) {
        /// <param name="photoTags" type="Object">
        /// </param>
        /// <param name="context" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        var $dict1 = photoTags;
        for (var $key2 in $dict1) {
            var photoTag = { key: $key2, value: $dict1[$key2] };
            this._tagSets[photoTag.key] = photoTag.value;
        }
        this._displayTags();
    },
    _displayTags: function SpottedScript_Controls_TaggingControl_Controller$_displayTags() {
        this._setTags(this._tagSets[this.get__currentPhotoK().toString()]);
    },
    _getPhotoKsNotAlreadyLoaded: function SpottedScript_Controls_TaggingControl_Controller$_getPhotoKsNotAlreadyLoaded(photoKs) {
        /// <param name="photoKs" type="Array" elementType="Number" elementInteger="true">
        /// </param>
        /// <returns type="Array"></returns>
        var notLoaded = [];
        var index = 0;
        for (var i = 0; i < photoKs.length; i++) {
            var found = false;
            var $dict1 = this._tagSets;
            for (var $key2 in $dict1) {
                var d = { key: $key2, value: $dict1[$key2] };
                if (d.key === photoKs[i].toString()) {
                    found = true;
                    break;
                }
            }
            if (!found) {
                notLoaded[index] = photoKs[i];
                index++;
            }
        }
        return notLoaded;
    },
    _setTags: function SpottedScript_Controls_TaggingControl_Controller$_setTags(tags) {
        /// <param name="tags" type="Object">
        /// </param>
        this._view.get_uiTagsDiv().style.display = 'none';
        this._view.get_uiTagsDivServerSide().style.display = 'none';
        if (tags != null) {
            this._view.get_uiTagsDiv().innerHTML = '';
            var $dict1 = tags;
            for (var $key2 in $dict1) {
                var tag = { key: $key2, value: $dict1[$key2] };
                this._view.get_uiTagsDiv().style.display = '';
                this._addTagToTagsDiv(tag.value);
            }
        }
    },
    _addTagToTagsDiv: function SpottedScript_Controls_TaggingControl_Controller$_addTagToTagsDiv(tag) {
        /// <param name="tag" type="SpottedScript.Controls.TaggingControl.TagStub">
        /// </param>
        this._view.get_uiTagsDiv().appendChild(this._createTagDiv(tag));
    },
    _removeTagClicked: function SpottedScript_Controls_TaggingControl_Controller$_removeTagClicked(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (confirm('Are you sure you want to remove the tag \"' + (e.target).getAttribute('tagText').toString() + '\" from this photo?')) {
            var tagK = e.target.getAttribute('tagK');
            Spotted.WebServices.Controls.TaggingControl.Service.removeTagFromPhoto(tagK, this._currentPhotoK, Function.createDelegate(this, this._removeTagSuccessCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), tagK, -1);
        }
    },
    _removeTagSuccessCallback: function SpottedScript_Controls_TaggingControl_Controller$_removeTagSuccessCallback(nullObject, tagK, methodName) {
        /// <param name="nullObject" type="Object">
        /// </param>
        /// <param name="tagK" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        delete (this._tagSets[this._currentPhotoK.toString()])[tagK.toString()];
        this._displayTags();
    },
    _createTagDiv: function SpottedScript_Controls_TaggingControl_Controller$_createTagDiv(tag) {
        /// <param name="tag" type="SpottedScript.Controls.TaggingControl.TagStub">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var span = document.createElement('span');
        var a = document.createElement('a');
        a.innerHTML = tag.tagText;
        a.href = '/tags/' + tag.tagText;
        a.style.paddingLeft = '3px';
        a.style.paddingRight = '3px';
        span.appendChild(a);
        var img = document.createElement('img');
        img.src = '/gfx/minus.gif';
        img.alt = 'X';
        img.title = 'Remove this tag';
        img.className = 'RemoveTagButton';
        img.style.borderWidth = '0px';
        img.style.cursor = 'hand';
        img.setAttribute('tagText', tag.tagText);
        img.setAttribute('tagK', tag.k);
        span.appendChild(img);
        $addHandler(img, 'click', Function.createDelegate(this, this._removeTagClicked));
        return span;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.TaggingControl.TagStub
SpottedScript.Controls.TaggingControl.TagStub = function SpottedScript_Controls_TaggingControl_TagStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="photoK" type="Number" integer="true">
    /// </field>
    /// <field name="tagText" type="String">
    /// </field>
}
SpottedScript.Controls.TaggingControl.TagStub.prototype = {
    k: 0,
    photoK: 0,
    tagText: null
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.TaggingControl.View
SpottedScript.Controls.TaggingControl.View = function SpottedScript_Controls_TaggingControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.TaggingControl.View.prototype = {
    clientId: null,
    get_uiTagPanel: function SpottedScript_Controls_TaggingControl_View$get_uiTagPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagPanel');
    },
    get_uiTagsDiv: function SpottedScript_Controls_TaggingControl_View$get_uiTagsDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagsDiv');
    },
    get_uiTagsDivServerSide: function SpottedScript_Controls_TaggingControl_View$get_uiTagsDivServerSide() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagsDivServerSide');
    },
    get_uiTagRepeater: function SpottedScript_Controls_TaggingControl_View$get_uiTagRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagRepeater');
    },
    get_uiAddPanel: function SpottedScript_Controls_TaggingControl_View$get_uiAddPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddPanel');
    },
    get_uiTagAutoSuggest: function SpottedScript_Controls_TaggingControl_View$get_uiTagAutoSuggest() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiTagAutoSuggestBehaviour');
    },
    get_uiAddTagButton: function SpottedScript_Controls_TaggingControl_View$get_uiAddTagButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddTagButton');
    }
}
SpottedScript.Controls.TaggingControl.Controller.registerClass('SpottedScript.Controls.TaggingControl.Controller');
SpottedScript.Controls.TaggingControl.TagStub.registerClass('SpottedScript.Controls.TaggingControl.TagStub');
SpottedScript.Controls.TaggingControl.View.registerClass('SpottedScript.Controls.TaggingControl.View');
