
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertionOrder]') AND type in (N'U'))
DROP TABLE [dbo].[InsertionOrder]

GO
CREATE TABLE [dbo].[InsertionOrder](
	[K] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[CampaignCredits] [int] NULL,
	[NextInvoiceDue] [datetime] NULL,
	[PromoterK] [int] NOT NULL,
	[UsrK] [int] NOT NULL,
	[UsrNameOverride] [varchar](250) NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[ClientRef] [varchar](250) NULL,
	[CampaignStartDate] [datetime] NULL,
	[CampaignEndDate] [datetime] NULL,
	[TrafficUsrK] [int] NULL,
	[Notes] [varchar](max) NULL,
	[ActionUsrK] [int] NULL,
	[PaymentTerms] [varchar](250) NULL,
	[InvoicePeriod] [varchar](250) NULL,
	[CampaignName] [varchar](50) NULL,
	[AgencyDiscount] [decimal](18, 2) NULL,
	[DuplicateGuid] UNIQUEIDENTIFIER,
	[CampaignCreditsOverriden] BIT NULL
 CONSTRAINT [PK_InsertionOrder] PRIMARY KEY CLUSTERED 
(
	[K] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'auto incrementing primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'K'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Status - Proforma = 1, Enabled = 2, Disabled = 3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(in corporate IOs, we calculate this from the banner types and impressions)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'CampaignCredits'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'this is a reminder that can be set. When the next invoice is due, this IO will pop up in the admin, and an invoice can be manually raised.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'NextInvoiceDue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'the K of the promoter to whom the insertion order applies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'PromoterK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'the K of the usr for the promoter to whom the insertion order applies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'UsrK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'the name the insertion order report is pertinent to if the usrK is set to -1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'UsrNameOverride'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When the insertion order was created' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'DateTimeCreated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The clients reference code for the insertion order' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'ClientRef'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The start date of the campaign' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'CampaignStartDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The end date of the campaign' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'CampaignEndDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'the K of the user to be used as a traffic contact' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'TrafficUsrK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Misc notes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'Notes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The K of the user who made the changes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'ActionUsrK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PaymentTerms e.g. "30 days from date of invoice"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'PaymentTerms'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'InvoicePeriod  e.g. "Monthly" or "Campaign"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'InvoicePeriod'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the campaign' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'CampaignName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Agency discount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'AgencyDiscount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Track campaign credits and outstanding corporate IOs and Insertion Order Credits "IOCs"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'used to prevent the InsertionOrder from being raised twice' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'DuplicateGuid'

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'true if the campaign credit value was overridden' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrder', @level2type=N'COLUMN',@level2name=N'CampaignCreditsOverriden'



GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertionOrderItem]') AND type in (N'U'))
DROP TABLE [dbo].[InsertionOrderItem]
GO

CREATE TABLE [dbo].[InsertionOrderItem](
	[K] [int] IDENTITY(1,1) NOT NULL,
	[InsertionOrderK] [int] NULL,
	[Description] [varchar](150) NULL,
	[BannerPosition] [int] NULL,
	[ImpressionQuantity] [int] NULL,
	[PriceBeforeDiscount] [decimal](18, 2) NULL,	
	[Discount] [decimal](18, 4) NULL,
	[PriceBeforeAgencyDiscount] [decimal](18, 2) NULL,
	[AgencyDiscount] [decimal](18, 4) NULL,
	[Price] [decimal](18, 2) NULL,
	[Cpm] [decimal](18, 2) NULL,
 CONSTRAINT [PK_InsertionOrderItem] PRIMARY KEY CLUSTERED 
(
	[K] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'auto incrementing primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'K'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Insertion Order K where one exists' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'InsertionOrderK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BannerPosition' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'BannerPosition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ImpressionQuantity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'ImpressionQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'Discount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'GrossCost = (Cpm * Impressions / 1000)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'PriceBeforeDiscount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DiscountedCost = (GrossCost * (1 - discount))' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'PriceBeforeAgencyDiscount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'agency discount - copied from InsertionOrder' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'AgencyDiscount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NetCost = (DiscountedCost * (1-agency discount)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cpm - cost per mille' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem', @level2type=N'COLUMN',@level2name=N'Cpm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Corporate IOs are split up into items' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InsertionOrderItem'

GO

IF NOT EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.name = 'Invoice' AND c.Name = 'AgencyDiscount') BEGIN
	ALTER TABLE dbo.Invoice ADD
		PriceBeforeDiscount float NULL,
		Discount float NULL,
		PriceBeforeAgencyDiscount float NULL,
		AgencyDiscount float NULL

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Price before any discounts have been applied' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Invoice', @level2type=N'COLUMN',@level2name=N'PriceBeforeDiscount'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'average of item level Discount - percentage - stored between 0 and 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Invoice', @level2type=N'COLUMN',@level2name=N'Discount'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'sum of Invoice Item PriceBeforeAgencyDiscount but after item discount has been applied' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Invoice', @level2type=N'COLUMN',@level2name=N'PriceBeforeAgencyDiscount'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'average of item level agency discount - percentage - stored between 0 and 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Invoice', @level2type=N'COLUMN',@level2name=N'AgencyDiscount'


	ALTER TABLE dbo.InvoiceItem ADD
		PriceBeforeAgencyDiscount float NULL,
		AgencyDiscount float NULL

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Price before agency discount but after item discount has been applied' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InvoiceItem', @level2type=N'COLUMN',@level2name=N'PriceBeforeAgencyDiscount'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AgencyDiscount - percentage - stored between 0 and 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InvoiceItem', @level2type=N'COLUMN',@level2name=N'AgencyDiscount'
	
	
	
	
END
GO
	UPDATE Invoice SET PriceBeforeAgencyDiscount = Price
	UPDATE Invoice SET Discount = 0
	UPDATE Invoice SET PriceBeforeDiscount = Price
	UPDATE Invoice SET AgencyDiscount = 0
	UPDATE InvoiceItem SET PriceBeforeAgencyDiscount = Price
	UPDATE InvoiceItem SET Discount = 0
	UPDATE InvoiceItem SET PriceBeforeDiscount = Price
	UPDATE InvoiceItem SET AgencyDiscount = 0
GO
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Invoice' 
	AND	[column].name = 'InsertionOrderK'
) BEGIN

ALTER TABLE Invoice add InsertionOrderK int
EXECUTE sp_addextendedproperty N'MS_Description', N'Used when the item is a campaign credit top-up', N'SCHEMA', N'dbo', N'TABLE', N'Invoice', N'COLUMN', N'InsertionOrderK'

END
