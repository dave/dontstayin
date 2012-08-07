IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Article' 
	AND	[column].name = 'IsMixmagNews'
) BEGIN

ALTER TABLE dbo.Article ADD
	IsMixmagNews bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this article displayed in the Mixmag news section?', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'IsMixmagNews'

END

