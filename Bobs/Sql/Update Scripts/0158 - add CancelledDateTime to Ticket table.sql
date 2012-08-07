
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Ticket' 
	AND	[column].name = 'CancelledDateTime'
) BEGIN
	ALTER TABLE dbo.Ticket ADD
	CancelledDateTime datetime NULL

	DECLARE @v sql_variant 
	SET @v = N'Date / time that the ticket was cancelled / refunded'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'CancelledDateTime'

END

 


