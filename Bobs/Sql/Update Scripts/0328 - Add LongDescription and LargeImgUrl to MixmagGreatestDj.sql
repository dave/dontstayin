
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'LongDescription'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	LongDescription text NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Long description', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'LongDescription'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'ShortDescription'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	ShortDescription text NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Short description', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'ShortDescription'

END





IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'LargeImageUrl'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	LargeImageUrl varchar(200) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Large image - should be 200px square', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'LargeImageUrl'

END

