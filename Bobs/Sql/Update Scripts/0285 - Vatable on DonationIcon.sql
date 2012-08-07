IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'DonationIcon' 
	AND	[column].name = 'Vatable'
) BEGIN

ALTER TABLE dbo.DonationIcon ADD Vatable bit NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this icon a vatable item?', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'Vatable'

END

