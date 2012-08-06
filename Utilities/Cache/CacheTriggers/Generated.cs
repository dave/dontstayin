using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using Microsoft.SqlServer.Server;

namespace CacheTriggers
{

	public class Triggers 
	{ 
		public static void AbuseTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Abuse"
				, "C8CB24BE",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void AdminTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Admin"
				, "FD30E1A8",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void ArticleTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Article"
				, "D389F153",
				new string[] {
					"Place"
					,
					"Country"
					,
					"Event"
					,
					"Thread"
					,
					"Venue"
				},
				new string[] {
				}
			);
		}
		
		public static void BacardiEmailTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"BacardiEmail"
				, "6407625D",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void BankExportTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"BankExport"
				, "9471DF4D",
				new string[] {
					"Promoter"
					,
					"Transfer"
				},
				new string[] {
				}
			);
		}
		
		public static void BannerTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Banner"
				, "73D8C33D",
				new string[] {
					"Misc"
					,
					"Promoter"
					,
					"BannerFolder"
					,
					"Brand"
					,
					"Event"
					,
					"Usr"
					,
					"Venue"
				},
				new string[] {
				}
			);
		}
		
		public static void BannerFolderTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"BannerFolder"
				, "F4A4B2D6",
				new string[] {
					"Promoter"
					,
					"Event"
				},
				new string[] {
				}
			);
		}
		
		public static void BrandTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Brand"
				, "7CDA01A4",
				new string[] {
					"Promoter"
					,
					"Group"
				},
				new string[] {
				}
			);
		}
		
		public static void BuddyTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Buddy"
				, "85FC3E35",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void CampaignCreditTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"CampaignCredit"
				, "CDCF195E",
				new string[] {
					"Promoter"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void ChatMessageTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"ChatMessage"
				, "FEBF12DC",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void ClubDetailsTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"ClubDetails"
				, "8E6B772A",
				new string[] {
					"Promoter"
					,
					"Venue"
				},
				new string[] {
				}
			);
		}
		
		public static void CommentTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Comment"
				, "B51CFA22",
				new string[] {
					"Usr"
					,
					"Thread"
				},
				new string[] {
				}
			);
		}
		
		public static void CompTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Comp"
				, "61E0C58E",
				new string[] {
					"Promoter"
					,
					"Brand"
					,
					"Event"
				},
				new string[] {
				}
			);
		}
		
		public static void CountryTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Country"
				, "F4C72973",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void DomainTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Domain"
				, "6F78DD45",
				new string[] {
					"Promoter"
				},
				new string[] {
				}
			);
		}
		
		public static void DonationIconTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"DonationIcon"
				, "DCDBA406",
				new string[] {
					"Thread"
				},
				new string[] {
				}
			);
		}
		
		public static void EventTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Event"
				, "8F18B18D",
				new string[] {
					"Venue"
				},
				new string[] {
					"DateTime"
				}
			);
		}
		
		public static void FacebookPostTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"FacebookPost"
				, "6D42EC75",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void Fiat500EntryTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Fiat500Entry"
				, "79123DA7",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void FlyerTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Flyer"
				, "1933EADE",
				new string[] {
					"Misc"
					,
					"Promoter"
					,
					"Event"
				},
				new string[] {
				}
			);
		}
		
		public static void GalleryTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Gallery"
				, "A1E9A35D",
				new string[] {
					"Article"
					,
					"Event"
				},
				new string[] {
				}
			);
		}
		
		public static void GlobalTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Global"
				, "B70C679F",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void GroupTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Group"
				, "81294206",
				new string[] {
					"Place"
					,
					"MusicType"
					,
					"Country"
					,
					"Brand"
					,
					"Theme"
				},
				new string[] {
				}
			);
		}
		
		public static void GroupPhotoTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"GroupPhoto"
				, "90DFAF32",
				new string[] {
					"Photo"
					,
					"Group"
				},
				new string[] {
				}
			);
		}
		
		public static void GuestlistCreditTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"GuestlistCredit"
				, "7BD67FFA",
				new string[] {
					"Promoter"
				},
				new string[] {
				}
			);
		}
		
		public static void HitTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Hit"
				, "AA59A111",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void IncomingSmsTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"IncomingSms"
				, "3B819721",
				new string[] {
					"Mobile"
				},
				new string[] {
				}
			);
		}
		
		public static void InsertionOrderTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"InsertionOrder"
				, "050AA86B",
				new string[] {
					"Promoter"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void InsertionOrderItemTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"InsertionOrderItem"
				, "008F89FF",
				new string[] {
					"InsertionOrder"
				},
				new string[] {
				}
			);
		}
		
		public static void InvoiceTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Invoice"
				, "E3B130F4",
				new string[] {
					"InsertionOrder"
					,
					"Promoter"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void InvoiceItemTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"InvoiceItem"
				, "4632788C",
				new string[] {
					"Invoice"
				},
				new string[] {
				}
			);
		}
		
		public static void LogPageTimeTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"LogPageTime"
				, "5A4210C2",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void LolTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Lol"
				, "B9944A9A",
				new string[] {
					"Usr"
					,
					"Comment"
				},
				new string[] {
				}
			);
		}
		
		public static void MiscTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Misc"
				, "DEDF0D0A",
				new string[] {
					"Promoter"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void MixmagEntryTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"MixmagEntry"
				, "2F19DDF0",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void MixmagGreatestDjTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"MixmagGreatestDj"
				, "517C0CA5",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void MixmagIssueTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"MixmagIssue"
				, "96A53D9E",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void MixmagSubscriptionTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"MixmagSubscription"
				, "2AB10F32",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void MixmagVoteTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"MixmagVote"
				, "10B5EA57",
				new string[] {
					"MixmagEntry"
				},
				new string[] {
				}
			);
		}
		
		public static void MobileTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Mobile"
				, "45E0E482",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void MusicTypeTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"MusicType"
				, "548B4966",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void OutgoingSmsTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"OutgoingSms"
				, "35F6F63C",
				new string[] {
					"Mobile"
					,
					"IncomingSms"
				},
				new string[] {
				}
			);
		}
		
		public static void PageTimeTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"PageTime"
				, "F6344C1B",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void ParaTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Para"
				, "325990B0",
				new string[] {
					"Photo"
					,
					"Article"
					,
					"Thread"
				},
				new string[] {
				}
			);
		}
		
		public static void PhoneTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Phone"
				, "A40108A6",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void PhotoTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Photo"
				, "71653456",
				new string[] {
					"Mobile"
					,
					"Gallery"
					,
					"Article"
					,
					"Event"
					,
					"Usr"
					,
					"Thread"
				},
				new string[] {
					"UsrK"
					,
					"Status"
					,
					"IsInCaptionCompetition"
				}
			);
		}
		
		public static void PhotoReviewTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"PhotoReview"
				, "BAD94C7E",
				new string[] {
					"Photo"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void PlaceTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Place"
				, "E6E58561",
				new string[] {
					"Region"
					,
					"Country"
				},
				new string[] {
				}
			);
		}
		
		public static void PromoterTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Promoter"
				, "3B1D9CDA",
				new string[] {
					"SalesCampaign"
				},
				new string[] {
				}
			);
		}
		
		public static void RegionTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Region"
				, "5C2A5A71",
				new string[] {
					"Country"
				},
				new string[] {
				}
			);
		}
		
		public static void SalesCallTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"SalesCall"
				, "A041F5A2",
				new string[] {
					"Promoter"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void SalesCampaignTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"SalesCampaign"
				, "481F9D79",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void SalesStatusChangeTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"SalesStatusChange"
				, "5BA6B3C1",
				new string[] {
					"Promoter"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void SpottedExceptionTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"SpottedException"
				, "37CE92A7",
				new string[] {
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void TagTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Tag"
				, "17E84D6B",
				new string[] {
				},
				new string[] {
					"Blocked"
					,
					"ShowInTagCloud"
				}
			);
		}
		
		public static void TagPhotoTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"TagPhoto"
				, "9D98A21D",
				new string[] {
					"Photo"
					,
					"Tag"
				},
				new string[] {
					"Disabled"
				}
			);
		}
		
		public static void TagPhotoHistoryTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"TagPhotoHistory"
				, "CC32FE64",
				new string[] {
					"Usr"
					,
					"TagPhoto"
				},
				new string[] {
				}
			);
		}
		
		public static void ThemeTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Theme"
				, "02CCF83A",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void ThreadTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Thread"
				, "4576C3F1",
				new string[] {
					"Region"
					,
					"Place"
					,
					"MusicType"
					,
					"Country"
					,
					"Brand"
					,
					"Photo"
					,
					"Article"
					,
					"Theme"
					,
					"Event"
					,
					"Usr"
					,
					"Group"
					,
					"Venue"
				},
				new string[] {
					"Enabled"
				}
			);
		}
		
		public static void TicketTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Ticket"
				, "117368D6",
				new string[] {
					"Invoice"
					,
					"TicketRun"
					,
					"Event"
					,
					"Domain"
					,
					"InvoiceItem"
				},
				new string[] {
				}
			);
		}
		
		public static void TicketRunTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"TicketRun"
				, "D8F98D64",
				new string[] {
					"Promoter"
					,
					"Brand"
					,
					"Event"
				},
				new string[] {
				}
			);
		}
		
		public static void TransferTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Transfer"
				, "B7FD3576",
				new string[] {
					"Promoter"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void UsrTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Usr"
				, "5D76E076",
				new string[] {
				},
				new string[] {
				}
			);
		}
		
		public static void UsrDonationIconTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"UsrDonationIcon"
				, "324FC462",
				new string[] {
					"Usr"
					,
					"DonationIcon"
				},
				new string[] {
				}
			);
		}
		
		public static void UsrPhotoFavouriteTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"UsrPhotoFavourite"
				, "9743B94F",
				new string[] {
					"Photo"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void UsrPhotoMeTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"UsrPhotoMe"
				, "4CE926B4",
				new string[] {
					"Photo"
					,
					"Usr"
				},
				new string[] {
				}
			);
		}
		
		public static void VenueTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Venue"
				, "B5C142C8",
				new string[] {
					"Promoter"
					,
					"Place"
				},
				new string[] {
				}
			);
		}
		
		public static void VisitTrigger() 
		{
			Trigger.UpdateMemcachedOnTableChange
			(
				"Visit"
				, "89BFB233",
				new string[] {
					"Country"
					,
					"Usr"
					,
					"Domain"
				},
				new string[] {
				}
			);
		}
		
	}
}
