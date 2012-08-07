--DateTimeLastRead, TotalReads, DateTimeLastStoryPublished

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'FirstName'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	FirstName varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'First name', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'FirstName'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'LastName'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	LastName varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Last name', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'LastName'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'AddressFirstLine'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	AddressFirstLine varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'First line of the postal address.', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'AddressFirstLine'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'AddressPostCode'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	AddressPostCode varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Postal code.', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'AddressPostCode'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'AddressCountryK'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	AddressCountryK int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Country', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'AddressCountryK'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'IsAddressComplete'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	IsAddressComplete bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Has the name and address been completed', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'IsAddressComplete'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'IsEmailVerified'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	IsEmailVerified bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is the email verified', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'IsEmailVerified'

END
