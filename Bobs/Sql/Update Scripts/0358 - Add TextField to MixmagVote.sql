


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagVote' 
	AND	[column].name = 'TextField1'
) BEGIN

ALTER TABLE dbo.MixmagVote ADD
	TextField1 varchar(255) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Custom text field', N'SCHEMA', N'dbo', N'TABLE', N'MixmagVote', N'COLUMN', N'TextField1'

END


