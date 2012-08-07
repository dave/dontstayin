


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'StealthMode'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	StealthMode bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Stealth mode for wildly unpopular acts', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'StealthMode'

END

