IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'DuplicateGuid'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Banner ADD
	DuplicateGuid uniqueidentifier NULL
	
DECLARE @v sql_variant 
SET @v = N'Duplicate guid used to prevent duplicates while adding'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'DuplicateGuid'

END
