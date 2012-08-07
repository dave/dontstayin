
IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'AverageCommentDateTimeShallow')and((tbl.name=N'Article' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'AverageCommentDateTimeShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'LastPostShallow')and((tbl.name=N'Article' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'LastPostShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalCommentsShallow')and((tbl.name=N'Article' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Article', N'COLUMN', N'TotalCommentsShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'DateLastHit')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'DateLastHit'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'HitsToday')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'HitsToday'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'HitsTodayNormalised')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'HitsTodayNormalised'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalClicks')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalClicks'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalClicksMusicMatch')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalClicksMusicMatch'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalClicksPlaceMatch')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalClicksPlaceMatch'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalHits')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHits'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalHitsMusicMatch')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHitsMusicMatch'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalHitsPlaceMatch')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHitsPlaceMatch'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalHitsTargetMatch')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHitsTargetMatch'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'Weight')and((tbl.name=N'Banner' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'Weight'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomain')and((tbl.name=N'Brand' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomain'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomainExclude')and((tbl.name=N'Brand' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomainExclude'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomainInclude')and((tbl.name=N'Brand' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomainInclude'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomainRating')and((tbl.name=N'Brand' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Brand', N'COLUMN', N'TicketsDomainRating'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'AverageCommentDateTimeShallow')and((tbl.name=N'Event' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'AverageCommentDateTimeShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'LastPostShallow')and((tbl.name=N'Event' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'LastPostShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'MustBuyTicket')and((tbl.name=N'Event' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'MustBuyTicket'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalCommentsShallow')and((tbl.name=N'Event' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Event', N'COLUMN', N'TotalCommentsShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'HideWhenRead')and((tbl.name=N'GroupUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'GroupUsr', N'COLUMN', N'HideWhenRead'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'InviteMessage1')and((tbl.name=N'GroupUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'GroupUsr', N'COLUMN', N'InviteMessage1'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'InviteMessage2')and((tbl.name=N'GroupUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'GroupUsr', N'COLUMN', N'InviteMessage2'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'NewCommentCount')and((tbl.name=N'GroupUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'GroupUsr', N'COLUMN', N'NewCommentCount'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'NewNewsCount')and((tbl.name=N'GroupUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'GroupUsr', N'COLUMN', N'NewNewsCount'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'NewThreadCount')and((tbl.name=N'GroupUsr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'GroupUsr', N'COLUMN', N'NewThreadCount'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'SqlServerDateTime')and((tbl.name=N'LogPageTime' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'SqlServerDateTime'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'GuestClientK')and((tbl.name=N'Mobile' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Mobile', N'COLUMN', N'GuestClientK'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'NextPhoto1Icon')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'NextPhoto1Icon'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'NextPhoto2Icon')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'NextPhoto2Icon'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'NextPhoto3Icon')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'NextPhoto3Icon'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'PreviousPhoto1Icon')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'PreviousPhoto1Icon'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'PreviousPhoto2Icon')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'PreviousPhoto2Icon'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'PreviousPhoto3Icon')and((tbl.name=N'Photo' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Photo', N'COLUMN', N'PreviousPhoto3Icon'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'AverageCommentDateTimeShallow')and((tbl.name=N'Place' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Place', N'COLUMN', N'AverageCommentDateTimeShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'LastPostShallow')and((tbl.name=N'Place' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Place', N'COLUMN', N'LastPostShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalCommentsShallow')and((tbl.name=N'Place' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Place', N'COLUMN', N'TotalCommentsShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'HasPage')and((tbl.name=N'Promoter' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'HasPage'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'SqlServerDateTime')and((tbl.name=N'SpottedException' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'SqlServerDateTime'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'NewsLevelSuggested')and((tbl.name=N'Thread' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Thread', N'COLUMN', N'NewsLevelSuggested'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'RegionK')and((tbl.name=N'Thread' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Thread', N'COLUMN', N'RegionK'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'EmailVerifyString')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'EmailVerifyString'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'HasDonated')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'HasDonated'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'Introductions')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'Introductions'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'LegalDrinkingAgeDateTime')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'LegalDrinkingAgeDateTime'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'Link')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'Link'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'ModerationItems')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'ModerationItems'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'UpdateError')and((tbl.name=N'Usr' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'UpdateError'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'AverageCommentDateTimeShallow')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'AverageCommentDateTimeShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'FeatureButtonUrl')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'FeatureButtonUrl'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'FeatureExpires')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'FeatureExpires'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'HasFeature')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'HasFeature'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'LastPostShallow')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'LastPostShallow'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomain')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomain'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomainExclude')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomainExclude'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomainInclude')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomainInclude'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TicketsDomainRating')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TicketsDomainRating'
END	

IF EXISTS(SELECT * FROM	
sys.tables AS tbl	
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id	
INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1	
WHERE	
(p.name=N'MS_Description')and((clmns.name=N'TotalCommentsShallow')and((tbl.name=N'Venue' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))	
BEGIN	
	EXECUTE sp_DropExtendedProperty N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Venue', N'COLUMN', N'TotalCommentsShallow'
END	
