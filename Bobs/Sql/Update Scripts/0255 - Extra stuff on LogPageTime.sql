IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'LogPageTime' 
	AND	[column].name = 'IsCrawler'
) BEGIN

ALTER TABLE dbo.LogPageTime ADD
	IsCrawler bit NULL,
	IsAjaxRequest bit NULL,
	IsRendered bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Is the browser a crawler?', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'IsCrawler'
EXECUTE sp_addextendedproperty N'MS_Description', N'Is this an AJAX request?', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'IsAjaxRequest'
EXECUTE sp_addextendedproperty N'MS_Description', N'Has Page.Render fired?', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'IsRendered'

END





