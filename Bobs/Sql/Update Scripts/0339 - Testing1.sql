
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Phone' 
	AND	[column].name = 'TestColumn1'
) BEGIN

ALTER TABLE dbo.Phone ADD TestColumn1 varchar(100) NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Test column 1 - not used', N'SCHEMA', N'dbo', N'TABLE', N'Phone', N'COLUMN', N'TestColumn1'

END
