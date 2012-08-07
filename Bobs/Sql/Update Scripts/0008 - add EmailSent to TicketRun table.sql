IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'TicketRun' 
	AND	[column].name = 'EmailSent'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.TicketRun ADD
	EmailSent bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Bit flag to note when email has been sent to promoter after ticket run has ended'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'TicketRun', N'COLUMN', N'EmailSent'

END
