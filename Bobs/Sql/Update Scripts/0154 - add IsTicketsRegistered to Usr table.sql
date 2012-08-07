
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'IsTicketsRegistered'
) BEGIN
	ALTER TABLE dbo.Usr ADD
	IsTicketsRegistered bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Has the user registered through a styled tickets microsite?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'IsTicketsRegistered'

END

 


