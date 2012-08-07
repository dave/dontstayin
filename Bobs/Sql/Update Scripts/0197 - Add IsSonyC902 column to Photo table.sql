IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'IsSonyC902'
) BEGIN

ALTER TABLE dbo.Photo ADD
	IsSonyC902 bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Was this photo taken with the C902?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'IsSonyC902'

END
