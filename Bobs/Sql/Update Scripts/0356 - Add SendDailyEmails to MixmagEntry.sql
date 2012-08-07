
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagEntry' 
	AND	[column].name = 'SendDailyEmails'
) BEGIN

ALTER TABLE dbo.MixmagEntry ADD
	SendDailyEmails bit NOT NULL default 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Send daily vote update emails?', N'SCHEMA', N'dbo', N'TABLE', N'MixmagEntry', N'COLUMN', N'SendDailyEmails'

END

