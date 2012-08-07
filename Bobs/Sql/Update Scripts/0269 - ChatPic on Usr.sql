IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'ChatPic'
) BEGIN

ALTER TABLE dbo.Usr ADD
	ChatPic uniqueidentifier NULL,
	ChatPicPhotoK int NULL,
	ChatPicState varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Guid of the chat pic (300px x 100px)', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'ChatPic'
EXECUTE sp_addextendedproperty N'MS_Description', N'PhotoK that the chat pic was cropped from', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'ChatPicPhotoK'
EXECUTE sp_addextendedproperty N'MS_Description', N'State of the cropper for the chat pic', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'ChatPicState'

END

