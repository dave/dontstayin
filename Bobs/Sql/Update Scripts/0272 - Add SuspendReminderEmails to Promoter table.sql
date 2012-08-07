IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Promoter' 
	AND	[column].name = 'SuspendReminderEmails'
) BEGIN

ALTER TABLE dbo.Promoter ADD
	SuspendReminderEmails BIT NOT NULL DEFAULT 0
	
DECLARE @v sql_variant 
SET @v = N'Dont send this promoter reminder emails when they havent paid'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'SuspendReminderEmails'

END


