
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Domain' 
	AND	[column].name = 'RedirectApp'
) BEGIN
	ALTER TABLE dbo.Domain ADD RedirectApp varchar(50) NULL

	DECLARE @v sql_variant 
	SET @v = N'Site Application to invoke, in combination with RedirectObjectK and RedirectObjectType where relevant'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Domain', N'COLUMN', N'RedirectApp'

END

go

DELETE FROM Domain where DomainName in ('yougotspotted.com', 'dont-stay-in.com') -- we don't need these in the list



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Visit' 
	AND	[column].name = 'DomainK'
) BEGIN
	ALTER TABLE dbo.Visit ADD DomainK int NULL

	DECLARE @v sql_variant 
	SET @v = N'Domain from which this request originated'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Visit', N'COLUMN', N'DomainK'

END

go




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Ticket' 
	AND	[column].name = 'DomainK'
) BEGIN
	ALTER TABLE dbo.Ticket ADD DomainK int NULL

	DECLARE @v sql_variant 
	SET @v = N'Domain from which the request originated'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'DomainK'

END

go


