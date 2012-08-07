IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'DateTimeLastMessageSent'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	DateTimeLastMessageSent datetime NULL
	
DECLARE @v sql_variant 
SET @v = N'When was the last activity?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'DateTimeLastMessageSent'

END
