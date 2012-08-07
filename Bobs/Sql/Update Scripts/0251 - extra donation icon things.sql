IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'RolloverDonationIconK'
) BEGIN

ALTER TABLE dbo.Usr ADD RolloverDonationIconK int NULL
	
DECLARE @v sql_variant 
SET @v = N'The Donation Icon, if applicable, which the user will have appear in their rollover'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'RolloverDonationIconK'

END
