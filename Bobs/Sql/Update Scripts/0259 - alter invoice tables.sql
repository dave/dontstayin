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
CREATE TABLE dbo.Tmp_InvoiceItem
	(
	K int NOT NULL IDENTITY (1, 1),
	InvoiceK int NOT NULL,
	Type int NOT NULL,
	KeyData int NULL,
	CustomData varchar(255) NULL,
	ItemProcessed bit NOT NULL,
	Description varchar(255) NOT NULL,
	Price decimal(18, 2) NOT NULL,
	Vat decimal(18, 2) NOT NULL,
	Total decimal(18, 2) NOT NULL,
	RevenueStartDate datetime NULL,
	RevenueEndDate datetime NULL,
	VatCode int NOT NULL,
	BuyableObjectType int NULL,
	BuyableObjectK int NULL,
	PriceBeforeDiscount decimal(18, 2) NOT NULL,
	Discount float(53) NOT NULL,
	PriceBeforeAgencyDiscount decimal(18, 2) NOT NULL,
	AgencyDiscount float(53) NOT NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Invoice / credit note line'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'The primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'InvoiceK'
SET @v = N'The invoice'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'InvoiceK'
GO
DECLARE @v sql_variant 
SET @v = N'Types'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Type'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Type'
SET @v = N'The type of the invoiceitem (Misc=0, Banner=1, etc.)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Type'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'KeyData'
SET @v = N'Key of the item (e.g. BannerK)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'KeyData'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'CustomData'
SET @v = N'Additional data needed to enable the item on the site'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'CustomData'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'ItemProcessed'
SET @v = N'Has the code been processed?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'ItemProcessed'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Description'
SET @v = N'Description of the item (for when there isn''t a code)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Description'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Price'
SET @v = N'Price excluding VAT (+ve for invoices, -ve for credits)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Price'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Vat'
SET @v = N'Vat (+ve for invoices, -ve for credits)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Vat'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Total'
SET @v = N'Price including VAT (+ve for invoices, -ve for credits)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Total'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'RevenueStartDate'
SET @v = N'The revenue start date of the invoice item'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'RevenueStartDate'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'RevenueEndDate'
SET @v = N'The revenue end date of the invoice item'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'RevenueEndDate'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'VatCode'
SET @v = N'T0, T1, T9'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'VatCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'BuyableObjectType'
SET @v = N'The IBuyable Bob type of that the invoiceitem points to (Banner=1, etc.)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'BuyableObjectType'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'BuyableObjectK'
SET @v = N'The IBuyable ObjectType reference Key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'BuyableObjectK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'PriceBeforeDiscount'
SET @v = N'Price before applying discount'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'PriceBeforeDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Discount'
SET @v = N'Discount to apply to this item, between 0.0 and 1.0'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'Discount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'PriceBeforeAgencyDiscount'
SET @v = N'Price before agency discount but after item discount has been applied'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'PriceBeforeAgencyDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'AgencyDiscount'
SET @v = N'AgencyDiscount - percentage - stored between 0 and 1'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItem', N'COLUMN', N'AgencyDiscount'
GO
SET IDENTITY_INSERT dbo.Tmp_InvoiceItem ON
GO
IF EXISTS(SELECT * FROM dbo.InvoiceItem)
	 EXEC('INSERT INTO dbo.Tmp_InvoiceItem (K, InvoiceK, Type, KeyData, CustomData, ItemProcessed, Description, Price, Vat, Total, RevenueStartDate, RevenueEndDate, VatCode, BuyableObjectType, BuyableObjectK, PriceBeforeDiscount, Discount, PriceBeforeAgencyDiscount, AgencyDiscount)
		SELECT K, InvoiceK, Type, KeyData, CustomData, ItemProcessed, Description, CONVERT(decimal(18, 2), Price), CONVERT(decimal(18, 2), Vat), CONVERT(decimal(18, 2), Total), RevenueStartDate, RevenueEndDate, VatCode, BuyableObjectType, BuyableObjectK, CONVERT(decimal(18, 2), PriceBeforeDiscount), Discount, CONVERT(decimal(18, 2), PriceBeforeAgencyDiscount), AgencyDiscount FROM dbo.InvoiceItem WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_InvoiceItem OFF
GO
DROP TABLE dbo.InvoiceItem
GO
EXECUTE sp_rename N'dbo.Tmp_InvoiceItem', N'InvoiceItem', 'OBJECT' 
GO
ALTER TABLE dbo.InvoiceItem ADD CONSTRAINT
	PK_InvoiceItem PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.InvoiceItemTrigger ON dbo.InvoiceItem 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].InvoiceItemTrigger')
end
GO
COMMIT
GO
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
CREATE TABLE dbo.Tmp_Invoice
	(
	K int NOT NULL IDENTITY (1, 1),
	Type int NOT NULL,
	UsrK int NULL,
	PromoterK int NULL,
	ActionUsrK int NULL,
	Name varchar(150) NULL,
	Address varchar(150) NULL,
	Postcode varchar(150) NULL,
	PaymentType int NOT NULL,
	Paid bit NOT NULL,
	CreatedDateTime datetime NOT NULL,
	DueDateTime datetime NOT NULL,
	PaidDateTime datetime NULL,
	Price decimal(18, 2) NOT NULL,
	Vat decimal(18, 2) NOT NULL,
	Total decimal(18, 2) NOT NULL,
	DuplicateGuid uniqueidentifier NULL,
	Notes text NULL,
	VatCode int NOT NULL,
	SalesUsrK int NULL,
	SalesUsrAmount float(53) NULL,
	IsImmediateCreditCardPayment bit NOT NULL,
	TaxDateTime datetime NOT NULL,
	PurchaseOrderNumber varchar(50) NULL,
	BuyerType int NOT NULL,
	PriceBeforeDiscount decimal(18, 2) NOT NULL,
	Discount float(53) NOT NULL,
	PriceBeforeAgencyDiscount decimal(18, 2) NOT NULL,
	AgencyDiscount float(53) NOT NULL,
	InsertionOrderK int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Invoice or credit note'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'The primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'Types'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Type'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Type'
SET @v = N'Invoice, Credit'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Type'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'UsrK'
SET @v = N'The user that created this invoice'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'UsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PromoterK'
SET @v = N'The If this is a promoter invoice - this is the promoter'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PromoterK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'ActionUsrK'
SET @v = N'Link to the user that initiated this transfer (e.g. the admin user if it''s done manually!)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'ActionUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Name'
SET @v = N'TO BE REMOVED - Name from credit card payment control'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Name'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Address'
SET @v = N'TO BE REMOVED - First line of the address from credit card payment control'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Address'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Postcode'
SET @v = N'TO BE REMOVED - Postcode from credit card payment control'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Postcode'
GO
DECLARE @v sql_variant 
SET @v = N'PaymentTypes'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PaymentType'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PaymentType'
SET @v = N'TO BE REMOVED - Payment type - 1=CreditCard, 2=Invoiced'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PaymentType'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Paid'
SET @v = N'Has this invoice been fully paid?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Paid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'CreatedDateTime'
SET @v = N'When was the invoice created - the tax point'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'CreatedDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'DueDateTime'
SET @v = N'When is the invoice due to be paid (4 weeks). After this we can charge interest.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'DueDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PaidDateTime'
SET @v = N'When the invoice was fully paid'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PaidDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Price'
SET @v = N'Price excluding VAT (+ve for invoices, -ve for credits)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Price'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Vat'
SET @v = N'Vat (+ve for invoices, -ve for credits)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Vat'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Total'
SET @v = N'Price including VAT (+ve for invoices, -ve for credits)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Total'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'DuplicateGuid'
SET @v = N'Guid to catch duplicate "pay now" clicks'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'DuplicateGuid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Notes'
SET @v = N'Additional Notes'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Notes'
GO
DECLARE @v sql_variant 
SET @v = N'VATCodes'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'VatCode'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'VatCode'
SET @v = N'T0, T1, T4, T9'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'VatCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'SalesUsrK'
SET @v = N'Who is the account manager for this invoice?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'SalesUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'SalesUsrAmount'
SET @v = N'How much is contributed to the account managers target?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'SalesUsrAmount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'IsImmediateCreditCardPayment'
SET @v = N'Flag for immediate credit card payments. This flag to be used for exports to Sage'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'IsImmediateCreditCardPayment'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'TaxDateTime'
SET @v = N'Tax date - to be used for exporting to Sage'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'TaxDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PurchaseOrderNumber'
SET @v = N'Invoice purchase order number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PurchaseOrderNumber'
GO
DECLARE @v sql_variant 
SET @v = N'BuyerTypes'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'BuyerType'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'BuyerType'
SET @v = N'Type of the buyer: AgencyPromoter = 1, NonAgencyPromoter = 2, TicketUsr = 3, NonTicketUsr = 4'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'BuyerType'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PriceBeforeDiscount'
SET @v = N'Price before any discounts have been applied'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PriceBeforeDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Discount'
SET @v = N'average of item level Discount - percentage - stored between 0 and 1'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'Discount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PriceBeforeAgencyDiscount'
SET @v = N'sum of Invoice Item PriceBeforeAgencyDiscount but after item discount has been applied'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'PriceBeforeAgencyDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'AgencyDiscount'
SET @v = N'average of item level agency discount - percentage - stored between 0 and 1'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'AgencyDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'InsertionOrderK'
SET @v = N'Used when the item is a campaign credit top-up'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Invoice', N'COLUMN', N'InsertionOrderK'
GO
SET IDENTITY_INSERT dbo.Tmp_Invoice ON
GO
IF EXISTS(SELECT * FROM dbo.Invoice)
	 EXEC('INSERT INTO dbo.Tmp_Invoice (K, Type, UsrK, PromoterK, ActionUsrK, Name, Address, Postcode, PaymentType, Paid, CreatedDateTime, DueDateTime, PaidDateTime, Price, Vat, Total, DuplicateGuid, Notes, VatCode, SalesUsrK, SalesUsrAmount, IsImmediateCreditCardPayment, TaxDateTime, PurchaseOrderNumber, BuyerType, PriceBeforeDiscount, Discount, PriceBeforeAgencyDiscount, AgencyDiscount, InsertionOrderK)
		SELECT K, Type, UsrK, PromoterK, ActionUsrK, Name, Address, Postcode, PaymentType, Paid, CreatedDateTime, DueDateTime, PaidDateTime, CONVERT(decimal(18, 2), Price), CONVERT(decimal(18, 2), Vat), CONVERT(decimal(18, 2), Total), DuplicateGuid, Notes, VatCode, SalesUsrK, SalesUsrAmount, IsImmediateCreditCardPayment, TaxDateTime, PurchaseOrderNumber, BuyerType, CONVERT(decimal(18, 2), PriceBeforeDiscount), Discount, CONVERT(decimal(18, 2), PriceBeforeAgencyDiscount), AgencyDiscount, InsertionOrderK FROM dbo.Invoice WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Invoice OFF
GO
DROP TABLE dbo.Invoice
GO
EXECUTE sp_rename N'dbo.Tmp_Invoice', N'Invoice', 'OBJECT' 
GO
ALTER TABLE dbo.Invoice ADD CONSTRAINT
	PK_Invoice PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.InvoiceTrigger ON dbo.Invoice 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].InvoiceTrigger')
end
GO
COMMIT
GO
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
CREATE TABLE dbo.Tmp_InvoiceCredit
	(
	InvoiceK int NOT NULL,
	CreditInvoiceK int NOT NULL,
	Amount decimal(18, 2) NOT NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Invoice to Credit relational table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceCredit', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceCredit', N'COLUMN', N'InvoiceK'
SET @v = N'Link to the Invoice table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceCredit', N'COLUMN', N'InvoiceK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceCredit', N'COLUMN', N'CreditInvoiceK'
SET @v = N'Link to the Invoice table, referring to a credit'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceCredit', N'COLUMN', N'CreditInvoiceK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceCredit', N'COLUMN', N'Amount'
SET @v = N'+ve for DSI receiving money, -ve for DSI paying out money'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceCredit', N'COLUMN', N'Amount'
GO
IF EXISTS(SELECT * FROM dbo.InvoiceCredit)
	 EXEC('INSERT INTO dbo.Tmp_InvoiceCredit (InvoiceK, CreditInvoiceK, Amount)
		SELECT InvoiceK, CreditInvoiceK, CONVERT(decimal(18, 2), Amount) FROM dbo.InvoiceCredit WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.InvoiceCredit
GO
EXECUTE sp_rename N'dbo.Tmp_InvoiceCredit', N'InvoiceCredit', 'OBJECT' 
GO
ALTER TABLE dbo.InvoiceCredit ADD CONSTRAINT
	PK_InvoiceCredit PRIMARY KEY CLUSTERED 
	(
	InvoiceK,
	CreditInvoiceK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
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
CREATE TABLE dbo.Tmp_InsertionOrderItem
	(
	K int NOT NULL IDENTITY (1, 1),
	InsertionOrderK int NULL,
	Description varchar(150) NOT NULL,
	BannerPosition int NOT NULL,
	ImpressionQuantity int NOT NULL,
	PriceBeforeDiscount decimal(18, 2) NOT NULL,
	Discount float(53) NOT NULL,
	PriceBeforeAgencyDiscount decimal(18, 2) NOT NULL,
	AgencyDiscount float(53) NOT NULL,
	Price decimal(18, 2) NOT NULL,
	Cpm decimal(18, 2) NOT NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Corporate IOs are split up into items'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'auto incrementing primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'InsertionOrderK'
SET @v = N'Insertion Order K where one exists'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'InsertionOrderK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Description'
SET @v = N'Description'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Description'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'BannerPosition'
SET @v = N'BannerPosition'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'BannerPosition'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'ImpressionQuantity'
SET @v = N'ImpressionQuantity'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'ImpressionQuantity'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'PriceBeforeDiscount'
SET @v = N'GrossCost = (Cpm * Impressions / 1000)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'PriceBeforeDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Discount'
SET @v = N'Discount'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Discount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'PriceBeforeAgencyDiscount'
SET @v = N'DiscountedCost = (GrossCost * (1 - discount))'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'PriceBeforeAgencyDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'AgencyDiscount'
SET @v = N'agency discount - copied from InsertionOrder'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'AgencyDiscount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Price'
SET @v = N'NetCost = (DiscountedCost * (1-agency discount)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Price'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Cpm'
SET @v = N'Cpm - cost per mille'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InsertionOrderItem', N'COLUMN', N'Cpm'
GO
SET IDENTITY_INSERT dbo.Tmp_InsertionOrderItem ON
GO
IF EXISTS(SELECT * FROM dbo.InsertionOrderItem)
	 EXEC('INSERT INTO dbo.Tmp_InsertionOrderItem (K, InsertionOrderK, Description, BannerPosition, ImpressionQuantity, PriceBeforeDiscount, Discount, PriceBeforeAgencyDiscount, AgencyDiscount, Price, Cpm)
		SELECT K, InsertionOrderK, Description, BannerPosition, ImpressionQuantity, PriceBeforeDiscount, CONVERT(float(53), Discount), PriceBeforeAgencyDiscount, CONVERT(float(53), AgencyDiscount), Price, Cpm FROM dbo.InsertionOrderItem WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_InsertionOrderItem OFF
GO
DROP TABLE dbo.InsertionOrderItem
GO
EXECUTE sp_rename N'dbo.Tmp_InsertionOrderItem', N'InsertionOrderItem', 'OBJECT' 
GO
ALTER TABLE dbo.InsertionOrderItem ADD CONSTRAINT
	PK_InsertionOrderItem PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.InsertionOrderItemTrigger ON dbo.InsertionOrderItem 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].InsertionOrderItemTrigger')
end
GO
COMMIT
GO
COMMIT
GO
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
CREATE TABLE dbo.Tmp_InvoiceItemRevenue
	(
	InvoiceItemK int NOT NULL,
	Year int NOT NULL,
	Month int NOT NULL,
	Amount decimal(18, 2) NOT NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'The revenue from each invoice line may be attributable to several months'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'InvoiceItemK'
SET @v = N'Link to the InvoiceItem'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'InvoiceItemK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'Year'
SET @v = N'Year this revenue acts on'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'Year'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'Month'
SET @v = N'Month this revenue acts on'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'Month'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'Amount'
SET @v = N'Revenue that acts on this month'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceItemRevenue', N'COLUMN', N'Amount'
GO
IF EXISTS(SELECT * FROM dbo.InvoiceItemRevenue)
	 EXEC('INSERT INTO dbo.Tmp_InvoiceItemRevenue (InvoiceItemK, Year, Month, Amount)
		SELECT InvoiceItemK, Year, Month, CONVERT(decimal(18, 2), Amount) FROM dbo.InvoiceItemRevenue WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.InvoiceItemRevenue
GO
EXECUTE sp_rename N'dbo.Tmp_InvoiceItemRevenue', N'InvoiceItemRevenue', 'OBJECT' 
GO
ALTER TABLE dbo.InvoiceItemRevenue ADD CONSTRAINT
	IX_InvoiceItemRevenue UNIQUE NONCLUSTERED 
	(
	InvoiceItemK,
	Year,
	Month
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
GO
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
CREATE TABLE dbo.Tmp_InvoiceTransfer
	(
	InvoiceK int NOT NULL,
	TransferK int NOT NULL,
	Amount decimal(18, 2) NOT NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Invoice to Transfer relational table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceTransfer', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceTransfer', N'COLUMN', N'InvoiceK'
SET @v = N'Link to the Invoice table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceTransfer', N'COLUMN', N'InvoiceK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceTransfer', N'COLUMN', N'TransferK'
SET @v = N'Link to the Transfer table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceTransfer', N'COLUMN', N'TransferK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceTransfer', N'COLUMN', N'Amount'
SET @v = N'+ve for DSI receiving money, -ve for DSI paying out money'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_InvoiceTransfer', N'COLUMN', N'Amount'
GO
IF EXISTS(SELECT * FROM dbo.InvoiceTransfer)
	 EXEC('INSERT INTO dbo.Tmp_InvoiceTransfer (InvoiceK, TransferK, Amount)
		SELECT InvoiceK, TransferK, CONVERT(decimal(18, 2), Amount) FROM dbo.InvoiceTransfer WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.InvoiceTransfer
GO
EXECUTE sp_rename N'dbo.Tmp_InvoiceTransfer', N'InvoiceTransfer', 'OBJECT' 
GO
ALTER TABLE dbo.InvoiceTransfer ADD CONSTRAINT
	PK_InvoiceTransfer PRIMARY KEY CLUSTERED 
	(
	InvoiceK,
	TransferK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT

GO
