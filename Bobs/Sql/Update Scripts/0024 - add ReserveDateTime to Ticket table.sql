IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Ticket' 
	AND	[column].name = 'ReserveDateTime'
) BEGIN
	ALTER TABLE dbo.Ticket ADD
	ReserveDateTime datetime NULL

	DECLARE @v sql_variant 
	SET @v = N'Date time til when a pending ticket is reserved until'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'ReserveDateTime'

END
