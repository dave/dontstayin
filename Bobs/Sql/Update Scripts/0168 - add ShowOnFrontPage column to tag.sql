IF NOT EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'Tag' and c.Name='ShowInTagCloud') BEGIN

	ALTER TABLE dbo.Tag ADD
		ShowInTagCloud bit NOT NULL CONSTRAINT DF_Tag_ShowInTagCloud DEFAULT 1

	EXECUTE sp_addextendedproperty N'MS_Description', N'should this be shown in the tag cloud', N'SCHEMA', N'dbo', N'TABLE', N'Tag', N'COLUMN', N'ShowInTagCloud'
	EXECUTE sp_addextendedproperty N'CausesInvalidation', N'true', N'SCHEMA', N'dbo', N'TABLE', N'Tag', N'COLUMN', N'ShowInTagCloud'
END



