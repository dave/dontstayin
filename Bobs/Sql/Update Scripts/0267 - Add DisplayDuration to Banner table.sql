IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'DisplayDuration'
) BEGIN

ALTER TABLE dbo.Banner ADD
	DisplayDuration INT NULL
	
DECLARE @v sql_variant 
SET @v = N'Number of seconds to display banner for when rotating banners. null indicates that the default should be used'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'DisplayDuration'

END


