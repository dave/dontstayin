DECLARE @v sql_variant 

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Visit' 
	AND	[column].name = 'IsFromExternal'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Visit ADD
	IsFromExternal bit NULL
	

SET @v = N'Is this visit from an external source?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Visit', N'COLUMN', N'IsFromExternal'

END






IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Visit' 
	AND	[column].name = 'ExternalTag'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Visit ADD
	ExternalTag varchar(50) NULL
	

SET @v = N'External source tag'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Visit', N'COLUMN', N'ExternalTag'

END