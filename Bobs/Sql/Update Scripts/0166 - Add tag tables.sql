IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TagToObject]') AND type in (N'U'))
DROP TABLE [dbo].[TagToObject]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TagPhoto]') AND type in (N'U')) BEGIN
	DROP TABLE TagPhoto
END
GO
	CREATE TABLE [dbo].[TagPhoto](
		[K] [int] IDENTITY(1,1) NOT NULL,
		[TagK] [int] NOT NULL,
		[PhotoK] [int] NOT NULL,
		[UsrK] [int] NOT NULL,
		[DateTime] [DateTime] NOT NULL,
		[Disabled] [bit] NOT NULL CONSTRAINT [DF_Tag_Disabled]  DEFAULT ((0)),
		[DisabledByUsrK] [int] NULL,
		[DisabledDateTime] [DateTime]  NULL,
		[ReenabledByUsrK] [bit] NULL,
		[ReenabledDateTime] [DateTime] NULL
	 CONSTRAINT [PK_TagPhoto] PRIMARY KEY NONCLUSTERED 
	(
		[K] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	CREATE UNIQUE CLUSTERED INDEX IX_TagPhoto_TagKPhotoK ON dbo.TagPhoto
		(
		TagK,
		PhotoK
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX IX_TagPhoto_PhotoK ON dbo.TagPhoto
		(
		PhotoK
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'K'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'the k of the tag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'TagK'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'the k of the photo that is tagged' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'PhotoK'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The K of the usr who tagged the photo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'UsrK'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indicates that a tag has been removed from an object' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'Disabled'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The usr who removed the tag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'DisabledByUsrK'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When the tag was disabled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'DisabledDateTime'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When the tag was re-enabled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'ReenabledDateTime'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The usr who re-enabled the tag' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'ReenabledByUsrK'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When the tagging occurred' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'DateTime'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'a table that links tags to photos' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto'

GO

/****** Object:  Table [dbo].[Tag]    Script Date: 01/02/2008 13:08:51 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tag]') AND type in (N'U')) BEGIN
	DROP TABLE Tag
END
GO

	CREATE TABLE [dbo].[Tag](
		[K] [int] IDENTITY(1,1) NOT NULL,
		[TagText] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
		[Blocked] [bit] NOT NULL CONSTRAINT [DF_Tag_Blocked]  DEFAULT ((0)),
		[BlockedByUsrK] [INT] NULL,
		[BlockedDateTime] [DateTime] 
	 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
	(
		[K] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	CREATE UNIQUE NONCLUSTERED INDEX IX_TagText ON dbo.Tag
	(
	TagText
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tag', @level2type=N'COLUMN',@level2name=N'K'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The actual tag itself' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tag', @level2type=N'COLUMN',@level2name=N'TagText'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Used to block offensive terms' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tag', @level2type=N'COLUMN',@level2name=N'Blocked'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Who blocked it' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tag', @level2type=N'COLUMN',@level2name=N'BlockedByUsrK'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'When it was blocked' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tag', @level2type=N'COLUMN',@level2name=N'BlockedDateTime'
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tag definitions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tag'
	
GO

IF NOT EXISTS(SELECT value FROM ::fn_listextendedproperty ('CausesInvalidation', 'SCHEMA','dbo', 'TABLE','Photo', 'COLUMN','Status') WHERE value = 'true')
BEGIN
EXEC sys.sp_addextendedproperty @name=N'CausesInvalidation',@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Photo', @level2type=N'COLUMN',@level2name=N'Status', @value=N'true'
END
GO
IF NOT EXISTS(SELECT value FROM ::fn_listextendedproperty ('CausesInvalidation', 'SCHEMA','dbo', 'TABLE','TagPhoto', 'COLUMN','Disabled') WHERE value = 'true')
BEGIN
EXEC sys.sp_addextendedproperty @name=N'CausesInvalidation',@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TagPhoto', @level2type=N'COLUMN',@level2name=N'Disabled', @value=N'true'
END
GO
IF NOT EXISTS(SELECT value FROM ::fn_listextendedproperty ('CausesInvalidation', 'SCHEMA','dbo', 'TABLE','Tag', 'COLUMN','Blocked') WHERE value = 'true')
BEGIN
EXEC sys.sp_addextendedproperty @name=N'CausesInvalidation',@level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tag', @level2type=N'COLUMN',@level2name=N'Blocked', @value=N'true'
END
GO
