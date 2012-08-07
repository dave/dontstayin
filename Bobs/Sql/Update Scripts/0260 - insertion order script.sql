/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_InsertionOrder
	(
	K int NOT NULL IDENTITY (1, 1),
	Status int NOT NULL,
	CampaignCredits int NULL,
	NextInvoiceDue datetime NULL,
	PromoterK int NOT NULL,
	UsrK int NOT NULL,
	UsrNameOverride varchar(250) NULL,
	DateTimeCreated datetime NOT NULL,
	ClientRef varchar(250) NULL,
	CampaignStartDate datetime NULL,
	CampaignEndDate datetime NULL,
	TrafficUsrK int NULL,
	Notes varchar(MAX) NULL,
	ActionUsrK int NULL,
	PaymentTerms varchar(250) NULL,
	InvoicePeriod varchar(250) NULL,
	CampaignName varchar(50) NULL,
	AgencyDiscount float(53) NULL,
	DuplicateGuid uniqueidentifier NULL,
	CampaignCreditsOverriden bit NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Track campaign credits and outstanding corporate IOs and Insertion Order Credits "IOCs"'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'auto incrementing primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'Status'
SET @v = N'Status - Proforma = 1, Enabled = 2, Disabled = 3'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'Status'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignCredits'
SET @v = N'(in corporate IOs, we calculate this from the banner types and impressions)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignCredits'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'NextInvoiceDue'
SET @v = N'this is a reminder that can be set. When the next invoice is due, this IO will pop up in the admin, and an invoice can be manually raised.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'NextInvoiceDue'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'PromoterK'
SET @v = N'the K of the promoter to whom the insertion order applies'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'PromoterK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'UsrK'
SET @v = N'the K of the usr for the promoter to whom the insertion order applies'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'UsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'UsrNameOverride'
SET @v = N'the name the insertion order report is pertinent to if the usrK is set to -1'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'UsrNameOverride'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'DateTimeCreated'
SET @v = N'When the insertion order was created'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'DateTimeCreated'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'ClientRef'
SET @v = N'The clients reference code for the insertion order'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'ClientRef'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignStartDate'
SET @v = N'The start date of the campaign'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignStartDate'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignEndDate'
SET @v = N'The end date of the campaign'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignEndDate'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'TrafficUsrK'
SET @v = N'the K of the user to be used as a traffic contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'TrafficUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'Notes'
SET @v = N'Misc notes'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'Notes'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'ActionUsrK'
SET @v = N'The K of the user who made the changes'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'ActionUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'PaymentTerms'
SET @v = N'PaymentTerms e.g. "30 days from date of invoice"'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'PaymentTerms'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'InvoicePeriod'
SET @v = N'InvoicePeriod  e.g. "Monthly" or "Campaign"'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'InvoicePeriod'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignName'
SET @v = N'Name of the campaign'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'AgencyDiscount'
SET @v = N'Agency discount'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'AgencyDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'DuplicateGuid'
SET @v = N'used to prevent the InsertionOrder from being raised twice'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'DuplicateGuid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignCreditsOverriden'
SET @v = N'true if the campaign credit value was overridden'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrder', N'COLUMN', N'CampaignCreditsOverriden'
GO
SET IDENTITY_INSERT dbo.Tmp_InsertionOrder ON
GO
IF EXISTS(SELECT * FROM dbo.InsertionOrder)
	 EXEC('INSERT INTO dbo.Tmp_InsertionOrder (K, Status, CampaignCredits, NextInvoiceDue, PromoterK, UsrK, UsrNameOverride, DateTimeCreated, ClientRef, CampaignStartDate, CampaignEndDate, TrafficUsrK, Notes, ActionUsrK, PaymentTerms, InvoicePeriod, CampaignName, AgencyDiscount, DuplicateGuid, CampaignCreditsOverriden)
		SELECT K, Status, CampaignCredits, NextInvoiceDue, PromoterK, UsrK, UsrNameOverride, DateTimeCreated, ClientRef, CampaignStartDate, CampaignEndDate, TrafficUsrK, Notes, ActionUsrK, PaymentTerms, InvoicePeriod, CampaignName, CONVERT(float(53), AgencyDiscount), DuplicateGuid, CampaignCreditsOverriden FROM dbo.InsertionOrder WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_InsertionOrder OFF
GO
DROP TABLE dbo.InsertionOrder
GO
EXECUTE sp_rename N'dbo.Tmp_InsertionOrder', N'InsertionOrder', 'OBJECT' 
GO
ALTER TABLE dbo.InsertionOrder ADD CONSTRAINT
	PK_InsertionOrder PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.InsertionOrderTrigger ON dbo.InsertionOrder 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].InsertionOrderTrigger')
end
GO
COMMIT
