IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'FacebookPost' 
	AND	[column].name = 'DataInt'
) BEGIN

ALTER TABLE dbo.FacebookPost ADD DataInt int NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Int data used for de-duplicates', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'DataInt'

END

