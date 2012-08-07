
DELETE FROM BannerFolder
TRUNCATE TABLE [BannerFolder]
SET IDENTITY_INSERT [BannerFolder] ON
--																				 K, [Name],			[PromoterK], [DateTimeCreated]
INSERT INTO [BannerFolder] ([K], [Name], [PromoterK], [DateTimeCreated]) VALUES (1, 'BannerFolder1', 1,          '1 Jan 2001')
INSERT INTO [BannerFolder] ([K], [Name], [PromoterK], [DateTimeCreated]) VALUES (2, 'BannerFolder2', 1,          '2 Jan 2001')
INSERT INTO [BannerFolder] ([K], [Name], [PromoterK], [DateTimeCreated]) VALUES (3, 'BannerFolder3', 2,          '3 Jan 2001')
INSERT INTO [BannerFolder] ([K], [Name], [PromoterK], [DateTimeCreated]) VALUES (4, 'BannerFolder4', 2,          '4 Jan 2001')
INSERT INTO [BannerFolder] ([K], [Name], [PromoterK], [DateTimeCreated]) VALUES (5, 'BannerFolder5', 2,          '5 Jan 2001')


SET IDENTITY_INSERT [BannerFolder] OFF
