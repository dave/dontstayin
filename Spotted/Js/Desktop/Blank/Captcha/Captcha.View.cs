//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.Captcha
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public ImageElement HipImage {get {if (_HipImage == null) {_HipImage = (ImageElement)Document.GetElementById(clientId + "_HipImage");}; return _HipImage;}} private ImageElement _HipImage;
		public jQueryObject HipImageJ {get {if (_HipImageJ == null) {_HipImageJ = jQuery.Select("#" + clientId + "_HipImage");}; return _HipImageJ;}} private jQueryObject _HipImageJ;
		public InputElement HipChallengeTextBox {get {if (_HipChallengeTextBox == null) {_HipChallengeTextBox = (InputElement)Document.GetElementById(clientId + "_HipChallengeTextBox");}; return _HipChallengeTextBox;}} private InputElement _HipChallengeTextBox;
		public jQueryObject HipChallengeTextBoxJ {get {if (_HipChallengeTextBoxJ == null) {_HipChallengeTextBoxJ = jQuery.Select("#" + clientId + "_HipChallengeTextBox");}; return _HipChallengeTextBoxJ;}} private jQueryObject _HipChallengeTextBoxJ;
		public Element DoneButton {get {if (_DoneButton == null) {_DoneButton = (Element)Document.GetElementById(clientId + "_DoneButton");}; return _DoneButton;}} private Element _DoneButton;
		public jQueryObject DoneButtonJ {get {if (_DoneButtonJ == null) {_DoneButtonJ = jQuery.Select("#" + clientId + "_DoneButton");}; return _DoneButtonJ;}} private jQueryObject _DoneButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element Customvalidator10 {get {if (_Customvalidator10 == null) {_Customvalidator10 = (Element)Document.GetElementById(clientId + "_Customvalidator10");}; return _Customvalidator10;}} private Element _Customvalidator10;
		public jQueryObject Customvalidator10J {get {if (_Customvalidator10J == null) {_Customvalidator10J = jQuery.Select("#" + clientId + "_Customvalidator10");}; return _Customvalidator10J;}} private jQueryObject _Customvalidator10J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
