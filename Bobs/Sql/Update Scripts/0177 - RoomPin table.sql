IF EXISTS(
	SELECT * FROM sys.tables WHERE Name = 'RoomPin' 
) BEGIN
	drop table dbo.RoomPin
END

GO

create TABLE dbo.RoomPin
(
	UsrK int not null,
	RoomGuid uniqueidentifier not null,
	DateTime dateTime,
	ListOrder int,
	Pinned bit,
	Expires bit,
	DateTimeExpires datetime
)
ALTER TABLE dbo.RoomPin ADD CONSTRAINT
	PK_RoomPin PRIMARY KEY CLUSTERED 
	(
		UsrK, RoomGuid
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

EXECUTE sp_addextendedproperty N'MS_Description', N'Chat rooms pinned by users', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', NULL, NULL

EXECUTE sp_addextendedproperty N'MS_Description', N'Usr', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'UsrK'
EXECUTE sp_addextendedproperty N'MS_Description', N'Room', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'RoomGuid'
EXECUTE sp_addextendedproperty N'MS_Description', N'Date/time the room was last pinned', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'DateTime'
EXECUTE sp_addextendedproperty N'MS_Description', N'Order in this users list', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'ListOrder'
EXECUTE sp_addextendedproperty N'MS_Description', N'Set to false if the room is un-pinned', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'Pinned'
EXECUTE sp_addextendedproperty N'MS_Description', N'True if the pinned room expires', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'Expires'
EXECUTE sp_addextendedproperty N'MS_Description', N'If the pinned room expires, this is the expiry time', N'SCHEMA', N'dbo', N'TABLE', N'RoomPin', N'COLUMN', N'DateTimeExpires'

GO

