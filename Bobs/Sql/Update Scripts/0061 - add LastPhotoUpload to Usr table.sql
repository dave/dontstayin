IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'LastPhotoUpload'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Usr ADD
	LastPhotoUpload datetime NULL
	
DECLARE @v1 sql_variant 
SET @v1 = N'Datetime of this users last photo upload'
EXECUTE sp_addextendedproperty N'MS_Description', @v1, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'LastPhotoUpload'
END
GO
update Usr set LastPhotoUpload = (select MAX(ParentDateTime) from Photo Where Photo.UsrK = Usr.K) where TotalPhotoUploads>0



