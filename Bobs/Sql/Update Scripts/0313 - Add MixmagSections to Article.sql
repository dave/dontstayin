IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Article' 
	AND	[column].name = 'MixmagSections'
) BEGIN

ALTER TABLE dbo.Article ADD
	MixmagSections int NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Which Mixmag sections is this article in?', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'MixmagSections'

END

