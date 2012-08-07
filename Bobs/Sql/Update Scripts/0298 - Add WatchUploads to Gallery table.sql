IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Gallery' 
	AND	[column].name = 'WatchUploads'
) BEGIN

ALTER TABLE dbo.Gallery ADD
	WatchUploads bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Watch uploads for comments? (default = true)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Gallery', N'COLUMN', N'WatchUploads'

END
