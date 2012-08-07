IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'PhotoUsage'
) BEGIN

ALTER TABLE dbo.Usr ADD PhotoUsage int NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Photo usage permission', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'PhotoUsage'
EXECUTE sp_addextendedproperty N'EnumProperty', N'PhotoUsageEnum', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'PhotoUsage'

END
