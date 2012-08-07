
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'LegalTermsPromoter2'
) BEGIN
	ALTER TABLE dbo.Usr ADD
	LegalTermsPromoter2 bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Has the user agreed to the new (2007-06) promoter terms?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'LegalTermsPromoter2'

END
