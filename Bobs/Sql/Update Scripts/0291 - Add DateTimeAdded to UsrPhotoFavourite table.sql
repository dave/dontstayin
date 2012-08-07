IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'UsrPhotoFavourite' 
	AND	[column].name = 'DateTimeAdded'
) BEGIN

ALTER TABLE dbo.UsrPhotoFavourite ADD
	DateTimeAdded datetime NULL
	
DECLARE @v sql_variant 
SET @v = N'When was this favourite added?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'UsrPhotoFavourite', N'COLUMN', N'DateTimeAdded'

END
