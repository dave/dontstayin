
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Article' 
	AND	[column].name = 'ShowAboveFoldOnFrontPage'
) BEGIN

ALTER TABLE dbo.Article ADD ShowAboveFoldOnFrontPage bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Do we show this article above the fold on the front page?', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'ShowAboveFoldOnFrontPage'

END

