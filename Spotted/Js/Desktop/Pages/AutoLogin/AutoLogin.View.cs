//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.AutoLogin
{
	public partial class View
		 : Js.DsiUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public InputElement AutoLogin_Value {get {if (_AutoLogin_Value == null) {_AutoLogin_Value = (InputElement)Document.GetElementById(clientId + "_AutoLogin_Value");}; return _AutoLogin_Value;}} private InputElement _AutoLogin_Value;
		public jQueryObject AutoLogin_ValueJ {get {if (_AutoLogin_ValueJ == null) {_AutoLogin_ValueJ = jQuery.Select("#" + clientId + "_AutoLogin_Value");}; return _AutoLogin_ValueJ;}} private jQueryObject _AutoLogin_ValueJ;
		public InputElement AutoLogin_RedirectUrl {get {if (_AutoLogin_RedirectUrl == null) {_AutoLogin_RedirectUrl = (InputElement)Document.GetElementById(clientId + "_AutoLogin_RedirectUrl");}; return _AutoLogin_RedirectUrl;}} private InputElement _AutoLogin_RedirectUrl;
		public jQueryObject AutoLogin_RedirectUrlJ {get {if (_AutoLogin_RedirectUrlJ == null) {_AutoLogin_RedirectUrlJ = jQuery.Select("#" + clientId + "_AutoLogin_RedirectUrl");}; return _AutoLogin_RedirectUrlJ;}} private jQueryObject _AutoLogin_RedirectUrlJ;
		public InputElement AutoLogin_UsrK {get {if (_AutoLogin_UsrK == null) {_AutoLogin_UsrK = (InputElement)Document.GetElementById(clientId + "_AutoLogin_UsrK");}; return _AutoLogin_UsrK;}} private InputElement _AutoLogin_UsrK;
		public jQueryObject AutoLogin_UsrKJ {get {if (_AutoLogin_UsrKJ == null) {_AutoLogin_UsrKJ = jQuery.Select("#" + clientId + "_AutoLogin_UsrK");}; return _AutoLogin_UsrKJ;}} private jQueryObject _AutoLogin_UsrKJ;
		public InputElement AutoLogin_String {get {if (_AutoLogin_String == null) {_AutoLogin_String = (InputElement)Document.GetElementById(clientId + "_AutoLogin_String");}; return _AutoLogin_String;}} private InputElement _AutoLogin_String;
		public jQueryObject AutoLogin_StringJ {get {if (_AutoLogin_StringJ == null) {_AutoLogin_StringJ = jQuery.Select("#" + clientId + "_AutoLogin_String");}; return _AutoLogin_StringJ;}} private jQueryObject _AutoLogin_StringJ;
		public InputElement AutoLogin_LogOutFirst {get {if (_AutoLogin_LogOutFirst == null) {_AutoLogin_LogOutFirst = (InputElement)Document.GetElementById(clientId + "_AutoLogin_LogOutFirst");}; return _AutoLogin_LogOutFirst;}} private InputElement _AutoLogin_LogOutFirst;
		public jQueryObject AutoLogin_LogOutFirstJ {get {if (_AutoLogin_LogOutFirstJ == null) {_AutoLogin_LogOutFirstJ = jQuery.Select("#" + clientId + "_AutoLogin_LogOutFirst");}; return _AutoLogin_LogOutFirstJ;}} private jQueryObject _AutoLogin_LogOutFirstJ;
		public InputElement AutoLogin_UsrEmail {get {if (_AutoLogin_UsrEmail == null) {_AutoLogin_UsrEmail = (InputElement)Document.GetElementById(clientId + "_AutoLogin_UsrEmail");}; return _AutoLogin_UsrEmail;}} private InputElement _AutoLogin_UsrEmail;
		public jQueryObject AutoLogin_UsrEmailJ {get {if (_AutoLogin_UsrEmailJ == null) {_AutoLogin_UsrEmailJ = jQuery.Select("#" + clientId + "_AutoLogin_UsrEmail");}; return _AutoLogin_UsrEmailJ;}} private jQueryObject _AutoLogin_UsrEmailJ;
		public InputElement AutoLogin_UsrIsSkeleton {get {if (_AutoLogin_UsrIsSkeleton == null) {_AutoLogin_UsrIsSkeleton = (InputElement)Document.GetElementById(clientId + "_AutoLogin_UsrIsSkeleton");}; return _AutoLogin_UsrIsSkeleton;}} private InputElement _AutoLogin_UsrIsSkeleton;
		public jQueryObject AutoLogin_UsrIsSkeletonJ {get {if (_AutoLogin_UsrIsSkeletonJ == null) {_AutoLogin_UsrIsSkeletonJ = jQuery.Select("#" + clientId + "_AutoLogin_UsrIsSkeleton");}; return _AutoLogin_UsrIsSkeletonJ;}} private jQueryObject _AutoLogin_UsrIsSkeletonJ;
		public InputElement AutoLogin_UsrIsEnhancedSecurity {get {if (_AutoLogin_UsrIsEnhancedSecurity == null) {_AutoLogin_UsrIsEnhancedSecurity = (InputElement)Document.GetElementById(clientId + "_AutoLogin_UsrIsEnhancedSecurity");}; return _AutoLogin_UsrIsEnhancedSecurity;}} private InputElement _AutoLogin_UsrIsEnhancedSecurity;
		public jQueryObject AutoLogin_UsrIsEnhancedSecurityJ {get {if (_AutoLogin_UsrIsEnhancedSecurityJ == null) {_AutoLogin_UsrIsEnhancedSecurityJ = jQuery.Select("#" + clientId + "_AutoLogin_UsrIsEnhancedSecurity");}; return _AutoLogin_UsrIsEnhancedSecurityJ;}} private jQueryObject _AutoLogin_UsrIsEnhancedSecurityJ;
		public InputElement AutoLogin_UsrIsFacebookNotConfirmed {get {if (_AutoLogin_UsrIsFacebookNotConfirmed == null) {_AutoLogin_UsrIsFacebookNotConfirmed = (InputElement)Document.GetElementById(clientId + "_AutoLogin_UsrIsFacebookNotConfirmed");}; return _AutoLogin_UsrIsFacebookNotConfirmed;}} private InputElement _AutoLogin_UsrIsFacebookNotConfirmed;
		public jQueryObject AutoLogin_UsrIsFacebookNotConfirmedJ {get {if (_AutoLogin_UsrIsFacebookNotConfirmedJ == null) {_AutoLogin_UsrIsFacebookNotConfirmedJ = jQuery.Select("#" + clientId + "_AutoLogin_UsrIsFacebookNotConfirmed");}; return _AutoLogin_UsrIsFacebookNotConfirmedJ;}} private jQueryObject _AutoLogin_UsrIsFacebookNotConfirmedJ;
		public InputElement AutoLogin_UsrNeedsCaptcha {get {if (_AutoLogin_UsrNeedsCaptcha == null) {_AutoLogin_UsrNeedsCaptcha = (InputElement)Document.GetElementById(clientId + "_AutoLogin_UsrNeedsCaptcha");}; return _AutoLogin_UsrNeedsCaptcha;}} private InputElement _AutoLogin_UsrNeedsCaptcha;
		public jQueryObject AutoLogin_UsrNeedsCaptchaJ {get {if (_AutoLogin_UsrNeedsCaptchaJ == null) {_AutoLogin_UsrNeedsCaptchaJ = jQuery.Select("#" + clientId + "_AutoLogin_UsrNeedsCaptcha");}; return _AutoLogin_UsrNeedsCaptchaJ;}} private jQueryObject _AutoLogin_UsrNeedsCaptchaJ;
		public InputElement AutoLogin_UsrCaptchaEncrypted {get {if (_AutoLogin_UsrCaptchaEncrypted == null) {_AutoLogin_UsrCaptchaEncrypted = (InputElement)Document.GetElementById(clientId + "_AutoLogin_UsrCaptchaEncrypted");}; return _AutoLogin_UsrCaptchaEncrypted;}} private InputElement _AutoLogin_UsrCaptchaEncrypted;
		public jQueryObject AutoLogin_UsrCaptchaEncryptedJ {get {if (_AutoLogin_UsrCaptchaEncryptedJ == null) {_AutoLogin_UsrCaptchaEncryptedJ = jQuery.Select("#" + clientId + "_AutoLogin_UsrCaptchaEncrypted");}; return _AutoLogin_UsrCaptchaEncryptedJ;}} private jQueryObject _AutoLogin_UsrCaptchaEncryptedJ;
		public InputElement AutoLogin_HomePlaceName {get {if (_AutoLogin_HomePlaceName == null) {_AutoLogin_HomePlaceName = (InputElement)Document.GetElementById(clientId + "_AutoLogin_HomePlaceName");}; return _AutoLogin_HomePlaceName;}} private InputElement _AutoLogin_HomePlaceName;
		public jQueryObject AutoLogin_HomePlaceNameJ {get {if (_AutoLogin_HomePlaceNameJ == null) {_AutoLogin_HomePlaceNameJ = jQuery.Select("#" + clientId + "_AutoLogin_HomePlaceName");}; return _AutoLogin_HomePlaceNameJ;}} private jQueryObject _AutoLogin_HomePlaceNameJ;
		public InputElement AutoLogin_HomeCountryName {get {if (_AutoLogin_HomeCountryName == null) {_AutoLogin_HomeCountryName = (InputElement)Document.GetElementById(clientId + "_AutoLogin_HomeCountryName");}; return _AutoLogin_HomeCountryName;}} private InputElement _AutoLogin_HomeCountryName;
		public jQueryObject AutoLogin_HomeCountryNameJ {get {if (_AutoLogin_HomeCountryNameJ == null) {_AutoLogin_HomeCountryNameJ = jQuery.Select("#" + clientId + "_AutoLogin_HomeCountryName");}; return _AutoLogin_HomeCountryNameJ;}} private jQueryObject _AutoLogin_HomeCountryNameJ;
		public InputElement AutoLogin_HomePlaceK {get {if (_AutoLogin_HomePlaceK == null) {_AutoLogin_HomePlaceK = (InputElement)Document.GetElementById(clientId + "_AutoLogin_HomePlaceK");}; return _AutoLogin_HomePlaceK;}} private InputElement _AutoLogin_HomePlaceK;
		public jQueryObject AutoLogin_HomePlaceKJ {get {if (_AutoLogin_HomePlaceKJ == null) {_AutoLogin_HomePlaceKJ = jQuery.Select("#" + clientId + "_AutoLogin_HomePlaceK");}; return _AutoLogin_HomePlaceKJ;}} private jQueryObject _AutoLogin_HomePlaceKJ;
		public InputElement AutoLogin_HomeCountryK {get {if (_AutoLogin_HomeCountryK == null) {_AutoLogin_HomeCountryK = (InputElement)Document.GetElementById(clientId + "_AutoLogin_HomeCountryK");}; return _AutoLogin_HomeCountryK;}} private InputElement _AutoLogin_HomeCountryK;
		public jQueryObject AutoLogin_HomeCountryKJ {get {if (_AutoLogin_HomeCountryKJ == null) {_AutoLogin_HomeCountryKJ = jQuery.Select("#" + clientId + "_AutoLogin_HomeCountryK");}; return _AutoLogin_HomeCountryKJ;}} private jQueryObject _AutoLogin_HomeCountryKJ;
		public InputElement AutoLogin_HomeGoodMatch {get {if (_AutoLogin_HomeGoodMatch == null) {_AutoLogin_HomeGoodMatch = (InputElement)Document.GetElementById(clientId + "_AutoLogin_HomeGoodMatch");}; return _AutoLogin_HomeGoodMatch;}} private InputElement _AutoLogin_HomeGoodMatch;
		public jQueryObject AutoLogin_HomeGoodMatchJ {get {if (_AutoLogin_HomeGoodMatchJ == null) {_AutoLogin_HomeGoodMatchJ = jQuery.Select("#" + clientId + "_AutoLogin_HomeGoodMatch");}; return _AutoLogin_HomeGoodMatchJ;}} private jQueryObject _AutoLogin_HomeGoodMatchJ;
		public InputElement AutoLogin_FavouriteMusicK {get {if (_AutoLogin_FavouriteMusicK == null) {_AutoLogin_FavouriteMusicK = (InputElement)Document.GetElementById(clientId + "_AutoLogin_FavouriteMusicK");}; return _AutoLogin_FavouriteMusicK;}} private InputElement _AutoLogin_FavouriteMusicK;
		public jQueryObject AutoLogin_FavouriteMusicKJ {get {if (_AutoLogin_FavouriteMusicKJ == null) {_AutoLogin_FavouriteMusicKJ = jQuery.Select("#" + clientId + "_AutoLogin_FavouriteMusicK");}; return _AutoLogin_FavouriteMusicKJ;}} private jQueryObject _AutoLogin_FavouriteMusicKJ;
		public InputElement AutoLogin_SendSpottedEmails {get {if (_AutoLogin_SendSpottedEmails == null) {_AutoLogin_SendSpottedEmails = (InputElement)Document.GetElementById(clientId + "_AutoLogin_SendSpottedEmails");}; return _AutoLogin_SendSpottedEmails;}} private InputElement _AutoLogin_SendSpottedEmails;
		public jQueryObject AutoLogin_SendSpottedEmailsJ {get {if (_AutoLogin_SendSpottedEmailsJ == null) {_AutoLogin_SendSpottedEmailsJ = jQuery.Select("#" + clientId + "_AutoLogin_SendSpottedEmails");}; return _AutoLogin_SendSpottedEmailsJ;}} private jQueryObject _AutoLogin_SendSpottedEmailsJ;
		public InputElement AutoLogin_SendEflyers {get {if (_AutoLogin_SendEflyers == null) {_AutoLogin_SendEflyers = (InputElement)Document.GetElementById(clientId + "_AutoLogin_SendEflyers");}; return _AutoLogin_SendEflyers;}} private InputElement _AutoLogin_SendEflyers;
		public jQueryObject AutoLogin_SendEflyersJ {get {if (_AutoLogin_SendEflyersJ == null) {_AutoLogin_SendEflyersJ = jQuery.Select("#" + clientId + "_AutoLogin_SendEflyers");}; return _AutoLogin_SendEflyersJ;}} private jQueryObject _AutoLogin_SendEflyersJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
