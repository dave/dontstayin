//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.Mailing
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
		public Element TitleLabel {get {if (_TitleLabel == null) {_TitleLabel = (Element)Document.GetElementById(clientId + "_TitleLabel");}; return _TitleLabel;}} private Element _TitleLabel;
		public jQueryObject TitleLabelJ {get {if (_TitleLabelJ == null) {_TitleLabelJ = jQuery.Select("#" + clientId + "_TitleLabel");}; return _TitleLabelJ;}} private jQueryObject _TitleLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element DoneLabel {get {if (_DoneLabel == null) {_DoneLabel = (Element)Document.GetElementById(clientId + "_DoneLabel");}; return _DoneLabel;}} private Element _DoneLabel;
		public jQueryObject DoneLabelJ {get {if (_DoneLabelJ == null) {_DoneLabelJ = jQuery.Select("#" + clientId + "_DoneLabel");}; return _DoneLabelJ;}} private jQueryObject _DoneLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
