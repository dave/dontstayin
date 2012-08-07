IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagIssue' 
	AND	[column].name = 'CoverImageUrl'
) BEGIN

ALTER TABLE dbo.MixmagIssue ADD
	CoverImageUrl varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Url of an image of the cover. MUST be 194px x 254px.', N'SCHEMA', N'dbo', N'TABLE', N'MixmagIssue', N'COLUMN', N'CoverImageUrl'

END
