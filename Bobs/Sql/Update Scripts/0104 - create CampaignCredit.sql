



IF EXISTS(
	SELECT * FROM sys.tables [table] 
	WHERE [table].name = 'CampaignCredit' 
) BEGIN
DROP TABLE CampaignCredit
END 

CREATE TABLE dbo.CampaignCredit
	(
	K int not null identity(1,1),
	PromoterK int NOT NULL,
	ActionDateTime DateTime,
	BuyableObjectType int,
	BuyableObjectK int,
	BuyableLockDateTime DateTime,
	InvoiceItemType int,
	Description varchar(250),
	Credits int,
	Enabled bit,
	BalanceToDate int,
	DisplayOrder int
	)  ON [PRIMARY]

ALTER TABLE dbo.CampaignCredit ADD CONSTRAINT
	PK_CampaignCredit PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.CampaignCredit ADD CONSTRAINT
	DF_CampaignCredit_DisplayOrder DEFAULT 0 FOR DisplayOrder
 
DECLARE @v sql_variant 
SET @v = N'Used to track how many campaign credits each promoter has bought / spent'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', NULL, NULL
SET @v = N'auto incrementing primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'K'
SET @v = N'the promoter'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'PromoterK'
SET @v = N'DateTime at which the Credit addition or subtraction occurred'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'ActionDateTime'
SET @v = N'the type of the linked object - InsertionOrder = 19, Banner = 16, EmailSpotlight = 20, Event = 2'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'BuyableObjectType'
SET @v = N'the key of the linked object'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'BuyableObjectK'
SET @v = N'DateTime til when this buyable object is locked'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'BuyableLockDateTime'
SET @v = N'the invoice item type'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'InvoiceItemType'
SET @v = N'if not linked to an object, this is the description'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'Description'
SET @v = N'the credits for this item (+ve for credits being bought, -ve for credits being spent)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'Credits'
SET @v = N'used when buying CampaignCredits - otherwise always true'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'Enabled'
SET @v = N'running total of the promoters balance to date, including this CampaignCredit'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'BalanceToDate'
SET @v = N'Display ascending ordering to order records processed at the same action datetime'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'CampaignCredit', N'COLUMN', N'DisplayOrder'
GO





