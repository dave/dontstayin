IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Comment' 
	AND	[column].name = 'ChatItemGuid'
) BEGIN

ALTER TABLE dbo.Comment ADD
	ChatItemGuid uniqueidentifier NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The guid of the main chat item (for the archive)', N'SCHEMA', N'dbo', N'TABLE', N'Comment', N'COLUMN', N'ChatItemGuid'

END





IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'ChatMessage' 
	AND	[column].name = 'ChatItemGuid'
) BEGIN

ALTER TABLE dbo.ChatMessage ADD
	ChatItemGuid uniqueidentifier NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The guid of the main chat item (for the archive)', N'SCHEMA', N'dbo', N'TABLE', N'ChatMessage', N'COLUMN', N'ChatItemGuid'

END




