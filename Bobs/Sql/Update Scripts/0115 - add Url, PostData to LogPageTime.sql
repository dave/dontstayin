DECLARE @v sql_variant 

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'LogPageTime' 
	AND	[column].name = 'Url'
) BEGIN

ALTER TABLE dbo.LogPageTime ADD Url varchar(150) NULL
ALTER TABLE dbo.LogPageTime ADD PostData varchar(4000) NULL

EXECUTE sp_addextendedproperty N'MS_Description', 'The Current Url', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'Url'
EXECUTE sp_addextendedproperty N'MS_Description', 'Post data of Request, if Request exceeded certain duration', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'PostData'

END

GO
