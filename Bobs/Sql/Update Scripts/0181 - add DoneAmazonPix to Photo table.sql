IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'DoneAmazonPix'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Photo ADD
	DoneAmazonPix bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Have the Pix files been uploaded to Amazon?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'DoneAmazonPix'

END

