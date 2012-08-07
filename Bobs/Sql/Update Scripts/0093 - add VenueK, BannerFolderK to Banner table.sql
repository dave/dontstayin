IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'VenueK'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	VenueK int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The venue to link to', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'VenueK'

END

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'AutomaticDates'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	AutomaticDates bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Are automatic dates selected in the banner wizard?', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AutomaticDates'

END






IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'AutomaticDatesWeeks'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	AutomaticDatesWeeks int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The number of weeks selected in the automatic dates section of the banner wizard', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AutomaticDatesWeeks'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'AutomaticTargetting'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	AutomaticTargetting bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is automatic targetting selected in the banner wizard?', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AutomaticTargetting'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'AutomaticExposure'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	AutomaticExposure bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is one of the automatic exposure levels selected in the banner wizard?', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AutomaticExposure'

END

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'AutomaticExposureLevel'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	AutomaticExposureLevel int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The automatic exposure level that is selected in the banner wizard', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AutomaticExposureLevel'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'AutomaticExposureLevel'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	AutomaticExposureLevel int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The automatic exposure level that is selected in the banner wizard', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AutomaticExposureLevel'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'StatusEnabled'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	StatusEnabled bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Usually true, only false if the banner has been paused or cancelled (cancelled when IsRefunded = true)', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'StatusEnabled'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'StatusBooked'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	StatusBooked bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'false if the banner is new, true if it has been paid for', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'StatusBooked'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'StatusArtwork'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	StatusArtwork bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'true if the artwork is ready, false if not', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'StatusArtwork'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'Refunded'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	Refunded bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'true if campaign credits have been successfully refunded to the promoter account', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'Refunded'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'RefundedCredits'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	RefundedCredits int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'How many credits were refunded?', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'RefundedCredits'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'RefundCampaignCreditK'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	RefundCampaignCreditK int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Link to the CampaignCredit table for the refund', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'RefundCampaignCreditK'

END
GO
update banner set StatusEnabled=0, StatusBooked=0, StatusArtwork=0


update banner set StatusEnabled=1 where Status=1 OR Status=2 OR Status=4
update banner set StatusBooked=1 where Status=2 OR Status=4
update banner set StatusArtwork=1 where Status=2

GO

IF EXISTS (SELECT * FROM sys.views WHERE name = '_hypmv_267') BEGIN
	DROP VIEW _hypmv_267
END
GO
