IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'DonationIcon' 
	AND	[column].name = 'Charity'
) BEGIN

ALTER TABLE dbo.DonationIcon ADD Charity bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this icon a charity donation?', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'Charity'

END





--IF EXISTS(
--	SELECT * FROM DonationIcon WHERE K=25
--)
--
--BEGIN
--
--UPDATE DonationIcon SET Charity=1 WHERE K=25
--
--END
