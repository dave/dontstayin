IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Visit' 
	AND	[column].name = 'UserAgent'
) BEGIN

	ALTER TABLE dbo.Visit ADD UserAgent varchar(400) NULL
		
	DECLARE @v sql_variant 
	SET @v = N'The browser''s UserAgent string'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Visit', N'COLUMN', N'UserAgent'





END











