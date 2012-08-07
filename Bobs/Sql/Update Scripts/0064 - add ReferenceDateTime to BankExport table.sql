IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'BankExport' 
	AND	[column].name = 'ReferenceDateTime'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.BankExport ADD
	ReferenceDateTime datetime NULL
	
DECLARE @v sql_variant 
SET @v = N'Date of transaction that it is referencing'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BankExport', N'COLUMN', N'ReferenceDateTime'

END
