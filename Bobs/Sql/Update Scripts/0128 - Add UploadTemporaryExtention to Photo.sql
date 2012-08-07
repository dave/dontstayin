IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'UploadTemporaryExtention'
) BEGIN
	ALTER TABLE dbo.Photo ADD UploadTemporaryExtention varchar(10)
	EXECUTE sp_addextendedproperty N'MS_Description', N'Extention of the temporary uploaded file', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'UploadTemporaryExtention'

END
