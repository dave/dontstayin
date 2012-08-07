IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'BlockedFromPhotoOfWeekUser'
) BEGIN

ALTER TABLE dbo.Photo ADD BlockedFromPhotoOfWeekUser bit NULL

EXECUTE sp_addextendedproperty N'MS_Description', N'Has this photo been blocked from being User Photo of the week?', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'BlockedFromPhotoOfWeekUser'

END

