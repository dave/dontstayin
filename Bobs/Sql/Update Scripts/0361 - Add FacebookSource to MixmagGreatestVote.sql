


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagGreatestVote' 
	AND	[column].name = 'FacebookSource'
) BEGIN

ALTER TABLE dbo.MixmagGreatestVote ADD
	FacebookSource bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Did this vote come from a facebook wall post?', N'SCHEMA', N'dbo', N'TABLE', N'MixmagGreatestVote', N'COLUMN', N'FacebookSource'

END


