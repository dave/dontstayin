//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.BannerEditAutomatic
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
		public Element H120 {get {if (_H120 == null) {_H120 = (Element)Document.GetElementById(clientId + "_H120");}; return _H120;}} private Element _H120;
		public jQueryObject H120J {get {if (_H120J == null) {_H120J = jQuery.Select("#" + clientId + "_H120");}; return _H120J;}} private jQueryObject _H120J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement CustomiseFirstLine {get {if (_CustomiseFirstLine == null) {_CustomiseFirstLine = (InputElement)Document.GetElementById(clientId + "_CustomiseFirstLine");}; return _CustomiseFirstLine;}} private InputElement _CustomiseFirstLine;
		public jQueryObject CustomiseFirstLineJ {get {if (_CustomiseFirstLineJ == null) {_CustomiseFirstLineJ = jQuery.Select("#" + clientId + "_CustomiseFirstLine");}; return _CustomiseFirstLineJ;}} private jQueryObject _CustomiseFirstLineJ;
		public InputElement CustomiseFirstLineSize {get {if (_CustomiseFirstLineSize == null) {_CustomiseFirstLineSize = (InputElement)Document.GetElementById(clientId + "_CustomiseFirstLineSize");}; return _CustomiseFirstLineSize;}} private InputElement _CustomiseFirstLineSize;
		public jQueryObject CustomiseFirstLineSizeJ {get {if (_CustomiseFirstLineSizeJ == null) {_CustomiseFirstLineSizeJ = jQuery.Select("#" + clientId + "_CustomiseFirstLineSize");}; return _CustomiseFirstLineSizeJ;}} private jQueryObject _CustomiseFirstLineSizeJ;
		public InputElement CustomiseSecondLine {get {if (_CustomiseSecondLine == null) {_CustomiseSecondLine = (InputElement)Document.GetElementById(clientId + "_CustomiseSecondLine");}; return _CustomiseSecondLine;}} private InputElement _CustomiseSecondLine;
		public jQueryObject CustomiseSecondLineJ {get {if (_CustomiseSecondLineJ == null) {_CustomiseSecondLineJ = jQuery.Select("#" + clientId + "_CustomiseSecondLine");}; return _CustomiseSecondLineJ;}} private jQueryObject _CustomiseSecondLineJ;
		public InputElement CustomiseThirdLine {get {if (_CustomiseThirdLine == null) {_CustomiseThirdLine = (InputElement)Document.GetElementById(clientId + "_CustomiseThirdLine");}; return _CustomiseThirdLine;}} private InputElement _CustomiseThirdLine;
		public jQueryObject CustomiseThirdLineJ {get {if (_CustomiseThirdLineJ == null) {_CustomiseThirdLineJ = jQuery.Select("#" + clientId + "_CustomiseThirdLine");}; return _CustomiseThirdLineJ;}} private jQueryObject _CustomiseThirdLineJ;
		public Element Button9 {get {if (_Button9 == null) {_Button9 = (Element)Document.GetElementById(clientId + "_Button9");}; return _Button9;}} private Element _Button9;
		public jQueryObject Button9J {get {if (_Button9J == null) {_Button9J = jQuery.Select("#" + clientId + "_Button9");}; return _Button9J;}} private jQueryObject _Button9J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SaveButton {get {if (_SaveButton == null) {_SaveButton = (Element)Document.GetElementById(clientId + "_SaveButton");}; return _SaveButton;}} private Element _SaveButton;
		public jQueryObject SaveButtonJ {get {if (_SaveButtonJ == null) {_SaveButtonJ = jQuery.Select("#" + clientId + "_SaveButton");}; return _SaveButtonJ;}} private jQueryObject _SaveButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element RemoveButton {get {if (_RemoveButton == null) {_RemoveButton = (Element)Document.GetElementById(clientId + "_RemoveButton");}; return _RemoveButton;}} private Element _RemoveButton;
		public jQueryObject RemoveButtonJ {get {if (_RemoveButtonJ == null) {_RemoveButtonJ = jQuery.Select("#" + clientId + "_RemoveButton");}; return _RemoveButtonJ;}} private jQueryObject _RemoveButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
