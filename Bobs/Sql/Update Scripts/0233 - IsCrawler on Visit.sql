IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Visit' 
	AND	[column].name = 'IsCrawler'
) BEGIN

ALTER TABLE dbo.Visit DROP COLUMN IsCrawler

END

ALTER TABLE dbo.Visit ADD
	IsCrawler bit NULL
	
DECLARE @v sql_variant 
SET @v = N'Is this visit from a Crawler bot'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Visit', N'COLUMN', N'IsCrawler'

EXEC sys.sp_addextendedproperty @name=N'IsNotNull', @value=N'true' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Visit', @level2type=N'COLUMN',@level2name=N'IsCrawler'

