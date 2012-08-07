IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'DonationIcon' 
	AND	[column].name = 'Description'
) BEGIN

ALTER TABLE dbo.DonationIcon ADD Description text NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Not used', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'Description'

END

