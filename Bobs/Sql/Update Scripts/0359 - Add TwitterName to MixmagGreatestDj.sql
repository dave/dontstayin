


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'TwitterName'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	TwitterName varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Twitter name of the artist', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'TwitterName'

END


