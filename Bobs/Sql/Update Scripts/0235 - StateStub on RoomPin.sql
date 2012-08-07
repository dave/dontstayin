IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'RoomPin' 
	AND	[column].name = 'StateStub'
) BEGIN

ALTER TABLE dbo.RoomPin ADD
	StateStub varchar(4096) NULL
	
DECLARE @v sql_variant 
SET @v = N'The persisted room state'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'StateStub'

END
