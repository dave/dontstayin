IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'FrontPagePic'
) BEGIN

ALTER TABLE dbo.Photo ADD
	FrontPagePic uniqueidentifier NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Pic for the front page (600 x 250) image.', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'FrontPagePic'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'FrontPagePicState'
) BEGIN

ALTER TABLE dbo.Photo ADD
	FrontPagePicState varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Cropper state for the front page pic.', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'FrontPagePicState'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'FrontPageCaptionClass'
) BEGIN

ALTER TABLE dbo.Photo ADD
	FrontPageCaptionClass varchar(50) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'CSS class for the front page caption - for colour, alignment etc.', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'FrontPageCaptionClass'

END

