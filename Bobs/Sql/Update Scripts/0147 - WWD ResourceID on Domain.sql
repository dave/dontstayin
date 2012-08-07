
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Domain' 
	AND	[column].name = 'WwdResourceID'
) BEGIN
	ALTER TABLE dbo.Domain ADD WwdResourceID varchar(50) NULL

	DECLARE @v sql_variant 
	SET @v = N'The resource ID of this domain when registered with Wild West Domains, useful for automated domain renewal'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Domain', N'COLUMN', N'WwdResourceID'

END

go
