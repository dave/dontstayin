
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'PromotersOnly'
) BEGIN
	ALTER TABLE dbo.Flyer ADD PromotersOnly bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Is eFlyer only to be sent to Usrs with IsPromoter true?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'PromotersOnly'

END

