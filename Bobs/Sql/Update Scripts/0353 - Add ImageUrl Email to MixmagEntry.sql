
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagEntry' 
	AND	[column].name = 'ImageUrl'
) BEGIN

ALTER TABLE dbo.MixmagEntry ADD
	ImageUrl varchar(500) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Url of the image entered', N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'ImageUrl'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagEntry' 
	AND	[column].name = 'Email'
) BEGIN

ALTER TABLE dbo.MixmagEntry ADD
	Email varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Email', N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'Email'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagEntry' 
	AND	[column].name = 'Name'
) BEGIN

ALTER TABLE dbo.MixmagEntry ADD
	Name varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Name', N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'Name'

END
