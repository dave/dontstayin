//#define DEBUG //Use plain javascript
#undef DEBUG //Use encoded javascript
#region using [...]
using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;
using System.Security.Cryptography;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Web.UI.Design;
using System.Drawing.Design;
using System.Drawing;
using System.Runtime.InteropServices;
#endregion

namespace Cambro.Web.DbCombo
{
	#region Comment and attributes
	/// <summary>
	/// The Database DbCombo box.
	/// 
	/// Create a file called "DbComboServer.aspx" with the following content (just one line):
	/// &lt;%@ Page AutoEventWireup="false" Inherits="Cambro.Web.DbCombo.ServerPage" %>
	/// 
	/// </summary>
	[
	DefaultProperty("ServerMethod"), 
	ValidationPropertyAttribute("ValidationPropertyValue"), 
	Designer(typeof(Cambro.Web.DbCombo.Design.DbComboDesigner))
	]
	#endregion
	public class DbCombo : System.Web.UI.WebControls.WebControl, INamingContainer, IPostBackDataHandler 
	{
		protected override void Render(HtmlTextWriter writer)
		{

			//Page.ClientScript.RegisterForEventValidation(this.UniqueID);

			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "revert");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "autoPostBack");
			
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbTextBox");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbMoreResultsButton");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbQueryHidden");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbReQueryRecordsHidden");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbValueHidden");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbReQueryOnLoad");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbDropDown");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "ulbSearchButton");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbTextBox");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbSearchButton");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbClearButton");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbSelectButton");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbMoreResultsButton");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbHelpButton");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbListBox");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbValueHidden");
			//Page.ClientScript.RegisterForEventValidation(this.UniqueID, "dlbRowsHidden");
			
			base.Render(writer);
		}


		#region Constructor, Control_OnLoad, Control_OnInit, PreRender
		#region Constructor
		/// <summary>
		/// Constructor - public DbCombo()
		/// </summary>
		public DbCombo()
		{
			//System.Threading.Thread.CurrentThread.Name;
			this.Load+=new EventHandler(Control_OnLoad);
			this.Init+=new EventHandler(Control_OnInit);
			this.PreRender+=new EventHandler(Control_OnPreRender);
		}
		#endregion
		#region Control_OnInit
		/// <summary>
		/// Control_OnInit
		/// </summary>
		/// <param name="o">object</param>
		/// <param name="e">EventArgs</param>
		public void Control_OnInit(object o, EventArgs e)
		{
			this.EnsureChildControls();

			//This forces the page to emit the __postBack javascript function.
			string a = Page.GetPostBackClientEvent(this,"");

			//RegisterRequiresPostBack - this is required for LoadPostData to run.
			if (Page!=null)
				Page.RegisterRequiresPostBack(this);

			
		}
		#endregion
		#region Control_OnLoad
		/// <summary>
		/// Control_OnLoad
		/// </summary>
		/// <param name="o">object</param>
		/// <param name="e">EventArgs</param>
		public void Control_OnLoad(object o, EventArgs e)
		{
			if (!DesignMode)
			{
				TieButton(dlbTextBox,dlbSearchButton);
			
				#region Do a search and set the dlbTextBox if we are reverting
				if (Page.IsPostBack && Page.Request.Form["__EVENTTARGET"]==this.UniqueID && Page.Request.Form["__EVENTARGUMENT"]=="revert")
				{
					dlbTextBox.Text=ulbTextBox.Text;
					if(ulbTextBox.Text!="")
						dlbSearchButtonClick(null,new System.EventArgs());
				}
				#endregion
			}

			ulbHolder.Visible=!DownLevel;
			dlbHolder.Visible=DownLevel;

			if (!DesignMode)
			{
				//				TextRevertBox=textRevertBox;

				#region Default Assembly's, types etc.

				Control parent = this;
				int i=0;
				try
				{
					while (!( parent is System.Web.UI.UserControl) && i<100 &! (parent is System.Web.UI.Page ))
					{
						i++;
						parent = parent.Parent;
					}
				}
				catch
				{}

				ulbParentAssembly.Text		= parent.GetType().Assembly.GetName().Name;
				ulbParentType.Text			= parent.GetType().ToString();
				ulbParentBaseAssembly.Text	= parent.GetType().BaseType.Assembly.GetName().Name;
				ulbParentBaseType.Text		= parent.GetType().BaseType.ToString();

				ulbPageAssembly.Text		= this.Page.GetType().Assembly.GetName().Name;
				ulbPageType.Text			= this.Page.GetType().ToString();
				ulbPageBaseAssembly.Text	= this.Page.GetType().BaseType.Assembly.GetName().Name;
				ulbPageBaseType.Text		= this.Page.GetType().BaseType.ToString();
				#endregion
		
			}
		}
		#endregion
		#region PreRender
		/// <summary>
		/// PreRender
		/// </summary>
		/// <param name="o"></param>
		/// <param name="e"></param>
		public void Control_OnPreRender(object o, EventArgs e)
		{
			#region tie up the validators
			foreach(object c in Page.Validators)
			{
				if (c is BaseValidator)
				{
					BaseValidator val = (BaseValidator)c;
					if (val.ControlToValidate == this.ID)
					{
						switch(ValidationProperty)
						{
							case ValidationProperties.Value:
							default:
								val.Attributes["controltovalidate"] = this.UniqueID.Replace(":","_")+"_ulbValueHidden";
								break;
							case ValidationProperties.Text:
								val.Attributes["controltovalidate"] = this.UniqueID.Replace(":","_")+"_ulbTextBox";
								break;
						}
					}
				}
			}
			#endregion

			

			this.EnsureChildControls();
			//			if (!DesignMode)
			//				ulbRevertButton.Attributes["onclick"]=@"if (confirm('"+textRevertBox+"')){"+Page.GetPostBackEventReference(this,"revert")+";}else{return false;}";
			#region Include Javascript
			if (!DownLevel && !DesignMode)
			{
				
				// Define the name and type of the client scripts on the page.
				String csname = "JsScript";
				Type cstype = this.GetType();

				// Get a ClientScriptManager reference from the Page class.
				ClientScriptManager cs = Page.ClientScript;

				// Check to see if the client script is already registered.
				if (!cs.IsClientScriptBlockRegistered(cstype, csname))
				{
					string versionString = "?" + Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Build.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
					string script = "";
					//#if DEBUG
					//script = "<script language=\"JScript\" src=\"" + ServerDir + "DbComboServer.aspx" + versionString + "\"></script>";
					//#else
					script = "<script src=\"" + ServerDir + "DbComboServer.aspx" + versionString + "\"></script>";
					//#endif
					//script = "<script language=\"JScript\" src=\"DbCombo.js\"></script>";
					cs.RegisterClientScriptBlock(cstype, csname, script, false);
				}



			//	Page.RegisterClientScriptBlock("DbCombo", script);
			}
			#endregion

			
			if (!DesignMode)
			{
				ulbUniqueId.Text = this.UniqueID;
				ulbTextBox.Attributes.Add("onkeyup",  "DbComboTextBoxKeyUp('"+this.UniqueID+"');");
				ulbTextBox.Attributes.Add("onkeydown","DbComboTextBoxKeyDown('"+this.UniqueID+"');");
				ulbTextBox.Attributes.Add("onfocus","DbComboTextBoxFocus('"+this.UniqueID+"');");
				ulbMoreResultsButton.Attributes.Add("onclick","DbComboGetMoreResults('"+this.UniqueID+"');return false;");
				ulbDropDown.Attributes.Add("onfocus","DbComboFocus('"+this.UniqueID+"');");
				ulbDropDown.Attributes.Add("onclick","DbComboClick('"+this.UniqueID+"');");
				ulbDropDown.Attributes.Add("ondblclick","DbComboClick('"+this.UniqueID+"');");
				ulbSearchButton.Attributes.Add("onclick","DbComboSendRequestButton('"+this.UniqueID+"');");
				
				ulbTextBox.Attributes.Add(           "onblur","DbComboGenericBlurTextBox('"+this.UniqueID+"');");
				ulbButtonHolder.Attributes.Add(      "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbSearchButton.Attributes.Add(      "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbHelpButton.Attributes.Add(        "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbResultsSpan.Attributes.Add(       "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbInnerResultsSpan.Attributes.Add(  "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbDropDown.Attributes.Add(          "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbSpacer.Attributes.Add(            "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbClipper.Attributes.Add(           "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbStatusDisplaySpan.Attributes.Add( "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbStatusBarDiv.Attributes.Add(      "onblur","DbComboGenericBlur('"+this.UniqueID+"');");
				ulbMoreResultsButton.Attributes.Add( "onblur","DbComboGenericBlur('"+this.UniqueID+"');");


				ulbTextBox.Attributes.Add(         "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");//was removed in last version 09/11/06
				ulbButtonHolder.Attributes.Add(      "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbSearchButton.Attributes.Add(      "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbHelpButton.Attributes.Add(        "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbResultsSpan.Attributes.Add(       "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbInnerResultsSpan.Attributes.Add(  "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbDropDown.Attributes.Add(        "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");//was removed in last version 09/11/06
				ulbSpacer.Attributes.Add(            "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbClipper.Attributes.Add(           "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbStatusDisplaySpan.Attributes.Add( "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbStatusBarDiv.Attributes.Add(      "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");
				ulbMoreResultsButton.Attributes.Add( "onfocus","DbComboGenericFocus('"+this.UniqueID+"');");

				if (HttpContext.Current.Request.Headers["Accept-Language"]==null || HttpContext.Current.Request.Headers["Accept-Language"].Length==0)
					ulbSetLanguage.Text="true";


				
			}

			#region License string
			#region Edition string
			string ed="";
			switch (Reg.Edition)
			{
				case RegistrationDetails.EditionTypes.Free:
					ed="Free";
					break;
				case RegistrationDetails.EditionTypes.Lite:
					ed="Lite";
					break;
				case RegistrationDetails.EditionTypes.Pro:
					ed="Pro";
					break;
				default:
					break;
			}
			#endregion
			#region License string
			string lic="";
			if (Reg.UrlRestriction.Equals(RegistrationDetails.UrlRestrictionTypes.UrlRestricted))
				lic = "single site license";
			if (Reg.ServerNameRestriction.Equals(RegistrationDetails.ServerNameRestrictionTypes.ServerNameRestricted))
				lic = "single server license";
			else if (Reg.LicensedServers>0)
				lic = Reg.LicensedServers.ToString()+" server license";
			else if (Reg.LicensedServers==0 && Reg.LicenseType.Equals(RegistrationDetails.LicenseTypes.Unrestricted))
				lic = "enterprise license";

			#endregion
			string txt = "<!-- DbCombo v$1.$2.$3 - www.dbcombo.net - Copyright Cambro Limited 2002-$8 - $4 edition, $5, licensed to $6 [$7] -->";
			txt=txt.Replace("$1",Assembly.GetExecutingAssembly().GetName().Version.Major.ToString());
			txt=txt.Replace("$2",Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString());
			txt=txt.Replace("$3",Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());
			txt=txt.Replace("$4",ed);
			txt=txt.Replace("$5",lic);
			txt=txt.Replace("$6",System.Web.HttpUtility.HtmlEncode(Reg.Organisation));
			txt=txt.Replace("$7",Reg.PurchaseK.ToString());
			txt=txt.Replace("$8",DateTime.Now.Year.ToString());
			licenseComment.Text = txt;
			#endregion

			if (RegistrationKey=="")
				throw new Exception("Please specify a value for the RegistrationKey property.");

			if (!DesignMode)
				ulbLogoImgAndLink.Text=@"<a href=""http://www.dbcombo.net/home.aspx?campaignk=8&affpurchasek="+Reg.PurchaseK.ToString()+@""" target=""_blank"" tabindex=""-1"" onfocus=""DbComboGenericFocus('"+this.UniqueID+@"');"" onblur=""DbComboGenericBlur('"+this.UniqueID+@"');""><img tabindex=""-1"" onfocus=""DbComboGenericFocus('"+this.UniqueID+@"');"" onblur=""DbComboGenericBlur('"+this.UniqueID+@"');"" src=""";

			if (Reg.LicenseError)
			{	
				ulbTestMode1.Text  = @"<div style=""border-top:solid 1px #999999;padding:5px;padding-right:6px;text-justify:auto;text-align:justify;""><span onfocus=""DbComboGenericFocus('"+this.UniqueID+@"');"" onblur=""DbComboGenericBlur('"+this.UniqueID+@"');"" style=""width:250px;font-family:Arial, Verdana, Sans-serif;font-size:x-small;""><b>DbCombo Unlicensed Test Mode</b><br>The registration key you have provided is restricted. The current conditions do not comply with the license restriction, so DbCombo is running in test mode. This mode is <b>unlicensed</b> for production use - only use during testing and development is alowed.<br>Please see <a href=""http://www.dbcombo.net/"" onfocus=""DbComboGenericFocus('"+this.UniqueID+@"');"" onblur=""DbComboGenericBlur('"+this.UniqueID+@"');"" target=""_blank"">www.dbcombo.net</a> for a valid registration key or contact David Brophy at <a href=""mailto:support@dbcombo.net"" onfocus=""DbComboGenericFocus('"+this.UniqueID+@"');"" onblur=""DbComboGenericBlur('"+this.UniqueID+@"');"">support@dbcombo.net</a>.<br>"+Reg.LicenseErrorText+@"</span></div>";
				
				string randomText = GenRandomText(10);
				SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
				byte[] hashFromRandomTextByteArray = sha.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(randomText+"kfdsajhfdsakjh432987423"));
				string hashFromRandomText = System.Text.ASCIIEncoding.ASCII.GetString(hashFromRandomTextByteArray);
                hashFromRandomText = hashFromRandomText.Substring(0,10);

				ulbTestMode4.Text  = @"<div><img src=""http://www.dbcombo.net/sites/dbcombo/t4_0.aspx?a=1&b="+Reg.PurchaseK+@"&c="+HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.Host)+@"&d="+HttpContext.Current.Server.UrlEncode(HttpContext.Current.Server.MachineName)+@"&e="+HttpContext.Current.Server.UrlEncode(randomText)+@"&f="+HttpContext.Current.Server.UrlEncode(hashFromRandomText)+@""" width=""1"" height=""1"" /></div>";
				ulbTestMode1.Visible=true;
				ulbTestMode4.Visible=true;

			}
			else
			{
				ulbTestMode1.Visible=false;
				ulbTestMode4.Visible=false;
			}

			#region ServerState serialisation and hash forming
			if (ServerState==null)
				ServerState=new Hashtable();

			string s = en(ServerState);

			if (s!="")
			{
				string stringToHash = s+ServerStateSecretString;
				SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
				byte[] hashFromClientByteArray = sha1.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(stringToHash));
				string hashFromClientString = System.Text.ASCIIEncoding.ASCII.GetString(hashFromClientByteArray);
				string hash = en(hashFromClientString);
				ulbServerState.Text = s;
				ulbServerStateHash.Text = hash;
			}
			else
			{
				ulbServerState.Text = "";
				ulbServerStateHash.Text = "";
			}
			#endregion

			#region Detect and throw an exception if pro features are used in Lite or Free mode
			if (Reg.Edition.Equals(RegistrationDetails.EditionTypes.Free) || Reg.Edition.Equals(RegistrationDetails.EditionTypes.Lite))
			{
				string exStr = "You have attempted to use a DbCombo Pro feature without a valid DbCombo Pro registration key. You have used the following feature(s), which are restricted to DbCombo Pro: $1. You may purchase a DbCombo Pro registration key from www.dbcombo.net.";
				ArrayList errors=new ArrayList();
				if (ServerAssembly!=null && !ServerAssembly.Equals(serverAssemblyDefault)) errors.Add("ServerAssembly");
				if (ServerType!=null && !ServerType.Equals(serverTypeDefault)) errors.Add("ServerType");
				if (DataValueField!=null && !DataValueField.Equals(dataValueFieldDefault)) errors.Add("DataValueField");
				if (DataTextField!=null && !DataTextField.Equals(dataTextFieldDefault)) errors.Add("DataTextField");
				if (ClientStateFunction!=null && !ClientStateFunction.Equals(clientStateFunctionDefault)) errors.Add("ClientStateFunction");
				if (ClientOnSelectFunction!=null && !ClientOnSelectFunction.Equals(clientOnSelectFunctionDefault)) errors.Add("ClientOnSelectFunction");
				if (UpLevelButtonStyle!=null && !UpLevelButtonStyle.Equals(upLevelButtonStyleDefault)) errors.Add("UpLevelButtonStyle");
				if (UpLevelMoreResultsButtonStyle!=null && !UpLevelMoreResultsButtonStyle.Equals(upLevelMoreResultsButtonStyleDefault)) errors.Add("UpLevelMoreResultsButtonStyle");
				if (DownLevelButtonStyle!=null && !DownLevelButtonStyle.Equals(downLevelButtonStyleDefault)) errors.Add("DownLevelButtonStyle");
				if (TextBoxStyle!=null && !TextBoxStyle.Equals(textBoxStyleDefault)) errors.Add("TextBoxStyle");
				if (ResultsBackgroundSpanStyle!=null && !ResultsBackgroundSpanStyle.Equals(resultsBackgroundSpanStyleDefault)) errors.Add("ResultsBackgroundSpanStyle");
				if (ResultsSelectBoxStyle!=null && !ResultsSelectBoxStyle.Equals(resultsSelectBoxStyleDefault)) errors.Add("ResultsSelectBoxStyle");
				if (StatusMessageStyle!=null && !StatusMessageStyle.Equals(statusMessageStyleDefault)) errors.Add("StatusMessageStyle");
				if (MoreResultsBarStyle!=null && !MoreResultsBarStyle.Equals(moreResultsBarStyleDefault)) errors.Add("MoreResultsBarStyle");
				if (MainSpanStyle!=null && !MainSpanStyle.Equals(mainSpanStyleDefault)) errors.Add("MainSpanStyle");
				if (UpLevelButtonClass!=null && !UpLevelButtonClass.Equals("")) errors.Add("UpLevelButtonClass");
				if (UpLevelMoreResultsButtonClass!=null && !UpLevelMoreResultsButtonClass.Equals("")) errors.Add("UpLevelMoreResultsButtonClass");
				if (DownLevelButtonClass!=null && !DownLevelButtonClass.Equals("")) errors.Add("DownLevelButtonClass");
				if (TextBoxClass!=null && !TextBoxClass.Equals("")) errors.Add("TextBoxClass");
				if (ResultsBackgroundSpanClass!=null && !ResultsBackgroundSpanClass.Equals("")) errors.Add("ResultsBackgroundSpanClass");
				if (ResultsSelectBoxClass!=null && !ResultsSelectBoxClass.Equals("")) errors.Add("ResultsSelectBoxClass");
				if (StatusMessageClass!=null && !StatusMessageClass.Equals("")) errors.Add("StatusMessageClass");
				if (MoreResultsBarClass!=null && !MoreResultsBarClass.Equals("")) errors.Add("MoreResultsBarClass");
				if (MainSpanClass!=null && !MainSpanClass.Equals("")) errors.Add("MainSpanClass");
				if (TextClearButton!=null && !TextClearButton.Equals(textClearButtonDefault)) errors.Add("TextClearButton");
				if (TextDownLevelSearchButton!=null && !TextDownLevelSearchButton.Equals(textDownLevelSearchButtonDefault)) errors.Add("TextDownLevelSearchButton");
				if (TextUpLevelSearchButton!=null && !TextUpLevelSearchButton.Equals(textUpLevelSearchButton)) errors.Add("TextUpLevelSearchButton");
				if (TextLoading!=null && !TextLoading.Equals(textLoadingDefault)) errors.Add("TextLoading");
				if (TextNoResults!=null && !TextNoResults.Equals(textNoResultsDefault)) errors.Add("TextNoResults");
				if (TextMoreButton!=null && !TextMoreButton.Equals(textMoreButtonDefault)) errors.Add("TextMoreButton");
				if (TextSelectButton!=null && !TextSelectButton.Equals(textSelectButtonDefault)) errors.Add("TextSelectButton");
				if (TextHelpButton!=null && !TextHelpButton.Equals(textHelpButtonDefault)) errors.Add("TextHelpButton");
				//				if (TextRevertButton!=null && !TextRevertButton.Equals(textRevertButtonDefault)) errors.Add("TextRevertButton");
				if (TextUpLevelHelpBox!=null && !TextUpLevelHelpBox.Equals(textUpLevelHelpBox)) errors.Add("TextUpLevelHelpBox");
				if (TextDownLevelHelpBox!=null && !TextDownLevelHelpBox.Equals(textDownLevelHelpBox)) errors.Add("TextDownLevelHelpBox");
				//				if (TextRevertBox!=null && !TextRevertBox.Equals(textRevertBox)) errors.Add("TextRevertBox");
				if (!ShowUpLevelHelpButton.Equals(showUpLevelHelpButtonDefault)) errors.Add("ShowUpLevelHelpButton");
				if (!ShowDownLevelHelpButton.Equals(showDownLevelHelpButtonDefault)) errors.Add("ShowDownLevelHelpButton");
				//				if (!ShowRevertButton.Equals(showRevertButtonDefault)) errors.Add("ShowRevertButton");
				if (!ShowUpLevelSearchButton.Equals(showUpLevelSearchButtonDefault)) errors.Add("ShowUpLevelSearchButton");
				if (ServerState!=null && ServerState.Count>0) errors.Add("ServerState");
				if (this.AutoPostBack!=autoPostBackDefault) errors.Add("AutoPostBack");
				//if (this.AutoQueryOnLoad!=autoQueryOnLoadDefault ) errors.Add("AutoQueryOnLoad");
				//if (this.ReQueryOnLoad!=reQueryOnLoadDefault ) errors.Add("ReQueryOnLoad");
				if (this.ReQueryDisabled!=reQueryDisabledDefault ) errors.Add("ReQueryDisabled");
				if (this.MaxLength!=maxLengthDefault ) errors.Add("MaxLength");
				//if (this.ReQueryRecords!=reQueryRecordsDefault ) errors.Add("ReQueryRecords");
				//if (this.ReQueryText!=req ) errors.Add("ReQueryText");
				if (this.TabIndex!=tabIndexDefault ) errors.Add("TabIndex");
				if (this.ClearQueryOnUpLevelSearchButton != clearQueryOnUpLevelSearchButtonDefault ) errors.Add("ClearQueryOnUpLevelSearchButton");
				if (this.HideIntersectingSelectTags!=hideIntersectingSelectTagsDefault ) errors.Add("HideIntersectingSelectTags");
				if (this.HideAllSelectTags!=hideAllSelectTagsDefault)errors.Add("HideAllSelectTags");
				if (this.TabIndex!=tabIndexDefault ) errors.Add("TabIndex");
				if (this.ResultsSpanTweakX!=resultsSpanTweakXDefault) errors.Add("ResultsSpanTweakX");
				if (this.ResultsSpanTweakY!=resultsSpanTweakYDefault) errors.Add("ResultsSpanTweakY");
				if (this.SelectSingleItemOnEnter!=selectSingleItemOnEnterDefault)errors.Add("SelectSingleItemOnEnter");
				if (this.SelectSingleItemOnTab!=selectSingleItemOnTabDefault)errors.Add("SelectSingleItemOnTab");
				if (this.TabToNextFieldOnEnter!=tabToNextFieldOnEnterDefault)errors.Add("TabToNextFieldOnEnter");
				if (! this.ErrorBoxType.Equals(errorBoxTypeDefault))errors.Add("ErrorBoxType");
				if (this.ErrorBoxCustomText!=errorBoxCustomTextDefault)errors.Add("ErrorBoxCustomText");
				if (this.DataMember!=dataMemberDefault) errors.Add("DataMember");
				if (this.CloseResultsOnBlur != closeResultsOnBlurDefault) errors.Add("CloseResultsOnBlur");
				if (this.CloseResultsOnClick != closeResultsOnClickDefault) errors.Add("CloseResultsOnClick");
				if (this.CloseResultsOnEnter != closeResultsOnEnterDefault) errors.Add("CloseResultsOnEnter");
				if (this.CloseResultsOnTab != closeResultsOnTabDefault) errors.Add("CloseResultsOnTab");
				

				if (errors.Count>0)
				{
					string err="";
					for (int i=0; i<errors.Count; i++)
					{
						err+=(i!=0?", ":"")+errors[i];
					}
					
					throw new Exception(exStr.Replace("$1", err));
				}

				if (ShowDbComboLink != showDbComboLinkDefault && Reg.Edition.Equals(RegistrationDetails.EditionTypes.Free))
				{
					throw new Exception("You have attempted to disable the DbCombo.net logo link. This feature is not available in the free edition. You may purchase a DbCombo Lite or Pro registration key from www.dbcombo.net.");
				}
			}
			#endregion

			if (this.Enabled==false)
			{
				ulbHelpButton.Visible=false;
				ulbSearchButton.Visible=false;
				ulbTextBox.Attributes.Add("disabled","disabled");
			}
			else
			{
				ulbHelpButton.Visible=this.ShowUpLevelHelpButton;
				ulbSearchButton.Visible=this.ShowUpLevelSearchButton;
				ulbTextBox.Attributes.Remove("disabled");
			}

			//if (Page!=null && Page.IsPostBack &! ReQueryDisabled && this.ViewState["DbComboPreviousVisible"]!=null && (bool)this.ViewState["DbComboPreviousVisible"]==true)
				//ulbReQueryOnLoad.Text="true";
			//else
				//ulbReQueryOnLoad.Text="false";

			if (Page!=null && this.Visible==true)
				this.ViewState["DbComboPreviousVisible"]=true;


			ulbJavaScriptInit.Text = @"var DbCombo_Load_"+this.UniqueID.Replace(":","_")+@"_tmp = window.onload;
function DbCombo_Load_"+this.UniqueID.Replace(":","_")+@"(){
if(DbCombo_Load_"+this.UniqueID.Replace(":","_")+@"_tmp)
	{DbCombo_Load_"+this.UniqueID.Replace(":","_")+@"_tmp()}
	DbComboLoad('"+this.UniqueID+@"');
}window.onload=DbCombo_Load_"+this.UniqueID.Replace(":","_")+";"+@"
DbComboInit(this_c);
}
else
{
if(confirm('DbCombo error: Scripting disabled or support file missing.\n\nIf you are an end-user, a personal firewall or privacy application may cause this error.\n\nClick OK to revert to non-scripted (HTML 3.2 only) functionality.\nClick Cancel to ignore the error - DbCombo will be disabled.\n\nYou may wish to configure your personal firewall or privacy application to allow scripting, and retry.\n\nNote to developers - this message can also be caused by the DbComboServer.aspx file being ommitted.\nPlease read the DbCombo documentation if this is the case.')){__doPostBack('"+this.UniqueID+"','revert');}else{document.all['"+this.UniqueID+@"'].innerHTML='[DbCombo disabled: <a href=\'#\' onclick=\'__doPostBack("""+this.UniqueID+@""","""");return false;\'>retry</a> or <a href=\'#\' onclick=\'__doPostBack("""+this.UniqueID+@""",""revert"");return false;\'>revert to HTML 3.2 only</a>.]';}};";
		}
		#endregion

		#region RaisePostDataChangedEvent
		/// <summary>
		/// RaisePostDataChangedEvent - this raises the SelectedItemChanged event if either the Text or Value properties have changed.
		/// </summary>
		public void RaisePostDataChangedEvent()
		{
			if (SelectedItemChanged != null)
				SelectedItemChanged(this,EventArgs.Empty);
		}
		#endregion
		#region LoadPostData
		/// <summary>
		/// 
		/// </summary>
		/// <param name="postDataKey"></param>
		/// <param name="postCollection"></param>
		/// <returns></returns>
		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			string PrevText = "";
			string PrevValue = "";
			string CurrentText="";
			string CurrentValue="";
			
			if (this.ViewState["PrevText"]!=null)
				PrevText = this.ViewState["PrevText"].ToString();
			if (this.ViewState["PrevValue"]!=null)
				PrevValue = this.ViewState["PrevValue"].ToString();

			if (postCollection[postDataKey+"$ulbTextBox"]!=null)
				CurrentText=postCollection[postDataKey+"$ulbTextBox"];
			if (postCollection[postDataKey+"$ulbValueHidden"]!=null)
				CurrentValue=postCollection[postDataKey+"$ulbValueHidden"];

			if ( PrevText!=CurrentText || PrevValue!=CurrentValue )
				return true;
			else
				return false;
		}
		#endregion
		#endregion

		#region RegistrationDetails Reg
		RegistrationDetails Reg
		{
			get
			{
				if (registrationKey=="")
					throw new Exception("You have not entered a registration key. If you do not have a registration key, please go to http://www.dbcombo.net/purchases.aspx to get one. You must include your registration key in the RegistrationKey property of the DbCombo tag.");
				if (reg==null || reg.RegistrationKey!= registrationKey)
					reg = new RegistrationDetails(registrationKey);
				return reg;
			}
		}
		RegistrationDetails reg;
		#endregion

		#region DesignMode
		/// <summary>
		/// Used internally by design-time tools.
		/// </summary>
		[
		Browsable(false)
		]
		public bool DesignMode
		{
			get
			{
				if (HttpContext.Current==null)
					return true;
				else
					return false;
			}
		}
		#endregion

		#region Define and create child controls
		
		#region Define up-level controls
		HtmlGenericControl ulbHolder;
		Label ulbButtonHolder, ulbSpacer, ulbClipper;
		TextBox ulbTextBox;
		HtmlInputButton ulbMoreResultsButton;
		HtmlInputHidden ulbQueryHidden, ulbReQueryRecordsHidden, ulbValueHidden, ulbReQueryOnLoad;
		HtmlGenericControl ulbStatusDisplaySpan, ulbStatusBarDiv, ulbResultsSpan, ulbHelpButton, ulbInnerResultsSpan;
		internal HtmlGenericControl ulbNewVersionDiv;
		DropDownList ulbDropDown;
		HtmlButton ulbSearchButton;
		PlaceHolder ulbLogoPlaceHolder;
		LiteralControl licenseComment, ulbServerDirLogo, ulbClientStateFunction, ulbClientOnSelectFunction, ulbUniqueId, ulbDebug, ulbLatency, 
			ulbServerAssembly, ulbServerType, ulbServerMethod, ulbDataMember, ulbDropDownRows, ulbServerDir, 
			ulbParentAssembly, ulbParentType, ulbParentBaseAssembly, ulbParentBaseType, 
			ulbPageAssembly,   ulbPageType,   ulbPageBaseAssembly,   ulbPageBaseType, 
			ulbTextLoading, ulbTextNoResults, ulbShowDbComboLink, ulbResultsSpanTweakX, 
			ulbResultsSpanTweakY, ulbHideIntersectingSelectTags, ulbHideAllSelectTags, ulbServerState, 
			ulbServerStateHash, ulbLogoImgAndLink, ulbCloseResultsOnBlur, ulbCloseResultsOnClick, ulbCloseResultsOnEnter, ulbCloseResultsOnTab,
			ulbClearQueryOnUpLevelSearchButton, ulbAutoPostBack, ulbReQueryDisabled, 
			ulbDataValueField, ulbDataTextField,
			ulbSelectSingleItemOnEnter, ulbSelectSingleItemOnTab, ulbTabToNextFieldOnEnter,
			ulbErrorBoxType, ulbErrorBoxCustomText, ulbTestMode1, ulbTestMode4, ulbJavaScriptInit, ulbInvertArrow, ulbSetLanguage;
		//ulbAutoQueryOnLoad, 
		#endregion

		#region Define down-level controls
		Label dlbHolder, dlbNoResultsLabel, dlbResultsHolder;
		TextBox dlbTextBox;
		HtmlInputButton dlbSearchButton, dlbClearButton, dlbSelectButton, dlbMoreResultsButton;
		HtmlGenericControl dlbHelpButton;
		LiteralControl dlbHelpButtonSpacer;
		ListBox dlbListBox;
		HtmlInputHidden dlbValueHidden, dlbRowsHidden;
		#endregion

		#region CreateChildControls
		/// <summary>
		/// Creates all chile controls
		/// </summary>
		protected override void CreateChildControls() 
		{
			
			#region Add our ulb controls
			licenseComment = new LiteralControl("");
			Controls.Add(licenseComment);
			
			ulbHolder = new HtmlGenericControl("nobr");
			ulbHolder.ID="ulbHolder";

			#region ulbTextBox
			ulbTextBox = new TextBox();
			ulbTextBox.ID="ulbTextBox";
			ulbTextBox.Attributes.Add("autocomplete","off");
			ulbTextBox.Columns=textBoxColumnsDefault;
			//ulbTextBox.Attributes.Add("size",textBoxColumnsDefault.ToString());
			ulbTextBox.Attributes.Add("style",textBoxStyleDefault);
			ulbTextBox.Text="";
			ulbHolder.Controls.Add(ulbTextBox);
			#endregion

			#region ulbButtonHolder inc ulbSearchButton, ulbHelpButton
			ulbButtonHolder = new Label();
			//if (!DesignMode)
			//{
			//	if (HttpContext.Current.Request.UserAgent.IndexOf("Windows NT 5.1")>-1)
			//		ulbButtonHolder.Attributes.Add("style","position:relative;left:-2;top:1;z-index:500;");
			//	else
			//		ulbButtonHolder.Attributes.Add("style","position:relative;left:-2;z-index:500;");
			//}

			#region ulbSearchButton
			ulbSearchButton = new HtmlButton();
			ulbSearchButton.InnerHtml="<img src=\""+serverDirDefault+"DbComboServer.aspx?DownArrow\" style=\"vertical-align:middle;\" />";
			ulbSearchButton.Attributes.Add("style",upLevelButtonStyleDefault);
			ulbSearchButton.Visible=showUpLevelSearchButtonDefault;
			ulbSearchButton.ID="ulbSearchButton";
			ulbSearchButton.Attributes.Add("tabindex","-1");
			ulbButtonHolder.Controls.Add(ulbSearchButton);
			#endregion

			#region ulbClearButton (removed)
			//	ulbClearButton = new HtmlInputButton();
			//	ulbClearButton.Value=textClearButtonDefault;
			//	ulbClearButton.Attributes.Add("style",upLevelButtonStyleDefault);
			//	ulbClearButton.Visible=false;
			//	ulbClearButton.ID="ulbClearButton";
			//	ulbButtonHolder.Controls.Add(ulbClearButton);
			#endregion
			
			#region ulbHelpButton
			ulbHelpButton = new HtmlGenericControl("input");
			ulbHelpButton.Attributes.Add("type", "button");
			ulbHelpButton.Attributes.Add("value", textHelpButtonDefault);
			ulbHelpButton.Attributes.Add("style", upLevelButtonStyleDefault);
			ulbHelpButton.Attributes.Add("onclick", "alert('"+textUpLevelHelpBox+"');return false;");
			ulbHelpButton.Visible=showUpLevelHelpButtonDefault;
			ulbHelpButton.ID="ulbHelpButton";
			ulbButtonHolder.Controls.Add(ulbHelpButton);
			#endregion
			
			#region ulbRevertButton (removed)
			//	ulbRevertButton = new HtmlGenericControl("input");
			//	ulbRevertButton.Attributes.Add("type",  "button");
			//	ulbRevertButton.Attributes.Add("style", upLevelButtonStyleDefault);
			//	ulbRevertButton.Attributes.Add("value", textRevertButtonDefault);
			//	ulbRevertButton.ID="ulbRevertButton";
			//	ulbRevertButton.Visible=showRevertButtonDefault;
			//	ulbButtonHolder.Controls.Add(ulbRevertButton);
			#endregion

			ulbHolder.Controls.Add(ulbButtonHolder);
			#endregion

			#region ulbQueryHidden
			ulbQueryHidden = new HtmlInputHidden();
			ulbQueryHidden.ID="ulbQueryHidden";
			ulbQueryHidden.Value="";
			ulbHolder.Controls.Add(ulbQueryHidden);
			#endregion

			#region ulbReQueryOnLoad
			ulbReQueryOnLoad = new HtmlInputHidden();
			ulbReQueryOnLoad.ID="ulbReQueryOnLoad";
			ulbReQueryOnLoad.Value=reQueryOnLoadDefault.ToString().ToLower();
			ulbHolder.Controls.Add(ulbReQueryOnLoad);
			#endregion

			#region ulbReQueryRecordsHidden
			ulbReQueryRecordsHidden = new HtmlInputHidden();
			ulbReQueryRecordsHidden.ID="ulbReQueryRecordsHidden";
			ulbReQueryRecordsHidden.Value=reQueryRecordsDefault.ToString();
			ulbHolder.Controls.Add(ulbReQueryRecordsHidden);
			#endregion

			#region ulbValueHidden
			ulbValueHidden = new HtmlInputHidden();
			ulbValueHidden.ID="ulbValueHidden";
			ulbValueHidden.Value="";
			ulbHolder.Controls.Add(ulbValueHidden);
			#endregion

			if (DesignMode)
				ulbHolder.Controls.Add(new LiteralControl("<br>"));

			#region ulbResultsSpan - inc. ulbDropDown, ulbMoreResultsButton and ulbStatusDisplaySpan
			ulbResultsSpan = new HtmlGenericControl("div");
			ulbResultsSpan.ID="ulbResultsSpan";
			ulbResultsSpan.Attributes.Add("style",resultsBackgroundSpanStyleDefault);

			ulbInnerResultsSpan = new HtmlGenericControl("span");
			ulbInnerResultsSpan.Attributes.Add("style",resultsInnerSpanStyleDefault);

			#region ulbDropDown
			ulbDropDown = new DropDownList();
			ulbDropDown.ID="ulbDropDown";
			ulbDropDown.Attributes.Add("size",dropDownRowsDefault.ToString());
			//ulbDropDown.Attributes.Add("multiple","true");
			ulbDropDown.Attributes.Add("tabindex","-1");
			if (DesignMode)
			{
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 1","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 2","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 3","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 4","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 5","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 6","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 7","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 8","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 9","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 10","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 11","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 12","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 13","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 14","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 15","1"));
				ulbDropDown.Items.Add(new ListItem("Drop-down test item 16","1"));
			}
			ulbSpacer = new Label();
			ulbSpacer.ID="ulbSpacer";
			ulbClipper = new Label();
			ulbClipper.ID="ulbClipper";
			if (DesignMode)
				ulbClipper.Attributes.Add("style","");
			else
				ulbClipper.Attributes.Add("style","overflow:hidden;position:absolute;clip:rect(5px 50px 50px 5px);top:-3;left:-3;");
			ulbClipper.Controls.Add(ulbDropDown);
			ulbSpacer.Controls.Add(ulbClipper);
			ulbInnerResultsSpan.Controls.Add(ulbSpacer);
			ulbResultsSpan.Controls.Add(ulbInnerResultsSpan);
			#endregion

			#region ulbStatusDisplaySpan
			ulbStatusDisplaySpan = new HtmlGenericControl("div");
			ulbStatusDisplaySpan.ID="ulbStatusDisplaySpan";
			ulbStatusDisplaySpan.Attributes.Add("style",statusMessageStyleDefault);
			if (DesignMode)
			{
				ulbStatusDisplaySpan.InnerText="Status Message";
			}
			ulbResultsSpan.Controls.Add(ulbStatusDisplaySpan);
			#endregion

			ulbStatusBarDiv = new HtmlGenericControl("div");
			ulbStatusBarDiv.Attributes.Add("style",moreResultsBarStyleDefault);
			ulbStatusBarDiv.ID="ulbStatusBar";

			ulbStatusBarDiv.Controls.Add(new LiteralControl("<nobr>"));

			#region ulbMoreResultsButton
			ulbMoreResultsButton = new HtmlInputButton();
			ulbMoreResultsButton.ID="ulbMoreResultsButton";
			ulbMoreResultsButton.Attributes.Add("style",upLevelMoreResultsButtonStyleDefault);
			ulbMoreResultsButton.Value=textMoreButtonDefault;
			ulbMoreResultsButton.Attributes.Add("tabindex","-1");
			ulbStatusBarDiv.Controls.Add(ulbMoreResultsButton);
			#endregion
            
			#region ulbLogoPlaceHolder
			ulbLogoPlaceHolder = new PlaceHolder();
			ulbLogoPlaceHolder.Visible = showDbComboLinkDefault;
			ulbServerDirLogo = new LiteralControl(serverDirDefault);
			ulbLogoImgAndLink = new LiteralControl(@"<a href=""http://www.dbcombo.net/home.aspx?campaignk=8&affpurchasek=0"" target=""_blank"" tabindex=""-1""><img src=""");
			ulbLogoPlaceHolder.Controls.Add(ulbLogoImgAndLink);
			ulbLogoPlaceHolder.Controls.Add(ulbServerDirLogo);
			ulbLogoPlaceHolder.Controls.Add(new LiteralControl("DbComboServer.aspx?f\" hspace=\"2\" vspace=\"2\" border=\"0\" style=\"vertical-align:bottom;\" alt=\"This is DbCombo.net. The items you see in the drop-down are delivered from the server in real-time. Click to find out more.\" /></a>"));
			//DbComboServer.aspx?f" vspace="2" hspace="2" border="0" style="vertical-align:bottom;" alt="This is DbCombo.net. The items you see in the drop-down are delivered from the server in real-time. Click to find out more."></a>
			ulbStatusBarDiv.Controls.Add(ulbLogoPlaceHolder);
			#endregion

			ulbStatusBarDiv.Controls.Add(new LiteralControl("</nobr>"));

			ulbResultsSpan.Controls.Add(ulbStatusBarDiv);

			ulbNewVersionDiv = new HtmlGenericControl("div");
			ulbNewVersionDiv.Attributes["style"]="background-color:red;color:white;font-weight:bold;padding:3px;font-family:Arial, Verdana, Sans-serif;font-size:x-small;";
			ulbNewVersionDiv.Visible=false;
			ulbResultsSpan.Controls.Add(ulbNewVersionDiv);

			ulbTestMode1 = new LiteralControl("");
			ulbResultsSpan.Controls.Add(ulbTestMode1);
			
			ulbHolder.Controls.Add(ulbResultsSpan);

			//string testMode4 = @"<div style=""""><img src=""http://north.cambro.net/allasp/sites/dbcombo/t3_0.aspx?a=1"" width=""1"" height=""1"" /></div>";
			ulbTestMode4 = new LiteralControl("");
			ulbHolder.Controls.Add(ulbTestMode4);

			if (DesignMode)
				if (DisplayResultsInDesigner)
					ulbResultsSpan.Visible=true;
				else
					ulbResultsSpan.Visible=false;

			#endregion

			#region initialise our LiteralControls
			ulbUniqueId					= new LiteralControl("");
			ulbDebug					= new LiteralControl(debugDefault.ToString().ToLower());
			ulbSetLanguage				= new LiteralControl("false");
			ulbSelectSingleItemOnEnter	= new LiteralControl(selectSingleItemOnEnterDefault.ToString().ToLower());
			ulbSelectSingleItemOnTab	= new LiteralControl(selectSingleItemOnTabDefault.ToString().ToLower());
			ulbTabToNextFieldOnEnter	= new LiteralControl(tabToNextFieldOnEnterDefault.ToString().ToLower());
			ulbAutoPostBack				= new LiteralControl(autoPostBackDefault.ToString().ToLower());
			//ulbAutoQueryOnLoad			= new LiteralControl(autoQueryOnLoadDefault.ToString().ToLower());
			//ulbReQueryOnLoad			= new LiteralControl(reQueryOnLoadDefault.ToString().ToLower());
			ulbReQueryDisabled			= new LiteralControl(reQueryDisabledDefault.ToString().ToLower());
			ulbShowDbComboLink			= new LiteralControl(showDbComboLinkDefault.ToString().ToLower());
			ulbServerAssembly			= new LiteralControl(serverAssemblyDefault);
			ulbServerType				= new LiteralControl(serverTypeDefault);
			ulbServerMethod				= new LiteralControl(serverMethodDefault);
			ulbDataMember				= new LiteralControl(dataMemberDefault);
			ulbDropDownRows				= new LiteralControl(dropDownRowsDefault.ToString());
			ulbServerDir				= new LiteralControl(serverDirDefault);
			ulbErrorBoxType				= new LiteralControl(errorBoxTypeDefault.ToString());
			ulbErrorBoxCustomText		= new LiteralControl(errorBoxCustomTextDefault.ToString());

			ulbParentAssembly			= new LiteralControl("");
			ulbParentType				= new LiteralControl("");
			ulbParentBaseAssembly		= new LiteralControl("");
			ulbParentBaseType			= new LiteralControl("");

			ulbDataValueField			= new LiteralControl(dataValueFieldDefault);
			ulbDataTextField			= new LiteralControl(dataTextFieldDefault);

			ulbPageAssembly				= new LiteralControl("");
			ulbPageType					= new LiteralControl("");
			ulbPageBaseAssembly			= new LiteralControl("");
			ulbPageBaseType				= new LiteralControl("");

			ulbHideIntersectingSelectTags = new LiteralControl(hideIntersectingSelectTagsDefault.ToString().ToLower());
			ulbHideAllSelectTags		= new LiteralControl(hideAllSelectTagsDefault.ToString().ToLower());

			ulbResultsSpanTweakX		= new LiteralControl(resultsSpanTweakXDefault.ToString());
			ulbResultsSpanTweakY		= new LiteralControl(resultsSpanTweakYDefault.ToString());

			ulbTextLoading				= new LiteralControl(textLoadingDefault);
			ulbTextNoResults			= new LiteralControl(textNoResultsDefault);
			ulbLatency					= new LiteralControl(latencyDefault.ToString());
			ulbClientStateFunction		= new LiteralControl(clientStateFunctionDefault);
			ulbClientOnSelectFunction	= new LiteralControl(clientOnSelectFunctionDefault);

			ulbServerState				= new LiteralControl("");
			ulbServerStateHash			= new LiteralControl("");

			ulbCloseResultsOnBlur		= new LiteralControl(closeResultsOnBlurDefault.ToString().ToLower());
			ulbCloseResultsOnClick		= new LiteralControl(closeResultsOnClickDefault.ToString().ToLower());
			ulbCloseResultsOnEnter		= new LiteralControl(closeResultsOnEnterDefault.ToString().ToLower());
			ulbCloseResultsOnTab		= new LiteralControl(closeResultsOnTabDefault.ToString().ToLower());

			ulbInvertArrow				= new LiteralControl(invertArrowDefault.ToString().ToLower());

			ulbClearQueryOnUpLevelSearchButton
				= new LiteralControl(clearQueryOnUpLevelSearchButtonDefault.ToString().ToLower());

			ulbJavaScriptInit			= new LiteralControl("");

			#endregion

			#region LiteralControls
			ulbHolder.Controls.Add(new LiteralControl(@"<script language=""javascript"">"));
			ulbHolder.Controls.Add(new LiteralControl(@"if (typeof(DbComboServerExists)!='undefined'){var this_c="""));
			ulbHolder.Controls.Add(ulbUniqueId);
			ulbHolder.Controls.Add(new LiteralControl(@""";eval(DbComboStringToEval(this_c,"));
			ulbHolder.Controls.Add(ulbDebug);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbSetLanguage);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbSelectSingleItemOnEnter);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbSelectSingleItemOnTab);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbTabToNextFieldOnEnter);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbAutoPostBack);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbReQueryDisabled);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			//ulbHolder.Controls.Add(ulbAutoQueryOnLoad);
			//ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbClearQueryOnUpLevelSearchButton);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbCloseResultsOnBlur);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbCloseResultsOnClick);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbCloseResultsOnEnter);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbCloseResultsOnTab);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbInvertArrow);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbHideIntersectingSelectTags);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbHideAllSelectTags);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbShowDbComboLink);
			ulbHolder.Controls.Add(new LiteralControl(@","""));
			ulbHolder.Controls.Add(ulbServerAssembly);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbServerType);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbServerMethod);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbDataMember);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbDataValueField);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbDataTextField);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbErrorBoxType);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbErrorBoxCustomText);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));

			ulbHolder.Controls.Add(ulbParentAssembly);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbParentType);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbParentBaseAssembly);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbParentBaseType);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));

			ulbHolder.Controls.Add(ulbPageAssembly);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbPageType);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbPageBaseAssembly);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbPageBaseType);
			ulbHolder.Controls.Add(new LiteralControl(@""","));

			ulbHolder.Controls.Add(ulbResultsSpanTweakX);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbResultsSpanTweakY);
			ulbHolder.Controls.Add(new LiteralControl(@","""));

			ulbHolder.Controls.Add(ulbTextLoading);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbTextNoResults);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbClientStateFunction);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbClientOnSelectFunction);
			ulbHolder.Controls.Add(new LiteralControl(@""","));
			ulbHolder.Controls.Add(ulbDropDownRows);
			ulbHolder.Controls.Add(new LiteralControl(@","));
			ulbHolder.Controls.Add(ulbLatency);
			ulbHolder.Controls.Add(new LiteralControl(@","""));
			ulbHolder.Controls.Add(ulbServerDir);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbServerState);
			ulbHolder.Controls.Add(new LiteralControl(@""","""));
			ulbHolder.Controls.Add(ulbServerStateHash);
			ulbHolder.Controls.Add(new LiteralControl(@"""));"));
			ulbHolder.Controls.Add(ulbJavaScriptInit);
			ulbHolder.Controls.Add(new LiteralControl(@"</script>"));
			#endregion

			Controls.Add(ulbHolder);
			#endregion

			#region Add our dlb controls
			dlbHolder = new Label();
			dlbHolder.Visible=false;

			#region dlbTextBox
			dlbTextBox = new TextBox();
			dlbTextBox.Text="";
			dlbTextBox.ID="dlbTextBox";
			dlbTextBox.Attributes.Add("size",textBoxColumnsDefault.ToString());
			dlbTextBox.Attributes.Add("autocomplete","off");
			dlbHolder.Controls.Add(dlbTextBox);
			dlbHolder.Controls.Add(new LiteralControl("&nbsp;"));
			#endregion

			#region dlbSearchButton
			dlbSearchButton = new HtmlInputButton();
			dlbSearchButton.ServerClick+=new EventHandler(dlbSearchButtonClick);
			dlbSearchButton.Value=textDownLevelSearchButtonDefault;
			dlbSearchButton.CausesValidation=false;
			dlbSearchButton.ID="dlbSearchButton";
			dlbSearchButton.Attributes["style"]=downLevelButtonStyleDefault;
			dlbHolder.Controls.Add(dlbSearchButton);
			dlbHolder.Controls.Add(new LiteralControl("&nbsp;"));
			#endregion

			#region dlbClearButton
			dlbClearButton = new HtmlInputButton();
			dlbClearButton.Value=textClearButtonDefault;
			dlbClearButton.ID= "dlbClearButton";
			dlbClearButton.CausesValidation=false;
			dlbClearButton.ServerClick+=new EventHandler(ClearValue);
			dlbHolder.Controls.Add(dlbClearButton);
			#endregion

			#region dlbHelpButton and dlbHelpButtonSpacer
			dlbHelpButtonSpacer = new LiteralControl("&nbsp;");
			dlbHolder.Controls.Add(dlbHelpButtonSpacer);
			dlbHelpButton = new HtmlGenericControl("input");
			dlbHelpButton.Attributes.Add("type", "button");
			dlbHelpButton.Visible = showDownLevelHelpButtonDefault;
			dlbHelpButton.Attributes.Add("value", textHelpButtonDefault);
			dlbHelpButton.Attributes.Add("onclick","alert('"+textDownLevelHelpBox+"');return false;");
			dlbHolder.Controls.Add(dlbHelpButton);
			#endregion
			
			#region dlbNoResultsLabel
			dlbHolder.Controls.Add(new LiteralControl("&nbsp;"));
			dlbNoResultsLabel = new Label();
			dlbNoResultsLabel.Text=textNoResultsDefault;
			dlbNoResultsLabel.ID="dlbNoResultsLabel";
			dlbNoResultsLabel.Visible=false;
			dlbHolder.Controls.Add(dlbNoResultsLabel);
			dlbHolder.Controls.Add(new LiteralControl("<br>"));
			#endregion

			#region dlbResultsHolder - inc. dlbListBox, dlbSelectButton, dlbMoreResultsButton
			dlbResultsHolder = new Label();

			#region dlbListBox
			dlbListBox = new ListBox();
			dlbListBox.ID="dlbListBox";
			dlbListBox.Rows=dropDownRowsDefault;
			dlbResultsHolder.Visible=false;
			dlbResultsHolder.Controls.Add(dlbListBox);
			dlbResultsHolder.Controls.Add(new LiteralControl("<br>"));
			#endregion

			#region dlbSelectButton
			dlbSelectButton = new HtmlInputButton();
			dlbSelectButton.ID="dlbSelectButton";
			dlbSelectButton.ServerClick+=new EventHandler(dlbSelectButtonClick);
			dlbSelectButton.Value=textSelectButtonDefault;
			dlbSelectButton.CausesValidation=false;
			dlbSelectButton.Visible=false;
			dlbResultsHolder.Controls.Add(dlbSelectButton);
			dlbResultsHolder.Controls.Add(new LiteralControl("&nbsp;"));
			#endregion

			#region dlbMoreResultsButton
			dlbMoreResultsButton = new HtmlInputButton();
			dlbMoreResultsButton.ID="dlbMoreResultsButton";
			dlbMoreResultsButton.ServerClick+=new EventHandler(dlbMoreResultsButtonClick);
			dlbMoreResultsButton.Value=textMoreButtonDefault;
			dlbMoreResultsButton.CausesValidation=false;
			dlbMoreResultsButton.Visible=false;
			dlbResultsHolder.Controls.Add(dlbMoreResultsButton);
			#endregion

			dlbHolder.Controls.Add(dlbResultsHolder);
			#endregion

			#region dlbValueHidden
			dlbValueHidden = new HtmlInputHidden();
			dlbValueHidden.ID="dlbValueHidden";
			dlbHolder.Controls.Add(dlbValueHidden);
			#endregion

			#region dlbRowsHidden
			dlbRowsHidden = new HtmlInputHidden();
			dlbRowsHidden.ID="dlbRowsHidden";
			dlbHolder.Controls.Add(dlbRowsHidden);
			#endregion
			
			Controls.Add(dlbHolder);
			#endregion

		}
		#endregion

		#endregion

		#region Public properties
		
		#region ForceDownLevel
		/// <summary>
		/// This forces the control to operate in downlevel browser mode (html 3.2 only)
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(false),
		Description("This forces the control to operate in downlevel browser mode (html 3.2 only)"),
		RefreshProperties(RefreshProperties.All)
		]
		public bool ForceDownLevel
		{
			get
			{
				this.EnsureChildControls();
				return forceDownLevel;
			}
			set
			{
				this.EnsureChildControls();
				forceDownLevel=value;
				ulbHolder.Visible=!DownLevel;
				dlbHolder.Visible=DownLevel;
			}
		}
		private bool forceDownLevel=false;
		private bool forceDownLevelDefault=false;
		#endregion

		#region RegistrationKey
		/// <summary>
		/// This is the registration key - If you do not have one, please go to www.dbcombo.net.
		/// </summary>
		[
		Bindable(false),
		Category("Misc"),
		DefaultValue("aeaaaaU99999baaaaaaaaaEbbaaaaQwm6rdDVULn6ndDZULm6fdDQ70yFVg-TnhB6ruzKfwBQr1xWv0zNnhBWfgBN7K~DRwz3VLmUaHm6FdDWqdDUUxm6fdDVUxlZqulVadnFZGnUYwl0atvDnxuRiHnpBxlUItoRiHnpZcmcZWmcR"),
		Description("This is the registration key - If you do not have one, please go to www.dbcombo.net."),
		RefreshProperties(RefreshProperties.All)
		]
		public string RegistrationKey
		{
			set
			{
				registrationKey = value;
			}
			get
			{
				return registrationKey;
			}
		}
		string registrationKey="aeaaaaU99999baaaaaaaaaEbbaaaaQwm6rdDVULn6ndDZULm6fdDQ70yFVg-TnhB6ruzKfwBQr1xWv0zNnhBWfgBN7K~DRwz3VLmUaHm6FdDWqdDUUxm6fdDVUxlZqulVadnFZGnUYwl0atvDnxuRiHnpBxlUItoRiHnpZcmcZWmcR";
		//string registrationKey="";
		#endregion

		#region ServerAssembly
		/// <summary>
		/// This is the Assembly that the server will look in for the ServerMethod.
		/// </summary>
		[
		Bindable(true),
		Category("Data"),
		DefaultValue(""),
		Description("This is the Assembly that the server will look in for the ServerMethod.")
		]
		public string ServerAssembly
		{
			get
			{
				this.EnsureChildControls();
				return serverAssembly;
			}
			set
			{
				this.EnsureChildControls();
				
				serverAssembly=value;
				ulbServerAssembly.Text=value;
			}
		}
		private string serverAssembly="";
		private string serverAssemblyDefault="";
		#endregion
		#region ServerType
		/// <summary>
		/// This is the Type (namespace.class) that the server will look in for the ServerMethod.
		/// </summary>
		[
		Bindable(true),
		Category("Data"),
		DefaultValue(""),
		Description("This is the Type (namespace.class) that the server will look in for the ServerMethod.")
		]
		public string ServerType
		{
			get
			{
				this.EnsureChildControls();
				return serverType;
			}
			set
			{
				this.EnsureChildControls();
				
				serverType=value;
				ulbServerType.Text=value;
			}
		}
		private string serverType="";
		private string serverTypeDefault="";
		#endregion
		#region ServerMethod
		/// <summary>
		/// This is the name of the method that the server execute each time new results are needed. Default is DbComboMethod.
		/// It should have the following signature and attribute:
		/// [Cambro.Web.DbCombo.ResultsMethod(true)] public static object xxx(Cambro.Web.DbCombo.ServerMethodArgs args){}
		/// </summary>
		[
		Bindable(true),
		Category("Data"),
		DefaultValue("DbComboMethod"),
		Description("This is the name of the method that the server execute each time new results are needed. It should have the following signature and attribute: [Cambro.Web.DbCombo.ResultsMethod(true)] public static object xxx(Cambro.Web.DbCombo.ServerMethodArgs args){}")
		]
		public string ServerMethod
		{
			get
			{
				this.EnsureChildControls();
				return serverMethod;
			}
			set
			{
				this.EnsureChildControls();
				serverMethod=value;
				ulbServerMethod.Text=value;
			}
		}
		private string serverMethod="DbComboMethod";
		private string serverMethodDefault="DbComboMethod";
		#endregion
		#region DataMember
		/// <summary>
		/// Use the DataMember property to specify a member from a multimember data source. For example, if 
		/// you have a data source with more than one table, use the DataMember property to specify which 
		/// table to bind to. Leave blank for the default.
		/// Remember if you change this from it's default, you MUST change the 
		/// ServerMethodEventArgs.DataMemberSecurity property in your ServerMethod.
		/// </summary>
		[
		Bindable(true),
		Category("Data"),
		DefaultValue(""),
		Description("Use the DataMember property to specify a member from a multimember data source. For example, if you have a data source with more than one table, use the DataMember property to specify which table to bind to. Leave blank for the default.Remember if you change this from it's default, you MUST change the ServerMethodEventArgs.DataMemberSecurity property in your ServerMethod."),
		]
		public string DataMember
		{
			get
			{
				this.EnsureChildControls();
				return dataMember;
			}
			set
			{
				this.EnsureChildControls();
				dataMember=value;
				ulbDataMember.Text=value;
			}
		}
		private string dataMember="";
		private string dataMemberDefault="";
		#endregion

		#region Debug
		/// <summary>
		/// Set this to true if you are having problems. It will redirect the output of the server component to the browser window.
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(false),
		Description("Set this to true if you are having problems. It will redirect the output of the server component to the browser window.")
		]
		public bool Debug
		{
			get
			{
				this.EnsureChildControls();
				return debug;
			}
			set
			{
				this.EnsureChildControls();
				debug=value;
				ulbDebug.Text=value.ToString().ToLower();
			}
		}
		private bool debug=false;
		private bool debugDefault=false;
		#endregion

		#region DisplayResultsInDesigner
		/// <summary>
		/// Set this to true if you want to preview how DbCombo will look in the designer. Please note that due to the lack of designer JavaScript support, the edges of the drop-down will not be hidden, and the up level search button will not be resized to the height of the text box.
		/// </summary>
		[
		Category("Misc"),
		DefaultValue(false),
		Description("Set this to true if you want to preview how DbCombo will look in the designer. Please note that due to the lack of designer JavaScript support, the edges of the drop-down will not be hidden, and the up level search button will not be resized to the height of the text box."),
		RefreshProperties(RefreshProperties.All)
		]
		public bool DisplayResultsInDesigner
		{
			get
			{
				this.EnsureChildControls();
				return displayResultsInDesigner;
			}
			set
			{
				this.EnsureChildControls();
				displayResultsInDesigner=value;
				if (DesignMode)
				{
					ulbResultsSpan.Visible=value;
					if (value)
						ulbSearchButton.InnerHtml="<img src=\""+serverDirDefault+"DbComboServer.aspx?UpArrow\" style=\"vertical-align:middle;\" />";
					else
						ulbSearchButton.InnerHtml="<img src=\""+serverDirDefault+"DbComboServer.aspx?DownArrow\" style=\"vertical-align:middle;\" />";
				}
			}
		}
		private bool displayResultsInDesigner=false;
		private bool displayResultsInDesignerDefault=false;
		#endregion

		#region ValidationProperty
		/// <summary>
		/// Validation can occur on either the text of value properties of DbCombo. (Default: ValidationProperties.Value)
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(ValidationProperties.Value),
		Description("Validation can occur on either the text of value properties of DbCombo. (Default: ValidationProperties.Value)")
		]
		public ValidationProperties ValidationProperty
		{
			get
			{
				this.EnsureChildControls();
				return validationProperty;
			}
			set
			{
				this.EnsureChildControls();
				validationProperty=value;
				//ulbDebug.Text=value.ToString().ToLower();
			}
		}
		private ValidationProperties validationProperty=ValidationProperties.Value;
		private ValidationProperties validationPropertyDefalt=ValidationProperties.Value;
		#endregion
		#region ValidationPropertyValue
		/// <summary>
		/// Internally used during validation. Please ignore this property.
		/// </summary>
		[
		Browsable(false)
		]
		public string ValidationPropertyValue
		{
			get
			{
				switch(ValidationProperty)
				{
					case ValidationProperties.Value:
						return Value;
					case ValidationProperties.Text:
						return Text;
					default:
						return Value;
				}
			}
		}
		#endregion

		#region SelectSingleItemOnEnter
		/// <summary>
		/// If there is only one item in the results and enter is pressed, this item will be selected if this property is set to true. (Default: true).
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(true),
		Description("If there is only one item in the results and enter is pressed, this item will be selected if this property is set to true. (Default: true).")
		]
		public bool SelectSingleItemOnEnter
		{
			get
			{
				this.EnsureChildControls();
				return selectSingleItemOnEnter;
			}
			set
			{
				this.EnsureChildControls();
				selectSingleItemOnEnter=value;
				ulbSelectSingleItemOnEnter.Text=value.ToString().ToLower();
			}
		}
		private bool selectSingleItemOnEnter=true;
		private bool selectSingleItemOnEnterDefault=true;
		#endregion
		#region SelectSingleItemOnTab
		/// <summary>
		/// If there is only one item in the results and tab is pressed, this item will be selected if this property is set to true. (Default: true).
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(true),
		Description("If there is only one item in the results and tab is pressed, this item will be selected if this property is set to true. (Default: true).")
		]
		public bool SelectSingleItemOnTab
		{
			get
			{
				this.EnsureChildControls();
				return selectSingleItemOnTab;
			}
			set
			{
				this.EnsureChildControls();
				selectSingleItemOnTab=value;
				ulbSelectSingleItemOnTab.Text=value.ToString().ToLower();
			}
		}
		private bool selectSingleItemOnTab=true;
		private bool selectSingleItemOnTabDefault=true;
		#endregion
		#region TabToNextFieldOnEnter
		/// <summary>
		/// When enter is pressed, DbCombo will simulate tab being pressed - e.g. skip to the next field. (Default: false).
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(false),
		Description("When enter is pressed, DbCombo will simulate tab being pressed - e.g. skip to the next field. (Default: false).")
		]
		public bool TabToNextFieldOnEnter
		{
			get
			{
				this.EnsureChildControls();
				return tabToNextFieldOnEnter;
			}
			set
			{
				this.EnsureChildControls();
				tabToNextFieldOnEnter=value;
				ulbTabToNextFieldOnEnter.Text=value.ToString().ToLower();
			}
		}
		private bool tabToNextFieldOnEnter=false;
		private bool tabToNextFieldOnEnterDefault=false;
		#endregion

		#region CloseResultsOnBlur
		/// <summary>
		/// If this is set to false, the results stay visible when the DbCombo looses focus. Default is true.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(true),
		Description("If this is set to false, the results stay visible when the DbCombo looses focus. Default is true.")
		]
		public bool CloseResultsOnBlur
		{
			get
			{
				this.EnsureChildControls();
				return closeResultsOnBlur;
			}
			set
			{
				this.EnsureChildControls();
				closeResultsOnBlur=value;
				ulbCloseResultsOnBlur.Text=value.ToString().ToLower();
			}
		}
		private bool closeResultsOnBlur=true;
		private bool closeResultsOnBlurDefault=true;
		#endregion

		#region CloseResultsOnClick
		/// <summary>
		/// If this is set to false, the results stay visible when an item is clicked on. Default is true.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(true),
		Description("If this is set to false, the results stay visible when an item is clicked on. Default is true.")
		]
		public bool CloseResultsOnClick
		{
			get
			{
				this.EnsureChildControls();
				return closeResultsOnClick;
			}
			set
			{
				this.EnsureChildControls();
				closeResultsOnClick=value;
				ulbCloseResultsOnClick.Text=value.ToString().ToLower();
			}
		}
		private bool closeResultsOnClick=true;
		private bool closeResultsOnClickDefault=true;
		#endregion

		#region CloseResultsOnEnter
		/// <summary>
		/// If this is set to false, the results stay visible when enter is pressed. Default is true.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(true),
		Description("If this is set to false, the results stay visible when when enter is pressed. Default is true.")
		]
		public bool CloseResultsOnEnter
		{
			get
			{
				this.EnsureChildControls();
				return closeResultsOnEnter;
			}
			set
			{
				this.EnsureChildControls();
				closeResultsOnEnter=value;
				ulbCloseResultsOnEnter.Text=value.ToString().ToLower();
			}
		}
		private bool closeResultsOnEnter=true;
		private bool closeResultsOnEnterDefault=true;
		#endregion

		#region CloseResultsOnTab
		/// <summary>
		/// If this is set to false, the results stay visible when tab is pressed. Default is true.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(true),
		Description("If this is set to false, the results stay visible when when tab is pressed. Default is true.")
		]
		public bool CloseResultsOnTab
		{
			get
			{
				this.EnsureChildControls();
				return closeResultsOnTab;
			}
			set
			{
				this.EnsureChildControls();
				closeResultsOnTab=value;
				ulbCloseResultsOnTab.Text=value.ToString().ToLower();
			}
		}
		private bool closeResultsOnTab=true;
		private bool closeResultsOnTabDefault=true;
		#endregion

		#region InvertArrow
		/// <summary>
		/// The Visual Studio.NET 2003 built-in browser fires
		/// the blur event of the DbCombo textbox when the image 
		/// on the up-level search button is changed (from a down arrow to an up arrow). 
		/// By default DbCombo will cancel the current query if this happens. If 
		/// you specifically need DbCombo to work inside Visual Studio.NET 2003, set 
		/// this to false. The arrow image on the button will not change while the results are open. Default is true.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(true),
		Description("The Visual Studio.NET 2003 built-in browser fires the blur event of the DbCombo textbox when the image  on the up-level search button is changed (from a down arrow to an up arrow). By default DbCombo will cancel the current query if this happens. If you specifically need DbCombo to work inside Visual Studio.NET 2003, set this to false. The arrow image on the button will not change while the results are open. Default is true.")
		]
		public bool InvertArrow
		{
			get
			{
				this.EnsureChildControls();
				return invertArrow;
			}
			set
			{
				this.EnsureChildControls();
				invertArrow=value;
				ulbInvertArrow.Text=value.ToString().ToLower();
			}
		}
		private bool invertArrow=true;
		private bool invertArrowDefault=true;
		#endregion

		#region AutoPostBack
		/// <summary>
		/// If this is set to true, the control will postback when either an item is clicked on, or enter is pressed on an item. Default is false.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(false),
		Description("If this is set to true, the control will postback when either an item is clicked on, or enter is pressed on an item. Default is false.")
		]
		public bool AutoPostBack
		{
			get
			{
				this.EnsureChildControls();
				return autoPostBack;
			}
			set
			{
				this.EnsureChildControls();
				autoPostBack=value;
				ulbAutoPostBack.Text=value.ToString().ToLower();
			}
		}
		private bool autoPostBack=false;
		private bool autoPostBackDefault=false;
		#endregion

		#region HideIntersectingSelectTags
		/// <summary>
		/// Setting this to true will hide all select tags that would intersect with DbCombo. 
		/// Select tags render on top of all objects in IE, sometimes causing the more button or status message to be obscured.
		/// This function is disabled by default because the process of hiding the SELECT tags may reduce performance on complex pages.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(false),
		Description("Setting this to true will hide all select tags that would intersect with DbCombo. Select tags render on top of all objects in IE, sometimes causing the more button or status message to be obscured. This function is disabled by default because the process of hiding the SELECT tags may reduce performance on complex pages.")
		]
		public bool HideIntersectingSelectTags
		{
			get
			{
				this.EnsureChildControls();
				return hideIntersectingSelectTags;
			}
			set
			{
				this.EnsureChildControls();
				hideIntersectingSelectTags=value;
				ulbHideIntersectingSelectTags.Text=value.ToString().ToLower();
			}
		}
		private bool hideIntersectingSelectTags=false;
		private bool hideIntersectingSelectTagsDefault=false;
		#endregion
		#region HideAllSelectTags
		/// <summary>
		/// Setting this to true will hide all select tags when DbCombo opens. 
		/// Select tags render on top of all objects in IE, sometimes causing the more button or status message to be obscured.
		/// All select tags on the page will be hidden while the DbCombo drop-down remains open. 
		/// This provides far better performance than HideIntersectingSelectTags especially on large pages.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(false),
		Description("Setting this to true will hide all select tags when DbCombo opens. Select tags render on top of all objects in IE, sometimes causing the more button or status message to be obscured. All select tags on the page will be hidden while the DbCombo drop-down remains open. This provides far better performance than HideIntersectingSelectTags especially on large pages.")
		]
		public bool HideAllSelectTags
		{
			get
			{
				this.EnsureChildControls();
				return hideAllSelectTags;
			}
			set
			{
				this.EnsureChildControls();
				hideAllSelectTags=value;
				ulbHideAllSelectTags.Text=value.ToString().ToLower();
			}
		}
		private bool hideAllSelectTags=false;
		private bool hideAllSelectTagsDefault=false;
		#endregion
		
		#region ClearQueryOnUpLevelSearchButton
		/// <summary>
		/// If this is set to true, clicking on the UpLevel search button will cause the contents of the text box to be cleared.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(false),
		Description("If this is set to true, clicking on the UpLevel search button will cause the contents of the text box to be cleared.")
		]
		public bool ClearQueryOnUpLevelSearchButton
		{
			get
			{
				this.EnsureChildControls();
				return clearQueryOnUpLevelSearchButton;
			}
			set
			{
				this.EnsureChildControls();
				clearQueryOnUpLevelSearchButton=value;
				ulbClearQueryOnUpLevelSearchButton.Text=value.ToString().ToLower();
			}
		}
		private bool clearQueryOnUpLevelSearchButton=false;
		private bool clearQueryOnUpLevelSearchButtonDefault=false;
		#endregion

		#region Latency
		/// <summary>
		/// This is the number of miliseconds that DbCombo waits after the last keypress before making a query.
		/// The default of 300 ensures that a query is not fired on every keypress, when the user types several characters.
		/// A lower latency figures may be useful when server load is not important, and when internet latency is minimal (e.g. intranet useage).
		/// When Latency is set to -1, automatic searching on key presses is disabled.
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(300),
		Description("This is the number of miliseconds that DbCombo waits after the last keypress before making a query. The default of 300ms ensures that a query is not fired on every keypress, when the user types several characters. A lower latency figures may be useful when server load is not important, and when internet latency is minimal (e.g. intranet useage). When Latency is set to -1, automatic searching on key presses is disabled.")
		]
		public int Latency
		{
			get
			{
				this.EnsureChildControls();
				return latency;
			}
			set
			{
				this.EnsureChildControls();
				latency=value;
				ulbLatency.Text = value.ToString();
			}
		}
		private int latency=300;
		private int latencyDefault=300;
		#endregion

		#region ClientStateFunction
		/// <summary>
		/// This client function enables you to pass information about the pages state back to your ServerMethod. This enables you to provide different results dependant on other form elements (e.g. drop-downs etc.)
