/*
--Need to do this before we start

EXEC sp_configure 'clr enabled', 1
RECONFIGURE

--Restart SQL server now

ALTER DATABASE db_spotted SET TRUSTWORTHY ON

EXEC dbo.sp_changedbowner @loginame = N'sa', @map = false

--Run the following to import all triggers


IF OBJECT_ID ('AbuseDeleteTrigger') IS NOT NULL DROP TRIGGER AbuseDeleteTrigger
IF OBJECT_ID ('AbuseUpdateTrigger') IS NOT NULL DROP TRIGGER AbuseUpdateTrigger
IF OBJECT_ID ('AdminDeleteTrigger') IS NOT NULL DROP TRIGGER AdminDeleteTrigger
IF OBJECT_ID ('AdminUpdateTrigger') IS NOT NULL DROP TRIGGER AdminUpdateTrigger
IF OBJECT_ID ('ArticleDeleteTrigger') IS NOT NULL DROP TRIGGER ArticleDeleteTrigger
IF OBJECT_ID ('ArticleUpdateTrigger') IS NOT NULL DROP TRIGGER ArticleUpdateTrigger
IF OBJECT_ID ('BankExportDeleteTrigger') IS NOT NULL DROP TRIGGER BankExportDeleteTrigger
IF OBJECT_ID ('BankExportUpdateTrigger') IS NOT NULL DROP TRIGGER BankExportUpdateTrigger
IF OBJECT_ID ('BannerDeleteTrigger') IS NOT NULL DROP TRIGGER BannerDeleteTrigger
IF OBJECT_ID ('BannerUpdateTrigger') IS NOT NULL DROP TRIGGER BannerUpdateTrigger
IF OBJECT_ID ('BrandDeleteTrigger') IS NOT NULL DROP TRIGGER BrandDeleteTrigger
IF OBJECT_ID ('BrandUpdateTrigger') IS NOT NULL DROP TRIGGER BrandUpdateTrigger
IF OBJECT_ID ('ChatDeleteTrigger') IS NOT NULL DROP TRIGGER ChatDeleteTrigger
IF OBJECT_ID ('ChatUpdateTrigger') IS NOT NULL DROP TRIGGER ChatUpdateTrigger
IF OBJECT_ID ('ChatMessageDeleteTrigger') IS NOT NULL DROP TRIGGER ChatMessageDeleteTrigger
IF OBJECT_ID ('ChatMessageUpdateTrigger') IS NOT NULL DROP TRIGGER ChatMessageUpdateTrigger
IF OBJECT_ID ('ClubDetailsDeleteTrigger') IS NOT NULL DROP TRIGGER ClubDetailsDeleteTrigger
IF OBJECT_ID ('ClubDetailsUpdateTrigger') IS NOT NULL DROP TRIGGER ClubDetailsUpdateTrigger
IF OBJECT_ID ('CommentDeleteTrigger') IS NOT NULL DROP TRIGGER CommentDeleteTrigger
IF OBJECT_ID ('CommentUpdateTrigger') IS NOT NULL DROP TRIGGER CommentUpdateTrigger
IF OBJECT_ID ('CompDeleteTrigger') IS NOT NULL DROP TRIGGER CompDeleteTrigger
IF OBJECT_ID ('CompUpdateTrigger') IS NOT NULL DROP TRIGGER CompUpdateTrigger
IF OBJECT_ID ('CountryDeleteTrigger') IS NOT NULL DROP TRIGGER CountryDeleteTrigger
IF OBJECT_ID ('CountryUpdateTrigger') IS NOT NULL DROP TRIGGER CountryUpdateTrigger
IF OBJECT_ID ('DemographicsDeleteTrigger') IS NOT NULL DROP TRIGGER DemographicsDeleteTrigger
IF OBJECT_ID ('DemographicsUpdateTrigger') IS NOT NULL DROP TRIGGER DemographicsUpdateTrigger
IF OBJECT_ID ('DomainDeleteTrigger') IS NOT NULL DROP TRIGGER DomainDeleteTrigger
IF OBJECT_ID ('DomainUpdateTrigger') IS NOT NULL DROP TRIGGER DomainUpdateTrigger
IF OBJECT_ID ('EventDeleteTrigger') IS NOT NULL DROP TRIGGER EventDeleteTrigger
IF OBJECT_ID ('EventUpdateTrigger') IS NOT NULL DROP TRIGGER EventUpdateTrigger
IF OBJECT_ID ('EventPerformerDeleteTrigger') IS NOT NULL DROP TRIGGER EventPerformerDeleteTrigger
IF OBJECT_ID ('EventPerformerUpdateTrigger') IS NOT NULL DROP TRIGGER EventPerformerUpdateTrigger
IF OBJECT_ID ('GalleryDeleteTrigger') IS NOT NULL DROP TRIGGER GalleryDeleteTrigger
IF OBJECT_ID ('GalleryUpdateTrigger') IS NOT NULL DROP TRIGGER GalleryUpdateTrigger
IF OBJECT_ID ('GlobalDeleteTrigger') IS NOT NULL DROP TRIGGER GlobalDeleteTrigger
IF OBJECT_ID ('GlobalUpdateTrigger') IS NOT NULL DROP TRIGGER GlobalUpdateTrigger
IF OBJECT_ID ('GroupDeleteTrigger') IS NOT NULL DROP TRIGGER GroupDeleteTrigger
IF OBJECT_ID ('GroupUpdateTrigger') IS NOT NULL DROP TRIGGER GroupUpdateTrigger
IF OBJECT_ID ('GuestlistCreditDeleteTrigger') IS NOT NULL DROP TRIGGER GuestlistCreditDeleteTrigger
IF OBJECT_ID ('GuestlistCreditUpdateTrigger') IS NOT NULL DROP TRIGGER GuestlistCreditUpdateTrigger
IF OBJECT_ID ('HitDeleteTrigger') IS NOT NULL DROP TRIGGER HitDeleteTrigger
IF OBJECT_ID ('HitUpdateTrigger') IS NOT NULL DROP TRIGGER HitUpdateTrigger
IF OBJECT_ID ('IncomingSmsDeleteTrigger') IS NOT NULL DROP TRIGGER IncomingSmsDeleteTrigger
IF OBJECT_ID ('IncomingSmsUpdateTrigger') IS NOT NULL DROP TRIGGER IncomingSmsUpdateTrigger
IF OBJECT_ID ('InvoiceDeleteTrigger') IS NOT NULL DROP TRIGGER InvoiceDeleteTrigger
IF OBJECT_ID ('InvoiceUpdateTrigger') IS NOT NULL DROP TRIGGER InvoiceUpdateTrigger
IF OBJECT_ID ('InvoiceItemDeleteTrigger') IS NOT NULL DROP TRIGGER InvoiceItemDeleteTrigger
IF OBJECT_ID ('InvoiceItemUpdateTrigger') IS NOT NULL DROP TRIGGER InvoiceItemUpdateTrigger
IF OBJECT_ID ('InvoiceLinkDeleteTrigger') IS NOT NULL DROP TRIGGER InvoiceLinkDeleteTrigger
IF OBJECT_ID ('InvoiceLinkUpdateTrigger') IS NOT NULL DROP TRIGGER InvoiceLinkUpdateTrigger
IF OBJECT_ID ('LolDeleteTrigger') IS NOT NULL DROP TRIGGER LolDeleteTrigger
IF OBJECT_ID ('LolUpdateTrigger') IS NOT NULL DROP TRIGGER LolUpdateTrigger
IF OBJECT_ID ('MiscDeleteTrigger') IS NOT NULL DROP TRIGGER MiscDeleteTrigger
IF OBJECT_ID ('MiscUpdateTrigger') IS NOT NULL DROP TRIGGER MiscUpdateTrigger
IF OBJECT_ID ('MobileDeleteTrigger') IS NOT NULL DROP TRIGGER MobileDeleteTrigger
IF OBJECT_ID ('MobileUpdateTrigger') IS NOT NULL DROP TRIGGER MobileUpdateTrigger
IF OBJECT_ID ('MusicTypeDeleteTrigger') IS NOT NULL DROP TRIGGER MusicTypeDeleteTrigger
IF OBJECT_ID ('MusicTypeUpdateTrigger') IS NOT NULL DROP TRIGGER MusicTypeUpdateTrigger
IF OBJECT_ID ('OutgoingSmsDeleteTrigger') IS NOT NULL DROP TRIGGER OutgoingSmsDeleteTrigger
IF OBJECT_ID ('OutgoingSmsUpdateTrigger') IS NOT NULL DROP TRIGGER OutgoingSmsUpdateTrigger
IF OBJECT_ID ('PageTimeDeleteTrigger') IS NOT NULL DROP TRIGGER PageTimeDeleteTrigger
IF OBJECT_ID ('PageTimeUpdateTrigger') IS NOT NULL DROP TRIGGER PageTimeUpdateTrigger
IF OBJECT_ID ('ParaDeleteTrigger') IS NOT NULL DROP TRIGGER ParaDeleteTrigger
IF OBJECT_ID ('ParaUpdateTrigger') IS NOT NULL DROP TRIGGER ParaUpdateTrigger
IF OBJECT_ID ('PerformanceReviewDeleteTrigger') IS NOT NULL DROP TRIGGER PerformanceReviewDeleteTrigger
IF OBJECT_ID ('PerformanceReviewUpdateTrigger') IS NOT NULL DROP TRIGGER PerformanceReviewUpdateTrigger
IF OBJECT_ID ('PerformerDeleteTrigger') IS NOT NULL DROP TRIGGER PerformerDeleteTrigger
IF OBJECT_ID ('PerformerUpdateTrigger') IS NOT NULL DROP TRIGGER PerformerUpdateTrigger
IF OBJECT_ID ('PhoneDeleteTrigger') IS NOT NULL DROP TRIGGER PhoneDeleteTrigger
IF OBJECT_ID ('PhoneUpdateTrigger') IS NOT NULL DROP TRIGGER PhoneUpdateTrigger
IF OBJECT_ID ('PhotoDeleteTrigger') IS NOT NULL DROP TRIGGER PhotoDeleteTrigger
IF OBJECT_ID ('PhotoUpdateTrigger') IS NOT NULL DROP TRIGGER PhotoUpdateTrigger
IF OBJECT_ID ('PhotoReviewDeleteTrigger') IS NOT NULL DROP TRIGGER PhotoReviewDeleteTrigger
IF OBJECT_ID ('PhotoReviewUpdateTrigger') IS NOT NULL DROP TRIGGER PhotoReviewUpdateTrigger
IF OBJECT_ID ('PlaceDeleteTrigger') IS NOT NULL DROP TRIGGER PlaceDeleteTrigger
IF OBJECT_ID ('PlaceUpdateTrigger') IS NOT NULL DROP TRIGGER PlaceUpdateTrigger
IF OBJECT_ID ('PromoterDeleteTrigger') IS NOT NULL DROP TRIGGER PromoterDeleteTrigger
IF OBJECT_ID ('PromoterUpdateTrigger') IS NOT NULL DROP TRIGGER PromoterUpdateTrigger
IF OBJECT_ID ('RegionDeleteTrigger') IS NOT NULL DROP TRIGGER RegionDeleteTrigger
IF OBJECT_ID ('RegionUpdateTrigger') IS NOT NULL DROP TRIGGER RegionUpdateTrigger
IF OBJECT_ID ('SalesCallDeleteTrigger') IS NOT NULL DROP TRIGGER SalesCallDeleteTrigger
IF OBJECT_ID ('SalesCallUpdateTrigger') IS NOT NULL DROP TRIGGER SalesCallUpdateTrigger
IF OBJECT_ID ('SalesStatusChangeDeleteTrigger') IS NOT NULL DROP TRIGGER SalesStatusChangeDeleteTrigger
IF OBJECT_ID ('SalesStatusChangeUpdateTrigger') IS NOT NULL DROP TRIGGER SalesStatusChangeUpdateTrigger
IF OBJECT_ID ('ThemeDeleteTrigger') IS NOT NULL DROP TRIGGER ThemeDeleteTrigger
IF OBJECT_ID ('ThemeUpdateTrigger') IS NOT NULL DROP TRIGGER ThemeUpdateTrigger
IF OBJECT_ID ('ThreadDeleteTrigger') IS NOT NULL DROP TRIGGER ThreadDeleteTrigger
IF OBJECT_ID ('ThreadUpdateTrigger') IS NOT NULL DROP TRIGGER ThreadUpdateTrigger
IF OBJECT_ID ('TicketDeleteTrigger') IS NOT NULL DROP TRIGGER TicketDeleteTrigger
IF OBJECT_ID ('TicketUpdateTrigger') IS NOT NULL DROP TRIGGER TicketUpdateTrigger
IF OBJECT_ID ('TicketRunDeleteTrigger') IS NOT NULL DROP TRIGGER TicketRunDeleteTrigger
IF OBJECT_ID ('TicketRunUpdateTrigger') IS NOT NULL DROP TRIGGER TicketRunUpdateTrigger
IF OBJECT_ID ('TransferDeleteTrigger') IS NOT NULL DROP TRIGGER TransferDeleteTrigger
IF OBJECT_ID ('TransferUpdateTrigger') IS NOT NULL DROP TRIGGER TransferUpdateTrigger
IF OBJECT_ID ('UsrDeleteTrigger') IS NOT NULL DROP TRIGGER UsrDeleteTrigger
IF OBJECT_ID ('UsrUpdateTrigger') IS NOT NULL DROP TRIGGER UsrUpdateTrigger
IF OBJECT_ID ('VenueDeleteTrigger') IS NOT NULL DROP TRIGGER VenueDeleteTrigger
IF OBJECT_ID ('VenueUpdateTrigger') IS NOT NULL DROP TRIGGER VenueUpdateTrigger
IF OBJECT_ID ('VisitDeleteTrigger') IS NOT NULL DROP TRIGGER VisitDeleteTrigger
IF OBJECT_ID ('VisitUpdateTrigger') IS NOT NULL DROP TRIGGER VisitUpdateTrigger
DROP ASSEMBLY CacheTriggers

CREATE ASSEMBLY CacheTriggers
FROM 'C:\CacheTriggers\bin\CacheTriggers.dll'
WITH PERMISSION_SET = UNSAFE
GO


CREATE TRIGGER AbuseUpdateTrigger ON [Abuse] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.AbuseTrigger].AbuseUpdateDeleteTrigger
GO
CREATE TRIGGER AbuseDeleteTrigger ON [Abuse] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.AbuseTrigger].AbuseUpdateDeleteTrigger
GO
CREATE TRIGGER AdminUpdateTrigger ON [Admin] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.AdminTrigger].AdminUpdateDeleteTrigger
GO
CREATE TRIGGER AdminDeleteTrigger ON [Admin] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.AdminTrigger].AdminUpdateDeleteTrigger
GO
CREATE TRIGGER ArticleUpdateTrigger ON [Article] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ArticleTrigger].ArticleUpdateDeleteTrigger
GO
CREATE TRIGGER ArticleDeleteTrigger ON [Article] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ArticleTrigger].ArticleUpdateDeleteTrigger
GO
CREATE TRIGGER BankExportUpdateTrigger ON [BankExport] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.BankExportTrigger].BankExportUpdateDeleteTrigger
GO
CREATE TRIGGER BankExportDeleteTrigger ON [BankExport] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.BankExportTrigger].BankExportUpdateDeleteTrigger
GO
CREATE TRIGGER BannerUpdateTrigger ON [Banner] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.BannerTrigger].BannerUpdateDeleteTrigger
GO
CREATE TRIGGER BannerDeleteTrigger ON [Banner] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.BannerTrigger].BannerUpdateDeleteTrigger
GO
CREATE TRIGGER BrandUpdateTrigger ON [Brand] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.BrandTrigger].BrandUpdateDeleteTrigger
GO
CREATE TRIGGER BrandDeleteTrigger ON [Brand] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.BrandTrigger].BrandUpdateDeleteTrigger
GO
CREATE TRIGGER ChatUpdateTrigger ON [Chat] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ChatTrigger].ChatUpdateDeleteTrigger
GO
CREATE TRIGGER ChatDeleteTrigger ON [Chat] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ChatTrigger].ChatUpdateDeleteTrigger
GO
CREATE TRIGGER ChatMessageUpdateTrigger ON [ChatMessage] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ChatMessageTrigger].ChatMessageUpdateDeleteTrigger
GO
CREATE TRIGGER ChatMessageDeleteTrigger ON [ChatMessage] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ChatMessageTrigger].ChatMessageUpdateDeleteTrigger
GO
CREATE TRIGGER ClubDetailsUpdateTrigger ON [ClubDetails] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ClubDetailsTrigger].ClubDetailsUpdateDeleteTrigger
GO
CREATE TRIGGER ClubDetailsDeleteTrigger ON [ClubDetails] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ClubDetailsTrigger].ClubDetailsUpdateDeleteTrigger
GO
CREATE TRIGGER CommentUpdateTrigger ON [Comment] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.CommentTrigger].CommentUpdateDeleteTrigger
GO
CREATE TRIGGER CommentDeleteTrigger ON [Comment] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.CommentTrigger].CommentUpdateDeleteTrigger
GO
CREATE TRIGGER CompUpdateTrigger ON [Comp] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.CompTrigger].CompUpdateDeleteTrigger
GO
CREATE TRIGGER CompDeleteTrigger ON [Comp] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.CompTrigger].CompUpdateDeleteTrigger
GO
CREATE TRIGGER CountryUpdateTrigger ON [Country] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.CountryTrigger].CountryUpdateDeleteTrigger
GO
CREATE TRIGGER CountryDeleteTrigger ON [Country] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.CountryTrigger].CountryUpdateDeleteTrigger
GO
CREATE TRIGGER DemographicsUpdateTrigger ON [Demographics] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.DemographicsTrigger].DemographicsUpdateDeleteTrigger
GO
CREATE TRIGGER DemographicsDeleteTrigger ON [Demographics] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.DemographicsTrigger].DemographicsUpdateDeleteTrigger
GO
CREATE TRIGGER DomainUpdateTrigger ON [Domain] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.DomainTrigger].DomainUpdateDeleteTrigger
GO
CREATE TRIGGER DomainDeleteTrigger ON [Domain] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.DomainTrigger].DomainUpdateDeleteTrigger
GO
CREATE TRIGGER EventUpdateTrigger ON [Event] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.EventTrigger].EventUpdateDeleteTrigger
GO
CREATE TRIGGER EventDeleteTrigger ON [Event] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.EventTrigger].EventUpdateDeleteTrigger
GO
CREATE TRIGGER EventPerformerUpdateTrigger ON [EventPerformer] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.EventPerformerTrigger].EventPerformerUpdateDeleteTrigger
GO
CREATE TRIGGER EventPerformerDeleteTrigger ON [EventPerformer] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.EventPerformerTrigger].EventPerformerUpdateDeleteTrigger
GO
CREATE TRIGGER GalleryUpdateTrigger ON [Gallery] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GalleryTrigger].GalleryUpdateDeleteTrigger
GO
CREATE TRIGGER GalleryDeleteTrigger ON [Gallery] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GalleryTrigger].GalleryUpdateDeleteTrigger
GO
CREATE TRIGGER GlobalUpdateTrigger ON [Global] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GlobalTrigger].GlobalUpdateDeleteTrigger
GO
CREATE TRIGGER GlobalDeleteTrigger ON [Global] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GlobalTrigger].GlobalUpdateDeleteTrigger
GO
CREATE TRIGGER GroupUpdateTrigger ON [Group] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GroupTrigger].GroupUpdateDeleteTrigger
GO
CREATE TRIGGER GroupDeleteTrigger ON [Group] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GroupTrigger].GroupUpdateDeleteTrigger
GO
CREATE TRIGGER GuestlistCreditUpdateTrigger ON [GuestlistCredit] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GuestlistCreditTrigger].GuestlistCreditUpdateDeleteTrigger
GO
CREATE TRIGGER GuestlistCreditDeleteTrigger ON [GuestlistCredit] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.GuestlistCreditTrigger].GuestlistCreditUpdateDeleteTrigger
GO
CREATE TRIGGER HitUpdateTrigger ON [Hit] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.HitTrigger].HitUpdateDeleteTrigger
GO
CREATE TRIGGER HitDeleteTrigger ON [Hit] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.HitTrigger].HitUpdateDeleteTrigger
GO
CREATE TRIGGER IncomingSmsUpdateTrigger ON [IncomingSms] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.IncomingSmsTrigger].IncomingSmsUpdateDeleteTrigger
GO
CREATE TRIGGER IncomingSmsDeleteTrigger ON [IncomingSms] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.IncomingSmsTrigger].IncomingSmsUpdateDeleteTrigger
GO
CREATE TRIGGER InvoiceUpdateTrigger ON [Invoice] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.InvoiceTrigger].InvoiceUpdateDeleteTrigger
GO
CREATE TRIGGER InvoiceDeleteTrigger ON [Invoice] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.InvoiceTrigger].InvoiceUpdateDeleteTrigger
GO
CREATE TRIGGER InvoiceItemUpdateTrigger ON [InvoiceItem] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.InvoiceItemTrigger].InvoiceItemUpdateDeleteTrigger
GO
CREATE TRIGGER InvoiceItemDeleteTrigger ON [InvoiceItem] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.InvoiceItemTrigger].InvoiceItemUpdateDeleteTrigger
GO
CREATE TRIGGER InvoiceLinkUpdateTrigger ON [InvoiceLink] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.InvoiceLinkTrigger].InvoiceLinkUpdateDeleteTrigger
GO
CREATE TRIGGER InvoiceLinkDeleteTrigger ON [InvoiceLink] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.InvoiceLinkTrigger].InvoiceLinkUpdateDeleteTrigger
GO
CREATE TRIGGER LolUpdateTrigger ON [Lol] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.LolTrigger].LolUpdateDeleteTrigger
GO
CREATE TRIGGER LolDeleteTrigger ON [Lol] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.LolTrigger].LolUpdateDeleteTrigger
GO
CREATE TRIGGER MiscUpdateTrigger ON [Misc] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.MiscTrigger].MiscUpdateDeleteTrigger
GO
CREATE TRIGGER MiscDeleteTrigger ON [Misc] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.MiscTrigger].MiscUpdateDeleteTrigger
GO
CREATE TRIGGER MobileUpdateTrigger ON [Mobile] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.MobileTrigger].MobileUpdateDeleteTrigger
GO
CREATE TRIGGER MobileDeleteTrigger ON [Mobile] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.MobileTrigger].MobileUpdateDeleteTrigger
GO
CREATE TRIGGER MusicTypeUpdateTrigger ON [MusicType] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.MusicTypeTrigger].MusicTypeUpdateDeleteTrigger
GO
CREATE TRIGGER MusicTypeDeleteTrigger ON [MusicType] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.MusicTypeTrigger].MusicTypeUpdateDeleteTrigger
GO
CREATE TRIGGER OutgoingSmsUpdateTrigger ON [OutgoingSms] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.OutgoingSmsTrigger].OutgoingSmsUpdateDeleteTrigger
GO
CREATE TRIGGER OutgoingSmsDeleteTrigger ON [OutgoingSms] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.OutgoingSmsTrigger].OutgoingSmsUpdateDeleteTrigger
GO
CREATE TRIGGER PageTimeUpdateTrigger ON [PageTime] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PageTimeTrigger].PageTimeUpdateDeleteTrigger
GO
CREATE TRIGGER PageTimeDeleteTrigger ON [PageTime] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PageTimeTrigger].PageTimeUpdateDeleteTrigger
GO
CREATE TRIGGER ParaUpdateTrigger ON [Para] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ParaTrigger].ParaUpdateDeleteTrigger
GO
CREATE TRIGGER ParaDeleteTrigger ON [Para] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ParaTrigger].ParaUpdateDeleteTrigger
GO
CREATE TRIGGER PerformanceReviewUpdateTrigger ON [PerformanceReview] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PerformanceReviewTrigger].PerformanceReviewUpdateDeleteTrigger
GO
CREATE TRIGGER PerformanceReviewDeleteTrigger ON [PerformanceReview] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PerformanceReviewTrigger].PerformanceReviewUpdateDeleteTrigger
GO
CREATE TRIGGER PerformerUpdateTrigger ON [Performer] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PerformerTrigger].PerformerUpdateDeleteTrigger
GO
CREATE TRIGGER PerformerDeleteTrigger ON [Performer] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PerformerTrigger].PerformerUpdateDeleteTrigger
GO
CREATE TRIGGER PhoneUpdateTrigger ON [Phone] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PhoneTrigger].PhoneUpdateDeleteTrigger
GO
CREATE TRIGGER PhoneDeleteTrigger ON [Phone] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PhoneTrigger].PhoneUpdateDeleteTrigger
GO
CREATE TRIGGER PhotoUpdateTrigger ON [Photo] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PhotoTrigger].PhotoUpdateDeleteTrigger
GO
CREATE TRIGGER PhotoDeleteTrigger ON [Photo] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PhotoTrigger].PhotoUpdateDeleteTrigger
GO
CREATE TRIGGER PhotoReviewUpdateTrigger ON [PhotoReview] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PhotoReviewTrigger].PhotoReviewUpdateDeleteTrigger
GO
CREATE TRIGGER PhotoReviewDeleteTrigger ON [PhotoReview] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PhotoReviewTrigger].PhotoReviewUpdateDeleteTrigger
GO
CREATE TRIGGER PlaceUpdateTrigger ON [Place] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PlaceTrigger].PlaceUpdateDeleteTrigger
GO
CREATE TRIGGER PlaceDeleteTrigger ON [Place] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PlaceTrigger].PlaceUpdateDeleteTrigger
GO
CREATE TRIGGER PromoterUpdateTrigger ON [Promoter] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PromoterTrigger].PromoterUpdateDeleteTrigger
GO
CREATE TRIGGER PromoterDeleteTrigger ON [Promoter] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.PromoterTrigger].PromoterUpdateDeleteTrigger
GO
CREATE TRIGGER RegionUpdateTrigger ON [Region] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.RegionTrigger].RegionUpdateDeleteTrigger
GO
CREATE TRIGGER RegionDeleteTrigger ON [Region] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.RegionTrigger].RegionUpdateDeleteTrigger
GO
CREATE TRIGGER SalesCallUpdateTrigger ON [SalesCall] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.SalesCallTrigger].SalesCallUpdateDeleteTrigger
GO
CREATE TRIGGER SalesCallDeleteTrigger ON [SalesCall] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.SalesCallTrigger].SalesCallUpdateDeleteTrigger
GO
CREATE TRIGGER SalesStatusChangeUpdateTrigger ON [SalesStatusChange] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.SalesStatusChangeTrigger].SalesStatusChangeUpdateDeleteTrigger
GO
CREATE TRIGGER SalesStatusChangeDeleteTrigger ON [SalesStatusChange] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.SalesStatusChangeTrigger].SalesStatusChangeUpdateDeleteTrigger
GO
CREATE TRIGGER ThemeUpdateTrigger ON [Theme] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ThemeTrigger].ThemeUpdateDeleteTrigger
GO
CREATE TRIGGER ThemeDeleteTrigger ON [Theme] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ThemeTrigger].ThemeUpdateDeleteTrigger
GO
CREATE TRIGGER ThreadUpdateTrigger ON [Thread] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ThreadTrigger].ThreadUpdateDeleteTrigger
GO
CREATE TRIGGER ThreadDeleteTrigger ON [Thread] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.ThreadTrigger].ThreadUpdateDeleteTrigger
GO
CREATE TRIGGER TicketUpdateTrigger ON [Ticket] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.TicketTrigger].TicketUpdateDeleteTrigger
GO
CREATE TRIGGER TicketDeleteTrigger ON [Ticket] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.TicketTrigger].TicketUpdateDeleteTrigger
GO
CREATE TRIGGER TicketRunUpdateTrigger ON [TicketRun] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.TicketRunTrigger].TicketRunUpdateDeleteTrigger
GO
CREATE TRIGGER TicketRunDeleteTrigger ON [TicketRun] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.TicketRunTrigger].TicketRunUpdateDeleteTrigger
GO
CREATE TRIGGER TransferUpdateTrigger ON [Transfer] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.TransferTrigger].TransferUpdateDeleteTrigger
GO
CREATE TRIGGER TransferDeleteTrigger ON [Transfer] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.TransferTrigger].TransferUpdateDeleteTrigger
GO
CREATE TRIGGER UsrUpdateTrigger ON [Usr] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.UsrTrigger].UsrUpdateDeleteTrigger
GO
CREATE TRIGGER UsrDeleteTrigger ON [Usr] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.UsrTrigger].UsrUpdateDeleteTrigger
GO
CREATE TRIGGER VenueUpdateTrigger ON [Venue] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.VenueTrigger].VenueUpdateDeleteTrigger
GO
CREATE TRIGGER VenueDeleteTrigger ON [Venue] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.VenueTrigger].VenueUpdateDeleteTrigger
GO
CREATE TRIGGER VisitUpdateTrigger ON [Visit] FOR UPDATE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.VisitTrigger].VisitUpdateDeleteTrigger
GO
CREATE TRIGGER VisitDeleteTrigger ON [Visit] FOR DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.VisitTrigger].VisitUpdateDeleteTrigger
GO

--Run this to update the assembly

ALTER ASSEMBLY CacheTriggers
FROM 'C:\CacheTriggers\bin\CacheTriggers.dll'
WITH PERMISSION_SET = UNSAFE
GO


--Run this to test the assembly

UPDATE Usr SET LoginCount=1 WHERE K=1
SET NOCOUNT ON; INSERT INTO Usr (Email, Password) VALUES ('me@my.com', 'pwd'); DELETE FROM Usr WHERE K=(SELECT @@IDENTITY);

*/
