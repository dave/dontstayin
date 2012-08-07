IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Gallery' 
	AND	[column].name = 'RunFinishedUploadingTask'
) BEGIN

ALTER TABLE dbo.Gallery ADD RunFinishedUploadingTask bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Has the FinishedUploading task run on this gallery?', N'SCHEMA', N'dbo', N'TABLE', N'Gallery', N'COLUMN', N'RunFinishedUploadingTask'

END

