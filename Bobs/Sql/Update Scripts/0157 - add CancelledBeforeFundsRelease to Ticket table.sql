
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Ticket' 
	AND	[column].name = 'CancelledBeforeFundsRelease'
) BEGIN
	ALTER TABLE dbo.Ticket ADD
	CancelledBeforeFundsRelease bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Was this ticket cancelled / refunded before the promoter funds release event?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'CancelledBeforeFundsRelease'

END

 


