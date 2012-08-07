IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Event' 
	AND	[column].name = 'DontShowHotelLink'
) BEGIN

ALTER TABLE dbo.Event ADD DontShowHotelLink bit NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Exclude this event from showing "Find Hotel" banners etc', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'DontShowHotelLink'

END


