//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.MemcachedUtils
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public InputElement SetKey {get {if (_SetKey == null) {_SetKey = (InputElement)Document.GetElementById(clientId + "_SetKey");}; return _SetKey;}} private InputElement _SetKey;
		public jQueryObject SetKeyJ {get {if (_SetKeyJ == null) {_SetKeyJ = jQuery.Select("#" + clientId + "_SetKey");}; return _SetKeyJ;}} private jQueryObject _SetKeyJ;
		public InputElement SetValue {get {if (_SetValue == null) {_SetValue = (InputElement)Document.GetElementById(clientId + "_SetValue");}; return _SetValue;}} private InputElement _SetValue;
		public jQueryObject SetValueJ {get {if (_SetValueJ == null) {_SetValueJ = jQuery.Select("#" + clientId + "_SetValue");}; return _SetValueJ;}} private jQueryObject _SetValueJ;
		public InputElement SetResponse {get {if (_SetResponse == null) {_SetResponse = (InputElement)Document.GetElementById(clientId + "_SetResponse");}; return _SetResponse;}} private InputElement _SetResponse;
		public jQueryObject SetResponseJ {get {if (_SetResponseJ == null) {_SetResponseJ = jQuery.Select("#" + clientId + "_SetResponse");}; return _SetResponseJ;}} private jQueryObject _SetResponseJ;
		public InputElement DeleteKey {get {if (_DeleteKey == null) {_DeleteKey = (InputElement)Document.GetElementById(clientId + "_DeleteKey");}; return _DeleteKey;}} private InputElement _DeleteKey;
		public jQueryObject DeleteKeyJ {get {if (_DeleteKeyJ == null) {_DeleteKeyJ = jQuery.Select("#" + clientId + "_DeleteKey");}; return _DeleteKeyJ;}} private jQueryObject _DeleteKeyJ;
		public InputElement DeleteKeyResponse {get {if (_DeleteKeyResponse == null) {_DeleteKeyResponse = (InputElement)Document.GetElementById(clientId + "_DeleteKeyResponse");}; return _DeleteKeyResponse;}} private InputElement _DeleteKeyResponse;
		public jQueryObject DeleteKeyResponseJ {get {if (_DeleteKeyResponseJ == null) {_DeleteKeyResponseJ = jQuery.Select("#" + clientId + "_DeleteKeyResponse");}; return _DeleteKeyResponseJ;}} private jQueryObject _DeleteKeyResponseJ;
		public InputElement SetCounterKey {get {if (_SetCounterKey == null) {_SetCounterKey = (InputElement)Document.GetElementById(clientId + "_SetCounterKey");}; return _SetCounterKey;}} private InputElement _SetCounterKey;
		public jQueryObject SetCounterKeyJ {get {if (_SetCounterKeyJ == null) {_SetCounterKeyJ = jQuery.Select("#" + clientId + "_SetCounterKey");}; return _SetCounterKeyJ;}} private jQueryObject _SetCounterKeyJ;
		public InputElement SetCounterValue {get {if (_SetCounterValue == null) {_SetCounterValue = (InputElement)Document.GetElementById(clientId + "_SetCounterValue");}; return _SetCounterValue;}} private InputElement _SetCounterValue;
		public jQueryObject SetCounterValueJ {get {if (_SetCounterValueJ == null) {_SetCounterValueJ = jQuery.Select("#" + clientId + "_SetCounterValue");}; return _SetCounterValueJ;}} private jQueryObject _SetCounterValueJ;
		public InputElement SetCounterKeyResponse {get {if (_SetCounterKeyResponse == null) {_SetCounterKeyResponse = (InputElement)Document.GetElementById(clientId + "_SetCounterKeyResponse");}; return _SetCounterKeyResponse;}} private InputElement _SetCounterKeyResponse;
		public jQueryObject SetCounterKeyResponseJ {get {if (_SetCounterKeyResponseJ == null) {_SetCounterKeyResponseJ = jQuery.Select("#" + clientId + "_SetCounterKeyResponse");}; return _SetCounterKeyResponseJ;}} private jQueryObject _SetCounterKeyResponseJ;
		public InputElement GetKey {get {if (_GetKey == null) {_GetKey = (InputElement)Document.GetElementById(clientId + "_GetKey");}; return _GetKey;}} private InputElement _GetKey;
		public jQueryObject GetKeyJ {get {if (_GetKeyJ == null) {_GetKeyJ = jQuery.Select("#" + clientId + "_GetKey");}; return _GetKeyJ;}} private jQueryObject _GetKeyJ;
		public Element GetPara {get {if (_GetPara == null) {_GetPara = (Element)Document.GetElementById(clientId + "_GetPara");}; return _GetPara;}} private Element _GetPara;
		public jQueryObject GetParaJ {get {if (_GetParaJ == null) {_GetParaJ = jQuery.Select("#" + clientId + "_GetPara");}; return _GetParaJ;}} private jQueryObject _GetParaJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement GetCounterKey {get {if (_GetCounterKey == null) {_GetCounterKey = (InputElement)Document.GetElementById(clientId + "_GetCounterKey");}; return _GetCounterKey;}} private InputElement _GetCounterKey;
		public jQueryObject GetCounterKeyJ {get {if (_GetCounterKeyJ == null) {_GetCounterKeyJ = jQuery.Select("#" + clientId + "_GetCounterKey");}; return _GetCounterKeyJ;}} private jQueryObject _GetCounterKeyJ;
		public Element GetCounterKeyLabel {get {if (_GetCounterKeyLabel == null) {_GetCounterKeyLabel = (Element)Document.GetElementById(clientId + "_GetCounterKeyLabel");}; return _GetCounterKeyLabel;}} private Element _GetCounterKeyLabel;
		public jQueryObject GetCounterKeyLabelJ {get {if (_GetCounterKeyLabelJ == null) {_GetCounterKeyLabelJ = jQuery.Select("#" + clientId + "_GetCounterKeyLabel");}; return _GetCounterKeyLabelJ;}} private jQueryObject _GetCounterKeyLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GetCounterValue {get {if (_GetCounterValue == null) {_GetCounterValue = (Element)Document.GetElementById(clientId + "_GetCounterValue");}; return _GetCounterValue;}} private Element _GetCounterValue;
		public jQueryObject GetCounterValueJ {get {if (_GetCounterValueJ == null) {_GetCounterValueJ = jQuery.Select("#" + clientId + "_GetCounterValue");}; return _GetCounterValueJ;}} private jQueryObject _GetCounterValueJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
