
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Phone' 
	AND	[column].name = 'TestColumn2'
) BEGIN

ALTER TABLE dbo.Phone ADD TestColumn2 varchar(100) NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Test column 2 - not used', N'SCHEMA', N'dbo', N'TABLE', N'Phone', N'COLUMN', N'TestColumn2'

END
