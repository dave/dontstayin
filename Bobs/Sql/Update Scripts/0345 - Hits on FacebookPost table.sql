
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'FacebookPost' 
	AND	[column].name = 'Hits'
) BEGIN

ALTER TABLE dbo.FacebookPost ADD Hits int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Total number of hits', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'Hits'

END

