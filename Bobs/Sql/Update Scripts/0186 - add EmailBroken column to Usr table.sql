IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'IsEmailBroken'
) BEGIN

ALTER TABLE dbo.Usr ADD
	IsEmailBroken bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Did a recent email to this user suffer a hard bounce?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'IsEmailBroken'

END
