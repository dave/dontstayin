IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Event' 
	AND	[column].name = 'FacebookEventId'
) BEGIN

ALTER TABLE dbo.[Event] ADD FacebookEventId bigint NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook event id', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'FacebookEventId'

END
