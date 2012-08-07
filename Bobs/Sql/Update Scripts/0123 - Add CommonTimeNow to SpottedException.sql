IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'SpottedException' 
	AND	[column].name = 'CommonTimeNow'
) BEGIN
	ALTER TABLE dbo.SpottedException ADD CommonTimeNow datetime
	EXECUTE sp_addextendedproperty N'MS_Description', N'Now according to current Common.Time context', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'CommonTimeNow'

END
