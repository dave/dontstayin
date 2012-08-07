IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'TotalRequiredImpressions'
) BEGIN

ALTER TABLE dbo.Banner DROP COLUMN TotalRequiredImpressions

END

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'TotalRequiredImpressions'
) BEGIN

ALTER TABLE dbo.Banner ADD TotalRequiredImpressions int
EXECUTE sp_addextendedproperty N'MS_Description', 'Total impressions required for this banner campaign', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalRequiredImpressions'


END


alter table SpottedException alter column Cookies varchar(4000)

