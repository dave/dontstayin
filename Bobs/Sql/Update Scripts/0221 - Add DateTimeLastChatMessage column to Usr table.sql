IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'DateTimeLastChatMessage'
) BEGIN

ALTER TABLE dbo.Usr ADD
	DateTimeLastChatMessage datetime NULL
	
DECLARE @v sql_variant 
SET @v = N'When was the last chat message posted?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'DateTimeLastChatMessage'

END
