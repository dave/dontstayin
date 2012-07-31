using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;

namespace Bobs
{

	#region Demographics
	/// <summary>
	/// Stores information about a Demographics - guid from the cookie etc.
	/// </summary>
	[Serializable] 
	public partial class Demographics 
	{

		#region simple members
		/// <summary>
		/// Guid of browser or Usr
		/// </summary>
		public override Guid Guid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Demographics.Columns.Guid]); }
			set { this[Demographics.Columns.Guid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Date/time the questionairre was completed
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[Demographics.Columns.DateTime]; }
			set { this[Demographics.Columns.DateTime] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Water
		/// </summary>
		public override bool DrinkWater
		{
			get { return (bool)this[Demographics.Columns.DrinkWater]; }
			set { this[Demographics.Columns.DrinkWater] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Soft drinks
		/// </summary>
		public override bool DrinkSoft
		{
			get { return (bool)this[Demographics.Columns.DrinkSoft]; }
			set { this[Demographics.Columns.DrinkSoft] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Energy drinks
		/// </summary>
		public override bool DrinkEnergy
		{
			get { return (bool)this[Demographics.Columns.DrinkEnergy]; }
			set { this[Demographics.Columns.DrinkEnergy] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Beer / lager (in a pint / glass)
		/// </summary>
		public override bool DrinkDraftBeer
		{
			get { return (bool)this[Demographics.Columns.DrinkDraftBeer]; }
			set { this[Demographics.Columns.DrinkDraftBeer] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Bottled beer / lager
		/// </summary>
		public override bool DrinkBottledBeer
		{
			get { return (bool)this[Demographics.Columns.DrinkBottledBeer]; }
			set { this[Demographics.Columns.DrinkBottledBeer] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Spirits
		/// </summary>
		public override bool DrinkSpirits
		{
			get { return (bool)this[Demographics.Columns.DrinkSpirits]; }
			set { this[Demographics.Columns.DrinkSpirits] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Wine
		/// </summary>
		public override bool DrinkWine
		{
			get { return (bool)this[Demographics.Columns.DrinkWine]; }
			set { this[Demographics.Columns.DrinkWine] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Alcopops
		/// </summary>
		public override bool DrinkAlcopops
		{
			get { return (bool)this[Demographics.Columns.DrinkAlcopops]; }
			set { this[Demographics.Columns.DrinkAlcopops] = value; }
		}
		/// <summary>
		/// When going out, what do you drink? Cider
		/// </summary>
		public override bool DrinkCider
		{
			get { return (bool)this[Demographics.Columns.DrinkCider]; }
			set { this[Demographics.Columns.DrinkCider] = value; }
		}
		/// <summary>
		/// Do you smoke? Yes=1, No=2, Only when I go out=3
		/// </summary>
		public override int Smoke
		{
			get { return (int)this[Demographics.Columns.Smoke]; }
			set { this[Demographics.Columns.Smoke] = value; }
		}
		/// <summary>
		/// How / how often do you spend your evenings: All night clubbing (times per week)
		/// </summary>
		public override double EveningAllNight
		{
			get { return (double)this[Demographics.Columns.EveningAllNight]; }
			set { this[Demographics.Columns.EveningAllNight] = value; }
		}
		/// <summary>
		/// How / how often do you spend your evenings: Late night at a pub/club (in bed by 3am) (times per week)
		/// </summary>
		public override double EveningLateNight
		{
			get { return (double)this[Demographics.Columns.EveningLateNight]; }
			set { this[Demographics.Columns.EveningLateNight] = value; }
		}
		/// <summary>
		/// How / how often do you spend your evenings: Couple of drinks in a bar (in bed by midnight) (times per week)
		/// </summary>
		public override double EveningCoupleDrinks
		{
			get { return (double)this[Demographics.Columns.EveningCoupleDrinks]; }
			set { this[Demographics.Columns.EveningCoupleDrinks] = value; }
		}
		/// <summary>
		/// How / how often do you spend your evenings: Other social event (e.g. cinema, restaurant etc.) (times per week)
		/// </summary>
		public override double EveningOther
		{
			get { return (double)this[Demographics.Columns.EveningOther]; }
			set { this[Demographics.Columns.EveningOther] = value; }
		}
		/// <summary>
		/// How / how often do you spend your evenings: Stay in / work (times per week)
		/// </summary>
		public override double EveningStayIn
		{
			get { return (double)this[Demographics.Columns.EveningStayIn]; }
			set { this[Demographics.Columns.EveningStayIn] = value; }
		}
		/// <summary>
		/// What�s your employment status: Full-time=1, Part-time=2, Currently unemployed=3, Student=4
		/// </summary>
		public override int Employment
		{
			get { return (int)this[Demographics.Columns.Employment]; }
			set { this[Demographics.Columns.Employment] = value; }
		}
		/// <summary>
		/// How much do you earn per year? [less than �15k]=1, [15 - 19]=2, [20 - 24]=3, [25 - 29]=4, [30 - 39]=5, [40 - 49]=6, [�50k+]=7
		/// </summary>
		public override int Salary
		{
			get { return (int)this[Demographics.Columns.Salary]; }
			set { this[Demographics.Columns.Salary] = value; }
		}
		/// <summary>
		/// Do you use a credit card?
		/// </summary>
		public override bool CreditCard
		{
			get { return (bool)this[Demographics.Columns.CreditCard]; }
			set { this[Demographics.Columns.CreditCard] = value; }
		}
		/// <summary>
		/// Do you have a personal loan?
		/// </summary>
		public override bool Loan
		{
			get { return (bool)this[Demographics.Columns.Loan]; }
			set { this[Demographics.Columns.Loan] = value; }
		}
		/// <summary>
		/// Do you have a mortgage?
		/// </summary>
		public override bool Mortgage
		{
			get { return (bool)this[Demographics.Columns.Mortgage]; }
			set { this[Demographics.Columns.Mortgage] = value; }
		}
		/// <summary>
		/// Do you own: Car / motorbike
		/// </summary>
		public override bool OwnCar
		{
			get { return (bool)this[Demographics.Columns.OwnCar]; }
			set { this[Demographics.Columns.OwnCar] = value; }
		}
		/// <summary>
		/// Do you own: Pedal bike
		/// </summary>
		public override bool OwnBike
		{
			get { return (bool)this[Demographics.Columns.OwnBike]; }
			set { this[Demographics.Columns.OwnBike] = value; }
		}
		/// <summary>
		/// Do you own: MP3 player
		/// </summary>
		public override bool OwnMp3
		{
			get { return (bool)this[Demographics.Columns.OwnMp3]; }
			set { this[Demographics.Columns.OwnMp3] = value; }
		}
		/// <summary>
		/// Do you own: PC
		/// </summary>
		public override bool OwnPc
		{
			get { return (bool)this[Demographics.Columns.OwnPc]; }
			set { this[Demographics.Columns.OwnPc] = value; }
		}
		/// <summary>
		/// Do you own: Laptop
		/// </summary>
		public override bool OwnLaptop
		{
			get { return (bool)this[Demographics.Columns.OwnLaptop]; }
			set { this[Demographics.Columns.OwnLaptop] = value; }
		}
		/// <summary>
		/// Do you own: Mac
		/// </summary>
		public override bool OwnMac
		{
			get { return (bool)this[Demographics.Columns.OwnMac]; }
			set { this[Demographics.Columns.OwnMac] = value; }
		}
		/// <summary>
		/// Do you own: Broadband internet
		/// </summary>
		public override bool OwnBroadband
		{
			get { return (bool)this[Demographics.Columns.OwnBroadband]; }
			set { this[Demographics.Columns.OwnBroadband] = value; }
		}
		/// <summary>
		/// Do you own: Games console
		/// </summary>
		public override bool OwnConsole
		{
			get { return (bool)this[Demographics.Columns.OwnConsole]; }
			set { this[Demographics.Columns.OwnConsole] = value; }
		}
		/// <summary>
		/// Do you own: Digital camera
		/// </summary>
		public override bool OwnCamera
		{
			get { return (bool)this[Demographics.Columns.OwnCamera]; }
			set { this[Demographics.Columns.OwnCamera] = value; }
		}
		/// <summary>
		/// Do you own: DVD player
		/// </summary>
		public override bool OwnDvd
		{
			get { return (bool)this[Demographics.Columns.OwnDvd]; }
			set { this[Demographics.Columns.OwnDvd] = value; }
		}
		/// <summary>
		/// Do you own: DVD recorder
		/// </summary>
		public override bool OwnDvdRec
		{
			get { return (bool)this[Demographics.Columns.OwnDvdRec]; }
			set { this[Demographics.Columns.OwnDvdRec] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: Car / motorbike
		/// </summary>
		public override bool BuyCar
		{
			get { return (bool)this[Demographics.Columns.BuyCar]; }
			set { this[Demographics.Columns.BuyCar] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: Pedal bike
		/// </summary>
		public override bool BuyBike
		{
			get { return (bool)this[Demographics.Columns.BuyBike]; }
			set { this[Demographics.Columns.BuyBike] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: MP3 player
		/// </summary>
		public override bool BuyMp3
		{
			get { return (bool)this[Demographics.Columns.BuyMp3]; }
			set { this[Demographics.Columns.BuyMp3] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: PC
		/// </summary>
		public override bool BuyPc
		{
			get { return (bool)this[Demographics.Columns.BuyPc]; }
			set { this[Demographics.Columns.BuyPc] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: Laptop
		/// </summary>
		public override bool BuyLaptop
		{
			get { return (bool)this[Demographics.Columns.BuyLaptop]; }
			set { this[Demographics.Columns.BuyLaptop] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: Mac
		/// </summary>
		public override bool BuyMac
		{
			get { return (bool)this[Demographics.Columns.BuyMac]; }
			set { this[Demographics.Columns.BuyMac] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: Broadband internet
		/// </summary>
		public override bool BuyBroadband
		{
			get { return (bool)this[Demographics.Columns.BuyBroadband]; }
			set { this[Demographics.Columns.BuyBroadband] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: Games console
		/// </summary>
		public override bool BuyConsole
		{
			get { return (bool)this[Demographics.Columns.BuyConsole]; }
			set { this[Demographics.Columns.BuyConsole] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: Digital camera
		/// </summary>
		public override bool BuyCamera
		{
			get { return (bool)this[Demographics.Columns.BuyCamera]; }
			set { this[Demographics.Columns.BuyCamera] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: DVD player
		/// </summary>
		public override bool BuyDvd
		{
			get { return (bool)this[Demographics.Columns.BuyDvd]; }
			set { this[Demographics.Columns.BuyDvd] = value; }
		}
		/// <summary>
		/// Do you think you might buy in the next 6 months: DVD recorder
		/// </summary>
		public override bool BuyDvdRec
		{
			get { return (bool)this[Demographics.Columns.BuyDvdRec]; }
			set { this[Demographics.Columns.BuyDvdRec] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Designer / branded clothes (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendDesignerClothes
		{
			get { return (int)this[Demographics.Columns.SpendDesignerClothes]; }
			set { this[Demographics.Columns.SpendDesignerClothes] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: High street / non-branded clothes (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendHighStreetClothes
		{
			get { return (int)this[Demographics.Columns.SpendHighStreetClothes]; }
			set { this[Demographics.Columns.SpendHighStreetClothes] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Music on CD (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendMusicCd
		{
			get { return (int)this[Demographics.Columns.SpendMusicCd]; }
			set { this[Demographics.Columns.SpendMusicCd] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Music on vinyl (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendMusicVinyl
		{
			get { return (int)this[Demographics.Columns.SpendMusicVinyl]; }
			set { this[Demographics.Columns.SpendMusicVinyl] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Music downloads (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendMusicDownload
		{
			get { return (int)this[Demographics.Columns.SpendMusicDownload]; }
			set { this[Demographics.Columns.SpendMusicDownload] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: DVDs (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendDvd
		{
			get { return (int)this[Demographics.Columns.SpendDvd]; }
			set { this[Demographics.Columns.SpendDvd] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Computer/video games (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendGames
		{
			get { return (int)this[Demographics.Columns.SpendGames]; }
			set { this[Demographics.Columns.SpendGames] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Mobile phone calls (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendMobile
		{
			get { return (int)this[Demographics.Columns.SpendMobile]; }
			set { this[Demographics.Columns.SpendMobile] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Ringtones / text voting etc. (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendSms
		{
			get { return (int)this[Demographics.Columns.SpendSms]; }
			set { this[Demographics.Columns.SpendSms] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Car / motorbike (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendCar
		{
			get { return (int)this[Demographics.Columns.SpendCar]; }
			set { this[Demographics.Columns.SpendCar] = value; }
		}
		/// <summary>
		/// How much do you spend on average per month on: Other travel / public override transport (Nothing=1, less than �10=2, �10-�19=3, �20-�49=4, �50-�99=5, �100-�200=6, �200+=7)
		/// </summary>
		public override int SpendTravel
		{
			get { return (int)this[Demographics.Columns.SpendTravel]; }
			set { this[Demographics.Columns.SpendTravel] = value; }
		}
		/// <summary>
		/// How often do you go abroad on holiday? (time(s) per year)
		/// </summary>
		public override int Holidays
		{
			get { return (int)this[Demographics.Columns.Holidays]; }
			set { this[Demographics.Columns.Holidays] = value; }
		}
		/// <summary>
		/// When you think of mobile phone imaging technology, which mobile phone manufacturer comes to mind fir
		/// </summary>
		public override string ImagingManufacturer
		{
			get { return (string)this[Demographics.Columns.ImagingManufacturer]; }
			set { this[Demographics.Columns.ImagingManufacturer] = value; }
		}
		/// <summary>
		/// On a scale of 1-5, how important is imaging functionality in a mobile phone when considering which h
		/// </summary>
		public override int ImagingImportant
		{
			get { return (int)this[Demographics.Columns.ImagingImportant]; }
			set { this[Demographics.Columns.ImagingImportant] = value; }
		}
		/// <summary>
		/// How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not
		/// </summary>
		public override int ImagingOpinionSony
		{
			get { return (int)this[Demographics.Columns.ImagingOpinionSony]; }
			set { this[Demographics.Columns.ImagingOpinionSony] = value; }
		}
		/// <summary>
		/// How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not
		/// </summary>
		public override int ImagingOpinionNokia
		{
			get { return (int)this[Demographics.Columns.ImagingOpinionNokia]; }
			set { this[Demographics.Columns.ImagingOpinionNokia] = value; }
		}
		/// <summary>
		/// How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not
		/// </summary>
		public override int ImagingOpinionMotorola
		{
			get { return (int)this[Demographics.Columns.ImagingOpinionMotorola]; }
			set { this[Demographics.Columns.ImagingOpinionMotorola] = value; }
		}
		/// <summary>
		/// How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not
		/// </summary>
		public override int ImagingOpinionSiemens
		{
			get { return (int)this[Demographics.Columns.ImagingOpinionSiemens]; }
			set { this[Demographics.Columns.ImagingOpinionSiemens] = value; }
		}
		/// <summary>
		/// How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not
		/// </summary>
		public override int ImagingOpinionLg
		{
			get { return (int)this[Demographics.Columns.ImagingOpinionLg]; }
			set { this[Demographics.Columns.ImagingOpinionLg] = value; }
		}
		/// <summary>
		/// How would you describe your overall opinion about the following mobile phone manufacturers? (1 = not
		/// </summary>
		public override int ImagingOpinionSamsung
		{
			get { return (int)this[Demographics.Columns.ImagingOpinionSamsung]; }
			set { this[Demographics.Columns.ImagingOpinionSamsung] = value; }
		}
		/// <summary>
		/// Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturer
		/// </summary>
		public override int ImagingCapabilitySony
		{
			get { return (int)this[Demographics.Columns.ImagingCapabilitySony]; }
			set { this[Demographics.Columns.ImagingCapabilitySony] = value; }
		}
		/// <summary>
		/// Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturer
		/// </summary>
		public override int ImagingCapabilityNokia
		{
			get { return (int)this[Demographics.Columns.ImagingCapabilityNokia]; }
			set { this[Demographics.Columns.ImagingCapabilityNokia] = value; }
		}
		/// <summary>
		/// Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturer
		/// </summary>
		public override int ImagingCapabilityMotorola
		{
			get { return (int)this[Demographics.Columns.ImagingCapabilityMotorola]; }
			set { this[Demographics.Columns.ImagingCapabilityMotorola] = value; }
		}
		/// <summary>
		/// Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturer
		/// </summary>
		public override int ImagingCapabilitySiemens
		{
			get { return (int)this[Demographics.Columns.ImagingCapabilitySiemens]; }
			set { this[Demographics.Columns.ImagingCapabilitySiemens] = value; }
		}
		/// <summary>
		/// Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturer
		/// </summary>
		public override int ImagingCapabilityLg
		{
			get { return (int)this[Demographics.Columns.ImagingCapabilityLg]; }
			set { this[Demographics.Columns.ImagingCapabilityLg] = value; }
		}
		/// <summary>
		/// Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturer
		/// </summary>
		public override int ImagingCapabilitySamsung
		{
			get { return (int)this[Demographics.Columns.ImagingCapabilitySamsung]; }
			set { this[Demographics.Columns.ImagingCapabilitySamsung] = value; }
		}
		/// <summary>
		/// If you were to buy a new mobile phone, how likely would you be to consider the following manufacture
		/// </summary>
		public override int ImagingBuySony
		{
			get { return (int)this[Demographics.Columns.ImagingBuySony]; }
			set { this[Demographics.Columns.ImagingBuySony] = value; }
		}
		/// <summary>
		/// If you were to buy a new mobile phone, how likely would you be to consider the following manufacture
		/// </summary>
		public override int ImagingBuyNokia
		{
			get { return (int)this[Demographics.Columns.ImagingBuyNokia]; }
			set { this[Demographics.Columns.ImagingBuyNokia] = value; }
		}
		/// <summary>
		/// If you were to buy a new mobile phone, how likely would you be to consider the following manufacture
		/// </summary>
		public override int ImagingBuyMotorola
		{
			get { return (int)this[Demographics.Columns.ImagingBuyMotorola]; }
			set { this[Demographics.Columns.ImagingBuyMotorola] = value; }
		}
		/// <summary>
		/// If you were to buy a new mobile phone, how likely would you be to consider the following manufacture
		/// </summary>
		public override int ImagingBuySiemens
		{
			get { return (int)this[Demographics.Columns.ImagingBuySiemens]; }
			set { this[Demographics.Columns.ImagingBuySiemens] = value; }
		}
		/// <summary>
		/// If you were to buy a new mobile phone, how likely would you be to consider the following manufacture
		/// </summary>
		public override int ImagingBuyLg
		{
			get { return (int)this[Demographics.Columns.ImagingBuyLg]; }
			set { this[Demographics.Columns.ImagingBuyLg] = value; }
		}
		/// <summary>
		/// If you were to buy a new mobile phone, how likely would you be to consider the following manufacture
		/// </summary>
		public override int ImagingBuySamsung
		{
			get { return (int)this[Demographics.Columns.ImagingBuySamsung]; }
			set { this[Demographics.Columns.ImagingBuySamsung] = value; }
		}
		#endregion


	}
	#endregion

}





















































