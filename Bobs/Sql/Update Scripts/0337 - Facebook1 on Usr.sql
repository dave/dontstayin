IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookStory1'
) BEGIN

ALTER TABLE dbo.Usr ADD FacebookStory1 bit NOT NULL DEFAULT 1 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Main Facebook Story permission - Well update your Facebook wall when you create stuff', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStory1'

END
