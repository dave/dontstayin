IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'IncomingSms' 
	AND	[column].name = 'PostData'
) BEGIN

ALTER TABLE dbo.IncomingSms ADD
	PostData text NULL
	
DECLARE @v sql_variant 
SET @v = N'The xml data received from the gateway'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'IncomingSms', N'COLUMN', N'PostData'

END
