
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'BouncePeriodDateTime'
) BEGIN

ALTER TABLE dbo.Usr ADD
	BouncePeriodDateTime datetime NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Date/time of the start of the month that bounce mails are being counted', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'BouncePeriodDateTime'

END






IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'TotalEmailsSentInPeriod'
) BEGIN

ALTER TABLE dbo.Usr ADD
	TotalEmailsSentInPeriod int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Total emails sent in the bounce mail period', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'TotalEmailsSentInPeriod'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'MatchedHardBounceInPeriod'
) BEGIN

ALTER TABLE dbo.Usr ADD
	MatchedHardBounceInPeriod int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Total hard bounces with a matching string detected in the bounce period', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'MatchedHardBounceInPeriod'

END

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'UnmatchedHardBounceInPeriod'
) BEGIN

ALTER TABLE dbo.Usr ADD
	UnmatchedHardBounceInPeriod int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Total hard bounces without a matching string detected in the bounce period', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'UnmatchedHardBounceInPeriod'

END

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'SoftBounceInPeriod'
) BEGIN

ALTER TABLE dbo.Usr ADD
	SoftBounceInPeriod int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Total soft bounces detected in the bounce period', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'SoftBounceInPeriod'

END
