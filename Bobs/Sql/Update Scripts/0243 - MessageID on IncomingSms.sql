IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'IncomingSms' 
	AND	[column].name = 'MessageID'
) BEGIN

ALTER TABLE dbo.IncomingSms ADD
	MessageID varchar(64) NULL
	
DECLARE @v sql_variant 
SET @v = N'The message_id received from the gateway'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'IncomingSms', N'COLUMN', N'MessageID'

END
