IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'RoomPin' 
	AND	[column].name = 'Starred'
) BEGIN

ALTER TABLE dbo.RoomPin ADD
	Starred bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Is the room starred?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'Starred'

END
