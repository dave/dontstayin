
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Buddy' 
	AND	[column].name = 'Denied'
) BEGIN
	ALTER TABLE dbo.Buddy ADD
	Denied bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Has this buddy request been denied?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Buddy', N'COLUMN', N'Denied'

END

go

update buddy set Denied = cast(0 as bit)


