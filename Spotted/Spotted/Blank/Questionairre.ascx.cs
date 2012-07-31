using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Blank
{
	public partial class Questionairre : BlankUserControl
	{
		protected Panel DoneQuestionairrePanel, QuestionairrePanel;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page.");
			if (!Page.IsPostBack)
			{
				DoneQuestionairrePanel.Visible = Usr.Current.Demographics != null;
				QuestionairrePanel.Visible = Usr.Current.Demographics == null;
			}

			Page.DataBind();
		}

		#region PanelDemographics

		protected HtmlGenericControl QuestionairreDiv;
		#region Drinks
		protected CheckBox
			DrinkWater,
			DrinkSoft,
			DrinkEnergy,
			DrinkDraftBeer,
			DrinkBottledBeer,
			DrinkSpirits,
			DrinkWine,
			DrinkAlcopops,
			DrinkCider;
		#endregion
		#region Smoking
		protected RadioButton
			Smoke1,
			Smoke2,
			Smoke3;
		#endregion
		#region Evenings
		protected DropDownList
			EveningAllNight,
			EveningLateNight,
			EveningCoupleDrinks,
			EveningOther,
			EveningStayIn;
		#endregion
		#region Employment
		protected RadioButton
			Employment1,
			Employment2,
			Employment3,
			Employment4;
		#endregion
		#region Salary
		protected RadioButton
			Salary1,
			Salary2,
			Salary3,
			Salary4,
			Salary5,
			Salary6,
			Salary7;
		#endregion
		#region Money
		protected RadioButton
			CreditCardYes,
			CreditCardNo,
			LoanYes,
			LoanNo,
			MortgageYes,
			MortgageNo;
		#endregion
		#region Stuff
		protected CheckBox
			OwnCar,
			OwnBike,
			OwnMp3,
			OwnPc,
			OwnLaptop,
			OwnMac,
			OwnBroadband,
			OwnConsole,
			OwnCamera,
			OwnDvd,
			OwnDvdRec,
			BuyCar,
			BuyBike,
			BuyMp3,
			BuyPc,
			BuyLaptop,
			BuyMac,
			BuyBroadband,
			BuyConsole,
			BuyCamera,
			BuyDvd,
			BuyDvdRec;
		#endregion
		#region Shopping
		protected DropDownList
			SpendDesignerClothes,
			SpendHighStreetClothes,
			SpendMusicCd,
			SpendMusicVinyl,
			SpendMusicDownload,
			SpendDvd,
			SpendGames,
			SpendMobile,
			SpendSms,
			SpendCar,
			SpendTravel;
		#endregion
		#region Holidays
		protected DropDownList
			Holidays;
		#endregion

		protected Panel PanelDemographics;
		private void PanelDemographics_Load(object sender, System.EventArgs e)
		{
		}
		public void Finish(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{

				Demographics d = Usr.Current.Demographics;
				if (d == null)
				{
					d = new Demographics();
					d.Guid = Usr.Current.Guid;
				}

				d.DateTime = DateTime.Now;

				#region Drinks
				d.DrinkWater = DrinkWater.Checked;
				d.DrinkSoft = DrinkSoft.Checked;
				d.DrinkEnergy = DrinkEnergy.Checked;
				d.DrinkDraftBeer = DrinkDraftBeer.Checked;
				d.DrinkBottledBeer = DrinkBottledBeer.Checked;
				d.DrinkSpirits = DrinkSpirits.Checked;
				d.DrinkWine = DrinkWine.Checked;
				d.DrinkAlcopops = DrinkAlcopops.Checked;
				d.DrinkCider = DrinkCider.Checked;
				#endregion
				#region Smoking
				if (Smoke1.Checked)
					d.Smoke = 1;
				else if (Smoke2.Checked)
					d.Smoke = 2;
				else
					d.Smoke = 3;
				#endregion
				#region Evenings
				d.EveningAllNight = float.Parse(EveningAllNight.SelectedValue);
				d.EveningLateNight = float.Parse(EveningLateNight.SelectedValue);
				d.EveningCoupleDrinks = float.Parse(EveningCoupleDrinks.SelectedValue);
				d.EveningOther = float.Parse(EveningOther.SelectedValue);
				d.EveningStayIn = float.Parse(EveningStayIn.SelectedValue);
				#endregion
				#region Employment
				if (Employment1.Checked)
					d.Employment = 1;
				else if (Employment2.Checked)
					d.Employment = 2;
				else if (Employment3.Checked)
					d.Employment = 3;
				else
					d.Employment = 4;
				#endregion
				#region Salary
				if (Salary1.Checked)
					d.Salary = 1;
				else if (Salary2.Checked)
					d.Salary = 2;
				else if (Salary3.Checked)
					d.Salary = 3;
				else if (Salary4.Checked)
					d.Salary = 4;
				else if (Salary5.Checked)
					d.Salary = 5;
				else if (Salary6.Checked)
					d.Salary = 6;
				else
					d.Salary = 7;
				#endregion
				#region Money
				d.CreditCard = CreditCardYes.Checked;
				d.Loan = LoanYes.Checked;
				d.Mortgage = MortgageYes.Checked;
				#endregion
				#region Stuff
				d.OwnCar = OwnCar.Checked;
				d.OwnBike = OwnBike.Checked;
				d.OwnMp3 = OwnMp3.Checked;
				d.OwnPc = OwnPc.Checked;
				d.OwnLaptop = OwnLaptop.Checked;
				d.OwnMac = OwnMac.Checked;
				d.OwnBroadband = OwnBroadband.Checked;
				d.OwnConsole = OwnConsole.Checked;
				d.OwnCamera = OwnCamera.Checked;
				d.OwnDvd = OwnDvd.Checked;
				d.OwnDvdRec = OwnDvdRec.Checked;
				d.BuyCar = BuyCar.Checked;
				d.BuyBike = BuyBike.Checked;
				d.BuyMp3 = BuyMp3.Checked;
				d.BuyPc = BuyPc.Checked;
				d.BuyLaptop = BuyLaptop.Checked;
				d.BuyMac = BuyMac.Checked;
				d.BuyBroadband = BuyBroadband.Checked;
				d.BuyConsole = BuyConsole.Checked;
				d.BuyCamera = BuyCamera.Checked;
				d.BuyDvd = BuyDvd.Checked;
				d.BuyDvdRec = BuyDvdRec.Checked;
				#endregion
				#region Shopping
				d.SpendDesignerClothes = int.Parse(SpendDesignerClothes.SelectedValue);
				d.SpendHighStreetClothes = int.Parse(SpendHighStreetClothes.SelectedValue);
				d.SpendMusicCd = int.Parse(SpendMusicCd.SelectedValue);
				d.SpendMusicVinyl = int.Parse(SpendMusicVinyl.SelectedValue);
				d.SpendMusicDownload = int.Parse(SpendMusicDownload.SelectedValue);
				d.SpendDvd = int.Parse(SpendDvd.SelectedValue);
				d.SpendGames = int.Parse(SpendGames.SelectedValue);
				d.SpendMobile = int.Parse(SpendMobile.SelectedValue);
				d.SpendSms = int.Parse(SpendSms.SelectedValue);
				d.SpendCar = int.Parse(SpendCar.SelectedValue);
				d.SpendTravel = int.Parse(SpendTravel.SelectedValue);
				#endregion
				#region Holidays
				d.Holidays = int.Parse(Holidays.SelectedValue);
				#endregion
				#region Imaging
				//d.ImagingManufacturer = Cambro.Web.Helpers.Strip(ImagingManufacturer.Text);
				//d.ImagingImportant = GetImagingVal(ImagingImportant1, ImagingImportant2, ImagingImportant3, ImagingImportant4, ImagingImportant5);

				//d.ImagingOpinionSony = GetImagingVal(ImagingOpinionSony1, ImagingOpinionSony2, ImagingOpinionSony3, ImagingOpinionSony4, ImagingOpinionSony5);
				//d.ImagingOpinionNokia = GetImagingVal(ImagingOpinionNokia1, ImagingOpinionNokia2, ImagingOpinionNokia3, ImagingOpinionNokia4, ImagingOpinionNokia5);
				//d.ImagingOpinionMotorola = GetImagingVal(ImagingOpinionMotorola1, ImagingOpinionMotorola2, ImagingOpinionMotorola3, ImagingOpinionMotorola4, ImagingOpinionMotorola5);
				//d.ImagingOpinionSiemens = GetImagingVal(ImagingOpinionSiemens1, ImagingOpinionSiemens2, ImagingOpinionSiemens3, ImagingOpinionSiemens4, ImagingOpinionSiemens5);
				//d.ImagingOpinionLg = GetImagingVal(ImagingOpinionLg1, ImagingOpinionLg2, ImagingOpinionLg3, ImagingOpinionLg4, ImagingOpinionLg5);
				//d.ImagingOpinionSamsung = GetImagingVal(ImagingOpinionSamsung1, ImagingOpinionSamsung2, ImagingOpinionSamsung3, ImagingOpinionSamsung4, ImagingOpinionSamsung5);

				//d.ImagingCapabilitySony = GetImagingVal(ImagingCapabilitySony1, ImagingCapabilitySony2, ImagingCapabilitySony3, ImagingCapabilitySony4, ImagingCapabilitySony5);
				//d.ImagingCapabilityNokia = GetImagingVal(ImagingCapabilityNokia1, ImagingCapabilityNokia2, ImagingCapabilityNokia3, ImagingCapabilityNokia4, ImagingCapabilityNokia5);
				//d.ImagingCapabilityMotorola = GetImagingVal(ImagingCapabilityMotorola1, ImagingCapabilityMotorola2, ImagingCapabilityMotorola3, ImagingCapabilityMotorola4, ImagingCapabilityMotorola5);
				//d.ImagingCapabilitySiemens = GetImagingVal(ImagingCapabilitySiemens1, ImagingCapabilitySiemens2, ImagingCapabilitySiemens3, ImagingCapabilitySiemens4, ImagingCapabilitySiemens5);
				//d.ImagingCapabilityLg = GetImagingVal(ImagingCapabilityLg1, ImagingCapabilityLg2, ImagingCapabilityLg3, ImagingCapabilityLg4, ImagingCapabilityLg5);
				//d.ImagingCapabilitySamsung = GetImagingVal(ImagingCapabilitySamsung1, ImagingCapabilitySamsung2, ImagingCapabilitySamsung3, ImagingCapabilitySamsung4, ImagingCapabilitySamsung5);

				//d.ImagingBuySony = GetImagingVal(ImagingBuySony1, ImagingBuySony2, ImagingBuySony3, ImagingBuySony4, ImagingBuySony5);
				//d.ImagingBuyNokia = GetImagingVal(ImagingBuyNokia1, ImagingBuyNokia2, ImagingBuyNokia3, ImagingBuyNokia4, ImagingBuyNokia5);
				//d.ImagingBuyMotorola = GetImagingVal(ImagingBuyMotorola1, ImagingBuyMotorola2, ImagingBuyMotorola3, ImagingBuyMotorola4, ImagingBuyMotorola5);
				//d.ImagingBuySiemens = GetImagingVal(ImagingBuySiemens1, ImagingBuySiemens2, ImagingBuySiemens3, ImagingBuySiemens4, ImagingBuySiemens5);
				//d.ImagingBuyLg = GetImagingVal(ImagingBuyLg1, ImagingBuyLg2, ImagingBuyLg3, ImagingBuyLg4, ImagingBuyLg5);
				//d.ImagingBuySamsung = GetImagingVal(ImagingBuySamsung1, ImagingBuySamsung2, ImagingBuySamsung3, ImagingBuySamsung4, ImagingBuySamsung5);
				#endregion

				d.Update();

				Usr.Current.HasCompletedDemographics = true;
				Usr.Current.ExtraIcon = 1;
				Usr.Current.ExtraExpire = DateTime.Now.AddMonths(1);
				Usr.Current.Update();

				Bobs.Log.Increment(Bobs.Log.Items.CompleteQuestionairre);

				if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
					Response.Redirect(Request.QueryString["Url"]);
				else
					Response.Redirect("/");
			}
			else
				QuestionairreDiv.Style["visibility"] = "visible";//.Remove("display");

		}
		int GetImagingVal(params RadioButton[] controls)
		{
			for (int i = 0; i < controls.Length; i++)
			{
				if (controls[i].Checked)
					return i + 1;
			}
			return 0;
		}
		public void Skip(object o, System.EventArgs e)
		{
			Bobs.Log.Increment(Bobs.Log.Items.SkipQuestionairre);

			if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
				Response.Redirect(Request.QueryString["Url"]);
			else
				Response.Redirect("/");
		}

		public void SmokeVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (Smoke1.Checked || Smoke2.Checked || Smoke3.Checked);
		}
		public void EveningsVal(object o, ServerValidateEventArgs e)
		{
			int val = getEveningVal(EveningAllNight) +
				getEveningVal(EveningLateNight) +
				getEveningVal(EveningCoupleDrinks) +
				getEveningVal(EveningOther) +
				getEveningVal(EveningStayIn);
			e.IsValid = val == 7;
		}
		int getEveningVal(DropDownList d)
		{
			try
			{
				float f = float.Parse(d.SelectedValue);
				if (f < 1.0)
					return 0;
				else
					return (int)f;
			}
			catch
			{
				return 0;
			}
		}
		public void EmployVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (Employment1.Checked || Employment2.Checked || Employment3.Checked || Employment4.Checked);
		}
		public void SalaryVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (Salary1.Checked || Salary2.Checked || Salary3.Checked || Salary4.Checked || Salary5.Checked || Salary6.Checked || Salary7.Checked);
		}
		public void CreditCardVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (CreditCardYes.Checked || CreditCardNo.Checked);
		}
		public void LoanVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (LoanYes.Checked || LoanNo.Checked);
		}
		public void MortgageVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (MortgageYes.Checked || MortgageNo.Checked);
		}
		public void ImagingImportantVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (ImagingImportant1.Checked || ImagingImportant2.Checked || ImagingImportant3.Checked || ImagingImportant4.Checked || ImagingImportant5.Checked);
		}
		public void ImagingOpinionVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = ((ImagingOpinionSony1.Checked || ImagingOpinionSony2.Checked || ImagingOpinionSony3.Checked || ImagingOpinionSony4.Checked || ImagingOpinionSony5.Checked) &&
				(ImagingOpinionNokia1.Checked || ImagingOpinionNokia2.Checked || ImagingOpinionNokia3.Checked || ImagingOpinionNokia4.Checked || ImagingOpinionNokia5.Checked) &&
				(ImagingOpinionMotorola1.Checked || ImagingOpinionMotorola2.Checked || ImagingOpinionMotorola3.Checked || ImagingOpinionMotorola4.Checked || ImagingOpinionMotorola5.Checked) &&
				(ImagingOpinionSiemens1.Checked || ImagingOpinionSiemens2.Checked || ImagingOpinionSiemens3.Checked || ImagingOpinionSiemens4.Checked || ImagingOpinionSiemens5.Checked) &&
				(ImagingOpinionLg1.Checked || ImagingOpinionLg2.Checked || ImagingOpinionLg3.Checked || ImagingOpinionLg4.Checked || ImagingOpinionLg5.Checked) &&
				(ImagingOpinionSamsung1.Checked || ImagingOpinionSamsung2.Checked || ImagingOpinionSamsung3.Checked || ImagingOpinionSamsung4.Checked || ImagingOpinionSamsung5.Checked));
		}
		public void ImagingCapabilityVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = ((ImagingCapabilitySony1.Checked || ImagingCapabilitySony2.Checked || ImagingCapabilitySony3.Checked || ImagingCapabilitySony4.Checked || ImagingCapabilitySony5.Checked) &&
				(ImagingCapabilityNokia1.Checked || ImagingCapabilityNokia2.Checked || ImagingCapabilityNokia3.Checked || ImagingCapabilityNokia4.Checked || ImagingCapabilityNokia5.Checked) &&
				(ImagingCapabilityMotorola1.Checked || ImagingCapabilityMotorola2.Checked || ImagingCapabilityMotorola3.Checked || ImagingCapabilityMotorola4.Checked || ImagingCapabilityMotorola5.Checked) &&
				(ImagingCapabilitySiemens1.Checked || ImagingCapabilitySiemens2.Checked || ImagingCapabilitySiemens3.Checked || ImagingCapabilitySiemens4.Checked || ImagingCapabilitySiemens5.Checked) &&
				(ImagingCapabilityLg1.Checked || ImagingCapabilityLg2.Checked || ImagingCapabilityLg3.Checked || ImagingCapabilityLg4.Checked || ImagingCapabilityLg5.Checked) &&
				(ImagingCapabilitySamsung1.Checked || ImagingCapabilitySamsung2.Checked || ImagingCapabilitySamsung3.Checked || ImagingCapabilitySamsung4.Checked || ImagingCapabilitySamsung5.Checked));
		}
		public void ImagingBuyVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = ((ImagingBuySony1.Checked || ImagingBuySony2.Checked || ImagingBuySony3.Checked || ImagingBuySony4.Checked || ImagingBuySony5.Checked) &&
				(ImagingBuyNokia1.Checked || ImagingBuyNokia2.Checked || ImagingBuyNokia3.Checked || ImagingBuyNokia4.Checked || ImagingBuyNokia5.Checked) &&
				(ImagingBuyMotorola1.Checked || ImagingBuyMotorola2.Checked || ImagingBuyMotorola3.Checked || ImagingBuyMotorola4.Checked || ImagingBuyMotorola5.Checked) &&
				(ImagingBuySiemens1.Checked || ImagingBuySiemens2.Checked || ImagingBuySiemens3.Checked || ImagingBuySiemens4.Checked || ImagingBuySiemens5.Checked) &&
				(ImagingBuyLg1.Checked || ImagingBuyLg2.Checked || ImagingBuyLg3.Checked || ImagingBuyLg4.Checked || ImagingBuyLg5.Checked) &&
				(ImagingBuySamsung1.Checked || ImagingBuySamsung2.Checked || ImagingBuySamsung3.Checked || ImagingBuySamsung4.Checked || ImagingBuySamsung5.Checked));
		}
		public void ShoppingVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !(SpendDesignerClothes.SelectedValue.Equals("0") ||
				SpendHighStreetClothes.SelectedValue.Equals("0") ||
				SpendMusicCd.SelectedValue.Equals("0") ||
				SpendMusicVinyl.SelectedValue.Equals("0") ||
				SpendMusicDownload.SelectedValue.Equals("0") ||
				SpendDvd.SelectedValue.Equals("0") ||
				SpendGames.SelectedValue.Equals("0") ||
				SpendMobile.SelectedValue.Equals("0") ||
				SpendSms.SelectedValue.Equals("0") ||
				SpendCar.SelectedValue.Equals("0") ||
				SpendTravel.SelectedValue.Equals("0"));
		}
		public void HolidayVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !Holidays.SelectedValue.Equals("0");
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.PanelDemographics_Load);
		}
		#endregion
	}
}
