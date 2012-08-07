
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'LegalTermsUser2'
) BEGIN
	ALTER TABLE dbo.Usr ADD
	LegalTermsUser2 bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Has the user agreed to the new (2007-06) user terms?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'LegalTermsUser2'

END

 


