IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookAccessToken'
) BEGIN

ALTER TABLE dbo.Usr ADD FacebookAccessToken varchar(128) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Access token needed for offline access', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookAccessToken'

END
