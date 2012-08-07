IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'ChatMessage' 
	AND	[column].name = 'Deleted'
) BEGIN

ALTER TABLE dbo.ChatMessage ADD
	Deleted bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Has this item been deleted from the archive?', N'SCHEMA', N'dbo', N'TABLE', N'ChatMessage', N'COLUMN', N'Deleted'

END




