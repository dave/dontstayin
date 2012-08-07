
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagIssue' 
	AND	[column].name = 'IssueCoverId'
) BEGIN

ALTER TABLE dbo.MixmagIssue ADD
	IssueCoverId int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'If there are multiple covers, this is the ID', N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'IssueCoverId'

END

