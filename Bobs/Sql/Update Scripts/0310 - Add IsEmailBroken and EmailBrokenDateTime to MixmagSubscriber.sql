
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'IsEmailBroken'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	IsEmailBroken bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Have we received a bounce message for this email?', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'IsEmailBroken'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'EmailBrokenDateTime'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	EmailBrokenDateTime datetime NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'When did we receive the bounce?', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'EmailBrokenDateTime'

END


