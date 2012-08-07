IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'PassedCaptcha'
) BEGIN

ALTER TABLE dbo.Usr ADD PassedCaptcha bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Has this user correctly passed the captcha test?', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'PassedCaptcha'

END
