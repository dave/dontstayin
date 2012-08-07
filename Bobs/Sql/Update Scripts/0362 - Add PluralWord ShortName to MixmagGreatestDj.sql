


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'PluralWord'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	PluralWord varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Plural word - is / are', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'PluralWord'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'ShortName'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	ShortName varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Name short version', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'ShortName'

END

