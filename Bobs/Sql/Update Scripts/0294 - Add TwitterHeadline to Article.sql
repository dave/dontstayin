IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Article' 
	AND	[column].name = 'TwitterHeadline'
) BEGIN

ALTER TABLE dbo.Article ADD
	TwitterHeadline varchar(140) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Headline of the article for sending to Twitter', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'TwitterHeadline'

END

