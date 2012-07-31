using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PagedData.Templates.Events.Header
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public CheckBoxElement uiShowPast {get {if (_uiShowPast == null) {_uiShowPast = (CheckBoxElement)Document.GetElementById(clientId + "_uiShowPast");}; return _uiShowPast;}} private CheckBoxElement _uiShowPast;
		public jQueryObject uiShowPastJ {get {if (_uiShowPastJ == null) {_uiShowPastJ = jQuery.Select("#" + clientId + "_uiShowPast");}; return _uiShowPastJ;}} private jQueryObject _uiShowPastJ;
		public CheckBoxElement uiShowFuture {get {if (_uiShowFuture == null) {_uiShowFuture = (CheckBoxElement)Document.GetElementById(clientId + "_uiShowFuture");}; return _uiShowFuture;}} private CheckBoxElement _uiShowFuture;
		public jQueryObject uiShowFutureJ {get {if (_uiShowFutureJ == null) {_uiShowFutureJ = jQuery.Select("#" + clientId + "_uiShowFuture");}; return _uiShowFutureJ;}} private jQueryObject _uiShowFutureJ;
		public SelectElement uiMusicType {get {if (_uiMusicType == null) {_uiMusicType = (SelectElement)Document.GetElementById(clientId + "_uiMusicType");}; return _uiMusicType;}} private SelectElement _uiMusicType;
		public jQueryObject uiMusicTypeJ {get {if (_uiMusicTypeJ == null) {_uiMusicTypeJ = jQuery.Select("#" + clientId + "_uiMusicType");}; return _uiMusicTypeJ;}} private jQueryObject _uiMusicTypeJ;
	}
}
