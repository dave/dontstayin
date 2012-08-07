IF NOT EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Usr' and c.Name='ExDirectory') BEGIN

	ALTER TABLE dbo.Usr ADD ExDirectory bit
	EXECUTE sp_addextendedproperty N'MS_Description', N'Disallow this usr''s profile to be found by searching name or email address?', 
		N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'ExDirectory'
END


IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Buddy' and c.Name='UsrFoundByMethod') BEGIN
	alter table dbo.Buddy drop column UsrFoundByMethod
END


IF NOT EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Buddy' and c.Name='BuddyFoundByMethod') BEGIN

	ALTER TABLE dbo.Buddy ADD BuddyFoundByMethod int
	EXECUTE sp_addextendedproperty N'MS_Description', N'0 = Nickname, 1 = Real Name, 2 = Email Address, 3 = Spotter Number', 
		N'SCHEMA', N'dbo', N'TABLE', N'Buddy', N'COLUMN', N'BuddyFoundByMethod'
END








