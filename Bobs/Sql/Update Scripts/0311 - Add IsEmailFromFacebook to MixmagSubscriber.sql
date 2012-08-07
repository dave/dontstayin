
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'IsEmailFromFacebook'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	IsEmailFromFacebook bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this email address from Facebook?', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'IsEmailFromFacebook'

END

