IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagIssue' 
	AND	[column].name = 'ContentsData'
) BEGIN

ALTER TABLE dbo.MixmagIssue ADD
	ContentsData text NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Data used to show the contents.', N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'ContentsData'

END
