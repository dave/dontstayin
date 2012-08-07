IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Ticket' 
	AND	[column].name = 'IsFraud'
) BEGIN

ALTER TABLE dbo.Ticket ADD
	IsFraud bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Is this ticket suspected fraud?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Ticket', N'COLUMN', N'IsFraud'

END
