IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'IsInCaptionCompetition'
) BEGIN
	 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.Photo ADD
	IsInCaptionCompetition bit NULL
	
DECLARE @v1 sql_variant 
SET @v1 = N'Is this thread in a caption competition?'
EXECUTE sp_addextendedproperty N'MS_Description', @v1, N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'IsInCaptionCompetition'

END