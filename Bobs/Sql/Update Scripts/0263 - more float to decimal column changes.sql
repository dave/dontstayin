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
ALTER TABLE dbo.Promoter
	DROP CONSTRAINT DF_Promoter_VatStatus
GO
ALTER TABLE dbo.Promoter
	DROP CONSTRAINT DF_Promoter_IsAgency
GO
CREATE TABLE dbo.Tmp_Promoter
	(
	K int NOT NULL IDENTITY (1, 1),
	Name varchar(200) NULL,
	Pic uniqueidentifier NULL,
	PrimaryUsrK int NULL,
	ContactName varchar(200) NULL,
	CompanyName varchar(200) NULL,
	PayPalAddress varchar(200) NULL,
	PhoneNumber varchar(200) NULL,
	AddressStreet varchar(200) NULL,
	AddressArea varchar(200) NULL,
	AddressTown varchar(200) NULL,
	AddressCounty varchar(200) NULL,
	AddressPostcode varchar(200) NULL,
	AddressCountryK int NULL,
	PricingMultiplier float(53) NULL,
	DateTimeSignUp datetime NULL,
	Status int NULL,
	TotalPaid decimal(18, 2) NULL,
	DateExpires datetime NULL,
	RenewalFee decimal(18, 2) NULL,
	RenewalMonths int NULL,
	HasPage bit NULL,
	AdminNote text NULL,
	QuestionsThreadK int NULL,
	DuplicateGuid uniqueidentifier NULL,
	UrlName varchar(200) NULL,
	HasGuestlist bit NULL,
	GuestlistCharge decimal(18, 2) NULL,
	GuestlistCredit int NULL,
	GuestlistCreditLimit int NULL,
	PicState varchar(100) NULL,
	PicPhotoK int NULL,
	PicMiscK int NULL,
	ClientsPerMonth int NULL,
	LastMessage int NULL,
	ManualNote text NULL,
	CreditLimit decimal(18, 2) NULL,
	InvoiceDueDays int NULL,
	EnabledDateTime datetime NULL,
	EnabledByUsrK int NULL,
	SalesUsrK int NULL,
	SalesStatus int NULL,
	SalesStatusExpires datetime NULL,
	SalesNextCall datetime NULL,
	LetterType int NULL,
	LetterStatus int NULL,
	IsSkeleton bit NULL,
	AccessCodeRandom varchar(50) NULL,
	OfferType int NULL,
	OfferExpireDateTime datetime NULL,
	SalesEstimate int NULL,
	SalesHold bit NULL,
	FutureEvents int NULL,
	DisableOverdueRedirect bit NULL,
	ContactEmail varchar(200) NULL,
	ContactTitle varchar(200) NULL,
	ContactPersonalTitle varchar(50) NULL,
	PhoneNumber2 varchar(200) NULL,
	WebAddress varchar(200) NULL,
	Alarm bit NULL,
	AccountsName varchar(200) NULL,
	AccountsEmail varchar(200) NULL,
	AccountsPhone varchar(200) NULL,
	ClientSector int NULL,
	EnableTickets bit NULL,
	VatStatus int NULL,
	VatNumber varchar(50) NULL,
	VatCountryK int NULL,
	AddedByUsrK int NULL,
	AddedMethod int NULL,
	BankName varchar(100) NULL,
	BankAccountName varchar(100) NULL,
	BankAccountSortCode varchar(50) NULL,
	BankAccountNumber varchar(50) NULL,
	OverrideApplyTicketFundsToInvoices bit NULL,
	SalesCallCount int NULL,
	RecentlyTransferred bit NULL,
	IsAgency bit NOT NULL,
	Discount int NULL,
	AddRandomCodeToTickets bit NULL,
	WillCheckCardsForPurchasedTickets bit NULL,
	SalesCampaignK int NULL,
	CostPerCampaignCredit decimal(18, 2) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'26/08/2008 14:45:51'
EXECUTE sp_addextendedproperty N'DataScriptLastRun', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', NULL, NULL
SET @v = N'A client who either operates clubbing brands or places adverts on dsi'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'The primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Name'
SET @v = N'Name of the Promoter / Event Promoter'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Name'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Pic'
SET @v = N'Cropped image 100*100'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Pic'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PrimaryUsrK'
SET @v = N'The user that first signed up this promoter'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PrimaryUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactName'
SET @v = N'Name of primary contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'CompanyName'
SET @v = N'Name of the company for billing purpouses'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'CompanyName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PayPalAddress'
SET @v = N'The email address to send paypal payments to'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PayPalAddress'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PhoneNumber'
SET @v = N'Contact phone number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PhoneNumber'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressStreet'
SET @v = N'Billing address street'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressStreet'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressArea'
SET @v = N'Billing address area'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressArea'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressTown'
SET @v = N'Billing address town'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressTown'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressCounty'
SET @v = N'Billing address county'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressCounty'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressPostcode'
SET @v = N'Billing address postcode'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressPostcode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressCountryK'
SET @v = N'Billing address country'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddressCountryK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PricingMultiplier'
SET @v = N'Base pricng is multiplied by this figure.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PricingMultiplier'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DateTimeSignUp'
SET @v = N'When the promoter first signed up'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DateTimeSignUp'
GO
DECLARE @v sql_variant 
SET @v = N'StatusEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Status'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Status'
SET @v = N'Status - AwaitingQuote=1, AwaitingPayment=2, Enabled=3, Disabled=4'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Status'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'TotalPaid'
SET @v = N'The total paid by this promoter for services'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'TotalPaid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DateExpires'
SET @v = N'The date that the promoters account expires and drops to limited functionality.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DateExpires'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'RenewalFee'
SET @v = N'The fee for renewing membership'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'RenewalFee'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'RenewalMonths'
SET @v = N'The number of months that the renewal fee is for'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'RenewalMonths'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'HasPage'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AdminNote'
SET @v = N'Admin note'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AdminNote'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'QuestionsThreadK'
SET @v = N'Private message thread'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'QuestionsThreadK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DuplicateGuid'
SET @v = N'Guid used to ensure duplicate promoters don''t get posted if the user refreshes the page after saving.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DuplicateGuid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'UrlName'
SET @v = N'Unique name used in the url'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'UrlName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'HasGuestlist'
SET @v = N'Can the promoter set up guestlist?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'HasGuestlist'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'GuestlistCharge'
SET @v = N'Charge per name on the guestlist...'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'GuestlistCharge'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'GuestlistCredit'
SET @v = N'Number of guestlist credits that the promoter has'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'GuestlistCredit'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'GuestlistCreditLimit'
SET @v = N'Amount that the promoter is alowed to go overdrawn on their guestlist credits'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'GuestlistCreditLimit'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PicState'
SET @v = N'State var used to reconstruct cropper when re-cropping'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PicState'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PicPhotoK'
SET @v = N'The Photo that was used to create the Pic.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PicPhotoK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PicMiscK'
SET @v = N'The Misc that was used to create the Pic.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PicMiscK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ClientsPerMonth'
SET @v = N'Calculated number of clients per month through the door'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ClientsPerMonth'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LastMessage'
SET @v = N'Id of the last message that was successfully sent to this promoter (used in case PM sender fails)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LastMessage'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ManualNote'
SET @v = N'Plain text editable by sales person, only used when idle or proactive'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ManualNote'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'CreditLimit'
SET @v = N'Credit limit in pounds'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'CreditLimit'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'InvoiceDueDays'
SET @v = N'When are invoices due (days) 0 = default'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'InvoiceDueDays'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'EnabledDateTime'
SET @v = N'When was this promoter first enabled?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'EnabledDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'EnabledByUsrK'
SET @v = N'Whick admin user enabled this promoter?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'EnabledByUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesUsrK'
SET @v = N'The sales person who owns the account / owned this account before expires date'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'SalesStatusEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesStatus'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesStatus'
SET @v = N'Status of this client before expires date (1 = New, 2 = Idle, 3 = Proactive, 4 = Active)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesStatus'
GO
DECLARE @v sql_variant 
SET @v = N'Date time when this client''s sales status expires, and they become idle'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesStatusExpires'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesNextCall'
SET @v = N'When to make the next call - used when someone requests to be called back in a month or something'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesNextCall'
GO
DECLARE @v sql_variant 
SET @v = N'LetterTypes'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LetterType'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LetterType'
SET @v = N'What type of letter are we about to send this promoter? 1 = CurrentNewPromoter, 2 = CurrentIdlePromoter, 3 = CurrentActivePromoter, 4 = AutoVenue'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LetterType'
GO
DECLARE @v sql_variant 
SET @v = N'LetterStatusEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LetterStatus'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LetterStatus'
SET @v = N'What is the printing status? 1 = New, 2 = Printing, 3 = Posted'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'LetterStatus'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'IsSkeleton'
SET @v = N'Is the account a skeleton account? (missing some contact details)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'IsSkeleton'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccessCodeRandom'
SET @v = N'Four digit random number used to auth access code'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccessCodeRandom'
GO
DECLARE @v sql_variant 
SET @v = N'OfferTypes'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'OfferType'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'OfferType'
SET @v = N'Which offer type are we showing?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'OfferType'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'OfferExpireDateTime'
SET @v = N'When does the offer expire?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'OfferExpireDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'SalesEstimateEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesEstimate'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesEstimate'
SET @v = N'Estimation of how good the client will be 0=not rated, 1=crap, 2=ok, 3=good, 4=excellent'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesEstimate'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesHold'
SET @v = N'Is this promoter account on hold? (No sales calls)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesHold'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'FutureEvents'
SET @v = N'Number of future events, updated overnight and when brands are added...'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'FutureEvents'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DisableOverdueRedirect'
SET @v = N'To disable the redirect when promoter account has overdue invoices for an extended period'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'DisableOverdueRedirect'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactEmail'
SET @v = N'Email of primary contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactEmail'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactTitle'
SET @v = N'Title of primary contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactTitle'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactPersonalTitle'
SET @v = N'Personal title of primary contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ContactPersonalTitle'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PhoneNumber2'
SET @v = N'Promoter''s 2nd phone number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'PhoneNumber2'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'WebAddress'
SET @v = N'Promoter''s primary website address'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'WebAddress'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Alarm'
SET @v = N'Alarm for SalesUsr when next call time arrives'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Alarm'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccountsName'
SET @v = N'Name of accounts contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccountsName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccountsEmail'
SET @v = N'Email of accounts contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccountsEmail'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccountsPhone'
SET @v = N'Phone number of accounts contact'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AccountsPhone'
GO
DECLARE @v sql_variant 
SET @v = N'ClientSectorEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ClientSector'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ClientSector'
SET @v = N'Client Sector: Promoter, Agency, Mobile operator, etc.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'ClientSector'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'EnableTickets'
SET @v = N'Has Promoter completed tickets/credit application form and been approved'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'EnableTickets'
GO
DECLARE @v sql_variant 
SET @v = N'VatStatusEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'VatStatus'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'VatStatus'
SET @v = N'Enum for Promoter''s VAT status: 0 = unknown, 1 = not registered, 2 = registered'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'VatStatus'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'VatNumber'
SET @v = N'Promoter''s VAT number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'VatNumber'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'VatCountryK'
SET @v = N'Country K in which the Promoter is VAT registered'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'VatCountryK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddedByUsrK'
SET @v = N'Who was the promoter added by (e.g. for sales admins)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddedByUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'AddedMedhods'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddedMethod'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddedMethod'
SET @v = N'How was this promoter added to the site (1=By end user on the site, 2=By sales user in the backend, 3=By automated import)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddedMethod'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankName'
SET @v = N'Promoter''s bank name'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankAccountName'
SET @v = N'Promoter''s bank account name'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankAccountName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankAccountSortCode'
SET @v = N'Promoter''s bank account sort code'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankAccountSortCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankAccountNumber'
SET @v = N'Promoter''s bank account number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'BankAccountNumber'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'OverrideApplyTicketFundsToInvoices'
SET @v = N'Override applying of ticket funds to unpaid invoices'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'OverrideApplyTicketFundsToInvoices'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesCallCount'
SET @v = N'Number of sales calls made to this promoter'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesCallCount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'RecentlyTransferred'
SET @v = N'Has this promoter been recently transferred to this sales user?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'RecentlyTransferred'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'IsAgency'
SET @v = N'if the promoter is an agency or not'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'IsAgency'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Discount'
SET @v = N'Discount percentage as an integer'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'Discount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddRandomCodeToTickets'
SET @v = N'Add a random code to tickets, to be displayed on doorlists'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'AddRandomCodeToTickets'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'WillCheckCardsForPurchasedTickets'
SET @v = N'Does this promoter want to confirm card details with us to avoid responsibility for card payments?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'WillCheckCardsForPurchasedTickets'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesCampaignK'
SET @v = N'If this promoter was added in a sales campaign, this is it'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'SalesCampaignK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'CostPerCampaignCredit'
SET @v = N'Cost per campaign credit'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Promoter', N'COLUMN', N'CostPerCampaignCredit'
GO
ALTER TABLE dbo.Tmp_Promoter ADD CONSTRAINT
	DF_Promoter_VatStatus DEFAULT ((0)) FOR VatStatus
GO
ALTER TABLE dbo.Tmp_Promoter ADD CONSTRAINT
	DF_Promoter_IsAgency DEFAULT ((0)) FOR IsAgency
GO
SET IDENTITY_INSERT dbo.Tmp_Promoter ON
GO
IF EXISTS(SELECT * FROM dbo.Promoter)
	 EXEC('INSERT INTO dbo.Tmp_Promoter (K, Name, Pic, PrimaryUsrK, ContactName, CompanyName, PayPalAddress, PhoneNumber, AddressStreet, AddressArea, AddressTown, AddressCounty, AddressPostcode, AddressCountryK, PricingMultiplier, DateTimeSignUp, Status, TotalPaid, DateExpires, RenewalFee, RenewalMonths, HasPage, AdminNote, QuestionsThreadK, DuplicateGuid, UrlName, HasGuestlist, GuestlistCharge, GuestlistCredit, GuestlistCreditLimit, PicState, PicPhotoK, PicMiscK, ClientsPerMonth, LastMessage, ManualNote, CreditLimit, InvoiceDueDays, EnabledDateTime, EnabledByUsrK, SalesUsrK, SalesStatus, SalesStatusExpires, SalesNextCall, LetterType, LetterStatus, IsSkeleton, AccessCodeRandom, OfferType, OfferExpireDateTime, SalesEstimate, SalesHold, FutureEvents, DisableOverdueRedirect, ContactEmail, ContactTitle, ContactPersonalTitle, PhoneNumber2, WebAddress, Alarm, AccountsName, AccountsEmail, AccountsPhone, ClientSector, EnableTickets, VatStatus, VatNumber, VatCountryK, AddedByUsrK, AddedMethod, BankName, BankAccountName, BankAccountSortCode, BankAccountNumber, OverrideApplyTicketFundsToInvoices, SalesCallCount, RecentlyTransferred, IsAgency, Discount, AddRandomCodeToTickets, WillCheckCardsForPurchasedTickets, SalesCampaignK, CostPerCampaignCredit)
		SELECT K, Name, Pic, PrimaryUsrK, ContactName, CompanyName, PayPalAddress, PhoneNumber, AddressStreet, AddressArea, AddressTown, AddressCounty, AddressPostcode, AddressCountryK, PricingMultiplier, DateTimeSignUp, Status, CONVERT(decimal(18, 2), TotalPaid), DateExpires, CONVERT(decimal(18, 2), RenewalFee), RenewalMonths, HasPage, AdminNote, QuestionsThreadK, DuplicateGuid, UrlName, HasGuestlist, CONVERT(decimal(18, 2), GuestlistCharge), GuestlistCredit, GuestlistCreditLimit, PicState, PicPhotoK, PicMiscK, ClientsPerMonth, LastMessage, ManualNote, CONVERT(decimal(18, 2), CreditLimit), InvoiceDueDays, EnabledDateTime, EnabledByUsrK, SalesUsrK, SalesStatus, SalesStatusExpires, SalesNextCall, LetterType, LetterStatus, IsSkeleton, AccessCodeRandom, OfferType, OfferExpireDateTime, SalesEstimate, SalesHold, FutureEvents, DisableOverdueRedirect, ContactEmail, ContactTitle, ContactPersonalTitle, PhoneNumber2, WebAddress, Alarm, AccountsName, AccountsEmail, AccountsPhone, ClientSector, EnableTickets, VatStatus, VatNumber, VatCountryK, AddedByUsrK, AddedMethod, BankName, BankAccountName, BankAccountSortCode, BankAccountNumber, OverrideApplyTicketFundsToInvoices, SalesCallCount, RecentlyTransferred, IsAgency, Discount, AddRandomCodeToTickets, WillCheckCardsForPurchasedTickets, SalesCampaignK, CONVERT(decimal(18, 2), CostPerCampaignCredit) FROM dbo.Promoter WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Promoter OFF
GO
DROP TABLE dbo.Promoter
GO
EXECUTE sp_rename N'dbo.Tmp_Promoter', N'Promoter', 'OBJECT' 
GO
ALTER TABLE dbo.Promoter ADD CONSTRAINT
	PK_Promoter PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.PromoterTrigger ON dbo.Promoter 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].PromoterTrigger')
end
GO
COMMIT















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
CREATE TABLE dbo.Tmp_Ticket
	(
	K int NOT NULL IDENTITY (1, 1),
	TicketRunK int NULL,
	EventK int NULL,
	BuyerUsrK int NULL,
	OwnerUsrK int NULL,
	OfferUsrK int NULL,
	Enabled bit NULL,
	Cancelled bit NULL,
	Refunded bit NULL,
	BuyDateTime datetime NULL,
	OfferDateTime datetime NULL,
	AcceptDateTime datetime NULL,
	AddressStreet varchar(150) NULL,
	AddressArea varchar(50) NULL,
	AddressTown varchar(50) NULL,
	AddressCounty varchar(50) NULL,
	AddressPostcode varchar(50) NULL,
	AddressCountryK int NULL,
	Mobile varchar(50) NULL,
	MobileCountryCode varchar(3) NULL,
	MobileNumber varchar(50) NULL,
	FirstName varchar(100) NULL,
	LastName varchar(100) NULL,
	CardNumberHash uniqueidentifier NULL,
	CardNumberEnd varchar(6) NULL,
	CardNumberDigits int NULL,
	Quantity int NULL,
	CustomData text NULL,
	CustomXml xml NULL,
	InvoiceK int NULL,
	InvoiceItemK int NULL,
	BrowserGuid uniqueidentifier NULL,
	Price decimal(18, 2) NULL,
	BookingFee decimal(18, 2) NULL,
	IpAddress varchar(15) NULL,
	Feedback int NULL,
	FeedbackNote varchar(4096) NULL,
	ReserveDateTime datetime NULL,
	Code varchar(4) NULL,
	DomainK int NULL,
	CancelledBeforeFundsRelease bit NULL,
	CancelledDateTime datetime NULL,
	CardCV2 varchar(3) NULL,
	CardCheckedByPromoter bit NULL,
	CardCheckAttempts int NULL,
	AddressName varchar(150) NULL,
	IsFraud bit NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'One ticket created each time someone buys a ticket'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'Key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'TicketRunK'
SET @v = N'Ticket run link'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'TicketRunK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'EventK'
SET @v = N'Link to the event table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'EventK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BuyerUsrK'
SET @v = N'The user that bought the ticket'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BuyerUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'OwnerUsrK'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'OwnerUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'OfferUsrK'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'OfferUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Enabled'
SET @v = N'Tickets that have been successfully processed should set Enabled = true. Tickets that are not enabled are a place holder to be used while trying to process.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Enabled'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Cancelled'
SET @v = N'If the ticket has been cancelled'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Cancelled'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Refunded'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Refunded'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BuyDateTime'
SET @v = N'Date time of the original purchase'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BuyDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'OfferDateTime'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'OfferDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AcceptDateTime'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AcceptDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressStreet'
SET @v = N'Address - Street'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressStreet'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressArea'
SET @v = N'Address - Area'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressArea'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressTown'
SET @v = N'Address - Place'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressTown'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressCounty'
SET @v = N'Address - County'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressCounty'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressPostcode'
SET @v = N'Address - Postcode'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressPostcode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressCountryK'
SET @v = N'Address - Country (link to Country table)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressCountryK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Mobile'
SET @v = N'Full mobile number including country code (e.g. 447971597702)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Mobile'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'MobileCountryCode'
SET @v = N'Country code of mobile number (e.g. 44)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'MobileCountryCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'MobileNumber'
SET @v = N'Mobile number excluding country code and leading zero (e.g. 7971597702)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'MobileNumber'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'FirstName'
SET @v = N'First name, verified by credit card'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'FirstName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'LastName'
SET @v = N'Last name, verified by credit card'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'LastName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardNumberHash'
SET @v = N'Cryptographic hash of the card number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardNumberHash'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardNumberEnd'
SET @v = N'Last 6 digits of the card number used to order'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardNumberEnd'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardNumberDigits'
SET @v = N'Number of digits on the card used to order'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardNumberDigits'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Quantity'
SET @v = N'Quantity of tickets (for multiple-entrance tickets)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Quantity'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CustomData'
SET @v = N'Custom data specific to this ticket run (as a string)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CustomData'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CustomXml'
SET @v = N'Custom data specific to this ticket run (as xml)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CustomXml'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'InvoiceK'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'InvoiceK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'InvoiceItemK'
SET @v = N'Link to the invoice item table - e.g. booking reference'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'InvoiceItemK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BrowserGuid'
SET @v = N'Guid from the browser cookie'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BrowserGuid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Price'
SET @v = N'Price in pounds'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Price'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BookingFee'
SET @v = N'Our booking fee'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'BookingFee'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'IpAddress'
SET @v = N'Buyer''s IpAddress'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'IpAddress'
GO
DECLARE @v sql_variant 
SET @v = N'FeedbackEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Feedback'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Feedback'
SET @v = N'Post event feedback enum: None=0, Good=1, Bad=2'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Feedback'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'FeedbackNote'
SET @v = N'Post event feedback text for negative comments'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'FeedbackNote'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'ReserveDateTime'
SET @v = N'Date time til when a pending ticket is reserved until'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'ReserveDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Code'
SET @v = N'Random code generated for the ticket'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'Code'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'DomainK'
SET @v = N'Domain from which the request originated'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'DomainK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CancelledBeforeFundsRelease'
SET @v = N'Was this ticket cancelled / refunded before the promoter funds release event?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CancelledBeforeFundsRelease'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CancelledDateTime'
SET @v = N'Date / time that the ticket was cancelled / refunded'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CancelledDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardCV2'
SET @v = N'CV2 Security code on the back on the credit card'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardCV2'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardCheckedByPromoter'
SET @v = N'Has the promoter proven that the card was checked?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardCheckedByPromoter'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardCheckAttempts'
SET @v = N'How many times the promoter has attempted to confirm card details'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'CardCheckAttempts'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressName'
SET @v = N'Name of the person to deliver the tickets to'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'AddressName'
GO
DECLARE @v sql_variant 
SET @v = N'Is this ticket suspected fraud?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Ticket', N'COLUMN', N'IsFraud'
GO
SET IDENTITY_INSERT dbo.Tmp_Ticket ON
GO
IF EXISTS(SELECT * FROM dbo.Ticket)
	 EXEC('INSERT INTO dbo.Tmp_Ticket (K, TicketRunK, EventK, BuyerUsrK, OwnerUsrK, OfferUsrK, Enabled, Cancelled, Refunded, BuyDateTime, OfferDateTime, AcceptDateTime, AddressStreet, AddressArea, AddressTown, AddressCounty, AddressPostcode, AddressCountryK, Mobile, MobileCountryCode, MobileNumber, FirstName, LastName, CardNumberHash, CardNumberEnd, CardNumberDigits, Quantity, CustomData, CustomXml, InvoiceK, InvoiceItemK, BrowserGuid, Price, BookingFee, IpAddress, Feedback, FeedbackNote, ReserveDateTime, Code, DomainK, CancelledBeforeFundsRelease, CancelledDateTime, CardCV2, CardCheckedByPromoter, CardCheckAttempts, AddressName, IsFraud)
		SELECT K, TicketRunK, EventK, BuyerUsrK, OwnerUsrK, OfferUsrK, Enabled, Cancelled, Refunded, BuyDateTime, OfferDateTime, AcceptDateTime, AddressStreet, AddressArea, AddressTown, AddressCounty, AddressPostcode, AddressCountryK, Mobile, MobileCountryCode, MobileNumber, FirstName, LastName, CardNumberHash, CardNumberEnd, CardNumberDigits, Quantity, CustomData, CustomXml, InvoiceK, InvoiceItemK, BrowserGuid, CONVERT(decimal(18, 2), Price), CONVERT(decimal(18, 2), BookingFee), IpAddress, Feedback, FeedbackNote, ReserveDateTime, Code, DomainK, CancelledBeforeFundsRelease, CancelledDateTime, CardCV2, CardCheckedByPromoter, CardCheckAttempts, AddressName, IsFraud FROM dbo.Ticket WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Ticket OFF
GO
DROP TABLE dbo.Ticket
GO
EXECUTE sp_rename N'dbo.Tmp_Ticket', N'Ticket', 'OBJECT' 
GO
ALTER TABLE dbo.Ticket ADD CONSTRAINT
	PK_TicketSale PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.TicketTrigger ON dbo.Ticket 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].TicketTrigger')
end
GO
COMMIT









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
	SalesUsrAmount decimal(18, 2) NULL,
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
		SELECT K, Type, UsrK, PromoterK, ActionUsrK, Name, Address, Postcode, PaymentType, Paid, CreatedDateTime, DueDateTime, PaidDateTime, CONVERT(decimal(18, 2), Price), CONVERT(decimal(18, 2), Vat), CONVERT(decimal(18, 2), Total), DuplicateGuid, Notes, VatCode, SalesUsrK, CONVERT(decimal(18, 2), SalesUsrAmount), IsImmediateCreditCardPayment, TaxDateTime, PurchaseOrderNumber, BuyerType, CONVERT(decimal(18, 2), PriceBeforeDiscount), Discount, CONVERT(decimal(18, 2), PriceBeforeAgencyDiscount), AgencyDiscount, InsertionOrderK FROM dbo.Invoice WITH (HOLDLOCK TABLOCKX)')
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
CREATE TABLE dbo.Tmp_TicketRun
	(
	K int NOT NULL IDENTITY (1, 1),
	EventK int NULL,
	PromoterK int NULL,
	BrandK int NULL,
	Name varchar(30) NULL,
	Description varchar(256) NULL,
	Price decimal(18, 2) NULL,
	BookingFee decimal(18, 2) NULL,
	LockPrice bit NULL,
	FollowsTicketRunK int NULL,
	StartDateTime datetime NULL,
	EndDateTime datetime NULL,
	Enabled bit NULL,
	MaxTickets int NULL,
	SoldTickets int NULL,
	CustomData text NULL,
	CustomXml xml NULL,
	ListOrder float(53) NULL,
	Paused bit NULL,
	DuplicateGuid uniqueidentifier NULL,
	EmailSent bit NULL,
	DeliveryDate datetime NULL,
	DeliveryMethod int NULL,
	DeliveryCharge decimal(6, 2) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'26/08/2008 14:45:51'
EXECUTE sp_addextendedproperty N'DataScriptLastRun', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', NULL, NULL
SET @v = N'Run of tickets for sale'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'Key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'EventK'
SET @v = N'Event this ticket is for'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'EventK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'PromoterK'
SET @v = N'Promoter selling the ticket'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'PromoterK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'BrandK'
SET @v = N'Brand this ticket is for (zero if not relevant)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'BrandK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Name'
SET @v = N'Brief name for ticket run: i.e. VIP, Early bird, etc'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Name'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Description'
SET @v = N'Description short description e.g. "Early Bird"'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Description'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Price'
SET @v = N'Price in pounds'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Price'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'BookingFee'
SET @v = N'Our booking fee'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'BookingFee'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'LockPrice'
SET @v = N'If locked, the promoter won''t be able to edit the price'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'LockPrice'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'FollowsTicketRunK'
SET @v = N'If this is specified, these tickets aren''t offered until a different ticket type sells out or date ends'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'FollowsTicketRunK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'StartDateTime'
SET @v = N'Tickets are offered from this DateTime onward'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'StartDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'EndDateTime'
SET @v = N'Tickets are unavailable after this DateTime'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'EndDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Enabled'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Enabled'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'MaxTickets'
SET @v = N'Maximum number of tickets to sell'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'MaxTickets'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'SoldTickets'
SET @v = N'Number of tickets sold at the moment'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'SoldTickets'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'CustomData'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'CustomData'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'CustomXml'
SET @v = N''
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'CustomXml'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'ListOrder'
SET @v = N'Order in the list on the event page'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'ListOrder'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Paused'
SET @v = N'Has the selling of this ticket run been paused'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'Paused'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DuplicateGuid'
SET @v = N'Guid to catch duplicate "save" clicks'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DuplicateGuid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'EmailSent'
SET @v = N'Bit flag to note when email has been sent to promoter after ticket run has ended'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'EmailSent'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DeliveryDate'
SET @v = N'Approximate date tickets usrs will be told tickets will be delivered'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DeliveryDate'
GO
DECLARE @v sql_variant 
SET @v = N'DeliveryMethodType'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DeliveryMethod'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DeliveryMethod'
SET @v = N'Delivery method for the tickets'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DeliveryMethod'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DeliveryCharge'
SET @v = N'Delivery charge for deliverinh the tickets'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketRun', N'COLUMN', N'DeliveryCharge'
GO
SET IDENTITY_INSERT dbo.Tmp_TicketRun ON
GO
IF EXISTS(SELECT * FROM dbo.TicketRun)
	 EXEC('INSERT INTO dbo.Tmp_TicketRun (K, EventK, PromoterK, BrandK, Name, Description, Price, BookingFee, LockPrice, FollowsTicketRunK, StartDateTime, EndDateTime, Enabled, MaxTickets, SoldTickets, CustomData, CustomXml, ListOrder, Paused, DuplicateGuid, EmailSent, DeliveryDate, DeliveryMethod, DeliveryCharge)
		SELECT K, EventK, PromoterK, BrandK, Name, Description, CONVERT(decimal(18, 2), Price), CONVERT(decimal(18, 2), BookingFee), LockPrice, FollowsTicketRunK, StartDateTime, EndDateTime, Enabled, MaxTickets, SoldTickets, CustomData, CustomXml, ListOrder, Paused, DuplicateGuid, EmailSent, DeliveryDate, DeliveryMethod, CONVERT(decimal(18,2), DeliveryCharge) FROM dbo.TicketRun WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_TicketRun OFF
GO
DROP TABLE dbo.TicketRun
GO
EXECUTE sp_rename N'dbo.Tmp_TicketRun', N'TicketRun', 'OBJECT' 
GO
ALTER TABLE dbo.TicketRun ADD CONSTRAINT
	PK_TicketRun PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.TicketRunTrigger ON dbo.TicketRun 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].TicketRunTrigger')
end
GO
COMMIT


















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
CREATE TABLE dbo.Tmp_Transfer
	(
	K int NOT NULL IDENTITY (1, 1),
	Type int NULL,
	Status int NULL,
	Method int NULL,
	UsrK int NULL,
	PromoterK int NULL,
	ActionUsrK int NULL,
	Amount decimal(18, 2) NULL,
	DateTime datetime NULL,
	DateTimeCreated datetime NULL,
	DateTimeComplete datetime NULL,
	ClientHost varchar(15) NULL,
	CardName varchar(150) NULL,
	CardAddress1 varchar(150) NULL,
	CardPostcode varchar(150) NULL,
	CardSavedTransferK int NULL,
	CardNumberHash uniqueidentifier NULL,
	CardNumberEnd varchar(20) NULL,
	CardType int NULL,
	CardStart datetime NULL,
	CardExpires datetime NULL,
	CardIssue int NULL,
	CardCV2 varchar(5) NULL,
	CardSaved bit NULL,
	BankAccountName varchar(50) NULL,
	BankName varchar(50) NULL,
	BankSortCode varchar(50) NULL,
	BankAccountNumber varchar(50) NULL,
	BankTransferReference varchar(50) NULL,
	CardResponseAuthCode varchar(50) NULL,
	CardResponseCv2Avs varchar(50) NULL,
	CardResponseMessage varchar(150) NULL,
	CardResponseRespCode varchar(10) NULL,
	CardResponseCode varchar(20) NULL,
	CardResponseIsCv2Match bit NULL,
	CardResponseIsPostCodeMatch bit NULL,
	CardResponseIsAddressMatch bit NULL,
	CardResponseIsDataChecked bit NULL,
	Notes text NULL,
	IsFullyApplied bit NULL,
	Guid uniqueidentifier NULL,
	TransferRefundedK int NULL,
	RefundStatus int NULL,
	DuplicateGuid uniqueidentifier NULL,
	ChequeReferenceNumber varchar(20) NULL,
	CardDigits int NULL,
	DSIBankAccount int NULL,
	CardAddressArea varchar(50) NULL,
	CardAddressTown varchar(50) NULL,
	CardAddressCounty varchar(50) NULL,
	CardAddressCountryK int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Customer pays us using a credit card / card is refunded / customer transfers money into our bank acc'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'Primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'TransferTypes'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Type'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Type'
SET @v = N'Payment, Refund'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Type'
GO
DECLARE @v sql_variant 
SET @v = N'StatusEnum'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Status'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Status'
SET @v = N'Pending, Success, Cancelled'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Status'
GO
DECLARE @v sql_variant 
SET @v = N'Methods'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Method'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Method'
SET @v = N'Card, Bank Transfer, Cheque, Cash'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Method'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'UsrK'
SET @v = N'Link to the relevant user'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'UsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'PromoterK'
SET @v = N'Link to the promoter (if this is a promoter transfer)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'PromoterK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'ActionUsrK'
SET @v = N'Link to the user that initiated this transfer (e.g. the admin user if it''s a refund!)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'ActionUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Amount'
SET @v = N'+ve for DSI receiving money, -ve for DSI paying out money'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Amount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DateTime'
SET @v = N'Date / time the transfer was initiated / received'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DateTimeCreated'
SET @v = N'Date / time the transfer was created'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DateTimeCreated'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DateTimeComplete'
SET @v = N'Date / time the transfer was completed'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DateTimeComplete'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'ClientHost'
SET @v = N'IP address of the client machine'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'ClientHost'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardName'
SET @v = N'for card payment - the billing name'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddress1'
SET @v = N'for card payment - the billing address (line 1)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddress1'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardPostcode'
SET @v = N'for card payment - the billing postcode'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardPostcode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardSavedTransferK'
SET @v = N'This transfer used card details from an earlier transfer (saved card details or refund)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardSavedTransferK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardNumberHash'
SET @v = N'Cryptographic hash of the card number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardNumberHash'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardNumberEnd'
SET @v = N'Last 6 digits of the card number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardNumberEnd'
GO
DECLARE @v sql_variant 
SET @v = N'BinRange.Types'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardType'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardType'
SET @v = N'Card issuer deduced from card number (e.g. Visa, Mastercard etc.)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardType'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardStart'
SET @v = N'Card start date'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardStart'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardExpires'
SET @v = N'Card expiry date'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardExpires'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardIssue'
SET @v = N'Issue number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardIssue'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardCV2'
SET @v = N'Card CV2 number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardCV2'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardSaved'
SET @v = N'Is the card saved for further use?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardSaved'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankAccountName'
SET @v = N'The account name - e.g. Uprising Clubs Limited'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankAccountName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankName'
SET @v = N'The bank name - e.g. Lloyds'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankSortCode'
SET @v = N'Sort code'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankSortCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankAccountNumber'
SET @v = N'Account number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankAccountNumber'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankTransferReference'
SET @v = N'The reference/comment added to the transfer'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'BankTransferReference'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseAuthCode'
SET @v = N'Only when Status=Success.The bank''s authorisation code for your information only, do not show to customer.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseAuthCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseCv2Avs'
SET @v = N'The Apacs approved text that is supplied as a result of the CV2 and AVS anti-Fraud checks. There are five core values defined, these are ALL MATCH, SECURITY CODE MATCH ONLY, ADDRESS MATCH ONLY, NO DATA MATCHES and DATA NOT CHECKED. With these core codes an address is only understood to match if and only if both the address proper and the postcode match at the same time. This is a little strict for some people so the following codes have been introduced too : PARTIAL ADDRESS MATCH / POSTCODE, PARTIAL ADDRESS MATCH / ADDRESS, SECURITY CODE MATCH / POSTCODE, SECURITY CODE MATCH / ADDRESS. Codes are only supplied when CV2 and/or Billing Address data is supplied, it is in your interests to supply this data to us. Note that at present all issuers should be issuing new cards with a CV2 security code on them however the AVS checks will only work with UK issued cards. Also note that Switch have not yet implemented these checks (but the cards should have the CV2 security code on them nevertheless). '
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseCv2Avs'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseMessage'
SET @v = N'Only when Status=Failed. The bank''s failure message for your information only, do not show to customer.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseMessage'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseRespCode'
SET @v = N'Only when Status=Failed and CardResponseCode=''N''. The bank''s failure code for your information only, do not show to customer. 2 or 83 : referral, 5 or 54 : Not Authorised, 30 : general error (retrying after 1 minute may succeed, depending on error)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseRespCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseCode'
SET @v = N'The code field is a short code giving extensive details of failure states. It is of particular use to SECBatch/SECVPN users. Note : preauth checks can have several errors, e.g. P:NEC means the name, expiry date and card number fields are all invalid or not supplied.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsCv2Match'
SET @v = N'Flag to mark results from CV2 Fraud check'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsCv2Match'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsPostCodeMatch'
SET @v = N'Flag to mark results from Post Code Fraud check'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsPostCodeMatch'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsAddressMatch'
SET @v = N'Flag to mark results from Address Fraud check'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsAddressMatch'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsDataChecked'
SET @v = N'Flag to mark if fraud check was enforced'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardResponseIsDataChecked'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Notes'
SET @v = N'Additional Notes'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Notes'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'IsFullyApplied'
SET @v = N'This flag is to be set when the sum of InvoiceTransfers amounts = Transfer.Amount.  It will facilitate faster / easier searches'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'IsFullyApplied'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Guid'
SET @v = N'The guid of the transfer.  Allows unique identifier to be assigned prior to saving to the db'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'Guid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'TransferRefundedK'
SET @v = N'Transfer K of transfer that this has refunded'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'TransferRefundedK'
GO
DECLARE @v sql_variant 
SET @v = N'RefundStatuses'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'RefundStatus'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'RefundStatus'
SET @v = N'Not Refunded, Partial Refund, Full Refund'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'RefundStatus'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DuplicateGuid'
SET @v = N'Guid to catch duplicate on save'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DuplicateGuid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'ChequeReferenceNumber'
SET @v = N'The cheque reference number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'ChequeReferenceNumber'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardDigits'
SET @v = N'Number of digits in the card number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardDigits'
GO
DECLARE @v sql_variant 
SET @v = N'DSIBankAccounts'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DSIBankAccount'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DSIBankAccount'
SET @v = N'Which DSI bank account was used in this transfer. DSI Current account = 1, DSI Client account = 2'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'DSIBankAccount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressArea'
SET @v = N'Part of address card is registered to'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressArea'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressTown'
SET @v = N'Part of address card is registered to'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressTown'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressCounty'
SET @v = N'Part of address card is registered to'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressCounty'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressCountryK'
SET @v = N'Part of address card is registered to'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Transfer', N'COLUMN', N'CardAddressCountryK'
GO
SET IDENTITY_INSERT dbo.Tmp_Transfer ON
GO
IF EXISTS(SELECT * FROM dbo.Transfer)
	 EXEC('INSERT INTO dbo.Tmp_Transfer (K, Type, Status, Method, UsrK, PromoterK, ActionUsrK, Amount, DateTime, DateTimeCreated, DateTimeComplete, ClientHost, CardName, CardAddress1, CardPostcode, CardSavedTransferK, CardNumberHash, CardNumberEnd, CardType, CardStart, CardExpires, CardIssue, CardCV2, CardSaved, BankAccountName, BankName, BankSortCode, BankAccountNumber, BankTransferReference, CardResponseAuthCode, CardResponseCv2Avs, CardResponseMessage, CardResponseRespCode, CardResponseCode, CardResponseIsCv2Match, CardResponseIsPostCodeMatch, CardResponseIsAddressMatch, CardResponseIsDataChecked, Notes, IsFullyApplied, Guid, TransferRefundedK, RefundStatus, DuplicateGuid, ChequeReferenceNumber, CardDigits, DSIBankAccount, CardAddressArea, CardAddressTown, CardAddressCounty, CardAddressCountryK)
		SELECT K, Type, Status, Method, UsrK, PromoterK, ActionUsrK, CONVERT(decimal(18, 2), Amount), DateTime, DateTimeCreated, DateTimeComplete, ClientHost, CardName, CardAddress1, CardPostcode, CardSavedTransferK, CardNumberHash, CardNumberEnd, CardType, CardStart, CardExpires, CardIssue, CardCV2, CardSaved, BankAccountName, BankName, BankSortCode, BankAccountNumber, BankTransferReference, CardResponseAuthCode, CardResponseCv2Avs, CardResponseMessage, CardResponseRespCode, CardResponseCode, CardResponseIsCv2Match, CardResponseIsPostCodeMatch, CardResponseIsAddressMatch, CardResponseIsDataChecked, Notes, IsFullyApplied, Guid, TransferRefundedK, RefundStatus, DuplicateGuid, ChequeReferenceNumber, CardDigits, DSIBankAccount, CardAddressArea, CardAddressTown, CardAddressCounty, CardAddressCountryK FROM dbo.Transfer WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Transfer OFF
GO
DROP TABLE dbo.Transfer
GO
EXECUTE sp_rename N'dbo.Tmp_Transfer', N'Transfer', 'OBJECT' 
GO
ALTER TABLE dbo.Transfer ADD CONSTRAINT
	PK_Transfer PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.TransferTrigger ON dbo.Transfer 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].TransferTrigger')
end
GO
COMMIT

















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
CREATE TABLE dbo.Tmp_BankExport
	(
	K int NOT NULL IDENTITY (1, 1),
	AddedDateTime datetime NOT NULL,
	OutputDateTime datetime NULL,
	ProcessingDateTime datetime NULL,
	TransferK int NULL,
	PromoterK int NULL,
	Type int NULL,
	Amount decimal(18, 2) NULL,
	BatchRef varchar(18) NULL,
	Status int NULL,
	BankName varchar(35) NULL,
	BankAccountSortCode varchar(20) NULL,
	BankAccountNumber varchar(25) NULL,
	Details varchar(4000) NULL,
	ReferenceDateTime datetime NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Exports to bank for transferring funds'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'Primary Key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'AddedDateTime'
SET @v = N'Date when it was added'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'AddedDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'OutputDateTime'
SET @v = N'Date when it was output was exported'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'OutputDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'ProcessingDateTime'
SET @v = N'Date when it will be processed'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'ProcessingDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'TransferK'
SET @v = N'Transfer key reference'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'TransferK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'PromoterK'
SET @v = N'Promoter key reference'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'PromoterK'
GO
DECLARE @v sql_variant 
SET @v = N'Types'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Type'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Type'
SET @v = N'Type enum: BACS = 1, Internal = 2'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Type'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Amount'
SET @v = N'Amount of transaction'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Amount'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BatchRef'
SET @v = N'Unique batch reference #'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BatchRef'
GO
DECLARE @v sql_variant 
SET @v = N'Statuses'
EXECUTE sp_addextendedproperty N'EnumProperty', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Status'
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Status'
SET @v = N'Status enum: Added, AwaitingConfirmation, Successful, Failed, Cancelled.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Status'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BankName'
SET @v = N'Beneficiary''s bank name'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BankName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BankAccountSortCode'
SET @v = N'Beneficiary''s bank account sort code'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BankAccountSortCode'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BankAccountNumber'
SET @v = N'Beneficiary''s bank account number'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'BankAccountNumber'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Details'
SET @v = N'Details of bank export for reference'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'Details'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'ReferenceDateTime'
SET @v = N'Date of transaction that it is referencing'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_BankExport', N'COLUMN', N'ReferenceDateTime'
GO
SET IDENTITY_INSERT dbo.Tmp_BankExport ON
GO
IF EXISTS(SELECT * FROM dbo.BankExport)
	 EXEC('INSERT INTO dbo.Tmp_BankExport (K, AddedDateTime, OutputDateTime, ProcessingDateTime, TransferK, PromoterK, Type, Amount, BatchRef, Status, BankName, BankAccountSortCode, BankAccountNumber, Details, ReferenceDateTime)
		SELECT K, AddedDateTime, OutputDateTime, ProcessingDateTime, TransferK, PromoterK, Type, CONVERT(decimal(18, 2), Amount), BatchRef, Status, BankName, BankAccountSortCode, BankAccountNumber, Details, ReferenceDateTime FROM dbo.BankExport WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_BankExport OFF
GO
DROP TABLE dbo.BankExport
GO
EXECUTE sp_rename N'dbo.Tmp_BankExport', N'BankExport', 'OBJECT' 
GO
ALTER TABLE dbo.BankExport ADD CONSTRAINT
	PK_BankExport PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.BankExportTrigger ON dbo.BankExport 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].BankExportTrigger')
end
GO
COMMIT




























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
CREATE TABLE dbo.Tmp_TicketPromoterEvent
	(
	PromoterK int NOT NULL,
	EventK int NOT NULL,
	TotalTickets int NULL,
	SoldTickets int NULL,
	TotalFunds decimal(18, 2) NULL,
	FundsLockManual bit NULL,
	FundsLockManualUsrK int NULL,
	FundsLockManualDateTime datetime NULL,
	FundsLockManualNote varchar(500) NULL,
	FundsLockFraudIpDuplicate bit NULL,
	FundsLockFraudIpCountry int NULL,
	FundsLockFraudGuid bit NULL,
	FundsLockUsrResponses bit NULL,
	FundsLockText varchar(2000) NULL,
	FundsLockOverride bit NULL,
	FundsLockOverrideUsrK int NULL,
	FundsLockOverrideDateTime datetime NULL,
	FundsLockOverrideNote varchar(500) NULL,
	FundsReleased bit NULL,
	FundsTransferK int NULL,
	CancelledTickets int NULL,
	FundsLockTotalFundsDontMatch bit NULL,
	TotalVat decimal(18, 2) NULL,
	TotalBookingFees decimal(18, 2) NULL,
	ContactEmail varchar(100) NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'26/08/2008 14:45:51'
EXECUTE sp_addextendedproperty N'DataScriptLastRun', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', NULL, NULL
SET @v = N'TicketPromoter to Event relational table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'PromoterK'
SET @v = N'The promoter for the tickets'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'PromoterK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'EventK'
SET @v = N'The event for the tickets'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'EventK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalTickets'
SET @v = N'Total number of tickets available'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalTickets'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'SoldTickets'
SET @v = N'Total number of tickets sold'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'SoldTickets'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalFunds'
SET @v = N'Total amount of money from sold tickets'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalFunds'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManual'
SET @v = N'Have the funds been locked manually'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManual'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManualUsrK'
SET @v = N'The user who locked the funds manually'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManualUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManualDateTime'
SET @v = N'Timestamp for manual funds lock'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManualDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManualNote'
SET @v = N'Note for manual funds lock'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockManualNote'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockFraudIpDuplicate'
SET @v = N'Are funds locked due to duplicate IP fraud'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockFraudIpDuplicate'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockFraudIpCountry'
SET @v = N'Country origin of duplicate IP fraud'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockFraudIpCountry'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockFraudGuid'
SET @v = N'Are funds locked due to GUID fraud'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockFraudGuid'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockUsrResponses'
SET @v = N'Are funds locked due to users negative responses'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockUsrResponses'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockText'
SET @v = N'Text explaining any locks, readable by admins and used when making unlock decisions'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockText'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverride'
SET @v = N'Is funds lock overridden'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverride'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverrideUsrK'
SET @v = N'The user who overrode the funds lock'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverrideUsrK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverrideDateTime'
SET @v = N'Timestamp for funds lock override'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverrideDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverrideNote'
SET @v = N'Note explaining why funds lock has been overridden'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockOverrideNote'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsReleased'
SET @v = N'Have funds been released to promoter'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsReleased'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsTransferK'
SET @v = N'Transfer reference for funds to promoter'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsTransferK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'CancelledTickets'
SET @v = N'Total number of tickets cancelled'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'CancelledTickets'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockTotalFundsDontMatch'
SET @v = N'Lock when the total funds dont match the ticket run funds'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'FundsLockTotalFundsDontMatch'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalVat'
SET @v = N'Total amount of VAT from ticket invoices'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalVat'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalBookingFees'
SET @v = N'Total amount of booking fees'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'TotalBookingFees'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'ContactEmail'
SET @v = N'Contact email address for users to contact regarding ticket sales'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_TicketPromoterEvent', N'COLUMN', N'ContactEmail'
GO
IF EXISTS(SELECT * FROM dbo.TicketPromoterEvent)
	 EXEC('INSERT INTO dbo.Tmp_TicketPromoterEvent (PromoterK, EventK, TotalTickets, SoldTickets, TotalFunds, FundsLockManual, FundsLockManualUsrK, FundsLockManualDateTime, FundsLockManualNote, FundsLockFraudIpDuplicate, FundsLockFraudIpCountry, FundsLockFraudGuid, FundsLockUsrResponses, FundsLockText, FundsLockOverride, FundsLockOverrideUsrK, FundsLockOverrideDateTime, FundsLockOverrideNote, FundsReleased, FundsTransferK, CancelledTickets, FundsLockTotalFundsDontMatch, TotalVat, TotalBookingFees, ContactEmail)
		SELECT PromoterK, EventK, TotalTickets, SoldTickets, CONVERT(decimal(18, 2), TotalFunds), FundsLockManual, FundsLockManualUsrK, FundsLockManualDateTime, FundsLockManualNote, FundsLockFraudIpDuplicate, FundsLockFraudIpCountry, FundsLockFraudGuid, FundsLockUsrResponses, FundsLockText, FundsLockOverride, FundsLockOverrideUsrK, FundsLockOverrideDateTime, FundsLockOverrideNote, FundsReleased, FundsTransferK, CancelledTickets, FundsLockTotalFundsDontMatch, CONVERT(decimal(18, 2), TotalVat), CONVERT(decimal(18, 2), TotalBookingFees), ContactEmail FROM dbo.TicketPromoterEvent WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.TicketPromoterEvent
GO
EXECUTE sp_rename N'dbo.Tmp_TicketPromoterEvent', N'TicketPromoterEvent', 'OBJECT' 
GO
ALTER TABLE dbo.TicketPromoterEvent ADD CONSTRAINT
	PK_TicketPromoterEvent PRIMARY KEY CLUSTERED 
	(
	PromoterK,
	EventK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
































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
CREATE TABLE dbo.Tmp_GuestlistCredit
	(
	K int NOT NULL IDENTITY (1, 1),
	PromoterK int NULL,
	DateTimeCreated datetime NULL,
	Credits int NULL,
	TotalPrice decimal(18, 2) NULL,
	Done bit NULL,
	DateTimeDone datetime NULL,
	BuyableLockDateTime datetime NULL
	)  ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'Guestlist credit top-up items'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'The primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'PromoterK'
SET @v = N'Link to the promoter table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'PromoterK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'DateTimeCreated'
SET @v = N'DateTime the credit request was created'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'DateTimeCreated'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'Credits'
SET @v = N'Number of credits bought'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'Credits'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'TotalPrice'
SET @v = N'Total price charged'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'TotalPrice'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'Done'
SET @v = N'Has the confirmation been received from paypal?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'Done'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'DateTimeDone'
SET @v = N'Has the confirmation been received from paypal?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'DateTimeDone'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'BuyableLockDateTime'
SET @v = N'Time stamp to record when someone is trying to purchase an IBuyable item that is linked to this Bob. No lock = DateTime.MinValue'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_GuestlistCredit', N'COLUMN', N'BuyableLockDateTime'
GO
SET IDENTITY_INSERT dbo.Tmp_GuestlistCredit ON
GO
IF EXISTS(SELECT * FROM dbo.GuestlistCredit)
	 EXEC('INSERT INTO dbo.Tmp_GuestlistCredit (K, PromoterK, DateTimeCreated, Credits, TotalPrice, Done, DateTimeDone, BuyableLockDateTime)
		SELECT K, PromoterK, DateTimeCreated, Credits, CONVERT(decimal(18, 2), TotalPrice), Done, DateTimeDone, BuyableLockDateTime FROM dbo.GuestlistCredit WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_GuestlistCredit OFF
GO
DROP TABLE dbo.GuestlistCredit
GO
EXECUTE sp_rename N'dbo.Tmp_GuestlistCredit', N'GuestlistCredit', 'OBJECT' 
GO
ALTER TABLE dbo.GuestlistCredit ADD CONSTRAINT
	PK_GuestlistCredit PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
if exists(select * from sys.assemblies where name = 'CacheTriggers')
begin
	exec('CREATE TRIGGER dbo.GuestlistCreditTrigger ON dbo.GuestlistCredit 
	AFTER UPDATE , DELETE , INSERT 
	AS 
	 EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].GuestlistCreditTrigger')
end
GO
COMMIT
