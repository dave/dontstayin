/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Mobile' 
	AND	[column].name = 'ListingsRequests'
) BEGIN


ALTER TABLE dbo.Mobile
	DROP COLUMN ListingsRequests, ListingsResponseFound, ListingsResponseNone, EventDetailRequests, VenueDetailRequests, TotalError, TotalFollowOn, SentIntro, SentBusinessCard, IsPllay, IsTonight, ServiceType, SendUpdates, GuestClientK

END
