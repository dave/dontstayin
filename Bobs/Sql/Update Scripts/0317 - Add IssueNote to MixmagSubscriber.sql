
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagIssue' 
	AND	[column].name = 'IssueNote'
) BEGIN

ALTER TABLE dbo.MixmagIssue ADD
	IssueNote varchar(1000) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Note shown under the link on the listings page', N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'IssueNote'

END

