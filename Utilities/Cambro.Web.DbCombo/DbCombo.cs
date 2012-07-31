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
		///	IMPORTANT – you must include the parenthesis after the name of your function. You may optionally include parameters to pass to the function. This function must return a javascript ‘Object’ object
		/// </summary>
		[
		Bindable(true),
		Category("Misc - Advanced"),
		DefaultValue(""),
		Description("This client function enables you to pass information about the pages state back to your ServerMethod. This enables you to provide different results dependant on other form elements (e.g. drop-downs etc.) IMPORTANT – you must include the parenthesis after the name of your function. You may optionally include parameters to pass to the function. This function must return a javascript ‘Object’ object")
		]
		public string ClientStateFunction
		{
			get
			{
				this.EnsureChildControls();
				return clientStateFunction;
			}
			set
			{
				this.EnsureChildControls();
				clientStateFunction = value;
				ulbClientStateFunction.Text = value;
			}
		}
		string clientStateFunction="";
		string clientStateFunctionDefault="";
		#endregion

		#region ServerState
		/// <summary>
		/// This server state enables you to pass information about your application state securely from the server to the ServerMethod. You should ony use this property from code (not set it in the html).
		/// </summary>
		[
		Browsable(false),
		Bindable(true),
		Description("This server state enables you to pass information about your application state securely from the server to the ServerMethod. You should ony use this property from code (not set it in the html).")
		]
		public Hashtable ServerState
		{
			get
			{
				this.EnsureChildControls();
				return serverState;
			}
			set
			{
				this.EnsureChildControls();
				serverState = value;
			}
		}
		Hashtable serverState;
		#endregion

		#region ServerStateSecretString
		/// <summary>
		/// Setting this string enables you to authenticate the ServerState Hashtable as authentic. 
		/// </summary>
		[
		Bindable(true),
		Browsable(false),
		Description("Setting this string enables you to authenticate the ServerState Hashtable as authentic. ")
		]
		public string ServerStateSecretString
		{
			get
			{
				this.EnsureChildControls();
				return serverStateSecretString;
			}
			set
			{
				this.EnsureChildControls();
				serverStateSecretString = value;
			}
		}
		string serverStateSecretString="%$#^GFH*HGFfdghSGFDfjhjhgkf87680k,juyks2qretwdxbfvcKJl?:-0[-89yruyegfsdfdsgjh8543wDFSGG";
		#endregion

		#region ClientOnSelectFunction
		/// <summary>
		/// If specified, this javascript function is executed each time a DbCombo item is selected in the drop-down. 
		/// IMPORTANT – you must NOT include the parenthesis after the name of your function. Your function must accept two strings as parameters. The first is DbCombo’s Value property; the second is the Text property. If we are unselecting from DbCombo, the value property is a blank string, and the Text property is set to whatever the textbox contains.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - Advanced"),
		DefaultValue(""),
		Description("If specified, this javascript function is executed each time a DbCombo item is selected in the drop-down. IMPORTANT – you must NOT include the parenthesis after the name of your function. Your function must accept two strings as parameters. The first is DbCombo’s Value property; the second is the Text property. If we are unselecting from DbCombo, the value property is a blank string, and the Text property is set to whatever the textbox contains.")
		]
		public string ClientOnSelectFunction
		{
			get
			{
				this.EnsureChildControls();
				return clientOnSelectFunction;
			}
			set
			{
				this.EnsureChildControls();
				clientOnSelectFunction = value;
				ulbClientOnSelectFunction.Text = value;
			}
		}
		string clientOnSelectFunction="";
		string clientOnSelectFunctionDefault="";
		#endregion

		#region ServerDir
		/// <summary>
		/// If the DbComboServer.aspx file is not in the current directory, please specify the directory here
		/// e.g. If it is in the root directory, ServerDir should be set to "/".
		/// e.g. If it is in a directory called "support" that is in the root, ServerDir should be set to "/support/".
		/// e.g. If it is in a directory called "support" that is in the current directory, ServerDir should be set to "support/".
		/// Note: If you change this value, the design-time view may incorrectly render the up-level search button and dbcombo logo.
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(""),
		Description("If the DbComboServer.aspx file is not in the current directory, please specify the directory here e.g. If it is in the root directory, ServerDir should be set to \"/\". e.g. If it is in a directory called \"support\" that is in the root, ServerDir should be set to \"/support/\". e.g. If it is in a directory called \"support\" that is in the current directory, ServerDir should be set to \"support/\". Note: If you change this value, the design-time view may incorrectly render the up-level search button and dbcombo logo.")
		]
		public string ServerDir
		{
			get
			{
				this.EnsureChildControls();
				return serverDir;
			}
			set
			{
				this.EnsureChildControls();
				if (value.EndsWith("/") || value=="")
					serverDir=value;
				else
					serverDir=value+"/";
				
				ulbServerDir.Text=serverDir;
				ulbServerDirLogo.Text=serverDir;
				if (UpLevelSearchButtonType.Equals(ButtonTypes.Graphic))
				{
					if (DesignMode)
						ulbSearchButton.InnerHtml="<img src=\""+serverDirDesignMode+"DbComboServer.aspx?DownArrow\" style=\"vertical-align:middle;\" />";
					else
						ulbSearchButton.InnerHtml="<img src=\""+serverDir+"DbComboServer.aspx?DownArrow\" style=\"vertical-align:middle;\" />";
				}
			}
		}
		private string serverDir;
		private string serverDirDefault
		{
			get
			{
				if (DesignMode)
					return serverDirDesignMode;
				else
					return "";
			}
		}
		private string serverDirDesignMode = "http://www.dbcombo.net/gfx/design-time/dbcombo-3-1-0/";
		#endregion

		#region DataValueField
		/// <summary>
		/// This is the name of the field in the dataset returned by the ServerMethod that will be used for the Value property of DbCombo. Remember if you change this from it's default value of 'DbComboValue', you MUST change the ServerMethodEventArgs.FieldSecurity property in your ServerMethod.
		/// </summary>
		[
		Bindable(true),
		Category("Data"),
		DefaultValue(""),
		Description("This is the name of the field in the dataset returned by the ServerMethod that will be used for the Value property of DbCombo. Remember if you change this from it's default value of 'DbComboValue', you MUST change the ServerMethodEventArgs.FieldSecurity property in your ServerMethod.")
		]
		public string DataValueField
		{
			get
			{
				this.EnsureChildControls();
				return dataValueField;
			}
			set
			{
				this.EnsureChildControls();
				dataValueField=value;
				ulbDataValueField.Text=value;
			}
		}
		private string dataValueField="";
		private string dataValueFieldDefault="";
		#endregion
		#region DataTextField
		/// <summary>
		/// This is the name of the field in the dataset returned by the ServerMethod that will be used for the Text property of DbCombo. Remember if you change this from it's default value of 'DbComboText', you MUST change the ServerMethodEventArgs.FieldSecurity property in your ServerMethod.
		/// </summary>
		[
		Bindable(true),
		Category("Data"),
		DefaultValue(""),
		Description("This is the name of the field in the dataset returned by the ServerMethod that will be used for the Text property of DbCombo. Remember if you change this from it's default value of 'DbComboText', you MUST change the ServerMethodEventArgs.FieldSecurity property in your ServerMethod.")
		]
		public string DataTextField
		{
			get
			{
				this.EnsureChildControls();
				return dataTextField;
			}
			set
			{
				this.EnsureChildControls();
				dataTextField=value;
				ulbDataTextField.Text=value;
			}
		}
		private string dataTextField="";
		private string dataTextFieldDefault="";
		#endregion

		#region Value
		/// <summary>
		/// This is the currently selected value, provided by the DbComboValue db field
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(""),
		Description("This is the currently selected value, provided by the DbComboValue db field"),
		RefreshProperties(RefreshProperties.All)
		]
		public string Value
		{
			get
			{
				this.EnsureChildControls();
				if (DownLevel)
					if (dlbListBox.SelectedIndex>-1)
						return dlbListBox.SelectedItem.Value;
					else
						return dlbValueHidden.Value;
				else
					return ulbValueHidden.Value;
			}
			set
			{
				this.EnsureChildControls();
				ulbValueHidden.Value=value;
				dlbValueHidden.Value=value;
			}
		}
		#endregion
		#region Text
		/// <summary>
		/// This is the text of the currently selected value, provided by the DbComboText db field
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(""),
		Description("This is the text of the currently selected value, provided by the DbComboText db field"),
		RefreshProperties(RefreshProperties.All)
		]
		public string Text
		{
			get
			{
				this.EnsureChildControls();
				if (DownLevel)
					if (dlbListBox.SelectedIndex>-1)
						return dlbListBox.SelectedItem.Text;
					else
						return dlbTextBox.Text;
				else
					return ulbTextBox.Text;
			}
			set
			{
				this.EnsureChildControls();
				ulbTextBox.Text=value;
				dlbTextBox.Text=value;
			}
		}
		#endregion
		#region ReQueryText
		/// <summary>
		/// This is the re-query text. This must be properly set to initiate a re-querey on page load to occur.<br></br>
		/// This is the last peice of text that was actually typed by the user in the text box. It is usually 
		/// smaller than the selected text and, when queried on, will usually return a larger number or items.<br></br>
		/// The text property, when queried, will usually only return one record. On postback DbCombo remembers 
		/// what was last queried on, and uses this for a re-query. You can force a re-query on page load by 
		/// entering a value for this property, and the ReQueryRecordCount property.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - ReQuery"),
		DefaultValue(""),
		Description(@"This is the re-query text. This must be properly set to initiate a re-querey on page load to occur.
			This is the last peice of text that was actually typed by the user in the text box. It is usually 
			smaller than the selected text and, when queried on, will usually return a larger number or items.
			The text property, when queried, will usually only return one record. On postback DbCombo remembers 
			what was last queried on, and uses this for a re-query. You can force a re-query on page load by 
			entering a value for this property, and the ReQueryRecords property."),
		RefreshProperties(RefreshProperties.All)
		]
		public string ReQueryText
		{
			get
			{
				this.EnsureChildControls();
				return ulbQueryHidden.Value;
			}
			set
			{
				this.EnsureChildControls();
				ulbQueryHidden.Value=value;
				//ReQueryOnLoad=true;
			}
		}
		#endregion
		#region ReQueryRecords
		/// <summary>
		/// This is the number of records to return in a re-query. Defaults to the 10.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - ReQuery"),
		DefaultValue(10),
		Description(@"This is the number of records to return in a re-query. Defaults to 10."),
		RefreshProperties(RefreshProperties.All)
		]
		public int ReQueryRecords
		{
			get
			{
				this.EnsureChildControls();
				try
				{
					return int.Parse(ulbReQueryRecordsHidden.Value);
				}
				catch
				{
					return reQueryRecords; 
				}
			}
			set
			{
				this.EnsureChildControls();
				reQueryRecords = value;
				ulbReQueryRecordsHidden.Value=value.ToString();
			}
		}
		int reQueryRecords = 10;
		int reQueryRecordsDefault = 10;
		#endregion
		#region ReQueryOnLoad
		/// <summary>
		/// This property determines if DbCombo will perform a re-query the next time it loads. It is initialy set to false, but if a query is attempted and the page is posted back, it will become true. Set to true initially to force a re-query. (default: false).
		/// </summary>
		[
		Bindable(true),
		Category("Misc - ReQuery"),
		DefaultValue(false),
		Description("This property determines if DbCombo will perform a re-query the next time it loads. It is initialy set to false, but if a query is attempted and the page is posted back, it will become true. Set to true initially to force a re-query. (default: false).")
		]
		public bool ReQueryOnLoad
		{
			get
			{
				this.EnsureChildControls();
				return bool.Parse(ulbReQueryOnLoad.Value);
			}
			set
			{
				this.EnsureChildControls();
				reQueryOnLoad=value;
				ulbReQueryOnLoad.Value = value.ToString().ToLower();

			//	if (Page!=null && Page.IsPostBack && value &! ReQueryDisabled && this.ViewState["DbComboPreviousVisible"]!=null && (bool)this.ViewState["DbComboPreviousVisible"]==true)
			//		ulbReQueryOnLoad.Text="true";
			//	else
			//		ulbReQueryOnLoad.Text="false";
			}
		}
		private bool reQueryOnLoad=false;
		private bool reQueryOnLoadDefault=false;
		#endregion
		#region ReQueryDisabled
		/// <summary>
		/// If this is set to true, the control will not perform a re-query on load. If set to false, the control will perform a re-query if necessary. (Default: false).
		/// </summary>
		[
		Bindable(true),
		Category("Misc - ReQuery"),
		DefaultValue(false),
		Description("If this is set to true, the control will not perform a re-query on load. If set to false, the control will perform a re-query if necessary. (Default: false).")
		]
		public bool ReQueryDisabled
		{
			get
			{
				this.EnsureChildControls();
				return reQueryDisabled;
			}
			set
			{
				this.EnsureChildControls();
				reQueryDisabled=value;
				ulbReQueryDisabled.Text=value.ToString().ToLower();
			}
		}
		private bool reQueryDisabled=false;
		private bool reQueryDisabledDefault=false;
		#endregion
	/*	#region AutoQueryOnLoad
		/// <summary>
		/// If this is set to true, the control will automatically do a default query when the page loads.
		/// </summary>
		[
		Bindable(true),
		Category("Misc"),
		DefaultValue(false),
		Description("If this is set to true, the control will automatically do a default query when the page loads.")
		]
		public bool AutoQueryOnLoad
		{
			get
			{
				this.EnsureChildControls();
				return autoQueryOnLoad;
			}
			set
			{
				this.EnsureChildControls();
				autoQueryOnLoad=value;
				ulbAutoQueryOnLoad.Text=value.ToString().ToLower();
				
			}
		}
		private bool autoQueryOnLoad=false;
		private bool autoQueryOnLoadDefault=false;
		#endregion
		*/

		#region TextBoxColumns
		/// <summary>
		/// This sets the size of the text-box (defaults to 30)
		/// </summary>
		[
		Bindable(true),
		Category("Layout"),
		DefaultValue(30),
		Description("This sets the size of the text-box (defaults to 30)"),
		RefreshProperties(RefreshProperties.All)
		]
		public int TextBoxColumns
		{
			get
			{
				this.EnsureChildControls();
				return ulbTextBox.Columns;
			}
			set
			{
				this.EnsureChildControls();
				ulbTextBox.Columns=value;
				dlbTextBox.Columns=value;
			}
		}
		private int textBoxColumnsDefault=30;
		#endregion
		#region DropDownRows
		/// <summary>
		/// This sets the number of rows in the drop-box (defaults to 10)
		/// </summary>
		[
		Bindable(true),
		Category("Layout"),
		DefaultValue(10),
		Description("This sets the number of rows in the drop-box (defaults to 10)"),
		RefreshProperties(RefreshProperties.All)
		]
		public int DropDownRows
		{
			get
			{
				this.EnsureChildControls();
				return int.Parse(ulbDropDown.Attributes["size"]);
			}
			set
			{
				this.EnsureChildControls();
				ulbDropDown.Attributes["size"] = value.ToString();
				ulbDropDownRows.Text = value.ToString();
				dlbListBox.Rows = value;
			}
		}
		private int dropDownRowsDefault=10;
		#endregion
		#region MaxLength
		/// <summary>
		/// This sets the maximum length of the text-box - if 0, then unlimited. Default is 0. Note: this only affects text input. If an item longer than the MaxLength is selected, it will be displayed in full.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(0),
		Description("This sets the maximum length of the text-box - if 0, then unlimited. Default is 0. Note: this only affects text input. If an item longer than the MaxLength is selected, it will be displayed in full."),
		RefreshProperties(RefreshProperties.All)
		]
		public int MaxLength
		{
			get
			{
				this.EnsureChildControls();
				return ulbTextBox.MaxLength;
			}
			set
			{
				this.EnsureChildControls();
				ulbTextBox.MaxLength=value;
				dlbTextBox.MaxLength=value;
			}
		}
		private int maxLengthDefault=0;
		#endregion
		#region TabIndex
		/// <summary>
		/// This sets the tab index of the text box. The tab index of the up level search button is always set to -1. Tab indexes of other buttons follow in sequence from the main tabindex. Defaults to 1.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue((short)0),
		Description("This sets the tab index of the text box. The tab index of the up level search button is always set to -1. Tab indexes of other buttons follow in sequence from the main tabindex. Defaults to not set."),
		RefreshProperties(RefreshProperties.All)
		]
		public override short TabIndex
		{
			get
			{
				this.EnsureChildControls();
				return ulbTextBox.TabIndex;
			}
			set
			{
				this.EnsureChildControls();
				ulbTextBox.TabIndex=value;
				ulbHelpButton.Attributes["tabindex"]=((short)(value+((short)0.1))).ToString();
				dlbTextBox.TabIndex=value;
				dlbSearchButton.Attributes["tabindex"]=((short)(value+((short)0.1))).ToString();
				dlbClearButton.Attributes["tabindex"]=((short)(value+((short)0.2))).ToString();
				dlbHelpButton.Attributes["tabindex"]=((short)(value+((short)0.3))).ToString();
				dlbListBox.Attributes["tabindex"]=((short)(value+((short)0.4))).ToString();
				dlbSelectButton.Attributes["tabindex"]=((short)(value+((short)0.5))).ToString();
				dlbMoreResultsButton.Attributes["tabindex"]=((short)(value+((short)0.6))).ToString();
			}
		}
		private short tabIndexDefault=(short)0;
		#endregion
		
		#region Style attribute for styled elements
		#region UpLevelButtonStyle
		/// <summary>
		/// This is the style html attribute of the up-level buttons in the control. (Except the 'More...' button in the status panel).
		/// Default = "border:solid 1px #999999;margin-left:-1;font-weight:bold;".
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue("border:solid 1px #999999;margin-left:-1;font-weight:bold;"),
		Description("This is the style html attribute of the up-level buttons in the control. (Except the 'More...' button in the status panel). Default = \"border:solid 1px #999999;margin-left:-1;font-weight:bold;\""),
		RefreshProperties(RefreshProperties.All),
		]
		public string UpLevelButtonStyle
		{
			get
			{
				this.EnsureChildControls();
				return upLevelButtonStyle;
			}
			set
			{
				this.EnsureChildControls();
				upLevelButtonStyle = value;
				//ulbClearButton.Attributes["style"]=value;
				ulbHelpButton.Attributes["style"]=value;
				//ulbRevertButton.Attributes["style"]=value;
				ulbSearchButton.Attributes["style"]=value;
			}
		}
		private string upLevelButtonStyle = "border:solid 1px #999999;margin-left:-1;font-weight:bold;";
		private string upLevelButtonStyleDefault = "border:solid 1px #999999;margin-left:-1;font-weight:bold;";
		#endregion
		#region UpLevelMoreResultsButtonStyle
		/// <summary>
		/// This is the style html attribute of the 'More...' button in the status panel.
		/// Default: "border:solid 1px #999999;height:23px;".
		/// The hard-coded height of 23px fixes the button at the same height as the DbCombo logo and link. This ensures the button doesn't display smaller than the logo, as this situation doesn't look good.
		/// If you disable the link, you probably want to remove the hard-coded height here.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue("border:solid 1px #999999;vertical-align:bottom;height:21px;"),
		Description("This is the style html attribute of the 'More...' button in the status panel. Default: \"border:solid 1px #999999;vertical-align:bottom;height:21px;\". The hard-coded height of 23px fixes the button at the same height as the DbCombo logo and link. This ensures the button doesn't display smaller than the logo, as this situation doesn't look good. If you disable the link, you probably want to remove the hard-coded height here."),
		RefreshProperties(RefreshProperties.All)
		]
		public string UpLevelMoreResultsButtonStyle
		{
			get
			{
				this.EnsureChildControls();
				return upLevelMoreResultsButtonStyle;
			}
			set
			{
				this.EnsureChildControls();
				upLevelMoreResultsButtonStyle = value;
				ulbMoreResultsButton.Attributes["style"]=value;
			}
		}
		private string upLevelMoreResultsButtonStyle="border:solid 1px #999999;vertical-align:bottom;height:21px;";
		private string upLevelMoreResultsButtonStyleDefault="border:solid 1px #999999;vertical-align:bottom;height:21px;";
		#endregion
		#region DownLevelButtonStyle
		/// <summary>
		/// This is the style html attribute of all the down-level buttons in the control
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue(""),
		Description("This is the style html attribute of all the down-level buttons in the control"),
		RefreshProperties(RefreshProperties.All)
		]
		public string DownLevelButtonStyle
		{
			get
			{
				this.EnsureChildControls();
				return downLevelButtonStyle;
			}
			set
			{
				this.EnsureChildControls();
				downLevelButtonStyle=value;
				dlbSearchButton.Attributes["style"]=value;
				dlbClearButton.Attributes["style"]=value;
				dlbHelpButton.Attributes["style"]=value;
				dlbSelectButton.Attributes["style"]=value;
				dlbMoreResultsButton.Attributes["style"]=value;
			}
		}
		private string downLevelButtonStyle = "";
		private string downLevelButtonStyleDefault = "";
		#endregion
		#region TextBoxStyle
		/// <summary>
		/// This is the style html attribute of the text box. Default: "border:solid 1px #999999;"
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue("border:solid 1px #999999;"),
		Description("This is the style html attribute of the text box. - Default: \"border:solid 1px #999999;\""),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextBoxStyle
		{
			get
			{
				this.EnsureChildControls();
				return textBoxStyle;
			}
			set
			{
				this.EnsureChildControls();
				textBoxStyle = value;
				ulbTextBox.Attributes["style"]=value;
				dlbTextBox.Attributes["style"]=value;
			}
		}
		private string textBoxStyle = "border:solid 1px #999999;";
		private string textBoxStyleDefault = "border:solid 1px #999999;";
		#endregion
		#region ResultsBackgroundSpanStyle
		/// <summary>
		/// This is the style html attribute of the background of the UpLevel results
		/// Default: "position:absolute;background-color:#ffffff;border:solid 1px #999999;".
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue("position:absolute;background-color:#ffffff;border:solid 1px #999999;"),
		Description("This is the style html attribute of the background of the UpLevel results. Default: \"position:absolute;background-color:#ffffff;border:solid 1px #999999;\""),
		RefreshProperties(RefreshProperties.All)
		]
		public string ResultsBackgroundSpanStyle
		{
			get
			{
				this.EnsureChildControls();
				return resultsBackgroundSpanStyle;
			}
			set
			{
				this.EnsureChildControls();
				resultsBackgroundSpanStyle = value;
				ulbResultsSpan.Attributes["style"] = value;
			}
		}
		private string resultsBackgroundSpanStyle = "position:absolute;background-color:#ffffff;border:solid 1px #999999;";
		private string resultsBackgroundSpanStyleDefault = "position:absolute;background-color:#ffffff;border:solid 1px #999999;";
		#endregion
		#region ResultsInnerSpanStyle
		/// <summary>
		/// This is the style html attribute of the Inner of the UpLevel results
		/// Default: "border-bottom:solid 1px #999999;"
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue("border-bottom:solid 1px #999999;"),
		Description("This is the style html attribute of the inner span of the UpLevel results. Default: \"border-bottom:solid 1px #999999;\""),
		RefreshProperties(RefreshProperties.All)
		]
		public string ResultsInnerSpanStyle
		{
			get
			{
				this.EnsureChildControls();
				return resultsInnerSpanStyle;
			}
			set
			{
				this.EnsureChildControls();
				resultsInnerSpanStyle = value;
				ulbInnerResultsSpan.Attributes["style"] = value;
			}
		}
		private string resultsInnerSpanStyle = "border-bottom:solid 1px #999999;";
		private string resultsInnerSpanStyleDefault = "border-bottom:solid 1px #999999;";
		#endregion
		#region ResultsSelectBoxStyle
		/// <summary>
		/// This is the style html attribute of the results list
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue(""),
		Description("This is the style html attribute of the results list"),
		RefreshProperties(RefreshProperties.All)
		]
		public string ResultsSelectBoxStyle
		{
			get
			{
				this.EnsureChildControls();
				return resultsSelectBoxStyle;
			}
			set
			{
				this.EnsureChildControls();
				resultsSelectBoxStyle = value;
				ulbDropDown.Attributes["style"] = value;
				dlbListBox.Attributes["style"] = value;
			}
		}
		private string resultsSelectBoxStyle = "";
		private string resultsSelectBoxStyleDefault = "";
		#endregion
		#region StatusMessageStyle
		/// <summary>
		/// This is the style html attribute of the status messages (Default "padding:5px;font-face:Arial, Verdana, Sans-serif;font-size:x-small;")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue("padding:3px;font-family:Arial, Verdana, Sans-serif;font-size:x-small;"),
		Description("This is the style html attribute of the status messages"),
		RefreshProperties(RefreshProperties.All)
		]
		public string StatusMessageStyle
		{
			get
			{
				this.EnsureChildControls();
				return statusMessageStyle;
			}
			set
			{
				this.EnsureChildControls();
				statusMessageStyle = value;
				ulbStatusDisplaySpan.Attributes["style"] = value;
			}
		}
		private string statusMessageStyle = "padding:3px;font-family:Arial, Verdana, Sans-serif;font-size:x-small;";
		private string statusMessageStyleDefault = "padding:3px;font-family:Arial, Verdana, Sans-serif;font-size:x-small;";
		#endregion
		#region MoreResultsBarStyle
		/// <summary>
		/// This is the style html attribute of the 'more results' bar (the area that contains the 'more' button in up-level mode)
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue("padding:1px;background-color:#eeeeee;border-top:solid 1px #999999;"),
		Description("This is the style html attribute of the 'more results' bar (the area that contains the 'more' button in up-level mode). Default: \"padding:1px;background-color:#eeeeee;border-top:solid 1px #999999;\""),
		RefreshProperties(RefreshProperties.All)
		]
		public string MoreResultsBarStyle
		{
			get
			{
				this.EnsureChildControls();
				return moreResultsBarStyle;
			}
			set
			{
				this.EnsureChildControls();
				moreResultsBarStyle = value;
				ulbStatusBarDiv.Attributes["style"] = value;
			}
		}
		private string moreResultsBarStyle  = "padding:1px;background-color:#eeeeee;border-top:solid 1px #999999;";
		private string moreResultsBarStyleDefault = "padding:1px;background-color:#eeeeee;border-top:solid 1px #999999;";
		#endregion
		#region MainSpanStyle
		/// <summary>
		/// This is the style html attribute of the main span that encloses the whole control. 
		/// You may want to set this to "display:block;" to draw the control in a block.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Styles"),
		DefaultValue(""),
		Description("This is the style html attribute of the main span that encloses the whole control. You may want to set this to \"display:block;\" to draw the control in a block."),
		RefreshProperties(RefreshProperties.All)
		]
		public string MainSpanStyle
		{
			get
			{
				this.EnsureChildControls();
				return mainSpanStyle;
			}
			set
			{
				this.EnsureChildControls();
				mainSpanStyle = value;
				ulbHolder.Attributes["style"] = value;
				dlbHolder.Attributes["style"] = value;
			}
		}
		private string mainSpanStyle = "";
		private string mainSpanStyleDefault = "";
		#endregion

		#region ResultsSpanTweakX
		/// <summary>
		/// NOTE: The problem that caused this has been solved, so you should never need this. (I've left it in, just in case). The results span can be misaligned by an IE browser bug. If DbCombo is contained an element that has a border property set in a class definition, the width of the border will not be included in the offset of the results span. This will cause a misalignment. Tweak this variable until the edge of the results span is aligned with the edge of the text box. The first person providing a fix for this problem will receive a free copy of DbCombo.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(0),
		Description("NOTE: The problem that caused this has been solved, so you should never need this. (I've left it in, just in case). The results span can be misaligned by an IE browser bug. If DbCombo is contained an element that has a border property set in a class definition, the width of the border will not be included in the offset of the results span. This will cause a misalignment. Tweak this variable until the edge of the results span is aligned with the edge of the text box. The first person providing a fix for this problem will receive a free copy of DbCombo."),
		RefreshProperties(RefreshProperties.All)
		]
		public int ResultsSpanTweakX
		{
			get
			{
				this.EnsureChildControls();
				return resultsSpanTweakX;
			}
			set
			{
				this.EnsureChildControls();
				resultsSpanTweakX = value;
				ulbResultsSpanTweakX.Text=value.ToString();
			}
		}
		private int resultsSpanTweakX=0;
		private int resultsSpanTweakXDefault=0;
		#endregion

		#region ResultsSpanTweakY
		/// <summary>
		/// NOTE: The problem that caused this has been solved, so you should never need this. (I've left it in, just in case). The results span can be misaligned by an IE browser bug. If DbCombo is contained an element that has a border property set in a class definition, the height of the border will not be included in the offset of the results span. This will cause a misalignment. Tweak this variable until the top of the results span is aligned with the bottom of the text box. The first person providing a fix for this problem will receive a free copy of DbCombo.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(0),
		Description("NOTE: The problem that caused this has been solved, so you should never need this. (I've left it in, just in case). The results span can be misaligned by an IE browser bug. If DbCombo is contained an element that has a border property set in a class definition, the height of the border will not be included in the offset of the results span. This will cause a misalignment. Tweak this variable until the top of the results span is aligned with the bottom of the text box. The first person providing a fix for this problem will receive a free copy of DbCombo."),
		RefreshProperties(RefreshProperties.All)
		]
		public int ResultsSpanTweakY
		{
			get
			{
				this.EnsureChildControls();
				return resultsSpanTweakY;
			}
			set
			{
				this.EnsureChildControls();
				resultsSpanTweakY = value;
				ulbResultsSpanTweakY.Text=value.ToString();
			}
		}
		private int resultsSpanTweakY=0;
		private int resultsSpanTweakYDefault=0;
		#endregion

		#endregion

		#region Class attribute for styled elements
		#region UpLevelButtonClass
		/// <summary>
		/// This is the class html attribute of all the up-level buttons in the control (Except the 'More...' button in the status panel.)
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of all the up-level buttons in the control (Except the 'More...' button in the status panel.)"),
		RefreshProperties(RefreshProperties.All)
		]
		public string UpLevelButtonClass
		{
			get
			{
				this.EnsureChildControls();
				return upLevelButtonClass;
			}
			set
			{
				this.EnsureChildControls();
				upLevelButtonClass = value;
				//ulbClearButton.Attributes["class"]=value;
				ulbHelpButton.Attributes["class"]=value;
				//				ulbRevertButton.Attributes["class"]=value;
				ulbSearchButton.Attributes["class"]=value;
			}
		}
		private string upLevelButtonClass = "";
		#endregion
		#region UpLevelMoreResultsButtonClass
		/// <summary>
		/// This is the class html attribute of the 'More...' button in the status panel
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the 'More...' button in the status panel"),
		RefreshProperties(RefreshProperties.All)
		]
		public string UpLevelMoreResultsButtonClass
		{
			get
			{
				this.EnsureChildControls();
				return upLevelMoreResultsButtonClass;
			}
			set
			{
				this.EnsureChildControls();
				upLevelMoreResultsButtonClass = value;
				ulbMoreResultsButton.Attributes["class"]=value;
			}
		}
		private string upLevelMoreResultsButtonClass = "";
		#endregion
		#region DownLevelButtonClass
		/// <summary>
		/// This is the class html attribute of all the down-level buttons in the control
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of all the down-level buttons in the control"),
		RefreshProperties(RefreshProperties.All)
		]
		public string DownLevelButtonClass
		{
			get
			{
				this.EnsureChildControls();
				return downLevelButtonClass;
			}
			set
			{
				this.EnsureChildControls();
				downLevelButtonClass = value;
				dlbSearchButton.Attributes["class"]=value;
				dlbClearButton.Attributes["class"]=value;
				dlbHelpButton.Attributes["class"]=value;
				dlbSelectButton.Attributes["class"]=value;
				dlbMoreResultsButton.Attributes["class"]=value;
			}
		}
		private string downLevelButtonClass = "";
		#endregion
		#region TextBoxClass
		/// <summary>
		/// This is the class html attribute of the text box
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the text box"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextBoxClass
		{
			get
			{
				this.EnsureChildControls();
				return textBoxClass;
			}
			set
			{
				this.EnsureChildControls();
				textBoxClass = value;
				ulbTextBox.Attributes["class"]=value;
				dlbTextBox.Attributes["class"]=value;
			}
		}
		private string textBoxClass = "";
		#endregion
		#region ResultsBackgroundSpanClass
		/// <summary>
		/// This is the class html attribute of the background of the UpLevel results
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the background of the UpLevel results"),
		RefreshProperties(RefreshProperties.All)
		]
		public string ResultsBackgroundSpanClass
		{
			get
			{
				this.EnsureChildControls();
				return resultsBackgroundSpanClass;
			}
			set
			{
				this.EnsureChildControls();
				resultsBackgroundSpanClass = value;
				ulbResultsSpan.Attributes["class"] = value;
			}
		}
		private string resultsBackgroundSpanClass="";
		#endregion
		#region ResultsInnerSpanClass
		/// <summary>
		/// This is the class html attribute of the background of the UpLevel results
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the inner span of the UpLevel results"),
		RefreshProperties(RefreshProperties.All)
		]
		public string ResultsInnerSpanClass
		{
			get
			{
				this.EnsureChildControls();
				return resultsInnerSpanClass;
			}
			set
			{
				this.EnsureChildControls();
				resultsInnerSpanClass = value;
				ulbInnerResultsSpan.Attributes["class"] = value;
			}
		}
		private string resultsInnerSpanClass="";
		#endregion
		#region ResultsSelectBoxClass
		/// <summary>
		/// This is the class html attribute of the results list
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the results list"),
		RefreshProperties(RefreshProperties.All)
		]
		public string ResultsSelectBoxClass
		{
			get
			{
				this.EnsureChildControls();
				return resultsSelectBoxClass;
			}
			set
			{
				this.EnsureChildControls();
				resultsSelectBoxClass = value;
				ulbDropDown.Attributes["class"] = value;
				dlbListBox.Attributes["class"] = value;
			}
		}
		private string resultsSelectBoxClass = "";
		#endregion
		#region StatusMessageClass
		/// <summary>
		/// This is the class html attribute of the status messages
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the status messages"),
		RefreshProperties(RefreshProperties.All)
		]
		public string StatusMessageClass
		{
			get
			{
				this.EnsureChildControls();
				return statusMessageClass;
			}
			set
			{
				this.EnsureChildControls();
				statusMessageClass = value;
				ulbStatusDisplaySpan.Attributes["class"] = value;
				dlbNoResultsLabel.Attributes["class"] = value;
			}
		}
		private string statusMessageClass="";
		#endregion
		#region MoreResultsBarClass
		/// <summary>
		/// This is the class html attribute of the 'more results' bar (the area that contains the 'more' button in up-level mode)
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the 'more results' bar (the area that contains the 'more' button in up-level mode)"),
		RefreshProperties(RefreshProperties.All)
		]
		public string MoreResultsBarClass
		{
			get
			{
				this.EnsureChildControls();
				return moreResultsBarClass;
			}
			set
			{
				this.EnsureChildControls();
				moreResultsBarClass = value;
				ulbStatusBarDiv.Attributes["class"] = value;
			}
		}
		private string moreResultsBarClass = "";
		#endregion
		#region MainSpanClass
		/// <summary>
		/// This is the class html attribute of the main span that encloses the whole control. 
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - CSS Classes"),
		DefaultValue(""),
		Description("This is the class html attribute of the main span that encloses the whole control. "),
		RefreshProperties(RefreshProperties.All)
		]
		public string MainSpanClass
		{
			get
			{
				this.EnsureChildControls();
				return mainSpanClass;
			}
			set
			{
				this.EnsureChildControls();
				mainSpanClass = value;
				ulbHolder.Attributes["class"] = value;
				dlbHolder.Attributes["class"] = value;
			}
		}
		private string mainSpanClass="";
		#endregion
		#endregion

		#region Text properties
		#region TextClearButton
		/// <summary>
		/// The text that appears in the 'Clear' button. (Default: "Clear")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("Clear"),
		Description("The text that appears in the 'Clear' button. (Default: \"Clear\")"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextClearButton
		{
			get
			{
				this.EnsureChildControls();
				return textClearButton;
			}
			set
			{
				this.EnsureChildControls();
				textClearButton = value;
				//ulbClearButton.Value=value;
				dlbClearButton.Value=value;
			}
		}
		private string textClearButton = "Clear";
		private string textClearButtonDefault = "Clear";
		#endregion
		#region TextAlt
		/// <summary>
		/// The text that appears in the ALT attribute of the textbox. This is useful for screen-readers.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue(""),
		Description("The text that appears in the ALT attribute of the textbox. This is useful for screen-readers."),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextAlt
		{
			get
			{
				this.EnsureChildControls();
				return textAlt;
			}
			set
			{
				this.EnsureChildControls();
				textAlt = value;
				dlbTextBox.Attributes["alt"]=value;
				ulbTextBox.Attributes["alt"]=value;
			}
		}
		private string textAlt = "";
		private string textAltDefault = "";
		#endregion
		#region TextDownLevelSearchButton
		/// <summary>
		/// The text that appears in the down-level 'Search' button. (Default: "Search")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("Search"),
		Description("The text that appears in the down-level 'Search' button. (Default: \"Search\")"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextDownLevelSearchButton
		{
			get
			{
				this.EnsureChildControls();
				return textDownLevelSearchButton;
			}
			set
			{
				this.EnsureChildControls();
				textDownLevelSearchButton = value;
				dlbSearchButton.Value=value;
			}
		}
		private string textDownLevelSearchButton = "Search";
		private string textDownLevelSearchButtonDefault = "Search";
		#endregion
		#region TextUpLevelSearchButton
		/// <summary>
		/// This text appears in the up-level search button (this button is hidden by default).
		/// (Default: "\/")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("\\/"),
		Description("This text appears in the up-level search button (this button is hidden by default)."),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextUpLevelSearchButton
		{
			get
			{
				this.EnsureChildControls();
				return textUpLevelSearchButton;
			}
			set
			{
				this.EnsureChildControls();
				textUpLevelSearchButton = value;
				if (UpLevelSearchButtonType.Equals(ButtonTypes.Text))
					ulbSearchButton.InnerText = value;
			}
		}
		private string textUpLevelSearchButton=@"\/";
		#endregion
		#region TextLoading
		/// <summary>
		/// The text that appears in the status message while DbCombo is loading more results. (Default: "Loading")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("Loading"),
		Description("The text that appears in the status message while DbCombo is loading more results. (Default: \"Loading\")"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextLoading
		{
			get
			{
				this.EnsureChildControls();
				return textLoading;
			}
			set
			{
				this.EnsureChildControls();
				textLoading = value;
				ulbTextLoading.Text = value;
			}
		}
		private string textLoading = "Loading";
		private string textLoadingDefault = "Loading";
		#endregion
		#region TextNoResults
		/// <summary>
		/// The text that appears when there are no results to display (Default: "No results")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("No results"),
		Description("The text that appears when there are no results to display (Default: \"No results\")"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextNoResults
		{
			get
			{
				this.EnsureChildControls();
				return textNoResults;
			}
			set
			{
				this.EnsureChildControls();
				textNoResults = value;
				ulbTextNoResults.Text = value;
				dlbNoResultsLabel.Text = value;
			}
		}
		private string textNoResults = "No results";
		private string textNoResultsDefault = "No results";
		#endregion
		#region TextMoreButton
		/// <summary>
		/// The text that appears on the 'More' button (Default: "More...")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("More..."),
		Description("The text that appears on the 'More' button (Default: \"More...\")"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextMoreButton
		{
			get
			{
				this.EnsureChildControls();
				return textMoreButton;
			}
			set
			{
				try
				{
					this.EnsureChildControls();
				}
				catch{}
				textMoreButton = value;
				try
				{
					ulbMoreResultsButton.Value=value;
					dlbMoreResultsButton.Value=value;
				}
				catch{}
			}
		}
		private string textMoreButton = "More...";
		private string textMoreButtonDefault = "More...";
		#endregion
		#region TextSelectButton
		/// <summary>
		/// The text that appears on the 'Select' button (DownLevel only) (Default: "Select")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("Select"),
		Description("The text that appears on the 'Select' button (DownLevel only) (Default: \"Select\")"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextSelectButton
		{
			get
			{
				this.EnsureChildControls();
				return textSelectButton;
			}
			set
			{
				this.EnsureChildControls();
				textSelectButton = value;
				dlbSelectButton.Value=value;
			}
		}
		private string textSelectButton = "Select";
		private string textSelectButtonDefault="Select";
		#endregion
		#region TextHelpButton
		/// <summary>
		/// The text that appears on the help button (Default: "?")
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("?"),
		Description("The text that appears on the help button (Default: \"?\")"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextHelpButton
		{
			get
			{
				this.EnsureChildControls();
				return textHelpButton;
			}
			set
			{
				this.EnsureChildControls();
				textHelpButton = value;
				ulbHelpButton.Attributes["value"]=value;
				dlbHelpButton.Attributes["value"]=value;
			}
		}
		private string textHelpButton = "?";
		private string textHelpButtonDefault = "?";
		#endregion
		#region Removed
		/*		#region TextRevertButton
				/// <summary>
				/// The text that appears on the revert button (UpLevel only) (Default: " X ")
				/// </summary>
				[
				Bindable(true),
				Category("Appearance - Text"),
				DefaultValue(" X "),
				Description("The text that appears on the revert button (UpLevel only) (Default: \" X \")"),
				RefreshProperties(RefreshProperties.All)
				]
				public string TextRevertButton
				{
					get
					{
						this.EnsureChildControls();
						return textRevertButton;
					}
					set
					{
						this.EnsureChildControls();
						textRevertButton = value;
						ulbRevertButton.Attributes["value"]=value;
					}
				}
				private string textRevertButton = " X ";
				private string textRevertButtonDefault = " X ";
		#endregion
		*/
		#endregion
		#region TextUpLevelHelpBox
		/// <summary>
		/// The text that appears in the UpLevel help box (Default: "This textbox will auto-complete as you type.\nEnter a few letters of the item you're looking for, and a pop-up will appear listing possible values.")
		/// Note - use "\n" for a new line, and remember to escape certain characters - e.g. "'" must be written as "\'".
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue("This textbox will auto-complete as you type.\\nEnter a few letters of the item you\\'re looking for, and a pop-up will appear listing possible values."),
		Description("The text that appears in the UpLevel help box"),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextUpLevelHelpBox
		{
			get
			{
				this.EnsureChildControls();
				return textUpLevelHelpBox;
			}
			set
			{
				this.EnsureChildControls();
				
				textUpLevelHelpBox=value;
				ulbHelpButton.Attributes["onclick"]="alert('"+textUpLevelHelpBox+"');return false;";
			}
		}
		private string textUpLevelHelpBox=@"This textbox will auto-complete as you type.\nEnter a few letters of the item you\'re looking for, and a pop-up will appear listing possible values.";
		#endregion
		#region TextDownLevelHelpBox
		/// <summary>
		/// The text that appears in the DownLevel help box (Default: "Enter a few letters of the item you\'re looking for, and press the \'Search\' button. A list will appear with possible values.")
		/// Note - use "\n" for a new line, and remember to escape certain characters - e.g. "'" must be written as "\'".
		/// </summary>
		[
		Bindable(true),
		Category("Appearance - Text"),
		DefaultValue(@"Enter a few letters of the item you\'re looking for, and press the \'Search\' button. A list will appear with possible values."),
		Description("The text that appears in the DownLevel help box - Note - use \"\n\" for a new line, and remember to escape certain characters - e.g. \"'\" must be written as \"\'\"."),
		RefreshProperties(RefreshProperties.All)
		]
		public string TextDownLevelHelpBox
		{
			get
			{
				this.EnsureChildControls();
				return textDownLevelHelpBox;
			}
			set
			{
				this.EnsureChildControls();
				
				textDownLevelHelpBox=value;
				dlbHelpButton.Attributes["onclick"]="alert('"+textDownLevelHelpBox+"');return false;";
			}
		}
		private string textDownLevelHelpBox=@"Enter a few letters of the item you\'re looking for, and press the \'Search\' button. A list will appear with possible values.";
		#endregion
		#region Removed
		/*
		#region TextRevertBox
				/// <summary>
				/// The text that appears in the revert box (Default: "This control uses complex JavaScript, which may not work perfectly with all browsers. \nTo revert to more simple HTML, click the OK button. If you have not experianced difficulties, click the CANCEL button.")
				/// Note - use "\n" for a new line, and remember to escape certain characters - e.g. "'" must be written as "\'".
				/// </summary>
				[
				Bindable(true),
				Category("Appearance - Text"),
				DefaultValue("This control uses complex JavaScript, which may not work perfectly with all browsers. \nTo revert to more simple HTML, click the OK button. If you have not experianced difficulties, click the CANCEL button."),
				Description("The text that appears in the revert box - Note - use \"\n\" for a new line, and remember to escape certain characters - e.g. \"'\" must be written as \"\'\"."),
				RefreshProperties(RefreshProperties.All)
				]
				public string TextRevertBox
				{
					get
					{
						this.EnsureChildControls();
						return textRevertBox;
					}
					set
					{
						this.EnsureChildControls();
						textRevertBox=value;
					//	ulbRevertButton.Attributes["onclick"]=@"if (confirm('"+textRevertBox+"')){"+Page.GetPostBackEventReference(this,"revert")+";}else{return false;}";
					}
				}
				private string textRevertBox=@"This control uses complex JavaScript, which may not work perfectly with all browsers. \nTo revert to more simple HTML, click the OK button. If you have not experianced difficulties, click the CANCEL button.";
		#endregion
		*/
		#endregion
		#endregion

		#region Properties to show/hide the optional parts
		#region ShowUpLevelHelpButton
		/// <summary>
		/// This hides or displays the UpLevel help button
		/// </summary>
		[
		Bindable(true),
		Category("Functionality"),
		DefaultValue(false),
		Description("This hides or displays the UpLevel help button"),
		RefreshProperties(RefreshProperties.All)
		]
		public bool ShowUpLevelHelpButton
		{
			get
			{
				this.EnsureChildControls();
				return showUpLevelHelpButton;
			}
			set
			{
				this.EnsureChildControls();
				showUpLevelHelpButton = value;
				ulbHelpButton.Visible = value;
			}
		}
		private bool showUpLevelHelpButton = false;
		private bool showUpLevelHelpButtonDefault = false;
		#endregion
		#region ShowDownLevelHelpButton
		/// <summary>
		/// This hides or displays the DownLevel help button
		/// </summary>
		[
		Bindable(true),
		Category("Functionality"),
		DefaultValue(false),
		Description("This hides or displays the DownLevel help button"),
		RefreshProperties(RefreshProperties.All)
		]
		public bool ShowDownLevelHelpButton
		{
			get
			{
				this.EnsureChildControls();
				return showDownLevelHelpButton;
			}
			set
			{
				this.EnsureChildControls();
				showDownLevelHelpButton = value;
				dlbHelpButton.Visible = value;
				dlbHelpButtonSpacer.Visible = value;
			}
		}
		private bool showDownLevelHelpButton = false;
		private bool showDownLevelHelpButtonDefault = false;
		#endregion
		/*
				#region ShowRevertButton
				/// <summary>
				/// This hides or displays the revert button
				/// </summary>
				[
				Bindable(true),
				Category("Functionality"),
				DefaultValue(""),
				Description("This hides or displays the revert button"),
				RefreshProperties(RefreshProperties.All)
				]
				public bool ShowRevertButton
				{
					get
					{
						this.EnsureChildControls();
						return showRevertButton;
					}
					set
					{
						this.EnsureChildControls();
						showRevertButton = value;
						ulbRevertButton.Visible=value;
					}
				}
				private bool showRevertButton = false;
				private bool showRevertButtonDefault = false;
				#endregion
		*/
		#region ShowUpLevelSearchButton
		/// <summary>
		/// This hides or displays the up-level search button (this button is hidden by default). The up-level search button acts like a combo-box button, allowing a blank search to be performed.
		/// </summary>
		[
		Bindable(true),
		Category("Functionality"),
		DefaultValue(true),
		Description("This hides or displays the up-level search button (this button is hidden by default). The up-level search button acts like a combo-box button, allowing a blank search to be performed."),
		RefreshProperties(RefreshProperties.All)
		]
		public bool ShowUpLevelSearchButton
		{
			get
			{
				this.EnsureChildControls();
				return showUpLevelSearchButton;
			}
			set
			{
				this.EnsureChildControls();
				showUpLevelSearchButton = value;
				ulbSearchButton.Visible=value;
			}
		}
		private bool showUpLevelSearchButton = true;
		private bool showUpLevelSearchButtonDefault = true;
		#endregion
		#region ShowDbComboLink
		/// <summary>
		/// This hides or displays the DbCombo.net logo link.
		/// </summary>
		[
		Bindable(true),
		Category("Functionality"),
		DefaultValue(true),
		Description("This hides or displays the DbCombo.net logo link."),
		RefreshProperties(RefreshProperties.All)
		]
		public bool ShowDbComboLink
		{
			get
			{
				this.EnsureChildControls();
				return showDbComboLink;
			}
			set
			{
				this.EnsureChildControls();
				showDbComboLink = value;
				ulbLogoPlaceHolder.Visible = value;
				ulbShowDbComboLink.Text=value.ToString().ToLower();
			}
		}
		private bool showDbComboLink = true;
		private bool showDbComboLinkDefault = true;
		#endregion
		#endregion

		#region UpLevelSearchButtonType
		/// <summary>
		/// Determines whether the Up-Level search button displays as a string or a graphic.
		/// </summary>
		[
		Bindable(true),
		Category("Appearance"),
		DefaultValue(ButtonTypes.Graphic),
		Description("Determines whether the Up-Level search button displays as a string or a graphic."),
		RefreshProperties(RefreshProperties.All)
		]
		public ButtonTypes UpLevelSearchButtonType
		{
			get
			{
				this.EnsureChildControls();
				return upLevelSearchButtonType;
			}
			set
			{
				this.EnsureChildControls();
				upLevelSearchButtonType = value;
				if (value.Equals(ButtonTypes.Graphic))
				{
					if (DesignMode)
						ulbSearchButton.InnerHtml="<img src=\""+serverDirDesignMode+"DbComboServer.aspx?DownArrow\" style=\"vertical-align:middle;\" />";
					else
						ulbSearchButton.InnerHtml="<img src=\""+ServerDir+"DbComboServer.aspx?DownArrow\" style=\"vertical-align:middle;\" />";
				}
				else
					ulbSearchButton.InnerText = TextUpLevelSearchButton;
			}
		}
		private ButtonTypes upLevelSearchButtonType;
		private ButtonTypes upLevelSearchButtonTypeDefault = ButtonTypes.Graphic;
		#endregion

		#region GetDownLevelState
		/// <summary>
		/// This event is used to generate the state hashtable in downlevel browser mode. The event should take a DbCombo as a parameter, and return a Hashtable.
		/// </summary>
		public event DownLevelStateDelegate GetDownLevelState;
		#endregion

		#region public event EventHandler SelectedItemChanged
		/// <summary>
		/// This event fires on the server when the selected item has changed since the last postback.
		/// </summary>
		public event EventHandler SelectedItemChanged;
		#endregion

		#region NewVersionAvailable
		/// <summary>
		/// Used by the designer to persist whether a new version is available.
		/// </summary>
		[
		Browsable(false),
		DefaultValue(false),
		Description("Used by the designer to persist whether a new version is available."),
		]
		public bool NewVersionAvailable{get{return this.newVersionAvailable;}set{this.newVersionAvailable=value;}} private bool newVersionAvailable=false;
		#endregion

		#region ErrorBoxType
		/// <summary>
		/// This allows you to specify the way that DbCombo handles errors on the server.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(ErrorBoxTypes.Auto),
		Description("This allows you to specify the way that DbCombo handles errors on the server."),
		]
		public ErrorBoxTypes ErrorBoxType
		{
			get
			{
				this.EnsureChildControls();
				return errorBoxType;
			}
			set
			{
				this.EnsureChildControls();
				errorBoxType = value;
				ulbErrorBoxType.Text = value.ToString();
			}
		}
		private ErrorBoxTypes errorBoxType = ErrorBoxTypes.Auto;
		private ErrorBoxTypes errorBoxTypeDefault = ErrorBoxTypes.Auto;
		#endregion
		#region ErrorBoxCustomText
		/// <summary>
		/// If ErrorBoxType = Custom, this string is used in the pop-up dialogue for all errors.
		/// </summary>
		[
		Bindable(true),
		Category("Misc - UI behaviour"),
		DefaultValue(""),
		Description("If ErrorBoxType = Custom, this string is used in the pop-up dialogue for all errors.")
		]
		public string ErrorBoxCustomText
		{
			get
			{
				this.EnsureChildControls();
				return errorBoxCustomText;
			}
			set
			{
				this.EnsureChildControls();
				errorBoxCustomText = value;
				ulbErrorBoxCustomText.Text = value;
			}
		}
		string errorBoxCustomText="";
		string errorBoxCustomTextDefault="";
		#endregion

		#endregion

		#region LoadViewState
		/// <summary>
		/// Loads persisted values from the viewstate
		/// </summary>
		/// <param name="savedState"></param>
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);

			if (this.ViewState["ForceDownLevel"]!=null)				ForceDownLevel=(bool)this.ViewState["ForceDownLevel"];
			if (this.ViewState["ServerAssembly"]!=null)				ServerAssembly=(string)this.ViewState["ServerAssembly"];
			if (this.ViewState["ServerType"]!=null)					ServerType=(string)this.ViewState["ServerType"];
			if (this.ViewState["ServerMethod"]!=null)				ServerMethod=(string)this.ViewState["ServerMethod"];
			if (this.ViewState["ClientStateFunction"]!=null)		ClientStateFunction=(string)this.ViewState["ClientStateFunction"];
			if (this.ViewState["ClientOnSelectFunction"]!=null)		ClientOnSelectFunction=(string)this.ViewState["ClientOnSelectFunction"];
			
			if (this.ViewState["Debug"]!=null)						Debug=(bool)this.ViewState["Debug"];
			if (this.ViewState["Latency"]!=null)					Latency=(int)this.ViewState["Latency"];
			if (this.ViewState["ServerDir"]!=null)					ServerDir=(string)this.ViewState["ServerDir"];
			if (this.ViewState["TextBoxColumns"]!=null)				TextBoxColumns=(int)this.ViewState["TextBoxColumns"];
			if (this.ViewState["DropDownRows"]!=null)				DropDownRows=(int)this.ViewState["DropDownRows"];
			
			if (this.ViewState["ShowUpLevelHelpButton"]!=null)		ShowUpLevelHelpButton=(bool)this.ViewState["ShowUpLevelHelpButton"];
			if (this.ViewState["ShowDownLevelHelpButton"]!=null)	ShowDownLevelHelpButton=(bool)this.ViewState["ShowDownLevelHelpButton"];
			//			if (this.ViewState["ShowRevertButton"]!=null)			ShowRevertButton=(bool)this.ViewState["ShowRevertButton"];
			if (this.ViewState["ShowUpLevelSearchButton"]!=null)	ShowUpLevelSearchButton=(bool)this.ViewState["ShowUpLevelSearchButton"];

			if (this.ViewState["UpLevelSearchButtonType"]!=null)	UpLevelSearchButtonType=(ButtonTypes)this.ViewState["UpLevelSearchButtonType"];
			
			if (this.Enabled==false)
			{
				Text=this.ViewState["Text"].ToString();
				Value=this.ViewState["Value"].ToString();
			}

			#region Persist our ForceDownLevel if we are reverting
			if (Page.IsPostBack && Page.Request.Form["__EVENTTARGET"]==this.UniqueID && Page.Request.Form["__EVENTARGUMENT"]=="revert")
			{
				ForceDownLevel=true;
			}
			#endregion
		}
		#endregion

		#region SaveViewState
		/// <summary>
		/// Saves persisted values in the viewstate
		/// </summary>
		/// <returns></returns>
		protected override object SaveViewState()
		{
			if (ForceDownLevel != forceDownLevelDefault)						this.ViewState["ForceDownLevel"]=ForceDownLevel;
			if (Debug != debugDefault)											this.ViewState["Debug"]=Debug;
			if (Latency != latencyDefault)										this.ViewState["Latency"]=Latency;
			if (ServerDir != serverDirDefault)									this.ViewState["ServerDir"]=ServerDir;
			if (TextBoxColumns != textBoxColumnsDefault)						this.ViewState["TextBoxColumns"]=TextBoxColumns;
			if (DropDownRows != dropDownRowsDefault)							this.ViewState["DropDownRows"]=DropDownRows;
			if (ServerAssembly != serverAssemblyDefault)						this.ViewState["ServerAssembly"]=ServerAssembly;
			if (ServerType != serverTypeDefault)								this.ViewState["ServerType"]=ServerType;
			if (ServerMethod != serverMethodDefault)							this.ViewState["ServerMethod"]=ServerMethod;
			if (ClientStateFunction != clientStateFunctionDefault)				this.ViewState["ClientStateFunction"]=ClientStateFunction;
			if (ClientOnSelectFunction != clientOnSelectFunctionDefault)		this.ViewState["ClientOnSelectFunction"]=ClientOnSelectFunction;
			

			if (ShowUpLevelHelpButton != showUpLevelHelpButtonDefault)			this.ViewState["ShowUpLevelHelpButton"]=ShowUpLevelHelpButton;
			if (ShowDownLevelHelpButton != showDownLevelHelpButtonDefault)		this.ViewState["ShowDownLevelHelpButton"]=ShowDownLevelHelpButton;
			//			if (ShowRevertButton != showRevertButtonDefault)		this.ViewState["ShowRevertButton"]=ShowRevertButton;
			if (ShowUpLevelSearchButton != showUpLevelSearchButtonDefault)		this.ViewState["ShowUpLevelSearchButton"]=ShowUpLevelSearchButton;

			if (UpLevelSearchButtonType.Equals(upLevelSearchButtonTypeDefault)) this.ViewState["UpLevelSearchButtonType"]=UpLevelSearchButtonType;

			if (this.Enabled==false)
			{
				this.ViewState["Text"]=Text;
				this.ViewState["Value"]=Value;
			}
			this.ViewState["PrevText"]=Text;
			this.ViewState["PrevValue"]=Value;

			return base.SaveViewState();
		}
		#endregion

		#region DownLevel and ClientDownLevel
		/// <summary>
		/// This read-only bool shows whether the control should render in DownLevel mode
		/// </summary>
		protected bool DownLevel
		{
			get
			{
				if (ForceDownLevel || ClientDownLevel)
					return true;
				else
					return false;
			}
		}
		bool ClientDownLevel
		{
			get
			{
				if (!DesignMode)
					return !(HttpContext.Current.Request.Browser.Browser.IndexOf("IE")>-1 && HttpContext.Current.Request.Browser.MajorVersion>=5 && HttpContext.Current.Request.Browser.Platform.StartsWith("Win"));
				else
					return false;
			}
		}
		#endregion

		#region Define our dlb methods
		#region ClearValue
		/// <summary>
		/// this event clears the value of the control
		/// </summary>
		/// <param name="o"></param>
		/// <param name="e"></param>
		internal void ClearValue(object o, EventArgs e)
		{
			Value="";
			Text="";
			dlbListBox.SelectedIndex=-1;
			dlbResultsHolder.Visible=false;
			dlbMoreResultsButton.Visible=false;
			dlbSelectButton.Visible=false;
			dlbNoResultsLabel.Visible=false;
		}
		#endregion
		#region dlbMoreResultsButtonClick
		internal void dlbMoreResultsButtonClick(object o, System.EventArgs e)
		{
			int dlbRows=Int32.Parse(dlbRowsHidden.Value);
			dlbRows=dlbRows*2;
			dlbRowsHidden.Value=dlbRows.ToString();
			dlbSearch(dlbRows);
			dlbResultsHolder.Visible=true;
		}
		#endregion
		#region dlbSearchButtonClick
		internal void dlbSearchButtonClick(object o, System.EventArgs e)
		{
			int dlbRows=DropDownRows*2;
			dlbRowsHidden.Value=dlbRows.ToString();
			dlbSearch(dlbRows);
		}
		#endregion
		#region dlbSelectButtonClick
		internal void dlbSelectButtonClick(object o, EventArgs e)
		{
			if (dlbListBox.SelectedIndex!=-1)
			{
				Value = dlbListBox.SelectedItem.Value;
				Text = dlbListBox.SelectedItem.Text;
				dlbResultsHolder.Visible=false;
				dlbMoreResultsButton.Visible=false;
				dlbSelectButton.Visible=false;
			}
		}
		#endregion
		#region dlbStoreResult
		void dlbStoreResult(XmlDocument xmlDoc, XmlNode responseTag, string val, string text)
		{
			dlbListBox.Items.Add(new ListItem(text,val));
		}
		#endregion
		#region dlbSearch
		internal void dlbSearch(int dlbRows)
		{
			dlbListBox.Items.Clear();

			Hashtable stateHash;
			if (GetDownLevelState==null)
			{
				stateHash=null;
			}
			else
			{
				stateHash=GetDownLevelState(this);
			}

			DbCombo.StoreResult resDel = new DbCombo.StoreResult(dlbStoreResult);
			bool moreRowsAvailable = StoreResults(
				dlbRows, 
				Text, 
				0, 
				ServerAssembly, 
				ServerType,  
				ServerMethod,
				DataMember,
				DataValueField,
				DataTextField,
				this.Parent.GetType().Assembly.GetName().Name,
				this.Parent.GetType().ToString(),
				this.Parent.GetType().BaseType.Assembly.GetName().Name,
				this.Parent.GetType().BaseType.ToString(),
				this.Page.GetType().Assembly.GetName().Name,
				this.Page.GetType().ToString(),
				this.Page.GetType().BaseType.Assembly.GetName().Name,
				this.Page.GetType().BaseType.ToString(),
				resDel, 
				null, 
				null,
				stateHash,
				ServerState,
				"",
				"",
				false
				);
			if (dlbListBox.Items.Count>=DropDownRows)
			{
				dlbListBox.Rows=DropDownRows;
				dlbResultsHolder.Visible=true;
				dlbNoResultsLabel.Visible=false;
				dlbSelectButton.Visible=true;
			}
			else
			{
				if (dlbListBox.Items.Count==0)
				{
					dlbListBox.Rows=2;
					dlbResultsHolder.Visible=false;
					dlbSelectButton.Visible=false;
					dlbNoResultsLabel.Visible=true;
				}
				else if (dlbListBox.Items.Count==1)
				{
					dlbListBox.Rows=2;
					dlbResultsHolder.Visible=true;
					dlbNoResultsLabel.Visible=false;
					dlbSelectButton.Visible=true;
				}
				else
				{
					dlbListBox.Rows=dlbListBox.Items.Count;
					dlbResultsHolder.Visible=true;
					dlbNoResultsLabel.Visible=false;
					dlbSelectButton.Visible=true;
				}
			}

			if (moreRowsAvailable)
			{
				dlbMoreResultsButton.Visible=true;
			}
			else
			{
				dlbMoreResultsButton.Visible=false;
			}
		}
		#endregion
		#endregion

		#region Back-end stuff & misc

		#region Define our GetResults delegate
		internal delegate object GetResults(ServerMethodArgs args);
		#endregion

		#region Define our StoreResult delegate
		internal delegate void StoreResult(XmlDocument xmlDoc, XmlNode responseTag, string val, string text);
		#endregion

		
		#region GetMethod - this tries to get a methodInfo object of the specified method
		internal static bool GetMethod(
			string serverAssembly, 
			string serverType, 
			string serverMethod, 
			ref bool attributeMissingException,
			ref MethodInfo methodInfo)
		{
			System.Reflection.Assembly assembly;
			Type type;
			System.Reflection.MethodInfo method;
			Attribute[] attribs;
			try
			{
				assembly = System.Reflection.Assembly.LoadWithPartialName(serverAssembly);
				type = assembly.GetType(serverType);
				method = type.GetMethod(serverMethod);
				attribs = Attribute.GetCustomAttributes(method);
			}
			catch
			{
				return false;
			}
			
			bool MethodOk=false;
			foreach(Attribute current in attribs)
			{
				if (current is ResultsMethodAttribute)
				{
					ResultsMethodAttribute thisAttribute = (ResultsMethodAttribute)current;
					if (thisAttribute.AllowResults)
						MethodOk=true;
				}
			}
			if (!MethodOk)
			{
				attributeMissingException=true;
				return false;
			}
			else
			{
				methodInfo = method;
				return true;
			}
		}
		#endregion

		#region Define out StoreResults method
		internal static bool StoreResults(
			int rows, 
			string query, 
			int skip, 
			string serverAssembly, 
			string serverType, 
			string serverMethod,
			string dataMember,

			string dataValueField,
			string dataTextField,

			string parentAssembly,
			string parentType,
			string parentBaseAssembly,
			string parentBaseType,

			string pageAssembly,
			string pageType,
			string pageBaseAssembly,
			string pageBaseType,

			StoreResult StoreResultDel, 
			XmlDocument xmlDoc, 
			XmlNode responseTag,
			Hashtable clientState,
			Hashtable serverState,
			string serverStateString,
			string serverStateHash,
			bool upLevel)
		{
			bool foundMethod = false;
			bool attributeMissingException=false;
			MethodInfo methodInfo = null;

			#region Find the method - bit messy, could be improved?
			if (serverAssembly!="")//assembly specified
			{
				if (serverType!="")//type specified
				{
					foundMethod = GetMethod(serverAssembly, serverType, serverMethod, ref attributeMissingException, ref methodInfo); //try with specified assembly and specified type
				}
			}
			if (serverType!="" &! foundMethod)//type specified
			{
				foundMethod = GetMethod(pageAssembly, serverType, serverMethod, ref attributeMissingException, ref methodInfo); //try with page assembly and specified type
				if(!foundMethod)
					foundMethod = GetMethod(pageBaseAssembly, serverType, serverMethod, ref attributeMissingException, ref methodInfo); //try with page codebehind assembly and specified type
				if(!foundMethod)
					foundMethod = GetMethod(parentAssembly, serverType, serverMethod, ref attributeMissingException, ref methodInfo); //try with parent assembly and specified type
				if(!foundMethod)
					foundMethod = GetMethod(parentBaseAssembly, serverType, serverMethod, ref attributeMissingException, ref methodInfo); //try with parent codebehind assembly and specified type
			}
			if(!foundMethod)
				foundMethod = GetMethod(pageAssembly, pageType, serverMethod, ref attributeMissingException, ref methodInfo); //try with page assembly and page type
			if(!foundMethod)
				foundMethod = GetMethod(pageBaseAssembly, pageBaseType, serverMethod, ref attributeMissingException, ref methodInfo); //try with page codebehind assembly and page codebehind type
			if(!foundMethod)
				foundMethod = GetMethod(parentAssembly, parentType, serverMethod, ref attributeMissingException, ref methodInfo); //try with parent assembly and parent type
			if(!foundMethod)
				foundMethod = GetMethod(parentBaseAssembly, parentBaseType, serverMethod, ref attributeMissingException, ref methodInfo); //try with parent codebehind assembly and parent codebehind type

			if(!foundMethod && attributeMissingException)
				throw new Exception("ServerMethod is not tagged with '[Cambro.Web.DbCombo.ResultsMethodAttribute(true)]' attribute.");

			if(!foundMethod)
				throw new Exception("Can't find ServerMethod.");
			#endregion

			#region Convert the method to a GetResults delegate, and throw a nice exception if it doesn't convert
			DbCombo.GetResults mydel;
			try
			{
				mydel = (DbCombo.GetResults) Delegate.CreateDelegate(typeof(DbCombo.GetResults),methodInfo);
			}
			catch(Exception ex)
			{
				throw new Exception("Error while getting results - I've found your ServerMethod, but an exception was thrown while converting it to a DbCombo.GetResults delegate. Are you sure your ServerMethod has the signature: public static object YourServerMethodName(Cambro.Web.DbCombo.ServerMethodArgs args)? ",ex);
			}
			#endregion

			#region Populate our Secure Server State hashtable
			SecureHashtable serverStateSecure = new SecureHashtable();
			serverStateSecure.HashHash=serverStateHash;
			serverStateSecure.Downlevel=!upLevel;
			serverStateSecure.HashSerial=serverStateString;
			if (serverState != null)
			{
				foreach(object c in serverState.Keys)
				{
					serverStateSecure.Add(c,serverState[c]);
				}
			}
			#endregion
			
			FieldSecurityClass fieldSecurity = new FieldSecurityClass();
			ArrayList fieldSubset = new ArrayList();

			DataMemberSecurityClass dataMemberSecurity = new DataMemberSecurityClass();
			ArrayList dataMemberSubset = new ArrayList();

			#region Build our ServerMethodArgs and execute our delegate
			ServerMethodArgs a = new ServerMethodArgs(
				query,
				skip+rows+2,
				clientState,
				serverStateSecure,
				upLevel,
				fieldSecurity,
				fieldSubset,
				dataMemberSecurity,
				dataMemberSubset);
			object result1 = mydel(a);
			#endregion

			object result = new object();
			object iListObj = new object();

			#region Throw an exception if we are trying to use a field that is not alowed by the fieldsecurity.
			if ( fieldSecurity.Value.Equals(FieldSecurity.Default) )
			{
				if ( dataValueField!="" || dataTextField!="" )
					throw new Exception("You have specified either DataValueField or DataTextField, but have not specified FieldSecurity in the ServerMethod. Add 'args.FieldSecurity.Value = FieldSecurity.xxx' to your ServerMethod, where xxx is either 'AllFields' or 'fieldSubset'");
			}
			else if ( fieldSecurity.Value.Equals(FieldSecurity.IncludeFieldSubset) )
			{
				string valueField="DbComboValue";
				string textField="DbComboText";
				if (dataValueField!="")
					valueField = dataValueField;
				if (dataTextField!="")
					textField = dataTextField;
				if ( ! fieldSubset.Contains(valueField) )
					throw new Exception("You have specified FieldSecurity=IncludeFieldSubset in the ServerMethod, but your DataValueField ('"+valueField+"') does not occur in the ArrayList FieldSubset.");
				if ( ! fieldSubset.Contains(textField) )
					throw new Exception("You have specified FieldSecurity=IncludeFieldSubset in the ServerMethod, but your DataTextField ('"+textField+"') does not occur in the ArrayList FieldSubset.");
			}
			else if ( fieldSecurity.Value.Equals(FieldSecurity.ExcludeFieldSubset) )
			{
				string valueField="DbComboValue";
				string textField="DbComboText";
				if (dataValueField!="")
					valueField = dataValueField;
				if (dataTextField!="")
					textField = dataTextField;
				if ( fieldSubset.Contains(valueField) )
					throw new Exception("You have specified FieldSecurity=ExcludeFieldSubset in the ServerMethod, and your DataValueField ('"+valueField+"') occurs in the ArrayList FieldSubset.");
				if ( fieldSubset.Contains(textField) )
					throw new Exception("You have specified FieldSecurity=ExcludeFieldSubset in the ServerMethod, and your DataTextField ('"+textField+"') occurs in the ArrayList FieldSubset.");
			}	
			if (dataValueField=="")
				dataValueField = "DbComboValue";
			if (dataTextField=="")
				dataTextField = "DbComboText";
			#endregion
			#region Throw an exception if we are trying to use a DataMember that is not alowed by the DataMemberSecurity.
			if (dataMemberSecurity.Equals(DataMemberSecurity.Default))
			{
				if (dataMember!="")
					throw new Exception("You have specified a DataMember, but DataMemberSecurity is still set on Default. In your ServerMethod, please choose a diferent value for DataMemberSecurity.");
			}
			else if (dataMemberSecurity.Equals(DataMemberSecurity.IncludeDataMemberSubset))
			{
				if (!dataMemberSubset.Contains(dataMember))
					throw new Exception("You have specified a DataMember, and you have chosed DataMemberSecurity.IncludeDataMemberSubset. Your chosen DataMember does not occur in the ArrayList DataMemberSubset.");
			}
			else if (dataMemberSecurity.Equals(DataMemberSecurity.ExcludeDataMemberSubset))
			{
				if (dataMemberSubset.Contains(dataMember))
					throw new Exception("You have specified a DataMember, and you have chosed DataMemberSecurity.ExcludeDataMemberSubset. Your chosen DataMember occurs in the ArrayList DataMemberSubset.");
			}
			#endregion

			bool moreRowsAvailable=false;
			int counter=0;

			#region Extract an IList (if we can)
			bool useIList = true;
			if (result1 is DataSet)
			{
				if (dataMember.Length>0)
					iListObj = ((IListSource)(object)((DataSet)result1).Tables[dataMember]).GetList();
				else
					iListObj = ((IListSource)(object)((DataSet)result1).Tables[0]).GetList();
			}
			else if (result1 is IListSource)
				iListObj = ((IListSource)result1).GetList();
			else if (result1 is IList)
				iListObj = (IList)result1;
			else
				useIList = false;
			#endregion

			if (useIList)
			{
				#region This is the new DataBinding code (>=v4.1) - it handles custom objects, but not DataReaders.
				IList iList = (IList)iListObj;
				int maxVar = rows+skip;
				if (rows+skip>iList.Count)
					maxVar = iList.Count;
				for(int i=skip;i<maxVar;i++)
				{
					string thisValue = GetField(iList[i],dataValueField);
					string thisText  = GetField(iList[i],dataTextField);
					StoreResultDel(xmlDoc,responseTag,thisValue,thisText);
				}
				if (iList.Count>rows+skip)
					return true;
				else
					return false;
				#endregion
			}
			else
			{
				#region This is the old DataDinding code (<v4.1) - it handles DataReaders.
				if (result1 is DataSet)
				{
					result = (object)((DataSet)result1).Tables[0].DefaultView;
				}
				else
				{
					result = result1;
				}
				if (result is IEnumerable)
				{
					IEnumerator iEnumerator = ((IEnumerable)result).GetEnumerator();
					while (counter<skip && iEnumerator.MoveNext())
					{
						counter++;
					}
					while (counter<(rows+skip) && iEnumerator.MoveNext())
					{
						string val = GetField(iEnumerator.Current,dataValueField);
						string text = GetField(iEnumerator.Current,dataTextField);
						StoreResultDel(xmlDoc,responseTag,val,text);
						counter++;
					}
					try
					{
						moreRowsAvailable=iEnumerator.MoveNext();
					}
					catch
					{
						moreRowsAvailable=false;
					}
				
				}
				else 
				{
					throw new Exception("Returned object is not DataSet or IListSource or IList or IEnumerable");
				}
				if (result is IDisposable)
				{
					IDisposable dis = (IDisposable)result;
					dis.Dispose();
				}
				#endregion
			}
		
			#region Old DataBinding code (removed)
			/*
			if (result1 is DataSet)
			{
				result = (object)((DataSet)result1).Tables[0].DefaultView;
			}
			else
			{
				result = result1;
			}
			if (result is IEnumerable)
			{
				IEnumerator iEnumerator = ((IEnumerable)result).GetEnumerator();
				while (counter<skip && iEnumerator.MoveNext())
				{
					counter++;
				}
				while (counter<(rows+skip) && iEnumerator.MoveNext())
				{
					string val;
					string text;
					#region extract the data record
					if (iEnumerator.Current is IDataRecord)
					{
						IDataRecord current = (IDataRecord)iEnumerator.Current;
						val = current[dataValueField].ToString();
						text = current[dataTextField].ToString();
					}
					else if (iEnumerator.Current is DataRowView)
					{
						DataRowView current = (DataRowView)iEnumerator.Current;
						val = current[dataValueField].ToString();
						text = current[dataTextField].ToString();
					}
					else if (iEnumerator.Current is IDictionary)
					{
						IDictionary current = (IDictionary)iEnumerator.Current;
						val = current[dataValueField].ToString();
						text = current[dataTextField].ToString();
					}
					else if (iEnumerator.Current is IEnumerable)
					{
						IEnumerator ie = ((IEnumerable)iEnumerator.Current).GetEnumerator();
						ie.MoveNext();
						val = ie.Current.ToString();
						ie.MoveNext();
						text = ie.Current.ToString();
					}
					else
					{
						throw new Exception("Returned Object's IEnumerator.Current is not IDataRecord, DataRowView, IDictionary or IEnumerable");
					}
					#endregion
					StoreResultDel(xmlDoc,responseTag,val,text);
					counter++;
				}
				try
				{
					moreRowsAvailable=iEnumerator.MoveNext();
				}
				catch
				{
					moreRowsAvailable=false;
				}
				
			}
			else 
			{
				throw new Exception("returned object is not IEnumerable");
			}

			if (result is IDisposable)
			{
				IDisposable dis = (IDisposable)result;
				dis.Dispose();
			}
			*/
			#endregion

			return moreRowsAvailable;
		}
		#endregion

		#region GetField - this is used in StoreResults to get a field from a data row or object (includes code to use reflection to get values from properties and fields of custom objects)
		internal static string GetField(object obj, string FieldName)
		{
			if (obj is DataRowView)
				return ((DataRowView)obj)[FieldName].ToString();
			else if (obj is IDataRecord)
				return ((IDataRecord)obj)[FieldName].ToString();
			else if (obj is IDictionary)
				return ((IDictionary)obj)[FieldName].ToString();
			else if (obj is ValueType && obj.GetType().IsPrimitive)
				return obj.ToString();
			else if (obj is string)
				return (string)obj;
			else
			{
				try
				{
					Type sourceType = obj.GetType();
					// See if the field is a property.
					PropertyInfo prop = sourceType.GetProperty(FieldName);
					if (prop == null || ! prop.CanRead)
					{
						//no readable property - check for a field
						FieldInfo field = sourceType.GetField(FieldName);
						if (field == null)
							//no field name exists either
							//return "No such field " + FieldName;
							throw new Exception("No such field or property " + FieldName + " in datasource.");
						else
							//got a field, return it's value
							return field.GetValue(obj).ToString();
					}
					else
						//found a property, return it's value
						return prop.GetValue(obj,null).ToString();
				}
				catch(Exception ex)
				{
					throw new Exception("Complex object member reflection failure",ex);
				}
			}
		}
		#endregion
		
		#region TieButton
		/// <summary>
		///     This ties a textbox to a button. 
		/// </summary>
		/// <param name="TextBoxToTie">
		///     This is the textbox to tie to. It doesn't have to be a TextBox control, but must be derived from either HtmlControl or WebControl,
		///     and the html control should accept an 'onkeydown' attribute.
		/// </param>
		/// <param name="ButtonToTie">
		///     This is the button to tie to. All we need from this is it's ClientID. The Html tag it renders should support click()
		/// </param>
		internal static void TieButton(Control TextBoxToTie, Control ButtonToTie)
		{
			string formName;
			try
			{
				int i=0;
				Control c = ButtonToTie.Parent;
				// Step up the control heirachy until either:
				// 1) We find an HtmlForm control
				// 2) We find a Page control - not what we want, but we should stop searching because we a Page will be higher than the HtmlForm.
				// 3) We complete 500 itterations. Obviously we are in a loop, and should stop.
				while(! (c is System.Web.UI.HtmlControls.HtmlForm) &! (c is System.Web.UI.Page) && i<500)
				{
					c=c.Parent;
					i++;
				}
				// If we have found an HtmlForm, we use it's ClientID for the formName.
				// If not, we use the first form on the page ("forms[0]").
				if (c is System.Web.UI.HtmlControls.HtmlForm)
					formName = c.ClientID;
				else
					formName = "forms[0]";
			}
			catch
			{
				//If we catch an exception, we should use the first form on the page ("forms[0]").
				formName = "forms[0]";
			}
			// Tie the button.
			TieButton(TextBoxToTie, ButtonToTie, formName);
		}
		/// <summary>
		///     This ties a textbox to a button. 
		/// </summary>
		/// <param name="TextBoxToTie">
		///     This is the textbox to tie to. It doesn't have to be a TextBox control, but must be derived from either HtmlControl or WebControl,
		///     and the html control should accept an 'onkeydown' attribute.
		/// </param>
		/// <param name="ButtonToTie">
		///     This is the button to tie to. All we need from this is it's ClientID. The Html tag it renders should support click()
		/// </param>
		/// <param name="formName">
		///     This is the ClientID of the form that the button resides in.
		/// </param>
		internal static void TieButton(Control TextBoxToTie, Control ButtonToTie, string formName)
		{
			// This is our javascript - we fire the client-side click event of the button if the enter key is pressed.
			string jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document."+formName+".elements['"+ButtonToTie.UniqueID+"'].click();return false;} else return true; ";
			// We attach this to the onkeydown attribute - we have to cater for HtmlControl or WebControl.
			if (TextBoxToTie is System.Web.UI.HtmlControls.HtmlControl)
				((System.Web.UI.HtmlControls.HtmlControl)TextBoxToTie).Attributes.Add("onkeydown",jsString);
			else if (TextBoxToTie is System.Web.UI.WebControls.WebControl)
				((System.Web.UI.WebControls.WebControl)TextBoxToTie).Attributes.Add("onkeydown",jsString);
			else
			{
				// We throw an exception if TextBoxToTie is not of type HtmlControl or WebControl.
				throw new ArgumentException("Control TextBoxToTie should be derived from either System.Web.UI.HtmlControls.HtmlControl or System.Web.UI.WebControls.WebControl", "TextBoxToTie");
			}
		}
		#endregion
		
		#region ProVersionRequiredException
		internal class ProVersionRequiredException : Exception
		{
			internal ProVersionRequiredException() : base("You have attempted to use a function that is only available in DbCombo Pro edition. Please see www.dbcombo.net for a new registration key.")
			{
			}
		}
		#endregion

		#region enum ButtonTypes
		/// <summary>
		/// Button types - Graphic or Text
		/// </summary>
		public enum ButtonTypes
		{
			/// <summary>
			/// Graphic buttons display a graphic.
			/// </summary>
			Graphic,
			/// <summary>
			/// Text buttons display a string.
			/// </summary>
			Text
		}
		#endregion

		#region enum ValidationProperties
		/// <summary>
		/// Validation can occur on either the text of value properties of DbCombo.
		/// </summary>
		public enum ValidationProperties
		{
			/// <summary>
			/// Value property is validated on. The value will always be blank if an item is not selected from the drop-down.
			/// </summary>
			Value,
			/// <summary>
			/// Text property is validated on.
			/// </summary>
			Text
		}
		#endregion

		#region enum ErrorBoxTypes
		/// <summary>
		/// This allows you to specify the way that DbCombo handles errors on the server.
		/// </summary>
		public enum ErrorBoxTypes
		{
			/// <summary>
			/// If an error occurs on the server, DbCombo will display a pop-up dialogue detailing the error.
			/// </summary>
			Auto,
			/// <summary>
			/// If an error occurs on the server, DbCombo will ignore the error.
			/// </summary>
			None,
			/// <summary>
			/// If an error occurs on the server, DbCombo will display a pop-up dialogue with custom text in it. Adjust the CustomErrorText property to set the error text.
			/// </summary>
			Custom
		}
		#endregion

		#region Reset()
		/// <summary>
		/// This method resets the DbCombo. DbCombo will not perform a requery after this method is called.
		/// </summary>
		public void Reset()
		{
			this.Value="";
			this.Text="";
			this.ReQueryRecords=this.reQueryRecordsDefault;
			this.ReQueryText="";
			this.ReQueryOnLoad=reQueryDisabledDefault;
			this.ViewState["DbComboPreviousVisible"]=null;
		}
		#endregion

		#region en, de - encoder and decoder functions
		private static string en(object a)
		{
			String aString;
			MemoryStream aStream = new MemoryStream();
			try
			{
				BinaryFormatter aFormatter = new BinaryFormatter();
				aFormatter.Serialize(aStream,a);
				aString = System.Convert.ToBase64String(aStream.ToArray());
			}
			finally
			{
				aStream.Close();
			}
			return aString;
		}
		private static object de(string a)
		{
			object aObject;
			MemoryStream aStream;
			try
			{
				byte[] bytes = System.Convert.FromBase64String(a);
				aStream = new MemoryStream(bytes);
				BinaryFormatter aFormatter = new BinaryFormatter();
				aObject = aFormatter.Deserialize(aStream);
			}
			catch(Exception e)
			{
				throw new Exception("Can't decode string.",e);
			}
			return aObject;
		}
		#endregion

		#region GenRandomText
		private static string GenRandomText(int length)
		{
			System.Random randomOb = new System.Random();
			string tmpStr="";
			for(int i=0;i<length;i++)
			{
				int randomNumber=randomOb.Next(62);
				randomNumber=randomNumber+48;
				if (randomNumber>57)
					randomNumber=randomNumber+7;
				if (randomNumber>90)
					randomNumber=randomNumber+6;
				tmpStr+=((char)randomNumber).ToString();
			}
			return tmpStr;
		}
		#endregion

		#endregion

	}

	#region public class SimpleResult : ArrayList
	/// <summary>
	/// This generates a simple set of data that DbCombo will accept from a ServerMethod.
	/// </summary>
	public class SimpleResult : ArrayList
	{
		/// <summary>
		/// This generates a simple set of data that DbCombo will accept from a ServerMethod.
		/// </summary>
		/// <param name="FirstColumnName">The name of the first column (e.g. DbComboText)</param>
		/// <param name="FirstColumnValues">A string array containing the values of the members of the first column.</param>
		/// <param name="SecondColumnName">The name of the second column (e.g. DbComboValue)</param>
		/// <param name="SecondColumnValues">A string array containing the values of the members of the first column.</param>
		public SimpleResult(string FirstColumnName, string[] FirstColumnValues, string SecondColumnName, string[] SecondColumnValues)
		{
			if (FirstColumnValues.Length!=SecondColumnValues.Length)
				throw new Exception("FirstColumnValues.Length != SecondColumnValues.Length");
			for(int i=0;i<FirstColumnValues.Length;i++)
			{
				Hashtable h = new Hashtable();
				h.Add(FirstColumnName,FirstColumnValues[i]);
				h.Add(SecondColumnName,SecondColumnValues[i]);
				this.Add(h);
			}
		}
	}
	#endregion

	#region ServerMethodArgs
	/// <summary>
	/// This is the parameter for the ServerMethod function.
	/// </summary>
	public class ServerMethodArgs
	{
		/// <summary>
		/// This is the query that the user has entered that caused this event.
		/// </summary>
		public string Query{get{return this.query;}} private string query;
		/// <summary>
		/// This is the maximum number of records to return.
		/// </summary>
		public int Top{get{return this.top;}} private int top;
		/// <summary>
		/// This is the Hashtable of state returned by the client.
		/// </summary>
		public Hashtable ClientState{get{return this.clientState;}} private Hashtable clientState;
		/// <summary>
		/// This is the hashtable of state returned by the server. Remember to authenticate it with the Authenticate method.
		/// </summary>
		public SecureHashtable ServerState{get{return this.serverState;}} private SecureHashtable serverState;
		/// <summary>
		/// This is whether the browser is in up-level mode.
		/// </summary>
		public bool UpLevel{get{return this.upLevel;}} private bool upLevel;

		/// <summary>
		/// This determines which fields are available to the client. You only need 
		/// to change this if you have changed the DataValueField or DataTextField 
		/// properties. This field is necessary because the request comes from an 
		/// untrusted source. 
		/// </summary>
		public FieldSecurityClass FieldSecurity{get{return fieldSecurity;}set{fieldSecurity=value;}}private FieldSecurityClass fieldSecurity;
		/// <summary>
		/// If FieldSecurity is set to IncludeFieldSubset or ExcludeFieldSubset, 
		/// this arraylist contains all fields that are in the subset. Note: 
		/// 'DbComboValue' and 'DbComboText' are NOT included by defailt, and must 
		/// be added if needed.
		/// </summary>
		public ArrayList FieldSubset{get{return fieldSubset;}set{fieldSubset=value;}}private ArrayList fieldSubset;

		/// <summary>
		/// This determines which DataMembers are available to the client. You only 
		/// need to change this if you have specified a value for DataMember. This 
		/// field is necessary because the request comes from an untrusted source.
		/// </summary>
		public DataMemberSecurityClass DataMemberSecurity{get{return dataMemberSecurity;}set{dataMemberSecurity=value;}}private DataMemberSecurityClass dataMemberSecurity;
		/// <summary>
		/// If DataMemberSecurity is set to IncludeDataMemberSubset or 
		/// ExcludeDataMemberSubset, this arraylist contains all DataMembers that are 
		/// in the subset.
		/// </summary>
		public ArrayList DataMemberSubset{get{return dataMemberSubset;}set{dataMemberSubset=value;}}private ArrayList dataMemberSubset;

		internal ServerMethodArgs(string query, int top, Hashtable clientState, Cambro.Web.DbCombo.SecureHashtable serverState, bool upLevel, FieldSecurityClass fieldSecurity, ArrayList fieldSubset, DataMemberSecurityClass dataMamberSecurity, ArrayList dataMemberSubset )
		{
			this.query = query;
			this.top = top;
			this.clientState = clientState; 
			this.serverState = serverState;
			this.upLevel = upLevel;
			this.fieldSecurity = fieldSecurity;
			this.fieldSubset = fieldSubset;
			this.dataMemberSecurity = dataMemberSecurity;
			this.dataMemberSubset = dataMemberSubset;
		}
	}

	#region public enum FieldSecurity
	/// <summary>
	/// This enum determines which fields are available to the client. This field is necessary because the request comes from an untrusted source.
	/// </summary>
	public enum FieldSecurity
	{
		/// <summary>
		/// Default - mose secure - only the default DbComboValue and DbComboText fields are available to the client.
		/// </summary>
		Default,
		/// <summary>
		/// IncludeFieldSubset - Only fields contained in the field subset are available to the client. Add each required field to the ArrayList FieldSubset.
		/// </summary>
		IncludeFieldSubset,
		/// <summary>
		/// ExcludeFieldSubset - All fields are available to the client EXCEPT those contained in the field subset. Add each required field to the ArrayList FieldSubset.
		/// </summary>
		ExcludeFieldSubset,
		/// <summary>
		/// AllFields - least secure - all fields are available to the client.
		/// </summary>
		AllFields
	}
	#endregion
	#region public class FieldSecurityClass
	/// <summary>
	/// Simple class used to hold the FieldSecurity value.
	/// </summary>
	public class FieldSecurityClass
	{
		/// <summary>
		/// The FieldSecurity value.
		/// </summary>
		public FieldSecurity Value{get{return _value;}set{_value=value;}}private FieldSecurity _value;
	}
	#endregion

	#region public enum DataMemberSecurity
	/// <summary>
	/// This enum determines which DataMembers are available to the client. This field is necessary 
	/// because the request comes from an untrusted source.
	/// </summary>
	public enum DataMemberSecurity
	{
		/// <summary>
		/// Default - mose secure - only the default DataMember is available to the client.
		/// </summary>
		Default,
		/// <summary>
		/// IncludeDataMemberSubset - Only DataMembers contained in the DataMember subset are available 
		/// to the client. Add each required DataMember to the ArrayList DataMemberSubset.
		/// </summary>
		IncludeDataMemberSubset,
		/// <summary>
		/// ExcludeFieldSubset - All DataMembers are available to the client EXCEPT those contained 
		/// in the DataMember subset. Add each excluded field to the ArrayList DataMemberSubset.
		/// </summary>
		ExcludeDataMemberSubset,
		/// <summary>
		/// AllDataMembers - least secure - all DataMembers are available to the client.
		/// </summary>
		AllDataMembers
	}
	#endregion
	#region public class DataMemberSecurityClass
	/// <summary>
	/// Simple class used to hold the DataMemberSecurity value.
	/// </summary>
	public class DataMemberSecurityClass
	{
		/// <summary>
		/// The DataMemberSecurity value.
		/// </summary>
		public DataMemberSecurity Value{get{return _value;}set{_value=value;}}private DataMemberSecurity _value;
	}
	#endregion
	#endregion

	#region SecureHashtable
	/// <summary>
	/// This is a hashtable that may be verified as authentic with the Authenticate method.
	/// </summary>
	public class SecureHashtable : Hashtable
	{
		internal string HashHash{get{return this.dataHash;}set{this.dataHash=value;}} private string dataHash;
		internal string HashSerial{get{return this.hashSerial;}set{this.hashSerial=value;}} private string hashSerial;
		internal bool Downlevel{get{return this.downlevel;}set{this.downlevel=value;}} private bool downlevel=false;

		#region Authenticate
		/// <summary>
		/// Use this method to verify that the Hashtable has not been tampered with while on the client. Always returns true when in Down-Level browser mode, and there is no round-trip to the browser.
		/// </summary>
		/// <param name="secretString">This should be the same as in your DbCombo control.</param>
		/// <returns>true if the data has not been tampered with, false if it has been. </returns>
		public bool Authenticate(string secretString)
		{
			if (Downlevel)
				return true;
			else if (this.Count==0)
				return true;
			else
			{
				string stringToHash = HashSerial+secretString;
				SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
				byte[] hashFromClientByteArray = sha1.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(stringToHash));
				string hashFromClientString = System.Text.ASCIIEncoding.ASCII.GetString(hashFromClientByteArray);
				string hash = en(hashFromClientString);
				if (hash == HashHash)
					return true;
				else
					return false;
			}
		}
		/// <summary>
		/// Use this method to verify that the Hashtable has not been tampered with while on the client. This overload uses the default secret string.
		/// </summary>
		/// <returns>true if the data has not been tampered with, false if it has been.</returns>
		public bool Authenticate()
		{
			return Authenticate("%$#^GFH*HGFfdghSGFDfjhjhgkf87680k,juyks2qretwdxbfvcKJl?:-0[-89yruyegfsdfdsgjh8543wDFSGG");
		}
		#endregion

		#region en, de - encoder and decoder functions
		private static string en(object a)
		{
			String aString;
			MemoryStream aStream = new MemoryStream();
			try
			{
				BinaryFormatter aFormatter = new BinaryFormatter();
				aFormatter.Serialize(aStream,a);
				aString = System.Convert.ToBase64String(aStream.ToArray());
			}
			finally
			{
				aStream.Close();
			}
			return aString;
		}
		private static object de(string a)
		{
			object aObject;
			MemoryStream aStream;
			try
			{
				byte[] bytes = System.Convert.FromBase64String(a);
				aStream = new MemoryStream(bytes);
				BinaryFormatter aFormatter = new BinaryFormatter();
				aObject = aFormatter.Deserialize(aStream);
			}
			catch(Exception e)
			{
				throw new Exception("Can't decode string.",e);
			}
			return aObject;
		}
		#endregion
	}
	#endregion

	#region DownLevelStateDelegate
	/// <summary>
	/// This is the delegate or the DownLevelStateFunction event
	/// </summary>
	public delegate Hashtable DownLevelStateDelegate(DbCombo sender);
	#endregion

	#region ResultsMethodAttribute
	/// <summary>
	/// This attribute tags a method as able to be executed by a request to the server page of DbCombo, and results returned to the web client. 
	/// </summary>
	[AttributeUsage(AttributeTargets.Method,AllowMultiple=false)]
	public class ResultsMethodAttribute : Attribute
	{
		/// <summary>
		/// This attribute tags a method as able to be executed by a request to the server page of DbCombo, and results returned to the web client. 
		/// </summary>
		/// <param name="allowResults">Set this to true to allow the method to be executed by DbComboServer</param>
		public ResultsMethodAttribute(bool allowResults) 
		{
			this.allowResults = allowResults;
		}
		private bool allowResults;
		/// <summary>
		/// Set this to true to allow the method to be executed by DbComboServer
		/// </summary>
		public bool AllowResults
		{
			get
			{
				return this.allowResults;
			}
		}
	}
	#endregion

	#region ServerPage - used as the server side of the client script, and as the .js page
	/// <summary>
	/// This is the page that delivers the xml results, and the JavaScript resource file.
	/// </summary>
	public class ServerPage : Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			int expiresTime = 9999;
			try
			{
				#region images
				if(HttpContext.Current.Request.QueryString.GetValues(0)[0]=="DownArrow")
				{
					#region respond with arrow image
					Response.ContentType="image/gif";
					Response.Expires=expiresTime;
					Response.Clear();
					Assembly assembly = Assembly.GetExecutingAssembly();
					System.IO.Stream imageStream = assembly.GetManifestResourceStream("Cambro.Web.DbCombo.arrow.gif");
					byte[] b=new byte[54];
					imageStream.Read(b,0,54);
					Response.BinaryWrite(b);
					return;
					#endregion
				}
				if(HttpContext.Current.Request.QueryString.GetValues(0)[0]=="UpArrow")
				{
					#region respond with arrow image
					Response.ContentType="image/gif";
					Response.Expires=expiresTime;
					Response.Clear();
					Assembly assembly = Assembly.GetExecutingAssembly();
					System.IO.Stream imageStream = assembly.GetManifestResourceStream("Cambro.Web.DbCombo.arrowUp.gif");
					byte[] b=new byte[54];
					imageStream.Read(b,0,54);
					Response.BinaryWrite(b);
					return;
					#endregion
				}
				if(HttpContext.Current.Request.QueryString.GetValues(0)[0]=="d")
				{
					#region respond with DbCombo button image
					Response.ContentType="image/gif";
					Response.Expires=expiresTime;
					Response.Clear();
					Assembly assembly = Assembly.GetExecutingAssembly();
					System.IO.Stream imageStream = assembly.GetManifestResourceStream("Cambro.Web.DbCombo.DbComboButton3.gif");
					byte[] b=new byte[849];
					imageStream.Read(b,0,849);
					Response.BinaryWrite(b);
					return;
					#endregion
				}
				if(HttpContext.Current.Request.QueryString.GetValues(0)[0]=="e")
				{
					#region respond with DbCombo button image
					Response.ContentType="image/gif";
					Response.Expires=expiresTime;
					Response.Clear();
					Assembly assembly = Assembly.GetExecutingAssembly();
					System.IO.Stream imageStream = assembly.GetManifestResourceStream("Cambro.Web.DbCombo.DbComboButton4.gif");
					byte[] b=new byte[838];
					imageStream.Read(b,0,838);
					Response.BinaryWrite(b);
					return;
					#endregion
				}
				if(HttpContext.Current.Request.QueryString.GetValues(0)[0]=="f")
				{
					#region respond with DbCombo button image
					Response.ContentType="image/gif";
					Response.Expires=expiresTime;
					Response.Clear();
					Assembly assembly = Assembly.GetExecutingAssembly();
					System.IO.Stream imageStream = assembly.GetManifestResourceStream("Cambro.Web.DbCombo.DbComboButton5-eeeeee-big-c.gif");
					byte[] b=new byte[864];
					imageStream.Read(b,0,864);
					Response.BinaryWrite(b);
					return;
					#endregion
				}

				#endregion
			}
			catch{}
			#region Get Xml from request
			bool requestXml=false;
			string debugStr="false";
			XmlDocument xmlResp = new XmlDocument();
			try
			{
				xmlResp.Load(Request.InputStream);
				requestXml=true;
			}
			catch
			{
				requestXml=false;
			}
			#endregion
			if (!requestXml)
			{
				#region validate query string hash bits (removed)
				/*	
					string timeStringPlusRnd = HttpContext.Current.Request.QueryString["a"]+" fdksjhaklhdsawmewqnm43bn3248kjhfgfdskgjhrewtmrenwrewtyoicxvbyiovcxuby4wswpwcdpevom";
					SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
					byte[] hash = sha1.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(timeStringPlusRnd));
					string hashFromClientComputed = System.Text.ASCIIEncoding.ASCII.GetString(hash);

					if (hashFromClientComputed != HttpContext.Current.Request.QueryString["b"])
					{
						throw (new Exception("QueryString corrupt, er=1"));
					}
	
					DateTime dateFromClient = System.DateTime.Parse(HttpContext.Current.Request.QueryString["a"]);
					if (dateFromClient.AddSeconds(6.0)<DateTime.Now || dateFromClient>DateTime.Now)
					{
						//Response.Write("history.go(0);function DbComboStringToEval(a,b,c,d,e){return(\"\")}function DbComboInit(a){return true;}");
						//Response.End();
						throw (new Exception("QueryString corrupt, er=2"));
					}
				*/	
				#endregion 
			
				#region respond with javascript include
				Response.ContentType="text/html";
				Response.Expires=expiresTime;
				Response.Clear();

				Assembly assembly = Assembly.GetExecutingAssembly();
			//	#if DEBUG
				System.IO.StreamReader textStream = new System.IO.StreamReader(assembly.GetManifestResourceStream("Cambro.Web.DbCombo.DbCombo.js"));
			//	#else
			//	System.IO.StreamReader textStream = new System.IO.StreamReader(assembly.GetManifestResourceStream("Cambro.Web.DbCombo.DbComboEncoded.js"));
			//	#endif
				
				string text = textStream.ReadToEnd();

				Response.Write(text);
				#endregion
			}
			else
			{
				#region respons with results
				try
				{
					#region respond with results
					#region get data out of xml
					debugStr = xmlResp.DocumentElement.GetElementsByTagName("debug")[0].InnerText;
					int rows=Int32.Parse(xmlResp.DocumentElement.GetElementsByTagName("rows")[0].InnerText);
					string query=xmlResp.DocumentElement.GetElementsByTagName("query")[0].InnerText;
					int skip=Int32.Parse(xmlResp.DocumentElement.GetElementsByTagName("skip")[0].InnerText);
					string serverAssembly=xmlResp.DocumentElement.GetElementsByTagName("serverAssembly")[0].InnerText;
					string serverType    =xmlResp.DocumentElement.GetElementsByTagName("serverType")[0].InnerText;
					string serverMethod  =xmlResp.DocumentElement.GetElementsByTagName("serverMethod")[0].InnerText;
					string dataMember    =xmlResp.DocumentElement.GetElementsByTagName("dataMember")[0].InnerText;

					string dataValueField  =xmlResp.DocumentElement.GetElementsByTagName("dataValueField")[0].InnerText;
					string dataTextField  =xmlResp.DocumentElement.GetElementsByTagName("dataTextField")[0].InnerText;
					
					string parentAssembly     =xmlResp.DocumentElement.GetElementsByTagName("parentAssembly")[0].InnerText;
					string parentType         =xmlResp.DocumentElement.GetElementsByTagName("parentType")[0].InnerText;
					string parentBaseAssembly =xmlResp.DocumentElement.GetElementsByTagName("parentBaseAssembly")[0].InnerText;
					string parentBaseType     =xmlResp.DocumentElement.GetElementsByTagName("parentBaseType")[0].InnerText;

					string pageAssembly       =xmlResp.DocumentElement.GetElementsByTagName("pageAssembly")[0].InnerText;
					string pageType           =xmlResp.DocumentElement.GetElementsByTagName("pageType")[0].InnerText;
					string pageBaseAssembly   =xmlResp.DocumentElement.GetElementsByTagName("pageBaseAssembly")[0].InnerText;
					string pageBaseType       =xmlResp.DocumentElement.GetElementsByTagName("pageBaseType")[0].InnerText;

					Hashtable serverState     =new Hashtable();
					string serverStateString  ="";
					string serverStateHash	  ="";
					if (xmlResp.DocumentElement.GetElementsByTagName("serverState")[0].InnerText.Length>0)
					{
						serverState        =(Hashtable)de(xmlResp.DocumentElement.GetElementsByTagName("serverState")[0].InnerText);
						serverStateString  =xmlResp.DocumentElement.GetElementsByTagName("serverState")[0].InnerText;
						serverStateHash    =xmlResp.DocumentElement.GetElementsByTagName("serverStateHash")[0].InnerText;
					}
					
					XmlNode clientState = xmlResp.DocumentElement.GetElementsByTagName("clientState")[0];
					Hashtable stateHashtable = new Hashtable();
					if (clientState.ChildNodes.Count>0)
					{
						for(int i=0;i<clientState.ChildNodes.Count;i++)
						{
							XmlNode stateItem = clientState.ChildNodes[i];
							string key = stateItem.ChildNodes[0].InnerText;
							string data = stateItem.ChildNodes[1].InnerText;
							stateHashtable[key]=data;
						}
					}
					if (stateHashtable.Count==0)
						stateHashtable=null;
					#endregion

					#region Prepare the response
					Response.ContentType="text/xml";
					Response.Expires=-1;
					Response.Clear();
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.AppendChild(xmlDoc.CreateElement("response"));
					XmlNode responseTag=xmlDoc.GetElementsByTagName("response")[0];
					#endregion

					DbCombo.StoreResult resDel;
					bool moreRowsAvailable;

					#region Get our results
					resDel = new DbCombo.StoreResult(StoreResultXmlDoc);
					moreRowsAvailable = DbCombo.StoreResults(
						rows,
						query,
						skip,
						serverAssembly,
						serverType,
						serverMethod,
						dataMember,
						dataValueField,
						dataTextField,
					
						parentAssembly, 
						parentType, 
						parentBaseAssembly, 
						parentBaseType,

						pageAssembly, 
						pageType, 
						pageBaseAssembly, 
						pageBaseType,

						resDel,
						xmlDoc,
						responseTag,
						stateHashtable,
						serverState,
						serverStateString,
						serverStateHash,
						true
						);
					#endregion

					#region Store the results in a the xml
					responseTag.PrependChild(createXmlNode(xmlDoc,"exception",""));

					if (moreRowsAvailable)
					{
						responseTag.PrependChild(createXmlNode(xmlDoc,"results","-1"));
					}
					else
					{
						responseTag.PrependChild(createXmlNode(xmlDoc,"results","0"));
					}
					#endregion

					Response.Write(xmlDoc.InnerXml);
					#endregion
				}
				catch (Exception exception)
				{
					#region Handle any exceptions
					if (debugStr=="-1")
						throw exception;
					else
					{
						Response.Expires=-1;
						Response.ContentType="text/xml";
						Response.Clear();
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.AppendChild(xmlDoc.CreateElement("response"));
						XmlNode responseTag=xmlDoc.GetElementsByTagName("response")[0];

						responseTag.PrependChild(createXmlNode(xmlDoc,"exception",exception.ToString()));

						Response.Write(xmlDoc.InnerXml);
					}
					#endregion
				}
				#endregion
			}
		}
		
		#region en, de - encoder and decoder functions
		private string en(object a)
		{
			String aString;
			MemoryStream aStream = new MemoryStream();
			try
			{
				BinaryFormatter aFormatter = new BinaryFormatter();
				aFormatter.Serialize(aStream,a);
				aString = System.Convert.ToBase64String(aStream.ToArray());
			}
			finally
			{
				aStream.Close();
			}
			return aString;
		}
		private object de(string a)
		{
			object aObject;
			MemoryStream aStream;
			try
			{
				byte[] bytes = System.Convert.FromBase64String(a);
				aStream = new MemoryStream(bytes);
				BinaryFormatter aFormatter = new BinaryFormatter();
				aObject = aFormatter.Deserialize(aStream);
			}
			catch(Exception e)
			{
				throw new Exception("Can't decode string.",e);
			}
			return aObject;
		}
		#endregion

		void StoreResultXmlDoc(XmlDocument xmlDoc, XmlNode responseTag, string val, string text)
		{
			responseTag.AppendChild(createXmlNode(xmlDoc,"a",val,"|"+text+"|"));
		}
		#region Helper functions
		private XmlElement createXmlNode(XmlDocument xmlDoc, string tagName, string valueInt, string data)
		{
			XmlElement xmlEle=xmlDoc.CreateElement(tagName);
			XmlAttribute iAttribute = xmlDoc.CreateAttribute("i");
			iAttribute.Value=valueInt.ToString();

			xmlEle.Attributes.Append(iAttribute);
			

			xmlEle.InnerText=data;
			return xmlEle;
		}
		private XmlElement createXmlNode(XmlDocument xmlDoc, string tagName, string data)
		{
			XmlElement xmlEle=xmlDoc.CreateElement(tagName);
			xmlEle.InnerText=data;
			return xmlEle;
		}
		#endregion
		#region Web Form Designer generated code
		private void Page_Init(object sender, EventArgs e)
		{
			InitializeComponent(); //Required by the ASP.NET Web Form Designer.
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public ServerPage()
		{
			Page.Init += new System.EventHandler(Page_Init);
		}
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
	#endregion

}

#region Cambro.Web.DbCombo.Design
namespace Cambro.Web.DbCombo.Design
{
	#region DbComboDesigner
	/// <summary>
	/// Designer - used internally when DbCombo is in Visual Studio .NET design-time mode
	/// </summary>
	public class DbComboDesigner : ControlDesigner
	{
		#region MyDbCombo
		DbCombo MyDbCombo
		{
			get
			{
				if (myDbCombo==null)
					myDbCombo=(DbCombo)Component;
				return myDbCombo;
			}
		}
		DbCombo myDbCombo;
		#endregion
			
		#region DbComboDesigner()
		/// <summary>
		/// Used internally when DbCombo is in Visual Studio .NET design-time mode
		/// </summary>
		public DbComboDesigner()
		{
		}
		#endregion

		#region Async code
		/// <summary>
		/// StartAsync
		/// </summary>
		/// <param name="MyDbCombo"></param>
		protected void StartAsync(DbCombo MyDbCombo)
		{
			AsyncMethodDelegate func = new AsyncMethodDelegate(AsyncMethod);
			IAsyncResult iar = func.BeginInvoke(MyDbCombo, null, null);
		}

		delegate void AsyncMethodDelegate(DbCombo MyDbCombo);

		/// <summary>
		/// AsyncMethod
		/// </summary>
		/// <param name="MyDbCombo"></param>
		public void AsyncMethod(DbCombo MyDbCombo)
		{
			try
			{
				if (!MyDbCombo.NewVersionAvailable)
				{
					net.dbcombo.www.wsLatestBuild service = new net.dbcombo.www.wsLatestBuild();
					string s = service.GetLatestBuild(1);
					string[] sAry = s.Split('.');
					int major = int.Parse(sAry[0]);
					int minor = int.Parse(sAry[1]);
					int build = int.Parse(sAry[2]);
					bool laterBuild = false;
					if (major>Assembly.GetExecutingAssembly().GetName().Version.Major)
						laterBuild = true;
					else if (major==Assembly.GetExecutingAssembly().GetName().Version.Major)
						if (minor>Assembly.GetExecutingAssembly().GetName().Version.Minor)
							laterBuild = true;
						else if (minor==Assembly.GetExecutingAssembly().GetName().Version.Minor)
							if (build>Assembly.GetExecutingAssembly().GetName().Version.Build)
								laterBuild = true;

					if (laterBuild)
					{
						MyDbCombo.NewVersionAvailable=true;
						MyDbCombo.ulbNewVersionDiv.Visible=true;
						MyDbCombo.ulbNewVersionDiv.InnerHtml="There is a newer version of DbCombo<br>available. Go to www.dbcombo.net to<br>upgrade to v"+s;

						this.UpdateDesignTimeHtml();
					}
				}
			}
			catch//(Exception e)
			{
				//System.Windows.Forms.MessageBox.Show(e.Message+"\n"+e.StackTrace);
			}
		}
		#endregion

		int previousHeight=0;
		int previousWidth=0;

		#region GetDesignTimeHtml
		/// <summary>
		/// Used internally when DbCombo is in Visual Studio .NET design-time mode
		/// </summary>
		/// <returns>n/a</returns>
		public override	string GetDesignTimeHtml()
		{
			try
			{
				if (MyDbCombo.Height.Value!=previousHeight || MyDbCombo.Width.Value!=previousWidth)
				{
					//mod height and width
					int proposedCols = 30;
					if (MyDbCombo.Width.Value>0)
						proposedCols = (int)Math.Floor((MyDbCombo.Width.Value - 14)/6.0);
					MyDbCombo.TextBoxColumns=proposedCols>1?proposedCols:1;

					int proposedRows = 10;
					if (MyDbCombo.Height.Value>0)
						proposedRows = (int)Math.Floor((MyDbCombo.Height.Value - 75)/16.0);
					MyDbCombo.DropDownRows=proposedRows>2?proposedRows:2;

					previousHeight=(int)MyDbCombo.Height.Value;
					previousWidth=(int)MyDbCombo.Width.Value;

				}

				this.IsDirty=true;
				
				StartAsync(MyDbCombo);

				Panel p = new Panel();
				p.Controls.Add(MyDbCombo);
				StringWriter t = new StringWriter();
				HtmlTextWriter w = new HtmlTextWriter(t);
				p.RenderControl(w);
				return t.ToString();
			}
			catch (Exception e)
			{
				return GetErrorDesignTimeHtml(e);
			}
		}
		#endregion

		#region GetErrorDesignTimeHtml
		/// <summary>
		/// Used internally when DbCombo is in Visual Studio .NET design-time mode
		/// </summary>
		/// <param name="e">n/a</param>
		/// <returns>n/a</returns>
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			string text=string.Format("There was an error and the control can't be displayed.<br>Exception: {0}<br>{1}",
				e.Message, e.StackTrace);
			return CreatePlaceHolderDesignTimeHtml(text);
		}
		#endregion

		#region GetRenderedHtml
		string GetRenderedHtml(Control c)
		{
			StringWriter t = new StringWriter();
			HtmlTextWriter w = new HtmlTextWriter(t);
			c.RenderControl(w);
			return t.ToString();
		}
		#endregion

		#region OnControlResize
		/// <summary>
		/// Used internally when DbCombo is in Visual Studio .NET design-time mode
		/// </summary>
		protected override void OnControlResize()
		{
			return;
		}
		#endregion

	}
	#endregion
}
#endregion

#region RegistrationDetails
internal class RegistrationDetails
{
	#region Constructor
	internal RegistrationDetails(){}
	internal RegistrationDetails(string registrationKey)
	{
		try
		{
			int y = DateTime.Now.Year;
			int m = DateTime.Now.Month;
			int d = DateTime.Now.Day;
			string host;
			if (HttpContext.Current!=null)
			{
				host = HttpContext.Current.Request.Url.Host.ToLower();
			}
			else
			{
				host="localhost";
			}
			string serverName = HttpContext.Current.Server.MachineName;


			this.registrationKey = registrationKey;
			string[] regParts = UrlDeSerialize(registrationKey).ToString().Split('|');

			Product = (Products)int.Parse(regParts[0]);
			MajorVersion = (int)int.Parse(regParts[1]);
			MinorVersion = (int)int.Parse(regParts[2]);
			Build = (int)int.Parse(regParts[3]);

			Edition = (EditionTypes)int.Parse(regParts[4]);
			LicenseType = (LicenseTypes)int.Parse(regParts[5]);
			UrlRestriction = (UrlRestrictionTypes)int.Parse(regParts[6]);
			TimeRestriction = (TimeRestrictionTypes)int.Parse(regParts[7]);

			RestrictionString = UrlTextDeSerialize(regParts[8]);
			string restrictionStringPlusWww = "www."+RestrictionString;
			Organisation = UrlTextDeSerialize(regParts[9]);
			ExpiryYear = int.Parse(regParts[10]);
			ExpiryMonth = int.Parse(regParts[11]);
			ExpiryDay = int.Parse(regParts[12]);

			PurchaseK = int.Parse(regParts[13]);

			LicensedServers = int.Parse(regParts[14]);
			ServerNameRestriction = (ServerNameRestrictionTypes)int.Parse(regParts[15]);
			LicenseIsPaid = (LicenseIsPaidTypes)int.Parse(regParts[16]);

			HashFromRegKey = UrlTextDeSerialize(regParts[17]);

			string licenseString = regParts[0]+"|"+regParts[1]+"|"+regParts[2]+"|"+regParts[4]+"|"+regParts[5]+"|"+regParts[6]+"|"+regParts[7]+"|"+regParts[8]+"|"+regParts[9]+"|"+regParts[10]+"|"+regParts[11]+"|"+regParts[12]+"|"+regParts[13]+"|"+regParts[14]+"|"+regParts[15]+"|"+regParts[16]+"|"+UrlDeSerialize("aeaaaaU99999baaaaaaaaaEbbaaaaufzPNKzIn1zPnHnZqXmZqHx7R1mZqXoImGxOyGxHqcBWJ1-QNg-KB0-QNg-LnhzJqWiWvgBWvKzIB0-OBKzIVKreBeshzIszNYmYutnYmdzKnh-LF0zl");
			SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
			byte[] hashFromClientByteArray = sha1.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(licenseString));
			string hashFromClientString = System.Text.ASCIIEncoding.ASCII.GetString(hashFromClientByteArray);

			if ( Product!=Products.DbCombo || (Edition == EditionTypes.Free && ( ! LicenseType.Equals(LicenseTypes.Restricted) || ! UrlRestriction.Equals(UrlRestrictionTypes.UrlRestricted) ) ) )
				throw new InvalidSerialException();
			if (MajorVersion!=Assembly.GetExecutingAssembly().GetName().Version.Major || MinorVersion != Assembly.GetExecutingAssembly().GetName().Version.Minor )
				throw new InvalidVersionException(this);
			if (TimeRestriction.Equals(TimeRestrictionTypes.TimeRestricted) && ( ExpiryYear < y || ( ExpiryYear == y && ExpiryMonth < m ) || ( ExpiryYear == y && ExpiryMonth == m && ExpiryDay <= d ) ) )
				throw new ExpiredSerialException();
			if (LicenseIsPaid.Equals(LicenseIsPaidTypes.Free))
			{
				if (UrlRestriction.Equals(UrlRestrictionTypes.UrlRestricted) &! (host == RestrictionString.ToLower() || host == "www."+RestrictionString.ToLower() || host == "localhost"))
					throw new DomainSerialException(this);
				if (ServerNameRestriction.Equals(ServerNameRestrictionTypes.ServerNameRestricted) &! (host == "localhost") &! (RestrictionString.ToUpper() == HttpContext.Current.Server.MachineName.ToUpper()))
					throw new ServerNameSerialException(this);
			}
			//if (HashFromRegKey != hashFromClientString )
			//	throw new InvalidSerialException();
		}
		catch (DbComboGenericSerialException a)
		{
			throw a;
		}
		catch (Exception)
		{
			throw new InvalidSerialException();
		}
	}
	#endregion

	#region Properties
	internal LicenseIsPaidTypes LicenseIsPaid{get{return licenseIsPaid;}set{licenseIsPaid=value;}}private LicenseIsPaidTypes licenseIsPaid;
	internal int LicensingScheme{get{return licensingScheme;}set{licensingScheme=value;}}private int licensingScheme;
	internal Products Product{get{return this.product;}set{this.product=value;}} private Products product;
	internal int MajorVersion{get{return this.majorVersion;}set{this.majorVersion=value;}} private int majorVersion;
	internal int MinorVersion{get{return this.minorVersion;}set{this.minorVersion=value;}} private int minorVersion;
	internal int Build{get{return this.build;}set{this.build=value;}} private int build;
	internal EditionTypes Edition{get{return this.edition;}set{this.edition=value;}} private EditionTypes edition;
	internal LicenseTypes LicenseType{get{return this.licenseType;}set{this.licenseType=value;}} private LicenseTypes licenseType;
	internal UrlRestrictionTypes UrlRestriction{get{return this.urlRestriction;}set{this.urlRestriction=value;}} private UrlRestrictionTypes urlRestriction;
	internal int LicensedServers{get{return licensedServers;}set{licensedServers=value;}}private int licensedServers;
	internal ServerNameRestrictionTypes ServerNameRestriction{get{return serverNameRestriction;}set{serverNameRestriction=value;}}private ServerNameRestrictionTypes serverNameRestriction;
	internal TimeRestrictionTypes TimeRestriction{get{return this.timeRestriction;}set{this.timeRestriction=value;}} private TimeRestrictionTypes timeRestriction;
	internal string RestrictionString{get{return this.restrictionString;}set{this.restrictionString=value;}} private string restrictionString;
	internal string Organisation{get{return this.organisation;}set{this.organisation=value;}} private string organisation;
	internal int ExpiryYear{get{return this.expiryYear;}set{this.expiryYear=value;}} private int expiryYear;
	internal int ExpiryMonth{get{return this.expiryMonth;}set{this.expiryMonth=value;}} private int expiryMonth;
	internal int ExpiryDay{get{return this.expiryDay;}set{this.expiryDay=value;}} private int expiryDay;
	internal string RegistrationKey{get{return this.registrationKey;}} private string registrationKey;
	internal string HashFromRegKey{get{return this.hashFromRegKey;}set{this.hashFromRegKey=value;}} private string hashFromRegKey;
	internal int PurchaseK{get{return this.purchaseK;}set{this.purchaseK=value;}} private int purchaseK;
	internal bool LicenseError
	{
		get
		{
			if (HttpContext.Current==null)
				return false;
			else
			{
				string host = HttpContext.Current.Request.Url.Host.ToLower();
				return ( 
					(UrlRestriction.Equals(UrlRestrictionTypes.UrlRestricted) &! (host == RestrictionString || host == "www."+RestrictionString || host == "localhost")) || 
					(ServerNameRestriction.Equals(ServerNameRestrictionTypes.ServerNameRestricted) &! (host == "localhost") &! (RestrictionString.ToUpper() == HttpContext.Current.Server.MachineName.ToUpper()))
					);
			}
		}
	}
	internal string LicenseErrorText
	{
		get
		{
			if (HttpContext.Current==null)
				return "";
			else
			{
				string host = HttpContext.Current.Request.Url.Host.ToLower();
				if (UrlRestriction.Equals(UrlRestrictionTypes.UrlRestricted) &! (host == RestrictionString || host == "www."+RestrictionString || host == "localhost"))
					return "DbCombo error: You have entered an invalid registration key. The registration key you entered is limited to the following domains: '$1', 'www.$1' and 'localhost'. The current domain is '$2'. Please see www.dbcombo.net for a valid key.".Replace("$1",this.RestrictionString.ToLower()).Replace("$2",HttpContext.Current.Request.Url.Host);
				else if (ServerNameRestriction.Equals(ServerNameRestrictionTypes.ServerNameRestricted) &! (host == "localhost") &! (RestrictionString.ToUpper() == HttpContext.Current.Server.MachineName.ToUpper()))
					return "DbCombo error: You have entered an invalid registration key. The registration key you entered is limited to the following server name: '$1'. The current server name is '$2'. Please see www.dbcombo.net for a valid key.".Replace("$1",this.RestrictionString.ToUpper()).Replace("$2",HttpContext.Current.Server.MachineName.ToUpper());
				else 
					return "";
			}

		}
	}
	#endregion

	#region Enums
	internal enum Products
	{
		DbCombo = 1,
		QuickAdmin = 2,
		ImageAdmin = 3,
		DbZipcode = 4,
		DbGrid = 5
	}
	internal enum EditionTypes 
	{
		Free = 1,
		Lite = 2,
		Pro = 3
	}
	internal enum LicenseTypes 
	{
		SingleDomain = 1,
		SingleServer = 2,
		EnterpriseLimited = 3,
		EnterpriseUnlimited = 4,
		Restricted = 5,
		Unrestricted = 6
	}
	internal enum UrlRestrictionTypes
	{
		NotUrlRestricted = 1,
		UrlRestricted = 2,
		Null1=3,
		Null2=4,
		Null3=5
	}
	internal enum TimeRestrictionTypes
	{
		NotTimeRestricted = 1,
		TimeRestricted = 2,
		Null1=3,
		Null2=4,
		Null3=5
	}
	internal enum ServerNameRestrictionTypes
	{
		NotServerNameRestricted = 1,
		ServerNameRestricted = 2,
		Null1=3,
		Null2=4,
		Null3=5
	}
	internal enum LicenseIsPaidTypes
	{
		Free = 1,
		Paid = 2,
		Null1=3,
		Null2=4,
		Null3=5
	}
	#endregion

	#region InvalidSerialException
	internal class InvalidSerialException : DbComboGenericSerialException
	{
		internal InvalidSerialException() : 
			base("DbCombo error: You have entered an invalid registration key. Please see www.dbcombo.net for a valid key."){}
	}
	internal class ExpiredSerialException : DbComboGenericSerialException
	{
		internal ExpiredSerialException() :
			base("DbCombo error: You have entered an invalid registration key. The registration key you entered has expired. Please see www.dbcombo.net for a valid key."){}
	}
	internal class DomainSerialException : DbComboGenericSerialException
	{
		internal DomainSerialException(RegistrationDetails reg) : 
			base("DbCombo error: You have entered an invalid registration key. The registration key you entered is limited to the following domains: '$1', 'www.$1' and 'localhost'. The current domain is '$2'. Please see www.dbcombo.net for a valid key.".Replace("$1",reg.RestrictionString.ToLower()).Replace("$2",HttpContext.Current.Request.Url.Host)){}
	}
	internal class ServerNameSerialException : DbComboGenericSerialException
	{
		internal ServerNameSerialException(RegistrationDetails reg) : 
			base("DbCombo error: You have entered an invalid registration key. The registration key you entered is limited to the following server name: '$1'. The current server name is $2. Please see www.dbcombo.net for a valid key.".Replace("$1",reg.RestrictionString).Replace("$2",HttpContext.Current.Server.MachineName)){}
	}
	internal class InvalidVersionException : DbComboGenericSerialException
	{
		internal InvalidVersionException(RegistrationDetails reg) : 
			base("DbCombo error: You have entered an invalid registration key. The registration key you entered is for DbCombo v$1.$2. You are using DbCombo v$4.$5. Please visit www.dbcombo.net and get a registration key for version $4.$5.".Replace("$1",reg.MajorVersion.ToString()).Replace("$2",reg.MinorVersion.ToString()).Replace("$3",reg.Build.ToString()).Replace("$4",Assembly.GetExecutingAssembly().GetName().Version.Major.ToString()).Replace("$5",Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()).Replace("$6",Assembly.GetExecutingAssembly().GetName().Version.Build.ToString()) ){}
	}
	internal class InvalidLicensingSchemeException : DbComboGenericSerialException
	{
		internal InvalidLicensingSchemeException(RegistrationDetails reg) : 
			base("DbCombo error: You have entered an invalid registration key. The registration key you entered is for an earlier version to the one that you are using. Please visit www.dbcombo.net and get a registration key for version $1.$2.$3.".Replace("$1",reg.MajorVersion.ToString()).Replace("$2",reg.MinorVersion.ToString()).Replace("$3",reg.Build.ToString())){}
	}
	internal class DbComboGenericSerialException : Exception	
	{
		internal DbComboGenericSerialException(string text) : base(text){}
	}
	#endregion

	#region UrlDeSerialize
	/// <summary>
	/// UrlDeserialize - this deserialises any object from a valid, search-engine compiant url (using a-z, A-Z, 0-9, -, ~)
	/// </summary>
	internal static object UrlDeSerialize(string serializedString)
	{
		try
		{
			if (serializedString == "")
				return null;
		}
		catch{}
		string chars= "abcdefghijklmnopqrstuvwxyz-~ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		int[] bits = {1,2,4,8,16,32,64,128};
		IFormatter formatter = new BinaryFormatter();
		Stream stream = new MemoryStream();
		int thisByte = 0;
		int bitNumber = 0;
		for(int i=0;i<serializedString.Length;i++)
		{
			int this6yte = chars.IndexOf(serializedString.Substring(i,1));
			for(int j=0;j<6;j++)
			{
				if ( (this6yte & bits[j]) > 0 )
				{
					thisByte+=bits[bitNumber%8];
				}
				if ( bitNumber % 8 == 7 )
				{
					//Helpers.Trace("ByteOut:"+thisByte.ToString());
					stream.WriteByte((byte)thisByte);
					thisByte=0;
				}
				bitNumber++;
			}
		}
		if ( bitNumber % 8 != 7 )
		{
			//Helpers.Trace("ByteOut:"+thisByte.ToString());
			stream.WriteByte((byte)thisByte);
			thisByte=0;
		}
		stream.Position=0;
		object obj = formatter.Deserialize(stream);
		stream.Close();
		return obj;

	}
	#endregion
	#region UrlTextSerialize
	/// <summary>
	/// UrlTextDeserialize - this deserialises a string created with UrlTextSerialize int a Unicode string.
	/// </summary>
	/// <param name="s">url string</param>
	/// <returns>unicode string</returns>
	internal static string UrlTextDeSerialize(string s)
	{
		if (s=="")
			return "";
		string outStr="";
		for(int i=0;i<s.Length;i++)
		{
			string sub = s.Substring(i,1);
			if (sub=="~")
			{
				byte[] outBytes = new Byte[2];
				outBytes[0] = (byte)int.Parse(s.Substring(i+1,2),System.Globalization.NumberStyles.HexNumber);
				outBytes[1] = (byte)int.Parse(s.Substring(i+3,2),System.Globalization.NumberStyles.HexNumber);
				string thisCharStr = System.Text.UnicodeEncoding.Unicode.GetString(outBytes);
				outStr+=thisCharStr;
				i+=4;
			}
			if (sub=="-")
			{
				byte[] outBytes = new Byte[2];
				outBytes[0] = (byte)int.Parse(s.Substring(i+1,2),System.Globalization.NumberStyles.HexNumber);
				outBytes[1] = (byte)0;
				string thisCharStr = System.Text.UnicodeEncoding.Unicode.GetString(outBytes);
				outStr+=thisCharStr;
				i+=2;
			}
			else if (sub=="_")
			{
				outStr+=" ";
			}
			else
			{
				outStr+=sub;
			}
		}
		return outStr;
	}
	#endregion
}
#region for source
/*

#region RegistrationDetails
internal class RegistrationDetails
{
#region Constructor
	internal RegistrationDetails()
	{
		this.MajorVersion = Assembly.GetExecutingAssembly().GetName().Version.Major;
		this.MinorVersion = Assembly.GetExecutingAssembly().GetName().Version.Minor;
		this.Build = Assembly.GetExecutingAssembly().GetName().Version.Build;
		this.Edition = EditionTypes.Pro;
		this.LicenseType = LicenseTypes.Unrestricted;
		this.UrlRestriction = UrlRestrictionTypes.NotUrlRestricted;
		this.LicensedServers = 0;
		this.ServerNameRestriction = ServerNameRestrictionTypes.NotServerNameRestricted;
		this.TimeRestriction = TimeRestrictionTypes.NotTimeRestricted;
		this.RestrictionString = "";
		this.Organisation="ORGANISATION";
		this.PurchaseK=0;
	}
#endregion

#region Properties
	internal int MajorVersion{get{return this.majorVersion;}set{this.majorVersion=value;}} private int majorVersion;
	internal int MinorVersion{get{return this.minorVersion;}set{this.minorVersion=value;}} private int minorVersion;
	internal int Build{get{return this.build;}set{this.build=value;}} private int build;
	internal EditionTypes Edition{get{return this.edition;}set{this.edition=value;}} private EditionTypes edition;
	internal LicenseTypes LicenseType{get{return this.licenseType;}set{this.licenseType=value;}} private LicenseTypes licenseType;
	internal UrlRestrictionTypes UrlRestriction{get{return this.urlRestriction;}set{this.urlRestriction=value;}} private UrlRestrictionTypes urlRestriction;
	internal int LicensedServers{get{return licensedServers;}set{licensedServers=value;}}private int licensedServers;
	internal ServerNameRestrictionTypes ServerNameRestriction{get{return serverNameRestriction;}set{serverNameRestriction=value;}}private ServerNameRestrictionTypes serverNameRestriction;
	internal TimeRestrictionTypes TimeRestriction{get{return this.timeRestriction;}set{this.timeRestriction=value;}} private TimeRestrictionTypes timeRestriction;
	internal string RestrictionString{get{return this.restrictionString;}set{this.restrictionString=value;}} private string restrictionString;
	internal string Organisation{get{return this.organisation;}set{this.organisation=value;}} private string organisation;
	internal int ExpiryYear{get{return this.expiryYear;}set{this.expiryYear=value;}} private int expiryYear;
	internal int ExpiryMonth{get{return this.expiryMonth;}set{this.expiryMonth=value;}} private int expiryMonth;
	internal int ExpiryDay{get{return this.expiryDay;}set{this.expiryDay=value;}} private int expiryDay;
	internal string RegistrationKey{get{return this.registrationKey;}} private string registrationKey;
	internal int PurchaseK{get{return this.purchaseK;}set{this.purchaseK=value;}} private int purchaseK;
	internal bool LicenseError
	{
		get
		{
			if (HttpContext.Current==null)
				return false;
			else
			{
				string host = HttpContext.Current.Request.Url.Host.ToLower();
				return ( 
					(UrlRestriction.Equals(UrlRestrictionTypes.UrlRestricted) &! (host == RestrictionString || host == "www."+RestrictionString || host == "localhost")) || 
					(ServerNameRestriction.Equals(ServerNameRestrictionTypes.ServerNameRestricted) &! (host == "localhost") &! (RestrictionString.ToUpper() == HttpContext.Current.Server.MachineName.ToUpper()))
					);
			}
		}
	}
	internal string LicenseErrorText
	{
		get
		{
			if (HttpContext.Current==null)
				return "";
			else
			{
				string host = HttpContext.Current.Request.Url.Host.ToLower();
				if (UrlRestriction.Equals(UrlRestrictionTypes.UrlRestricted) &! (host == RestrictionString || host == "www."+RestrictionString || host == "localhost"))
					return "DbCombo error: You have entered an invalid registration key. The registration key you entered is limited to the following domains: '$1', 'www.$1' and 'localhost'. The current domain is '$2'. Please see www.dbcombo.net for a valid key.".Replace("$1",this.RestrictionString.ToLower()).Replace("$2",HttpContext.Current.Request.Url.Host);
				else if (ServerNameRestriction.Equals(ServerNameRestrictionTypes.ServerNameRestricted) &! (host == "localhost") &! (RestrictionString.ToUpper() == HttpContext.Current.Server.MachineName.ToUpper()))
					return "DbCombo error: You have entered an invalid registration key. The registration key you entered is limited to the following server name: '$1'. The current server name is '$2'. Please see www.dbcombo.net for a valid key.".Replace("$1",this.RestrictionString.ToUpper()).Replace("$2",HttpContext.Current.Server.MachineName.ToUpper());
				else 
					return "";
			}

		}
	}
#endregion

#region Enums
	internal enum EditionTypes 
	{
		Free = 1,
		Lite = 2,
		Pro = 3
	}
	internal enum LicenseTypes 
	{
		SingleDomain = 1,
		SingleServer = 2,
		EnterpriseLimited = 3,
		EnterpriseUnlimited = 4,
		Restricted = 5,
		Unrestricted = 6
	}
	internal enum UrlRestrictionTypes
	{
		NotUrlRestricted = 1,
		UrlRestricted = 2,
		Null1=3,
		Null2=4,
		Null3=5
	}
	internal enum TimeRestrictionTypes
	{
		NotTimeRestricted = 1,
		TimeRestricted = 2,
		Null1=3,
		Null2=4,
		Null3=5
	}
	internal enum ServerNameRestrictionTypes
	{
		NotServerNameRestricted = 1,
		ServerNameRestricted = 2,
		Null1=3,
		Null2=4,
		Null3=5
	}
#endregion
}
#endregion
 */
#endregion
#endregion





















