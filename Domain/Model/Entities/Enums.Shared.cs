#if SCRIPTSHARP
namespace Js.Library
#else
namespace Model.Entities
#endif
{
	public enum ObjectType
	{
		Photo = 1,
		Event = 2,
		Venue = 3,
		Place = 4,
		None = 5,
		Thread = 6,
		Country = 7,
		Article = 8,
		Para = 9,
		Brand = 10,
		Promoter = 11,
		MainPage = 999,
		Usr = 12,
		Region = 13,
		Gallery = 14,
		Group = 15,
		Banner = 16,
		GuestlistCredit = 17,
		Ticket = 18,
		InsertionOrder = 19,
		EmailSpotlight = 20,
		CampaignCredit = 21,
		Invoice = 22,
		Comp = 23,
		Misc = 24,
		UsrDonationIcon = 25
	}
}
