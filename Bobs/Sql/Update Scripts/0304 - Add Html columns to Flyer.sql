
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'IsHtml'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	IsHtml bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this an HTML eFlyer?', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'IsHtml'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'Html'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	Html [text] NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Html to send for HTML eFlyers', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'Html'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'TextAlternative'
) BEGIN

ALTER TABLE dbo.Flyer ADD
	TextAlternative [text] NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Text to send as the alternative to HTML', N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'TextAlternative'

END
