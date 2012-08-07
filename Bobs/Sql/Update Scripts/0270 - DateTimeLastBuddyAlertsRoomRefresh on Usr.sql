IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'DateTimeLastBuddyAlertsRoomRefresh'
) BEGIN

ALTER TABLE dbo.Usr ADD
	DateTimeLastBuddyAlertsRoomRefresh datetime NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Date / time that the buddy alerts room was last refreshed', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'DateTimeLastBuddyAlertsRoomRefresh'

END

