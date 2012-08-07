
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookEmail'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookEmail varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook email', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookEmail'

END


