
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'MixmagSubscription' 
	AND	[column].name = 'EmailVerificationSecret'
) BEGIN

ALTER TABLE dbo.MixmagSubscription ADD
	EmailVerificationSecret varchar(10) NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Email verification secret', N'SCHEMA', N'dbo', N'TABLE', N'MixmagSubscription', N'COLUMN', N'EmailVerificationSecret'

END

