IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'DonationIcon' 
	AND	[column].name = 'Enabled'
) BEGIN

ALTER TABLE dbo.DonationIcon ADD Enabled bit NOT NULL default 0
	
DECLARE @v sql_variant 
SET @v = N'Is this DonationIcon ready to go live from the StartDate?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'Enabled'

-- it is stupid that we need this. Must fix the generator. But later.
EXECUTE sp_addextendedproperty N'IsNotNull', 'true', N'SCHEMA', N'dbo', N'TABLE', N'DonationIcon', N'COLUMN', N'StartDateTime'

END

go

Update DonationIcon set Enabled = 1

