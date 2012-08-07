IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'LogPageTime' 
	AND	[column].name = 'IsGet'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.LogPageTime ADD
	IsGet bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Is page request a GET or POST. GET = true, POST = false'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'IsGet'

END
