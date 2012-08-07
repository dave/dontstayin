﻿IF NOT EXISTS(
	SELECT * FROM sys.indexes i
	INNER JOIN sys.tables t ON i.object_id = t.object_id 
	WHERE i.name  ='IX_RoomPin_RoomGuid_Pinned' AND t.name = 'RoomPin'
) BEGIN

	CREATE NONCLUSTERED INDEX [IX_RoomPin_RoomGuid_Pinned] ON [dbo].[RoomPin] 
(
	[RoomGuid] ASC,
	[Pinned] DESC
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]
END