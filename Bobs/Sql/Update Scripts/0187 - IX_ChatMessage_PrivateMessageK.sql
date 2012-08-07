IF NOT EXISTS(
	SELECT * FROM sys.indexes i
	INNER JOIN sys.tables t ON i.object_id = t.object_id 
	WHERE i.name  ='IX_ChatMessage_PrivateThreadK' AND t.name = 'ChatMessage'
) BEGIN

	CREATE NONCLUSTERED INDEX [IX_ChatMessage_PrivateThreadK] ON [dbo].[ChatMessage] 
	(
		[PrivateThreadK] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
END
