DELETE FROM [Group]
TRUNCATE TABLE [Group]
SET IDENTITY_INSERT [Group] ON



INSERT INTO [Group] (
[K],[Name],[Description],[LongDescriptionHtml],[LongDescriptionPlain],[PostingRules],[DateTimeCreated],[TotalMembers],[TotalModerators],[TotalOwners],[TotalComments],
[LastPost],[AverageCommentDateTime],[PrivateGroupPage],[PrivateChat],[PrivateMemberList],[Restriction],[CustomRestrictionType],
[ThemeK],[CountryK],[PlaceK],[MusicTypeK],[BrandK],
[UrlName],[Pic],[PicState],[PicPhotoK],[PicMiscK],[DuplicateGuid],[EmailOnAllThreads],[FavouriteCount],[WatchCount])VALUES
(1,'Hed Kandi','Hed Kandi regulars group for discussing Hed Kandi parties',' ',0,'Discussions about Hed Kandi parties only. Other topics may be deleted by group moderators.','Aug  3 2005 12:03:55:483AM',1454,7,7,4056,
'Sep 13 2007  8:01:31:483PM','Jun 23 2006  3:20:10:373PM',0,0,0,1,0,1,0,0,0,1,
'parties/hed-kandi','2F0BE35F-5717-4120-A62E-B9F7910762A0','',0,0,'44F7CAFF-7395-4EEC-AA32-389C1E2C4A08',0,138,0)
                      
SET IDENTITY_INSERT [Group] OFF
