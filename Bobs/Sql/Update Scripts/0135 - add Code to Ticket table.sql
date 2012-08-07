IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Ticket' 
	AND	[column].name = 'Code'
) BEGIN
	ALTER TABLE dbo.Ticket ADD
	Code varchar(4) NULL

	DECLARE @v sql_variant 
	SET @v = N'Random code generated for the ticket'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'Code'

END
