
namespace Caching.CacheKeys
{
	public static partial class Abuse
	{
		public static BobCacheKey Bob(int abuseK)
		{
			return new BobCacheKey("Abuse", abuseK, "C8CB24BE");
		}
	}
	public static partial class Admin
	{
		public static BobCacheKey Bob(int adminK)
		{
			return new BobCacheKey("Admin", adminK, "FD30E1A8");
		}
	}
	public static partial class Article
	{
		public static BobCacheKey Bob(int articleK)
		{
			return new BobCacheKey("Article", articleK, "D389F153");
		}
	
		public static BobChildren Gallerys(int articleK)
		{
			return new BobChildren("Article", articleK, "Gallery", "A1E9A35D");
		}
	
		public static BobChildren Paras(int articleK)
		{
			return new BobChildren("Article", articleK, "Para", "325990B0");
		}
	
		public static BobChildren Photos(int articleK)
		{
			return new BobChildren("Article", articleK, "Photo", "71653456");
		}
	
		public static BobChildren Threads(int articleK)
		{
			return new BobChildren("Article", articleK, "Thread", "4576C3F1");
		}
	}
	public static partial class BacardiEmail
	{
		public static BobCacheKey Bob(int bacardiemailK)
		{
			return new BobCacheKey("BacardiEmail", bacardiemailK, "6407625D");
		}
	}
	public static partial class BankExport
	{
		public static BobCacheKey Bob(int bankexportK)
		{
			return new BobCacheKey("BankExport", bankexportK, "9471DF4D");
		}
	}
	public static partial class Banner
	{
		public static BobCacheKey Bob(int bannerK)
		{
			return new BobCacheKey("Banner", bannerK, "73D8C33D");
		}
	
		public static BobChildren BannerMusicTypes(int bannerK)
		{
			return new BobChildren("Banner", bannerK, "BannerMusicType", "11F97371");
		}
	
		public static BobChildren BannerPlaces(int bannerK)
		{
			return new BobChildren("Banner", bannerK, "BannerPlace", "D5DF14E0");
		}
	
		public static BobChildren BannerStats(int bannerK)
		{
			return new BobChildren("Banner", bannerK, "BannerStat", "5928621F");
		}
	}
	public static partial class BannerFolder
	{
		public static BobCacheKey Bob(int bannerfolderK)
		{
			return new BobCacheKey("BannerFolder", bannerfolderK, "F4A4B2D6");
		}
	
		public static BobChildren Banners(int bannerfolderK)
		{
			return new BobChildren("BannerFolder", bannerfolderK, "Banner", "73D8C33D");
		}
	}
	public static partial class Brand
	{
		public static BobCacheKey Bob(int brandK)
		{
			return new BobCacheKey("Brand", brandK, "7CDA01A4");
		}
	
		public static BobChildren Banners(int brandK)
		{
			return new BobChildren("Brand", brandK, "Banner", "73D8C33D");
		}
	
		public static BobChildren Comps(int brandK)
		{
			return new BobChildren("Brand", brandK, "Comp", "61E0C58E");
		}
	
		public static BobChildren EventBrands(int brandK)
		{
			return new BobChildren("Brand", brandK, "EventBrand", "8CFC8B28");
		}
	
		public static BobChildren Groups(int brandK)
		{
			return new BobChildren("Brand", brandK, "Group", "81294206");
		}
	
		public static BobChildren Threads(int brandK)
		{
			return new BobChildren("Brand", brandK, "Thread", "4576C3F1");
		}
	
		public static BobChildren TicketRuns(int brandK)
		{
			return new BobChildren("Brand", brandK, "TicketRun", "D8F98D64");
		}
	}
	public static partial class Buddy
	{
		public static BobCacheKey Bob(int buddyK)
		{
			return new BobCacheKey("Buddy", buddyK, "85FC3E35");
		}
	}
	public static partial class CampaignCredit
	{
		public static BobCacheKey Bob(int campaigncreditK)
		{
			return new BobCacheKey("CampaignCredit", campaigncreditK, "CDCF195E");
		}
	}
	public static partial class ChatMessage
	{
		public static BobCacheKey Bob(int chatmessageK)
		{
			return new BobCacheKey("ChatMessage", chatmessageK, "FEBF12DC");
		}
	}
	public static partial class ClubDetails
	{
		public static BobCacheKey Bob(int clubdetailsK)
		{
			return new BobCacheKey("ClubDetails", clubdetailsK, "8E6B772A");
		}
	}
	public static partial class Comment
	{
		public static BobCacheKey Bob(int commentK)
		{
			return new BobCacheKey("Comment", commentK, "B51CFA22");
		}
	
		public static BobChildren Lols(int commentK)
		{
			return new BobChildren("Comment", commentK, "Lol", "B9944A9A");
		}
	}
	public static partial class Comp
	{
		public static BobCacheKey Bob(int compK)
		{
			return new BobCacheKey("Comp", compK, "61E0C58E");
		}
	
		public static BobChildren CompEntrys(int compK)
		{
			return new BobChildren("Comp", compK, "CompEntry", "CEEC0128");
		}
	}
	public static partial class Country
	{
		public static BobCacheKey Bob(int countryK)
		{
			return new BobCacheKey("Country", countryK, "F4C72973");
		}
	
		public static BobChildren Articles(int countryK)
		{
			return new BobChildren("Country", countryK, "Article", "D389F153");
		}
	
		public static BobChildren Groups(int countryK)
		{
			return new BobChildren("Country", countryK, "Group", "81294206");
		}
	
		public static BobChildren IpCountrys(int countryK)
		{
			return new BobChildren("Country", countryK, "IpCountry", "C2F26276");
		}
	
		public static BobChildren Places(int countryK)
		{
			return new BobChildren("Country", countryK, "Place", "E6E58561");
		}
	
		public static BobChildren Regions(int countryK)
		{
			return new BobChildren("Country", countryK, "Region", "5C2A5A71");
		}
	
		public static BobChildren Threads(int countryK)
		{
			return new BobChildren("Country", countryK, "Thread", "4576C3F1");
		}
	
		public static BobChildren Visits(int countryK)
		{
			return new BobChildren("Country", countryK, "Visit", "89BFB233");
		}
	}
	public static partial class Domain
	{
		public static BobCacheKey Bob(int domainK)
		{
			return new BobCacheKey("Domain", domainK, "6F78DD45");
		}
	
		public static BobChildren DomainStatss(int domainK)
		{
			return new BobChildren("Domain", domainK, "DomainStats", "2E2E29B3");
		}
	
		public static BobChildren Tickets(int domainK)
		{
			return new BobChildren("Domain", domainK, "Ticket", "117368D6");
		}
	
		public static BobChildren Visits(int domainK)
		{
			return new BobChildren("Domain", domainK, "Visit", "89BFB233");
		}
	}
	public static partial class DonationIcon
	{
		public static BobCacheKey Bob(int donationiconK)
		{
			return new BobCacheKey("DonationIcon", donationiconK, "DCDBA406");
		}
	
		public static BobChildren UsrDonationIcons(int donationiconK)
		{
			return new BobChildren("DonationIcon", donationiconK, "UsrDonationIcon", "324FC462");
		}
	}
	public static partial class Event
	{
		public static BobCacheKey Bob(int eventK)
		{
			return new BobCacheKey("Event", eventK, "8F18B18D");
		}
	
		public static BobChildren Articles(int eventK)
		{
			return new BobChildren("Event", eventK, "Article", "D389F153");
		}
	
		public static BobChildren Banners(int eventK)
		{
			return new BobChildren("Event", eventK, "Banner", "73D8C33D");
		}
	
		public static BobChildren BannerFolders(int eventK)
		{
			return new BobChildren("Event", eventK, "BannerFolder", "F4A4B2D6");
		}
	
		public static BobChildren Comps(int eventK)
		{
			return new BobChildren("Event", eventK, "Comp", "61E0C58E");
		}
	
		public static BobChildren EventBrands(int eventK)
		{
			return new BobChildren("Event", eventK, "EventBrand", "8CFC8B28");
		}
	
		public static BobChildren EventMusicTypes(int eventK)
		{
			return new BobChildren("Event", eventK, "EventMusicType", "D9782475");
		}
	
		public static BobChildren Flyers(int eventK)
		{
			return new BobChildren("Event", eventK, "Flyer", "1933EADE");
		}
	
		public static BobChildren Gallerys(int eventK)
		{
			return new BobChildren("Event", eventK, "Gallery", "A1E9A35D");
		}
	
		public static BobChildren GroupEvents(int eventK)
		{
			return new BobChildren("Event", eventK, "GroupEvent", "3AB4F1F6");
		}
	
		public static BobChildren Photos(int eventK)
		{
			return new BobChildren("Event", eventK, "Photo", "71653456");
		}
	
		public static BobChildren Threads(int eventK)
		{
			return new BobChildren("Event", eventK, "Thread", "4576C3F1");
		}
	
		public static BobChildren Tickets(int eventK)
		{
			return new BobChildren("Event", eventK, "Ticket", "117368D6");
		}
	
		public static BobChildren TicketPromoterEvents(int eventK)
		{
			return new BobChildren("Event", eventK, "TicketPromoterEvent", "4A1C9197");
		}
	
		public static BobChildren TicketRuns(int eventK)
		{
			return new BobChildren("Event", eventK, "TicketRun", "D8F98D64");
		}
	
		public static BobChildren UsrEventAttendeds(int eventK)
		{
			return new BobChildren("Event", eventK, "UsrEventAttended", "95A27612");
		}
	
		public static BobChildren UsrEventGuestlists(int eventK)
		{
			return new BobChildren("Event", eventK, "UsrEventGuestlist", "6F23F8F8");
		}
	}
	public static partial class FacebookPost
	{
		public static BobCacheKey Bob(int facebookpostK)
		{
			return new BobCacheKey("FacebookPost", facebookpostK, "6D42EC75");
		}
	}
	public static partial class Fiat500Entry
	{
		public static BobCacheKey Bob(int fiat500entryK)
		{
			return new BobCacheKey("Fiat500Entry", fiat500entryK, "79123DA7");
		}
	}
	public static partial class Flyer
	{
		public static BobCacheKey Bob(int flyerK)
		{
			return new BobCacheKey("Flyer", flyerK, "1933EADE");
		}
	}
	public static partial class Gallery
	{
		public static BobCacheKey Bob(int galleryK)
		{
			return new BobCacheKey("Gallery", galleryK, "A1E9A35D");
		}
	
		public static BobChildren GalleryUsrs(int galleryK)
		{
			return new BobChildren("Gallery", galleryK, "GalleryUsr", "73FAB6DB");
		}
	
		public static BobChildren Photos(int galleryK)
		{
			return new BobChildren("Gallery", galleryK, "Photo", "71653456");
		}
	}
	public static partial class Global
	{
		public static BobCacheKey Bob(int globalK)
		{
			return new BobCacheKey("Global", globalK, "B70C679F");
		}
	}
	public static partial class Group
	{
		public static BobCacheKey Bob(int groupK)
		{
			return new BobCacheKey("Group", groupK, "81294206");
		}
	
		public static BobChildren Brands(int groupK)
		{
			return new BobChildren("Group", groupK, "Brand", "7CDA01A4");
		}
	
		public static BobChildren GroupEvents(int groupK)
		{
			return new BobChildren("Group", groupK, "GroupEvent", "3AB4F1F6");
		}
	
		public static BobChildren GroupPhotos(int groupK)
		{
			return new BobChildren("Group", groupK, "GroupPhoto", "90DFAF32");
		}
	
		public static BobChildren GroupUsrs(int groupK)
		{
			return new BobChildren("Group", groupK, "GroupUsr", "21C961A3");
		}
	
		public static BobChildren Threads(int groupK)
		{
			return new BobChildren("Group", groupK, "Thread", "4576C3F1");
		}
	}
	public static partial class GroupPhoto
	{
		public static BobCacheKey Bob(int groupphotoK)
		{
			return new BobCacheKey("GroupPhoto", groupphotoK, "90DFAF32");
		}
	}
	public static partial class GuestlistCredit
	{
		public static BobCacheKey Bob(int guestlistcreditK)
		{
			return new BobCacheKey("GuestlistCredit", guestlistcreditK, "7BD67FFA");
		}
	}
	public static partial class Hit
	{
		public static BobCacheKey Bob(int hitK)
		{
			return new BobCacheKey("Hit", hitK, "AA59A111");
		}
	}
	public static partial class IncomingSms
	{
		public static BobCacheKey Bob(int incomingsmsK)
		{
			return new BobCacheKey("IncomingSms", incomingsmsK, "3B819721");
		}
	
		public static BobChildren OutgoingSmss(int incomingsmsK)
		{
			return new BobChildren("IncomingSms", incomingsmsK, "OutgoingSms", "35F6F63C");
		}
	}
	public static partial class InsertionOrder
	{
		public static BobCacheKey Bob(int insertionorderK)
		{
			return new BobCacheKey("InsertionOrder", insertionorderK, "050AA86B");
		}
	
		public static BobChildren InsertionOrderItems(int insertionorderK)
		{
			return new BobChildren("InsertionOrder", insertionorderK, "InsertionOrderItem", "008F89FF");
		}
	
		public static BobChildren Invoices(int insertionorderK)
		{
			return new BobChildren("InsertionOrder", insertionorderK, "Invoice", "E3B130F4");
		}
	}
	public static partial class InsertionOrderItem
	{
		public static BobCacheKey Bob(int insertionorderitemK)
		{
			return new BobCacheKey("InsertionOrderItem", insertionorderitemK, "008F89FF");
		}
	}
	public static partial class Invoice
	{
		public static BobCacheKey Bob(int invoiceK)
		{
			return new BobCacheKey("Invoice", invoiceK, "E3B130F4");
		}
	
		public static BobChildren InvoiceCredits(int invoiceK)
		{
			return new BobChildren("Invoice", invoiceK, "InvoiceCredit", "7CCFF60C");
		}
	
		public static BobChildren InvoiceItems(int invoiceK)
		{
			return new BobChildren("Invoice", invoiceK, "InvoiceItem", "4632788C");
		}
	
		public static BobChildren InvoiceTransfers(int invoiceK)
		{
			return new BobChildren("Invoice", invoiceK, "InvoiceTransfer", "D75AE2A5");
		}
	
		public static BobChildren Tickets(int invoiceK)
		{
			return new BobChildren("Invoice", invoiceK, "Ticket", "117368D6");
		}
	}
	public static partial class InvoiceItem
	{
		public static BobCacheKey Bob(int invoiceitemK)
		{
			return new BobCacheKey("InvoiceItem", invoiceitemK, "4632788C");
		}
	
		public static BobChildren Tickets(int invoiceitemK)
		{
			return new BobChildren("InvoiceItem", invoiceitemK, "Ticket", "117368D6");
		}
	}
	public static partial class LogPageTime
	{
		public static BobCacheKey Bob(int logpagetimeK)
		{
			return new BobCacheKey("LogPageTime", logpagetimeK, "5A4210C2");
		}
	}
	public static partial class Lol
	{
		public static BobCacheKey Bob(int lolK)
		{
			return new BobCacheKey("Lol", lolK, "B9944A9A");
		}
	}
	public static partial class Misc
	{
		public static BobCacheKey Bob(int miscK)
		{
			return new BobCacheKey("Misc", miscK, "DEDF0D0A");
		}
	
		public static BobChildren Banners(int miscK)
		{
			return new BobChildren("Misc", miscK, "Banner", "73D8C33D");
		}
	
		public static BobChildren Flyers(int miscK)
		{
			return new BobChildren("Misc", miscK, "Flyer", "1933EADE");
		}
	}
	public static partial class MixmagEntry
	{
		public static BobCacheKey Bob(int mixmagentryK)
		{
			return new BobCacheKey("MixmagEntry", mixmagentryK, "2F19DDF0");
		}
	
		public static BobChildren MixmagVotes(int mixmagentryK)
		{
			return new BobChildren("MixmagEntry", mixmagentryK, "MixmagVote", "10B5EA57");
		}
	}
	public static partial class MixmagGreatestDj
	{
		public static BobCacheKey Bob(int mixmaggreatestdjK)
		{
			return new BobCacheKey("MixmagGreatestDj", mixmaggreatestdjK, "517C0CA5");
		}
	
		public static BobChildren MixmagGreatestVotes(int mixmaggreatestdjK)
		{
			return new BobChildren("MixmagGreatestDj", mixmaggreatestdjK, "MixmagGreatestVote", "2BE68CF2");
		}
	}
	public static partial class MixmagIssue
	{
		public static BobCacheKey Bob(int mixmagissueK)
		{
			return new BobCacheKey("MixmagIssue", mixmagissueK, "96A53D9E");
		}
	
		public static BobChildren MixmagReads(int mixmagissueK)
		{
			return new BobChildren("MixmagIssue", mixmagissueK, "MixmagRead", "6BC24996");
		}
	}
	public static partial class MixmagSubscription
	{
		public static BobCacheKey Bob(int mixmagsubscriptionK)
		{
			return new BobCacheKey("MixmagSubscription", mixmagsubscriptionK, "2AB10F32");
		}
	}
	public static partial class MixmagVote
	{
		public static BobCacheKey Bob(int mixmagvoteK)
		{
			return new BobCacheKey("MixmagVote", mixmagvoteK, "10B5EA57");
		}
	}
	public static partial class Mobile
	{
		public static BobCacheKey Bob(int mobileK)
		{
			return new BobCacheKey("Mobile", mobileK, "45E0E482");
		}
	
		public static BobChildren IncomingSmss(int mobileK)
		{
			return new BobChildren("Mobile", mobileK, "IncomingSms", "3B819721");
		}
	
		public static BobChildren OutgoingSmss(int mobileK)
		{
			return new BobChildren("Mobile", mobileK, "OutgoingSms", "35F6F63C");
		}
	
		public static BobChildren Photos(int mobileK)
		{
			return new BobChildren("Mobile", mobileK, "Photo", "71653456");
		}
	}
	public static partial class MusicType
	{
		public static BobCacheKey Bob(int musictypeK)
		{
			return new BobCacheKey("MusicType", musictypeK, "548B4966");
		}
	
		public static BobChildren BannerMusicTypes(int musictypeK)
		{
			return new BobChildren("MusicType", musictypeK, "BannerMusicType", "11F97371");
		}
	
		public static BobChildren EventMusicTypes(int musictypeK)
		{
			return new BobChildren("MusicType", musictypeK, "EventMusicType", "D9782475");
		}
	
		public static BobChildren Groups(int musictypeK)
		{
			return new BobChildren("MusicType", musictypeK, "Group", "81294206");
		}
	
		public static BobChildren Threads(int musictypeK)
		{
			return new BobChildren("MusicType", musictypeK, "Thread", "4576C3F1");
		}
	
		public static BobChildren UsrMusicTypeFavourites(int musictypeK)
		{
			return new BobChildren("MusicType", musictypeK, "UsrMusicTypeFavourite", "48E84A36");
		}
	}
	public static partial class OutgoingSms
	{
		public static BobCacheKey Bob(int outgoingsmsK)
		{
			return new BobCacheKey("OutgoingSms", outgoingsmsK, "35F6F63C");
		}
	}
	public static partial class PageTime
	{
		public static BobCacheKey Bob(int pagetimeK)
		{
			return new BobCacheKey("PageTime", pagetimeK, "F6344C1B");
		}
	}
	public static partial class Para
	{
		public static BobCacheKey Bob(int paraK)
		{
			return new BobCacheKey("Para", paraK, "325990B0");
		}
	}
	public static partial class Phone
	{
		public static BobCacheKey Bob(int phoneK)
		{
			return new BobCacheKey("Phone", phoneK, "A40108A6");
		}
	}
	public static partial class Photo
	{
		public static BobCacheKey Bob(int photoK)
		{
			return new BobCacheKey("Photo", photoK, "71653456");
		}
	
		public static BobChildren GroupPhotos(int photoK)
		{
			return new BobChildren("Photo", photoK, "GroupPhoto", "90DFAF32");
		}
	
		public static BobChildren Paras(int photoK)
		{
			return new BobChildren("Photo", photoK, "Para", "325990B0");
		}
	
		public static BobChildren PhotoReviews(int photoK)
		{
			return new BobChildren("Photo", photoK, "PhotoReview", "BAD94C7E");
		}
	
		public static BobChildren TagPhotos(int photoK)
		{
			return new BobChildren("Photo", photoK, "TagPhoto", "9D98A21D");
		}
	
		public static BobChildren Threads(int photoK)
		{
			return new BobChildren("Photo", photoK, "Thread", "4576C3F1");
		}
	
		public static BobChildren UsrPhotoFavourites(int photoK)
		{
			return new BobChildren("Photo", photoK, "UsrPhotoFavourite", "9743B94F");
		}
	
		public static BobChildren UsrPhotoMes(int photoK)
		{
			return new BobChildren("Photo", photoK, "UsrPhotoMe", "4CE926B4");
		}
	}
	public static partial class PhotoReview
	{
		public static BobCacheKey Bob(int photoreviewK)
		{
			return new BobCacheKey("PhotoReview", photoreviewK, "BAD94C7E");
		}
	}
	public static partial class Place
	{
		public static BobCacheKey Bob(int placeK)
		{
			return new BobCacheKey("Place", placeK, "E6E58561");
		}
	
		public static BobChildren Articles(int placeK)
		{
			return new BobChildren("Place", placeK, "Article", "D389F153");
		}
	
		public static BobChildren BannerPlaces(int placeK)
		{
			return new BobChildren("Place", placeK, "BannerPlace", "D5DF14E0");
		}
	
		public static BobChildren Groups(int placeK)
		{
			return new BobChildren("Place", placeK, "Group", "81294206");
		}
	
		public static BobChildren Threads(int placeK)
		{
			return new BobChildren("Place", placeK, "Thread", "4576C3F1");
		}
	
		public static BobChildren UsrPlaceVisits(int placeK)
		{
			return new BobChildren("Place", placeK, "UsrPlaceVisit", "0DF9F093");
		}
	
		public static BobChildren Venues(int placeK)
		{
			return new BobChildren("Place", placeK, "Venue", "B5C142C8");
		}
	}
	public static partial class Promoter
	{
		public static BobCacheKey Bob(int promoterK)
		{
			return new BobCacheKey("Promoter", promoterK, "3B1D9CDA");
		}
	
		public static BobChildren BankExports(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "BankExport", "9471DF4D");
		}
	
		public static BobChildren Banners(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Banner", "73D8C33D");
		}
	
		public static BobChildren BannerFolders(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "BannerFolder", "F4A4B2D6");
		}
	
		public static BobChildren Brands(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Brand", "7CDA01A4");
		}
	
		public static BobChildren CampaignCredits(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "CampaignCredit", "CDCF195E");
		}
	
		public static BobChildren ClubDetailss(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "ClubDetails", "8E6B772A");
		}
	
		public static BobChildren Comps(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Comp", "61E0C58E");
		}
	
		public static BobChildren Domains(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Domain", "6F78DD45");
		}
	
		public static BobChildren Flyers(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Flyer", "1933EADE");
		}
	
		public static BobChildren GuestlistCredits(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "GuestlistCredit", "7BD67FFA");
		}
	
		public static BobChildren InsertionOrders(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "InsertionOrder", "050AA86B");
		}
	
		public static BobChildren Invoices(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Invoice", "E3B130F4");
		}
	
		public static BobChildren Miscs(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Misc", "DEDF0D0A");
		}
	
		public static BobChildren PromoterUsrs(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "PromoterUsr", "6F1F45E8");
		}
	
		public static BobChildren SalesCalls(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "SalesCall", "A041F5A2");
		}
	
		public static BobChildren SalesStatusChanges(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "SalesStatusChange", "5BA6B3C1");
		}
	
		public static BobChildren TicketPromoterEvents(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "TicketPromoterEvent", "4A1C9197");
		}
	
		public static BobChildren TicketRuns(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "TicketRun", "D8F98D64");
		}
	
		public static BobChildren Transfers(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Transfer", "B7FD3576");
		}
	
		public static BobChildren Venues(int promoterK)
		{
			return new BobChildren("Promoter", promoterK, "Venue", "B5C142C8");
		}
	}
	public static partial class Region
	{
		public static BobCacheKey Bob(int regionK)
		{
			return new BobCacheKey("Region", regionK, "5C2A5A71");
		}
	
		public static BobChildren Places(int regionK)
		{
			return new BobChildren("Region", regionK, "Place", "E6E58561");
		}
	
		public static BobChildren Threads(int regionK)
		{
			return new BobChildren("Region", regionK, "Thread", "4576C3F1");
		}
	}
	public static partial class SalesCall
	{
		public static BobCacheKey Bob(int salescallK)
		{
			return new BobCacheKey("SalesCall", salescallK, "A041F5A2");
		}
	}
	public static partial class SalesCampaign
	{
		public static BobCacheKey Bob(int salescampaignK)
		{
			return new BobCacheKey("SalesCampaign", salescampaignK, "481F9D79");
		}
	
		public static BobChildren Promoters(int salescampaignK)
		{
			return new BobChildren("SalesCampaign", salescampaignK, "Promoter", "3B1D9CDA");
		}
	}
	public static partial class SalesStatusChange
	{
		public static BobCacheKey Bob(int salesstatuschangeK)
		{
			return new BobCacheKey("SalesStatusChange", salesstatuschangeK, "5BA6B3C1");
		}
	}
	public static partial class SpottedException
	{
		public static BobCacheKey Bob(int spottedexceptionK)
		{
			return new BobCacheKey("SpottedException", spottedexceptionK, "37CE92A7");
		}
	}
	public static partial class Tag
	{
		public static BobCacheKey Bob(int tagK)
		{
			return new BobCacheKey("Tag", tagK, "17E84D6B");
		}
	
		public static BobChildren TagPhotos(int tagK)
		{
			return new BobChildren("Tag", tagK, "TagPhoto", "9D98A21D");
		}
	}
	public static partial class TagPhoto
	{
		public static BobCacheKey Bob(int tagphotoK)
		{
			return new BobCacheKey("TagPhoto", tagphotoK, "9D98A21D");
		}
	
		public static BobChildren TagPhotoHistorys(int tagphotoK)
		{
			return new BobChildren("TagPhoto", tagphotoK, "TagPhotoHistory", "CC32FE64");
		}
	}
	public static partial class TagPhotoHistory
	{
		public static BobCacheKey Bob(int tagphotohistoryK)
		{
			return new BobCacheKey("TagPhotoHistory", tagphotohistoryK, "CC32FE64");
		}
	}
	public static partial class Theme
	{
		public static BobCacheKey Bob(int themeK)
		{
			return new BobCacheKey("Theme", themeK, "02CCF83A");
		}
	
		public static BobChildren Groups(int themeK)
		{
			return new BobChildren("Theme", themeK, "Group", "81294206");
		}
	
		public static BobChildren Threads(int themeK)
		{
			return new BobChildren("Theme", themeK, "Thread", "4576C3F1");
		}
	}
	public static partial class Thread
	{
		public static BobCacheKey Bob(int threadK)
		{
			return new BobCacheKey("Thread", threadK, "4576C3F1");
		}
	
		public static BobChildren Articles(int threadK)
		{
			return new BobChildren("Thread", threadK, "Article", "D389F153");
		}
	
		public static BobChildren Comments(int threadK)
		{
			return new BobChildren("Thread", threadK, "Comment", "B51CFA22");
		}
	
		public static BobChildren DonationIcons(int threadK)
		{
			return new BobChildren("Thread", threadK, "DonationIcon", "DCDBA406");
		}
	
		public static BobChildren Paras(int threadK)
		{
			return new BobChildren("Thread", threadK, "Para", "325990B0");
		}
	
		public static BobChildren Photos(int threadK)
		{
			return new BobChildren("Thread", threadK, "Photo", "71653456");
		}
	
		public static BobChildren ThreadUsrs(int threadK)
		{
			return new BobChildren("Thread", threadK, "ThreadUsr", "02B49E6F");
		}
	}
	public static partial class Ticket
	{
		public static BobCacheKey Bob(int ticketK)
		{
			return new BobCacheKey("Ticket", ticketK, "117368D6");
		}
	}
	public static partial class TicketRun
	{
		public static BobCacheKey Bob(int ticketrunK)
		{
			return new BobCacheKey("TicketRun", ticketrunK, "D8F98D64");
		}
	
		public static BobChildren Tickets(int ticketrunK)
		{
			return new BobChildren("TicketRun", ticketrunK, "Ticket", "117368D6");
		}
	}
	public static partial class Transfer
	{
		public static BobCacheKey Bob(int transferK)
		{
			return new BobCacheKey("Transfer", transferK, "B7FD3576");
		}
	
		public static BobChildren BankExports(int transferK)
		{
			return new BobChildren("Transfer", transferK, "BankExport", "9471DF4D");
		}
	
		public static BobChildren InvoiceTransfers(int transferK)
		{
			return new BobChildren("Transfer", transferK, "InvoiceTransfer", "D75AE2A5");
		}
	}
	public static partial class Usr
	{
		public static BobCacheKey Bob(int usrK)
		{
			return new BobCacheKey("Usr", usrK, "5D76E076");
		}
	
		public static BobChildren Admins(int usrK)
		{
			return new BobChildren("Usr", usrK, "Admin", "FD30E1A8");
		}
	
		public static BobChildren Banners(int usrK)
		{
			return new BobChildren("Usr", usrK, "Banner", "73D8C33D");
		}
	
		public static BobChildren Buddys(int usrK)
		{
			return new BobChildren("Usr", usrK, "Buddy", "85FC3E35");
		}
	
		public static BobChildren CampaignCredits(int usrK)
		{
			return new BobChildren("Usr", usrK, "CampaignCredit", "CDCF195E");
		}
	
		public static BobChildren Chats(int usrK)
		{
			return new BobChildren("Usr", usrK, "Chat", "497351AF");
		}
	
		public static BobChildren ChatMessages(int usrK)
		{
			return new BobChildren("Usr", usrK, "ChatMessage", "FEBF12DC");
		}
	
		public static BobChildren Comments(int usrK)
		{
			return new BobChildren("Usr", usrK, "Comment", "B51CFA22");
		}
	
		public static BobChildren CommentAlerts(int usrK)
		{
			return new BobChildren("Usr", usrK, "CommentAlert", "E8EF0595");
		}
	
		public static BobChildren CompEntrys(int usrK)
		{
			return new BobChildren("Usr", usrK, "CompEntry", "CEEC0128");
		}
	
		public static BobChildren FacebookPosts(int usrK)
		{
			return new BobChildren("Usr", usrK, "FacebookPost", "6D42EC75");
		}
	
		public static BobChildren Fiat500Entrys(int usrK)
		{
			return new BobChildren("Usr", usrK, "Fiat500Entry", "79123DA7");
		}
	
		public static BobChildren GalleryUsrs(int usrK)
		{
			return new BobChildren("Usr", usrK, "GalleryUsr", "73FAB6DB");
		}
	
		public static BobChildren GroupUsrs(int usrK)
		{
			return new BobChildren("Usr", usrK, "GroupUsr", "21C961A3");
		}
	
		public static BobChildren Hits(int usrK)
		{
			return new BobChildren("Usr", usrK, "Hit", "AA59A111");
		}
	
		public static BobChildren InsertionOrders(int usrK)
		{
			return new BobChildren("Usr", usrK, "InsertionOrder", "050AA86B");
		}
	
		public static BobChildren Invoices(int usrK)
		{
			return new BobChildren("Usr", usrK, "Invoice", "E3B130F4");
		}
	
		public static BobChildren LogPageTimes(int usrK)
		{
			return new BobChildren("Usr", usrK, "LogPageTime", "5A4210C2");
		}
	
		public static BobChildren Lols(int usrK)
		{
			return new BobChildren("Usr", usrK, "Lol", "B9944A9A");
		}
	
		public static BobChildren Miscs(int usrK)
		{
			return new BobChildren("Usr", usrK, "Misc", "DEDF0D0A");
		}
	
		public static BobChildren Mobiles(int usrK)
		{
			return new BobChildren("Usr", usrK, "Mobile", "45E0E482");
		}
	
		public static BobChildren Phones(int usrK)
		{
			return new BobChildren("Usr", usrK, "Phone", "A40108A6");
		}
	
		public static BobChildren Photos(int usrK)
		{
			return new BobChildren("Usr", usrK, "Photo", "71653456");
		}
	
		public static BobChildren PhotoReviews(int usrK)
		{
			return new BobChildren("Usr", usrK, "PhotoReview", "BAD94C7E");
		}
	
		public static BobChildren PromoterUsrs(int usrK)
		{
			return new BobChildren("Usr", usrK, "PromoterUsr", "6F1F45E8");
		}
	
		public static BobChildren RoomPins(int usrK)
		{
			return new BobChildren("Usr", usrK, "RoomPin", "92EE52CA");
		}
	
		public static BobChildren SalesCalls(int usrK)
		{
			return new BobChildren("Usr", usrK, "SalesCall", "A041F5A2");
		}
	
		public static BobChildren SalesCampaigns(int usrK)
		{
			return new BobChildren("Usr", usrK, "SalesCampaign", "481F9D79");
		}
	
		public static BobChildren SalesStatusChanges(int usrK)
		{
			return new BobChildren("Usr", usrK, "SalesStatusChange", "5BA6B3C1");
		}
	
		public static BobChildren SpottedExceptions(int usrK)
		{
			return new BobChildren("Usr", usrK, "SpottedException", "37CE92A7");
		}
	
		public static BobChildren TagPhotoHistorys(int usrK)
		{
			return new BobChildren("Usr", usrK, "TagPhotoHistory", "CC32FE64");
		}
	
		public static BobChildren Threads(int usrK)
		{
			return new BobChildren("Usr", usrK, "Thread", "4576C3F1");
		}
	
		public static BobChildren ThreadUsrs(int usrK)
		{
			return new BobChildren("Usr", usrK, "ThreadUsr", "02B49E6F");
		}
	
		public static BobChildren Transfers(int usrK)
		{
			return new BobChildren("Usr", usrK, "Transfer", "B7FD3576");
		}
	
		public static BobChildren UsrDates(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrDate", "2A289ED0");
		}
	
		public static BobChildren UsrDonationIcons(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrDonationIcon", "324FC462");
		}
	
		public static BobChildren UsrEventAttendeds(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrEventAttended", "95A27612");
		}
	
		public static BobChildren UsrEventGuestlists(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrEventGuestlist", "6F23F8F8");
		}
	
		public static BobChildren UsrMusicTypeFavourites(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrMusicTypeFavourite", "48E84A36");
		}
	
		public static BobChildren UsrPhotoFavourites(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrPhotoFavourite", "9743B94F");
		}
	
		public static BobChildren UsrPhotoMes(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrPhotoMe", "4CE926B4");
		}
	
		public static BobChildren UsrPlaceVisits(int usrK)
		{
			return new BobChildren("Usr", usrK, "UsrPlaceVisit", "0DF9F093");
		}
	
		public static BobChildren Visits(int usrK)
		{
			return new BobChildren("Usr", usrK, "Visit", "89BFB233");
		}
	}
	public static partial class UsrDonationIcon
	{
		public static BobCacheKey Bob(int usrdonationiconK)
		{
			return new BobCacheKey("UsrDonationIcon", usrdonationiconK, "324FC462");
		}
	}
	public static partial class UsrPhotoFavourite
	{
		public static BobCacheKey Bob(int usrphotofavouriteK)
		{
			return new BobCacheKey("UsrPhotoFavourite", usrphotofavouriteK, "9743B94F");
		}
	}
	public static partial class UsrPhotoMe
	{
		public static BobCacheKey Bob(int usrphotomeK)
		{
			return new BobCacheKey("UsrPhotoMe", usrphotomeK, "4CE926B4");
		}
	}
	public static partial class Venue
	{
		public static BobCacheKey Bob(int venueK)
		{
			return new BobCacheKey("Venue", venueK, "B5C142C8");
		}
	
		public static BobChildren Articles(int venueK)
		{
			return new BobChildren("Venue", venueK, "Article", "D389F153");
		}
	
		public static BobChildren Banners(int venueK)
		{
			return new BobChildren("Venue", venueK, "Banner", "73D8C33D");
		}
	
		public static BobChildren ClubDetailss(int venueK)
		{
			return new BobChildren("Venue", venueK, "ClubDetails", "8E6B772A");
		}
	
		public static BobChildren Events(int venueK)
		{
			return new BobChildren("Venue", venueK, "Event", "8F18B18D");
		}
	
		public static BobChildren Threads(int venueK)
		{
			return new BobChildren("Venue", venueK, "Thread", "4576C3F1");
		}
	}
	public static partial class Visit
	{
		public static BobCacheKey Bob(int visitK)
		{
			return new BobCacheKey("Visit", visitK, "89BFB233");
		}
	}
}
