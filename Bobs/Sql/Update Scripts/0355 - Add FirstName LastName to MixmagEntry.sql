


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagEntry' 
	AND	[column].name = 'FirstName'
) BEGIN

ALTER TABLE dbo.MixmagEntry ADD
	FirstName varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'First name', N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'FirstName'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagEntry' 
	AND	[column].name = 'LastName'
) BEGIN

ALTER TABLE dbo.MixmagEntry ADD
	LastName varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Last name', N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'LastName'

END
