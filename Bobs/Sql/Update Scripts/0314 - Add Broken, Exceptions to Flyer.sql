
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'Broken'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	Broken int NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'How many emasils were skipped due to broken emails?', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Broken'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'Exceptions'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	Exceptions int NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'How many emasils were skipped due to exceptions?', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Exceptions'

END
