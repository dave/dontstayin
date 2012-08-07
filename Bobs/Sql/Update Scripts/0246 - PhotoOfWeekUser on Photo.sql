IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Photo' 
	AND	[column].name = 'PhotoOfWeekUser'
) BEGIN

ALTER TABLE dbo.Photo ADD
	PhotoOfWeekUser bit NULL,
	PhotoOfWeekUserCaption varchar(200) NULL,
	PhotoOfWeekUserDateTime datetime NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Photo of week selected by users', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'PhotoOfWeekUser'
EXECUTE sp_addextendedproperty N'MS_Description', N'Photo of week selected by users caption', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'PhotoOfWeekUserCaption'
EXECUTE sp_addextendedproperty N'MS_Description', N'Photo of week selected by users date time', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'PhotoOfWeekUserDateTime'

END
