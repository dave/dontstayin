


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'InterviewImageUrl'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	InterviewImageUrl varchar(200) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Image of the interviewer', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'InterviewImageUrl'

END

