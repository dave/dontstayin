DELETE FROM [Group] WHERE k = 13195
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND name = N'Group89')
DROP INDEX [Group89] ON [dbo].[Group] WITH ( ONLINE = OFF )
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND name = N'Group69')
DROP INDEX [Group69] ON [dbo].[Group] WITH ( ONLINE = OFF )
GO
GO
/****** Object:  Statistic [_dta_stat_1433772165_23_14_11_8_25_21_1]    Script Date: 10/30/2008 17:15:17 ******/
if  exists (select * from sys.stats where name = N'_dta_stat_1433772165_23_14_11_8_25_21_1' and object_id = object_id(N'[dbo].[Group]'))
DROP STATISTICS [dbo].[Group].[_dta_stat_1433772165_23_14_11_8_25_21_1]
GO
/****** Object:  Statistic [_dta_stat_1433772165_23_14_11_8_25_1]    Script Date: 10/30/2008 17:14:54 ******/
if  exists (select * from sys.stats where name = N'_dta_stat_1433772165_23_14_11_8_25_1' and object_id = object_id(N'[dbo].[Group]'))
	DROP STATISTICS [dbo].[Group].[_dta_stat_1433772165_23_14_11_8_25_1]
GO
UPDATE [Group] SET [DuplicateGuid] = '00000000-0000-0000-0000-000000000000' WHERE [DuplicateGuid] is null
GO
ALTER TABLE [GROUP]
	ALTER COLUMN [DuplicateGuid] UNIQUEIDENTIFIER NOT NULL 
GO
IF NOT EXISTS( SELECT * FROM sysobjects WHERE Name = 'DF_Group_DuplicateGuid')
ALTER TABLE dbo.[Group] ADD CONSTRAINT
	DF_Group_DuplicateGuid DEFAULT '00000000-0000-0000-0000-000000000000' FOR DuplicateGuid
GO
UPDATE [Group] SET [Pic] = '00000000-0000-0000-0000-000000000000' WHERE [Pic] is null
GO
ALTER TABLE [GROUP]
	ALTER COLUMN [Pic] UNIQUEIDENTIFIER NOT NULL 
GO
IF NOT EXISTS( SELECT * FROM sysobjects WHERE Name = 'DF_Group_Pic')
ALTER TABLE dbo.[Group] ADD CONSTRAINT
	DF_Group_Pic DEFAULT '00000000-0000-0000-0000-000000000000' FOR Pic
GO
/****** Object:  Index [Group89]    Script Date: 10/30/2008 17:13:54 ******/
CREATE NONCLUSTERED INDEX [Group89] ON [dbo].[Group] 
(
	[K] ASC,
	[Pic] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
	

GO
/****** Object:  Index [Group69]    Script Date: 10/30/2008 17:14:31 ******/
CREATE NONCLUSTERED INDEX [Group69] ON [dbo].[Group] 
(
	[BrandK] ASC,
	[K] ASC,
	[Name] ASC,
	[Description] ASC,
	[TotalMembers] ASC,
	[TotalComments] ASC,
	[PrivateGroupPage] ASC,
	[CountryK] ASC,
	[UrlName] ASC,
	[Pic] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
