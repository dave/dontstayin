IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'IsDj'
) BEGIN

ALTER TABLE dbo.Usr ADD IsDj bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this user a DJ?', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'IsDj'

END
