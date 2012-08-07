
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookUID'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookUID bigint NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook user id', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookUID'

END


IF NOT EXISTS(
	SELECT * FROM sys.indexes i
	INNER JOIN sys.tables t ON i.object_id = t.object_id 
	WHERE i.name  ='IX_Usr_FacebookUID' AND t.name = 'Usr'
) BEGIN

	CREATE NONCLUSTERED INDEX [IX_Usr_FacebookUID] ON [dbo].[Usr] 
(
	[FacebookUID] ASC
	
)WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = OFF) ON [PRIMARY]

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookConnected'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookConnected bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook connect linked', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookConnected'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookConnectedDateTime'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookConnectedDateTime datetime NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook connect linked date/time', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookConnectedDateTime'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookPermissionEmail'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookPermissionEmail bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook email extended permission', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookPermissionEmail'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookPermissionPublish'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookPermissionPublish bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook publish_stream extended permission', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookPermissionPublish'

END


IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookPermissionEvent'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookPermissionEvent bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook create_event extended permission', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookPermissionEvent'

END



IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'FacebookPermissionRsvp'
) BEGIN

ALTER TABLE dbo.Usr ADD
	FacebookPermissionRsvp bit NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Facebook rsvp_event extended permission', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookPermissionRsvp'

END



IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryAttendEvent')    BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryAttendEvent    bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I attend events',                           N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryAttendEvent'    END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryBuyTicket')      BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryBuyTicket      bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I buy tickets',                             N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryBuyTicket'      END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryUploadPhoto')    BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryUploadPhoto    bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I upload photos',                           N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryUploadPhoto'    END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStorySpotted')        BEGIN ALTER TABLE dbo.Usr ADD FacebookStorySpotted        bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I get spotted in photos',                   N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStorySpotted'        END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryPhotoFeatured')  BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryPhotoFeatured  bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I have a photo featured on the front page', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryPhotoFeatured'  END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryNewBuddy')       BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryNewBuddy       bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I make a new buddy',                        N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryNewBuddy'       END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryPublishArticle') BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryPublishArticle bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I publish an article',                      N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryPublishArticle' END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryJoinGroup')      BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryJoinGroup      bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I join a group',                            N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryJoinGroup'      END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryFavourite')      BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryFavourite      bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I put stuff on my favourites',              N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryFavourite'      END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryNewTopic')       BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryNewTopic       bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I post new topics',                         N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryNewTopic'       END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryEventReview')    BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryEventReview    bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I post an event review',                    N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryEventReview'    END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryPostNews')       BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryPostNews       bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I post news',                               N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryPostNews'       END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookStoryLaugh')          BEGIN ALTER TABLE dbo.Usr ADD FacebookStoryLaugh          bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Post a facebook stream story when I laugh at a comment',                      N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookStoryLaugh'          END

IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookEventAdd')            BEGIN ALTER TABLE dbo.Usr ADD FacebookEventAdd            bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Add an event to facebook when I add an event',                                N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookEventAdd'            END
IF NOT EXISTS(SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id WHERE [table].name = 'Usr' AND	[column].name = 'FacebookEventAttend')         BEGIN ALTER TABLE dbo.Usr ADD FacebookEventAttend         bit NOT NULL DEFAULT 1 EXECUTE sp_addextendedproperty N'MS_Description', N'Add me on Facebook when I attend an event',	                                  N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'FacebookEventAttend'         END
