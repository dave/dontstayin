
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'Email'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	Email varchar(100) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Email address', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'Email'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'IsEmailComplete'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	IsEmailComplete bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Has the email been provided', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'IsEmailComplete'

END

