


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'YoutubeId2'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	YoutubeId2 varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Second youtube clip', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'YoutubeId2'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'TotalVotes'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	TotalVotes int NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Total votes', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'TotalVotes'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestDj' 
	AND	[column].name = 'IsPublicNominated'
) BEGIN

ALTER TABLE dbo.MixmagGreatestDj ADD
	IsPublicNominated bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this in the public nominated section?', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestDj', N'COLUMN', N'IsPublicNominated'

END


