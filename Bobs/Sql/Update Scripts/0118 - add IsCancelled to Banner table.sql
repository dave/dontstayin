
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'IsCancelled'
) BEGIN
	ALTER TABLE dbo.Banner ADD
	IsCancelled bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Has this banner been cancelled?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'IsCancelled'

END

--update Banner set IsCancelled = 1 where 


