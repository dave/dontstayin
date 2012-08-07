
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Promoter' 
	AND	[column].name = 'AddedMethod'
) BEGIN
	ALTER TABLE dbo.Promoter ADD
	AddedMethod int NULL

	DECLARE @v sql_variant 
	SET @v = N'How was this promoter added to the site (1=By end user on the site, 2=By sales user in the backend, 3=By automated import)'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'AddedMethod'

END

