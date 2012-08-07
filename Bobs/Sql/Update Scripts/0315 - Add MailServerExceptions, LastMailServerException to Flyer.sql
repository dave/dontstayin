
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'MailServerRetries'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	MailServerRetries int NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'How many retries due to mail server out of space?', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'MailServerRetries'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'MailServerLastRetry'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	MailServerLastRetry datetime NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'When was the last mail server retry?', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'MailServerLastRetry'

END
