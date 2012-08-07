
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'FacebookPost' 
	AND	[column].name = 'FacebookUid'
) BEGIN

ALTER TABLE dbo.FacebookPost ADD
	FacebookUid bigint NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook user id', N'SCHEMA', N'dbo', N'TABLE', N'FacebookPost', N'COLUMN', N'FacebookUid'




CREATE NONCLUSTERED INDEX [IX_FacebookPost_Type_FacebookUid] ON [dbo].[FacebookPost] 
(
	[Type] ASC,
	[FacebookUid] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]


END

