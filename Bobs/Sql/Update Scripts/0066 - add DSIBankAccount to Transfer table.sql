IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Transfer' 
	AND	[column].name = 'DSIBankAccount'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Transfer ADD
	DSIBankAccount int NULL
	
DECLARE @v sql_variant 
SET @v = N'Which DSI bank account was used in this transfer. DSI Current account = 1, DSI Client account = 2'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'DSIBankAccount'

END
