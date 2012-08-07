
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Event' 
	AND	[column].name = 'SpotterRequest'
) BEGIN

ALTER TABLE dbo.Event ADD
	SpotterRequest bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', 'Display the spotter request panel?', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'SpotterRequest'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Event' 
	AND	[column].name = 'SpotterRequestName'
) BEGIN

ALTER TABLE dbo.Event ADD
	SpotterRequestName varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', 'Name for spotter request panel', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'SpotterRequestName'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Event' 
	AND	[column].name = 'SpotterRequestNumber'
) BEGIN

ALTER TABLE dbo.Event ADD
	SpotterRequestNumber varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', 'Number for spotter request panel', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'SpotterRequestNumber'

END
