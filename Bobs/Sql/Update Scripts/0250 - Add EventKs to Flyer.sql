IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'EventKs'
) BEGIN

ALTER TABLE dbo.Flyer ADD EventKs varchar(MAX) NULL
	
DECLARE @v sql_variant 
SET @v = N'Comma-separated list of EventKs, Usrs Attended of which to target this Flyer to'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'EventKs'

END
