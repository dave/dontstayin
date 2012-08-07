IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'Rotate'
) BEGIN
	ALTER TABLE dbo.Photo ADD Rotate int
	EXECUTE sp_addextendedproperty N'MS_Description', N'Rotation transformation when the photo was uploaded', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'Rotate'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'UploadTemporary'
) BEGIN
	ALTER TABLE dbo.Photo ADD UploadTemporary uniqueidentifier
	EXECUTE sp_addextendedproperty N'MS_Description', N'Location of the temporary uploaded file (in PixMaster)', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'UploadTemporary'

END
